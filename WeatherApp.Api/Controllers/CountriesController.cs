using Microsoft.AspNetCore.Mvc;

using AspNetCore.Proxy;
using AspNetCore.Proxy.Options;

using WeatherApp.Shared.Utilities;
using WeatherApp.Api.Settings;

namespace WeatherApp.Api.Controllers
{
    [ApiController]
    [Route("api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly CountriesSettings _settings;

        private IHttpProxyOptionsBuilder _proxyOptionsBuilder = HttpProxyOptionsBuilder.Instance
            .WithHttpClientName("Countries");

        public CountriesController(
            ILogger<CountriesController> logger, CountriesSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        [HttpGet]
        [Route("cities")]
        public Task GetCitiesOfCountry([FromQuery]string country)
        {
            return this.HttpProxyAsync(_settings.CitiesOfCountry, _proxyOptionsBuilder
                .WithBeforeSend((context, message) =>
                {
                    message.Method = HttpMethod.Post;
                    message.Content = JsonContent.Create(
                        QueryStringHelper.Parse(context.Request.QueryString.Value));

                    return Task.CompletedTask;
                })
            .Build());
        }

        [HttpGet]
        [Route("state/cities")]
        public Task GetCitiesInState([FromQuery]string country, [FromQuery]string state)
        {
            return this.HttpProxyAsync(_settings.CitiesInState, _proxyOptionsBuilder
                .WithBeforeSend((context, message) =>
                {
                    message.Method = HttpMethod.Post;
                    message.Content = JsonContent.Create(
                        QueryStringHelper.Parse(context.Request.QueryString.Value));

                    return Task.CompletedTask;
                })
            .Build());
        }

        [HttpGet]
        [Route("states")]
        public Task GetStatesOfCountry([FromQuery] string country)
        {
            return this.HttpProxyAsync(_settings.StatesOfCountry, _proxyOptionsBuilder
                .WithBeforeSend((context, message) =>
                {
                    message.Method = HttpMethod.Post;
                    message.Content = JsonContent.Create(
                        QueryStringHelper.Parse(context.Request.QueryString.Value));

                    return Task.CompletedTask;
                })
            .Build());
        }
    }
}
