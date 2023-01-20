namespace WeatherApp.Api.Models.Enumerations
{
    public class ForecastCondition : Enumeration
    {
        public static readonly ForecastCondition Cloudly = new (100, "Cloudly", "cloudly.png");
        public static readonly ForecastCondition Clearly = new (102, "Clearly", "clearly.png");
        public static readonly ForecastCondition Rain = new (105, "Rain", "rain.png");

        public string IconName { get; }

        private ForecastCondition(int value, string description, string iconName) 
            : base(value, description)
        {
            IconName = iconName;
        }

        public static ForecastCondition FromCode(int code)
        {
            return Cloudly;
        }
    }
}
