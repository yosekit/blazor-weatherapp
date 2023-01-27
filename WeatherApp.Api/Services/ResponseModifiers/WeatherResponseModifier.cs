using System.Text.Json.Nodes;

using WeatherApp.Api.Services.ResponseModifiers.ResponseMethods;
using WeatherApp.Api.Utilities;

namespace WeatherApp.Api.Services.ResponseModifiers
{
    public class WeatherResponseModifier
    {
        public void Modify(HttpResponseMessage message, IWeatherResponseMethod method)
        {
            string json = ResponseContentReader.Read(message.Content);

            var dom = JsonNode.Parse(json)!.AsObject();

            if (dom is null) return;

            dom = dom.ContainsKey("error") ? ModifyError(dom) : method.Modify(dom);

            message.Content = JsonContent.Create(dom);
        }

        protected JsonObject ModifyError(JsonObject dom)
        {
            var error = dom["error"].AsObject();

            error.Add("source", "Weather API");

            return dom;
        }
    }
}
