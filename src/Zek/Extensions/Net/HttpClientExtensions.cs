using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zek.Extensions.Net
{
    /// <summary>
    /// Extension methods that aid in making formatted requests using <see cref="HttpClient"/>.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class HttpClientExtensions
    {
        private static StringContent CreateJsonContent(object value)
        {
            return new StringContent(JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }), Encoding.UTF8, "application/json");
        }







        /// <summary>
        /// Sends a POST request as an asynchronous operation to the specified Uri with the given <paramref name="value"/> serialized
        /// as JSON.
        /// </summary>
        /// <remarks>
        /// This method uses the default instance of <see cref="JsonMediaTypeFormatter"/>.
        /// </remarks>
        /// <typeparam name="T">The type of <paramref name="value"/>.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#", Justification = "We want to support URIs as strings")]
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string requestUri, T value) => client.PostAsJsonAsync(requestUri, value, CancellationToken.None);

        /// <summary>
        /// Sends a POST request as an asynchronous operation to the specified Uri with the given <paramref name="value"/> serialized
        /// as JSON.
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="value"/>.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#", Justification = "We want to support URIs as strings")]
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string requestUri, T value, CancellationToken cancellationToken) =>
            //return client.PostAsync(requestUri, value, new JsonMediaTypeFormatter(), cancellationToken);
            client.InternalPostAsync(requestUri, CreateJsonContent(value), cancellationToken);

        private static Task<HttpResponseMessage> InternalPostAsync(this HttpClient client, string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            //ObjectContent<T> content = new ObjectContent<T>(value, formatter, mediaType);
            return client.PostAsync(requestUri, content, cancellationToken);
        }





        /// <summary>
        /// Sends a POST request as an asynchronous operation to the specified Uri with the given <paramref name="value"/> serialized
        /// as JSON.
        /// </summary>
        /// <remarks>
        /// This method uses the default instance of <see cref="JsonMediaTypeFormatter"/>.
        /// </remarks>
        /// <typeparam name="T">The type of <paramref name="value"/>.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#", Justification = "We want to support URIs as strings")]
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value) => client.PostAsJsonAsync(requestUri, value, CancellationToken.None);

        /// <summary>
        /// Sends a POST request as an asynchronous operation to the specified Uri with the given <paramref name="value"/> serialized
        /// as JSON.
        /// </summary>
        /// <remarks>
        /// This method uses the default instance of <see cref="JsonMediaTypeFormatter"/>.
        /// </remarks>
        /// <typeparam name="T">The type of <paramref name="value"/>.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#", Justification = "We want to support URIs as strings")]
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken cancellationToken) =>
            //return client.PostAsync(requestUri, value, new JsonMediaTypeFormatter(), cancellationToken);
            client.InternalPostAsync(requestUri, CreateJsonContent(value), cancellationToken);

        private static Task<HttpResponseMessage> InternalPostAsync(this HttpClient client, Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            // ObjectContent<T> content = new ObjectContent<T>(value, formatter, mediaType);
            return client.PostAsync(requestUri, content, cancellationToken);
        }
    }
}
