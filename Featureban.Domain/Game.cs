using System.Linq;

namespace Featureban.Domain
{
    public class Game
    {
        private Player[] _players;
        
        public Game(int playerCount)
        {
            _players = new Player[playerCount];
            for (var i = 0; i < playerCount; i++)
            {
                _players[i] = new Player();
            }
        }
        
        public void NextDay()
        {
            
        }

        public int Throughout { get; }

        public int PlayerCount => _players.Length;
    }
}