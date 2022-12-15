namespace WeatherApp.Client.Options
{
    public class CountriesOptions
    {
        public const string JsonName = "Countries";

        public string? BaseUrl { get; set; }
        public string? CitiesEndpoint { get; set; }
    }
}
