using BigTwo.Models;
using BigTwo.CardPatterns;
using BigTwo.UI;

namespace BigTwo.GameLogic;

public class BigTwoGame
{
    private readonly List<Player> _players;
    private readonly Deck _deck;
    private readonly Card _clubThree = new(new Suit("♣", 0, "C"), new Rank("3", 0,3));
    private const int MaxHandCardNumber = 13;
    private GameState GameState { get; }

    public BigTwoGame(List<Player> players)
    {
        _players = players;
        _deck = new Deck();
        GameState = new GameState();
        
        Initialize();
    }

    private void Initialize()
    {
        _deck.Initialize();
    }

    private void InitializeFirstRound()
    {
        GameState.Reset();

        foreach (var player in _players)
        {
            player.IsWinner = false;
        }

        _deck.Initialize();
        _deck.Shuffle();

        DealingCards();

        foreach (var player in _players)
        {
            player.SortHandCards();
        }
        
        
        GameState.SetCurrentPlayerIndex(FindClubThreePlayer());
        GameState.SetIsFirstRound(true);
        ConsoleUI.DisPlayNewRound();
    }

    private void DealingCards()
    {
        for (var cardIndex = 0; cardIndex < MaxHandCardNumber; cardIndex++)
        {
            foreach (var player in _players)
            {
                player.ReceiveCard(_deck.DealingCard());
            }
        }
    }

    private int FindClubThreePlayer()
    {
        for (var playerIndex = 0; playerIndex < _players.Count; playerIndex++)
        {
            if (_players[playerIndex].HasClubThree())
            {
                return playerIndex;
            }
        }
        return 0; 
    }

    private Player GetCurrentPlayer()
    {
        return _players[GameState.CurrentPlayerIndex];
    }

    private (bool validPlayCard, string message) HandlePlayerPlay(List<Card> cards)
    {
        var currentPlayer = GetCurrentPlayer();
        var pattern = currentPlayer.ValidatePlay(cards);

        if (pattern.IsInvalid)
        {
            return (false, "無效的牌型！");
        }

        if (pattern.IsPass)
        {
            if (GameState.TopPlay == null)
            {
                return (false, "桌面是空的，不能Pass！");
            }

            GameState.IncrementPassCount();
            return (true, $"{currentPlayer.Name} Pass");
        }

        if (GameState.IsFirstRound)
        {
            if (!pattern.PlayCards.Contains(_clubThree))
            {
                return (false, "第一手必須包含梅花3！");
            }
            
            GameState.SetIsFirstRound(false);
        }

        var (canPlay, compareMessage) = ComparingSize(pattern);
        if (!canPlay)
        {
            return (false, compareMessage);
        }

        currentPlayer.PlayCards(cards);

        GameState.UpdateTopPlay(currentPlayer, pattern);

        if (pattern.Type == CardPatternType.Pass)
        {
            return (true, $"玩家 {currentPlayer.Name} Pass");
        }
        
        var cardsDisplay = string.Join(" ", pattern.PlayCards);
        var message = $"玩家 {currentPlayer.Name} 打出了 {pattern.GetTypeMandarinName()} {cardsDisplay}";

        return (true, message);
    }

    private (bool canPlay, string message) ComparingSize(CardPatternValue pattern)
    {
        if (GameState.TopPlay == null)
        {
            return (true, "");
        }

        if (pattern.Type != GameState.TopPlay.Type)
        {
            return (false, $"必須出{GameState.TopPlay.Type}！");
        }

        if (!pattern.IsBiggerThan(GameState.TopPlay))
        {
            return (false, "你的牌不夠大！");
        }

        return (true, "");
    }

    private bool CheckThreePass()
    {
        if (!GameState.HasThreePass()) return false;
        GameState.ClearTable();
            
        var topPlayerIndex = _players.IndexOf(GameState.TopPlayer!);
        GameState.SetCurrentPlayerIndex(topPlayerIndex);
            
        return true;
    }

    private Player? CheckWinner()
    {
        foreach (var player in _players)
        {
            if (!player.HasNoCards()) continue;
            player.IsWinner = true;
            return player;
        }

        return null;
    }

    private void NextPlayer()
    {
        GameState.SetCurrentPlayerIndex((GameState.CurrentPlayerIndex + 1) % 4);
    }

    public void StartGame()
    {
        InitializeFirstRound();
        
        while (true)
        {
            ConsoleUI.DisPlayTopPlayerPlay(GameState.TopPlayer, GameState.TopPlay);
            var currentPlayer = GetCurrentPlayer();
            ConsoleUI.DisplayCurrentPlayer(currentPlayer);
            ConsoleUI.DisplayerHandCards(currentPlayer);

            var  selectedCards= InputHandler.GetPlayerInput(currentPlayer);
            var (success, message) = HandlePlayerPlay(selectedCards);

            if (!success)
            {
                ConsoleUI.DisplayMessage(message, isError: true);
                ConsoleUI.WaitForKey();
                continue;
            }
            
            var winner = CheckWinner();
            if (winner != null)
            {
                ConsoleUI.WaitForKey();
                ConsoleUI.DisplayWinner(winner);
                
                if (InputHandler.GetConfirmation("\n要再玩一局嗎？"))
                {
                    InitializeFirstRound();
                    
                    ConsoleUI.WaitForKey();
                    continue;
                }
                
                Console.WriteLine("\n感謝遊玩！再見！");
                break;
            }

            if (CheckThreePass())
            {
                ConsoleUI.DisplayThreePassMessage(GetCurrentPlayer());
                ConsoleUI.WaitForKey();
                continue;
            }

            NextPlayer();
            ConsoleUI.WaitForKey();
        }
    }
}

