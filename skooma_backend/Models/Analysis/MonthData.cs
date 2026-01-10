namespace skooma_backend.Models.Analysis;

/// <summary>
/// Daten für einen einzelnen Monat
/// </summary>
public class MonthData
{
    public int Month { get; set; }
    public string MonthName { get; set; } = string.Empty;
    public int TotalLaunches { get; set; }
    public int SuccessfulLaunches { get; set; }
    public int FailedLaunches { get; set; }
    public double SuccessRate { get; set; }
}

