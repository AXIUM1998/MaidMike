using Microsoft.Extensions.Logging;

namespace MaidMike.Helpers; 

public static class LogHelper {
	public static void LogDebug(string msg) {
		Program.Logger.LogDebug(MaidEvents.MaiidMike, msg);
	}
	public static void Log(string msg) {
		Program.Logger.LogInformation(MaidEvents.MaiidMike, msg);
	}
}