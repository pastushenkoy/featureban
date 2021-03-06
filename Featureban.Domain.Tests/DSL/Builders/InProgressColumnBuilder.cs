﻿using System.Collections.Generic;
using Featureban.Domain;

namespace Featureban.Tests.DSL.Builders
{
    internal class InProgressColumnBuilder
    {
        private int _wipLimit;
        private readonly List<Card> _cardsToAdd;

        public InProgressColumnBuilder()
        {
            _cardsToAdd = new List<Card>();
        }
        
        public InProgressColumn Please()
        {
            var column = new InProgressColumn(_wipLimit);

            foreach (var card in _cardsToAdd)
            {
                column.AddCard(card);
            }

            return column;
        }

        public InProgressColumnBuilder WithCardFor(int player)
        {
            var card = Create.Card.OwnedBy(player).Please();
            _cardsToAdd.Add(card);
            return this;
        }

        public InProgressColumnBuilder WithCardBlockedByPlayer(int player)
        {
            var card = Create.Card.BlockedBy(player).Please();
            _cardsToAdd.Add(card);
            return this;
        }

        public InProgressColumnBuilder WithWipLimit(int wipLimit)
        {
            _wipLimit = wipLimit;
            return this;
        }
    }
}