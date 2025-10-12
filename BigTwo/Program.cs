using BigTwo.GameLogic;
using BigTwo.UI;

namespace BigTwo;

public static class Program
{
    private static void Main()
    {
        ConsoleUI.DisplayTitle();

        var game = new BigTwoGame(InputHandler.GetPlayerNames());
        
        ConsoleUI.WaitForKey("準備開始遊戲，按任意鍵開始...");

        game.StartGame();
    }
}
