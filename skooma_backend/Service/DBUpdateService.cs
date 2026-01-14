using System.Runtime.CompilerServices;
using skooma_backend.Data.DBModels;
using skooma_backend.Data.DBServices;
using skooma_backend.Models;

namespace skooma_backend.Services;

public class DBUpdateService
{
    private readonly LaunchFetch _launchFetch;
    private readonly MoonFetch _moonFetch;
    private readonly MoonDataService _moonDB;
    private readonly LaunchService _launchDB;

    public DBUpdateService(LaunchFetch launchFetch, MoonFetch moonFetch, MoonDataService moonDB, LaunchService launchDB)
    {
        _launchFetch = launchFetch;
        _moonFetch = moonFetch;
        _moonDB = moonDB;
        _launchDB = launchDB;
    }
    
    public async Task UpdateAllDataAsync()
    {
        DateTime year = DateTime.Now;
        
        // Update moon data
        List<MoonData> moonData = await _moonFetch.GetMoonPhasesForYearAsync(year.Year);
        if(moonData.Count > 0){
        List<DBMoonData> moonDataDB = PressIntoDBFormat(moonData);
        foreach(DBMoonData entry in moonDataDB)
        {    
        await _moonDB.AddMoonDataAsync(entry);
        }
        }
        
        // Update launch data
        DateTime startDate = new DateTime(year.Year, 1, 1);
        DateTime endDate = new DateTime(year.Year, 12, 31);
        List<Launch> launches = await _launchFetch.GetLaunchesFromTimeframeAsync(startDate, endDate);
        if(launches.Count > 0){
        List<DBLaunch> launchesDB = PressLaunchesIntoDBFormat(launches);
        foreach(DBLaunch entry in launchesDB)
        {
        await _launchDB.AddLaunchAsync(entry);
        }
        }
    }

    private List<DBMoonData> PressIntoDBFormat(List<MoonData> moonData)
    {
        List<DBMoonData> DBData =new();

        foreach(MoonData entry in moonData)
        {
            DBData.Add(new DBMoonData
            {
            MoonPhase = (int)entry.Phase,
            Date = entry.Date
            });
        }
        return DBData;
    }

    private List<DBLaunch> PressLaunchesIntoDBFormat(List<Launch> launches)
    {
        List<DBLaunch> DBData = new();

        foreach(Launch entry in launches)
        {
            DBData.Add(new DBLaunch
            {
                RocketName = entry.RocketName,
                LaunchDate = entry.LaunchDate,
                Status = entry.Status,
                Location = new DBLocation
                {
                    LocationId = entry.Location.Id,
                    CountryName = entry.Location.CountryName,
                    Latitude = entry.Location.Latitude,
                    Longitude = entry.Location.Longitude
                }
            });
        }
        return DBData;
    }
}