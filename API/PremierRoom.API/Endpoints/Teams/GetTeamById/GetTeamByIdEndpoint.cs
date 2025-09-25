namespace PremierRoom.API.Endpoints.Teams.GetTeamById;

public static class GetTeamByIdEndpoint
{
    public static RouteGroupBuilder AddGetTeamByIdEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/getbyid/{teamId:int}", (int teamId) =>
        {

        });

        return group;
    }
}
