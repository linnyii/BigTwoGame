using BigTwo.CardPatterns;
using BigTwo.GameLogic;
using BigTwo.Models;

namespace BigTwo.AI;

/// <summary>
/// AI 出牌策略介面
/// 每種牌型都有對應的策略實作
/// </summary>
public interface IAIPlayStrategy
{
    /// <summary>
    /// 該策略支援的牌型
    /// </summary>
    CardPatternType SupportedPatternType { get; }
    
    /// <summary>
    /// 找出所有可以打敗 TopPlay 的牌型組合
    /// </summary>
    /// <param name="player">AI 玩家</param>
    /// <param name="context">遊戲狀態</param>
    /// <returns>所有有效的牌型組合列表</returns>
    List<List<Card>> FindValidPlays(Player player, GameContext context);
    
    /// <summary>
    /// 從有效牌型中選擇最優的出牌（策略：選擇最小可打敗的牌）
    /// </summary>
    /// <param name="validPlays">有效的牌型組合列表</param>
    /// <param name="player">AI 玩家</param>
    /// <param name="context">遊戲狀態</param>
    /// <returns>選中的牌</returns>
    List<Card> SelectOptimalPlay(List<List<Card>> validPlays, Player player, GameContext context);
}

