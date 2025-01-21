using System.Security.Cryptography;
using System.Text;
using Zek.Utils;

namespace Zek.Cryptography
{
    public static class SHA256Helper
    {
        public static string SHA256Hex(string plainText) => SHA256Hex(Encoding.UTF8.GetBytes(plainText));

        public static string SHA256Hex(byte[] bytes)
        {
            using var sha = SHA256.Create();
            return ByteArrayHelper.ByteArrayToHex(sha.ComputeHash(bytes));
        }
        public static bool VerifySHA256Hex(string cypherText, string plainText)
        {
            var sha = SHA256Hex(plainText);
            return cypherText == sha;
        }



        public static string SHA256Base64(string plainText) => SHA256Base64(Encoding.UTF8.GetBytes(plainText));

        public static string SHA256Base64(byte[] bytes)
        {
            using var sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(bytes));
        }
        public static bool VerifySHA256Base64(string cypherText, string plainText)
        {
            var sha = SHA256Base64(plainText);
            return cypherText == sha;
        }
    }
}
