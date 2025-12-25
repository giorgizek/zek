using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Zek.Utils
{
    public static class JsonHelper
    {
        public static string SerializeObject(object value, bool camelCasePropertyNames = false)
        {
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            if (camelCasePropertyNames)
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return JsonConvert.SerializeObject(value, Formatting.None, settings);
        }
    }
}
