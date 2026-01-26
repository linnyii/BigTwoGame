using BigTwo.AI;
using BigTwo.CardPatterns;
using BigTwo.GameLogic;

namespace BigTwo.Models;

public class AIPlayer(string name, CardPatternHandler cardPatternHandler) 
    : Player(name, cardPatternHandler)
{
    private static AiPlayStrategyManager? _strategyManager;
    
    public static void SetStrategyManager(AiPlayStrategyManager strategyManager)
    {
        _strategyManager = strategyManager;
    }
    
    public override List<Card> GetSelectedCards(GameContext? context)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context), "AI 需要遊戲狀態資訊");
        }
        
        if (_strategyManager is null)
        {
            throw new InvalidOperationException("AI 策略管理器尚未初始化");
        }
        
        return _strategyManager.SelectCards(this, context);
    }
}

