using System.Text.Json.Nodes;
using System.Net.Http.Json;

using WeatherApp.Shared.Utilities;
using WeatherApp.Client.Application.Models.Dto.Astronomy;
using WeatherApp.Client.Application.Models.Dto.Forecast;
using WeatherApp.Client.Application.Options;

namespace WeatherApp.Client.Application.Services
{
    public class WeatherService
    {
        private readonly ILogger<WeatherService> _logger;
        private readonly HttpClient _httpClient;
        private readonly WeatherOptions _options;

        public WeatherService(ILogger<WeatherService> logger, HttpClient client, WeatherOptions options)
        {
            _logger = logger;
            _httpClient = client;
            _options = options;
        }

        public async Task<ForecastDto> GetForecastAsync(string city)
        {
            var queryBuilder = new QueryStringBuilder();

            queryBuilder.Add(nameof(city), city);

            return await GetAsync<ForecastDto>(
                _options.Forecast + queryBuilder.Build());
        }

        public async Task<AstronomyDto> GetAstronomyAsync(string city)
        {
            var queryBuilder = new QueryStringBuilder();

            queryBuilder.Add(nameof(city), city);

            return await GetAsync<AstronomyDto>(
                _options.Astronomy + queryBuilder.Build());
        }

        private async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                var response = await _httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default;
                    }

                    return await response.Content.ReadFromJsonAsync<T>();
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var error = JsonNode.Parse(content)!["error"];

                    throw new Exception($"Http status code: {response.StatusCode} " +
                        $"code: {error!["code"]} message: {error!["message"]} source: {error!["source"]}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }
    }
}
