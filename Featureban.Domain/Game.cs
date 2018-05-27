using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Featureban.Domain
{
    public class Game
    {
        private readonly Coin _coin;

        protected readonly bool[] _coinResults;
        
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
        }

        private void GenerateCoinResults()
        {
            for (var player = 0; player < _playerCount; player++)
            {
                _coinResults[player] = _coin.Flip();
            }
        }
    }
}