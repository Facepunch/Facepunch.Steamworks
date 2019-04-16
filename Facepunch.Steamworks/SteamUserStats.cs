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

		/// <summary>
		/// Tries to get the number of players currently playing this game.
		/// Or -1 if failed.
		/// </summary>
		public static async Task<int> PlayerCountAsync()
		{
			var result = await Internal.GetNumberOfCurrentPlayers();
			if ( !result.HasValue || result.Value.Success == 0 )
				return -1;

			return result.Value.CPlayers;
		}
	}
}