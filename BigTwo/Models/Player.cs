using BigTwo.CardPatterns;
using BigTwo.GameLogic;

namespace BigTwo.Models;

public abstract class Player(string name, CardPatternHandler cardPatternHandler)
{
    public string Name { get; } = name;
    public HandCards HandCards { get; } = new();
    protected CardPatternHandler CardPatternHandler { get; } = cardPatternHandler;

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

    /// <summary>
    /// 抽象方法：讓 Human 和 AI 各自實作如何選擇牌
    /// </summary>
    public abstract List<Card> GetSelectedCards(GameContext? context = null);
}

