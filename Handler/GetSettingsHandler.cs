using TelegramBotHackathonVDCOMDemo.Constants;

namespace TelegramBotHackathonVDCOMDemo.Helper
{
	internal class GetSettingsHandler
	{
		private IConfiguration _configuration;
		internal GetSettingsHandler(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		internal string GetValue(string key)
		{
			string? result = _configuration[key];
			if (result == null)
				throw new Exception($"There is no system setting {key}");
			return result;
		}
	}
}
