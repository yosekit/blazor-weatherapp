using System.Text.Json.Serialization;

namespace WeatherApp.Api.Models.Dtos
{
    public class ForecastConditionDto
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }
    }
}
