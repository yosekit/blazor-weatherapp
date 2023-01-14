namespace WeatherApp.Options
{
    public class CountriesOptions
    {
        public const string JsonName = "Countries";
        public string? CitiesOfCountry { get; set; }
        public string? CitiesInState { get; set; }
        public string? StatesOfCountry { get; set; }
    }
}
