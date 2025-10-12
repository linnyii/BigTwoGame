namespace BigTwo.Models;

public class Player(string name)
{
    public string Name { get; } = name;
    public Hand Hand { get; } = new();
    public bool IsWinner { get; set; }

    public void ReceiveCard(Card card)
    {
        Hand.AddCard(card);
    }

    public void PlayCards(List<Card> cards)
    {
        Hand.RemoveCards(cards);
    }

    /// <summary>
    /// 檢查是否已出完所有牌
    /// </summary>
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
}

