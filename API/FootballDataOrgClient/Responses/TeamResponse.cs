namespace FootballDataOrgProvider.Responses;

public class TeamResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string Tla { get; set; } = string.Empty;
    public string CrestUrl { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public int Founded { get; set; }
    public string ClubColors { get; set; } = string.Empty;
    public string Venue { get; set; } = string.Empty;
    public List<PlayerResponse> Squad { get; set; } = [];
}
