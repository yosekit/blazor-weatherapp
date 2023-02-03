using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherApp.Client.Models.Dto.Forecast
{
    public class ConditionDto
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

}