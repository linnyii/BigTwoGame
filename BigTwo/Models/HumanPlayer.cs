using BigTwo.CardPatterns;
using BigTwo.GameLogic;
using BigTwo.UI;

namespace BigTwo.Models;

public class HumanPlayer(string name, CardPatternHandler cardPatternHandler) 
    : Player(name, cardPatternHandler)
{
    public override List<Card> GetSelectedCards(GameContext? context = null)
    {
        return InputHandler.GetHumanInput(this);
    }
}

