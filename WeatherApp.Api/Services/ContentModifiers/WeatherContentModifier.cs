using System.Text.Json.Nodes;

using WeatherApp.Api.Services.ContentModifiers.Responses;

namespace WeatherApp.Api.Services.ContentModifiers
{
    public class WeatherContentModifier
    {
        public IWeatherResponse Response { private get;  set; }

        public JsonContent? Modify(string json)
        {
            return Modify(JsonNode.Parse(json)!.AsObject());
        }

        public JsonContent? Modify(JsonObject? dom)
        {
            if(dom is null) return null;

            dom = dom.ContainsKey("error") ? ModifyError(dom) : Response.Modify(dom);

            return JsonContent.Create(dom);
        }

        protected JsonObject ModifyError(JsonObject dom)
        {
            var error = dom["error"].AsObject();

            error.Remove("code");

            return dom;
        }
    }
}
