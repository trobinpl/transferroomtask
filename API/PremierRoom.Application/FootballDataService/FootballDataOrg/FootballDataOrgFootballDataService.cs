using FootballDataOrg;
using Microsoft.Extensions.Logging;
using PremierRoom.Application.FootballDataService.FootballDataOrg.Mappers;
using PremierRoom.Application.Models;

namespace PremierRoom.Application.FootballDataService.FootballDataOrg;

public class FootballDataOrgFootballDataService : IFootballDataService
{
    private readonly IFootballDataOrgClient _footballDataOrgClient;
    private readonly ILogger<FootballDataOrgFootballDataService> _logger;

    private const string PremiereLeagueCompetitionCode = "PL";

    public FootballDataOrgFootballDataService(IFootballDataOrgClient footballDataOrgClient, ILogger<FootballDataOrgFootballDataService> logger)
    {
        _footballDataOrgClient = footballDataOrgClient;
        _logger = logger;
    }

    public async Task<List<Team>> GetPremierLeagueTeamsAsync(CancellationToken cancellationToken = default)
    {
        return await GetTeams(PremiereLeagueCompetitionCode, cancellationToken);
    }

    public async Task<Team?> GetTeamByIdAsync(int teamId, CancellationToken cancellationToken = default)
    {
        var footballDataOrgTeam = await _footballDataOrgClient.GetTeamByIdAsync(teamId, cancellationToken);

        if (footballDataOrgTeam is null)
        {
            return null;
        }

        var team = footballDataOrgTeam.FromApiDto();

        return team;
    }

    private async Task<List<Team>> GetTeams(string competitionCode, CancellationToken cancellationToken = default)
    {
        try
        {
            var teams = await _footballDataOrgClient.GetTeamsAsync(competitionCode, cancellationToken);

            return teams.Select(t => t.FromApiDto()).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured while fetching teams for competition {Competition}", competitionCode);

            return [];
        }
    }
}
