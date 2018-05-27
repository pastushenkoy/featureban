using Featureban.Tests.DSL.Helpers;

namespace Featureban.Tests.DSL.Builders
{
    internal class BoardBuilder
    {
        private BoardTestable board;

        public BoardBuilder()
        {
            board = new BoardTestable();
        }

        public BoardTestable Please()
        {
            return board;
        }

        public BoardBuilder WithCardInTestingOwnedBy(int player)
        {
            board.AddCardIntoTestingFor(player);
            return this;
        }

        public BoardBuilder WithCardInDevelopmentOwnedBy(int player)
        {
            board.AddCardIntoDevelopmentFor(player);
            return this;
        }

        public BoardBuilder WithBlockedCardInDevelopmentOwnedBy(int player)
        {
            board.AddBlockedCardIntoDevelopmentFor(player);
            return this;
        }
    }
}