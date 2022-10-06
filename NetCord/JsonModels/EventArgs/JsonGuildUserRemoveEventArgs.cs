﻿using System.Text.Json.Serialization;

namespace NetCord.JsonModels.EventArgs;

public partial class JsonGuildUserRemoveEventArgs
{
    [JsonPropertyName("guild_id")]
    public Snowflake GuildId { get; set; }

    [JsonPropertyName("user")]
    public JsonUser User { get; set; }

    [JsonSerializable(typeof(JsonGuildUserRemoveEventArgs))]
    public partial class JsonGuildUserRemoveEventArgsSerializerContext : JsonSerializerContext
    {
        public static JsonGuildUserRemoveEventArgsSerializerContext WithOptions { get; } = new(new(ToObjectExtensions._options));
    }
}
