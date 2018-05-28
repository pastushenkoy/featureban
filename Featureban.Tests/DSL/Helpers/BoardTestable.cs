using Featureban.Domain;
using Xunit;

namespace Featureban.Tests.DSL.Helpers
{
    internal class BoardTestable : Board
    {
        public BoardTestable(int developmentWipLimit, int testingWipLimit) : base(developmentWipLimit, testingWipLimit)
        {
        }

        public void AssertAllColumnsAreEmpty()
        {
            Assert.Equal(0, _developmentColumn.CardCount);
            Assert.Equal(0, _testingColumn.CardCount);
            Assert.Equal(0, DoneCardsCount);
        }

        public void AssertCardFromTestingMovedToDone()
        {
            Assert.Equal(0, _testingColumn.CardCount);
            Assert.Equal(1, DoneCardsCount);
        }

        public void AssertCardFromDevelopmentMovedToTesting()
        {
            Assert.Equal(0, _developmentColumn.CardCount);
            Assert.Equal(1, _testingColumn.CardCount);
        }

        public void AssertHasUnblockedCardInDevelopmentFor(int player)
        {
            Assert.True(_developmentColumn.HasUnblockedCardOwnedBy(player));
        }

        public void AssertHasBlockedCardInDevelopmentFor(int player)
        {
            Assert.True(_developmentColumn.HasBlockedCardOwnedBy(player));
        }

        public void AssertHasUnblockedCardInTestingFor(int player)
        {
            Assert.True(_testingColumn.HasUnblockedCardOwnedBy(player));
        }

        public void AssertHasBlockedCardInTestingFor(int player)
        {
            Assert.True(_testingColumn.HasBlockedCardOwnedBy(player));
        }

        public void AssertInDevelopmentHasCardCount(int cardCount)
        {
            Assert.Equal(cardCount, _developmentColumn.CardCount);
        }

        public void AddCardIntoDevelopment(Card card)
        {
            _developmentColumn.AddCard(card);
        }

        public void AddCardIntoTesting(Card card)
        {
            _testingColumn.AddCard(card);
        }

	
    }
}