using System.Net.Http.Json;

using WeatherApp.Options;
using WeatherApp.Models.Dto;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private WeatherOptions _options;

        public WeatherService(WeatherOptions options, HttpClient client)
        {
            _options = options;
            _httpClient = client;
        }

        public async Task<ForecastDto> GetForecast()
        {
            var response = await _httpClient.GetFromJsonAsync<ForecastDto>(_options.ForecastEndpoint);

            return response;
        }

        public async Task<AstronomyDto> GetAstronomy()
        {
            var response = await _httpClient.GetFromJsonAsync<AstronomyDto>(_options.AstronomyEndpoint);

            return response;
        }
    }
}
