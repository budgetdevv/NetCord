﻿using System.Text.Json;

using NetCord.JsonModels;

namespace NetCord.Rest;

public static class HttpInteractionFactory
{
    public static async ValueTask<IInteraction> CreateAsync(Stream body, RestClient client, CancellationToken cancellationToken = default)
        => IInteraction.CreateFromJson((await JsonSerializer.DeserializeAsync(body, JsonInteraction.JsonInteractionSerializerContext.WithOptions.JsonInteraction, cancellationToken).ConfigureAwait(false))!, client);

    public static IInteraction Create(Stream body, RestClient client)
        => IInteraction.CreateFromJson(JsonSerializer.Deserialize(body, JsonInteraction.JsonInteractionSerializerContext.WithOptions.JsonInteraction)!, client);

    public static IInteraction Create(ReadOnlySpan<byte> body, RestClient client)
        => IInteraction.CreateFromJson(JsonSerializer.Deserialize(body, JsonInteraction.JsonInteractionSerializerContext.WithOptions.JsonInteraction)!, client);
}