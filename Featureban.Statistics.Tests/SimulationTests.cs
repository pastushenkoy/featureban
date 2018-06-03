using Featureban.Domain;
using Featureban.Statistics.Tests.DSL;
using Fetureban.Simulator.StatisticsStructure;
using Moq;
using Xunit;

namespace Featureban.Statistics.Tests
{
    public class SimulationTests
    {
        [Fact]
        public void Simulate_ExecutesDaysPassed()
        {
            var dayCount = 15;
            var interationsPerPoint = 20;
            var pointsCount = 10;

            var simulation = Create.Simulation
                .WithDayCount(dayCount)
                .WithIterationsPerPoint(interationsPerPoint)
                .WithPlayerCount(2)
                .WithPointsCount(pointsCount)
                .Please();
            
            simulation.Simulate();

            simulation.AssertGameDaysPassedHasBeenExecutedTimes(dayCount, interationsPerPoint * pointsCount);
        }

        [Fact]
        public void Simulate_GeneratesCorrectNumberOfPoints()
        {
            const int pointsCount = 10;
            
            var simulation = Create.Simulation
                .WithDayCount(15)
                .WithIterationsPerPoint(20)
                .WithPlayerCount(2)
                .WithPointsCount(pointsCount)
                .Please();
            
            simulation.Simulate();

            simulation.AvssertPointCountIs(pointsCount);
        }
        
        [Fact]
        public void Simulate_EvaluatesAverageThroughput()
        {
            const int throughput = 5;

            var simulation = Create.Simulation
                .WithConstantGameThroughput(throughput)
                .WithDayCount(15)
                .WithIterationsPerPoint(20)
                .WithPlayerCount(2)
                .WithPointsCount(10)
                .Please();
            
            simulation.Simulate();

            simulation.AssertAverageThroughputIs(throughput);
        }
    }
}