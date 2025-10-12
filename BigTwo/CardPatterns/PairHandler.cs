using BigTwo.Models;

namespace BigTwo.CardPatterns;

/// <summary>
/// 對子處理器
/// 兩張相同點數的牌
/// </summary>
public class PairHandler : CardPatternHandler
{
    public override bool CanHandle(List<Card> cards)
    {
        return cards.Count == 2;
    }

    public override bool Validate(List<Card> cards)
    {
        return cards[0].Rank == cards[1].Rank;
    }

    public override CardPatternValue GetPattern(List<Card> cards)
    {
        if (!Validate(cards))
        {
            return new CardPatternValue(CardPatternType.Invalid, 0, cards);
        }

        // 取最大的牌作為代表
        Card highCard = cards.Max()!;
        int size = CalculateSize(highCard);

        return new CardPatternValue(
            CardPatternType.Pair,
            size,
            cards.OrderBy(c => c).ToList()
        );
    }
}

