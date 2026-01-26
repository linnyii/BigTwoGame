using BigTwo.GameLogic;
using BigTwo.Models;

namespace BigTwo.AI;

public class OpeningPlayStrategy
{
    public List<Card> SelectOpeningPlay(Player player, GameContext context)
    {
        var handCards = player.GetHandCards();
        
        if (context is { IsFirstRound: true, ClubThree: not null })
        {
            return FindSmallestPlayWithClubThree(player, context.ClubThree);
        }
        
        return [handCards.OrderBy(c => c.CalculateSize()).First()];
    }
    
    private List<Card> FindSmallestPlayWithClubThree(Player player, Card clubThree)
    {
        var handCards = player.GetHandCards();
        
        if (handCards.Contains(clubThree))
        {
            return [clubThree];
        }
        
        //Can implement other CardType with ClubThree
        throw new InvalidOperationException(
            $"玩家 {player.Name} 在第一輪被要求出包含梅花3的牌，但手上沒有梅花3。這表示遊戲邏輯出現錯誤。");
    }
}

