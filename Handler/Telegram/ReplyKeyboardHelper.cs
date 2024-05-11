using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using TelegramBotHackathonVDCOMDemo.Resource;

namespace TelegramBotHackathonVDCOMDemo.Handler.Telegram
{
	internal static class ReplyKeyboardHelper
	{
		internal static async Task<ReplyKeyboardMarkup> GetParamReply()
		{
			WebAppInfo site = new WebAppInfo();
			site.Url = "https://shabalinms.github.io/TelegramBotWeb/trip.html";

			var replyKeyboard = new ReplyKeyboardMarkup(
				new List<KeyboardButton[]>()
				{
					new KeyboardButton[]
					{
						new KeyboardButton(ResourceRu.GetStatusTrip),
					},
					new KeyboardButton[]
					{
						KeyboardButton.WithWebApp(ResourceRu.CreateTrip, site)
					},
					new KeyboardButton[]
					{
						new KeyboardButton(ResourceRu.Exit),
					},
					new KeyboardButton[]
					{
						new KeyboardButton("Уведомление о необходимости согласовать (Test)")
					},
					new KeyboardButton[]
					{
						new KeyboardButton("Уведомление о изменении состояния командировки (Test)")
					}
				}
			)
			{
				ResizeKeyboard = true,
			};

			return replyKeyboard;
		}
	}
}
