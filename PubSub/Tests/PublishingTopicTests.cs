using PubSub.Solution;
using Xunit;

namespace PubSub.Tests
{
	public class PublishingTopicTests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("/")]
		[InlineData("invalid")]
		[InlineData("invalid/")]
		[InlineData("/home/")]
		[InlineData("//home")]
		[InlineData("/home/#")]
		[InlineData("/home/kit chen")]
		public void CannotCreateInvalidTopic(string topicAsString)
		{
			var ex = Assert.Throws<InvalidTopicException>(() => PublishingTopic.From(topicAsString));
			Assert.NotNull(ex);
		}

		[Theory]
		[InlineData("/home/123")]
		[InlineData("/home/kitchen/oW3n")]
		public void CanCreateTopicFromValidString(string topicAsString)
		{
			PublishingTopic.From(topicAsString);
		}
	}
}