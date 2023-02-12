using System.Text.Json.Nodes;

using WeatherApp.Api.Services.ResponseModifiers.ResponseMethods;
using WeatherApp.Api.Utilities;

namespace WeatherApp.Api.Services.ResponseModifiers
{
    public class CountriesResponseModifier
    {
        public void Modify(HttpResponseMessage message, ICountriesResponseMethod method)
        {
            string json = ResponseContentReader.Read(message.Content);

            var dom = JsonNode.Parse(json)!.AsObject();

            if (dom is null) return;

            dom = dom["error"].GetValue<bool>() ? ModifyError(dom) : method.Modify(dom);

            message.Content = JsonContent.Create(dom);
        }

        protected JsonObject ModifyError(JsonObject dom)
        {
            string message = dom["msg"].GetValue<string>();

            message = char.ToUpper(message[0]) + message[1..] + '.';

            dom["error"] = new JsonObject( new Dictionary<string, JsonNode?> 
            { 
                ["message"] = message,
                ["source"] = "Countries & Cities API"
            });
            
            dom.Remove("msg");

            return dom;
        }
    }
}

