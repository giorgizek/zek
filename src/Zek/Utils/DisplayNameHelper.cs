using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Zek.Utils
{
    public class DisplayNameHelper
    {
        public static string GetDisplayNameForProperty(Type type, string propertyName)
        {
            //if (type == null)
            //    throw new ArgumentNullException(nameof(type));
            //if (string.IsNullOrEmpty(propertyName))
            //    throw new ArgumentException("The parameter \"propertyName\" is required.", nameof(propertyName));

            var property = type.GetRuntimeProperties().SingleOrDefault(prop => string.Equals(propertyName, prop.Name, StringComparison.OrdinalIgnoreCase));
            if (property == null)
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The property {0}.{1} could not be found", type.FullName, propertyName));

            return GetDisplayNameForProperty(property);
        }

        public static string GetDisplayNameForProperty(MemberInfo member)
        {
            var attribute = member.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().SingleOrDefault();
            return attribute != null ? attribute.GetName() : member.Name;
        }
    }
}
