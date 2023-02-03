using Ardalis.SmartEnum;

namespace WeatherApp.Api.Models.Enumerations
{
    public class ForecastCondition : SmartEnum<ForecastCondition>
    {
        public string IconName { get; }

        // example conditions
        public static readonly ForecastCondition Cloudly = new ("Cloudly", 100, "cloudly.png");
        public static readonly ForecastCondition Clearly = new ("Clearly", 102, "clearly.png");
        public static readonly ForecastCondition Rain = new ("Rain", 104, "rain.png");

        private ForecastCondition(string name, int value, string iconName) 
            : base(name, value)
        {
            IconName = iconName;
        }
    }
}
