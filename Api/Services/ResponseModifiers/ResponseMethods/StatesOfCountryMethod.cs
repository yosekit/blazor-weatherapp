using System.Text.Json.Nodes;

namespace WeatherApp.Api.Services.ResponseModifiers.ResponseMethods
{
    public class StatesOfCountryMethod : ICountriesResponseMethod
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
