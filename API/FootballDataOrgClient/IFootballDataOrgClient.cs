using FootballDataOrg.Responses;

namespace FootballDataOrg;

public interface IFootballDataOrgClient
{
    Task<TeamResponse?> GetTeamByIdAsync(int teamId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TeamResponse>> GetTeamsAsync(string competitionCode = "PL", CancellationToken cancellationToken = default);
}