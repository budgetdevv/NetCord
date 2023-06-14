﻿using System.Globalization;

namespace NetCord.Services.ApplicationCommands.TypeReaders;

public class MentionableTypeReader<TContext> : SlashCommandTypeReader<TContext> where TContext : IApplicationCommandContext
{
    public override ApplicationCommandOptionType Type => ApplicationCommandOptionType.Mentionable;

    public override Task<object?> ReadAsync(string value, TContext context, SlashCommandParameter<TContext> parameter, ApplicationCommandServiceConfiguration<TContext> configuration, IServiceProvider? serviceProvider)
    {
        var slashInteraction = (SlashCommandInteraction)context.Interaction;
        var resolvedData = slashInteraction.Data.ResolvedData!;
        var roles = resolvedData.Roles;
        var id = ulong.Parse(value, NumberStyles.None, CultureInfo.InvariantCulture);
        if (roles is not null && roles.TryGetValue(id, out var role))
            return Task.FromResult<object?>(new Mentionable(role));
        else
            return Task.FromResult<object?>(new Mentionable(resolvedData.Users![id]));
    }
}
