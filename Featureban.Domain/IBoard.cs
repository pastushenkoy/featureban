namespace Featureban.Domain
{
    internal interface IBoard
    {
        int DoneCardsCount { get; }
        bool TryMoveCardOwnedBy(int player);
        bool TryUnblockCardOwnedBy(int player);
        bool TryTakeNewCardFor(int player);
        bool TryBlockCardOwnedBy(int player);
    }
}