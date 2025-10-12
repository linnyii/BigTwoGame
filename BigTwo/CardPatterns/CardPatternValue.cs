using BigTwo.Models;

namespace BigTwo.CardPatterns;

public class CardPatternValue(CardPatternType type, int size, List<Card> playCards) : IComparable<CardPatternValue>
{
    public CardPatternType Type { get; } = type;

    private int Size { get; } = size;

    public List<Card> PlayCards { get; } = playCards;

    public int CompareTo(CardPatternValue? other)
    {
        if (other == null) return 1;

        // 不同牌型無法比較
        if (Type != other.Type)
            return 0;

        // 比較Size
        return Size.CompareTo(other.Size);
    }

    public bool IsBiggerThan(CardPatternValue other)
    {
        // 必須是相同牌型才能比較
        if (Type != other.Type)
            return false;

        return Size > other.Size;
    }

    public bool IsPass => Type == CardPatternType.Pass;

    public bool IsInvalid => Type == CardPatternType.Invalid;

    public string GetTypeMandarinName()
    {
        var typeName = Type switch
        {
            CardPatternType.Single => "單張",
            CardPatternType.Pair => "對子",
            CardPatternType.Straight => "順子",
            CardPatternType.FullHouse => "葫蘆",
            _ => Type.ToString()
        };
        return typeName;
    }
}

