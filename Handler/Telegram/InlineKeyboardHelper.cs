using Telegram.Bot.Requests;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;

namespace TelegramBotHackathonVDCOMDemo.Handler.Telegram
{
	internal class InlineKeyboardHelper
	{
		internal static InlineKeyboardMarkup Get()
		{
			return  new InlineKeyboardMarkup(
				new List<InlineKeyboardButton[]>() 
				{
                    new InlineKeyboardButton[]
                    {
						InlineKeyboardButton.WithUrl("Ссылка на источник", "https://www.google.com/"),
					},
					new InlineKeyboardButton[]
					{
						InlineKeyboardButton.WithCallbackData("Согласовано", "Agreed_Id"),
						InlineKeyboardButton.WithCallbackData("Не согласовано", "Not_Agreed_Id"),
					},
				}
			);
		}
	}
}
