using System;
using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests
{
    public class InProgressColumnTests
    {
        [Fact]
        public void AddCard_AddsCard_WhenWipLimitHasNotBeenReached()
        {
            var column = Create
                .InProgressColumn
                .WithWipLimit(1)
                .Please();
            
            column.AddCard(Create.Card.Please());
            
            Assert.Equal(1, column.CardCount);
        }

        [Fact]
        public void AddCard_AddsCard_WhenWipLimitIsZero()
        {
            var column = Create
                .InProgressColumn
                .WithWipLimit(0)
                .Please();
            
            column.AddCard(Create.Card.Please());
            
            Assert.Equal(1, column.CardCount);
        }

        [Fact]
        public void AddCard_ThrowsException_WhenWipLimitHasBeenReached()
        {
            var player = 2;
            var column = Create
                .InProgressColumn
                .WithWipLimit(1)
                .WithCardFor(player)
                .Please();
            
            Assert.Throws<InvalidOperationException>(() => column.AddCard(Create.Card.Please()));
        }

        [Fact]
        public void AddCard_ThrowsException_WhenTryToAddAlreadyAddedCard()
        {
            var column = Create.InProgressColumn.Please();
            var card = Create.Card.Please();
            
            column.AddCard(card);

            Assert.Throws<ArgumentException>(() => column.AddCard(card));
        }

        [Fact]
        public void HasUnblockedCardOwnedByPlayer_ReturnsTrue_WhenUnblockedCardOwnedByPlayerIsInColumn()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .WithCardFor(player)
                .Please();

            Assert.True(column.HasUnblockedCardOwnedBy(player));
        }
        
        [Fact]
        public void HasUnblockedCardOwnedByPlayer_ReturnsFalse_WhenUnblockedCardOwnedByAnotherPlayerIsInColumn()
        {
            var player1 = 1;
            var player2 = 2;
            var column = Create.InProgressColumn
                .WithCardFor(player2)
                .Please();

            Assert.False(column.HasUnblockedCardOwnedBy(player1));
        }

        [Fact]
        public void HasUnblockedCardOwnedByPlayer_ReturnsFalse_WhenBlockedCardOwnedByPlayerIsInColumn()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .WithCardBlockedByPlayer(player)
                .Please();

            Assert.False(column.HasUnblockedCardOwnedBy(player));
        }

        [Fact]
        public void ExtractCardOwnedBy_ExtractsCard_WhenUnbockedCardOwnedByPlayerIsInColumn()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .WithCardFor(player)
                .Please();

            var extractedCard = column.ExtractCardOwnedBy(player);
            
            Assert.NotNull(extractedCard);
            Assert.Equal(player, extractedCard.Player);
            Assert.False(extractedCard.Blocked);
            Assert.Equal(0, column.CardCount);
        }

        [Fact]
        public void ExtractCardOwnedBy_ThrowsException_WhenUnblockedCardOwnedByAnotherPlayerIsInColumn()
        {
            var player1 = 1;
            var player2 = 2;
            var column = Create.InProgressColumn
                .WithCardFor(player2)
                .Please();

            Assert.Throws<InvalidOperationException>(() => column.ExtractCardOwnedBy(player1));
            Assert.Equal(1, column.CardCount);
        }

        [Fact]
        public void ExtractCardOwnedBy_ThrowsException_WhenBlockedCardOwnedByPlayerIsInColumn()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .WithCardBlockedByPlayer(player)
                .Please();

            Assert.Throws<InvalidOperationException>(() => column.ExtractCardOwnedBy(player));
            Assert.Equal(1, column.CardCount);
        }
        
        [Fact]
        public void HasBlockedCardOwnedByPlayer_ReturnsTrue_WhenBlockedCardOwnedByPlayerIsInColumn()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .WithCardBlockedByPlayer(player)
                .Please();

            Assert.True(column.HasBlockedCardOwnedBy(player));
        }
        
        [Fact]
        public void HasBlockedCardOwnedByPlayer_ReturnsFalse_WhenBlockedCardOwnedByAnotherPlayerIsInColumn()
        {
            var player1 = 1;
            var player2 = 2;
            var column = Create.InProgressColumn
                .WithCardBlockedByPlayer(player2)
                .Please();

            Assert.False(column.HasBlockedCardOwnedBy(player1));
        }

        [Fact]
        public void HasBlockedCardOwnedByPlayer_ReturnsFalse_WhenUnblockedCardOwnedByPlayerIsInColumn()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .WithCardFor(player)
                .Please();

            Assert.False(column.HasBlockedCardOwnedBy(player));
        }

        [Fact]
        public void UnblockCardOwnedBy_UnblocksCard_WhenBlockedCardOwnedByPlayerIsInColumn()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .WithCardBlockedByPlayer(player)
                .Please();

            column.UnblockCardOwnedBy(player);

            Assert.True(column.HasUnblockedCardOwnedBy(player));
        }

        [Fact]
        public void UnblockCardOwnedBy_ThrowsException_WhenBlockedCardOwnedByPlayerNotInColumn()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .Please();

            Assert.Throws<InvalidOperationException>(() => column.UnblockCardOwnedBy(player));
        }
        
        [Fact]
        public void BlockCardOwnedBy_BlocksCard_WhenUnblockedCardOwnedByPlayerIsInColumn()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .WithCardFor(player)
                .Please();

            column.BlockCardOwnedBy(player);

            Assert.True(column.HasBlockedCardOwnedBy(player));
        }

        [Fact]
        public void BlockCardOwnedBy_ThrowsException_BlockedCardOwnedByPlayerNotInColumn()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .Please();

            Assert.Throws<InvalidOperationException>(() => column.BlockCardOwnedBy(player));
        }

        [Fact]
        public void HasPlaceForCard_ReturnsTrue_WhenWipLimitHasNotBeenReached()
        {
            var column = Create.InProgressColumn
                .WithWipLimit(1)
                .Please();
            
            Assert.True(column.HasPlaceForCard());
        }
        
        [Fact]
        public void HasPlaceForCard_ReturnsTrue_WhenWipLimitIsZero()
        {
            var column = Create.InProgressColumn
                .WithWipLimit(0)
                .Please();
            
            Assert.True(column.HasPlaceForCard());
        }
        
        [Fact]
        public void HasPlaceForCard_ReturnsFalse_WhenWipLimitHasBeenReached()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .WithWipLimit(1)
                .WithCardFor(player)
                .Please();
            
            Assert.False(column.HasPlaceForCard());
        }
    }
}