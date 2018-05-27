using Featureban.Domain;

namespace Featureban.Tests.DSL.Helpers
{
    public class AlwaysTrueCoin : ICoin
    {
        public bool Flip()
        {
            return true;
        }
    }
}