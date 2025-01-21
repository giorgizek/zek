using System.Globalization;
using System.Reflection;

namespace Zek.Utils
{
    public static class ConvertHelper
    {
        public static T ChangeType<T>(object value)
        {
            return (T)ChangeType(value, typeof(T));
        }
        public static object ChangeType(object value, Type type)
        {
            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return value != null ? Convert.ChangeType(value, type.GetGenericArguments()[0], CultureInfo.InvariantCulture) : null;
            }

            return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
        }

    }
}


