using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;

using MaidMike.Helpers;
using MaidMike.Commands;

namespace MaidMike {
	internal static class Program {
		private static DiscordClient? _discord;
		private static string? _token;
		private static string? _prefix;
		
		private static async Task Main() {
			DoConfig();
			_discord = new DiscordClient(new DiscordConfiguration() {
				Token = _token,
				TokenType = TokenType.Bot
			});

			_discord.UseInteractivity(new InteractivityConfiguration() {
				PollBehaviour = PollBehaviour.KeepEmojis,
				Timeout = TimeSpan.FromSeconds(30)
			});

			var commands = _discord.UseCommandsNext(new CommandsNextConfiguration() { StringPrefixes = new[] {_prefix}});
			commands.RegisterCommands<CmdModule>();
			commands.RegisterConverter(new ArgConverter());
            
			await _discord.ConnectAsync();
			await Task.Delay(-1);
		}

		private static void DoConfig() {
			var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\config.cfg");
			_token = (from x in lines select x.Split("=") into line where line[0].Trim().Equals("token") select line[1]).FirstOrDefault() ?? throw new Exception("Invalid token configuration");
			_prefix = (from x in lines select x.Split("=") into line where line[0].Trim().Equals("prefix") select line[1]).FirstOrDefault() ?? "!";
		}
	}
}