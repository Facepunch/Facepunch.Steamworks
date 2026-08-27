using System.Threading.Tasks;
using System.Collections.Generic;

namespace Steamworks.Data
{
	/// <summary>
	/// A fluent builder for searching Steam's lobby list. Start from
	/// <see cref="SteamMatchmaking.LobbyList"/>, chain as many filter calls as you need, and finish with
	/// <see cref="RequestAsync"/>. Filters match against the lobby data the host published with
	/// <see cref="Lobby.SetData"/>.
	/// </summary>
	/// <remarks>
	/// Filters accumulate: every call adds a constraint rather than replacing previous ones, except the
	/// distance filter, which is a single setting where the last call wins. Nothing reaches Steam until
	/// <see cref="RequestAsync"/> runs, which applies the whole set at once.
	/// <para>
	/// Two things are always filtered out for you and cannot be switched off. The SDK header states that a
	/// lobby list request never returns full lobbies, and that only lobbies which are public or invisible
	/// <em>and</em> still joinable are returned at all - so friends-only and private lobbies are
	/// unreachable through this API by design.
	/// </para>
	/// <para>
	/// This is a mutable <see langword="struct"/> whose filter collections are reference types, so copies
	/// are not independent. Once a query has string, numerical or near-value filters on it, every copy
	/// taken from it shares those collections, and adding a filter to one copy is visible from the others.
	/// Build each query in a single chain from a fresh <see cref="SteamMatchmaking.LobbyList"/> rather than
	/// stashing a partly built query and branching off it.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// Lobby[] lobbies = await SteamMatchmaking.LobbyList
	///                             .WithKeyValue( "mode", "coop" )   // host called SetData( "mode", "coop" )
	///                             .WithHigher( "level", 10 )
	///                             .WithSlotsAvailable( 1 )
	///                             .FilterDistanceClose()
	///                             .WithMaxResults( 25 )
	///                             .RequestAsync();
	///
	/// // null means no matches AND request failure - there is no separate empty array.
	/// if ( lobbies == null )
	/// {
	///     Console.WriteLine( "Nothing found" );
	///     return;
	/// }
	///
	/// RoomEnter enter = await lobbies[0].Join();
	/// </code>
	/// </example>
	public struct LobbyQuery
	{
		// TODO FILTERS
		// AddRequestLobbyListStringFilter
		// - WithoutKeyValue

		#region Distance Filter
		internal LobbyDistanceFilter? distance;

		/// <summary>
		/// Restrict results to the tightest geographic radius Steam offers - per the SDK header, "only
		/// lobbies in the same immediate region will be returned". The best latency and the fewest results.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		/// <remarks>
		/// Distance is a single setting, not an accumulating filter: a later
		/// <see cref="FilterDistanceFar"/> or <see cref="FilterDistanceWorldwide"/> replaces this one
		/// rather than widening it. Steam locates players by mapping their IP address to a region, so this
		/// is approximate. If you set no distance filter at all, Steam applies its own default, which the
		/// header describes as the same region or nearby regions - this binding exposes no method for that
		/// default, so simply omit the call.
		/// </remarks>
		public LobbyQuery FilterDistanceClose()
		{
			distance = LobbyDistanceFilter.Close;
			return this;
		}

		/// <summary>
		/// Widen the search to roughly half the globe. The SDK header recommends this "for games that
		/// don't have many latency requirements" - use it when a populated lobby matters more than ping.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		/// <remarks>
		/// Replaces any distance previously set on this query. Wider than
		/// <see cref="FilterDistanceClose"/> but still bounded, unlike
		/// <see cref="FilterDistanceWorldwide"/>.
		/// </remarks>
		public LobbyQuery FilterDistanceFar()
		{
			distance = LobbyDistanceFilter.Far;
			return this;
		}

		/// <summary>
		/// Disable geographic filtering entirely. The SDK header explicitly does not recommend this: it
		/// will match lobbies "as far as India to NY", where you should "expect multiple seconds of latency
		/// between the clients".
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		/// <remarks>
		/// Replaces any distance previously set on this query. Reasonable as a last-resort fallback when a
		/// narrower search returned nothing, but not as a default. Results are still ordered closest-first
		/// per the header, so prefer the earliest entries.
		/// </remarks>
		public LobbyQuery FilterDistanceWorldwide()
		{
			distance = LobbyDistanceFilter.Worldwide;
			return this;
		}
		#endregion

		#region String key/value filter
		internal Dictionary<string, string> stringFilters;

		/// <summary>
		/// Require that a lobby's data contains this exact key/value pair - the main way to search by
		/// game mode, map, region tag or version. Matches against what the host published with
		/// <see cref="Lobby.SetData"/>.
		/// </summary>
		/// <param name="key">
		/// The lobby data key to test. Must be non-empty and at most <c>255</c> characters, the key length
		/// limit stated in the SDK header.
		/// </param>
		/// <param name="value">
		/// The value the key must equal. Not validated or length-checked by this binding. Lobbies that
		/// never set the key do not match.
		/// </param>
		/// <returns>The query, for chaining.</returns>
		/// <exception cref="System.ArgumentException">
		/// <paramref name="key"/> is <see langword="null"/>, empty, or longer than <c>255</c> characters;
		/// or the same <paramref name="key"/> has already been added to this query, since string filters
		/// are held in a dictionary keyed by name.
		/// </exception>
		/// <remarks>
		/// This binding only ever performs an equality comparison. Steam's underlying string filter also
		/// supports the other comparison types, but they are not reachable from here, and there is no
		/// "key must not equal" or "key absent" filter - see the TODO at the top of this file. For
		/// inequalities, store the value as a number and use <see cref="WithNotEqual"/>,
		/// <see cref="WithHigher"/> or <see cref="WithLower"/>.
		/// <para>
		/// Because one key can carry only one filter per query, you cannot express "map is forest or
		/// desert" in a single request; run separate requests and merge the results.
		/// </para>
		/// </remarks>
		public LobbyQuery WithKeyValue( string key, string value )
		{
			if ( string.IsNullOrEmpty( key ) )
				throw new System.ArgumentException( "Key string provided for LobbyQuery filter is null or empty", nameof( key ) );

			if ( key.Length > SteamMatchmaking.MaxLobbyKeyLength )
				throw new System.ArgumentException( $"Key length is longer than {SteamMatchmaking.MaxLobbyKeyLength}", nameof( key ) );

			if ( stringFilters == null )
				stringFilters = new Dictionary<string, string>();

			stringFilters.Add( key, value );

			return this;
		}
		#endregion

		#region Numerical filters
		internal List<NumericalFilter> numericalFilters;

		/// <summary>
		/// Keep only lobbies whose numeric value under <paramref name="key"/> is strictly less than
		/// <paramref name="value"/> - for upper bounds such as "fewer than 8 players" or "difficulty
		/// below 3".
		/// </summary>
		/// <param name="key">
		/// The lobby data key holding the number. The host must have published it with
		/// <see cref="Lobby.SetData"/>; how Steam interprets a key whose stored text is not numeric is not
		/// documented by Valve.
		/// </param>
		/// <param name="value">The exclusive upper bound.</param>
		/// <returns>The query, for chaining.</returns>
		/// <exception cref="System.ArgumentException">
		/// <paramref name="key"/> is <see langword="null"/>, empty, or longer than <c>255</c> characters.
		/// </exception>
		/// <remarks>
		/// Unlike <see cref="WithKeyValue"/>, numerical filters are held in a list, so the same key may be
		/// filtered more than once - combine this with <see cref="WithHigher"/> to express a range. All
		/// numerical filters must be satisfied.
		/// </remarks>
		public LobbyQuery WithLower( string key, int value )
		{
			AddNumericalFilter( key, value, LobbyComparison.LessThan );
			return this;
		}

		/// <summary>
		/// Keep only lobbies whose numeric value under <paramref name="key"/> is strictly greater than
		/// <paramref name="value"/> - for lower bounds such as "at least this many players already waiting".
		/// </summary>
		/// <param name="key">The lobby data key holding the number, at most <c>255</c> characters.</param>
		/// <param name="value">The exclusive lower bound.</param>
		/// <returns>The query, for chaining.</returns>
		/// <exception cref="System.ArgumentException">
		/// <paramref name="key"/> is <see langword="null"/>, empty, or longer than <c>255</c> characters.
		/// </exception>
		/// <remarks>
		/// Strictly greater than, not "greater than or equal" - Steam has an inclusive comparison but this
		/// binding does not expose it, so subtract one if you need inclusivity. Pair with
		/// <see cref="WithLower"/> for a range.
		/// </remarks>
		public LobbyQuery WithHigher( string key, int value )
		{
			AddNumericalFilter( key, value, LobbyComparison.GreaterThan );
			return this;
		}

		/// <summary>
		/// Keep only lobbies whose numeric value under <paramref name="key"/> is exactly
		/// <paramref name="value"/>. The numeric counterpart of <see cref="WithKeyValue"/>, useful for
		/// protocol or content-version gating so incompatible builds never see each other.
		/// </summary>
		/// <param name="key">The lobby data key holding the number, at most <c>255</c> characters.</param>
		/// <param name="value">The value the key must equal.</param>
		/// <returns>The query, for chaining.</returns>
		/// <exception cref="System.ArgumentException">
		/// <paramref name="key"/> is <see langword="null"/>, empty, or longer than <c>255</c> characters.
		/// </exception>
		public LobbyQuery WithEqual( string key, int value )
		{
			AddNumericalFilter( key, value, LobbyComparison.Equal );
			return this;
		}

		/// <summary>
		/// Exclude lobbies whose numeric value under <paramref name="key"/> equals
		/// <paramref name="value"/> - for skipping a mode or state you never want, such as
		/// "in progress = 1".
		/// </summary>
		/// <param name="key">The lobby data key holding the number, at most <c>255</c> characters.</param>
		/// <param name="value">The value to exclude.</param>
		/// <returns>The query, for chaining.</returns>
		/// <exception cref="System.ArgumentException">
		/// <paramref name="key"/> is <see langword="null"/>, empty, or longer than <c>255</c> characters.
		/// </exception>
		/// <remarks>
		/// Valve does not document how a lobby that never set the key is treated by a not-equal filter, so
		/// do not rely on such lobbies being included or excluded. Publish the key on every lobby if it
		/// matters.
		/// </remarks>
		public LobbyQuery WithNotEqual( string key, int value )
		{
			AddNumericalFilter( key, value, LobbyComparison.NotEqual );
			return this;
		}

		/// <summary>
		/// Test key, initialize numerical filter list if necessary, then add new numerical filter
		/// </summary>
		/// <param name="key">The lobby data key to filter on.</param>
		/// <param name="value">The number to compare against.</param>
		/// <param name="compare">The comparison to apply.</param>
		/// <exception cref="System.ArgumentException">
		/// <paramref name="key"/> is <see langword="null"/>, empty, or longer than <c>255</c> characters.
		/// </exception>
		internal void AddNumericalFilter( string key, int value, LobbyComparison compare )
		{
			if ( string.IsNullOrEmpty( key ) )
				throw new System.ArgumentException( "Key string provided for LobbyQuery filter is null or empty", nameof( key ) );

			if ( key.Length > SteamMatchmaking.MaxLobbyKeyLength )
				throw new System.ArgumentException( $"Key length is longer than {SteamMatchmaking.MaxLobbyKeyLength}", nameof( key ) );

			if ( numericalFilters == null )
				numericalFilters = new List<NumericalFilter>();

			numericalFilters.Add( new NumericalFilter( key, value, compare ) );
		}
		#endregion

		#region Near value filter
		internal Dictionary<string, int> nearValFilters;

		/// <summary>
		/// Order filtered results according to key/values nearest the provided key/value pair.
		/// Can specify multiple near value filters; each successive filter is lower priority than the previous.
		/// </summary>
		/// <param name="key">
		/// The lobby data key to sort by. Must be non-empty and at most <c>255</c> characters. Each key may
		/// be used once per query.
		/// </param>
		/// <param name="value">The ideal value; lobbies are ordered by how close their value is to this.</param>
		/// <returns>The query, for chaining.</returns>
		/// <exception cref="System.ArgumentException">
		/// <paramref name="key"/> is <see langword="null"/>, empty, longer than <c>255</c> characters, or
		/// has already been added to this query.
		/// </exception>
		/// <remarks>
		/// This sorts, it does not filter - it never removes a lobby from the results, so use it together
		/// with the numerical filters if you also need to exclude poor matches. Skill-based ordering is the
		/// usual use.
		/// <para>
		/// The documented priority rule (earlier filters take precedence) depends on the order these reach
		/// Steam. This binding stores them in a dictionary and replays them by enumerating it, and
		/// dictionary enumeration order is not a guaranteed part of the .NET contract - so do not depend on
		/// the relative priority of multiple near-value filters in this binding. One filter is safe.
		/// </para>
		/// </remarks>
		public LobbyQuery OrderByNear( string key, int value )
		{
			if ( string.IsNullOrEmpty( key ) )
				throw new System.ArgumentException( "Key string provided for LobbyQuery filter is null or empty", nameof( key ) );

			if ( key.Length > SteamMatchmaking.MaxLobbyKeyLength )
				throw new System.ArgumentException( $"Key length is longer than {SteamMatchmaking.MaxLobbyKeyLength}", nameof( key ) );

			if ( nearValFilters == null )
				nearValFilters = new Dictionary<string, int>();

			nearValFilters.Add( key, value );

			return this;
		}
		#endregion

		#region Slots Filter
		internal int? slotsAvailable;

		/// <summary>
		/// Keep only lobbies with free space - essential when a group needs to join together, since a
		/// lobby with one seat left is useless to a party of three.
		/// </summary>
		/// <param name="minSlots">
		/// How many slots must be free. The SDK header phrases this as "only lobbies with the specified
		/// number of slots available"; the parameter name here reads as a minimum. Valve does not state
		/// whether the comparison is "at least" or "exactly", so for a party of <c>n</c> pass <c>n</c>
		/// and re-check <see cref="Lobby.MemberCount"/> against <see cref="Lobby.MaxMembers"/> before
		/// committing.
		/// </param>
		/// <returns>The query, for chaining.</returns>
		/// <remarks>
		/// Only one slots filter applies per query - calling this twice keeps the last value rather than
		/// combining. Independent of this filter, the header says a lobby list request never returns full
		/// lobbies at all.
		/// <para>
		/// This is a snapshot from Steam's side. Between the search completing and your
		/// <see cref="Lobby.Join"/>, other players may take the seats, so still check the result of the
		/// join.
		/// </para>
		/// </remarks>
		public LobbyQuery WithSlotsAvailable( int minSlots )
		{
			slotsAvailable = minSlots;
			return this;
		}

		#endregion

		#region Max results filter
		internal int? maxResults;

		/// <summary>
		/// Cap how many lobbies come back. This is a latency control, not just a convenience: the SDK
		/// header notes the client downloads each result's details, so a smaller cap makes the whole
		/// request noticeably faster.
		/// </summary>
		/// <param name="max">
		/// Maximum number of lobbies to return. Not validated by this binding, and Valve documents no
		/// default or ceiling - omit the call to accept Steam's own behaviour rather than passing a
		/// deliberately huge number.
		/// </param>
		/// <returns>The query, for chaining.</returns>
		/// <remarks>
		/// Only one cap applies per query - calling this twice keeps the last value. Combine with
		/// <see cref="FilterDistanceClose"/> for a fast browser refresh; the header says results are
		/// ordered closest first, so a low cap keeps the nearest lobbies rather than an arbitrary subset.
		/// </remarks>
		public LobbyQuery WithMaxResults( int max )
		{
			maxResults = max;
			return this;
		}

		#endregion

		/// <summary>
		/// Replays every filter collected on this query into Steam's pending-request state, immediately
		/// before the lobby list request is issued.
		/// </summary>
		/// <remarks>
		/// The SDK header requires filters to be registered before each and every lobby list request, and
		/// says they are cleared by that request - hence re-applying here rather than at the point the
		/// caller added them. This state belongs to the matchmaking interface, not to this struct, so two
		/// overlapping requests would contribute filters to each other.
		/// </remarks>
		void ApplyFilters()
		{
			if ( distance.HasValue )
			{
				SteamMatchmaking.Internal.AddRequestLobbyListDistanceFilter( distance.Value );
			}

			if ( slotsAvailable.HasValue )
			{
				SteamMatchmaking.Internal.AddRequestLobbyListFilterSlotsAvailable( slotsAvailable.Value );
			}

			if ( maxResults.HasValue )
			{
				SteamMatchmaking.Internal.AddRequestLobbyListResultCountFilter( maxResults.Value );
			}

			if ( stringFilters != null )
			{
				foreach ( var k in stringFilters )
				{
					SteamMatchmaking.Internal.AddRequestLobbyListStringFilter( k.Key, k.Value, LobbyComparison.Equal );
				}
			}

			if( numericalFilters != null )
			{
				foreach ( var n in numericalFilters )
				{
					SteamMatchmaking.Internal.AddRequestLobbyListNumericalFilter( n.Key, n.Value, n.Comparer );
				}
			}

			if( nearValFilters != null )
			{
				foreach (var v in nearValFilters )
				{
					SteamMatchmaking.Internal.AddRequestLobbyListNearValueFilter( v.Key, v.Value );
				}
			}
		}

		/// <summary>
		/// Applies every filter collected on this query and asks Steam for the matching lobbies. This is
		/// the only member here that talks to Steam.
		/// </summary>
		/// <returns>
		/// An array of matching lobbies, ordered closest first per the SDK header, or
		/// <see langword="null"/>. Be careful with the failure contract: <see langword="null"/> means
		/// <em>either</em> that no lobby matched <em>or</em> that the request failed outright. This method
		/// never returns an empty array, and the two cases are not distinguishable - always null-check
		/// before enumerating.
		/// </returns>
		/// <remarks>
		/// The task only completes while Steam callbacks are being pumped.
		/// <para>
		/// Filters are registered with Steam at this point, not when you called the <c>With…</c> methods,
		/// and the SDK header says Steam clears them once the request is made. That state lives on the
		/// matchmaking interface rather than on this struct, so do not run two lobby searches concurrently:
		/// their filters can be applied to whichever request is issued next. Await one before starting
		/// another.
		/// </para>
		/// <para>
		/// The query object itself is not consumed - its filters are still held, so awaiting the same
		/// <see cref="LobbyQuery"/> again re-applies them and re-runs the search. That makes it convenient
		/// for a refresh button, but remember the struct-copy aliasing noted on the type.
		/// </para>
		/// <para>
		/// Results are a snapshot. A lobby can fill up or close between this returning and your
		/// <see cref="Lobby.Join"/>, so always check the <see cref="RoomEnter"/> value the join gives you.
		/// </para>
		/// </remarks>
		public async Task<Lobby[]> RequestAsync()
		{
			ApplyFilters();

			LobbyMatchList_t? list = await SteamMatchmaking.Internal.RequestLobbyList();
			if ( !list.HasValue || list.Value.LobbiesMatching == 0 )
			{
				return null;
			}

			Lobby[] lobbies = new Lobby[list.Value.LobbiesMatching];

			for ( int i = 0; i < list.Value.LobbiesMatching; i++ )
			{
				lobbies[i] = new Lobby { Id = SteamMatchmaking.Internal.GetLobbyByIndex( i ) };
			}

			return lobbies;
		}
	}
}