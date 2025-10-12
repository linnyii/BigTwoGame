using BigTwo.Models;
using BigTwo.GameLogic;

namespace BigTwo.UI;

public static class ConsoleUI
{
    public static void DisplayTitle()
    {
        Console.Clear();
        Console.WriteLine("====================================");
        Console.WriteLine("         welcome Big Two            ");
        Console.WriteLine("====================================");
        Console.WriteLine();
    }

    public static void DisplayHand(Player player)
    {
        Console.WriteLine($"\n{player.Name} 的手牌 ({player.Hand.Count} 張):");
        Console.WriteLine();
        
        var cards = player.Hand.Cards.ToList();
        
        for (var i = 0; i < cards.Count; i++)
        {
            Console.Write($"{i + 1,2}  ");
        }
        Console.WriteLine();
        
        foreach (var card in cards)
        {
            var cardDisplay = $"{card.Suit.Name}[{card.RankSymbol}]";
            Console.Write($"{cardDisplay,-5} ");
        }
        Console.WriteLine();
    }

    public static void DisplayTable(GameState gameState)
    {
        Console.WriteLine("\n====================================");
        Console.WriteLine("桌面狀態:");
        
        if (gameState.TopPlay == null || gameState.TopPlay.IsPass)
        {
            Console.WriteLine("  目前桌面是空的");
        }
        else
        {
            Console.WriteLine($"  最後出牌: {gameState.TopPlayer?.Name}");
            Console.WriteLine($"  牌型: {gameState.TopPlay}");
        }

        if (gameState.PassCount > 0)
        {
            Console.WriteLine($"  Pass計數: {gameState.PassCount}/3");
        }

        Console.WriteLine("====================================\n");
    }

    /// <summary>
    /// 顯示所有玩家狀態
    /// </summary>
    public static void DisplayAllPlayers(List<Player> players, int currentPlayerIndex)
    {
        Console.WriteLine("所有玩家:");
        for (int i = 0; i < players.Count; i++)
        {
            string marker = (i == currentPlayerIndex) ? "→ " : "  ";
            Console.WriteLine($"{marker}{players[i]}");
        }
        Console.WriteLine();
    }

    /// <summary>
    /// 顯示訊息
    /// </summary>
    public static void DisplayMessage(string message, bool isError = false)
    {
        if (isError)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"✗ {message}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✓ {message}");
            Console.ResetColor();
        }
    }

    /// <summary>
    /// 顯示勝利者
    /// </summary>
    public static void DisplayWinner(Player winner)
    {
        Console.Clear();
        Console.WriteLine("\n====================================");
        Console.WriteLine("           遊戲結束！");
        Console.WriteLine("====================================");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n🎉 恭喜 {winner.Name} 獲勝！🎉\n");
        Console.ResetColor();
        Console.WriteLine("====================================");
    }

    /// <summary>
    /// 顯示首回合提示
    /// </summary>
    public static void DisplayFirstRoundHint(Player player)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n>>> {player.Name} 持有梅花3，第一手出牌必須包含梅花3！");
        Console.ResetColor();
    }

    /// <summary>
    /// 顯示三家Pass提示
    /// </summary>
    public static void DisplayThreePassMessage(Player player)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n>>> 三家都Pass了！{player.Name} 可以出任意牌型");
        Console.ResetColor();
    }

    /// <summary>
    /// 顯示輸入提示
    /// </summary>
    public static void DisplayInputHint()
    {
        Console.WriteLine("\n請選擇要出的牌:");
        Console.WriteLine("  輸入牌的編號 (例如: 1 2 3)");
        Console.WriteLine("  輸入 'pass' 跳過");
        Console.WriteLine("  輸入 'quit' 退出遊戲");
        Console.Write("\n你的選擇: ");
    }

    /// <summary>
    /// 等待使用者按鍵
    /// </summary>
    public static void WaitForKey(string message = "\n按任意鍵繼續...")
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }

    public static void DisPlayNewRound()
    {
        Console.WriteLine("\n新的回合開始了");
    }

    public static void DisplayCurrentPlayer(Player currentPlayer)
    {
        Console.WriteLine($"\n輪到{currentPlayer}了");
    }
}

