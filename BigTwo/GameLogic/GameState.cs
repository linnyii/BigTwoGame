using BigTwo.Models;
using BigTwo.CardPatterns;

namespace BigTwo.GameLogic;

public class GameState
{
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
        return PassCount >= 3;
    }

    public void SetCurrentPlayerIndex(int currentPlayer)
    {
        CurrentPlayerIndex = currentPlayer;
    }

    public void SetIsFirstRound(bool firstRound)
    {
        IsFirstRound = firstRound;
    }
}