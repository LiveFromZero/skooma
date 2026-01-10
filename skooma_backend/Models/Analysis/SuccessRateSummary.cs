namespace skooma_backend.Models.Analysis;

/// <summary>
/// Zusammenfassung für SuccessRate-Chart
/// </summary>
public class SuccessRateSummary
{
    public int TotalAnalyzedLaunches { get; set; }
    public int TotalLaunches { get; set; }
    public int LaunchesWithoutMoonData { get; set; }
}
