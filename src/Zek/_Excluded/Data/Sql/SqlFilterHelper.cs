using Zek.Data.Filtering;
using Zek.Extensions;
using Zek.Extensions.Sql;
using Zek.Utils;

namespace Zek.Data.Sql
{
    public class SqlFilterHelper
    {
        private SqlFilterHelper() { }


        private static string FormatLike(string value)
        {
            return value.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");//მიმდევრობა არ შეცვალოთ თორემ აირევა!!!
        }


        private static string[] InternalGetWhereOperators(WhereOperator op)
        {
            var result = new List<string> { string.Empty };
            foreach (WhereOperator en in Enum.GetValues(typeof(WhereOperator)))
            {
                if (en == WhereOperator.ForText || en == WhereOperator.ForDate || en == WhereOperator.ForNumber)
                    continue;

                if (op.HasFlag(en))
                    result.Add(en.ToString());
            }

            return result.ToArray();
        }
        private static string[] InternalGetWhereOperatorsAbr(WhereOperator op)
        {
            var result = new List<string> { string.Empty };
            foreach (WhereOperator en in Enum.GetValues(typeof(WhereOperator)))
            {
                if (en == WhereOperator.ForText || en == WhereOperator.ForDate || en == WhereOperator.ForNumber)
                    continue;

                if (op.HasFlag(en))
                    result.Add(InternalGetWhereOperatorAbr(en));
            }

            return result.ToArray();
        }
        private static string InternalGetWhereOperatorAbr(WhereOperator op)
        {
            switch (op)
            {
                case WhereOperator.Equals: return "=";
                case WhereOperator.NotEquals: return "<>";
                case WhereOperator.GreaterThan: return ">";
                case WhereOperator.GreaterThanOrEquals: return ">=";
                case WhereOperator.LessThan: return "<";
                case WhereOperator.LessThanOrEquals: return "<=";
                case WhereOperator.Between: return "[...]";
                case WhereOperator.NotBetween: return "![...]";
                case WhereOperator.Overlap: return "OL";
                case WhereOperator.NotOverlap: return "NOL";
                case WhereOperator.Like: return "a%c";
                case WhereOperator.NotLike: return "!a%c";
                case WhereOperator.Contains: return "a[b]c";
                case WhereOperator.NotContains: return "!a[b]c";
                case WhereOperator.Begins: return "[a]b";
                case WhereOperator.NotBegins: return "![a]b";
                case WhereOperator.Ends: return "b[c]";
                case WhereOperator.NotEnds: return "!b[c]";
                case WhereOperator.In: return "(a,b,c)";
                case WhereOperator.NotIn: return "!(a,b,c)";
            }

            return string.Empty;
        }
        private static WhereOperator InternalParseWhereOperator(string whereOperator)
        {
            switch (whereOperator)
            {
                case "=":
                    return WhereOperator.Equals;
                case "<>":
                    return WhereOperator.NotEquals;
                case ">":
                    return WhereOperator.GreaterThan;
                case ">=":
                    return WhereOperator.GreaterThanOrEquals;
                case "<":
                    return WhereOperator.LessThan;
                case "<=":
                    return WhereOperator.LessThanOrEquals;

                case "[a-b]":
                case "[...]":
                case "[..]":
                case "[.]":
                case "[]":
                case "[ ]":
                case "[  ]":
                case "[   ]":
                    return WhereOperator.Between;
                case "![a-b]":
                case "![...]":
                case "![..]":
                case "![.]":
                case "![]":
                case "![ ]":
                case "![  ]":
                case "![   ]":
                    return WhereOperator.NotBetween;

                case "OL":
                    return WhereOperator.Overlap;
                case "NOL":
                    return WhereOperator.NotOverlap;

                case "a%c":
                    return WhereOperator.Like;
                case "!a%c":
                    return WhereOperator.NotLike;
                case "a[b]c":
                    return WhereOperator.Contains;
                case "!a[b]c":
                    return WhereOperator.NotContains;
                case "[a]b":
                    return WhereOperator.Begins;
                case "![a]b":
                    return WhereOperator.NotBegins;
                case "b[c]":
                    return WhereOperator.Ends;
                case "!b[c]":
                    return WhereOperator.NotEnds;

                case "(a,b,c)":
                    return WhereOperator.In;
                case "!(a,b,c)":
                    return WhereOperator.NotIn;
            }

            return (WhereOperator)Enum.Parse(typeof(WhereOperator), whereOperator);
        }

        private static bool InternalIsEmpty(object value)
        {
            return (value is string && ((string)value).Trim().Length == 0) || CheckHelper.IsNullOrDefault(value);
        }


        public static string[] GetWhereOperatorsAbrForText()
        {
            return InternalGetWhereOperatorsAbr(WhereOperator.ForText);
        }
        public static string[] GetWhereOperatorsAbrForDate()
        {
            return InternalGetWhereOperatorsAbr(WhereOperator.ForDate);
        }
        public static string[] GetWhereOperatorsAbrNumber()
        {
            return InternalGetWhereOperatorsAbr(WhereOperator.ForNumber);
        }

        public static string[] GetWhereOperatorsForText()
        {
            return InternalGetWhereOperators(WhereOperator.ForText);
        }
        public static string[] GetWhereOperatorsForDate()
        {
            return InternalGetWhereOperators(WhereOperator.ForDate);
        }
        public static string[] GetWhereOperatorsNumber()
        {
            return InternalGetWhereOperators(WhereOperator.ForNumber);
        }

        public static string GetWhereClause(string fieldName, string whereOperator, bool doNotFilterIfEmpty, DateTime? value, DateTimePrecision precission)
        {
            if (string.IsNullOrEmpty(whereOperator))
                return string.Empty;

            return GetWhereClause(fieldName, InternalParseWhereOperator(whereOperator), doNotFilterIfEmpty, value, precission);
        }
        public static string GetWhereClause(string fieldName, WhereOperator whereOperator, bool doNotFilterIfEmpty, DateTime? value, DateTimePrecision precission)
        {
            if (doNotFilterIfEmpty && value == null)
                return string.Empty;

            if (value == null)
                throw new ArgumentNullException(nameof(value), "Cannot use comparison operator " + whereOperator + " for NULL values.");

            var output = string.Empty;
            value = RoundDown(value, precission);
            switch (whereOperator)
            {
                case WhereOperator.Equals:
                    output = fieldName + " >= " + value.ToSqlValue() + " AND " + fieldName + " < " + Add(value, 1d, precission).ToSqlValue();
                    break;

                case WhereOperator.NotEquals:
                    output = fieldName + " < " + value.ToSqlValue() + " OR " + fieldName + " >= " + Add(value, 1d, precission).ToSqlValue();
                    break;

                case WhereOperator.GreaterThan:
                    output = fieldName + " >= " + Add(value, 1d, precission).ToSqlValue();
                    break;

                case WhereOperator.GreaterThanOrEquals:
                    output = fieldName + " >= " + value.ToSqlValue();
                    break;

                case WhereOperator.LessThan:
                    output = fieldName + " < " + value.ToSqlValue();
                    break;

                case WhereOperator.LessThanOrEquals:
                    output = fieldName + " <= " + value.ToSqlValue();
                    break;
            }

            return output.Length != 0 ? "(" + output + ")" : output;
        }

        private static DateTime? RoundDown(DateTime? date, DateTimePrecision precission)
        {
            if (date == null) return null;
            switch (precission)
            {
                case DateTimePrecision.Seconds:
                    return date.Value.GetStartOfSecond();

                case DateTimePrecision.Minutes:
                    return date.Value.GetStartOfMinute();

                case DateTimePrecision.Hours:
                    return date.Value.GetStartOfHour();

                case DateTimePrecision.Days:
                    return date.Value.GetStartOfDay();
            }

            return date.Value.GetStartOfSecond();
        }
        private static DateTime? Add(DateTime? date, double value, DateTimePrecision precission)
        {
            if (date == null) return null;

            switch (precission)
            {
                case DateTimePrecision.Seconds:
                    return date.Value.AddSeconds(value);

                case DateTimePrecision.Minutes:
                    return date.Value.AddMinutes(value);

                case DateTimePrecision.Hours:
                    return date.Value.AddHours(value);

                case DateTimePrecision.Days:
                    return date.Value.AddDays(value);
            }

            return date.Value.AddSeconds(value);
        }

        public static string GetWhereClause(string fieldName, string whereOperator, bool doNotFilterIfEmpty, DateTime? value1, DateTime? value2, DateTimePrecision precission)
        {
            if (string.IsNullOrEmpty(whereOperator))
                return string.Empty;

            return GetWhereClause(fieldName, InternalParseWhereOperator(whereOperator), doNotFilterIfEmpty, value1, value2, precission);
        }
        public static string GetWhereClause(string fieldName, WhereOperator whereOperator, bool doNotFilterIfEmpty, DateTime? value1, DateTime? value2, DateTimePrecision precission)
        {
            if (whereOperator != WhereOperator.Between && whereOperator != WhereOperator.NotBetween && whereOperator != WhereOperator.Overlap && whereOperator != WhereOperator.NotOverlap)
                return GetWhereClause(fieldName, whereOperator, doNotFilterIfEmpty, value1, precission);

            if (doNotFilterIfEmpty && (value1 == null || value2 == null))
                return string.Empty;

            if (value1 == null)
                throw new ArgumentNullException(nameof(value1), "Cannot use comparison operator " + whereOperator + " for NULL values.");

            if (value2 == null && (whereOperator == WhereOperator.Between || whereOperator == WhereOperator.NotBetween || whereOperator == WhereOperator.Overlap || whereOperator == WhereOperator.NotOverlap))
                throw new ArgumentNullException(nameof(value2), "Cannot use comparison operator " + whereOperator + " for NULL values.");


            var output = string.Empty;
            value1 = RoundDown(value1, precission);
            switch (whereOperator)
            {
                case WhereOperator.Between:
                    output = fieldName + " >= " + value1.ToSqlValue() + " AND " + fieldName + " < " + Add(RoundDown(value2, precission), 1d, precission).ToSqlValue();
                    break;

                case WhereOperator.NotBetween:
                    output = fieldName + " < " + value1.ToSqlValue() + " OR " + fieldName + " >= " + Add(RoundDown(value2, precission), 1d, precission).ToSqlValue();
                    break;
            }

            return output.Length != 0 ? "(" + output + ")" : output;
        }

        public static string GetWhereClause(string fieldName, string whereOperator, bool doNotFilterIfEmpty, object value1, object value2)
        {
            if (string.IsNullOrEmpty(whereOperator))
                return string.Empty;

            return GetWhereClause(fieldName, InternalParseWhereOperator(whereOperator), doNotFilterIfEmpty, value1, value2);
        }
        public static string GetWhereClause(string fieldName, WhereOperator whereOperator, bool doNotFilterIfEmpty, object value1, object value2)
        {
            return GetWhereClause(fieldName, fieldName, whereOperator, doNotFilterIfEmpty, value1, value2);
        }
        public static string GetWhereClause(string fieldName1, string fieldName2, WhereOperator whereOperator, bool doNotFilterIfEmpty, object value1, object value2)
        {
            if (whereOperator != WhereOperator.Between && whereOperator != WhereOperator.NotBetween && whereOperator != WhereOperator.Overlap && whereOperator != WhereOperator.NotOverlap)
                return GetWhereClause(fieldName1, whereOperator, doNotFilterIfEmpty, value1);


            if (doNotFilterIfEmpty && InternalIsEmpty(value1) && InternalIsEmpty(value2))
                return string.Empty;


            if (value1 == null || value1 == DBNull.Value)
                throw new ArgumentNullException(nameof(value1), "Cannot use comparison operator " + whereOperator + " for NULL values.");
            if (value2 == null || value2 == DBNull.Value)
                throw new ArgumentNullException(nameof(value2), "Cannot use comparison operator " + whereOperator + " for NULL values.");


            var output = string.Empty;
            var formatedValue1 = value1.ToSqlValue();
            var formatedValue2 = value2.ToSqlValue();
            switch (whereOperator)
            {
                case WhereOperator.Between:
                    output = fieldName1 + " BETWEEN " + formatedValue1 + " AND " + formatedValue2;
                    break;

                case WhereOperator.NotBetween:
                    output = fieldName1 + " NOT BETWEEN " + formatedValue1 + " AND " + formatedValue2;
                    break;

                case WhereOperator.Overlap:
                    output = "(" + fieldName1 + " >= " + formatedValue1 + " AND " + fieldName1 + " <= " + formatedValue2 + ")" +
                        " OR (" + fieldName2 + " >= " + formatedValue1 + " AND " + fieldName2 + " <= " + formatedValue2 + ")" +
                        " OR (" + fieldName1 + " <= " + formatedValue1 + " AND " + fieldName2 + " >= " + formatedValue2 + ")";
                    break;

                case WhereOperator.NotOverlap:
                    output = "(" + fieldName1 + " > " + formatedValue2 + ")" +
                        " OR (" + fieldName2 + " < " + formatedValue1 + ")";
                    break;
            }

            return output.Length != 0 ? "(" + output + ")" : output;
        }

        public static string GetWhereClause(string fieldName, string whereOperator, bool doNotFilterIfEmpty, object value)
        {
            if (string.IsNullOrEmpty(whereOperator))
                return string.Empty;

            return GetWhereClause(fieldName, InternalParseWhereOperator(whereOperator), doNotFilterIfEmpty, value);
        }
        public static string GetWhereClause(string fieldName, WhereOperator whereOperator, bool doNotFilterIfEmpty, object value)
        {
            var output = string.Empty;
            var empty = InternalIsEmpty(value);

            if (doNotFilterIfEmpty && empty)
                return string.Empty;


            if (value != null && value != DBNull.Value)
            {
                var formatedValue = value.ToSqlValue();
                switch (whereOperator)
                {
                    case WhereOperator.Equals:
                        output = fieldName + " = " + formatedValue;
                        break;

                    case WhereOperator.NotEquals:
                        output = fieldName + " <> " + formatedValue;
                        break;

                    case WhereOperator.GreaterThan:
                        output = fieldName + " > " + formatedValue;
                        break;

                    case WhereOperator.GreaterThanOrEquals:
                        output = fieldName + " >= " + formatedValue;
                        break;

                    case WhereOperator.LessThan:
                        output = fieldName + " < " + formatedValue;
                        break;

                    case WhereOperator.LessThanOrEquals:
                        output = fieldName + " <= " + formatedValue;
                        break;

                    case WhereOperator.Like:
                        if (!empty)
                            output = fieldName + " LIKE " + formatedValue;
                        break;

                    case WhereOperator.NotLike:
                        if (!empty)
                            output = fieldName + " NOT LIKE " + formatedValue;
                        break;

                    case WhereOperator.Contains:
                        //if (!empty)
                        //    output = fieldName + " LIKE N'%' + " + FormatLike(formatedValue) + " + N'%'";
                        // new version is faster (used in entity framework core)
                        if (!empty)
                            output = "CHARINDEX(" + formatedValue + ", " + fieldName + ") > 0";
                        break;

                    case WhereOperator.NotContains:
                        //if (!empty)
                        //    output = fieldName + " NOT LIKE N'%' + " + FormatLike(formatedValue) + "+ N'%'";
                        // new version is faster (used in entity framework core)
                        if (!empty)
                            output = "CHARINDEX(" + formatedValue + ", " + fieldName + ") <= 0";
                        break;

                    case WhereOperator.Begins:
                        if (!empty)
                            output = fieldName + " LIKE " + FormatLike(formatedValue) + " + N'%' AND (CHARINDEX(" + formatedValue + ", " + fieldName + ") = 1)";
                        break;

                    case WhereOperator.NotBegins://todo need to update new version like entity framework core
                        if (!empty)
                            output = fieldName + " NOT LIKE " + FormatLike(formatedValue) + "+ N'%'";
                        break;

                    case WhereOperator.Ends:
                        //if (!empty)
                        //    output = fieldName + " LIKE '%'+" + FormatLike(formatedValue);
                        if (!empty)
                            output = "RIGHT(" + fieldName + ", " + formatedValue.Length + ") = " + formatedValue;
                        break;

                    case WhereOperator.NotEnds://todo need to update new version like entity framework core
                        if (!empty)
                            output = fieldName + " NOT LIKE '%'+" + FormatLike(formatedValue);
                        break;

                    case WhereOperator.In:
                        if (!empty)
                            output = fieldName + " IN (" + formatedValue + ")";
                        else
                            output = "0 = 1";
                        break;

                    case WhereOperator.NotIn:
                        if (!empty)
                            output = fieldName + " NOT IN (" + formatedValue + ")";
                        break;
                }
            }
            else
            {
                if ((whereOperator != WhereOperator.Equals) && (whereOperator != WhereOperator.NotEquals))
                {
                    throw new ArgumentNullException(nameof(value), "Cannot use comparison operator " + whereOperator + " for NULL values.");
                }

                switch (whereOperator)
                {
                    case WhereOperator.Equals:
                        output = fieldName + " IS NULL"; break;
                    case WhereOperator.NotEquals:
                        output = fieldName + " IS NOT NULL"; break;
                }
            }
            return output.Length != 0 ? "(" + output + ")" : output;
        }
    }
}
