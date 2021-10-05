﻿using Newtonsoft.Json;
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

        private static HttpClient Client;

        /// <summary>
        /// Static constructor of the HttpClientUtility
        /// </summary>
        static HttpClientUtility()
        {
            if (Client == null)
            {
                Client = new HttpClient();
            }
        }


        /// <summary>
        /// Send Http Get to the request uri and get the TResult from response content
        /// </summary>
        public static async Task<TResult> GetAsync<TResult>(Uri requestUri, IDictionary<string, string> headers)
        {
            // Get response
            var response = await GetAsync(requestUri, headers);

            // Read response
            var responseContent = await response.Content.ReadAsStringAsync();

            // Get result
            var result = JsonConvert.DeserializeObject<TResult>(responseContent);
            return result;
        }

        /// <summary>
        /// Send Http Get to the request uri and get HttpResponseMessage
        /// </summary>
        public static async Task<HttpResponseMessage> GetAsync(Uri requestUri, IDictionary<string, string> headers)
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
            var response = await ExecuteActionkWithAutoRetry(() => Client.SendAsync(createRequestMessage()));
            return response;
        }


        /// <summary>
        /// Send Http Get to the request uri and get the byte array from response content
        /// </summary>
        public static async Task<byte[]> GetBytesAsync(Uri requestUri, IDictionary<string, string> headers = default(Dictionary<string, string>))
        {
            // Get response
            var response = await GetAsync(requestUri, headers);

            // Read response
            var responseContent = await response.Content.ReadAsByteArrayAsync();
            return responseContent;
        }

        /// <summary>
        /// Send Http Post to request uri and get TResult from response content 
        /// </summary>
        public static async Task<TResult> PostAsBytesAsync<TResult>(Uri requestUri, IDictionary<string, string> headers, byte[] content)
        {
            // Post request and get response
            var response = await PostAsBytesAsync(requestUri, headers, content);

            // Read response
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(responseContent);
        }

        /// <summary>
        /// Send Http Post to request uri and get HttpResponseMessage
        /// </summary>
        public static async Task<HttpResponseMessage> PostAsBytesAsync(Uri requestUri, IDictionary<string, string> headers, byte[] content)
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
            HttpResponseMessage response = await ExecuteActionkWithAutoRetry(() => Client.SendAsync(createRequestMessage()));
            return response;
        }


        public static Task<TResult> PostAsJsonAsync<TResult>(string requestUri, IDictionary<string, string> headers, object content, bool camelCasePropertyNames = true)
        {
            return PostAsJsonAsync<TResult>(requestUri, headers, content, camelCasePropertyNames, CancellationToken.None);
        }

        public static async Task<TResult> PostAsJsonAsync<TResult>(string requestUri, IDictionary<string, string> headers, object content, bool camelCasePropertyNames = true, CancellationToken cancellationToken)
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
        public static async Task<HttpResponseMessage> PostAsJsonAsync(string requestUri, IDictionary<string, string> headers, object content, bool camelCasePropertyNames, CancellationToken cancellationToken)
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
            var response = await ExecuteActionkWithAutoRetry(() => Client.SendAsync(createRequestMessage(), cancellationToken));
            return response;
        }

        public static async Task<HttpResponseMessage> PutAsJsonAsync(Uri requestUri, IDictionary<string, string> headers, object content)
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
            var response = await ExecuteActionkWithAutoRetry(() => Client.SendAsync(createRequestMessage()));
            return response;
        }

        /// <summary>
        /// Send Http Delete to request uri and get HttpResponseMessage
        /// </summary>
        public static async Task<HttpResponseMessage> DeleteAsync(Uri requestUri, IDictionary<string, string> headers)
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
            HttpResponseMessage response = await ExecuteActionkWithAutoRetry(() => Client.SendAsync(deleteRequestMessage()));
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
    }
}
