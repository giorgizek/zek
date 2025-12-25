using Newtonsoft.Json;
using Zek.Cryptography;
// ReSharper disable InconsistentNaming

namespace Zek.Utils
{
    public enum IdLinkMode
    {
        //MD5,
        SHA1 = 1,
        //SHA256,
        Aes
    }

    public class IdLinkBase
    {
        public DateTime? ValidTo { get; set; }
    }

    public static class IdLinkHelper
    {
        public static string Encode(object value, string key)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(value));

            var json = JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return AesHelper.Encrypt(json, key);
        }

        public static T? Decode<T>(string idLink, string key)
        {
            ArgumentNullException.ThrowIfNull(idLink);

            byte[] array;
            try
            {
                array = Convert.FromBase64String(idLink);
            }
            catch
            {
                return default;
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(AesHelper.Decrypt(array, key));
            }
            catch
            {
                return default;
            }

        }
    }
}
