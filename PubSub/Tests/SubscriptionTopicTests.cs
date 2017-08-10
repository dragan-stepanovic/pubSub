using FluentAssertions;
using PubSub.Solution;
using Xunit;

namespace PubSub.Tests
{
	public class SubscriptionTopicTests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("invalid")]
		[InlineData("invalid/")]
		[InlineData("/home/")]
		[InlineData("//home")]
		[InlineData("/home/##")]
		[InlineData("/home/temp+")]
		[InlineData("/home/temp#")]
		[InlineData("/+/##")]
		public void CannotCreateInvalidSubscription(string subscriptionAsString)
		{
			var ex = Assert.Throws<InvalidTopicException>(() => SubscriptionTopic.From(subscriptionAsString));
			Assert.NotNull(ex);
		}

		[Theory]
		[InlineData("/home/#")]
		[InlineData("/home/+/temperature")]
		public void CanCreateSubscriptionFromValidString(string subscriptionAsString)
		{
			SubscriptionTopic.From(subscriptionAsString);
		}


		[Theory]
		[InlineData("/home/bedroom/+", "/home/bedroom/temperature", true)]
		[InlineData("/home/bedroom/+", "/home/bedroom/humidity", true)]
		//todo: change to member data and use SubscriptionTopic and PublishingTopic
		public void MatchesPublishingTopic(string subscriptionAsString, string publishingAsString , bool isMatched)
		{
			SubscriptionTopic.From(subscriptionAsString)
				.Matches(PublishingTopic.From(publishingAsString))
				.Should()
				.Be(isMatched);
		}
	}
}