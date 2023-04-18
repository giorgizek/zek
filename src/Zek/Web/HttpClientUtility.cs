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

        /// <summary>
        /// Static constructor of the HttpClientUtility
        /// </summary>
        static HttpClientUtility()
        {
            if (сlient == null)
            {
                сlient = new HttpClient();
            }
        }


        /// <summary>
        /// Send Http Get to the request uri and get the TResult from response content
        /// </summary>
        public static Task<TResult> GetAsync<TResult>(string requestUri, IDictionary<string, string> headers = default(Dictionary<string, string>), CancellationToken cancellationToken = default)
            => GetAsync<TResult>(new Uri(requestUri, UriKind.RelativeOrAbsolute), headers, cancellationToken);

        /// <summary>
        /// Send Http Get to the request uri and get the TResult from response content
        /// </summary>
        public static async Task<TResult> GetAsync<TResult>(Uri requestUri, IDictionary<string, string> headers = default(Dictionary<string, string>), CancellationToken cancellationToken = default)
        {
            // Get response
            var response = await GetAsync(requestUri, headers, cancellationToken);

            // Read response
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            // Get result
            var result = JsonConvert.DeserializeObject<TResult>(responseContent);
            return result;
        }

        /// <summary>
        /// Send Http Get to the request uri and get HttpResponseMessage
        /// </summary>
        public static async Task<HttpResponseMessage> GetAsync(Uri requestUri, IDictionary<string, string> headers = default(Dictionary<string, string>), CancellationToken cancellationToken = default)
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
            var response = await ExecuteActionkWithAutoRetry(() => сlient.SendAsync(createRequestMessage(), cancellationToken), cancellationToken);
            return response;
        }


        /// <summary>
        /// Send Http Get to the request uri and get the byte array from response content
        /// </summary>
        public static async Task<byte[]> GetBytesAsync(Uri requestUri, IDictionary<string, string> headers = default(Dictionary<string, string>), CancellationToken cancellationToken = default)
        {
            // Get response
            var response = await GetAsync(requestUri, headers, cancellationToken);

            // Read response
            var responseContent = await response.Content.ReadAsByteArrayAsync(cancellationToken);
            return responseContent;
        }

        /// <summary>
        /// Send Http Post to request uri and get TResult from response content 
        /// </summary>
        public static async Task<TResult> PostAsBytesAsync<TResult>(Uri requestUri, IDictionary<string, string> headers, byte[] content, CancellationToken cancellationToken = default)
        {
            // Post request and get response
            var response = await PostAsBytesAsync(requestUri, headers, content, cancellationToken);

            // Read response
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<TResult>(responseContent);
        }

        /// <summary>
        /// Send Http Post to request uri and get HttpResponseMessage
        /// </summary>
        public static async Task<HttpResponseMessage> PostAsBytesAsync(Uri requestUri, IDictionary<string, string> headers, byte[] content, CancellationToken cancellationToken = default)
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
            var response = await ExecuteActionkWithAutoRetry(() => сlient.SendAsync(createRequestMessage(), cancellationToken), cancellationToken);
            return response;
        }


        public static Task<TResult> PostAsJsonAsync<TResult>(string requestUri, IDictionary<string, string> headers, object content, bool camelCasePropertyNames = true)
        {
            return PostAsJsonAsync<TResult>(requestUri, headers, content, camelCasePropertyNames, CancellationToken.None);
        }

        public static async Task<TResult> PostAsJsonAsync<TResult>(string requestUri, IDictionary<string, string> headers, object content, bool camelCasePropertyNames, CancellationToken cancellationToken = default)
        {
            // Post request and get response
            var response = await PostAsJsonAsync(requestUri, headers, content, camelCasePropertyNames, cancellationToken);

            // Read response
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<TResult>(responseContent);
        }

        /// <summary>
        /// Send Http Post to request uri and get HttpResponseMessage
        /// </summary>
        public static Task<HttpResponseMessage> PostAsJsonAsync(string requestUri, IDictionary<string, string> headers, object content, bool camelCasePropertyNames, CancellationToken cancellationToken = default)
            => PostAsJsonAsync(new Uri(requestUri, UriKind.RelativeOrAbsolute), headers, content, camelCasePropertyNames, cancellationToken);


        /// <summary>
        /// Send Http Post to request uri and get HttpResponseMessage
        /// </summary>
        public static async Task<HttpResponseMessage> PostAsJsonAsync(Uri requestUri, IDictionary<string, string> headers, object content, bool camelCasePropertyNames, CancellationToken cancellationToken = default)
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
            var response = await ExecuteActionkWithAutoRetry(() => сlient.SendAsync(createRequestMessage(), cancellationToken));
            return response;
        }


        public static Task<HttpResponseMessage> PutAsJsonAsync(string requestUri, IDictionary<string, string> headers, object content, CancellationToken cancellationToken = default)
            => PutAsJsonAsync(new Uri(requestUri, UriKind.RelativeOrAbsolute), headers, content, cancellationToken);

        public static async Task<HttpResponseMessage> PutAsJsonAsync(Uri requestUri, IDictionary<string, string> headers, object content, CancellationToken cancellationToken)
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
            var response = await ExecuteActionkWithAutoRetry(() => сlient.SendAsync(createRequestMessage(), cancellationToken), cancellationToken);
            return response;
        }



        public static Task<TResult> DeleteAsync<TResult>(string requestUri, IDictionary<string, string> headers, CancellationToken cancellationToken = default)
            => DeleteAsync<TResult>(new Uri(requestUri, UriKind.RelativeOrAbsolute), headers, cancellationToken);
        public static async Task<TResult> DeleteAsync<TResult>(Uri requestUri, IDictionary<string, string> headers, CancellationToken cancellationToken = default)
        {
            var response = await DeleteAsync(requestUri, headers);

            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<TResult>(responseContent);
        }



        /// <summary>
        /// Send Http Delete to request uri and get HttpResponseMessage
        /// </summary>
        public static Task<HttpResponseMessage> DeleteAsync(string requestUri, IDictionary<string, string> headers, CancellationToken cancellationToken = default)
            => DeleteAsync(new Uri(requestUri, UriKind.RelativeOrAbsolute), headers, cancellationToken);

        /// <summary>
        /// Send Http Delete to request uri and get HttpResponseMessage
        /// </summary>
        public static async Task<HttpResponseMessage> DeleteAsync(Uri requestUri, IDictionary<string, string> headers, CancellationToken cancellationToken = default)
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
            var response = await ExecuteActionkWithAutoRetry(() => сlient.SendAsync(deleteRequestMessage(), cancellationToken), cancellationToken);
            return response;
        }


        /// <summary>
        /// Execute the action which returns HttpResponseMessage with auto retry
        /// </summary>
        private static async Task<HttpResponseMessage> ExecuteActionkWithAutoRetry(Func<Task<HttpResponseMessage>> action, CancellationToken cancellationToken = default)
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
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync(cancellationToken));
                }

                break;
            }

            return response;
        }
    }
}
