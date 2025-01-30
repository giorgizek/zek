using Newtonsoft.Json;
using Zek.Extensions;

namespace Zek.Office
{
    public class DateTimeTimeZone
    {
        [JsonProperty("dateTime")]
        public virtual string? DateTimeRaw { get; set; }

        [JsonIgnore]
        public virtual DateTime? DateTime
        {
            get => DateTimeRaw.ToNullableDateTime();
            set => DateTimeRaw = value.ToRfc3339String();
        }

        public virtual string? TimeZone { get; set; }

      
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