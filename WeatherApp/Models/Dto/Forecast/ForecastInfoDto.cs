using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherApp.Models.Dto.Forecast
{
    public class ForecastInfoDto
    {
        [JsonPropertyName("forecastday")]
        public ForecastDayDto[] ForecastDays { get; set; }
    }

}