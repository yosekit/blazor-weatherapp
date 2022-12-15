using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.Server.Controllers
{
    [ApiController]
    [Route("api/forecast")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get([FromQuery]string q, [FromQuery]int days)
        {
            return "api_response";
        }
    }
}