using System;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        //public override string ToString()
        //{
        //    var builder = new StringBuilder();
        //    builder.Append('[');
        //    if (Start != null)
        //    {
        //        builder.Append(Start);
        //    }
        //    builder.Append(", ");
        //    if (End != null)
        //    {
        //        builder.Append(End);
        //    }
        //    builder.Append(']');
        //    return builder.ToString();
        //}
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