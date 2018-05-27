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

        public BoardBuilder AddCardIntoTestingFor(int player)
        {
            board.AddCardIntoTestingFor(player);
            return this;
        }
    }
}