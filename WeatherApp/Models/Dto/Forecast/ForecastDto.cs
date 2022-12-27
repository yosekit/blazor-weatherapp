using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherApp.Models.Dto.Forecast
{
    public class ForecastDto
    {
        [JsonPropertyName("location")]
        public LocationDto Location { get; set; }

        [JsonPropertyName("current")]
        public CurrentDto Current { get; set; }

        [JsonPropertyName("forecast")]
        public ForecastInfoDto ForecastInfo { get; set; }
    }

}