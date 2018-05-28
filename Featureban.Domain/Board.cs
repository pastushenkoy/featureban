namespace Featureban.Domain
{
    internal class Board
    {
	    private readonly TodoColumn _todoColumn;
	    
        protected readonly InProgressColumn _developmentColumn;
        protected readonly InProgressColumn _testingColumn;

	    private readonly DoneColumn _doneColumn;

	    public int DoneCardsCount => _doneColumn.CardCount;
        
        public Board(int developmentWipLimit, int testingWipLimit)
        {
	        _todoColumn = new TodoColumn();
	        _developmentColumn = new InProgressColumn(developmentWipLimit);
	        _testingColumn = new InProgressColumn(testingWipLimit);
	        _doneColumn = new DoneColumn();
        }
        
        public bool TryMoveCardOwnedBy(int player)
        {
            if (_testingColumn.HasUnblockedCardOwnedBy(player))
            {
                var card = _testingColumn.ExtractCardOwnedBy(player);
                _doneColumn.AddCard(card);
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
	    
	    public void MoveNearestCard()
	    {
		    var card=_testingColumn.ExtractNonBlockedCard();
		    if (card != null)
		    {
			    _doneColumn.AddCard(card);
			    return;
		    }

		    card = _testingColumn.ExtractBlockedCard();
		    _testingColumn.UnlockCard(card);
		    
		    if (_testingColumn.HasPlaceForCard())
		    {
			    card = _developmentColumn.ExtractNonBlockedCard();
			    if (card != null)
			    {
				    _testingColumn.AddCard(card);
			    }
		    }
		    else
		    {
			    card = _developmentColumn.ExtractBlockedCard();
			    if (card != null)
			    {
				    _developmentColumn.UnlockCard(card);
			    }
		    }
	    }
    }
}