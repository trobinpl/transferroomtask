using System.Text.Json.Serialization;

namespace TheSportsDb.Responses;

public class SearchTeamResponse
{
    [JsonPropertyName("teams")]
    public List<SingleTeamResponse>? Teams { get; set; }
}

public class SingleTeamResponse
{
    [JsonPropertyName("strTeam")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("strTeamAlternate")]
    public string AlternateNames { get; set; } = string.Empty;

    [JsonPropertyName("strKeywords")]
    public string Nicknames { get; set; } = string.Empty;
}