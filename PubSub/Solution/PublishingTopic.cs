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

		//note: this is needed only because of weak typing publishing topic in the subscriber (string) currently used.
		//I didn't wan't to change the type beacuse of initial tests, but with strong typing, we would removed this method as well
		public string AsString()
		{
			return _levels.AsString();
		}
	}
}