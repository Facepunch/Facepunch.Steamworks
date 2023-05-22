using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Class for utilizing the Steam Friends API.
	/// </summary>
	public class SteamFriends : SteamClientClass<SteamFriends>
	{
		internal static ISteamFriends Internal => Interface as ISteamFriends;

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamFriends( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			richPresence = new Dictionary<string, string>();

			InstallEvents();

			return true;
		}

		static Dictionary<string, string> richPresence;

		internal void InstallEvents()
		{
			Dispatch.Install<PersonaStateChange_t>( x => OnPersonaStateChange?.Invoke( new Friend( x.SteamID ) ) );
			Dispatch.Install<GameRichPresenceJoinRequested_t>( x => OnGameRichPresenceJoinRequested?.Invoke( new Friend( x.SteamIDFriend), x.ConnectUTF8() ) );
			Dispatch.Install<GameConnectedFriendChatMsg_t>( OnFriendChatMessage );
			Dispatch.Install<GameConnectedClanChatMsg_t>( OnGameConnectedClanChatMessage );
			Dispatch.Install<GameOverlayActivated_t>( x => OnGameOverlayActivated?.Invoke( x.Active != 0 ) );
			Dispatch.Install<GameServerChangeRequested_t>( x => OnGameServerChangeRequested?.Invoke( x.ServerUTF8(), x.PasswordUTF8() ) );
			Dispatch.Install<GameLobbyJoinRequested_t>( x => OnGameLobbyJoinRequested?.Invoke( new Lobby( x.SteamIDLobby ), x.SteamIDFriend ) );
			Dispatch.Install<FriendRichPresenceUpdate_t>( x => OnFriendRichPresenceUpdate?.Invoke( new Friend( x.SteamIDFriend ) ) );
			Dispatch.Install<OverlayBrowserProtocolNavigation_t>( x => OnOverlayBrowserProtocol?.Invoke( x.RgchURIUTF8() ) );
		}

		/// <summary>
		/// Invoked when a chat message has been received from a friend. You'll need to enable
		/// <see cref="ListenForFriendsMessages"/> to recieve this. (friend, msgtype, message)
		/// </summary>
		public static event Action<Friend, string, string> OnChatMessage;

		/// <summary>
		/// Invoked when a chat message has been received in a Steam group chat that we are in. Associated Functions: JoinClanChatRoom. (friend, msgtype, message)
		/// </summary>
		public static event Action<Friend, string, string> OnClanChatMessage;

		/// <summary>
		/// Invoked when a friends' status changes.
		/// </summary>
		public static event Action<Friend> OnPersonaStateChange;


		/// <summary>
		/// Invoked when the user tries to join a game from their friends list.
		///	Rich presence will have been set with the "connect" key which is set here.
		/// </summary>
		public static event Action<Friend, string> OnGameRichPresenceJoinRequested;

		/// <summary>
		/// Invoked when game overlay activates or deactivates.
		///	The game can use this to be pause or resume single player games.
		/// </summary>
		public static event Action<bool> OnGameOverlayActivated;

		/// <summary>
		/// Invoked when the user tries to join a different game server from their friends list.
		///	Game client should attempt to connect to specified server when this is received.
		/// </summary>
		public static event Action<string, string> OnGameServerChangeRequested;

		/// <summary>
		/// Invoked when the user tries to join a lobby from their friends list.
		///	Game client should attempt to connect to specified lobby when this is received.
		/// </summary>
		public static event Action<Lobby, SteamId> OnGameLobbyJoinRequested;

		/// <summary>
		/// Invoked when a friend's rich presence data is updated.
		/// </summary>
		public static event Action<Friend> OnFriendRichPresenceUpdate;

		/// <summary>
		/// Invoked when an overlay browser instance is navigated to a
		/// protocol/scheme registered by <see cref="RegisterProtocolInOverlayBrowser(string)"/>.
		/// </summary>
		public static event Action<string> OnOverlayBrowserProtocol;


		static unsafe void OnFriendChatMessage( GameConnectedFriendChatMsg_t data )
		{
			if ( OnChatMessage == null ) return;

			var friend = new Friend( data.SteamIDUser );

			using var buffer = Helpers.TakeMemory();
			var type = ChatEntryType.ChatMsg;

			var len = Internal.GetFriendMessage( data.SteamIDUser, data.MessageID, buffer, Helpers.MemoryBufferSize, ref type );

			if ( len == 0 && type == ChatEntryType.Invalid )
				return;

			var typeName = type.ToString();
			var message = Helpers.MemoryToString( buffer );

			OnChatMessage( friend, typeName, message );
		}

		static unsafe void OnGameConnectedClanChatMessage( GameConnectedClanChatMsg_t data )
		{
			if ( OnClanChatMessage == null ) return;

			var friend = new Friend( data.SteamIDUser );

			using var buffer = Helpers.TakeMemory();
			var type = ChatEntryType.ChatMsg;
			SteamId chatter = data.SteamIDUser;

			var len = Internal.GetClanChatMessage( data.SteamIDClanChat, data.MessageID, buffer, Helpers.MemoryBufferSize, ref type, ref chatter );

			if ( len == 0 && type == ChatEntryType.Invalid )
				return;

			var typeName = type.ToString();
			var message = Helpers.MemoryToString( buffer );

			OnClanChatMessage( friend, typeName, message );
		}

		private static IEnumerable<Friend> GetFriendsWithFlag(FriendFlags flag)
		{
			for ( int i=0; i<Internal.GetFriendCount( (int)flag); i++ )
			{
				yield return new Friend( Internal.GetFriendByIndex( i, (int)flag ) );
			}
		}

		/// <summary>
		/// Gets an <see cref="IEnumerable{T}"/> of friends that the current user has.
		/// </summary>
		/// <returns>An <see cref="IEnumerable{T}"/> of friends.</returns>
		public static IEnumerable<Friend> GetFriends()
		{
			return GetFriendsWithFlag(FriendFlags.Immediate);
		}

		/// <summary>
		/// Gets an <see cref="IEnumerable{T}"/> of blocked users that the current user has.
		/// </summary>
		/// <returns>An <see cref="IEnumerable{T}"/> of blocked users.</returns>
		public static IEnumerable<Friend> GetBlocked()
		{
			return GetFriendsWithFlag(FriendFlags.Blocked);
		}

		/// <summary>
		/// Gets an <see cref="IEnumerable{T}"/> of friend requests that the current user has.
		/// </summary>
		/// <returns>An <see cref="IEnumerable{T}"/> of friend requests.</returns>
		public static IEnumerable<Friend> GetFriendsRequested()
		{
			return GetFriendsWithFlag( FriendFlags.FriendshipRequested );
		}

		public static IEnumerable<Friend> GetFriendsClanMembers()
		{
			return GetFriendsWithFlag( FriendFlags.ClanMember );
		}

		public static IEnumerable<Friend> GetFriendsOnGameServer()
		{
			return GetFriendsWithFlag( FriendFlags.OnGameServer );
		}

		public static IEnumerable<Friend> GetFriendsRequestingFriendship()
		{
			return GetFriendsWithFlag( FriendFlags.RequestingFriendship );
		}

		public static IEnumerable<Friend> GetPlayedWith()
		{
			for ( int i = 0; i < Internal.GetCoplayFriendCount(); i++ )
			{
				yield return new Friend( Internal.GetCoplayFriend( i ) );
			}
		}

		public static IEnumerable<Friend> GetFromSource( SteamId steamid )
		{
		    for ( int i = 0; i < Internal.GetFriendCountFromSource( steamid ); i++ )
		    {
		        yield return new Friend( Internal.GetFriendFromSourceByIndex( steamid, i ) );
		    }
		}

		public static IEnumerable<Clan> GetClans()
		{
			for (int i = 0; i < Internal.GetClanCount(); i++)
			{
				yield return new Clan( Internal.GetClanByIndex( i ) );
			}
		}

		/// <summary>
		/// Opens a specific overlay window. Valid options are:
		/// "friends", 
		/// "community", 
		/// "players", 
		/// "settings", 
		/// "officialgamegroup", 
		/// "stats", 
		/// "achievements".
		/// </summary>
		public static void OpenOverlay( string type ) => Internal.ActivateGameOverlay( type );

		/// <summary>
		/// "steamid" - Opens the overlay web browser to the specified user or groups profile.
		/// "chat" - Opens a chat window to the specified user, or joins the group chat.
		/// "jointrade" - Opens a window to a Steam Trading session that was started with the ISteamEconomy/StartTrade Web API.
		/// "stats" - Opens the overlay web browser to the specified user's stats.
		/// "achievements" - Opens the overlay web browser to the specified user's achievements.
		/// "friendadd" - Opens the overlay in minimal mode prompting the user to add the target user as a friend.
		/// "friendremove" - Opens the overlay in minimal mode prompting the user to remove the target friend.
		/// "friendrequestaccept" - Opens the overlay in minimal mode prompting the user to accept an incoming friend invite.
		/// "friendrequestignore" - Opens the overlay in minimal mode prompting the user to ignore an incoming friend invite.
		/// </summary>
		public static void OpenUserOverlay( SteamId id, string type ) => Internal.ActivateGameOverlayToUser( type, id );

		/// <summary>
		/// Activates the Steam Overlay to the Steam store page for the provided app.
		/// </summary>
		public static void OpenStoreOverlay( AppId id, OverlayToStoreFlag overlayToStoreFlag = OverlayToStoreFlag.None ) => Internal.ActivateGameOverlayToStore( id.Value, overlayToStoreFlag );

		/// <summary>
		/// Activates Steam Overlay web browser directly to the specified URL.
		/// </summary>
		public static void OpenWebOverlay( string url, bool modal = false ) => Internal.ActivateGameOverlayToWebPage( url, modal ? ActivateGameOverlayToWebPageMode.Modal : ActivateGameOverlayToWebPageMode.Default );

		/// <summary>
		/// Activates the Steam Overlay to open the invite dialog. Invitations sent from this dialog will be for the provided lobby.
		/// </summary>
		public static void OpenGameInviteOverlay( SteamId lobby ) => Internal.ActivateGameOverlayInviteDialog( lobby );

		/// <summary>
		/// Mark a target user as 'played with'.
		/// NOTE: The current user must be in game with the other player for the association to work.
		/// </summary>
		public static void SetPlayedWith( SteamId steamid ) => Internal.SetPlayedWith( steamid );

		/// <summary>
		/// Requests the persona name and optionally the avatar of a specified user.
		/// NOTE: It's a lot slower to download avatars and churns the local cache, so if you don't need avatars, don't request them.
		/// returns true if we're fetching the data, false if we already have it
		/// </summary>
		public static bool RequestUserInformation( SteamId steamid, bool nameonly = true ) => Internal.RequestUserInformation( steamid, nameonly );


		internal static async Task CacheUserInformationAsync( SteamId steamid, bool nameonly )
		{
			// Got it straight away, skip any waiting.
			if ( !RequestUserInformation( steamid, nameonly ) )
				return;

			await Task.Delay( 100 );

			while ( RequestUserInformation( steamid, nameonly ) )
			{
				await Task.Delay( 50 );
			}

			//
			// And extra wait here seems to solve avatars loading as [?]
			//
			await Task.Delay( 500 );
		}

		/// <summary>
		/// Returns a small avatar of the user with the given <paramref name="steamid"/>.
		/// </summary>
		/// <param name="steamid">The <see cref="SteamId"/> of the user to get.</param>
		/// <returns>A <see cref="Data.Image"/> with a value if the image was successfully retrieved.</returns>
		public static async Task<Data.Image?> GetSmallAvatarAsync( SteamId steamid )
		{
			await CacheUserInformationAsync( steamid, false );
			return SteamUtils.GetImage( Internal.GetSmallFriendAvatar( steamid ) );
		}

		/// <summary>
		/// Returns a medium avatar of the user with the given <paramref name="steamid"/>.
		/// </summary>
		/// <param name="steamid">The <see cref="SteamId"/> of the user to get.</param>
		/// <returns>A <see cref="Data.Image"/> with a value if the image was successfully retrieved.</returns>
		public static async Task<Data.Image?> GetMediumAvatarAsync( SteamId steamid )
		{
			await CacheUserInformationAsync( steamid, false );
			return SteamUtils.GetImage( Internal.GetMediumFriendAvatar( steamid ) );
		}

		/// <summary>
		/// Returns a large avatar of the user with the given <paramref name="steamid"/>.
		/// </summary>
		/// <param name="steamid">The <see cref="SteamId"/> of the user to get.</param>
		/// <returns>A <see cref="Data.Image"/> with a value if the image was successfully retrieved.</returns>
		public static async Task<Data.Image?> GetLargeAvatarAsync( SteamId steamid )
		{
			await CacheUserInformationAsync( steamid, false );

			var imageid = Internal.GetLargeFriendAvatar( steamid );

			// Wait for the image to download
			while ( imageid == -1 )
			{
				await Task.Delay( 50 );
				imageid = Internal.GetLargeFriendAvatar( steamid );
			}

			return SteamUtils.GetImage( imageid );
		}

		/// <summary>
		/// Find a rich presence value by key for current user. Will be null if not found.
		/// </summary>
		public static string GetRichPresence( string key )
		{
			if ( richPresence.TryGetValue( key, out var val ) )
				return val;

			return null;
		}

		/// <summary>
		/// Sets a rich presence value by key for current user.
		/// </summary>
		public static bool SetRichPresence( string key, string value )
		{
			bool success = Internal.SetRichPresence( key, value );

			if ( success ) 
				richPresence[key] = value;

			return success;
		}

		/// <summary>
		/// Clears all of the current user's rich presence data.
		/// </summary>
		public static void ClearRichPresence()
		{
			richPresence.Clear();
			Internal.ClearRichPresence();
		}

		static bool _listenForFriendsMessages;

		/// <summary>
		/// Listens for Steam friends chat messages.
		/// You can then show these chats inline in the game. For example with a Blizzard style chat message system or the chat system in Dota 2.
		/// After enabling this you will receive callbacks when ever the user receives a chat message.
		/// </summary>
		public static bool ListenForFriendsMessages
		{
			get => _listenForFriendsMessages;
				
			set
			{
				_listenForFriendsMessages = value;
				Internal.SetListenForFriendsMessages( value );
			}
		}

		/// <summary>
		/// Gets whether or not the current user is following the user with the given <paramref name="steamID"/>.
		/// </summary>
		/// <param name="steamID">The <see cref="SteamId"/> to check.</param>
		/// <returns>Boolean.</returns>
		public static async Task<bool> IsFollowing(SteamId steamID)
		{
			var r = await Internal.IsFollowing(steamID);
			return r.Value.IsFollowing;
		}

		public static async Task<int> GetFollowerCount(SteamId steamID)
		{
			var r = await Internal.GetFollowerCount(steamID);
			return r.Value.Count;
		}

        public static async Task<SteamId[]> GetFollowingList()
        {
            int resultCount = 0;
            var steamIds = new List<SteamId>();

            FriendsEnumerateFollowingList_t? result;

            do
            {
                if ( (result = await Internal.EnumerateFollowingList((uint)resultCount)) != null)
                {
                    resultCount += result.Value.ResultsReturned;

                    Array.ForEach(result.Value.GSteamID, id => { if (id > 0) steamIds.Add(id); });
                }
            } while (result != null && resultCount < result.Value.TotalResultCount);

            return steamIds.ToArray();
        }

		/// <summary>
		/// Call this before calling ActivateGameOverlayToWebPage() to have the Steam Overlay Browser block navigations
		///  to your specified protocol (scheme) uris and instead dispatch a OverlayBrowserProtocolNavigation callback to your game.
		/// </summary>
		public static bool RegisterProtocolInOverlayBrowser( string protocol )
        {
			return Internal.RegisterProtocolInOverlayBrowser( protocol );
        }

		public static async Task<bool> JoinClanChatRoom( SteamId chatId )
		{
			var result = await Internal.JoinClanChatRoom( chatId );
			if ( !result.HasValue )
				return false;

			return result.Value.ChatRoomEnterResponse == RoomEnter.Success ;
		}

		public static bool SendClanChatRoomMessage( SteamId chatId, string message )
		{
			return Internal.SendClanChatMessage( chatId, message );
		}
	}
}
