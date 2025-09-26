using PremierRoom.Application.Models;

namespace PremierRoom.Application.FootballDataService.InMemory;

public class InMemoryFootballDataService : IFootballDataService
{
    private readonly List<Team> _availableTeams = [];

    public InMemoryFootballDataService(List<Team> availableTeams)
    {
        _availableTeams = availableTeams;
    }

    public async Task<IEnumerable<Team>> GetPremierLeagueTeamsAsync(CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(_availableTeams);
    }

    public async Task<Team?> GetTeamByIdAsync(int teamId, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(_availableTeams.FirstOrDefault(t => t.Id == teamId));
    }
}
