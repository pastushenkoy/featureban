using Xunit;
using Featureban.Domain;

namespace Featureban.Tests
{
    public class Executor
    {
        [Fact]
        public void ExecuteGame()
        {
            var game = new Game();
            game.NextDay();
            int throughout = game.Throughout;
        }
    }
}