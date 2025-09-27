using Microsoft.Extensions.Caching.Memory;
using PremierRoom.Application.Models;

namespace PremierRoom.Application.FootballDataService.Enhancers.PlayerEnhancers.ProfilePicture;

public abstract class ProfilePictureEnhancer : IEnhancer<Player>
{
    private readonly IMemoryCache _memoryCache;

    public ProfilePictureEnhancer(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public abstract Task<string> GetProfilePictureUrl(string playerName, CancellationToken cancellationToken = default);

    public async Task EnhanceAsync(Player player, CancellationToken cancellationToken = default)
    {
        string playerName = $"{player.Firstname} {player.Surname}";

        var profilePicture = await _memoryCache.GetOrCreateAsync(new PlayerProfilePictureCacheKey(playerName), async (cacheEntry) =>
        {
            cacheEntry.SlidingExpiration = TimeSpan.FromSeconds(60);
            return await GetProfilePictureUrl(playerName, cancellationToken);
        }) ?? string.Empty;

        player.ProfilePicture = profilePicture;
    }
}
