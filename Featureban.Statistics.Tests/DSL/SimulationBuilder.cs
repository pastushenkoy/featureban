using Featureban.Domain;
using Moq;

namespace Featureban.Statistics.Tests.DSL
{
    public class SimulationBuilder
    {
        private int _playerCount;
        private int _dayCount;
        private int _iterationsPerPoint;
        private int _pointsCount;
        private int _constantThroughput;

        public SimulationTestable Please()
        {
            var gameMock = CreateGameMock();
            var gameFactoryMock = CreateGameFactoryMock(gameMock);

            return new SimulationTestable(gameMock, gameFactoryMock, _playerCount, _dayCount, _iterationsPerPoint, _pointsCount);
        }

        private Mock<IGame> CreateGameMock()
        {
            var gameMock = new Mock<IGame>();
            if (_constantThroughput > 0)
            {
                gameMock
                    .SetupGet(game => game.DoneCardsCount)
                    .Returns(_constantThroughput);
            }
            
            return gameMock;
        }

        public SimulationBuilder WithPlayerCount(int playerCount)
        {
            _playerCount = playerCount;
            return this;
        }

        public SimulationBuilder WithDayCount(int dayCount)
        {
            _dayCount = dayCount;
            return this;
        }

        public SimulationBuilder WithIterationsPerPoint(int interationsPerPoint)
        {
            _iterationsPerPoint = interationsPerPoint;
            return this;
        }

        public SimulationBuilder WithPointsCount(int pointsCount)
        {
            _pointsCount = pointsCount;
            return this;
        }

        public SimulationBuilder WithConstantGameThroughput(int throughput)
        {
            _constantThroughput = throughput;
            return this;
        }

        private Mock<IGameFactory> CreateGameFactoryMock(Mock<IGame> gameMock)
        {
            var gameFactoryMock = new Mock<IGameFactory>();
            gameFactoryMock
                .Setup(factory => factory.Create(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(gameMock.Object);

            return gameFactoryMock;
        }
    }
}