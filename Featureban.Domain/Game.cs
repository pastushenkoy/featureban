namespace Featureban.Domain
{
    public class Game
    {
        private readonly ICoin _coin;

        protected readonly bool[] _coinResults;
        private readonly Board _board;
        private int _savedWins;

        public int Throughout => _board.DoneCardsCount;

        private int _playerCount { get; }

        
        public Game(int playerCount, ICoin coin, int developmentWipLimit, int testingWipLimit)
        {
            _coin = coin;
            
            _playerCount = playerCount;
            _coinResults = new bool[playerCount];
            
            _board = new Board(developmentWipLimit, testingWipLimit);
        }

        public void NextDay()
        {
            GenerateCoinResults();
            MakeMoves();
        }

        private void MakeMoves()
        {
            _savedWins = 0;
	       
            for (var player = 0; player < _playerCount; player++)
            {
                if (Won(player))
                {
	                _board.TryMoveCardOwnedBy(player);
                    if (!_board.TryMoveCardOwnedBy(player)
                        && !_board.TryUnblockCardOwnedBy(player)
                        && !_board.TryTakeNewCardFor(player))
                    {
                        SaveWinForNextPlayer();
                    }
                }
                else
                {
                    _board.TryBlockCardOwnedBy(player);
                    _board.TryTakeNewCardFor(player);
                }

            }
        }

        private void SaveWinForNextPlayer()
        {
            _savedWins++;
        }

        private bool Won(int player)
        {
            return _coinResults[player] || TryTakeSavedWin();
        }

        private bool TryTakeSavedWin()
        {
            if (_savedWins == 0)
            {
                return false;
            }
            else
            {
                _savedWins--;
                return true;
            }
        }

        protected void GenerateCoinResults()
        {
            for (var player = 0; player < _playerCount; player++)
            {
                _coinResults[player] = _coin.Flip();
            }
        }
    }
}