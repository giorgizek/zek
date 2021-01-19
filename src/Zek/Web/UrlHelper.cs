namespace Zek.Web
{
    public static class UrlHelper
    {
        public static string Combine(string baseUri, string relativeUri)
        {
            if (string.IsNullOrEmpty(baseUri))
                return relativeUri;
            if (string.IsNullOrEmpty(relativeUri))
                return baseUri;

            baseUri = baseUri.TrimEnd('/');
            relativeUri = relativeUri.TrimStart('/');
            return $"{baseUri}/{relativeUri}";
        }
    }
}