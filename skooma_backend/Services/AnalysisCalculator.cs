using skooma_backend.Models;
using skooma_backend.Models.Responses;
using skooma_backend.Models.Analysis;
using Microsoft.EntityFrameworkCore;

namespace skooma_backend.Services;

public class AnalysisCalculator
{
    private readonly DbContext _db;

    public AnalysisCalculator(DbContext db)
    {
        _db = db;
    }

    private const string LaunchStatusSuccess = "Success";

    /// <summary>
    /// Berechnet die Erfolgsrate von Raketenstarts gruppiert nach Mondphasen für ein bestimmtes Jahr.
    /// </summary>
    /// <param name="year">Das Jahr für die Analyse</param>
    /// <returns>SuccessRateByMoonPhaseResponse mit gruppierter Erfolgsrate</returns>
    public async Task<SuccessRateByMoonPhaseResponse> CalculateSuccessRateByMoonPhaseAsync(int year)
    {
        // 1. Hole alle Launches für das Jahr
        var launches = await QueryLaunchesAsync(year);

        // 2. Hole alle Mondphasen für das Jahr
        var moonData = await QueryMoonDataAsync(year);

        // 3. Verknüpfe Launches mit Mondphasen
        var launchesWithMoonPhase = JoinWithMoonPhases(launches, moonData);

        // 4. Gruppiere nach Mondphase und berechne Erfolgsrate
        var groupedData = launchesWithMoonPhase
            .GroupBy(x => x.MoonPhase)
            .Select(g => new MoonPhaseData
            {
                MoonPhase = g.Key.ToString(),
                TotalLaunches = g.Count(),
                SuccessfulLaunches = g.Count(x => x.Launch.Status == LaunchStatusSuccess),
                FailedLaunches = g.Count(x => x.Launch.Status != LaunchStatusSuccess),
                SuccessRate = Math.Round(
                    (double)g.Count(x => x.Launch.Status == LaunchStatusSuccess) / g.Count() * 100,
                    2
                )
            })
            .OrderBy(x => x.MoonPhase)
            .ToList();

        // 5. Erstelle Response-Objekt
        return new SuccessRateByMoonPhaseResponse
        {
            ChartType = "successRate",
            Year = year,
            Data = groupedData,
            Summary = new SuccessRateSummary
            {
                TotalAnalyzedLaunches = launchesWithMoonPhase.Count,
                TotalLaunches = launches.Count,
                LaunchesWithoutMoonData = launches.Count - launchesWithMoonPhase.Count
            }
        };
    }

    /// <summary>
    /// Berechnet die Anzahl der Raketenstarts pro Monat für ein bestimmtes Jahr.
    /// </summary>
    /// <param name="year">Das Jahr für die Analyse</param>
    /// <returns>LaunchesPerMonthResponse mit monatlicher Statistik</returns>
    public async Task<LaunchesPerMonthResponse> CalculateLaunchesPerMonthAsync(int year)
    {
        // 1. Hole alle Launches für das Jahr
        var launches = await QueryLaunchesAsync(year);

        // 2. Gruppiere nach Monat
        var groupedData = launches
            .GroupBy(l => l.LaunchDate.Month)
            .Select(g => new MonthData
            {
                Month = g.Key,
                MonthName = new DateOnly(year, g.Key, 1).ToString("MMMM"),
                TotalLaunches = g.Count(),
                SuccessfulLaunches = g.Count(l => l.Status == LaunchStatusSuccess),
                FailedLaunches = g.Count(l => l.Status != LaunchStatusSuccess),
                SuccessRate = Math.Round(
                    (double)g.Count(l => l.Status == LaunchStatusSuccess) / g.Count() * 100,
                    2
                )
            })
            .OrderBy(x => x.Month)
            .ToList();

        // 3. Erstelle Response-Objekt
        return new LaunchesPerMonthResponse
        {
            ChartType = "launchesPerMonth",
            Year = year,
            Data = groupedData,
            Summary = new LaunchesPerMonthSummary
            {
                TotalLaunches = launches.Count,
                TotalSuccessful = launches.Count(l => l.Status == LaunchStatusSuccess),
                TotalFailed = launches.Count(l => l.Status != LaunchStatusSuccess),
                OverallSuccessRate = launches.Count > 0
                    ? Math.Round((double)launches.Count(l => l.Status == LaunchStatusSuccess) / launches.Count * 100, 2)
                    : 0
            }
        };
    }

    /// <summary>
    /// Holt alle Launches für ein bestimmtes Jahr aus der Datenbank.
    /// Inkludiert auch die Location-Daten (optional).
    /// </summary>
    /// <param name="year">Das Jahr für die Abfrage</param>
    /// <returns>Liste von Launch-Objekten</returns>
    private async Task<List<Launch>> QueryLaunchesAsync(int year)
    {
        // Diese Methode holt alle Launches für das angegebene Jahr aus der Datenbank
        return await _db.Set<Launch>()
            .Include(l => l.Location) // Optional: Location-Daten mit laden
            .Where(l => l.LaunchDate.Year == year)
            .ToListAsync();
    }

    /// <summary>
    /// Holt alle Mondphasen-Daten für ein bestimmtes Jahr aus der Datenbank.
    /// </summary>
    /// <param name="year"></param>
    /// <returns>Liste von MoonData-Objekten</returns>
    private async Task<List<MoonData>> QueryMoonDataAsync(int year)
    {
        // Diese Methode holt alle Mondphasen-Daten für das angegebene Jahr aus der Datenbank
        return await _db.Set<MoonData>()
            .ToListAsync();
    }

    /// <summary>
    /// Verknüpft Launches mit den entsprechenden Mondphasen basierend auf dem Datum.
    /// </summary>
    /// <param name="launches">Liste von Launches</param>
    /// <param name="moonData">Liste von MoonData</param>
    /// <returns>Liste von Objekten mit Launch und zugehöriger Mondphase</returns>
    private List<LaunchWithMoonPhase> JoinWithMoonPhases(List<Launch> launches, List<MoonData> moonData)
    {
        var result = new List<LaunchWithMoonPhase>();

        foreach (var launch in launches)
        {
            // Finde die Mondphase für das Launch-Datum (nur Datum, keine Uhrzeit)
            var moon = moonData.FirstOrDefault(m => m.Date.Date == launch.LaunchDate.Date);

            // Nur hinzufügen wenn Mondphase gefunden wurde
            if (moon != null)
            {   
                result.Add(new LaunchWithMoonPhase
                {
                    Launch = launch,
                    MoonPhase = moon.Phase
                });
            }
        }

        return result;
    }

    /// <summary>
    /// Gibt eine Zusammenfassung aller verfügbaren Statistiken für ein Jahr zurück.
    /// Nützlich für Dashboard-Ansichten.
    /// </summary>
    /// <param name="year">Das Jahr für die Analyse</param>
    /// <returns>YearSummaryResponse mit allen wichtigen Kennzahlen</returns>
    public async Task<YearSummaryResponse> GetYearSummaryAsync(int year)
    {
        var launches = await QueryLaunchesAsync(year);
        var moonData = await QueryMoonDataAsync(year);
        var launchesWithMoon = JoinWithMoonPhases(launches, moonData);

        var mostActiveMonth = launches
            .GroupBy(l => l.LaunchDate.Month)
            .OrderByDescending(g => g.Count())
            .Select(g => new DateOnly(year, g.Key, 1).ToString("MMMM"))
            .FirstOrDefault();

        return new YearSummaryResponse
        {
            Year = year,
            TotalLaunches = launches.Count,
            SuccessfulLaunches = launches.Count(l => l.Status == LaunchStatusSuccess),
            FailedLaunches = launches.Count(l => l.Status != LaunchStatusSuccess),
            OverallSuccessRate = launches.Count > 0
                ? Math.Round((double)launches.Count(l => l.Status == LaunchStatusSuccess) / launches.Count * 100, 2)
                : 0,
            LaunchesWithMoonData = launchesWithMoon.Count,
            UniqueLocations = launches.Select(l => l.Location.CountryName).Distinct().Count(),
            MostActiveMonth = mostActiveMonth
        };
    }


}