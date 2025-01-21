using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;

namespace Zek.Web
{

    public static class HttpExtensions
    {
        /// <summary>
        /// Add headers to request
        /// </summary>
        public static void AddHeaders(this HttpRequestMessage request, IDictionary<string, string> headers)
        {
            // Add headers to request
            if (headers != null)
            {
                foreach (string key in headers.Keys)
                {
                    request.Headers.Add(key, headers[key]);
                }
            }
        }

        /// <summary>
        /// Add headers to request
        /// </summary>
        public static void AddHeaders(this HttpRequestHeaders httpRequestHeaders, IDictionary<string, string> headers)
        {
            // Add headers to request
            if (headers != null)
            {
                foreach (string key in headers.Keys)
                {
                    httpRequestHeaders.Add(key, headers[key]);
                }
            }
        }

        /// <summary>
        /// Add content to request as byte array
        /// </summary>
        public static void AddContentAsBytes(this HttpRequestMessage request, byte[] content)
        {
            if (content?.Length > 0)
            {
                var byteContent = new ByteArrayContent(content);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                request.Content = byteContent;
            }
        }

        /// <summary>
        /// Add content to request as json
        /// </summary>
        public static void AddContentAsJson(this HttpRequestMessage request, object content, bool camelCasePropertyNames = true)
        {
            if (content != null)
            {
                //var jsonContent = JsonConvert.SerializeObject(content);
                var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                if (camelCasePropertyNames)
                    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                var jsonContent = JsonConvert.SerializeObject(content, Formatting.None, settings);
                request.Content = new StringContentNoCharset(jsonContent, "application/json");
            }
        }


        /// <summary>
        /// Returns site base url.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetBaseUrl(this HttpRequest request)
        {
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            return baseUrl;
        }
    }
}
