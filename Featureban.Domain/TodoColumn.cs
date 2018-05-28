namespace Featureban.Domain
{
	internal class TodoColumn
	{
		public Card ExtractCardFor(int player)
		{
			return new Card(player);
		}
	}
}