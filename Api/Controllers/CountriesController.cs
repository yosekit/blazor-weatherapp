using Microsoft.AspNetCore.Mvc;

using AspNetCore.Proxy;
using AspNetCore.Proxy.Options;

using WeatherApp.Shared.Utilities;
using WeatherApp.Api.Settings;
using WeatherApp.Api.Services.ResponseModifiers;
using WeatherApp.Api.Services.ResponseModifiers.ResponseMethods;

namespace WeatherApp.Api.Controllers
{
    [ApiController]
    [Route("api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly CountriesSettings _settings;

        private IHttpProxyOptionsBuilder _proxyOptionsBuilder = HttpProxyOptionsBuilder.Instance
            .WithHttpClientName("Countries")
            .WithBeforeSend((context, message) =>
            {
                string? query = context.Request.QueryString.Value;

                if(query is null)
                {
                    message.Method = HttpMethod.Get;
                }
                else
                {
                    message.Method = HttpMethod.Post;
                    message.Content = JsonContent.Create(QueryStringHelper.Parse(query));
                }

                return Task.CompletedTask;
            });

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
                .WithAfterReceive((context, message) =>
                {
                    var modifier = context.RequestServices.GetRequiredService<CountriesResponseModifier>();

                    modifier.Modify(message, new CitiesOfCountryMethod());

                    return Task.CompletedTask;
                })
            .Build());
        }

        [HttpGet]
        [Route("state/cities")]
        public Task GetCitiesInState([FromQuery]string country, [FromQuery]string state)
        {
            return this.HttpProxyAsync(_settings.CitiesInState, _proxyOptionsBuilder
                .WithAfterReceive((context, message) =>
                {
                    var modifier = context.RequestServices.GetRequiredService<CountriesResponseModifier>();

                    modifier.Modify(message, new CitiesInStateMethod());

                    return Task.CompletedTask;
                })
            .Build());
        }

        [HttpGet]
        [Route("states")]
        public Task GetStatesOfCountry([FromQuery] string country)
        {
            return this.HttpProxyAsync(_settings.StatesOfCountry, _proxyOptionsBuilder
                .WithAfterReceive((context, message) =>
                {
                    var modifier = context.RequestServices.GetRequiredService<CountriesResponseModifier>();

                    modifier.Modify(message, new StatesOfCountryMethod());

                    return Task.CompletedTask;
                })
            .Build());
        }
    }
}
