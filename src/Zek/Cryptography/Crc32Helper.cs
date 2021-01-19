using System.Text;
using Zek.Utils;

namespace Zek.Cryptography
{
    public class Crc32Helper
    {
        public static string Crc32Hex(string plainText) => Crc32Hex(Encoding.UTF8.GetBytes(plainText));

        public static string Crc32Hex(byte[] bytes)
        {
            using var crc32 = new Crc32();
            return ByteArrayHelper.ByteArrayToHex(crc32.ComputeHash(bytes));
        }


        public static bool VerifyCrc32Hex(string cypherText, string plainText)
        {
            var crc32 = Crc32Hex(plainText);
            return cypherText == crc32;
        }
    }
}