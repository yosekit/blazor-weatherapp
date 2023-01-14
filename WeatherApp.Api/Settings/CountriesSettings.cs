namespace WeatherApp.Api.Settings
{
    public class CountriesSettings
    {
        public const string JsonName = "Countries";

        public string? BaseUrl { get; set; }
        public string? CitiesOfCountry { get; set; }
        public string? CitiesInState { get; set; }
        public string? StatesOfCountry { get; set; }
    }
}
