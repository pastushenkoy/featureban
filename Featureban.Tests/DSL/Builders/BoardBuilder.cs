using System.Collections.Generic;
using Featureban.Domain;
using Featureban.Tests.DSL.Helpers;

namespace Featureban.Tests.DSL.Builders
{
    internal class BoardBuilder
    {
        private int _developmentWipLimit;
        private int _testingWipLimit;
        
        private List<Card> _developmentCards = new List<Card>();
        private readonly List<Card> _testingCards = new List<Card>();
        
        public BoardTestable Please()
        {
            var board = new BoardTestable(_developmentWipLimit, _testingWipLimit);

            _developmentCards.ForEach(card => board.AddCardIntoDevelopment(card));
            _testingCards.ForEach(card => board.AddCardIntoTesting(card));
            return board;
        }

        public BoardBuilder WithCardInTestingOwnedBy(int player)
        {
            _testingCards.Add(Create.Card.OwnedBy(player).Please());
            return this;
        }

        public BoardBuilder WithCardInDevelopmentOwnedBy(int player)
        {
            _developmentCards.Add(Create.Card.OwnedBy(player).Please());
            return this;
        }

        public BoardBuilder WithBlockedCardInDevelopmentOwnedBy(int player)
        {
            _developmentCards.Add(Create.Card.BlockedBy(player).Please());
            return this;
        }

        public BoardBuilder WithBlockedCardInTestingOwnedBy(int player)
        {
            _testingCards.Add(Create.Card.BlockedBy(player).Please());
            return this;
        }
    }
}