using BigTwo.Models;

namespace BigTwo.CardPatterns;

/// <summary>
/// 單張牌處理器
/// </summary>
public class SingleHandler : CardPatternHandler
{
    protected override bool CanHandle(List<Card> cards)
    {
        return cards.Count == 1;
    }

    protected override bool Validate(List<Card> cards)
    {
        return cards.Count == 1;
    }

    protected override CardPatternValue GetPattern(List<Card> cards)
    {
        var card = cards[0];
        var size = card.CalculateSize();

        return new CardPatternValue(
            CardPatternType.Single,
            size,
            [card]
        );
    }
}

