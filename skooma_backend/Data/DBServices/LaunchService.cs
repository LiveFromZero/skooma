using skooma_backend.Data.DBModels;
using skooma_backend.Data.DBServices.DTOs;

namespace skooma_backend.Data.DBServices
{
    public class LaunchService
    {
        private readonly AppDbContext _context;

        public LaunchService(AppDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task<DBLaunch> AddLaunchAsync(DBLaunch launch)
        {
            _context.Launches.Add(launch);
            await _context.SaveChangesAsync();
            return launch;
        }

        // Read - Get all
        public async Task<List<DBLaunch>> GetAllLaunchesAsync()
        {
            return await Task.FromResult(_context.Launches.ToList());
        }

        // Read - Get by ID
        public async Task<DBLaunch?> GetLaunchByIdAsync(int id)
        {
            return await Task.FromResult(_context.Launches.FirstOrDefault(l => l.Id == id));
        }

        // Read - Get by location
        public async Task<List<DBLaunch>> GetLaunchesByLocationAsync(int locationId)
        {
            return await Task.FromResult(_context.Launches
                .Where(l => l.LocationId == locationId)
                .ToList());
        }

        // Read - Get by status
        public async Task<List<DBLaunch>> GetLaunchesByStatusAsync(string status)
        {
            return await Task.FromResult(_context.Launches
                .Where(l => l.Status == status)
                .ToList());
        }

        // Update
        public async Task<DBLaunch?> UpdateLaunchAsync(int id, DBLaunch updatedLaunch)
        {
            var launch = _context.Launches.FirstOrDefault(l => l.Id == id);
            if (launch == null) return null;

            launch.RocketName = updatedLaunch.RocketName;
            launch.LaunchDate = updatedLaunch.LaunchDate;
            launch.Status = updatedLaunch.Status;
            launch.LocationId = updatedLaunch.LocationId;

            await _context.SaveChangesAsync();
            return launch;
        }

        // Delete
        public async Task<bool> DeleteLaunchAsync(int id)
        {
            var launch = _context.Launches.FirstOrDefault(l => l.Id == id);
            if (launch == null) return false;

            _context.Launches.Remove(launch);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get launch statistics filtered by year and rocket type, grouped by moon phase
        public async Task<LaunchFilterResultDto> GetLaunchStatisticsByYearAndRocketAsync(string year, string rocketType)
        {
            var launches = _context.Launches.AsEnumerable();

            // Filter by year if not "all"
            if (year != "all")
            {
                if (int.TryParse(year, out int yearInt))
                {
                    launches = launches.Where(l => l.LaunchDate.Year == yearInt);
                }
            }

            // Filter by rocket type if not "all"
            if (rocketType != "all" && !string.IsNullOrEmpty(rocketType))
            {
                launches = launches.Where(l => l.RocketName == rocketType);
            }

            // Get moon phase data
            var moonDataMap = _context.MoonData.ToDictionary(m => m.Date.Date, m => m.MoonPhase);

            // Group by moon phase and calculate statistics
            var statistics = launches
                .GroupBy(l => moonDataMap.ContainsKey(l.LaunchDate.Date) ? moonDataMap[l.LaunchDate.Date] : 0)
                .Select(g => new MoonPhaseStatisticsDto
                {
                    MoonPhase = g.Key,
                    TotalLaunches = g.Count(),
                    SuccessfulLaunches = g.Count(l => l.Status == "Success"),
                    SuccessRate = Math.Round(g.Count(l => l.Status == "Success") * 100.0 / g.Count(), 2)
                })
                .OrderBy(s => s.MoonPhase)
                .ToList();

            return new LaunchFilterResultDto
            {
                Year = year == "all" ? 0 : (int.TryParse(year, out int y) ? y : 0),
                RocketType = rocketType,
                Statistics = statistics
            };
        }
    }
}

