using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherApp.Client.Application.Models.Dto.Astronomy
{
    public class AstronomyInfoDto
    {
        [JsonPropertyName("astro")]
        public AstroDto Astro { get; set; }
    }

}