using System;
using System.Collections;
using System.Collections.Generic;
using Featureban.Domain;

namespace Fetureban.Simulator.StatisticsStructure
{
    public class Simulation : IEnumerable<Point>
    {
        private readonly int _playerCount;
        private readonly int _dayCount;
        private readonly int _iterationsPerPoint;
        private readonly int _wipLimitMax;
        private readonly Point[] _points;
        private readonly Coin _coin;

        public IEnumerator<Point> GetEnumerator()
        {
            return ((IEnumerable<Point>) _points).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Simulation(int playerCount, int dayCount, int iterationsPerPoint, int wipLimitMax)
        {
            _playerCount = playerCount;
            _dayCount = dayCount;
            _iterationsPerPoint = iterationsPerPoint;
            _wipLimitMax = wipLimitMax;
            _points = new Point[_wipLimitMax + 1];
            _coin = new Coin();
        }

        public void Simulate()
        {
            for (var wipLimit = 0; wipLimit <= _wipLimitMax; wipLimit++)
            {
                _points[wipLimit] = new Point(wipLimit, GetAverageThroughout(wipLimit)); 
            }
        }

        private double GetAverageThroughout(int wipLimit)
        {
            var throughout = 0d;
            
            for (var i = 0; i < _iterationsPerPoint; i++)
            {
                throughout += GetThroughout(_coin, wipLimit);
            }

            return throughout / _iterationsPerPoint;
        }

        private int GetThroughout(Coin coin, int wipLimit)
        {
            var game = new Game(_playerCount, coin, wipLimit, wipLimit);
            for (var day = 1; day <= _dayCount; day++)
            {
                game.NextDay();
            }

            return game.Throughout;
        }
    }
}