using Featureban.Tests.DSL.Builders;

namespace Featureban.Tests.DSL
{
    internal static class Create
    {
        public static GameBuilder Game => new GameBuilder();
        public static BoardBuilder Board => new BoardBuilder();

        public static InProgressColumnBuilder InProgressColumn => new InProgressColumnBuilder();
        public static CardBuilder Card => new CardBuilder();
    }
}