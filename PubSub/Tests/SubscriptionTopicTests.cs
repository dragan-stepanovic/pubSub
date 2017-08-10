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
		[InlineData("/home/bedroom/temperature", "/home/bedroom/temperature", true)]
		[InlineData("/home/bedroom/+", "/home/bedroom/temperature", true)]
		[InlineData("/home/bedroom/#", "/home/bedroom/somethingElse", true)]
		[InlineData("/home/bedroom/#", "/home/bedroom/temperature/somethingElse", true)]
		[InlineData("/home/bedroom/+/somethingElse", "/home/bedroom/temperature/somethingElse", true)]

		[InlineData("/home/bedroom/+", "/home/humidity/somethingElse", false)]
		[InlineData("/home/bedroom/+/somethingElse", "/home/bedroom/temperature/kitchen", false)]
		[InlineData("/home/bedroom/#", "/home/humidity/somethingElse", false)]
		[InlineData("/home/+/temperature/+", "/office/garage/temperature/celsius", false)]
		//todo: change to member data and use SubscriptionTopic and PublishingTopic
		public void MatchesPublishingTopic(string subscriptionAsString, string publishingAsString, bool isMatched)
		{
			SubscriptionTopic.From(subscriptionAsString)
				.Matches(PublishingTopic.From(publishingAsString))
				.Should()
				.Be(isMatched);
		}
	}
}