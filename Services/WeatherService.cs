using System.Text.Json.Nodes;
using System.Net.Http.Json;
using Microsoft.AspNetCore.WebUtilities;

using WeatherApp.Options;
using WeatherApp.Models.Dto;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly WeatherOptions _options;

        public WeatherService(WeatherOptions options, HttpClient client)
        {
            _options = options;
            _httpClient = client;
        }

        public async Task<T> GetAsync<T>(Uri uri)
        {
            try
            {
                var response = await _httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(T);
                    }

                    return await response.Content.ReadFromJsonAsync<T>();
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var error = JsonNode.Parse(content)!["error"];

                    throw new Exception($"Http status code: {response.StatusCode} code: {error!["code"]} message: {error!["message"]}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<ForecastDto> GetForecastAsync(string location, int days)
        {
            var parameters = new Dictionary<string, string>
            {
                // API key auth
                { "auth_key", "auth_value" },

                { "q", location},
                { "days", days.ToString() }
            };

            return await GetAsync<ForecastDto>(
                BuildUri(_options.ForecastEndpoint, parameters));
        }

        public async Task<AstronomyDto> GetAstronomyAsync(string location)
        {
            var parameters = new Dictionary<string, string>
            {
                // API key auth
                { "auth_key", "auth_value" },

                { "q", location}
            };

            return await GetAsync<AstronomyDto>(
                BuildUri(_options.AstronomyEndpoint, parameters));
        }

        private Uri BuildUri(string endpoint, IDictionary<string, string> queryParams)
        {
            string uri = _httpClient.BaseAddress + endpoint;

            uri = QueryHelpers.AddQueryString(uri, queryParams);

            var builder = new UriBuilder(uri);
            return builder.Uri;
        }
    }
}
