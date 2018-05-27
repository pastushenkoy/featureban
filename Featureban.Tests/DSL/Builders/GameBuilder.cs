using Featureban.Domain;
using Featureban.Tests.DSL.Helpers;

namespace Featureban.Tests.DSL.Builders
{
    public class GameBuilder
    {
        private Coin _coin = new Coin();
        private int _playerCount = 2;
        
        public GameBuilder WithAlwaysTrueCoin()
        {
            _coin = new AlwaysTrueCoin();
            return this;
        }

        public GameBuilder WithPlayers(int playerCount)
        {
            _playerCount = playerCount;
            return this;
        }

        public GameTestable Please()
        {
            var game = new GameTestable(_playerCount, _coin);
            return game;
        }
    }
}