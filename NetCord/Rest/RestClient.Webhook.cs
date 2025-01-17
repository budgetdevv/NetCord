﻿using NetCord.Gateway;

namespace NetCord.Rest;

public partial class RestClient
{
    [GenerateAlias([typeof(TextGuildChannel)], nameof(TextGuildChannel.Id))]
    public async Task<Webhook> CreateWebhookAsync(ulong channelId, WebhookProperties webhookProperties, RestRequestProperties? properties = null)
    {
        using (HttpContent content = new JsonContent<WebhookProperties>(webhookProperties, Serialization.Default.WebhookProperties))
            return Webhook.CreateFromJson(await (await SendRequestAsync(HttpMethod.Post, content, $"/channels/{channelId}/webhooks", null, new(channelId), properties).ConfigureAwait(false)).ToObjectAsync(Serialization.Default.JsonWebhook).ConfigureAwait(false), this);
    }

    [GenerateAlias([typeof(TextGuildChannel)], nameof(TextGuildChannel.Id))]
    public async Task<IReadOnlyDictionary<ulong, Webhook>> GetChannelWebhooksAsync(ulong channelId, RestRequestProperties? properties = null)
        => (await (await SendRequestAsync(HttpMethod.Get, $"/channels/{channelId}/webhooks", null, new(channelId), properties).ConfigureAwait(false)).ToObjectAsync(Serialization.Default.JsonWebhookArray).ConfigureAwait(false)).ToDictionary(w => w.Id, w => Webhook.CreateFromJson(w, this));

    [GenerateAlias([typeof(RestGuild)], nameof(RestGuild.Id), TypeNameOverride = nameof(Guild))]
    public async Task<IReadOnlyDictionary<ulong, Webhook>> GetGuildWebhooksAsync(ulong guildId, RestRequestProperties? properties = null)
        => (await (await SendRequestAsync(HttpMethod.Get, $"/guilds/{guildId}/webhooks", null, new(guildId), properties).ConfigureAwait(false)).ToObjectAsync(Serialization.Default.JsonWebhookArray).ConfigureAwait(false)).ToDictionary(w => w.Id, w => Webhook.CreateFromJson(w, this));

    [GenerateAlias([typeof(Webhook)], nameof(Webhook.Id), Cast = true)]
    public async Task<Webhook> GetWebhookAsync(ulong webhookId, RestRequestProperties? properties = null)
        => Webhook.CreateFromJson(await (await SendRequestAsync(HttpMethod.Get, $"/webhooks/{webhookId}", null, new(webhookId), properties).ConfigureAwait(false)).ToObjectAsync(Serialization.Default.JsonWebhook).ConfigureAwait(false), this);

    [GenerateAlias([typeof(IncomingWebhook)], nameof(IncomingWebhook.Id), nameof(IncomingWebhook.Token), Cast = true, TypeNameOverride = nameof(Webhook))]
    [GenerateAlias([typeof(WebhookClient)], nameof(WebhookClient.Id), nameof(WebhookClient.Token), TypeNameOverride = $"{nameof(Webhook)}WithToken")]
    public async Task<Webhook> GetWebhookWithTokenAsync(ulong webhookId, string webhookToken, RestRequestProperties? properties = null)
        => Webhook.CreateFromJson(await (await SendRequestAsync(HttpMethod.Get, $"/webhooks/{webhookId}/{webhookToken}", null, new(webhookId, webhookToken), properties).ConfigureAwait(false)).ToObjectAsync(Serialization.Default.JsonWebhook).ConfigureAwait(false), this);

    [GenerateAlias([typeof(Webhook)], nameof(Webhook.Id), Cast = true)]
    public async Task<Webhook> ModifyWebhookAsync(ulong webhookId, Action<WebhookOptions> action, RestRequestProperties? properties = null)
    {
        WebhookOptions webhookOptions = new();
        action(webhookOptions);
        using (HttpContent content = new JsonContent<WebhookOptions>(webhookOptions, Serialization.Default.WebhookOptions))
            return Webhook.CreateFromJson(await (await SendRequestAsync(HttpMethod.Patch, content, $"/webhooks/{webhookId}", null, new(webhookId), properties).ConfigureAwait(false)).ToObjectAsync(Serialization.Default.JsonWebhook).ConfigureAwait(false), this);
    }

    [GenerateAlias([typeof(IncomingWebhook)], nameof(IncomingWebhook.Id), nameof(IncomingWebhook.Token), Cast = true, TypeNameOverride = nameof(Webhook))]
    [GenerateAlias([typeof(WebhookClient)], nameof(WebhookClient.Id), nameof(WebhookClient.Token), TypeNameOverride = $"{nameof(Webhook)}WithToken")]
    public async Task<Webhook> ModifyWebhookWithTokenAsync(ulong webhookId, string webhookToken, Action<WebhookOptions> action, RestRequestProperties? properties = null)
    {
        WebhookOptions webhookOptions = new();
        action(webhookOptions);
        using (HttpContent content = new JsonContent<WebhookOptions>(webhookOptions, Serialization.Default.WebhookOptions))
            return Webhook.CreateFromJson(await (await SendRequestAsync(HttpMethod.Patch, content, $"/webhooks/{webhookId}/{webhookToken}", null, new(webhookId, webhookToken), properties).ConfigureAwait(false)).ToObjectAsync(Serialization.Default.JsonWebhook).ConfigureAwait(false), this);
    }

    [GenerateAlias([typeof(Webhook)], nameof(Webhook.Id))]
    public Task DeleteWebhookAsync(ulong webhookId, RestRequestProperties? properties = null)
        => SendRequestAsync(HttpMethod.Delete, $"/webhooks/{webhookId}", null, new(webhookId), properties);

    [GenerateAlias([typeof(IncomingWebhook)], nameof(IncomingWebhook.Id), nameof(IncomingWebhook.Token), TypeNameOverride = nameof(Webhook))]
    [GenerateAlias([typeof(WebhookClient)], nameof(WebhookClient.Id), nameof(WebhookClient.Token), TypeNameOverride = $"{nameof(Webhook)}WithToken")]
    public Task DeleteWebhookWithTokenAsync(ulong webhookId, string webhookToken, RestRequestProperties? properties = null)
        => SendRequestAsync(HttpMethod.Delete, $"/webhooks/{webhookId}/{webhookToken}", null, new(webhookId, webhookToken), properties);

    [GenerateAlias([typeof(IncomingWebhook)], nameof(IncomingWebhook.Id), nameof(IncomingWebhook.Token), TypeNameOverride = nameof(Webhook))]
    [GenerateAlias([typeof(WebhookClient)], nameof(WebhookClient.Id), nameof(WebhookClient.Token), TypeNameOverride = nameof(Webhook))]
    public async Task<RestMessage?> ExecuteWebhookAsync(ulong webhookId, string webhookToken, WebhookMessageProperties message, bool wait = false, ulong? threadId = null, RestRequestProperties? properties = null)
    {
        using (HttpContent content = message.Serialize())
        {
            if (wait)
                return new(await (await SendRequestAsync(HttpMethod.Post, content, $"/webhooks/{webhookId}/{webhookToken}", threadId.HasValue ? $"?wait=True&thread_id={threadId.GetValueOrDefault()}" : $"?wait=True", new(webhookId, webhookToken), properties).ConfigureAwait(false)).ToObjectAsync(Serialization.Default.JsonMessage).ConfigureAwait(false), this);
            else
            {
                await SendRequestAsync(HttpMethod.Post, content, $"/webhooks/{webhookId}/{webhookToken}", threadId.HasValue ? $"?thread_id={threadId.GetValueOrDefault()}" : null, new(webhookId, webhookToken), properties).ConfigureAwait(false);
                return null;
            }
        }
    }

    [GenerateAlias([typeof(IncomingWebhook)], nameof(IncomingWebhook.Id), nameof(IncomingWebhook.Token), TypeNameOverride = nameof(Webhook))]
    [GenerateAlias([typeof(WebhookClient)], nameof(WebhookClient.Id), nameof(WebhookClient.Token), TypeNameOverride = nameof(Webhook))]
    public async Task<RestMessage> GetWebhookMessageAsync(ulong webhookId, string webhookToken, ulong messageId, RestRequestProperties? properties = null)
        => new(await (await SendRequestAsync(HttpMethod.Get, $"/webhooks/{webhookId}/{webhookToken}/messages/{messageId}", null, new(webhookId, webhookToken), properties).ConfigureAwait(false)).ToObjectAsync(Serialization.Default.JsonMessage).ConfigureAwait(false), this);

    [GenerateAlias([typeof(IncomingWebhook)], nameof(IncomingWebhook.Id), nameof(IncomingWebhook.Token), TypeNameOverride = nameof(Webhook))]
    [GenerateAlias([typeof(WebhookClient)], nameof(WebhookClient.Id), nameof(WebhookClient.Token), TypeNameOverride = nameof(Webhook))]
    public async Task<RestMessage> ModifyWebhookMessageAsync(ulong webhookId, string webhookToken, ulong messageId, Action<MessageOptions> action, ulong? threadId = null, RestRequestProperties? properties = null)
    {
        MessageOptions messageOptions = new();
        action(messageOptions);
        using (HttpContent content = messageOptions.Serialize())
            return new(await (await SendRequestAsync(HttpMethod.Patch, content, $"/webhooks/{webhookId}/{webhookToken}/messages/{messageId}", threadId.HasValue ? $"?thread_id={threadId.GetValueOrDefault()}" : null, new(webhookId, webhookToken), properties).ConfigureAwait(false)).ToObjectAsync(Serialization.Default.JsonMessage).ConfigureAwait(false), this);
    }

    [GenerateAlias([typeof(IncomingWebhook)], nameof(IncomingWebhook.Id), nameof(IncomingWebhook.Token), TypeNameOverride = nameof(Webhook))]
    [GenerateAlias([typeof(WebhookClient)], nameof(WebhookClient.Id), nameof(WebhookClient.Token), TypeNameOverride = nameof(Webhook))]
    public Task DeleteWebhookMessageAsync(ulong webhookId, string webhookToken, ulong messageId, ulong? threadId = null, RestRequestProperties? properties = null)
        => SendRequestAsync(HttpMethod.Delete, $"/webhooks/{webhookId}/{webhookToken}/messages/{messageId}", threadId.HasValue ? $"?thread_id={threadId.GetValueOrDefault()}" : null, new(webhookId, webhookToken), properties);
}
