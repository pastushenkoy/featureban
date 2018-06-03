using Featureban.Tests.DSL;
using Xunit;

namespace Featureban.Tests
{
	
	public class TodoColumnTests
	{
		[Fact]
		public void ExtractCard_Success()
		{
			var player = 1;
			var column = Create
				.ToDoColumn
				.Please();

			var card = column.ExtractCardFor(player);
			
			Assert.NotNull(card);
		}
	}
}