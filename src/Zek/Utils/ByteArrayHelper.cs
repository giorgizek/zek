using System.Globalization;
using System.Text;

namespace Zek.Utils
{
    public static class ByteArrayHelper
    {
        public static byte[] HexToByteArray(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];

            for (var i = 0; i < hexString.Length; i += 2)
            {
                var s = hexString.Substring(i, 2);
                bytes[i / 2] = byte.Parse(s, NumberStyles.HexNumber, null);
            }

            return bytes;
        }

        public static string ByteArrayToHex(byte[] bytes)
        {
            var builder = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes)
            {
                //builder.AppendFormat("{0:X2}", b);
                builder.AppendFormat(b.ToString("X2"));//this is faster
            }

            //BitConverter.ToString(bytes).Replace("-", string.Empty);

            return builder.ToString();
        }


        public static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }
            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }
    }
}
