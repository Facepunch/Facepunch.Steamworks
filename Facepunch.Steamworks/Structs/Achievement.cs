﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	public struct Achievement
	{
		internal string Value;

		public Achievement( string name )
		{
			Value = name;
		}

		public override string ToString() => Value;

		/// <summary>
		/// True if unlocked
		/// </summary>
		public bool State
		{
			get
			{
				var state = false;
				SteamUserStats.Internal.GetAchievement( Value, ref state );
				return state;
			}
		}

		public string Identifier => Value;

		public string Name => SteamUserStats.Internal.GetAchievementDisplayAttribute( Value, "name" );

		public string Description => SteamUserStats.Internal.GetAchievementDisplayAttribute( Value, "desc" );


		/// <summary>
		/// Should hold the unlock time if State is true
		/// </summary>
		public DateTime? UnlockTime
		{
			get
			{
				var state = false;
				uint time = 0;

				if ( !SteamUserStats.Internal.GetAchievementAndUnlockTime( Value, ref state, ref time ) || !state )
					return null;

				return Epoch.ToDateTime( time );
			}
		}

		/// <summary>
		/// Gets the icon of the achievement. This can return a null image even though the image exists if the image
		/// hasn't been downloaded by Steam yet. You can use GetIconAsync if you want to wait for the image to be downloaded.
		/// </summary>
		public Image? GetIcon()
		{
			return SteamUtils.GetImage( SteamUserStats.Internal.GetAchievementIcon( Value ) );
		}


		/// <summary>
		/// Gets the icon of the achievement, waits for it to load if we have to
		/// </summary>
		public async Task<Image?> GetIconAsync( int timeout = 5000 )
		{
			var i = SteamUserStats.Internal.GetAchievementIcon( Value );
			if ( i != 0 ) return SteamUtils.GetImage( i );

			var ident = Identifier;
			bool gotCallback = false;

			void f( string x, int icon )
			{
				if ( x != ident ) return;
				i = icon;
				gotCallback = true;
			}

			try
			{
				SteamUserStats.OnAchievementIconFetched += f;

				int waited = 0;
				while ( !gotCallback )
				{
					await Task.Delay( 10 );
					waited += 10;

					// Time out after x milliseconds
					if ( waited > timeout )
						return null;
				}

				if ( i == 0 ) return null;
				return SteamUtils.GetImage( i );
			}
			finally
			{
				SteamUserStats.OnAchievementIconFetched -= f;
			}
		}

		/// <summary>
		/// Returns the fraction (0-1) of users who have unlocked the specified achievement, or -1 if no data available.
		/// </summary>
		public float GlobalUnlocked
		{
			get
			{
				float pct = 0;

				if ( !SteamUserStats.Internal.GetAchievementAchievedPercent( Value, ref pct ) )
					return -1.0f;

				return pct / 100.0f;
			}
		}

		/// <summary>
		/// Make this achievement earned
		/// </summary>
		public bool Trigger( bool apply = true )
		{
			var r = SteamUserStats.Internal.SetAchievement( Value );

			if ( apply && r )
			{
				SteamUserStats.Internal.StoreStats();
			}

			return r;
		}

		/// <summary>
		/// Reset this achievement to not achieved
		/// </summary>
		public bool Clear()
		{
			return SteamUserStats.Internal.ClearAchievement( Value );
		}
	}
}