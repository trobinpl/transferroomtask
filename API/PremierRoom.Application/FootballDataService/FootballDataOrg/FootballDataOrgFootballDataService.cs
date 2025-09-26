using FootballDataOrg;
using Microsoft.Extensions.Logging;
using PremierRoom.Application.FootballDataService.FootballDataOrg.Mappers;
using PremierRoom.Application.Models;

namespace PremierRoom.Application.FootballDataService.FootballDataOrg;

public class FootballDataOrgFootballDataService : IFootballDataService
{
    private readonly FootballDataOrgClient _footballDataOrgClient;
    private readonly ILogger<FootballDataOrgFootballDataService> _logger;

    private const string PremiereLeagueCompetitionCode = "PL";

    public FootballDataOrgFootballDataService(FootballDataOrgClient footballDataOrgClient, ILogger<FootballDataOrgFootballDataService> logger)
    {
        _footballDataOrgClient = footballDataOrgClient;
        _logger = logger;
    }

    public async Task<IEnumerable<Team>> GetPremierLeagueTeamsAsync(CancellationToken cancellationToken = default)
    {
        return await GetTeams(PremiereLeagueCompetitionCode, cancellationToken);
    }

    public async Task<Team?> GetTeamByIdAsync(int teamId, CancellationToken cancellationToken = default)
    {
        var team = await _footballDataOrgClient.GetTeamByIdAsync(teamId, cancellationToken);

        if (team is null)
        {
            return null;
        }

        return team.FromApiDto();
    }

    private async Task<IEnumerable<Team>> GetTeams(string competitionCode, CancellationToken cancellationToken = default)
    {
        try
        {
            var teams = await _footballDataOrgClient.GetTeamsAsync(competitionCode, cancellationToken);

            return teams.Select(t => t.FromApiDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured while fetching teams for competition {Competition}", competitionCode);

            return [];
        }
    }
}
