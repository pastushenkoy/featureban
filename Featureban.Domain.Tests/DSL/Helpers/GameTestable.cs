using Featureban.Domain;
using Moq;
using Xunit;

namespace Featureban.Tests.DSL.Helpers
{
    internal class GameTestable : Game
    {
        private readonly Mock<IBoard> _boardMock;
        private readonly Mock<ICoin> _coinMock;

        public GameTestable(Mock<IBoard> boardMock, Mock<ICoin> coinMock, int playerCount, int developmentWipLimit, int testingWipLimit) 
            : base(playerCount, developmentWipLimit, testingWipLimit)
        {
            _boardMock = boardMock;
            _coinMock = coinMock;

            Board = _boardMock.Object;
            Coin = _coinMock.Object;
        }

        public void AssertAllCoinResultsAreTrue()
        {
            Assert.All(CoinResults, Assert.True);
        }

        public void ExecuteGenerateCoinResults()
        {
            GenerateCoinResults();
        }

        public void AssertWinMoveWasCalledFor(int player)
        {
            TryMoveCardOwnedByWasCalled(player, 1);
            TryUnblockCardOwnedByWasCalled(player, 1);
            TryTakeNewCardForWasCalled(player, 1);
        }

        public void AssertWinMoveWasCalledTwiceFor(int player)
        {
            TryMoveCardOwnedByWasCalled(player, 2);
            TryUnblockCardOwnedByWasCalled(player, 2);
            TryTakeNewCardForWasCalled(player, 2);
        }

        public void AssertLooseMoveWasCalledBy(int player)
        {
            TryBlockCardOwnedByWasCalled(player, 1);
            TryTakeNewCardForWasCalled(player, 1);
        }

        public void AssertPlayerOnlyMovesCard(int player)
        {
            TryMoveCardOwnedByWasCalled(player, 1);
            TryUnblockCardOwnedByWasCalled(player, 0);
            TryTakeNewCardForWasCalled(player, 0);
            TryBlockCardOwnedByWasCalled(player, 0);
        }

        public void AssertPlayerOnlyUnblocksCard(int player)
        {
            TryMoveCardOwnedByWasCalled(player, 1);
            TryUnblockCardOwnedByWasCalled(player, 1);
            TryTakeNewCardForWasCalled(player, 0);
            TryBlockCardOwnedByWasCalled(player, 0);
        }

        public void AssertPlayerTakesOneMoreCardOnWinMove(int player)
        {
            TryMoveCardOwnedByWasCalled(player, 1);
            TryUnblockCardOwnedByWasCalled(player, 1);
            TryTakeNewCardForWasCalled(player, 1);
            TryBlockCardOwnedByWasCalled(player, 0);
        }

        public void AssertWinMoveWasNotCalledFor(int player)
        {
            TryMoveCardOwnedByWasCalled(player, 0);
            TryUnblockCardOwnedByWasCalled(player, 0);
            TryTakeNewCardForWasCalled(player, 0);
        }

        private void TryTakeNewCardForWasCalled(int player, int count)
        {
            _boardMock.Verify(board => board.TryTakeNewCardFor(player), Times.Exactly(count));
        }

        private void TryUnblockCardOwnedByWasCalled(int player, int count)
        {
            _boardMock.Verify(board => board.TryUnblockCardOwnedBy(player), Times.Exactly(count));
        }

        private void TryMoveCardOwnedByWasCalled(int player, int count)
        {
            _boardMock.Verify(board => board.TryMoveCardOwnedBy(player), Times.Exactly(count));
        }

        private void TryBlockCardOwnedByWasCalled(int player, int count)
        {
            _boardMock.Verify(board => board.TryBlockCardOwnedBy(player), Times.Exactly(count));
        }
    }
}