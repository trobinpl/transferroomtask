namespace TheSportsDb;

internal class TheSportsDbApiClientOptions
{
    public const string SectionName = "TheSportsDb";

    public string BaseUrl { get; set; } = "https://www.thesportsdb.com/api/v1/json/123/";
    public int TimeoutSeconds { get; set; } = 30;
    public int RetryCount { get; set; } = 3;
    public int RetryDelaySeconds { get; set; } = 2;
    public string UserAgent { get; set; } = "PremierRoom/1.0";
}