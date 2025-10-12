using BigTwo.Models;

namespace BigTwo.CardPatterns;

public abstract class CardPatternHandler
{
    private CardPatternHandler? Next { get; set; }

    public CardPatternHandler SetNext(CardPatternHandler handler)
    {
        Next = handler;
        return handler;
    }

    public CardPatternValue Handle(List<Card> cards)
    {
        if (CanHandle(cards) && Validate(cards))
        {
            return GetPattern(cards);
        }

        if (Next != null)
        {
            return Next.Handle(cards);
        }

        return new CardPatternValue(
            CardPatternType.Invalid,
            0,
            cards
        );
    }

    protected abstract bool CanHandle(List<Card> cards);

    protected abstract bool Validate(List<Card> cards);

    protected abstract CardPatternValue GetPattern(List<Card> cards);
}

