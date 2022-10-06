﻿using System.Text.Json.Serialization;

namespace NetCord.Rest;

public partial class GroupDMUserAddProperties
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; }

    [JsonPropertyName("nick")]
    public string? Nickname { get; set; }

    public GroupDMUserAddProperties(string accessToken)
    {
        AccessToken = accessToken;
    }

    [JsonSerializable(typeof(GroupDMUserAddProperties))]
    public partial class GroupDMUserAddPropertiesSerializerContext : JsonSerializerContext
    {
        public static GroupDMUserAddPropertiesSerializerContext WithOptions { get; } = new(new(ToObjectExtensions._options));
    }
}
