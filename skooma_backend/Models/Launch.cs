namespace skooma_backend.Models;

public class Launch
{
    public string Id { get; set; } = string.Empty;
    public string RocketName { get; set; } = string.Empty;
    public DateTimeOffset LaunchDate { get; set; }
    public Location Location { get; set; } = new Location();
    public string Status { get; set; } = string.Empty;
}

