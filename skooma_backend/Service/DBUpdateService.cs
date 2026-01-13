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

    public DBUpdateService(LaunchFetch launchFetch, MoonFetch moonFetch, MoonDataService moonDB)
    {
        _launchFetch = launchFetch;
        _moonFetch = moonFetch;
        _moonDB = moonDB;
    }
    
    public async Task UpdateAllDataAsync()
    {
        DateTime year = DateTime.Now;
        List<MoonData> moonData = await _moonFetch.GetMoonPhasesForYearAsync(year.Year);
        if(moonData.Count > 0){
        List<DBMoonData> moonDataDB = PressIntoDBFormat(moonData);
        foreach(DBMoonData entry in moonDataDB)
        {    
        await _moonDB.AddMoonDataAsync(entry);
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
}