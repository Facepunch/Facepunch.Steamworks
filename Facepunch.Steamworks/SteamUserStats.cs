using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Stats, achievements and leaderboards for the logged-in player. This is the entry point for
	/// everything that shows up on a player's Steam profile: the achievement showcase, the stats box,
	/// and the global leaderboards linked from the community hub.
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>Read this before using anything below.</b> None of the per-user stat or achievement members
	/// work until Steam has delivered this user's data. You do not request it — Steam pushes it
	/// shortly after <see cref="SteamClient.Init"/> — but that means there is a window of several
	/// frames at startup during which every achievement reads as locked, every stat reads as
	/// <c>0</c>, and every write is discarded without an error. The concrete rule:
	/// </para>
	/// <list type="number">
	/// <item><description>Call <see cref="SteamClient.Init"/>.</description></item>
	/// <item><description>Keep callbacks pumping. Leave <c>asyncCallbacks</c> at its default of <see langword="true"/>, or call <see cref="SteamClient.RunCallbacks"/> yourself every frame. Skip this and step 3 never happens.</description></item>
	/// <item><description>Gate your first read or write on <see cref="StatsReceived"/>, or hang it off <see cref="OnUserStatsReceived"/>. Do not do stats work in the same frame you initialise.</description></item>
	/// </list>
	/// <para>
	/// <b>Writes are local until stored.</b> <see cref="SetStat(string, int)"/>,
	/// <see cref="AddStat(string, int)"/> and unlocking an <see cref="Achievement"/> all change only
	/// Steam's in-memory copy. <see cref="StoreStats"/> is what sends them to Valve, and it is rate
	/// limited — call it at round end or on major state changes, not per frame.
	/// </para>
	/// <para>
	/// The leaderboard members are unaffected by <see cref="StatsReceived"/>; they are their own
	/// asynchronous system. See <see cref="FindLeaderboardAsync"/>.
	/// </para>
	/// </remarks>
	/// <example>
	/// A complete, realistic startup-to-store cycle:
	/// <code>
	/// SteamClient.Init( 480 );
	///
	/// // Wait for the data instead of guessing at a frame count.
	/// SteamUserStats.OnUserStatsReceived += ( steamid, result ) =>
	/// {
	///     if ( steamid != SteamClient.SteamId || result != Result.OK ) return;
	///
	///     // Safe from here on: the cache is populated.
	///     var kills = SteamUserStats.GetStatInt( "kills" );
	/// };
	///
	/// // ...later, during play. StatsReceived is the same signal as the callback above.
	/// if ( SteamUserStats.StatsReceived )
	/// {
	///     SteamUserStats.AddStat( "kills", 1 );
	/// }
	///
	/// // ...at the end of the round. Nothing above is durable until this succeeds.
	/// if ( !SteamUserStats.StoreStats() )
	/// {
	///     // Keep the pending values and try again at the next round end.
	/// }
	/// </code>
	/// Reading and unlocking achievements:
	/// <code>
	/// foreach ( var a in SteamUserStats.Achievements )
	/// {
	///     Console.WriteLine( $"{a.Identifier}: {a.Name} - {( a.State ? "unlocked" : "locked" )}" );
	/// }
	///
	/// // Trigger() stores by default, so the unlock toast appears immediately.
	/// new Achievement( "ACH_WIN_ONE_GAME" ).Trigger();
	/// </code>
	/// </example>
	public class SteamUserStats : SteamClientClass<SteamUserStats>
	{
		internal static ISteamUserStats Internal => Interface as ISteamUserStats;

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamUserStats( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			InstallEvents();
			RequestCurrentStats();

			return true;
		}

		/// <summary>
		/// Whether Steam has delivered this user's stats and achievements yet. Until this is
		/// <see langword="true"/>, reads return zero/locked and writes are discarded.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Steam sends stats automatically shortly after <see cref="SteamClient.Init"/> — you
		/// do not request them. This flips to <see langword="true"/> when the
		/// <c>UserStatsReceived_t</c> callback for the local user arrives, which means it is
		/// <b>false for the first few frames of your game</b>. Reading an achievement before
		/// then reports it as locked, and writing a stat before then is silently lost.
		/// </para>
		/// <para>
		/// Gate your stats code on this, or subscribe to <see cref="OnUserStatsReceived"/>.
		/// Note it only ever becomes <see langword="true"/> while callbacks are being pumped
		/// — see the getting-started guide on <c>asyncCallbacks</c>.
		/// </para>
		/// </remarks>
		public static bool StatsReceived { get; internal set; }

		/// <summary>
		/// Whether Steam has delivered this user's stats and achievements yet.
		/// </summary>
		/// <remarks>
		/// Misspelled. Use <see cref="StatsReceived"/> instead. Kept so existing code keeps
		/// compiling; it reads and writes the same underlying state.
		/// </remarks>
		[Obsolete( "Misspelled - use StatsReceived instead. This forwards to it.", false )]
		public static bool StatsRecieved
		{
			get => StatsReceived;
			internal set => StatsReceived = value;
		}

		internal static void InstallEvents()
		{
			Dispatch.Install<UserStatsReceived_t>( x =>
			{
				if ( x.SteamIDUser == SteamClient.SteamId )
					StatsReceived = true;

				OnUserStatsReceived?.Invoke( x.SteamIDUser, x.Result );
			} );

			Dispatch.Install<UserStatsStored_t>( x => OnUserStatsStored?.Invoke( x.Result ) );
			Dispatch.Install<UserAchievementStored_t>( x => OnAchievementProgress?.Invoke( new Achievement( x.AchievementNameUTF8() ), (int) x.CurProgress, (int)x.MaxProgress ) );
			Dispatch.Install<UserStatsUnloaded_t>( x => OnUserStatsUnloaded?.Invoke( x.SteamIDUser ) );
			Dispatch.Install<UserAchievementIconFetched_t>( x => OnAchievementIconFetched?.Invoke( x.AchievementNameUTF8(), x.IconHandle ) );
		}


		/// <summary>
		/// Invoked when an achivement icon is loaded.
		/// </summary>
		internal static event Action<string, int> OnAchievementIconFetched;

		/// <summary>
		/// Raised when a user's stats and achievements have arrived from the server. Subscribe to
		/// this and do your first stat read from the handler — it is the earliest moment at which
		/// anything in this class returns real data.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The <see cref="SteamId"/> argument identifies <b>whose</b> stats arrived, and it is not
		/// always the local player: it also fires for other users after
		/// <see cref="Friend.RequestUserStatsAsync"/>. Compare it against <c>SteamClient.SteamId</c>
		/// before treating it as the startup signal, exactly as this class does internally when it
		/// sets <see cref="StatsReceived"/>.
		/// </para>
		/// <para>
		/// The <see cref="Result"/> argument is <see cref="Result.OK"/> on success. Valve documents
		/// that a request for a user who has no stats comes back as <see cref="Result.Fail"/> — the
		/// event still fires, so do not assume being invoked means data is available.
		/// </para>
		/// <para>
		/// Subscribe before or immediately after <see cref="SteamClient.Init"/>. This is not a sticky
		/// event: a handler added after the callback has already fired is never called for it, so a
		/// late subscriber must also check <see cref="StatsReceived"/>.
		/// </para>
		/// </remarks>
		public static event Action<SteamId, Result> OnUserStatsReceived;

		/// <summary>
		/// Raised when a <see cref="StoreStats"/> upload finishes, carrying the server's verdict.
		/// This — not <see cref="StoreStats"/>'s return value — is how you find out whether your
		/// stats were actually persisted.
		/// </summary>
		/// <remarks>
		/// <see cref="Result.OK"/> means the values are committed. Valve documents
		/// <see cref="Result.InvalidParam"/> as meaning one or more stats were rejected, either
		/// because they broke the constraints configured in the App Admin panel or because they were
		/// out of date; in that case the server sends its own values back and you must re-read every
		/// stat to resync, because your local copies are now wrong.
		/// </remarks>
		public static event Action<Result> OnUserStatsStored;

		/// <summary>
		/// Raised when an achievement is stored, or when an
		/// <see cref="IndicateAchievementProgress"/> notification is shown. Useful for driving your
		/// own in-game progress UI off the same signal Steam uses.
		/// </summary>
		/// <remarks>
		/// The two <see langword="int"/> arguments are current progress and maximum progress. Valve
		/// documents the sentinel: <b>if both are zero the achievement has been fully unlocked</b>
		/// rather than being at 0-of-0 progress. The <see cref="Achievement"/> argument is
		/// constructed from the API Name Steam reports, so it can be compared with
		/// <see cref="Achievement.Identifier"/>.
		/// </remarks>
		public static event Action<Achievement, int, int> OnAchievementProgress;


		/// <summary>
		/// Raised when a user's stats have been unloaded from memory, invalidating everything you
		/// have read about them.
		/// </summary>
		/// <remarks>
		/// After this fires for a given <see cref="SteamId"/>, reads for that user return <c>0</c>
		/// and <see langword="false"/> again. Valve's guidance is to call
		/// <see cref="Friend.RequestUserStatsAsync"/> to get them back. Note that this class does not
		/// clear <see cref="StatsReceived"/> in response to this event, so that flag can be
		/// <see langword="true"/> while the local user's data is no longer resident.
		/// </remarks>
		public static event Action<SteamId> OnUserStatsUnloaded;

		/// <summary>
		/// Every achievement defined for this app, in the order Steam lists them. Enumerate this to
		/// build an achievements screen without hard-coding API Names, or to show locked entries
		/// alongside unlocked ones.
		/// </summary>
		/// <value>
		/// A lazily evaluated sequence of <see cref="Achievement"/> handles. The sequence is never
		/// <see langword="null"/>, but it is empty if the app has no published achievements, and it
		/// can also be empty before Steam has finished initialising.
		/// </value>
		/// <remarks>
		/// <para>
		/// The count is fixed by your Steamworks configuration and is read once per enumeration; the
		/// names themselves are fetched one native call at a time as you iterate. Each element is
		/// only a name — reading <see cref="Achievement.State"/> or
		/// <see cref="Achievement.UnlockTime"/> off it still requires that stats have arrived
		/// (<see cref="StatsReceived"/>), otherwise every achievement reports as locked.
		/// </para>
		/// <para>
		/// Because it is deferred, calling <c>.Count()</c> or iterating twice repeats all of that
		/// native work. Materialise it once with <c>.ToArray()</c> if you need it more than once.
		/// Valve notes games generally should not need this at all, since they usually have their
		/// achievement list compiled in.
		/// </para>
		/// </remarks>
		public static IEnumerable<Achievement> Achievements
		{
			get
			{
				// Hoisted: this was a native call per achievement per enumeration. The
				// achievement count is fixed by the app's Steamworks configuration.
				var count = Internal.GetNumAchievements();

				for ( int i = 0; i < count; i++ )
				{
					yield return new Achievement( Internal.GetAchievementName( (uint) i ) );
				}
			}
		}

		/// <summary>
		/// Show the user a pop-up notification with their current progress toward an
		/// achievement — the "17 of 50 zombies killed" toast.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Returns <see langword="false"/>, without telling you which, if: stats have not
		/// arrived yet (wait for <see cref="StatsReceived"/>), the achievement name does not
		/// exist or has unpublished changes in the Steamworks Admin page, or the achievement
		/// is already unlocked.
		/// </para>
		/// <para>
		/// This <b>only shows a toast</b> — it does not record progress. To store progress
		/// you set a stat and let Steam's achievement progress rules act on it. Calling this
		/// without also storing a stat produces a notification that is forgotten immediately.
		/// </para>
		/// </remarks>
		/// <param name="achName">The achievement's API name from the Steamworks Admin page (not its display name).</param>
		/// <param name="curProg">Current progress. Must be less than <paramref name="maxProg"/>.</param>
		/// <param name="maxProg">The value at which the achievement unlocks.</param>
		/// <returns>
		/// <see langword="true"/> if the notification was shown. <see langword="false"/> is a single
		/// undifferentiated failure — see the remarks for the possible causes; the caller cannot tell
		/// which one occurred.
		/// </returns>
		/// <exception cref="ArgumentNullException"><paramref name="achName"/> is null or empty.</exception>
		/// <exception cref="ArgumentException"><paramref name="curProg"/> is greater than or equal to <paramref name="maxProg"/>. Steam has nothing to show for a completed achievement — unlock it instead.</exception>
		public static bool IndicateAchievementProgress( string achName, int curProg, int maxProg )
		{
			if ( string.IsNullOrEmpty( achName ) )
				throw new ArgumentNullException( "Achievement string is null or empty" );

			if ( curProg >= maxProg )
				throw new ArgumentException( $" Current progress [{curProg}] arguement toward achievement greater than or equal to max [{maxProg}]" );

			return Internal.IndicateAchievementProgress( achName, (uint)curProg, (uint)maxProg );
		}

		/// <summary>
		/// How many people are playing this game right now, worldwide — online and offline players
		/// combined. The number you would put on a "N players in game" banner.
		/// </summary>
		/// <returns>
		/// The current player count, or <c>-1</c> if the request failed. <c>-1</c> is the only
		/// failure signal, and it is unambiguous: a genuine result of zero comes back as <c>0</c>.
		/// </returns>
		/// <remarks>
		/// A live network round trip, not a cached value — do not poll it per frame. It is
		/// independent of <see cref="StatsReceived"/>, so it works before the user's own stats have
		/// arrived. Like all awaitable calls here, it only completes while callbacks are being
		/// pumped.
		/// </remarks>
		public static async Task<int> PlayerCountAsync()
		{
			var result = await Internal.GetNumberOfCurrentPlayers();
			if ( !result.HasValue || result.Value.Success == 0 )
				return -1;

			return result.Value.CPlayers;
		}

		/// <summary>
		/// Commits every pending stat and achievement change for the local user to Valve's servers.
		/// This is the only thing that makes progress durable — everything set through
		/// <see cref="SetStat(string, int)"/>, <see cref="AddStat(string, int)"/> or
		/// <see cref="Achievement.Trigger"/> lives only in memory until it succeeds.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if the upload was started. It succeeds when
		/// <see cref="StatsReceived"/> is <see langword="true"/> <b>and</b> the app has stats
		/// configured and published in the Steamworks partner backend. <see langword="false"/> means
		/// nothing at all was sent, and your pending values are still pending — no partial write is
		/// possible.
		/// </returns>
		/// <remarks>
		/// <para>
		/// <b>A <see langword="true"/> return is not confirmation of storage.</b> It only means the
		/// request left. The server's verdict arrives on <see cref="OnUserStatsStored"/>, plus one
		/// callback per newly unlocked achievement. Valve documents that a result of
		/// <see cref="Result.InvalidParam"/> there means one or more stats were rejected for breaking
		/// their configured constraints or being out of date — the server then sends back its own
		/// values, and you must re-read your stats to resync.
		/// </para>
		/// <para>
		/// <b>Retry contract.</b> On failure nothing was sent, so retrying is safe and is what Valve
		/// advises. Do not retry in a loop: the call is rate limited and should be made on the order
		/// of minutes. Call it on major state changes — end of round, map change, the player leaving
		/// a server — not every frame.
		/// </para>
		/// <para>
		/// The one exception to that pacing: this call is what makes the achievement unlock toast
		/// appear, so it is worth calling soon after unlocking an achievement. (That is why
		/// <see cref="Achievement.Trigger"/> calls it for you by default.)
		/// </para>
		/// <para>
		/// Steam calls this automatically if your process exits with unsaved changes, but a crash
		/// still loses them. Debug output is written to
		/// <c>%steam_install%\logs\stats_log.txt</c>.
		/// </para>
		/// </remarks>
		public static bool StoreStats()
		{
			return Internal.StoreStats();
		}

		/// <summary>
		/// Does nothing. Kept only so that code written against older Steamworks versions still
		/// compiles.
		/// </summary>
		/// <returns>
		/// Always <see langword="true"/>. It is a hard-coded constant, not a status — Valve removed
		/// the underlying <c>RequestCurrentStats</c> entry point and this binding makes no native
		/// call at all.
		/// </returns>
		/// <remarks>
		/// <b>Do not treat a <see langword="true"/> return as "stats are now available".</b> The
		/// Steam client synchronises stats and achievements on its own after
		/// <see cref="SteamClient.Init"/>; the signal that they have actually arrived is
		/// <see cref="StatsReceived"/> or <see cref="OnUserStatsReceived"/>. This method is called
		/// once during interface setup purely for backwards compatibility.
		/// </remarks>
		[Obsolete( "No longer required. Automatically handled by the Steam client.", false )]
		public static bool RequestCurrentStats()
		{
			return true;
		}

		/// <summary>
		/// Asynchronously fetches global stats data, which is available for stats marked as 
		/// "aggregated" in the App Admin panel of the Steamworks website.
		/// Stats must have arrived first — check <see cref="StatsReceived"/>, or wait for
		/// <see cref="OnUserStatsReceived"/>. Steam delivers them automatically after
		/// <see cref="SteamClient.Init"/>; there is nothing to request.
		/// </summary>
		/// <param name="days">How many days of day-by-day history to retrieve in addition to the overall totals. The limit is <c>60</c>.</param>
		/// <returns><see cref="Result.OK"/> indicates success, <see cref="Result.InvalidState"/> means the user's stats have not arrived yet (see <see cref="StatsReceived"/>), <see cref="Result.Fail"/> means the remote call failed</returns>
		/// <remarks>
		/// <para>
		/// This populates a cache; it does not return the numbers. Once it reports
		/// <see cref="Result.OK"/>, read the values with <see cref="Stat.GetGlobalInt"/> or
		/// <see cref="Stat.GetGlobalFloat"/>. Skip this call and those getters silently return
		/// <c>0</c>.
		/// </para>
		/// <para>
		/// Global stats cover the whole player base, not the local user, and are only produced for
		/// stats flagged "aggregated" in App Admin. This is a network round trip — call it once and
		/// refresh occasionally, not per frame.
		/// </para>
		/// </remarks>
		public static async Task<Result> RequestGlobalStatsAsync( int days )
		{
			var result = await SteamUserStats.Internal.RequestGlobalStats( days );
			if ( !result.HasValue ) return Result.Fail;
			return result.Value.Result;
		}


		/// <summary>
		/// Looks up a leaderboard by name and creates it on the spot if it does not exist. Use this
		/// only when leaderboards are genuinely dynamic — one per daily challenge, per user-made
		/// level — because leaderboards created this way are second-class citizens.
		/// </summary>
		/// <param name="name">
		/// The leaderboard's name, capped by Valve at 128 bytes once UTF-8 encoded. This is the
		/// internal name, not the community display name.
		/// </param>
		/// <param name="sort">
		/// Which direction counts as "best", and therefore what rank 1 means.
		/// <see cref="LeaderboardSort.Ascending"/> makes the lowest score the top score (times, par
		/// scores); <see cref="LeaderboardSort.Descending"/> makes the highest score the top score.
		/// Only applied when the leaderboard is created — it is ignored if one already exists under
		/// this name.
		/// </param>
		/// <param name="display">
		/// How the Steam Community website should format the score column
		/// (<see cref="LeaderboardDisplay.Numeric"/>, <see cref="LeaderboardDisplay.TimeSeconds"/> or
		/// <see cref="LeaderboardDisplay.TimeMilliSeconds"/>). Presentation only; it does not change
		/// the value you upload. Also ignored if the leaderboard already exists.
		/// </param>
		/// <returns>
		/// A handle to the leaderboard, or <see langword="null"/> if the call failed outright.
		/// Because this variant creates on miss, <see langword="null"/> here means a real failure —
		/// no Steam connection, a rejected name — rather than "not found".
		/// </returns>
		/// <remarks>
		/// <para>
		/// Leaderboards created this way do <b>not</b> appear in the Steam Community until you fill
		/// in their Community Name field by hand in the App Admin panel. Prefer defining leaderboards
		/// in App Admin and calling <see cref="FindLeaderboardAsync"/> instead.
		/// </para>
		/// <para>
		/// This is a network round trip and is a separate step from reading any scores — the returned
		/// handle is only a key, and each <see cref="Leaderboard"/> read is another round trip. Cache
		/// the handle; do not call this every time you want a score list.
		/// </para>
		/// </remarks>
		public static async Task<Leaderboard?> FindOrCreateLeaderboardAsync( string name, LeaderboardSort sort, LeaderboardDisplay display )
		{
			var result = await Internal.FindOrCreateLeaderboard( name, sort, display );
			if ( !result.HasValue || result.Value.LeaderboardFound == 0 )
				return null;

			return new Leaderboard { Id = result.Value.SteamLeaderboard };
		}


		/// <summary>
		/// Looks up an existing leaderboard by name. This is the normal way in: define your
		/// leaderboards in the Steamworks App Admin panel, then resolve them here at runtime.
		/// </summary>
		/// <param name="name">
		/// The leaderboard's name exactly as configured in App Admin. Capped by Valve at 128 bytes
		/// once UTF-8 encoded. Never creates anything — an unrecognised name is simply a miss.
		/// </param>
		/// <returns>
		/// A handle to the leaderboard, or <see langword="null"/>. <see langword="null"/> covers both
		/// "no leaderboard with that name exists" and "the request itself failed", and this binding
		/// collapses the two — you cannot tell a typo from a network problem.
		/// </returns>
		/// <remarks>
		/// <para>
		/// <b>Finding a leaderboard is a separate round trip from reading it.</b> This call resolves
		/// a name to a handle and nothing more; it downloads no entries. Getting scores is a second
		/// asynchronous call on the returned <see cref="Leaderboard"/> — see
		/// <see cref="Leaderboard.GetScoresAsync"/> — and submitting a score is a third. Budget for
		/// two awaits before you can show a scoreboard.
		/// </para>
		/// <para>
		/// Handles stay valid for the session, so resolve once at startup and keep the result.
		/// Leaderboards are independent of <see cref="StatsReceived"/> and work before the user's own
		/// stats have arrived.
		/// </para>
		/// </remarks>
		/// <example>
		/// <code>
		/// var board = await SteamUserStats.FindLeaderboardAsync( "Fastest Lap" );
		/// if ( board == null )
		/// {
		///     // Wrong name, or the lookup failed - you cannot tell which.
		///     return;
		/// }
		///
		/// // Second round trip: the handle above carries no entries.
		/// var top = await board.Value.GetScoresAsync( 10 );
		/// </code>
		/// </example>
		public static async Task<Leaderboard?> FindLeaderboardAsync( string name )
		{
			var result = await Internal.FindLeaderboard( name );
			if ( !result.HasValue || result.Value.LeaderboardFound == 0 )
				return null;

			return new Leaderboard { Id = result.Value.SteamLeaderboard };
		}



		/// <summary>
		/// Increments an <c>INT</c> stat for the local user. A convenience wrapper only — Steam
		/// offers no atomic increment, so this reads the current value with
		/// <see cref="GetStatInt"/> and writes the sum back with <see cref="SetStat(string, int)"/>.
		/// </summary>
		/// <param name="name">
		/// The stat's API Name from the Steamworks App Admin panel — the internal identifier, not a
		/// display name. Case sensitive, and capped by Valve at 128 bytes once UTF-8 encoded.
		/// </param>
		/// <param name="amount">
		/// How much to add. Negative values subtract, which Steam rejects at store time for stats
		/// configured to only increase. Because of the <see langword="int"/>/<see langword="float"/>
		/// overload pair, always pass this explicitly rather than relying on the default.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the new value was accepted into Steam's local cache. This is
		/// just <see cref="SetStat(string, int)"/>'s result and carries its meaning:
		/// <see langword="false"/> most often means the user's stats have not arrived yet
		/// (<see cref="StatsReceived"/>), but also covers an unknown API Name or a stat that is not
		/// of type <c>INT</c>.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Local only. Nothing is persisted until <see cref="StoreStats"/> is called.
		/// </para>
		/// <para>
		/// <b>Do not call this before <see cref="StatsReceived"/> is <see langword="true"/>.</b> The
		/// read half yields <c>0</c> when the cache is empty, so an early call computes from zero and
		/// would rebase the player's total — the accompanying write is normally dropped as well, but
		/// that is a coincidence, not a guarantee.
		/// </para>
		/// </remarks>
		public static bool AddStat( string name, int amount = 1 )
		{
			var val = GetStatInt( name );
			val += amount;
			return SetStat( name, val );
		}

		/// <summary>
		/// Increments a <c>FLOAT</c> stat for the local user, via a
		/// <see cref="GetStatFloat"/>/<see cref="SetStat(string, float)"/> pair. Steam has no atomic
		/// increment; this exists purely for convenience.
		/// </summary>
		/// <param name="name">
		/// The stat's API Name from the Steamworks App Admin panel. Case sensitive, capped at 128
		/// UTF-8 bytes.
		/// </param>
		/// <param name="amount">
		/// How much to add; negative values subtract. Pass this explicitly — with both an
		/// <see langword="int"/> and a <see langword="float"/> overload defaulting this parameter,
		/// omitting it leaves the compiler nothing to choose on.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the new value reached Steam's local cache; otherwise
		/// <see langword="false"/>, with the same undifferentiated causes as
		/// <see cref="SetStat(string, float)"/>.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Local until <see cref="StoreStats"/>. Carries the same read-modify-write hazard as
		/// <see cref="AddStat(string, int)"/> when called before <see cref="StatsReceived"/> is
		/// <see langword="true"/>.
		/// </para>
		/// <para>
		/// <paramref name="amount"/> has no default, deliberately. It used to default to
		/// <c>1.0f</c> while <see cref="AddStat(string, int)"/> defaulted to <c>1</c>, which made
		/// the one-argument call <c>AddStat( "name" )</c> ambiguous between the two overloads and
		/// a compile error (CS0121) — so both defaults were unreachable. Dropping this one lets
		/// the one-argument form compile and resolve to the integer overload, which is what
		/// "increment by one" means for the counter stats it is normally used on. Removing it
		/// could not break existing callers, because no call that relied on it could ever have
		/// compiled. Pass the amount explicitly for float stats.
		/// </para>
		/// </remarks>
		public static bool AddStat( string name, float amount )
		{
			var val = GetStatFloat( name );
			val += amount;
			return SetStat( name, val );
		}

		/// <summary>
		/// Overwrites an <c>INT</c> stat for the local user. Use this when you know the absolute
		/// value — a high score, a completion percentage — rather than a delta.
		/// </summary>
		/// <param name="name">
		/// The stat's API Name from the Steamworks App Admin panel, not a display name. Case
		/// sensitive, capped by Valve at 128 bytes once UTF-8 encoded. Never validated by this
		/// binding.
		/// </param>
		/// <param name="value">
		/// The new value. Min/max and "only increases" rules configured in App Admin are enforced by
		/// the server, not here — a value that breaks them is accepted locally and rejected later, at
		/// store time.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if Steam took the value into its local cache. <see langword="false"/>
		/// means it was dropped, with no way to tell which cause applied: the user's stats have not
		/// arrived (<see cref="StatsReceived"/>), the API Name is unknown or unpublished, or the stat
		/// is configured as <c>FLOAT</c> rather than <c>INT</c>.
		/// </returns>
		/// <remarks>
		/// <b>This does not store anything.</b> Despite what older revisions of this comment claimed,
		/// no call to <see cref="StoreStats"/> is made here — the value is in memory only until you
		/// call it yourself, and is lost if the process is killed.
		/// </remarks>
		public static bool SetStat( string name, int value )
		{
			return Internal.SetStat( name, value );
		}

		/// <summary>
		/// Overwrites a <c>FLOAT</c> stat for the local user.
		/// </summary>
		/// <param name="name">
		/// The stat's API Name from the Steamworks App Admin panel. Case sensitive, capped at 128
		/// UTF-8 bytes, never validated here.
		/// </param>
		/// <param name="value">
		/// The new value. Server-side constraints are checked at store time, not now.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the value reached Steam's local cache; <see langword="false"/>
		/// if it was dropped — stats not yet received, unknown API Name, or a stat configured as
		/// <c>INT</c> rather than <c>FLOAT</c>. The three are indistinguishable.
		/// </returns>
		/// <remarks>
		/// <b>This does not store anything.</b> Contrary to what older revisions of this comment
		/// said, <see cref="StoreStats"/> is not called for you.
		/// </remarks>
		public static bool SetStat( string name, float value )
		{
			return Internal.SetStat( name, value );
		}

		/// <summary>
		/// Reads an <c>INT</c> stat for the local user out of Steam's local cache.
		/// </summary>
		/// <param name="name">
		/// The stat's API Name from the Steamworks App Admin panel, not a display name. Case
		/// sensitive, capped at 128 UTF-8 bytes.
		/// </param>
		/// <returns>
		/// The stat's value, or <c>0</c> on failure — and <b>you cannot tell the two apart</b>. This
		/// binding discards the success flag Valve returns, so a stat that has legitimately never
		/// been incremented, a misspelled name, a stat configured as <c>FLOAT</c>, and stats that
		/// simply have not arrived yet all read as <c>0</c>.
		/// </returns>
		/// <remarks>
		/// A memory read, not a network call — cheap enough for per-frame UI once the data is there.
		/// Check <see cref="StatsReceived"/> before trusting the result; before that flag flips this
		/// returns <c>0</c> for everything.
		/// </remarks>
		public static int GetStatInt( string name )
		{
			int data = 0;
			Internal.GetStat( name, ref data );
			return data;
		}

		/// <summary>
		/// Reads a <c>FLOAT</c> stat for the local user out of Steam's local cache.
		/// </summary>
		/// <param name="name">
		/// The stat's API Name from the Steamworks App Admin panel. Case sensitive, capped at 128
		/// UTF-8 bytes.
		/// </param>
		/// <returns>
		/// The stat's value, or <c>0</c> on failure, indistinguishably — the success flag is
		/// discarded here just as in <see cref="GetStatInt"/>. Causes include an unknown API Name, a
		/// stat configured as <c>INT</c>, and stats not having arrived yet.
		/// </returns>
		/// <remarks>
		/// A memory read, not a network call. Gate on <see cref="StatsReceived"/> before trusting the
		/// result.
		/// </remarks>
		public static float GetStatFloat( string name )
		{
			float data = 0;
			Internal.GetStat( name, ref data );
			return data;
		}

		/// <summary>
		/// Wipes every stat for the local user back to its default, and optionally relocks every
		/// achievement. Intended for development and QA — it is the fastest way to re-test a
		/// progression path — and is destructive and irreversible for a real player.
		/// </summary>
		/// <param name="includeAchievements">
		/// <see langword="true"/> to also relock all achievements. <see langword="false"/> resets
		/// stats only, which can leave achievements unlocked while the progress that earned them
		/// reads as zero.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the reset was applied to Steam's local cache;
		/// <see langword="false"/> if it was rejected, most commonly because the user's stats have
		/// not arrived yet (<see cref="StatsReceived"/>).
		/// </returns>
		/// <remarks>
		/// Like every other write in this class, the reset is local until <see cref="StoreStats"/>
		/// pushes it to the server. There is no undo once it has been stored.
		/// </remarks>
		public static bool ResetAll( bool includeAchievements )
		{
			return Internal.ResetAllStats( includeAchievements );
		}
	}
}
