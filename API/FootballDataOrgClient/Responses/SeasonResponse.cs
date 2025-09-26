namespace FootballDataOrg.Responses;

public class SeasonResponse
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int CurrentMatchday { get; set; }
}
