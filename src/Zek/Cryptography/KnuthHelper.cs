using System;
using System.Text;

namespace Zek.Cryptography
{
    public static class KnuthHelper
    {
        public static string KnuthHex(ReadOnlySpan<char> key)
        {
            return CalculateHash(key).ToString("X2");
        }

        public static bool VerifyKnuthHex(string cypherText, string plainText)
        {
            var knuth = KnuthHex(plainText);
            return cypherText == knuth;

        }

        public static ulong CalculateHash(ReadOnlySpan<char> key)
        {
            var hashedValue = 3074457345618258791UL;
            for (int i = 0; i < key.Length; i++)
            {
                hashedValue += key[i];
                hashedValue *= 3074457345618258799UL;
            }
            return hashedValue;
        }
    }
}
