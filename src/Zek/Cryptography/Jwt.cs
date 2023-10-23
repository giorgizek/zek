//using Newtonsoft.Json.Serialization;
//using Newtonsoft.Json;
//using System;
//using System.Security.Cryptography;
//using System.Text;
//using Zek.Utils;

//namespace Zek.Cryptography
//{

//    public static class Zzz
//    {

//        private static DefaultContractResolver contractResolver = new()
//        {
//            NamingStrategy = new CamelCaseNamingStrategy()
//        };

//        private static string SerializeObject(object obj)
//        {
//            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
//            {
//                ContractResolver = contractResolver,
//                Formatting = Formatting.Indented
//            });
//        }

//        public static string Create(Header header, object payload, string secret)
//        {
//            var headerJson = SerializeObject(header);
//            var payloadJson = SerializeObject(payload);

//            var base64Header = Base64UrlEncode(headerJson);
//            var base64Payload = Base64UrlEncode(payloadJson);

//            var signature = GenerateSignature(base64Header, base64Payload, secret);

//            return $"{base64Header}.{base64Payload}.{signature}";
//        }

//        private static string GenerateSignature(string base64Header, string base64Payload, string secret)
//        {
//            var cipherText = $"{base64Header}.{base64Payload}";
//            var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
//            var hashResult = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(cipherText));
//            return Base64UrlEncode(hashResult);
//        }

//        private static string Base64UrlEncode(string value)
//        {
//            var bytes = Encoding.UTF8.GetBytes(value);
//            return Base64UrlEncode(bytes);
//        }

//        private static string Base64UrlEncode(byte[] bytes)
//        {
//            var base64 = Convert.ToBase64String(bytes);
//            var base64Url = base64.TrimEnd('=').Replace('+', '-').Replace('/', '_');
//            return base64Url;
//        }


//        public static bool Verify<T>(string jwt, string secret)
//        {
//            var chunks = jwt.Split('.', StringSplitOptions.RemoveEmptyEntries);
//            if (chunks.Length != 3)
//                return false;

//            //get the header
//            var header = JsonConvert.DeserializeObject<Header>(Base64UrlDecode(chunks[0]));
//            if (header == null) return false;
//            if (header.Alg != "HS256")
//            {
//                return false;
//            }

//            var payload = JsonConvert.DeserializeObject<T>(Base64UrlDecode(chunks[1]));



//            //we only need to generate the signature again and match with the signature in the jwt to verify it.
//            var signature = GenerateSignature(chunks[0], chunks[1], secret);
//            return signature == chunks[2];
//        }

//        //Thanks to this StackOverflow (answer)[https://stackoverflow.com/a/26354677]
//        public static string Base64UrlDecode(string encodedString)
//        {
//            string incoming = encodedString
//                .Replace('_', '/').Replace('-', '+');
//            switch (incoming.Length % 4)
//            {
//                case 2: incoming += "=="; break;
//                case 3: incoming += "="; break;
//            }

//            byte[] bytes = Convert.FromBase64String(incoming);
//            string originalText = Encoding.ASCII.GetString(bytes);
//            return originalText;
//        }
//    }

//    public struct Header
//    {
//        public string Alg { set; get; }
//        public string Typ { set; get; }
//    }

//    public class Payload<T>
//    {
//        internal long? _exp;
//        internal DateTime? _expDateTime;


//        /// <summary>
//        /// Gets the 'value' of the 'expiration' claim { exp, 'value' } converted to a <see cref="DateTime"/> assuming 'value' is seconds since UnixEpoch (UTC 1970-01-01T0:0:0Z).
//        /// </summary>
//        /// <remarks>If the 'expiration' claim is not found, then <see cref="DateTime.MinValue"/> is returned.</remarks>
//        [JsonIgnore]
//        public DateTime ValidTo
//        {
//            get
//            {
//                if (_expDateTime.HasValue)
//                    return _expDateTime.Value;

//                long? l = Exp;
//                if (l.HasValue)
//                    return EpochTime.DateTime(l.Value);

//                _expDateTime = DateTime.MinValue;

//                return _expDateTime.Value;
//            }
//            set
//            {
//                _exp = EpochTime.GetIntDate(value.ToUniversalTime());
//            }
//        }

//        public long? Exp
//        {
//            get => _exp;
//            set
//            {
//                _exp = value;
//                if (value.HasValue)
//                {
//                    _expDateTime = EpochTime.DateTime(value.Value);
//                }
//            }
//        }


//        public T Value { get; set; }
//    }
//}
