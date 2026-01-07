using Microsoft.AspNetCore.Mvc;

namespace skooma_backend.Controllers
{
    [ApiController]
    public class CommunicationToFrontendController : ControllerBase
    {
        // Die Url wäre hier sowas wie: 
        // /GetCountOfRocketStartsForMoonPhase?phase=2
        // Und mit "FromQuery" kann man diese Parameter quasi auslesen
        // also man würde ?phase=4 schreiben, um die Anzahl der Starts für Vollmond bspw zu bekommen
        // später würde man dann "3,7,12,1,0" aus der Datenbank holen mit vorgefertigten Queries,
        // das hier ist aber erstmal eine Beispiel-API-Anfrage vom Frontend
        [HttpGet("/GetCountOfRocketStartsForMoonPhase")]
        public IActionResult Get([FromQuery] int phase)
        {
            if (phase < 1 || phase > 4)
                return BadRequest("phase muss zwischen 1 und 4 liegen");

            int result = phase switch
            {
                1 => 3,
                2 => 7,
                3 => 12,
                4 => 1,
                _ => 0
            };

            return Ok(result);
        }
    }
}
