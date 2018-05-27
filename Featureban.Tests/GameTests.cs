using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests
{
    public class GameTests
    {
        [Fact]
        public void GameGeneratesResults()
        {
            var game = Create.Game
                .WithAlwaysTrueCoin()
                .WithPlayers(2)
                .Please();

            game.ExecuteGenerateCoinResults();
            
            game.AssertAllCoinResultsAreTrue();
        }
    }
}