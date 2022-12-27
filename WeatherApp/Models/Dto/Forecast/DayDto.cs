using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherApp.Models.Dto.Forecast
{
    public class DayDto
    {
        [JsonPropertyName("maxtemp_c")]
        public double MaxTemp { get; set; }

        [JsonPropertyName("mintemp_c")]
        public double MinTemp { get; set; }

        [JsonPropertyName("avgtemp_c")]
        public double AvgTemp { get; set; }

        [JsonPropertyName("totalsnow_cm")]
        public double TotalSnow { get; set; }

        [JsonPropertyName("condition")]
        public ConditionDto Condition { get; set; }
    }

}