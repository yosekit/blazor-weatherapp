﻿using System.Net.Http.Json;
using System.Text.Json.Nodes;

using WeatherApp.Shared.Utilities;
using WeatherApp.Client.Application.Models.Dto.Countries;
using WeatherApp.Client.Application.Options;

namespace WeatherApp.Client.Application.Services
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

        public async Task<StatesDto> GetStatesOfCountry(string country)
        {
            var queryBuilder = new QueryStringBuilder();

            queryBuilder.Add(nameof(country), country);

            return await GetAsync<StatesDto>(
                _options.StatesOfCountry + queryBuilder.Build());
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
            catch (Exception e)
            {
                //Log exception
                throw;
            }
        }
    }
}
