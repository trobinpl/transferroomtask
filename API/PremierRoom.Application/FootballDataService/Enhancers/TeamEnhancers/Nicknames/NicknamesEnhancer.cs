using PremierRoom.Application.Models;

namespace PremierRoom.Application.FootballDataService.Enhancers.TeamEnhancers.Nicknames;

public abstract class NicknamesEnhancer : IEnhancer<Team>
{
    public abstract Task<IEnumerable<string>> GetNicknamesForTeam(Team team);

    public async Task EnhanceAsync(Team team, CancellationToken cancellationToken = default)
    {
        var nicknamesForTeam = await GetNicknamesForTeam(team);

        team.Nicknames = [.. nicknamesForTeam.Distinct()];
    }

}
