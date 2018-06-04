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
            WasCalledTryMoveCardOwnedBy(player, 1);
            WasCalledTryUnblockCardOwnedBy(player, 1);
            WasCalledTryTakeNewCardFor(player, 1);
        }

        public void AssertWinMoveWasCalledTwiceFor(int player)
        {
            WasCalledTryMoveCardOwnedBy(player, 2);
            WasCalledTryUnblockCardOwnedBy(player, 2);
            WasCalledTryTakeNewCardFor(player, 2);
        }

        public void AssertLooseMoveWasCalledBy(int player)
        {
            WasCalledTryBlockCardOwnedBy(player, 1);
            WasCalledTryTakeNewCardFor(player, 1);
        }

        public void AssertPlayerOnlyMovesCard(int player)
        {
            WasCalledTryMoveCardOwnedBy(player, 1);
            WasCalledTryUnblockCardOwnedBy(player, 0);
            WasCalledTryTakeNewCardFor(player, 0);
            WasCalledTryBlockCardOwnedBy(player, 0);
        }

        public void AssertPlayerOnlyUnblocksCard(int player)
        {
            WasCalledTryMoveCardOwnedBy(player, 1);
            WasCalledTryUnblockCardOwnedBy(player, 1);
            WasCalledTryTakeNewCardFor(player, 0);
            WasCalledTryBlockCardOwnedBy(player, 0);
        }

        public void AssertPlayerTakesOneMoreCardOnWinMove(int player)
        {
            WasCalledTryMoveCardOwnedBy(player, 1);
            WasCalledTryUnblockCardOwnedBy(player, 1);
            WasCalledTryTakeNewCardFor(player, 1);
            WasCalledTryBlockCardOwnedBy(player, 0);
        }

        public void AssertWinMoveWasNotCalledFor(int player)
        {
            WasCalledTryMoveCardOwnedBy(player, 0);
            WasCalledTryUnblockCardOwnedBy(player, 0);
        }

        private void WasCalledTryTakeNewCardFor(int player, int count)
        {
            _boardMock.Verify(board => board.TryTakeNewCardFor(player), Times.Exactly(count));
        }

        private void WasCalledTryUnblockCardOwnedBy(int player, int count)
        {
            _boardMock.Verify(board => board.TryUnblockCardOwnedBy(player), Times.Exactly(count));
        }

        private void WasCalledTryMoveCardOwnedBy(int player, int count)
        {
            _boardMock.Verify(board => board.TryMoveCardOwnedBy(player), Times.Exactly(count));
        }

        private void WasCalledTryBlockCardOwnedBy(int player, int count)
        {
            _boardMock.Verify(board => board.TryBlockCardOwnedBy(player), Times.Exactly(count));
        }
    }
}