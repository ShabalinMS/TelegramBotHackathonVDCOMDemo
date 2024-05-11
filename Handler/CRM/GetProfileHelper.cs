namespace TelegramBotHackathonVDCOMDemo.Handler.CRM
{
	/// <summary>
	/// Получение данных из CRM по контакту
	/// </summary>
	public class GetProfileHelper
	{
		/// <summary>
		/// Получить параметры контакта
		/// </summary>
		/// <returns>(Имя, Отчество)</returns>
		public async Task<(string name, string surname)> Get()
		{
			return ("Супервизор", "Админович");
		}
	}
}
