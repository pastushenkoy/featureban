using System;

namespace Featureban.Domain
{
    internal class Coin : ICoin
    {
        private readonly Random _random;

        public Coin()
        {
            _random = new Random();
        }

        public bool Flip()
        {
            return Convert.ToBoolean(_random.Next(2));
        }
    }
}