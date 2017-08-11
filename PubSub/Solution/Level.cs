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
			return _value.Equals(Wildcard.SingleLevel) || _value.Equals(Wildcard.MultiLevel) || this.Equals(thatLevel);
		}

		private bool Equals(Level thatLevel)
		{
			return this._value.Equals(thatLevel._value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Level && Equals((Level)obj);
		}

		public override int GetHashCode()
		{
			return _value != null ? _value.GetHashCode() : 0;
		}

		public override string ToString()
		{
			return $"{_value}";
		}
	}
}