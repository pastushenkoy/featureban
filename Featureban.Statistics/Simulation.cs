using System.Collections;
using System.Collections.Generic;
using Featureban.Domain;
using Fetureban.Simulator.StatisticsStructure;

namespace Featureban.Statistics
{
    public class Simulation : IEnumerable<Point>
    {
        public int PlayerCount { get; }
        public int DayCount { get; }
        public int IterationsPerPoint { get; }
        public int PointsCount { get; }
        
        private readonly IGameFactory _gameFactory;
        private readonly Point[] _points;

        public IEnumerator<Point> GetEnumerator()
        {
            return ((IEnumerable<Point>) _points).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Simulation(IGameFactory gameFactory, int playerCount, int dayCount, int iterationsPerPoint, int pointsCount)
        {
            _gameFactory = gameFactory;
            
            PlayerCount = playerCount;
            DayCount = dayCount;
            IterationsPerPoint = iterationsPerPoint;
            PointsCount = pointsCount;
            
            _points = new Point[PointsCount];
        }

        public void Simulate()
        {
            for (var wipLimit = 0; wipLimit < PointsCount; wipLimit++)
            {
                _points[wipLimit] = new Point(wipLimit, GetAverageThroughput(wipLimit));
            }
        }

        private double GetAverageThroughput(int wipLimit)
        {
            var throughput = 0d;

            for (var i = 0; i < IterationsPerPoint; i++)
            {
                throughput += GetThroughput(wipLimit);
            }

            return throughput / IterationsPerPoint;
        }

        private int GetThroughput(int wipLimit)
        {
            var game = _gameFactory.Create(PlayerCount, wipLimit, wipLimit);
            game.DaysPassed(DayCount);

            return game.DoneCardsCount;
        }
    }
}