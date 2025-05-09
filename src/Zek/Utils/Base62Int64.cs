﻿namespace Zek.Utils
{
    public readonly struct Base62Int64
    {
        /// <summary>
        /// A read-only instance of the Base62Int64 struct whose value is guaranteed to be all zeroes i.e. equivalent
        /// to 0.
        /// </summary>
        public static readonly Base62Int64 Empty = new(0);

        public Base62Int64(string value)
        {
            _encodedString = value;
            _int64Value = Decode(value);
        }
        public Base62Int64(long value)
        {
            _encodedString = Encode(value);
            _int64Value = value;
        }


        private readonly long _int64Value;
        public long Int64Value => _int64Value;

        private readonly string _encodedString;
        public string Value => _encodedString;

        public override string ToString() => _encodedString;


        public override bool Equals(object? obj)
        {
            if (obj is Base62Int64 shortInt)
            {
                return _int64Value.Equals(shortInt._int64Value);
            }

            if (obj is long val)
            {
                return _int64Value.Equals(val);
            }

            if (obj is string str)
            {
                // Try a Base62Int64 string.
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
            var encoded = UrlShortener.Encode(value);//WebEncoders.Base64UrlEncode(BitConverter.GetBytes(value));
            return encoded;
        }

        public static long Decode(string value)
        {
            return UrlShortener.Decode(value);// BitConverter.ToInt64(WebEncoders.Base64UrlDecode(value));
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

        public static bool operator ==(Base62Int64 x, Base62Int64 y) => x._int64Value == y._int64Value;

        public static bool operator ==(Base62Int64 x, long y) => x._int64Value == y;

        public static bool operator ==(long x, Base62Int64 y) => y == x;

        public static bool operator !=(Base62Int64 x, Base62Int64 y) => !(x == y);

        public static bool operator !=(Base62Int64 x, long y) => !(x == y);

        public static bool operator !=(long x, Base62Int64 y) => !(x == y);

        public static implicit operator string(Base62Int64 shortInt) => shortInt._encodedString;

        public static implicit operator long(Base62Int64 shortInt) => shortInt._int64Value;


        /// <summary>
        /// Implicitly converts the string to a Base62Int64.
        /// </summary>
        public static implicit operator Base62Int64(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Empty;

            if (TryParse(value, out Base62Int64 shortInt))
                return shortInt;

            throw new FormatException();
        }


        /// <summary>
        /// Implicitly converts the <see cref="int"/> to a Base62Int64.
        /// </summary>
        public static implicit operator Base62Int64(long value)
        {
            return value == 0
                ? Empty
                : new Base62Int64(value);
        }

        #endregion



        /// <summary>
        /// Tries to parse the value as a <see cref="Base62Int64"/> or <see cref="long"/> string, and outputs an actual <see cref="Base62Int64"/> instance.
        /// 
        /// <para>The difference between TryParse and TryDecode:</para>
        /// <list type="number">
        ///     <item>
        ///         <term><see cref="TryParse(string, out Base62Int64)"/></term>
        ///         <description>Tries to parse as a <see cref="Base62Int64"/> before attempting parsing as a <see cref="long"/>, outputs the actual <see cref="Base62Int64"/> instance - this method.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TryParse(string, out long)"/></term>
        ///         <description>Tries to parse as a <see cref="Base62Int64"/> before attempting parsing as a <see cref="long"/>, outputs the underlying <see cref="long"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TryDecode(string, out long)"/></term>
        ///         <description>Tries to parse as a <see cref="Base62Int64"/> only, but outputs the result as a <see cref="long"/>.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="input">The Base62Int64 encoded string or string representation of a int.</param>
        /// <param name="result">A new <see cref="Base62Int64"/> instance from the parsed string.</param>
        /// <returns>A boolean indicating if the parse was successful.</returns>
        public static bool TryParse(string input, out Base62Int64 result)
        {
            // Try a Base62Int64 string.
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
        /// Tries to parse the value as a <see cref="Base62Int64"/> or <see cref="int"/> string, and outputs the underlying <see cref="int"/> value.
        ///
        /// <para>The difference between TryParse and TryDecode:</para>
        /// <list type="number">
        ///     <item>
        ///         <term><see cref="TryParse(string, out Base62Int64)"/></term>
        ///         <description>Tries to parse as a <see cref="Base62Int64"/> before attempting parsing as a <see cref="int"/>, outputs the actual <see cref="Base62Int64"/> instance.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TryParse(string, out int)"/></term>
        ///         <description>Tries to parse as a <see cref="Base62Int64"/> before attempting parsing as a <see cref="int"/>, outputs the underlying <see cref="int"/> - this method.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TryDecode(string, out int)"/></term>
        ///         <description>Tries to parse as a <see cref="Base62Int64"/> only, but outputs the result as a <see cref="int"/>.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="input">The Base62Int64 encoded string or string representation of a int.</param>
        /// <param name="result">A new <see cref="int"/> instance from the parsed string.</param>
        /// <returns>A boolean indicating if the parse was successful.</returns>
        public static bool TryParse(string? input, out long result)
        {
            // Try a Base62Int64 string.
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
