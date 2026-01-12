using skooma_backend.Data.DBModels;

namespace skooma_backend.Data.DBServices
{
    public class MoonDataService
    {
        private readonly AppDbContext _context;

        public MoonDataService(AppDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task<DBMoonData> AddMoonDataAsync(DBMoonData moonData)
        {
            _context.MoonData.Add(moonData);
            await _context.SaveChangesAsync();
            return moonData;
        }

        // Read - Get all
        public async Task<List<DBMoonData>> GetAllMoonDataAsync()
        {
            return await Task.FromResult(_context.MoonData.ToList());
        }

        // Read - Get by ID
        public async Task<DBMoonData?> GetMoonDataByIdAsync(int id)
        {
            return await Task.FromResult(_context.MoonData.FirstOrDefault(m => m.ID == id));
        }

        // Read - Get by date range
        public async Task<List<DBMoonData>> GetMoonDataByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await Task.FromResult(_context.MoonData
                .Where(m => m.Date >= startDate && m.Date <= endDate)
                .ToList());
        }

        // Update
        public async Task<DBMoonData?> UpdateMoonDataAsync(int id, DBMoonData updatedMoonData)
        {
            var moonData = _context.MoonData.FirstOrDefault(m => m.ID == id);
            if (moonData == null) return null;

            moonData.MoonPhase = updatedMoonData.MoonPhase;
            moonData.Date = updatedMoonData.Date;

            await _context.SaveChangesAsync();
            return moonData;
        }

        // Delete
        public async Task<bool> DeleteMoonDataAsync(int id)
        {
            var moonData = _context.MoonData.FirstOrDefault(m => m.ID == id);
            if (moonData == null) return false;

            _context.MoonData.Remove(moonData);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
