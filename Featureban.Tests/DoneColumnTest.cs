using Featureban.Domain;
using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests
{
	public class DoneColumnTest
	{
		[Fact]
		public void AddCard_AddsCard()
		{
			var column = Create.DoneColumn
				.Please();

			column.AddCard(Create.Card.Please());
			
			Assert.Equal(1, column.CardCount);
		}
	}
}