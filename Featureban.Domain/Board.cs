﻿namespace Featureban.Domain
{
    internal class Board
    {
        protected readonly InProgressColumn _developmentColumn;
        protected readonly InProgressColumn _testingColumn;

        public int DoneCardsCount { get; private set; }
        
        public Board()
        {
            _developmentColumn = new InProgressColumn();
            _testingColumn = new InProgressColumn();
        }
        
        public bool TryMoveCardOwnedBy(int player)
        {
            if (_testingColumn.HasUnblockedCardOwnedBy(player))
            {
                _testingColumn.ExtractCardOwnedBy(player);
                DoneCardsCount++;
                return true;
            }

            if (_developmentColumn.HasUnblockedCardOwnedBy(player))
            {
                var card = _developmentColumn.ExtractCardOwnedBy(player);
                _testingColumn.AddCard(card);
                return true;
            }

            return false;
        }

        public bool TryUnblockCardOwnedBy(int player)
        {
            if (_testingColumn.HasBlockedCardOwnedBy(player))
            {
                _testingColumn.UnblockCardOwnedBy(player);
                return true;
            }
            
            if (_developmentColumn.HasBlockedCardOwnedBy(player))
            {
                _developmentColumn.UnblockCardOwnedBy(player);
                return true;
            }
            
            return false;
        }

        public bool TryTakeNewCardFor(int player)
        {
            _developmentColumn.AddCard(new Card(player));
            return true;
        }

        public bool TryBlockCardOwnedBy(int player)
        {
            if (_developmentColumn.HasUnblockedCardOwnedBy(player))
            {
                _developmentColumn.BlockCardOwnedBy(player);
                return true;
            }

            return false;
        }
    }
}