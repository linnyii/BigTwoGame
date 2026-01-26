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

        var cardPatternHandler = CreateHandlerChain();

        AIPlayer.SetStrategyManager(new AiPlayStrategyManager([
            new SinglePlayStrategy(),
            new PairPlayStrategy(),
            new StraightPlayStrategy(),
            new FullHousePlayStrategy()
        ]));
        
        var players = CreatePlayers(cardPatternHandler);
        
        var game = new BigTwoGame(players);
        
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

        Console.WriteLine("\n請選擇每個玩家的類型：\n");

        for (var playerIndex = 0; playerIndex < TotalPlayers; playerIndex++)
        {
            var playerName = IsValidName(playerNames, playerIndex)
                ? playerNames[playerIndex].Trim()
                : $"玩家 {playerIndex + 1}";

            if (IsAI(playerIndex, playerName))
            {
                players.Add(new AIPlayer(playerName, cardPatternHandler));
            }
            else
            {
                players.Add(new HumanPlayer(playerName, cardPatternHandler));
            }
        }

        Console.WriteLine();
        return players;
    }

    private static bool IsAI(int playerIndex, string playerName)
    {
        return InputHandler.AskPlayerType(playerIndex, playerName);
    }

    private static bool IsValidName(List<string> playerNames, int playerIndex)
    {
        return playerIndex < playerNames.Count && !string.IsNullOrWhiteSpace(playerNames[playerIndex]);
    }
}
