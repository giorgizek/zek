using System;

namespace Zek.Utils
{
    public class Range<T>
    {
        public Range()
        {
        }

        public Range(T start, T end)
        {
            Start = start;
            End = end;
        }
        public T Start { get; set; }
        public T End { get; set; }
    }

    public class DateRange : Range<DateTime>
    {
        public DateRange()
        {
        }

        public DateRange(DateTime start, DateTime end) : base(start, end)
        {
        }
    }
}