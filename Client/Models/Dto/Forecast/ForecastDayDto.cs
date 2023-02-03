using System.Text.Json;
using System.Text.Json.Serialization;

using WeatherApp.Client.Models.Dto.Astronomy;

namespace WeatherApp.Client.Models.Dto.Forecast
{
    public class ForecastDayDto
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("day")]
        public DayDto Day { get; set; }

        [JsonPropertyName("astro")]
        public AstroDto? Astro { get; set; }

        [JsonPropertyName("hour")]
        public HourDto[] Hours { get; set; }
    }

}