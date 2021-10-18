using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	public struct Stat
	{
		public string Name { get; internal set; }
		public SteamId UserId { get; internal set; }

		public Stat( string name )
		{
			Name = name;
			UserId = 0;
		}

		public Stat( string name, SteamId user )
		{
			Name = name;
			UserId = user;
		}

		internal void LocalUserOnly( [CallerMemberName] string caller = null )
		{
			if ( UserId == 0 ) return;
			throw new System.Exception( $"Stat.{caller} can only be called for the local user" );
		}

		public double GetGlobalFloat()
		{
			double val = 0.0;

			if ( SteamUserStats.Internal.GetGlobalStat( Name, ref val ) )
				return val;

			return 0;
		}

		public long GetGlobalInt()
		{
			long val = 0;
			SteamUserStats.Internal.GetGlobalStat( Name, ref val );
			return val;
		}

		public async Task<long[]> GetGlobalIntDaysAsync( int days )
		{
			var result = await SteamUserStats.Internal.RequestGlobalStats( days );
			if ( result?.Result != Result.OK  ) return null;

			var r = new long[days];

			var rows = SteamUserStats.Internal.GetGlobalStatHistory( Name, r, (uint) r.Length * sizeof(long) );
			
			if ( days != rows )
				r = r.Take( rows ).ToArray();

			return r;
		}

		public async Task<double[]> GetGlobalFloatDays( int days )
		{
			var result = await SteamUserStats.Internal.RequestGlobalStats( days );
			if ( result?.Result != Result.OK ) return null;

			var r = new double[days];

			var rows = SteamUserStats.Internal.GetGlobalStatHistory( Name, r, (uint)r.Length * sizeof( double ) );

			if ( days != rows )
				r = r.Take( rows ).ToArray();

			return r;
		}

		public float GetFloat()
		{
			float val = 0.0f;

			if ( UserId > 0 )
			{
				SteamUserStats.Internal.GetUserStat( UserId, Name, ref val );
			}
			else
			{
				SteamUserStats.Internal.GetStat( Name, ref val );
			}

			return val;
		}

		public int GetInt()
		{
			int val = 0;

			if ( UserId > 0 )
			{
				SteamUserStats.Internal.GetUserStat( UserId, Name, ref val );
			}
			else
			{
				SteamUserStats.Internal.GetStat( Name, ref val );
			}

			return val;
		}

		public bool Set( int val )
		{
			LocalUserOnly();
			return SteamUserStats.Internal.SetStat( Name, val );
		}

		public bool Set( float val )
		{
			LocalUserOnly();
			return SteamUserStats.Internal.SetStat( Name, val );
		}

		public bool Add( int val )
		{
			LocalUserOnly();
			return Set( GetInt() + val );
		}

		public bool Add( float val )
		{
			LocalUserOnly();
			return Set( GetFloat() + val );
		}

		public bool UpdateAverageRate( float count, float sessionlength )
		{
			LocalUserOnly();
			return SteamUserStats.Internal.UpdateAvgRateStat( Name, count, sessionlength );
		}

		public bool Store()
		{
			LocalUserOnly();
			return SteamUserStats.Internal.StoreStats();
		}
	}
}
