using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks
{
    /// <summary>
    /// A handle to a Steam group — what the Steam Community calls a group and the API calls a clan.
    /// Games use these for guilds, official game groups and community hubs: read a group's name and
    /// tag, find its owner and officers, and join its chat room. Obtain them from
    /// <see cref="SteamFriends.GetClans"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Like <see cref="Friend"/> this stores nothing but the <see cref="Id"/> — every property is a
    /// fresh native call, and none of them can report an error. A clan the local client knows
    /// nothing about returns empty strings and zero counts rather than failing.
    /// </para>
    /// <para>
    /// How much is readable depends on where the ID came from. Valve's rule is that "for clans a user
    /// is a member of, they will have reasonably up-to-date information, but for others you'll have
    /// to download the info to have the latest" — so groups from
    /// <see cref="SteamFriends.GetClans"/> are generally populated, while an ID from elsewhere may
    /// read as blank. Officer data is never present until you await
    /// <see cref="RequestOfficerList"/>.
    /// </para>
    /// <para>
    /// This binding does not expose Valve's <c>GetClanActivityCounts</c> or
    /// <c>DownloadClanActivityCounts</c>, so there is no way to read how many members are online, in
    /// game or chatting.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// foreach ( var clan in SteamFriends.GetClans() )
    /// {
    ///     Console.WriteLine( $"{clan.Name} [{clan.Tag}] owned by {clan.Owner.Name}" );
    ///
    ///     // Officers are not available until the list has been downloaded.
    ///     if ( !await clan.RequestOfficerList() )
    ///         continue;
    ///
    ///     foreach ( var officer in clan.GetOfficers() )
    ///         Console.WriteLine( $"  officer: {officer.Name}" );
    /// }
    /// </code>
    /// </example>
    public struct Clan
    {
        /// <summary>
        /// The Steam ID of the group. This is the only state the struct carries, and it is never
        /// validated — a <see cref="Clan"/> built from a bogus or non-group ID constructs fine and
        /// simply reads back as empty.
        /// </summary>
        public SteamId Id;

        /// <summary>
        /// Wraps a group's Steam ID so the helpers on this struct can be used with it. No lookup
        /// happens here, so this never blocks and never fails.
        /// </summary>
        /// <param name="id">
        /// The group's Steam ID — a clan ID, not a user ID. Not validated.
        /// </param>
        public Clan(SteamId id)
        {
            Id = id;
        }

        /// <summary>
        /// The group's full display name, as shown on its Community page — the string to put in your
        /// UI.
        /// </summary>
        /// <returns>
        /// The name in UTF-8, or an empty string when the local client has no data for this group.
        /// Empty means "not downloaded" rather than "unnamed", and the two cannot be told apart.
        /// </returns>
        /// <remarks>
        /// Reliable for groups obtained from <see cref="SteamFriends.GetClans"/>, since the user is a
        /// member of those. Valve returns a <c>const char *</c> and documents nothing about its
        /// lifetime; this binding copies it into a managed <see cref="string"/> immediately, so the
        /// value is safe to keep. Assume — an assumption, not documented behaviour — that the native
        /// pointer is invalidated by the next call on this interface.
        /// </remarks>
        public string Name => SteamFriends.Internal.GetClanName(Id);

        /// <summary>
        /// The group's short tag — the abbreviation members can display next to their name, as in
        /// <c>[TAG]</c>. Use it where the full <see cref="Name"/> would not fit, such as scoreboards
        /// and nameplates.
        /// </summary>
        /// <returns>
        /// The tag in UTF-8, or an empty string when the group has no tag set or the client has no
        /// data for it. Those two cases are indistinguishable, and many groups genuinely have no tag,
        /// so always handle empty.
        /// </returns>
        /// <remarks>
        /// Valve documents nothing at all about this call beyond its signature — no length limit, no
        /// character set, no statement of what an empty return means. As with <see cref="Name"/>, the
        /// native string is copied into managed memory before it is returned, and the underlying
        /// pointer is assumed invalidated by the next call on this interface.
        /// </remarks>
        public string Tag => SteamFriends.Internal.GetClanTag(Id);

        /// <summary>
        /// How many people are currently sitting in this group's chat room — the number to show
        /// beside a "join group chat" button.
        /// </summary>
        /// <returns>
        /// The count of users in the chat room, or <c>0</c> when nobody is in it or the local client
        /// cannot see the room.
        /// </returns>
        /// <remarks>
        /// This counts occupants of the live chat room, not the group's membership. A large group
        /// with an empty chat reports <c>0</c>. Valve documents nothing about this call, but in
        /// practice it is only meaningful once the local user has joined the room with
        /// <see cref="SteamFriends.JoinClanChatRoom"/> — this binding does not expose the per-member
        /// iteration (<c>GetChatMemberByIndex</c>) that would let you enumerate them, so the count is
        /// all you get.
        /// </remarks>
        public int ChatMemberCount => SteamFriends.Internal.GetClanChatMemberCount(Id);

        /// <summary>
        /// The group's owner — the single account with full control over it, distinct from the
        /// officers returned by <see cref="GetOfficers"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Friend"/> for the owner. When the owner is unknown this still returns a
        /// <see cref="Friend"/>, wrapping an invalid (zero) Steam ID rather than
        /// <see langword="null"/> — check <c>Owner.Id</c> before using it. The owner's name will be
        /// empty unless their data happens to be cached, so request it before displaying.
        /// </returns>
        /// <remarks>
        /// Unlike <see cref="GetOfficers"/> this does not require <see cref="RequestOfficerList"/>
        /// first. Note that Valve counts the owner among the officers, so the owner also appears in
        /// <see cref="GetOfficers"/>.
        /// </remarks>
        public Friend Owner => new Friend(SteamFriends.Internal.GetClanOwner(Id));

        /// <summary>
        /// Whether the group is publicly visible on the Steam Community rather than invite-only.
        /// Useful for deciding whether it is safe to advertise the group to non-members.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the group is public. <see langword="false"/> covers both a
        /// genuinely private group and a group the local client knows nothing about, which are not
        /// distinguishable.
        /// </returns>
        /// <remarks>
        /// Valve documents nothing about this call beyond its signature. In particular it says
        /// nothing about whether "public" implies joinable, so do not treat this as a permission
        /// check.
        /// </remarks>
        public bool Public => SteamFriends.Internal.IsClanPublic(Id);

        /// <summary>
        /// Whether this is the official Steam group for a game, as opposed to a player-created one.
        /// Use it to give your own game's group special treatment in a list of the player's groups.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the group is an official game group. <see langword="false"/> for
        /// a normal group and also for a group the client has no data for.
        /// </returns>
        /// <remarks>
        /// Valve documents nothing about this call, and in particular this does not tell you
        /// <em>which</em> game the group is official for — a <see langword="true"/> here does not
        /// mean it is your app's group.
        /// </remarks>
        public bool Official => SteamFriends.Internal.IsClanOfficialGameGroup(Id);

        /// <summary>
        /// Downloads the group's officer list into the local cache. You must await this successfully
        /// before <see cref="GetOfficers"/> returns anything — that method is a cache read, not a
        /// request.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the officer list arrived. <see langword="false"/> covers both the
        /// API call failing outright and Steam completing the call but reporting failure; the two are
        /// not distinguishable. Note that Valve restricts this: "you can only ask about clans that a
        /// user is a member of", so a group the local user has not joined fails here.
        /// </returns>
        /// <remarks>
        /// <para>
        /// A backend round trip, so callbacks must be pumped or the returned task never completes.
        /// The result is a snapshot that does not refresh itself — await it again when you need
        /// current data.
        /// </para>
        /// <para>
        /// Valve warns that "this won't download avatars automatically; if you get an officer, and no
        /// avatar image is available, call RequestUserInformation( steamID, false ) to download the
        /// avatar" — in this binding, <c>SteamFriends.RequestUserInformation( id, nameonly: false )</c>
        /// or the avatar helpers on <see cref="Friend"/>.
        /// </para>
        /// </remarks>
        public async Task<bool> RequestOfficerList()
        {
            var req = await SteamFriends.Internal.RequestClanOfficerList(Id);
            return req.HasValue && req.Value.Success != 0x0;
        }

        /// <summary>
        /// The group's officers — the moderators and administrators, including the owner. Use it to
        /// show who to contact about a guild, or to grant in-game privileges to group staff.
        /// </summary>
        /// <returns>
        /// A lazily evaluated sequence of officers, empty when the list has not been downloaded. That
        /// makes an empty result ambiguous between "no officers" and "you forgot to call
        /// <see cref="RequestOfficerList"/>" — and since the latter is by far the more common cause,
        /// treat empty as "not loaded".
        /// </returns>
        /// <remarks>
        /// <para>
        /// Valve is explicit that iteration "can only be done when a RequestClanOfficerList() call has
        /// completed". This method does not check that and does not trigger the request; it silently
        /// yields nothing.
        /// </para>
        /// <para>
        /// Valve states the count includes the owner, so <see cref="Owner"/> appears in this sequence
        /// too. The officers are typically not on the local user's friends list, so their names and
        /// avatars will be blank until requested — see the remarks on <see cref="Friend"/>.
        /// </para>
        /// <para>
        /// Lazy: the count is read once when enumeration begins and each officer is a separate native
        /// call. Materialise it if you intend to walk it more than once.
        /// </para>
        /// </remarks>
        public IEnumerable<Friend> GetOfficers()
        {
            var count = SteamFriends.Internal.GetClanOfficerCount( Id );

            for ( int i = 0; i < count; i++ )
            {
                yield return new Friend(SteamFriends.Internal.GetClanOfficerByIndex(Id, i));
            }
        }
    }
}
