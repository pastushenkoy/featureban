namespace Featureban.Domain
{
    internal class Card
    {
        public bool Blocked { get; private set; }
        public int Player { get; }

        public Card(int player)
        {
            Player = player;
        }

        public void Block()
        {
            Blocked = true;
        }

        public void Unblock()
        {
            Blocked = false;
        }

        public override bool Equals(object o)
        {
            if (o is null || o.GetType() != GetType())
                return false;

            return Equals((Card) o);
        }

        private bool Equals(Card anotherCard)
        {
            return Player == anotherCard.Player
                   && Blocked == anotherCard.Blocked;
        }
    }
}