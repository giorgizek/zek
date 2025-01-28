using System.Collections;
using System.Globalization;
using System.Text;

namespace Zek.Utils
{
    public static class SqlHelper
    {
        /// <summary>
        /// აკონვერტირებს მნიშვნელობას პარამეტრისთვის გადასაცემ მნიშვნელობად.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ToDbValue(object value)
        {
            return value ?? DBNull.Value;
        }

        public static string ToSqlValue(DBNull value)
        {
            return "NULL";
        }

        public static string ToSqlValue(DateTime? value)
        {
            return value == null ? "NULL" : ToSqlValue(value.Value);
        }
        public static string ToSqlValue(DateTime value)
        {
            return "'" + value.ToString("yyyy-MM-ddTHH:mm:ss.fff") + "'";
        }

        public static string ToSqlDateValue(DateTime? value)
        {
            return value == null ? "NULL" : ToSqlDateValue(value.Value);
        }
        public static string ToSqlDateValue(DateTime value)
        {
            return "'" + value.ToString("yyyy-MM-dd") + "'";
        }
        public static string ToSqlDateTime2Value(DateTime? value)
        {
            return value == null ? "NULL" : ToSqlDateTime2Value(value.Value);
        }
        public static string ToSqlDateTime2Value(DateTime value)
        {
            return "'" + value.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
        }

        public static string ToSqlValue(char? value)
        {
            return value == null ? "NULL" : ToSqlValue(value.Value);
        }
        public static string ToSqlValue(char value)
        {
            return "N'" + value.ToString().Replace("'", "''") + "'";
        }
        public static string ToSqlValue(string value)
        {
            return value == null ? "NULL" : "N'" + value.Replace("'", "''") + "'";
        }

        public static string ToSqlValue(Guid? value)
        {
            return value == null ? "NULL" : ToSqlValue(value.Value);
        }
        public static string ToSqlValue(Guid value)
        {
            return "'" + value + "'";
        }

        public static string ToSqlValue(bool? value)
        {
            return value == null ? "NULL" : ToSqlValue(value.Value);
        }
        public static string ToSqlValue(bool value)
        {
            return value ? "1" : "0";
        }

        public static string ToSqlValue(byte? value)
        {
            return value == null ? "NULL" : ToSqlValue(value.Value);
        }
        public static string ToSqlValue(byte value)
        {
            return value.ToString(null, NumberFormatInfo.InvariantInfo);
        }
        public static string ToSqlValue(sbyte? value)
        {
            return value == null ? "NULL" : ToSqlValue(value.Value);
        }
        public static string ToSqlValue(sbyte value)
        {
            return value.ToString(null, NumberFormatInfo.InvariantInfo);
        }
        public static string ToSqlValue(short? value)
        {
            return value == null ? "NULL" : ToSqlValue(value.Value);
        }
        public static string ToSqlValue(short value)
        {
            return value.ToString(null, NumberFormatInfo.InvariantInfo);
        }
        public static string ToSqlValue(ushort? value)
        {
            return value == null ? "NULL" : ToSqlValue(value.Value);
        }
        public static string ToSqlValue(ushort value)
        {
            return value.ToString(null, NumberFormatInfo.InvariantInfo);
        }
        public static string ToSqlValue(int? value)
        {
            return value == null ? "NULL" : ToSqlValue(value.Value);
        }
        public static string ToSqlValue(int value)
        {
            return value.ToString(null, NumberFormatInfo.InvariantInfo);
        }
        public static string ToSqlValue(uint? value)
        {
            return value == null ? "NULL" : ToSqlValue(value.Value);
        }
        public static string ToSqlValue(uint value)
        {
            return value.ToString(null, NumberFormatInfo.InvariantInfo);
        }
        public static string ToSqlValue(long? value)
        {
            return value == null ? "NULL" : ToSqlValue(value.Value);
        }
        public static string ToSqlValue(long value)
        {
            return value.ToString(null, NumberFormatInfo.InvariantInfo);
        }
        public static string ToSqlValue(ulong? value)
        {
            return value == null ? "NULL" : ToSqlValue(value.Value);
        }
        public static string ToSqlValue(ulong value)
        {
            return value.ToString(null, NumberFormatInfo.InvariantInfo);
        }
        public static string ToSqlValue(decimal? value)
        {
            return value == null ? "NULL" : ToSqlValue(value.Value);
        }
        public static string ToSqlValue(decimal value)
        {
            return value.ToString(null, NumberFormatInfo.InvariantInfo);
        }
        public static string ToSqlValue(double? value)
        {
            return value == null ? "NULL" : ToSqlValue(value.Value);
        }
        public static string ToSqlValue(double value)
        {
            return value.ToString("R", NumberFormatInfo.InvariantInfo);
        }
        public static string ToSqlValue(float? value)
        {
            return value == null ? "NULL" : ToSqlValue(value.Value);
        }
        public static string ToSqlValue(float value)
        {
            return value.ToString("R", NumberFormatInfo.InvariantInfo);
        }



        /// <summary>
        /// აფორმატირებს მნიშვნელობას String-ში, ისე რომ Sql-თან არ შეიქმნას პრობლემა.
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომლის დაფორმატირებაც გვინდა.</param>
        /// <returns>დაფორმატირებული String.</returns>
        public static string ToSqlValue(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return "NULL";
            }

            switch (value.GetType().Name)
            {
                case "String":
                    return ToSqlValue((string)value);

                case "Char":
                    return ToSqlValue((char)value);

                case "DateTime":
                    return ToSqlValue((DateTime)value);

                case "Guid":
                    return ToSqlValue((Guid)value);

                case "DBNull":
                    return "NULL";

                case "Boolean":
                    return ToSqlValue((bool)value);

                //case "SqlLiteral":
                //    return ((Zek.Data.QueryBuilder.SqlLiteral)value).Value;
                //    break;
                case "Byte":
                    return ToSqlValue((byte)value);

                case "SByte":
                    return ToSqlValue((sbyte)value);

                case "Int16":
                    return ToSqlValue((short)value);

                case "UInt16":
                    return ToSqlValue((ushort)value);

                case "Int32":
                    return ToSqlValue((int)value);

                case "UInt32":
                    return ToSqlValue((uint)value);

                case "Int64":
                    return ToSqlValue((long)value);

                case "UInt64":
                    return ToSqlValue((ulong)value);

                case "Decimal":
                    return ToSqlValue((decimal)value);

                case "Double":
                    return ToSqlValue((double)value);

                case "Single":
                    return ToSqlValue((float)value);

                case "Object[]":
                case "String[]":
                case "DateTime[]":
                case "Guid[]":
                case "Boolean[]":
                case "SqlLiteral[]":
                case "Decimal[]":
                case "Double[]":
                case "Single[]":
                case "Byte[]":
                case "Int16[]":
                case "Int32[]":
                case "Int64[]":
                case "Nullable`1[]":
                    var sbArray = new StringBuilder();
                    foreach (var item in (Array)value)
                    {
                        sbArray.Append(ToSqlValue(item) + ", ");
                    }
                    return sbArray.Remove(sbArray.Length - 2, 2).ToString();

                case "List`1":
                    var sbList = new StringBuilder();
                    foreach (var item in (IList)value)
                    {
                        sbList.Append(ToSqlValue(item) + ", ");
                    }
                    if (sbList.Length > 0)
                        sbList.Remove(sbList.Length - 2, 2);
                    return sbList.ToString();

                default:
                    return value.ToString();
            }
        }
        /// <summary>
        /// აფორმატირებს მნიშვნელობას String-ში, ისე რომ Sql-თან არ შეიქმნას პრობლემა.
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომლის დაფორმატირებაც გვინდა.</param>
        /// <returns>დაფორმატირებული String.</returns>
        public static string ToEntitySqlValue(object value)
        {
            var formattedValue = string.Empty;
            //string StringType = Type.GetType("string").Name;
            //string DateTimeType = Type.GetType("DateTime").Name;

            if (value == null)
            {
                formattedValue = "NULL";
            }
            else
            {
                switch (value.GetType().Name)
                {
                    case "String":
                    case "Char":
                        formattedValue = "N'" + ((string)value).Replace("'", "''") + "'";
                        break;

                    case "DateTime":
                        formattedValue = "DATETIME'" + ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
                        break;

                    case "Guid":
                        formattedValue = "GUID'" + value + "'";
                        break;

                    case "DBNull":
                        formattedValue = "NULL";
                        break;

                    case "Boolean":
                        formattedValue = (bool)value ? "1" : "0";
                        break;

                    case "Byte":
                        formattedValue = ((byte)value).ToString(null, NumberFormatInfo.InvariantInfo);
                        break;
                    case "SByte":
                        formattedValue = ((sbyte)value).ToString(null, NumberFormatInfo.InvariantInfo);
                        break;

                    case "Int16":
                        formattedValue = ((short)value).ToString(null, NumberFormatInfo.InvariantInfo);
                        break;
                    case "UInt16":
                        formattedValue = ((ushort)value).ToString(null, NumberFormatInfo.InvariantInfo);
                        break;

                    case "Int32":
                        formattedValue = ((int)value).ToString(null, NumberFormatInfo.InvariantInfo);
                        break;
                    case "UInt32":
                        formattedValue = ((uint)value).ToString(null, NumberFormatInfo.InvariantInfo);
                        break;

                    case "Int64":
                        formattedValue = ((long)value).ToString(null, NumberFormatInfo.InvariantInfo);
                        break;
                    case "UInt64":
                        formattedValue = ((ulong)value).ToString(null, NumberFormatInfo.InvariantInfo);
                        break;

                    //case "SqlLiteral":
                    //    formattedValue = ((Zek.Data.QueryBuilder.SqlLiteral)value).Value;
                    //    break;

                    case "Decimal":
                        formattedValue = ((decimal)value).ToString(null, NumberFormatInfo.InvariantInfo);
                        break;
                    case "Double":
                        formattedValue = ((double)value).ToString("R", NumberFormatInfo.InvariantInfo);
                        break;
                    case "Single":
                        formattedValue = ((float)value).ToString("R", NumberFormatInfo.InvariantInfo);
                        break;

                    case "Object[]":
                    case "String[]":
                    case "DateTime[]":
                    case "Guid[]":
                    case "Boolean[]":
                    case "SqlLiteral[]":
                    case "Decimal[]":
                    case "Double[]":
                    case "Single[]":
                    case "Byte[]":
                    case "Int16[]":
                    case "Int32[]":
                    case "Int64[]":
                        foreach (var item in (Array)value)
                        {
                            formattedValue += ToEntitySqlValue(item) + ", ";
                        }
                        formattedValue = formattedValue.Remove(formattedValue.Length - ", ".Length);
                        break;

                    default:
                        formattedValue = value.ToString();
                        break;
                }
            }
            return formattedValue ?? string.Empty;
        }

    }
}
