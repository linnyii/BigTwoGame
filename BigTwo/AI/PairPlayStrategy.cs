using BigTwo.CardPatterns;
using BigTwo.GameLogic;
using BigTwo.Models;

namespace BigTwo.AI;

public class PairPlayStrategy : IAIPlayStrategy
{
    public CardPatternType SupportedPatternType => CardPatternType.Pair;
    
    public List<List<Card>> FindValidPlays(Player player, GameContext context)
    {
        var handCards = player.GetHandCards();
        var validPlays = new List<List<Card>>();
        var topPlay = context.TopPlay!;
        
        // 生成所有對子組合
        var pairs = GeneratePairs(handCards);
        
        foreach (var pair in pairs)
        {
            var pattern = player.ValidatePlay(pair);
            
            if (!pattern.IsInvalid && 
                pattern.Type == CardPatternType.Pair && 
                pattern.IsBiggerThan(topPlay))
            {
                validPlays.Add(pair);
            }
        }
        
        return validPlays;
    }
    
    public List<Card> SelectOptimalPlay(List<List<Card>> validPlays, Player player, GameContext context)
    {
        // 策略：選擇最小可打敗的對子
        return validPlays
            .OrderBy(play => play.Max(c => c.CalculateSize()))
            .First();
    }
    
    private List<List<Card>> GeneratePairs(List<Card> cards)
    {
        var pairs = new List<List<Card>>();
        var rankGroups = cards.GroupBy(c => c.Rank).Where(g => g.Count() >= 2);
        
        foreach (var group in rankGroups)
        {
            var cardsOfRank = group.ToList();
            // 對於每種點數，生成所有可能的對子組合（考慮不同花色）
            for (var i = 0; i < cardsOfRank.Count; i++)
            {
                for (var j = i + 1; j < cardsOfRank.Count; j++)
                {
                    pairs.Add(new List<Card> { cardsOfRank[i], cardsOfRank[j] });
                }
            }
        }
        
        return pairs;
    }
}

