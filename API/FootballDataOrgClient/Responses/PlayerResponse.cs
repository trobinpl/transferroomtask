namespace FootballDataOrg.Responses;

public class PlayerResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public string CountryOfBirth { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
}
