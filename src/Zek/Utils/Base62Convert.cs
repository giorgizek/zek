using System.Text;

namespace Zek.Utils
{
    public static class Base62Convert
    {
        private const string DefaultCharacterSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public static string Encode(long value)
        {
            if (value == 0) return DefaultCharacterSet[0].ToString();

            var result = new StringBuilder();
            while (value > 0)
            {
                result.Insert(0, DefaultCharacterSet[(int)(value % 62)]);
                value /= 62;
            }
            return result.ToString();
        }

        public static long Decode(string value)
        {
            long result = 0;
            foreach (char c in value)
            {
                result = result * 62 + DefaultCharacterSet.IndexOf(c);
            }
            return result;
        }
    }
}
