using System;
using System.Collections.Generic;
using System.Text;

namespace Zek.Utils
{
    public class StringHelper
    {
        public static string RemoveChars(string value, List<char> charsToRemove)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var sb = new StringBuilder();

            foreach (var c in value)
            {
                if (!charsToRemove.Contains(c))
                    sb.Append(c);
            }

            return sb.ToString();
        }


        public static string Replace(string str, string[] from, string[] to)
        {
            if (string.IsNullOrEmpty(str) || from == null || from.Length == 0 || to == null || to.Length == 0)
                return str;

            if (from.Length != to.Length)
                throw new ArgumentException($"'{nameof(from)}' and '{nameof(to)}' params size must be same", nameof(to));

            var sb = new StringBuilder(str);
            for (var i = 0; i < from.Length; i++)
            {
                sb.Replace(from[i], to[i]);
            }

            return sb.ToString();
        }


        /// <summary>
        /// Finds char array count from text.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="searchChars"></param>
        /// <returns></returns>
        public static int FindCount(string str, string searchChars)
        {
            if (string.IsNullOrEmpty(str))
                return 0;
            var count = 0;
            foreach (var c1 in str)
            {
                foreach (var c2 in searchChars)
                {
                    if (c1 == c2)
                        count++;
                }
            }

            return count;
        }


        /// <summary>
        /// Extension method to test whether the value is a base64 string
        /// </summary>
        /// <param name="value">Value to test</param>
        /// <returns>Returns true if the string is base64, otherwise false</returns>
        public static bool IsBase64(string value)
        {
            // The quickest test. If the value is null or is equal to 0 it is not base64
            // Base64 string's length is always divisible by four, i.e. 8, 16, 20 etc. 
            // If it is not you can return false. Quite effective
            // Further, if it meets the above criterias, then test for spaces.
            // If it contains spaces, it is not base64
            if (string.IsNullOrEmpty(value) || value.Length % 4 != 0
                                            || value.Contains(' ') || value.Contains('\t') || value.Contains('\r') || value.Contains('\n'))
                return false;



            /*
                        // 98% of all non base64 values are invalidated by this time.
                        var index = value.Length - 1;

                        // if there is padding step back
                        if (value[index] == '=')
                            index--;

                        // if there are two padding chars step back a second time
                        if (value[index] == '=')
                            index--;

                        // Now traverse over characters
                        // You should note that I'm not creating any copy of the existing strings, 
                        // assuming that they may be quite large
                        for (var i = 0; i <= index; i++)
                            if (IsInvalid(value[i]))// If any of the character is not from the allowed list
                                return false;
                        */

            // If we got here, then the value is a valid base64 string
            return true;
        }
        //private static bool IsInvalid(char value)
        //{
        //    var intValue = (int)value;

        //    // 1 - 9
        //    if (intValue >= 48 && intValue <= 57)
        //        return false;
            
        //    // A - Z
        //    if (intValue >= 65 && intValue <= 90)
        //        return false;
            
        //    // a - z   
        //    if (intValue >= 97 && intValue <= 122)
        //        return false;

        //    // + or /
        //    return intValue != 43 && intValue != 47;
        //}


        public static string Join(string separator, params object[] values)
        {
            var sb = new StringBuilder();
            foreach (var value in values)
                if (value != null)
                    sb.Append(value + separator);

            sb.Remove(sb.Length - separator.Length, separator.Length);
            
            return sb.ToString();
        }
    }
}
