using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TelegramBotHackathonVDCOMDemo.DB;
using TelegramBotHackathonVDCOMDemo.Model;

namespace TelegramBotHackathonVDCOMDemo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContactController : ControllerBase
	{
		/// <summary>
		/// Получить всех контактов
		/// </summary>
		/// <returns>Коллекция контактов в системе</returns>
		[HttpGet]
		public async Task<IEnumerable<ContactModel>> Get()
		{
			return await SQLiteHelper.GetContacts();
		}

		/// <summary>
		/// Добавить нового контакта
		/// </summary>
		/// <param name="contact">Данные по контакту</param>
		/// <returns>Добавленный контакт</returns>
		[HttpPost]		
		public async Task<ContactModel> Post(ContactModel contact)
		{
			return await SQLiteHelper.CreateContact(contact);
		}

		[HttpDelete]
		public async Task<bool> Delete(Guid contactId)
		{
			return await SQLiteHelper.DeleteContact(contactId);
		}
	}
}
