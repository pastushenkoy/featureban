using System;
using System.Collections.Generic;
using System.Linq;

namespace Featureban.Domain
{
    internal class InProgressColumn
    {
        private List<Card> _cards;

        public InProgressColumn()
        {
            _cards = new List<Card>();
        }
        
        public bool HasUnblockedCardOwnedBy(int player)
        {
            return _cards.Any(card => !card.Blocked && card.Player == player);
        }

        public Card ExtractCardOwnedBy(int player)
        {
            var cardToExtract = _cards.FirstOrDefault(card => !card.Blocked && card.Player == player);
            if (cardToExtract == null)
            {
                throw new InvalidOperationException($"Can not get unblocked card for player {player}");
            }

            _cards.Remove(cardToExtract);
            return cardToExtract;
        }

        public void AddCard(Card card)
        {
            if (_cards.Contains(card))
            {
                throw new InvalidOperationException("Card is already in list");
            }
            
            _cards.Add(card);
        }
    }
}