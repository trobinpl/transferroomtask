using OneOf;
using PremierRoom.Application.Features.Teams.GetTeamById.Results;
using PremierRoom.Application.FootballDataService;
using PremierRoom.Application.Models;

namespace PremierRoom.Application.Features.Teams.GetTeamById;

public class GetTeamByIdQueryHandler : IQueryHandler<GetTeamByIdQuery, OneOf<Team, SpecifiedTeamNotFound>>
{
    private IFootballDataService _footballDataService;

    public GetTeamByIdQueryHandler(IFootballDataService footballDataService)
    {
        _footballDataService = footballDataService;
    }

    public async Task<OneOf<Team, SpecifiedTeamNotFound>> HandleAsync(GetTeamByIdQuery query, CancellationToken cancellationToken = default)
    {
        var team = await _footballDataService.GetTeamByIdAsync(query.TeamId, cancellationToken);

        if (team is null)
        {
            return new SpecifiedTeamNotFound();
        }

        return team;
    }
}
