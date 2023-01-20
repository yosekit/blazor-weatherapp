using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Nodes;
using System.IO.Compression;

using AutoMapper;

using WeatherApp.Api.Models.Dtos;
using WeatherApp.Api.Models.Enumerations;

namespace WeatherApp.Api.Utilities
{
    public class WeatherResponseProducer
    {
        private readonly IMapper _mapper;

        private HttpResponseMessage? _message;

        public WeatherResponseProducer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public WeatherResponseProducer FromMessage(HttpResponseMessage message)
        {
            _message = message;

            return this;
        }

        public WeatherResponseProducer EditConditions()
        {
            const string c = "condition";

            var dom = ReadContent(_message.Content);

            dom["current"][c] = EditCondition(dom["current"][c]);

            foreach (var fday in dom["forecast"]["forecastday"].AsArray())
            {
                fday["day"][c] = EditCondition(fday["day"][c]);

                foreach (var hour in fday["hour"].AsArray())
                    hour[c] = EditCondition(hour[c]);
            }

            _message.Content = JsonContent.Create(dom);

            return this;
        }

        public HttpResponseMessage Produce()
        {
            var result = _message;

            _message = null;

            return result;
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

        private JsonNode? EditCondition(JsonNode? node)
        {
            int code = int.Parse(node["code"].ToString());

            var condition = ForecastCondition.FromCode(code);

            var dto = _mapper.Map<ForecastConditionDto>(condition);

            string json = JsonSerializer.Serialize(dto);

            return JsonNode.Parse(json);
        }
    }
}
