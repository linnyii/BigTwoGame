using BigTwo.Models;

namespace BigTwo.CardPatterns;

/// <summary>
/// 牌型處理器抽象基類
/// 使用責任鏈模式處理不同牌型
/// </summary>
public abstract class CardPatternHandler
{
    /// <summary>
    /// 責任鏈中的下一個處理器
    /// </summary>
    protected CardPatternHandler? Next { get; private set; }

    /// <summary>
    /// 設定責任鏈中的下一個處理器
    /// </summary>
    public CardPatternHandler SetNext(CardPatternHandler handler)
    {
        Next = handler;
        return handler;
    }

    public CardPatternValue? Handle(List<Card> cards)
    {
        if (CanHandle(cards) && Validate(cards))
        {
            //TODO: Need TO Modify
            return GetPattern(cards);
        }

        // 傳遞給下一個處理器
        return Next?.Handle(cards);
    }

    /// <summary>
    /// 判斷是否能處理這組牌
    /// 快速篩選，檢查牌的數量等基本條件
    /// </summary>
    public abstract bool CanHandle(List<Card> cards);

    /// <summary>
    /// 驗證這組牌是否為合法的牌型
    /// 詳細檢查牌型規則
    /// </summary>
    public abstract bool Validate(List<Card> cards);

    /// <summary>
    /// 建立並返回 CardPatternValue 物件
    /// </summary>
    public abstract CardPatternValue GetPattern(List<Card> cards);

    /// <summary>
    /// 計算牌型的強度值
    /// 用於比較牌型大小
    /// </summary>
    protected int CalculateSize(Card highCard)
    {
        // Size = Rank * 4 + Suit
        // 確保相同點數中，花色大的會有更大的Size
        return highCard.Rank.SizeValue * 4 + highCard.Suit.Value;
    }
}

