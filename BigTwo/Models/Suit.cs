namespace BigTwo.Models;

public class Suit(string symbol, int value, string name) : IComparable<Suit>, IEquatable<Suit>
{
    private string Symbol { get; } = symbol;
    public int Value { get; } = value;
    public string Name { get; } = name;
    public static readonly Suit Club = new("♣", 0, "C");
    private static readonly Suit Diamond = new("♦", 1, "D");
    private static readonly Suit Heart = new("♥", 2, "H");
    private static readonly Suit Spade = new("♠", 3, "S");


    // 所有花色列表 (用於遍歷)
    public static readonly List<Suit> All = [Club, Diamond, Heart, Spade];

    public int CompareTo(Suit? other)
    {
        if (other == null) return 1;
        return Value.CompareTo(other.Value);
    }

    public bool Equals(Suit? other)
    {
        if (other == null) return false;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Suit);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(Suit? left, Suit? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(Suit? left, Suit? right)
    {
        return !(left == right);
    }

    public override string ToString()
    {
        return Symbol;
    }
}
