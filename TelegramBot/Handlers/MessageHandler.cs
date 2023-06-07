using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Contracts;
using TelegramBot.GameProcess;

namespace TelegramBot.Handlers;

public class MessageHandler : IBotUpdateHandler
{
    private readonly TelegramBotClient _botClient;
    private readonly StartGame _startGame;

    public MessageHandler(TelegramBotClient botClient)
    {
        _botClient = botClient;
        _startGame = new StartGame(botClient);
    }

    public async Task HandlerUpdateAsync(Update? update, CancellationToken cancellationToken)
    {
        var chatId = update.Message.Chat.Id;
        var inputMessage = update.Message.Text;
        var outputMessage = "";
            
        switch (inputMessage)
        {
            case "/start":
                outputMessage = "Привет боец!\n" +
                    "Битва начнется когда ты дашь команду /game\n" +
                    "Если хочешь узнать о правилах легендарной битвы скажи /help";
                break;
            case "/help":
                outputMessage = "Правила просты\n" +
                                "Битва не окончится по ты сам не закончишь\n" +
                                "У тебя и у противника есть 3 вида оружия:\n \n" +
                                "Смертоносный булыжник\n" +
                                "Всепоглощающее полотно бесконечности\n" +
                                "Двойные мечи бога войны\n \n" +
                                "За один ход можно использвать одно оружие";
                break;
            case "/game":
                _startGame.HandlerUpdateAsync(update, cancellationToken);
                break;
            case "/stop":
                outputMessage = "Ты бидся храбро, благодарю за бой";
                break;
            default:
                outputMessage = "Ты говоришь на языке древних, нам не понять тебя";
                break;
        }
        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            outputMessage,
            cancellationToken: cancellationToken);
    }
}