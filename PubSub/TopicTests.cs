using PubSub.Solution;
using Xunit;

namespace PubSub
{
	public class TopicTests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("invalid")]
		[InlineData("invalid/")]
		[InlineData("/home/")]
		[InlineData("//home")]
		[InlineData("/home/#")]
		public void CannotCreateInvalidTopic(string topicAsString)
		{
			var ex = Assert.Throws<InvalidTopicException>(() => new Topic(topicAsString));
			Assert.NotNull(ex);
		}

		[Theory]
		[InlineData("/home/123")]
		public void CanCreateTopicFromValidString(string topicAsString)
		{
			new Topic(topicAsString);
		}
	}
}