using System.Text.Json.Nodes;

namespace WeatherApp.Api.Services.ContentModifiers.Responses
{
    public class CitiesOfCountryResponse : ICountriesResponse
    {
        public JsonObject Modify(JsonObject dom)
        {
            var data = dom["data"];
            dom.Remove("data");
            dom.Add("cities", data);

            dom.Remove("error");
            dom.Remove("msg");

            return dom;
        }
    }
}
