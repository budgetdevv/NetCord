﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetCord.Gateway.Voice.JsonModels;

internal class JsonSessionDescription
{
    [JsonConverter(typeof(ByteArrayOfLength32Converter))]
    [JsonPropertyName("secret_key")]
    public byte[] SecretKey { get; set; }

    public class ByteArrayOfLength32Converter : JsonConverter<byte[]>
    {
        public override byte[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var result = new byte[32];
            for (int i = 0; i < 32; i++)
            {
                reader.Read();
                result[i] = reader.GetByte();
            }
            reader.Read();
            return result;
        }

        public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options) => ThrowUtils.Throw<NotImplementedException>();
    }
}
