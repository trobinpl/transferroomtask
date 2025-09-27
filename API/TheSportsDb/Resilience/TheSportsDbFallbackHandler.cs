using Microsoft.Extensions.Logging;
using Polly;
using Polly.Fallback;
using System.Net;

namespace TheSportsDb.Resilience;

internal class TheSportsDbFallbackHandler : DelegatingHandler
{
    private readonly ILogger<TheSportsDbFallbackHandler> _logger;

    public TheSportsDbFallbackHandler(ILogger<TheSportsDbFallbackHandler> logger)
    {
        _logger = logger;
    }

    private static AsyncFallbackPolicy<HttpResponseMessage> CreateFallbackPolicy() => Policy
            .HandleResult<HttpResponseMessage>(response => response.StatusCode == HttpStatusCode.TooManyRequests)
            .Or<HttpRequestException>()
            .Or<TaskCanceledException>()
            .FallbackAsync(TheSportsDbFallbackData.CreateDummyResponse("Unknown"));

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var fallbackPolicy = CreateFallbackPolicy();

        // Execute the request with fallback policy
        return await fallbackPolicy.ExecuteAsync(async (ct) =>
        {
            _logger.LogDebug("Executing TheSportsDB request: {Uri}", request.RequestUri);
            return await base.SendAsync(request, ct);
        }, cancellationToken);
    }
}
