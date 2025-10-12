using BigTwo.Models;

namespace BigTwo.CardPatterns;

/// 對子處理器
/// 兩張相同點數的牌
public class PairHandler : CardPatternHandler
{
    protected override bool CanHandle(List<Card> cards)
    {
        return cards.Count == 2;
    }

    protected override bool Validate(List<Card> cards)
    {
        return cards[0].Rank == cards[1].Rank;
    }

    protected override CardPatternValue GetPattern(List<Card> cards)
    {
        var highCard = cards.Max()!;
        var size = highCard.CalculateSize();

        return new CardPatternValue(
            CardPatternType.Pair,
            size,
            cards.OrderBy(c => c).ToList()
        );
    }
}

