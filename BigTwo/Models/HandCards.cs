namespace BigTwo.Models;

public class HandCards
{
    private readonly List<Card> _cards = [];
    
    public IReadOnlyList<Card> Cards => _cards.AsReadOnly();
    public int Count => _cards.Count;

    public void AddCard(Card card)
    {
        _cards.Add(card);
    }

    public void RemoveCards(List<Card> cards)
    {
        if (cards.Count == 0) return;
        
        var cardSet = new HashSet<Card>(cards);
        _cards.RemoveAll(cardSet.Contains);
    }

    public void Sort()
    {
        _cards.Sort();
    }

    public bool ContainsClubThree()
    {
        return _cards.Any(c => c.Suit == Suit.Club && c.Rank == Rank.Three);
    }
}

