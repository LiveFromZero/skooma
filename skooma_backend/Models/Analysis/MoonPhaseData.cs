namespace skooma_backend.Models.Analysis;

/// <summary>
/// Daten für eine einzelne Mondphase
/// </summary>
public class MoonPhaseData
{
    public string MoonPhase { get; set; } = string.Empty;
    public int TotalLaunches { get; set; }
    public int SuccessfulLaunches { get; set; }
    public int FailedLaunches { get; set; }
    public double SuccessRate { get; set; }
}