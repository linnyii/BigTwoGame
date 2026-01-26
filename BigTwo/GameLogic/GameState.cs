using BigTwo.Models;
using BigTwo.CardPatterns;

namespace BigTwo.GameLogic;

public class GameState
{
    private const int MaxPassCount = 3;
    
    public Player? TopPlayer { get; private set; }

    public CardPatternValue? TopPlay { get; private set; }

    public int CurrentPlayerIndex { get; private set; }

    private int PassCount { get; set; }

    public bool IsFirstRound { get; private set; }

    public GameState()
    {
        Reset();
    }

    public void Reset()
    {
        TopPlayer = null;
        TopPlay = null;
        SetCurrentPlayerIndex(0);
        ResetPassCount();
        IsFirstRound = true;
    }

    public void IncreasePassCount()
    {
        PassCount += 1;
    }

    public void ResetPassCount()
    {
        PassCount = 0;
    }

    public void ClearTable()
    {
        TopPlayer = null;
        TopPlay = null;
        ResetPassCount();
    }

    public void UpdateTopPlayerPlay(Player player, CardPatternValue pattern)
    {
        TopPlayer = player;
        TopPlay = pattern;
        ResetPassCount();
    }

    public bool HasThreePass()
    {
        return PassCount >= MaxPassCount;
    }

    public void SetCurrentPlayerIndex(int currentPlayer, int? maxPlayers = null)
    {
        if (currentPlayer < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(currentPlayer), 
                currentPlayer, 
                "玩家索引不能為負數");
        }
        
        if (maxPlayers.HasValue && currentPlayer >= maxPlayers.Value)
        {
            throw new ArgumentOutOfRangeException(
                nameof(currentPlayer), 
                currentPlayer, 
                $"玩家索引 {currentPlayer} 超出範圍 [0, {maxPlayers.Value - 1}]");
        }
        
        CurrentPlayerIndex = currentPlayer;
    }

    public void SetIsFirstRound(bool firstRound)
    {
        IsFirstRound = firstRound;
    }
}