namespace BigTwo.Models;

public class HandCards
{
    public readonly List<Card> Cards = [];
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
}

