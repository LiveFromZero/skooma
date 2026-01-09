using System.Text.Json;
using skooma_backend.Models;

namespace skooma_backend.Services;

public class LaunchFetch(HttpClient httpClient, ILogger<LaunchFetch> logger)
{
    public async Task<List<Launch>> GetLaunchesFromTimeframeAsync(DateTime startingDate, DateTime endingDate)
    {
        try
        {
            logger.LogInformation("Fetching launches from {StartingDate} to {EndingDate}", startingDate, endingDate);

            List<Launch> launches = new List<Launch>();
            var startDate = startingDate.ToString("yyyy-MM-dd");
            var endDate = endingDate.ToString("yyyy-MM-dd");

            var url = $"https://ll.thespacedevs.com/2.3.0/launches/?net__gte={startDate}&net__lte={endDate}&limit=100";

            JsonElement data;

            do
            {
                logger.LogInformation("Making API call to URL: {Url}", url);

                var response = await httpClient.GetStringAsync(url);

                data = JsonSerializer.Deserialize<JsonElement>(response);

                var newlaunches = data.GetProperty("results").EnumerateArray()
                    .Select(launch => new Launch
                    {
                        Id = launch.GetProperty("id").GetString() ?? string.Empty,
                        RocketName =
                            launch.GetProperty("rocket").GetProperty("configuration").GetProperty("name").GetString() ??
                            string.Empty,
                        Date = launch.GetProperty("net").GetDateTimeOffset(),
                        Location = new Location
                        {
                            Id = launch.GetProperty("pad").GetProperty("location").GetProperty("id").GetInt32(),
                            CountryName =
                                launch.GetProperty("pad").GetProperty("country").GetProperty("name").GetString() ??
                                string.Empty,
                            Latitude = launch.GetProperty("pad").GetProperty("latitude").GetDouble().ToString() ??
                                       string.Empty,
                            Longitude = launch.GetProperty("pad").GetProperty("longitude").GetDouble().ToString() ??
                                        string.Empty,
                        },
                        Status = launch.GetProperty("status").GetProperty("abbrev").GetString() ?? "Unknown"
                    })
                    .ToList();

                launches.AddRange(newlaunches);

                url = data.GetProperty("next").GetString() ?? string.Empty;
                logger.LogInformation("Next URL: {NextUrl}", url);
            } while (!string.IsNullOrEmpty(url));

            logger.LogInformation("Successfully fetched {LaunchCount} launches", launches.Count);
            return launches;
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "HTTP error occurred while fetching launches");
            return new List<Launch>();
        }
        catch (JsonException ex)
        {
            logger.LogError(ex, "JSON parsing error occurred while processing launches");
            return new List<Launch>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while fetching launches");
            return new List<Launch>();
        }
    }
    
    
    public async Task<List<Launch>> GetLaunchesByYearAsync(int year)
    {
        var startingDate = new DateTime(year, 1, 1);
        var endingDate = new DateTime(year, 12, 31);
        return await GetLaunchesFromTimeframeAsync(startingDate, endingDate);
    }
    
    private void SaveLaunchesToDatabase(List<Launch> launches)
    {
        // TODO:
        // remove return statement when implemented
        // Placeholder for database saving logic
        logger.LogInformation("Saving {LaunchCount} launches to the database", launches.Count);
    }
}