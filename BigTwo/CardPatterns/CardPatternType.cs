namespace BigTwo.CardPatterns;

/// <summary>
/// 牌型類型枚舉
/// </summary>
public enum CardPatternType
{
    Invalid,      // 無效牌型
    Pass,         // Pass (不出牌)
    Single,       // 單張
    Pair,         // 對子 (2張)
    Straight,     // 順子 (5張)
    FullHouse     // 葫蘆 (3+2)
}

