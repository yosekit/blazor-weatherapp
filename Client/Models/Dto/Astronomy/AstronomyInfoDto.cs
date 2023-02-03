using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherApp.Client.Models.Dto.Astronomy
{
    public class AstronomyInfoDto
    {
        [JsonPropertyName("astro")]
        public AstroDto Astro { get; set; }
    }

}