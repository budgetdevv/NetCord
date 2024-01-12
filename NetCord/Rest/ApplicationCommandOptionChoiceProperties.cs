﻿using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetCord.Rest;

[JsonConverter(typeof(ApplicationCommandOptionChoicePropertiesConverter))]
public partial class ApplicationCommandOptionChoiceProperties
{
    /// <summary>
    /// Name of the choice (1-100 characters).
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// Translations of <see cref="Name"/> (1-100 characters each).
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("name_localizations")]
    public IReadOnlyDictionary<CultureInfo, string>? NameLocalizations { get; set; }

    /// <summary>
    /// String value for the choice (max 100 characters).
    /// </summary>
    public string? StringValue { get; set; }

    /// <summary>
    /// Numeric value for the choice (max 100 characters).
    /// </summary>
    public double? NumericValue { get; set; }

    /// <summary>
    /// Type of value.
    /// </summary>
    public ApplicationCommandOptionChoiceValueType ValueType { get; set; }

    public ApplicationCommandOptionChoiceProperties(string name, string stringValue)
    {
        Name = name;
        StringValue = stringValue;
        ValueType = ApplicationCommandOptionChoiceValueType.String;
    }

    public ApplicationCommandOptionChoiceProperties(string name, double numericValue)
    {
        Name = name;
        NumericValue = numericValue;
        ValueType = ApplicationCommandOptionChoiceValueType.Numeric;
    }

    public partial class ApplicationCommandOptionChoicePropertiesConverter : JsonConverter<ApplicationCommandOptionChoiceProperties>
    {
        private static readonly JsonEncodedText _name = JsonEncodedText.Encode("name");
        private static readonly JsonEncodedText _nameLocalizations = JsonEncodedText.Encode("name_localizations");
        private static readonly JsonEncodedText _value = JsonEncodedText.Encode("value");

        public override ApplicationCommandOptionChoiceProperties? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => ThrowUtils.Throw<NotImplementedException, ApplicationCommandOptionChoiceProperties?>();

        public override void Write(Utf8JsonWriter writer, ApplicationCommandOptionChoiceProperties value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString(_name, value.Name);

            var nameLocalizations = value.NameLocalizations;
            if (nameLocalizations is not null)
            {
                writer.WritePropertyName(_nameLocalizations);
                JsonSerializer.Serialize(writer, nameLocalizations, Serialization.Default.IReadOnlyDictionaryCultureInfoString);
            }

            writer.WritePropertyName(_value);
            if (value.ValueType == ApplicationCommandOptionChoiceValueType.String)
                writer.WriteStringValue(value.StringValue);
            else
                writer.WriteNumberValue(value.NumericValue.GetValueOrDefault());

            writer.WriteEndObject();
        }
    }
}
