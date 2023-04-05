using DSharpPlus;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;

using MaidMike.Helpers;
using MaidMike.Commands;
using Microsoft.Extensions.Logging;

namespace MaidMike {
	internal static class Program {
		private static DiscordClient _discord = null!;

		private static async Task Main() {
			BotInit();
			
			if(_discord is null) throw new Exception("Discord bot failed to initialize");
			
			_discord.UseInteractivity(new InteractivityConfiguration() {
				PollBehaviour = PollBehaviour.KeepEmojis,
				Timeout = TimeSpan.FromSeconds(30)
			});
			
			var slash = _discord.UseSlashCommands();
			slash.RegisterCommands<CmdModule>();

			LogHelper.Log( "MaidMike is starting up...");
			await _discord.ConnectAsync();
			
			await Task.Delay(-1);
		}
		
		private static void BotInit() {
			var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\config.cfg");

			_discord = new DiscordClient(new DiscordConfiguration() {
				TokenType = TokenType.Bot,
				Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents,
				Token = (from x in lines select x.Split("=") into line where line[0].Trim()
					.Equals("token") select line[1].Trim()).FirstOrDefault() ?? throw new Exception("Invalid token in config.cfg"),
				MinimumLogLevel = (from x in lines select x.Split("=") into line where line[0].Trim()
					.Equals("loglevel") select line[1].Trim().ToLower()).FirstOrDefault() switch {
					"trace" => LogLevel.Trace,
					"debug" => LogLevel.Debug,
					"info" or "default" => LogLevel.Information,
					"warning" or "warn" => LogLevel.Warning,
					"error"=> LogLevel.Error,
					"critical" => LogLevel.Critical,
					"none" => LogLevel.None,
					_ => throw new Exception("Invalid loglevel in config.cfg"),
				}
			});
		}
		
		public static ILogger<BaseDiscordClient> Logger => _discord.Logger ?? throw new Exception("Discord client not initialized");
	}
}