using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherApp.Client.Models.Dto
{
    public class AstronomyInfoDto
    {
        [JsonPropertyName("astro")]
        public AstroDto Astro { get; set; }
    }

}