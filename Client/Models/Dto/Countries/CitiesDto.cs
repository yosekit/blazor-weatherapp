using System.Text.Json.Serialization;

namespace WeatherApp.Client.Models.Dto.Countries
{
    public class CitiesDto
    {
        [JsonPropertyName("cities")]
        public string[] Cities { get; set; }
    }
}
