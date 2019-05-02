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
				{
					_internal = new ISteamMatchmaking();
					_internal.InitClient();
				}

				return _internal;
			}
		}

		internal static void Shutdown()
		{
			_internal = null;
		}

		internal static void InstallEvents()
		{
			LobbyInvite_t.Install( x => OnLobbyInvite?.Invoke( new Friend( x.SteamIDUser ), new Lobby( x.SteamIDLobby ) ) );

			LobbyDataUpdate_t.Install( x =>
			{
				if ( x.Success == 0 ) return;

				if ( x.SteamIDLobby == x.SteamIDMember )
					OnLobbyDataChanged?.Invoke( new Lobby( x.SteamIDLobby ) );
				else
					OnLobbyMemberDataChanged?.Invoke( new Lobby( x.SteamIDLobby ), new Friend( x.SteamIDMember ) );
			} );

			LobbyChatUpdate_t.Install( x =>
			{
				if ( (x.GfChatMemberStateChange & (int)ChatMemberStateChange.Entered) != 0 )
					OnLobbyMemberJoined?.Invoke( new Lobby( x.SteamIDLobby ), new Friend( x.SteamIDUserChanged ) );

				if ( (x.GfChatMemberStateChange & (int)ChatMemberStateChange.Left) != 0 )
					OnLobbyMemberLeave?.Invoke( new Lobby( x.SteamIDLobby ), new Friend( x.SteamIDUserChanged ) );

				if ( (x.GfChatMemberStateChange & (int)ChatMemberStateChange.Disconnected) != 0 )
					OnLobbyMemberDisconnected?.Invoke( new Lobby( x.SteamIDLobby ), new Friend( x.SteamIDUserChanged ) );

				if ( (x.GfChatMemberStateChange & (int)ChatMemberStateChange.Kicked) != 0 )
					OnLobbyMemberKicked?.Invoke( new Lobby( x.SteamIDLobby ), new Friend( x.SteamIDUserChanged ), new Friend( x.SteamIDMakingChange ) );

				if ( (x.GfChatMemberStateChange & (int)ChatMemberStateChange.Banned) != 0 )
					OnLobbyMemberBanned?.Invoke( new Lobby( x.SteamIDLobby ), new Friend( x.SteamIDUserChanged ), new Friend( x.SteamIDMakingChange ) );
			} );

			LobbyChatMsg_t.Install( OnLobbyChatMessageRecievedAPI );
		}

		static private unsafe void OnLobbyChatMessageRecievedAPI( LobbyChatMsg_t callback )
		{
			SteamId steamid = default;
			ChatEntryType chatEntryType = default;
			var buffer = Helpers.TakeBuffer( 1024 * 4 );

			fixed ( byte* p = buffer )
			{
				var readData = Internal.GetLobbyChatEntry( callback.SteamIDLobby, (int)callback.ChatID, ref steamid, (IntPtr)p, buffer.Length, ref chatEntryType );

				if ( readData > 0 )
				{
					OnChatMessage?.Invoke( new Lobby( callback.SteamIDLobby ), new Friend( steamid ), Encoding.UTF8.GetString( buffer, 0, readData ) );
				}
			}
		}

		/// <summary>
		/// Someone invited you to a lobby
		/// </summary>
		public static event Action<Friend, Lobby> OnLobbyInvite;

		/// <summary>
		/// The lobby metadata has changed
		/// </summary>
		public static event Action<Lobby> OnLobbyDataChanged;

		/// <summary>
		/// The lobby member metadata has changed
		/// </summary>
		public static event Action<Lobby, Friend> OnLobbyMemberDataChanged;

		/// <summary>
		/// The lobby member joined
		/// </summary>
		public static event Action<Lobby, Friend> OnLobbyMemberJoined;

		/// <summary>
		/// The lobby member left the room
		/// </summary>
		public static event Action<Lobby, Friend> OnLobbyMemberLeave;

		/// <summary>
		/// The lobby member left the room
		/// </summary>
		public static event Action<Lobby, Friend> OnLobbyMemberDisconnected;

		/// <summary>
		/// The lobby member was kicked. The 3rd param is the user that kicked them.
		/// </summary>
		public static event Action<Lobby, Friend, Friend> OnLobbyMemberKicked;

		/// <summary>
		/// The lobby member was banned. The 3rd param is the user that banned them.
		/// </summary>
		public static event Action<Lobby, Friend, Friend> OnLobbyMemberBanned;

		/// <summary>
		/// A chat message was recieved from a member of a lobby
		/// </summary>
		public static event Action<Lobby, Friend, string> OnChatMessage;

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