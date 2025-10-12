using BigTwo.CardPatterns;

namespace BigTwo.Models;

public class Player(string name, CardPatternHandler cardPatternHandler)
{
    public string Name { get; } = name;
    public Hand Hand { get; } = new();
    public bool IsWinner { get; set; }
    private CardPatternHandler CardPatternHandler { get; } = cardPatternHandler;

    public void ReceiveCard(Card card)
    {
        Hand.AddCard(card);
    }

    public void PlayCards(List<Card> cards)
    {
        Hand.RemoveCards(cards);
    }

    public bool HasNoCards()
    {
        return Hand.Count == 0;
    }

    public override string ToString()
    {
        return $"{Name} ({Hand.Count} 張牌)";
    }

    public void SortHandCards()
    {
        Hand.Sort();
    }

    public bool HasClubThree()
    {
        return Hand.ContainsClubThree();
    }

    public List<Card> GetHandCards()
    {
        return Hand.Cards.ToList();
    }

    public CardPatternValue? ValidatePlay(List<Card> cards)
    {
        return CardPatternHandler.Handle(cards);
    }
}

