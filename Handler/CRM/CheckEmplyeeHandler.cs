using Newtonsoft.Json;
using TelegramBotHackathonVDCOMDemo.Model;

namespace TelegramBotHackathonVDCOMDemo.Handler.CRM
{
	/// <summary>
	/// Проверка пользователя в CRM
	/// </summary>
	public class CheckEmplyeeHandler
	{
		/// <summary>
		/// Проверка
		/// </summary>
		/// <param name="responce"></param>
		/// <returns>{true - проверка пройдена}{false - проверка не пройдена}</returns>
		public async Task<bool> Check(string responce)
		{
			AuthModel? auth = JsonConvert.DeserializeObject<AuthModel>(responce);
			if (auth == null) return false;

			return (auth.Login == "Supervisor" && auth.Password == "Supervisor");
		}
	}
}
