using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherApp.Models.Dto
{
    public class ConditionDto
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

}