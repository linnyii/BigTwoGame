using BigTwo.Models;

namespace BigTwo.CardPatterns;

public class CardPatternValue(CardPatternType type, int size, List<Card> playCards)
{
    public CardPatternType Type { get; } = type;

    private int Size { get; } = size;

    public List<Card> PlayCards { get; } = playCards;

    public bool IsBiggerThan(CardPatternValue other)
    {
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

