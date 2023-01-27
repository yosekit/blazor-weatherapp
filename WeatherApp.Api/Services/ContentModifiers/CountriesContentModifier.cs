using System.Text.Json.Nodes;

using WeatherApp.Api.Services.ContentModifiers.Responses;

namespace WeatherApp.Api.Services.ContentModifiers
{
    public class CountriesContentModifier
    {
        public ICountriesResponse Response { private get; set; }

        public JsonContent? Modify(string json)
        {
            return Modify(JsonNode.Parse(json)!.AsObject());
        }

        public JsonContent Modify(JsonObject? dom)
        {
            if (dom is null) return null;

            dom = dom["error"].GetValue<bool>() ? ModifyError(dom) : Response.Modify(dom);

            return JsonContent.Create(dom);
        }

        protected JsonObject ModifyError(JsonObject dom)
        {
            string message = dom["msg"].GetValue<string>();

            message = char.ToUpper(message[0]) + message[1..] + '.';

            dom["error"] = new JsonObject(
                new Dictionary<string, JsonNode?> { ["message"] = message }
            );
            
            dom.Remove("msg");

            return dom;
        }
    }
}

