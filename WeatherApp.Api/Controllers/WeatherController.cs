using Microsoft.AspNetCore.Mvc;

using AspNetCore.Proxy;
using AspNetCore.Proxy.Options;

using WeatherApp.Api.Settings;

namespace WeatherApp.Api.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly WeatherSettings _settings;

        private HttpProxyOptions _proxyOptions = HttpProxyOptionsBuilder.Instance
            .WithHttpClientName("Weather")
            .WithBeforeSend((context, message) =>
            {
                var settings = context.RequestServices.GetRequiredService<WeatherSettings>();
                context.Request.QueryString.Add(settings.Auth!.Key!, settings.Auth!.Value!);

                /*message.RequestUri = new Uri(
                    message.RequestUri!.ToString() +
                    $"?{settings.Auth!.Key}={settings.Auth!.Value}");*/

                return Task.CompletedTask;
            })
            .Build();

        public WeatherController(
            ILogger<WeatherController> logger, WeatherSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        [HttpGet]
        [Route("/forecast")]
        public Task GetForecast()
        {
            _logger.LogInformation("FORECAST: " + _settings.Forecast + Request.QueryString.Value);
            return this.HttpProxyAsync(_settings.Forecast + Request.QueryString.Value, _proxyOptions);
        }

        [HttpGet]
        [Route("/astronomy")]
        public Task GetAstronomy()
        {
            return this.HttpProxyAsync(_settings.Astronomy + Request.QueryString.Value, _proxyOptions);
        }
    }
}