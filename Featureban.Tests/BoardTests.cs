using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests
{
    public class BoardTests
    {
        [Fact]
        public void TryMoveCardOwnedBy_MovesCard()
        {
            var player = 1;
            
            var board = Create.Board
                .AddCardIntoTestingFor(player)
                .Please();
            
            board.TryMoveCardOwnedBy(1);
            
            
        }
    }
}