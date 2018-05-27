using Featureban.Domain;
using Xunit;

namespace Featureban.Tests.DSL.Helpers
{
    public class GameTestable : Game
    {
        public GameTestable(int playerCount, Coin coin) : base(playerCount, coin)
        {
        }

        public void AssertAllCoinResultsAreTrue()
        {
            Assert.All(_coinResults, Assert.True);
        }

        public void ExecuteGenerateCoinResults()
        {
            GenerateCoinResults();
        }
    }
}