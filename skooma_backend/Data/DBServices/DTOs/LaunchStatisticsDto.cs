namespace skooma_backend.Data.DBServices.DTOs
{
    public class MoonPhaseStatisticsDto
    {
        public int MoonPhase { get; set; }
        public int TotalLaunches { get; set; }
        public int SuccessfulLaunches { get; set; }
        public double SuccessRate { get; set; }
    }

    public class LaunchFilterResultDto
    {
        public int Year { get; set; }
        public string RocketType { get; set; } = string.Empty;
        public List<MoonPhaseStatisticsDto> Statistics { get; set; } = new();
    }
}
