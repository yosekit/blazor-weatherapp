using System.Text.Json.Nodes;

namespace WeatherApp.Api.Services.ContentModifiers.Responses
{
    public interface IWeatherResponse
    {
        JsonObject Modify(JsonObject dom);
    }
}
