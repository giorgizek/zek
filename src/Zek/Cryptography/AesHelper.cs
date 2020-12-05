using System;
using System.IO;
using System.Security.Cryptography;

namespace Zek.Cryptography
{
    public static class AesHelper
    {
        public static byte[] EncryptToByteArray(string plainText, string key)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            using var aesAlg = Aes.Create();
            aesAlg.Key = aesAlg.GetLegalKey(key);

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }
            }

            var iv = aesAlg.IV;

            var decryptedContent = msEncrypt.ToArray();

            var result = new byte[iv.Length + decryptedContent.Length];

            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
            Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

            return result;
        }
        public static string Encrypt(string plainText, string key) => Convert.ToBase64String(EncryptToByteArray(plainText, key));


        public static string Decrypt(byte[] cipherBytes, string key)
        {
            if (cipherBytes == null || cipherBytes.Length == 0)
                throw new ArgumentNullException(nameof(cipherBytes));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            using var aesAlg = Aes.Create();
            var iv = new byte[aesAlg.IV.Length];
            var cipher = new byte[cipherBytes.Length - iv.Length];

            Buffer.BlockCopy(cipherBytes, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(cipherBytes, iv.Length, cipher, 0, cipher.Length);
            var decryptor = aesAlg.CreateDecryptor(aesAlg.GetLegalKey(key), iv);
            using var msDecrypt = new MemoryStream(cipher);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            var plaintext = srDecrypt.ReadToEnd();

            return plaintext;
        }

        public static string Decrypt(string cipherText, string key)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException(nameof(cipherText));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return Decrypt(Convert.FromBase64String(cipherText), key);
        }
    }
}
