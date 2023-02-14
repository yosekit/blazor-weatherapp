using System.Text.Json.Serialization;

namespace WeatherApp.Client.Application.Models.Dto.Countries
{
    public class CitiesDto
    {
        [JsonPropertyName("cities")]
        public string[] Cities { get; set; }
    }
}
