using Microsoft.AspNetCore.WebUtilities;

namespace Zek.Utils
{
    public readonly struct ShortInt32
    {
        /// <summary>
        /// A read-only instance of the ShortInt32 struct whose value is guaranteed to be all zeroes i.e. equivalent
        /// to 0.
        /// </summary>
        public static readonly ShortInt32 Empty = new(0);

        public ShortInt32(string value)
        {
            _encodedString = value;
            _int32Value = Decode(value);
        }
        public ShortInt32(int value)
        {
            _encodedString = Encode(value);
            this._int32Value = value;
        }


        private readonly int _int32Value;
        public int Int32Value => _int32Value;

        private readonly string _encodedString;
        public string Value => _encodedString;

        public override string ToString() => _encodedString;


        public override bool Equals(object? obj)
        {
            if (obj is ShortInt32 shortInt)
            {
                return _int32Value.Equals(shortInt._int32Value);
            }

            if (obj is int val)
            {
                return _int32Value.Equals(val);
            }

            if (obj is string str)
            {
                // Try a ShortInt32 string.
                if (TryDecode(str, out val))
                    return _int32Value.Equals(val);

                // Try a result string.
                if (int.TryParse(str, out val))
                    return _int32Value.Equals(val);
            }

            return false;

        }
        public override int GetHashCode() => _int32Value.GetHashCode();



        public static string Encode(int value)
        {
            var encoded = WebEncoders.Base64UrlEncode(BitConverter.GetBytes(value));
            return encoded;
        }

        public static int Decode(string value)
        {
            return BitConverter.ToInt32(WebEncoders.Base64UrlDecode(value));
        }

        public static bool TryDecode(string? input, out int result)
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

        public static bool operator ==(ShortInt32 x, ShortInt32 y)
        {
            return x._int32Value == y._int32Value;
        }

        public static bool operator ==(ShortInt32 x, int y)
        {
            return x._int32Value == y;
        }

        public static bool operator ==(int x, ShortInt32 y) => y == x;

        public static bool operator !=(ShortInt32 x, ShortInt32 y) => !(x == y);

        public static bool operator !=(ShortInt32 x, int y) => !(x == y);

        public static bool operator !=(int x, ShortInt32 y) => !(x == y);

        public static implicit operator string(ShortInt32 shortInt) => shortInt._encodedString;

        public static implicit operator int(ShortInt32 shortInt) => shortInt._int32Value;


        /// <summary>
        /// Implicitly converts the string to a ShortInt32.
        /// </summary>
        public static implicit operator ShortInt32(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Empty;

            if (TryParse(value, out ShortInt32 shortInt))
                return shortInt;

            throw new FormatException("ShortInt32 should contain 22 Base64 characters or int should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
        }


        /// <summary>
        /// Implicitly converts the <see cref="int"/> to a ShortInt32.
        /// </summary>
        public static implicit operator ShortInt32(int value)
        {
            return value == 0
                ? Empty
                : new ShortInt32(value);
        }

        #endregion



        /// <summary>
        /// Tries to parse the value as a <see cref="ShortInt32"/> or <see cref="int"/> string, and outputs an actual <see cref="ShortInt32"/> instance.
        /// 
        /// <para>The difference between TryParse and TryDecode:</para>
        /// <list type="number">
        ///     <item>
        ///         <term><see cref="TryParse(string, out ShortInt32)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortInt32"/> before attempting parsing as a <see cref="int"/>, outputs the actual <see cref="ShortInt32"/> instance - this method.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TryParse(string, out int)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortInt32"/> before attempting parsing as a <see cref="int"/>, outputs the underlying <see cref="int"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TryDecode(string, out int)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortInt32"/> only, but outputs the result as a <see cref="int"/>.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="input">The ShortInt32 encoded string or string representation of a int.</param>
        /// <param name="result">A new <see cref="ShortInt32"/> instance from the parsed string.</param>
        /// <returns>A boolean indicating if the parse was successful.</returns>
        public static bool TryParse(string input, out ShortInt32 result)
        {
            // Try a ShortInt32 string.
            if (TryDecode(input, out var val))
            {
                result = val;
                return true;
            }

            // Try a int string.
            if (int.TryParse(input, out val))
            {
                result = val;
                return true;
            }

            result = Empty;
            return false;
        }



        /// <summary>
        /// Tries to parse the value as a <see cref="ShortInt32"/> or <see cref="int"/> string, and outputs the underlying <see cref="int"/> value.
        ///
        /// <para>The difference between TryParse and TryDecode:</para>
        /// <list type="number">
        ///     <item>
        ///         <term><see cref="TryParse(string, out ShortInt32)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortInt32"/> before attempting parsing as a <see cref="int"/>, outputs the actual <see cref="ShortInt32"/> instance.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TryParse(string, out int)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortInt32"/> before attempting parsing as a <see cref="int"/>, outputs the underlying <see cref="int"/> - this method.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TryDecode(string, out int)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortInt32"/> only, but outputs the result as a <see cref="int"/>.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="input">The ShortInt32 encoded string or string representation of a int.</param>
        /// <param name="result">A new <see cref="int"/> instance from the parsed string.</param>
        /// <returns>A boolean indicating if the parse was successful.</returns>
        public static bool TryParse(string input, out int result)
        {
            // Try a ShortInt32 string.
            if (TryDecode(input, out result))
                return true;

            // Try a int string.
            if (int.TryParse(input, out result))
                return true;

            result = 0;
            return false;
        }
    }
}
