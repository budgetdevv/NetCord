﻿using NetCord.Services.Interactions;

namespace NetCord.Test;

public class MenuInteractions : BaseInteractionModule<MenuInteractionContext>
{
    [Interaction("roles")]
    public async Task Roles()
    {
        var user = Context.User;
        if (user is GuildUser guildUser)
        {
            var selectedValues = Context.Interaction.Data.SelectedValues.Select(s => new DiscordId(s));
            await guildUser.ModifyAsync(x => x.NewRolesIds = selectedValues);
            await Context.Interaction.SendResponseAsync(InteractionCallback.ChannelMessageWithSource(new() { Content = "Roles updated" }));
        }
        else
            await Context.Interaction.SendResponseAsync(InteractionCallback.ChannelMessageWithSource(new() { Content = "You are not in guild" }));
    }

    [Interaction("menu")]
    public Task Menu()
    {
        InteractionMessageProperties interactionMessage = new()
        {
            Flags = MessageFlags.Ephemeral,
            Content = "You selected: " + string.Join(", ", Context.Interaction.Data.SelectedValues),
        };
        return Context.Interaction.SendResponseAsync(InteractionCallback.ChannelMessageWithSource(interactionMessage));
    }
}