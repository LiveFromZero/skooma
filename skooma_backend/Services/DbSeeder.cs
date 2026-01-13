using skooma_backend.Data;
using skooma_backend.Models;

namespace skooma_backend.Services;

public static class DbSeeder
{
    public static void SeedDatabase(AppDbContext context)
    {
        // Lösche alte Daten
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        // Mock Locations
        var locations = new List<Location>
        {
            new Location { Id = 1, CountryName = "USA", Latitude = "28.5", Longitude = "-80.5" },
            new Location { Id = 2, CountryName = "Russia", Latitude = "45.9", Longitude = "63.3" },
            new Location { Id = 3, CountryName = "China", Latitude = "28.2", Longitude = "102.0" }
        };

        // Mock Launches für 2023
        var launches = new List<Launch>
        {
            new Launch { Id = "launch-001", RocketName = "Falcon 9", LaunchDate = new DateTimeOffset(new DateTime(2023, 1, 15)), Location = locations[0], Status = "Success" },
            new Launch { Id = "launch-002", RocketName = "Soyuz", LaunchDate = new DateTimeOffset(new DateTime(2023, 1, 22)), Location = locations[1], Status = "Success" },
            new Launch { Id = "launch-003", RocketName = "Long March", LaunchDate = new DateTimeOffset(new DateTime(2023, 2, 6)), Location = locations[2], Status = "Failure" },
            new Launch { Id = "launch-004", RocketName = "Falcon 9", LaunchDate = new DateTimeOffset(new DateTime(2023, 2, 14)), Location = locations[0], Status = "Success" },
            new Launch { Id = "launch-005", RocketName = "Ariane 5", LaunchDate = new DateTimeOffset(new DateTime(2023, 3, 7)), Location = locations[0], Status = "Success" },
            new Launch { Id = "launch-006", RocketName = "Falcon Heavy", LaunchDate = new DateTimeOffset(new DateTime(2023, 3, 21)), Location = locations[0], Status = "Success" },
            new Launch { Id = "launch-007", RocketName = "Soyuz", LaunchDate = new DateTimeOffset(new DateTime(2023, 4, 5)), Location = locations[1], Status = "Failure" },
            new Launch { Id = "launch-008", RocketName = "Long March", LaunchDate = new DateTimeOffset(new DateTime(2023, 4, 19)), Location = locations[2], Status = "Success" },
            new Launch { Id = "launch-009", RocketName = "Falcon 9", LaunchDate = new DateTimeOffset(new DateTime(2023, 5, 3)), Location = locations[0], Status = "Success" },
            new Launch { Id = "launch-010", RocketName = "Delta IV", LaunchDate = new DateTimeOffset(new DateTime(2023, 5, 28)), Location = locations[0], Status = "Success" },
            new Launch { Id = "launch-011", RocketName = "Falcon 9", LaunchDate = new DateTimeOffset(new DateTime(2023, 6, 13)), Location = locations[0], Status = "Success" },
            new Launch { Id = "launch-012", RocketName = "Soyuz", LaunchDate = new DateTimeOffset(new DateTime(2023, 6, 25)), Location = locations[1], Status = "Success" },
            new Launch { Id = "launch-013", RocketName = "Long March", LaunchDate = new DateTimeOffset(new DateTime(2023, 7, 9)), Location = locations[2], Status = "Failure" },
            new Launch { Id = "launch-014", RocketName = "Falcon 9", LaunchDate = new DateTimeOffset(new DateTime(2023, 7, 24)), Location = locations[0], Status = "Success" },
            new Launch { Id = "launch-015", RocketName = "Ariane 5", LaunchDate = new DateTimeOffset(new DateTime(2023, 8, 8)), Location = locations[0], Status = "Success" },
            new Launch { Id = "launch-016", RocketName = "Falcon 9", LaunchDate = new DateTimeOffset(new DateTime(2023, 8, 16)), Location = locations[0], Status = "Success" },
            new Launch { Id = "launch-017", RocketName = "Soyuz", LaunchDate = new DateTimeOffset(new DateTime(2023, 9, 1)), Location = locations[1], Status = "Success" },
            new Launch { Id = "launch-018", RocketName = "Long March", LaunchDate = new DateTimeOffset(new DateTime(2023, 9, 14)), Location = locations[2], Status = "Success" },
            new Launch { Id = "launch-019", RocketName = "Falcon Heavy", LaunchDate = new DateTimeOffset(new DateTime(2023, 10, 5)), Location = locations[0], Status = "Success" },
            new Launch { Id = "launch-020", RocketName = "Falcon 9", LaunchDate = new DateTimeOffset(new DateTime(2023, 10, 28)), Location = locations[0], Status = "Success" },
        };

        // Mock MoonData für 2023 (alle Mondphasen)
        var moonData = new List<MoonData>
        {
            // Januar
            new MoonData { Id = "moon-001", Phase = MoonPhase.Neumond, MoonDate = new DateTime(2023, 1, 6) },
            new MoonData { Id = "moon-002", Phase = MoonPhase.ZunehmenderHalbmond, MoonDate = new DateTime(2023, 1, 15) },
            new MoonData { Id = "moon-003", Phase = MoonPhase.Vollmond, MoonDate = new DateTime(2023, 1, 22) },
            new MoonData { Id = "moon-004", Phase = MoonPhase.AbnehmenderHalbmond, MoonDate = new DateTime(2023, 1, 28) },
            
            // Februar
            new MoonData { Id = "moon-005", Phase = MoonPhase.Neumond, MoonDate = new DateTime(2023, 2, 5) },
            new MoonData { Id = "moon-006", Phase = MoonPhase.ZunehmenderHalbmond, MoonDate = new DateTime(2023, 2, 14) },
            new MoonData { Id = "moon-007", Phase = MoonPhase.Vollmond, MoonDate = new DateTime(2023, 2, 20) },
            new MoonData { Id = "moon-008", Phase = MoonPhase.AbnehmenderHalbmond, MoonDate = new DateTime(2023, 2, 27) },
            
            // März
            new MoonData { Id = "moon-009", Phase = MoonPhase.Neumond, MoonDate = new DateTime(2023, 3, 7) },
            new MoonData { Id = "moon-010", Phase = MoonPhase.ZunehmenderHalbmond, MoonDate = new DateTime(2023, 3, 15) },
            new MoonData { Id = "moon-011", Phase = MoonPhase.Vollmond, MoonDate = new DateTime(2023, 3, 21) },
            new MoonData { Id = "moon-012", Phase = MoonPhase.AbnehmenderHalbmond, MoonDate = new DateTime(2023, 3, 29) },
            
            // April
            new MoonData { Id = "moon-013", Phase = MoonPhase.Neumond, MoonDate = new DateTime(2023, 4, 5) },
            new MoonData { Id = "moon-014", Phase = MoonPhase.ZunehmenderHalbmond, MoonDate = new DateTime(2023, 4, 13) },
            new MoonData { Id = "moon-015", Phase = MoonPhase.Vollmond, MoonDate = new DateTime(2023, 4, 19) },
            new MoonData { Id = "moon-016", Phase = MoonPhase.AbnehmenderHalbmond, MoonDate = new DateTime(2023, 4, 27) },
            
            // Mai
            new MoonData { Id = "moon-017", Phase = MoonPhase.Neumond, MoonDate = new DateTime(2023, 5, 3) },
            new MoonData { Id = "moon-018", Phase = MoonPhase.ZunehmenderHalbmond, MoonDate = new DateTime(2023, 5, 12) },
            new MoonData { Id = "moon-019", Phase = MoonPhase.Vollmond, MoonDate = new DateTime(2023, 5, 19) },
            new MoonData { Id = "moon-020", Phase = MoonPhase.Vollmond, MoonDate = new DateTime(2023, 5, 28) },
            
            // Juni
            new MoonData { Id = "moon-021", Phase = MoonPhase.ZunehmenderHalbmond, MoonDate = new DateTime(2023, 6, 10) },
            new MoonData { Id = "moon-022", Phase = MoonPhase.Vollmond, MoonDate = new DateTime(2023, 6, 18) },
            new MoonData { Id = "moon-023", Phase = MoonPhase.AbnehmenderHalbmond, MoonDate = new DateTime(2023, 6, 25) },
            
            // Juli
            new MoonData { Id = "moon-024", Phase = MoonPhase.Neumond, MoonDate = new DateTime(2023, 7, 9) },
            new MoonData { Id = "moon-025", Phase = MoonPhase.ZunehmenderHalbmond, MoonDate = new DateTime(2023, 7, 17) },
            new MoonData { Id = "moon-026", Phase = MoonPhase.Vollmond, MoonDate = new DateTime(2023, 7, 24) },
            
            // August
            new MoonData { Id = "moon-027", Phase = MoonPhase.Neumond, MoonDate = new DateTime(2023, 8, 8) },
            new MoonData { Id = "moon-028", Phase = MoonPhase.ZunehmenderHalbmond, MoonDate = new DateTime(2023, 8, 16) },
            new MoonData { Id = "moon-029", Phase = MoonPhase.Vollmond, MoonDate = new DateTime(2023, 8, 23) },
            
            // September
            new MoonData { Id = "moon-030", Phase = MoonPhase.Neumond, MoonDate = new DateTime(2023, 9, 1) },
            new MoonData { Id = "moon-031", Phase = MoonPhase.ZunehmenderHalbmond, MoonDate = new DateTime(2023, 9, 14) },
            new MoonData { Id = "moon-032", Phase = MoonPhase.Vollmond, MoonDate = new DateTime(2023, 9, 21) },
            
            // Oktober
            new MoonData { Id = "moon-033", Phase = MoonPhase.Neumond, MoonDate = new DateTime(2023, 10, 5) },
            new MoonData { Id = "moon-034", Phase = MoonPhase.ZunehmenderHalbmond, MoonDate = new DateTime(2023, 10, 14) },
            new MoonData { Id = "moon-035", Phase = MoonPhase.Vollmond, MoonDate = new DateTime(2023, 10, 28) },
        };

        // Daten hinzufügen - Locations sind bereits korrekt referenziert
        context.Launches.AddRange(launches);
        context.MoonData.AddRange(moonData);
        context.SaveChanges();
    }
}