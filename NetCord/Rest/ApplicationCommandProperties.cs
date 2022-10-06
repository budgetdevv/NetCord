﻿using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetCord.Rest;

public partial class SlashCommandProperties : ApplicationCommandProperties
{
    [JsonPropertyName("description")]
    public string Description { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("description_localizations")]
    public IReadOnlyDictionary<CultureInfo, string>? DescriptionLocalizations { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("options")]
    public IEnumerable<ApplicationCommandOptionProperties>? Options { get; set; }

    public SlashCommandProperties(string name, string description) : base(name)
    {
        Description = description;
    }

    [JsonSerializable(typeof(SlashCommandProperties))]
    public partial class SlashCommandPropertiesSerializerContext : JsonSerializerContext
    {
        public static SlashCommandPropertiesSerializerContext WithOptions { get; } = new(new(ToObjectExtensions._options));
    }
}

public partial class UserCommandProperties : ApplicationCommandProperties
{
    [JsonPropertyName("type")]
    public ApplicationCommandType Type => ApplicationCommandType.User;

    public UserCommandProperties(string name) : base(name)
    {
    }

    [JsonSerializable(typeof(UserCommandProperties))]
    public partial class UserCommandPropertiesSerializerContext : JsonSerializerContext
    {
        public static UserCommandPropertiesSerializerContext WithOptions { get; } = new(new(ToObjectExtensions._options));
    }
}

public partial class MessageCommandProperties : ApplicationCommandProperties
{
    [JsonPropertyName("type")]
    public ApplicationCommandType Type => ApplicationCommandType.Message;

    public MessageCommandProperties(string name) : base(name)
    {
    }

    [JsonSerializable(typeof(MessageCommandProperties))]
    public partial class MessageCommandPropertiesSerializerContext : JsonSerializerContext
    {
        public static MessageCommandPropertiesSerializerContext WithOptions { get; } = new(new(ToObjectExtensions._options));
    }
}

[JsonConverter(typeof(ApplicationCommandPropertiesConverter))]
public abstract partial class ApplicationCommandProperties
{
    private protected ApplicationCommandProperties(string name)
    {
        Name = name;
    }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("name_localizations")]
    public IReadOnlyDictionary<CultureInfo, string>? NameLocalizations { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("default_member_permissions")]
    public Permission? DefaultGuildUserPermissions { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("dm_permission")]
    public bool? DMPermission { get; set; }

    [Obsolete("Replaced by `default_member_permissions`")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("default_permission")]
    public bool? DefaultPermission { get; set; }

    internal class ApplicationCommandPropertiesConverter : JsonConverter<ApplicationCommandProperties>
    {
        public override ApplicationCommandProperties? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, ApplicationCommandProperties value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case SlashCommandProperties slashCommandProperties:
                    JsonSerializer.Serialize(writer, slashCommandProperties, SlashCommandProperties.SlashCommandPropertiesSerializerContext.WithOptions.SlashCommandProperties);
                    break;
                case UserCommandProperties userCommandProperties:
                    JsonSerializer.Serialize(writer, userCommandProperties, UserCommandProperties.UserCommandPropertiesSerializerContext.WithOptions.UserCommandProperties);
                    break;
                case MessageCommandProperties messageCommandProperties:
                    JsonSerializer.Serialize(writer, messageCommandProperties, MessageCommandProperties.MessageCommandPropertiesSerializerContext.WithOptions.MessageCommandProperties);
                    break;
            }
        }
    }

    [JsonSerializable(typeof(ApplicationCommandProperties))]
    public partial class ApplicationCommandPropertiesSerializerContext : JsonSerializerContext
    {
        public static ApplicationCommandPropertiesSerializerContext WithOptions { get; } = new(new(ToObjectExtensions._options));
    }

    [JsonSerializable(typeof(IEnumerable<ApplicationCommandProperties>))]
    public partial class IEnumerableOfApplicationCommandPropertiesSerializerContext : JsonSerializerContext
    {
        public static IEnumerableOfApplicationCommandPropertiesSerializerContext WithOptions { get; } = new(new(ToObjectExtensions._options));
    }
}
