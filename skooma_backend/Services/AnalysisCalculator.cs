using skooma_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace skooma_backend.Services;
//public class AnalysisCalculator(DatabaseContext db)
public class AnalysisCalculator()
{
    // Todo implement database context injection
    // private readonly DatabaseContext _db = db;
    
    /// <summary>
    /// Berechnet die Erfolgsrate von Raketenstarts gruppiert nach Mondphasen für ein bestimmtes Jahr.
    /// </summary>
    /// <param name="year">Das Jahr für die Analyse</param>
    /// <returns>Objekt mit Chart-Daten (Mondphase und Erfolgsrate)</returns>
    public async Task<object> CalculateSuccessRateByMoonPhaseAsync(int year)
    {
        // 1. Hole alle Launches für das Jahr
        var launches = await QueryLaunchesAsync(year);

        // 2. Hole alle Mondphasen für das Jahr
        var moonData = await QueryMoonData(year);
        // 3. Verknüpfe Launches mit Mondphasen
        var launchesWithMoonPhase = JoinWithMoonPhases(launches, moonData);

        // 4. Gruppiere nach Mondphase und berechne Erfolgsrate
        var grouped = launchesWithMoonPhase
            .GroupBy(x => x.MoonPhase)
            .Select(g => new
            {
                MoonPhase = g.Key.ToString(),
                TotalLaunches = g.Count(),
                SuccessfulLaunches = g.Count(x => x.Launch.Status == "Success"),
                FailedLaunches = g.Count(x => x.Launch.Status != "Success"),
                SuccessRate = Math.Round(
                    (double)g.Count(x => x.Launch.Status == "Success") / g.Count() * 100,
                    2
                )
            })
            .OrderBy(x => x.MoonPhase)
            .ToList();

        // 5. Formatiere als Chart-Daten und gebe zurück
        return new
        {
            ChartType = "successRate",
            Year = year,
            Data = grouped,
            Summary = new
            {
                TotalAnalyzedLaunches = launchesWithMoonPhase.Count(),
                TotalLaunches = launches.Count,
                LaunchesWithoutMoonData = launches.Count - launchesWithMoonPhase.Count()
            }
        };
    }

    /// <summary>
    /// Berechnet die Anzahl der Raketenstarts pro Monat für ein bestimmtes Jahr.
    /// </summary>
    /// <param name="year">Das Jahr für die Analyse</param>
    /// <returns>Objekt mit Chart-Daten (Monat und Anzahl Starts)</returns>
    public async Task<object> CalculateLaunchesPerMonthAsync(int year)
    {
        // 1. Hole alle Launches für das Jahr
        var launches = await QueryLaunchesAsync(year);

        // 2. Gruppiere nach Monat
        var grouped = launches
            .GroupBy(l => l.Date.Month)
            .Select(g => new
            {
                Month = g.Key,
                MonthName = new DateTime(year, g.Key, 1).ToString("MMMM"),
                TotalLaunches = g.Count(),
                SuccessfulLaunches = g.Count(l => l.Status == "Success"),
                FailedLaunches = g.Count(l => l.Status != "Success"),
                SuccessRate = Math.Round(
                    (double)g.Count(l => l.Status == "Success") / g.Count() * 100,
                    2
                )
            })
            .OrderBy(x => x.Month)
            .ToList();

        // 3. Formatiere als Chart-Daten und gebe zurück
        return new
        {
            ChartType = "launchesPerMonth",
            Year = year,
            Data = grouped,
            Summary = new
            {
                TotalLaunches = launches.Count,
                TotalSuccessful = launches.Count(l => l.Status == "Success"),
                TotalFailed = launches.Count(l => l.Status != "Success"),
                OverallSuccessRate = launches.Count > 0
                    ? Math.Round((double)launches.Count(l => l.Status == "Success") / launches.Count * 100, 2)
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
        // TODO: Implementieren
        // Diese Methode soll alle Launches für das angegebene Jahr aus der Datenbank holen
        // Optional: Include Location für zusätzliche Informationen

        // Beispiel-Implementierung:
        // return await _db.Launches
        //     .Include(l => l.Location) // Optional: Location-Daten mit laden
        //     .Where(l => l.Date.Year == year)
        //     .ToListAsync();
        return new List<Launch>();
    }
    
    public async Task<List<MoonData>> QueryMoonData(int year)
    {
        // TODO: Implement moon data querying logic
        
        // return _db.MoonData
        //     .Where(m => m.Date.Year == year)
        //     .ToListAsync();
        return new List<MoonData>();
    }

    /// <summary>
    /// Verknüpft Launches mit den entsprechenden Mondphasen basierend auf dem Datum.
    /// </summary>
    /// <param name="launches">Liste von Launches</param>
    /// <param name="moonData">Liste von MoonData</param>
    /// <returns>Liste von anonymen Objekten mit Launch und zugehöriger Mondphase</returns>
    private List<dynamic> JoinWithMoonPhases(List<Launch> launches, List<MoonData> moonData)
    {
        var result = new List<dynamic>();

        foreach (var launch in launches)
        {
            // Finde die Mondphase für das Launch-Datum (nur Datum, keine Uhrzeit)
            var moon = moonData.FirstOrDefault(m => m.Date.Date == launch.Date.Date);

            // Nur hinzufügen wenn Mondphase gefunden wurde
            if (moon != null)
            {
                result.Add(new
                {
                    Launch = launch,
                    MoonPhase = moon.PhaseName
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
    /// <returns>Objekt mit allen wichtigen Kennzahlen</returns>
    public async Task<object> GetYearSummaryAsync(int year)
    {
        var launches = await QueryLaunchesAsync(year);
        var moonData = await QueryMoonData(year);
        var launchesWithMoon = JoinWithMoonPhases(launches, moonData);

        return new
        {
            Year = year,
            TotalLaunches = launches.Count,
            SuccessfulLaunches = launches.Count(l => l.Status == "Success"),
            FailedLaunches = launches.Count(l => l.Status != "Success"),
            OverallSuccessRate = launches.Count > 0
                ? Math.Round((double)launches.Count(l => l.Status == "Success") / launches.Count * 100, 2)
                : 0,
            LaunchesWithMoonData = launchesWithMoon.Count,
            UniqueLocations = launches.Select(l => l.Location.CountryName).Distinct().Count(),
            MostActiveMonth = launches
                .GroupBy(l => l.Date.Month)
                .OrderByDescending(g => g.Count())
                .Select(g => new DateTime(year, g.Key, 1).ToString("MMMM"))
                .FirstOrDefault()
        };
    }
}

