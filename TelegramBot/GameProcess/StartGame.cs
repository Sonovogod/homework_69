using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Contracts;
using TelegramBot.Handlers;

namespace TelegramBot.GameProcess;

public class StartGame : IBotUpdateHandler
{
    private readonly TelegramBotClient _botClient;
    private readonly GameMechanic _gameMechanic;
    private readonly MessageHandler _messageHandler;
    private readonly InlineKeyboardHandler _inlineKeyboardHandler;

    public StartGame(TelegramBotClient botClient)
    {
        _botClient = botClient;
        _gameMechanic = new GameMechanic();
        _messageHandler = new MessageHandler(botClient);
        _inlineKeyboardHandler = new InlineKeyboardHandler(botClient);
    }
    
    public async Task HandlerUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        var chatId = update.CallbackQuery.Message.Chat.Id;
        var callBackAnswer = update.CallbackQuery.Message.Text;
        var outputMessage = _gameMechanic.RockPaperScissors(callBackAnswer);
        
        var inlineButtons = new InlineKeyboardMarkup(new[]
        {
            InlineKeyboardButton.WithCallbackData("Закончить", "/stop"),
            InlineKeyboardButton.WithCallbackData("Продолжить", "/game"),
        });
        
        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            outputMessage,
            replyMarkup: inlineButtons,
            cancellationToken: cancellationToken);
    }
}