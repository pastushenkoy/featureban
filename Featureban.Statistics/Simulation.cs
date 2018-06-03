using System.Collections;
using System.Collections.Generic;
using Featureban.Domain;
using Fetureban.Simulator.StatisticsStructure;

namespace Featureban.Statistics
{
    public class Simulation : IEnumerable<Point>
    {
        private readonly IGameFactory _gameFactory;
        private readonly int _playerCount;
        private readonly int _dayCount;
        private readonly int _iterationsPerPoint;
        private readonly int _pointsCount;
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
            _playerCount = playerCount;
            _dayCount = dayCount;
            _iterationsPerPoint = iterationsPerPoint;
            _pointsCount = pointsCount;
            _points = new Point[_pointsCount];
        }

        public void Simulate()
        {
            for (var wipLimit = 0; wipLimit < _pointsCount; wipLimit++)
            {
                _points[wipLimit] = new Point(wipLimit, GetAverageThroughput(wipLimit));
            }
        }

        private double GetAverageThroughput(int wipLimit)
        {
            var throughput = 0d;

            for (var i = 0; i < _iterationsPerPoint; i++)
            {
                throughput += GetThroughput(wipLimit);
            }

            return throughput / _iterationsPerPoint;
        }

        private int GetThroughput(int wipLimit)
        {
            var game = _gameFactory.Create(_playerCount, wipLimit, wipLimit);
            game.DaysPassed(_dayCount);

            return game.DoneCardsCount;
        }
    }
}