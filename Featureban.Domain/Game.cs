namespace Featureban.Domain
{
    public class Game : IGame
    {
        public int DoneCardsCount => Board.DoneCardsCount;

        private bool[] _winners;
        private bool[] _loosers;
        
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
            ResetWinnersAndLoosers();

            for (var player = 0; player < _playerCount; player++)
            {
                if (Won(player))
                {
                    if (!TryMakeWinMove(player))
                    {
                        GiveWinToAnotherPlayer(player);
                    }
                }
                else if (!MoveMade(player))
                {
                    LooseMove(player);
                }
            }
        }

        private void ResetWinnersAndLoosers()
        {
            _winners = new bool[_playerCount];
            _loosers = new bool[_playerCount];
        }

        private void GiveWinToAnotherPlayer(int givingPlayer)
        {
            for (var i = givingPlayer + 1; i < _playerCount + givingPlayer; i++)
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
            
            var result = Board.TryBlockCardOwnedBy(player)
                & Board.TryTakeNewCardFor(player);

            if (result)
            {
                MarkAsLooser(player);
            }
        }

        private bool TryMakeWinMove(int player)
        {
            if (LooseMoveMade(player))
            {
                return false;
            }
            
            var result = Board.TryMoveCardOwnedBy(player)
                   || Board.TryUnblockCardOwnedBy(player)
                   || Board.TryTakeNewCardFor(player);

            if (result)
            {
                MarkAsWinner(player);
            }

            return result;
        }

        private void MarkAsWinner(int player)
        {
            _winners[player] = true;
        }

        private void MarkAsLooser(int player)
        {
            _loosers[player] = true;
        }

        private bool Won(int player)
        {
            return CoinResults[player];
        }

        private bool MoveMade(int player)
        {
            return _winners[player] || _loosers[player];
        }

        private bool LooseMoveMade(int player)
        {
            return _loosers[player];
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