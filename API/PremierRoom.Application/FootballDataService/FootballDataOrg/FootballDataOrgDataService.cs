using FootballDataOrg;
using PremierRoom.Application.FootballDataService.FootballDataOrg.Mappers;
using PremierRoom.Application.Models;

namespace PremierRoom.Application.FootballDataService.FootballDataOrg;

public class FootballDataOrgDataService : IFootballDataService
{
    private readonly FootballDataOrgClient _footballDataOrgClient;

    public FootballDataOrgDataService(FootballDataOrgClient footballDataOrgClient)
    {
        _footballDataOrgClient = footballDataOrgClient;
    }

    public async Task<IEnumerable<Team>> GetPremierLeagueTeamsAsync(CancellationToken cancellationToken = default)
    {
        var teams = await _footballDataOrgClient.GetPremierLeagueTeamsAsync(cancellationToken);

        return teams.Select(t => t.FromApiDto());
    }

    public Task<Team> GetTeamByIdAsync(int teamId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
