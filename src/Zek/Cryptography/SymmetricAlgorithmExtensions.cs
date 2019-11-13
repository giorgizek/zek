using System;
using System.Security.Cryptography;
using System.Text;

namespace Zek.Cryptography
{
    internal static class SymmetricAlgorithmExtensions
    {
        public static byte[] GetLegalKey(this SymmetricAlgorithm algorithm, string key) => GetLegalKey(algorithm, Encoding.UTF8.GetBytes(key));

        public static byte[] GetLegalKey(this SymmetricAlgorithm algorithm, byte[] key)
        {
            // Adjust key if necessary, and return a valid key
            if (algorithm.LegalKeySizes.Length > 0)
            {
                // Key sizes in bits
                var keySize = key.Length * 8;
                var minSize = algorithm.LegalKeySizes[0].MinSize;
                var maxSize = algorithm.LegalKeySizes[0].MaxSize;
                var skipSize = algorithm.LegalKeySizes[0].SkipSize;

                if (keySize > maxSize)
                {
                    var buffer = new byte[maxSize / 8];
                    Buffer.BlockCopy(key, 0, buffer, 0, buffer.Length);
                    return buffer;
                }

                if (keySize < maxSize)
                {
                    var validSize = keySize <= minSize
                        ? minSize
                        : keySize - keySize % skipSize + skipSize;
                    if (keySize < validSize)
                    {
                        // Pad the key with asterisk to make up the size
                        var buffer = new byte[validSize / 8];
                        Buffer.BlockCopy(key, 0, buffer, 0, key.Length);
                        return buffer;
                    }
                }
            }

            return key;
        }
    }
}
