using System.Linq;
using Featureban.Domain;
using Moq;
using Xunit;

namespace Featureban.Statistics.Tests.DSL
{
    public class SimulationTestable : Simulation
    {
        private readonly Mock<IGame> _gameMock;
        private readonly Mock<IGameFactory> _gameFactoryMock;

        public SimulationTestable(Mock<IGame> gameMock, Mock<IGameFactory> gameFactoryMock, int playerCount, int dayCount, int iterationsPerPoint, int pointsCount) 
            : base(gameFactoryMock.Object, playerCount, dayCount, iterationsPerPoint, pointsCount)
        {
            _gameMock = gameMock;
            _gameFactoryMock = gameFactoryMock;
        }


        public void AssertGameDaysPassedHasBeenExecutedTimes(int dayCount, int executedTimes)
        {
            _gameMock.Verify(game => game.DaysPassed(dayCount), Times.Exactly(executedTimes));
        }

        public void AssertAverageThroughputIs(int throughput)
        {
            Assert.All(this, point => Assert.Equal(throughput, point.Throughput));
        }

        public void AvssertPointCountIs(int pointsCount)
        {
            Assert.Equal(pointsCount, this.Count());
        }
    }
}