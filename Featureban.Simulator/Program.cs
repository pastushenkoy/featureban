using System;
using Featureban.Domain;
using Featureban.Statistics;

namespace Fetureban.Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameFactory = new GameFactory();
            var series = new Simulation(gameFactory, 
                playerCount: 10, 
                dayCount: 15, 
                iterationsPerPoint: 1000, 
                pointsCount: 11);
            
            series.Simulate();
            PrintResults(series);
        }

        private static void PrintResults(Simulation series)
        {
            const int wipColumnWidth = 3;
            const int throughputColumnWidth = 10;
            var delimiter = new string('-', wipColumnWidth + throughputColumnWidth + 7);

            Console.WriteLine(delimiter);
            Console.WriteLine($"| {"Wip", wipColumnWidth} | {"Throughput", throughputColumnWidth} |");
            Console.WriteLine(delimiter);
            foreach (var point in series)
            {
                Console.WriteLine($"| {point.WipLimit, wipColumnWidth} | {point.Throughput, throughputColumnWidth:F2} |");
            }
            Console.WriteLine(delimiter);
        }
    }
}