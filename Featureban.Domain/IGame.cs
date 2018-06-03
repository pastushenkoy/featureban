namespace Featureban.Domain
{
    public interface IGame
    {
        int DoneCardsCount { get; }
        void DaysPassed(int dayCount);
    }
}