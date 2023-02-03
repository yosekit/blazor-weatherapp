using System.Text.Json.Nodes;
using System.IO.Compression;

namespace WeatherApp.Api.Utilities
{
    public class ResponseContentReader
    {
        public static string Read(HttpContent content)
        {
            if(content.Headers.ContentEncoding.Contains("br"))
                return ReadBrotli(content);

            return content.ReadAsStringAsync().Result;
        }

        private static string ReadBrotli(HttpContent content)
        {
            using (var bs = new BrotliStream(content.ReadAsStream(), CompressionMode.Decompress))
            using (var ms = new MemoryStream())
            {
                bs.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(ms))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
