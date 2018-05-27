using Featureban.Domain;
using Xunit;

namespace Featureban.Tests.DSL.Helpers
{
    internal class GameTestable : Game
    {
        public GameTestable(int playerCount, ICoin coin, int developmentWipLimit, int testingWipLimit) 
            : base(playerCount, coin, developmentWipLimit, testingWipLimit)
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