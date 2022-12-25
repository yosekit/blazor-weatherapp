namespace WeatherApp.Api.Settings
{
    public class WeatherSettings
    {
        public const string JsonName = "Weather"; 

        public string? BaseUrl { get; set; }
        public string? Forecast { get; set; }
        public string? Astronomy { get; set; }

        public WeatherAuthSettings? Auth { get; set; }
    }

    public class WeatherAuthSettings
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
    }
}
