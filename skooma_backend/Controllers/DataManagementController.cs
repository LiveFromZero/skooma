using Microsoft.AspNetCore.Mvc;
using skooma_backend.Services;
using skooma_backend.Data;
using Microsoft.EntityFrameworkCore;

namespace skooma_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DataManagementController : ControllerBase
{
    private readonly LaunchFetch _launchFetch;
    private readonly MoonFetch _moonFetch;
    private readonly AppDbContext _db;
    private readonly ILogger<DataManagementController> _logger;

    public DataManagementController(
        LaunchFetch launchFetch, 
        MoonFetch moonFetch, 
        AppDbContext db,
        ILogger<DataManagementController> logger)
    {
        _launchFetch = launchFetch;
        _moonFetch = moonFetch;
        _db = db;
        _logger = logger;
    }

    // POST: api/datamanagement/refresh
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAllData()
    {
        try
        {
            _logger.LogInformation("Starting database refresh for all years");

            // Hole alle Jahre, die in der DB vorhanden sind
            var launchYears = await _db.Launches
                .Select(l => l.LaunchDate.Year)
                .Distinct()
                .ToListAsync();

            var moonYears = await _db.MoonData
                .Select(m => m.MoonDate.Year)
                .Distinct()
                .ToListAsync();

            var allYears = launchYears.Union(moonYears).Distinct().OrderBy(y => y).ToList();

            if (!allYears.Any())
            {
                return Ok(new { message = "No data in database to refresh." });
            }

            var refreshResults = new List<object>();

            foreach (var year in allYears)
            {
                _logger.LogInformation("Refreshing data for year {Year}", year);

                // Lösche alte Daten für dieses Jahr
                var oldLaunches = await _db.Launches
                    .FromSql($"SELECT * FROM Launches WHERE strftime('%Y', LaunchDate) = {year.ToString()}")
                    .Include(l => l.Location)
                    .ToListAsync();
                
                var oldMoonData = await _db.MoonData
                    .FromSql($"SELECT * FROM MoonData WHERE strftime('%Y', MoonDate) = {year.ToString()}")
                    .ToListAsync();

                _db.Launches.RemoveRange(oldLaunches);
                _db.MoonData.RemoveRange(oldMoonData);
                await _db.SaveChangesAsync();

                // Fetch neue Daten
                var launches = await _launchFetch.GetLaunchesByYearAsync(year);
                var moonData = await _moonFetch.GetMoonPhasesForYearAsync(year);

                // Speichere neue Daten
                if (launches.Any())
                {
                    // Extrahiere und speichere Locations zuerst (dedupliziert)
                    var uniqueLocations = launches
                        .Select(l => l.Location)
                        .GroupBy(loc => loc.Id)
                        .Select(g => g.First())
                        .ToList();
                    
                    // Prüfe welche Locations schon existieren
                    var existingLocationIds = await _db.Locations
                        .Select(l => l.Id)
                        .ToListAsync();
                    
                    var newLocations = uniqueLocations
                        .Where(loc => !existingLocationIds.Contains(loc.Id))
                        .ToList();
                    
                    if (newLocations.Any())
                    {
                        _db.Locations.AddRange(newLocations);
                        await _db.SaveChangesAsync();
                    }
                    
                    // Jetzt die Launches hinzufügen
                    _db.Launches.AddRange(launches);
                    await _db.SaveChangesAsync();
                }

                if (moonData.Any())
                {
                    // Generiere IDs für MoonData
                    for (int i = 0; i < moonData.Count; i++)
                    {
                        moonData[i].Id = $"moon-{year}-{i:D3}";
                    }
                    _db.MoonData.AddRange(moonData);
                    await _db.SaveChangesAsync();
                }

                refreshResults.Add(new
                {
                    year,
                    launchesFetched = launches.Count,
                    moonPhasesFetched = moonData.Count
                });

                _logger.LogInformation("Refreshed {Year}: {LaunchCount} launches, {MoonCount} moon phases", 
                    year, launches.Count, moonData.Count);
            }

            return Ok(new
            {
                message = "Database refresh completed",
                yearsRefreshed = allYears.Count,
                details = refreshResults
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during database refresh");
            return StatusCode(500, new { error = "Database refresh failed: " + ex.Message });
        }
    }

    // POST: api/datamanagement/fetch?year=2023
    [HttpPost("fetch")]
    public async Task<IActionResult> FetchDataForYear([FromQuery] int year)
    {
        try
        {
            if (year <= 0)
            {
                return BadRequest(new { error = "Year must be a positive integer." });
            }

            _logger.LogInformation("Fetching data for year {Year}", year);

            // Prüfe ob Daten bereits existieren mit SQL Statement
            int existingLaunches = await _db.Launches
                .FromSql($"SELECT * FROM Launches WHERE strftime('%Y', LaunchDate) = {year.ToString()}")
                .CountAsync();
            
            int existingMoonData = await _db.MoonData
                .FromSql($"SELECT * FROM MoonData WHERE strftime('%Y', MoonDate) = {year.ToString()}")
                .CountAsync();

            if (existingLaunches > 0 || existingMoonData > 0)
            {
                return BadRequest(new { 
                    error = $"Data for year {year} already exists. Use /refresh endpoint to update.",
                    existingLaunches,
                    existingMoonData
                });
            }

            // Fetch neue Daten
            var launches = await _launchFetch.GetLaunchesByYearAsync(year);
            var moonData = await _moonFetch.GetMoonPhasesForYearAsync(year);

            // Speichere Daten
            if (launches.Any())
            {
                // Extrahiere und speichere Locations zuerst (dedupliziert)
                var uniqueLocations = launches
                    .Select(l => l.Location)
                    .GroupBy(loc => loc.Id)
                    .Select(g => g.First())
                    .ToList();
                
                // Prüfe welche Locations schon existieren
                var existingLocationIds = await _db.Locations
                    .Select(l => l.Id)
                    .ToListAsync();
                
                var newLocations = uniqueLocations
                    .Where(loc => !existingLocationIds.Contains(loc.Id))
                    .ToList();
                
                if (newLocations.Any())
                {
                    _db.Locations.AddRange(newLocations);
                    await _db.SaveChangesAsync();
                }
                
                // Jetzt die Launches hinzufügen
                _db.Launches.AddRange(launches);
                await _db.SaveChangesAsync();
            }

            if (moonData.Any())
            {
                for (int i = 0; i < moonData.Count; i++)
                {
                    moonData[i].Id = $"moon-{year}-{i:D3}";
                }
                _db.MoonData.AddRange(moonData);
                await _db.SaveChangesAsync();
            }

            return Ok(new
            {
                message = $"Successfully fetched data for year {year}",
                year,
                launchesFetched = launches.Count,
                moonPhasesFetched = moonData.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching data for year {Year}", year);
            return StatusCode(500, new { error = "Data fetch failed: " + ex.Message });
        }
    }
}