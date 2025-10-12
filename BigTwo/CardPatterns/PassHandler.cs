using BigTwo.Models;

namespace BigTwo.CardPatterns;

/// Pass 處理器
/// 處理玩家選擇不出牌的情況
public class PassHandler : CardPatternHandler
{
    protected override bool CanHandle(List<Card> cards)
    {
        return cards.Count == 0;
    }

    protected override bool Validate(List<Card> cards)
    {
        return cards.Count == 0;
    }

    protected override CardPatternValue GetPattern(List<Card> cards)
    {
        return new CardPatternValue(
            CardPatternType.Pass,
            -1,
            []
        );
    }
}

