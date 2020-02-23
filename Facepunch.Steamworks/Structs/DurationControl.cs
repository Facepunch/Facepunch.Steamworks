using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks.Data
{
	/// <summary>
	/// Sent for games with enabled anti indulgence / duration control, for enabled users. 
	/// Lets the game know whether persistent rewards or XP should be granted at normal rate, half rate, or zero rate.
	/// </summary>
	public struct DurationControl
	{
		internal DurationControl_t _inner;

		/// <summary>
		/// appid generating playtime
		/// </summary>
		public AppId Appid => _inner.Appid;

		/// <summary>
		/// is duration control applicable to user + game combination
		/// </summary>
		public bool Applicable => _inner.Applicable;

		/// <summary>
		/// playtime since most recent 5 hour gap in playtime, only counting up to regulatory limit of playtime, in seconds
		/// </summary>
		internal TimeSpan PlaytimeInLastFiveHours => TimeSpan.FromSeconds( _inner.CsecsLast5h );

		/// <summary>
		/// playtime on current calendar day
		/// </summary>
		internal TimeSpan PlaytimeToday => TimeSpan.FromSeconds( _inner.CsecsLast5h );

		/// <summary>
		/// recommended progress
		/// </summary>
		internal DurationControlProgress Progress => _inner.Progress;
	}
}