using BigTwo.GameLogic;
using BigTwo.Models;

namespace BigTwo.UI;

public static class InputHandler
{
    private const int TotalPlayers = 4;
    private static readonly char[] Separator = [' ', ',', '\t'];

    public static List<Card> GetPlayerInput(Player player, GameContext? context = null)
    {
        // 判斷是 Human 還是 AI，調用各自的 GetSelectedCards
        return player.GetSelectedCards(context);
    }
    
    /// <summary>
    /// 人類玩家的輸入方法
    /// </summary>
    public static List<Card> GetHumanInput(Player player)
    {
        while (true)
        {
            var input = Console.ReadLine()?.Trim();

            if (InValid(input))
            {
                Console.WriteLine("請輸入內容！(輸入卡牌編號或 -1 跳過)");
                Console.Write("請重新輸入: ");
                continue;
            }

            return ParseCardIndices(input!, player);
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
        }
        return selectedCards;
    }

    public static bool ConfirmPlayAgain(string message)
    {
        Console.Write($"{message} (y/n): ");
        var input = Console.ReadLine()?.Trim().ToLower();
        return input is "y" or "yes";
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

    /// <summary>
    /// 詢問玩家類型（Human 或 AI）
    /// </summary>
    /// <param name="playerIndex">玩家索引（從 0 開始）</param>
    /// <param name="playerName">玩家名稱</param>
    /// <returns>true 表示 AI，false 表示 Human</returns>
    public static bool AskPlayerType(int playerIndex, string playerName)
    {
        while (true)
        {
            Console.Write($"玩家 {playerIndex + 1} ({playerName}) 的類型 (H=人類, A=AI，直接按 Enter 預設為人類): ");
            var input = Console.ReadLine()?.Trim().ToLower();
            
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"  → 使用預設類型: 人類");
                Console.ResetColor();
                return false;
            }
            
            if (input is "h" or "human" or "人" or "人類")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  → 類型已設定: 人類");
                Console.ResetColor();
                return false;
            }
            
            if (input is "a" or "ai" or "機器人")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  → 類型已設定: AI");
                Console.ResetColor();
                return true;
            }
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  無效輸入！請輸入 H (人類) 或 A (AI)");
            Console.ResetColor();
        }
    }
}

