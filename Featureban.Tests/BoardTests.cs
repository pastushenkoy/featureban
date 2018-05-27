using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests
{
    public class BoardTests
    {
        [Fact]
        public void PlayersCardIsInTesting_TryMoveCardOwnedBy_MovesCardToDone()
        {
            var player = 1;
            
            var board = Create.Board
                .WithCardInTestingOwnedBy(player)
                .Please();
            
            var result = board.TryMoveCardOwnedBy(player);
            
            Assert.True(result);
            board.AssertCardFromTestingMovedToDone();
        }
        
        [Fact]
        public void PlayersCardIsInDevelopment_TryMoveCardOwnedBy_MovesCardToTesting()
        {
            var player = 1;
            
            var board = Create.Board
                .WithCardInDevelopmentOwnedBy(player)
                .Please();
            
            var result = board.TryMoveCardOwnedBy(player);
            
            Assert.True(result);
            board.AssertCardFromDevelopmentMovedToTesting();
        }

        [Fact]
        public void PlayerHasNoCardsInBoard_TryMoveCardOwnedBy_ReturnsFalse()
        {
            var player = 1;
            
            var board = Create.Board
                .Please();
            
            var result = board.TryMoveCardOwnedBy(player);
            
            Assert.False(result);
            board.AssertAllColumnsAreEmpty();
        }
    }
}