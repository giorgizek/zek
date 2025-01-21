namespace Zek.Web
{
    public static class UrlHelper
    {
        public static string Combine(params string[] parts)
        {
            string result = string.Empty;
            foreach (string part in parts)
            {
                if (string.IsNullOrEmpty(part))
                    continue;

                result = CombineEnsureSingleSeparator(result, part, '/');
            }

            return result;
        }


        private static string CombineEnsureSingleSeparator(string a, string b, char separator)
        {
            if (string.IsNullOrEmpty(a)) return b;
            if (string.IsNullOrEmpty(b)) return a;
            return a.TrimEnd(separator) + separator + b.TrimStart(separator);
        }
    }
}