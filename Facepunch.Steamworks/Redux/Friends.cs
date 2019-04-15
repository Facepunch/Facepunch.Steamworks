using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Facepunch.Steamworks;
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
				{
					_internal = new Internal.ISteamFriends();

					richPresence = new Dictionary<string, string>();
				}

				return _internal;
			}
		}

		static Dictionary<string, string> richPresence;

		internal static void InstallEvents()
		{
			new Event<PersonaStateChange_t>( x => OnPersonaStateChange?.Invoke( new Friend( x.SteamID ) ) );
			new Event<GameRichPresenceJoinRequested_t>( x => OnGameRichPresenceJoinRequested?.Invoke( new Friend( x.SteamIDFriend), x.Connect ) );
			new Event<GameConnectedFriendChatMsg_t>( OnFriendChatMessage );
			new Event<GameOverlayActivated_t>( x => OnGameOverlayActivated?.Invoke() );
			new Event<GameServerChangeRequested_t>( x => OnGameServerChangeRequested?.Invoke( x.Server, x.Password ) );
			new Event<GameLobbyJoinRequested_t>( x => OnGameLobbyJoinRequested?.Invoke( x.SteamIDLobby, x.SteamIDFriend ) );
			new Event<FriendRichPresenceUpdate_t>( x => OnFriendRichPresenceUpdate?.Invoke( new Friend( x.SteamIDFriend ) ) );
		}

		/// <summary>
		/// Called when chat message has been received from a friend. You'll need to turn on
		/// ListenForFriendsMessages to recieve this. (friend, msgtype, message)
		/// </summary>
		public static event Action<Friend, string, string> OnChatMessage;

		/// <summary>
		/// called when a friends' status changes
		/// </summary>
		public static event Action<Friend> OnPersonaStateChange;


		/// <summary>
		/// Called when the user tries to join a game from their friends list
		///	rich presence will have been set with the "connect" key which is set here
		/// </summary>
		public static event Action<Friend, string> OnGameRichPresenceJoinRequested;

		/// <summary>
		/// Posted when game overlay activates or deactivates
		///	the game can use this to be pause or resume single player games
		/// </summary>
		public static event Action OnGameOverlayActivated;

		/// <summary>
		/// Called when the user tries to join a different game server from their friends list
		///	game client should attempt to connect to specified server when this is received
		/// </summary>
		public static event Action<string, string> OnGameServerChangeRequested;

		/// <summary>
		/// Called when the user tries to join a lobby from their friends list
		///	game client should attempt to connect to specified lobby when this is received
		/// </summary>
		public static event Action<CSteamID, CSteamID> OnGameLobbyJoinRequested;

		/// <summary>
		/// Callback indicating updated data about friends rich presence information
		/// </summary>
		public static event Action<Friend> OnFriendRichPresenceUpdate;

		static unsafe void OnFriendChatMessage( GameConnectedFriendChatMsg_t data )
		{
			if ( OnChatMessage == null ) return;

			var friend = new Friend( data.SteamIDUser );

			var buffer = Helpers.TakeBuffer( 1024 * 32 );
			var type = ChatEntryType.ChatMsg;

			fixed ( byte* ptr = buffer )
			{
				var len = Internal.GetFriendMessage( data.SteamIDUser, data.MessageID, (IntPtr)ptr, buffer.Length, ref type );

				if ( len == 0 && type == ChatEntryType.Invalid )
					return;

				var typeName = type.ToString();
				var message = Encoding.UTF8.GetString( buffer, 0, len );

				OnChatMessage( friend, typeName, message );
			}
		}

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

		/// <summary>
		/// The dialog to open. Valid options are: 
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
		public static void OpenUserOverlay( CSteamID id, string type ) => Internal.ActivateGameOverlayToUser( type, id );

		/// <summary>
		/// Activates the Steam Overlay to the Steam store page for the provided app.
		/// </summary>
		public static void OpenStoreOverlay( AppId id ) => Internal.ActivateGameOverlayToStore( id.Value, OverlayToStoreFlag.None );

		/// <summary>
		/// Activates Steam Overlay web browser directly to the specified URL.
		/// </summary>
		public static void OpenWebOverlay( string url, bool modal = false ) => Internal.ActivateGameOverlayToWebPage( url, modal ? ActivateGameOverlayToWebPageMode.Modal : ActivateGameOverlayToWebPageMode.Default );

		/// <summary>
		/// Activates the Steam Overlay to open the invite dialog. Invitations sent from this dialog will be for the provided lobby.
		/// </summary>
		public static void OpenGameInviteOverlay( CSteamID lobby ) => Internal.ActivateGameOverlayInviteDialog( lobby );

		/// <summary>
		/// Mark a target user as 'played with'.
		/// NOTE: The current user must be in game with the other player for the association to work.
		/// </summary>
		public static void SetPlayedWith( CSteamID steamid ) => Internal.SetPlayedWith( steamid );

		static async Task CacheUserInformationAsync( CSteamID steamid, bool nameonly )
		{
			// Got it straight away, skip any waiting.
			if ( !Internal.RequestUserInformation( steamid, nameonly ) )
				return;

			await Task.Delay( 100 );

			while ( Internal.RequestUserInformation( steamid, nameonly ) )
			{
				await Task.Delay( 50 );
			}

			//
			// And extra wait here seems to solve avatars loading as [?]
			//
			await Task.Delay( 500 );
		}

		public static async Task<Image?> GetSmallAvatarAsync( CSteamID steamid )
		{
			await CacheUserInformationAsync( steamid, false );
			return Utils.GetImage( Internal.GetSmallFriendAvatar( steamid ) );
		}

		public static async Task<Image?> GetMediumAvatarAsync( CSteamID steamid )
		{
			await CacheUserInformationAsync( steamid, false );
			return Utils.GetImage( Internal.GetMediumFriendAvatar( steamid ) );
		}

		public static async Task<Image?> GetLargeAvatarAsync( CSteamID steamid )
		{
			await CacheUserInformationAsync( steamid, false );

			var imageid = Internal.GetLargeFriendAvatar( steamid );

			// Wait for the image to download
			while ( imageid == -1 )
			{
				await Task.Delay( 50 );
				imageid = Internal.GetLargeFriendAvatar( steamid );
			}

			return Utils.GetImage( imageid );
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
			richPresence[key] = value;
			return Internal.SetRichPresence( key, value );
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

	}
}