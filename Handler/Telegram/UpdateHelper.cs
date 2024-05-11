using System;
using System.Text;
using System.Xml.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotHackathonVDCOMDemo.DB;
using TelegramBotHackathonVDCOMDemo.Model;
using TelegramBotHackathonVDCOMDemo.Resource;

namespace TelegramBotHackathonVDCOMDemo.Handler.Telegram
{
	internal static class UpdateHelper
	{
		private static ITelegramBotClient _botClient;
		internal static async Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
		{
			if (update.Type != UpdateType.Message && update.Type != UpdateType.CallbackQuery) return;
			
			
					
			_botClient = botClient;

			if(update.Type == UpdateType.Message)
			{
				var message = update.Message;

				if (message == null) return;

				var chat = message.Chat;

				bool IsNext = await new MiniAppHelper(botClient).WorkingWithForm(message);
				if (IsNext) return;

				bool isAuthorization = await CheckAuthorization(chat.Id);
				if (!isAuthorization)
				{
					return;
				}

				if (message.Text == "/start")
				{
					await StageGetBaseMenu(chat.Id);
				}
				else if (message.Text == "Получить состояния командировок")
				{
					await WriteTrips(chat.Id);
				}
				else if (message.Text == "Уведомление о изменении состояния командировки (Test)")
				{
					await TestNotificationUpdateStatusTrip(chat.Id);
				}
				else if (message.Text == "Уведомление о необходимости согласовать (Test)")
				{
					await botClient.SendTextMessageAsync(
						chat.Id,
						"Необходимо согласовать командировку сотрудник ... город ... дата ...",
						replyMarkup: InlineKeyboardHelper.Get()
					);
				} if (message.Text == "Выйти")
				{
					await Task.WhenAll(
						StageAuth(chat.Id),
						SQLiteHelper.DeleteContactByChat(chat.Id)
					);
				}
			} else if (update.Type == UpdateType.CallbackQuery) {
				var callbackQuery = update.CallbackQuery;

				var user = callbackQuery.From;

				var chat = callbackQuery.Message.Chat;

				switch (callbackQuery.Data)
				{
					case "Agreed_Id":
						{
							await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);

							await botClient.SendTextMessageAsync(
								chat.Id,
								"Конмандировка ... согласована");
							return;
						}

					case "Not_Agreed_Id":
						{
							await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Тут может быть ваш текст!");

							await botClient.SendTextMessageAsync(
								chat.Id,
								"Командировка ... не согласована");
							return;
						}
				}

				return;
			}	
		}
		


		#region Methods private

		private static async Task TestNotificationUpdateStatusTrip(long chatId)
		{
			await _botClient.SendTextMessageAsync(
					chatId,
					string.Format(ResourceRu.NotificationUpdateStatusTrip, " дата 11.05 в город Самара ")
				);
		}

		private static async Task WriteTrips(long chatId)
		{
			IEnumerable<TripModel> tripList = await SQLiteHelper.Get(chatId.ToString());

			StringBuilder resultText = new StringBuilder();
			StringBuilder resultHTML = new StringBuilder();
			resultHTML.AppendLine("<pre> Можно и такой вариант возвращать");
			resultHTML.AppendLine("| Город | Статус |");
			foreach (TripModel trip in tripList)
			{
				resultText.AppendLine($"Город: {trip.City}, Статус: На согласовании");
				resultHTML.AppendLine($"| {trip.City}  |  На согласовании |");
			}
			resultHTML.AppendLine("</pre>");
			await Task.WhenAll(
				_botClient.SendTextMessageAsync(
					chatId,
					resultHTML.ToString(),
					parseMode: ParseMode.Html
				),
				_botClient.SendTextMessageAsync(
					chatId,
					resultText.ToString()
				)
			);
		}

		/// <summary>
		/// Получить общее меню
		/// </summary>
		/// <param name="chatId">Идентификатор чата</param>
		private static async Task StageGetBaseMenu(long chatId)
		{
			await _botClient.SendTextMessageAsync(
				chatId,
				ResourceRu.WelcomeAgain,
				replyMarkup: await ReplyKeyboardHelper.GetParamReply()
			);
		}

		/// <summary>
		/// Проверка авторизации пользователя, пользователю передается сообщение о необходимости пройти авторизацию
		/// </summary>
		/// <param name="chatId">Идентификатор чата</param>
		private static async Task<bool> CheckAuthorization(long chatId)
		{
			bool result = await SQLiteHelper.CheckAvailabilityContact(chatId);
			if (!result)
			{
				await StageAuth(chatId);
			}
			return result;
		}

		private static async Task StageAuth(long chatId)
		{
			WebAppInfo site = new WebAppInfo();
			site.Url = "https://shabalinms.github.io/TelegramBotWeb/index.html";

			var replyKeyboard = new ReplyKeyboardMarkup(
				new List<KeyboardButton[]>()
				{
					new KeyboardButton[]
					{
						KeyboardButton.WithWebApp(ResourceRu.BtnAuthorization, site)
					}
				}
			)
			{
				ResizeKeyboard = true,
			};

			await _botClient.SendTextMessageAsync(
				chatId,
				ResourceRu.GoToAuthorization,
				replyMarkup: replyKeyboard
			);
		}

		#endregion

	}
}