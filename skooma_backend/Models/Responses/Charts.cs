using skooma_backend.Models.Analysis;

namespace skooma_backend.Models.Responses
{

    /// <summary>
    /// Response für die Erfolgsrate von Raketenstarts gruppiert nach Mondphasen
    /// </summary>
    public class SuccessRateByMoonPhaseResponse
    {
        public string ChartType { get; set; } = "successRate";
        public int Year { get; set; }
        public List<MoonPhaseData> Data { get; set; } = new();
        public SuccessRateSummary Summary { get; set; } = new();
    }


    /// <summary>
    /// Response für die Anzahl der Raketenstarts pro Monat
    /// </summary>
    public class LaunchesPerMonthResponse
    {
        public string ChartType { get; set; } = "launchesPerMonth";
        public int Year { get; set; }
        public List<MonthData> Data { get; set; } = new();
        public LaunchesPerMonthSummary Summary { get; set; } = new();
    }

    /// <summary>
    /// Response für die Jahres-Zusammenfassung (Dashboard)
    /// </summary>
    public class YearSummaryResponse
    {
        public int Year { get; set; }
        public int TotalLaunches { get; set; }
        public int SuccessfulLaunches { get; set; }
        public int FailedLaunches { get; set; }
        public double OverallSuccessRate { get; set; }
        public int LaunchesWithMoonData { get; set; }
        public int UniqueLocations { get; set; }
        public string? MostActiveMonth { get; set; }
    }
}