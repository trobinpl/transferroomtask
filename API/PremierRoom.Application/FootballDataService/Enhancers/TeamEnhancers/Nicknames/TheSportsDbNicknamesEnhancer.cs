using PremierRoom.Application.Models;
using TheSportsDb;

namespace PremierRoom.Application.FootballDataService.Enhancers.TeamEnhancers.Nicknames;

public class TheSportsDbNicknamesEnhancer : NicknamesEnhancer
{
    private readonly ITheSportsDbApiClient _theSportsDbApiClient;

    public TheSportsDbNicknamesEnhancer(ITheSportsDbApiClient theSportsDbApiClient)
    {
        _theSportsDbApiClient = theSportsDbApiClient;
    }

    public override async Task<IEnumerable<string>> GetNicknamesForTeam(Team team)
    {
        var searchTerm = string.IsNullOrEmpty(team.ShortName) ? team.Name : team.ShortName;

        if (string.IsNullOrEmpty(searchTerm))
        {
            return [];
        }

        var foundTeam = await _theSportsDbApiClient.SearchTeams(searchTerm);

        return foundTeam.Teams?.FirstOrDefault()?.Nicknames.Split(',') ?? [];
    }
}
