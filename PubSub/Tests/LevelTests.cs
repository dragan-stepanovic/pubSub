using System.Collections.Generic;
using FluentAssertions;
using PubSub.Solution;
using Xunit;

namespace PubSub.Tests
{
	public class LevelTests
	{
		//note: I'd probably use AutoFixture to generate random (anonymous) data like the second level;
		//this communicates the intent of the test better by saying that the second parameter string is actually not important in terms of business logic
		[Theory]
		[MemberData(nameof(MatchesExactlySameLevel))]
		[MemberData(nameof(MatchesWildcards))]
		public void CanMatchAnotherLevel(Level thisLevel, Level thatLevel, bool isMatching)
		{
			thisLevel.Matches(thatLevel).Should().Be(isMatching);
		}

		private static IEnumerable<object[]> MatchesExactlySameLevel()
		{
			yield return new object[]
			{
				new Level("kitchen"), new Level("kitchen"), true
			};
			yield return new object[]
			{
				new Level("kitchen"), new Level("livingRoom"), false
			};
		}

		private static IEnumerable<object[]> MatchesWildcards()
		{
			yield return new object[]
			{
				Wildcard.SingleLevel, new Level("temperature"), true
			};
			yield return new object[]
			{
				Wildcard.MultiLevel, new Level("kitchen"), true
			};
		}
	}
}