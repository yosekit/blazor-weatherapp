using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherApp.Models.Dto
{
    public class AstronomyDto
    {
        [JsonPropertyName("astronomy")]
        public AstronomyInfoDto AstronomyInfo { get; set; }
    }

}