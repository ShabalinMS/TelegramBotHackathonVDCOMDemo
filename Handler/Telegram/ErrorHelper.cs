using Telegram.Bot;
using Telegram.Bot.Exceptions;
using TelegramBotHackathonVDCOMDemo.Constants;

namespace TelegramBotHackathonVDCOMDemo.Handler.Telegram
{
	internal static class ErrorHelper
	{
		internal static Task Error(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
		{
			var ErrorMessage = error switch
			{
				ApiRequestException apiRequestException
					=> $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
				_ => error.ToString()
			};

			ConfigurationApp.App.Logger.LogError(ErrorMessage);
			return Task.CompletedTask;
		}
	}
}
