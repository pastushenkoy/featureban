using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Featureban.Domain
{
    public class Game
    {
        private readonly Coin _coin;

        protected readonly bool[] _coinResults;
        private int _savedWins;
        private Board board;

        public int Throughout { get; }

        private int _playerCount { get; }

        
        public Game(int playerCount, Coin coin)
        {
            _coin = coin;
            
            _playerCount = playerCount;
            _coinResults = new bool[playerCount];
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
                    if (!board.TryMoveCardOwnedBy(player)
                        && !board.TryUnblockCardOwnedBy(player)
                        && !board.TryTakeNewCardFor(player))
                    {
                        SaveWinForNextPlayer();
                    }
                }
                else
                {
                    board.BlockCardOwnedByPlayer(player);
                    board.TryTakeNewCardFor(player);
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