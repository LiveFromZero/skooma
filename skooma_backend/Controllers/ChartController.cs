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
            // Validate query parameters
            if (string.IsNullOrEmpty(type))
            {
                return BadRequest(new { error = "The 'type' query parameter is required." });
            }

            if (year <= 0)
            {
                return BadRequest(new { error = "The 'year' query parameter must be a positive integer." });
            }

            // Cache-Key generation
            string cacheKey = $"{type}_{year}";

            // 1. Check cache
            var cachedData = await cacheService.GetCachedDataAsync(cacheKey);
            if (cachedData != null)
            {
                return Ok(cachedData); // 200 OK with cached data
            }

            // 2. Calculate data (if not cached)
            object chartData = type switch
            {
                "successRate" => await analysisService.CalculateSuccessRateByMoonPhaseAsync(year),
                "launchesPerMonth" => await analysisService.CalculateLaunchesPerMonthAsync(year),
                _ => null
            };

            if (chartData == null)
            {
                return BadRequest(new { error = "Invalid chart type. Supported types are 'successRate' and 'launchesPerMonth'." });
            }

            // 3. Save to cache
            await cacheService.SaveToCacheAsync(cacheKey, chartData);

            // 4. Return data
            return Ok(chartData); // 200 OK
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An unexpected error occurred. Please try again later. " + ex.Message }); // 500 Internal Server Error
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