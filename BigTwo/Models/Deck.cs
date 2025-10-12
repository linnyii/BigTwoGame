namespace BigTwo.Models;

public class Deck
{
    private readonly List<Card> _cards;
    private readonly Random _random;

    public Deck()
    {
        _cards = [];
        _random = new Random();
        Initialize();
    }

    public void Initialize()
    {
        _cards.Clear();

        foreach (var suit in Suit.All)
        {
            foreach (var rank in Rank.All)
            {
                _cards.Add(new Card(suit, rank));
            }
        }
    }

    public void Shuffle()
    {
        ShuffleAlgorithm();
    }

    private void ShuffleAlgorithm()
    {
        for (var i = _cards.Count - 1; i > 0; i--)
        {
            var j = _random.Next(i + 1);
            (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
        }
    }

    public Card DealingCard()
    {
        var card = _cards[0];
        _cards.RemoveAt(0);
        return card;
    }
}

