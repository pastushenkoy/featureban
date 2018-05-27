using Featureban.Domain;
using Featureban.Tests.DSL.Helpers;

namespace Featureban.Tests.DSL.Builders
{
    internal class GameBuilder
    {
        private Coin _coin = new Coin();
        private int _playerCount = 2;
        private int developmentWipLimit;
        private int testingWipLimit;
        
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
            var game = new GameTestable(_playerCount, _coin, developmentWipLimit, testingWipLimit);
            return game;
        }
    }
}