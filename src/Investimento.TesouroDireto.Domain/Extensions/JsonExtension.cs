using Newtonsoft.Json.Linq;

namespace Investimento.TesouroDireto.Domain.Extensions
{
    public static class JsonExtension
    {
        public static JObject ToJObject(this string str)
        {
            try
            {
                return JObject.Parse(str);
            }
            catch
            {
                return null;
            }
        }

        public static string JsonGetByName(this string json, string item)
        {
            try
            {
                var obj = ToJObject(json);
                return obj?[item]?.ToString();
            }
            catch
            {
                return null;
            }
        }
    }
}
