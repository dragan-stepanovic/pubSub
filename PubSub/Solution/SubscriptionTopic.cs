using System.Collections.Generic;
using System.Linq;

namespace PubSub.Solution
{
	public class SubscriptionTopic
	{
		private readonly Levels _levels;

		private SubscriptionTopic(Levels levels)
		{
			_levels = levels;
		}

		public static SubscriptionTopic From(string topicAsString)
		{
			if (TopicNotValid(topicAsString))
				throw new InvalidTopicException(topicAsString);

			var levels = Levels.From(topicAsString);
			if (AnyLevelNotValid(levels))
				throw new InvalidTopicException(topicAsString); //note: I'd rather throw more specific exception in this case, e.g. InvalidLevelException, but don't want to change the initial tests

			return new SubscriptionTopic(levels);
		}

		//note: there's duplication in terms of topic validation in SubscriptionTopic and PublishingTopic classes, but I'd rather have small duplication in code than false abstraction
		private static bool TopicNotValid(string topicAsString)
		{
			return string.IsNullOrWhiteSpace(topicAsString)
				   || !topicAsString.StartsWith(Level.Separator)
				   || topicAsString.Contains("//")
				   || topicAsString.EndsWith(Level.Separator);
		}

		private static bool AnyLevelNotValid(IEnumerable<Level> levels)
		{
			return levels.Any(level => !level.IsAlphaNumeric() && !level.Equals(Wildcard.MultiLevel) && !level.Equals(Wildcard.SingleLevel));
		}

		public bool Matches(PublishingTopic publishingTopic)
		{
			if (_levels.HasMoreElementsThan(publishingTopic.AsLevels()))
				return false;

			return _levels
					.Zip(publishingTopic.AsLevels(), (first, second) => first.Matches(second))
					.All(isMatching => isMatching);
		}
	}
}