using PremierRoom.Application.Models;

namespace PremierRoom.Application.FootballDataService;

public interface IFootballDataService
{
    Task<IEnumerable<Team>> GetPremierLeagueTeamsAsync(CancellationToken cancellationToken = default);
    Task<Team> GetTeamByIdAsync(int teamId, CancellationToken cancellationToken = default);
}
