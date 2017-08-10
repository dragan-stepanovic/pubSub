using FluentAssertions;
using PubSub.Solution;
using Xunit;

namespace PubSub.Tests
{
	public class LevelTests
	{
		//todo: Member data with two test cases suites MatchesExactlySameLevel and MatchesWildcards
		[Theory]
		[InlineData("kitchen", "kitchen", true)]
		[InlineData("kitchen", "livingRoom", false)]
		public void MatchesExactlySameLevel(string thisLevelAsString, string thatLevelAsString, bool isMatching)
		{
			//todo: named constructor
			var thisLevel = new Level(thisLevelAsString);
			var thatLevel = new Level(thatLevelAsString);
			thisLevel.Matches(thatLevel).Should().Be(isMatching);
		}

		//note: I'd probably use AutoFixture to generate random (anonymous) data like the second level;
		//this communicates the intent of the test better by saying that the second parameter string is actually not important in terms of business logic
		[Theory]
		[InlineData("+", "temperature", true)]
		[InlineData("#", "kitchen", true)]
		public void MatchesWildcards(string wildcardAsString, string thatLevelAsString, bool isMatching)
		{
			var wildcard = new Level(wildcardAsString);
			var thatLevel = new Level(thatLevelAsString);
			wildcard.Matches(thatLevel).Should().Be(isMatching);
		}

		//todo: move validation from Topics to levels (as to the type of characters that it accepts)
	}
}