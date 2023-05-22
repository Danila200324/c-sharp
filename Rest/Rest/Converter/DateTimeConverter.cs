using Newtonsoft.Json;

namespace WebApplication2.Converter;

public class DateTimeConverter : JsonConverter<DateTime>
{
    private const string Format = "yyyy-MM-dd";

    public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.Value == null)
        {
            return DateTime.MinValue;
        }

        if (reader.ValueType == typeof(DateTime))
        {
            return (DateTime)reader.Value;
        }

        if (reader.ValueType == typeof(string))
        {
            var dateString = (string)reader.Value;
            return DateTime.ParseExact(dateString, Format, null);
        }

        throw new JsonReaderException($"Unexpected value type {reader.ValueType} when deserializing DateTime.");
    }

    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString(Format));
    }
}