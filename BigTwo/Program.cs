using BigTwo.AI;
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
        
        var aiStrategies = new List<IAIPlayStrategy>
        {
            new SinglePlayStrategy(),
            new PairPlayStrategy(),
            new StraightPlayStrategy(),
            new FullHousePlayStrategy()
        };
        var strategyManager = new AIPlayStrategyManager(aiStrategies);
        AIPlayer.SetStrategyManager(strategyManager);
        
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
            .SetNext(new FullHouseHandler());
        
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
            
            // 目前所有玩家都是 HumanPlayer，未來可以在這裡決定哪些是 AI
            players.Add(new HumanPlayer(playerName, cardPatternHandler));
        }

        return players;
    }

    private static bool IsValidName(List<string> playerNames, int playerIndex)
    {
        return playerIndex < playerNames.Count && !string.IsNullOrWhiteSpace(playerNames[playerIndex]);
    }
}
