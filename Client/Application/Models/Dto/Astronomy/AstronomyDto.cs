using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherApp.Client.Application.Models.Dto.Astronomy
{
    public class AstronomyDto
    {
        [JsonPropertyName("astronomy")]
        public AstronomyInfoDto AstronomyInfo { get; set; }
    }

}