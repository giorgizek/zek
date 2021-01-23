using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Text;

namespace Zek.Office
{
    // ReSharper disable once InconsistentNaming
    public class iCalendar
    {
        private EventCollection _events;
        public EventCollection Events => _events ??= new EventCollection();


        public override string ToString()
        {
            var sb = new StringBuilder();
            //Calendar
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("PRODID:-//Zek Inc//Zek Library//EN");
            sb.AppendLine("VERSION:2.0");
            //sb.AppendLine("CALSCALE:GREGORIAN");
            sb.AppendLine("METHOD:REQUEST");
            //sb.AppendLine("METHOD:PUBLISH");

            foreach (var @event in Events)
            {
                sb.Append(@event);
            }


            sb.AppendLine("END:VCALENDAR");
            return sb.ToString();
        }


        public class EventCollection : Collection<Event>
        {

        }

        public class Event
        {
            public Event()
            {
                TimeStamp = DateTime.Now;
                Created = TimeStamp;
                LastModified = TimeStamp;
            }

            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public DateTime TimeStamp { get; set; }
            public EmailAddress Organizer { get; set; }
            public DateTime Created { get; set; }
            public DateTime LastModified { get; set; }
            // ReSharper disable once InconsistentNaming
            public string UID { get; set; }
            public string Description { get; set; }
            public string Location { get; set; }
            public string Summary { get; set; }
            public string Url { get; set; }



            private AlarmCollection _alarms;
            public AlarmCollection Alarms => _alarms ??= new AlarmCollection();

            private AttendeeCollection _atendees;
            public AttendeeCollection Atendees => _atendees ??= new AttendeeCollection();

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendLine("BEGIN:VEVENT");
                sb.AppendLine("DTSTART:" + ToUniversalTime(Start));
                sb.AppendLine("DTEND:" + ToUniversalTime(End));
                sb.AppendLine("DTSTAMP:" + ToUniversalTime(TimeStamp));
                if (!string.IsNullOrEmpty(Organizer?.Address))
                {
                    sb.AppendLine(!string.IsNullOrEmpty(Organizer.Name)
                        ? $"ORGANIZER;CN={Organizer.Name}:mailto:{Organizer.Address}"
                        : $"ORGANIZER:mailto:{Organizer.Address}");
                }
                sb.AppendLine("UID:" + UID);

                foreach (var item in Atendees)
                    sb.Append(item);

                sb.AppendLine("CREATED:" + ToUniversalTime(Created));
                sb.AppendLine("X-ALT-DESC;FMTTYPE=text/html:" + Description);
                sb.AppendLine("LAST-MODIFIED:" + ToUniversalTime(LastModified));
                sb.AppendLine("LOCATION:" + Location);
                sb.AppendLine("SEQUENCE:0");
                sb.AppendLine("STATUS:CONFIRMED");
                sb.AppendLine("SUMMARY:" + Summary);
                sb.AppendLine("TRANSP:OPAQUE");

                foreach (var item in Alarms)
                    sb.Append(item);

                sb.AppendLine("END:VEVENT");
                return sb.ToString();
            }
        }



        private const string iCalendarFormat = "yyyyMMddTHHmmssZ";
        private static string ToUniversalTime(DateTime dt)
        {
            return dt.ToUniversalTime().ToString(iCalendarFormat);
        }

        /// <summary>
        ///  Folds lines at 75 characters, and prepends the next line with a space per RFC https://tools.ietf.org/html/rfc5545#section-3.1
        /// </summary>
        /// <param name="incoming"></param>
        /// <returns></returns>
        private static string FoldLines(string incoming)
        {
            //The spec says nothing about trimming, but it seems reasonable...
            var trimmed = incoming.Trim();
            if (trimmed.Length <= 75)
            {
                return trimmed + SerializationConstants.LineBreak;
            }

            const int takeLimit = 74;

            var firstLine = trimmed.Substring(0, takeLimit);
            var remainder = trimmed.Substring(takeLimit, trimmed.Length - takeLimit);

            var chunkedRemainder = string.Join(SerializationConstants.LineBreak + " ", Chunk(remainder));
            return firstLine + SerializationConstants.LineBreak + " " + chunkedRemainder + SerializationConstants.LineBreak;
        }
        private static IEnumerable<string> Chunk(string str, int chunkSize = 73)
        {
            for (var index = 0; index < str.Length; index += chunkSize)
            {
                yield return str.Substring(index, Math.Min(chunkSize, str.Length - index));
            }
        }
        private static class SerializationConstants
        {
            public const string LineBreak = "\r\n";
        }


        public class AttendeeCollection : Collection<Attendee>
        {

        }
        public class Attendee : EmailAddress
        {
            public override string ToString()
            {
                var sb = new StringBuilder();

                sb.AppendLine(!string.IsNullOrEmpty(Name)
                    ? $"ATTENDEE;CUTYPE=INDIVIDUAL;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;CN={Name};X-NUM-GUESTS=0:{Address}"
                    : $"ATTENDEE;CUTYPE=INDIVIDUAL;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;X-NUM-GUESTS=0:{Address}");
                return sb.ToString();
            }
        }


        public class AlarmCollection : Collection<Alarm>
        {

        }

        public class Alarm
        {
            public Alarm(TimeSpan trigger) : this()
            {
                Trigger = trigger;
            }
            public Alarm()
            {
                Action = "DISPLAY";
                Description = "Reminder";
            }

            /// <summary>
            /// Amount of time before event to display alarm
            /// </summary>
            public TimeSpan Trigger { get; set; }

            /// <summary>
            ///  Action to take to notify user of alarm
            /// </summary>
            public string Action;

            public string Repeat { get; set; }

            public TimeSpan? Duration { get; set; }

            /// <summary>
            /// Description of the alarm
            /// </summary>
            public string Description { get; set; }

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendLine("BEGIN:VALARM");
                sb.AppendLine($"TRIGGER:-P{Trigger.Days}DT{Trigger.Hours}H{Trigger.Minutes}M");
                if (!string.IsNullOrEmpty(Repeat))
                    sb.AppendLine("REPEAT:" + Repeat);
                if (Duration.HasValue)
                    sb.AppendLine($"DURATION:PT{Duration.Value.Hours}H{Duration.Value.Minutes}M{Duration.Value.Seconds}S");
                sb.AppendLine("ACTION:" + Action);
                sb.AppendLine("DESCRIPTION:" + Description);
                sb.AppendLine("END:VALARM");
                return sb.ToString();

            }
        }



    }
}