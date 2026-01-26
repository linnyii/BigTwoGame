using BigTwo.CardPatterns;
using BigTwo.GameLogic;
using BigTwo.Models;

namespace BigTwo.AI;

public class AiPlayStrategyManager
{
    private readonly Dictionary<CardPatternType, IAIPlayStrategy> _strategies;
    private readonly OpeningPlayStrategy _openingStrategy;
    
    public AiPlayStrategyManager(List<IAIPlayStrategy> strategies)
    {
        _strategies = strategies.ToDictionary(s => s.SupportedPatternType);
        _openingStrategy = new OpeningPlayStrategy();
    }
    
    public List<Card> SelectCards(Player aiPlayer, GameContext context)
    {
        if (context.IsTableEmpty)
        {
            return _openingStrategy.SelectOpeningPlay(aiPlayer, context);
        }
        
        var requiredType = context.RequiredPatternType!.Value;
        
        if (!_strategies.TryGetValue(requiredType, out var strategy))
        {
            return context.CanPass ? [] : _openingStrategy.SelectOpeningPlay(aiPlayer, context);
        }
        
        var validPlays = strategy.FindValidPlays(aiPlayer, context);
        
        if (validPlays.Count == 0)
        {
            return context.CanPass ? [] : _openingStrategy.SelectOpeningPlay(aiPlayer, context);
        }
        
        return strategy.SelectOptimalPlay(validPlays, aiPlayer, context);
    }
}

