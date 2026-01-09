namespace skooma_backend.Models;

public class Location
{
    public int Id { get; set; } = 0;
    public string CountryName { get; set; } = string.Empty;
    public string Latitude { get; set; } = string.Empty;
    public string Longitude { get; set; } = string.Empty;
}