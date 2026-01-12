using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;

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

        [HttpGet("rocketfilterdata")]
        public IActionResult GetRocketFilterData()
        {
            // TODO START
            // THIS is DUMMYDATA for API Call with frontend, needs to be changed to data from models and database

            var backendDataToSendBack = new
            {
                yearOptions = new[]
                {

                    new { value = "2020", label = "2020" },
                    new { value = "2021", label = "2021" },
                    new { value = "2022", label = "2022" },
                    new { value = "2023", label = "2023" },
                },
                
                rocketTypeOptions = new[]
                {
                    new { value = "Type0", label = "Alle Typen" },
                    new { value = "Type1", label = "Type 1"},
                    new { value = "Type2", label = "Type 2"},
                    new { value = "Type3", label = "Type 3"}
                },
            };

            var result = backendDataToSendBack; // needs to be adjustet when connected with backend
            // TODO END

            return Ok(result);
        }

        [HttpGet("updateBackend")]
        public IActionResult UpdateBackendAndDatabase()
        {
            // TODO
            // Add function that starts updating the backend, making api calls to the 3rd party etc.
            // essentially everything that updates the results above

            return Ok();
        }



    }
}
