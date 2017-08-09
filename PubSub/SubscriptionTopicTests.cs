using PubSub.Solution;
using Xunit;

namespace PubSub
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
	}
}