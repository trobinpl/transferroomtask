using FootballDataOrg.Responses;
using PremierRoom.Application.Models;

namespace PremierRoom.Application.FootballDataService.FootballDataOrg.Mappers;

public static class TeamMapper
{
    public static Team FromApiDto(this TeamResponse teamResponse) => new()
    {
        Id = teamResponse.Id,
        Name = teamResponse.Name,
        ShortName = teamResponse.ShortName,
        TLA = teamResponse.Tla,
        Crest = teamResponse.CrestUrl,
        Squad = [.. teamResponse.Squad.Select(p => p.FromApiDto())],
    };
}
