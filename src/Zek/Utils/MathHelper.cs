using System.Globalization;

namespace Zek.Utils
{
    public static class MathHelper
    {
        public static DateTime? Min(DateTime? a, DateTime? b)
        {
            if (a == null) return b;
            if (b == null) return a;

            return Min(a.Value, b.Value);
        }
        public static DateTime? Max(DateTime? a, DateTime? b)
        {
            if (a == null) return b;
            if (b == null) return a;

            return Max(a.Value, b.Value);
        }
        public static DateTime Min(DateTime a, DateTime b)
        {
            return a <= b ? a : b;
        }
        public static DateTime Max(DateTime a, DateTime b)
        {
            return a >= b ? a : b;
        }


        /// <summary>
        /// Returns value between min and max. if out of range returns min/max
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static decimal Clamp(decimal value, decimal min, decimal max)
        {
            return Math.Max(min, Math.Min(max, value));
        }



        /// <summary>
        /// Converts the specified value to its equivalent string representation.
        /// </summary>
        /// <param name="value">value - A value containing a number to convert</param>
        /// <param name="decimals">decimals - A value containing a decimals.</param>
        /// <returns>Returns or does not return the string representation of the integer value.</returns>
        public static string NumToStr(decimal value, int decimals = 0)
        {
            if (decimals > 6)
                throw new ArgumentOutOfRangeException(nameof(decimals), decimals, "Looks up a localized string similar to Maximum number is 6.");

            var str = NumToStrHelper.NumToStr((long)value);

            if (decimals > 0)
            {
                value = Math.Round(Math.Abs(value) % 1, decimals);//აქ უკვე value მხოლოდ წილადი რიცხვი ხდება 123.456 => 0.456

                //decimal fraction = Math.Abs(value - Math.Truncate(value));
                //int val = Convert.ToInt32((double)fraction * Math.Pow(10, fraction.ToString().Length - 2));

                var length = FractionLength(value);
                if (length > 0)
                {
                    string fraction = length switch
                    {
                        1 => "მეათედი",
                        2 => "მეასედი",
                        3 => "მეათასედი",
                        4 => "მეათიათასედი",
                        5 => "მეასიათასედი",
                        6 => "მემილიონედი",
                        _ => throw new Exception("fraction არის 6-ზე მეტი."),
                    };
                    str += $" მთელი {FracToInt32(value)} {fraction}";
                }
            }
            return str;
        }


        /// <summary>
        /// რიცხვიდან წილადის ამოღება 123.456 => 456
        /// </summary>
        /// <param name="value">რიცხვი რომლის წილადის ამოღებაც გვინდა.</param>
        /// <returns>წილადის მნიშვნელობა.</returns>
        public static int FracToInt32(decimal value)
        {
            var frac = Math.Abs(value) % 1;
            var fraction = frac.ToString(CultureInfo.InvariantCulture);
            return frac != decimal.Zero ? Convert.ToInt32(fraction.Substring(2)) : 0;
        }

        /// <summary>
        /// რიცხვიდან წილადის სიგრძის ამოღება.
        /// </summary>
        /// <param name="value">რიცხვი რომლის წილადის სიგრძეც გვაინტერესებს.</param>
        /// <returns>აბრუნებს სიგრძეს.</returns>
        public static int FractionLength(decimal value)
        {
            var frac = Math.Abs(value) % 1;

            return frac != decimal.Zero
                ? frac.ToString(CultureInfo.InvariantCulture).Length - 2
                : 0;
        }



        public static decimal Round(decimal value, RoundType roundType)
        {
            switch (roundType)
            {
                case RoundType.TwoDecimal:
                    return Math.Round(value, 2);

                case RoundType.ToInteger:
                    return Math.Round(value, 0);

                default:
                    return value;
            }
        }

        public static decimal? Round(decimal? d, int decimals)
        {
            if (d == null)
                return null;

            return Math.Round(d.Value, decimals);
        }




        public static decimal Gross(decimal netValue, decimal percent)
        {
            return netValue / (1m - percent / 100m);
        }
        public static decimal Net(decimal grossValue, decimal percent)
        {
            return grossValue * (1m - percent / 100m);
        }


        //public static decimal RemoveAddedPercent(decimal value, decimal percent)
        //{
        //    return value / (1m + percent / 100m);
        //}

        //public static decimal AddPercent(decimal value, decimal percent)
        //{
        //    return value * (1m + percent / 100m);
        //}

        /// <summary>
        /// GetPercent(200, 220) = 10;
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static decimal GetPercent(decimal val1, decimal val2)
        {
            return val2 * 100m / val1;
        }
    }
}
