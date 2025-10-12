using BigTwo.Models;

namespace BigTwo.CardPatterns;

/// <summary>
/// 牌型值類別
/// 封裝牌型的類型、強度和內容
/// </summary>
public class CardPatternValue : IComparable<CardPatternValue>
{
    /// <summary>
    /// 牌型類型
    /// </summary>
    public CardPatternType Type { get; }

    /// <summary>
    /// 牌型強度值 (用於比較大小)
    /// </summary>
    public int Size { get; }

    /// <summary>
    /// 牌的內容
    /// </summary>
    public List<Card> PlayCards { get; }

    public CardPatternValue(CardPatternType type, int size, List<Card> playCards)
    {
        Type = type;
        Size = size;
        PlayCards = playCards;
    }

    /// <summary>
    /// 比較兩個牌型的大小
    /// </summary>
    public int CompareTo(CardPatternValue? other)
    {
        if (other == null) return 1;

        // 不同牌型無法比較
        if (Type != other.Type)
            return 0;

        // 比較Size
        return Size.CompareTo(other.Size);
    }

    /// <summary>
    /// 判斷是否比另一個牌型強
    /// </summary>
    public bool IsBiggerThan(CardPatternValue other)
    {
        // 必須是相同牌型才能比較
        if (Type != other.Type)
            return false;

        return Size > other.Size;
    }

    /// <summary>
    /// 是否為Pass
    /// </summary>
    public bool IsPass => Type == CardPatternType.Pass;

    /// <summary>
    /// 是否為無效牌型
    /// </summary>
    public bool IsInvalid => Type == CardPatternType.Invalid;

    public override string ToString()
    {
        string typeName = Type switch
        {
            CardPatternType.Single => "單張",
            CardPatternType.Pair => "對子",
            CardPatternType.Straight => "順子",
            CardPatternType.FullHouse => "葫蘆",
            CardPatternType.Pass => "Pass",
            CardPatternType.Invalid => "無效",
            _ => "未知"
        };

        if (Type == CardPatternType.Pass)
            return typeName;

        string cards = string.Join(" ", PlayCards.Select(c => c.ToString()));
        return $"{typeName}: {cards}";
    }
}

