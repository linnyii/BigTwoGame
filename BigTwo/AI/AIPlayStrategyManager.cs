using BigTwo.CardPatterns;
using BigTwo.GameLogic;
using BigTwo.Models;

namespace BigTwo.AI;

/// <summary>
/// AI 策略管理器：根據遊戲狀態選擇合適的策略
/// </summary>
public class AIPlayStrategyManager
{
    private readonly Dictionary<CardPatternType, IAIPlayStrategy> _strategies;
    private readonly OpeningPlayStrategy _openingStrategy;
    
    public AIPlayStrategyManager(List<IAIPlayStrategy> strategies)
    {
        _strategies = strategies.ToDictionary(s => s.SupportedPatternType);
        _openingStrategy = new OpeningPlayStrategy(strategies);
    }
    
    public List<Card> SelectCards(Player aiPlayer, GameContext context)
    {
        // 1. 如果桌面是空的，使用開局策略
        if (context.IsTableEmpty)
        {
            return _openingStrategy.SelectOpeningPlay(aiPlayer, context);
        }
        
        // 2. 根據 TopPlay 的類型選擇對應的策略
        var requiredType = context.RequiredPatternType!.Value;
        
        if (!_strategies.TryGetValue(requiredType, out var strategy))
        {
            // 如果沒有對應的策略，嘗試 Pass（如果可以的話）
            return context.CanPass ? new List<Card>() : _openingStrategy.SelectOpeningPlay(aiPlayer, context);
        }
        
        // 3. 找出所有有效的牌型
        var validPlays = strategy.FindValidPlays(aiPlayer, context);
        
        // 4. 如果沒有可以打敗的牌，嘗試 Pass
        if (validPlays.Count == 0)
        {
            return context.CanPass ? new List<Card>() : _openingStrategy.SelectOpeningPlay(aiPlayer, context);
        }
        
        // 5. 選擇最優的出牌
        return strategy.SelectOptimalPlay(validPlays, aiPlayer, context);
    }
}

