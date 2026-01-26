using BigTwo.CardPatterns;
using BigTwo.GameLogic;
using BigTwo.Models;

namespace BigTwo.AI;

public interface IAIPlayStrategy
{
    CardPatternType SupportedPatternType { get; }
    
    List<List<Card>> FindValidPlays(Player player, GameContext context);
    
    List<Card> SelectOptimalPlay(List<List<Card>> validPlays, Player player, GameContext context);
}

