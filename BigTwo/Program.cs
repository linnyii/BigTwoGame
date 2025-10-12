using BigTwo.CardPatterns;
using BigTwo.GameLogic;
using BigTwo.Models;
using BigTwo.UI;

namespace BigTwo;

public static class Program
{
    private const int TotalPlayers = 4;

    private static void Main()
    {
        ConsoleUI.DisplayTitle();

        var cardPatternHandler = CreateHandlerChain();
        
        var players = CreatePlayers(cardPatternHandler);
        
        var game = new BigTwoGame(players);
        
        ConsoleUI.WaitForKey("準備開始遊戲，按任意鍵開始...");

        game.StartGame();
    }

    private static CardPatternHandler CreateHandlerChain()
    {
        var handler = new PassHandler();
        handler.SetNext(new SingleHandler())
            .SetNext(new PairHandler())
            .SetNext(new StraightHandler())
            .SetNext(new FullHouseHandler())
            .SetNext(new InvalidHandler());
        
        return handler;
    }

    private static List<Player> CreatePlayers(CardPatternHandler cardPatternHandler)
    {
        var playerNames = InputHandler.GetPlayerNames();
        var players = new List<Player>();

        for (var playerIndex = 0; playerIndex < TotalPlayers; playerIndex++)
        {
            var playerName = IsValidName(playerNames, playerIndex)
                ? playerNames[playerIndex].Trim()
                : $"玩家 {playerIndex + 1}";
            
            players.Add(new Player(playerName, cardPatternHandler));
        }

        return players;
    }

    private static bool IsValidName(List<string> playerNames, int playerIndex)
    {
        return playerIndex < playerNames.Count && !string.IsNullOrWhiteSpace(playerNames[playerIndex]);
    }
}
