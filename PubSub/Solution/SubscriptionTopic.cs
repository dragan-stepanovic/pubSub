using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PubSub.Solution
{
	public class SubscriptionTopic
	{
		private readonly IEnumerable<string> _levels;

		private SubscriptionTopic(IEnumerable<string> levels)
		{
			_levels = levels;
		}

		//todo: refactor towards better naming
		public static SubscriptionTopic From(string subscriptionAsString)
		{
			if (string.IsNullOrWhiteSpace(subscriptionAsString)
				|| !subscriptionAsString.StartsWith("/")
				|| subscriptionAsString.Contains("//")
				|| subscriptionAsString.EndsWith("/"))
				throw new InvalidTopicException(subscriptionAsString);

			var alphaNumeric = new Regex("^[a-zA-Z0-9]*$");
			var levels = subscriptionAsString.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();

			if (levels.Any(level => !alphaNumeric.IsMatch(level) && !level.Equals("#") && !level.Equals("+")))
				throw new InvalidTopicException(subscriptionAsString);

			return new SubscriptionTopic(levels);
		}

		public bool Matches(PublishingTopic publishingTopic)
		{
			return _levels.SequenceEqual(publishingTopic.AsLevels());
		}
	}
}