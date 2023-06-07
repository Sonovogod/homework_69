using Telegram.Bot.Types;

namespace TelegramBot.Contracts;

public interface IBotUpdateHandler
{
    Task HandlerUpdateAsync(Update update, CancellationToken cancellationToken);
}