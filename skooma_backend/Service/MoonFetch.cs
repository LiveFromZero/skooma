using System.Text.Json;
using skooma_backend.Models;

namespace skooma_backend.Services;

public class MoonFetch(HttpClient httpClient, ILogger<MoonFetch> logger)
{
    public async Task<List<MoonData>> GetMoonPhasesForYearAsync(int year)
    {
        try
        {
            logger.LogInformation("Fetching moon phases for year {Year}", year);

            var url = $"https://aa.usno.navy.mil/api/moon/phases/year?year={year}";

            logger.LogInformation("Making API call to URL: {Url}", url);

            var response = await httpClient.GetStringAsync(url);
            var data = JsonSerializer.Deserialize<JsonElement>(response);

            var phases = data.GetProperty("phasedata").EnumerateArray()
                .Select(phase => new MoonData
                {
                    Phase = MapPhaseStringToEnum(phase.GetProperty("phase").GetString() ?? string.Empty),
                    Date = new DateTime(
                        phase.GetProperty("year").GetInt32(),
                        phase.GetProperty("month").GetInt32(),
                        phase.GetProperty("day").GetInt32())
                })
                .ToList();

            logger.LogInformation("Successfully fetched {PhaseCount} moon phases", phases.Count);
            return phases;
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "HTTP error occurred while fetching moon phases");
            return new List<MoonData>();
        }
        catch (JsonException ex)
        {
            logger.LogError(ex, "JSON parsing error occurred while processing moon phases");
            return new List<MoonData>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while fetching moon phases");
            return new List<MoonData>();
        }
    }

    public async Task<MoonData> GetMoonPhaseForDayAsync(DateTime date)
    {
        try
        {
            logger.LogInformation("Fetching moon phase for date {Date}", date);

            var formattedDate = date.ToString("yyyy-MM-dd");
            var url = $"https://aa.usno.navy.mil/api/rstt/oneday?date={formattedDate}";

            logger.LogInformation("Making API call to URL: {Url}", url);

            var response = await httpClient.GetStringAsync(url);
            var data = JsonSerializer.Deserialize<JsonElement>(response);

            var properties = data.GetProperty("properties");

            var moonPhase = new MoonData
            {
                Phase = MapPhaseStringToEnum(properties.GetProperty("curphase").GetString() ?? string.Empty),
                Date = date
            };

            logger.LogInformation("Successfully fetched moon phase: {PhaseName}", moonPhase.Phase.ToString());
            return moonPhase;
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "HTTP error occurred while fetching moon phase");
            return new MoonData();
        }
        catch (JsonException ex)
        {
            logger.LogError(ex, "JSON parsing error occurred while processing moon phase");
            return new MoonData();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while fetching moon phase");
            return new MoonData();
        }
    }
    
    private void SaveToDatabase(List<MoonData> moonData )
    {
        //TODO: Implement database saving logic
        // remove return statement when implemented
        // Placeholder for database saving logic
        foreach (var data in moonData)
        {
            logger.LogInformation("Saving moon data to database: {PhaseName} on {Date}", data.Phase.ToString(), data.Date);
        }
        
            
    }

    private MoonPhase MapPhaseStringToEnum(string phase)
    {
        // Normalize input
        var normalized = phase.Trim().ToLowerInvariant();

        return normalized switch
        {
            "new" or "new moon" or "neumond" => MoonPhase.Neumond,
            "first" or "first quarter" or "first quarter" or "zunehmender halbmond" or "zunehmender_halbmond" => MoonPhase.ZunehmenderHalbmond,
            "full" or "full moon" or "vollmond" => MoonPhase.Vollmond,
            "last" or "last quarter" or "last quarter" or "waning half" or "abnehmender halbmond" or "abnehmender_halbmond" => MoonPhase.AbnehmenderHalbmond,
            _ => MoonPhase.Neumond
        };
    }
}