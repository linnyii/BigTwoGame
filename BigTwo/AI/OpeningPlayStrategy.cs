using BigTwo.CardPatterns;
using BigTwo.GameLogic;
using BigTwo.Models;

namespace BigTwo.AI;

/// <summary>
/// 開局策略：當桌面為空時選擇牌型
/// </summary>
public class OpeningPlayStrategy
{
    private readonly List<IAIPlayStrategy> _strategies;
    
    public OpeningPlayStrategy(List<IAIPlayStrategy> strategies)
    {
        _strategies = strategies;
    }
    
    public List<Card> SelectOpeningPlay(Player player, GameContext context)
    {
        var handCards = player.GetHandCards();
        
        // 如果是第一輪，必須包含梅花3
        if (context.IsFirstRound && context.ClubThree != null)
        {
            return FindSmallestPlayWithClubThree(player, context.ClubThree);
        }
        
        // 否則選擇最小的單張
        return new List<Card> { handCards.OrderBy(c => c.CalculateSize()).First() };
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

