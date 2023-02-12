using System.Text.Json.Serialization;

namespace WeatherApp.Client.Models.Dto.Countries
{
	public class CountryDto
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("iso3")]
		public string Iso3 { get; set; }

		[JsonPropertyName("iso2")]
		public string Iso2 { get; set; }

		[JsonPropertyName("states")]
		public StateDto[] States { get; set; }
	}
}
