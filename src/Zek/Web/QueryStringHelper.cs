using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Web;

namespace Zek.Web
{
    public static class QueryStringHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nvc"></param>
        /// <returns></returns>
        public static string ToQueryString(this NameValueCollection nvc)
        {
            return string.Join("&",
                nvc.AllKeys.Select(
                    key => $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(nvc[key])}"));
        }

        public static string ToQueryString(IDictionary<string, string?> dictionary)
        {
            return string.Join("&",
                dictionary.Select(item => $"{HttpUtility.UrlEncode(item.Key)}={HttpUtility.UrlEncode(item.Value)}"));
        }

        public static string? ToJsonString(object? value)
        {
            if (value == null)
                return null;

            var type = Nullable.GetUnderlyingType(value.GetType()) ?? value.GetType();

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    return (bool)value ? "true" : "false";

                case TypeCode.String:
                case TypeCode.Char:
                    return value.ToString();

                case TypeCode.DateTime:
                    return ((DateTime)value).ToString("O", CultureInfo.InvariantCulture);

                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return type.IsEnum
                        ? Convert.ToInt32(value).ToString(CultureInfo.InvariantCulture)
                        : Convert.ToString(value, CultureInfo.InvariantCulture);

                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return Convert.ToString(value, CultureInfo.InvariantCulture);

                default:
                    if (value is Guid g)
                        return g.ToString();

                    if (value is DateTimeOffset dto)
                        return dto.ToString("O", CultureInfo.InvariantCulture);

                    if (value is TimeSpan ts)
                        return ts.ToString("c", CultureInfo.InvariantCulture);

                    if (value is Enum e)
                        return ((int)value).ToString();

                    // fallback
                    return value.ToString();
            }
        }

        public static Dictionary<string, string?>? ToDictionary<T>(T obj)
            where T : class
        {
            if (obj is null)
                return null;

            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var result = new Dictionary<string, string?>();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj, null);
                var strValue = ToJsonString(value);
                if (strValue is null)
                    continue;
                result[prop.Name] = strValue;
            }

            return result;
        }
    }
}
