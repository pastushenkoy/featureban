namespace Fetureban.Simulator.StatisticsStructure
{
    public class Point
    {
        public int WipLimit { get; }
        public double Throughput { get; }

        public Point(int wipLimit, double throughput)
        {
            WipLimit = wipLimit;
            Throughput = throughput;
        }
    }
}