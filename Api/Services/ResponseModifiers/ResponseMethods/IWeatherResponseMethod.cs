using System.Text.Json.Nodes;

namespace WeatherApp.Api.Services.ResponseModifiers.ResponseMethods
{
    public interface IWeatherResponseMethod
    {
        JsonObject Modify(JsonObject dom);
    }
}
