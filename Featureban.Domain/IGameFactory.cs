namespace Featureban.Domain
{
    public interface IGameFactory
    {
        IGame Create(int playerCount, int developmentWipLimit, int testingWipLimit);
    }
}