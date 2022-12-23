using System;
using System.Text;

namespace Zek.Utils
{
    public partial class Func
    {
        public class En
        {
            private static readonly string[] MonthsAbbr = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec" };
            public static string DateToStrAbbr(DateTime date, bool uppercase = false)
            {
                var builder = new StringBuilder($"{date.Day} {MonthsAbbr[date.Month - 1]} {date.Year}");
                if (uppercase)
                {
                    builder[0] = char.ToUpper(builder[0]);
                }
                return builder.ToString();
            }



            private static readonly string[] Months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            private static readonly string[] Tens = { "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
            private static readonly string[] Units = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

            private static void AddRank(StringBuilder sb, ref long rank, ref long value, string unit)
            {
                var num = value / rank;
                if (num > 0L)
                {
                    var num2 = num / 100L;
                    var num3 = (num / 10L) % 10L;
                    var num4 = num % 10L;
                    if (num3 == 1L)
                    {
                        num3 = 0L;
                        num4 = num % 100L;
                    }
                    if (sb.Length > 0)
                    {
                        if (num2 > 0L)
                        {
                            sb.Append(" ");
                        }
                        else if ((num3 + num4) > 0L)
                        {
                            sb.Append(" and ");
                        }
                    }
                    if (num2 > 0L)
                    {
                        AddUnits(sb, num2);
                        sb.Append(" hundred");
                        if ((num3 + num4) > 0L)
                        {
                            sb.Append(" and ");
                        }
                    }
                    if (num3 > 0L)
                    {
                        AddTens(sb, num3);
                        if (num4 > 0L)
                        {
                            sb.Append("-");
                        }
                    }
                    if (num4 > 0L)
                    {
                        AddUnits(sb, num4);
                    }
                    sb.Append(" ");
                    sb.Append(unit);
                    value = value % rank;
                }
                rank /= 1000L;
            }

            private static void AddTens(StringBuilder sb, long value)
            {
                if (value != 0)
                {
                    sb.Append(Tens[value - 1L]);
                }
            }

            private static void AddUnits(StringBuilder sb, long value)
            {
                if (value != 0)
                {
                    sb.Append(Units[value - 1L]);
                }
            }

            public static string CurrToStr(decimal value) => CurrToStr(value, true, true);

            public static string CurrToStr(double value) => CurrToStr((decimal)value);

            public static string CurrToStr(long value) => CurrToStr((decimal)value);

            public static string CurrToStr(decimal value, bool showCents) => CurrToStr(value, true, showCents);

            public static string CurrToStr(double value, bool showCents) => CurrToStr((decimal)value, showCents);

            public static string CurrToStr(long value, bool showCents) => CurrToStr((decimal)value, showCents);

            public static string CurrToStr(double value, bool uppercase, bool cents) => CurrToStr((decimal)value, uppercase, cents);

            public static string CurrToStr(long value, bool uppercase, bool cents) => CurrToStr((decimal)value, uppercase, cents);

            public static string CurrToStr(decimal value, bool uppercase, bool showCents, string dollars = "dollar/dollars", string cents = "cent/cents")
            {
                var d = Math.Truncate(value);
                long num2;
                //if (StiOptions.Engine.UseRoundForToCurrencyWordsFunctions)//UseRoundForToCurrencyWordsFunctions = true;
                //{
                num2 = (long)Math.Round((value - ((long)d)) * 100M);
                if (num2 > 99L)
                {
                    num2 = 0L;
                    d++;
                }
                //}
                //else
                //{
                //    num2 = (long)((value - ((long)d)) * 100M);
                //}
                var str = NumToStr((long)d, uppercase);
                if (value == decimal.Zero)
                {
                    str = str + " ";
                }
                if (!str.EndsWith(" "))
                {
                    str = str + " ";
                }
                str = $"{str}{Decline((long)d, false, dollars, cents)}";
                if (!showCents)
                {
                    return str;
                }
                str = str + " and " + $"{NumToStr(num2, false)}";
                if (num2 == 0)
                {
                    str = str + " ";
                }
                return (str + $"{Decline(num2, true, dollars, cents)}");
            }

            public static string CurrToStr(double value, bool uppercase, bool showCents, string dollars, string cents) => CurrToStr((decimal)value, uppercase, showCents, dollars, cents);

            public static string CurrToStr(long value, bool uppercase, bool showCents, string dollars, string cents) => CurrToStr((decimal)value, uppercase, showCents, dollars, cents);

            public static string DateToStr(DateTime date, bool uppercase = false)
            {
                var builder = new StringBuilder($"{date.Day} {Months[date.Month - 1]} {date.Year}");
                if (uppercase)
                {
                    builder[0] = char.ToUpper(builder[0]);
                }
                return builder.ToString();
            }

            public static string Decline(long value, string one, string two)
            {
                var num = value % 100L;
                if (num == 1L)
                {
                    return one;
                }
                return two;
            }

            public static string Decline(long value, bool showCents, string dollars, string cents)
            {
                if (showCents)
                {
                    var chArray1 = new[] { '/' };
                    var strArray = cents.Split(chArray1);
                    return Decline(value, strArray[0], strArray[1]);
                }
                var separator = new[] { '/' };
                var strArray2 = dollars.Split(separator);
                return Decline(value, strArray2[0], strArray2[1]);
            }

            public static string NumToStr(decimal value) => NumToStr((long)value);

            public static string NumToStr(double value) => NumToStr((long)value);

            public static string NumToStr(long value) => NumToStr(value, true);

            public static string NumToStr(decimal value, bool uppercase) => NumToStr((long)value, uppercase);

            public static string NumToStr(double value, bool uppercase) => NumToStr((long)value, uppercase);

            public static string NumToStr(long value, bool uppercase)
            {
                var sb = new StringBuilder();
                if (value == 0)
                {
                    sb.Append("zero");
                }
                else
                {
                    var flag = false;
                    if (value < 0L)
                    {
                        flag = true;
                        value = Math.Abs(value);
                    }
                    var rank = 1000000000000000000L;
                    AddRank(sb, ref rank, ref value, "quintillion");
                    AddRank(sb, ref rank, ref value, "quadrillion");
                    AddRank(sb, ref rank, ref value, "trillion");
                    AddRank(sb, ref rank, ref value, "billion");
                    AddRank(sb, ref rank, ref value, "million");
                    AddRank(sb, ref rank, ref value, "thousand");
                    AddRank(sb, ref rank, ref value, "");
                    if (flag)
                    {
                        sb.Insert(0, "minus ");
                    }
                }
                if (uppercase)
                {
                    sb[0] = char.ToUpper(sb[0]);
                }
                return sb.ToString();
            }
        }
    }
}
