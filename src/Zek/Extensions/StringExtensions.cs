using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Zek.Utils;

namespace Zek.Extensions
{
    public static class StringExtensions
    {
        public static string? RemoveDiacritics(this string? text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string? SafeToUpper(this string? str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var result = new StringBuilder();
            foreach (var c in str)
            {
                if (IsGeorgianLetter(c))
                    result.Append(c);
                else
                    result.Append(char.ToUpper(c));
            }

            return result.ToString();
        }
        public static string? SafeToUpperInvariant(this string? str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var result = new StringBuilder();
            foreach (var c in str)
            {
                if (IsGeorgianLetter(c))
                    result.Append(c);
                else
                    result.Append(char.ToUpperInvariant(c));
            }

            return result.ToString();
        }
        public static string? TryToUpper(this string? str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return str.ToUpper();
        }
        public static string? TryToLower(this string? str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return str.ToLower();
        }

        //public static bool IsEnglishLetter(this char c)
        //{
        //    return (c >= 'a' && c <= 'z') ||
        //        (c >= 'A' && c <= 'Z');
        //}
        private static bool IsGeorgianLetter(this char c)
        {
            return (c >= 'ა' && c <= 'ჰ');
        }
        public static bool IsAsciiOrDigit(this char c)
        {
            return ((c >= 'a' && c <= 'z') ||
                        (c >= 'A' && c <= 'Z') ||
                        (c >= '0' && c <= '9'));
        }

        /// <summary>
        /// Checks if string is null and return string.Empty otherwise returns string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EmptyIfNull(this string? str)
        {
            return str ?? string.Empty;
        }
        public static string? NullIfEmpty(this string? str)
        {
            return string.IsNullOrEmpty(str) ? null : str;
        }


        /// <summary>
        /// Ensures that a string only contains numeric values
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Input string with only numeric values, empty string if str is null/empty</returns>
        public static string? ToDigitOnly(this string? str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var result = new StringBuilder();
            foreach (var c in str)
            {
                if (char.IsDigit(c))
                    result.Append(c);
            }

            return result.ToString();
        }

        public static string? ToAsciiAlphanumeric(this string? str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var result = new StringBuilder(str.Length);
            foreach (var c in str)
            {
                if ((c >= 'a' && c <= 'z') ||
                    (c >= 'A' && c <= 'Z') ||
                    (c >= '0' && c <= '9'))
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// Returns a string containing only alphanumeric characters included unicodes (letters and digits).
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string? ToAlphanumeric(this string? str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var result = new StringBuilder();
            foreach (var c in str)
            {
                if (char.IsLetterOrDigit(c))
                    result.Append(c);
            }

            return result.ToString();
        }

        public static string[] Split(this string str, string seperator, StringSplitOptions options = StringSplitOptions.None)
        {
            return str.Split([seperator], options);
        }

        /*
        /// <summary>
        /// Returns a normalized representation of the specified <paramref name="key"/>
        /// by converting keys to their upper cased invariant culture representation.
        /// </summary>
        /// <param name="key">The key to normalize.</param>
        /// <returns>A normalized representation of the specified <paramref name="key"/>.</returns>
        public static string Normalize(this string?key)
        {
            return key?.Normalize().ToUpperInvariant();
        }
        */

        /// <summary>
        /// Safe Substring.
        /// </summary>
        /// <param name="str">Source text.</param>
        /// <param name="startIndex">Start index.</param>
        /// <param name="length">Chars length.</param>
        /// <returns>Substringed text</returns>
        public static string? SafeSubstring(this string? str, int startIndex, int? length = null)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            if (startIndex < 0)
                startIndex = 0;

            switch (length)
            {
                case null:
                    length = str.Length - startIndex;
                    break;
                case 0:
                    return string.Empty;
            }

            return str.Length > startIndex
                ? str.Length > startIndex + length
                    ? str.Substring(startIndex, length.Value)
                    : str[startIndex..]
                : string.Empty;
        }



        /// <summary>
        /// Returns a string containing a specified number of characters from the left side of a string.
        /// </summary>
        /// <param name="str">Required. String expression from which the leftmost characters are returned</param>
        /// <param name="length">Required. Integer expression. Numeric expression indicating how many characters to return. If 0, a zero-length string ("") is returned. If greater than or equal to the number of characters in str, the entire string is returned.</param>
        /// <returns>Returns a string containing a specified number of characters from the left side of a string.</returns>
        public static string? Left(this string? str, int length)
        {
            if (string.IsNullOrEmpty(str) || length >= str.Length || length < 0)
                return str;

            return str.Substring(0, length);
        }

        /// <summary>
        /// Returns a string containing a specified number of characters from the right side of a string.
        /// </summary>
        /// <param name="str">Required. String expression from which the rightmost characters are returned.</param>
        /// <param name="length">Required. Integer. Numeric expression indicating how many characters to return. If 0, a zero-length string ("") is returned. If greater than or equal to the number of characters in str, the entire string is returned.</param>
        /// <returns>Returns a string containing a specified number of characters from the right side of a string.</returns>
        public static string? Right(this string? str, int length)
        {
            if (string.IsNullOrEmpty(str) || length >= str.Length || length < 0)
                return str;

            return str[^length..];
        }



        public static string? TryTrim(this string? str)
        {
            return str?.Trim();
        }
        public static string? TryTrim(this string? str, params char[] trimChars)
        {
            return str?.Trim(trimChars);
        }

        public static string? TryTrimStart(this string? str)
        {
            return str?.TrimStart();
        }
        public static string? TryTrimStart(this string? str, params char[] trimChars)
        {
            return str?.TrimStart(trimChars);
        }



        /// <summary>
        /// If string is null or empty returns default text.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultText"></param>
        /// <returns></returns>
        public static string DefaultText(this string? str, string defaultText)
        {
            return !string.IsNullOrEmpty(str) ? str : defaultText;
        }

        //public static bool Contains(this string?str, string value, StringComparison comparisonType)
        //{
        //    return str.IndexOf(value, comparisonType) != -1;
        //}

        public static string? Add(this string? str, string separator, string? part)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (!string.IsNullOrEmpty(part))
                    return str + separator + part;
                return str;
            }
            return part;
        }

        public static string? AddLine(this string? str, string? part)
        {
            return Add(str, Environment.NewLine, part);
        }

        public static string[] Lines(this string? str)
        {
            return !string.IsNullOrEmpty(str)
                ? str.Split([Environment.NewLine], StringSplitOptions.None)
                : [];
        }

        public static string? Etc(this string? str, int max, string etcString = "(...)")
        {
            if (!string.IsNullOrEmpty(str) && str.Length > max)
                return str.Left(max - (!string.IsNullOrEmpty(etcString) ? etcString.Length : 0)) + etcString;
            return str;
        }

        public static string? FirstUpperInvariant(this string? str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return char.ToUpperInvariant(str[0]) + str[1..];
        }
        public static string? FirstUpper(this string? value)
        {
            if (value == null) { return value; }
            if (value.Length < 2) { return value.ToUpper(); }

            // Start with the first character.
            return value.Substring(0, 1).ToUpper() + value[1..];
        }


        public static string? NormalizeName(this string? key)
        {
            return key?.Normalize().SafeToUpperInvariant();
        }
        public static string? NormalizeKey(this string? key)
        {
            return key?.Normalize().ToUpperInvariant();
        }
        public static string? NormalizeTag(this string? tag)
        {
            return tag?.Normalize().RemoveWhiteSpace().RemoveSpecialChars()?.ToUpperInvariant();
        }

        public static string? RemoveWhiteSpace(this string? str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var len = str.Length;
            var src = str.ToCharArray();
            var dstIdx = 0;

            for (var i = 0; i < len; i++)
            {
                var ch = src[i];

                switch (ch)
                {
                    case '\u0020':
                    case '\u00A0':
                    case '\u1680':
                    case '\u2000':
                    case '\u2001':
                    case '\u2002':
                    case '\u2003':
                    case '\u2004':
                    case '\u2005':
                    case '\u2006':
                    case '\u2007':
                    case '\u2008':
                    case '\u2009':
                    case '\u200A':
                    case '\u202F':
                    case '\u205F':
                    case '\u3000':
                    case '\u2028':
                    case '\u2029':
                    case '\u0009':
                    case '\u000A':
                    case '\u000B':
                    case '\u000C':
                    case '\u000D':
                    case '\u0085':
                        continue;

                    default:
                        src[dstIdx++] = ch;
                        break;
                }
            }
            return new string(src, 0, dstIdx);
        }


        public static string? RemoveSpecialChars(this string? str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var len = str.Length;
            var src = str.ToCharArray();
            var dstIdx = 0;

            for (var i = 0; i < len; i++)
            {
                var ch = src[i];

                switch (ch)
                {
                    case '`':
                    case '-':
                    case '=':
                    case '\\':
                    case '~':
                    case '!':
                    case '@':
                    case '#':
                    case '$':
                    case '%':
                    case '^':
                    case '&':
                    case '*':
                    case '(':
                    case ')':
                    case '_':
                    case '+':
                    case '|':
                    case '[':
                    case ']':
                    case '{':
                    case '}':
                    case ';':
                    case '\'':
                    case ':':
                    case '"':
                    case ',':
                    case '.':
                    case '/':
                    case '<':
                    case '>':
                    case '?':
                        continue;

                    default:
                        src[dstIdx++] = ch;
                        break;
                }
            }
            return new string(src, 0, dstIdx);
        }

        /// <summary>
        /// Parses an bool from a String. Returns false value if input is invalid.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string? str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            return str.ToUpperInvariant() switch
            {
                "TRUE" or "Y" or "YES" or "1" or "ON" => true,
                _ => false,
            };
        }
        /// <summary>
        /// Parses an byte from a String. Returns default value if input is invalid.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte ToByte(this string? str, byte defaultValue = 0)
        {
            //str = str.IfNullEmpty();
            return byte.TryParse(str, out var result) ? result : defaultValue;
        }

        /// <summary>
        /// Parses an short from a String. Returns default value if input is invalid.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static short ToInt16(this string? str, short defaultValue = 0)
        {
            //str = str.IfNullEmpty();
            return short.TryParse(str, out var result) ? result : defaultValue;
        }
        /// <summary>
        ///  Parses an integer from a String. Returns default value if input is invalid.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt32(this string? str, int defaultValue = 0)
        {
            //str = str.IfNullEmpty();
            return int.TryParse(str, out var result) ? result : defaultValue;
        }

        /// <summary>
        /// Parses an long from a String. Returns default value if input is invalid.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToInt64(this string? str, long defaultValue = 0)
        {
            //str = str.IfNullEmpty();
            return long.TryParse(str, out var result) ? result : defaultValue;
        }

        /// <summary>
        /// Parses an ulong from a String. Returns default value if input is invalid.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ulong ToUInt64(this string? str, ulong defaultValue = 0)
        {
            return ulong.TryParse(str, out var result) ? result : defaultValue;
        }

        /// <summary>
        /// Parses an ulong from a Hex String. Returns default value if input is invalid.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ulong ToUInt64Hex(this string? str, ulong defaultValue = 0)
        {
            return ulong.TryParse(str, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var result) ? result : defaultValue;
        }



        /// <summary>
        /// იღებს ToDecimal-ს ტექსტიდან
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string? str, decimal defaultValue = decimal.Zero)
        {
            //str = str.IfNullEmpty();
            return decimal.TryParse(str, out var result) ? result : defaultValue;
        }
        /// <summary>
        /// იღებს Double-ს ტექსტიდან
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(this string? str, double defaultValue = 0D)
        {
            //str = str.IfNullEmpty();
            return double.TryParse(str, out var result) ? result : defaultValue;
        }

        /// <summary>
        /// Gets GUID value from string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string? str, Guid defaultValue = default)
        {
            //str = str.IfNullEmpty();
            return Guid.TryParse(str, out var result) ? result : defaultValue;
            //var result = Guid.Empty;
            //try
            //{
            //    result = new Guid(str);
            //}
            //catch { }
            //return result;
        }

        /// <summary>
        ///  Gets DateTime value from string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string? str, DateTime? defaultValue = null)
        {
            defaultValue ??= DateTime.MinValue;

            return DateTime.TryParse(str, out var result) ? result : defaultValue.Value;
        }

        public static DateTime ParseUniversalDateTime(this string? str, DateTime? defaultValue = null)
        {
            defaultValue ??= DateTime.MinValue;

            return DateTime.TryParseExact(str, DateTimeHelper.UniversalDateTimeFormat, null, DateTimeStyles.None, out var result) ? result : defaultValue.Value;
        }


        public static bool? ToNullableBoolean(this string? str)
        {
            str = str.EmptyIfNull().ToUpperInvariant();
            return str switch
            {
                "TRUE" or "Y" or "YES" or "1" or "ON" => true,
                "FALSE" or "N" or "NO" or "0" or "OFF" => false,
                _ => null,
            };
        }

        public static byte? ToNullableByte(this string? str)
        {
            return byte.TryParse(str, out var result) ? result : null;
        }
        public static short? ToNullableInt16(this string? str)
        {
            return short.TryParse(str, out var result) ? result : null;
        }
        public static int? ToNullableInt32(this string? str)
        {
            return int.TryParse(str, out var result) ? result : null;
        }
        public static long? ToNullableInt64(this string? str)
        {
            return long.TryParse(str, out var result) ? result : null;
        }
        public static float? ToNullableSingle(this string? str)
        {
            return float.TryParse(str, out var result) ? result : null;
        }
        public static double? ToNullableDouble(this string? str)
        {
            return double.TryParse(str, out var result) ? result : null;
        }
        public static decimal? ToNullableDecimal(this string? str)
        {
            return decimal.TryParse(str, out var result) ? result : null;
        }
        public static Guid? ToNullableGuid(this string? str)
        {
            return Guid.TryParse(str, out var result) ? result : null;
        }
        public static DateTime? ToNullableDateTime(this string? str)
        {
            return DateTime.TryParse(str, out var result) ? result : null;
        }



        /// <summary>
        /// Splits pascal case, so "FooBar" would become "Foo Bar"
        /// </summary>
        public static string? SplitPascalCase(this string? str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var retVal = new StringBuilder(str.Length + 5);

            for (var i = 0; i < str.Length; ++i)
            {
                var currentChar = str[i];
                if (char.IsUpper(currentChar))
                {
                    if (i > 1 && !char.IsUpper(str[i - 1])
                        || i + 1 < str.Length && !char.IsUpper(str[i + 1]))
                        retVal.Append(' ');
                }

                retVal.Append(currentChar);
            }

            return retVal.ToString().Trim();
        }
    }
}
