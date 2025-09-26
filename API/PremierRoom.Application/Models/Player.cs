namespace PremierRoom.Application.Models;

public class Player
{
    public string ProfilePicture { get; set; } = string.Empty;
    public string Firstname { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Position { get; set; } = string.Empty;
}
