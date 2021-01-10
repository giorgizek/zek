using System.Text;

namespace Zek.Cryptography
{
    public static class KnuthHelper
    {
        public static string KnuthHex(string plainText) => KnuthHex(Encoding.UTF8.GetBytes(plainText));
        public static string KnuthHex(byte[] bytes)
        {
            return CalculateHash(bytes).ToString("X2");
        }


        public static long CalculateHash(string plainText) => CalculateHash(Encoding.Default.GetBytes(plainText));
        public static long CalculateHash(byte[] bytes)
        {
            unchecked
            {
                var hashedValue = 3074457345618258791UL;
                foreach (var b in bytes)
                {
                    hashedValue += b;
                    hashedValue *= 3074457345618258799UL;
                }
                return (long)hashedValue;
            }
        }
    }
}
