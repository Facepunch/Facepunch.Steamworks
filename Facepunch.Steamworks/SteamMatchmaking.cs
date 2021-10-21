using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Methods for clients to access matchmaking services, favorites, and to operate on game lobbies
	/// </summary>
	public class SteamMatchmaking : SteamClientClass<SteamMatchmaking>
	{
		internal static ISteamMatchmaking Internal => Interface as ISteamMatchmaking;

		internal override void InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamMatchmaking( server ) );

			InstallEvents();
		}
	
		/// <summary>
		/// Maximum number of characters a lobby metadata key can be
		/// </summary>
		internal static int MaxLobbyKeyLength => 255;


		internal static void InstallEvents()
		{
			Dispatch.Install<LobbyInvite_t>( x => OnLobbyInvite?.Invoke( new Friend( x.SteamIDUser ), new Lobby( x.SteamIDLobby ) ) );

			Dispatch.Install<LobbyEnter_t>( x => OnLobbyEntered?.Invoke( new Lobby( x.SteamIDLobby ) ) );

			Dispatch.Install<LobbyCreated_t>( x => OnLobbyCreated?.Invoke( x.Result, new Lobby( x.SteamIDLobby ) ) );

			Dispatch.Install<LobbyGameCreated_t>( x => OnLobbyGameCreated?.Invoke( new Lobby( x.SteamIDLobby ), x.IP, x.Port, x.SteamIDGameServer ) );

			Dispatch.Install<LobbyDataUpdate_t>( x =>
			{
				if ( x.Success == 0 ) return;

				if ( x.SteamIDLobby == x.SteamIDMember )
					OnLobbyDataChanged?.Invoke( new Lobby( x.SteamIDLobby ) );
				else
					OnLobbyMemberDataChanged?.Invoke( new Lobby( x.SteamIDLobby ), new Friend( x.SteamIDMember ) );
			} );

			Dispatch.Install<LobbyChatUpdate_t>( x =>
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

			Dispatch.Install<LobbyChatMsg_t>( OnLobbyChatMessageRecievedAPI );
		}

		static private unsafe void OnLobbyChatMessageRecievedAPI( LobbyChatMsg_t callback )
		{
			SteamId steamid = default;
			ChatEntryType chatEntryType = default;
			using var buffer = Helpers.TakeMemory();

			var readData = Internal.GetLobbyChatEntry( callback.SteamIDLobby, (int)callback.ChatID, ref steamid, buffer, Helpers.MemoryBufferSize, ref chatEntryType );

			if ( readData > 0 )
			{
				OnChatMessage?.Invoke( new Lobby( callback.SteamIDLobby ), new Friend( steamid ), Helpers.MemoryToString( buffer ) );
			}
		}

		/// <summary>
		/// Invoked when the current user is invited to a lobby.
		/// </summary>
		public static event Action<Friend, Lobby> OnLobbyInvite;

		/// <summary>
		/// Invoked when the current user joins a lobby.
		/// </summary>
		public static event Action<Lobby> OnLobbyEntered;

		/// <summary>
		/// Invoked when the current user creates a lobby.
		/// </summary>
		public static event Action<Result, Lobby> OnLobbyCreated;

		/// <summary>
		/// Invoked when a game server has been associated with a lobby.
		/// </summary>
		public static event Action<Lobby, uint, ushort, SteamId> OnLobbyGameCreated;

		/// <summary>
		/// Invoked when a lobby's metadata is modified.
		/// </summary>
		public static event Action<Lobby> OnLobbyDataChanged;

		/// <summary>
		/// Invoked when a member in a lobby's metadata is modified.
		/// </summary>
		public static event Action<Lobby, Friend> OnLobbyMemberDataChanged;

		/// <summary>
		/// Invoked when a member joins a lobby.
		/// </summary>
		public static event Action<Lobby, Friend> OnLobbyMemberJoined;

		/// <summary>
		/// Invoked when a lobby member leaves the lobby.
		/// </summary>
		public static event Action<Lobby, Friend> OnLobbyMemberLeave;

		/// <summary>
		/// Invoked when a lobby member leaves the lobby.
		/// </summary>
		public static event Action<Lobby, Friend> OnLobbyMemberDisconnected;

		/// <summary>
		/// Invoked when a lobby member is kicked from a lobby. The 3rd param is the user that kicked them.
		/// </summary>
		public static event Action<Lobby, Friend, Friend> OnLobbyMemberKicked;

		/// <summary>
		/// Invoked when a lobby member is kicked from a lobby. The 3rd param is the user that kicked them.
		/// </summary>
		public static event Action<Lobby, Friend, Friend> OnLobbyMemberBanned;

		/// <summary>
		/// Invoked when a chat message is received from a member of the lobby.
		/// </summary>
		public static event Action<Lobby, Friend, string> OnChatMessage;

		public static LobbyQuery LobbyList => new LobbyQuery();

		/// <summary>
		/// Creates a new invisible lobby. Call <see cref="Lobby.SetPublic"/> to take it online.
		/// </summary>
		public static async Task<Lobby?> CreateLobbyAsync( int maxMembers = 100 )
		{
			var lobby = await Internal.CreateLobby( LobbyType.Invisible, maxMembers );
			if ( !lobby.HasValue || lobby.Value.Result != Result.OK ) return null;

			return new Lobby { Id = lobby.Value.SteamIDLobby };
		}

		/// <summary>
		/// Attempts to directly join the specified lobby.
		/// </summary>
		public static async Task<Lobby?> JoinLobbyAsync( SteamId lobbyId )
		{
			var lobby = await Internal.JoinLobby( lobbyId );
			if ( !lobby.HasValue ) return null;

			return new Lobby { Id = lobby.Value.SteamIDLobby };
		}

		/// <summary>
		/// Get a list of servers that are on the current user's favorites list.
		/// </summary>
		public static IEnumerable<ServerInfo> GetFavoriteServers()
		{
			var count = Internal.GetFavoriteGameCount();

			for( int i=0; i<count; i++ )
			{
				uint timeplayed = 0;
				uint flags = 0;
				ushort qport = 0;
				ushort cport = 0;
				uint ip = 0;
				AppId appid = default;

				if ( Internal.GetFavoriteGame( i, ref appid, ref ip, ref cport, ref qport, ref flags, ref timeplayed ) )
				{
					if ( (flags & ServerInfo.k_unFavoriteFlagFavorite) == 0 ) continue;
					yield return new ServerInfo( ip, cport, qport, timeplayed );
				}
			}
		}

		/// <summary>
		/// Get a list of servers that the current user has added to their history.
		/// </summary>
		public static IEnumerable<ServerInfo> GetHistoryServers()
		{
			var count = Internal.GetFavoriteGameCount();

			for ( int i = 0; i < count; i++ )
			{
				uint timeplayed = 0;
				uint flags = 0;
				ushort qport = 0;
				ushort cport = 0;
				uint ip = 0;
				AppId appid = default;

				if ( Internal.GetFavoriteGame( i, ref appid, ref ip, ref cport, ref qport, ref flags, ref timeplayed ) )
				{
					if ( (flags & ServerInfo.k_unFavoriteFlagHistory) == 0 ) continue;
					yield return new ServerInfo( ip, cport, qport, timeplayed );
				}
			}
		}

	}
}
