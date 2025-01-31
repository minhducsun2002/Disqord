﻿using System.Threading.Tasks;
using Disqord.Gateway;
using Qmmands;

namespace Disqord.Bot
{
    /// <summary>
    ///     Specifies that the module or command can only be executed by authors with the given channel permissions.
    /// </summary>
    public class RequireAuthorChannelPermissionsAttribute : DiscordGuildCheckAttribute
    {
        public ChannelPermissions Permissions { get; }

        public RequireAuthorChannelPermissionsAttribute(Permission permissions)
        {
            Permissions = permissions;
        }

        public override ValueTask<CheckResult> CheckAsync(DiscordGuildCommandContext context)
        {
            var permissions = context.Author.GetPermissions(context.Channel);
            if (permissions.Has(Permissions))
                return Success();

            return Failure($"You lack the necessary channel permissions ({Permissions - permissions}) to execute this.");
        }
    }
}
