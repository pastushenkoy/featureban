using System;
using Featureban.Domain;
using Featureban.Tests.DSL.Helpers;
using Moq;

namespace Featureban.Tests.DSL.Builders
{
    internal class GameBuilder
    {
        private int _playerCount = 2;

        private int _developmentWipLimit = 0;
        private int _testingWipLimit = 0;
        
        private CoinType _coinType = CoinType.AlwaysWin;

        private bool _resultOfTryMoveCardOwnedBy;
        private bool _resultOfTryUnblockCardOwnedBy;
        private bool _resultTryTakeNewCardFor;

        public GameBuilder WithAlwaysWinCoin()
        {
            _coinType = CoinType.AlwaysWin;
            return this;
        }

        public GameBuilder WithAlwaysLooseCoin()
        {
            _coinType = CoinType.AlwaysLoose;
            return this;
        }

        public GameBuilder WithPlayerCount(int playerCount)
        {
            _playerCount = playerCount;
            return this;
        }

        public GameTestable Please()
        {
            var boardMock = CreateBoardMock();
            var coinMock = CreateCoinMock();
            
            var game = new GameTestable(boardMock, coinMock, _playerCount, _developmentWipLimit, _testingWipLimit);
            return game;
        }

        private Mock<ICoin> CreateCoinMock()
        {
            var coinMock = new Mock<ICoin>();
            switch (_coinType)
            {
                case CoinType.AlwaysLoose:
                    coinMock.Setup(coin => coin.Flip()).Returns(false);
                    break;
                case CoinType.AlwaysWin:
                    coinMock.Setup(coin => coin.Flip()).Returns(true);
                    break;
            }

            return coinMock;
        }

        private Mock<IBoard> CreateBoardMock()
        {
            var boardMock = new Mock<IBoard>();
            
            boardMock
                .Setup(board => board.TryMoveCardOwnedBy(It.IsAny<int>()))
                .Returns(_resultOfTryMoveCardOwnedBy);

            boardMock
                .Setup(board => board.TryUnblockCardOwnedBy(It.IsAny<int>()))
                .Returns(_resultOfTryUnblockCardOwnedBy);

            boardMock
                .Setup(board => board.TryTakeNewCardFor(It.IsAny<int>()))
                .Returns(_resultTryTakeNewCardFor);
            
            return boardMock;
        }

        public GameBuilder WithNoOpportunityForWinMove()
        {
            _resultOfTryMoveCardOwnedBy = false;
            _resultOfTryUnblockCardOwnedBy = false;
            _resultTryTakeNewCardFor = false;

            return this;
        }

        public GameBuilder WithOpportunityToMoveCard()
        {
            _resultOfTryMoveCardOwnedBy = true;
            return this;
        }

        public GameBuilder WithOpportunityToUnblockCard()
        {
            _resultOfTryUnblockCardOwnedBy = true;
            return this;
        }

        public GameBuilder WithOpportunityToTakeOneMoreCard()
        {
            _resultTryTakeNewCardFor = true;
            return this;
        }

        private enum CoinType
        {
            AlwaysWin,
            AlwaysLoose
        }
    }
}