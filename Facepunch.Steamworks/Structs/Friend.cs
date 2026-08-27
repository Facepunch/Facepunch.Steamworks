using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// A handle to a Steam user that the local client knows something about. Despite the name this
	/// is not limited to actual friends — lobby members, players on the same game server, clan
	/// members and blocked accounts all surface as a <see cref="Friend"/>. Use it to read a user's
	/// display name, online state, avatar, what they are playing and their rich presence, and to
	/// invite or message them.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This struct stores nothing but the <see cref="Id"/>. Every property below is a fresh call
	/// into the Steam client each time you read it, so it is cheap to keep a <see cref="Friend"/>
	/// around forever but not free to read one in a tight loop.
	/// </para>
	/// <para>
	/// <b>The data-availability trap.</b> Steam only answers questions about users it currently has
	/// cached. Valve states this on <c>GetFriendPersonaState</c>: the state "will only be known by
	/// the local user if steamIDFriend is in their friends list; on the same game server; in a chat
	/// room or lobby; or in a small group with the local user", and on <c>GetFriendPersonaName</c>
	/// that "on first joining a lobby, chat room or game server the local user will not know the
	/// name of the other users automatically; that information will arrive asynchronously". For an
	/// uncached user <see cref="Name"/> comes back empty (or as the numeric ID in some Steam client
	/// versions), <see cref="State"/> reads <see cref="FriendState.Offline"/>, and avatars are
	/// blank. Nothing throws and nothing tells you the read failed. Await
	/// <see cref="RequestInfoAsync"/> before trusting the values, or drive your UI from
	/// <c>SteamFriends.OnPersonaStateChange</c>.
	/// </para>
	/// <para>
	/// Data only arrives while callbacks are being pumped. If you initialised with
	/// <c>SteamClient.Init( appid, asyncCallbacks: false )</c> you must call
	/// <see cref="SteamClient.RunCallbacks"/> every frame or the awaits here never complete.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// foreach ( var friend in SteamFriends.GetFriends() )
	/// {
	///     // Steam may not have this user cached yet - the name would come back empty.
	///     if ( string.IsNullOrEmpty( friend.Name ) )
	///         await friend.RequestInfoAsync();
	///
	///     Console.WriteLine( $"{friend.Name} is {friend.State}" );
	///
	///     if ( !friend.IsPlayingThisGame )
	///         continue;
	///
	///     // GameInfo is a fresh native call every read - take one copy.
	///     var game = friend.GameInfo;
	///     if ( game?.Lobby is { } lobby &amp;&amp; await lobby.Join() == RoomEnter.Success )
	///         Console.WriteLine( $"Joined {friend.Name}" );
	/// }
	/// </code>
	/// </example>
	public struct Friend
	{
		/// <summary>
		/// The Steam ID this handle refers to. This is the only state the struct carries — it is
		/// never validated, so a <see cref="Friend"/> built from a bogus ID is constructed happily
		/// and every property on it simply returns empty or default values.
		/// </summary>
		public SteamId Id;

		/// <summary>
		/// Wraps a Steam ID so the rest of this struct's helpers can be used on it. No network or
		/// cache lookup happens here, so this never blocks and never fails.
		/// </summary>
		/// <param name="steamid">
		/// The user to refer to. Not checked for validity, and the user does not need to be a
		/// friend or even known to the local client — see the type remarks about reads on an
		/// uncached user silently returning empty values.
		/// </param>
		public Friend( SteamId steamid )
		{
			Id = steamid;
		}

		/// <summary>
		/// A debugging-friendly rendering of this user, of the form <c>Name (76561197960287930)</c>.
		/// </summary>
		/// <returns>
		/// The persona name followed by the numeric ID in brackets. Because this reads
		/// <see cref="Name"/>, an uncached user renders with an empty name — as
		/// <c> (76561197960287930)</c> — rather than failing. Intended for logs, not for display in
		/// your UI.
		/// </returns>
		public override string ToString()
		{
			return $"{Name} ({Id.ToString()})";
		}


		/// <summary>
		/// Whether this handle points at the signed-in local user rather than someone else. Useful
		/// for filtering yourself out of lobby and clan member lists, which do include you.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if <see cref="Id"/> matches the logged-in account.
		/// </returns>
		/// <remarks>
		/// Reads <see cref="SteamClient.SteamId"/>, so the Steam API must already be initialised —
		/// calling this before <c>SteamClient.Init</c> is not meaningful.
		/// </remarks>
		public bool IsMe => Id == SteamClient.SteamId;

		/// <summary>
		/// Whether this user is a confirmed, mutual friend of the local user. Pending invites in
		/// either direction do not count, so this is the check you want before treating someone as
		/// a real friend rather than "someone we happen to know about".
		/// </summary>
		/// <returns>
		/// <see langword="true"/> only for <see cref="Relationship.Friend"/>. A user with a friend
		/// request outstanding (<see cref="Relationship.RequestInitiator"/> or
		/// <see cref="Relationship.RequestRecipient"/>) returns <see langword="false"/>.
		/// </returns>
		public bool IsFriend => Relationship == Relationship.Friend;

		/// <summary>
		/// Whether the local user has blocked this account — the signal to hide their name, avatar
		/// and chat in your UI.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> only for <see cref="Relationship.Blocked"/>.
		/// </returns>
		/// <remarks>
		/// Steam has two distinct notions of blocking and this property only covers one of them.
		/// Valve's header describes <c>k_EFriendRelationshipBlocked</c> as "this doesn't get stored;
		/// the user has just done an Ignore on a friendship invite", whereas
		/// <see cref="Relationship.Ignored"/> is the persistent block ("the user has explicitly
		/// blocked this other user from comments/chat/etc"). If you are moderating chat you almost
		/// certainly want to test for <see cref="Relationship.Ignored"/> and
		/// <see cref="Relationship.IgnoredFriend"/> as well as this property.
		/// </remarks>
		public bool IsBlocked => Relationship == Relationship.Blocked;

		/// <summary>
		/// Whether this user is right now playing the same app the local client is running — the
		/// usual gate for showing "join game" affordances.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> only when the friend is in a game, that game is a plain Steam app
		/// (<see cref="GameIdType.App"/>), and its app ID equals <see cref="SteamClient.AppId"/>.
		/// A friend running a mod of your game (<see cref="GameIdType.GameMod"/>) or a non-Steam
		/// shortcut (<see cref="GameIdType.Shortcut"/>) returns <see langword="false"/>, as does
		/// a friend whose game Steam has not told us about yet.
		/// </returns>
		/// <remarks>
		/// This evaluates <see cref="GameInfo"/> twice, so it costs two native calls. If you are
		/// about to read <see cref="GameInfo"/> anyway, read it once into a local and test the
		/// app ID yourself instead of calling this.
		/// </remarks>
		public bool IsPlayingThisGame => GameInfo?.GameID is { Type: GameIdType.App } && GameInfo.Value.GameID.AppId == SteamClient.AppId;

		/// <summary>
		/// Whether Steam reports this user as signed in, in any state — online, busy, away, snoozing
		/// or looking to trade/play. Use it for the green/grey dot in a friends list.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> for any <see cref="State"/> other than
		/// <see cref="FriendState.Offline"/>.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Two things make <see langword="false"/> ambiguous. First, a user the local client has no
		/// cached data for also reads as <see cref="FriendState.Offline"/> — see the type remarks.
		/// Second, Valve notes that <c>k_EPersonaStateInvisible</c> "is never published to clients",
		/// so a friend who is online but invisible is genuinely indistinguishable from one who is
		/// offline.
		/// </para>
		/// <para>
		/// Note that being online is not the same as being reachable in your game; use
		/// <see cref="IsPlayingThisGame"/> for that.
		/// </para>
		/// </remarks>
		public bool IsOnline => State != FriendState.Offline;

		/// <summary>
		/// Waits until Steam has downloaded this user's persona name, then returns. This is the fix
		/// for the empty-<see cref="Name"/> problem described in the type remarks: call it once for
		/// any user who did not come from your own friends list — lobby members, players on a game
		/// server — before you show their name.
		/// </summary>
		/// <returns>
		/// A task that completes once the name is cached locally. It carries no success flag: if
		/// the ID does not resolve you simply get a completed task and <see cref="Name"/> stays
		/// empty.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Requests the name only, not the avatar — Valve warns that "it's a lot slower to download
		/// avatars and churns the local cache". Use the <c>GetAvatar</c> methods on this struct if
		/// you need the picture; they request avatar data themselves.
		/// </para>
		/// <para>
		/// The wait is a poll loop with no timeout and no cancellation, so it only completes while
		/// callbacks are being pumped. If Steam never resolves the user this task never completes —
		/// do not <c>await</c> it on a path that must make progress.
		/// </para>
		/// </remarks>
		public async Task RequestInfoAsync()
		{
			await SteamFriends.CacheUserInformationAsync( Id, true );
		}

		/// <summary>
		/// Whether Steam has auto-flagged this user as away — the client sets this itself after a
		/// short idle period, so it is a weaker signal than the user having deliberately set a
		/// status.
		/// </summary>
		/// <returns><see langword="true"/> only for <see cref="FriendState.Away"/>.</returns>
		public bool IsAway => State == FriendState.Away;

		/// <summary>
		/// Whether this user has explicitly marked themselves busy. Unlike <see cref="IsAway"/> and
		/// <see cref="IsSnoozing"/> this is a deliberate choice by the user, so it is a reasonable
		/// signal to suppress invites and notifications.
		/// </summary>
		/// <returns><see langword="true"/> only for <see cref="FriendState.Busy"/>.</returns>
		public bool IsBusy => State == FriendState.Busy;

		/// <summary>
		/// Whether Steam has auto-flagged this user as snoozing. Valve describes this as "auto-away
		/// for a long time" — it is the same mechanism as <see cref="IsAway"/> after a longer idle
		/// period, not a separate user-set status.
		/// </summary>
		/// <returns><see langword="true"/> only for <see cref="FriendState.Snooze"/>.</returns>
		public bool IsSnoozing => State == FriendState.Snooze;



		/// <summary>
		/// How this user relates to the local user — friend, blocked, ignored, or an invite pending
		/// in one direction or the other. This is the raw value behind <see cref="IsFriend"/> and
		/// <see cref="IsBlocked"/>; read it directly when you need to tell an incoming friend
		/// request from an outgoing one.
		/// </summary>
		/// <returns>
		/// The relationship, or <see cref="Relationship.None"/> for a user the local client has no
		/// relationship data for. <see cref="Relationship.None"/> is therefore ambiguous between
		/// "definitely a stranger" and "we have not been told yet".
		/// </returns>
		/// <remarks>
		/// Valve documents nothing beyond "returns a relationship to a user"; the per-value meanings
		/// quoted elsewhere in this file come from the comments on <c>EFriendRelationship</c> in
		/// <c>isteamfriends.h</c>. <see cref="Relationship.Suggested_DEPRECATED"/> is dead — Valve
		/// marks it "was used by the original implementation of the facebook linking feature, but
		/// now unused" — and <see cref="Relationship.Max"/> is a bounds marker, not a real state.
		/// Changes arrive through <c>SteamFriends.OnPersonaStateChange</c>.
		/// </remarks>
		public Relationship Relationship => SteamFriends.Internal.GetFriendRelationship( Id );

		/// <summary>
		/// The user's current online status — offline, online, busy, away, snoozing, or one of the
		/// "looking to trade/play" states. This is the raw value behind <see cref="IsOnline"/>,
		/// <see cref="IsAway"/>, <see cref="IsBusy"/> and <see cref="IsSnoozing"/>.
		/// </summary>
		/// <returns>
		/// The persona state, or <see cref="FriendState.Offline"/> when the local client has no
		/// data for this user. There is no separate "unknown" value, so offline and uncached are
		/// indistinguishable here.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Valve is explicit that this "will only be known by the local user if steamIDFriend is in
		/// their friends list; on the same game server; in a chat room or lobby; or in a small group
		/// with the local user". For anyone else the answer is meaningless rather than merely stale.
		/// </para>
		/// <para>
		/// <see cref="FriendState.Invisible"/> is documented by Valve as "never published to
		/// clients", so you should not expect to observe it for other users. Updates are delivered
		/// through <c>SteamFriends.OnPersonaStateChange</c>, which requires callbacks to be pumped.
		/// </para>
		/// </remarks>
		public FriendState State => SteamFriends.Internal.GetFriendPersonaState( Id );

		/// <summary>
		/// The name to show for this player — their current Steam persona name, which is what
		/// appears on their community profile. This is the display name you want in almost every
		/// case; prefer it over <see cref="Nickname"/>.
		/// </summary>
		/// <returns>
		/// The persona name in UTF-8, or an empty string for a user Steam has not cached. Valve
		/// guarantees the native call is "not NULL", so you get <c>""</c> rather than
		/// <see langword="null"/> when the name is unknown — and some Steam client versions return
		/// the numeric Steam ID as a placeholder instead. Either way an empty or numeric-looking
		/// name means "not downloaded yet", not "this user has no name".
		/// </returns>
		/// <remarks>
		/// <para>
		/// This is the single most common source of confusion in the Steam friends API. For anyone
		/// who is not on the local user's friends list the name arrives asynchronously — Valve:
		/// "on first joining a lobby, chat room or game server the local user will not know the name
		/// of the other users automatically; that information will arrive asynchronously." Await
		/// <see cref="RequestInfoAsync"/> first, or refresh your UI from
		/// <c>SteamFriends.OnPersonaStateChange</c>.
		/// </para>
		/// <para>
		/// Steam folds nicknames into this value when "Append nicknames to friends' names" is
		/// disabled in the Steam client, so what you get here is a user-facing display name and not
		/// necessarily the account's own persona name.
		/// </para>
		/// <para>
		/// Valve returns a <c>const char *</c> here and documents nothing about how long it stays
		/// valid for this particular function. This binding copies the bytes into a managed
		/// <see cref="string"/> before returning, so the value you receive is yours and outlives any
		/// native buffer. Assume — this is an assumption, not documented behaviour — that the
		/// underlying pointer is invalidated by the next call on the same interface; the general
		/// warning Valve gives on <c>GetPersonaName</c> ("it's important that this pointer is not
		/// saved off; it will eventually be free'd or re-allocated") is the closest thing to a rule.
		/// </para>
		/// </remarks>
		public string Name => SteamFriends.Internal.GetFriendPersonaName( Id );

		/// <summary>
		/// The private nickname the local user has assigned to this player, if they set one. This is
		/// a label local to this Steam account — the other player did not choose it and cannot see it.
		/// </summary>
		/// <returns>
		/// The nickname, or <see langword="null"/> when no nickname is set — Valve returns <c>NULL</c>
		/// in that case and this binding maps a null pointer to <see langword="null"/>. Also
		/// <see langword="null"/> when the client has no cached data for the user, so you cannot tell
		/// "no nickname" from "not downloaded yet".
		/// </returns>
		/// <remarks>
		/// <para>
		/// Valve marks the underlying <c>GetPlayerNickname</c> as deprecated: "GetPersonaName follows
		/// the Steam nickname preferences, so apps shouldn't need to care about nicknames
		/// explicitly." Read <see cref="Name"/> instead unless you specifically need the nickname as
		/// a separate field.
		/// </para>
		/// <para>
		/// Nicknames are only reported separately here when "Append nicknames to friends' names" is
		/// enabled in the Steam client. With that setting disabled Steam never returns a nickname
		/// from this call — it folds it into <see cref="Name"/> instead.
		/// </para>
		/// <para>
		/// As with <see cref="Name"/>, the native <c>const char *</c> is copied into managed memory
		/// immediately, so the returned string is safe to keep; the pointer itself is assumed — not
		/// documented — to be invalidated by the next call on this interface.
		/// </para>
		/// </remarks>
		public string Nickname => SteamFriends.Internal.GetPlayerNickname( Id );

		/// <summary>
		/// The player's previous Steam persona names, most recent first — the same history the Steam
		/// client shows on a profile. Useful for moderation, and for recognising someone who has
		/// recently renamed.
		/// </summary>
		/// <returns>
		/// A lazily evaluated sequence of past names, empty when Steam has no history cached for this
		/// user. There is no error signal: empty means either "no history" or "not downloaded", and
		/// the two are indistinguishable.
		/// </returns>
		/// <remarks>
		/// <para>
		/// This is a lazy iterator — each step is a separate native call, and the sequence is re-read
		/// from scratch on every enumeration. Materialise it (for example with <c>ToArray()</c>) if
		/// you intend to walk it more than once.
		/// </para>
		/// <para>
		/// Valve's only stated contract is that the native call "returns an empty string when there
		/// are no more items in the history"; no maximum is documented. This binding additionally
		/// stops after 32 entries, so a longer history is silently truncated — that cap is imposed
		/// here, not by Steam.
		/// </para>
		/// </remarks>
		public IEnumerable<string> NameHistory
		{
			get
			{
				for( int i=0; i<32; i++ )
				{
					var n = SteamFriends.Internal.GetFriendPersonaNameHistory( Id, i );
					if ( string.IsNullOrEmpty( n ) )
						break;

					yield return n;
				}
			}
		}

		/// <summary>
		/// The player's Steam community level — the badge number shown on their profile. Games use
		/// it as a cheap, coarse trust or account-age signal, since level correlates with money and
		/// time spent on the platform.
		/// </summary>
		/// <returns>
		/// The Steam level, or <c>0</c> when the client has not cached this user's level. Valve
		/// documents no sentinel for "unknown", and a brand new account really is level 0, so
		/// <c>0</c> is genuinely ambiguous — do not treat it as proof of a new account.
		/// </returns>
		/// <remarks>
		/// Valve's entire documentation for the underlying call is the comment "friends steam
		/// level". Level changes are reported through <c>SteamFriends.OnPersonaStateChange</c>;
		/// the corresponding native change flag is <c>k_EPersonaChangeSteamLevel</c>, though this
		/// binding does not surface the flags on that event, so you cannot tell which field changed.
		/// </remarks>
		public int SteamLevel => SteamFriends.Internal.GetFriendSteamLevel( Id );



		/// <summary>
		/// What this user is playing right now, including the server address and lobby you would
		/// need in order to join them. This is the backing data for "join game" buttons in a friends
		/// list.
		/// </summary>
		/// <returns>
		/// A populated <see cref="FriendGameInfo"/> when Valve's <c>GetFriendGamePlayed</c> reports
		/// the friend is in a game, otherwise <see langword="null"/>. <see langword="null"/> covers
		/// both "not playing anything" and "we have no data for this user", which are not
		/// distinguishable here.
		/// </returns>
		/// <remarks>
		/// Every read performs a native call and copies a fresh struct, so assign it to a local
		/// rather than re-reading <c>friend.GameInfo</c> for each field. The returned struct is a
		/// snapshot: it does not update when the friend changes game. Refresh it when
		/// <c>SteamFriends.OnPersonaStateChange</c> fires.
		/// </remarks>
		public FriendGameInfo? GameInfo
		{
			get
			{
				FriendGameInfo_t gameInfo = default;
				if ( !SteamFriends.Internal.GetFriendGamePlayed( Id, ref gameInfo ) )
					return null;

				return FriendGameInfo.From( gameInfo );
			}
		}

		/// <summary>
		/// Whether the local client can see this user as a member of a given group, chat room, lobby
		/// or game server. Use it to check membership without enumerating everyone in the source.
		/// </summary>
		/// <param name="group_or_room">
		/// The Steam ID of the source to test against — Valve allows "a group, game server, lobby or
		/// chat room" here. It is a source ID, not another user's ID; passing a user ID is not
		/// rejected, it simply returns <see langword="false"/>.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the local user can see this user in that source. Valve's
		/// wording is deliberately about visibility, not truth — the answer is
		/// <see langword="false"/> when the local client is not itself in the source, or when the
		/// membership list has not been downloaded, even though the user may really be in there.
		/// </returns>
		/// <remarks>
		/// Valve notes on the related iteration calls that "large clans cannot be iterated by the
		/// local user" and that "the current user must be in a lobby to retrieve CSteamIDs of other
		/// users in that lobby". Both caveats apply to the answer you get here.
		/// </remarks>
		public bool IsIn( SteamId group_or_room )
		{
			return SteamFriends.Internal.IsUserInSource( Id, group_or_room );
		}

		/// <summary>
		/// A snapshot of the game a friend is currently in — which game, which server, and which
		/// lobby. Obtained from <see cref="Friend.GameInfo"/>; this is the managed mirror of Valve's
		/// <c>FriendGameInfo_t</c>.
		/// </summary>
		/// <remarks>
		/// This is a value copied at the moment you read <see cref="Friend.GameInfo"/>. It is not
		/// live and will not track the friend moving between servers or lobbies.
		/// </remarks>
		/// <example>
		/// <code>
		/// var info = friend.GameInfo;
		///
		/// if ( info?.Lobby is { } lobby )
		/// {
		///     await lobby.Join();
		/// }
		/// else if ( info is { } game &amp;&amp; game.IpAddressRaw != 0 )
		/// {
		///     // Only trust QueryPort once you have excluded Valve's two sentinel values.
		///     var query = game.QueryPort is 0xFFFF or 0xFFFE ? game.ConnectionPort : game.QueryPort;
		///     Connect( game.IpAddress, game.ConnectionPort, query );
		/// }
		/// </code>
		/// </example>
		public struct FriendGameInfo
		{
			internal uint GameIP; // m_unGameIP uint32
			internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID

			/// <summary>
			/// Identifies the title the friend is running. Check <c>GameID.Type</c> before comparing
			/// app IDs: the same numeric app ID can belong to a plain Steam app, a mod of it, a
			/// non-Steam shortcut or a P2P entry, and only <see cref="GameIdType.App"/> means "the
			/// real game". <see cref="Friend.IsPlayingThisGame"/> already does this check.
			/// </summary>
			public GameId GameID;

			/// <summary>
			/// The game port on the friend's server — the port a client actually connects to. Paired
			/// with <see cref="IpAddress"/> this is the address to join.
			/// </summary>
			/// <remarks>
			/// Widened from Valve's <c>uint16 m_usGamePort</c>, so the value is always in
			/// <c>0..65535</c> and never negative. Valve documents no sentinel for this field; in
			/// practice <c>0</c> means the friend is not on a game server at all (they may still be
			/// in a lobby — check <see cref="Lobby"/>). That reading is inferred from the field
			/// being zero-initialised, not stated by Valve.
			/// </remarks>
			public int ConnectionPort;

			/// <summary>
			/// The port to send server-browser queries to, which is often not the same as
			/// <see cref="ConnectionPort"/>. Only meaningful for games that run a queryable
			/// dedicated server.
			/// </summary>
			/// <remarks>
			/// This field has two sentinel values that Valve defines in <c>isteamfriends.h</c> and
			/// that survive into this <c>int</c> unchanged, because the native <c>uint16</c> is
			/// widened rather than reinterpreted: <c>65535</c> (<c>0xFFFF</c>,
			/// <c>k_usFriendGameInfoQueryPort_NotInitialized</c>) means "we haven't asked the game
			/// server for this query port's actual value yet", and <c>65534</c> (<c>0xFFFE</c>,
			/// <c>k_usFriendGameInfoQueryPort_Error</c>) means "we were unable to get the query port
			/// for this server". Neither is a usable port. Test for both before you use this value;
			/// they are not surfaced as named constants by this binding.
			/// </remarks>
			public int QueryPort;

			/// <summary>
			/// The friend's game server address as the raw 32-bit value Steam stores, for callers
			/// that want to avoid allocating an <see cref="System.Net.IPAddress"/> — for instance
			/// when comparing against a server address you already hold.
			/// </summary>
			/// <returns>
			/// The address in host byte order as Steam reports it, or <c>0</c> when the friend is
			/// not on a game server. Valve documents no sentinel for <c>m_unGameIP</c>; treating
			/// <c>0</c> as "no server" is inferred from the field being zero-initialised.
			/// </returns>
			public uint IpAddressRaw => GameIP;

			/// <summary>
			/// The friend's game server address, byte-swapped into the order
			/// <see cref="System.Net.IPAddress"/> expects, ready to connect to or display.
			/// </summary>
			/// <returns>
			/// The server address. When the friend is not on a game server the underlying value is
			/// <c>0</c> and this returns <c>0.0.0.0</c> rather than <see langword="null"/> — check
			/// <see cref="IpAddressRaw"/> against <c>0</c> instead of relying on this being absent.
			/// A new object is allocated on every read.
			/// </returns>
			public System.Net.IPAddress IpAddress => Utility.Int32ToIp( GameIP );

			/// <summary>
			/// The Steam lobby the friend is in, if any. Joining the lobby is almost always the right
			/// way to follow a friend into a game — prefer it over connecting straight to
			/// <see cref="IpAddress"/>, since the lobby carries the game's own join logic.
			/// </summary>
			/// <returns>
			/// The lobby, or <see langword="null"/> when Steam reported no lobby (the native field
			/// was zero). A non-null value is not a promise that you may join it — the lobby can be
			/// full, private or gone by the time you call <c>Join</c>, which reports that through its
			/// own return value.
			/// </returns>
			public Lobby? Lobby
			{
				get
				{
					if ( SteamIDLobby == 0 ) return null;
					return new Lobby( SteamIDLobby );
				}
			}

			internal static FriendGameInfo From( FriendGameInfo_t i )
			{
				return new FriendGameInfo
				{
					GameID = i.GameID,
					GameIP = i.GameIP,
					ConnectionPort = i.GamePort,
					QueryPort = i.QueryPort,
					SteamIDLobby = i.SteamIDLobby,
				};
			}
		}

		/// <summary>
		/// Downloads this user's 32x32 avatar as raw RGBA pixels. This is the size to use for
		/// friends lists and chat lines — it is the cheapest of the three to fetch and the least
		/// disruptive to Steam's local avatar cache.
		/// </summary>
		/// <returns>
		/// The decoded image, or <see langword="null"/> if the user has no avatar set or the image
		/// could not be read. The two cases are not distinguishable. Note the avatar-not-yet-loaded
		/// case is handled by waiting, not by returning <see langword="null"/> — see the remarks.
		/// </returns>
		/// <remarks>
		/// Unlike <see cref="RequestInfoAsync"/> this requests avatar data as well as the name, which
		/// Valve warns is "a lot slower to download" and "churns the local cache" — do not call it
		/// for every player you merely see. The wait is a poll loop with no timeout or cancellation,
		/// so it only progresses while callbacks are being pumped and never completes if Steam never
		/// resolves the user.
		/// </remarks>
		public async Task<Data.Image?> GetSmallAvatarAsync()
		{
			return await SteamFriends.GetSmallAvatarAsync( Id );
		}

		/// <summary>
		/// Downloads this user's 64x64 avatar as raw RGBA pixels — the middle size, suitable for
		/// scoreboards and player cards where the 32x32 version would look soft.
		/// </summary>
		/// <returns>
		/// The decoded image, or <see langword="null"/> if the user has no avatar set or the image
		/// could not be read.
		/// </returns>
		/// <remarks>
		/// Same cost and callback-pumping caveats as <see cref="GetSmallAvatarAsync"/>. Unlike
		/// <see cref="GetLargeAvatarAsync"/> this does not have a second wait loop for a
		/// still-downloading image, because Valve only documents the deferred-load behaviour for the
		/// large avatar.
		/// </remarks>
		public async Task<Data.Image?> GetMediumAvatarAsync()
		{
			return await SteamFriends.GetMediumAvatarAsync( Id );
		}

		/// <summary>
		/// Downloads this user's 184x184 avatar as raw RGBA pixels — the full-resolution version, for
		/// profile panels and anywhere the avatar is displayed large.
		/// </summary>
		/// <returns>
		/// The decoded image, or <see langword="null"/> if the user has no avatar set or the image
		/// could not be read.
		/// </returns>
		/// <remarks>
		/// This is the slowest of the three and the one most likely to block. Valve documents that
		/// the underlying <c>GetLargeFriendAvatar</c> "returns -1 if this image has yet to be loaded,
		/// in this case wait for a AvatarImageLoaded_t callback and then call this again". Rather
		/// than expose that callback, this binding polls until the handle stops being <c>-1</c>. The
		/// poll has no timeout and no cancellation, so if the image never arrives — a user with no
		/// avatar returns handle <c>0</c> and exits cleanly, but a stalled download does not — the
		/// returned task never completes. Do not await it on a path that must make progress, and
		/// make sure callbacks are being pumped.
		/// </remarks>
		public async Task<Data.Image?> GetLargeAvatarAsync()
		{
			return await SteamFriends.GetLargeAvatarAsync( Id );
		}

		/// <summary>
		/// Reads one of this friend's rich presence values — the small key/value payload a game
		/// publishes about what its player is doing. Read the <c>"connect"</c> key to get the command
		/// line for joining them, or <c>"status"</c> for the human-readable line Steam shows in the
		/// friends list.
		/// </summary>
		/// <param name="key">
		/// The rich presence key to look up. Valve caps keys at 64 characters
		/// (<c>k_cchMaxRichPresenceKeyLength</c>). Matching against the keys the friend's game set;
		/// an unknown key is not an error, it simply yields <see langword="null"/>.
		/// </param>
		/// <returns>
		/// The value, or <see langword="null"/> if the key is not set. Valve's native call returns an
		/// empty string when no value is set and this binding maps both empty and null to
		/// <see langword="null"/>, so "key absent", "value is the empty string" and "we have no rich
		/// presence for this user at all" all look identical to the caller.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Valve states that "Rich Presence data is automatically shared between friends who are in
		/// the same game". You will generally get nothing for a friend playing something else, and
		/// nothing at all until the data has arrived — <c>SteamFriends.OnFriendRichPresenceUpdate</c>
		/// is how you learn it changed. This binding does not expose Valve's
		/// <c>RequestFriendRichPresence</c>, so there is no way to explicitly ask for a specific
		/// user's rich presence through this API; you wait for the callback.
		/// </para>
		/// <para>
		/// Values are capped by Valve at 256 characters
		/// (<c>k_cchMaxRichPresenceValueLength</c>). The native <c>const char *</c> is copied into
		/// managed memory before this returns, so the string is safe to keep; the underlying pointer
		/// is assumed — not documented by Valve — to be invalidated by the next call on this
		/// interface.
		/// </para>
		/// </remarks>
		public string GetRichPresence( string key )
		{
			var val = SteamFriends.Internal.GetFriendRichPresence( Id, key );
			if ( string.IsNullOrEmpty( val ) ) return null;
			return val;
		}

		/// <summary>
		/// Sends this friend an in-game invite carrying a connect string. If they accept, Steam
		/// launches or focuses your game on their machine and hands it the string, which arrives
		/// through <c>SteamFriends.OnGameRichPresenceJoinRequested</c>.
		/// </summary>
		/// <param name="Text">
		/// The connect string the recipient's copy of the game receives — typically a server address
		/// or lobby ID, in whatever format your own game parses. Valve caps this at 256 characters
		/// (<c>k_cchMaxRichPresenceValueLength</c>); it does not document what happens to a longer
		/// string, so keep it short. Not validated by this binding.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if Steam accepted the invite for sending. This says nothing about
		/// whether it was delivered, seen or accepted — the only signal of acceptance is the
		/// recipient's game receiving the connect string. Valve does not document what makes this
		/// return <see langword="false"/>, so failures cannot be told apart.
		/// </returns>
		/// <remarks>
		/// Valve calls this "rich invite support" and notes the alternative of having the connect
		/// string "passed on the command line instead", which it describes as "a deprecated path".
		/// Prefer this call. For invites the user picks recipients for, use the overlay dialog via
		/// <c>SteamFriends.OpenGameInviteOverlay</c> instead.
		/// </remarks>
		public bool InviteToGame( string Text )
		{
			return SteamFriends.Internal.InviteUserToGame( Id, Text );
		}

		/// <summary>
		/// Sends this friend a Steam chat message from inside the game, so you can host Steam friend
		/// chat in your own UI rather than making the player open the overlay.
		/// </summary>
		/// <param name="message">
		/// The message text, UTF-8. Valve documents no length limit for this call and this binding
		/// imposes none; note that incoming messages are read back through a 32 KB buffer, so
		/// anything longer round-trips badly.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if Steam accepted the message. Valve documents no failure reasons,
		/// so a <see langword="false"/> gives you nothing to act on beyond "it did not send" — the
		/// likely causes are the recipient not being a friend, or the local user being chat
		/// restricted.
		/// </returns>
		/// <remarks>
		/// The underlying call is <c>ReplyToFriendMessage</c>, which sits in Valve's "peer-to-peer
		/// chat interception" section alongside <c>SetListenForFriendsMessages</c>. Valve documents
		/// nothing about it beyond its signature; the name implies reply semantics, and this binding
		/// does nothing to establish a conversation first. To receive the other half of the
		/// conversation you must set <c>SteamFriends.ListenForFriendsMessages</c> to
		/// <see langword="true"/> and handle <c>SteamFriends.OnChatMessage</c>.
		/// </remarks>
		public bool SendMessage( string message )
		{
			return SteamFriends.Internal.ReplyToFriendMessage( Id, message );
		}


		/// <summary>
		/// Downloads this user's stats and achievements into the local cache. You must await this
		/// successfully before <see cref="GetStatInt"/>, <see cref="GetStatFloat"/>,
		/// <see cref="GetAchievement"/> or <see cref="GetAchievementUnlockTime"/> return anything
		/// real — those are cache reads, not requests.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if the stats arrived. <see langword="false"/> covers three
		/// different situations that this binding collapses together: the API call itself failed,
		/// Steam returned a non-OK result, or — as Valve documents — "if the other user has no stats,
		/// UserStatsReceived_t.m_eResult will be set to k_EResultFail". A user who has simply never
		/// played your game is therefore reported the same way as a genuine error.
		/// </returns>
		/// <remarks>
		/// Valve is explicit that "these stats won't be auto-updated; you'll need to call
		/// RequestUserStats() again to refresh any data" — the snapshot never refreshes itself, so
		/// re-await this whenever you need current values. Requires callbacks to be pumped. Valve
		/// states no rate limit for this call in <c>isteamuserstats.h</c>, but it is a round trip to
		/// the Steam backend — treat it as expensive and do not issue it per frame.
		/// </remarks>
		public async Task<bool> RequestUserStatsAsync()
		{
			var result = await SteamUserStats.Internal.RequestUserStats( Id );
			return result.HasValue && result.Value.Result == Result.OK;
		}

		/// <summary>
		/// Reads one of this user's floating-point stats out of the locally cached copy downloaded by
		/// <see cref="RequestUserStatsAsync"/> — for showing another player's totals on a scoreboard
		/// or profile panel.
		/// </summary>
		/// <param name="statName">
		/// The API name of the stat exactly as configured in the Steamworks partner site for your
		/// app. Case sensitive, and it must be a <c>FLOAT</c> stat: asking for an <c>INT</c> stat
		/// here fails rather than converting. An unknown name is not an error, you just get
		/// <paramref name="defult"/>.
		/// </param>
		/// <param name="defult">
		/// The value returned whenever the stat cannot be read. Note the spelling — this is the
		/// parameter name in the shipped API and cannot be corrected without breaking callers who
		/// pass it by name.
		/// </param>
		/// <returns>
		/// The stat value, or <paramref name="defult"/> if the read failed. Failure and success are
		/// indistinguishable when the stat genuinely equals <paramref name="defult"/>, and the three
		/// failure causes — stats never requested, request failed, wrong stat name or type — all look
		/// the same. If you need to tell them apart, confirm
		/// <see cref="RequestUserStatsAsync"/> returned <see langword="true"/> first and pass a
		/// <paramref name="defult"/> that is not a legal value for the stat.
		/// </returns>
		/// <remarks>
		/// This is a pure cache read: it never triggers a download and never blocks. Without a prior
		/// successful <see cref="RequestUserStatsAsync"/> for this same user it always returns
		/// <paramref name="defult"/>, silently.
		/// </remarks>
		public float GetStatFloat( string statName, float defult = 0 )
		{
			var val = defult;

			if ( !SteamUserStats.Internal.GetUserStat( Id, statName, ref val ) )
				return defult;

			return val;
		}

		/// <summary>
		/// Reads one of this user's integer stats out of the locally cached copy downloaded by
		/// <see cref="RequestUserStatsAsync"/>. This is the counterpart to
		/// <see cref="GetStatFloat"/> and dispatches to a different native entry point
		/// (<c>GetUserStatInt32</c>) — the two are not interchangeable.
		/// </summary>
		/// <param name="statName">
		/// The API name of the stat exactly as configured in the Steamworks partner site for your
		/// app. Case sensitive, and it must be an <c>INT</c> stat: asking for a <c>FLOAT</c> stat
		/// here fails rather than truncating.
		/// </param>
		/// <param name="defult">
		/// The value returned whenever the stat cannot be read. Note the spelling — this is the
		/// parameter name in the shipped API and cannot be corrected without breaking callers who
		/// pass it by name.
		/// </param>
		/// <returns>
		/// The stat value, or <paramref name="defult"/> if the read failed. As with
		/// <see cref="GetStatFloat"/>, a genuine value equal to <paramref name="defult"/> is
		/// indistinguishable from failure, and all failure causes look alike.
		/// </returns>
		/// <remarks>
		/// A pure cache read — it never downloads anything. Without a prior successful
		/// <see cref="RequestUserStatsAsync"/> for this same user it always returns
		/// <paramref name="defult"/>, silently.
		/// </remarks>
		public int GetStatInt( string statName, int defult = 0 )
		{
			var val = defult;

			if ( !SteamUserStats.Internal.GetUserStat( Id, statName, ref val ) )
				return defult;

			return val;
		}

		/// <summary>
		/// Whether this user has unlocked one of your app's achievements, read from the locally
		/// cached copy downloaded by <see cref="RequestUserStatsAsync"/>. Use it to show a friend's
		/// progress next to the local player's.
		/// </summary>
		/// <param name="statName">
		/// The API name of the achievement as configured in the Steamworks partner site — case
		/// sensitive. Despite the parameter being called <c>statName</c> this is an achievement name,
		/// not a stat name; the two namespaces are separate and passing a stat name here fails.
		/// </param>
		/// <param name="defult">
		/// The value returned whenever the achievement state cannot be read. Note the spelling — this
		/// is the parameter name in the shipped API and cannot be corrected without breaking callers
		/// who pass it by name.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if unlocked, <see langword="false"/> if not, or
		/// <paramref name="defult"/> if the state could not be read at all. With the default
		/// <paramref name="defult"/> of <see langword="false"/>, "locked" and "we have no data" are
		/// the same answer — pass <see langword="true"/> as <paramref name="defult"/> if you need to
		/// spot the failure case.
		/// </returns>
		/// <remarks>
		/// A pure cache read. Without a prior successful <see cref="RequestUserStatsAsync"/> for this
		/// same user it always returns <paramref name="defult"/>, silently. To also learn when the
		/// achievement was unlocked, use <see cref="GetAchievementUnlockTime"/> rather than calling
		/// both — it reports the unlocked flag as part of the same native call.
		/// </remarks>
		public bool GetAchievement( string statName, bool defult = false )
		{
			var val = defult;

			if ( !SteamUserStats.Internal.GetUserAchievement( Id, statName, ref val ) )
				return defult;

			return val;
		}		
		
		/// <summary>
		/// When this user unlocked one of your app's achievements — for "first to unlock" boards,
		/// friend comparison screens and anything that orders players by when they got there.
		/// </summary>
		/// <param name="statName">
		/// The API name of the achievement as configured in the Steamworks partner site — case
		/// sensitive. As with <see cref="GetAchievement"/>, the parameter is called <c>statName</c>
		/// but an achievement name is what is wanted here.
		/// </param>
		/// <returns>
		/// <para>
		/// The unlock time as a UTC <see cref="DateTime"/> (converted from Steam's Unix seconds), or
		/// <see cref="DateTime.MinValue"/> if the achievement is locked, unknown, or the stats have
		/// not been downloaded — those three are not distinguishable.
		/// </para>
		/// <para>
		/// There is a second, sharper trap. Valve documents that "if the return value is true, but
		/// the unlock time is zero, that means it was unlocked before Steam began tracking
		/// achievement unlock times (December 2009)". This binding passes that zero straight through
		/// the epoch conversion, so such an achievement comes back as <c>1970-01-01</c> — a real,
		/// unlocked achievement wearing a nonsense date, and notably <em>not</em>
		/// <see cref="DateTime.MinValue"/>. If your game predates 2010, special-case the Unix epoch
		/// rather than displaying it.
		/// </para>
		/// </returns>
		/// <remarks>
		/// A pure cache read that requires a prior successful <see cref="RequestUserStatsAsync"/> for
		/// this same user. This also tells you whether the achievement is unlocked at all — an
		/// unlocked achievement never returns <see cref="DateTime.MinValue"/> — so there is no need
		/// to call <see cref="GetAchievement"/> as well.
		/// </remarks>
		public DateTime GetAchievementUnlockTime( string statName )
		{
			bool val = false;
			uint time = 0;

			if ( !SteamUserStats.Internal.GetUserAchievementAndUnlockTime( Id, statName, ref val, ref time ) || !val )
				return DateTime.MinValue;

			return Epoch.ToDateTime( time );
		}

	}
}
