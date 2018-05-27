namespace Fetureban.Simulator.StatisticsStructure
{
    public class Point
    {
        public int WipLimit { get; }
        public double Throughout { get; }

        public Point(int wipLimit, double throughout)
        {
            WipLimit = wipLimit;
            Throughout = throughout;
        }
    }
}