namespace Zek.Extensions
{
    public static class StructExtensions
    {
        public static T? AsNullable<T>(this T instance) where T : struct
        {
            return instance;
        }

        public static T? NullIfDefault<T>(this Enum value, T defaultValue) where T : struct, IConvertible, IComparable, IFormattable
        {
            if (value == null)
                return null;

            return Convert.ToInt32(value) == Convert.ToInt32(defaultValue) ? null : (T)(object)value;
        }



        public static T? NullIfDefault<T>(this T? value, T defaultValue = default) where T : struct
        {
            if (value == null)
                return null;

            return EqualityComparer<T>.Default.Equals(defaultValue, value.Value) ? null : value;
        }

        //public static T DefaultIfNull<T>(this T? value, T defaultValue = default(T)) where T : struct
        //{
        //    return value ?? defaultValue;
        //}
    }
}
