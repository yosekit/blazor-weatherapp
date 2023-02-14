using System.Text.Json.Serialization;

namespace WeatherApp.Client.Application.Models.Dto.Countries
{
    public class StateDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("state_code")]
        public string Code { get; set; }
    }
}
