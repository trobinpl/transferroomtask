using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;
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

    public async Task<SearchTeamResponse> SearchTeams(string teamName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(teamName))
        {
            throw new ArgumentException("Team name cannot be null or empty", nameof(teamName));
        }

        try
        {
            _logger.LogInformation("Looking for team {TeamName} in TheSportsDB API", teamName);

            var response = await _httpClient.GetAsync($"searchteams.php?t={teamName}", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync(cancellationToken);
                var searchTeamResponse = JsonSerializer.Deserialize<SearchTeamResponse>(json, GetJsonOptions());

                _logger.LogInformation("Successfull search for team {TeamName}", teamName);
                return searchTeamResponse ?? new SearchTeamResponse { Teams = [] };
            }

            _logger.LogWarning("Failed to search for team {TeamName}. Status: {StatusCode}", teamName, response.StatusCode);
            throw new HttpRequestException($"API request failed with status {response.StatusCode}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching for team {TeamName}", teamName);
            throw;
        }
    }

    private static JsonSerializerOptions GetJsonOptions() => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}
