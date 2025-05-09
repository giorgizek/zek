﻿using System.Diagnostics;

namespace Zek.Utils
{
    /// <summary>
    /// A convenience wrapper struct for dealing with URL-safe Base64 encoded globally unique identifiers (GUID),
    /// making a shorter string value (22 vs 36 characters long).
    /// </summary>
    /// <remarks>
    /// What is URL-safe Base64? That's just a Base64 string with well known special characters replaced (/, +)
    /// or removed (==).
    /// </remarks>
    [DebuggerDisplay("{" + nameof(Value) + "}")]
    public readonly struct ShortGuid
    {
        /// <summary>
        /// A read-only instance of the ShortGuid struct whose value is guaranteed to be all zeroes i.e. equivalent
        /// to <see cref="Guid.Empty"/>.
        /// </summary>
        public static readonly ShortGuid Empty = new(Guid.Empty);

        /// <summary>
        /// Creates a new instance with the given URL-safe Base64 encoded string.
        /// See also <seealso cref="ShortGuid.TryParse(string, out ShortGuid)"/> which will try to coerce the
        /// the value from URL-safe Base64 or normal Guid string
        /// </summary>
        /// <param name="value">A ShortGuid encoded string e.g. URL-safe Base64.</param>
        public ShortGuid(string value)
        {
            _encodedString = value;
            _guid = Decode(value);
        }

        /// <summary>
        /// Creates a new instance with the given <see cref="System.Guid"/>.
        /// </summary>
        /// <param name="guid">The <see cref="System.Guid"/> to encode.</param>
        public ShortGuid(Guid guid)
        {
            _encodedString = Encode(guid);
            _guid = guid;
        }

        private readonly Guid _guid;
        /// <summary>
        /// Gets the underlying <see cref="System.Guid"/> for the encoded ShortGuid.
        /// </summary>
        public Guid Guid => _guid;

        private readonly string _encodedString;
        /// <summary>
        /// Gets the encoded string value of the <see cref="Guid"/> i.e. a URL-safe Base64 string.
        /// </summary>
        public string Value => _encodedString;

        /// <summary>
        /// Returns the encoded URL-safe Base64 string.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => _encodedString;

        /// <summary>
        /// Returns a value indicating whether this instance and a specified object represent the same type and value.
        /// Compares for equality against other string, Guid and ShortGuid types.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is ShortGuid shortGuid)
            {
                return _guid.Equals(shortGuid._guid);
            }

            if (obj is Guid guid)
            {
                return _guid.Equals(guid);
            }

            if (obj is string str)
            {
                // Try a ShortGuid string.
                if (TryDecode(str, out guid))
                    return _guid.Equals(guid);

                // Try a result string.
                if (Guid.TryParse(str, out guid))
                    return _guid.Equals(guid);
            }

            return false;
        }

        /// <summary>
        /// Returns the hash code for the underlying <see cref="System.Guid"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => _guid.GetHashCode();

        /// <summary>
        /// Initialises a new instance of the ShortGuid using <see cref="Guid.NewGuid()"/>.
        /// </summary>
        /// <returns></returns>
        public static ShortGuid NewGuid() => new(Guid.NewGuid());

        /// <summary>
        /// Encodes the given value as an encoded ShortGuid string. The encoding is similar to
        /// Base64, with some non-URL safe characters replaced, and padding removed.
        /// </summary>
        /// <param name="value">Any valid <see cref="System.Guid"/> string.</param>
        /// <returns>A 22 character ShortGuid URL-safe Base64 string.</returns>
        public static string Encode(string value)
        {
            var guid = new Guid(value);
            return Encode(guid);
        }

        /// <summary>
        /// Encodes the given <see cref="System.Guid"/> as an encoded ShortGuid string. The encoding is similar to
        /// Base64, with some non-URL safe characters replaced, and padding removed.
        /// </summary>
        /// <param name="guid">The <see cref="System.Guid"/> to encode.</param>
        /// <returns>A 22 character ShortGuid URL-safe Base64 string.</returns>
        public static string Encode(Guid guid)
        {
            var encoded = Convert.ToBase64String(guid.ToByteArray());

            encoded = encoded
                .Replace("/", "_")
                .Replace("+", "-");

            return encoded.Substring(0, 22);//remove ==
        }

        /// <summary>
        /// Decodes the given value to a <see cref="System.Guid"/>.
        /// <para>See also <seealso cref="TryDecode(string, out Guid)"/> or <seealso cref="TryParse(string, out Guid)"/>.</para>
        /// </summary>
        /// <param name="value">The ShortGuid encoded string to decode.</param>
        /// <returns>A new <see cref="System.Guid"/> instance from the parsed string.</returns>
        public static Guid Decode(string value)
        {
            value = value
                .Replace("_", "/")
                .Replace("-", "+");

            var blob = Convert.FromBase64String(value + "==");

            return new Guid(blob);
        }

        /// <summary>
        /// Attempts to decode the given value to a <see cref="System.Guid"/>.
        ///
        /// <para>The difference between TryParse and TryDecode:</para>
        /// <list type="number">
        ///     <item>
        ///         <term><see cref="TryParse(string, out ShortGuid)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortGuid"/> before attempting parsing as a <see cref="System.Guid"/>, outputs the actual <see cref="ShortGuid"/> instance.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TryParse(string, out Guid)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortGuid"/> before attempting parsing as a <see cref="System.Guid"/>, outputs the underlying <see cref="System.Guid"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TryDecode(string, out Guid)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortGuid"/> only, but outputs the result as a <see cref="System.Guid"/> - this method.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="input">The ShortGuid encoded string to decode.</param>
        /// <param name="result">A new <see cref="System.Guid"/> instance from the parsed string.</param>
        /// <returns>A boolean indicating if the decode was successful.</returns>
        public static bool TryDecode(string? input, out Guid result)
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
                result = Guid.Empty;
                return false;
            }
        }

        #region Operators

        /// <summary>
        /// Determines if both ShortGuid instances have the same underlying <see cref="System.Guid"/> value.
        /// </summary>
        public static bool operator ==(ShortGuid x, ShortGuid y)
        {
            //if (ReferenceEquals(x, null))
            //    return ReferenceEquals(y, null);

            return x._guid == y._guid;
        }

        /// <summary>
        /// Determines if both instances have the same underlying <see cref="System.Guid"/> value.
        /// </summary>
        public static bool operator ==(ShortGuid x, Guid y)
        {
            //if (ReferenceEquals(x, null))
            //    return ReferenceEquals(y, null);

            return x._guid == y;
        }

        /// <summary>
        /// Determines if both instances have the same underlying <see cref="System.Guid"/> value.
        /// </summary>
        public static bool operator ==(Guid x, ShortGuid y) => y == x; // NB: order of arguments

        /// <summary>
        /// Determines if both ShortGuid instances do not have the same underlying <see cref="System.Guid"/> value.
        /// </summary>
        public static bool operator !=(ShortGuid x, ShortGuid y) => !(x == y);

        /// <summary>
        /// Determines if both instances do not have the same underlying <see cref="System.Guid"/> value.
        /// </summary>
        public static bool operator !=(ShortGuid x, Guid y) => !(x == y);

        /// <summary>
        /// Determines if both instances do not have the same underlying <see cref="System.Guid"/> value.
        /// </summary>
        public static bool operator !=(Guid x, ShortGuid y) => !(x == y);

        /// <summary>
        /// Implicitly converts the ShortGuid to its string equivalent.
        /// </summary>
        public static implicit operator string(ShortGuid shortGuid) => shortGuid._encodedString;

        /// <summary>
        /// Implicitly converts the ShortGuid to its <see cref="System.Guid"/> equivalent.
        /// </summary>
        public static implicit operator Guid(ShortGuid shortGuid) => shortGuid._guid;

        /// <summary>
        /// Implicitly converts the string to a ShortGuid.
        /// </summary>
        public static implicit operator ShortGuid(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Empty;

            if (TryParse(value, out ShortGuid shortGuid))
                return shortGuid;

            throw new FormatException("ShortGuid should contain 22 Base64 characters or Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
        }

        /// <summary>
        /// Implicitly converts the <see cref="System.Guid"/> to a ShortGuid.
        /// </summary>
        public static implicit operator ShortGuid(Guid guid)
        {
            return guid == Guid.Empty
                ? Empty
                : new ShortGuid(guid);
        }

        #endregion

        /// <summary>
        /// Tries to parse the value as a <see cref="ShortGuid"/> or <see cref="System.Guid"/> string, and outputs an actual <see cref="ShortGuid"/> instance.
        /// 
        /// <para>The difference between TryParse and TryDecode:</para>
        /// <list type="number">
        ///     <item>
        ///         <term><see cref="TryParse(string, out ShortGuid)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortGuid"/> before attempting parsing as a <see cref="System.Guid"/>, outputs the actual <see cref="ShortGuid"/> instance - this method.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TryParse(string, out Guid)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortGuid"/> before attempting parsing as a <see cref="System.Guid"/>, outputs the underlying <see cref="System.Guid"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TryDecode(string, out Guid)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortGuid"/> only, but outputs the result as a <see cref="System.Guid"/>.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="input">The ShortGuid encoded string or string representation of a Guid.</param>
        /// <param name="result">A new <see cref="ShortGuid"/> instance from the parsed string.</param>
        /// <returns>A boolean indicating if the parse was successful.</returns>
        public static bool TryParse(string? input, out ShortGuid result)
        {
            // Try a ShortGuid string.
            if (TryDecode(input, out var guid))
            {
                result = guid;
                return true;
            }

            // Try a Guid string.
            if (Guid.TryParse(input, out guid))
            {
                result = guid;
                return true;
            }

            result = Empty;
            return false;
        }

        /// <summary>
        /// Tries to parse the value as a <see cref="ShortGuid"/> or <see cref="System.Guid"/> string, and outputs the underlying <see cref="Guid"/> value.
        ///
        /// <para>The difference between TryParse and TryDecode:</para>
        /// <list type="number">
        ///     <item>
        ///         <term><see cref="TryParse(string, out ShortGuid)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortGuid"/> before attempting parsing as a <see cref="System.Guid"/>, outputs the actual <see cref="ShortGuid"/> instance.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TryParse(string, out Guid)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortGuid"/> before attempting parsing as a <see cref="System.Guid"/>, outputs the underlying <see cref="System.Guid"/> - this method.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TryDecode(string, out Guid)"/></term>
        ///         <description>Tries to parse as a <see cref="ShortGuid"/> only, but outputs the result as a <see cref="System.Guid"/>.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="input">The ShortGuid encoded string or string representation of a Guid.</param>
        /// <param name="result">A new <see cref="System.Guid"/> instance from the parsed string.</param>
        /// <returns>A boolean indicating if the parse was successful.</returns>
        public static bool TryParse(string input, out Guid result)
        {
            // Try a ShortGuid string.
            if (TryDecode(input, out result))
                return true;

            // Try a Guid string.
            if (Guid.TryParse(input, out result))
                return true;

            result = Guid.Empty;
            return false;
        }
    }
}
