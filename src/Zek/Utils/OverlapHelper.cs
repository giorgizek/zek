using System;

namespace Zek.Utils
{
    public static class OverlapHelper
    {
        public static bool HasInside(int start, int end, int date)
        {
            return date >= start && date <= end;
        }

        public static bool HasInside(int start1, int end1, int start2, int end2)
        {
            return HasInside(start1, end1, start2) && HasInside(start1, end1, end2);
        }

        public static bool Intersects(int start1, int end1, int start2, int end2)
        {
            return
                HasInside(start1, end1, start2) ||
                HasInside(start1, end1, end2) ||
                (start2 < start1 && end2 > end1);
        }

        public static bool Overlaps(int start1, int end1, int start2, int end2)
        {
            var relation = GetRelation(start1, end1, start2, end2);
            return
                relation != PeriodRelation.After &&
                relation != PeriodRelation.StartTouching &&
                relation != PeriodRelation.EndTouching &&
                relation != PeriodRelation.Before;
        }

        public static PeriodRelation GetRelation(int start1, int end1, int start2, int end2)
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




        #region DateTime

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

        #endregion






        #region TimeSpan


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

        #endregion





        #region Decimal

        public static bool HasInside(decimal start, decimal end, decimal date)
        {
            return date >= start && date <= end;
        }

        public static bool HasInside(decimal start1, decimal end1, decimal start2, decimal end2)
        {
            return HasInside(start1, end1, start2) && HasInside(start1, end1, end2);
        }

        public static bool Intersects(decimal start1, decimal end1, decimal start2, decimal end2)
        {
            return
                HasInside(start1, end1, start2) ||
                HasInside(start1, end1, end2) ||
                (start2 < start1 && end2 > end1);
        }

        public static bool Overlaps(decimal start1, decimal end1, decimal start2, decimal end2)
        {
            var relation = GetRelation(start1, end1, start2, end2);
            return
                relation != PeriodRelation.After &&
                relation != PeriodRelation.StartTouching &&
                relation != PeriodRelation.EndTouching &&
                relation != PeriodRelation.Before;
        }

        public static PeriodRelation GetRelation(decimal start1, decimal end1, decimal start2, decimal end2)
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

            throw new InvalidOperationException($"Invalid period relation of '{start1}-{end1}' and '{start2}-{end2}'");
        }

        #endregion
    }
}
