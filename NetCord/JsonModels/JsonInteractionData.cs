﻿using System.Text.Json.Serialization;

namespace NetCord.JsonModels;

public partial class JsonInteractionData
{
    [JsonPropertyName("id")]
    public Snowflake? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("type")]
    public ApplicationCommandType? Type { get; set; }

    [JsonPropertyName("resolved")]
    public JsonApplicationCommandResolvedData? ResolvedData { get; set; }

    [JsonPropertyName("options")]
    public JsonApplicationCommandInteractionDataOption[]? Options { get; set; }

    [JsonPropertyName("custom_id")]
    public string? CustomId { get; set; }

    [JsonPropertyName("component_type")]
    public ComponentType? ComponentType { get; set; }

    [JsonPropertyName("values")]
    public string[]? SelectedValues { get; set; }

    [JsonPropertyName("target_id")]
    public Snowflake? TargetId { get; set; }

    [JsonPropertyName("components")]
    public JsonComponent[]? Components { get; set; }

    [JsonSerializable(typeof(JsonInteractionData))]
    public partial class JsonInteractionDataSerializerContext : JsonSerializerContext
    {
        public static JsonInteractionDataSerializerContext WithOptions { get; } = new(new(ToObjectExtensions._options));
    }
}
