using System.Text.Json.Nodes;
using System.IO.Compression;

namespace WeatherApp.Api.Utilities
{
    public class BrotliContentReader
    {
        public static string Read(HttpContent content)
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

        public static string Read(Stream body)
        {
            using (var bs = new BrotliStream(body, CompressionMode.Decompress))
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
