namespace Featureban.Domain
{
    internal class Board
    {
	    private readonly TodoColumn _todoColumn;
	    
        protected readonly InProgressColumn _developmentColumn;
        protected readonly InProgressColumn _testingColumn;

        public int DoneCardsCount { get; private set; }
        
        public Board(int developmentWipLimit, int testingWipLimit)
        {
	        _todoColumn = new TodoColumn();
	        _developmentColumn = new InProgressColumn(developmentWipLimit);
	        _testingColumn = new InProgressColumn(testingWipLimit);
        }
        
        public bool TryMoveCardOwnedBy(int player)
        {
            if (_testingColumn.HasUnblockedCardOwnedBy(player))
            {
                _testingColumn.ExtractCardOwnedBy(player);
                DoneCardsCount++;
                return true;
            }

            if (_developmentColumn.HasUnblockedCardOwnedBy(player) && _testingColumn.HasPlaceForCard())
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
            if (_developmentColumn.HasPlaceForCard())
            {
	            var card = _todoColumn.ExtractCardFor(player);
                _developmentColumn.AddCard(card);
                return true;
            }

            return false;
        }

        public bool TryBlockCardOwnedBy(int player)
        {
            if (_testingColumn.HasUnblockedCardOwnedBy(player))
            {
                _testingColumn.BlockCardOwnedBy(player);
                return true;
            }

            if (_developmentColumn.HasUnblockedCardOwnedBy(player))
            {
                _developmentColumn.BlockCardOwnedBy(player);
                return true;
            }

            return false;
        }
    }
}