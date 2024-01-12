﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetCord.Gateway;

[JsonConverter(typeof(PartySizePropertiesConverter))]
public partial class PartySizeProperties
{
    public PartySizeProperties(int currentSize, int maxSize)
    {
        CurrentSize = currentSize;
        MaxSize = maxSize;
    }

    public int CurrentSize { get; set; }

    public int MaxSize { get; set; }

    public class PartySizePropertiesConverter : JsonConverter<PartySizeProperties>
    {
        public override PartySizeProperties? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => ThrowUtils.Throw<NotImplementedException, PartySizeProperties>();

        public override void Write(Utf8JsonWriter writer, PartySizeProperties value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            writer.WriteNumberValue(value.CurrentSize);
            writer.WriteNumberValue(value.MaxSize);
            writer.WriteEndArray();
        }
    }
}
