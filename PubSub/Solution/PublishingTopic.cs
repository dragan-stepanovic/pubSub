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
			if (string.IsNullOrWhiteSpace(topicAsString)
				|| !topicAsString.StartsWith("/")
				|| topicAsString.Contains("//")
				|| topicAsString.EndsWith("/"))
				throw new InvalidTopicException(topicAsString);

			var levels = topicAsString.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

			var alphaNumeric = new Regex("^[a-zA-Z0-9]*$");
			if (levels.Any(level => !alphaNumeric.IsMatch(level)))
				throw new InvalidTopicException(topicAsString);

			return new PublishingTopic(levels.Select(asString => new Level(asString)));
		}

		public IEnumerable<Level> AsLevels()
		{
			return _levels;
		}

		public string AsString()
		{
			const string separator = "/";
			return separator + string.Join(separator, _levels);
		}
	}
}