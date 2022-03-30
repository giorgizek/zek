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


        public static bool VerifyKnuthHex(string cypherText, string plainText)
        {
            var knuth = KnuthHex(plainText);
            return cypherText == knuth;

        }

        public static ulong CalculateHash(string plainText) => CalculateHash(Encoding.Default.GetBytes(plainText));
        public static ulong CalculateHash(byte[] bytes)
        {
            var hashedValue = 3074457345618258791UL;
            foreach (var b in bytes)
            {
                hashedValue += b;
                hashedValue *= 3074457345618258799UL;
            }
            return hashedValue;
        }
    }
}
