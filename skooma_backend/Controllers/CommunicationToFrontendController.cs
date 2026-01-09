using Microsoft.AspNetCore.Mvc;

namespace skooma_backend.Controllers
{
    [ApiController]
    [Route("rocket-starts")] //zeigt den basis zweig aus dem url-tree -> /rocket-starts/(alle Optionen hier)
    public class CommunicationToFrontendController : ControllerBase
    {
        // das Get und das jeweils dahinter ist der API-Call und dann die URL, bei welcher reagiert wird

        // GET /rocket-starts?phase=2
        [HttpGet]
        public IActionResult GetRocketStarts(int phase)
        {
            if (phase < 1 || phase > 4)
                return BadRequest("phase muss zwischen 1 und 4 liegen");

            int result = GetRocketStartCount(phase);

            return Ok(result);
        }

        // GET /rocket-starts/successful?phase=2
        [HttpGet("successful")]
        public IActionResult GetSuccessfulRocketStarts(int phase)
        {
            if (phase < 1 || phase > 4)
                return BadRequest("phase muss zwischen 1 und 4 liegen");

            int result = GetRocketStartCount(phase);

            return Ok(result);
        }

        // GET /rocket-starts/success-rate
        [HttpGet("success-rate")]
        public IActionResult GetAverageSuccessRate()
        {
            double result = 75.8; // Platzhalter, später DB

            return Ok(result);
        }

        private static int GetRocketStartCount(int phase) // hier platzhalter, später DB
        {
            return phase switch
            {
                1 => 3,
                2 => 7,
                3 => 12,
                4 => 1,
                _ => 0
            };
        }
    }
}
