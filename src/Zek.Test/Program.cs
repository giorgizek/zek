using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using Zek.Cryptography;
using Zek.Extensions;
using Zek.Model;
using Zek.Model.Config;
using Zek.Model.DTO.Email;
using Zek.Services;
using Zek.Utils;

namespace Zek.Test
{
    class Program
    {

        static void Main(string[] args)
        {
            for (int i = 0; i < 10000; i++)
            {
                var s1 = Base62.Encode(i);
                var i1 = Base62.Decode(s1);
                if (i != i1)
                {

                }
            }

            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 10000000; i++)
            {
                var shorter = new Base62Int32(int.MaxValue);
                var i1 = new Base62Int32(shorter.Value);
                if (i != i1)
                {

                }
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 10000000; i++)
            {
                var shorter = new ShortInt32(int.MaxValue);
                var i1 = new ShortInt32(shorter.Value);
                if (i != i1)
                {

                }
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Reset();
            //var shorter = new ShortInt32(int.MaxValue);



            //var shorter2 = new ShortInt32(shorter.Value);
            //if (shorter == shorter2)
            //{
            //    Console.WriteLine("yes");
            //}

            //Console.WriteLine(shorter.ToString());



            Console.ReadKey();
            return;





            var start = DateTime.Today.AddHours(9);
            var end = DateTime.Today.AddHours(14);

            var busySlots = new List<DateRange>()
            {
                new DateRange(DateTime.Today.AddHours(7), DateTime.Today.AddHours(9).AddMinutes(00)),
                new DateRange(DateTime.Today.AddHours(7), DateTime.Today.AddHours(9).AddMinutes(10)),
                new DateRange(DateTime.Today.AddHours(9), DateTime.Today.AddHours(9).AddMinutes(10)),
                new DateRange(DateTime.Today.AddHours(10), DateTime.Today.AddHours(10).AddMinutes(10)),
                new DateRange(DateTime.Today.AddHours(13).AddMinutes(50), DateTime.Today.AddHours(14)),
                new DateRange(DateTime.Today.AddHours(13).AddMinutes(50), DateTime.Today.AddHours(18)),

                new DateRange(DateTime.Today.AddHours(7), DateTime.Today.AddHours(22)),

                new DateRange(DateTime.Today.AddHours(22), DateTime.Today.AddHours(23)),
                //new DateRange(DateTime.Today.AddHours(9).AddMinutes(20), DateTime.Today.AddHours(9).AddMinutes(30)),
                //new DateRange(DateTime.Today.AddHours(10), DateTime.Today.AddHours(10).AddMinutes(15)),
                //new DateRange(DateTime.Today.AddHours(11), DateTime.Today.AddHours(11).AddMinutes(20)),
                //new DateRange(DateTime.Today.AddHours(11).AddMinutes(40), DateTime.Today.AddHours(12)),
                //new DateRange(DateTime.Today.AddHours(11).AddMinutes(40), DateTime.Today.AddHours(12).AddMinutes(13)),

            };


            foreach (var item in busySlots)
            {
                DateRangeHelper.GetFreeSlots(start, end, new[] { item }, 30);
            }

            //var slots = DateRangeHelper.GetFreeSlots(start, end, busySlots, 30);
            

            /*Console.WriteLine("Work hours:");
            var ranges = new List<DateRange>()
            {
                new DateRange(start, end)
            };
            foreach (var range in ranges)
                Console.WriteLine($"{range.Start:HH:mm} - {range.End:HH:mm}");
            Console.WriteLine();

            Console.WriteLine("Busy slots:");
            foreach (var range in busySlots)
                Console.WriteLine($"{range.Start:HH:mm} - {range.End:HH:mm}");
            Console.WriteLine();

            for (int i = 0; i < ranges.Count; i++)
            {
                for (int j = 0; j < busySlots.Count; j++)
                {
                    var relation = DateTimeHelper.GetRelation(busySlots[j].Start, busySlots[j].End, ranges[i].Start, ranges[i].End);

                    if (relation == PeriodRelation.EndInside || relation == PeriodRelation.InsideStartTouching)
                    {
                        ranges[i].Start = busySlots[j].End;
                    }
                    else
                    if (relation == PeriodRelation.StartInside || relation == PeriodRelation.InsideEndTouching)
                    {
                        ranges[i].End = busySlots[j].Start;
                    }
                    else if (relation == PeriodRelation.Inside)
                    {
                        var tmp = new DateRange(busySlots[j].End, ranges[i].End);
                        ranges[i].End = busySlots[j].Start;
                        ranges.Insert(i + 1, tmp);
                        i--;
                        break;
                    }
                }
            }

            foreach (var range in ranges)
            {
                var overlap = busySlots.FirstOrDefault(x => DateTimeHelper.Overlaps(x.Start, x.End, range.Start, range.End));
                if (overlap != null)
                {

                }
                Console.WriteLine($"{range.Start:HH:mm} - {range.End:HH:mm}   overlap:{overlap != null}");
            }



            var allFreeSlots = ranges.SelectMany(x => DateTimeHelper.SplitDateRangeByMinutes(x.Start, x.End, 30));
            Console.WriteLine();
            Console.WriteLine("Free slots");
            foreach (var slot in allFreeSlots)
            {
                Console.WriteLine($"{slot.Start:HH:mm} - {slot.End:HH:mm}");
            }

            var slots = ranges.SelectMany(x => DateTimeHelper.SplitDateRangeByMinutes(x.Start, x.End, 30).Where(x => (x.End - x.Start).TotalMinutes == 30));
            Console.WriteLine();
            Console.WriteLine("Free filtered slots");
            foreach (var slot in slots)
            {
                Console.WriteLine($"{slot.Start:HH:mm} - {slot.End:HH:mm}");
            }*/




            //var sw = new Stopwatch();
            //var count = 1000000;
            //sw.Start();
            //for (var i = 0; i < count; i++)
            //{
            //}
            //sw.Stop();
            //Console.WriteLine(sw.ElapsedMilliseconds);
            //sw.Reset();


            //sw.Start();
            //for (var i = 0; i < count; i++)
            //{
            //}
            //sw.Stop();
            //Console.WriteLine(sw.ElapsedMilliseconds);


            Console.ReadKey(false);
        }

        //private static ILdapConnection _conn;

        //static ILdapConnection GetConnection()
        //{
        //    LdapConnection ldapConn = _conn as LdapConnection;

        //    if (ldapConn == null)
        //    {
        //        // Creating an LdapConnection instance 
        //        ldapConn = new LdapConnection() { SecureSocketLayer = false };

        //        //Connect function will create a socket connection to the server - Port 389 for insecure and 3269 for secure    
        //        ldapConn.Connect("uadevdc", 389);

        //        //Bind function with null user dn and password value will perform anonymous bind to LDAP server 
        //        ldapConn.Bind(@"uadev\ia.javakhishvili", "Zxcvbnm1!");
        //    }

        //    return ldapConn;
        //}

    }
}