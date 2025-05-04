namespace Zek.Utils
{
    public static class UrlShortener
    {
        private const string Base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        //public static string Encode(long value)
        //{
        //    if (value == 0) return "0";

        //    var result = new StringBuilder();
        //    while (value > 0)
        //    {
        //        result.Insert(0, Base62Chars[(int)(value % 62)]);
        //        value /= 62;
        //    }
        //    return result.ToString();
        //}

        public static string Encode(long number)
        {
            if (number == 0) return "0";

            var result = string.Empty;
            while (number > 0)
            {
                result = Base62Chars[(int)(number % 62)] + result;
                number /= 62;
            }
            return result;
        }


        public static long Decode(string code)
        {
            long result = 0;
            foreach (char c in code)
            {
                result = result * 62 + Base62Chars.IndexOf(c);
            }
            return result;
        }


        //public static string Encode(Guid guid)
        //{
        //    var bytes = guid.ToByteArray();
        //    var result = new StringBuilder();
        //    ulong value = 0;

        //    foreach (byte b in bytes)
        //    {
        //        value = (value << 8) | b;
        //    }

        //    while (value > 0)
        //    {
        //        result.Insert(0, Base62Chars[(int)(value % 62)]);
        //        value /= 62;
        //    }

        //    return result.ToString();
        //}
    }
}
