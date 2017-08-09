using System.Linq;
using System.Text.RegularExpressions;
using PubSub.Solution;
using Xunit;

namespace PubSub
{
	public class SubscriptionTests
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
			var ex = Assert.Throws<InvalidTopicException>(() => Subscription.From(subscriptionAsString));
			Assert.NotNull(ex);
		}

		[Theory]
		[InlineData("/home/#")]
		[InlineData("/home/+/temperature")]
		public void CanCreateSubscriptionFromValidString(string subscriptionAsString)
		{
			Subscription.From(subscriptionAsString);
		}
	}

	public class Subscription
	{
		public static Subscription From(string subscriptionAsString)
		{
			if (string.IsNullOrWhiteSpace(subscriptionAsString) || !subscriptionAsString.StartsWith("/") || subscriptionAsString.Contains("//") || subscriptionAsString.EndsWith("/"))
				throw new InvalidTopicException(subscriptionAsString);

			var alphaNumeric = new Regex("^[a-zA-Z0-9]*$");
			var levels = subscriptionAsString.Split('/');

			if (levels.Any(level => !alphaNumeric.IsMatch(level)))
				throw new InvalidTopicException(subscriptionAsString);

			return new Subscription();
		}
	}
}