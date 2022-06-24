﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Zek.Utils
{
    public static class DateRangeHelper
    {
        public static IEnumerable<DateRange> GetFreeRanges(DateTime start, DateTime end, IEnumerable<DateRange> busySlots)
        {
            //Console.WriteLine("Work hours:");
            var ranges = new List<DateRange>()
            {
                new DateRange(start, end)
            };
            //foreach (var range in ranges)
            //    Console.WriteLine($"{range.Start:HH:mm} - {range.End:HH:mm}");
            //Console.WriteLine();

            //Console.WriteLine("Busy slots:");
            //foreach (var range in busySlots)
            //    Console.WriteLine($"{range.Start:HH:mm} - {range.End:HH:mm}");
            //Console.WriteLine();

            for (int i = 0; i < ranges.Count; i++)
            {
                foreach (var busySlot in busySlots)
                {
                    var relation = DateTimeHelper.GetRelation(busySlot.Start, busySlot.End, ranges[i].Start, ranges[i].End);

                    if (relation == PeriodRelation.EndInside || relation == PeriodRelation.InsideStartTouching)
                    {
                        ranges[i].Start = busySlot.End;
                    }
                    else if (relation == PeriodRelation.StartInside || relation == PeriodRelation.InsideEndTouching)
                    {
                        ranges[i].End = busySlot.Start;
                    }
                    else if (relation == PeriodRelation.Inside)
                    {
                        var tmp = new DateRange(busySlot.End, ranges[i].End);
                        ranges[i].End = busySlot.Start;
                        ranges.Insert(i + 1, tmp);
                        i--;
                        break;
                    }
                }
            }

            //foreach (var range in ranges)
            //{
            //    var overlap = busySlots.FirstOrDefault(x => DateTimeHelper.Overlaps(x.Start, x.End, range.Start, range.End));
            //    if (overlap != null)
            //    {

            //    }
            //    Console.WriteLine($"{range.Start:HH:mm} - {range.End:HH:mm}   overlap:{overlap != null}");
            //}

            return ranges;
        }


        public static IEnumerable<DateRange> GetFreeSlots(DateTime start, DateTime end, IEnumerable<DateRange> busySlots, int slotMinutes)
        {
            var ranges = GetFreeRanges(start, end, busySlots);
            var allFreeSlots = ranges.SelectMany(x => DateTimeHelper.SplitDateRangeByMinutes(x.Start, x.End, slotMinutes));
            //Console.WriteLine();
            //Console.WriteLine("Free slots");
            //foreach (var slot in allFreeSlots)
            //{
            //    Console.WriteLine($"{slot.Start:HH:mm} - {slot.End:HH:mm} {(slot.End - slot.Start).TotalMinutes}");
            //}

            var slots = ranges.SelectMany(x => DateTimeHelper.SplitDateRangeByMinutes(x.Start, x.End, 30).Where(x => (x.End - x.Start).TotalMinutes == 30));
            //Console.WriteLine();
            //Console.WriteLine("Free filtered slots");
            //foreach (var slot in slots)
            //{
            //    Console.WriteLine($"{slot.Start:HH:mm} - {slot.End:HH:mm} {(slot.End - slot.Start).TotalMinutes}");
            //}

            return slots;
        }
    }
}