using BigTwo.Models;

namespace BigTwo.CardPatterns;

/// <summary>
/// 葫蘆處理器
/// 三張相同點數 + 兩張相同點數
/// </summary>
public class FullHouseHandler : CardPatternHandler
{
    public override bool CanHandle(List<Card> cards)
    {
        return cards.Count == 5;
    }

    public override bool Validate(List<Card> cards)
    {
        var rankGroups = cards.GroupBy(c => c.Rank)
                              .Select(g => g.Count())
                              .OrderByDescending(count => count)
                              .ToList();

        return rankGroups.Count == 2 && 
               rankGroups[0] == 3 && 
               rankGroups[1] == 2;
    }

    public override CardPatternValue GetPattern(List<Card> cards)
    {

        // 找出三條的牌
        var threeOfAKind = cards.GroupBy(c => c.Rank)
                                .First(g => g.Count() == 3)
                                .ToList();

        // 葫蘆的大小由三條決定，取三條中最大的牌
        var highCard = threeOfAKind.Max()!;
        var size = CalculateSize(highCard);

        return new CardPatternValue(
            CardPatternType.FullHouse,
            size,
            cards.OrderBy(c => c).ToList()
        );
    }
}

