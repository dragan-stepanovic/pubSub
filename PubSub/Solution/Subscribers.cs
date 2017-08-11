using System;
using System.Collections.Generic;
using System.Linq;

namespace PubSub.Solution
{
	internal class Subscribers<T>
	{
		//todo: List<Subscriber<T>>?
		private readonly Dictionary<SubscriptionTopic, Action<string, T>> _subscribers;

		private Subscribers(Dictionary<SubscriptionTopic, Action<string, T>> subscribers)
		{
			_subscribers = subscribers;
		}

		public static Subscribers<T> Empty()
		{
			return new Subscribers<T>(new Dictionary<SubscriptionTopic, Action<string, T>>());
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
			//todo: I would also change the delegate to accept SubscriptionTopic rather than generic built in string type, which doesn't distinguish between SubscriptionTopic and PublishingTopic
			_subscribers
				.Values
				.ToList()
				.ForEach(callback => callback.Invoke(publishingTopic.AsString(), message));
		}
	}
}