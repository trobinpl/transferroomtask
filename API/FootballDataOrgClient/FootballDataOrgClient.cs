using FootballDataOrgProvider.Responses;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FootballDataOrg;

public class FootballDataOrgClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<FootballDataOrgClient> _logger;
    private const string PremierLeagueCode = "PL";

    public FootballDataOrgClient(HttpClient httpClient, ILogger<FootballDataOrgClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IEnumerable<TeamResponse>> GetPremierLeagueTeamsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Fetching Premier League teams from Football-Data.org API");

            var response = await _httpClient.GetAsync($"competitions/{PremierLeagueCode}/teams", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync(cancellationToken);
                var teamsResponse = JsonSerializer.Deserialize<TeamsResponse>(json, GetJsonOptions());

                _logger.LogInformation("Successfully fetched {Count} Premier League teams", teamsResponse?.Teams?.Count ?? 0);
                return teamsResponse?.Teams ?? Enumerable.Empty<TeamResponse>();
            }

            _logger.LogWarning("Failed to fetch teams. Status: {StatusCode}", response.StatusCode);
            throw new HttpRequestException($"API request failed with status {response.StatusCode}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching Premier League teams");
            throw;
        }
    }

    public async Task<TeamResponse> GetTeamByIdAsync(int teamId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Fetching team details for ID: {TeamId}", teamId);

            var response = await _httpClient.GetAsync($"teams/{teamId}", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync(cancellationToken);
                var team = JsonSerializer.Deserialize<TeamResponse>(json, GetJsonOptions());

                _logger.LogInformation("Successfully fetched team: {TeamName}", team?.Name);
                return team ?? throw new InvalidOperationException("Team not found");
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException($"Team with ID {teamId} not found");
            }

            throw new HttpRequestException($"API request failed with status {response.StatusCode}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching team with ID: {TeamId}", teamId);
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
