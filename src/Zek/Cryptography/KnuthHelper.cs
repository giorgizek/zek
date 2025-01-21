namespace Zek.Cryptography
{
    public static class KnuthHelper
    {
        public static string KnuthHex(ReadOnlySpan<char> plainText)
        {
            return CalculateHash(plainText).ToString("X2");
        }

        public static bool VerifyKnuthHex(string cypherText, string plainText)
        {
            var knuth = KnuthHex(plainText);
            return cypherText == knuth;

        }

        public static ulong CalculateHash(ReadOnlySpan<char> plainText)
        {
            var hashedValue = 3074457345618258791UL;
            for (int i = 0; i < plainText.Length; i++)
            {
                hashedValue += plainText[i];
                hashedValue *= 3074457345618258799UL;
            }
            return hashedValue;
        }
    }
}
