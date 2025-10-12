using BigTwo.Models;

namespace BigTwo.CardPatterns;

/// <summary>
/// 單張牌處理器
/// </summary>
public class SingleHandler : CardPatternHandler
{
    public override bool CanHandle(List<Card> cards)
    {
        return cards.Count == 1;
    }

    public override bool Validate(List<Card> cards)
    {
        return cards.Count == 1;
    }

    public override CardPatternValue GetPattern(List<Card> cards)
    {
        if (!Validate(cards))
        {
            return new CardPatternValue(CardPatternType.Invalid, 0, cards);
        }

        Card card = cards[0];
        int size = CalculateSize(card);

        return new CardPatternValue(
            CardPatternType.Single,
            size,
            new List<Card> { card }
        );
    }
}

