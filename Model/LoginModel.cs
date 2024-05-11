using Newtonsoft.Json;

namespace TelegramBotHackathonVDCOMDemo.Model
{
	public class AuthModel
	{
		[JsonProperty("login")]
		public string Login { get; set; } = null!;

		[JsonProperty("password")]
		public string Password { get; set; } = null!;
	}
}
