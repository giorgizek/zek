namespace Zek.Utils
{
    public static class Base64Helper
    {
        public static string ToBase64Url(byte[] bytes)
        {
            var base64 = Convert.ToBase64String(bytes);
            var base64Url = base64
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');
            return base64Url;
        }
    }
}


