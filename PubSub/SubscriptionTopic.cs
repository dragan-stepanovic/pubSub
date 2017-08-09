using System;
using System.Linq;
using System.Text.RegularExpressions;
using PubSub.Solution;

namespace PubSub
{
	public class SubscriptionTopic
	{
		public static SubscriptionTopic From(string subscriptionAsString)
		{
			if (string.IsNullOrWhiteSpace(subscriptionAsString) || !subscriptionAsString.StartsWith("/") || subscriptionAsString.Contains("//") || subscriptionAsString.EndsWith("/"))
				throw new InvalidTopicException(subscriptionAsString);

			var alphaNumeric = new Regex("^[a-zA-Z0-9]*$");
			var levels = subscriptionAsString.Split(new[]{'/'}, StringSplitOptions.RemoveEmptyEntries);

			if (levels.Any(level => !alphaNumeric.IsMatch(level) && !level.Equals("#") && !level.Equals("+")))
				throw new InvalidTopicException(subscriptionAsString);

			return new SubscriptionTopic();
		}
	}
}