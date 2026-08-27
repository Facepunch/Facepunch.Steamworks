using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	/// <summary>
	/// A handle to a Steam lobby - the pre-game room where players gather, agree on settings, chat, and
	/// are eventually sent to a game server together. A lobby is Steam-hosted shared state: a member list,
	/// a bag of key/value data, and an owner.
	/// </summary>
	/// <remarks>
	/// This is only an id wrapper. Constructing one costs nothing and proves nothing - every member on it
	/// calls into Steam, and most of them are meaningful only if the local user is actually in the lobby.
	/// Nothing is cached, so reading <see cref="MemberCount"/> in a loop is a native call each time.
	/// <para>
	/// Two permission rules cause most silent failures. Lobby data (<see cref="SetData"/>) is shared state
	/// for the whole room; Steam's published documentation restricts writing it to the lobby owner, and a
	/// non-owner's call simply returns <see langword="false"/> and changes nothing. Member data
	/// (<see cref="SetMemberData"/>) is per-user and always written for the local user - you cannot set
	/// anyone else's, and there is no return value to tell you anything went wrong.
	/// </para>
	/// <para>
	/// Everything here depends on Steam callbacks being pumped. Lobby state read in the same frame you
	/// joined may still be empty.
	/// </para>
	/// </remarks>
	/// <example>
	/// The host creates and advertises a lobby; a second player finds it and joins:
	/// <code>
	/// // --- host ---
	/// Lobby? created = await SteamMatchmaking.CreateLobbyAsync( maxMembers: 4 );
	/// if ( !created.HasValue ) return;
	///
	/// Lobby lobby = created.Value;
	/// lobby.SetData( "name", "Bob's game" );   // owner-only, shared with every member
	/// lobby.SetData( "map", "forest" );
	/// lobby.SetMemberData( "ready", "0" );      // about *me*, any member may do this
	/// lobby.SetPublic();
	///
	/// // --- joiner ---
	/// Lobby[] found = await SteamMatchmaking.LobbyList.WithKeyValue( "map", "forest" ).RequestAsync();
	/// if ( found == null ) return;             // null covers "none matched" too
	///
	/// RoomEnter enter = await found[0].Join();
	/// if ( enter != RoomEnter.Success )
	/// {
	///     Console.WriteLine( $"Could not join: {enter}" );
	///     return;
	/// }
	///
	/// foreach ( var member in found[0].Members )
	///     Console.WriteLine( member.Name );    // may be a placeholder for the first few frames
	/// </code>
	/// </example>
	public struct Lobby
	{
		/// <summary>
		/// The lobby's <see cref="SteamId"/>. This is the only thing a <see cref="Lobby"/> actually holds,
		/// and the value you pass around out-of-band (over your own network, in a command line, in a
		/// Discord invite) so another player can reach the same lobby via
		/// <see cref="SteamMatchmaking.JoinLobbyAsync"/>.
		/// </summary>
		/// <value>
		/// The lobby id. A default-constructed <see cref="Lobby"/> has an id whose <c>IsValid</c> is
		/// <see langword="false"/>; no member here checks for that, so calls on such a handle fail inside
		/// Steam instead.
		/// </value>
		public SteamId Id { get; internal set; }


		/// <summary>
		/// Wraps a lobby id you already have - from another player, from a <c>+connect_lobby</c> command
		/// line argument, or from your own storage - so you can call <see cref="Join"/> on it.
		/// </summary>
		/// <param name="id">The lobby's <see cref="SteamId"/>. Not validated, and no contact is made with Steam.</param>
		public Lobby( SteamId id )
		{
			Id = id;
		}

		/// <summary>
		/// Try to join this room. Will return <see cref="RoomEnter.Success"/> on success,
		/// and anything else is a failure.
		/// </summary>
		/// <returns>
		/// <see cref="RoomEnter.Success"/> if you are now a member. Other values name the reason:
		/// <see cref="RoomEnter.DoesntExist"/>, <see cref="RoomEnter.NotAllowed"/>,
		/// <see cref="RoomEnter.Full"/>, <see cref="RoomEnter.Banned"/> and so on.
		/// <see cref="RoomEnter.Error"/> is ambiguous - this binding returns it both when Steam reports a
		/// generic error and when the call result never arrived at all, so those two are not
		/// distinguishable here.
		/// </returns>
		/// <remarks>
		/// Prefer this over <see cref="SteamMatchmaking.JoinLobbyAsync"/> when you care whether the join
		/// actually succeeded, since that method does not report the enter response.
		/// <para>
		/// The task only completes while callbacks are being pumped. On success the SDK header says lobby
		/// metadata is usable immediately, but member persona data (names, avatars) still arrives
		/// asynchronously afterwards.
		/// </para>
		/// </remarks>
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
		/// <remarks>
		/// The remaining members see this as a clean departure - they receive
		/// <see cref="SteamMatchmaking.OnLobbyMemberLeave"/>, not
		/// <see cref="SteamMatchmaking.OnLobbyMemberDisconnected"/>. Always call this rather than just
		/// dropping the handle, otherwise your slot stays occupied until Steam notices you are gone.
		/// <para>
		/// If you were the owner, the SDK header states ownership passes to another member automatically;
		/// there is no way to nominate a successor as part of leaving. Use the <see cref="Owner"/> setter
		/// before leaving if you care who gets it. Nothing is returned, so a call on a lobby you are not
		/// in does nothing observable.
		/// </para>
		/// </remarks>
		public void Leave()
		{
			SteamMatchmaking.Internal.LeaveLobby( Id );
		}

		/// <summary>
		/// Invite another user to the lobby.
		/// Will return <see langword="true"/> if the invite is successfully sent, whether or not the target responds
		/// returns <see langword="false"/> if the local user is not connected to the Steam servers
		/// </summary>
		/// <param name="steamid">The user to invite. They do not have to be a friend of the local user.</param>
		/// <returns>
		/// <see langword="true"/> if the invite was handed to Steam - this says nothing about whether the
		/// invitee saw it, wanted it, or acted on it. <see langword="false"/> means the local user is not
		/// connected to the Steam servers.
		/// </returns>
		/// <remarks>
		/// The recipient gets <see cref="SteamMatchmaking.OnLobbyInvite"/> if they are in-game. Per the SDK
		/// header, if they accept while the game is not running Steam launches it with
		/// <c>+connect_lobby &lt;64-bit lobby id&gt;</c> on the command line, so you must parse that
		/// argument at startup or invites from outside the game will do nothing.
		/// <para>
		/// An invite does not override <see cref="SetJoinable"/>: the header is explicit that when a lobby
		/// is not joinable, no user can join "even if they are a friend or have been invited".
		/// </para>
		/// </remarks>
		public bool InviteFriend( SteamId steamid )
		{
			return SteamMatchmaking.Internal.InviteUserToLobby( Id, steamid );
		}

		/// <summary>
		/// How many users are currently in this lobby, for showing "3/8" style occupancy against
		/// <see cref="MaxMembers"/>.
		/// </summary>
		/// <value>
		/// The member count including the local user. <c>0</c> is also what you get for a lobby you are
		/// not a member of or that does not exist, so it is not by itself evidence of an empty lobby.
		/// </value>
		/// <remarks>
		/// Read fresh from Steam on every access. Whether this has already been updated by the time a
		/// member-left or member-disconnected event fires is not documented by Valve - do not build
		/// bookkeeping that assumes either ordering.
		/// </remarks>
		public int MemberCount => SteamMatchmaking.Internal.GetNumLobbyMembers( Id );

		/// <summary>
		/// Returns current members in the lobby. The current user must be in the lobby in order to see the users.
		/// </summary>
		/// <value>
		/// A lazily evaluated sequence of the current members, including the local user. Empty if you are
		/// not in this lobby.
		/// </value>
		/// <remarks>
		/// Enumeration re-reads <see cref="MemberCount"/> on every step, so the sequence tracks membership
		/// changes mid-iteration rather than snapshotting. Materialise it with <c>ToArray()</c> if you need
		/// a stable list.
		/// <para>
		/// The SDK header warns that persona information for other members - name, avatar - is received
		/// asynchronously through the friends interface, so <c>Friend.Name</c> on a member who just
		/// appeared can be a placeholder for a few frames.
		/// </para>
		/// </remarks>
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
		/// Reads one of the lobby's shared key/value pairs - the room-wide settings the owner published
		/// with <see cref="SetData"/>, such as the map, game mode or server name.
		/// </summary>
		/// <param name="key">The key to look up. Case and content are yours to choose; keys are capped at <c>255</c> characters when written.</param>
		/// <returns>
		/// The stored value, or an empty string. Per the SDK header, an empty string is returned both when
		/// no value is set and when the lobby id is invalid, so a missing key and a bad lobby look
		/// identical. The header promises a string rather than a null pointer, but this binding's
		/// marshalling would surface a null pointer as <see langword="null"/>, so guard with
		/// <c>string.IsNullOrEmpty</c> rather than comparing against <c>""</c>.
		/// </returns>
		/// <remarks>
		/// Readable without being a member <em>if</em> you have pulled the lobby's metadata down first -
		/// that happens automatically for search results and on join, and on demand via
		/// <see cref="Refresh"/>. Reading before that data has arrived yields empty strings rather than an
		/// error.
		/// <para>
		/// This is lobby data, not member data: for the per-user values each member sets on themselves,
		/// use <see cref="GetMemberData"/>.
		/// </para>
		/// </remarks>
		public string GetData( string key )
		{
			return SteamMatchmaking.Internal.GetLobbyData( Id, key );
		}

		/// <summary>
		/// Publishes a shared key/value pair to the whole lobby - this is how you advertise the map, mode,
		/// server name or anything else every member (and any lobby search) should see. The SDK header
		/// notes existing data is also handed to members who join later.
		/// </summary>
		/// <param name="key">
		/// The key to write. Must be <c>255</c> characters or fewer - this is the one limit the SDK header
		/// states explicitly. Setting a key that already exists overwrites it.
		/// </param>
		/// <param name="value">
		/// The value to store. Must be 8192 characters or fewer. Per the SDK header, writing an empty
		/// string is the documented way to reset a key.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if Steam accepted the write. <see langword="false"/> is the <em>only</em>
		/// signal that it did not, and it is not broken down by cause - most commonly the local user is not
		/// the lobby owner, or the lobby id is invalid. Steam's published documentation restricts this call
		/// to the lobby owner; the SDK header in this repo does not state that requirement, so treat the
		/// return value as authoritative and check it.
		/// </returns>
		/// <exception cref="System.ArgumentException">
		/// The key is longer than <c>255</c> characters, or the value is longer than 8192. Note that the
		/// value check reports <c>key</c> as its parameter name, so do not branch on
		/// <c>ArgumentException.ParamName</c> to tell the two apart.
		/// </exception>
		/// <remarks>
		/// Members are notified through <see cref="SteamMatchmaking.OnLobbyDataChanged"/> and should
		/// re-read the keys they care about; the new value is not delivered with the event.
		/// <para>
		/// These same keys are what <see cref="LobbyQuery.WithKeyValue"/> and the numerical filters match
		/// against when other players search, so a lobby only becomes findable by a criterion once you have
		/// written that criterion here.
		/// </para>
		/// </remarks>
		public bool SetData( string key, string value )
		{
			if ( key.Length > 255 ) throw new System.ArgumentException( "Key should be < 255 chars", nameof( key ) );
			if ( value.Length > 8192 ) throw new System.ArgumentException( "Value should be < 8192 chars", nameof( key ) );

			return SteamMatchmaking.Internal.SetLobbyData( Id, key, value );
		}

		/// <summary>
		/// Removes a shared lobby key entirely, so it stops appearing in <see cref="Lobby.Data"/> and stops
		/// matching lobby searches. Distinct from setting it to an empty string, which leaves the key
		/// present.
		/// </summary>
		/// <param name="key">The key to remove. Removing a key that was never set is not an error.</param>
		/// <returns>
		/// <see langword="true"/> if Steam accepted the removal. <see langword="false"/> carries no
		/// detail - as with <see cref="SetData"/>, the usual cause is that the local user does not own the
		/// lobby, or the lobby id is invalid.
		/// </returns>
		/// <remarks>
		/// The SDK header documents no behaviour for this call beyond "removes a metadata key from the
		/// lobby"; in particular it does not say whether members are notified. Do not rely on
		/// <see cref="SteamMatchmaking.OnLobbyDataChanged"/> firing for a deletion.
		/// </remarks>
		public bool DeleteData( string key )
		{
			return SteamMatchmaking.Internal.DeleteLobbyData( Id, key );
		}

		/// <summary>
		/// Every shared key/value pair on the lobby, for inspecting a lobby whose keys you do not know in
		/// advance - a server browser row, or a debug view.
		/// </summary>
		/// <value>
		/// A lazily evaluated sequence of the lobby's key/value pairs. Empty if the lobby has no data, if
		/// the id is invalid, or if the metadata has not been fetched yet - the three are indistinguishable.
		/// </value>
		/// <remarks>
		/// Pairs Steam declines to return by index are skipped silently, so the sequence can be shorter
		/// than the lobby's actual key count. Only lobby data appears here; member data is not included.
		/// </remarks>
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
		/// Reads a value one member published about themselves - ready state, chosen team, chosen
		/// character. Separate namespace from <see cref="GetData"/>: each member owns their own keys.
		/// </summary>
		/// <param name="member">The member whose data you want. Only meaningful for someone currently in this lobby.</param>
		/// <param name="key">The key that member set via <see cref="SetMemberData"/>.</param>
		/// <returns>
		/// The stored value, or an empty string if that member never set the key, is not in this lobby, or
		/// their data has not replicated to you yet. The SDK header documents no way to tell those cases
		/// apart. As with <see cref="GetData"/>, prefer <c>string.IsNullOrEmpty</c> over comparing against
		/// <c>""</c>.
		/// </returns>
		/// <remarks>
		/// Only the member themselves can write these values, so treat them as claims by that player, not
		/// as authoritative state. Refresh in response to
		/// <see cref="SteamMatchmaking.OnLobbyMemberDataChanged"/>.
		/// </remarks>
		public string GetMemberData( Friend member, string key )
		{
			return SteamMatchmaking.Internal.GetLobbyMemberData( Id, member.Id, key );
		}

		/// <summary>
		/// Publishes a value about the local user to the rest of the lobby - "ready", team choice, loadout.
		/// Any member may call this, unlike <see cref="SetData"/>, but only ever for themselves: there is
		/// no overload that writes another member's data.
		/// </summary>
		/// <param name="key">The key to write under. Overwrites any previous value for the same key.</param>
		/// <param name="value">The value to store.</param>
		/// <remarks>
		/// This returns nothing, so it fails silently and completely: calling it on a lobby you are not in,
		/// or with an invalid id, produces no exception and no signal. Verify by reading back with
		/// <see cref="GetMemberData"/> if it matters.
		/// <para>
		/// Other members are notified via <see cref="SteamMatchmaking.OnLobbyMemberDataChanged"/>. The SDK
		/// header states no length limits for member data, and this binding enforces none - unlike
		/// <see cref="SetData"/>, which validates both key and value.
		/// </para>
		/// </remarks>
		public void SetMemberData( string key, string value )
		{
			SteamMatchmaking.Internal.SetLobbyMemberData( Id, key, value );
		}

		/// <summary>
		/// Broadcasts a text message to everyone in the lobby, using Steam's lobby chat rather than your
		/// own networking. Handy for pre-game chat and for small coordination messages before a server
		/// exists.
		/// </summary>
		/// <param name="message">
		/// The text to send. Encoded as UTF-8 with a null terminator appended, which is what the receiving
		/// side of this binding expects. The SDK header caps a chat payload at 4 KB - that budget is in
		/// encoded bytes, not characters, and neither this method nor Steam reports oversize as anything
		/// other than a failed send.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if Steam accepted the message for broadcast. <see langword="false"/> means
		/// it was not sent; the cause is not reported. Success here does not mean anyone received it.
		/// </returns>
		/// <remarks>
		/// Per the SDK header every member receives it - <em>including the sender</em>, via
		/// <see cref="SteamMatchmaking.OnChatMessage"/>. Do not also echo your own message locally or it
		/// will appear twice.
		/// <para>
		/// A <see langword="null"/> message is not rejected: C# concatenation turns it into the terminator
		/// alone, so a one-byte message is sent and arrives as an empty string. Guard against
		/// <see langword="null"/> yourself if that matters.
		/// </para>
		/// </remarks>
		public bool SendChatString( string message )
		{
			//adding null terminator as it's used in Helpers.MemoryToString
			var data = Utility.Utf8NoBom.GetBytes( message + '\0' );
			return SendChatBytes( data );
		}

		/// <summary>
		/// Broadcasts an arbitrary byte payload over the lobby chat channel - a way to push small
		/// structured messages to every member before your own networking is up.
		/// </summary>
		/// <param name="data">
		/// The payload. The SDK header allows binary content up to 4 KB. Must not be
		/// <see langword="null"/>; the length is taken from the array.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if Steam accepted the payload for broadcast, <see langword="false"/>
		/// otherwise, with no indication of why.
		/// </returns>
		/// <remarks>
		/// Nothing in this binding surfaces binary lobby chat on the receiving side.
		/// <see cref="SteamMatchmaking.OnChatMessage"/> decodes every incoming payload as a
		/// null-terminated UTF-8 string, so binary data arrives truncated at its first zero byte and a
		/// payload starting with zero is dropped entirely. Treat this as write-only unless the receiver is
		/// not Facepunch.Steamworks, or restrict yourself to text-safe encodings such as base64 or JSON.
		/// </remarks>
		public unsafe bool SendChatBytes( byte[] data )
		{
			fixed ( byte* ptr = data )
			{
				return SendChatBytesUnsafe( ptr, data.Length );
			}
		}

		/// <summary>
		/// Broadcasts a chat payload straight from a pinned buffer, avoiding the array allocation that
		/// <see cref="SendChatBytes"/> requires. For hot paths where you already hold native or stack memory.
		/// </summary>
		/// <param name="ptr">
		/// Pointer to the first byte to send. The caller is responsible for keeping the memory pinned and
		/// alive for the duration of the call; neither this binding nor Steam validates it.
		/// </param>
		/// <param name="length">
		/// Number of bytes to send, up to the SDK's documented 4 KB chat limit. If the payload is text, the
		/// SDK header requires this to include the null terminator. A wrong length reads out of bounds -
		/// there is no bounds check.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if Steam accepted the payload for broadcast, <see langword="false"/>
		/// otherwise, with no indication of why.
		/// </returns>
		/// <remarks>
		/// Same receive-side caveat as <see cref="SendChatBytes"/>: this binding decodes incoming chat as
		/// null-terminated UTF-8.
		/// </remarks>
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
		/// <returns>
		/// <see langword="true"/> if the request was sent - <em>not</em> that the data arrived, which
		/// happens later. <see langword="false"/> means the local user is not connected to the Steam
		/// servers.
		/// </returns>
		/// <remarks>
		/// Completion is signalled by <see cref="SteamMatchmaking.OnLobbyDataChanged"/>, after which
		/// <see cref="GetData"/> and <see cref="Lobby.Data"/> return the fetched values.
		/// <para>
		/// Beware the failure path: the SDK header says a request for a lobby that no longer exists comes
		/// back with its success flag clear, and this binding's dispatcher discards those callbacks
		/// outright. So a refresh of a dead lobby produces <see langword="true"/> here and then silence -
		/// no event ever fires. Do not use the event as your only completion signal; add a timeout.
		/// </para>
		/// </remarks>
		public bool Refresh()
		{
			return SteamMatchmaking.Internal.RequestLobbyData( Id );
		}

		/// <summary>
		/// Max members able to join this lobby. Cannot be over <c>250</c>.
		/// Can only be set by the owner of the lobby.
		/// </summary>
		/// <value>
		/// The current member limit. Per the SDK header, <c>0</c> means no limit is defined; you will also
		/// read <c>0</c> for a lobby that does not exist, so the two are indistinguishable. The <c>250</c>
		/// ceiling comes from Steam's published documentation - the SDK header in this repo does not state
		/// any cap.
		/// </value>
		/// <remarks>
		/// The setter silently discards Steam's success flag. If you are not the owner, or the value is
		/// rejected, nothing is thrown and nothing is logged - the write just does not happen. Read the
		/// property back if you need to confirm it took.
		/// <para>
		/// Lowering the limit below the current <see cref="MemberCount"/> is not documented by Valve; do
		/// not assume it evicts anyone.
		/// </para>
		/// </remarks>
		public int MaxMembers
		{
			get => SteamMatchmaking.Internal.GetLobbyMemberLimit( Id );
			set => SteamMatchmaking.Internal.SetLobbyMemberLimit( Id, value );
		}

		/// <summary>
		/// Makes the lobby fully discoverable: per the SDK header a public lobby is "visible for friends
		/// and in lobby list", so it shows in friends' Steam UI <em>and</em> is returned by
		/// <see cref="LobbyQuery"/> searches. This is what you want for open matchmaking.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if the type was changed. <see langword="false"/> gives no reason;
		/// normally it means the local user does not own the lobby, or the id is invalid.
		/// </returns>
		/// <remarks>
		/// Type is only half of discoverability. The header states that a lobby is returned by lobby list
		/// searches only if it is public or invisible <em>and</em> still joinable, so a public lobby with
		/// <see cref="SetJoinable"/> set to <see langword="false"/> vanishes from search results.
		/// Full lobbies are never returned either.
		/// </remarks>
		public bool SetPublic()
		{
			return SteamMatchmaking.Internal.SetLobbyType( Id, LobbyType.Public );
		}

		/// <summary>
		/// Makes the lobby reachable by invitation only. The SDK header is blunt: "the only way to join the
		/// lobby is to invite someone else". It disappears from lobby searches and from friends' Steam UI.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if the type was changed; <see langword="false"/> otherwise, typically
		/// because the local user does not own the lobby. No reason is reported.
		/// </returns>
		/// <remarks>
		/// Players can still join if they already have the lobby id and you pass it to them out-of-band,
		/// so private means undiscoverable, not access-controlled. Use <see cref="SetJoinable"/> with
		/// <see langword="false"/> to actually close the door.
		/// </remarks>
		public bool SetPrivate()
		{
			return SteamMatchmaking.Internal.SetLobbyType( Id, LobbyType.Private );
		}

		/// <summary>
		/// Hides the lobby from friends' Steam UI while leaving it findable by matchmaking. The name is
		/// misleading: per the SDK header an invisible lobby is still "returned by search", it is just not
		/// visible to friends. This is the type new lobbies are created with.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if the type was changed; <see langword="false"/> otherwise, with no
		/// reason reported.
		/// </returns>
		/// <remarks>
		/// The header describes the intended use: it lets a user be in two lobbies at once, for example to
		/// match groups together. It also states the limit - a user can be in only one regular lobby, but
		/// up to two invisible ones.
		/// </remarks>
		public bool SetInvisible()
		{
			return SteamMatchmaking.Internal.SetLobbyType( Id, LobbyType.Invisible );
		}

		/// <summary>
		/// Restricts the lobby to the owner's social circle: per the SDK header it "shows for friends or
		/// invitees, but not in lobby list", so friends can find and join it but matchmaking searches
		/// cannot.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if the type was changed; <see langword="false"/> otherwise, with no
		/// reason reported.
		/// </returns>
		/// <remarks>
		/// This is the only type that is visible to friends yet excluded from
		/// <see cref="LobbyQuery.RequestAsync"/> results - the header lists only public and invisible
		/// lobbies as searchable.
		/// </remarks>
		public bool SetFriendsOnly()
		{
			return SteamMatchmaking.Internal.SetLobbyType( Id, LobbyType.FriendsOnly );
		}

		/// <summary>
		/// Opens or closes the lobby to new arrivals, independently of its type. Close it when the match
		/// starts so latecomers cannot walk into a game in progress.
		/// </summary>
		/// <param name="b">Whether or not the lobby can be joined.</param>
		/// <returns>
		/// <see langword="true"/> if the flag was changed; <see langword="false"/> otherwise, typically
		/// because the local user does not own the lobby. No reason is reported.
		/// </returns>
		/// <remarks>
		/// The SDK header says new lobbies default to joinable, and that when this is
		/// <see langword="false"/> "no user can join, even if they are a friend or have been invited" - so
		/// this overrides <see cref="InviteFriend"/> entirely.
		/// <para>
		/// It also removes the lobby from search results, because the header requires a lobby to be
		/// joinable as well as public or invisible to be returned by a lobby list request. Closing a lobby
		/// therefore makes it invisible to matchmaking, not merely full.
		/// </para>
		/// </remarks>
		public bool SetJoinable( bool b )
		{
			return SteamMatchmaking.Internal.SetLobbyJoinable( Id, b );
		}

		/// <summary>
		/// [SteamID variant]
		/// Allows the owner to set the game server associated with the lobby. Triggers the
		/// Steammatchmaking.OnLobbyGameCreated event.
		/// </summary>
		/// <param name="steamServer">
		/// The game server's <see cref="SteamId"/>. Use this variant when clients connect by server
		/// identity rather than address. Members receive it through
		/// <see cref="SteamMatchmaking.OnLobbyGameCreated"/>, whose IP and port arguments will be zero.
		/// </param>
		/// <exception cref="System.ArgumentException">
		/// <paramref name="steamServer"/> is not a valid <see cref="SteamId"/>. This is checked here, in
		/// this binding, before Steam is called.
		/// </exception>
		/// <remarks>
		/// This returns nothing and Steam reports no result, so a call by a non-owner fails silently.
		/// <para>
		/// Per the SDK header, Steam takes no action on your behalf: it only notifies the members, and it
		/// is up to each client to leave the lobby and connect. Announcing a server does not close the
		/// lobby - use <see cref="SetJoinable"/> for that.
		/// </para>
		/// </remarks>
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
		/// <param name="ip">
		/// The server's address in a form <c>IPAddress.TryParse</c> accepts. Members receive it as a
		/// numeric address through <see cref="SteamMatchmaking.OnLobbyGameCreated"/>, whose server-id
		/// argument will not be valid.
		/// </param>
		/// <param name="port">The server's port, passed through to Steam unmodified and unvalidated.</param>
		/// <exception cref="System.ArgumentException">
		/// <paramref name="ip"/> could not be parsed as an IP address.
		/// </exception>
		/// <remarks>
		/// Steam's lobby game-server field is a 32-bit IPv4 address, and this binding converts through an
		/// IPv4-only path. An IPv6 string passes the parse check above and then fails during conversion, so
		/// it throws rather than reaching Steam - pass IPv4 here, or use the <see cref="SteamId"/> variant.
		/// <para>
		/// Returns nothing and Steam reports no result, so a call by a non-owner fails silently. As with
		/// the <see cref="SteamId"/> variant, connecting is entirely your game's responsibility.
		/// </para>
		/// </remarks>
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
		/// <param name="ip">Receives the server's IPv4 address as a 32-bit value. Zero if the owner nominated the server by <see cref="SteamId"/> instead.</param>
		/// <param name="port">Receives the server's port. Zero if the owner nominated the server by <see cref="SteamId"/> instead.</param>
		/// <param name="serverId">Receives the server's <see cref="SteamId"/>. Not valid if the owner nominated the server by address instead.</param>
		/// <returns>
		/// <see langword="true"/> if a server has been set on this lobby, in which case the arguments hold
		/// its details. <see langword="false"/> covers both "no server has been set" and "that lobby does
		/// not exist"; the SDK header gives no way to tell them apart. When it is
		/// <see langword="false"/> the arguments are left as you passed them in, so initialise them.
		/// </returns>
		/// <remarks>
		/// Only one addressing scheme is populated - whichever overload of <c>SetGameServer</c> the owner
		/// used. Check <c>serverId.IsValid</c> to decide which to trust rather than assuming.
		/// <para>
		/// This is a poll. To be told the moment a server is nominated, handle
		/// <see cref="SteamMatchmaking.OnLobbyGameCreated"/> instead.
		/// </para>
		/// </remarks>
		public bool GetGameServer( ref uint ip, ref ushort port, ref SteamId serverId )
		{
			return SteamMatchmaking.Internal.GetLobbyGameServer( Id, ref ip, ref port, ref serverId );
		}

		/// <summary>
		/// Gets or sets the owner of the lobby. You must be the lobby owner to set the owner
		/// </summary>
		/// <value>
		/// The current owner. The SDK header says there is always exactly one owner, and that you must be
		/// a member of the lobby to read this - for a lobby you are not in, or that does not exist, the
		/// returned <see cref="Friend"/> wraps an id whose <c>IsValid</c> is <see langword="false"/>.
		/// </value>
		/// <remarks>
		/// Ownership is not stable. Per the header, if the owner leaves, another member automatically
		/// becomes the owner - you are not consulted and no dedicated event is raised, so re-read this
		/// rather than caching it. The header also warns of a rare race: joining just as the owner leaves
		/// can leave you as the owner of a lobby you just entered.
		/// <para>
		/// Assigning transfers ownership. The header requires that you are currently the owner and that the
		/// new owner is already in the lobby; afterwards you are no longer the owner. The setter discards
		/// Steam's success flag, so a rejected transfer is silent - read the property back to confirm.
		/// </para>
		/// </remarks>
		public Friend Owner
		{
			get => new Friend( SteamMatchmaking.Internal.GetLobbyOwner( Id ) );
			set => SteamMatchmaking.Internal.SetLobbyOwner( Id, value.Id );
		}

		/// <summary>
		/// Tests whether a particular user is the lobby owner - the usual "am I the host?" check before
		/// attempting an owner-only operation such as <see cref="SetData"/> or <see cref="SetPublic"/>.
		/// </summary>
		/// <param name="k">The <see cref="SteamId"/> to test against the current owner.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="k"/> currently owns this lobby. Returns
		/// <see langword="false"/> when you are not a member or the lobby does not exist, because
		/// <see cref="Owner"/> yields an invalid id in those cases - so <see langword="false"/> does not
		/// prove someone else owns it.
		/// </returns>
		/// <remarks>
		/// Reads <see cref="Owner"/> on every call, so this is a native call, and the answer can change
		/// between calls if the owner leaves.
		/// </remarks>
		public bool IsOwnedBy( SteamId k ) => Owner.Id == k;
	}
}
