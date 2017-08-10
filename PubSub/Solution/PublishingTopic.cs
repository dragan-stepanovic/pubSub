using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PubSub.Solution
{
	public class PublishingTopic
	{
		private readonly IEnumerable<Level> _levels;

		private PublishingTopic(IEnumerable<Level> levels)
		{
			_levels = levels;
		}

		//todo: refactor towards better naming
		public static PublishingTopic From(string topicAsString)
		{
			if (string.IsNullOrWhiteSpace(topicAsString) || !topicAsString.StartsWith("/") || topicAsString.Contains("//") || topicAsString.EndsWith("/"))
				throw new InvalidTopicException(topicAsString);

			var alphaNumeric = new Regex("^[a-zA-Z0-9]*$");
			var levels = topicAsString.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

			if (levels.Any(level => !alphaNumeric.IsMatch(level)))
				throw new InvalidTopicException(topicAsString);

			return new PublishingTopic(levels.Select(asString => new Level(asString)));
		}

		public static implicit operator string(PublishingTopic topic)
		{
			const string separator = "/";
			return separator + string.Join(separator, topic._levels);
		}

		public IEnumerable<Level> AsLevels()
		{
			return _levels;
		}
	}
}