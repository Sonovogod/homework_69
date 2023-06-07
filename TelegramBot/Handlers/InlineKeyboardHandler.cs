using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Contracts;

namespace TelegramBot.Handlers;

public class InlineKeyboardHandler : IBotUpdateHandler
{
    private readonly TelegramBotClient _botClient;

    public InlineKeyboardHandler(TelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task HandlerUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        var inputMessage = update.CallbackQuery.Message.Text;
        switch (inputMessage)
        {
            case "/stop":
                _messageHandler.HandlerUpdateAsync(update, cancellationToken);
                break;
            case "/game":
                _inlineKeyboardHandler.HandlerUpdateAsync(update, cancellationToken);
                break;
        }
        
        
        var chatId = update.CallbackQuery.Message.Chat.Id;
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