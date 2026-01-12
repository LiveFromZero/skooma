using skooma_backend.Data.DBModels;

namespace skooma_backend.Data.DBServices
{
    public class LocationService
    {
        private readonly AppDbContext _context;

        public LocationService(AppDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task<DBLocation> AddLocationAsync(DBLocation location)
        {
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            return location;
        }

        // Read - Get all
        public async Task<List<DBLocation>> GetAllLocationsAsync()
        {
            return await Task.FromResult(_context.Locations.ToList());
        }

        // Read - Get by ID
        public async Task<DBLocation?> GetLocationByIdAsync(int id)
        {
            return await Task.FromResult(_context.Locations.FirstOrDefault(l => l.LocationId == id));
        }

        // Read - Get by country
        public async Task<List<DBLocation>> GetLocationsByCountryAsync(string countryName)
        {
            return await Task.FromResult(_context.Locations
                .Where(l => l.CountryName.Contains(countryName))
                .ToList());
        }

        // Update
        public async Task<DBLocation?> UpdateLocationAsync(int id, DBLocation updatedLocation)
        {
            var location = _context.Locations.FirstOrDefault(l => l.LocationId == id);
            if (location == null) return null;

            location.CountryName = updatedLocation.CountryName;
            location.Latitude = updatedLocation.Latitude;
            location.Longitude = updatedLocation.Longitude;

            await _context.SaveChangesAsync();
            return location;
        }

        // Delete
        public async Task<bool> DeleteLocationAsync(int id)
        {
            var location = _context.Locations.FirstOrDefault(l => l.LocationId == id);
            if (location == null) return false;

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
