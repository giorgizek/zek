using Newtonsoft.Json;
using System.Net;

namespace Zek.Web
{
    public static class HttpClientUtility
    {
        public static readonly int RETRY_COUNT = 10;
        public static readonly int RETRY_DELAY = 500;

        private static readonly InsecureHttpClientHandler? _handler;
        private static readonly HttpClient _сlient;
        private static readonly JsonSerializerSettings? _jsonSerializerSettings;

        /// <summary>
        /// Static constructor of the HttpClientUtility
        /// </summary>
        static HttpClientUtility()
        {
            if (_сlient == null)
            {
                _handler = new InsecureHttpClientHandler();
                _сlient = new HttpClient(_handler);
            }

            _jsonSerializerSettings ??= new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
            };
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
                    await Task.Delay(retryDelay).ConfigureAwait(false);
                    retryCount--;
                    retryDelay *= 2;
                    continue;
                }

                if (checkSuccessStatusCode && !response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));
                }

                break;
            }

            return response;
        }

        public static HttpRequestMessage CreateMessage(HttpMethod httpMethod, Uri requestUri, IDictionary<string, string?>? headers = null, object? content = null, bool camelCasePropertyNames = true)
        {
            // Create new request
            var message = new HttpRequestMessage(httpMethod, requestUri);

            // Add headers to request
            message.AddHeaders(headers);

            if (content is not null)
            {
                if (content is byte[])
                    message.AddContentAsBytes((byte[])content);
                else
                    message.AddContentAsJson(content, camelCasePropertyNames);
            }


            return message;
        }


        /// <summary>
        /// Send Http Get to the request uri and get HttpResponseMessage
        /// </summary>
        public static async Task<HttpResponseMessage> GetAsync(Uri requestUri, IDictionary<string, string?>? headers = null, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            var message = CreateMessage(HttpMethod.Get, requestUri, headers);
            var response = await ExecuteActionkWithAutoRetry(() => _сlient.SendAsync(message, cancellationToken), checkSuccessStatusCode, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Send Http Post to request uri and get HttpResponseMessage
        /// </summary>
        public static async Task<HttpResponseMessage> PostAsync(Uri requestUri, IDictionary<string, string?>? headers, byte[]? content, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            var message = CreateMessage(HttpMethod.Post, requestUri, headers, content);
            var response = await ExecuteActionkWithAutoRetry(() => _сlient.SendAsync(message, cancellationToken), checkSuccessStatusCode, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Send Http Post to request uri and get HttpResponseMessage
        /// </summary>
        public static Task<HttpResponseMessage> PostAsync(string requestUri, IDictionary<string, string?>? headers, object? content, bool camelCasePropertyNames, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
                => PostAsync(new Uri(requestUri, UriKind.RelativeOrAbsolute), headers, content, camelCasePropertyNames, checkSuccessStatusCode, cancellationToken);

        /// <summary>
        /// Send Http Post to request uri and get HttpResponseMessage
        /// </summary>
        public static async Task<HttpResponseMessage> PostAsync(Uri requestUri, IDictionary<string, string?>? headers, object? content, bool camelCasePropertyNames, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            var message = CreateMessage(HttpMethod.Post, requestUri, headers, content, camelCasePropertyNames);
            var response = await ExecuteActionkWithAutoRetry(() => _сlient.SendAsync(message, cancellationToken), checkSuccessStatusCode, cancellationToken).ConfigureAwait(false);
            return response;
        }


        /// <summary>
        /// Send Http Put to request uri and get HttpResponseMessage
        /// </summary>
        public static Task<HttpResponseMessage> PutAsync(string requestUri, IDictionary<string, string?>? headers, object? content, bool camelCasePropertyNames, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
                => PutAsync(new Uri(requestUri, UriKind.RelativeOrAbsolute), headers, content, camelCasePropertyNames, checkSuccessStatusCode, cancellationToken);

        /// <summary>
        /// Send Http Put to request uri and get HttpResponseMessage
        /// </summary>
        public static async Task<HttpResponseMessage> PutAsync(Uri requestUri, IDictionary<string, string?>? headers, object? content, bool camelCasePropertyNames, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            var message = CreateMessage(HttpMethod.Put, requestUri, headers, content, camelCasePropertyNames);
            var response = await ExecuteActionkWithAutoRetry(() => _сlient.SendAsync(message, cancellationToken), checkSuccessStatusCode, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Send Http Get to the request uri and get the TResult from response content
        /// </summary>
        public static Task<TResult?> GetAsync<TResult>(string requestUri, IDictionary<string, string?>? headers = null, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
            => GetAsync<TResult>(new Uri(requestUri, UriKind.RelativeOrAbsolute), headers, checkSuccessStatusCode, cancellationToken);

        /// <summary>
        /// Send Http Get to the request uri and get the TResult from response content
        /// </summary>
        public static async Task<TResult?> GetAsync<TResult>(Uri requestUri, IDictionary<string, string?>? headers = null, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            // Get response
            var response = await GetAsync(requestUri, headers, checkSuccessStatusCode, cancellationToken).ConfigureAwait(false);

            // Read response
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            // Get result
            var result = JsonConvert.DeserializeObject<TResult>(responseContent, _jsonSerializerSettings);
            return result;
        }

        /// <summary>
        /// Send Http Get to the request uri and get the byte array from response content
        /// </summary>
        /// <returns></returns>
        public static Task<byte[]> GetBytesAsync(string requestUri, IDictionary<string, string?>? headers = null, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            return GetBytesAsync(new Uri(requestUri), headers, checkSuccessStatusCode, cancellationToken);
        }
        /// <summary>
        /// Send Http Get to the request uri and get the byte array from response content
        /// </summary>
        public static async Task<byte[]> GetBytesAsync(Uri requestUri, IDictionary<string, string?>? headers = null, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            // Get response
            var response = await GetAsync(requestUri, headers, checkSuccessStatusCode, cancellationToken).ConfigureAwait(false);

            // Read response
            var responseContent = await response.Content.ReadAsByteArrayAsync(cancellationToken);
            return responseContent;
        }

        /// <summary>
        /// Send Http Post to request uri and get TResult from response content 
        /// </summary>
        public static async Task<TResult?> PostAsBytesAsync<TResult>(Uri requestUri, IDictionary<string, string?>? headers, byte[]? content, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            var response = await PostAsync(requestUri, headers, content, checkSuccessStatusCode, cancellationToken).ConfigureAwait(false);
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TResult>(responseContent, _jsonSerializerSettings);
        }

        public static async Task<TResult?> PostAsJsonAsync<TResult>(string requestUri, IDictionary<string, string?>? headers, object? content, bool camelCasePropertyNames = true, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            var response = await PostAsync(requestUri, headers, content, camelCasePropertyNames, checkSuccessStatusCode, cancellationToken).ConfigureAwait(false);
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TResult>(responseContent, _jsonSerializerSettings);
        }
      
        
        public static async Task<string?> PostAsStringAsync(string requestUri, IDictionary<string, string?>? headers, object? content, bool camelCasePropertyNames = true, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            var response = await PostAsync(requestUri, headers, content, camelCasePropertyNames, checkSuccessStatusCode, cancellationToken).ConfigureAwait(false);
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            return responseContent;
        }


        public static async Task<TResult?> PutAsJsonAsync<TResult>(string requestUri, IDictionary<string, string?>? headers, object? content, bool camelCasePropertyNames = true, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            var response = await PutAsync(requestUri, headers, content, camelCasePropertyNames, checkSuccessStatusCode, cancellationToken).ConfigureAwait(false);
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TResult>(responseContent, _jsonSerializerSettings);
        }


        public static Task<TResult?> DeleteAsync<TResult>(string requestUri, IDictionary<string, string?>? headers, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
            => DeleteAsync<TResult>(new Uri(requestUri, UriKind.RelativeOrAbsolute), headers, checkSuccessStatusCode, cancellationToken);
        public static async Task<TResult?> DeleteAsync<TResult>(Uri requestUri, IDictionary<string, string?>? headers, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            var response = await DeleteAsync(requestUri, headers, checkSuccessStatusCode, cancellationToken).ConfigureAwait(false);
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TResult>(responseContent, _jsonSerializerSettings);
        }
        /// <summary>
        /// Send Http Delete to request uri and get HttpResponseMessage
        /// </summary>
        public static Task<HttpResponseMessage> DeleteAsync(string requestUri, IDictionary<string, string?>? headers, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
            => DeleteAsync(new Uri(requestUri, UriKind.RelativeOrAbsolute), headers, checkSuccessStatusCode, cancellationToken);

        /// <summary>
        /// Send Http Delete to request uri and get HttpResponseMessage
        /// </summary>
        public static async Task<HttpResponseMessage> DeleteAsync(Uri requestUri, IDictionary<string, string?>? headers, bool checkSuccessStatusCode = true, CancellationToken cancellationToken = default)
        {
            var message = CreateMessage(HttpMethod.Delete, requestUri, headers);
            var response = await ExecuteActionkWithAutoRetry(() => _сlient.SendAsync(message, cancellationToken), checkSuccessStatusCode, cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}