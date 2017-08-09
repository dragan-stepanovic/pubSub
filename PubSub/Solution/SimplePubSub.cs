using System;
using System.Collections.Generic;

namespace PubSub.Solution
{
	/// <summary>
	/// Implement your solution here!
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="IPubSub{T}" />
	public class SimplePubSub<T> : IPubSub<T>
	{
		private readonly Dictionary<string, Action<string, T>> _subscribersOld = new Dictionary<string, Action<string, T>>();
		private readonly Subscriptions<T> _subscriptions = new Subscriptions<T>();

		/// <summary>
		/// Subscribes the specified topic.
		/// </summary>
		/// <param name="topic">The topic.</param>
		/// <param name="callback">The event.</param>
		/// <exception cref="InvalidTopicException">Topic is invalid.</exception>
		public void Subscribe(string topic, Action<string, T> callback)
		{
			_subscribersOld.Add(topic, callback);
			_subscriptions.Add(topic, callback);
		}

		/// <summary>
		/// Publishes the message to specified topic.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="topic">The topic.</param>
		/// <exception cref="InvalidTopicException"></exception>
		public void Publish(string topic, T message)
		{
			var aTopic = PublishingTopic.From(topic);
			
			if (_subscribersOld.ContainsKey(topic))
				_subscribersOld[topic](topic, message);
		}
	}

	internal class Subscriptions<T>
	{
		private readonly Dictionary<SubscriptionTopic, Action<string, T>> _subscriptions = new Dictionary<SubscriptionTopic, Action<string, T>>();

		public void Add(string subscriptionAsString, Action<string, T> callback)
		{
			_subscriptions.Add(SubscriptionTopic.From(subscriptionAsString), callback);
		}
	}
}