namespace Steamworks.Data
{
	public enum LeaderboardDisplay : int
	{
		/// <summary>
		/// The score is just a simple numerical value
		/// </summary>
		Numeric = 1,

		/// <summary>
		/// The score represents a time, in seconds
		/// </summary>
		TimeSeconds = 2,

		/// <summary>
		/// The score represents a time, in milliseconds
		/// </summary>
		TimeMilliSeconds = 3,
	}
}