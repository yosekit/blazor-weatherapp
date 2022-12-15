using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherApp.Models.Dto
{
    public class AstronomyInfoDto
    {
        [JsonPropertyName("astro")]
        public AstroDto Astro { get; set; }
    }

}