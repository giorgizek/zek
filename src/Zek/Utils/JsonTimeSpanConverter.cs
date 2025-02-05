using System.Text.Json;
using System.Text.Json.Serialization;

namespace Zek.Utils
{
    public class JsonTimeSpanConverter : JsonConverter<TimeSpan>
    {
        //public const string TimeSpanFormatString = @"hh\:mm\:ss\";

        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return TimeSpan.Parse(value ?? string.Empty);
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            //writer.WriteStringValue(value.ToString(TimeSpanFormatString));
            writer.WriteStringValue(value.ToString());
        }
    }
}
