using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Web;

namespace Zek.Web
{
    public static class QueryStringHelper
    {
        /// <summary>
        /// Converts a NameValueCollection to a URL-encoded query string.
        /// </summary>
        /// <param name="nvc">The NameValueCollection to convert</param>
        /// <returns>A URL-encoded query string (without leading '?')</returns>
        /// <example>
        /// var nvc = new NameValueCollection { { "name", "John Doe" }, { "age", "30" } };
        /// var query = nvc.ToQueryString(); // Returns: "name=John+Doe&age=30"
        /// </example>
        public static string ToQueryString(this NameValueCollection nvc)
        {
            return string.Join("&",
                nvc.AllKeys.Select(
                    key => $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(nvc[key])}"));
        }

        /// <summary>
        /// Converts a dictionary to a URL-encoded query string.
        /// </summary>
        /// <param name="dictionary">The dictionary to convert (keys and values will be URL-encoded)</param>
        /// <returns>A URL-encoded query string (without leading '?')</returns>
        /// <example>
        /// var dict = new Dictionary&lt;string, string&gt; { { "search", "hello world" }, { "page", "1" } };
        /// var query = UrlQueryMerger.ToQueryString(dict); // Returns: "search=hello+world&page=1"
        /// </example>
        public static string ToQueryString(IDictionary<string, string?> dictionary)
        {
            return string.Join("&",
                dictionary.Select(item => $"{HttpUtility.UrlEncode(item.Key)}={HttpUtility.UrlEncode(item.Value)}"));
        }

        /// <summary>
        /// Converts a value to its string representation suitable for query parameters.
        /// Handles nullable types, enums, dates, and numeric types with invariant culture.
        /// </summary>
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
                        return ((int)value).ToString(CultureInfo.InvariantCulture);

                    // fallback
                    return value.ToString();
            }
        }


        /// <summary>
        /// Converts an object's properties to a dictionary with string values.
        /// Null properties are excluded from the result.
        /// </summary>
        /// <typeparam name="T">The type of object to convert</typeparam>
        /// <param name="obj">The object to convert</param>
        /// <returns>Dictionary of property names and string values, or null if obj is null</returns>
        public static Dictionary<string, string?>? ToDictionary<T>(T obj)
            where T : class
        {
            if (obj is null)
                return null;

            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var result = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
            foreach (var prop in properties)
            {
                // Skip properties that can't be read
                if (!prop.CanRead)
                    continue;

                var value = prop.GetValue(obj);
                var stringValue = ToJsonString(value);
                if (stringValue is null)
                    continue;
                result[prop.Name] = stringValue;
            }

            return result;
        }


       
    }
}
