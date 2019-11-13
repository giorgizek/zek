using System;

namespace Zek.Extensions
{
    public static class NullableExtensions
    {
        public static bool IsEquals<T>(this T? first, T? second)
            where T : struct, IEquatable<T>
        {
            // if one is null, the other is not, then obviously it's not equal
            if (first.HasValue != second.HasValue)
                return false;

            // Both either null or not null - if one is null, then they're equal
            if (first.HasValue == false)
                return true;

            // Compare by calling the IEquatable implementation
            return first.Value.Equals(second);
        }

        public static bool IsEquals<T>(this T? first, T second)
           where T : struct, IEquatable<T>
        {
            // If first is null, then obviously not equal
            return first?.Equals(second) ?? false;
        }
    }
}
