using System;
using System.Collections.Generic;

namespace Zek.Utils
{
    public static class DateTimeHelper
    {
        public static bool Overlap(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return (start1 >= start2 && start1 <= end2)
                || (end1 >= start2 && end1 <= end2)
                || (start1 <= start2 && end1 >= end2);
        }
        public static bool NotOverlap(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return (start1 > end2) || (end1 < start2);
        }

        public static int DaysInMonth(int year, int month) => DateTime.DaysInMonth(year, month);


        public static int DaysInYear(int year) => DateTime.IsLeapYear(year) ? 366 : 365;

        public static IEnumerable<DateRange> SplitDateRangeByMinutes(DateTime start, DateTime end, int minutes) => SplitDateRange(start, end, TimeSpan.FromMinutes(minutes));

        public static IEnumerable<DateRange> SplitDateRange(DateTime start, DateTime end, TimeSpan chunk)
        {
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