using BigTwo.Models;

namespace BigTwo.CardPatterns;

/// <summary>
/// 順子處理器
/// 五張連續點數的牌
/// </summary>
public class StraightHandler : CardPatternHandler
{
    public override bool CanHandle(List<Card> cards)
    {
        return cards.Count == 5;
    }

    public override bool Validate(List<Card> cards)
    {
        var sorted = cards.OrderBy(c => c.Rank.OrderValue).ToList();
        var orderValues = sorted.Select(c => c.Rank.OrderValue).ToList();

        if (IsConsecutive(orderValues))
        {
            return true;
        }

        // 檢查循環情況（跨越 K->A 邊界）
        // 如果包含小數字(1-4)和大數字(10-13)，可能是循環順子
        // 循環順子最大為: K,A,2,3,4
        var hasLowCards = orderValues.Any(v => v <= 4);
        var hasHighCards = orderValues.Any(v => v >= 10);

        if (hasLowCards && hasHighCards)
        {
            // 嘗試將小於等於 4 的牌加上 13（K 後面繼續）
            // 例如：A(1)->14, 2(2)->15, 3(3)->16, 4(4)->17
            var adjusted = orderValues.Select(v => v <= 4 ? v + 13 : v).ToList();
            adjusted.Sort();

            if (IsConsecutive(adjusted))
            {
                return true;
            }
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

    public override CardPatternValue GetPattern(List<Card> cards)
    {
        if (!Validate(cards))
        {
            return new CardPatternValue(CardPatternType.Invalid, 0, cards);
        }

        // 順子的大小由最大的牌決定（使用 SizeValue 比較）
        Card highCard = cards.MaxBy(c => c.Rank.SizeValue)!;
        int size = CalculateSize(highCard);

        return new CardPatternValue(
            CardPatternType.Straight,
            size,
            cards.OrderBy(c => c.Rank.OrderValue).ToList()
        );
    }
}

