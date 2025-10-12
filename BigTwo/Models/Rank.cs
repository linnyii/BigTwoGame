namespace BigTwo.Models;

public class Rank(string symbol, int sizeValue, int orderValue)
    : IComparable<Rank>, IEquatable<Rank>
{
    public string Symbol { get; } = symbol;
    public int SizeValue { get; } = sizeValue;
    public int OrderValue { get; } = orderValue;

    public static readonly Rank Three = new("3", 0,3);
    private static readonly Rank Four = new("4", 1,4);
    private static readonly Rank Five = new("5", 2,5);
    private static readonly Rank Six = new("6", 3,6);
    private static readonly Rank Seven = new("7", 4,7);
    private static readonly Rank Eight = new("8", 5,8);
    private static readonly Rank Nine = new("9", 6,9);
    private static readonly Rank Ten = new("10", 7,10);
    private static readonly Rank Jack = new("J", 8,11);
    private static readonly Rank Queen = new("Q", 9,12);
    private static readonly Rank King = new("K", 10,13);
    private static readonly Rank Ace = new("A", 11,1);
    private static readonly Rank Two = new("2", 12,2);

    public static readonly List<Rank> All =
    [
        Three, Four, Five, Six, Seven, Eight, Nine, Ten,
        Jack, Queen, King, Ace, Two
    ];

    public int CompareTo(Rank? other)
    {
        if (other == null) return 1;
        return SizeValue.CompareTo(other.SizeValue);
    }

    public bool Equals(Rank? other)
    {
        if (other == null) return false;
        return SizeValue == other.SizeValue;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Rank);
    }

    public override int GetHashCode()
    {
        return SizeValue.GetHashCode();
    }

    public static bool operator ==(Rank? left, Rank? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(Rank? left, Rank? right)
    {
        return !(left == right);
    }

    public override string ToString()
    {
        return Symbol;
    }
}
