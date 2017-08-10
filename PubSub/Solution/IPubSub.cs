using System;

namespace PubSub.Solution
{
	/// <summary>
	/// PubSub Server
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IPubSub<T>
	{
		/// <summary>
		/// Subscribes to the specified topic.
		/// </summary>
		/// <param name="topicAsString">The topic.</param>
		/// <param name="callback">The callback.</param>
		void Subscribe(string topicAsString, Action<string, T> callback);

		/// <summary>
		/// Publishes the message to specified topic.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="topicAsString">The topic.</param>
		void Publish(string topicAsString, T message);
	}
}