namespace WeatherApp.Options
{
    public class CountriesOptions
    {
        public const string Key = "Countries";

        public string BaseUrl { get; set; } = String.Empty;
        public string CitiesEndpoint { get; set; } = String.Empty;
    }
}
