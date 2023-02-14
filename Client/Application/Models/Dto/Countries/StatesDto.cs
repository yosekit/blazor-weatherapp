﻿using System.Text.Json.Serialization;

namespace WeatherApp.Client.Application.Models.Dto.Countries
{
    public class StatesDto
    {
        [JsonPropertyName("country")]
        public CountryDto Country { get; set; }
    }
}
