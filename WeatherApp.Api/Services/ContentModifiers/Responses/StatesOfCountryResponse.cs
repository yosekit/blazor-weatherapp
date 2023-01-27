using System.Text.Json.Nodes;

namespace WeatherApp.Api.Services.ContentModifiers.Responses
{
    public class StatesOfCountryResponse : ICountriesResponse
    {
        public JsonObject Modify(JsonObject dom)
        {
            var data = dom["data"];
            dom.Remove("data");
            dom.Add("country", data);

            dom.Remove("error");
            dom.Remove("msg");

            return dom;
        }
    }
}
