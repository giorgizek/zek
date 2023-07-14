using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Zek.Web
{
    public static class HttpClientUtility
    {
        public static readonly int RETRY_COUNT = 10;
        public static readonly int RETRY_DELAY = 500;

        private static readonly HttpClient сlient;
        private static readonly JsonSerializerSettings jsonSerializerSettings;

        /// <summary>
        /// Static constructor of the HttpClientUtility
        /// </summary>
        static HttpClientUtility()
        {
            if (сlient == null)
            {
                сlient = new HttpClient();
            }

            if (jsonSerializerSettings == null)
            {
                jsonSerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                };
            }
        }


        /// <summary>
        /// Send Http Get to the request uri and get the TResult from response content
        /// </summary>
        public static Task<TResult> GetAsync<TResult>(string requestUri, IDictionary<string, string> headers = default(Dictionary<string, string>), bool checkSuccessStatusCode = true,CancellationToken cancellationToken = default)
            => GetAsync<TResult>(new Uri(requestUri, UriKind.RelativeOrAbsolute), headers, checkSuccessStatusCode, cancellationToken);

        /// <summary>
        /// Send Http Get to the request uri and get the TResult from response content
        /// </summary>
        public static async Task<TResult> GetAsync<TResult>(Uri requestUri, IDictionary<string, string> headers = default(Dictionary<string, string>), bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            // Get response
            var response = await GetAsync(requestUri, headers, checkSuccessStatusCode, cancellationToken);

            // Read response
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            // Get result
            var result = JsonConvert.DeserializeObject<TResult>(responseContent, jsonSerializerSettings);
            return result;
        }

        /// <summary>
        /// Send Http Get to the request uri and get HttpResponseMessage
        /// </summary>
        public static async Task<HttpResponseMessage> GetAsync(Uri requestUri, IDictionary<string, string> headers = default(Dictionary<string, string>), bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            // Create new request function
            HttpRequestMessage createRequestMessage()
            {
                // Create new request
                var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

                // Add headers to request
                request.AddHeaders(headers);
                return request;
            }

            // Send request and get response
            var response = await ExecuteActionkWithAutoRetry(() => сlient.SendAsync(createRequestMessage(), cancellationToken), checkSuccessStatusCode, cancellationToken);
            return response;
        }

        /// <summary>
        /// Send Http Get to the request uri and get the byte array from response content
        /// </summary>
        /// <returns></returns>
        public static Task<byte[]> GetBytesAsync(string requestUri, IDictionary<string, string> headers = default(Dictionary<string, string>), bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            return GetBytesAsync(new Uri(requestUri), headers, checkSuccessStatusCode, cancellationToken);
        }
        /// <summary>
        /// Send Http Get to the request uri and get the byte array from response content
        /// </summary>
        public static async Task<byte[]> GetBytesAsync(Uri requestUri, IDictionary<string, string> headers = default(Dictionary<string, string>), bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            // Get response
            var response = await GetAsync(requestUri, headers, checkSuccessStatusCode, cancellationToken);

            // Read response
            var responseContent = await response.Content.ReadAsByteArrayAsync(cancellationToken);
            return responseContent;
        }

        /// <summary>
        /// Send Http Post to request uri and get TResult from response content 
        /// </summary>
        public static async Task<TResult> PostAsBytesAsync<TResult>(Uri requestUri, IDictionary<string, string> headers, byte[] content, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            // Post request and get response
            var response = await PostAsBytesAsync(requestUri, headers, content, checkSuccessStatusCode, cancellationToken);

            // Read response
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<TResult>(responseContent, jsonSerializerSettings);
        }

        /// <summary>
        /// Send Http Post to request uri and get HttpResponseMessage
        /// </summary>
        public static async Task<HttpResponseMessage> PostAsBytesAsync(Uri requestUri, IDictionary<string, string> headers, byte[] content, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            // Create new request function
            Func<HttpRequestMessage> createRequestMessage = () =>
            {
                // Create new request
                var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

                // Add headers to request
                request.AddHeaders(headers);

                // Add content as Json
                request.AddContentAsBytes(content);

                return request;
            };

            // Post request
            var response = await ExecuteActionkWithAutoRetry(() => сlient.SendAsync(createRequestMessage(), cancellationToken), checkSuccessStatusCode, cancellationToken);
            return response;
        }



        public static async Task<TResult> PostAsJsonAsync<TResult>(string requestUri, IDictionary<string, string> headers, object content, bool camelCasePropertyNames = true, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            // Post request and get response
            var response = await PostAsJsonAsync(requestUri, headers, content, camelCasePropertyNames, checkSuccessStatusCode, cancellationToken);

            // Read response
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<TResult>(responseContent, jsonSerializerSettings);
        }

       

        /// <summary>
        /// Send Http Post to request uri and get HttpResponseMessage
        /// </summary>
        public static Task<HttpResponseMessage> PostAsJsonAsync(string requestUri, IDictionary<string, string> headers, object content, bool camelCasePropertyNames, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
                => PostAsJsonAsync(new Uri(requestUri, UriKind.RelativeOrAbsolute), headers, content, camelCasePropertyNames, checkSuccessStatusCode, cancellationToken);


        /// <summary>
        /// Send Http Post to request uri and get HttpResponseMessage
        /// </summary>
        public static async Task<HttpResponseMessage> PostAsJsonAsync(Uri requestUri, IDictionary<string, string> headers, object content, bool camelCasePropertyNames, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            // Create new request function
            HttpRequestMessage createRequestMessage()
            {
                // Create new request
                var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

                // Add headers to request
                request.AddHeaders(headers);

                // Add content as Json
                request.AddContentAsJson(content, camelCasePropertyNames);

                return request;
            }

            // Post request
            var response = await ExecuteActionkWithAutoRetry(() => сlient.SendAsync(createRequestMessage(), cancellationToken), checkSuccessStatusCode, cancellationToken);
            return response;
        }


        public static Task<HttpResponseMessage> PutAsJsonAsync(string requestUri, IDictionary<string, string> headers, object content, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
            => PutAsJsonAsync(new Uri(requestUri, UriKind.RelativeOrAbsolute), headers, content, checkSuccessStatusCode, cancellationToken);

        public static async Task<HttpResponseMessage> PutAsJsonAsync(Uri requestUri, IDictionary<string, string> headers, object content, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            // Create new request function
            Func<HttpRequestMessage> createRequestMessage = () =>
            {
                // Create new request
                var request = new HttpRequestMessage(HttpMethod.Put, requestUri);

                // Add headers to request
                request.AddHeaders(headers);

                // Add content as Json
                request.AddContentAsJson(content);

                return request;
            };

            // Put request
            var response = await ExecuteActionkWithAutoRetry(() => сlient.SendAsync(createRequestMessage(), cancellationToken), checkSuccessStatusCode, cancellationToken);
            return response;
        }



        public static Task<TResult> DeleteAsync<TResult>(string requestUri, IDictionary<string, string> headers, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
            => DeleteAsync<TResult>(new Uri(requestUri, UriKind.RelativeOrAbsolute), headers, checkSuccessStatusCode, cancellationToken);
        public static async Task<TResult> DeleteAsync<TResult>(Uri requestUri, IDictionary<string, string> headers, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            var response = await DeleteAsync(requestUri, headers, checkSuccessStatusCode, cancellationToken);

            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<TResult>(responseContent, jsonSerializerSettings);
        }



        /// <summary>
        /// Send Http Delete to request uri and get HttpResponseMessage
        /// </summary>
        public static Task<HttpResponseMessage> DeleteAsync(string requestUri, IDictionary<string, string> headers, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
            => DeleteAsync(new Uri(requestUri, UriKind.RelativeOrAbsolute), headers, checkSuccessStatusCode, cancellationToken);

        /// <summary>
        /// Send Http Delete to request uri and get HttpResponseMessage
        /// </summary>
        public static async Task<HttpResponseMessage> DeleteAsync(Uri requestUri, IDictionary<string, string> headers, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            // Create new request function
            Func<HttpRequestMessage> deleteRequestMessage = () =>
            {
                // Create new request
                var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);

                // Add headers to request
                request.AddHeaders(headers);

                return request;
            };

            // Delete request
            var response = await ExecuteActionkWithAutoRetry(() => сlient.SendAsync(deleteRequestMessage(), cancellationToken), checkSuccessStatusCode, cancellationToken);
            return response;
        }


        /// <summary>
        /// Execute the action which returns HttpResponseMessage with auto retry
        /// </summary>
        private static async Task<HttpResponseMessage> ExecuteActionkWithAutoRetry(Func<Task<HttpResponseMessage>> action, bool checkSuccessStatusCode, CancellationToken cancellationToken)
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

                if (checkSuccessStatusCode && !response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync(cancellationToken));
                }

                break;
            }

            return response;
        }
    }
}