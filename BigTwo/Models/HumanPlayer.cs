using BigTwo.CardPatterns;
using BigTwo.GameLogic;
using BigTwo.UI;

namespace BigTwo.Models;

public class HumanPlayer(string name, CardPatternHandler cardPatternHandler) 
    : Player(name, cardPatternHandler)
{
    public override List<Card> GetSelectedCards(GameContext? context = null)
    {
        // 人類玩家使用原本的輸入方式
        return InputHandler.GetHumanInput(this);
    }
}

