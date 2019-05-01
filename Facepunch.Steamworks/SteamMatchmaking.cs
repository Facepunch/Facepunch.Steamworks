using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Functions for clients to access matchmaking services, favorites, and to operate on game lobbies
	/// </summary>
	public static class SteamMatchmaking
	{
		static ISteamMatchmaking _internal;
		internal static ISteamMatchmaking Internal
		{
			get
			{
				if ( _internal == null )
					_internal = new ISteamMatchmaking();

				return _internal;
			}
		}

		internal static void Shutdown()
		{
			_internal = null;
		}

		internal static void InstallEvents()
		{
			//PlaybackStatusHasChanged_t.Install( x => OnPlaybackChanged?.Invoke() );
			//VolumeHasChanged_t.Install( x => OnVolumeChanged?.Invoke( x.NewVolume ) );
		}

		public static LobbyQuery LobbyList => new LobbyQuery();

		/// <summary>
		/// Creates a new invisible lobby. Call lobby.SetPublic to take it online.
		/// </summary>
		public static async Task<Lobby?> CreateLobbyAsync( int maxMembers = 100 )
		{
			var lobby = await Internal.CreateLobby( LobbyType.Invisible, maxMembers );
			if ( !lobby.HasValue || lobby.Value.Result != Result.OK ) return null;

			return new Lobby { Id = lobby.Value.SteamIDLobby };
		}

	}
}