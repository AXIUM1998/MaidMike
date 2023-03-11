using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace MaidMike.Commands {
	public class CmdModule : BaseCommandModule {
        
		[Command("ping")]
		public async Task PingCmd(CommandContext ctx) {
			await ctx.RespondAsync("Pong!");
		}
	}
}