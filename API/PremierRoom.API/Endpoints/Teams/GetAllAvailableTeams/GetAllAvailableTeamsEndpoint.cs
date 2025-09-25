using Microsoft.AspNetCore.Mvc;
using PremierRoom.Application.FootballDataService;

namespace PremierRoom.API.Endpoints.Teams.GetAllAvailableTeams;

public static class GetAllAvailableTeamsEndpoint
{
    public static RouteGroupBuilder AddGetAllAvailableTeamsEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/all", async ([FromServices] IFootballDataService footballDataService, CancellationToken cancellationToken) =>
        {
            var premiereLeagueTeams = await footballDataService.GetPremierLeagueTeamsAsync(cancellationToken);

            return TypedResults.Ok(premiereLeagueTeams);
        });

        return group;
    }
}
