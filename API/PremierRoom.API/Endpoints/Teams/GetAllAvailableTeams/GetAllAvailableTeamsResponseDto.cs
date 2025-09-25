namespace PremierRoom.API.Endpoints.Teams.GetAllAvailableTeams;

public class GetAllAvailableTeamsResponseDto
{
    public List<AvailableTeamDto> AvailableTeams { get; set; } = [];
}

public class AvailableTeamDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> Nicknames { get; set; } = [];
}
