using BigTwo.CardPatterns;
using BigTwo.GameLogic;
using BigTwo.Models;

namespace BigTwo.AI;

/// <summary>
/// 開局策略：當桌面為空時選擇牌型
/// </summary>
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
        
        // 簡單策略：如果手上有梅花3，先出單張梅花3
        if (handCards.Contains(clubThree))
        {
            return new List<Card> { clubThree };
        }
        
        // 否則嘗試找出包含梅花3的最小牌型
        // 這裡可以擴展為檢查對子、順子等
        return new List<Card> { clubThree };
    }
}

