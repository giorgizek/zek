using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zek.Web
{
    public static class HttpClientUtility
    {
        public static readonly int RETRY_COUNT = 10;
        public static readonly int RETRY_DELAY = 500;

        public static Task<TResult> PostAsJsonAsync<TResult>(string requestUri, IDictionary<string, string> headers, object content)
        {
            return PostAsJsonAsync<TResult>(requestUri, headers, content, CancellationToken.None);
        }

        public static async Task<TResult> PostAsJsonAsync<TResult>(string requestUri, IDictionary<string, string> headers, object content, CancellationToken cancellationToken)
        {
            // Post request and get response
            var response = await PostAsJsonAsync(requestUri, headers, content, cancellationToken);

            // Read response
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<TResult>(responseContent);
        }



        /// <summary>
        /// Send Http Post to request uri and get HttpResponseMessage
        /// </summary>
        public static async Task<HttpResponseMessage> PostAsJsonAsync(string requestUri, IDictionary<string, string> headers, object content, CancellationToken cancellationToken)
        {
            // Create new request function
            HttpRequestMessage createRequestMessage()
            {
                // Create new request
                var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

                // Add headers to request
                request.AddHeaders(headers);

                // Add content as Json
                request.AddContentAsJson(content);

                return request;
            }

            using var client = new HttpClient();
            // Post request
            var response = await ExecuteActionkWithAutoRetry(() => client.SendAsync(createRequestMessage()));
            return response;
        }


        /// <summary>
        /// Execute the action which returns HttpResponseMessage with auto retry
        /// </summary>
        private static async Task<HttpResponseMessage> ExecuteActionkWithAutoRetry(Func<Task<HttpResponseMessage>> action)
        {
            int retryCount = RETRY_COUNT;
            int retryDelay = RETRY_DELAY;

            HttpResponseMessage response;

            while (true)
            {
                response = await action();

                if (response.StatusCode == (HttpStatusCode)429 && retryCount > 0)
                {
                    await Task.Delay(retryDelay);
                    retryCount--;
                    retryDelay *= 2;
                    continue;
                }

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }

                break;
            }

            return response;
        }

        /// <summary>
        /// Add headers to request
        /// </summary>
        private static void AddHeaders(this HttpRequestMessage request, IDictionary<string, string> headers)
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
        /// Add content to request as json
        /// </summary>
        private static void AddContentAsJson(this HttpRequestMessage request, object content)
        {
            if (content != null)
            {
                string jsonContent = JsonConvert.SerializeObject(content, Formatting.None, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                });
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }
        }
    }
}
