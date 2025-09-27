using Microsoft.AspNetCore.Mvc;
using PremierRoom.API.Features.Teams.GetAllAvailableTeams;
using PremierRoom.Application;
using PremierRoom.Application.Features.Teams.GetAllAvailableTeams;
using PremierRoom.Application.Models;

namespace PremierRoom.API.Endpoints.Teams.GetAllAvailableTeams;

public static class GetAllAvailableTeamsEndpoint
{
    public static RouteGroupBuilder AddGetAllAvailableTeamsEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/all", async ([FromServices] IQueryHandler<GetAllAvailableTeamsQuery, IEnumerable<Team>> handler, CancellationToken cancellationToken) =>
        {
            var teams = await handler.HandleAsync(new GetAllAvailableTeamsQuery(), cancellationToken);

            var response = teams.ToResponseDto();

            return TypedResults.Ok(ApiResponse<GetAllAvailableTeamsResponseDto>.From(response));
        })
            .Produces<ApiResponse<GetAllAvailableTeamsResponseDto>>(200);

        return group;
    }
}
