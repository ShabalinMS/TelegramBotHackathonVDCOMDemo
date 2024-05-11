using Newtonsoft.Json;
using System;
using System.Xml.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotHackathonVDCOMDemo.DB;
using TelegramBotHackathonVDCOMDemo.Handler.CRM;
using TelegramBotHackathonVDCOMDemo.Model;
using TelegramBotHackathonVDCOMDemo.Resource;

namespace TelegramBotHackathonVDCOMDemo.Handler.Telegram
{
	/// <summary>
	/// Вспомогательный класс по работе с формами
	/// </summary>
	internal class MiniAppHelper
	{
		private ITelegramBotClient _botClient;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="botClient">Клиент</param>
		internal MiniAppHelper(ITelegramBotClient botClient)
		{
			_botClient = botClient;
		}

		/// <summary>
		/// Работат с формой
		/// </summary>
		/// <param name="message">Сообщение</param>
		/// <returns>{true - была выполнена работа с формой(остановить обработку)}{false -  продолжить обработку}</returns>
		internal async Task<bool> WorkingWithForm(Message message)
		{
			if (message.WebAppData != null)
			{
				if (message.WebAppData.ButtonText == ResourceRu.BtnAuthorization)
				{
					await AuthenticationContact(message.WebAppData.Data, message.Chat.Id);
				} else if(message.WebAppData.ButtonText == ResourceRu.CreateTrip)
				{
					var trip = JsonConvert.DeserializeObject<TripModel>(message.WebAppData.Data);
					if (trip == null) return false;

					trip.ChatId = message.Chat.Id.ToString();
					await Task.WhenAll(
						SQLiteHelper.Post(trip),
						_botClient.SendTextMessageAsync (
							message.Chat.Id,
							string.Format(ResourceRu.CreateNewTrip, trip.City, trip.Description)
						)
					);
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// Аунтификация контакта
		/// </summary>
		/// <param name="response">Данные для аунтификации</param>
		/// <param name="chatId">Идентификатор чата</param>
		private async Task AuthenticationContact(string response, long chatId)
		{
			bool check = await new CheckEmplyeeHandler().Check(response);
			if (check) {
				(string name, string surname) = await new GetProfileHelper().Get();

				await Task.WhenAll(
					SQLiteHelper.CreateContact(
						new Model.ContactModel()
						{
							ChatId = chatId,
							Name = $"{name}, {surname}",
							Id = Guid.NewGuid().ToString()
						}
					),
					_botClient.SendTextMessageAsync(
						chatId,
						String.Format(ResourceRu.Welcome, name, surname),
						replyMarkup: await ReplyKeyboardHelper.GetParamReply()
					)
				);
			} else
			{
				await _botClient.SendTextMessageAsync(chatId, String.Format(ResourceRu.AuthorizationFailed));
			}
		}
	}
}
