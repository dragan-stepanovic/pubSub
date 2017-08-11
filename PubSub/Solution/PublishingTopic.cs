using System.Collections.Generic;
using System.Linq;

namespace PubSub.Solution
{
	public class PublishingTopic
	{
		private readonly Levels _levels;

		private PublishingTopic(Levels levels)
		{
			_levels = levels;
		}

		public static PublishingTopic From(string topicAsString)
		{
			if (TopicNotValid(topicAsString))
				throw new InvalidTopicException(topicAsString);

			var levels = Levels.From(topicAsString);
			if (AnyLevelNotValid(levels))
				throw new InvalidTopicException(topicAsString); //note: I'd rather throw more specific exception in this case, e.g. InvalidLevelException

			return new PublishingTopic(levels);
		}

		private static bool TopicNotValid(string topicAsString)
		{
			return string.IsNullOrWhiteSpace(topicAsString)
				|| !topicAsString.StartsWith(Level.Separator)
				|| topicAsString.Contains("//")
				|| topicAsString.EndsWith(Level.Separator);
		}

		private static bool AnyLevelNotValid(IEnumerable<Level> levels)
		{
			return levels.Any(level => !level.IsAlphaNumeric());
		}

		public Levels AsLevels()
		{
			return _levels;
		}
		
		public string AsString()
		{
			return _levels.AsString();
		}
	}
}