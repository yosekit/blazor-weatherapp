using System.Net.Http.Json;

using WeatherApp.Client.Options;
using WeatherApp.Client.Models.Dto;

namespace WeatherApp.Client.Services
{
    public class CountriesService
    {
        private readonly HttpClient _httpClient;
        private readonly CountriesOptions _countriesOptions;

        public CountriesService(HttpClient httpClient, CountriesOptions countriesOptions)
        {
            _httpClient = httpClient;
            _countriesOptions = countriesOptions;
        }
    }
}
