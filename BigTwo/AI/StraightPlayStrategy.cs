using BigTwo.CardPatterns;
using BigTwo.GameLogic;
using BigTwo.Models;

namespace BigTwo.AI;

public class StraightPlayStrategy : IAIPlayStrategy
{
    public CardPatternType SupportedPatternType => CardPatternType.Straight;
    
    public List<List<Card>> FindValidPlays(Player player, GameContext context)
    {
        var handCards = player.GetHandCards();
        var validPlays = new List<List<Card>>();
        var topPlay = context.TopPlay!;
        
        // 生成所有順子組合
        var straights = GenerateStraights(handCards);
        
        foreach (var straight in straights)
        {
            var pattern = player.ValidatePlay(straight);
            
            if (!pattern.IsInvalid && 
                pattern.Type == CardPatternType.Straight && 
                pattern.IsBiggerThan(topPlay))
            {
                validPlays.Add(straight);
            }
        }
        
        return validPlays;
    }
    
    public List<Card> SelectOptimalPlay(List<List<Card>> validPlays, Player player, GameContext context)
    {
        // 策略：選擇最小可打敗的順子（根據最大牌的大小）
        return validPlays
            .OrderBy(play => play.MaxBy(c => c.Rank.SizeValue)!.CalculateSize())
            .First();
    }
    
    private List<List<Card>> GenerateStraights(List<Card> cards)
    {
        var straights = new List<List<Card>>();
        
        // 如果手牌少於 5 張，無法組成順子
        if (cards.Count < 5)
        {
            return straights;
        }
        
        // 使用回溯法生成所有可能的 5 張牌組合
        GenerateCombinations(cards, new List<Card>(), 0, 5, straights);
        
        // 過濾出有效的順子
        return straights.Where(IsValidStraight).ToList();
    }
    
    private void GenerateCombinations(
        List<Card> cards, 
        List<Card> current, 
        int startIndex, 
        int count, 
        List<List<Card>> result)
    {
        if (current.Count == count)
        {
            result.Add(new List<Card>(current));
            return;
        }
        
        for (var i = startIndex; i < cards.Count; i++)
        {
            current.Add(cards[i]);
            GenerateCombinations(cards, current, i + 1, count, result);
            current.RemoveAt(current.Count - 1);
        }
    }
    
    private bool IsValidStraight(List<Card> cards)
    {
        if (cards.Count != 5) return false;
        
        var sorted = cards.OrderBy(c => c.Rank.OrderValue).ToList();
        var orderValues = sorted.Select(c => c.Rank.OrderValue).ToList();
        
        // 檢查連續
        if (IsConsecutive(orderValues))
        {
            return true;
        }
        
        // 檢查循環順子
        var hasLowCards = orderValues.Any(v => v <= 4);
        var hasHighCards = orderValues.Any(v => v >= 10);
        
        if (hasLowCards && hasHighCards)
        {
            var adjusted = orderValues.Select(v => v <= 4 ? v + 13 : v).OrderBy(v => v).ToList();
            return IsConsecutive(adjusted);
        }
        
        return false;
    }
    
    private bool IsConsecutive(List<int> values)
    {
        for (var i = 0; i < values.Count - 1; i++)
        {
            if (values[i + 1] - values[i] != 1)
            {
                return false;
            }
        }
        return true;
    }
}

