﻿using System.Text.Json.Serialization;

namespace NetCord;

public class ChannelPermissionOverwrite
{
    [JsonPropertyName("id")]
    public DiscordId Id { get; }

    [JsonPropertyName("type")]
    public PermissionOverwriteType Type { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("allow")]
    public Permission? Allowed { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("deny")]
    public Permission? Denied { get; set; }

    public ChannelPermissionOverwrite(DiscordId id, PermissionOverwriteType type)
    {
        Id = id;
        Type = type;
    }
}