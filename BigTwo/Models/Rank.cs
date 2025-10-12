namespace BigTwo.Models;

public class Rank : IComparable<Rank>, IEquatable<Rank>
{
    public string Symbol { get; }
    public int SizeValue { get; }
    public int OrderValue { get; }
    public string Name { get; }

    private Rank(string symbol, int sizeValue, int orderValue, string name)
    {
        Symbol = symbol;
        SizeValue = sizeValue;
        OrderValue = orderValue;
        Name = name;
    }

    // 靜態實例
    public static readonly Rank Three = new("3", 0,3, "Three");
    public static readonly Rank Four = new("4", 1,4, "Four");
    public static readonly Rank Five = new("5", 2,5,  "Five");
    public static readonly Rank Six = new("6", 3,6, "Six");
    public static readonly Rank Seven = new("7", 4,7, "Seven");
    public static readonly Rank Eight = new("8", 5,8, "Eight");
    public static readonly Rank Nine = new("9", 6,9, "Nine");
    public static readonly Rank Ten = new("10", 7,10, "Ten");
    public static readonly Rank Jack = new("J", 8,11, "Jack");
    public static readonly Rank Queen = new("Q", 9,12, "Queen");
    public static readonly Rank King = new("K", 10,13, "King");
    public static readonly Rank Ace = new("A", 11,1 ,"Ace");
    public static readonly Rank Two = new("2", 12,2 ,"Two");

    // 所有點數列表 (用於遍歷)
    public static readonly List<Rank> All = new()
    {
        Three, Four, Five, Six, Seven, Eight, Nine, Ten,
        Jack, Queen, King, Ace, Two
    };

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
