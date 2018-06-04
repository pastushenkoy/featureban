using System;
using System.Collections.Generic;
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

        private readonly List<int> _playersWithMovableCards = new List<int>();
        
        private bool _resultTryTakeNewCardFor;
        private bool[] _coinResults;

        
        
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

        public GameBuilder WithCoinResults(params bool[] coinResults)
        {
            _coinType = CoinType.PreSet;
            _coinResults = coinResults;
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
                case CoinType.PreSet:
                    var sequence = coinMock.SetupSequence(coin => coin.Flip());
                    foreach (var coinResult in _coinResults)
                        sequence.Returns(coinResult);
                    
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

            foreach (var player in _playersWithMovableCards)
            {
                boardMock
                    .Setup(board => board.TryMoveCardOwnedBy(player))
                    .Returns(true);
            }
            
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

        public GameBuilder WithMovableCardsFor(int secondPlayer)
        {
            _playersWithMovableCards.Add(secondPlayer);            
            return this;
        }

        private enum CoinType
        {
            AlwaysWin,
            AlwaysLoose,
            PreSet
        }
    }
}