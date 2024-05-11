using TelegramBotHackathonVDCOMDemo.Helper;

namespace TelegramBotHackathonVDCOMDemo.Constants
{
	public static class ConfigurationApp
	{
		public static string? KeyTelegram { get; private set; }

		public static WebApplication? App { get; private set; }

		public static string? ConnectionStringDB { get; private set; }

		internal static WebApplication SetSettings
		{
			set
			{
				App = value;
				KeyTelegram = new GetSettingsHandler(value.Configuration).GetValue("TelegramBotKey");
				ConnectionStringDB = new GetSettingsHandler(value.Configuration).GetValue("ConnectionStringDB");
			}
		}
	}
}
