using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// The entry point to everything Steam knows about other people: enumerating the local user's
	/// friends, clans and recent teammates, reading and publishing rich presence, opening the Steam
	/// overlay, and receiving the "my friend wants to join" events that make a Steam game feel
	/// social. Individual users are represented by <see cref="Friend"/> and groups by
	/// <see cref="Clan"/>; this class is where you get hold of them.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Everything here is static and only valid after <c>SteamClient.Init</c> has succeeded. There is
	/// no null-guard on the underlying interface, so calling into this class before initialisation or
	/// after <c>SteamClient.Shutdown</c> throws rather than returning a failure value.
	/// </para>
	/// <para>
	/// <b>Nothing arrives unless callbacks are pumped.</b> Every event on this class, and every
	/// <c>Task</c> returned from it, is delivered from the Steam callback queue. If you initialised
	/// with <c>SteamClient.Init( appid, asyncCallbacks: false )</c> you must call
	/// <see cref="SteamClient.RunCallbacks"/> every frame; otherwise events never fire and awaits
	/// never complete.
	/// </para>
	/// <para>
	/// Handlers run on whatever pumps the queue. With <c>asyncCallbacks: false</c> that is the thread
	/// calling <see cref="SteamClient.RunCallbacks"/>. With <c>asyncCallbacks: true</c> the pump is a
	/// background <c>async</c> loop whose continuations follow the ambient
	/// <c>SynchronizationContext</c> — thread-pool threads when there is none, the host's own context
	/// when a game engine installs one. Do not assume a particular thread; marshal explicitly before
	/// touching engine state.
	/// </para>
	/// <para>
	/// <b>Friend data is not automatically present.</b> Names, avatars and persona states only exist
	/// for users the client has cached. Reads for anyone else return empty values with no error —
	/// see the remarks on <see cref="Friend"/>. Use <see cref="RequestUserInformation"/> or
	/// <c>Friend.RequestInfoAsync</c> to pull the data in, and refresh your UI from
	/// <see cref="OnPersonaStateChange"/>.
	/// </para>
	/// <para>
	/// Event handlers registered here are static and are never removed by
	/// <c>SteamClient.Shutdown</c>. Unsubscribe explicitly when your objects die, or you will leak
	/// them and invoke handlers on dead state after a re-init.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// SteamClient.Init( 480 );
	///
	/// // Follow a friend into their game when they invite us from the friends list.
	/// SteamFriends.OnGameLobbyJoinRequested += async ( lobby, invitedBy ) =&gt;
	/// {
	///     if ( await lobby.Join() == RoomEnter.Success )
	///         Console.WriteLine( $"Joined a lobby via {new Friend( invitedBy ).Name}" );
	/// };
	///
	/// // Tell Steam what we are doing so friends can see and join us.
	/// SteamFriends.SetRichPresence( "steam_display", "#Status_InMatch" );
	/// SteamFriends.SetRichPresence( "connect", "+connect 10.0.0.5:27015" );
	///
	/// foreach ( var friend in SteamFriends.GetFriends() )
	/// {
	///     if ( friend.IsPlayingThisGame )
	///         Console.WriteLine( $"{friend.Name} is in game" );
	/// }
	/// </code>
	/// </example>
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
		/// A one-to-one Steam chat message arrived from another user, so your game can render Steam
		/// friend chat in its own UI instead of sending the player to the overlay. Arguments are
		/// <c>(sender, messageType, messageText)</c>, where <c>messageType</c> is the string name of
		/// the underlying <c>EChatEntryType</c> — usually <c>"ChatMsg"</c>, but also things like
		/// <c>"Typing"</c> and <c>"Emote"</c>, which carry no useful text.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This fires only while <see cref="ListenForFriendsMessages"/> is <see langword="true"/>.
		/// Setting that property intercepts the messages: it is opt-in precisely because you are
		/// taking on the job of displaying them.
		/// </para>
		/// <para>
		/// Contrast with <see cref="OnClanChatMessage"/>, which is the group-chat equivalent. The two
		/// are separate Steam channels with separate opt-ins — this one needs
		/// <see cref="ListenForFriendsMessages"/>, that one needs a successful
		/// <see cref="JoinClanChatRoom"/> — and a given message arrives on exactly one of them.
		/// </para>
		/// <para>
		/// The message text is fetched through a 32 KB buffer when the event fires. If Steam reports
		/// both a zero length and an invalid entry type the event is silently dropped rather than
		/// raised with an empty string, so you will not see every message ID that passes through.
		/// Reply with <c>Friend.SendMessage</c>.
		/// </para>
		/// </remarks>
		public static event Action<Friend, string, string> OnChatMessage;

		/// <summary>
		/// A message arrived in a Steam group (clan) chat room that this game has joined via
		/// <see cref="JoinClanChatRoom"/> — the multi-user counterpart to
		/// <see cref="OnChatMessage"/>. Arguments are <c>(sender, messageType, messageText)</c>.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The differences from <see cref="OnChatMessage"/> that actually matter: this one is enabled
		/// per chat room by awaiting <see cref="JoinClanChatRoom"/> rather than by a global flag; it
		/// covers rooms with many participants, so the first argument identifies which member spoke;
		/// and it stays active only while you remain in the room.
		/// </para>
		/// <para>
		/// The event does not tell you <em>which</em> room the message came from, even though Steam
		/// supplies that. If your game joins more than one clan chat you cannot attribute a message
		/// to a room through this API.
		/// </para>
		/// <para>
		/// As with <see cref="OnChatMessage"/>, a message that reports zero length and an invalid
		/// entry type is dropped rather than raised. Send with
		/// <see cref="SendClanChatRoomMessage"/>.
		/// </para>
		/// </remarks>
		public static event Action<Friend, string, string> OnClanChatMessage;

		/// <summary>
		/// Something about a user changed — this is how friend data actually arrives. It is the
		/// signal to re-read a <see cref="Friend"/>'s name, avatar, state, level or relationship and
		/// refresh your UI, and it is what completes the wait inside
		/// <see cref="RequestUserInformation"/>.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Expect a burst of these at startup. Valve notes that the underlying change flags "describe
		/// what the client has learned has changed recently, so on startup you'll see a name, avatar
		/// &amp; relationship change for every friend". Do not treat each one as a meaningful state
		/// transition.
		/// </para>
		/// <para>
		/// Valve's <c>PersonaStateChange_t</c> carries an <c>m_nChangeFlags</c> field
		/// (<c>EPersonaChange</c>) saying which field changed — name, avatar, state, nickname, Steam
		/// level, rich presence and so on. <b>This binding does not surface those flags</b>: the
		/// event hands you only the user. You therefore cannot tell a name change from an avatar
		/// change and must re-read whatever you care about.
		/// </para>
		/// <para>
		/// Fires for any user the client tracks, not only friends — lobby and game-server members
		/// included. It does not fire for a user Steam knows nothing about, which is why an unknown
		/// user needs <see cref="RequestUserInformation"/> to get the first callback going.
		/// </para>
		/// </remarks>
		public static event Action<Friend> OnPersonaStateChange;


		/// <summary>
		/// The local user accepted a game invite or pressed "Join game" in the Steam friends list,
		/// and Steam is handing your game the connect string to act on. This is the primary
		/// invite-acceptance path — wire it up or invites silently do nothing. Arguments are
		/// <c>(friendJoined, connectString)</c>.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The connect string is whatever was published under the <c>"connect"</c> rich presence key
		/// (see <see cref="SetRichPresence"/>) or passed to <c>Friend.InviteToGame</c>. It is your
		/// game's own format; Steam does not interpret it. Valve caps it at 256 characters.
		/// </para>
		/// <para>
		/// Valve warns that the friend "will be invalid if not directly via a friend", so treat the
		/// first argument as advisory and drive the actual join off the connect string.
		/// </para>
		/// <para>
		/// If your game was not running, Steam launches it and this fires once the API is
		/// initialised — so register the handler before or immediately after <c>SteamClient.Init</c>,
		/// not lazily when a menu opens. Distinct from <see cref="OnGameLobbyJoinRequested"/>, which
		/// hands you a lobby to join rather than a string to parse, and from
		/// <see cref="OnGameServerChangeRequested"/>, which hands you a server address.
		/// </para>
		/// </remarks>
		public static event Action<Friend, string> OnGameRichPresenceJoinRequested;

		/// <summary>
		/// The Steam overlay opened or closed on top of your game. The argument is
		/// <see langword="true"/> when it has just been activated. Single-player games should pause
		/// here — the player cannot see or control the game while the overlay is up.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Also the moment to release mouse capture and stop treating input as gameplay input, since
		/// the overlay is consuming it.
		/// </para>
		/// <para>
		/// Valve's callback also carries <c>m_bUserInitiated</c> — whether the player asked for this
		/// or the overlay came up on its own (for an incoming invite, say). This binding does not
		/// surface it, so you cannot distinguish the two cases here.
		/// </para>
		/// </remarks>
		public static event Action<bool> OnGameOverlayActivated;

		/// <summary>
		/// The local user chose to join a friend who is on a game server, and your game should
		/// connect to it. Arguments are <c>(serverAddress, password)</c>.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The address is a string in the form Valve documents as <c>"127.0.0.1:27015"</c> or
		/// <c>"tf2.valvesoftware.com"</c> — note the port is optional, so parse defensively. Both
		/// fields come from fixed 64-byte native buffers; the password is an empty string when the
		/// server has none, not <see langword="null"/>.
		/// </para>
		/// <para>
		/// This is the dedicated-server sibling of <see cref="OnGameLobbyJoinRequested"/> (which
		/// gives you a Steam lobby) and <see cref="OnGameRichPresenceJoinRequested"/> (which gives
		/// you your own connect string). Which one fires depends on how the friend's presence was
		/// published, so a game that supports more than one of these must handle each.
		/// </para>
		/// </remarks>
		public static event Action<string, string> OnGameServerChangeRequested;

		/// <summary>
		/// The local user chose to join a friend's Steam lobby from the friends list or an invite.
		/// Arguments are <c>(lobby, invitedBy)</c>; call <c>Join</c> on the lobby to accept.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The lobby handle is valid immediately but joining can still fail — it may be full,
		/// private, or already gone. Check the <c>RoomEnter</c> result from <c>Lobby.Join</c> rather
		/// than assuming success.
		/// </para>
		/// <para>
		/// Valve notes the friend ID "will be invalid if not directly via a friend", so
		/// <c>invitedBy</c> can be a zero Steam ID; do not use it as a lookup key without checking.
		/// </para>
		/// <para>
		/// Like <see cref="OnGameRichPresenceJoinRequested"/>, this can fire during startup when
		/// Steam launched your game to accept an invite — register the handler early.
		/// </para>
		/// </remarks>
		public static event Action<Lobby, SteamId> OnGameLobbyJoinRequested;

		/// <summary>
		/// A friend's rich presence key/values changed — the cue to re-read
		/// <c>Friend.GetRichPresence</c> and update whatever you show about what they are doing.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This exists separately from <see cref="OnPersonaStateChange"/> because rich presence is
		/// game data rather than account data, and Valve raises the two callbacks independently.
		/// Rich presence is only shared between friends who are in the same game, so this fires for
		/// a far narrower set of users than <see cref="OnPersonaStateChange"/> does.
		/// </para>
		/// <para>
		/// Valve's callback also carries the app ID the presence belongs to; this binding does not
		/// surface it. In practice it "should always be the current game", per Valve's comment.
		/// </para>
		/// </remarks>
		public static event Action<Friend> OnFriendRichPresenceUpdate;

		/// <summary>
		/// The overlay browser tried to navigate to a URI using a custom scheme your game claimed
		/// with <see cref="RegisterProtocolInOverlayBrowser(string)"/>. Steam blocks the navigation
		/// and hands you the full URI instead, which is how a web page inside the overlay can call
		/// back into your game.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The URI comes from a fixed 1024-byte native buffer, so anything longer is truncated by
		/// Steam before you see it. It is attacker-controllable in the sense that the page in the
		/// overlay decides its contents — validate it before acting on it.
		/// </para>
		/// <para>
		/// This only works when the page was opened in modal mode. Valve requires that
		/// "ActivateGameOverlayToWebPage() must have been called with
		/// k_EActivateGameOverlayToWebPageMode_Modal", which in this binding means
		/// <c>OpenWebOverlay( url, modal: true )</c>. Open the page non-modally and the navigation is
		/// not intercepted and this event never fires.
		/// </para>
		/// </remarks>
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
			// Hoisted out of the loop condition - it was one extra native call per friend.
			// This backs six public enumerators, so it was the most-executed instance of the
			// pattern in the library.
			var count = Internal.GetFriendCount( (int)flag );

			for ( int i = 0; i < count; i++ )
			{
				yield return new Friend( Internal.GetFriendByIndex( i, (int)flag ) );
			}
		}

		/// <summary>
		/// The local user's actual friends — the "regular" friends list as shown in the Steam client,
		/// excluding blocked users and pending invites in either direction. This is the list you want
		/// for a friends panel.
		/// </summary>
		/// <returns>
		/// A lazily evaluated sequence of confirmed friends, empty if the user has none. There is no
		/// failure signal: an empty sequence when you expected entries means the friends list has not
		/// loaded, not that the call failed.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Lazy — the count is taken once when enumeration starts, then each entry is a separate
		/// native call. Do not hold the sequence across frames while the friends list can change; the
		/// index-based lookups behind it are only coherent for the length of one pass. Enumerate it
		/// twice and you get two fresh reads.
		/// </para>
		/// <para>
		/// The <see cref="Friend"/> values that come out carry only Steam IDs. Their names and
		/// avatars are populated for friends, but see the remarks on <see cref="Friend"/> before
		/// assuming any particular field is present.
		/// </para>
		/// </remarks>
		public static IEnumerable<Friend> GetFriends()
		{
			return GetFriendsWithFlag(FriendFlags.Immediate);
		}

		/// <summary>
		/// Users the local user has blocked. Use it to build a mute or hide list so blocked players
		/// stay blocked inside your game, not just in the Steam client.
		/// </summary>
		/// <returns>
		/// A lazily evaluated sequence of blocked users, empty if there are none.
		/// </returns>
		/// <remarks>
		/// This enumerates Steam's <c>k_EFriendFlagBlocked</c> bucket, which Valve describes as "the
		/// user has just done an Ignore on a friendship invite" and explicitly says "doesn't get
		/// stored". The persistent block list is a different flag (<c>k_EFriendFlagIgnored</c>) that
		/// this class does not expose an enumerator for, so this is not a complete picture of who the
		/// user has blocked. See the remarks on <c>Friend.IsBlocked</c>.
		/// </remarks>
		public static IEnumerable<Friend> GetBlocked()
		{
			return GetFriendsWithFlag(FriendFlags.Blocked);
		}

		/// <summary>
		/// Friend requests the local user has <b>sent</b> and that are still awaiting a response.
		/// </summary>
		/// <returns>
		/// A lazily evaluated sequence of users with an outgoing, unanswered request, empty if there
		/// are none.
		/// </returns>
		/// <remarks>
		/// Do not confuse this with <see cref="GetFriendsRequestingFriendship"/>, which is the
		/// opposite direction — requests <em>received</em>, awaiting the local user's answer. The
		/// method names are nearly identical and the two lists are disjoint. This one maps to
		/// <c>k_EFriendFlagFriendshipRequested</c>; that one maps to
		/// <c>k_EFriendFlagRequestingFriendship</c>. If you are building an "invites" badge, the
		/// received list is almost certainly the one you want.
		/// </remarks>
		public static IEnumerable<Friend> GetFriendsRequested()
		{
			return GetFriendsWithFlag( FriendFlags.FriendshipRequested );
		}

		/// <summary>
		/// Users the local user shares a Steam group (clan) with but is not directly friends with —
		/// useful for surfacing "people from your guild" without requiring a friendship.
		/// </summary>
		/// <returns>
		/// A lazily evaluated sequence of fellow clan members, empty if there are none.
		/// </returns>
		/// <remarks>
		/// This is a flat list of people across all the user's groups; it does not tell you which
		/// group anyone came from. To iterate a specific group use <see cref="GetClans"/> and then
		/// <see cref="GetFromSource"/> with that clan's ID. Note Valve's caveat that very large clans
		/// cannot be iterated by the local user at all.
		/// </remarks>
		public static IEnumerable<Friend> GetFriendsClanMembers()
		{
			return GetFriendsWithFlag( FriendFlags.ClanMember );
		}

		/// <summary>
		/// Users the local client knows about because they are on the same game server — the people
		/// currently in the match with you, whether or not they are friends.
		/// </summary>
		/// <returns>
		/// A lazily evaluated sequence of users on the same game server, empty if there are none.
		/// </returns>
		/// <remarks>
		/// Only meaningful when the local user is actually connected to a Steam game server. These
		/// users are typically strangers, so their names and avatars will not be cached — call
		/// <see cref="RequestUserInformation"/> or <c>Friend.RequestInfoAsync</c> for each one before
		/// displaying them, or you will render blanks.
		/// </remarks>
		public static IEnumerable<Friend> GetFriendsOnGameServer()
		{
			return GetFriendsWithFlag( FriendFlags.OnGameServer );
		}

		/// <summary>
		/// Users who have <b>sent the local user a friend request</b> that has not yet been answered.
		/// This is the list behind an "N pending invites" notification.
		/// </summary>
		/// <returns>
		/// A lazily evaluated sequence of users with an incoming, unanswered request, empty if there
		/// are none.
		/// </returns>
		/// <remarks>
		/// The mirror image of <see cref="GetFriendsRequested"/>, which lists requests the local user
		/// sent. The names differ by one word and the direction is the opposite; this is the one that
		/// needs the player's attention. There is no API here to accept or decline — open the overlay
		/// with <see cref="OpenUserOverlay"/> and the <c>"friendrequestaccept"</c> or
		/// <c>"friendrequestignore"</c> dialog.
		/// </remarks>
		public static IEnumerable<Friend> GetFriendsRequestingFriendship()
		{
			return GetFriendsWithFlag( FriendFlags.RequestingFriendship );
		}

		/// <summary>
		/// Users the local user has recently played with, across all games — the "recent players"
		/// list. A good source for "add as friend" or "report player" prompts after a match.
		/// </summary>
		/// <returns>
		/// A lazily evaluated sequence of recent co-players, empty if there are none.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Valve is explicit that this "iterates the entire list of users recently played with,
		/// across games" — entries are not filtered to your app, so expect people the player met in
		/// completely unrelated titles. Valve documents no retention period or maximum length.
		/// </para>
		/// <para>
		/// This list is populated by <see cref="SetPlayedWith"/>, which your game must call itself;
		/// Steam does not infer it from lobby or server membership.
		/// </para>
		/// </remarks>
		public static IEnumerable<Friend> GetPlayedWith()
		{
			var count = Internal.GetCoplayFriendCount();

			for ( int i = 0; i < count; i++ )
			{
				yield return new Friend( Internal.GetCoplayFriend( i ) );
			}
		}

		/// <summary>
		/// Everyone the local client can see inside a given lobby, chat room, game server or Steam
		/// group. This is how you enumerate the members of a specific source rather than a
		/// relationship bucket.
		/// </summary>
		/// <param name="steamid">
		/// The source to enumerate. Valve allows "the steamID of a group, game server, lobby or chat
		/// room" — it is not a user ID. An ID that is not a valid source is not rejected; you simply
		/// get an empty sequence.
		/// </param>
		/// <returns>
		/// A lazily evaluated sequence of the members the local client can see, empty when the source
		/// is unknown, unreadable, or the local user is not entitled to see its members. Emptiness is
		/// not distinguishable from failure.
		/// </returns>
		/// <remarks>
		/// Two caveats straight from Valve: "large clans cannot be iterated by the local user", and
		/// "the current user must be in a lobby to retrieve CSteamIDs of other users in that lobby".
		/// So an empty result is often a permissions or membership problem rather than an empty
		/// source. Members here are usually strangers — request their information before displaying
		/// names or avatars. To test a single user without enumerating, use <c>Friend.IsIn</c>.
		/// </remarks>
		public static IEnumerable<Friend> GetFromSource( SteamId steamid )
		{
		    var count = Internal.GetFriendCountFromSource( steamid );

		    for ( int i = 0; i < count; i++ )
		    {
		        yield return new Friend( Internal.GetFriendFromSourceByIndex( steamid, i ) );
		    }
		}

		/// <summary>
		/// The Steam groups (clans) the local user belongs to, including your app's official game
		/// group if they have joined it. Use it to show guild membership or to find a group chat to
		/// join.
		/// </summary>
		/// <returns>
		/// A lazily evaluated sequence of the user's groups, empty if they belong to none.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Lazy in the same way as <see cref="GetFriends"/>: the count is read once at the start of
		/// enumeration and each entry is a separate native call.
		/// </para>
		/// <para>
		/// Valve notes that for groups the user is a member of "they will have reasonably up-to-date
		/// information", so <c>Clan.Name</c> and <c>Clan.Tag</c> are normally readable straight away
		/// here — unlike clan IDs obtained from elsewhere. Officer information is not loaded until
		/// you await <c>Clan.RequestOfficerList</c>.
		/// </para>
		/// </remarks>
		public static IEnumerable<Clan> GetClans()
		{
			var count = Internal.GetClanCount();

			for ( int i = 0; i < count; i++ )
			{
				yield return new Clan( Internal.GetClanByIndex( i ) );
			}
		}

		/// <summary>
		/// Opens the Steam overlay on one of its standard pages — the way to send the player to the
		/// friends list, settings or achievements without them having to find the overlay hotkey.
		/// </summary>
		/// <param name="type">
		/// Which page to open. Valve's documented values are <c>"friends"</c>, <c>"community"</c>,
		/// <c>"players"</c>, <c>"settings"</c>, <c>"officialgamegroup"</c>, <c>"stats"</c>,
		/// <c>"achievements"</c> and <c>"chatroomgroup/nnnn"</c> (with a numeric group ID appended).
		/// Matching is case-insensitive in practice; Valve's header spells them capitalised.
		/// Passing <see langword="null"/> or an unrecognised string is not validated here and is not
		/// reported — see the remarks.
		/// </param>
		/// <remarks>
		/// This fails silently and completely. It returns <c>void</c>, and the overlay does nothing at
		/// all if it is disabled, if the game is not running through Steam, or if the string is not
		/// one Valve recognises. There is no way to detect any of those cases from here — never make
		/// a UI flow depend on the overlay having appeared. If it does appear,
		/// <see cref="OnGameOverlayActivated"/> fires, which is the closest thing to confirmation.
		/// </remarks>
		public static void OpenOverlay( string type ) => Internal.ActivateGameOverlay( type );

		/// <summary>
		/// Opens the Steam overlay pointed at a specific user — their profile, a chat window with
		/// them, or one of the minimal-mode friend-management prompts. This is how you offer "view
		/// profile", "add friend" or "message" for a player in your game without writing any of that
		/// UI yourself.
		/// </summary>
		/// <param name="id">
		/// The user the dialog acts on. For <c>"chat"</c> this may also be a Steam group ID, in which
		/// case the group chat is joined instead. Not validated.
		/// </param>
		/// <param name="type">
		/// Which dialog to open, from Valve's documented set:
		/// <c>"steamid"</c> opens the overlay web browser to the user's or group's profile;
		/// <c>"chat"</c> opens a chat window to the user, or joins the group chat;
		/// <c>"jointrade"</c> opens a Steam Trading session started via the
		/// <c>ISteamEconomy/StartTrade</c> Web API;
		/// <c>"stats"</c> and <c>"achievements"</c> open the browser to that user's stats or
		/// achievements;
		/// <c>"friendadd"</c>, <c>"friendremove"</c>, <c>"friendrequestaccept"</c> and
		/// <c>"friendrequestignore"</c> open the overlay in minimal mode prompting the local user to
		/// add, remove, accept or ignore. Unrecognised values are not reported.
		/// </param>
		/// <remarks>
		/// Note the argument order is <c>(id, type)</c> here while Valve's native call takes
		/// <c>(pchDialog, steamID)</c> — this binding swaps them, so a straight port from C++ example
		/// code will have the arguments the wrong way round.
		/// Like <see cref="OpenOverlay"/>, this returns <c>void</c> and fails silently when the
		/// overlay is unavailable. The friend-management prompts only ask the user; they do not
		/// perform the action, and there is no callback telling you what the user chose. Detect the
		/// outcome by re-reading <c>Friend.Relationship</c> after
		/// <see cref="OnPersonaStateChange"/> fires.
		/// </remarks>
		public static void OpenUserOverlay( SteamId id, string type ) => Internal.ActivateGameOverlayToUser( type, id );

		/// <summary>
		/// Opens the Steam overlay on a store page, optionally dropping the item straight into the
		/// player's cart. Use it for DLC upsells and bundle promotions from inside the game.
		/// </summary>
		/// <param name="id">
		/// The app or DLC to show. Passing an app ID the store does not recognise opens a broken page
		/// rather than reporting an error.
		/// </param>
		/// <param name="overlayToStoreFlag">
		/// What to do besides showing the page: <see cref="OverlayToStoreFlag.None"/> just displays
		/// it, <see cref="OverlayToStoreFlag.AddToCart"/> adds the item to the cart silently, and
		/// <see cref="OverlayToStoreFlag.AddToCartAndShow"/> adds it and shows the cart. The two
		/// add-to-cart values modify the player's cart as a side effect of opening a page — do not
		/// use them for a plain "learn more" link.
		/// </param>
		/// <remarks>
		/// Returns <c>void</c> and fails silently if the overlay is unavailable, so you cannot tell
		/// whether the store page — or the cart modification — actually happened.
		/// </remarks>
		public static void OpenStoreOverlay( AppId id, OverlayToStoreFlag overlayToStoreFlag = OverlayToStoreFlag.None ) => Internal.ActivateGameOverlayToStore( id.Value, overlayToStoreFlag );

		/// <summary>
		/// Opens an arbitrary web page in the Steam overlay browser — for patch notes, guides,
		/// support pages or anything else you would otherwise have to leave the game to read.
		/// </summary>
		/// <param name="url">
		/// The address to open. Valve requires a "full address with protocol type", for example
		/// <c>http://www.steamgames.com/</c>; a bare host or a relative path will not work. Not
		/// validated here, and a bad URL produces no error.
		/// </param>
		/// <param name="modal">
		/// <see langword="false"/> (the default) opens the page alongside the player's other overlay
		/// windows, where Valve notes it "will remain open, even if the user closes then re-opens the
		/// overlay". <see langword="true"/> opens it in modal mode: all other overlay windows are
		/// hidden, and closing either the window or the overlay closes both.
		/// <b>Modal is also required for custom protocol interception</b> —
		/// <see cref="RegisterProtocolInOverlayBrowser(string)"/> and
		/// <see cref="OnOverlayBrowserProtocol"/> do nothing for a page opened non-modally.
		/// </param>
		/// <remarks>
		/// Returns <c>void</c> and fails silently when the overlay is unavailable.
		/// </remarks>
		public static void OpenWebOverlay( string url, bool modal = false ) => Internal.ActivateGameOverlayToWebPage( url, modal ? ActivateGameOverlayToWebPageMode.Modal : ActivateGameOverlayToWebPageMode.Default );

		/// <summary>
		/// Opens Steam's own "invite friends" dialog, pre-loaded with a lobby, and lets the player
		/// pick who to invite. Preferable to building your own friend picker — it handles friend
		/// filtering, recent players and rate limiting for you.
		/// </summary>
		/// <param name="lobby">
		/// The lobby that invitations will be sent for. This is a lobby ID, not a user ID. There is
		/// no validation: an invalid ID produces a dialog whose invites go nowhere.
		/// </param>
		/// <remarks>
		/// Returns <c>void</c> and fails silently. You are not told whether the dialog opened, who
		/// was invited, or whether anyone accepted — acceptance surfaces on the recipient's machine
		/// as <see cref="OnGameLobbyJoinRequested"/>, and on yours only as a lobby member joining.
		/// </remarks>
		public static void OpenGameInviteOverlay( SteamId lobby ) => Internal.ActivateGameOverlayInviteDialog( lobby );

		/// <summary>
		/// Records that the local user has played with someone, adding them to the Steam "recent
		/// players" list. Call it once per opponent or teammate per session; it is what makes
		/// post-match "add friend" and "report player" flows work, both in your game via
		/// <see cref="GetPlayedWith"/> and in the Steam client itself.
		/// </summary>
		/// <param name="steamid">
		/// The user played with. Not validated.
		/// </param>
		/// <remarks>
		/// Two constraints from Valve: this "is a client-side only feature that requires that the
		/// calling user is in game", and the current user must genuinely be in game with the other
		/// player for the association to take. Called outside those conditions it does nothing, and
		/// since it returns <c>void</c> you are not told. Steam does not populate the recent players
		/// list on its own — if your game never calls this, <see cref="GetPlayedWith"/> stays empty
		/// for your title.
		/// </remarks>
		public static void SetPlayedWith( SteamId steamid ) => Internal.SetPlayedWith( steamid );

		/// <summary>
		/// Asks Steam to download a user's persona name, and optionally their avatar, so that later
		/// reads of <c>Friend.Name</c> or the avatar methods return real data instead of blanks. This
		/// is the fix for the empty-name problem with users who are not on the friends list.
		/// </summary>
		/// <param name="steamid">
		/// The user to fetch. Not validated — an ID Steam cannot resolve simply never produces a
		/// callback.
		/// </param>
		/// <param name="nameonly">
		/// <see langword="true"/> (the default) fetches only the persona name.
		/// <see langword="false"/> also downloads the avatar, which Valve warns is "a lot slower to
		/// download and churns the local cache" — pass <see langword="false"/> only when you are
		/// about to display the picture.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if a download has been started, in which case the data is not ready
		/// yet and you must wait for <see cref="OnPersonaStateChange"/>.
		/// <see langword="false"/> if Steam already has everything requested and you can read the
		/// user's details immediately. Note the polarity: <see langword="false"/> is the success-now
		/// case, not an error.
		/// </returns>
		/// <remarks>
		/// The <paramref name="nameonly"/> flag is part of the question being asked, so the return
		/// value is relative to it — a call with <paramref name="nameonly"/> <see langword="true"/>
		/// can return <see langword="false"/> (name cached) while the same user still has no avatar.
		/// For a simple "wait until it is here" await <c>Friend.RequestInfoAsync</c> or the avatar
		/// helpers on this class instead of polling this yourself. Requires callbacks to be pumped.
		/// </remarks>
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
		/// Downloads a user's 32x32 avatar as raw RGBA pixels. The cheapest of the three sizes and
		/// the right one for friends lists, chat lines and anywhere avatars appear at thumbnail size.
		/// </summary>
		/// <param name="steamid">The <see cref="SteamId"/> of the user to get. Not validated.</param>
		/// <returns>
		/// A <see cref="Data.Image"/> with a value if the image was successfully retrieved, or
		/// <see langword="null"/> when the user has no avatar set or the pixels could not be read.
		/// The two cases are not distinguishable.
		/// </returns>
		/// <remarks>
		/// Downloads the avatar as well as the name, which Valve warns is "a lot slower" and "churns
		/// the local cache" — do not call this for every player you merely observe. Internally this
		/// polls until Steam reports the user's data is cached; the poll has no timeout and no
		/// cancellation, so it only progresses while callbacks are being pumped and never completes
		/// for a Steam ID that never resolves. The returned image is a decoded copy in managed
		/// memory, safe to keep and to upload to a texture.
		/// </remarks>
		public static async Task<Data.Image?> GetSmallAvatarAsync( SteamId steamid )
		{
			await CacheUserInformationAsync( steamid, false );
			return SteamUtils.GetImage( Internal.GetSmallFriendAvatar( steamid ) );
		}

		/// <summary>
		/// Downloads a user's 64x64 avatar as raw RGBA pixels — the middle size, for scoreboards and
		/// player cards where the 32x32 version looks soft.
		/// </summary>
		/// <param name="steamid">The <see cref="SteamId"/> of the user to get. Not validated.</param>
		/// <returns>
		/// A <see cref="Data.Image"/> with a value if the image was successfully retrieved, or
		/// <see langword="null"/> when the user has no avatar set or the pixels could not be read.
		/// </returns>
		/// <remarks>
		/// Same cost and callback-pumping caveats as <see cref="GetSmallAvatarAsync"/>. Unlike
		/// <see cref="GetLargeAvatarAsync"/> there is no second wait for a still-downloading image,
		/// because Valve only documents deferred loading for the large avatar — so a medium avatar
		/// that has not finished downloading comes back as <see langword="null"/> rather than making
		/// you wait.
		/// </remarks>
		public static async Task<Data.Image?> GetMediumAvatarAsync( SteamId steamid )
		{
			await CacheUserInformationAsync( steamid, false );
			return SteamUtils.GetImage( Internal.GetMediumFriendAvatar( steamid ) );
		}

		/// <summary>
		/// Downloads a user's 184x184 avatar as raw RGBA pixels — full resolution, for profile panels
		/// and anywhere the avatar is shown large.
		/// </summary>
		/// <param name="steamid">The <see cref="SteamId"/> of the user to get. Not validated.</param>
		/// <returns>
		/// A <see cref="Data.Image"/> with a value if the image was successfully retrieved, or
		/// <see langword="null"/> when the user has no avatar set or the pixels could not be read.
		/// </returns>
		/// <remarks>
		/// The slowest of the three and the one most likely to block. Valve documents that the
		/// underlying call "returns -1 if this image has yet to be loaded, in this case wait for a
		/// AvatarImageLoaded_t callback and then call this again". Rather than expose that callback,
		/// this binding polls until the handle stops being <c>-1</c>. That poll has no timeout and no
		/// cancellation: a user with no avatar returns handle <c>0</c> and exits cleanly, but a
		/// download that stalls leaves the returned task pending forever. Do not await it on a path
		/// that must make progress, and make sure callbacks are being pumped.
		/// </remarks>
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
		/// Reads back a rich presence value this process previously published with
		/// <see cref="SetRichPresence"/> — useful for "what did I already set?" checks before
		/// overwriting a key.
		/// </summary>
		/// <param name="key">
		/// The key to look up. Case-sensitive dictionary lookup. Must not be <see langword="null"/> —
		/// unlike most calls in this class, a null key throws
		/// <see cref="System.ArgumentNullException"/> rather than returning nothing.
		/// </param>
		/// <returns>
		/// The value previously set for this key in this process, or <see langword="null"/> if it was
		/// never set here.
		/// </returns>
		/// <remarks>
		/// <para>
		/// <b>This does not ask Steam anything.</b> It is a lookup in a local dictionary that this
		/// class maintains as a side effect of <see cref="SetRichPresence"/> succeeding — there is no
		/// native call behind it. Consequences: it only knows about keys set through this API in the
		/// current process; it is empty immediately after <c>SteamClient.Init</c> even if Steam still
		/// holds rich presence from a previous run; and it can drift from Steam's own state, since
		/// Steam clears rich presence when the game exits while nothing re-syncs the dictionary.
		/// </para>
		/// <para>
		/// To read <em>another</em> user's rich presence you want <c>Friend.GetRichPresence</c>,
		/// which is a different method on a different type and does make a native call. The two share
		/// a name and do unrelated things.
		/// </para>
		/// </remarks>
		public static string GetRichPresence( string key )
		{
			if ( richPresence.TryGetValue( key, out var val ) )
				return val;

			return null;
		}

		/// <summary>
		/// Publishes a rich presence key/value for the local user, which is how friends in the same
		/// game see what you are doing and how they get the information needed to join you. Setting
		/// the <c>"connect"</c> key is what makes "Join game" appear next to your name in the Steam
		/// friends list.
		/// </summary>
		/// <param name="key">
		/// The key to set. Valve caps keys at 64 characters
		/// (<c>k_cchMaxRichPresenceKeyLength</c>) and allows at most 30 keys per user
		/// (<c>k_cchMaxRichPresenceKeys</c>). Five keys are special: <c>"status"</c> (a UTF-8 string
		/// shown in the 'view game info' dialog), <c>"connect"</c> (the command line a friend needs
		/// to connect), <c>"steam_display"</c> (names a localization token shown in the viewer's own
		/// language), and <c>"steam_player_group"</c> / <c>"steam_player_group_size"</c> for
		/// grouping players together in the Steam UI.
		/// </param>
		/// <param name="value">
		/// The value, capped by Valve at 256 characters
		/// (<c>k_cchMaxRichPresenceValueLength</c>). Passing <see langword="null"/> or an empty
		/// string <b>deletes the key</b> rather than setting it blank — but note this binding then
		/// records that null or empty string in its local cache rather than removing the entry, so a
		/// subsequent <see cref="GetRichPresence"/> reports the key as present with an empty value.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if Steam accepted the value. <see langword="false"/> means it was
		/// rejected — exceeding the key count, key length or value length limits are the documented
		/// causes — and in that case the local cache is left untouched. Valve does not distinguish
		/// the failure reasons, so you cannot tell which limit you hit.
		/// </returns>
		/// <remarks>
		/// Rich presence is only visible to friends who are in the same game, so this is not a
		/// general broadcast channel. Recipients learn of changes through
		/// <see cref="OnFriendRichPresenceUpdate"/> and read values with
		/// <c>Friend.GetRichPresence</c>. Values are strings only; there is no structured payload.
		/// </remarks>
		public static bool SetRichPresence( string key, string value )
		{
			bool success = Internal.SetRichPresence( key, value );

			if ( success ) 
				richPresence[key] = value;

			return success;
		}

		/// <summary>
		/// Removes every rich presence key the local user has published, so friends stop seeing a
		/// status line and any "Join game" option disappears. Call it when leaving a match or
		/// returning to the main menu so stale connect strings are not left pointing at a server the
		/// player has left.
		/// </summary>
		/// <remarks>
		/// Clears both Steam's copy and the local cache that <see cref="GetRichPresence"/> reads, so
		/// the two stay consistent. Returns <c>void</c> and cannot report failure. Steam also clears
		/// rich presence automatically when the game exits, so this is for mid-session transitions
		/// rather than shutdown.
		/// </remarks>
		public static void ClearRichPresence()
		{
			richPresence.Clear();
			Internal.ClearRichPresence();
		}

		static bool _listenForFriendsMessages;

		/// <summary>
		/// Whether this game is intercepting the local user's one-to-one Steam friend chat. Set it to
		/// <see langword="true"/> and incoming messages start arriving on
		/// <see cref="OnChatMessage"/>, so you can render Steam chat inside your own UI — a Blizzard
		/// style chat box, or the chat system in Dota 2 — instead of making the player open the
		/// overlay.
		/// </summary>
		/// <value>
		/// <see langword="true"/> while interception is enabled. The getter returns a locally cached
		/// flag, not Steam's own state: it reflects what was last assigned here, and it is
		/// <see langword="false"/> after a re-init even if you had enabled it before, because the
		/// backing field is static and the native flag is not re-applied by initialisation.
		/// </value>
		/// <remarks>
		/// <para>
		/// The setter's return value is discarded. Valve's <c>SetListenForFriendsMessages</c> returns
		/// a <c>bool</c>, but a property setter cannot surface it — so if Steam refuses, the cached
		/// getter still reports <see langword="true"/> while no messages ever arrive. If that matters,
		/// treat the first <see cref="OnChatMessage"/> as your only real confirmation.
		/// </para>
		/// <para>
		/// This is opt-in because enabling it makes your game responsible for showing the messages;
		/// the player will not see them anywhere else while you are intercepting. Turn it off when
		/// your chat UI is not available. It governs friend chat only — group chat is a separate
		/// channel, entered with <see cref="JoinClanChatRoom"/> and delivered on
		/// <see cref="OnClanChatMessage"/>.
		/// </para>
		/// </remarks>
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
		/// Asks the Steam backend whether the local user follows a given account. Following is the
		/// one-way Steam Community relationship — you can follow someone without being their friend —
		/// so this answers a different question from <c>Friend.IsFriend</c>.
		/// </summary>
		/// <param name="steamID">The <see cref="SteamId"/> to check. Not validated.</param>
		/// <returns>
		/// <see langword="true"/> if the local user follows that account, otherwise
		/// <see langword="false"/>.
		/// </returns>
		/// <remarks>
		/// <para>
		/// This is a round trip to Steam, not a local lookup, so it needs callbacks to be pumped and
		/// should not be called per frame or per list row.
		/// </para>
		/// <para>
		/// <b>Failure is not returned, it is thrown.</b> Valve's result carries an <c>EResult</c> that
		/// this binding does not inspect, and if the API call itself fails the awaited result is
		/// <see langword="null"/> and the immediate <c>.Value</c> access raises
		/// <see cref="System.InvalidOperationException"/> ("Nullable object must have a value") out of
		/// the returned task. So a network or Steam-side failure surfaces as an exception rather than
		/// <see langword="false"/>. Wrap the await in a <c>try</c> if you call it on a path that must
		/// not fault.
		/// </para>
		/// </remarks>
		public static async Task<bool> IsFollowing(SteamId steamID)
		{
			var r = await Internal.IsFollowing(steamID);
			return r.Value.IsFollowing;
		}

		/// <summary>
		/// How many Steam Community accounts follow a given user. Typically used for showing a
		/// creator's or a clan owner's reach.
		/// </summary>
		/// <param name="steamID">The <see cref="SteamId"/> whose followers to count. Not validated.</param>
		/// <returns>
		/// The number of followers. Valve documents no upper bound and no sentinel for "unknown"; a
		/// user genuinely with no followers and a request that returned nothing useful both read as
		/// <c>0</c>.
		/// </returns>
		/// <remarks>
		/// A backend round trip, so callbacks must be pumped. It carries the same failure behaviour as
		/// <see cref="IsFollowing"/>: the <c>EResult</c> is ignored, and a failed API call makes the
		/// awaited value <see langword="null"/>, so the returned task faults with
		/// <see cref="System.InvalidOperationException"/> instead of yielding a count.
		/// </remarks>
		public static async Task<int> GetFollowerCount(SteamId steamID)
		{
			var r = await Internal.GetFollowerCount(steamID);
			return r.Value.Count;
		}

        /// <summary>
        /// Every account the local user follows, fetched page by page until Steam has no more. Use it
        /// to build a "creators you follow" list or to cross-reference against players in your game.
        /// </summary>
        /// <returns>
        /// An array of the followed accounts, empty if the user follows nobody. There is no failure
        /// signal: if a page request fails the loop stops and you get whatever was collected so far,
        /// which for a first-page failure is an empty array indistinguishable from "follows nobody".
        /// </returns>
        /// <remarks>
        /// <para>
        /// Unlike the lazy enumerators on this class, this materialises everything before returning
        /// and issues one backend round trip per page — Steam returns at most 50 entries per page
        /// (<c>k_cEnumerateFollowersMax</c>). A user following hundreds of accounts costs several
        /// sequential round trips, so cache the result rather than calling it repeatedly. Callbacks
        /// must be pumped throughout or the returned task never completes.
        /// </para>
        /// <para>
        /// This is the inverse of <see cref="GetFollowerCount"/>: that counts who follows a user, this
        /// lists who the local user follows. There is no API here to enumerate another user's
        /// following list, nor to follow or unfollow anyone — following is done through the Steam
        /// Community, not the game.
        /// </para>
        /// </remarks>
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

                    var page = result.Value;
                    AddFollowedIds( ref page, steamIds );
                }
            } while (result != null && resultCount < result.Value.TotalResultCount);

            return steamIds.ToArray();
        }

		/// <summary>
		/// k_cEnumerateFollowersMax - m_rgSteamID is always this long, and Steam zeroes the
		/// entries it did not fill.
		/// </summary>
		private const int EnumerateFollowersMax = 50;

		/// <summary>
		/// Collects the non-zero ids out of one page of results. m_rgSteamID is a fixed size
		/// buffer held inline in the struct, so it has to be pinned before it can be read -
		/// and <paramref name="page"/> is taken by reference precisely so it can be.
		/// </summary>
		/// <remarks>
		/// The native member is <c>CSteamID[50]</c>. <c>CSteamID</c> is declared inside
		/// <c>#pragma pack( push, 1 )</c> (<c>steamclientpublic.h:475</c>), so the array is
		/// byte-aligned: entry <c>i</c> starts at byte <c>i * 8</c> from a base that itself need
		/// not be 8-byte aligned. That is why the buffer is held as raw bytes rather than as
		/// <c>ulong</c>, whose alignment would have pushed the whole array off its native
		/// offset. Each id is reassembled a byte at a time so no alignment is assumed;
		/// little-endian, matching every platform this binding ships for.
		/// </remarks>
		private static unsafe void AddFollowedIds( ref FriendsEnumerateFollowingList_t page, List<SteamId> steamIds )
		{
			fixed ( byte* ids = page.GSteamID )
			{
				for ( var i = 0; i < EnumerateFollowersMax; i++ )
				{
					var entry = ids + i * sizeof( ulong );

					ulong id = 0;
					for ( var b = sizeof( ulong ) - 1; b >= 0; b-- )
						id = (id << 8) | entry[b];

					if ( id > 0 )
						steamIds.Add( id );
				}
			}
		}

		/// <summary>
		/// Claims a custom URI scheme inside the Steam overlay browser, so that a web page you host
		/// can hand control back to your game. Once registered, the overlay blocks navigations to that
		/// scheme and raises <see cref="OnOverlayBrowserProtocol"/> with the full URI instead of
		/// trying to load it. This is the standard trick for in-overlay purchase or login flows that
		/// need to signal completion to the game.
		/// </summary>
		/// <param name="protocol">
		/// The scheme to claim, without the <c>://</c> — for example <c>"mygame"</c> so that
		/// <c>mygame://checkout-complete</c> is intercepted. Valve documents no restrictions on the
		/// string and this binding validates nothing; claiming a well-known scheme such as
		/// <c>"http"</c> is not rejected here and would be a bad idea.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the scheme was registered. Valve documents no failure reasons,
		/// so a <see langword="false"/> gives you nothing to act on.
		/// </returns>
		/// <remarks>
		/// Order and mode both matter. Valve requires that you "call this before calling
		/// ActivateGameOverlayToWebPage()", and — the part that is easy to miss — that
		/// "ActivateGameOverlayToWebPage() must have been called with
		/// k_EActivateGameOverlayToWebPageMode_Modal". In this binding that means you must open the
		/// page with <c>OpenWebOverlay( url, modal: true )</c>. Register after opening the page, or
		/// open it non-modally, and this call still returns <see langword="true"/> while
		/// <see cref="OnOverlayBrowserProtocol"/> never fires — the failure is entirely silent.
		/// </remarks>
		public static bool RegisterProtocolInOverlayBrowser( string protocol )
        {
			return Internal.RegisterProtocolInOverlayBrowser( protocol );
        }

		/// <summary>
		/// Joins a Steam group's chat room from inside the game, which is what enables
		/// <see cref="OnClanChatMessage"/> for that room and lets you send to it with
		/// <see cref="SendClanChatRoomMessage"/>. Use it to surface a guild or community chat in your
		/// own UI.
		/// </summary>
		/// <param name="chatId">
		/// The Steam ID of the group whose chat to join — a clan ID, as obtained from
		/// <see cref="GetClans"/>, not a user ID.
		/// </param>
		/// <returns>
		/// <see langword="true"/> only when Steam reports the room was entered successfully.
		/// <see langword="false"/> collapses two quite different situations: the API call failed
		/// outright, or it completed and Steam refused entry. Valve's <c>EChatRoomEnterResponse</c>
		/// distinguishes the refusals — <see cref="RoomEnter.Banned"/>, <see cref="RoomEnter.Full"/>,
		/// <see cref="RoomEnter.NotAllowed"/>, <see cref="RoomEnter.RatelimitExceeded"/> and others —
		/// but this binding discards the code, so you cannot tell the user why they could not get in.
		/// </returns>
		/// <remarks>
		/// <para>
		/// A backend round trip; callbacks must be pumped or the task never completes. Valve notes
		/// the behaviour is "somewhat sophisticated, because the user may or may not be already in the
		/// group chat from outside the game or in the overlay" — joining here does not disturb that.
		/// </para>
		/// <para>
		/// This binding exposes no way to leave a room again (Valve's <c>LeaveClanChatRoom</c> is not
		/// surfaced), so a room joined this way stays joined for the life of the process.
		/// </para>
		/// </remarks>
		public static async Task<bool> JoinClanChatRoom( SteamId chatId )
		{
			var result = await Internal.JoinClanChatRoom( chatId );
			if ( !result.HasValue )
				return false;

			return result.Value.ChatRoomEnterResponse == RoomEnter.Success ;
		}

		/// <summary>
		/// Posts a message to a Steam group chat room the game has joined, so players can talk to
		/// their guild from inside your UI. The outgoing half of <see cref="OnClanChatMessage"/>.
		/// </summary>
		/// <param name="chatId">
		/// The group chat to post to. This must be a room a successful
		/// <see cref="JoinClanChatRoom"/> has already entered — sending to a room you are not in
		/// fails rather than joining it implicitly.
		/// </param>
		/// <param name="message">
		/// The message text, UTF-8. Valve documents no length limit for this call and this binding
		/// imposes none; note that incoming messages are read back through a 32 KB buffer, so
		/// anything longer round-trips badly.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if Steam accepted the message for sending. Valve documents no
		/// failure reasons, so <see langword="false"/> gives you nothing to act on beyond "it did not
		/// send" — not being in the room, and the local user being chat restricted, are the likely
		/// causes. Acceptance is not delivery; there is no confirmation callback.
		/// </returns>
		/// <remarks>
		/// This is the group-chat counterpart to <c>Friend.SendMessage</c>, which sends to a single
		/// user. They target different Steam channels and are not interchangeable — the first
		/// argument here is a clan chat ID, not a user ID.
		/// </remarks>
		public static bool SendClanChatRoomMessage( SteamId chatId, string message )
		{
			return Internal.SendClanChatMessage( chatId, message );
		}

		/// <summary>
		/// Reads a user's Steam display name without having to construct a <see cref="Friend"/> — a
		/// convenience for code that already holds a raw <see cref="SteamId"/>, such as a callback
		/// payload or a network packet.
		/// </summary>
		/// <param name="steamId">The <see cref="SteamId"/> whose name to read. Not validated.</param>
		/// <returns>
		/// The persona name in UTF-8, or an empty string for a user Steam has not cached. Valve
		/// guarantees the native call is "not NULL", so you get <c>""</c> rather than
		/// <see langword="null"/> for an unknown user — and some Steam client versions substitute the
		/// numeric Steam ID. Either way it means "not downloaded yet", not "this user has no name".
		/// </returns>
		/// <remarks>
		/// <para>
		/// Identical in behaviour to <c>Friend.Name</c> — same native call, same caching rules — so
		/// the data-availability warning applies in full: for anyone not on the local user's friends
		/// list the name arrives asynchronously and this returns empty until it does. Call
		/// <see cref="RequestUserInformation"/> first, or read it again after
		/// <see cref="OnPersonaStateChange"/> fires.
		/// </para>
		/// <para>
		/// Valve returns a <c>const char *</c> here and documents nothing about its lifetime. This
		/// binding copies the bytes into a managed <see cref="string"/> before returning, so the value
		/// you get is safe to keep indefinitely. Assume — an assumption, not documented behaviour —
		/// that the native pointer is invalidated by the next call on this interface; Valve's general
		/// warning on <c>GetPersonaName</c> that such a pointer "will eventually be free'd or
		/// re-allocated" is the closest thing to a stated rule.
		/// </para>
		/// </remarks>
		public static string GetFriendPersonaName(SteamId steamId)
		{
			return Internal.GetFriendPersonaName( steamId );
		}
	}
}
