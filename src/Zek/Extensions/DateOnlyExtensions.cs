using System.Globalization;
using Zek.Utils;

namespace Zek.Extensions
{
    /// <summary>
    /// Provides extension methods for working with <see cref="DateOnly" /> values.
    /// </summary>
    public static class DateOnlyExtensions
    {
        /// <summary>
        /// Converts the date to a string using the <c>yyyy-MM-dd</c> format.
        /// </summary>
        /// <param name="date">The date to format.</param>
        /// <returns>A date string such as <c>1984-09-17</c>.</returns>
        public static string ToUniversalDateString(this DateOnly date) => date.ToString(DateTimeHelper.UniversalDateFormat);

        /// <summary>
        /// Returns the first day of the month for the specified date.
        /// </summary>
        /// <param name="date">The source date.</param>
        /// <returns>A <see cref="DateOnly" /> representing the first day of the month.</returns>
        public static DateOnly GetStartOfMonth(this DateOnly date) =>
            new(date.Year, date.Month, 1);

        /// <summary>
        /// Returns the last day of the month for the specified date.
        /// </summary>
        /// <param name="date">The source date.</param>
        /// <returns>A <see cref="DateOnly" /> representing the last day of the month.</returns>
        public static DateOnly GetEndOfMonth(this DateOnly date) =>
            new(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

        /// <summary>
        /// Returns the first day of the year for the specified date.
        /// </summary>
        /// <param name="date">The source date.</param>
        /// <returns>A <see cref="DateOnly" /> representing the first day of the year.</returns>
        public static DateOnly GetStartOfYear(this DateOnly date) =>
            new(date.Year, 1, 1);

        /// <summary>
        /// Returns the last day of the year for the specified date.
        /// </summary>
        /// <param name="date">The source date.</param>
        /// <returns>A <see cref="DateOnly" /> representing the last day of the year.</returns>
        public static DateOnly GetEndOfYear(this DateOnly date) =>
            new(date.Year, 12, 31);

        /// <summary>
        /// Returns the first day of the month for the specified nullable date.
        /// </summary>
        /// <param name="date">The source date.</param>
        /// <returns>
        /// A <see cref="DateOnly" /> representing the first day of the month, or <see langword="null" /> if the value is <see langword="null" />.
        /// </returns>
        public static DateOnly? GetStartOfMonth(this DateOnly? date) => date != null ? date.Value.GetStartOfMonth() : null;

        /// <summary>
        /// Returns the last day of the month for the specified nullable date.
        /// </summary>
        /// <param name="date">The source date.</param>
        /// <returns>
        /// A <see cref="DateOnly" /> representing the last day of the month, or <see langword="null" /> if the value is <see langword="null" />.
        /// </returns>
        public static DateOnly? GetEndOfMonth(this DateOnly? date) => date != null ? date.Value.GetEndOfMonth() : null;

        /// <summary>
        /// Returns the first day of the year for the specified nullable date.
        /// </summary>
        /// <param name="date">The source date.</param>
        /// <returns>
        /// A <see cref="DateOnly" /> representing the first day of the year, or <see langword="null" /> if the value is <see langword="null" />.
        /// </returns>
        public static DateOnly? GetStartOfYear(this DateOnly? date) => date != null ? date.Value.GetStartOfYear() : null;

        /// <summary>
        /// Returns the last day of the year for the specified nullable date.
        /// </summary>
        /// <param name="date">The source date.</param>
        /// <returns>
        /// A <see cref="DateOnly" /> representing the last day of the year, or <see langword="null" /> if the value is <see langword="null" />.
        /// </returns>
        public static DateOnly? GetEndOfYear(this DateOnly? date) => date != null ? date.Value.GetEndOfYear() : null;

        /// <summary>
        /// Returns the number of months between two dates.
        /// </summary>
        /// <param name="endDate">The ending date.</param>
        /// <param name="startDate">The starting date.</param>
        /// <param name="round">
        /// If set to <see langword="true" />, rounds the result by adding or subtracting one month when the day difference is at least 27 days.
        /// </param>
        /// <returns>The number of months between <paramref name="startDate"/> and <paramref name="endDate"/>.</returns>
        public static int SubtractMonth(this DateOnly endDate, DateOnly startDate, bool round = false)
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
        /// Returns the age calculated from the specified birth date.
        /// </summary>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="now">The current date used for the calculation. If <see langword="null" />, the current system date is used.</param>
        /// <returns>The calculated age.</returns>
        public static int GetAge(this DateOnly birthDate, DateOnly? now = null)
        {
            now ??= DateOnly.FromDateTime(DateTime.Now);

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
        /// Returns <see langword="null" /> if the value is <see langword="null" /> or equals the specified default value.
        /// </summary>
        /// <param name="value">The source value.</param>
        /// <param name="defaultValue">The value to compare against.</param>
        /// <returns>
        /// <see langword="null" /> if <paramref name="value"/> is <see langword="null" /> or equals <paramref name="defaultValue"/>; otherwise, the original value.
        /// </returns>
        public static DateOnly? NullIfDefault(this DateOnly? value, DateOnly defaultValue = default)
        {
            return value == null || value == defaultValue ? null : value;
        }

        /// <summary>
        /// Returns the specified default value if the source value is <see langword="null" />.
        /// </summary>
        /// <param name="value">The source value.</param>
        /// <param name="defaultValue">The value to return when <paramref name="value"/> is <see langword="null" />.</param>
        /// <returns><paramref name="value"/> if it has a value; otherwise, <paramref name="defaultValue"/>.</returns>
        public static DateOnly DefaultIfNull(this DateOnly? value, DateOnly defaultValue = default)
        {
            return value ?? defaultValue;
        }

        #region Business

        /// <summary>
        /// Determines whether the specified date falls on a weekend.
        /// </summary>
        /// <param name="date">The date to evaluate.</param>
        /// <returns><see langword="true" /> if the date is Saturday or Sunday; otherwise, <see langword="false" />.</returns>
        public static bool IsWeekEnd(this DateOnly date)
        {
            return date.DayOfWeek switch
            {
                DayOfWeek.Sunday or DayOfWeek.Saturday => true,
                _ => false,
            };
        }

        /// <summary>
        /// Returns the number of days in the month of the specified nullable date.
        /// </summary>
        /// <param name="date">The source date.</param>
        /// <returns>The number of days in the month, or <c>0</c> if <paramref name="date"/> is <see langword="null" />.</returns>
        public static int DaysInMonth(this DateOnly? date)
        {
            if (date.HasValue)
            {
                return DateTime.DaysInMonth(date.Value.Year, date.Value.Month);
            }

            return 0;
        }

        /// <summary>
        /// Returns the number of days in the month of the specified date.
        /// </summary>
        /// <param name="date">The source date.</param>
        /// <returns>The number of days in the month.</returns>
        public static int DaysInMonth(this DateOnly date) => DateTime.DaysInMonth(date.Year, date.Month);

        /// <summary>
        /// Returns the number of days in the year of the specified date.
        /// </summary>
        /// <param name="date">The source date.</param>
        /// <returns><c>366</c> for a leap year; otherwise, <c>365</c>.</returns>
        public static int DaysInYear(this DateOnly date) => DateTime.IsLeapYear(date.Year) ? 366 : 365;

        /// <summary>
        /// Returns the number of days in the year of the specified nullable date.
        /// </summary>
        /// <param name="date">The source date.</param>
        /// <returns><c>366</c> for a leap year, <c>365</c> for a common year, or <c>0</c> if <paramref name="date"/> is <see langword="null" />.</returns>
        public static int DaysInYear(this DateOnly? date)
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
        /// <param name="start">The starting date, inclusive.</param>
        /// <param name="end">The ending date, exclusive.</param>
        /// <returns>An array containing each date from <paramref name="start"/> up to, but not including, <paramref name="end"/>.</returns>
        public static DateOnly[] GetDates(this DateOnly start, DateOnly end)
        {
            var dates = new List<DateOnly>();
            while (start < end)
            {
                dates.Add(start);
                start = start.AddDays(1);
            }

            return dates.ToArray();
        }

        /// <summary>
        /// Adds the specified number of business days to the date.
        /// </summary>
        /// <param name="date">The base date.</param>
        /// <param name="businessDays">The number of business days to add. Negative values are supported.</param>
        /// <returns>The resulting business date.</returns>
        public static DateOnly AddBusinessDays(this DateOnly date, int businessDays)
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
        /// Adds the specified number of days and moves the result to Monday if it falls on a weekend.
        /// </summary>
        /// <param name="date">The base date.</param>
        /// <param name="days">The number of days to add.</param>
        /// <returns>The resulting date, adjusted to Monday when necessary.</returns>
        public static DateOnly AddDaysUntilMonday(this DateOnly date, int days)
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
        /// Returns the number of business days within the specified range.
        /// </summary>
        /// <param name="start">The starting date.</param>
        /// <param name="end">The ending date.</param>
        /// <returns>The number of business days between the two dates.</returns>
        public static int GetBusinessDays(this DateOnly start, DateOnly end)
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
        /// Returns the number of weekend days within the specified range.
        /// </summary>
        /// <param name="start">The starting date.</param>
        /// <param name="end">The ending date.</param>
        /// <returns>The number of weekend days between the two dates.</returns>
        public static int GetWeekEndDays(this DateOnly start, DateOnly end)
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
        /// Returns an array of business dates within the specified range.
        /// </summary>
        /// <param name="start">The starting date.</param>
        /// <param name="end">The ending date.</param>
        /// <returns>An array containing business dates between the two dates.</returns>
        public static DateOnly[] GetBusinessDates(this DateOnly start, DateOnly end)
        {
            var businessDates = new List<DateOnly>();
            while (start < end)
            {
                start = start.AddDays(1);
                if (!IsWeekEnd(start))
                    businessDates.Add(start);
            }

            return businessDates.ToArray();
        }

        /// <summary>
        /// Returns an array of weekend dates within the specified range.
        /// </summary>
        /// <param name="start">The starting date.</param>
        /// <param name="end">The ending date.</param>
        /// <returns>An array containing weekend dates between the two dates.</returns>
        public static DateOnly[] GetWeekEndDates(this DateOnly start, DateOnly end)
        {
            var weekEndDates = new List<DateOnly>();
            while (start < end)
            {
                start = start.AddDays(1);
                if (IsWeekEnd(start))
                    weekEndDates.Add(start);
            }

            return [.. weekEndDates];
        }

        #endregion
    }
}