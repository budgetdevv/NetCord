﻿using System.Text.Json.Serialization;

namespace NetCord.JsonModels;

public partial class JsonGuildWidgetSettings
{
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    [JsonPropertyName("channel_id")]
    public Snowflake? ChannelId { get; set; }

    [JsonSerializable(typeof(JsonGuildWidgetSettings))]
    public partial class JsonGuildWidgetSettingsSerializerContext : JsonSerializerContext
    {
        public static JsonGuildWidgetSettingsSerializerContext WithOptions { get; } = new(new(ToObjectExtensions._options));
    }
}
