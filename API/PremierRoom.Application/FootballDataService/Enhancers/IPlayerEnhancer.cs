using PremierRoom.Application.Models;

namespace PremierRoom.Application.FootballDataService.Enhancers;

public interface IPlayerEnhancer
{
    public Task Enhance(Player player, CancellationToken cancellationToken = default);
}
