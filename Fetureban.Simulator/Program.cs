using System;
using Fetureban.Simulator.StatisticsStructure;

namespace Fetureban.Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var series = new Simulation(3, 15, 3, 20);
            series.Simulate();
            PrintResults(series);
        }

        private static void PrintResults(Simulation series)
        {
            foreach (var point in series)
            {
                Console.WriteLine($"{point.WipLimit} : {point.Throughout:#.##}");
            }
        }
    }
}