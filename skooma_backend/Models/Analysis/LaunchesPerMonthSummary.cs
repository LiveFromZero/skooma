namespace skooma_backend.Models.Analysis;

/// <summary>
/// Zusammenfassung für LaunchesPerMonth-Chart
/// </summary>
public class LaunchesPerMonthSummary
{
    public int TotalLaunches { get; set; }
    public int TotalSuccessful { get; set; }
    public int TotalFailed { get; set; }
    public double OverallSuccessRate { get; set; }
}