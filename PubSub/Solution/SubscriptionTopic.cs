using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PubSub.Solution
{
	public class SubscriptionTopic
	{
		private readonly IEnumerable<Level> _levels;

		private SubscriptionTopic(IEnumerable<Level> levels)
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

			var levels = subscriptionAsString.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

			//todo: move validation to Level and change the thrown exception to InvalidLevelException
			var alphaNumeric = new Regex("^[a-zA-Z0-9]*$");
			if (levels.Any(level => !alphaNumeric.IsMatch(level) && !level.Equals("#") && !level.Equals("+")))
				throw new InvalidTopicException(subscriptionAsString);

			return new SubscriptionTopic(levels.Select(level => new Level(level)));
		}

		public bool Matches(PublishingTopic publishingTopic)
		{
			if (_levels.ToList().Count > publishingTopic.AsLevels().ToList().Count)
				return false;

			return _levels
					.Zip(publishingTopic.AsLevels(), (first, second) => first.Equals(second))
					.All(isMatching => isMatching);
		}
	}
}