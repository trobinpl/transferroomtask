using System.Text.Json.Serialization;

namespace TheSportsDb.Responses;

public class SearchPlayerResponse
{
    [JsonPropertyName("player")]
    public List<SinglePlayerResponse>? Players { get; set; }
}

public class SinglePlayerResponse
{
    [JsonPropertyName("idPlayer")]
    public string? Id { get; set; }

    [JsonPropertyName("strThumb")]
    public string? Thumbnail { get; set; }

    [JsonPropertyName("strCutout")]
    public string? Portrait { get; set; }
}
