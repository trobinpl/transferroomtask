using System.ComponentModel.DataAnnotations;

namespace PremierRoom.API.Features.Teams.GetAllAvailableTeams;

public class GetAllAvailableTeamsResponseDto
{
    [Required]
    public required List<AvailableTeamDto> AvailableTeams { get; set; } = [];
}

public class AvailableTeamDto
{
    [Required]
    public required int Id { get; set; }
    [Required]
    public required string Name { get; set; } = string.Empty;
    [Required]
    public required string ShortName { get; set; } = string.Empty;
    [Required]
    public required string TLA { get; set; } = string.Empty;
    [Required]
    public required string Crest { get; set; } = string.Empty;
    [Required]
    public required List<string> Nicknames { get; set; } = [];
}
