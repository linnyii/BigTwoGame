using BigTwo.CardPatterns;

namespace BigTwo.Models;

public class Player(string name, CardPatternHandler cardPatternHandler)
{
    public string Name { get; } = name;
    public HandCards HandCards { get; } = new();
    private CardPatternHandler CardPatternHandler { get; } = cardPatternHandler;

    public void ReceiveCard(Card card)
    {
        HandCards.AddCard(card);
    }

    public void PlayCards(List<Card> cards)
    {
        HandCards.RemoveCards(cards);
    }

    public bool HasNoCards()
    {
        return HandCards.Count == 0;
    }
    
    public void SortHandCards()
    {
        HandCards.Sort();
    }

    public bool HasClubThree()
    {
        return HandCards.ContainsClubThree();
    }

    public List<Card> GetHandCards()
    {
        return HandCards.Cards.ToList();
    }

    public CardPatternValue ValidatePlay(List<Card> cards)
    {
        return CardPatternHandler.Handle(cards);
    }
}

