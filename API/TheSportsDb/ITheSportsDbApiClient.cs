using TheSportsDb.Responses;

namespace TheSportsDb;
public interface ITheSportsDbApiClient
{
    Task<SearchPlayerResponse> SearchPlayers(string playerName, CancellationToken cancellationToken = default);
    Task<SearchTeamResponse> SearchTeams(string teamName, CancellationToken cancellationToken = default);
}