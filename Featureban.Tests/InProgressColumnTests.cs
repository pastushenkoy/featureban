using System;
using Featureban.Domain;
using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests
{
    public class InProgressColumnTests
    {
        [Fact]
        public void AddCard_AddsCard()
        {
            var column = Create.InProgressColumn.Please();
            
            column.AddCard(Create.Card.Please());
            
            Assert.Equal(1, column.CardCount);
        }

        [Fact]
        public void AddAlreadyAddedCard_ThrowsException()
        {
            var column = Create.InProgressColumn.Please();
            var card = Create.Card.Please();
            
            column.AddCard(card);

            Assert.Throws<InvalidOperationException>(() => column.AddCard(card));
        }

        [Fact]
        public void UnblockedCardOwnedByPlayerIsInColumn_HasUnblockedCardOwnedByPlayer_ReturnsTrue()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .WithCardForPlayer(player)
                .Please();

            Assert.True(column.HasUnblockedCardOwnedBy(player));
        }
        
        [Fact]
        public void UnblockedCardOwnedByAnotherPlayerIsInColumn_HasUnblockedCardOwnedByPlayer_ReturnsFalse()
        {
            var player1 = 1;
            var player2 = 2;
            var column = Create.InProgressColumn
                .WithCardForPlayer(player2)
                .Please();

            Assert.False(column.HasUnblockedCardOwnedBy(player1));
        }

        [Fact]
        public void BlockedCardOwnedByPlayerIsInColumn_HasUnblockedCardOwnedByPlayer_ReturnsFalse()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .WithCardBlockedByPlayer(player)
                .Please();

            Assert.False(column.HasUnblockedCardOwnedBy(player));
        }

        [Fact]
        public void UnbockedCardOwnedByPlayerIsInColumn_ExtractCardOwnedBy_ExtractsCard()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .WithCardForPlayer(player)
                .Please();

            var extractedCard = column.ExtractCardOwnedBy(player);
            
            Assert.NotNull(extractedCard);
            Assert.Equal(player, extractedCard.Player);
            Assert.False(extractedCard.Blocked);
            Assert.Equal(0, column.CardCount);
        }

        [Fact]
        public void UnblockedCardOwnedByAnotherPlayerIsInColumn_ExtractCardOwnedBy_ThrowsException()
        {
            var player1 = 1;
            var player2 = 2;
            var column = Create.InProgressColumn
                .WithCardForPlayer(player2)
                .Please();

            Assert.Throws<InvalidOperationException>(() => column.ExtractCardOwnedBy(player1));
            Assert.Equal(1, column.CardCount);
        }

        [Fact]
        public void BlockedCardOwnedByPlayerIsInColumn_ExtractCardOwnedBy_ThrowsException()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .WithCardBlockedByPlayer(player)
                .Please();

            Assert.Throws<InvalidOperationException>(() => column.ExtractCardOwnedBy(player));
            Assert.Equal(1, column.CardCount);
        }
        
        [Fact]
        public void BlockedCardOwnedByPlayerIsInColumn_HasBlockedCardOwnedByPlayer_ReturnsTrue()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .WithCardBlockedByPlayer(player)
                .Please();

            Assert.True(column.HasBlockedCardOwnedBy(player));
        }
        
        [Fact]
        public void BlockedCardOwnedByAnotherPlayerIsInColumn_HasBlockedCardOwnedByPlayer_ReturnsFalse()
        {
            var player1 = 1;
            var player2 = 2;
            var column = Create.InProgressColumn
                .WithCardBlockedByPlayer(player2)
                .Please();

            Assert.False(column.HasBlockedCardOwnedBy(player1));
        }

        [Fact]
        public void UnblockedCardOwnedByPlayerIsInColumn_HasBlockedCardOwnedByPlayer_ReturnsFalse()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .WithCardForPlayer(player)
                .Please();

            Assert.False(column.HasBlockedCardOwnedBy(player));
        }

        [Fact]
        public void BlockedCardOwnedByPlayerIsInColumn_UnblockCardOwnedBy_UnblocksCard()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .WithCardBlockedByPlayer(player)
                .Please();

            column.UnblockCardOwnedBy(player);

            Assert.True(column.HasUnblockedCardOwnedBy(player));
        }

        [Fact]
        public void BlockedCardOwnedByPlayerNotInColumn_UnblockCardOwnedBy_ThrowsException()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .Please();

            Assert.Throws<InvalidOperationException>(() => column.UnblockCardOwnedBy(player));
        }
        
        [Fact]
        public void UnblockedCardOwnedByPlayerIsInColumn_BlockCardOwnedBy_BlocksCard()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .WithCardForPlayer(player)
                .Please();

            column.BlockCardOwnedBy(player);

            Assert.True(column.HasBlockedCardOwnedBy(player));
        }

        [Fact]
        public void BlockedCardOwnedByPlayerNotInColumn_BlockCardOwnedBy_ThrowsException()
        {
            var player = 1;
            var column = Create.InProgressColumn
                .Please();

            Assert.Throws<InvalidOperationException>(() => column.BlockCardOwnedBy(player));
        }
    }
}