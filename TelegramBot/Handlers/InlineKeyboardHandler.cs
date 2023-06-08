using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Contracts;
using TelegramBot.GameProcess;

namespace TelegramBot.Handlers;

public class InlineKeyboardHandler : IBotUpdateHandler
{
    private readonly TelegramBotClient _botClient;
    private readonly GameMechanic _gameMechanic;
    private readonly StartGame _startGame;

    public InlineKeyboardHandler(TelegramBotClient botClient)
    {
        _botClient = botClient;
        _gameMechanic = new GameMechanic();
        _startGame = new StartGame(botClient);
    }

    public async Task HandlerUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        var inputMessage = update.CallbackQuery.Data;
        var chatId = update.CallbackQuery.Message.Chat.Id;

        switch (inputMessage)
        {
            case "/stop":
                await _botClient.SendTextMessageAsync(
                    chatId: chatId,
                    "Ты бился храбро, благодарю за бой",
                    cancellationToken: cancellationToken);
                return;
            case "/game":
                await _startGame.HandlerUpdateAsync(update, cancellationToken);
                return;
        }
        
        var winner = _gameMechanic.RockPaperScissors(inputMessage);

        var inlineButtons = new InlineKeyboardMarkup(new[]
        {
            InlineKeyboardButton.WithCallbackData("Закончить", "/stop"),
            InlineKeyboardButton.WithCallbackData("Продолжить", "/game"),
        });


        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            winner,
            replyMarkup: inlineButtons,
            cancellationToken: cancellationToken);

    }
    
    
}