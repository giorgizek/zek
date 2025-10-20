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

        public static bool IsValidUrl(string url)
        {
            // TryCreate attempts to create a Uri object from the string.
            // UriKind.Absolute ensures it's a fully qualified URL (not just a path).
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}