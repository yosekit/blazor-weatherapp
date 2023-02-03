using System.Text.Json.Serialization;

namespace WeatherApp.Client.Models.Dto.Countries
{
    public class CitiesDto
    {
        [JsonPropertyName("data")]
        public string[] Cities { get; set; }
    }
}
