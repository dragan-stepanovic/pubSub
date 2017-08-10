using System;
using System.Collections.Generic;

namespace PubSub
{
	internal class Recorder<T>
	{
		//note: not sure that having topic in publishing info (if it represents the publishing topic) that is sent to subscribers would be wise.
		//I'm not sure that this info is needed in subscriber, but more to that is that it will couple the subscriber to publisher.
		//Why would subscriber need to know the wildcard topic that the message was published to?
		public Action<string, T> Handler => (topic, message) => Messages.Add(Tuple.Create(topic, message));

		public List<Tuple<string, T>> Messages { get; } = new List<Tuple<string, T>>();
	}
}