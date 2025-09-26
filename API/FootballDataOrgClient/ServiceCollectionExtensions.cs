using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using Polly.Retry;
using System.Net;
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
            client.DefaultRequestHeaders.UserAgent.ParseAdd("PremierRoom/1.0");

            // Set Accept header
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        })
        .AddPolicyHandler(GetRetryPolicy())
        .AddPolicyHandler(GetCircuitBreakerPolicy()); ;

        return services;
    }

    private static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(retryCount: 3, sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    private static AsyncCircuitBreakerPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 5, durationOfBreak: TimeSpan.FromSeconds(30));
    }
}
