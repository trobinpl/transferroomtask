namespace PremierRoom.API.Features.Teams.GetTeamById;

public class GetTeamByIdResponseDto
{
    public string Name { get; set; } = string.Empty;
    public List<string> Nicknames { get; set; } = [];
    public List<PlayerDto> Squad { get; set; } = [];
}

public class PlayerDto
{
    public string ProfilePictureUrl { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Position { get; set; } = string.Empty;
}
