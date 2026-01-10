using Microsoft.AspNetCore.Mvc;

namespace skooma_backend.Controllers
{
    [ApiController]
    [Route("rocket-starts")]
    public class CommunicationToFrontendController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAvgSuccessfullRocketStarts(string year)
        {
            double BackendDataToSendBack = 30.2;

            double result = BackendDataToSendBack;

            return Ok(result);
        }


        // GET /rocket-starts/success-rate
        [HttpGet("success-rate")]
        public IActionResult GetAverageSuccessRate()
        {
            double result = 75.8; // Platzhalter, später DB

            return Ok(result);
        }

    }
}
