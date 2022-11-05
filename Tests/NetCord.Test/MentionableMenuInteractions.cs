﻿using NetCord.Services.Interactions;

namespace NetCord.Test;

public class MentionableMenuInteractions : InteractionModule<MentionableMenuInteractionContext>
{
    [Interaction("mentionables")]
    public Task MentionablesAsync()
    {
        return RespondAsync(InteractionCallback.ChannelMessageWithSource($"You selected: {string.Join(", ", Context.SelectedMentionables.Values.Select(m => m.Type == Services.MentionableType.User ? m.User!.ToString() : m.Role!.ToString()))}"));
    }
}