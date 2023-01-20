using Microsoft.AspNetCore.Mvc;

using AspNetCore.Proxy;
using AspNetCore.Proxy.Options;

using WeatherApp.Shared.Services;
using WeatherApp.Api.Settings;
using WeatherApp.Api.Utilities;

namespace WeatherApp.Api.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly WeatherSettings _settings;

        private IHttpProxyOptionsBuilder _proxyOptionsBuilder = HttpProxyOptionsBuilder.Instance
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
            });

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

            return this.HttpProxyAsync(_settings.Forecast + queryBuilder.Build(), _proxyOptionsBuilder
                .WithAfterReceive((context, message) =>
                {
                    message = context.RequestServices.GetRequiredService<WeatherResponseProducer>()
                    .FromMessage(message)
                    .EditConditions()
                    .Produce();

                    return Task.CompletedTask;
                })
            .Build());
        }

        [HttpGet]
        [Route("astronomy")]
        public Task GetAstronomy([FromQuery] string city)
        {
            return this.HttpProxyAsync(_settings.Astronomy, _proxyOptionsBuilder.Build());
        }
    }
}