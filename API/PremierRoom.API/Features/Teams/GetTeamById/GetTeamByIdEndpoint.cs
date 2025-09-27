using Microsoft.AspNetCore.Mvc;
using OneOf;
using PremierRoom.Application;
using PremierRoom.Application.Features.Teams.GetTeamById;
using PremierRoom.Application.Features.Teams.GetTeamById.Results;
using PremierRoom.Application.Models;

namespace PremierRoom.API.Endpoints.Teams.GetTeamById;

public static class GetTeamByIdEndpoint
{
    public static RouteGroupBuilder AddGetTeamByIdEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/getbyid/{teamId:int}", async (int teamId, [FromServices] IQueryHandler<GetTeamByIdQuery, OneOf<Team, SpecifiedTeamNotFound>> handler, CancellationToken cancellationToken) =>
        {
            var result = await handler.HandleAsync(new GetTeamByIdQuery { TeamId = teamId }, cancellationToken);

            return result.Match(
                team => TypedResults.Ok(ApiResponse<GetTeamByIdResponseDto>.From(team.ToResponseDto())),
                notFound => Results.NotFound()
            );
        })
            .Produces<ApiResponse<GetTeamByIdResponseDto>>(200)
            .Produces(404);

        return group;
    }
}
