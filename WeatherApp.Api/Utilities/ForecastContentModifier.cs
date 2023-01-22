using System.Text.Json;
using System.Text.Json.Nodes;
using System.IO.Compression;

namespace WeatherApp.Api.Utilities
{
    public class ForecastContentModifier
    {
        private readonly ForecastConditionDtoCreator _conditionCreator;

        public ForecastContentModifier(ForecastConditionDtoCreator creator)
        {
            _conditionCreator = creator;
        }

        public JsonContent ModifyConditions(HttpContent content)
        {
            const string c = "condition";

            var dom = ReadContent(content);

            dom["current"][c] = ModifyCondition(dom["current"][c]);

            foreach (var fday in dom["forecast"]["forecastday"].AsArray())
            {
                fday["day"][c] = ModifyCondition(fday["day"][c]);

                foreach (var hour in fday["hour"].AsArray())
                    hour[c] = ModifyCondition(hour[c]);
            }

            return JsonContent.Create(dom);
        }

        private JsonNode? ReadContent(HttpContent content)
        {
            using (var bs = new BrotliStream(content.ReadAsStream(), CompressionMode.Decompress))
            using (var ms = new MemoryStream())
            {
                bs.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(ms))
                {
                    string jsonContent = sr.ReadToEnd();

                    return JsonNode.Parse(jsonContent);
                }
            }
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
