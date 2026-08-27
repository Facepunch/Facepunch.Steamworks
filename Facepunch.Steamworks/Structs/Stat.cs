using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	/// <summary>
	/// A named counter that Steam stores per user, per app — the rows you configure under "Stats" on
	/// the Steamworks App Admin page. Stats are the substrate everything else in this subsystem is
	/// built on: achievement progress rules read them, leaderboards are fed from them, and the
	/// "aggregated" ones roll up into the global numbers on a game's Steam page. Unlike a local save
	/// file they live on Valve's servers, so they survive a reinstall and follow the account to
	/// another machine.
	/// </summary>
	/// <remarks>
	/// <para>
	/// A <see cref="Stat"/> is just a name plus an optional owner. Constructing one costs nothing and
	/// touches no native code — only the methods below talk to Steam, and they re-resolve the name on
	/// every call, so it is safe to keep one in a field for the lifetime of your game.
	/// </para>
	/// <para>
	/// <b>The trap: nothing here works until the local user's stats have arrived.</b> Steam pushes
	/// them shortly after <c>SteamClient.Init</c>, which means that for the first few frames of your
	/// game every read below returns <c>0</c> and every write is thrown away. There is no exception
	/// and no distinguishable return value — a stat that has not loaded is indistinguishable from a
	/// stat that is genuinely zero. The followable rule is:
	/// </para>
	/// <list type="number">
	/// <item><description>Call <c>SteamClient.Init</c>. You do <b>not</b> need to request stats; Steam sends them unprompted.</description></item>
	/// <item><description>Make sure callbacks are being pumped — either leave <c>asyncCallbacks</c> at its default of <see langword="true"/>, or call <c>SteamClient.RunCallbacks()</c> every frame yourself. If neither happens, step 3 never becomes true and this whole struct is inert forever.</description></item>
	/// <item><description>Before the first read or write, check <see cref="SteamUserStats.StatsReceived"/>, or do your first access from a <see cref="SteamUserStats.OnUserStatsReceived"/> handler. Both are equivalent; the flag is set by that same callback.</description></item>
	/// </list>
	/// <para>
	/// The global accessors — <see cref="GetGlobalInt"/>, <see cref="GetGlobalFloat"/> and the two
	/// day-history methods — are a different, unrelated data set (whole-app aggregates rather than
	/// this user's values) and have their own priming requirement, described on each member.
	/// </para>
	/// </remarks>
	/// <example>
	/// Bumping a stat when the player kills something, and flushing it at the end of the round:
	/// <code>
	/// // Somewhere in your game loop. StatsReceived is false for the first few frames.
	/// if ( SteamUserStats.StatsReceived )
	/// {
	///     new Stat( "kills" ).Add( 1 );
	/// }
	///
	/// // At a natural break — end of round, map change, quitting to menu.
	/// // Nothing above reached Valve's servers until this happens.
	/// if ( !new Stat( "kills" ).Store() )
	/// {
	///     // Try again later; do not spin on it, StoreStats is rate limited.
	/// }
	/// </code>
	/// Reading a friend's copy of the same stat, which needs its own round trip first:
	/// <code>
	/// var friend = new Friend( someSteamId );
	/// if ( await friend.RequestUserStatsAsync() )
	/// {
	///     var theirKills = new Stat( "kills", someSteamId ).GetInt();
	/// }
	/// </code>
	/// </example>
	public struct Stat
	{
		/// <summary>
		/// The stat's API Name — the identifier from the "Stats" page of the Steamworks App Admin
		/// panel, not a display name and not anything the player sees.
		/// </summary>
		/// <value>
		/// The name this stat was constructed with, verbatim. Never validated by this binding: a
		/// typo, a stat that only exists in an unpublished configuration, or a name longer than
		/// Valve's 128-byte UTF-8 limit all fail the same silent way at read/write time.
		/// </value>
		public string Name { get; internal set; }

		/// <summary>
		/// Which user's copy of the stat this instance reads. <c>0</c> means the local user, which is
		/// the only user whose stats you are allowed to modify.
		/// </summary>
		/// <value>
		/// The <see cref="SteamId"/> passed to <see cref="Stat(string, SteamId)"/>, or <c>0</c> when
		/// constructed with <see cref="Stat(string)"/>.
		/// </value>
		/// <remarks>
		/// When this is non-zero, every mutating member — <see cref="Set(int)"/>, <see cref="Set(float)"/>,
		/// <see cref="Add(int)"/>, <see cref="Add(float)"/>, <see cref="UpdateAverageRate"/> and
		/// <see cref="Store"/> — throws <see cref="System.Exception"/> rather than failing quietly.
		/// Reads are allowed, but only after <see cref="Friend.RequestUserStatsAsync"/> has completed
		/// successfully for that user; Valve notes those stats are a snapshot and are not kept
		/// up to date afterwards.
		/// </remarks>
		public SteamId UserId { get; internal set; }

		/// <summary>
		/// Targets the local user's copy of a stat — the normal case, and the only form that can be
		/// written to.
		/// </summary>
		/// <param name="name">
		/// The stat's API Name from the Steamworks App Admin panel. Case sensitive, and capped by
		/// Valve at 128 bytes once UTF-8 encoded. Not validated here or at construction time.
		/// </param>
		public Stat( string name )
		{
			Name = name;
			UserId = 0;
		}

		/// <summary>
		/// Targets another user's copy of a stat, for read-only inspection — showing a friend's
		/// career totals next to your own, for example.
		/// </summary>
		/// <param name="name">
		/// The stat's API Name from the Steamworks App Admin panel. Case sensitive, and capped by
		/// Valve at 128 bytes once UTF-8 encoded. Not validated here or at construction time.
		/// </param>
		/// <param name="user">
		/// The user to read from. Passing <c>0</c> is equivalent to using <see cref="Stat(string)"/>
		/// and produces a writable local-user stat; any other value makes this instance read-only.
		/// </param>
		/// <remarks>
		/// You must await <see cref="Friend.RequestUserStatsAsync"/> for <paramref name="user"/>
		/// before reading, otherwise <see cref="GetInt"/> and <see cref="GetFloat"/> return <c>0</c>
		/// with no indication that the data was simply never downloaded.
		/// </remarks>
		public Stat( string name, SteamId user )
		{
			Name = name;
			UserId = user;
		}

		internal void LocalUserOnly( [CallerMemberName] string caller = null )
		{
			if ( UserId == 0 ) return;
			throw new System.Exception( $"Stat.{caller} can only be called for the local user" );
		}

		/// <summary>
		/// The lifetime total of this stat summed across every player of the game, as a
		/// floating-point value — "bullets fired by all players, ever". Use this for the
		/// community-scale numbers games put on a stats screen, not for anything about the local
		/// player.
		/// </summary>
		/// <returns>
		/// The global lifetime total, or <c>0</c> if the stat is not marked "aggregated" in the App
		/// Admin panel, the name is wrong, or <see cref="SteamUserStats.RequestGlobalStatsAsync"/>
		/// has not yet completed. Failure and a genuine total of zero are indistinguishable — this
		/// method discards the underlying success flag.
		/// </returns>
		/// <remarks>
		/// <para>
		/// This is a cache read, not a network call. You must await
		/// <see cref="SteamUserStats.RequestGlobalStatsAsync"/> at least once beforehand or there is
		/// nothing in the cache to read. Global stats are a completely separate data set from the
		/// per-user values returned by <see cref="GetFloat"/>, and are unaffected by
		/// <see cref="SteamUserStats.StatsReceived"/>.
		/// </para>
		/// <para>
		/// Use this overload for stats configured as <c>FLOAT</c>/<c>AVGRATE</c> and
		/// <see cref="GetGlobalInt"/> for <c>INT</c> stats. Valve exposes these as two distinct
		/// native entry points; asking for the wrong one is a silent <c>0</c>, not a conversion.
		/// </para>
		/// </remarks>
		public double GetGlobalFloat()
		{
			double val = 0.0;

			if ( SteamUserStats.Internal.GetGlobalStat( Name, ref val ) )
				return val;

			return 0;
		}

		/// <summary>
		/// The lifetime total of this stat summed across every player of the game, as a 64-bit
		/// integer — the aggregate counter behind "12,483,991 zombies killed by the community".
		/// </summary>
		/// <returns>
		/// The global lifetime total, or <c>0</c> if the stat is not marked "aggregated" in the App
		/// Admin panel, the name is wrong, or <see cref="SteamUserStats.RequestGlobalStatsAsync"/>
		/// has not yet completed. As with <see cref="GetGlobalFloat"/>, failure cannot be told apart
		/// from a genuine zero: this binding ignores the success flag Valve returns.
		/// </returns>
		/// <remarks>
		/// A cache read, not a network call — await <see cref="SteamUserStats.RequestGlobalStatsAsync"/>
		/// first. Use this for stats configured as <c>INT</c>; <c>FLOAT</c> stats must go through
		/// <see cref="GetGlobalFloat"/>.
		/// </remarks>
		public long GetGlobalInt()
		{
			long val = 0;
			SteamUserStats.Internal.GetGlobalStat( Name, ref val );
			return val;
		}

		/// <summary>
		/// Day-by-day history of this stat's global total, newest first — the series you would plot
		/// as "community activity over the last month". Requests the data from Steam and then reads
		/// it back in one call, so unlike <see cref="GetGlobalInt"/> this needs no separate priming.
		/// </summary>
		/// <param name="days">
		/// How many days of history to fetch, counting back from today. Valve caps this at <c>60</c>;
		/// larger values are not rejected here, you simply get fewer rows back. A negative value
		/// throws when the result buffer is allocated.
		/// </param>
		/// <returns>
		/// Daily totals with index <c>0</c> being today, <c>1</c> yesterday and so on, trimmed to the
		/// number of days Steam actually had data for — so the array can be shorter than
		/// <paramref name="days"/>, including empty. Returns <see langword="null"/> if the request
		/// failed, which is the only way to distinguish "the call failed" from "this stat has no
		/// history": a stat that exists but is not marked "aggregated" returns an empty array, not
		/// <see langword="null"/>.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Awaiting this issues a fresh network round trip every time it is called; it is not cached
		/// and not cheap. Fetch once and hold the array rather than calling it per frame.
		/// </para>
		/// <para>
		/// The <see langword="await"/> only completes while callbacks are being pumped — with
		/// <c>asyncCallbacks: false</c> you must be calling <c>SteamClient.RunCallbacks()</c> or this
		/// never returns.
		/// </para>
		/// <para>
		/// Use this for stats configured as <c>INT</c>; use <see cref="GetGlobalFloatDays"/> for
		/// <c>FLOAT</c> stats.
		/// </para>
		/// </remarks>
		public async Task<long[]> GetGlobalIntDaysAsync( int days )
		{
			var result = await SteamUserStats.Internal.RequestGlobalStats( days );
			if ( result?.Result != Result.OK  ) return null;

			var r = new long[days];

			var rows = SteamUserStats.Internal.GetGlobalStatHistory( Name, r, (uint) r.Length * sizeof(long) );
			
			if ( days != rows )
				r = r.Take( rows ).ToArray();

			return r;
		}

		/// <summary>
		/// Day-by-day history of this stat's global total as floating-point values, newest first.
		/// The <c>FLOAT</c>/<c>AVGRATE</c> counterpart of <see cref="GetGlobalIntDaysAsync"/>; despite
		/// the name it is asynchronous and must be awaited.
		/// </summary>
		/// <param name="days">
		/// How many days of history to fetch, counting back from today. Valve caps this at <c>60</c>;
		/// larger values are not rejected here, you simply get fewer rows back. A negative value
		/// throws when the result buffer is allocated.
		/// </param>
		/// <returns>
		/// Daily totals with index <c>0</c> being today, <c>1</c> yesterday and so on, trimmed to the
		/// number of days Steam actually had data for. Returns <see langword="null"/> if the request
		/// failed; a stat that exists but is not marked "aggregated" comes back as an empty array
		/// instead.
		/// </returns>
		/// <remarks>
		/// Issues a network round trip on every call — fetch once and keep the result. The
		/// <see langword="await"/> only completes while callbacks are being pumped.
		/// </remarks>
		public async Task<double[]> GetGlobalFloatDays( int days )
		{
			var result = await SteamUserStats.Internal.RequestGlobalStats( days );
			if ( result?.Result != Result.OK ) return null;

			var r = new double[days];

			var rows = SteamUserStats.Internal.GetGlobalStatHistory( Name, r, (uint)r.Length * sizeof( double ) );

			if ( days != rows )
				r = r.Take( rows ).ToArray();

			return r;
		}

		/// <summary>
		/// The current value of this stat for the user this instance targets, read as a
		/// <see langword="float"/>. This is the read you use for anything the player has accumulated
		/// — distance travelled, accuracy, hours survived.
		/// </summary>
		/// <returns>
		/// The stat's value, or <c>0</c> on any failure. The failure modes are all silent and all
		/// look identical to a real zero: the local user's stats have not arrived yet (see
		/// <see cref="SteamUserStats.StatsReceived"/>), the stat is configured as <c>INT</c> rather
		/// than <c>FLOAT</c>, the API Name does not exist or is unpublished, or — when
		/// <see cref="UserId"/> is set — <see cref="Friend.RequestUserStatsAsync"/> was never awaited
		/// for that user. This binding discards the success flag Valve returns, so the caller has no
		/// way to tell these apart.
		/// </returns>
		/// <remarks>
		/// A local cache read, not a network call, so it is cheap enough to call per frame once the
		/// data has arrived. Reads the local user when <see cref="UserId"/> is <c>0</c> and the named
		/// user otherwise.
		/// </remarks>
		public float GetFloat()
		{
			float val = 0.0f;

			if ( UserId > 0 )
			{
				SteamUserStats.Internal.GetUserStat( UserId, Name, ref val );
			}
			else
			{
				SteamUserStats.Internal.GetStat( Name, ref val );
			}

			return val;
		}

		/// <summary>
		/// The current value of this stat for the user this instance targets, read as an
		/// <see langword="int"/> — the counter form: kills, matches played, items crafted.
		/// </summary>
		/// <returns>
		/// The stat's value, or <c>0</c> on any failure, indistinguishably. The causes are the same
		/// as for <see cref="GetFloat"/>: stats not yet received, wrong underlying type (this
		/// overload only reads stats configured as <c>INT</c>), an unknown or unpublished API Name,
		/// or a <see cref="UserId"/> whose stats were never downloaded with
		/// <see cref="Friend.RequestUserStatsAsync"/>.
		/// </returns>
		/// <remarks>
		/// Because a not-yet-loaded stat reads as <c>0</c>, calling this early and writing the result
		/// back is how progress gets erased — see the warning on <see cref="Add(int)"/>.
		/// </remarks>
		public int GetInt()
		{
			int val = 0;

			if ( UserId > 0 )
			{
				SteamUserStats.Internal.GetUserStat( UserId, Name, ref val );
			}
			else
			{
				SteamUserStats.Internal.GetStat( Name, ref val );
			}

			return val;
		}

		/// <summary>
		/// Overwrites the local user's copy of an <c>INT</c> stat. The write lands in Steam's local
		/// cache immediately and is only sent to Valve's servers when you call <see cref="Store"/>.
		/// </summary>
		/// <param name="val">
		/// The new value. Steam enforces the min/max and "only increases" constraints you configured
		/// in the App Admin panel — a rejected value is not reported here, it surfaces later as a
		/// failed store and the server sends back its own value instead.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if Steam accepted the value into its local cache.
		/// <see langword="false"/> means the write was dropped — most often because the local user's
		/// stats have not arrived yet (<see cref="SteamUserStats.StatsReceived"/>), but also for an
		/// unknown API Name or a stat configured as <c>FLOAT</c> rather than <c>INT</c>. A
		/// <see langword="true"/> here says nothing about whether the value will survive the server
		/// round trip.
		/// </returns>
		/// <exception cref="System.Exception">
		/// <see cref="UserId"/> is non-zero. You can only write your own stats; Steam has no
		/// client-side API for writing another player's.
		/// </exception>
		public bool Set( int val )
		{
			LocalUserOnly();
			return SteamUserStats.Internal.SetStat( Name, val );
		}

		/// <summary>
		/// Overwrites the local user's copy of a <c>FLOAT</c> stat. Local until <see cref="Store"/>.
		/// </summary>
		/// <param name="val">
		/// The new value. Subject to the same server-side constraints as <see cref="Set(int)"/>,
		/// which are not validated at this point.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the value was accepted into the local cache;
		/// <see langword="false"/> if it was dropped — stats not yet received, unknown API Name, or a
		/// stat configured as <c>INT</c> rather than <c>FLOAT</c>.
		/// </returns>
		/// <exception cref="System.Exception"><see cref="UserId"/> is non-zero.</exception>
		public bool Set( float val )
		{
			LocalUserOnly();
			return SteamUserStats.Internal.SetStat( Name, val );
		}

		/// <summary>
		/// Increments an <c>INT</c> stat by <paramref name="val"/>. Convenience only — Steam has no
		/// atomic increment, so this is literally <see cref="GetInt"/> followed by
		/// <see cref="Set(int)"/>.
		/// </summary>
		/// <param name="val">
		/// Amount to add. Negative values subtract, which Steam will reject at store time if the
		/// stat is configured to only increase.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the resulting value was accepted into the local cache.
		/// <see langword="false"/> propagates straight from <see cref="Set(int)"/> and has the same
		/// meaning.
		/// </returns>
		/// <exception cref="System.Exception"><see cref="UserId"/> is non-zero.</exception>
		/// <remarks>
		/// <b>Do not call this before stats have arrived.</b> The read half returns <c>0</c> when the
		/// cache is empty, so an early <c>Add( 1 )</c> computes <c>0 + 1</c> and sets the stat to
		/// <c>1</c>, discarding whatever the player had accumulated. In practice the write is usually
		/// dropped too and no damage is done, but do not rely on that — gate on
		/// <see cref="SteamUserStats.StatsReceived"/>. Being read-modify-write, it is also not safe
		/// to interleave with concurrent writes to the same stat.
		/// </remarks>
		public bool Add( int val )
		{
			LocalUserOnly();
			return Set( GetInt() + val );
		}

		/// <summary>
		/// Increments a <c>FLOAT</c> stat by <paramref name="val"/>, via a
		/// <see cref="GetFloat"/>/<see cref="Set(float)"/> pair. Steam provides no atomic increment.
		/// </summary>
		/// <param name="val">Amount to add. Negative values subtract.</param>
		/// <returns>
		/// <see langword="true"/> if the resulting value was accepted into the local cache; otherwise
		/// <see langword="false"/>, with the same meaning as <see cref="Set(float)"/>.
		/// </returns>
		/// <exception cref="System.Exception"><see cref="UserId"/> is non-zero.</exception>
		/// <remarks>
		/// Carries the same read-modify-write hazard as <see cref="Add(int)"/>: called before
		/// <see cref="SteamUserStats.StatsReceived"/> is <see langword="true"/>, the read half
		/// silently yields <c>0</c> and the stat is rebased from zero.
		/// </remarks>
		public bool Add( float val )
		{
			LocalUserOnly();
			return Set( GetFloat() + val );
		}

		/// <summary>
		/// Feeds one session's worth of data into an <c>AVGRATE</c> stat — the stat type Steam uses
		/// for sliding-window averages such as "points per hour" or "kills per minute". You supply
		/// the numerator and denominator for this session and Steam maintains the running average
		/// itself; you never set an <c>AVGRATE</c> stat's value directly.
		/// </summary>
		/// <param name="count">
		/// How much of the counted thing happened during this session — the numerator. Not
		/// cumulative: report only what happened since your last call.
		/// </param>
		/// <param name="sessionlength">
		/// How long that session lasted — the denominator, in whatever time unit the stat's window
		/// was configured with in the App Admin panel (commonly seconds or hours). This must match
		/// the stat's configuration, and nothing in this binding or in Steam will tell you if it
		/// does not; the average simply comes out wrong by a constant factor.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if Steam accepted the sample. <see langword="false"/> means it was
		/// dropped — stats not yet received (<see cref="SteamUserStats.StatsReceived"/>), an unknown
		/// API Name, or a stat that is not of type <c>AVGRATE</c>.
		/// </returns>
		/// <exception cref="System.Exception"><see cref="UserId"/> is non-zero.</exception>
		/// <remarks>
		/// <para>
		/// Like every other write here, the sample stays local until <see cref="Store"/> is called.
		/// </para>
		/// <para>
		/// Valve's header declares <c>UpdateAvgRateStat</c> with no accompanying comment at all — no
		/// units, no ranges, no failure conditions. The parameter meanings above are the standard
		/// Steamworks <c>AVGRATE</c> contract and the naming of the native parameters
		/// (<c>flCountThisSession</c>, <c>dSessionLength</c>); treat them as informed inference, not
		/// as documented behaviour. Note also that Valve takes the session length as a
		/// <see langword="double"/> while this binding narrows it to <see langword="float"/>.
		/// </para>
		/// </remarks>
		public bool UpdateAverageRate( float count, float sessionlength )
		{
			LocalUserOnly();
			return SteamUserStats.Internal.UpdateAvgRateStat( Name, count, sessionlength );
		}

		/// <summary>
		/// Flushes every pending stat and achievement change for the local user to Valve's servers.
		/// Until this succeeds, nothing you set with <see cref="Set(int)"/>, <see cref="Add(int)"/>
		/// or <see cref="UpdateAverageRate"/> exists anywhere but in memory.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if the upload was started. This is <b>not</b> confirmation that the
		/// server accepted it — the real outcome arrives later on
		/// <see cref="SteamUserStats.OnUserStatsStored"/>. <see langword="false"/> means nothing was
		/// sent at all, typically because the local user's stats have not arrived
		/// (<see cref="SteamUserStats.StatsReceived"/>) or the app has no published stats configured.
		/// </returns>
		/// <exception cref="System.Exception"><see cref="UserId"/> is non-zero.</exception>
		/// <remarks>
		/// <para>
		/// This is not per-stat despite living on <see cref="Stat"/> — it is the same global flush as
		/// <see cref="SteamUserStats.StoreStats"/> and commits every dirty stat and achievement at
		/// once. <see cref="Name"/> is not used.
		/// </para>
		/// <para>
		/// <b>Retry contract.</b> On <see langword="false"/>, nothing reached the server and your
		/// local changes are still pending, so retrying later is safe and is what Valve advises. Do
		/// not retry in a tight loop: the call is rate limited and should be made on the order of
		/// minutes — at round end, on map change, when the player leaves — not every frame.
		/// </para>
		/// <para>
		/// <b>Rejection contract.</b> A store that starts successfully can still be rejected: Valve
		/// documents that <see cref="SteamUserStats.OnUserStatsStored"/> reporting
		/// <see cref="Result.InvalidParam"/> means one or more stats violated their configured
		/// constraints or were out of date. In that case the server pushes its own values back and
		/// you must re-read your stats to resync — your local values are wrong from that point on.
		/// </para>
		/// <para>
		/// Valve calls this automatically if your process exits with unsaved changes, but that is a
		/// backstop, not a strategy — a crash loses everything not yet stored.
		/// </para>
		/// </remarks>
		public bool Store()
		{
			LocalUserOnly();
			return SteamUserStats.Internal.StoreStats();
		}
	}
}
