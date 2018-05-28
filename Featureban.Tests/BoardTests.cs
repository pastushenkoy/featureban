using System;
using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests
{
    public class BoardTests
    {
        [Fact]
        public void TryMoveCardOwnedBy_MovesCardToDone_WhenPlayersCardIsInTesting()
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
        public void TryMoveCardOwnedBy_MovesCardToTesting_PlayersCardIsInDevelopment()
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
        public void TryMoveCardOwnedBy_ReturnsFalse_PlayerHasNoCardsInBoard()
        {
            var player = 1;
            
            var board = Create.Board
                .Please();
            
            var result = board.TryMoveCardOwnedBy(player);
            
            Assert.False(result);
            board.AssertAllColumnsAreEmpty();
        }
        
        [Fact]
        public void TryMoveCardOwnedBy_MovesCardFromTestingToDone_PlayersCardsAreInTestingAndDevelopment()
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
        public void TryMoveCardOwnedBy_ReturnsFalse_WhenTestingWipLimitHasBeenReached()
        {
            var player = 1;
            
            var board = Create.Board
                .WithCardInDevelopmentOwnedBy(player)
                .WithBlockedCardInTestingOwnedBy(player)
                .WithTestingWipLimit(1)
                .Please();
            
            var result = board.TryMoveCardOwnedBy(player);
            
            Assert.False(result);
            board.AssertHasUnblockedCardInDevelopmentFor(player);
            board.AssertHasBlockedCardInTestingFor(player);
        }
        
        [Fact]
        public void TryUnblockCardOwnedBy_UnblocksCard_PlayersBlockedCardIsInDevelopment()
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
        public void TryUnblockCardOwnedBy_UnblocksCardInTesting_PlayersBlockedCardsAreInTestingAndDevelopment()
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
        public void TryTakeNewCardForPlayer_TakesNewCard_WhenDevelopmentWipLimitHasNotBeenReached()
        {
            var player = 1;
            
            var board = Create.Board
                .Please();

            var result = board.TryTakeNewCardFor(player);
            Assert.True(result);
            board.AssertHasUnblockedCardInDevelopmentFor(player);
        }

        [Fact]
        public void TryTakeNewCardForPlayer_ReturnsFalse_WhenDevelopmentWipLimitHasBeenReached()
        {
            var player = 1;
            
            var board = Create.Board
                .WithDevelopmentWipLimit(1)
                .WithCardInDevelopmentOwnedBy(player)
                .Please();

            var result = board.TryTakeNewCardFor(player);
            Assert.False(result);
            board.AssertInDevelopmentHasCardCount(1);
        }

        [Fact]
        public void BlockCardOwnedBy_BlocksCard_PlayerHasUnblockedCardsInDevelopment()
        {
            var player = 1;
            
            var board = Create.Board
                .WithCardInDevelopmentOwnedBy(player)
                .Please();

            var result = board.TryBlockCardOwnedBy(player);
            
            Assert.True(result);
            board.AssertHasBlockedCardInDevelopmentFor(player);
        }
        
        [Fact]
        public void BlockCardOwnedBy_BlocksCardInTesting_PlayerHasUnblockedCardsInDevelopmentAndInTesting()
        {
            var player = 1;
            
            var board = Create.Board
                .WithCardInDevelopmentOwnedBy(player)
                .WithCardInTestingOwnedBy(player)
                .Please();

            var result = board.TryBlockCardOwnedBy(player);
            
            Assert.True(result);
            board.AssertHasBlockedCardInTestingFor(player);
        }

	    [Fact]
	    public void MoveNearestCard_Success()
	    {
		    var player = 1;
            
		    var board = Create.Board
			    .WithCardInDevelopmentOwnedBy(player)
			    .WithCardInTestingOwnedBy(player)
			    .Please();

		    board.MoveNearestCard();
		    
		    Assert.Equal(1,board.DoneCardsCount);

	    }
    }
}