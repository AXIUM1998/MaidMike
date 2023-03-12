using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using MaidMike.Helpers;

namespace MaidMike.Commands {
	public class CmdModule : ApplicationCommandModule {
        
		[SlashCommand("ping", "pong")]
		public async Task PingCmd(InteractionContext ctx) {
			LogHelper.LogDebug("hit ping command"); 
			await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("pong"));
		}
    }
}