using Microsoft.AspNetCore.Mvc;

using AspNetCore.Proxy;
using AspNetCore.Proxy.Options;

using WeatherApp.Api.Settings;

namespace WeatherApp.Api.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherSettings _settings;

        private HttpProxyOptions _proxyOptions = HttpProxyOptionsBuilder.Instance
            .WithHttpClientName("Weather")
            .WithBeforeSend((context, message) =>
            {
                var settings = context.RequestServices.GetRequiredService<WeatherSettings>();

                // Add to query params API key auth 
                message.RequestUri = new Uri(message.RequestUri + context.Request.QueryString.Add(
                        settings.Auth!.Key!, settings.Auth!.Value!).Value);

                return Task.CompletedTask;
            })
            .Build();

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger, WeatherSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        [HttpGet]
        [Route("forecast")]
        public Task GetForecast()
        {
            return this.HttpProxyAsync(_settings.Forecast, _proxyOptions);
        }

        [HttpGet]
        [Route("astronomy")]
        public Task GetAstronomy()
        {
            return this.HttpProxyAsync(_settings.Astronomy, _proxyOptions);
        }
    }
}