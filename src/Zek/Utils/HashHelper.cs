using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Zek.Utils
{
    public static class HashHelper
    {
        private static byte[] _keyBytes = [];
        private static bool _isInitialized = false; // Flag to track initialization
        public static void Init(string secretKey)
        {
            if (string.IsNullOrEmpty(secretKey)) throw new ArgumentNullException(nameof(secretKey));
            _keyBytes = Encoding.UTF8.GetBytes(secretKey);
            _isInitialized = true;
        }

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            // Important: Ensure dictionary keys are sorted if your DTOs contain dictionaries
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };


        /// <summary>
        /// Computes a HMACSHA256 hash for any DTO.
        /// </summary>
        /// <typeparam name="T">The type of the DTO.</typeparam>
        /// <param name="dto">The object to hash.</param>
        /// <param name="secretKey">The secret key shared between sender and receiver.</param>
        /// <returns>The hash as a Base64 string.</returns>
        public static string ComputeHash<T>(T dto)
        {
            // FIX 1: Prevent running with an empty key
            if (!_isInitialized)
                throw new InvalidOperationException("HashHelper is not initialized. Call HashHelper.Init(key) at app startup.");
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            // 1. Serialize the DTO to a canonical JSON string
            var jsonString = JsonSerializer.Serialize(dto, _jsonOptions);
            var payloadBytes = Encoding.UTF8.GetBytes(jsonString);

            // 2. Compute HMAC
            using var hmac = new HMACSHA256(_keyBytes);
            var hashBytes = hmac.ComputeHash(payloadBytes);

            // 3. Return as Base64 (Standard for web transport)
            return Convert.ToBase64String(hashBytes);
        }


        /// <summary>
        /// Verifies if a DTO matches a specific hash using the secret key.
        /// </summary>
        public static bool Verify<T>(T dto , string incomingHash)
        {
            if (string.IsNullOrEmpty(incomingHash)) return false;

            // FIX 1 (Continued): Ensure Init is called before computing the comparison hash
            if (!_isInitialized)
                throw new InvalidOperationException("HashHelper is not initialized. Call HashHelper.Init(key) at app startup.");

            // Re-compute the hash for the current DTO state
            var computedHash = ComputeHash(dto);

            // Compare the two hashes securely
            return SecureEquals(computedHash, incomingHash);
        }

        /// <summary>
        /// Compares two strings in constant time to prevent Timing Attacks.
        /// </summary>
        private static bool SecureEquals(string a, string b)
        {
            // Optimization: If lengths differ, they can't match. 
            // (Takes negligible time, so not a timing attack risk in this context)
            if (a.Length != b.Length) return false;

            try
            {
                // Uses CryptographicOperations.FixedTimeEquals (Available in .NET Core 2.1+)
                // This prevents hackers from guessing the hash based on how long the comparison takes.
                var bytesA = Convert.FromBase64String(a);
                var bytesB = Convert.FromBase64String(b);

                return CryptographicOperations.FixedTimeEquals(bytesA, bytesB);
            }
            catch (FormatException)
            {
                // FIX 2: Handle invalid Base64 strings gracefully without crashing
                return false;
            }
        }
    }
}
