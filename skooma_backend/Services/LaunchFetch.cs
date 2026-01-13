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
            Dictionary<int, Location> locationCache = new Dictionary<int, Location>();
            
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
                    .Select(launch =>
                    {
                        // Skip if critical data is missing
                        if (!launch.TryGetProperty("pad", out var pad) || pad.ValueKind == JsonValueKind.Null ||
                            !pad.TryGetProperty("location", out var locationElement) || locationElement.ValueKind == JsonValueKind.Null ||
                            !locationElement.TryGetProperty("id", out var locationIdElement))
                        {
                            return null;
                        }
                        
                        var locationId = locationIdElement.GetInt32();
                        
                        // Use cached location or create new one
                        if (!locationCache.ContainsKey(locationId))
                        {
                            var countryName = string.Empty;
                            if (pad.TryGetProperty("country", out var country) && country.ValueKind != JsonValueKind.Null)
                            {
                                countryName = country.GetProperty("name").GetString() ?? string.Empty;
                            }
                            
                            locationCache[locationId] = new Location
                            {
                                Id = locationId,
                                CountryName = countryName,
                                Latitude = pad.TryGetProperty("latitude", out var lat) && lat.ValueKind != JsonValueKind.Null 
                                    ? lat.GetDouble().ToString() 
                                    : string.Empty,
                                Longitude = pad.TryGetProperty("longitude", out var lon) && lon.ValueKind != JsonValueKind.Null 
                                    ? lon.GetDouble().ToString() 
                                    : string.Empty
                            };
                        }
                        
                        var rocketName = string.Empty;
                        if (launch.TryGetProperty("rocket", out var rocket) && rocket.ValueKind != JsonValueKind.Null &&
                            rocket.TryGetProperty("configuration", out var config) && config.ValueKind != JsonValueKind.Null)
                        {
                            rocketName = config.GetProperty("name").GetString() ?? string.Empty;
                        }
                        
                        var status = "Unknown";
                        if (launch.TryGetProperty("status", out var statusObj) && statusObj.ValueKind != JsonValueKind.Null)
                        {
                            status = statusObj.GetProperty("abbrev").GetString() ?? "Unknown";
                        }
                        
                        return new Launch
                        {
                            Id = launch.GetProperty("id").GetString() ?? string.Empty,
                            RocketName = rocketName,
                            LaunchDate = launch.GetProperty("net").GetDateTimeOffset(),
                            Location = locationCache[locationId],
                            Status = status
                        };
                    })
                    .Where(l => l != null)
                    .ToList()!;

                launches.AddRange(newlaunches);

                url = data.TryGetProperty("next", out var next) && next.ValueKind != JsonValueKind.Null 
                    ? next.GetString() ?? string.Empty 
                    : string.Empty;
                    
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
}