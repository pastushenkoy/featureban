using Featureban.Domain;
using Featureban.Tests.DSL;
using Featureban.Tests.DSL.Helpers;
using Xunit;

namespace Featureban.Tests
{
    public class GameTests
    {
        [Fact]
        public void GameGeneratesResults()
        {
            var game = Create.Game
                .WithAlwaysWinCoin()
                .WithPlayerCount(2)
                .Please();

            game.NextDay();
            
            game.AssertAllCoinResultsAreTrue();
        }

        [Fact]
        public void WhenPlayerWins_HePerformsWinMove()
        {
            const int player = 0;
            var game = Create.Game
                .WithAlwaysWinCoin()
                .WithPlayerCount(1)
                .Please();

            game.NextDay();

            game.AssertWinMoveWasCalledBy(player);
        }

        [Fact]
        public void WhenPlayerLooses_HePerformsLooseMove()
        {
            const int player = 0;
            var game = Create.Game
                .WithAlwaysLooseCoin()
                .WithPlayerCount(1)
                .Please();

            game.NextDay();

            game.AssertLooseMoveWasCalledBy(player);
        }

        [Fact]
        public void WhenPlayerHasNothingToDo_HeGivesHisWinToAnotherPlayer()
        {
            const int secondPlayer = 1;
            var game = Create.Game
                .WithAlwaysWinCoin()
                .WithPlayerCount(2)
                .WithNoOpportunityForWinMove()
                .Please();
            
            game.NextDay();
            
            game.AssertWinMoveWasCalledTwiceBy(secondPlayer);
        }

        [Fact]
        public void WhenPlayerWinsAndMovesCard_HeDoesntDoAnythingElse()
        {
            const int player = 0;
            var game = Create.Game
                .WithAlwaysWinCoin()
                .WithOpportunityToMoveCard()
                .WithPlayerCount(1)
                .Please();

            game.NextDay();

            game.AssertPlayerOnlyMovesCard(player);
        }
        
        [Fact]
        public void WhenPlayerWinsAndUnblocksCard_HeDoesntDoAnythingElse()
        {
            const int player = 0;
            var game = Create.Game
                .WithAlwaysWinCoin()
                .WithOpportunityToUnblockCard()
                .WithPlayerCount(1)
                .Please();

            game.NextDay();

            game.AssertPlayerOnlyUnblocksCard(player);
        }

        [Fact]
        public void WhenPlayerWinsAndCannotMoveOrUnblockCard_HeTakesOneMoreCard()
        {
            const int player = 0;
            var game = Create.Game
                .WithAlwaysWinCoin()
                .WithOpportunityToTakeOneMoreCard()
                .WithPlayerCount(1)
                .Please();

            game.NextDay();

            game.AssertPlayerTakesOneMoreCardOnWinMove(player);
        }
    }
}