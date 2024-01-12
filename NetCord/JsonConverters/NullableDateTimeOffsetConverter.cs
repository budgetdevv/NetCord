using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetCord.JsonConverters;

public class NullableDateTimeOffsetConverter : JsonConverter<DateTimeOffset?>
{
    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => ThrowUtils.Throw<NotImplementedException, DateTimeOffset?>();

    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
    {
        var v = value.GetValueOrDefault();
        if (v == default)
            writer.WriteNullValue();
        else
            writer.WriteStringValue(v);
    }
}
