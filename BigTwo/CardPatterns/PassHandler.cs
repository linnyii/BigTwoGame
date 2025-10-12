using BigTwo.Models;

namespace BigTwo.CardPatterns;

/// <summary>
/// Pass 處理器
/// 處理玩家選擇不出牌的情況
/// </summary>
public class PassHandler : CardPatternHandler
{
    public override bool CanHandle(List<Card> cards)
    {
        return cards.Count == 0;
    }

    public override bool Validate(List<Card> cards)
    {
        return cards.Count == 0;
    }

    public override CardPatternValue GetPattern(List<Card> cards)
    {
        return new CardPatternValue(
            CardPatternType.Pass,
            -1,  // Pass沒有Size
            new List<Card>()
        );
    }
}

