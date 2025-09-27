namespace PremierRoom.Application.Models;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string TLA { get; set; } = string.Empty;
    public string Crest { get; set; } = string.Empty;
    public HashSet<string> Nicknames { get; set; } = [];
    public List<Player> Squad { get; set; } = [];
}
