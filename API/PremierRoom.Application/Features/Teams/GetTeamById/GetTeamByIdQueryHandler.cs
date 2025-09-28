using OneOf;
using PremierRoom.Application.Features.Teams.GetTeamById.Results;
using PremierRoom.Application.FootballDataService;
using PremierRoom.Application.FootballDataService.Enhancers;
using PremierRoom.Application.Models;

namespace PremierRoom.Application.Features.Teams.GetTeamById;

public class GetTeamByIdQueryHandler : IQueryHandler<GetTeamByIdQuery, OneOf<Team, SpecifiedTeamNotFound>>
{
    private readonly IFootballDataService _footballDataService;
    private readonly IEnumerable<IEnhancer<Team>> _teamEnhancers;
    private readonly IEnumerable<IEnhancer<Player>> _playerEnhancers;

    public GetTeamByIdQueryHandler(IFootballDataService footballDataService, IEnumerable<IEnhancer<Team>> teamEnhancers, IEnumerable<IEnhancer<Player>> playerEnhancers)
    {
        _footballDataService = footballDataService;
        _teamEnhancers = teamEnhancers;
        _playerEnhancers = playerEnhancers;
    }

    public async Task<OneOf<Team, SpecifiedTeamNotFound>> HandleAsync(GetTeamByIdQuery query, CancellationToken cancellationToken = default)
    {
        var team = await _footballDataService.GetTeamByIdAsync(query.TeamId, cancellationToken);

        if (team is null)
        {
            return new SpecifiedTeamNotFound();
        }

        foreach (var teamEnhancer in _teamEnhancers)
        {
            await teamEnhancer.EnhanceAsync(team, cancellationToken);
        }

        await Parallel.ForEachAsync(team.Squad, async (player, cancellationToken) =>
        {
            foreach (var playerEnhancer in _playerEnhancers)
            {
                await playerEnhancer.EnhanceAsync(player, cancellationToken);
            }
        });

        return team;
    }
}
