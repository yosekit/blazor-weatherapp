using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherApp.Models.Dto.Forecast
{
    public class CurrentDto
    {
        [JsonPropertyName("temp_c")]
        public double Temp { get; set; }

        [JsonPropertyName("condition:text")]
        public ConditionDto Condition { get; set; }

        [JsonPropertyName("wind_kph")]
        public double WindSpeed { get; set; }

        [JsonPropertyName("wind_dir")]
        public string WindDir { get; set; }

        [JsonPropertyName("feelslike_c")]
        public double FeelslikeTemp { get; set; }

        [JsonPropertyName("uv")]
        public double Uv { get; set; }
    }

}