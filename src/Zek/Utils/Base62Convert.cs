namespace Zek.Utils
{
    public static class Base62Convert
    {
        private const string DefaultCharacterSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public static string Encode(int number)
        {
            var ret = string.Empty;
            if (number == 0)
            {
                return $"{DefaultCharacterSet[0]}";
            }

            while (number > 0)
            {
               // Convert.ToBase64String
                ret = DefaultCharacterSet[number % 62] + ret;
                number /= 62;
            }
            return ret;
        }


        public static int Decode(string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;

            var result = 0;
            var length = value.Length;
            for (var i = 0; i < length; i++)
            {
                int ch = value[i];
                if (ch < 58)
                { // 0-9
                    ch -= 48;
                }
                else if (ch < 91)
                { // A-Z
                    ch -= 55;
                }
                else
                { // a-z
                    ch -= 61;
                }
                result += ch * (int)Math.Pow(62, length - i - 1);
            }
            return result;
        }

        //public static int Decode(string value)
        //{
        //    if (string.IsNullOrEmpty(value))
        //        return 0;


        //    string r = Reverse(value);

        //    var id = 0;
        //    for (int i = 0; i < r.Length; i++)
        //    {
        //        int charIndex = DefaultCharacterSet.IndexOf(r[i]);
        //        id += charIndex * (int)Math.Pow(62, i);
        //    }
        //    return id;
        //}

        //public static string Reverse(string s)
        //{
        //    char[] charArray = s.ToCharArray();
        //    Array.Reverse(charArray);
        //    return new string(charArray);
        //}

    }
}
