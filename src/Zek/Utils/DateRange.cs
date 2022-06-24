using System;

namespace Zek.Utils
{
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