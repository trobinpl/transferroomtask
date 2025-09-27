using Microsoft.Extensions.Logging;
using Polly;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using TheSportsDb.Resilience;
using TheSportsDb.Responses;

namespace TheSportsDb;

public class TheSportsDbApiClient : ITheSportsDbApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<TheSportsDbApiClient> _logger;

    public TheSportsDbApiClient(HttpClient httpClient, ILogger<TheSportsDbApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<SearchPlayerResponse> SearchPlayers(string playerName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(playerName))
        {
            throw new ArgumentException("Player name cannot be null or empty", nameof(playerName));
        }

        try
        {
            _logger.LogInformation("Looking for player {PlayerName} in TheSportsDB API", playerName);

            var response = await _httpClient.GetAsync($"searchplayers.php?p={playerName}", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync(cancellationToken);
                var searchPlayerResponse = JsonSerializer.Deserialize<SearchPlayerResponse>(json, GetJsonOptions());

                _logger.LogInformation("Successfull search for player {PlayerName}", playerName);
                return searchPlayerResponse ?? new SearchPlayerResponse { Players = [] };
            }

            _logger.LogWarning("Failed to search for player {PlayerName}. Status: {StatusCode}", playerName, response.StatusCode);
            throw new HttpRequestException($"API request failed with status {response.StatusCode}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching for player {PlayerName}", playerName);
            throw;
        }
    }

    private IAsyncPolicy<HttpResponseMessage> CreateFallbackPolicy()
    {
        return Policy
            .HandleResult<HttpResponseMessage>(response => response.StatusCode == HttpStatusCode.TooManyRequests) // Handle 429
            .FallbackAsync(
                // Fallback action - create dummy response
                fallbackAction: (context, cancellationToken) =>
                {
                    var playerName = ExtractPlayerNameFromContext(context);

                    _logger.LogWarning(
                        "TheSportsDB API unavailable (429 or error). Returning fallback data for player: {PlayerName}",
                        playerName);

                    var fallbackResponse = TheSportsDbFallbackData.CreateDummyResponse(playerName);

                    return Task.FromResult(fallbackResponse);
                },
                // OnFallback callback
                onFallbackAsync: (result, context) =>
                {
                    var playerName = ExtractPlayerNameFromContext(context);

                    if (result.Result?.StatusCode == HttpStatusCode.TooManyRequests)
                    {
                        var retryAfter = result.Result.Headers.RetryAfter?.Delta?.TotalSeconds ?? 60;

                        _logger.LogWarning("Rate limit exceeded (429) for player '{PlayerName}'. Retry after {RetryAfter} seconds. Using fallback data.", playerName, retryAfter);
                    }
                    else if (result.Exception != null)
                    {
                        _logger.LogWarning(result.Exception,
                            "Network error for player '{PlayerName}'. Using fallback data.", playerName);
                    }

                    return Task.CompletedTask;
                });
    }

    private static string ExtractPlayerNameFromContext(Context context)
    {
        // Try to get player name from context, fallback to "Unknown Player"
        return context.TryGetValue("PlayerName", out var playerNameObj) &&
               playerNameObj is string playerName
            ? playerName
            : "Unknown Player";
    }

    private static JsonSerializerOptions GetJsonOptions() => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}
