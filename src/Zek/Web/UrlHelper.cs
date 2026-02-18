using System.Web;

namespace Zek.Web
{
    public static class UrlHelper
    {
        public static string Combine(params string?[] parts)
        {
            string? result = string.Empty;
            foreach (var part in parts)
            {
                if (string.IsNullOrEmpty(part))
                    continue;

                result = CombineEnsureSingleSeparator(result, part, '/');
            }

            return result ?? string.Empty;
        }


        private static string? CombineEnsureSingleSeparator(string? a, string? b, char separator)
        {
            if (string.IsNullOrEmpty(a)) return b;
            if (string.IsNullOrEmpty(b)) return a;
            return a.TrimEnd(separator) + separator + b.TrimStart(separator);
        }

        public static bool IsValidUrl(string? url) =>
            Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
            (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);



        public static string? MergeQueryString(string url, Dictionary<string, string?>? newParams)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            // If there are no parameters to add, return the original URL
            if (newParams is null)
                return url;

            // 1. Use UriBuilder to safely parse and modify the URL
            var uriBuilder = new UriBuilder(url);

            // 2. Extract existing query parameters.
            // HttpUtility.ParseQueryString handles URL decoding and creates a collection.
            var existingParams = HttpUtility.ParseQueryString(uriBuilder.Query);

            // 4. Merge the new parameters into the existing collection.
            // This will add new keys or overwrite existing ones.
            if (newParams is not null)
            {
                foreach (var kvp in newParams)
                {
                    // We use item.Value, which ToJsonString has already converted to a string.
                    existingParams[kvp.Key] = kvp.Value;
                }
            }

            // 5. Re-assign the merged collection back to the UriBuilder.
            // Your existing ToQueryString(NameValueCollection) extension method
            // will correctly URL-encode and format the string.
            uriBuilder.Query = existingParams.ToQueryString();

            if ((uriBuilder.Scheme == Uri.UriSchemeHttp && uriBuilder.Port == 80) ||
                (uriBuilder.Scheme == Uri.UriSchemeHttps && uriBuilder.Port == 443))
            {
                uriBuilder.Port = -1; // Removes port from final URL
            }

            // 6. Return the fully reconstructed URL.
            // UriBuilder.ToString() correctly handles all parts (scheme, host, path, query).
            return uriBuilder.ToString();
        }


        /// <summary>
        /// Merges an object's properties into a URL's query string.
        /// Existing parameters in the URL with the same name are overwritten.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="url">The base URL, which may or may not have an existing query string.</param>
        /// <param name="parameters">The object whose public properties will be added as query parameters.</param>
        /// <returns>A new URL string with the merged query parameters.</returns>
        public static string? MergeQueryString<T>(string url, T parameters)
            where T : class
        {
            if (string.IsNullOrEmpty(url))
                return null;

            // If there are no parameters to add, return the original URL
            if (parameters is null)
                return url;

            var newParams = QueryStringHelper.ToDictionary(parameters);

            return MergeQueryString(url, newParams);
        }
    }
}