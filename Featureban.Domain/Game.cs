using System.Linq;

namespace Featureban.Domain
{
    public class Game
    {
        private int _playerCount;
        
        public Game(int playerCount)
        {
            _playerCount = playerCount;
        }
        
        public void NextDay()
        {
        }

        public int Throughout { get; }

        public int PlayerCount => _playerCount;
    }
}