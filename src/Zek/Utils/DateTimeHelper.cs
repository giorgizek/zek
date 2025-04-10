namespace Zek.Utils
{
    public static class DateTimeHelper
    {
        public const string UniversalDateFormat = "yyyy-MM-dd";
        public const string UniversalDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public const string Rfc3339Format = "yyyy-MM-dd'T'HH:mm:ss.fffK";

        //public const string UniversalDateTimeMillisecondFormat = "yyyy-MM-dd HH:mm:ss.fff";
        public static readonly DateTime MinDbDate = new(1900, 1, 1);
        public static readonly DateTime MaxDbDate = new(9000, 12, 31);


        /// <summary>
        /// Add a DateTime and a TimeSpan.
        /// The maximum time is DateTime.MaxTime.  It is not an error if time + timespan > MaxTime.
        /// Just return MaxTime.
        /// </summary>
        /// <param name="time">Initial <see cref="DateTime"/> value.</param>
        /// <param name="timespan"><see cref="TimeSpan"/> to add.</param>
        /// <returns><see cref="DateTime"/> as the sum of time and timespan.</returns>
        public static DateTime Add(DateTime time, TimeSpan timespan)
        {
            if (timespan == TimeSpan.Zero)
            {
                return time;
            }

            if (timespan > TimeSpan.Zero && DateTime.MaxValue - time <= timespan)
            {
                return GetMaxValue(time.Kind);
            }

            if (timespan < TimeSpan.Zero && DateTime.MinValue - time >= timespan)
            {
                return GetMinValue(time.Kind);
            }

            return time + timespan;
        }


        /// <summary>
        /// Gets the Maximum value for a DateTime specifying kind.
        /// </summary>
        /// <param name="kind">DateTimeKind to use.</param>
        /// <returns>DateTime of specified kind.</returns>
        public static DateTime GetMaxValue(DateTimeKind kind)
        {
            if (kind == DateTimeKind.Unspecified)
                return new DateTime(DateTime.MaxValue.Ticks, DateTimeKind.Utc);

            return new DateTime(DateTime.MaxValue.Ticks, kind);
        }


        /// <summary>
        /// Gets the Minimum value for a DateTime specifying kind.
        /// </summary>
        /// <param name="kind">DateTimeKind to use.</param>
        /// <returns>DateTime of specified kind.</returns>
        public static DateTime GetMinValue(DateTimeKind kind)
        {
            if (kind == DateTimeKind.Unspecified)
                return new DateTime(DateTime.MinValue.Ticks, DateTimeKind.Utc);

            return new DateTime(DateTime.MinValue.Ticks, kind);
        }


        /// <summary>
        /// Ensures that DataTime is UTC.
        /// </summary>
        /// <param name="value"><see cref="DateTime"/>to convert.</param>
        /// <returns></returns>
        public static DateTime? ToUniversalTime(DateTime? value)
        {
            if (value == null || value.Value.Kind == DateTimeKind.Utc)
                return value;

            return ToUniversalTime(value.Value);
        }

        /// <summary>
        /// Ensures that DateTime is UTC.
        /// </summary>
        /// <param name="value"><see cref="DateTime"/>to convert.</param>
        /// <returns></returns>
        public static DateTime ToUniversalTime(DateTime value)
        {

            if (value.Kind == DateTimeKind.Utc)
                return value;

            return value.ToUniversalTime();
        }

        public static int DaysInMonth(int year, int month) => DateTime.DaysInMonth(year, month);

        public static int DaysInYear(int year) => DateTime.IsLeapYear(year) ? 366 : 365;

        public static IEnumerable<DateRange> SplitDateRangeByMinutes(DateTime start, DateTime end, int minutes) => SplitDateRange(start, end, TimeSpan.FromMinutes(minutes));

        public static IEnumerable<DateRange> SplitDateRange(DateTime start, DateTime end, TimeSpan chunk)
        {
            if (chunk <= TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(chunk), "chunk should be greater than zero");

            DateTime chunkEnd;
            while ((chunkEnd = start.Add(chunk)) < end)
            {
                yield return new DateRange(start, chunkEnd);
                start = chunkEnd;
            }
            yield return new DateRange(start, end);
        }

        public static IEnumerable<(DateTime, DateTime)> SplitTupleByMinutes(DateTime start, DateTime end, int minutes) => SplitTuple(start, end, TimeSpan.FromMinutes(minutes));
        public static IEnumerable<(DateTime, DateTime)> SplitTuple(DateTime start, DateTime end, TimeSpan chunk)
        {
            DateTime chunkEnd;
            while ((chunkEnd = start.Add(chunk)) < end)
            {
                yield return (start, chunkEnd);
                start = chunkEnd;
            }
            yield return (start, end);
        }
    }
}