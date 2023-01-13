using Microsoft.AspNetCore.Mvc;

using AspNetCore.Proxy;
using AspNetCore.Proxy.Options;

using WeatherApp.Api.Settings;
using WeatherApp.Shared.Services;

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

                var queryBuilder = new QueryStringBuilder(
                    QueryStringHelper.Parse(message.RequestUri.Query));

                // Add API key auth
                queryBuilder.Add(settings.Auth!.Key!, settings.Auth!.Value!);
                // Add required param for all APIs
                queryBuilder.Add("q", context.Request.Query["city"]);

                message.RequestUri = new Uri(
                    message.RequestUri.GetLeftPart(UriPartial.Path) + queryBuilder.Build());
                
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
        [Route("forecast")]
        public Task GetForecast([FromQuery] string city)
        {
            var queryBuilder = new QueryStringBuilder();

            queryBuilder.Add("days", "14");
            queryBuilder.Add("aqi", "no");
            queryBuilder.Add("alerts", "no");

            return this.HttpProxyAsync(_settings.Forecast + queryBuilder.Build(), _proxyOptions);
        }

        [HttpGet]
        [Route("astronomy")]
        public Task GetAstronomy([FromQuery] string city)
        {
            return this.HttpProxyAsync(_settings.Astronomy, _proxyOptions);
        }
    }
}