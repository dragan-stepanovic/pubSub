namespace PubSub.Solution
{
	public struct Wildcard
	{
		public static Level SingleLevel => new Level("+");
		public static Level MultiLevel => new Level("#");
	}
}