namespace Featureban.Domain
{
	internal class DoneColumn
	{
		public int CardCount { get; private set; }

		public void AddCard(Card card)
		{
			CardCount++;
		}
	}
}