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

	}
}