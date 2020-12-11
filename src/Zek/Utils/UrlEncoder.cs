using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace Zek.Utils
{
    /// <summary>
    /// Encode and decode urls by Base64UrlEncode and Base64UrlDecode.
    /// This is not standard encoder and if you encode you should decode too.
    /// </summary>
    public static class UrlEncoder
    {
        public static string Encode(string code)
        {
            return code == null
                ? null
                : WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        }

        public static string Decode(string code)
        {
            if (code == null)
                return null;

            var buffer = WebEncoders.Base64UrlDecode(code);
            var decoded = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

            return decoded;
        }
    }
}
