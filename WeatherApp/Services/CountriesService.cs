using System.Net.Http.Json;
using System.Text.Json.Nodes;

using WeatherApp.Options;
using WeatherApp.Shared.Services;
using WeatherApp.Models.Dto.Countries;

namespace WeatherApp.Services
{
    public class CountriesService
    {
        private readonly HttpClient _httpClient;
        private readonly CountriesOptions _options;
        private readonly ILogger<CountriesService> _logger;

        public CountriesService(HttpClient httpClient, CountriesOptions options, ILogger<CountriesService> logger)
        {
            _httpClient = httpClient;
            _options = options;
            _logger = logger;
        }

        public async Task<CitiesDto> GetCitiesOfCountryAsync(string country)
        {
            var queryBuilder = new QueryStringBuilder();

            queryBuilder.Add(nameof(country), country);

            return await GetAsync<CitiesDto>(
                _options.CitiesOfCountry + queryBuilder.Build());
        }

        public async Task<CitiesDto> GetCitiesInStateAsync(string country, string state)
        {
            var queryBuilder = new QueryStringBuilder();

            queryBuilder.Add(nameof(country), country);
            queryBuilder.Add(nameof(state), state);

            return await GetAsync<CitiesDto>(
                _options.CitiesInState + queryBuilder.Build());
        }

        private async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                var response = await _httpClient.GetAsync(uri);

                var node = JsonNode.Parse(await response.Content.ReadAsStringAsync());

                if(!response.IsSuccessStatusCode ||
                    bool.Parse(node["error"].ToString()))
                {
                    throw new Exception($"Error: {node["msg"]}");
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(T);
                    }

                    return await response.Content.ReadFromJsonAsync<T>();
                }
            }
            catch (Exception e)
            {
                //Log exception
                _logger.LogDebug(e.Message);
                throw;
            }
        }
    }
}
