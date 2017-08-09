namespace PubSub.Solution
{
	public class Topic
	{
		private readonly string _value;

		public Topic(string value)
		{
			_value = value;
		}

		public static implicit operator string(Topic topic)
		{
			return topic._value;
		}
	}
}