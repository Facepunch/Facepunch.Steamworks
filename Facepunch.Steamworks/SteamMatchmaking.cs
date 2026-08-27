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
	/// <remarks>
	/// Everything here is asynchronous and callback-driven. Tasks returned by this class only complete,
	/// and events only fire, while Steam callbacks are being pumped: either initialise with
	/// <c>SteamClient.Init( appid, asyncCallbacks: true )</c> (the default) or call
	/// <see cref="SteamClient.RunCallbacks"/> yourself every frame. Without pumping, an awaited
	/// <see cref="CreateLobbyAsync"/> simply never returns.
	/// <para>
	/// Lobby state replicates asynchronously too. Reading <see cref="Lobby.Members"/> or lobby data in the
	/// same frame you joined can legitimately give you nothing. The SDK header also notes that persona
	/// information for other lobby members (name, avatar) arrives later through the friends interface, so
	/// <c>Friend.Name</c> may be a placeholder for the first frames after a member appears.
	/// </para>
	/// </remarks>
	/// <example>
	/// Create a lobby, make it discoverable, and react to people arriving:
	/// <code>
	/// SteamClient.Init( 480 );
	///
	/// SteamMatchmaking.OnLobbyMemberJoined       += ( l, f ) =&gt; Console.WriteLine( $"{f.Name} joined" );
	/// SteamMatchmaking.OnLobbyMemberLeave        += ( l, f ) =&gt; Console.WriteLine( $"{f.Name} left" );
	/// SteamMatchmaking.OnLobbyMemberDisconnected += ( l, f ) =&gt; Console.WriteLine( $"{f.Name} dropped" );
	///
	/// Lobby? created = await SteamMatchmaking.CreateLobbyAsync( maxMembers: 8 );
	/// if ( !created.HasValue )
	/// {
	///     Console.WriteLine( "Could not create a lobby - see OnLobbyCreated for the Result" );
	///     return;
	/// }
	///
	/// Lobby lobby = created.Value;
	/// lobby.SetData( "name", "Bob's game" );
	/// lobby.SetData( "map", "forest" );
	/// lobby.SetJoinable( true );
	/// lobby.SetPublic();                  // now visible to SteamMatchmaking.LobbyList searches
	///
	/// // Hand lobby.Id to friends, or invite them directly:
	/// lobby.InviteFriend( someFriendSteamId );
	/// </code>
	/// </example>
	public class SteamMatchmaking : SteamClientClass<SteamMatchmaking>
	{
		internal static ISteamMatchmaking Internal => Interface as ISteamMatchmaking;

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamMatchmaking( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			InstallEvents();

			return true;
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
		/// Another user has invited the local user to a lobby. Handle this only if you want your own
		/// in-game invite prompt: the Steam overlay already shows a "&lt;user&gt; has invited you to the
		/// lobby, join?" dialog for the same invite.
		/// </summary>
		/// <remarks>
		/// The first argument is the friend who sent the invite; the second is the lobby they are inviting
		/// you to. This is <em>not</em> the callback that fires when the user accepts an invite from
		/// outside the game - in that case the SDK header says the game is launched with
		/// <c>+connect_lobby &lt;64-bit lobby id&gt;</c> on the command line instead.
		/// </remarks>
		public static event Action<Friend, Lobby> OnLobbyInvite;

		/// <summary>
		/// The local user has finished entering a lobby - by creating one, by <see cref="JoinLobbyAsync"/>,
		/// or by <see cref="Lobby.Join"/>. This is the point at which the lobby's metadata is usable.
		/// </summary>
		/// <remarks>
		/// The SDK header notes this is also posted when you fail to enter, so receiving it is not proof
		/// that you are in the lobby. This binding does not surface the underlying enter-response code on
		/// this event; use the <see cref="RoomEnter"/> value returned by <see cref="Lobby.Join"/> if you
		/// need to know why an attempt failed. You also receive this when you create your own lobby,
		/// because creating implies joining.
		/// </remarks>
		public static event Action<Lobby> OnLobbyEntered;

		/// <summary>
		/// A lobby creation request by the local user has come back from the Steam servers. The
		/// <see cref="Result"/> tells you whether it worked, which makes this the only place to see
		/// <em>why</em> a <see cref="CreateLobbyAsync"/> call returned <see langword="null"/>.
		/// </summary>
		/// <remarks>
		/// The SDK header lists the results you can expect: <c>OK</c>, <c>NoConnection</c> (no connection
		/// to the Steam back-end), <c>Timeout</c>, <c>Fail</c> (unknown server-side error),
		/// <c>AccessDenied</c> (your app is not configured to allow lobbies) and <c>LimitExceeded</c>
		/// (this client has created too many lobbies). On failure the lobby id is zero, so the
		/// <see cref="Lobby"/> handed to you is not usable.
		/// </remarks>
		public static event Action<Result, Lobby> OnLobbyCreated;

		/// <summary>
		/// The lobby owner nominated a game server for everyone to connect to, by calling one of the
		/// <c>Lobby.SetGameServer</c> overloads. The usual reaction is to leave the lobby and connect.
		/// </summary>
		/// <remarks>
		/// The arguments are the lobby, then the server's IPv4 address as a host-order <see langword="uint"/>,
		/// its port, and its <see cref="SteamId"/>. Only the addressing scheme the owner actually supplied
		/// is meaningful: if they set the server by <see cref="SteamId"/> the IP and port arrive as zero,
		/// and if they set it by address the server id is not valid. The SDK header is explicit that Steam
		/// takes no action on your behalf here - travelling to the server is entirely your game's job.
		/// </remarks>
		public static event Action<Lobby, uint, ushort, SteamId> OnLobbyGameCreated;

		/// <summary>
		/// A lobby's metadata (the shared key/value data set with <see cref="Lobby.SetData"/>) changed,
		/// or a <see cref="Lobby.Refresh"/> you requested has arrived. Re-read the values you care about.
		/// </summary>
		/// <remarks>
		/// This binding raises the event when the native callback reports the change was for the room
		/// itself rather than for a member. Note that the dispatcher drops callbacks whose success flag is
		/// zero, so a <see cref="Lobby.Refresh"/> for a lobby that no longer exists produces
		/// <em>no</em> event at all - do not wait on this event as your only completion signal.
		/// </remarks>
		public static event Action<Lobby> OnLobbyDataChanged;

		/// <summary>
		/// One member's own per-user metadata changed - the data that member set on themselves with
		/// <see cref="Lobby.SetMemberData"/>. Read it back with <see cref="Lobby.GetMemberData"/>.
		/// </summary>
		/// <remarks>
		/// This is a different channel from <see cref="OnLobbyDataChanged"/>: member data is written by
		/// each member for themselves, lobby data is shared state for the whole room. Steam distinguishes
		/// them in a single native callback by whether the changed id is the lobby or a member.
		/// </remarks>
		public static event Action<Lobby, Friend> OnLobbyMemberDataChanged;

		/// <summary>
		/// A user entered the lobby. Raised for every member who joins, including the local user's own
		/// entry, so it is a reliable place to build up your member list.
		/// </summary>
		public static event Action<Lobby, Friend> OnLobbyMemberJoined;

		/// <summary>
		/// A member left the lobby of their own accord - their client called <see cref="Lobby.Leave"/>
		/// (or quit the game cleanly). This is an orderly departure, so their slot is genuinely free and
		/// they are not coming back unless they re-join.
		/// </summary>
		/// <remarks>
		/// Dispatched from the <c>Left</c> flag of the native lobby chat update, which the SDK header
		/// describes as "this user has left or is leaving the chat room". Contrast
		/// <see cref="OnLobbyMemberDisconnected"/>, which is the ungraceful case.
		/// <para>
		/// The state change is a bitfield and this binding tests each flag independently, so one native
		/// callback can raise more than one of these member events.
		/// </para>
		/// <para>
		/// Valve does not document how long a departed member's per-member data stays readable, so read
		/// anything you still need from <see cref="Lobby.GetMemberData"/> inside the handler rather than
		/// later. Whether <see cref="Lobby.MemberCount"/> has already been decremented when this fires is
		/// likewise undocumented - do not assume either way.
		/// </para>
		/// </remarks>
		public static event Action<Lobby, Friend> OnLobbyMemberLeave;

		/// <summary>
		/// A member dropped out without leaving first - a crash, a network failure, or losing their
		/// connection to Steam. Nothing on their side announced the departure, so treat this as an
		/// unexpected loss: the player may be reconnecting and trying to re-join in a moment.
		/// </summary>
		/// <remarks>
		/// Dispatched from the <c>Disconnected</c> flag of the native lobby chat update, which the SDK
		/// header describes as "user disconnected without leaving the chat first". If the local user is
		/// the one who loses their Steam connection, Valve's header says Steam posts a lobby-kicked
		/// callback to them instead; this binding does not surface that callback, so you cannot observe
		/// your own drop through this event.
		/// <para>
		/// Because the departure was not orderly, do not assume the member's per-member data is still
		/// available or that any pending chat from them was delivered. Games that support reconnection
		/// usually hold the player's slot open here, where they would free it on
		/// <see cref="OnLobbyMemberLeave"/>.
		/// </para>
		/// </remarks>
		public static event Action<Lobby, Friend> OnLobbyMemberDisconnected;

		/// <summary>
		/// A member was removed from the lobby by someone else, but is still allowed back in - they can
		/// re-join immediately unless your game stops them.
		/// </summary>
		/// <remarks>
		/// The second argument is the member who was removed; the third is the member who did the
		/// removing (the SDK header calls this "the chat member who made the change"). Dispatched from
		/// the <c>Kicked</c> flag, documented by Valve simply as "user kicked". Contrast
		/// <see cref="OnLobbyMemberBanned"/>, which additionally bars them from returning.
		/// </remarks>
		public static event Action<Lobby, Friend, Friend> OnLobbyMemberKicked;

		/// <summary>
		/// A member was removed from the lobby <em>and</em> barred from re-entering it. Unlike
		/// <see cref="OnLobbyMemberKicked"/>, Steam itself will refuse their next join attempt.
		/// </summary>
		/// <remarks>
		/// The second argument is the banned member; the third is the member who banned them. Dispatched
		/// from the <c>Banned</c> flag, documented by Valve as "user kicked and banned". Because the flags
		/// are a bitfield tested independently by this binding, a ban that Steam also marks as a kick will
		/// raise <see cref="OnLobbyMemberKicked"/> as well - so do not treat the two as mutually exclusive.
		/// </remarks>
		public static event Action<Lobby, Friend, Friend> OnLobbyMemberBanned;

		/// <summary>
		/// A chat message arrived from a lobby member, already fetched and decoded into a string. Includes
		/// messages the local user sent themselves.
		/// </summary>
		/// <remarks>
		/// This binding decodes the payload as a null-terminated UTF-8 string, so it pairs with
		/// <see cref="Lobby.SendChatString"/>. Arbitrary binary sent through
		/// <see cref="Lobby.SendChatBytes"/> arrives here truncated at its first zero byte and is not
		/// otherwise exposed - there is no binary-message event in this binding. Messages that decode to
		/// zero bytes are dropped silently rather than raised as empty strings.
		/// </remarks>
		public static event Action<Lobby, Friend, string> OnChatMessage;

		/// <summary>
		/// Starts a new lobby search. Chain filter calls onto the returned value and finish with
		/// <see cref="LobbyQuery.RequestAsync"/>; each read of this property gives you a fresh,
		/// unfiltered query.
		/// </summary>
		/// <value>An empty <see cref="LobbyQuery"/> with no filters applied.</value>
		/// <example>
		/// <code>
		/// Lobby[] lobbies = await SteamMatchmaking.LobbyList
		///                             .WithKeyValue( "map", "forest" )
		///                             .WithSlotsAvailable( 2 )
		///                             .FilterDistanceClose()
		///                             .WithMaxResults( 20 )
		///                             .RequestAsync();
		///
		/// // null means "no matches" as well as "the request failed" - both are handled the same way.
		/// if ( lobbies == null ) return;
		///
		/// foreach ( var l in lobbies )
		///     Console.WriteLine( $"{l.GetData( "name" )} - {l.MemberCount}/{l.MaxMembers}" );
		/// </code>
		/// </example>
		public static LobbyQuery LobbyList => new LobbyQuery();

		/// <summary>
		/// Creates a lobby on the Steam servers with the local user as its owner and only member, and waits
		/// for the servers to confirm it. The lobby is created as <c>Invisible</c>: friends will not see it
		/// in their Steam UI, but it <em>is</em> already returned by <see cref="LobbyList"/> searches. Call
		/// <see cref="Lobby.SetPublic"/> once it is configured if you also want friends to see it.
		/// </summary>
		/// <param name="maxMembers">
		/// Member limit for the new lobby, counting the owner. Steam's documented ceiling is 250; this
		/// binding does not validate the value, so an out-of-range number fails inside Steam rather than here.
		/// </param>
		/// <returns>
		/// The new lobby, already joined and ready to use, or <see langword="null"/> if creation failed.
		/// The two failure modes - the call result never arriving, and Steam returning a non-OK
		/// <see cref="Result"/> - are <em>not</em> distinguishable from the return value. Subscribe to
		/// <see cref="OnLobbyCreated"/> if you need the actual <see cref="Result"/>.
		/// </returns>
		/// <remarks>
		/// Creating implies joining, so <see cref="OnLobbyEntered"/> also fires for the local user. The
		/// returned task only completes while callbacks are being pumped.
		/// <para>
		/// A newly created lobby is joinable by default. Discoverability is a separate axis: per the SDK
		/// header, only lobbies that are <c>Public</c> or <c>Invisible</c> <em>and</em> joinable are
		/// returned by lobby list searches, so calling <see cref="Lobby.SetJoinable"/> with
		/// <see langword="false"/> silently removes the lobby from search results as well as blocking
		/// invited users.
		/// </para>
		/// </remarks>
		public static async Task<Lobby?> CreateLobbyAsync( int maxMembers = 100 )
		{
			var lobby = await Internal.CreateLobby( LobbyType.Invisible, maxMembers );
			if ( !lobby.HasValue || lobby.Value.Result != Result.OK ) return null;

			return new Lobby { Id = lobby.Value.SteamIDLobby };
		}

		/// <summary>
		/// Joins a lobby by id - the id you got from a friend, from a <c>+connect_lobby</c> command line,
		/// or from <see cref="LobbyList"/>. Use this when you have a raw <see cref="SteamId"/> rather than
		/// a <see cref="Lobby"/> value.
		/// </summary>
		/// <param name="lobbyId">The lobby's <see cref="SteamId"/>. You do not have to be a member already.</param>
		/// <returns>
		/// A <see cref="Lobby"/> for the id, or <see langword="null"/> only if the call result never
		/// arrived. Be careful: this method does <em>not</em> inspect the enter-response code, so a
		/// refusal by Steam (lobby full, banned, does not exist) still yields a non-<see langword="null"/>
		/// value here. If you need to know whether you actually got in, use
		/// <see cref="Lobby.Join"/> on a <see cref="Lobby"/> instead - it returns the
		/// <see cref="RoomEnter"/> code - or check <see cref="OnLobbyEntered"/>.
		/// </returns>
		/// <remarks>
		/// The SDK header notes that lobby metadata is available immediately once this completes, unlike
		/// member persona data. Completion requires callbacks to be pumped.
		/// </remarks>
		public static async Task<Lobby?> JoinLobbyAsync( SteamId lobbyId )
		{
			var lobby = await Internal.JoinLobby( lobbyId );
			if ( !lobby.HasValue ) return null;

			return new Lobby { Id = lobby.Value.SteamIDLobby };
		}

		/// <summary>
		/// The servers the user explicitly starred as favourites in the Steam server browser. Use this to
		/// offer a "your favourites" tab without running a master server query.
		/// </summary>
		/// <returns>
		/// A lazily evaluated sequence of favourite entries; empty if the user has none. Entries Steam
		/// fails to read, and entries not flagged as favourites, are skipped silently, so a short result
		/// is not distinguishable from a partly unreadable list.
		/// </returns>
		/// <remarks>
		/// This is local storage on the user's machine, not a live query - the servers may be long dead.
		/// Favourites and history share one underlying list and are separated only by a flag, which is why
		/// this and <see cref="GetHistoryServers"/> both walk the same entries. This binding reads but
		/// discards the app id on each entry, so if the list contains servers belonging to other games
		/// they are returned here too.
		/// </remarks>
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
		/// The servers the user has actually connected to before, as recorded by Steam. Useful for a
		/// "recently played on" list, and distinct from <see cref="GetFavoriteServers"/>, which the user
		/// has to opt into deliberately.
		/// </summary>
		/// <returns>
		/// A lazily evaluated sequence of history entries; empty if the user has none. As with favourites,
		/// unreadable entries are skipped silently.
		/// </returns>
		/// <remarks>
		/// Same local list as <see cref="GetFavoriteServers"/>, filtered on the history flag instead. A
		/// server can carry both flags and therefore appear in both sequences. The app id on each entry is
		/// read and discarded, so entries for other games are not filtered out.
		/// </remarks>
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
