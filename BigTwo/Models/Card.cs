namespace BigTwo.Models;

/// <summary>
/// 撲克牌類別
/// </summary>
public class Card : IComparable<Card>, IEquatable<Card>
{
    public Suit Suit { get; }
    public Rank Rank { get; }
    public string SuitSymbol => Suit.Symbol;
    public string RankSymbol => Rank.Symbol;

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    /// <summary>
    /// 比較兩張牌的大小
    /// 先比點數，點數相同再比花色
    /// </summary>
    public int CompareTo(Card? other)
    {
        if (other == null) return 1;

        // 先比較點數
        int rankComparison = Rank.CompareTo(other.Rank);
        if (rankComparison != 0)
            return rankComparison;

        // 點數相同，比較花色
        return Suit.CompareTo(other.Suit);
    }

    /// <summary>
    /// 判斷兩張牌是否相等
    /// </summary>
    public bool Equals(Card? other)
    {
        if (other == null) return false;
        return Suit == other.Suit && Rank == other.Rank;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Card);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Suit, Rank);
    }

    /// <summary>
    /// 顯示牌面
    /// </summary>
    public override string ToString()
    {
        return $"{SuitSymbol}{RankSymbol}";
    }

    /// <summary>
    /// 簡短表示 (用於輸入解析)
    /// 例如: C3, D4, HA, S2
    /// </summary>
    public string ToShortString()
    {
        char suitChar = Suit.Name switch
        {
            "Club" => 'C',
            "Diamond" => 'D',
            "Heart" => 'H',
            "Spade" => 'S',
            _ => '?'
        };

        return $"{suitChar}{RankSymbol}";
    }

    public int CalculateSize()
    {
        return Rank.SizeValue * 4 + Suit.Value;
    }
}
