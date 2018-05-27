using System;

namespace Featureban.Domain
{
    public class Coin : ICoin
    {
        private readonly Random _random;

        public Coin()
        {
            _random = new Random(42);
        }

        public bool Flip()
        {
            return Convert.ToBoolean(_random.Next(2));
        }
    }
}