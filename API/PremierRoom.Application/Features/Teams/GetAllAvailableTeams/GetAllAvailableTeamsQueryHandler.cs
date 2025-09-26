using PremierRoom.Application.FootballDataService;
using PremierRoom.Application.Models;

namespace PremierRoom.Application.Features.Teams.GetAllAvailableTeams;

public class GetAllAvailableTeamsQueryHandler : IQueryHandler<GetAllAvailableTeamsQuery, IEnumerable<Team>>
{
    private readonly IFootballDataService _footballDataService;

    public GetAllAvailableTeamsQueryHandler(IFootballDataService footballDataService)
    {
        _footballDataService = footballDataService;
    }

    public async Task<IEnumerable<Team>> HandleAsync(GetAllAvailableTeamsQuery query, CancellationToken cancellationToken = default)
    {
        var premiereLeagueTeams = await _footballDataService.GetPremierLeagueTeamsAsync(cancellationToken);

        return premiereLeagueTeams;
    }
}
