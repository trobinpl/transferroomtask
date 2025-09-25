namespace FootballDataOrgProvider.Responses;

public class TeamsResponse
{
    public int Count { get; set; }
    public CompetitionResponse Competition { get; set; } = new();
    public SeasonResponse Season { get; set; } = new();
    public List<TeamResponse> Teams { get; set; } = [];
}
