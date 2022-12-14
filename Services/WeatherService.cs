using System.Text.Json.Nodes;
using System.Net.Http.Json;

using WeatherApp.Options;
using WeatherApp.Models.Dto;
using WeatherApp.Infrastructure.Services;

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

        public async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                var response = await _httpClient.GetAsync(_httpClient.BaseAddress + uri);

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

        public async Task<ForecastDto> GetForecastAsync(string q, int days)
        {
            var queryBuilder = new QueryStringBuilder();

            queryBuilder.Add(nameof(q), q);
            queryBuilder.Add(nameof(days), days.ToString());

            AddAuthQuery(queryBuilder);

            return await GetAsync<ForecastDto>(
                _options.ForecastEndpoint + queryBuilder.Build());
        }

        public async Task<AstronomyDto> GetAstronomyAsync(string q)
        {
            var queryBuilder = new QueryStringBuilder();

            queryBuilder.Add(nameof(q), q);

            AddAuthQuery(queryBuilder);

            return await GetAsync<AstronomyDto>(
                _options.AstronomyEndpoint + queryBuilder.Build());
        }

        private void AddAuthQuery(QueryStringBuilder builder)
        {
            builder.Add("auth_key", "auth_value");
        }
    }
}
