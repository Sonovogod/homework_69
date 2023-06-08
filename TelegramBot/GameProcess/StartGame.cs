using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Contracts;
using TelegramBot.Handlers;

namespace TelegramBot.GameProcess;

public class StartGame : IBotUpdateHandler
{
    private readonly TelegramBotClient _botClient;

    public StartGame(TelegramBotClient botClient)
    {
        _botClient = botClient;
    }
    
    public async Task HandlerUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        var chatId = update.CallbackQuery?.Message?.Chat.Id == null ? 
            update.Message.Chat.Id : update.CallbackQuery?.Message?.Chat.Id;
        var outputMessage = "Выберите оружие";
        var inlineButtons = new InlineKeyboardMarkup(new[]
        {
            InlineKeyboardButton.WithCallbackData("Камень", "камень"),
            InlineKeyboardButton.WithCallbackData("Ножницы", "ножницы"),
            InlineKeyboardButton.WithCallbackData("Бумага", "бумага")
        });

        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            outputMessage,
            replyMarkup: inlineButtons,
            cancellationToken: cancellationToken);
    }
}