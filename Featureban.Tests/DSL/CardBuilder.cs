using Featureban.Domain;

namespace Featureban.Tests.DSL
{
    internal class CardBuilder
    {
        private int _player;
        private bool _blocked;

        public Card Please()
        {
            var card = new Card(_player);
            if (_blocked)
            {
                card.Block();
            }
            
            return card;
        }

        public CardBuilder OwnedBy(int player)
        {
            _player = player;
            return this;
        }

        public CardBuilder BlockedBy(int player)
        {
            _blocked = true;
            _player = player;
            return this;
        }
    }
}