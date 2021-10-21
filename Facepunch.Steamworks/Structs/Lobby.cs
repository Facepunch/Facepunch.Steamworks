using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	/// <summary>
	/// Represents a Steam lobby.
	/// </summary>
	public struct Lobby
	{
		public SteamId Id { get; internal set; }


		public Lobby( SteamId id )
		{
			Id = id;
		}

		/// <summary>
		/// Try to join this room. Will return <see cref="RoomEnter.Success"/> on success,
		/// and anything else is a failure.
		/// </summary>
		public async Task<RoomEnter> Join()
		{
			var result = await SteamMatchmaking.Internal.JoinLobby( Id );
			if ( !result.HasValue ) return RoomEnter.Error;

			return (RoomEnter) result.Value.EChatRoomEnterResponse;
		}

		/// <summary>
		/// Leave a lobby; this will take effect immediately on the client side
		/// other users in the lobby will be notified by a LobbyChatUpdate_t callback
		/// </summary>
		public void Leave()
		{
			SteamMatchmaking.Internal.LeaveLobby( Id );
		}

		/// <summary>
		/// Invite another user to the lobby.
		/// Will return <see langword="true"/> if the invite is successfully sent, whether or not the target responds
		/// returns <see langword="false"/> if the local user is not connected to the Steam servers
		/// </summary>
		public bool InviteFriend( SteamId steamid )
		{
			return SteamMatchmaking.Internal.InviteUserToLobby( Id, steamid );
		}

		/// <summary>
		/// Gets the number of users in this lobby.
		/// </summary>
		public int MemberCount => SteamMatchmaking.Internal.GetNumLobbyMembers( Id );

		/// <summary>
		/// Returns current members in the lobby. The current user must be in the lobby in order to see the users.
		/// </summary>
		public IEnumerable<Friend> Members
		{
			get
			{
				for( int i = 0; i < MemberCount; i++ )
				{
					yield return new Friend( SteamMatchmaking.Internal.GetLobbyMemberByIndex( Id, i ) );
				}
			}
		}


		/// <summary>
		/// Get data associated with this lobby.
		/// </summary>
		public string GetData( string key )
		{
			return SteamMatchmaking.Internal.GetLobbyData( Id, key );
		}

		/// <summary>
		/// Set data associated with this lobby.
		/// </summary>
		public bool SetData( string key, string value )
		{
			if ( key.Length > 255 ) throw new System.ArgumentException( "Key should be < 255 chars", nameof( key ) );
			if ( value.Length > 8192 ) throw new System.ArgumentException( "Value should be < 8192 chars", nameof( key ) );

			return SteamMatchmaking.Internal.SetLobbyData( Id, key, value );
		}

		/// <summary>
		/// Removes a metadata key from the lobby.
		/// </summary>
		public bool DeleteData( string key )
		{
			return SteamMatchmaking.Internal.DeleteLobbyData( Id, key );
		}

		/// <summary>
		/// Get all data for this lobby.
		/// </summary>
		public IEnumerable<KeyValuePair<string, string>> Data
		{
			get
			{
				var cnt = SteamMatchmaking.Internal.GetLobbyDataCount( Id );

				for ( int i =0; i<cnt; i++)
				{
					if ( SteamMatchmaking.Internal.GetLobbyDataByIndex( Id, i, out var a, out var b ) )
					{
						yield return new KeyValuePair<string, string>( a, b );
					}
				}
			}
		}

		/// <summary>
		/// Gets per-user metadata for someone in this lobby.
		/// </summary>
		public string GetMemberData( Friend member, string key )
		{
			return SteamMatchmaking.Internal.GetLobbyMemberData( Id, member.Id, key );
		}

		/// <summary>
		/// Sets per-user metadata (for the local user implicitly).
		/// </summary>
		public void SetMemberData( string key, string value )
		{
			SteamMatchmaking.Internal.SetLobbyMemberData( Id, key, value );
		}

		/// <summary>
		/// Sends a string to the chat room.
		/// </summary>
		public bool SendChatString( string message )
		{
			//adding null terminator as it's used in Helpers.MemoryToString
			var data = System.Text.Encoding.UTF8.GetBytes( message + '\0' );
			return SendChatBytes( data );
		}

		/// <summary>
		/// Sends bytes to the chat room.
		/// </summary>
		public unsafe bool SendChatBytes( byte[] data )
		{
			fixed ( byte* ptr = data )
			{
				return SendChatBytesUnsafe( ptr, data.Length );
			}
		}

		/// <summary>
		/// Sends bytes to the chat room from an unsafe buffer.
		/// </summary>
		public unsafe bool SendChatBytesUnsafe( byte* ptr, int length )
		{
			return SteamMatchmaking.Internal.SendLobbyChatMsg( Id, (IntPtr)ptr, length );
		}

		/// <summary>
		/// Refreshes metadata for a lobby you're not necessarily in right now.
		/// <para>
		/// You never do this for lobbies you're a member of, only if your
		/// this will send down all the metadata associated with a lobby.
		/// This is an asynchronous call.
		/// Returns <see langword="false"/> if the local user is not connected to the Steam servers.
		/// Results will be returned by a LobbyDataUpdate_t callback.
		/// If the specified lobby doesn't exist, LobbyDataUpdate_t::m_bSuccess will be set to <see langword="false"/>.
		/// </para>
		/// </summary>
		public bool Refresh()
		{
			return SteamMatchmaking.Internal.RequestLobbyData( Id );
		}

		/// <summary>
		/// Max members able to join this lobby. Cannot be over <c>250</c>.
		/// Can only be set by the owner of the lobby.
		/// </summary>
		public int MaxMembers
		{
			get => SteamMatchmaking.Internal.GetLobbyMemberLimit( Id );
			set => SteamMatchmaking.Internal.SetLobbyMemberLimit( Id, value );
		}

		/// <summary>
		/// Sets the lobby as public.
		/// </summary>
		public bool SetPublic()
		{
			return SteamMatchmaking.Internal.SetLobbyType( Id, LobbyType.Public );
		}

		/// <summary>
		/// Sets the lobby as private.
		/// </summary>
		public bool SetPrivate()
		{
			return SteamMatchmaking.Internal.SetLobbyType( Id, LobbyType.Private );
		}

		/// <summary>
		/// Sets the lobby as invisible.
		/// </summary>
		public bool SetInvisible()
		{
			return SteamMatchmaking.Internal.SetLobbyType( Id, LobbyType.Invisible );
		}

		/// <summary>
		/// Sets the lobby as friends only.
		/// </summary>
		public bool SetFriendsOnly()
		{
			return SteamMatchmaking.Internal.SetLobbyType( Id, LobbyType.FriendsOnly );
		}

		/// <summary>
		/// Set whether or not the lobby can be joined.
		/// </summary>
		/// <param name="b">Whether or not the lobby can be joined.</param>
		public bool SetJoinable( bool b )
		{
			return SteamMatchmaking.Internal.SetLobbyJoinable( Id, b );
		}

		/// <summary>
		/// [SteamID variant]
		/// Allows the owner to set the game server associated with the lobby. Triggers the
		/// Steammatchmaking.OnLobbyGameCreated event.
		/// </summary>
		public void SetGameServer( SteamId steamServer )
		{
			if ( !steamServer.IsValid )
				throw new ArgumentException( $"SteamId for server is invalid" );

			SteamMatchmaking.Internal.SetLobbyGameServer( Id, 0, 0, steamServer );
		}

		/// <summary>
		/// [IP/Port variant]
		/// Allows the owner to set the game server associated with the lobby. Triggers the
		/// Steammatchmaking.OnLobbyGameCreated event.
		/// </summary>
		public void SetGameServer( string ip, ushort port )
		{
			if ( !IPAddress.TryParse( ip, out IPAddress add ) )
				throw new ArgumentException( $"IP address for server is invalid" );

			SteamMatchmaking.Internal.SetLobbyGameServer( Id, add.IpToInt32(), port, new SteamId() );
		}

		/// <summary>
		/// Gets the details of the lobby's game server, if set. Returns true if the lobby is
		/// valid and has a server set, otherwise returns false.
		/// </summary>
		public bool GetGameServer( ref uint ip, ref ushort port, ref SteamId serverId )
		{
			return SteamMatchmaking.Internal.GetLobbyGameServer( Id, ref ip, ref port, ref serverId );
		}

		/// <summary>
		/// Gets or sets the owner of the lobby. You must be the lobby owner to set the owner
		/// </summary>
		public Friend Owner
		{
			get => new Friend( SteamMatchmaking.Internal.GetLobbyOwner( Id ) );
			set => SteamMatchmaking.Internal.SetLobbyOwner( Id, value.Id );
		}

		/// <summary>
		/// Check if the specified SteamId owns the lobby.
		/// </summary>
		public bool IsOwnedBy( SteamId k ) => Owner.Id == k;
	}
}
