using BigTwo.Models;

namespace BigTwo.CardPatterns;

//Can delete this pattern , can use default CardPatternValue return 
/// <summary>
/// 無效牌型處理器
/// 預設處理器，處理所有無效的牌型
/// </summary>
public class InvalidHandler : CardPatternHandler
{
    protected override bool CanHandle(List<Card> cards)
    {
        return true;
    }

    protected override bool Validate(List<Card> cards)
    {
        return false;
    }

    protected override CardPatternValue GetPattern(List<Card> cards)
    {
        return new CardPatternValue(
            CardPatternType.Invalid,
            0,
            cards
        );
    }
}

