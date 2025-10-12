using BigTwo.Models;
using BigTwo.CardPatterns;

namespace BigTwo.GameLogic;

public class GameState
{
    public Player? TopPlayer { get; private set; }

    public CardPatternValue? TopPlay { get; private set; }

    public int CurrentPlayerIndex { get; set; }

    public int PassCount { get; private set; }

    public bool IsFirstRound { get; set; }

    public GameState()
    {
        Reset();
    }

    public void Reset()
    {
        TopPlayer = null;
        TopPlay = null;
        CurrentPlayerIndex = 0;
        PassCount = 0;
        IsFirstRound = true;
    }

    public void IncrementPassCount()
    {
        PassCount++;
    }

    public void ResetPassCount()
    {
        PassCount = 0;
    }

    public void ClearTable()
    {
        TopPlay = null;
        PassCount = 0;
    }

    public void UpdateTopPlay(Player player, CardPatternValue pattern)
    {
        TopPlayer = player;
        TopPlay = pattern;
        PassCount = 0; // 有人出牌，重置Pass計數
    }

    public bool IsThreePass()
    {
        return PassCount >= 3;
    }
}

