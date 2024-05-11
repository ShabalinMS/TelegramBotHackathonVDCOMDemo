using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using TelegramBotHackathonVDCOMDemo.Constants;

namespace TelegramBotHackathonVDCOMDemo.Handler.Telegram
{
	internal static class BotHelper
	{
		internal static void Launch()
		{
			string? key = ConfigurationApp.KeyTelegram;
			if(key == null)
			{
				throw new Exception("The key for the bot is missing");
			}
			ITelegramBotClient botClient = new TelegramBotClient(key);

			ReceiverOptions receiverOptions = new ReceiverOptions
			{
				AllowedUpdates = new[] 
				{
					UpdateType.Message,
					UpdateType.CallbackQuery
				},

				ThrowPendingUpdates = true,
			};

			using var cts = new CancellationTokenSource();

			botClient.StartReceiving(UpdateHelper.Update, ErrorHelper.Error, receiverOptions, cts.Token);

			ConfigurationApp.App.Logger.LogInformation($"Start bot: {botClient.GetMeAsync()}");
		}
	}
}
