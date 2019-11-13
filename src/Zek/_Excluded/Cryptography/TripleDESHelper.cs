namespace Zek.Cryptography
{
   /*
    // ReSharper disable once InconsistentNaming
    public class TripleDESHelper
    {
        public static string Encrypt(string plainText, string key, string iv)
        {
            return Encrypt(plainText, ByteUtils.HexToByteArray(key), ByteUtils.HexToByteArray(iv));
        }
        public static string Encrypt(string plainText, byte[] key, byte[] iv)
        {
            using (var alg = TripleDES.Create())
            {
                alg.Key = key;
                alg.IV = iv;
                alg.Padding = PaddingMode.PKCS7;
                alg.Mode = CipherMode.CBC;

                return alg.Encrypt(plainText);
            }
        }

        public static string Decrypt(string cipher, string key, string iv)
        {
            return Decrypt(cipher, ByteUtils.HexToByteArray(key), ByteUtils.HexToByteArray(iv));
        }
        public static string Decrypt(string cipher, byte[] key, byte[] iv)
        {
            using (var alg = TripleDES.Create())
            {
                alg.Key = key;
                alg.IV = iv;
                alg.Padding = PaddingMode.PKCS7;
                alg.Mode = CipherMode.CBC;

                return alg.Decrypt(cipher);
            }
        }
    }*/
}
