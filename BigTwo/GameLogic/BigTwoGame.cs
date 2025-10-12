using BigTwo.Models;
using BigTwo.CardPatterns;
using BigTwo.UI;

namespace BigTwo.GameLogic;

public class BigTwoGame
{
    private readonly List<Player> _players;
    private readonly Deck _deck;
    private CardPatternHandler _cardPatternHandler;
    private const int MaxHandCardNumber = 13;
    private const int TotalPLayers = 4;
    private GameState GameState { get; }

    public BigTwoGame(List<string> playerNames)
    {
        _players = [];
        _deck = new Deck();
        GameState = new GameState();
        //need modify
        _cardPatternHandler = null!;
        
        Initialize(playerNames);
    }

    private void Initialize(List<string> playerNames)
    {
        AddPlayersIntoGame(playerNames);

        _deck.Initialize();

        //can move to Main function to create the chain
        //and DI into players 
        CreateHandlerChain();
    }

    private void CreateHandlerChain()
    {
        _cardPatternHandler = new PassHandler();
        _cardPatternHandler.SetNext(new SingleHandler())
            .SetNext(new PairHandler())
            .SetNext(new StraightHandler())
            .SetNext(new FullHouseHandler())
            .SetNext(new InvalidHandler());
    }

    private void AddPlayersIntoGame(List<string> playerNames)
    {
        for (var playerIndex = 0; playerIndex < TotalPLayers; playerIndex++)
        {
            var playerName = IsValidName(playerNames, playerIndex) 
                ? playerNames[playerIndex].Trim() 
                : $"玩家 {playerIndex + 1}";
            
            _players.Add(new Player(playerName));
        }
    }

    private static bool IsValidName(List<string> playerNames, int playerIndex)
    {
        return !string.IsNullOrWhiteSpace(playerNames[playerIndex]);
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

        GameState.CurrentPlayerIndex = FindClubThreePlayer();
        GameState.IsFirstRound = true;
        ConsoleUI.DisPlayNewRound();
    }

    private void DealingCards()
    {
        for (var cardIndex = 0; cardIndex < MaxHandCardNumber; cardIndex++)
        {
            foreach (var player in _players)
            {
                var card = _deck.DealingCard();
                player.ReceiveCard(card);
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
        
        var pattern = ValidateMove(cards);

        if (pattern == null || pattern.IsInvalid)
        {
            return (false, "無效的牌型！");
        }

        // 處理Pass
        if (pattern.IsPass)
        {
            // 如果桌面是空的，不能Pass
            if (GameState.TopPlay == null)
            {
                return (false, "桌面是空的，不能Pass！");
            }

            GameState.IncrementPassCount();
            return (true, $"{currentPlayer.Name} Pass");
        }

        // 檢查首回合是否包含梅花3
        if (GameState.IsFirstRound)
        {
            Card? clubThree = currentPlayer.Hand.GetClubThree();
            if (clubThree != null && !cards.Contains(clubThree))
            {
                return (false, "第一手必須包含梅花3！");
            }
            GameState.IsFirstRound = false;
        }

        // 比較牌型大小
        var (canPlay, compareMessage) = ComparingSize(pattern);
        if (!canPlay)
        {
            return (false, compareMessage);
        }

        currentPlayer.PlayCards(cards);

        // 更新遊戲狀態
        GameState.UpdateTopPlay(currentPlayer, pattern);

        return (true, $"{currentPlayer.Name} 出牌: {pattern}");
    }

    private CardPatternValue? ValidateMove(List<Card> cards)
    {
        return _cardPatternHandler.Handle(cards);
    }

    /// <summary>
    /// 比較牌型大小
    /// </summary>
    private (bool canPlay, string message) ComparingSize(CardPatternValue pattern)
    {
        // 如果桌面是空的，可以出任意牌型
        if (GameState.TopPlay == null)
        {
            return (true, "");
        }

        // 必須是相同牌型
        if (pattern.Type != GameState.TopPlay.Type)
        {
            return (false, $"必須出{GameState.TopPlay.Type}！");
        }

        // 必須比桌面的牌大
        if (!pattern.IsStrongerThan(GameState.TopPlay))
        {
            return (false, "你的牌不夠大！");
        }

        return (true, "");
    }

    /// <summary>
    /// 檢查是否三家Pass
    /// </summary>
    public bool CheckThreePass()
    {
        if (GameState.IsThreePass())
        {
            // 清空桌面
            GameState.ClearTable();
            
            // 將當前玩家設為最後出牌者
            if (GameState.TopPlayer != null)
            {
                int topPlayerIndex = _players.IndexOf(GameState.TopPlayer);
                GameState.CurrentPlayerIndex = topPlayerIndex;
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// 檢查勝利者
    /// </summary>
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
        GameState.CurrentPlayerIndex = (GameState.CurrentPlayerIndex + 1) % 4;
    }

    public void StartGame()
    {
        InitializeFirstRound();
        
        while (true)
        {
            var currentPlayer = GetCurrentPlayer();
            ConsoleUI.DisplayCurrentPlayer(currentPlayer);
            ConsoleUI.DisplayHand(currentPlayer);

            //use foreach to let each player do action
            //push Player Input into Player
            //and player can use handler to determine cardPattern
            // when BigTwoGame Get the cardPattern , determine if is valid, otherwise ask player to play card again
            var (isQuit, isPass, selectedCards) = InputHandler.GetPlayerInput(currentPlayer);

            //No Support IsQuit
            if (isQuit)
            {
                if (InputHandler.GetConfirmation("確定要退出遊戲嗎？"))
                {
                    Console.WriteLine("\n感謝遊玩！再見！");
                    break;
                }
                continue;
            }

            var cardsToPlay = isPass ? [] : selectedCards;
            var (success, message) = HandlePlayerPlay(cardsToPlay);

            if (!success)
            {
                ConsoleUI.DisplayMessage(message, isError: true);
                ConsoleUI.WaitForKey();
                continue;
            }

            ConsoleUI.DisplayMessage(message);

            var winner = CheckWinner();
            if (winner != null)
            {
                ConsoleUI.WaitForKey();
                ConsoleUI.DisplayWinner(winner);
                
                if (InputHandler.GetConfirmation("\n要再玩一局嗎？"))
                {
                    InitializeFirstRound();
                    
                    var firstPlayer = GetCurrentPlayer();
                    if (firstPlayer.HasClubThree())
                    {
                        ConsoleUI.DisplayFirstRoundHint(firstPlayer);
                    }
                    
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

