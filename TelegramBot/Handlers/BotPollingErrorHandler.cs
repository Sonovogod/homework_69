using Telegram.Bot.Exceptions;

namespace TelegramBot.Handlers;

public class BotPollingErrorHandler
{
    public Task HandlePollingErrorAsync(Exception exception, CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException =>
                $"Telegram Api Error: {apiRequestException.ErrorCode} {apiRequestException.Message}",
            _ => exception.ToString()
        };
        return Task.CompletedTask;
    }
}