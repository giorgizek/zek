using System.Globalization;
using Microsoft.IdentityModel.Tokens;
using Zek.Utils;

namespace Zek.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts a date to a string in the format <c>yyyy-MM-dd</c>.
        /// </summary>
        /// <param name="date">The date to convert.</param>
        /// <returns>A date string, for example <c>1984-09-17</c>.</returns>
        public static string ToUniversalDateString(this DateTime date) => date.ToString(DateTimeHelper.UniversalDateFormat);

        /// <summary>
        /// Converts a date to a string in the format <c>yyyy-MM-dd HH:mm:ss</c>.
        /// </summary>
        /// <param name="date">The date to convert.</param>
        /// <returns>A date-time string, for example <c>1984-09-17 15:30:00</c>.</returns>
        public static string ToUniversalDateTimeString(this DateTime date) => date.ToString(DateTimeHelper.UniversalDateTimeFormat);

        /// <summary>
        /// Converts a nullable date to an RFC 3339 string.
        /// </summary>
        /// <param name="date">The date to convert.</param>
        /// <returns>An RFC 3339 string, or <see langword="null"/> if <paramref name="date"/> is <see langword="null"/>.</returns>
        public static string? ToRfc3339String(this DateTime? date)
        {
            return date?.ToRfc3339String();
        }

        /// <summary>
        /// Converts a date to an RFC 3339 string.
        /// </summary>
        /// <param name="date">The date to convert.</param>
        /// <returns>An RFC 3339 formatted string.</returns>
        public static string ToRfc3339String(this DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
            {
                date = date.ToUniversalTime();
            }
            return date.ToString(DateTimeHelper.Rfc3339Format, DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Converts a date to Unix time in milliseconds.
        /// </summary>
        /// <param name="date">The date to convert.</param>
        /// <returns>The number of milliseconds since the Unix epoch.</returns>
        public static long ToJavaScriptMilliseconds(this DateTime date) => (long)date.ToUniversalTime().Subtract(EpochTime.UnixEpoch).TotalMilliseconds;

        /// <summary>
        /// Returns the same date rounded down to the start of the second.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The start of the second.</returns>
        public static DateTime GetStartOfSecond(this DateTime date) =>
            new(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);

        /// <summary>
        /// Returns the same date rounded up to the end of the second.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The end of the second.</returns>
        public static DateTime GetEndOfSecond(this DateTime date) =>
            new(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 999, date.Kind);

        /// <summary>
        /// Returns the same date rounded down to the start of the minute.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The start of the minute.</returns>
        public static DateTime GetStartOfMinute(this DateTime date) =>
            new(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, date.Kind);

        /// <summary>
        /// Returns the same date rounded up to the end of the minute.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The end of the minute.</returns>
        public static DateTime GetEndOfMinute(this DateTime date) =>
            new(date.Year, date.Month, date.Day, date.Hour, date.Minute, 59, 999, date.Kind);

        /// <summary>
        /// Returns the same date rounded down to the start of the hour.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The start of the hour.</returns>
        public static DateTime GetStartOfHour(this DateTime date) =>
            new(date.Year, date.Month, date.Day, date.Hour, 0, 0, date.Kind);

        /// <summary>
        /// Returns the same date rounded up to the end of the hour.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The end of the hour.</returns>
        public static DateTime GetEndOfHour(this DateTime date) =>
            new(date.Year, date.Month, date.Day, date.Hour, 59, 59, 999, date.Kind);

        /// <summary>
        /// Returns the start of the day for the specified date.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The start of the day.</returns>
        public static DateTime GetStartOfDay(this DateTime date) =>
            new(date.Year, date.Month, date.Day, 0, 0, 0, 0, date.Kind);

        /// <summary>
        /// Returns the end of the day for the specified date.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The end of the day.</returns>
        public static DateTime GetEndOfDay(this DateTime date) =>
            new(date.Year, date.Month, date.Day, 23, 59, 59, 999, date.Kind);

        /// <summary>
        /// Returns the start of the month for the specified date.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The start of the month.</returns>
        public static DateTime GetStartOfMonth(this DateTime date) =>
            new(date.Year, date.Month, 1, 0, 0, 0, 0, date.Kind);

        /// <summary>
        /// Returns the end of the month for the specified date.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The end of the month.</returns>
        public static DateTime GetEndOfMonth(this DateTime date) =>
            new(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999, date.Kind);

        /// <summary>
        /// Returns the start of the year for the specified date.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The start of the year.</returns>
        public static DateTime GetStartOfYear(this DateTime date) =>
            new(date.Year, 1, 1, 0, 0, 0, 0, date.Kind);

        /// <summary>
        /// Returns the end of the year for the specified date.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The end of the year.</returns>
        public static DateTime GetEndOfYear(this DateTime date) =>
            new(date.Year, 12, 31, 23, 59, 59, 999, date.Kind);


        /// <summary>
        /// Returns the start of the second for a nullable date.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The start of the second, or <see langword="null"/>.</returns>
        public static DateTime? GetStartOfSecond(this DateTime? date) => date != null ? date.Value.GetStartOfSecond() : null;

        /// <summary>
        /// Returns the end of the second for a nullable date.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The end of the second, or <see langword="null"/>.</returns>
        public static DateTime? GetEndOfSecond(this DateTime? date) => date != null ? date.Value.GetEndOfSecond() : null;

        /// <summary>
        /// Returns the start of the minute for a nullable date.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The start of the minute, or <see langword="null"/>.</returns>
        public static DateTime? GetStartOfMinute(this DateTime? date) => date != null ? date.Value.GetStartOfMinute() : null;

        /// <summary>
        /// Returns the end of the minute for a nullable date.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The end of the minute, or <see langword="null"/>.</returns>
        public static DateTime? GetEndOfMinute(this DateTime? date) => date != null ? date.Value.GetEndOfMinute() : null;

        /// <summary>
        /// Returns the start of the hour for a nullable date.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The start of the hour, or <see langword="null"/>.</returns>
        public static DateTime? GetStartOfHour(this DateTime? date) => date != null ? date.Value.GetStartOfHour() : null;

        /// <summary>
        /// Returns the end of the hour for a nullable date.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The end of the hour, or <see langword="null"/>.</returns>
        public static DateTime? GetEndOfHour(this DateTime? date) => date != null ? date.Value.GetEndOfHour() : null;

        /// <summary>
        /// Returns the start of the day for a nullable date.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The start of the day, or <see langword="null"/>.</returns>
        public static DateTime? GetStartOfDay(this DateTime? date) => date != null ? date.Value.GetStartOfDay() : null;

        /// <summary>
        /// Returns the end of the day for a nullable date.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The end of the day, or <see langword="null"/>.</returns>
        public static DateTime? GetEndOfDay(this DateTime? date) => date != null ? date.Value.GetEndOfDay() : null;

        /// <summary>
        /// Returns the start of the month for a nullable date.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The start of the month, or <see langword="null"/>.</returns>
        public static DateTime? GetStartOfMonth(this DateTime? date) => date != null ? date.Value.GetStartOfMonth() : null;

        /// <summary>
        /// Returns the end of the month for a nullable date.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The end of the month, or <see langword="null"/>.</returns>
        public static DateTime? GetEndOfMonth(this DateTime? date) => date != null ? date.Value.GetEndOfMonth() : null;

        /// <summary>
        /// Returns the start of the year for a nullable date.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The start of the year, or <see langword="null"/>.</returns>
        public static DateTime? GetStartOfYear(this DateTime? date) => date != null ? date.Value.GetStartOfYear() : null;

        /// <summary>
        /// Returns the end of the year for a nullable date.
        /// </summary>
        /// <param name="date">The date to adjust.</param>
        /// <returns>The end of the year, or <see langword="null"/>.</returns>
        public static DateTime? GetEndOfYear(this DateTime? date) => date != null ? date.Value.GetEndOfYear() : null;


        /// <summary>
        /// Returns the number of months between two dates.
        /// </summary>
        /// <param name="endDate">The end date.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="round">
        /// Whether to round the result. If the difference in days is 27 or more, one more month is added.
        /// </param>
        /// <returns>The number of months between the specified dates.</returns>
        public static int SubtractMonth(this DateTime endDate, DateTime startDate, bool round = false)
        {
            return 12 * (endDate.Year - startDate.Year) + endDate.Month - startDate.Month
                + (round
                ? endDate.Day - startDate.Day >= 27
                    ? 1
                    : startDate.Day - endDate.Day >= 27
                        ? -1
                        : 0
                : 0);
        }


        /// <summary>
        /// Returns the age in years.
        /// </summary>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="now">The current date.</param>
        /// <returns>The calculated age.</returns>
        public static int GetAge(this DateTime birthDate, DateTime? now = null)
        {
            now ??= DateTime.Now;

            var age = now.Value.Year - birthDate.Year;
            if (birthDate <= now)
            {
                if (now.Value.Month < birthDate.Month || now.Value.Month == birthDate.Month && now.Value.Day < birthDate.Day)
                    age--;
            }
            else
            {
                if (now.Value.Month > birthDate.Month || now.Value.Month == birthDate.Month && now.Value.Day > birthDate.Day)
                    age++;
            }
            return age;
        }




        /// <summary>
        /// Combines a date with a time value by replacing the time part of the date.
        /// </summary>
        /// <param name="date">The base date.</param>
        /// <param name="time">The time value to copy from.</param>
        /// <returns>The combined date and time.</returns>
        public static DateTime CombineTime(this DateTime date, DateTime time)
        {
            return CombineTime(date, time.Hour, time.Minute, time.Second);
        }

        /// <summary>
        /// Changes the time portion of a date.
        /// </summary>
        /// <param name="date">The date to update.</param>
        /// <param name="hours">The hour value.</param>
        /// <param name="minutes">The minute value.</param>
        /// <param name="seconds">The second value.</param>
        /// <returns>The updated date.</returns>
        public static DateTime CombineTime(this DateTime date, int hours, int minutes, int seconds)
        {
            return date.Date.Add(new TimeSpan(hours, minutes, seconds));
        }

        /// <summary>
        /// Returns <see langword="null"/> if the value is equal to the specified default value.
        /// </summary>
        /// <param name="value">The nullable date value.</param>
        /// <param name="defaultValue">The default value to compare against.</param>
        /// <returns><see langword="null"/> if the value is <see langword="null"/> or equal to <paramref name="defaultValue"/>; otherwise, the value.</returns>
        public static DateTime? NullIfDefault(this DateTime? value, DateTime defaultValue = default)
        {
            return value == null || value == defaultValue ? null : value;
        }

        /// <summary>
        /// Returns the value of a nullable date, or a default value when the value is <see langword="null"/>.
        /// </summary>
        /// <param name="value">The nullable date value.</param>
        /// <param name="defaultValue">The value to return when <paramref name="value"/> is <see langword="null"/>.</param>
        /// <returns>The value, or <paramref name="defaultValue"/>.</returns>
        public static DateTime DefaultIfNull(this DateTime? value, DateTime defaultValue = default)
        {
            return value ?? defaultValue;
        }


        #region Business
        /// <summary>
        /// Determines whether the specified date is a weekend day.
        /// </summary>
        /// <param name="date">The date to check.</param>
        /// <returns><see langword="true"/> if the date is Saturday or Sunday; otherwise, <see langword="false"/>.</returns>
        public static bool IsWeekEnd(this DateTime date)
        {
            return date.DayOfWeek switch
            {
                DayOfWeek.Sunday or DayOfWeek.Saturday => true,
                _ => false,
            };
        }



        /// <summary>
        /// Gets the number of days in the month for a nullable date.
        /// </summary>
        /// <param name="date">The date to evaluate.</param>
        /// <returns>The number of days in the month, or <c>0</c> if <paramref name="date"/> is <see langword="null"/>.</returns>
        public static int DaysInMonth(this DateTime? date)
        {
            if (date.HasValue)
            {
                return DateTime.DaysInMonth(date.Value.Year, date.Value.Month);
            }

            return 0;
        }

        /// <summary>
        /// Gets the number of days in the month for a date.
        /// </summary>
        /// <param name="date">The date to evaluate.</param>
        /// <returns>The number of days in the month.</returns>
        public static int DaysInMonth(this DateTime date) => DateTime.DaysInMonth(date.Year, date.Month);

        /// <summary>
        /// Gets the number of days in the year for a date.
        /// </summary>
        /// <param name="date">The date to evaluate.</param>
        /// <returns>The number of days in the year.</returns>
        public static int DaysInYear(this DateTime date) => DateTime.IsLeapYear(date.Year) ? 366 : 365;

        /// <summary>
        /// Gets the number of days in the year for a nullable date.
        /// </summary>
        /// <param name="date">The date to evaluate.</param>
        /// <returns>The number of days in the year, or <c>0</c> if <paramref name="date"/> is <see langword="null"/>.</returns>
        public static int DaysInYear(this DateTime? date)
        {
            if (date.HasValue)
            {
                return DateTime.IsLeapYear(date.Value.Year) ? 366 : 365;
            }
            return 0;
        }

        /// <summary>
        /// Returns an array of dates between the specified start and end dates.
        /// </summary>
        /// <param name="start">The start date.</param>
        /// <param name="end">The end date.</param>
        /// <returns>An array of dates in the range.</returns>
        public static DateTime[] GetDates(this DateTime start, DateTime end)
        {
            start = start.Date;
            end = end.Date;
            var dates = new List<DateTime>();
            while (start < end)
            {
                dates.Add(start);
                start = start.AddDays(1d);
            }

            return dates.ToArray();
        }

        /// <summary>
        /// Adds business days to a date.
        /// </summary>
        /// <param name="date">The base date.</param>
        /// <param name="businessDays">The number of business days to add.</param>
        /// <returns>The resulting date.</returns>
        public static DateTime AddBusinessDays(this DateTime date, int businessDays)
        {
            var dayOfWeek = businessDays < 0 ? ((int)date.DayOfWeek - 12) % 7 : ((int)date.DayOfWeek + 6) % 7;

            switch (dayOfWeek)
            {
                case 6:
                    businessDays--;
                    break;
                case -6:
                    businessDays++;
                    break;
            }

            return date.AddDays(businessDays + (businessDays + dayOfWeek) / 5 * 2);
        }

        /// <summary>
        /// Adds days to a date and moves the result to Monday if it falls on a weekend.
        /// </summary>
        /// <param name="date">The base date.</param>
        /// <param name="days">The number of days to add.</param>
        /// <returns>The adjusted date.</returns>
        public static DateTime AddDaysUntilMonday(this DateTime date, int days)
        {
            date = date.AddDays(days);

            if (IsWeekEnd(date))
            {
                var daysUntilMonday = DayOfWeek.Monday - date.DayOfWeek;
                if (daysUntilMonday < 0)
                    daysUntilMonday += 7;

                return date.AddDays(daysUntilMonday);
            }

            return date;
        }

        /// <summary>
        /// Returns the number of business days in the specified range.
        /// </summary>
        /// <param name="start">The start date.</param>
        /// <param name="end">The end date.</param>
        /// <returns>The number of business days.</returns>
        public static int GetBusinessDays(this DateTime start, DateTime end)
        {
            var businessDays = 0;
            while (start < end)
            {
                start = start.AddDays(1);
                if (!IsWeekEnd(start))
                    businessDays += 1;
            }
            return businessDays;
        }

        /// <summary>
        /// Returns the number of weekend days in the specified range.
        /// </summary>
        /// <param name="start">The start date.</param>
        /// <param name="end">The end date.</param>
        /// <returns>The number of weekend days.</returns>
        public static int GetWeekEndDays(this DateTime start, DateTime end)
        {
            var weekEndDays = 0;
            while (start < end)
            {
                start = start.AddDays(1);
                if (IsWeekEnd(start))
                    weekEndDays += 1;
            }
            return weekEndDays;
        }

        /// <summary>
        /// Returns an array of business dates in the specified range.
        /// </summary>
        /// <param name="start">The start date.</param>
        /// <param name="end">The end date.</param>
        /// <returns>An array of business dates.</returns>
        public static DateTime[] GetBusinessDates(this DateTime start, DateTime end)
        {
            start = start.Date;
            end = end.Date;
            var businessDates = new List<DateTime>();
            while (start < end)
            {
                start = start.AddDays(1d);
                if (!IsWeekEnd(start))
                    businessDates.Add(start);
            }

            return businessDates.ToArray();
        }

        /// <summary>
        /// Returns an array of weekend dates in the specified range.
        /// </summary>
        /// <param name="start">The start date.</param>
        /// <param name="end">The end date.</param>
        /// <returns>An array of weekend dates.</returns>
        public static DateTime[] GetWeekEndDates(this DateTime start, DateTime end)
        {
            start = start.Date;
            end = end.Date;
            var weekEndDates = new List<DateTime>();
            while (start < end)
            {
                start = start.AddDays(1d);
                if (IsWeekEnd(start))
                    weekEndDates.Add(start);
            }

            return [.. weekEndDates];
        }
        #endregion
    }
}
