using BigTwo.Models;

namespace BigTwo.UI;

public static class InputHandler
{
    private const int TotalPlayers = 4;
    private static readonly char[] Separator = [' ', ',', '\t'];

    public static (bool isQuit, bool isPass, List<Card> cards) GetPlayerInput(Player player)
    {
        while (true)
        {
            var input = Console.ReadLine()?.Trim().ToLower();

            if (InValid(input))
            {
                Console.WriteLine("請輸入內容！(輸入卡牌編號、'pass' 跳過、或 'quit' 離開)");
                Console.Write("請重新輸入: ");
                continue;
            }

            if (input is "quit" or "exit")
            {
                return (true, false, []);
            }

            if (input is "pass" or "p")
            {
                return (false, true, []);
            }

            var selectedCards = ParseCardIndices(input!, player);
            return (false, false, selectedCards);
        }
    }

    private static bool InValid(string? input)
    {
        return string.IsNullOrEmpty(input);
    }

    private static List<Card> ParseCardIndices(string input, Player player)
    {
        var selectedCards = new List<Card>();
        var cards = player.GetHandCards();

        var parts = input.Split(Separator, StringSplitOptions.RemoveEmptyEntries);

        foreach (var part in parts)
        {
            if (!int.TryParse(part, out var index)) continue;
            var cardIndex = index - 1;

            if (cardIndex >= 0 && cardIndex < cards.Count)
            {
                selectedCards.Add(cards[cardIndex]);
            }
            //TODO: Handle if out of Cards range, need to input again
        }
        return selectedCards;
    }

    /// <summary>
    /// 取得確認輸入
    /// </summary>
    public static bool GetConfirmation(string message)
    {
        Console.Write($"{message} (y/n): ");
        string? input = Console.ReadLine()?.Trim().ToLower();
        return input == "y" || input == "yes";
    }

    public static List<string> GetPlayerNames()
    {
        var names = new List<string>(TotalPlayers);
        
        Console.WriteLine("請輸入玩家名字（直接按 Enter 使用預設名字）\n");
        
        for (var playerIndex = 0; playerIndex < TotalPlayers; playerIndex++)
        {
            Console.Write($"玩家 {playerIndex + 1} 的名字: ");
            var input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
            {
                UseDefaultName(playerIndex, names);
            }
            else
            {
                UseAssignNameByPlayer(input, names);
            }

            Console.ResetColor();
        }
        
        return names;
    }

    private static void UseAssignNameByPlayer(string input, List<string> names)
    {
        var playerName = input.Trim();
        names.Add(playerName);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"  → 名字已設定: {playerName}");
    }

    private static void UseDefaultName(int playerIndex, List<string> names)
    {
        var defaultName = $"玩家 {playerIndex + 1}";
        names.Add(defaultName);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"  → 使用預設名字: {defaultName}");
    }
}

