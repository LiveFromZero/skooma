namespace skooma_backend.Models.Analysis;

/// <summary>
/// Helper-Klasse für das Verknüpfen von Launches mit Mondphasen
/// </summary>
public class LaunchWithMoonPhase
{
    public Launch Launch { get; set; } = null!;
    public MoonPhase MoonPhase { get; set; }
}