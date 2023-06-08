namespace TelegramBot.GameProcess;

public class GameMechanic
{
    private  readonly Random _random;

    public GameMechanic()
    {
        _random = new Random();
    }

    public string RockPaperScissors(string first)
    {
        string[] variable = new[] { "камень", "ножницы", "бумага" };
        int index = _random.Next(0, 3);
        string second = variable[index];

        string answer = (first, second) switch
        {
            ("камень", "бумага") => "Бумага завернула камень. Бумага победила.",
            ("камень", "ножницы") => "Камень сломал ножницы. Камень победил.",
            ("бумага", "камень") => "Бумага завернула камень. Бумага победила.",
            ("бумага", "ножницы") => "Ножницы порезали бумагу. Ножницы победили.",
            ("ножницы", "камень") => "Камень сломал ножницы. Камень победил.",
            ("ножницы", "бумага") => "Ножницы порезали бумагу. Ножницы победили.",
            (_, _) => "Ничья"
        };
        return answer;
    }
}