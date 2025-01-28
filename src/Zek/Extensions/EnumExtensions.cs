using JetBrains.Annotations;
using Zek.Utils;

namespace Zek.Extensions
{
    public static class EnumExtensions
    {
        //public static bool HasFlag(this Enum value, Enum flag)
        //{
        //    var val = Convert.ToInt32(flag);
        //    return (Convert.ToInt32(value) & val) == val;
        //}

        public static T AddFlags<T>(this Enum flags, params T[] flagsToAdd)
             where T : struct, Enum
        {
            try
            {
                if (flagsToAdd == null)
                    return (T)(object)flags;

                var result = (int)(object)flags;
                foreach (var flag in flagsToAdd)
                {
                    result |= (int)(object)flag;
                }

                return (T)(object)result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Could not append value from enumerated type '{typeof(T).Name}'.", ex);
            }
        }

        public static T DeleteFlags<T>(this Enum flags, params T[] flagsToDelete)
             where T : struct, Enum
        {
            try
            {
                if (flagsToDelete == null) return (T)(object)flags;

                var result = (int)(object)flags;
                foreach (var flag in flagsToDelete)
                {
                    result &= ~(int)(object)flag;
                }
                return (T)(object)result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Could not remove value from enumerated type '{typeof(T).Name}'.", ex);
            }
        }

        public static T ToggleFlag<T>(this Enum flags, T flagToToggle)
        {
            var result = (int)(object)flags ^ (int)(object)flagToToggle;
            return (T)(object)result;
        }

        public static string[] ToStringArray<T>(this T? flags)
            where T : struct, Enum
        {
            // If label is null, return an empty array
            if (!flags.HasValue)
            {
                return [];
            }

           return flags.Value.ToStringArray();
        }
        public static string[] ToStringArray<T>(this T flags)
            where T : struct, Enum
        {
            var result = new List<string>();
            // Iterate through all possible values of the enum and check if each is set
            foreach (Enum value in Enum.GetValues<T>())
            {
                if (flags.HasFlag(value))
                {
                    result.Add(value.ToString());
                }
            }

            // Return the list as an array
            return [.. result];
        }


        public static int ToInt32(this Enum value)
        {
            return Convert.ToInt32(value);
        }
        public static int? ToNullableInt32([CanBeNull] this Enum value)
        {
            if (value == null) return null;

            return Convert.ToInt32(value);
        }

        //public static long ToInt64(this Enum value)
        //{
        //    return Convert.ToInt64(value);
        //}

        public static string? GetDisplayName(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            return field == null ? null : DisplayNameHelper.GetDisplayNameForProperty(field);

            //var attributes = field.GetCustomAttributes(typeof(DisplayAttribute), true).ToArray();
            //return attributes.Length > 0 ? ((DisplayAttribute)attributes[0]).GetName() : value.ToString();
        }
    }
}