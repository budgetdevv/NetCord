using NetCord.JsonModels;

namespace NetCord;

public class AutoModerationActionMetadata : IJsonModel<JsonAutoModerationActionMetadata>
{
    JsonAutoModerationActionMetadata IJsonModel<JsonAutoModerationActionMetadata>.JsonModel => ThrowUtils.Throw<NotImplementedException, JsonAutoModerationActionMetadata>();
    private readonly JsonAutoModerationActionMetadata _jsonModel;

    public AutoModerationActionMetadata(JsonAutoModerationActionMetadata jsonModel)
    {
        _jsonModel = jsonModel;
    }

    public ulong? ChannelId => _jsonModel.ChannelId;
    public int? DurationSeconds => _jsonModel.DurationSeconds;
    public string? CustomMessage => _jsonModel.CustomMessage;
}
