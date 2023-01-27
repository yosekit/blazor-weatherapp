using System.Text.Json.Nodes;

namespace WeatherApp.Api.Services.ContentModifiers.Responses
{
    public interface ICountriesResponse
    {
        JsonObject Modify(JsonObject dom);
    }
}
