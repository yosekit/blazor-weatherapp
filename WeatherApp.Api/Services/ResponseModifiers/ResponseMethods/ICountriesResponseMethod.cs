using System.Text.Json.Nodes;

namespace WeatherApp.Api.Services.ResponseModifiers.ResponseMethods
{
    public interface ICountriesResponseMethod
    {
        JsonObject Modify(JsonObject dom);
    }
}
