using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SteamNative;

namespace Steamworks
{
	/// <summary>
	/// Undocumented Parental Settings
	/// </summary>
	public static class Friends
	{
		static Internal.ISteamFriends _internal;
		internal static Internal.ISteamFriends Internal
		{
			get
			{
				if ( _internal == null )
					_internal = new Internal.ISteamFriends();

				return _internal;
			}
		}

		internal static void InstallEvents()
		{
			//new Event<BroadcastUploadStart_t>( x => OnBroadcastStarted?.Invoke() );
			//new Event<BroadcastUploadStop_t>( x => OnBroadcastStopped?.Invoke( x.Result ) );
		}

		//	public static event Action OnBroadcastStarted;

		/// <summary>
		/// returns the local players name - guaranteed to not be NULL.
		/// this is the same name as on the users community profile page
		/// </summary>
		public static string PersonaName => Internal.GetPersonaName();

		/// <summary>
		/// gets the status of the current user
		/// </summary>
		public static PersonaState PersonaState => Internal.GetPersonaState();

		public static IEnumerable<Friend> GetFriends()
		{
			for ( int i=0; i<Internal.GetFriendCount( (int) FriendFlags.Immediate ); i++ )
			{
				yield return new Friend( Internal.GetFriendByIndex( i, 0xFFFF ) );
			}
		}

		public static IEnumerable<Friend> GetBlocked()
		{
			for ( int i = 0; i < Internal.GetFriendCount( (int)FriendFlags.Blocked ); i++ )
			{
				yield return new Friend( Internal.GetFriendByIndex( i, 0xFFFF ) );
			}
		}

		public static IEnumerable<Friend> GetPlayedWith()
		{
			for ( int i = 0; i < Internal.GetFriendCount( (int)FriendFlags.Blocked ); i++ )
			{
				yield return new Friend( Internal.GetFriendByIndex( i, 0xFFFF ) );
			}
		}



	}
}