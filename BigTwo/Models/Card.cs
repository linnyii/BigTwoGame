namespace BigTwo.Models;

public class Card(Suit suit, Rank rank) : IComparable<Card>, IEquatable<Card>
{
    public Suit Suit { get; } = suit;
    public Rank Rank { get; } = rank;
    public string RankSymbol => Rank.Symbol;

    public int CompareTo(Card? other)
    {
        if (other == null) return 1;

        var rankComparison = Rank.CompareTo(other.Rank);
        return rankComparison != 0 ? rankComparison : Suit.CompareTo(other.Suit);
    }

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

    public int CalculateSize()
    {
        return Rank.SizeValue * 4 + Suit.Value;
    }

    public override string ToString()
    {
        return $"{Suit.Name}{Rank.Symbol}";
    }
}
