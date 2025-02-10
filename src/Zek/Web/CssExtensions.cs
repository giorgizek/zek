using System.Globalization;

namespace Zek.Web
{
    public static class CssHelper
    {
        public static string ToCssValue(decimal value)
        {
            return value.ToString("G15", NumberFormatInfo.InvariantInfo);
        }

        public static string ToCssValue(double value)
        {
            return value.ToString("G15", NumberFormatInfo.InvariantInfo);
        }

        public static string ToCssValue(float value)
        {
            return value.ToString("G15", NumberFormatInfo.InvariantInfo);
        }
    }
}
