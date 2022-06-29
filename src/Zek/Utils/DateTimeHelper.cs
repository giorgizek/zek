using System;
using System.Collections.Generic;

namespace Zek.Utils
{
    public static class DateTimeHelper
    {
        public static bool HasInside(DateTime start, DateTime end, DateTime date)
        {
            return date >= start && date <= end;
        }

        public static bool HasInside(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return HasInside(start1, end1, start2) && HasInside(start1, end1, end2);
        }

        public static bool Intersects(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return
                HasInside(start1, end1, start2) ||
                HasInside(start1, end1, end2) ||
                (start2 < start1 && end2 > end1);
        }

        public static bool Overlaps(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            var relation = GetRelation(start1, end1, start2, end2);
            return
                relation != PeriodRelation.After &&
                relation != PeriodRelation.StartTouching &&
                relation != PeriodRelation.EndTouching &&
                relation != PeriodRelation.Before;
        }

        public static PeriodRelation GetRelation(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            if (end2 < start1)
            {
                return PeriodRelation.After;
            }
            if (start2 > end1)
            {
                return PeriodRelation.Before;
            }
            if (start2 == start1 && end2 == end1)
            {
                return PeriodRelation.ExactMatch;
            }
            if (end2 == start1)
            {
                return PeriodRelation.StartTouching;
            }
            if (start2 == end1)
            {
                return PeriodRelation.EndTouching;
            }
            if (HasInside(start1, end1, start2, end2))
            {
                if (start2 == start1)
                {
                    return PeriodRelation.EnclosingStartTouching;
                }
                return end2 == end1 ? PeriodRelation.EnclosingEndTouching : PeriodRelation.Enclosing;
            }
            var periodContainsMyStart = HasInside(start2, end2, start1);
            var periodContainsMyEnd = HasInside(start2, end2, end1);
            if (periodContainsMyStart && periodContainsMyEnd)
            {
                if (start2 == start1)
                {
                    return PeriodRelation.InsideStartTouching;
                }
                return end2 == end1 ? PeriodRelation.InsideEndTouching : PeriodRelation.Inside;
            }
            if (periodContainsMyStart)
            {
                return PeriodRelation.StartInside;
            }
            if (periodContainsMyEnd)
            {
                return PeriodRelation.EndInside;
            }
            throw new InvalidOperationException("invalid period relation of '" + start1 + "-" + end1 + "' and '" + start2 + "-" + end2 + "'");
        }




        public static bool HasInside(TimeSpan start, TimeSpan end, TimeSpan date)
        {
            return date >= start && date <= end;
        }
     
        public static bool HasInside(TimeSpan start1, TimeSpan end1, TimeSpan start2, TimeSpan end2)
        {
            return HasInside(start1, end1, start2) && HasInside(start1, end1, end2);
        }

        public static bool Intersects(TimeSpan start1, TimeSpan end1, TimeSpan start2, TimeSpan end2)
        {
            return
                HasInside(start1, end1, start2) ||
                HasInside(start1, end1, end2) ||
                (start2 < start1 && end2 > end1);
        }

        public static bool Overlaps(TimeSpan start1, TimeSpan end1, TimeSpan start2, TimeSpan end2)
        {
            var relation = GetRelation(start1, end1, start2, end2);
            return
                relation != PeriodRelation.After &&
                relation != PeriodRelation.StartTouching &&
                relation != PeriodRelation.EndTouching &&
                relation != PeriodRelation.Before;
        }

        public static PeriodRelation GetRelation(TimeSpan start1, TimeSpan end1, TimeSpan start2, TimeSpan end2)
        {
            if (end2 < start1)
            {
                return PeriodRelation.After;
            }
            if (start2 > end1)
            {
                return PeriodRelation.Before;
            }
            if (start2 == start1 && end2 == end1)
            {
                return PeriodRelation.ExactMatch;
            }
            if (end2 == start1)
            {
                return PeriodRelation.StartTouching;
            }
            if (start2 == end1)
            {
                return PeriodRelation.EndTouching;
            }
            if (HasInside(start1, end1, start2, end2))
            {
                if (start2 == start1)
                {
                    return PeriodRelation.EnclosingStartTouching;
                }
                return end2 == end1 ? PeriodRelation.EnclosingEndTouching : PeriodRelation.Enclosing;
            }
            var periodContainsMyStart = HasInside(start2, end2, start1);
            var periodContainsMyEnd = HasInside(start2, end2, end1);
            if (periodContainsMyStart && periodContainsMyEnd)
            {
                if (start2 == start1)
                {
                    return PeriodRelation.InsideStartTouching;
                }
                return end2 == end1 ? PeriodRelation.InsideEndTouching : PeriodRelation.Inside;
            }
            if (periodContainsMyStart)
            {
                return PeriodRelation.StartInside;
            }
            if (periodContainsMyEnd)
            {
                return PeriodRelation.EndInside;
            }
            throw new InvalidOperationException("invalid period relation of '" + start1 + "-" + end1 + "' and '" + start2 + "-" + end2 + "'");
        }



        [Obsolete("Use Overlaps or Intersects instead")]
        public static bool Overlap(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return (start1 >= start2 && start1 <= end2)
                || (end1 >= start2 && end1 <= end2)
                || (start1 <= start2 && end1 >= end2);
        }

        [Obsolete("Use !Overlaps instead")]
        public static bool NotOverlaps(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
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