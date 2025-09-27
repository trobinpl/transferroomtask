using Microsoft.Extensions.Caching.Memory;
using TheSportsDb;

namespace PremierRoom.Application.FootballDataService.Enhancers.PlayerEnhancers.ProfilePicture;

public class TheSportsDbProfilePictureEnhancer : ProfilePictureEnhancer
{
    private readonly ITheSportsDbApiClient _theSportsDbApiClient;

    public TheSportsDbProfilePictureEnhancer(IMemoryCache memoryCache, ITheSportsDbApiClient theSportsDbApiClient) : base(memoryCache)
    {
        _theSportsDbApiClient = theSportsDbApiClient;
    }

    public override async Task<string> GetProfilePictureUrl(string playerName, CancellationToken cancellationToken = default)
    {
        var searchPlayerResult = await _theSportsDbApiClient.SearchPlayers(playerName, cancellationToken);
        var foundPlayer = searchPlayerResult?.Players?.FirstOrDefault();

        if (foundPlayer is null)
        {
            return string.Empty;
        }

        return foundPlayer.Portrait ?? foundPlayer.Thumbnail ?? string.Empty;
    }
}
