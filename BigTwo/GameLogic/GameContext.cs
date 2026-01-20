using BigTwo.CardPatterns;
using BigTwo.Models;

namespace BigTwo.GameLogic;

public class GameContext
{
    public CardPatternValue? TopPlay { get; init; }
    public bool IsFirstRound { get; init; }
    public Card? ClubThree { get; init; }
    
    public bool CanPass => TopPlay != null;
    public bool IsTableEmpty => TopPlay == null;
    public CardPatternType? RequiredPatternType => TopPlay?.Type;
}

