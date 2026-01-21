using BigTwo.CardPatterns;
using BigTwo.GameLogic;
using BigTwo.Models;

namespace BigTwo.AI;

public class SinglePlayStrategy : IAIPlayStrategy
{
    public CardPatternType SupportedPatternType => CardPatternType.Single;
    
    public List<List<Card>> FindValidPlays(Player player, GameContext context)
    {
        var handCards = player.GetHandCards();
        var validPlays = new List<List<Card>>();
        var topPlay = context.TopPlay!;
        
        // 生成所有單張組合
        foreach (var card in handCards)
        {
            var play = new List<Card> { card };
            var pattern = player.ValidatePlay(play);
            
            if (pattern is { IsInvalid: false, Type: CardPatternType.Single } && 
                pattern.IsBiggerThan(topPlay))
            {
                validPlays.Add(play);
            }
        }
        
        return validPlays;
    }
    
    public List<Card> SelectOptimalPlay(List<List<Card>> validPlays, Player player, GameContext context)
    {
        // 策略：選擇最小可打敗的牌
        return validPlays
            .OrderBy(play => play[0].CalculateSize())
            .First();
    }
}

