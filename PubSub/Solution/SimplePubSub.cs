using System;

namespace PubSub.Solution
{
	/// <summary>
	/// Implement your solution here!
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="IPubSub{T}" />
	public class SimplePubSub<T> : IPubSub<T>
	{
		private readonly Subscribers<T> _subscribers = Subscribers<T>.Empty();

		/// <summary>
		/// Subscribes the specified topic.
		/// </summary>
		/// <param name="topicAsString">The topic.</param>
		/// <param name="callback">The event.</param>
		/// <exception cref="InvalidTopicException">Topic is invalid.</exception>
		public void Subscribe(string topicAsString, Action<string, T> callback)
		{
			_subscribers.Add(SubscriptionTopic.From(topicAsString), callback);
		}

		/// <summary>
		/// Publishes the message to specified topic.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="topicAsString">The topic.</param>
		/// <exception cref="InvalidTopicException"></exception>
		public void Publish(string topicAsString, T message)
		{
			//note: I'd rather pass strongly typed PublishingTopic as the parameter, but in order to not change the original tests, I did the conversion here; same goes for Subscribe method
			var publishingTopic = PublishingTopic.From(topicAsString);
			_subscribers
				.Matching(publishingTopic)
				.InvokeCallbacksFor(publishingTopic, message);
		}
	}
}