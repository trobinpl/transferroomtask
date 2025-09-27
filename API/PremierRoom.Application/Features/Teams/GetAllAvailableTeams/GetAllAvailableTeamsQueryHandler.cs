using PremierRoom.Application.FootballDataService;
using PremierRoom.Application.FootballDataService.Enhancers;
using PremierRoom.Application.Models;

namespace PremierRoom.Application.Features.Teams.GetAllAvailableTeams;

public class GetAllAvailableTeamsQueryHandler : IQueryHandler<GetAllAvailableTeamsQuery, IEnumerable<Team>>
{
    private readonly IFootballDataService _footballDataService;
    private readonly IEnumerable<IEnhancer<Team>> _teamEnhancers;

    public GetAllAvailableTeamsQueryHandler(IFootballDataService footballDataService, IEnumerable<IEnhancer<Team>> teamEnhancers)
    {
        _footballDataService = footballDataService;
        _teamEnhancers = teamEnhancers;
    }

    public async Task<IEnumerable<Team>> HandleAsync(GetAllAvailableTeamsQuery query, CancellationToken cancellationToken = default)
    {
        var premiereLeagueTeams = await _footballDataService.GetPremierLeagueTeamsAsync(cancellationToken);

        await Parallel.ForEachAsync(premiereLeagueTeams, async (team, cancellationToken) =>
        {
            foreach (var teamEnhancer in _teamEnhancers)
            {
                await teamEnhancer.EnhanceAsync(team, cancellationToken);
            }
        });

        return premiereLeagueTeams;
    }
}
