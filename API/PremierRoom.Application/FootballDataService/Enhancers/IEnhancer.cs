using PremierRoom.Application.Models;

namespace PremierRoom.Application.FootballDataService.Enhancers;

public interface IEnhancer<T>
{
    public Task EnhanceAsync(T entity, CancellationToken cancellationToken = default);
}
