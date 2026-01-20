using BigTwo.CardPatterns;
using BigTwo.GameLogic;
using BigTwo.Models;

namespace BigTwo.AI;

public class FullHousePlayStrategy : IAIPlayStrategy
{
    public CardPatternType SupportedPatternType => CardPatternType.FullHouse;
    
    public List<List<Card>> FindValidPlays(Player player, GameContext context)
    {
        var handCards = player.GetHandCards();
        var validPlays = new List<List<Card>>();
        var topPlay = context.TopPlay!;
        
        // 生成所有葫蘆組合
        var fullHouses = GenerateFullHouses(handCards);
        
        foreach (var fullHouse in fullHouses)
        {
            var pattern = player.ValidatePlay(fullHouse);
            
            if (!pattern.IsInvalid && 
                pattern.Type == CardPatternType.FullHouse && 
                pattern.IsBiggerThan(topPlay))
            {
                validPlays.Add(fullHouse);
            }
        }
        
        return validPlays;
    }
    
    public List<Card> SelectOptimalPlay(List<List<Card>> validPlays, Player player, GameContext context)
    {
        // 策略：選擇最小可打敗的葫蘆（根據三張相同點數的最大牌）
        return validPlays
            .OrderBy(play => 
            {
                var threeOfAKind = play.GroupBy(c => c.Rank)
                    .First(g => g.Count() == 3)
                    .Max()!
                    .CalculateSize();
                return threeOfAKind;
            })
            .First();
    }
    
    private List<List<Card>> GenerateFullHouses(List<Card> cards)
    {
        var fullHouses = new List<List<Card>>();
        var rankGroups = cards.GroupBy(c => c.Rank).ToList();
        
        // 找出所有可能的 3+2 組合
        for (var i = 0; i < rankGroups.Count; i++)
        {
            var threeGroup = rankGroups[i];
            if (threeGroup.Count() < 3) continue;
            
            var threeCombinations = GetCombinations(threeGroup.ToList(), 3);
            
            for (var j = 0; j < rankGroups.Count; j++)
            {
                if (i == j) continue;
                
                var twoGroup = rankGroups[j];
                if (twoGroup.Count() < 2) continue;
                
                var twoCombinations = GetCombinations(twoGroup.ToList(), 2);
                
                foreach (var three in threeCombinations)
                {
                    foreach (var two in twoCombinations)
                    {
                        var fullHouse = new List<Card>(three);
                        fullHouse.AddRange(two);
                        fullHouses.Add(fullHouse);
                    }
                }
            }
        }
        
        return fullHouses;
    }
    
    private List<List<Card>> GetCombinations(List<Card> cards, int count)
    {
        var combinations = new List<List<Card>>();
        GetCombinationsRecursive(cards, new List<Card>(), 0, count, combinations);
        return combinations;
    }
    
    private void GetCombinationsRecursive(
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
            GetCombinationsRecursive(cards, current, i + 1, count, result);
            current.RemoveAt(current.Count - 1);
        }
    }
}

