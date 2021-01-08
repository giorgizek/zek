using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Zek.Office
{
    public class DateTimeTimeZone
    {
        [JsonProperty("dateTime")]
        public virtual string DateTimeRaw { get; set; }

        [JsonIgnore]
        public virtual DateTime? DateTime
        {
            get => GetDateTimeFromString(DateTimeRaw);
            set => DateTimeRaw = GetStringFromDateTime(value);
        }

        public virtual string TimeZone { get; set; }


      
        private static string GetStringFromDateTime(DateTime? date) => date.HasValue ? ConvertToRFC3339(date.Value) : null;

        private static DateTime? GetDateTimeFromString(string raw)
        {
            if (System.DateTime.TryParse(raw, out var time))
            {
                return time;
            }
            return null;
        }

        private static string ConvertToRFC3339(DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
            {
                date = date.ToUniversalTime();
            }

            return date.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK", DateTimeFormatInfo.InvariantInfo);
        }


        /// <summary>
        /// Implicitly converts the DateTime? to a DateTimeTimeZone.
        /// </summary>
        public static implicit operator DateTimeTimeZone(DateTime? value)
        {
            return new()
            {
                DateTime = value
            };
        }

        /// <summary>
        ///  Implicitly converts the DateTimeTimeZone to its DateTime? equivalent.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator DateTime?(DateTimeTimeZone value) => value.DateTime;
    }
}