using System.Globalization;

namespace Zek.Web
{
    public static class CssHelper
    {
        public static string ToCssValue(this decimal value)
        {
            return value.ToString("G15", NumberFormatInfo.InvariantInfo);
        }

        public static string ToCssValue(this double value)
        {
            return value.ToString("G15", NumberFormatInfo.InvariantInfo);
        }

        public static string ToCssValue(this float value)
        {
            return value.ToString("G15", NumberFormatInfo.InvariantInfo);
        }
    }
}
