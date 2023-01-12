using System.Text.Json.Serialization;

namespace WeatherApp.Models.Dto.Countries
{
    public class CitiesDto
    {
        [JsonPropertyName("data")]
        public string[] Cities { get; set; }
    }
}
