using Microsoft.AspNetCore.WebUtilities;

namespace Zek.Utils
{
    public readonly struct ShortInt64
    {
        /// <summary>
        /// A read-only instance of the ShortInt64 struct whose value is guaranteed to be all zeroes i.e. equivalent
        /// to 0.
        /// </summary>
        public static readonly ShortInt64 Empty = new(0);

        public ShortInt64(string value)
        {
            _encodedString = value;
            _int64Value = Decode(value);
        }
        public ShortInt64(long value)
        {
            _encodedString = Encode(value);
            this._int64Value = value;
        }


        private readonly long _int64Value;
        public long Int64Value => _int64Value;

        private readonly string _encodedString;
        public string Value => _encodedString;

        public override string ToString() => _encodedString;


        public override bool Equals(object? obj)
        {
            if (obj is ShortInt64 shortInt)
            {
                return _int64Value.Equals(shortInt._int64Value);
            }

            if (obj is long val)
            {
                return _int64Value.Equals(val);
            }

            if (obj is string str)
            {
                // Try a ShortInt64 string.
                if (TryDecode(str, out val))
                    return _int64Value.Equals(val);

                // Try a result string.
                if (long.TryParse(str, out val))
                    return _int64Value.Equals(val);
            }

            return false;

        }
        public override int GetHashCode() => _int64Value.GetHashCode();



        public static string Encode(long value)
        {
            var encoded = WebEncoders.Base64UrlEncode(BitConverter.GetBytes(value));
            return encoded;
        }

        public static long Decode(string value)
        {
            return BitConverter.ToInt64(WebEncoders.Base64UrlDecode(value));
        }

        public static bool TryDecode(string? input, out long result)
        {
            if (input == null)
            {
                result = default;
                return false;
            }

            input = input.Trim();
            if (input.Length == 0)
            {
                result = default;
                return false;
            }

            try
            {
                result = Decode(input);
                return true;
            }
            catch
            {
                result = 0;
                return false;
            }
        }





        #region Operators

        public static bool operator ==(ShortInt64 x, ShortInt64 y)
        {
            return x._int64Value == y._int64Value;
        }

        public static bool operator ==(ShortInt64 x, long y)
        {
            return x._int64Value == y;
        }

        public static bool operator ==(long x, ShortInt64 y) => y == x;

        public static bool operator !=(ShortInt64 x, ShortInt64 y) => !(x == y);

        public static bool operator !=(ShortInt64 x, long y) => !(x == y);

        public static bool operator !=(long x, ShortInt64 y) => !(x == y);

        public static implicit operator string(ShortInt64 shortInt) => shortInt._encodedString;

        public static implicit operator long(ShortInt64 shortInt) => shortInt._int64Value;


        /// <summary>
        /// Implicitly converts the string to a ShortInt64.
        /// </summary>
        public static implicit operator ShortInt64(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Empty;

            if (TryParse(value, out ShortInt64 shortInt))
                return shortInt;

            throw new FormatException("ShortInt64 should contain 22 Base64 characters or long should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
        }


        /// <summary>
        /// Implicitly converts the <see cref="long"/> to a ShortInt64.
        /// </summary>
        public static implicit operator ShortInt64(long value)
        {
            return value == 0
                ? Empty
                : new ShortInt64(value);
        }

        #endregion



        /// <summary>
        /// Tries to parse the value as a <see cref="ShortInt64"/> or <see cref="long"/> string, and outputs an actual <see cref="ShortInt64"/> instance.
        /// 
        /// <para>The difference between TryParse and TryDecode:</para>
        /// <list type="number">
        ///     <item>
        ///         <term><see cref="TryParse(string, out ShortInt64)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortInt64"/> before attempting parsing as a <see cref="long"/>, outputs the actual <see cref="ShortInt64"/> instance - this method.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TryParse(string, out long)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortInt64"/> before attempting parsing as a <see cref="long"/>, outputs the underlying <see cref="long"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TryDecode(string, out long)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortInt64"/> only, but outputs the result as a <see cref="long"/>.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="input">The ShortInt64 encoded string or string representation of a int.</param>
        /// <param name="result">A new <see cref="ShortInt64"/> instance from the parsed string.</param>
        /// <returns>A boolean indicating if the parse was successful.</returns>
        public static bool TryParse(string input, out ShortInt64 result)
        {
            // Try a ShortInt64 string.
            if (TryDecode(input, out var val))
            {
                result = val;
                return true;
            }

            // Try a long string.
            if (long.TryParse(input, out val))
            {
                result = val;
                return true;
            }

            result = Empty;
            return false;
        }



        /// <summary>
        /// Tries to parse the value as a <see cref="ShortInt64"/> or <see cref="long"/> string, and outputs the underlying <see cref="long"/> value.
        ///
        /// <para>The difference between TryParse and TryDecode:</para>
        /// <list type="number">
        ///     <item>
        ///         <term><see cref="TryParse(string, out ShortInt64)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortInt64"/> before attempting parsing as a <see cref="long"/>, outputs the actual <see cref="ShortInt64"/> instance.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TryParse(string, out long)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortInt64"/> before attempting parsing as a <see cref="long"/>, outputs the underlying <see cref="long"/> - this method.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TryDecode(string, out long)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortInt64"/> only, but outputs the result as a <see cref="long"/>.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="input">The ShortInt64 encoded string or string representation of a int.</param>
        /// <param name="result">A new <see cref="int"/> instance from the parsed string.</param>
        /// <returns>A boolean indicating if the parse was successful.</returns>
        public static bool TryParse(string input, out long result)
        {
            // Try a ShortInt64 string.
            if (TryDecode(input, out result))
                return true;

            // Try a long string.
            if (long.TryParse(input, out result))
                return true;

            result = 0;
            return false;
        }
    }
}
