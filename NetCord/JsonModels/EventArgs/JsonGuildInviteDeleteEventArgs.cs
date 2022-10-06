﻿using System.Text.Json.Serialization;

namespace NetCord.JsonModels.EventArgs;

public partial class JsonGuildInviteDeleteEventArgs
{
    [JsonPropertyName("channel_id")]
    public Snowflake InviteChannelId { get; set; }

    [JsonPropertyName("guild_id")]
    public Snowflake? GuildId { get; set; }

    [JsonPropertyName("code")]
    public string InviteCode { get; set; }

    [JsonSerializable(typeof(JsonGuildInviteDeleteEventArgs))]
    public partial class JsonGuildInviteDeleteEventArgsSerializerContext : JsonSerializerContext
    {
        public static JsonGuildInviteDeleteEventArgsSerializerContext WithOptions { get; } = new(new(ToObjectExtensions._options));
    }
}
