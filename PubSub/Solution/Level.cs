using System.Text.RegularExpressions;

namespace PubSub.Solution
{
	public struct Level
	{
		public const string Separator = "/";
		private readonly string _value;

		public Level(string asString)
		{
			_value = asString;
		}

		public bool Matches(Level thatLevel)
		{
			return this.Equals(Wildcard.SingleLevel) || this.Equals(Wildcard.MultiLevel) || this.Equals(thatLevel);
		}

		public bool IsAlphaNumeric()
		{
			var alphaNumeric = new Regex("^[a-zA-Z0-9]*$");
			return alphaNumeric.IsMatch(_value);
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