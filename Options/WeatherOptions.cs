namespace WeatherApp.Options
{
    public class WeatherOptions
    {
        public const string Key = "Weather"; 

        public string BaseUrl { get; set; } = String.Empty;
        public string ForecastEndpoint { get; set; } = String.Empty;
        public string AstronomyEndpoint { get; set; } = String.Empty;
    }
}
