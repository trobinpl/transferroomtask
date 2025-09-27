using OneOf;
using PremierRoom.Application.Features.Teams.GetTeamById.Results;
using PremierRoom.Application.FootballDataService;
using PremierRoom.Application.FootballDataService.Enhancers;
using PremierRoom.Application.Models;

namespace PremierRoom.Application.Features.Teams.GetTeamById;

public class GetTeamByIdQueryHandler : IQueryHandler<GetTeamByIdQuery, OneOf<Team, SpecifiedTeamNotFound>>
{
    private readonly IFootballDataService _footballDataService;
    private readonly IEnumerable<IPlayerEnhancer> _playerEnhancers;

    public GetTeamByIdQueryHandler(IFootballDataService footballDataService, IEnumerable<IPlayerEnhancer> playerEnhancers)
    {
        _footballDataService = footballDataService;
        _playerEnhancers = playerEnhancers;
    }

    public async Task<OneOf<Team, SpecifiedTeamNotFound>> HandleAsync(GetTeamByIdQuery query, CancellationToken cancellationToken = default)
    {
        var team = await _footballDataService.GetTeamByIdAsync(query.TeamId, cancellationToken);

        if (team is null)
        {
            return new SpecifiedTeamNotFound();
        }

        foreach (var player in team.Squad)
        {
            foreach(var playerEnhancer in _playerEnhancers)
            {
                await playerEnhancer.Enhance(player, cancellationToken);
            }
        }

        return team;
    }
}
