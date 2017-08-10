namespace PubSub.Solution
{
	public struct Level
	{
		private readonly string _value;

		public Level(string asString)
		{
			_value = asString;
		}

		public bool Matches(Level thatLevel)
		{
			return _value.Equals(Wildcard.SingleLevel) || _value.Equals(Wildcard.MultiLevel) || this._value.Equals(thatLevel._value);
		}
	}
}