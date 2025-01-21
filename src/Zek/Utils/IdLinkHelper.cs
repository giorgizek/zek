using System.Security.Cryptography;
using System.Text;
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
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(value));

            var json = JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return AesHelper.Encrypt(json, key);
        }

        public static T Decode<T>(string idLink, string key)
        {
            if (idLink == null)
                throw new ArgumentNullException(nameof(idLink));

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






        [Obsolete]
        public static string Encode(IEnumerable<string> values, string key = null, IdLinkMode mode = IdLinkMode.SHA1)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            var tmpArray = values as string[] ?? values.ToArray();
            switch (mode)
            {
                case IdLinkMode.SHA1:
                    string hash;
                    using (var alg = SHA1.Create())
                    {
                        var plainText = string.Join(string.Empty, tmpArray) + key;
                        hash = Convert.ToBase64String(alg.ComputeHash(Encoding.UTF8.GetBytes(plainText)));
                    }
                    return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join("||", tmpArray) + "||" + hash));

                case IdLinkMode.Aes:
                    return AesHelper.Encrypt(string.Join("||", tmpArray), key);

                default:
                    return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join("||", tmpArray)));
            }
        }

        [Obsolete]
        public static string[] Decode(string idLink, string key = null, IdLinkMode mode = IdLinkMode.SHA1)
        {
            if (idLink == null)
                throw new ArgumentNullException(nameof(idLink));

            byte[] array;
            try
            {
                array = Convert.FromBase64String(idLink);
            }
            catch
            {
                return null;
            }

            string[] values;
            switch (mode)
            {
                case IdLinkMode.Aes:
                    try
                    {
                        values = AesHelper.Decrypt(array, key).Split(new[] { "||" }, StringSplitOptions.None);
                    }
                    catch
                    {
                        return null;
                    }
                    break;

                //case IdLinkMode.MD5:
                //    values = Encoding.UTF8.GetString(array).Split(new[] { "||" }, StringSplitOptions.None);
                //    if (values.Length == 1)
                //        return null;

                //    using (var alg = MD5.Create())
                //    {
                //        var hashed = values[values.Length - 1];
                //        Array.Resize(ref values, values.Length - 1);

                //        var plainText = string.Join(string.Empty, values) + key;
                //        var hash = Convert.ToBase64String(alg.ComputeHash(Encoding.UTF8.GetBytes(plainText)));
                //        if (hashed != hash)
                //            return null;
                //    }
                //    break;

                case IdLinkMode.SHA1:
                    values = Encoding.UTF8.GetString(array).Split(["||"], StringSplitOptions.None);
                    if (values.Length == 1)
                        return null;

                    using (var alg = SHA1.Create())
                    {
                        var hashed = values[values.Length - 1];
                        Array.Resize(ref values, values.Length - 1);

                        var plainText = string.Join(string.Empty, values) + key;
                        var hash = Convert.ToBase64String(alg.ComputeHash(Encoding.UTF8.GetBytes(plainText)));
                        if (hashed != hash)
                            return null;
                    }
                    break;

                //case IdLinkMode.SHA256:
                //    values = Encoding.UTF8.GetString(array).Split(new[] { "||" }, StringSplitOptions.None);
                //    if (values.Length == 1)
                //        return null;

                //    using (var alg = SHA256.Create())
                //    {
                //        var hashed = values[values.Length - 1];
                //        Array.Resize(ref values, values.Length - 1);

                //        var plainText = string.Join(string.Empty, values) + key;
                //        var hash = Convert.ToBase64String(alg.ComputeHash(Encoding.UTF8.GetBytes(plainText)));
                //        if (hashed != hash)
                //            return null;
                //    }
                //    break;

                default:
                    values = Encoding.UTF8.GetString(array).Split(new[] { "||" }, StringSplitOptions.None);
                    break;
            }

            return values;
        }


        [Obsolete]
        public static string EncodeObject(object value, string key = null, IdLinkMode mode = IdLinkMode.SHA1)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var json = JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            switch (mode)
            {
                case IdLinkMode.SHA1:
                    string hash;
                    using (var alg = SHA1.Create())
                    {
                        hash = Convert.ToBase64String(alg.ComputeHash(Encoding.UTF8.GetBytes(json + key)));
                    }
                    return Convert.ToBase64String(Encoding.UTF8.GetBytes(json + "||" + hash));


                case IdLinkMode.Aes:
                    return AesHelper.Encrypt(json, key);

                default:
                    return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            }
        }

        [Obsolete]
        public static T DecodeObject<T>(string idLink, string key = null, IdLinkMode mode = IdLinkMode.SHA1)
        {
            if (idLink == null)
                throw new ArgumentNullException(nameof(idLink));



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
                switch (mode)
                {
                    case IdLinkMode.Aes:
                        return JsonConvert.DeserializeObject<T>(AesHelper.Decrypt(array, key));

                    case IdLinkMode.SHA1:
                        var values = Encoding.UTF8.GetString(array).Split(new[] { "||" }, StringSplitOptions.None);
                        if (values.Length == 1)
                            return default;

                        using (var alg = SHA1.Create())
                        {
                            var hashed = values[values.Length - 1];

                            var hash = Convert.ToBase64String(alg.ComputeHash(Encoding.UTF8.GetBytes(values[0] + key)));
                            if (hashed != hash)
                                return default;

                            return JsonConvert.DeserializeObject<T>(values[0]);
                        }

                    default:
                        return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(array));
                }
            }
            catch
            {
                return default;
            }

        }
    }
}
