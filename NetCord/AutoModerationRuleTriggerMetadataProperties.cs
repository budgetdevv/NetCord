﻿using System.Text.Json.Serialization;

namespace NetCord;

public partial class AutoModerationRuleTriggerMetadataProperties
{
    [JsonPropertyName("keyword_filter")]
    public IEnumerable<string>? KeywordFilter { get; set; }

    [JsonPropertyName("presets")]
    public IEnumerable<AutoModerationRuleKeywordPresetType>? Presets { get; set; }

    [JsonPropertyName("allow_list")]
    public IEnumerable<string>? AllowList { get; set; }

    [JsonPropertyName("mention_total_limit")]
    public int? MentionTotalLimit { get; set; }

    [JsonSerializable(typeof(AutoModerationRuleTriggerMetadataProperties))]
    public partial class AutoModerationRuleTriggerMetadataPropertiesSerializerContext : JsonSerializerContext
    {
        public static AutoModerationRuleTriggerMetadataPropertiesSerializerContext WithOptions { get; } = new(new(ToObjectExtensions._options));
    }
}
