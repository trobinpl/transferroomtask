using PremierRoom.Application.Models;

namespace PremierRoom.Application.FootballDataService.Enhancers.PlayerEnhancers;

public interface IPlayerEnhancer
{
    public Task Enhance(Player player, CancellationToken cancellationToken = default);
}
