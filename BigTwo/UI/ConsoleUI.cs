using BigTwo.CardPatterns;
using BigTwo.Models;

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

    public static void DisplayerHandCards(Player player)
    {
        Console.WriteLine($"{player.Name} 的手牌:");
        var cards = player.Hand.Cards.ToList();
        
        for (var i = 0; i < cards.Count; i++)
        {
            Console.Write($"{i + 1,2}    ");
        }
        Console.WriteLine();
        
        foreach (var card in cards)
        {
            var cardDisplay = $"{card.Suit.Name}[{card.RankSymbol}]";
            Console.Write($"{cardDisplay,-5} ");
        }
        Console.WriteLine();
    }

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

    public static void DisplayThreePassMessage(Player player)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n>>> 三家都Pass了！{player.Name} 可以出任意牌型");
        Console.ResetColor();
    }

    public static void WaitForKey(string message = "按任意鍵繼續...")
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }

    public static void DisPlayNewRound()
    {
        Console.WriteLine("新的回合開始了");
    }

    public static void DisplayCurrentPlayer(Player currentPlayer)
    {
        Console.WriteLine($"輪到{currentPlayer}了");
    }

    public static void DisPlayTopPlayerPlay(Player? gameStateTopPlayer, CardPatternValue? gameStateTopPlay)
    {
        if (gameStateTopPlayer == null || gameStateTopPlay == null)
        {
            Console.WriteLine("目前桌面是空的");
            return;
        }

        var typeName = gameStateTopPlay.GetTypeMandarinName();
        var cards = string.Join(" ", gameStateTopPlay.PlayCards);
        Console.WriteLine($"目前的 Top Player: {gameStateTopPlayer.Name}, Top Play: {typeName} {cards}");
    }
}

