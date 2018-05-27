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
    }
}