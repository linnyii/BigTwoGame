namespace BigTwo.Models;

public class Hand
{
    public readonly List<Card> Cards = [];


    /// <summary>
    /// 手牌數量
    /// </summary>
    public int Count => Cards.Count;

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }

    public void RemoveCards(List<Card> cards)
    {
        foreach (var card in cards)
        {
            Cards.Remove(card);
        }

    }

    public void Sort()
    {
        Cards.Sort();
    }

    public bool ContainsClubThree()
    {
        return Cards.Any(c => c.Suit == Suit.Club && c.Rank == Rank.Three);
    }

    /// <summary>
    /// 取得梅花3
    /// </summary>
    public Card? GetClubThree()
    {
        return Cards.FirstOrDefault(c => c.Suit == Suit.Club && c.Rank == Rank.Three);
    }

    /// <summary>
    /// 檢查手牌中是否包含指定的牌
    /// </summary>
    public bool ContainsCards(List<Card> cards)
    {
        return cards.All(card => Cards.Contains(card));
    }

    /// <summary>
    /// 顯示手牌
    /// </summary>
    public override string ToString()
    {
        return string.Join(" ", Cards.Select(c => c.ToString()));
    }
}

