using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PubSub.Solution
{
	public class Levels : IEnumerable<Level>
	{
		private readonly IEnumerable<Level> _values;

		private Levels(string topicAsString)
		{
			_values = topicAsString
				.Split(new[] { Level.Separator }, StringSplitOptions.RemoveEmptyEntries)
				.Select(level => new Level(level));
		}

		public static Levels From(string topicAsString)
		{
			return new Levels(topicAsString);
		}

		public bool HasMoreElementsThan(Levels thatLevels)
		{
			return this._values.ToList().Count > thatLevels._values.ToList().Count;
		}

		public string AsString()
		{
			return Level.Separator + string.Join(Level.Separator, _values);
		}

		public IEnumerator<Level> GetEnumerator()
		{
			return _values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}