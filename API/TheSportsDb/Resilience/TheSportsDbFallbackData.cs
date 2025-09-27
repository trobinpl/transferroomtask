using System.Net;
using System.Text;
using System.Text.Json;
using TheSportsDb.Responses;

namespace TheSportsDb.Resilience;
public static class TheSportsDbFallbackData
{
    public static HttpResponseMessage CreateDummyResponse(string playerName)
    {
        var dummyResponse = new SearchPlayerResponse
        {
            Players =
            [
                new SinglePlayerResponse
                {
                    Id = $"{playerName}-{Guid.NewGuid()}",
                    Portrait = "unknown",
                    Thumbnail = "unknown"
                }
            ]
        };

        var jsonContent = JsonSerializer.Serialize(dummyResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
        };

        // Add special headers to indicate this is a fallback response
        response.Headers.Add("X-Fallback-Response", "true");
        response.Headers.Add("X-Fallback-Reason", "rate-limit-429");
        response.Headers.Add("X-Original-Request-Time", DateTimeOffset.UtcNow.ToString("O"));

        return response;
    }
}
