using Microsoft.Extensions.Caching.Memory;
using PremierRoom.Application.FootballDataService.FootballDataOrg.Cache.Keyes;
using PremierRoom.Application.Models;

namespace PremierRoom.Application.FootballDataService.FootballDataOrg.Cache;

// Context
// FootballDataOrg API has rate limiting when Free tier is used
// In order to make our application more resilient caching layer is implemented
// to reduce the chance of hitting the rate limit
public class CacheableFootballDataOrgFootballDataService : IFootballDataService
{
    private readonly IFootballDataService _footballDataService;
    private readonly IMemoryCache _memoryCache;

    public CacheableFootballDataOrgFootballDataService(IFootballDataService footballDataService, IMemoryCache memoryCache)
    {
        _footballDataService = footballDataService;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<Team>> GetPremierLeagueTeamsAsync(CancellationToken cancellationToken = default)
    {
        return await _memoryCache.GetOrCreateAsync(new AllTeamsInLeagueCacheKey("PL"), async (cacheEntry) =>
        {
            cacheEntry.SlidingExpiration = TimeSpan.FromSeconds(15);
            return await _footballDataService.GetPremierLeagueTeamsAsync(cancellationToken);
        }) ?? [];
    }

    public async Task<Team?> GetTeamByIdAsync(int teamId, CancellationToken cancellationToken = default)
    {
        return await _memoryCache.GetOrCreateAsync(new GetTeamByIdCacheKey(teamId), async (cacheEntry) =>
        {
            cacheEntry.SlidingExpiration = TimeSpan.FromSeconds(15);
            return await _footballDataService.GetTeamByIdAsync(teamId, cancellationToken);
        });
    }
}
