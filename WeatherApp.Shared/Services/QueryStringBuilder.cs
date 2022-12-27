using Microsoft.AspNetCore.WebUtilities;

namespace WeatherApp.Shared.Services
{
    public class QueryStringBuilder
    {
        private IDictionary<string, string> _params;

        public QueryStringBuilder()
        {
            _params = new Dictionary<string, string>();
        }

        public void Add(string key, string value)
        {
            _params.TryAdd(key, value);
        }

        public void Set(string key, string value)
        {
            if (_params.ContainsKey(key))
            {
                _params[key] = value;
            }
        }

        public void Remove(string key)
        {
            _params.Remove(key);
        }

        public string Build()
        {
            return QueryHelpers.AddQueryString("", _params);
        }
    }
}
