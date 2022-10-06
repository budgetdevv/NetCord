﻿using System.Text.Json.Serialization;

namespace NetCord.Rest;

public partial class GuildTemplateProperties
{
    public GuildTemplateProperties(string name)
    {
        Name = name;
    }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonSerializable(typeof(GuildTemplateProperties))]
    public partial class GuildTemplatePropertiesSerializerContext : JsonSerializerContext
    {
        public static GuildTemplatePropertiesSerializerContext WithOptions { get; } = new(new(ToObjectExtensions._options));
    }
}
