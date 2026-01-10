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
            // TODO START
            // THIS is DUMMYDATA for API Call with frontend, needs to be changed to data from models and database
            double BackendDataToSendBack = 30.2;

            double result = BackendDataToSendBack; // needs to be adjustet when connected with backend

            // TODO END
            return Ok(result);
        }


        [HttpGet("rocketlaunchdata")]
        public IActionResult GetRocketLaunchData(string year, string rocketType)
        {
            // TODO START
            // THIS is DUMMYDATA for API Call with frontend, needs to be changed to data from models and database

            var backendDataToSendBack = new[]
            {
            new { CountLaunches = 10, MoonPhase = 1, CountSuccessLaunches = 1 },
            new { CountLaunches = 10, MoonPhase = 2, CountSuccessLaunches = 1 },
            new { CountLaunches = 15, MoonPhase = 3, CountSuccessLaunches = 1 },
            new { CountLaunches = 15, MoonPhase = 4, CountSuccessLaunches = 1 }
            };


            var result = backendDataToSendBack; // needs to be adjustet when connected with backend
            // TODO END

            return Ok(result);
        }

    }
}
