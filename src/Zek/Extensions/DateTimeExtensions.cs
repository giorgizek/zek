using System;
using System.Collections.Generic;
using System.Globalization;

namespace Zek.Extensions
{
    public static class DateTimeExtensions
    {
        public const string UniversalDateFormat = "yyyy-MM-dd";
        public const string UniversalDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public const string Rfc3339Format = "yyyy-MM-dd'T'HH:mm:ss.fffK";

        //public const string UniversalDateTimeMillisecondFormat = "yyyy-MM-dd HH:mm:ss.fff";
        public static readonly DateTime MinDbDate = new(1900, 1, 1);
        public static readonly DateTime MaxDbDate = new(9000, 12, 31);


        public static string ToUniversalDateString(this DateTime date) => date.ToString(UniversalDateFormat);
        public static string ToUniversalDateTimeString(this DateTime date) => date.ToString(UniversalDateTimeFormat);


        public static string ToRfc3339String(this DateTime? date)
        {
            return date?.ToRfc3339String();
        }

        public static string ToRfc3339String(this DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
            {
                date = date.ToUniversalTime();
            }
           return date.ToString(Rfc3339Format, DateTimeFormatInfo.InvariantInfo);
        }

        public static long ToJavaScriptMilliseconds(this DateTime date) => (long)date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

        public static DateTime GetStartOfSecond(this DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);

        public static DateTime GetEndOfSecond(this DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 999);

        public static DateTime GetStartOfMinute(this DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);

        public static DateTime GetEndOfMinute(this DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, date.Minute, 59, 999);

        public static DateTime GetStartOfHour(this DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, 0, 0);

        public static DateTime GetEndOfHour(this DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, 59, 59, 999);

        public static DateTime GetStartOfDay(this DateTime date) => new(date.Year, date.Month, date.Day, 0, 0, 0, 0);

        public static DateTime GetEndOfDay(this DateTime date) => new(date.Year, date.Month, date.Day, 23, 59, 59, 999);

        public static DateTime GetStartOfMonth(this DateTime date) => new(date.Year, date.Month, 1, 0, 0, 0, 0);

        public static DateTime GetEndOfMonth(this DateTime date) => new(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999);

        public static DateTime GetStartOfYear(this DateTime date) => new(date.Year, 1, 1, 0, 0, 0, 0);

        public static DateTime GetEndOfYear(this DateTime date) => new(date.Year, 12, 31, 23, 59, 59, 999);


        public static DateTime? GetStartOfSecond(this DateTime? date) => date != null ? (DateTime?)GetStartOfSecond(date.Value) : null;

        public static DateTime? GetEndOfSecond(this DateTime? date) => date != null ? (DateTime?)GetEndOfSecond(date.Value) : null;

        public static DateTime? GetStartOfMinute(this DateTime? date) => date != null ? (DateTime?)GetStartOfMinute(date.Value) : null;

        public static DateTime? GetEndOfMinute(this DateTime? date) => date != null ? (DateTime?)GetEndOfMinute(date.Value) : null;

        public static DateTime? GetStartOfHour(this DateTime? date) => date != null ? (DateTime?)GetStartOfHour(date.Value) : null;

        public static DateTime? GetEndOfHour(this DateTime? date) => date != null ? (DateTime?)GetEndOfHour(date.Value) : null;

        public static DateTime? GetStartOfDay(this DateTime? date) => date != null ? (DateTime?)GetStartOfDay(date.Value) : null;

        public static DateTime? GetEndOfDay(this DateTime? date) => date != null ? (DateTime?)GetEndOfDay(date.Value) : null;

        public static DateTime? GetStartOfMonth(this DateTime? date) => date != null ? (DateTime?)GetStartOfMonth(date.Value) : null;

        public static DateTime? GetEndOfMonth(this DateTime? date) => date != null ? (DateTime?)GetEndOfMonth(date.Value) : null;

        public static DateTime? GetStartOfYear(this DateTime? date) => date != null ? (DateTime?)GetStartOfYear(date.Value) : null;

        public static DateTime? GetEndOfYear(this DateTime? date) => date != null ? (DateTime?)GetEndOfYear(date.Value) : null;


        /// <summary>
        /// აბრუნებს თარიღებს შორის თვეების რაოდენობას
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="round">(დამრგვალებულს: თუ დღეების რაოდენობა 28 ან მეტია მაშინ 1 თვეს კიდე ამატებს).</param>
        /// <returns></returns>
        public static int SubtractMonth(this DateTime date1, DateTime date2, bool round = false)
        {
            return 12 * (date1.Year - date2.Year) + date1.Month - date2.Month
                + (round
                ? date1.Day - date2.Day >= 27 ? 1 : date2.Day - date1.Day >= 27 ? -1 : 0
                : 0);
        }


        /// <summary>
        /// აბრუნებს ასაკს.
        /// </summary>
        /// <param name="birthDate">დაბადების თარიღი.</param>
        /// <param name="now">მიმდინარე თარიღი.</param>
        /// <returns>ასაკი.</returns>
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
        /// Combines date with time (removes time and add time from parameter hour, min, sec).
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="time">Gets time from this parameter.</param>
        /// <returns>Returns merger date time.</returns>
        public static DateTime CombineTime(this DateTime date, DateTime time)
        {
            return CombineTime(date, time.Hour, time.Minute, time.Second);
        }
        /// <summary>
        /// უცვლის დროს (საათი, წუთი, წამი).
        /// </summary>
        /// <param name="date">თარიღი, რომელზეც გვინდა დროის შეცვლა.</param>
        /// <param name="hours">საათი.</param>
        /// <param name="minutes">წუთი.</param>
        /// <param name="seconds">წამი.</param>
        /// <returns>აბრუნებს შეცვლილ თარიღს.</returns>
        public static DateTime CombineTime(this DateTime date, int hours, int minutes, int seconds)
        {
            return date.Date.Add(new TimeSpan(hours, minutes, seconds));
        }


        public static DateTime? NullIfDefault(this DateTime? value, DateTime defaultValue = default)
        {
            return value == null || value == defaultValue ? null : value;
        }
        public static DateTime DefaultIfNull(this DateTime? value, DateTime defaultValue = default)
        {
            return value ?? defaultValue;
        }


        #region Bussiness
        /// <summary>
        /// ამოწმებს მოცემული თარიღი არის თუ არა შაბათი/კვირა.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsWeekEnd(this DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                case DayOfWeek.Saturday:
                    return true;

                default:
                    return false;
            }
        }



        public static int DaysInMonth(this DateTime? date)
        {
            if (date.HasValue)
            {
                return DateTime.DaysInMonth(date.Value.Year, date.Value.Month);
            }

            return 0;
        }

        public static int DaysInMonth(this DateTime date) => DateTime.DaysInMonth(date.Year, date.Month);
        public static int DaysInYear(this DateTime date) => DateTime.IsLeapYear(date.Year) ? 366 : 365;
        public static int DaysInYear(this DateTime? date)
        {
            if (date.HasValue)
            {
                return DateTime.IsLeapYear(date.Value.Year) ? 366 : 365;
            }
            return 0;
        }


        /// <summary>
        /// ამატებს სამუშაო დღეებს.
        /// </summary>
        /// <param name="date">ათვლის თარიღი.</param>
        /// <param name="businessDays">სამუშაო დღეების რაოდენობა.</param>
        /// <returns>აბრუნებს სამუშაო დღის თარიღს.</returns>
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
        /// ამატებს დღეებს და თუ მოუწია შაბათი ან კვირა მაშინ ორშაბათამდე გადის.
        /// </summary>
        /// <param name="date">ათვლის თარიღი.</param>
        /// <param name="days">დღეების რაოდენობა.</param>
        /// <returns>აბრუნებს დამატებულ დღეებს ბოლო შაბათ-კვირის გადახტომით.</returns>
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
        /// აბრუნებს სამუშაო დღეების რაოდენობას შუალედში.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>აბრუნებს სამუშაო დღეების რაოდენობას.</returns>
        public static int GetBusinessDays(this DateTime from, DateTime to)
        {
            var businessDays = 0;
            while (from < to)
            {
                from = from.AddDays(1);
                if (!IsWeekEnd(from))
                    businessDays += 1;
            }
            return businessDays;
        }
        /// <summary>
        /// აბრუნებს დასვენების დღეების რაოდენობას შუალედში.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>აბრუნებს დასვენების დღეების რაოდენობას.</returns>
        public static int GetWeekEndDays(this DateTime from, DateTime to)
        {
            var weekEndDays = 0;
            while (from < to)
            {
                from = from.AddDays(1);
                if (IsWeekEnd(from))
                    weekEndDays += 1;
            }
            return weekEndDays;
        }
        /// <summary>
        /// აბრუნებს სამუშაო დღეების მასივს მოცემულ შუალედში.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>აბრუნებს სამუშაო დღეების მასივს.</returns>
        public static DateTime[] GetBusinessDates(this DateTime from, DateTime to)
        {
            from = from.Date;
            to = to.Date;
            var businessDates = new List<DateTime>();
            while (from < to)
            {
                from = from.AddDays(1d);
                if (!IsWeekEnd(from))
                    businessDates.Add(from);
            }

            return businessDates.ToArray();
        }
        /// <summary>
        /// აბრუნებს დასვენების დღეების მასივს მოცემულ შუალედში.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>აბრუნებს დასვენების დღეების მასივს.</returns>
        public static DateTime[] GetWeekEndDates(this DateTime from, DateTime to)
        {
            from = from.Date;
            to = to.Date;
            var weekEndDates = new List<DateTime>();
            while (from < to)
            {
                from = from.AddDays(1d);
                if (IsWeekEnd(from))
                    weekEndDates.Add(from);
            }

            return weekEndDates.ToArray();
        }
        #endregion
    }
}
