using Microsoft.EntityFrameworkCore;
using TelegramBotHackathonVDCOMDemo.Model;

namespace TelegramBotHackathonVDCOMDemo.DB
{
	internal class SQLiteHelper
	{
		internal static void InitDB()
		{
			using (ContextDB db = new ContextDB())
			{
				db.Database.EnsureCreated();
			}
		}

		#region Contact

		/// <summary>
		/// Проверка наличия пользователя в системе
		/// </summary>
		/// <param name="chatId"></param>
		/// <returns>{true - пользователь есть в системе}{false - к пользователю не привязан чат}</returns>
		internal static async Task<bool> CheckAvailabilityContact(long chatId)
		{
			using (ContextDB db = new ContextDB())
			{
				ContactModel? contact = await db.Contact.FirstOrDefaultAsync(x=>x.ChatId.Equals(chatId));
				return contact != null;
			}
		}

		/// <summary>
		/// Получить коллекцию всех контактов
		/// </summary>
		/// <returns>Коллекция контактов</returns>
		internal static async Task<IEnumerable<ContactModel>> GetContacts()
		{
			using(ContextDB db = new ContextDB())
			{
				return await db.Contact.ToListAsync();
			}
		}

		/// <summary>
		/// Добавление нового контакта
		/// </summary>
		/// <param name="contact">Данные по контакту</param>
		/// <returns>Новая сущность контакта</returns>
		internal static async Task<ContactModel> CreateContact(ContactModel contact)
		{
			contact.Id = Guid.NewGuid().ToString();

			using (ContextDB db = new ContextDB())
			{
				await db.Contact.AddAsync(contact);
				await db.SaveChangesAsync();
			}

			return contact;
		}

		/// <summary>
		/// Удаление контакта из системы
		/// </summary>
		/// <param name="contactId">Идентификат контакта</param>
		/// <returns>Результат выполнения</returns>
		internal static async Task<bool> DeleteContact(Guid contactId)
		{
			using (ContextDB db = new ContextDB())
			{
				db.Contact.Remove(new ContactModel() { Id = contactId.ToString()});
				await db.SaveChangesAsync();
				return true;
			}
		}

		internal static async Task DeleteContactByChat(long chatId)
		{
			using (ContextDB db = new ContextDB())
			{
				var contact = db.Contact.FirstOrDefault(x => x.ChatId.Equals(chatId));
				if (contact == null) return;
				db.Contact.Remove(contact);
				await db.SaveChangesAsync();
			}
		}

		#endregion

		#region Trip

		internal static async Task<List<TripModel>> Index()
		{
			using (ContextDB db = new ContextDB())
			{
				return await db.Trip.ToListAsync();
			}
		}

		internal static async Task<IEnumerable<TripModel>> Get(string chatId)
		{
			using (ContextDB db = new ContextDB())
			{
				return  await db.Trip.Where(x=>x.ChatId.Equals(chatId)).ToListAsync();
			}
		}

		internal static async Task<TripModel> Post(TripModel trip)
		{
			trip.Id = Guid.NewGuid().ToString();
			using (ContextDB db = new ContextDB())
			{
				await db.Trip.AddAsync(trip);
				await db.SaveChangesAsync();
			}

			return trip;
		}

		#endregion

	}
}
