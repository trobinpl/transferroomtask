using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace FootballDataOrg;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFootballDataApi(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure options
        services.Configure<FootballDataApiOptions>(configuration.GetSection(FootballDataApiOptions.SectionName));

        // Register HttpClient with all configurations
        services.AddHttpClient<FootballDataOrgClient>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<FootballDataApiOptions>>().Value;

            // Configure base address
            client.BaseAddress = new Uri(options.BaseUrl);

            // Configure timeout
            client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);

            // Set authentication header
            client.DefaultRequestHeaders.Add("X-Auth-Token", options.ApiKey);

            // Set User-Agent header (good practice for APIs)
            client.DefaultRequestHeaders.UserAgent.ParseAdd("TransferRoomApp/1.0");

            // Set Accept header
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        return services;
    }

    //private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    //{
    //    return HttpPolicyExtensions
    //        .HandleTransientHttpError()
    //        .OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
    //        .WaitAndRetryAsync(
    //            retryCount: 3,
    //            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
    //            onRetry: (outcome, timespan, retryCount, context) =>
    //            {
    //                var logger = context.GetLogger();
    //                logger?.LogWarning("Retry {RetryCount} after {Delay}ms due to {Result}",
    //                    retryCount, timespan.TotalMilliseconds, outcome.Result?.StatusCode);
    //            });
    //}

    //private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    //{
    //    return HttpPolicyExtensions
    //        .HandleTransientHttpError()
    //        .CircuitBreakerAsync(
    //            handledEventsAllowedBeforeBreaking: 5,
    //            durationOfBreak: TimeSpan.FromSeconds(30),
    //            onBreak: (result, duration) =>
    //            {
    //                // Log circuit breaker opened
    //            },
    //            onReset: () =>
    //            {
    //                // Log circuit breaker closed
    //            });
    //}
}
