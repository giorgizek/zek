using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zek.Utils
{
    public static class JsonHelper
    {
        public static string SerializeObject(object value) => JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        public static StringContent CreateJsonContent(object value)
        {
            return new(SerializeObject(value), Encoding.UTF8, "application/json");
        }

        private static T Deserialize<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default;

            using var sr = new StreamReader(stream);
            using var jtr = new JsonTextReader(sr);
            var js = new JsonSerializer();
            var searchResult = js.Deserialize<T>(jtr);
            return searchResult;
        }

        private static async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
            {
                using var sr = new StreamReader(stream);
                content = await sr.ReadToEndAsync();
            }

            return content;
        }


        public static async Task<T> PostAndDeserializeAsync<T>(string requestUri, object value, string token = null) => await PostAndDeserializeAsync<T>(requestUri, value, token, CancellationToken.None);

        public static async Task<T> PostAndDeserializeAsync<T>(string requestUri, object value, string token, CancellationToken cancellationToken) => await PostAndDeserializeAsync<T>(new Uri(requestUri), value, token, cancellationToken);

        public static async Task<T> PostAndDeserializeAsync<T>(Uri requestUri, object value, string token = null) => await PostAndDeserializeAsync<T>(requestUri, value, token, CancellationToken.None);

        public static async Task<T> PostAndDeserializeAsync<T>(Uri requestUri, object value, string token, CancellationToken cancellationToken)
        {
            using var client = new HttpClient();
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Add(nameof(HttpRequestHeaders.Authorization), token);

            using var request = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = CreateJsonContent(value) };
            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

            if (response.IsSuccessStatusCode)
                return Deserialize<T>(stream);

            var content = await StreamToStringAsync(stream);
            throw new ApiException
            {
                StatusCode = response.StatusCode,
                Content = content
            };
        }
    }

    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Content { get; set; }
    }
}
