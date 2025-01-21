using System.Text;

namespace Zek.Utils
{
    public static class NumToStrHelper
    {
        /// <summary>
        /// აკონვერტირებს გადაცემულ მნიშვნელობას სიტყვიერ ექვივალენტში.
        /// </summary>
        /// <param name="value">მნიშვნელობა რომელსაც აკონვერტირებთ.</param>
        /// <param name="currency">ვალუტის დასახელება (ლარი, დოლარი, ევრო...).</param>
        /// <param name="minorUnit">1/100 დასახელება (თეთრი, ცენტი, ევრო ცენტი...).</param>
        /// <param name="showMinorUnit">თუ გადავცემთ true-ს მაშინ ცენტების მნიშვნელობაც დაემატება.</param>
        /// <returns>აბრუნებს სიტყვიერ მნიშვნელობას გადაცემული რიცხვის.</returns>
        public static string CurrToStr(decimal value, string currency, string minorUnit = "", bool showMinorUnit = false)
        {
            var str = $"{NumToStr((long)value)} {currency}";

            if (showMinorUnit)
            {
                var dec = (long)((value - (long)value) * 100);
                str += $" და {dec:d2} {minorUnit}";
            }
            return str;
        }

        /// <summary>
        /// Converts the specified value to its equivalent string representation.
        /// </summary>
        /// <param name="value">value - A value containing a number to convert.</param>
        /// <returns>Returns or does not return the string representation of the integer value.</returns>
        public static string NumToStr(long value)
        {
            var sb = new StringBuilder();
            if (value == 0L)
            {
                sb.Append("ნული");
            }
            else
            {
                if (value < 0L)
                {
                    sb.Append("მინუს");
                    value = Math.Abs(value);
                }

                var rank = 1000000000000000000L;
                AddRank(sb, ref rank, ref value, "კვანტილიონ");
                AddRank(sb, ref rank, ref value, "კვადრილიონ");
                AddRank(sb, ref rank, ref value, "ტრილიონ");
                AddRank(sb, ref rank, ref value, "მილიარდ");
                AddRank(sb, ref rank, ref value, "მილიონ");
                AddRank(sb, ref rank, ref value, "ათას");
                AddThousand(sb, value);
            }

            return sb.ToString();
        }
        /// <summary>
        /// 1-19 დამატება
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="value"></param>
        private static void AddUnits(StringBuilder sb, long value)
        {
            //if (value != 0)
            //    sb.Append(_Units[value - 1]);

            switch (value)
            {
                case 1:
                    sb.Append("ერთი");
                    break;
                case 2:
                    sb.Append("ორი");
                    break;
                case 3:
                    sb.Append("სამი");
                    break;
                case 4:
                    sb.Append("ოთხი");
                    break;
                case 5:
                    sb.Append("ხუთი");
                    break;
                case 6:
                    sb.Append("ექვსი");
                    break;
                case 7:
                    sb.Append("შვიდი");
                    break;
                case 8:
                    sb.Append("რვა");
                    break;
                case 9:
                    sb.Append("ცხრა");
                    break;
                case 10:
                    sb.Append("ათი");
                    break;
                case 11:
                    sb.Append("თერთმეტი");
                    break;
                case 12:
                    sb.Append("თორმეტი");
                    break;
                case 13:
                    sb.Append("ცამეტი");
                    break;
                case 14:
                    sb.Append("თოთხმეტი");
                    break;
                case 15:
                    sb.Append("თხუთმეტი");
                    break;
                case 16:
                    sb.Append("თექვსმეტი");
                    break;
                case 17:
                    sb.Append("ჩვიდმეტი");
                    break;
                case 18:
                    sb.Append("თვრამეტი");
                    break;
                case 19:
                    sb.Append("ცხრამეტი");
                    break;
            }
        }
        /// <summary>
        /// 20,40,60,80-ის დამატება
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="value"></param>
        private static void AddTens(StringBuilder sb, long value)
        {
            if (value != 0L)
            {
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                }

                if (value % 20 > 0)
                {
                    //sb.Append(_TensLong[(value / 20) - 1]);
                    switch (value / 20L)
                    {
                        case 1:
                            sb.Append("ოცდა");
                            break;
                        case 2:
                            sb.Append("ორმოცდა");
                            break;
                        case 3:
                            sb.Append("სამოცდა");
                            break;
                        case 4:
                            sb.Append("ოთხმოცდა");
                            break;
                    }
                }
                else
                {
                    //sb.Append(_TensShort[(value / 20) - 1]);
                    switch (value / 20L)
                    {
                        case 1:
                            sb.Append("ოცი");
                            break;
                        case 2:
                            sb.Append("ორმოცი");
                            break;
                        case 3:
                            sb.Append("სამოცი");
                            break;
                        case 4:
                            sb.Append("ოთხმოცი");
                            break;
                    }
                }
            }

            AddUnits(sb, value % 20);
        }
        /// <summary>
        /// ასეულების დამატება
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="value"></param>
        private static void AddHundreds(StringBuilder sb, long value)
        {
            if (value == 0) return;

            if (sb.Length > 0)
            {
                sb.Append(" ");
            }
            //sb.Append(_Hundreds[value - 1]);
            switch (value)
            {
                case 1:
                    sb.Append("ას");
                    break;
                case 2:
                    sb.Append("ორას");
                    break;
                case 3:
                    sb.Append("სამას");
                    break;
                case 4:
                    sb.Append("ოთხას");
                    break;
                case 5:
                    sb.Append("ხუთას");
                    break;
                case 6:
                    sb.Append("ექვსას");
                    break;
                case 7:
                    sb.Append("შვიდას");
                    break;
                case 8:
                    sb.Append("რვაას");
                    break;
                case 9:
                    sb.Append("ცხრაას");
                    break;
            }
        }
        /// <summary>
        /// ათასეულების დამატება
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="value"></param>
        private static void AddThousand(StringBuilder sb, long value)
        {
            AddHundreds(sb, value / 100L);
            value = value % 100L;

            if (value == 0L)
            {
                sb.Append("ი");
            }
            else if (value < 20L)
            {
                if (sb.Length > 0) sb.Append(" ");
                AddUnits(sb, value);
            }
            else AddTens(sb, value);
        }
        /// <summary>
        /// 000-იანებით დაყოფა და დამატება.
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="rank"></param>
        /// <param name="value"></param>
        /// <param name="unit"></param>
        private static void AddRank(StringBuilder sb, ref long rank, ref long value, string unit)
        {
            var rankValue = value / rank;

            if (rankValue > 0L)
            {
                AddThousand(sb, rankValue);

                //long units = rankValue % 10;

                if (sb.Length > 0)
                {
                    sb.Append(" ");
                }
                sb.Append(unit);

                value %= rank;
            }
            rank /= 1000;
        }

        /// <summary>
        /// GEL, USD, EUR... გადაყავს ლარი, დოლარი, ევრო-ში.
        /// </summary>
        /// <param name="currencyCode"></param>
        /// <returns></returns>
        public static string ToCurrencyName(string currencyCode)
        {
            if (string.IsNullOrEmpty(currencyCode))
                return string.Empty;

            switch (currencyCode.Trim().ToUpperInvariant())
            {
                case "EUR":
                    return "ევრო";
                case "GBP":
                    return "ფუნტი";
                case "GEL":
                    return "ლარი";
                case "RUB":
                    return "რუბლი";
                case "USD":
                    return "დოლარი";
                default:
                    return string.Empty;
            }
        }
        /// <summary>
        /// GEL, USD, EUR... გადაყავს თეთრი, ცენტი, ევრო ცენტი.
        /// </summary>
        /// <param name="currencyCode"></param>
        /// <returns></returns>
        public static string ToCurrencyMinorUnit(string currencyCode)
        {
            if (string.IsNullOrEmpty(currencyCode))
                return string.Empty;

            switch (currencyCode.Trim().ToUpperInvariant())
            {
                case "EUR":
                    return "ევრო ცენტი";
                case "GBP":
                    return "პენსი";
                case "GEL":
                    return "თეთრი";
                case "RUB":
                    return "კაპიკი";
                case "USD":
                    return "ცენტი";
                default:
                    return string.Empty;
            }
        }


        //public static string ToCurrencySymbol(string currencyCode)
        //{
        //    if (string.IsNullOrEmpty(currencyCode))
        //        return string.Empty;

        //    switch (currencyCode.Trim().ToUpperInvariant())
        //    {
        //        case "EUR":
        //            return "€";
        //        case "GBP":
        //            return "£";
        //        case "GEL":
        //            return "₾";
        //        case "RUB":
        //            return "₽";
        //        case "USD":
        //            return "$";
        //        default:
        //            return string.Empty;
        //    }
        //}

       
        /// <summary>
        /// GEL, USD, EUR... To ლ, $, .
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string ToCurrencySymbol(ISO4217.ISO4217? currency) => currency != null ? ToCurrencySymbol(currency.Value) : string.Empty;
        
        /// <summary>
        /// GEL, USD, EUR... To ლ, $, .
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string ToCurrencySymbol(ISO4217.ISO4217 currency)
        {
            switch (currency)
            {
                case ISO4217.ISO4217.EUR:
                    return "€";
                case ISO4217.ISO4217.GBP:
                    return "£";
                case ISO4217.ISO4217.GEL:
                    return "₾";
                case ISO4217.ISO4217.RUB:
                    return "₽";
                case ISO4217.ISO4217.USD:
                    return "$";
                default:
                    return string.Empty;
            }
        }
    }
}
