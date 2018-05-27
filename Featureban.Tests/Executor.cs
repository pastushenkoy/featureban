using Xunit;
using Featureban.Domain;
using Featureban.Tests.DSL;

namespace Featureban.Tests
{
    public class Executor
    {
        [Fact]
        public void ExecuteGame()
        {
            Coin coin = new Coin();
            
            var game = new Game(2, coin);
            game.NextDay();
            
            int throughout = game.Throughout;
        }

        [Fact]
        public void GameGeneratesResults()
        {
            var game = Create.Game
                .WithAlwaysTrueCoin()
                .WithPlayers(2)
                .Please();

            game.NextDay();
            
            game.AssertAllCoinResultsAreTrue();
        }

    }
}