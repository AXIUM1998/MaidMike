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
		private static DiscordClient? _discord;
		private static string? _token;
		private static LogLevel _loglevel;

		private static async Task Main() {
			DoConfig();
			
			_discord = new DiscordClient(new DiscordConfiguration() {
            	Token = _token,
            	TokenType = TokenType.Bot,
            	MinimumLogLevel = _loglevel,
                Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
            });
			
			_discord.UseInteractivity(new InteractivityConfiguration() {
				PollBehaviour = PollBehaviour.KeepEmojis,
				Timeout = TimeSpan.FromSeconds(30)
			});
			
			var slash = _discord.UseSlashCommands();
			slash.RegisterCommands<CmdModule>();

			_discord.Logger.LogInformation(MaidEvents.MaiidMike, "MaidMike is starting up...");
			await _discord.ConnectAsync();
			
			await Task.Delay(-1);
		}
		
		private static void DoConfig() {
			var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\config.cfg");
			
			_token = (from x in lines select x.Split("=") into line where line[0].Trim()
				.Equals("token") select line[1].Trim()).FirstOrDefault() ?? throw new Exception("Dunno how you got here, but you did");

			_loglevel = (from x in lines select x.Split("=") into line where line[0].Trim()
				.Equals("loglevel") select line[1].Trim().ToLower()).FirstOrDefault() switch {
				"trace" => LogLevel.Trace,
				"debug" => LogLevel.Debug,
				"info" or "default" => LogLevel.Information,
				"warning" or "warn" => LogLevel.Warning,
				"error"=> LogLevel.Error,
				"critical" => LogLevel.Critical,
				"none" => LogLevel.None,
				_ => throw new Exception("Invalid loglevel in config.cfg")
			};
		}
		
		public static ILogger<BaseDiscordClient> Logger => _discord?.Logger ?? throw new Exception("Discord client not initialized");
	}
}