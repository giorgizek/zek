using System.Security.Cryptography;
using System.Text;
using Zek.Utils;

namespace Zek.Cryptography
{
    [Obsolete("Use SHA256Helper")]
    public static class SHA1Helper
    {
        public static string SHA1Hex(string plainText) => SHA1Hex(Encoding.UTF8.GetBytes(plainText));

        public static string SHA1Hex(byte[] bytes)
        {
            using var sha1 = SHA1.Create();
            return ByteArrayHelper.ByteArrayToHex(sha1.ComputeHash(bytes));
        }
        public static bool VerifySHA1Hex(string cypherText, string plainText)
        {
            var sha1 = SHA1Hex(plainText);
            return cypherText == sha1;
        }



        public static string SHA1Base64(string plainText) => SHA1Base64(Encoding.UTF8.GetBytes(plainText));

        public static string SHA1Base64(byte[] bytes)
        {
            using var sha1 = SHA1.Create();
            return Convert.ToBase64String(sha1.ComputeHash(bytes));
        }
        public static bool VerifySHA1Base64(string cypherText, string plainText)
        {
            var sha1 = SHA1Base64(plainText);
            return cypherText == sha1;
        }
    }
}
