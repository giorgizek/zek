using System;

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
    }
}
