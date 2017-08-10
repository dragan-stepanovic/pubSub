using System;
using System.Collections.Generic;
using System.Linq;

namespace PubSub.Solution
{
	internal class Subscribers<T>
	{
		private readonly Dictionary<SubscriptionTopic, Action<string, T>> _subscribers;

		private Subscribers(Dictionary<SubscriptionTopic, Action<string, T>> subscribers)
		{
			_subscribers = subscribers;
		}

		public void Add(SubscriptionTopic topic, Action<string, T> callback)
		{
			_subscribers.Add(topic, callback);
		}

		public Subscribers<T> Matching(PublishingTopic publishingTopic)
		{
			return new Subscribers<T>(_subscribers
				.Where(sub => sub.Key.Matches(publishingTopic))
				.ToDictionary(x => x.Key, x => x.Value));
		}

		public void InvokeCallbacksFor(PublishingTopic publishingTopic, T message)
		{
			_subscribers
				.Values
				.ToList()
				.ForEach(callback => callback.Invoke(publishingTopic, message));
		}

		public static Subscribers<T> Empty()
		{
			return new Subscribers<T>(new Dictionary<SubscriptionTopic, Action<string, T>>());
		}
	}
}