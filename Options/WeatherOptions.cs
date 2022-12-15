namespace WeatherApp.Client.Options
{
    public class WeatherOptions
    {
        public const string JsonName = "Weather"; 

        public string? BaseUrl { get; set; }
        public string? ForecastEndpoint { get; set; }
        public string? AstronomyEndpoint { get; set; }

    }
}
