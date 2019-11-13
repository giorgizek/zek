using System;
using System.Collections;
using System.Text;

// ReSharper disable once CheckNamespace
namespace Zek.Utils
{
    public partial class Func
    {
        public sealed class Ru
        {

            private static readonly string[] DaysOfWeek = { "воскресенье", "понедельник", "вторник", "среда", "четверг", "пятница", "суббота" };
            public static string DayOfWeek(DateTime date) => DaysOfWeek[(int)date.DayOfWeek];


            private static readonly Hashtable Currencies = new Hashtable();
            private static readonly string[,] Gendered;
            private static readonly string[] Hundreds = { "сто", "двести", "триста", "четыреста", "пятьсот", "шестьсот", "семьсот", "восемьсот", "девятьсот" };
            private static readonly string[] Months = { "января", "февраля", "марта", "апреля", "мая", "июня", "июля", "августа", "сентября", "октября", "ноября", "декабря" };
            private static readonly string[] Tens = { "десять", "двадцать", "тридцать", "сорок", "пятьдесят", "шестьдесят", "семьдесят", "восемьдесят", "девяносто" };

            private static readonly string[] Units = { "один", "два", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять", "десять", "одиннадцать", "двенадцать", "тринадцать", "четырнадцать", "пятнадцать", "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать" };

            static Ru()
            {
                Gendered = new[,] { { "один", "одна", "одно" }, { "два", "две", "два" } };
                RegisterCurrency(new EurCurrency(), "EUR");
                RegisterCurrency(new UsdCurrency(), "USD");
                RegisterCurrency(new RurCurrency(), "RUR");
                RegisterCurrency(new UahCurrency(), "UAH");
                RegisterCurrency(new KztCurrency(), "KZT");
            }

            private static void AddHundreds(StringBuilder sb, long value)
            {
                if (value != 0)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" ");
                    }
                    sb.Append(Hundreds[value - 1L]);
                }
            }

            private static void AddRank(StringBuilder sb, ref long rank, ref long value, string one, string two, string five, Gender gender)
            {
                var num = value / rank;
                if (num > 0L)
                {
                    AddThousand(sb, num, gender);
                    var num2 = num % 10L;
                    var num3 = num % 100L;
                    string str;
                    if ((num3 >= 11L) && (num3 < 20L))
                    {
                        str = five;
                    }
                    else if (num2 == 1L)
                    {
                        str = one;
                    }
                    else if ((num2 > 1L) && (num2 < 5L))
                    {
                        str = two;
                    }
                    else
                    {
                        str = five;
                    }
                    if ((num > 10L) && (num < 20L))
                    {
                        str = five;
                    }
                    if (sb.Length > 0)
                    {
                        sb.Append(" ");
                    }
                    sb.Append(str);
                    value = value % rank;
                }
                rank /= 1000L;
            }

            private static void AddTens(StringBuilder sb, long value)
            {
                if (value != 0)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" ");
                    }
                    sb.Append(Tens[value - 1L]);
                }
            }

            private static void AddThousand(StringBuilder sb, long value, Gender gender)
            {
                AddHundreds(sb, value / 100L);
                value = value % 100L;
                if (value < 20L)
                {
                    AddUnits(sb, value, gender);
                }
                else
                {
                    AddTens(sb, value / 10L);
                    AddUnits(sb, value % 10L, gender);
                }
            }

            private static void AddUnits(StringBuilder sb, long value, Gender gender)
            {
                if (value == 0) return;

                if (sb.Length > 0)
                {
                    sb.Append(" ");
                }

                if (value < 3L)
                {
                    sb.Append(Gendered[(value - 1L), (int)gender]);
                }
                else
                {
                    sb.Append(Units[value - 1L]);
                }
            }

            public static string CurrToStr(decimal value) => CurrToStr(value, "RUR");

            public static string CurrToStr(double value) => CurrToStr((decimal)value, "RUR");

            public static string CurrToStr(decimal value, bool cents) => CurrToStr(value, "RUR", cents);

            public static string CurrToStr(decimal value, string currency) => CurrToStr(value, true, currency);

            public static string CurrToStr(double value, bool cents) => CurrToStr(value, "RUR", cents);

            public static string CurrToStr(double value, string currency) => CurrToStr((decimal)value, true, currency);

            public static string CurrToStr(long value, bool cents) => CurrToStr((decimal)value, "RUR", cents);

            public static string CurrToStr(decimal value, bool uppercase, string currency) => CurrToStr((double)value, uppercase, currency);

            public static string CurrToStr(decimal value, string currency, bool cents) => CurrToStr(value, true, currency, cents);

            public static string CurrToStr(double value, bool uppercase, string currency) => CurrToStr((decimal)value, uppercase, currency, true);

            public static string CurrToStr(double value, string currency, bool cents) => CurrToStr((decimal)value, true, currency, cents);

            public static string CurrToStr(long value, bool uppercase, string currency) => CurrToStr((decimal)value, uppercase, currency, true);

            public static string CurrToStr(long value, string currency, bool cents) => CurrToStr((decimal)value, true, currency, cents);

            public static string CurrToStr(decimal value, bool uppercase, string currency, bool cents)
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
                string str = $"{NumToStr(d, uppercase, GetCurrency(currency).Gender)} {Decline(d, currency)}";
                if (cents)
                {
                    str = str + $" {num2:d2}" + $" {Decline(num2, currency, true)}";
                }
                return str;
            }

            public static string CurrToStr(double value, bool uppercase, string currency, bool cents) => CurrToStr((decimal)value, uppercase, currency, cents);

            public static string DateToStr(DateTime date, bool uppercase = false)
            {
                var builder = new StringBuilder($"{date.Day} {Months[date.Month - 1]} {date.Year}");
                if (uppercase)
                {
                    builder[0] = char.ToUpper(builder[0]);
                }
                return builder.ToString();
            }

            public static string Decline(decimal value, string currency) => Decline(value, currency, false);

            public static string Decline(double value, string currency) => Decline(value, currency, false);

            public static string Decline(long value, string currency) => Decline(value, currency, false);

            public static string Decline(decimal value, string currency, bool cents) => Decline((long)value, currency, cents);

            public static string Decline(double value, string currency, bool cents) => Decline((long)value, currency, cents);

            public static string Decline(long value, string currency, bool cents)
            {
                var currency2 = GetCurrency(currency);
                if (cents)
                {
                    return Decline(value, currency2.CentOne, currency2.CentTwo, currency2.CentFive);
                }
                return Decline(value, currency2.DollarOne, currency2.DollarTwo, currency2.DollarFive);
            }

            public static string Decline(decimal value, string one, string two, string five) => Decline((long)value, one, two, five);

            public static string Decline(double value, string one, string two, string five) => Decline((long)value, one, two, five);

            public static string Decline(long value, string one, string two, string five)
            {
                var num = value % 100L;
                if ((num < 10L) || (num >= 20L))
                {
                    num = num % 10L;
                    if (num == 1L)
                    {
                        return one;
                    }
                    if ((num > 1L) && (num < 5L))
                    {
                        return two;
                    }
                }
                return five;
            }

            private static Currency GetCurrency(string currencyName)
            {
                var currency = Currencies[currencyName.ToUpper()] as Currency;
                if (currency == null)
                {
                    throw new ArgumentException($"Currency '{currencyName}' is not registered");
                }
                return currency;
            }

            public static string NumToStr(decimal value) => NumToStr((long)value);

            public static string NumToStr(double value) => NumToStr((decimal)value);

            public static string NumToStr(long value) => NumToStr(value, true, Gender.Masculine);

            public static string NumToStr(decimal value, bool uppercase) => NumToStr((long)value, uppercase);

            public static string NumToStr(double value, bool uppercase) => NumToStr((long)value, uppercase);

            public static string NumToStr(long value, bool uppercase) => NumToStr(value, uppercase, Gender.Masculine);

            public static string NumToStr(decimal value, bool uppercase, Gender gender) => NumToStr((long)value, uppercase, gender);

            public static string NumToStr(double value, bool uppercase, Gender gender) => NumToStr((long)value, uppercase, gender);

            public static string NumToStr(long value, bool uppercase, Gender gender)
            {
                var sb = new StringBuilder();
                if (value == 0)
                {
                    sb.Append("ноль");
                }
                else
                {
                    if (value < 0L)
                    {
                        sb.Append("минус");
                        value = Math.Abs(value);
                    }
                    var rank = 1000000000000000000L;
                    AddRank(sb, ref rank, ref value, "квинтильон", "квинтильона", "квинтильонов", Gender.Masculine);
                    AddRank(sb, ref rank, ref value, "квадрильон", "квадрильона", "квадрильонов", Gender.Masculine);
                    AddRank(sb, ref rank, ref value, "триллион", "триллиона", "триллионов", Gender.Masculine);
                    AddRank(sb, ref rank, ref value, "миллиард", "миллиарда", "миллиардов", Gender.Masculine);
                    AddRank(sb, ref rank, ref value, "миллион", "миллиона", "миллионов", Gender.Masculine);
                    AddRank(sb, ref rank, ref value, "тысяча", "тысячи", "тысяч", Gender.Feminine);
                    AddThousand(sb, value, gender);
                }
                if (uppercase)
                {
                    sb[0] = char.ToUpper(sb[0]);
                }
                return sb.ToString();
            }

            public static void RegisterCurrency(Currency currency, string currencyName)
            {
                Currencies[currencyName.ToUpper()] = currency;
            }

            // Nested Types
            public abstract class Currency : BaseCurrency
            {
                public string CentFive => Cents[2];

                public string CentOne => Cents[0];

                protected abstract string[] Cents { get; }

                public string CentTwo => Cents[1];

                public string DollarFive => Dollars[2];

                public string DollarOne => Dollars[0];

                protected abstract string[] Dollars { get; }

                public string DollarTwo => Dollars[1];
            }

            public class EurCurrency : Currency
            {
                protected override string[] Cents { get; } = { "цент", "цента", "центов" };

                public override Gender CentsGender => Gender.Masculine;

                protected override string[] Dollars { get; } = { "евро", "евро", "евро" };

                public override Gender Gender => Gender.Neutral;
            }

            public class KztCurrency : Currency
            {
                protected override string[] Cents { get; } = { "тиын", "тиына", "тиынов" };

                public override Gender CentsGender => Gender.Feminine;

                protected override string[] Dollars { get; } = { "тенге", "тенге", "тенге" };

                public override Gender Gender => Gender.Masculine;
            }

            public class RurCurrency : Currency
            {
                protected override string[] Cents { get; } = { "копейка", "копейки", "копеек" };

                public override Gender CentsGender => Gender.Feminine;

                protected override string[] Dollars { get; } = { "рубль", "рубля", "рублей" };

                public override Gender Gender => Gender.Masculine;
            }

            public class UahCurrency : Currency
            {
                protected override string[] Cents { get; } = { "копейка", "копейки", "копеек" };

                public override Gender CentsGender => Gender.Feminine;

                protected override string[] Dollars { get; } = { "гривна", "гривны", "гривен" };

                public override Gender Gender => Gender.Feminine;
            }

            public class UsdCurrency : Currency
            {
                protected override string[] Cents { get; } = { "цент", "цента", "центов" };

                public override Gender CentsGender => Gender.Masculine;

                protected override string[] Dollars { get; } = { "доллар", "доллара", "долларов" };

                public override Gender Gender => Gender.Masculine;
            }
        }
    }
}
