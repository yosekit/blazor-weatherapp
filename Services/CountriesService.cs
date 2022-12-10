using System.Net.Http.Json;

using WeatherApp.Options;
using WeatherApp.Models.Dto;

namespace WeatherApp.Services
{
    public class CountriesService
    {
        private readonly HttpClient _httpClient;
        private CountriesOptions _countriesOptions;

        public CountriesService(HttpClient httpClient, CountriesOptions countriesOptions)
        {
            _httpClient = httpClient;
            _countriesOptions = countriesOptions;
        }
    }
}
