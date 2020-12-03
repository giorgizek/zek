using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zek.Extensions.Net
{
    public static class HttpContentExtensions
    {
        /// <summary>
        /// Returns a <see cref="Task"/> that will yield an object of the specified
        /// from the <paramref name="content"/> instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <remarks>This override use the built-in collection of formatters.</remarks>
        /// <param name="content">The <see cref="HttpContent"/> instance from which to read.</param>
        /// <returns>A <see cref="Task"/> that will yield an object instance of the specified type.</returns>
        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            var data = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
