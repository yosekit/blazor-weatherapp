using System.Text.Json;
using System.Text.Json.Nodes;

namespace WeatherApp.Api.Services.ContentModifiers.Responses
{
    public class ForecastResponse : IWeatherResponse
    {
        private readonly ForecastConditionDtoCreator _conditionCreator;

        public ForecastResponse(ForecastConditionDtoCreator conditionCreator)
        {
            _conditionCreator = conditionCreator;
        }

        public JsonObject Modify(JsonObject dom)
        {
            const string c = "condition";

            dom["current"][c] = ModifyCondition(dom["current"][c]);

            foreach (var fday in dom["forecast"]["forecastday"].AsArray())
            {
                fday["day"][c] = ModifyCondition(fday["day"][c]);

                foreach (var hour in fday["hour"].AsArray())
                    hour[c] = ModifyCondition(hour[c]);
            }

            return dom;
        }

        private JsonNode? ModifyCondition(JsonNode? node)
        {
            int code = int.Parse(node["code"].ToString());

            var dto = _conditionCreator.FromCode(code);

            string json = JsonSerializer.Serialize(dto);

            return JsonNode.Parse(json);
        }
    }
}
