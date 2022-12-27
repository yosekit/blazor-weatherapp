using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherApp.Models.Dto.Forecast
{
    public class LocationDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lon")]
        public double Lon { get; set; }

        [JsonPropertyName("tz_id")]
        public string Timezone { get; set; }

        [JsonPropertyName("localtime")]
        public string Time { get; set; }
    }

}