using System;
using System.Collections.Generic;
using System.Linq;

namespace Featureban.Domain
{
    internal class InProgressColumn
    {
        private readonly int _wipLimit;
        private readonly List<Card> _cards;

        public InProgressColumn(int wipLimit)
        {
            _wipLimit = wipLimit;
            _cards = new List<Card>();
        }

        public int CardCount => _cards.Count;

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
                throw new ArgumentException("Card is already in list");
            }

            if (!HasPlaceForCard())
            {
                throw new InvalidOperationException($"Wip limit has been reached");
            }
            
            _cards.Add(card);
        }

        public bool HasBlockedCardOwnedBy(int player)
        {
            return _cards.Any(card => card.Blocked && card.Player == player);
        }

        public void UnblockCardOwnedBy(int player)
        {
            var cardToUnblock = _cards.FirstOrDefault(card => card.Blocked && card.Player == player);
            if (cardToUnblock == null)
            {
                throw new InvalidOperationException($"Can not get blocked card for player {player}");
            }

            cardToUnblock.Unblock();
        }

        public void BlockCardOwnedBy(int player)
        {
            var cardToBlock = _cards.FirstOrDefault(card => !card.Blocked && card.Player == player);
            if (cardToBlock == null)
            {
                throw new InvalidOperationException($"Can not get unblocked card for player {player}");
            }

            cardToBlock.Block();
        }

        public bool HasPlaceForCard()
        {
            return _wipLimit == 0 || CardCount < _wipLimit;
        }

	    public Card ExtractNonBlockedCard()
	    { 
		    return _cards.FirstOrDefault(c => !c.Blocked);
	    }

	    public Card ExtractBlockedCard()
	    {
		    return _cards.FirstOrDefault(c => c.Blocked);
	    }

	    public void UnlockCard(Card card)
	    {
		    var cardInColumn = _cards.FirstOrDefault(c => c.Blocked && c.Player == card.Player);
		    cardInColumn.Unblock();
	    }
    }
}