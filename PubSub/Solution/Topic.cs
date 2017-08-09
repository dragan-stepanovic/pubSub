using System.Linq;
using System.Text.RegularExpressions;

namespace PubSub.Solution
{
	public class Topic
	{
		private readonly string _value;

		public Topic(string value)
		{
			if (string.IsNullOrWhiteSpace(value) || !value.StartsWith("/") || value.Contains("//") || value.EndsWith("/") )
				throw new InvalidTopicException(value);

			var alphaNumeric = new Regex("^[a-zA-Z0-9]*$");
			var levels = value.Split('/');

			if (levels.Any(level => !alphaNumeric.IsMatch(level)))
				throw new InvalidTopicException(value);

			_value = value;
		}

		public static implicit operator string(Topic topic)
		{
			return topic._value;
		}
	}
}