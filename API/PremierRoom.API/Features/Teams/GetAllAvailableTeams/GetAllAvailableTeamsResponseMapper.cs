using PremierRoom.API.Features.Teams.GetAllAvailableTeams;
using PremierRoom.Application.Models;

namespace PremierRoom.API.Endpoints.Teams.GetAllAvailableTeams;

public static class GetAllAvailableTeamsResponseMapper
{
    public static GetAllAvailableTeamsResponseDto ToResponseDto(this IEnumerable<Team> teams)
    {
        return new GetAllAvailableTeamsResponseDto
        {
            AvailableTeams = [.. teams.Select(t => new AvailableTeamDto
            {
                Id = t.Id,
                Name = t.Name,
                ShortName = t.ShortName,
                TLA = t.TLA,
                Crest = t.Crest,
                Nicknames = [.. t.Nicknames],
            })]
        };
    }
}
