namespace FootballDataOrg;

public class FootballDataOrgApiOptions
{
    public const string SectionName = "FootballDataApi";

    public string BaseUrl { get; set; } = "https://api.football-data.org/v4/";
    public string ApiKey { get; set; } = string.Empty;
    public int TimeoutSeconds { get; set; } = 30;
    public int RetryCount { get; set; } = 3;
    public int RetryDelaySeconds { get; set; } = 2;
}
