using System.Linq;
using System.Text.RegularExpressions;

namespace PubSub.Solution
{
	public class PublishingTopic
	{
		private readonly string _value;

		private PublishingTopic(string value)
		{
			_value = value;
		}

		public static PublishingTopic From(string topicAsString)
		{
			if (string.IsNullOrWhiteSpace(topicAsString) || !topicAsString.StartsWith("/") || topicAsString.Contains("//") || topicAsString.EndsWith("/"))
				throw new InvalidTopicException(topicAsString);

			var alphaNumeric = new Regex("^[a-zA-Z0-9]*$");
			var levels = topicAsString.Split('/');

			if (levels.Any(level => !alphaNumeric.IsMatch(level)))
				throw new InvalidTopicException(topicAsString);

			return new PublishingTopic(topicAsString);
		}
	}
}