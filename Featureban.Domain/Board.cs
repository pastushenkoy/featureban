namespace Featureban.Domain
{
    internal class Board
    {
        protected InProgressColumn _developmentColumn;
        protected InProgressColumn _testingColumn;
        protected DoneColumn _doneColumn;

        public Board()
        {
            _developmentColumn = new InProgressColumn();
            _testingColumn = new InProgressColumn();
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
            throw new System.NotImplementedException();
        }

        public bool TryTakeNewCardFor(int player)
        {
            throw new System.NotImplementedException();
        }

        public void BlockCardOwnedByPlayer(int player)
        {
            throw new System.NotImplementedException();
        }
    }
}