using Featureban.Domain;
using Xunit;

namespace Featureban.Tests.DSL.Helpers
{
    internal class BoardTestable : Board
    {
        public void AddCardIntoTestingFor(int player)
        {
            _testingColumn.AddCard(Create.Card.OwnedBy(player).Please());
        }

        public void AddCardIntoDevelopmentFor(int player)
        {
            _developmentColumn.AddCard(Create.Card.OwnedBy(player).Please());
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
    }
}