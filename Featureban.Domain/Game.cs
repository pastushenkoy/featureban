namespace Featureban.Domain
{
    public class Game : IGame
    {
        public int DoneCardsCount => Board.DoneCardsCount;
        
        internal readonly bool[] CoinResults;
        internal IBoard Board;
        internal ICoin Coin;

        private readonly int _playerCount;

        public Game(int playerCount, int developmentWipLimit, int testingWipLimit)
        {
            Coin = new Coin();
            Board = new Board(developmentWipLimit, testingWipLimit);
            
            CoinResults = new bool[playerCount];
            _playerCount = playerCount;
        }
        
        internal void NextDay()
        {
            GenerateCoinResults();
            MakeMoves();
        }

        private void MakeMoves()
        {
            for (var player = 0; player < _playerCount; player++)
            {
                if (Won(player))
                {
                    if (!TryMakeWinMove(player))
                    {
                        GiveWinToAnotherPlayer(player);
                    }
                }
                else
                {
                    LooseMove(player);
                }
            }
        }

        private void GiveWinToAnotherPlayer(int givingPlayer)
        {
            for (int i = givingPlayer + 1; i < _playerCount + givingPlayer; i++)
            {
                var player = i % _playerCount;
                if (TryMakeWinMove(player))
                {
                    break;
                }
            }
        }

        private void LooseMove(int player)
        {
            Board.TryBlockCardOwnedBy(player);
            Board.TryTakeNewCardFor(player);
        }

        private bool TryMakeWinMove(int player)
        {
            return Board.TryMoveCardOwnedBy(player)
                   || Board.TryUnblockCardOwnedBy(player)
                   || Board.TryTakeNewCardFor(player);
        }

        private bool Won(int player)
        {
            return CoinResults[player];
        }

        protected void GenerateCoinResults()
        {
            for (var player = 0; player < _playerCount; player++)
            {
                CoinResults[player] = Coin.Flip();
            }
        }

        public void DaysPassed(int dayCount)
        {
            for (var i = 0; i < dayCount; i++)
            {
                NextDay();
            }
        }
    }
}