﻿namespace WeatherApp.Client.Application.Options
{
    public class WeatherOptions
    {
        public const string JsonName = "Weather";

        public string? Forecast { get; set; }
        public string? Astronomy { get; set; }
    }
}
