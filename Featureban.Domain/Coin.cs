using System;

namespace Featureban.Domain
{
    public class Coin
    {
        private readonly Random _random;

        public Coin()
        {
            _random = new Random(42);
        }

        public virtual bool Flip()
        {
            return Convert.ToBoolean(_random.Next(2));
        }
    }
}