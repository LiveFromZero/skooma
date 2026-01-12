namespace skooma_backend.Models;

public class MoonData
{
    public string Id { get; set; }
    public MoonPhase Phase { get; set; } = MoonPhase.Neumond;
    public DateTime Date { get; set; }
    
}