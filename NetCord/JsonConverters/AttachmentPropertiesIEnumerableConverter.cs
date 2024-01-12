﻿using System.Text.Json;
using System.Text.Json.Serialization;

using NetCord.Rest;

namespace NetCord.JsonConverters;

public class AttachmentPropertiesIEnumerableConverter : JsonConverter<IEnumerable<AttachmentProperties>>
{
    private static readonly JsonEncodedText _id = JsonEncodedText.Encode("id");
    private static readonly JsonEncodedText _fileName = JsonEncodedText.Encode("filename");
    private static readonly JsonEncodedText _description = JsonEncodedText.Encode("description");
    private static readonly JsonEncodedText _uploadedFileName = JsonEncodedText.Encode("uploaded_filename");

    public override IEnumerable<AttachmentProperties>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => ThrowUtils.Throw<NotImplementedException, IEnumerable<AttachmentProperties>>();

    public override void Write(Utf8JsonWriter writer, IEnumerable<AttachmentProperties> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        int i = 0;
        foreach (var attachment in value)
        {
            writer.WriteStartObject();

            writer.WriteNumber(_id, i);

            writer.WriteString(_fileName, attachment.FileName);

            var description = attachment.Description;
            if (description is not null)
                writer.WriteString(_description, description);

            if (attachment is GoogleCloudPlatformAttachmentProperties googleCloudPlatformAttachment)
                writer.WriteString(_uploadedFileName, googleCloudPlatformAttachment.UploadedFileName);

            writer.WriteEndObject();

            i++;
        }

        writer.WriteEndArray();
    }
}
