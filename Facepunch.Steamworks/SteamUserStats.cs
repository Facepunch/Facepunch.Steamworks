using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	public static class SteamUserStats
	{
		static ISteamUserStats _internal;
		internal static ISteamUserStats Internal
		{
			get
			{
				if ( _internal == null )
					_internal = new ISteamUserStats();

				return _internal;
			}
		}

		/// <summary>
		/// Get the available achievements
		/// </summary>
		public static IEnumerable<Achievement> Achievements
		{
			get
			{
				for( int i=0; i< Internal.GetNumAchievements(); i++  )
				{
					yield return new Achievement( Internal.GetAchievementName( (uint) i ) );
				}
			}
		}
	}
}