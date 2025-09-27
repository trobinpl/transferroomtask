using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using Polly.Retry;
using System.Net;
using System.Net.Http.Headers;
using TheSportsDb.Resilience;

namespace TheSportsDb;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTheSportsDbApi(this IServiceCollection services, IConfiguration configuration)
    {
        var configurationSection = configuration.GetSection(TheSportsDbApiClientOptions.SectionName);
        services.Configure<TheSportsDbApiClientOptions>(configurationSection);

        services.AddTransient<TheSportsDbFallbackHandler>();

        services.AddHttpClient<ITheSportsDbApiClient, TheSportsDbApiClient>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<TheSportsDbApiClientOptions>>().Value;

            client.BaseAddress = new Uri(options.BaseUrl);

            client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);

            client.DefaultRequestHeaders.UserAgent.ParseAdd(options.UserAgent);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        })
        .AddHttpMessageHandler<TheSportsDbFallbackHandler>()
        .AddPolicyHandler(GetRetryPolicy(configurationSection.Get<TheSportsDbApiClientOptions>() ?? new TheSportsDbApiClientOptions()))
        .AddPolicyHandler(GetCircuitBreakerPolicy());

        return services;
    }

    private static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy(TheSportsDbApiClientOptions options)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(retryCount: options.RetryCount, sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(options.RetryDelaySeconds, retryAttempt)));
    }

    private static AsyncCircuitBreakerPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 5, durationOfBreak: TimeSpan.FromSeconds(30));
    }


}
