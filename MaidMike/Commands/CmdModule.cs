using System.Text.RegularExpressions;
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

		[SlashCommand("roll", "Rolls a dice")]
		public async Task RollCmd(InteractionContext ctx, [Option("roll", "The dice to roll")] string dice="1d20"){
			LogHelper.LogDebug("hit roll command");
			// create regex for +, -, and *, trimming whitespace, and splitting on the + and - signs
			var regex = new Regex(@"\s*([+-/*])\s*");
			var split = regex.Split(dice);
			
			await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"Rolling {dice}..."));
		}
	}
}