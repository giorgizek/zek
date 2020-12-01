using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Zek.Utils
{
    public class RandomHelper
    {
        /// <summary>
        /// Gets a random object with a real random seed
        /// </summary>
        /// <returns></returns>
        public static Random GetRandom()
        {
            // Use a 4-byte array to fill it with random bytes and convert it then
            // to an integer value.
            var randomBytes = new byte[4];

            // Generate 4 random bytes.
            //new RNGCryptoServiceProvider().GetBytes(randomBytes);
            RandomNumberGenerator.Create().GetBytes(randomBytes);

            // Convert 4 bytes into a 32-bit integer value.
            var seed = (randomBytes[0] & 0x7f) << 24 |
                       randomBytes[1] << 16 |
                       randomBytes[2] << 8 |
                       randomBytes[3];

            // Now, this is real randomization.
            return new Random(seed);
        }

        public static string GetRandomString(int length, string chars)
        {
            var randomBytes = new byte[length];
            GetRandom().NextBytes(randomBytes);
            var tempChars = new char[length];
            var allowedCharCount = chars.Length;

            for (var i = 0; i < length; i++)
            {
                tempChars[i] = chars[randomBytes[i] % allowedCharCount];
            }

            return new string(tempChars);
        }

        public static string Shuffle(string str)
        {
            var rnd = GetRandom();
            //return new string(str.OrderBy(r => rnd.Next()).ToArray());

            var chars = new List<char>(str);
            var sb = new StringBuilder();
            while (chars.Count > 0)
            {
                var index = rnd.Next(chars.Count);
                sb.Append(chars[index]);
                chars.RemoveAt(index);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Fisher-Yates Shuffle
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        public static void Shuffle<T>(T[] array)
        {
            var rnd = GetRandom();
            for (var i = array.Length - 1; i > 0; i--)
            {
                var index = rnd.Next(i);
                //swap
                var tmp = array[index];
                array[index] = array[i];
                array[i] = tmp;
            }

            //var n = array.Length;
            //for (var i = 0; i < n; i++)
            //{
            //    // NextDouble returns a random number between 0 and 1.
            //    // ... It is equivalent to Math.random() in Java.
            //    var r = i + (int)(rnd.NextDouble() * (n - i));
            //    var t = array[r];
            //    array[r] = array[i];
            //    array[i] = t;
            //}
        }
    }
}
