using PremierRoom.Application.Models;

namespace PremierRoom.API.Endpoints.Teams.GetTeamById;

public static class GetTeamByIdResponseMapper
{
    public static GetTeamByIdResponseDto ToResponseDto(this Team team) => new()
    {
        Name = team.Name,
        Squad = [.. team.Squad.Select(p => p.ToResponseDto())],
    };

    private static PlayerDto ToResponseDto(this Player player) => new()
    {
        Name = $"{player.Firstname} {player.Surname}",
        DateOfBirth = player.DateOfBirth,
        Position = player.Position,
        ProfilePictureUrl = player.ProfilePicture
    };
}
