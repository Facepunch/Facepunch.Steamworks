using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Steamworks.Data;

using QueryType = Steamworks.Ugc.Query;

namespace Steamworks.Ugc
{
	/// <summary>
	/// A fluent builder that describes a search of the Steam Workshop, then runs it a page at a time.
	/// "UGC" (User Generated Content) is Valve's name for anything published to the Workshop: mods,
	/// maps, collections, artwork, videos, guides and controller configurations.
	/// <para>
	/// Build a query by starting from one of the static factories (<see cref="Items"/>, <see cref="All"/>,
	/// <see cref="Collections"/>, ...), chaining constraints onto it, and finishing with
	/// <see cref="GetPageAsync"/>. Nothing is sent to Steam until <see cref="GetPageAsync"/> is awaited,
	/// so building a query is free and cannot fail.
	/// </para>
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>Pages are 50 items.</b> Valve fixes the page size at <c>kNumUGCResultsPerPage = 50</c>
	/// (<c>isteamugc.h</c>); it is not configurable. Page numbers start at <b>1</b>, not 0 &#8212;
	/// <see cref="GetPageAsync"/> throws if you pass anything lower.
	/// </para>
	/// <para>
	/// <b>Dispose every page.</b> <see cref="ResultPage"/> owns a native query handle. If you do not
	/// dispose it the handle leaks for the lifetime of the process. Prefer <c>using</c>.
	/// </para>
	/// <para>
	/// <b>This is a mutable struct, and the fluent methods are not pure.</b> Each builder method assigns
	/// to the receiver's own fields and then returns a copy of it. Two consequences, both verified against
	/// this implementation rather than documented by Valve:
	/// </para>
	/// <list type="bullet">
	/// <item><description>
	/// The variable you call the method on is modified as well as the value you assign the result to.
	/// </description></item>
	/// <item><description>
	/// The tag and key/value collections are reference types, so a query and every copy taken from it
	/// <b>share one list</b>. Branching a stored query into two variants therefore contaminates both
	/// variants and the original: after
	/// <c>var b = q.WithTag( "a" ); var c = q.WithTag( "b" );</c> all three of
	/// <c>q</c>, <c>b</c> and <c>c</c> require <i>both</i> tags. Reusing one query variable across a
	/// paging loop accumulates tags on every iteration for the same reason.
	/// </description></item>
	/// </list>
	/// <para>
	/// Build each query from a fresh factory call rather than branching or reusing one, and this never
	/// bites you. A single straight-line chain always behaves as you would expect.
	/// </para>
	/// <para>
	/// <b>App id.</b> If you never call the internal app setters, both the creator and consumer app
	/// default to <see cref="SteamClient.AppId"/>. Valve requires that at least one of the two is the
	/// currently running app; querying another app's Workshop is not permitted.
	/// </para>
	/// </remarks>
	/// <example>
	/// Fetch the first two pages of ready-to-use items tagged "Weapon", newest first:
	/// <code>
	/// for ( int page = 1; page &lt;= 2; page++ )
	/// {
	///     var result = await Ugc.Query.ItemsReadyToUse
	///                                 .WithTag( "Weapon" )
	///                                 .RankedByPublicationDate()
	///                                 .GetPageAsync( page );
	///
	///     if ( !result.HasValue ) break;      // query failed, or Steam returned a non-OK result
	///
	///     using ( var entries = result.Value )
	///     {
	///         Console.WriteLine( $"page {page}: {entries.ResultCount} of {entries.TotalCount} total" );
	///
	///         foreach ( var item in entries.Entries )
	///             Console.WriteLine( $"  {item.Title} by {item.Owner.Name}" );
	///     }
	/// }
	/// </code>
	/// </example>
	public struct Query
	{
		UgcType matchingType;
		UGCQuery queryType;
		AppId consumerApp;
		AppId creatorApp;
        string searchText;

		/// <summary>
		/// Starts a query for one category of Workshop content. Equivalent to the matching static
		/// factory property, for when the category is only known at run time.
		/// </summary>
		/// <param name="type">
		/// Which kind of Workshop content to match. <see cref="UgcType.All"/> is only valid on a query
		/// that is later restricted to one user via one of the <c>WhereUser*</c> methods &#8212; Valve
		/// notes it "will only be valid for CreateQueryUserUGCRequest requests".
		/// </param>
		public Query( UgcType type ) : this()
		{
			matchingType = type;
		}

		/// <summary>
		/// Every kind of Workshop content at once. Only valid in combination with one of the
		/// <c>WhereUser*</c> methods; on an all-users query Steam rejects it and you get no results.
		/// </summary>
		public static Query All => new Query( UgcType.All );

		/// <summary>
		/// Regular Workshop items &#8212; both microtransaction items and ready-to-use items.
		/// This is the right starting point for a typical mod or map browser.
		/// </summary>
		public static Query Items => new Query( UgcType.Items );

		/// <summary>
		/// Only items flagged as microtransaction content (items sold through the item store rather
		/// than subscribed to for free).
		/// </summary>
		public static Query ItemsMtx => new Query( UgcType.Items_Mtx );

		/// <summary>
		/// Only items a user can subscribe to and use directly, excluding microtransaction items.
		/// </summary>
		public static Query ItemsReadyToUse => new Query( UgcType.Items_ReadyToUse );

		/// <summary>
		/// Only collections &#8212; Workshop entries whose payload is a list of other items rather
		/// than content of their own. Call <see cref="WithChildren"/> to have the member ids returned.
		/// </summary>
		public static Query Collections => new Query( UgcType.Collections );

		/// <summary>
		/// Only artwork submissions.
		/// </summary>
		public static Query Artwork => new Query( UgcType.Artwork );

		/// <summary>
		/// Only videos. A video item stores a URL rather than a downloadable file.
		/// </summary>
		public static Query Videos => new Query( UgcType.Videos );

		/// <summary>
		/// Only screenshots.
		/// </summary>
		public static Query Screenshots => new Query( UgcType.Screenshots );

		/// <summary>
		/// Both web guides and in-game integrated guides.
		/// </summary>
		public static Query AllGuides => new Query( UgcType.AllGuides );

		/// <summary>
		/// Only guides hosted as web pages on the Steam community site.
		/// </summary>
		public static Query WebGuides => new Query( UgcType.WebGuides );

		/// <summary>
		/// Only guides your game downloads and renders itself.
		/// </summary>
		public static Query IntegratedGuides => new Query( UgcType.IntegratedGuides );

		/// <summary>
		/// Everything the running game can actually consume: ready-to-use items plus integrated guides.
		/// </summary>
		public static Query UsableInGame => new Query( UgcType.UsableInGame );

		/// <summary>
		/// Only Steam Input controller configurations published for this app.
		/// </summary>
		public static Query ControllerBindings => new Query( UgcType.ControllerBindings );

		/// <summary>
		/// Only items the game itself manages rather than items published by users. These are created
		/// through the partner site or the Web API and cannot be subscribed to in the usual way.
		/// </summary>
		public static Query GameManagedItems => new Query( UgcType.GameManagedItems );


		/// <summary>
		/// Order results by vote score &#8212; roughly "best rated first". This is Steam's default
		/// ordering and what the Workshop front page shows.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		/// <remarks>
		/// All the <c>RankedBy*</c> orderings on this page apply <b>only</b> to a query that searches
		/// every user's content. They are silently ignored once you call any <c>WhereUser*</c> method
		/// (which switches to the <c>SortBy*</c> family) or <see cref="WithFileId"/> (which fetches
		/// specific ids and has no ordering at all). Nothing warns you; you just get a differently
		/// ordered list.
		/// </remarks>
		public Query RankedByVote() { queryType = UGCQuery.RankedByVote; return this; }

		/// <summary>
		/// Order results newest-published first. The usual choice for a "recently added" tab.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query RankedByPublicationDate() { queryType = UGCQuery.RankedByPublicationDate; return this; }

		/// <summary>
		/// Restrict results to items the developer has explicitly accepted into the game, ordered by
		/// when they were accepted. Acceptance is a flag you set on the partner site; it surfaces on
		/// an item as <see cref="Item.IsAcceptedForUse"/>.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query RankedByAcceptanceDate() { queryType = UGCQuery.AcceptedForGameRankedByAcceptanceDate; return this; }

		/// <summary>
		/// Order by how much an item is trending &#8212; activity over a recent window rather than
		/// all-time score. Pair with <see cref="WithTrendDays"/> to choose the window.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query RankedByTrend() { queryType = UGCQuery.RankedByTrend; return this; }

		/// <summary>
		/// Restrict results to items the local user's Steam friends have favourited, newest first.
		/// Returns nothing if the user has no friends who play this game.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query FavoritedByFriends() { queryType = UGCQuery.FavoritedByFriendsRankedByPublicationDate; return this; }

		/// <summary>
		/// Restrict results to items published by the local user's Steam friends, newest first.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query CreatedByFriends() { queryType = UGCQuery.CreatedByFriendsRankedByPublicationDate; return this; }

		/// <summary>
		/// Order by how often an item has been reported by users. Intended for moderation tooling,
		/// not for player-facing browsing.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query RankedByNumTimesReported() { queryType = UGCQuery.RankedByNumTimesReported; return this; }

		/// <summary>
		/// Restrict results to items published by creators the local user follows, newest first.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query CreatedByFollowedUsers() { queryType = UGCQuery.CreatedByFollowedUsersRankedByPublicationDate; return this; }

		/// <summary>
		/// Restrict results to items the local user has not voted on yet. Useful for building a
		/// "rate this" prompt that will not show the same item twice.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query NotYetRated() { queryType = UGCQuery.NotYetRated; return this; }

		/// <summary>
		/// Order by total vote count ascending &#8212; fewest-voted items first. A way to surface new
		/// or overlooked submissions rather than the same popular items.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query RankedByTotalVotesAsc() { queryType = UGCQuery.RankedByTotalVotesAsc; return this; }

		/// <summary>
		/// Order by raw number of upvotes, ignoring downvotes. Differs from <see cref="RankedByVote"/>,
		/// which uses the computed score.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query RankedByVotesUp() { queryType = UGCQuery.RankedByVotesUp; return this; }

		/// <summary>
		/// Order by relevance to the text passed to <see cref="WhereSearchText"/>. Only meaningful when
		/// search text is set; with no search text there is no relevance to rank by.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query RankedByTextSearch() { queryType = UGCQuery.RankedByTextSearch; return this; }

		/// <summary>
		/// Order by number of distinct users who have ever subscribed, so one user subscribing twice
		/// counts once. A better popularity signal than the raw subscription count.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query RankedByTotalUniqueSubscriptions() { queryType = UGCQuery.RankedByTotalUniqueSubscriptions; return this; }

		/// <summary>
		/// Order by playtime trend over a recent window. Requires that your game reports playtime with
		/// <see cref="Steamworks.SteamUGC.StartPlaytimeTracking"/>; without that this ranks by zero.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query RankedByPlaytimeTrend() { queryType = UGCQuery.RankedByPlaytimeTrend; return this; }

		/// <summary>
		/// Order by total accumulated playtime. Requires playtime tracking; see
		/// <see cref="RankedByPlaytimeTrend"/>.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query RankedByTotalPlaytime() { queryType = UGCQuery.RankedByTotalPlaytime; return this; }

		/// <summary>
		/// Order by average playtime per session over a recent window. Requires playtime tracking.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query RankedByAveragePlaytimeTrend() { queryType = UGCQuery.RankedByAveragePlaytimeTrend; return this; }

		/// <summary>
		/// Order by all-time average playtime per session. Requires playtime tracking.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query RankedByLifetimeAveragePlaytime() { queryType = UGCQuery.RankedByLifetimeAveragePlaytime; return this; }

		/// <summary>
		/// Order by number of play sessions over a recent window. Requires playtime tracking.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query RankedByPlaytimeSessionsTrend() { queryType = UGCQuery.RankedByPlaytimeSessionsTrend; return this; }

		/// <summary>
		/// Order by all-time number of play sessions. Requires playtime tracking.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query RankedByLifetimePlaytimeSessions() { queryType = UGCQuery.RankedByLifetimePlaytimeSessions; return this; }

		#region UserQuery

		SteamId? steamid;

		UserUGCList userType;
		UserUGCListSortOrder userSort;

		internal Query LimitUser( SteamId steamid )
		{
			if ( steamid.Value == 0 )
				steamid = SteamClient.SteamId;

			this.steamid = steamid;
			return this;
		}

		/// <summary>
		/// Restrict the query to items a specific user published. This switches the query into
		/// "one user's content" mode, which changes several other things &#8212; see the remarks.
		/// </summary>
		/// <param name="user">
		/// Whose content to list. Leave this at its default to mean the currently logged in user;
		/// a <see cref="SteamId"/> of 0 is replaced with <see cref="SteamClient.SteamId"/>.
		/// </param>
		/// <returns>The query, for chaining.</returns>
		/// <remarks>
		/// <para>
		/// Calling any <c>WhereUser*</c> method moves the query onto Valve's
		/// <c>CreateQueryUserUGCRequest</c> path. On that path the <c>RankedBy*</c> orderings are not
		/// used at all &#8212; ordering comes from the <c>SortBy*</c> family instead &#8212; and the
		/// all-users-only constraints (<see cref="WhereSearchText"/>, <see cref="MatchAnyTag"/>,
		/// <see cref="MatchAllTags"/>, <see cref="WithTrendDays"/>,
		/// <see cref="AddRequiredKeyValueTag"/>) are ignored by Steam. None of this produces an error;
		/// the constraint is simply not applied.
		/// </para>
		/// <para>
		/// The <c>WhereUser*</c> methods are mutually exclusive: each one overwrites the previous
		/// choice, so only the last call counts.
		/// </para>
		/// <para>
		/// Listing another user's private lists (votes, favourites, subscriptions) generally returns
		/// nothing unless that user's Steam profile privacy allows it. Valve does not document which
		/// lists are gated; only <see cref="WhereUserPublished"/> is reliably visible for other users.
		/// </para>
		/// </remarks>
		public Query WhereUserPublished( SteamId user = default ) { userType = UserUGCList.Published; LimitUser( user ); return this; }

		/// <summary>
		/// Restrict the query to items the user has cast any vote on, up or down.
		/// See <see cref="WhereUserPublished"/> for what switching to user mode changes.
		/// </summary>
		/// <param name="user">Whose list to read. Defaults to the currently logged in user.</param>
		/// <returns>The query, for chaining.</returns>
		public Query WhereUserVotedOn( SteamId user = default ) { userType = UserUGCList.VotedOn; LimitUser( user ); return this; }

		/// <summary>
		/// Restrict the query to items the user voted up.
		/// See <see cref="WhereUserPublished"/> for what switching to user mode changes.
		/// </summary>
		/// <param name="user">Whose list to read. Defaults to the currently logged in user.</param>
		/// <returns>The query, for chaining.</returns>
		public Query WhereUserVotedUp( SteamId user = default ) { userType = UserUGCList.VotedUp; LimitUser( user ); return this; }

		/// <summary>
		/// Restrict the query to items the user voted down.
		/// See <see cref="WhereUserPublished"/> for what switching to user mode changes.
		/// </summary>
		/// <param name="user">Whose list to read. Defaults to the currently logged in user.</param>
		/// <returns>The query, for chaining.</returns>
		public Query WhereUserVotedDown( SteamId user = default ) { userType = UserUGCList.VotedDown; LimitUser( user ); return this; }

		/// <summary>
		/// Restrict the query to items the user marked "I'll rate this later" in the Workshop UI.
		/// See <see cref="WhereUserPublished"/> for what switching to user mode changes.
		/// </summary>
		/// <param name="user">Whose list to read. Defaults to the currently logged in user.</param>
		/// <returns>The query, for chaining.</returns>
		public Query WhereUserWillVoteLater( SteamId user = default ) { userType = UserUGCList.WillVoteLater; LimitUser( user ); return this; }

		/// <summary>
		/// Restrict the query to items the user favourited. Favouriting is a bookmark and is
		/// independent of subscribing &#8212; a favourited item is not necessarily downloaded.
		/// </summary>
		/// <param name="user">Whose list to read. Defaults to the currently logged in user.</param>
		/// <returns>The query, for chaining.</returns>
		public Query WhereUserFavorited( SteamId user = default ) { userType = UserUGCList.Favorited; LimitUser( user ); return this; }

		/// <summary>
		/// Restrict the query to items the user is subscribed to. Subscribing is what causes Steam to
		/// download an item, so this is usually the list a game shows as "your installed mods".
		/// </summary>
		/// <param name="user">Whose list to read. Defaults to the currently logged in user.</param>
		/// <returns>The query, for chaining.</returns>
		/// <remarks>
		/// For the local user's subscriptions you almost always want
		/// <see cref="Steamworks.SteamUGC.GetSubscribedItems"/> instead: it reads the ids straight out
		/// of the Steam client with no network round trip and no paging.
		/// </remarks>
		public Query WhereUserSubscribed( SteamId user = default ) { userType = UserUGCList.Subscribed; LimitUser( user ); return this; }

		/// <summary>
		/// Restrict the query to items the user has actually used or played, as reported by playtime
		/// tracking. Empty unless your game calls
		/// <see cref="Steamworks.SteamUGC.StartPlaytimeTracking"/>.
		/// </summary>
		/// <param name="user">Whose list to read. Defaults to the currently logged in user.</param>
		/// <returns>The query, for chaining.</returns>
		public Query WhereUserUsedOrPlayed( SteamId user = default ) { userType = UserUGCList.UsedOrPlayed; LimitUser( user ); return this; }

		/// <summary>
		/// Restrict the query to items the user follows. Following an item means being notified about
		/// updates to it without subscribing.
		/// </summary>
		/// <param name="user">Whose list to read. Defaults to the currently logged in user.</param>
		/// <returns>The query, for chaining.</returns>
		public Query WhereUserFollowed( SteamId user = default ) { userType = UserUGCList.Followed; LimitUser( user ); return this; }

		/// <summary>
		/// Order a user query newest-created first. This is the default ordering for user queries.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		/// <remarks>
		/// The <c>SortBy*</c> family applies <b>only</b> to queries restricted to one user with a
		/// <c>WhereUser*</c> method. On an all-users query the sort is ignored and the
		/// <c>RankedBy*</c> ordering is used instead. As with the reverse case, this fails silently.
		/// </remarks>
		public Query SortByCreationDate() { userSort = UserUGCListSortOrder.CreationOrderDesc; return this; }

		/// <summary>
		/// Order a user query oldest-created first.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query SortByCreationDateAsc() { userSort = UserUGCListSortOrder.CreationOrderAsc; return this; }

		/// <summary>
		/// Order a user query alphabetically by title, A to Z.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query SortByTitleAsc() { userSort = UserUGCListSortOrder.TitleAsc; return this; }

		/// <summary>
		/// Order a user query by when each item was last updated, most recently updated first.
		/// Useful for showing a creator which of their items are stale.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query SortByUpdateDate() { userSort = UserUGCListSortOrder.LastUpdatedDesc; return this; }

		/// <summary>
		/// Order a user query by when the user subscribed to each item, most recent first. Only
		/// meaningful alongside <see cref="WhereUserSubscribed"/>.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query SortBySubscriptionDate() { userSort = UserUGCListSortOrder.SubscriptionDateDesc; return this; }

		/// <summary>
		/// Order a user query by vote score, highest first.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query SortByVoteScore() { userSort = UserUGCListSortOrder.VoteScoreDesc; return this; }

		/// <summary>
		/// Order a user query for moderation review. Valve does not document what this orders by;
		/// it is intended for moderation tooling rather than player-facing lists.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public Query SortByModeration() { userSort = UserUGCListSortOrder.ForModeration; return this; }

        /// <summary>
        /// Full-text search the title and description of Workshop items. Pair with
        /// <see cref="RankedByTextSearch"/> to order by relevance rather than by score or date.
        /// </summary>
        /// <param name="searchText">
        /// The text to search for. Valve documents no syntax, no escaping rules and no length limit
        /// for this string. Passing <see langword="null"/> or an empty string leaves the query
        /// unfiltered rather than matching nothing.
        /// </param>
        /// <returns>The query, for chaining.</returns>
        /// <remarks>
        /// This is an all-users-query constraint only. Steam ignores it once the query is restricted
        /// to one user with a <c>WhereUser*</c> method.
        /// </remarks>
        public Query WhereSearchText(string searchText) { this.searchText = searchText; return this; }

		#endregion

		#region Files
		PublishedFileId[] Files;

		/// <summary>
		/// Fetch a specific, known set of Workshop items by id instead of searching. This is how you
		/// turn ids you have stored (in a save file, a server config, a collection) back into full
		/// <see cref="Item"/> records with titles, descriptions and preview URLs.
		/// </summary>
		/// <param name="files">
		/// The published file ids to look up. Passing an empty array produces an empty result rather
		/// than an error. Valve documents no upper bound on the count; because results still come back
		/// one page at a time, requesting more than 50 in a single call is not useful.
		/// </param>
		/// <returns>The query, for chaining.</returns>
		/// <remarks>
		/// <para>
		/// This overrides everything else. When file ids are set the query runs through Valve's
		/// <c>CreateQueryUGCDetailsRequest</c>, which takes no ordering, no tags, no search text and
		/// <b>no page number</b> &#8212; the <c>page</c> argument to <see cref="GetPageAsync"/> is
		/// still validated but is not sent to Steam.
		/// </para>
		/// <para>
		/// Ids that do not exist, or that belong to another app, are simply absent from the results,
		/// so the returned count can be lower than the number you asked for. There is no per-id error.
		/// </para>
		/// <para>
		/// Calling this twice replaces the previous array rather than appending to it.
		/// </para>
		/// </remarks>
		public Query WithFileId( params PublishedFileId[] files )
		{
			Files = files;
			return this;
		}
		#endregion

		/// <summary>
		/// Send the query to Steam and await one page of results. This is the only method on
		/// <see cref="Query"/> that touches the network; everything else just accumulates state.
		/// </summary>
		/// <param name="page">
		/// Which page to fetch, <b>starting at 1</b>. Pages are a fixed 50 items. Use
		/// <see cref="ResultPage.TotalCount"/> from the first page to work out how many pages exist.
		/// Ignored when the query was built with <see cref="WithFileId"/>.
		/// </param>
		/// <returns>
		/// A <see cref="ResultPage"/> that <b>the caller must dispose</b>, or <see langword="null"/> if
		/// the request could not be completed. Null covers two different situations that this API
		/// cannot tell apart: the underlying call failed or timed out, or it completed with a result
		/// other than <see cref="Steamworks.Result.OK"/>. If you need to distinguish them, drop to
		/// <c>SteamUGC.Internal.SendQueryUGCRequest</c>.
		/// </returns>
		/// <exception cref="System.Exception">Thrown if <paramref name="page"/> is 0 or negative.</exception>
		/// <remarks>
		/// <para>
		/// An empty page is not an error. A successful query that matched nothing returns a
		/// <see cref="ResultPage"/> with <see cref="ResultPage.ResultCount"/> of 0, which still needs
		/// disposing. Paging past the end of the results does the same.
		/// </para>
		/// <para>
		/// Callbacks must be running for this to ever complete &#8212; that is automatic if you
		/// initialised with <c>SteamClient.Init( appid, asyncCallbacks: true )</c>, and otherwise
		/// requires <c>SteamClient.RunCallbacks()</c> to be pumped every frame. With no pump this task
		/// never finishes.
		/// </para>
		/// <para>
		/// Both the creator and consumer app default to <see cref="SteamClient.AppId"/> the first time
		/// this is called on a query.
		/// </para>
		/// </remarks>
		public async Task<ResultPage?> GetPageAsync( int page )
		{
			if ( page <= 0 ) throw new System.Exception( "page should be > 0" );

			if ( consumerApp == 0 ) consumerApp = SteamClient.AppId;
			if ( creatorApp == 0 ) creatorApp = consumerApp;

			UGCQueryHandle_t handle;

			if ( Files != null )
			{
				handle = SteamUGC.Internal.CreateQueryUGCDetailsRequest( Files, (uint)Files.Length );
			}
			else if ( steamid.HasValue )
			{
				handle = SteamUGC.Internal.CreateQueryUserUGCRequest( steamid.Value.AccountId, userType, matchingType, userSort, creatorApp.Value, consumerApp.Value, (uint)page );
			}
			else
			{
				handle = SteamUGC.Internal.CreateQueryAllUGCRequest( queryType, matchingType, creatorApp.Value, consumerApp.Value, (uint)page );
			}

		    ApplyReturns(handle);

		    if (maxCacheAge.HasValue)
		    {
		        SteamUGC.Internal.SetAllowCachedResponse(handle, (uint)maxCacheAge.Value);
		    }

			ApplyConstraints( handle );

			var result = await SteamUGC.Internal.SendQueryUGCRequest( handle );
			if ( !result.HasValue )
				return null;

			if ( result.Value.Result != Steamworks.Result.OK )
				return null;

			return new ResultPage
			{
				Handle = result.Value.Handle,
				ResultCount = (int) result.Value.NumResultsReturned,
				TotalCount = (int)result.Value.TotalMatchingResults,
				CachedData = result.Value.CachedData,
				ReturnsKeyValueTags = WantsReturnKeyValueTags ?? false,
				ReturnsDefaultStats = WantsDefaultStats ?? true, //true by default
				ReturnsMetadata = WantsReturnMetadata ?? false,
				ReturnsChildren = WantsReturnChildren ?? false,
				ReturnsAdditionalPreviews = WantsReturnAdditionalPreviews ?? false,
			};
		}

	    #region SharedConstraints
		/// <summary>
		/// Change which category of Workshop content the query matches, overriding whichever static
		/// factory the query was started from.
		/// </summary>
		/// <param name="type">The content category to match.</param>
		/// <returns>The query, for chaining.</returns>
		public QueryType WithType( UgcType type ) { matchingType = type; return this; }

		int? maxCacheAge;

		/// <summary>
		/// Allow Steam to answer from its cache rather than hitting the Workshop backend, as long as
		/// the cached copy is younger than the age you give. Cuts latency substantially and is the
		/// polite thing to do for a browse UI the player may page back and forth through.
		/// </summary>
		/// <param name="maxSecondsAge">
		/// How stale a cached response may be, in seconds. Valve documents no maximum and no meaning
		/// for 0; this binding passes the value straight through as an unsigned value, so negative
		/// numbers wrap to very large ages and effectively mean "any cached copy will do".
		/// </param>
		/// <returns>The query, for chaining.</returns>
		/// <remarks>
		/// When a page is served from cache, <see cref="ResultPage.CachedData"/> is
		/// <see langword="true"/>. Vote counts and subscription totals on a cached page are as stale as
		/// the page itself, so do not use a cached query to confirm that a write just landed.
		/// </remarks>
		public QueryType AllowCachedResponse( int maxSecondsAge ) { maxCacheAge = maxSecondsAge; return this; }

		string language;

		/// <summary>
		/// Ask Steam to return item titles and descriptions in a specific language, when the creator
		/// has supplied a translation for it. Items with no translation fall back to the language they
		/// were authored in rather than being filtered out.
		/// </summary>
		/// <param name="lang">
		/// An API language code as used elsewhere in Steamworks &#8212; <c>"english"</c>,
		/// <c>"french"</c>, <c>"schinese"</c> and so on, not an ISO code. Passing
		/// <see langword="null"/> or an empty string leaves the query at Steam's default, which is the
		/// Steam client's own language.
		/// </param>
		/// <returns>The query, for chaining.</returns>
		public QueryType InLanguage( string lang ) { language = lang; return this; }

		int? trendDays;

		/// <summary>
		/// Set the window used by the trend-based orderings, so "trending" can mean the last day, the
		/// last week, or whatever suits your game.
		/// </summary>
		/// <param name="days">
		/// The size of the trend window in days. Valve does not document the permitted range, the
		/// default used when this is not set, or what happens for 0.
		/// </param>
		/// <returns>The query, for chaining.</returns>
		/// <remarks>
		/// Only meaningful alongside <see cref="RankedByTrend"/>, and only on an all-users query. It is
		/// ignored by Steam on a query restricted to one user.
		/// </remarks>
		public QueryType WithTrendDays( int days ) { trendDays = days; return this; }

		List<string> requiredTags;
		bool? matchAnyTag;
		List<string> excludedTags;
		Dictionary<string, string> requiredKv;

		/// <summary>
		/// Treat the required tags as alternatives: an item matches if it carries <b>any one</b> of the
		/// tags added with <see cref="WithTag"/>. Use this for a "show me shotguns or rifles" filter.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		/// <remarks>
		/// This is an all-users-query constraint. Steam ignores it once the query is restricted to one
		/// user with a <c>WhereUser*</c> method.
		/// </remarks>
		public QueryType MatchAnyTag() { matchAnyTag = true; return this; }

		/// <summary>
		/// Treat the required tags as a conjunction: an item matches only if it carries <b>every</b>
		/// tag added with <see cref="WithTag"/>. This is Steam's behaviour when neither this nor
		/// <see cref="MatchAnyTag"/> is called.
		/// </summary>
		/// <returns>The query, for chaining.</returns>
		public QueryType MatchAllTags() { matchAnyTag = false; return this; }

		/// <summary>
		/// Require that matched items carry this Workshop tag. Call it repeatedly to require several;
		/// whether they combine with AND or OR is decided by <see cref="MatchAllTags"/> (the default)
		/// or <see cref="MatchAnyTag"/>.
		/// </summary>
		/// <param name="tag">
		/// The tag text, which must match the tag as configured on the Steamworks partner site for
		/// your app. Valve does not document whether matching is case sensitive.
		/// </param>
		/// <returns>The query, for chaining.</returns>
		/// <remarks>
		/// <b>Tags accumulate into a shared list.</b> Because <see cref="Query"/> is a struct holding a
		/// reference-typed list, every copy taken from a query shares that one list &#8212; see the
		/// remarks on <see cref="Query"/>. Start each query from a fresh factory rather than branching
		/// a stored one.
		/// </remarks>
		public QueryType WithTag( string tag )
		{
			if ( requiredTags == null ) requiredTags = new List<string>();
			requiredTags.Add( tag );
			return this;
		}

		/// <summary>
		/// Require that matched items carry a specific key/value metadata pair. Key/value tags are set
		/// by the publisher through <see cref="Editor.AddKeyValueTag"/> and are the way to filter on
		/// structured data that does not fit the flat tag list &#8212; a schema version, a map size,
		/// a required DLC.
		/// </summary>
		/// <param name="key">The metadata key to match. Valve documents no length limit or character restrictions.</param>
		/// <param name="value">The exact value the key must have. There is no wildcard or range matching.</param>
		/// <returns>The query, for chaining.</returns>
		/// <exception cref="System.ArgumentException">
		/// Thrown if <paramref name="key"/> has already been added to this query. The constraints are
		/// stored in a dictionary, so one key can only carry one required value.
		/// </exception>
		/// <remarks>
		/// This is an all-users-query constraint; Steam ignores it on a query restricted to one user.
		/// Note that <see cref="WithKeyValueTags"/> controls whether key/value tags are <i>returned</i>
		/// on the results, and is a separate switch from filtering on them here.
		/// </remarks>
		public QueryType AddRequiredKeyValueTag(string key, string value)
		{
			if (requiredKv == null) requiredKv = new Dictionary<string, string>();
			requiredKv.Add(key, value);
			return this;
		}

		/// <summary>
		/// Exclude items carrying this tag. Call repeatedly to exclude several; exclusions are always
		/// combined with AND (an item is dropped if it has <i>any</i> excluded tag), regardless of
		/// <see cref="MatchAnyTag"/>.
		/// </summary>
		/// <param name="tag">The tag text to exclude.</param>
		/// <returns>The query, for chaining.</returns>
		/// <remarks>
		/// Like <see cref="WithTag"/>, exclusions accumulate into a list that is shared by every copy
		/// of the query &#8212; see the remarks on <see cref="Query"/>.
		/// </remarks>
		public QueryType WithoutTag( string tag )
		{
			if ( excludedTags == null ) excludedTags = new List<string>();
			excludedTags.Add( tag );
			return this;
		}

		void ApplyConstraints( UGCQueryHandle_t handle )
		{
			if ( requiredTags != null )
			{
				foreach ( var tag in requiredTags )
					SteamUGC.Internal.AddRequiredTag( handle, tag );
			}

			if ( excludedTags != null )
			{
				foreach ( var tag in excludedTags )
					SteamUGC.Internal.AddExcludedTag( handle, tag );
			}

			if ( requiredKv != null )
			{
				foreach ( var tag in requiredKv )
					SteamUGC.Internal.AddRequiredKeyValueTag( handle, tag.Key, tag.Value );
			}

			if ( matchAnyTag.HasValue )
			{
				SteamUGC.Internal.SetMatchAnyTag( handle, matchAnyTag.Value );
			}

			if ( trendDays.HasValue )
			{
				SteamUGC.Internal.SetRankedByTrendDays( handle, (uint)trendDays.Value );
			}

            if ( !string.IsNullOrEmpty( searchText ) )
            {
                SteamUGC.Internal.SetSearchText( handle, searchText );
            }

			if ( !string.IsNullOrEmpty( language ) )
			{
				SteamUGC.Internal.SetLanguage( handle, language );
			}
		}

        #endregion

        #region ReturnValues

	    bool? WantsReturnOnlyIDs;

	    /// <summary>
	    /// Ask Steam to return only published file ids, skipping titles, descriptions, tags and
	    /// everything else. Much cheaper when all you need is the set of ids &#8212; for example to
	    /// diff against what you already have cached.
	    /// </summary>
	    /// <param name="b"><see langword="true"/> to fetch ids only; <see langword="false"/> for full details.</param>
	    /// <returns>The query, for chaining.</returns>
	    /// <remarks>
	    /// With this on, the <see cref="Item"/> values you get back have their id populated and most
	    /// other fields left at their defaults. Do not mistake the empty title for a deleted item.
	    /// </remarks>
	    public QueryType WithOnlyIDs(bool b) { WantsReturnOnlyIDs = b; return this; }

	    bool? WantsReturnKeyValueTags;

		/// <summary>
		/// Ask Steam to include each item's key/value metadata pairs, which arrive as
		/// <see cref="Item.KeyValueTags"/>. Off by default, and the dictionary is left null when it is
		/// off &#8212; so if you read <see cref="Item.KeyValueTags"/> without calling this you get
		/// null rather than an empty dictionary.
		/// </summary>
		/// <param name="b"><see langword="true"/> to return key/value tags.</param>
		/// <returns>The query, for chaining.</returns>
		public QueryType WithKeyValueTags(bool b) { WantsReturnKeyValueTags = b; return this; }

		/// <summary>
		/// Obsolete misspelling of <see cref="WithKeyValueTags"/>. Behaves identically.
		/// </summary>
		/// <param name="b"><see langword="true"/> to return key/value tags.</param>
		/// <returns>The query, for chaining.</returns>
		[Obsolete( "Renamed to WithKeyValueTags" )]
        public QueryType WithKeyValueTag(bool b) { WantsReturnKeyValueTags = b; return this; }

	    bool? WantsReturnLongDescription;

	    /// <summary>
	    /// Ask Steam to return each item's full description rather than a truncated one. Off by
	    /// default because descriptions can be long and most list UIs only need the title.
	    /// </summary>
	    /// <param name="b"><see langword="true"/> to return full descriptions.</param>
	    /// <returns>The query, for chaining.</returns>
	    /// <remarks>
	    /// Valve does not document the length at which a description is truncated when this is off.
	    /// </remarks>
	    public QueryType WithLongDescription(bool b) { WantsReturnLongDescription = b; return this; }

	    bool? WantsReturnMetadata;

	    /// <summary>
	    /// Ask Steam to return each item's developer metadata blob, which arrives as
	    /// <see cref="Item.Metadata"/>. This is free-form text the publisher sets with
	    /// <see cref="Editor.WithMetaData"/> &#8212; typically JSON describing the item to your game.
	    /// </summary>
	    /// <param name="b"><see langword="true"/> to return metadata.</param>
	    /// <returns>The query, for chaining.</returns>
	    /// <remarks>
	    /// Valve caps the blob at <c>k_cchDeveloperMetadataMax = 5000</c> bytes
	    /// (<c>isteamugc.h</c>). Off by default; <see cref="Item.Metadata"/> is null when unset.
	    /// </remarks>
	    public QueryType WithMetadata(bool b) { WantsReturnMetadata = b; return this; }

	    bool? WantsReturnChildren;

	    /// <summary>
	    /// Ask Steam to return the ids of each item's children, which arrive as
	    /// <see cref="Item.Children"/>. Only meaningful for collections, whose entire payload is the
	    /// list of items they contain.
	    /// </summary>
	    /// <param name="b"><see langword="true"/> to return child ids.</param>
	    /// <returns>The query, for chaining.</returns>
	    /// <remarks>
	    /// Children are ids only. To get their titles you need a second query &#8212;
	    /// <see cref="WithFileId"/> with the returned ids is the direct way to do that.
	    /// </remarks>
	    public QueryType WithChildren(bool b) { WantsReturnChildren = b; return this; }

	    bool? WantsReturnAdditionalPreviews;

	    /// <summary>
	    /// Ask Steam to return the extra preview images and videos attached to each item beyond its
	    /// main thumbnail. They arrive as <see cref="Item.AdditionalPreviews"/>.
	    /// </summary>
	    /// <param name="b"><see langword="true"/> to return additional previews.</param>
	    /// <returns>The query, for chaining.</returns>
	    public QueryType WithAdditionalPreviews(bool b) { WantsReturnAdditionalPreviews = b; return this; }

	    bool? WantsReturnTotalOnly;

	    /// <summary>
	    /// Ask Steam for the match count only, skipping the items themselves. Use this to show
	    /// "1,284 results" or to size a pager without paying for a page of data you will not display.
	    /// </summary>
	    /// <param name="b"><see langword="true"/> to fetch only the total.</param>
	    /// <returns>The query, for chaining.</returns>
	    /// <remarks>
	    /// With this on, read <see cref="ResultPage.TotalCount"/>;
	    /// <see cref="ResultPage.ResultCount"/> is 0 and iterating
	    /// <see cref="ResultPage.Entries"/> yields nothing. The page still needs disposing.
	    /// </remarks>
	    public QueryType WithTotalOnly(bool b) { WantsReturnTotalOnly = b; return this; }

	    uint? WantsReturnPlaytimeStats;

	    /// <summary>
	    /// Ask Steam to include playtime statistics covering the last <paramref name="unDays"/> days,
	    /// which populates the <c>DuringTimePeriod</c> fields on each <see cref="Item"/>.
	    /// </summary>
	    /// <param name="unDays">
	    /// How many days of playtime history to aggregate. Valve documents neither the maximum nor the
	    /// meaning of 0.
	    /// </param>
	    /// <returns>The query, for chaining.</returns>
	    /// <remarks>
	    /// The numbers are only non-zero if your game reports playtime with
	    /// <see cref="Steamworks.SteamUGC.StartPlaytimeTracking"/> and
	    /// <see cref="Steamworks.SteamUGC.StopPlaytimeTracking"/>. A game that never calls those sees
	    /// zeroes here forever, which reads as "nobody plays these items" rather than as "not measured".
	    /// </remarks>
	    public QueryType WithPlaytimeStats(uint unDays) { WantsReturnPlaytimeStats = unDays; return this; }

        private void ApplyReturns(UGCQueryHandle_t handle)
	    {
	        if (WantsReturnOnlyIDs.HasValue)
	        {
	            SteamUGC.Internal.SetReturnOnlyIDs(handle, WantsReturnOnlyIDs.Value);
	        }

	        if (WantsReturnKeyValueTags.HasValue)
	        {
	            SteamUGC.Internal.SetReturnKeyValueTags(handle, WantsReturnKeyValueTags.Value);
	        }

	        if (WantsReturnLongDescription.HasValue)
	        {
	            SteamUGC.Internal.SetReturnLongDescription(handle, WantsReturnLongDescription.Value);
	        }

	        if (WantsReturnMetadata.HasValue)
	        {
	            SteamUGC.Internal.SetReturnMetadata(handle, WantsReturnMetadata.Value);
	        }

	        if (WantsReturnChildren.HasValue)
	        {
	            SteamUGC.Internal.SetReturnChildren(handle, WantsReturnChildren.Value);
	        }

	        if (WantsReturnAdditionalPreviews.HasValue)
	        {
	            SteamUGC.Internal.SetReturnAdditionalPreviews(handle, WantsReturnAdditionalPreviews.Value);
	        }

	        if (WantsReturnTotalOnly.HasValue)
	        {
	            SteamUGC.Internal.SetReturnTotalOnly(handle, WantsReturnTotalOnly.Value);
	        }

	        if (WantsReturnPlaytimeStats.HasValue)
	        {
	            SteamUGC.Internal.SetReturnPlaytimeStats(handle, WantsReturnPlaytimeStats.Value);
	        }
	    }

        #endregion

		#region LoadingBehaviour

		bool? WantsDefaultStats; //true by default

		/// <summary>
		/// Control whether per-item popularity statistics are fetched alongside each result.
		/// <b>This is on by default</b>, which is unusual for this builder &#8212; every other
		/// <c>With*</c> switch here defaults to off. Turn it off when you are listing many items and
		/// do not intend to show vote or subscriber counts.
		/// </summary>
		/// <param name="b">
		/// <see langword="false"/> to skip the statistics. When off, every statistic listed below
		/// reads as 0 on the returned items, which is indistinguishable from a genuinely unpopular
		/// item.
		/// </param>
		/// <returns>The query, for chaining.</returns>
		/// <remarks>
		/// <para>
		/// The statistics loaded are <see cref="Item.NumSubscriptions"/>,
		/// <see cref="Item.NumFavorites"/>, <see cref="Item.NumFollowers"/>,
		/// <see cref="Item.NumUniqueSubscriptions"/>, <see cref="Item.NumUniqueFavorites"/>,
		/// <see cref="Item.NumUniqueFollowers"/>, <see cref="Item.NumUniqueWebsiteViews"/>,
		/// <see cref="Item.ReportScore"/>, <see cref="Item.NumSecondsPlayed"/>,
		/// <see cref="Item.NumPlaytimeSessions"/>, <see cref="Item.NumComments"/>,
		/// <see cref="Item.NumSecondsPlayedDuringTimePeriod"/> and
		/// <see cref="Item.NumPlaytimeSessionsDuringTimePeriod"/>.
		/// </para>
		/// <para>
		/// This is a client-side convenience rather than a native flag: this binding issues one
		/// <c>GetQueryUGCStatistic</c> call per statistic per item while enumerating
		/// <see cref="ResultPage.Entries"/>. That is 13 calls per item, so a full page of 50 items
		/// costs 650 extra interop calls. They are local &#8212; no network traffic &#8212; but on a
		/// large browse UI it is measurable.
		/// </para>
		/// <para>
		/// The two <c>DuringTimePeriod</c> statistics stay 0 unless you also call
		/// <see cref="WithPlaytimeStats"/> to specify the period.
		/// </para>
		/// </remarks>
		public QueryType WithDefaultStats( bool b ) { WantsDefaultStats = b; return this; }

		#endregion
	}
}