using skooma_backend.Services;

namespace skooma_backend.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ChartController(AnalysisCalculator analysisService, CacheService cacheService) : ControllerBase
{
    // Dependency Injection über Constructor

    // GET: api/chart?type=successRate&year=2023
    [HttpGet]
    public async Task<IActionResult> GetChart([FromQuery] string type, [FromQuery] int year)
    {
        try
        {
            // Cache-Key generieren
            string cacheKey = $"{type}_{year}";

            // 1. Cache prüfen
            var cachedData = await cacheService.GetCachedDataAsync(cacheKey);
            if (cachedData != null)
            {
                return Ok(cachedData); // 200 OK mit Cache-Daten
            }

            // 2. Daten berechnen (falls nicht gecacht)
            object chartData = type switch
            {
                "successRate" => await analysisService.CalculateSuccessRateByMoonPhaseAsync(year),
                "launchesPerMonth" => await analysisService.CalculateLaunchesPerMonthAsync(year),
                _ => null
            };

            if (chartData == null)
            {
                return BadRequest(new { error = "Invalid chart type" }); // 400 Bad Request
            }

            // 3. In Cache speichern
            await cacheService.SaveToCacheAsync(cacheKey, chartData);

            // 4. Daten zurückgeben
            return Ok(chartData); // 200 OK
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message }); // 500 Internal Server Error
        }
    }

    // GET: api/chart/types
    [HttpGet("types")]
    public IActionResult GetAvailableChartTypes()
    {
        var types = new[]
        {
            new { id = "successRate", name = "Erfolgsrate nach Mondphase" },
            new { id = "launchesPerMonth", name = "Starts pro Monat" }
        };

        return Ok(types); // 200 OK
    }
}