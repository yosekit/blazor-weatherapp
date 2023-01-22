using System.Web;

namespace WeatherApp.Shared.Utilities
{
    public class QueryStringHelper
    {
        public static IDictionary<string, string> Parse(string query)
        {
            var dict = new Dictionary<string, string>();

            var collection = HttpUtility.ParseQueryString(query);

            if(collection is not null)
            {
                foreach(string key in collection.AllKeys)
                    dict.Add(key, collection[key]);
            }

            return dict;
        }
    }
}
