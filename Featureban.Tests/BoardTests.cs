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
        
        [Fact]
        public void PlayersCardsAreInTestingAndDevelopment_TryMoveCardOwnedBy_MovesCardFromTestingToDone()
        {
            var player = 1;
            
            var board = Create.Board
                .WithCardInDevelopmentOwnedBy(player)
                .WithCardInTestingOwnedBy(player)
                .Please();
            
            var result = board.TryMoveCardOwnedBy(player);
            
            Assert.True(result);
            board.AssertCardFromTestingMovedToDone();
        }

        [Fact]
        public void PlayersBlockedCardIsInDevelopment_TryUnblockCardOwnedBy_UnblocksCard()
        {
            var player = 1;
            
            var board = Create.Board
                .WithBlockedCardInDevelopmentOwnedBy(player)
                .Please();
            
            var result = board.TryUnblockCardOwnedBy(player);
            
            Assert.True(result);
            board.AssertHasUnblockedCardInDevelopmentFor(player);
        }
        
        [Fact]
        public void PlayersBlockedCardsAreInTestingAndDevelopment_TryUnblockCardOwnedBy_UnblocksCardInTesting()
        {
            var player = 1;
            
            var board = Create.Board
                .WithBlockedCardInDevelopmentOwnedBy(player)
                .WithBlockedCardInTestingOwnedBy(player)
                .Please();
            
            var result = board.TryUnblockCardOwnedBy(player);
            
            Assert.True(result);
            board.AssertHasBlockedCardInDevelopmentFor(player);
            board.AssertHasUnblockedCardInTestingFor(player);
        }
        
        [Fact]
        public void TryTakeNewCardForPlayer_TakesNewCard()
        {
            var player = 1;
            
            var board = Create.Board
                .Please();

            var result = board.TryTakeNewCardFor(player);
            Assert.True(result);
            board.AssertHasUnblockedCardInDevelopmentFor(player);
        }

        [Fact]
        public void PlayerHasUnblockedCardsInDevelopment_BlockCardOwnedBy_BlocksCard()
        {
            var player = 1;
            
            var board = Create.Board
                .WithCardInDevelopmentOwnedBy(player)
                .Please();

            var result = board.TryBlockCardOwnedBy(player);
            
            Assert.True(result);
            board.AssertHasBlockedCardInDevelopmentFor(player);
        }
    }
}