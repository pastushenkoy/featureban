namespace Featureban.Domain
{
    public class GameFactory : IGameFactory
    {
        public IGame Create(int playerCount, int developmentWipLimit, int testingWipLimit)
        {
            return new Game(playerCount, developmentWipLimit, testingWipLimit);
        }
    }
}