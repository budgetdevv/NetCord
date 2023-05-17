﻿using NetCord.Rest.JsonModels;

namespace NetCord.Rest;

public partial class RestClient
{
    public async Task<RestGuildInvite> GetGuildInviteAsync(string inviteCode, bool withCounts = false, bool withExpiration = false, ulong? guildScheduledEventId = null, RequestProperties? properties = null)
    {
        if (guildScheduledEventId.HasValue)
            return new(await (await SendRequestAsync(HttpMethod.Get, $"/invites/{inviteCode}?with_counts={withCounts}&with_expiration={withExpiration}&guild_scheduled_event_id={guildScheduledEventId}", new RateLimits.Route(RateLimits.RouteParameter.GetGuildInvite), properties).ConfigureAwait(false)).ToObjectAsync(JsonRestGuildInvite.JsonRestGuildInviteSerializerContext.WithOptions.JsonRestGuildInvite).ConfigureAwait(false), this);
        else
            return new(await (await SendRequestAsync(HttpMethod.Get, $"/invites/{inviteCode}?with_counts={withCounts}&with_expiration={withExpiration}", new RateLimits.Route(RateLimits.RouteParameter.GetGuildInvite), properties).ConfigureAwait(false)).ToObjectAsync(JsonRestGuildInvite.JsonRestGuildInviteSerializerContext.WithOptions.JsonRestGuildInvite).ConfigureAwait(false), this);
    }

    public async Task<RestGuildInvite> DeleteGuildInviteAsync(string inviteCode, RequestProperties? properties = null)
        => new(await (await SendRequestAsync(HttpMethod.Delete, $"/invites/{inviteCode}", new RateLimits.Route(RateLimits.RouteParameter.DeleteGuildInvite), properties).ConfigureAwait(false)).ToObjectAsync(JsonRestGuildInvite.JsonRestGuildInviteSerializerContext.WithOptions.JsonRestGuildInvite).ConfigureAwait(false), this);
}
