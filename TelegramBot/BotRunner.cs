using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Contracts;
using TelegramBot.GameProcess;
using TelegramBot.Handlers;

namespace TelegramBot;

public class BotRunner
{
    private IBotUpdateHandler? _botUpdateHandler;
    private readonly BotPollingErrorHandler _errorHandler;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly TelegramBotClient _botClient;

    public BotRunner(string token)
    {
        _botClient = new TelegramBotClient(token);
        _errorHandler = new BotPollingErrorHandler();
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public async Task StartAsync()
    {
        var me = await _botClient.GetMeAsync();
        Console.WriteLine(me.Username);
        _botClient.StartReceiving(
            HandleUpdateAsync,
            HandlePollingErrorASync,
            null,
            _cancellationTokenSource.Token);
        Console.ReadKey();
        Console.WriteLine("Завершеине работы");
        _cancellationTokenSource.Cancel();
    }

    private async Task HandleUpdateAsync(
        ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                _botUpdateHandler = new MessageHandler(_botClient);
                break;
            case UpdateType.CallbackQuery:
                _botUpdateHandler = new StartGame(_botClient);
                break;
        }

        if (_botUpdateHandler is not null)
            await _botUpdateHandler.HandlerUpdateAsync(update, cancellationToken);
    }

    private async Task HandlePollingErrorASync(ITelegramBotClient botClient,
        Exception exception,
        CancellationToken cancellationToken)
    {
        await _errorHandler.HandlePollingErrorAsync(exception, cancellationToken);
    }
}