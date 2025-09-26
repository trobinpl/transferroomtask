using FootballDataOrg.Responses;
using PremierRoom.Application.Models;

namespace PremierRoom.Application.FootballDataService.FootballDataOrg.Mappers;

public static class PlayerMapper
{
    public static Player FromApiDto(this PlayerResponse playerResponse)
    {
        var nameSplitted = string.IsNullOrEmpty(playerResponse.Name) ? [string.Empty, string.Empty] : playerResponse.Name.Split(" ");

        return new()
        {
            Firstname = nameSplitted[0],
            Surname = nameSplitted.Length > 1 ? playerResponse.Name.Split(" ")[1] : string.Empty,
            DateOfBirth = DateOnly.FromDateTime(playerResponse.DateOfBirth ?? DateTime.MinValue),
            Position = playerResponse.Position,
        };
    }

}
