using Featureban.Domain;

namespace Featureban.Tests.DSL.Helpers
{
    public class AlwaysTrueCoin : Coin
    {
        public override bool Flip()
        {
            return true;
        }
    }
}