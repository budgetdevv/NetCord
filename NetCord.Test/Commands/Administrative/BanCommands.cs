﻿using NetCord.Services;
using NetCord.Services.Commands;

namespace NetCord.Test.Commands.Administrative;

[RequireUserPermission<CommandContext>(Permission.BanUsers), RequireBotPermission<CommandContext>(Permission.BanUsers)]
public class BanCommands : CommandModule
{
    [Command("ban")]
    public async Task Ban(UserId userId, int? days = null, [Remainder] string? reason = null)
    {
        if (Context.Guild != null)
        {
            if (days == null)
                await Context.Guild.BanUserAsync(userId, new() { AuditLogReason = reason });
            else
                await Context.Guild.BanUserAsync(userId, (int)days, new() { AuditLogReason = reason });

            ActionRowProperties actionRow = new(new List<ButtonProperties>
            {
                new ActionButtonProperties($"unban:{userId.Id}", ButtonStyle.Danger)
                {
                    Label = "Unban"
                }
            });
            MessageProperties message = new()
            {
                Content = Format.Bold($"{userId} got banned").ToString(),
                Components = new List<ComponentProperties>()
                {
                    actionRow
                },
                MessageReference = new(Context.Message),
                AllowedMentions = AllowedMentionsProperties.None,
            };
            await SendAsync(message);
        }
        else
            throw new RequiredContextException(RequiredContext.Guild);
    }

    [Command("unban")]
    public async Task Unban(UserId userId, [Remainder] string? reason = null)
    {
        if (Context.Guild != null)
        {
            await Context.Guild.UnbanUserAsync(userId, new() { AuditLogReason = reason });
            await ReplyAsync(Format.Bold($"{userId} got unbanned").ToString());
        }
        else
            throw new RequiredContextException(RequiredContext.Guild);
    }
}