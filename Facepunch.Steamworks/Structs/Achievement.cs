using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	/// <summary>
	/// A handle to one of this app's achievements — the badges on a player's Steam profile. Use it to
	/// read whether the local player has earned it, to fetch its localised name, description and
	/// icon for an in-game achievements screen, and to unlock it.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The struct holds nothing but the achievement's API Name; every member below is a live lookup
	/// against Steam. Constructing one is free and never fails, even for a name that does not exist —
	/// a typo shows up later as an achievement that is permanently locked with an empty name.
	/// </para>
	/// <para>
	/// <b>Reads are meaningless until stats have arrived.</b> Steam pushes the local player's
	/// achievement state shortly after <c>SteamClient.Init</c>, so during the first few frames
	/// <see cref="State"/> is <see langword="false"/> for everything and <see cref="UnlockTime"/> is
	/// <see langword="null"/> for everything — identical to a player who has earned nothing. Gate on
	/// <see cref="SteamUserStats.StatsReceived"/> or work from a
	/// <see cref="SteamUserStats.OnUserStatsReceived"/> handler, and keep callbacks pumping.
	/// </para>
	/// <para>
	/// Enumerate <see cref="SteamUserStats.Achievements"/> to discover every achievement rather than
	/// hard-coding the list.
	/// </para>
	/// </remarks>
	/// <example>
	/// Building an achievements screen and unlocking one:
	/// <code>
	/// if ( !SteamUserStats.StatsReceived ) return; // nothing is readable yet
	///
	/// foreach ( var a in SteamUserStats.Achievements )
	/// {
	///     // Icons stream in asynchronously; the first GetIcon() is usually null.
	///     var icon = await a.GetIconAsync();
	///
	///     AddRow( a.Name, a.Description, a.State, icon, a.UnlockTime );
	/// }
	///
	/// // Unlocking. Trigger() stores by default, so the Steam toast appears right away.
	/// var win = new Achievement( "ACH_WIN_ONE_GAME" );
	/// if ( !win.State )
	/// {
	///     win.Trigger();
	/// }
	/// </code>
	/// </example>
	public struct Achievement
	{
		internal string Value;

		/// <summary>
		/// Creates a handle to the achievement with this API Name.
		/// </summary>
		/// <param name="name">
		/// The achievement's API Name from the "Achievements" page of the Steamworks App Admin panel
		/// — the internal identifier, <b>not</b> the display name shown to players. Case sensitive,
		/// and capped by Valve at 128 bytes once UTF-8 encoded. Nothing is validated here: an unknown
		/// name constructs successfully and simply never unlocks.
		/// </param>
		public Achievement( string name )
		{
			Value = name;
		}

		/// <summary>
		/// The achievement's API Name — the same value as <see cref="Identifier"/>, suitable for logs
		/// and for keying dictionaries. Deliberately not the player-visible <see cref="Name"/>.
		/// </summary>
		/// <returns>The API Name this handle was constructed with, verbatim.</returns>
		public override string ToString() => Value;

		/// <summary>
		/// Whether the local player has unlocked this achievement. The flag you branch on to grey out
		/// a row, or to avoid re-triggering something already earned.
		/// </summary>
		/// <value>
		/// <see langword="true"/> if unlocked. <see langword="false"/> means locked <i>or</i> that the
		/// lookup failed — this binding discards the success flag Valve returns, so an unknown API
		/// Name and the startup window before <see cref="SteamUserStats.StatsReceived"/> is
		/// <see langword="true"/> both read as an ordinary locked achievement.
		/// </value>
		/// <remarks>
		/// This reflects local state, which after <see cref="Trigger"/> can be ahead of the server:
		/// it flips to <see langword="true"/> as soon as the unlock is set locally, before
		/// <c>StoreStats</c> has persisted anything.
		/// </remarks>
		public bool State
		{
			get
			{
				var state = false;
				SteamUserStats.Internal.GetAchievement( Value, ref state );
				return state;
			}
		}

		/// <summary>
		/// The achievement's API Name — the stable internal identifier from the Steamworks App Admin
		/// panel. This is what you persist, compare and send over the network; <see cref="Name"/> is
		/// localised and must never be used as a key.
		/// </summary>
		/// <value>
		/// Exactly the string this handle was constructed with. Purely local — no call into Steam,
		/// and no validation that the achievement exists.
		/// </value>
		public string Identifier => Value;

		/// <summary>
		/// The player-facing title of the achievement, localised to the Steam client's language.
		/// Display this; never key off it.
		/// </summary>
		/// <value>
		/// The localised display name, or an empty string if the API Name is unknown or Steam has not
		/// loaded the achievement schema yet. Because the value changes with the player's language,
		/// two players can see different strings for the same <see cref="Identifier"/>.
		/// </value>
		public string Name => SteamUserStats.Internal.GetAchievementDisplayAttribute( Value, "name" );

		/// <summary>
		/// The player-facing description — the "how you earn this" line under the title, localised to
		/// the Steam client's language.
		/// </summary>
		/// <value>
		/// The localised description, or an empty string if the API Name is unknown or the schema has
		/// not loaded. Note that for an achievement marked hidden in App Admin, Steam may withhold
		/// this until it is unlocked; this binding does not expose the "hidden" flag Valve also
		/// publishes, so you cannot tell a withheld description from a missing one.
		/// </value>
		public string Description => SteamUserStats.Internal.GetAchievementDisplayAttribute( Value, "desc" );


		/// <summary>
		/// When the local player unlocked this achievement, for "earned on 3 March 2021" captions and
		/// for sorting an achievements list by recency.
		/// </summary>
		/// <value>
		/// The unlock time in <b>UTC</b>, or <see langword="null"/> if the achievement is still
		/// locked. <see langword="null"/> is also what you get for an unknown API Name and during the
		/// startup window before <see cref="SteamUserStats.StatsReceived"/> is
		/// <see langword="true"/> — all three are indistinguishable.
		/// </value>
		/// <remarks>
		/// <b>Sentinel worth guarding against:</b> Valve only began recording unlock times in
		/// December 2009, and reports a timestamp of zero for anything earned before that. This
		/// binding converts that zero literally, so such achievements come back as
		/// <c>1970-01-01T00:00:00Z</c> rather than <see langword="null"/>. Long-lived games do hit
		/// this. Compare against the Unix epoch before formatting a date.
		/// </remarks>
		public DateTime? UnlockTime
		{
			get
			{
				var state = false;
				uint time = 0;

				if ( !SteamUserStats.Internal.GetAchievementAndUnlockTime( Value, ref state, ref time ) || !state )
					return null;

				return Epoch.ToDateTime( time );
			}
		}

		/// <summary>
		/// The achievement's icon as raw RGBA pixels, if Steam already has it in memory. The
		/// non-blocking form: use it when you can tolerate drawing a placeholder this frame and
		/// checking again later.
		/// </summary>
		/// <returns>
		/// The icon, or <see langword="null"/>. <b><see langword="null"/> usually does not mean "no
		/// icon"</b> — Valve is explicit that a missing handle normally means the bits are still
		/// being fetched, and icons are essentially always absent on the first request for each
		/// achievement. It also covers an unknown API Name and an achievement with genuinely no icon
		/// configured; the three are indistinguishable here.
		/// </returns>
		/// <remarks>
		/// Steam serves the locked or unlocked variant of the art according to the achievement's
		/// current <see cref="State"/>, so the icon can change under you when the player unlocks it.
		/// Use <see cref="GetIconAsync(int)"/> if you would rather wait than poll.
		/// </remarks>
		public Image? GetIcon()
		{
			return SteamUtils.GetImage( SteamUserStats.Internal.GetAchievementIcon( Value ) );
		}


		/// <summary>
		/// The achievement's icon, waiting for Steam to stream it in if it is not already resident.
		/// This is the form to use when populating an achievements screen — icons are loaded
		/// asynchronously and are empty on first request, so the synchronous
		/// <see cref="GetIcon"/> would hand you <see langword="null"/> for every row.
		/// </summary>
		/// <param name="timeout">
		/// How long to wait, in milliseconds, before giving up and returning
		/// <see langword="null"/>. Defaults to <c>5000</c>. The wait is polled in 10 ms steps, so
		/// values below that round up in practice. There is no cancellation token — the timeout is
		/// the only way out.
		/// </param>
		/// <returns>
		/// The icon as raw RGBA pixels, or <see langword="null"/> if the wait timed out or the
		/// achievement has no icon configured. Those two cases are not distinguishable, so do not
		/// treat <see langword="null"/> as proof that no art exists — retry later before caching a
		/// negative result.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Returns immediately without awaiting if Steam already has the image. Otherwise it
		/// subscribes to Steam's icon-fetched callback and yields until it fires for <i>this</i>
		/// achievement, which means it <b>only ever completes while callbacks are being pumped</b>:
		/// with <c>asyncCallbacks: false</c> and no <c>SteamClient.RunCallbacks()</c> loop, this
		/// always burns the full timeout and returns <see langword="null"/>.
		/// </para>
		/// <para>
		/// Steam returns the locked or unlocked variant of the art depending on the achievement's
		/// <see cref="State"/> at the time of the request, so re-fetch after an unlock if you show
		/// different art for the two.
		/// </para>
		/// </remarks>
		public async Task<Image?> GetIconAsync( int timeout = 5000 )
		{
			var i = SteamUserStats.Internal.GetAchievementIcon( Value );
			if ( i != 0 ) return SteamUtils.GetImage( i );

			var ident = Identifier;
			bool gotCallback = false;

			void f( string x, int icon )
			{
				if ( x != ident ) return;
				i = icon;
				gotCallback = true;
			}

			try
			{
				SteamUserStats.OnAchievementIconFetched += f;

				int waited = 0;
				while ( !gotCallback )
				{
					await Task.Delay( 10 );
					waited += 10;

					// Time out after x milliseconds
					if ( waited > timeout )
						return null;
				}

				if ( i == 0 ) return null;
				return SteamUtils.GetImage( i );
			}
			finally
			{
				SteamUserStats.OnAchievementIconFetched -= f;
			}
		}

		/// <summary>
		/// What fraction of all players worldwide have unlocked this achievement — the rarity figure
		/// Steam shows as a percentage on the community achievements page. Use it to label something
		/// "only 0.4% of players have this".
		/// </summary>
		/// <value>
		/// A fraction between <c>0</c> and <c>1</c> (multiply by 100 for a percentage), or
		/// <c>-1</c> when Steam has no global data. <c>-1</c> is an unambiguous "unavailable"
		/// sentinel and is distinct from a genuine <c>0</c>, so test for it explicitly rather than
		/// formatting the value straight into UI.
		/// </value>
		/// <remarks>
		/// <para>
		/// This reads a cache of whole-population data, unrelated to the local player's own progress
		/// and unaffected by <see cref="SteamUserStats.StatsReceived"/>.
		/// </para>
		/// <para>
		/// Valve's header documents no priming requirement for this specific call, but does state for
		/// the sibling "most achieved" iterators that the percentages are only present once
		/// <c>RequestGlobalAchievementPercentages</c> has been called and its callback awaited — and
		/// this binding exposes no public way to make that call. Treat <c>-1</c> as an expected
		/// outcome you must handle, not an error, and re-read later rather than caching it.
		/// </para>
		/// </remarks>
		public float GlobalUnlocked
		{
			get
			{
				float pct = 0;

				if ( !SteamUserStats.Internal.GetAchievementAchievedPercent( Value, ref pct ) )
					return -1.0f;

				return pct / 100.0f;
			}
		}

		/// <summary>
		/// Unlocks this achievement for the local player and, by default, immediately pushes it to
		/// Valve — which is what makes the Steam unlock toast appear on screen.
		/// </summary>
		/// <param name="apply">
		/// <see langword="true"/> (the default) to store straight away, so the notification shows and
		/// the unlock is durable. Pass <see langword="false"/> when unlocking several achievements at
		/// once, then call <see cref="SteamUserStats.StoreStats"/> yourself — storing is rate limited
		/// and one flush for the batch is correct. Note that with <see langword="false"/> nothing is
		/// persisted and no toast appears until you do.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the unlock was accepted into Steam's local cache. This reflects
		/// only the unlock, not the store — a <see langword="true"/> here with
		/// <paramref name="apply"/> set can still be followed by a failed upload, reported on
		/// <see cref="SteamUserStats.OnUserStatsStored"/>. <see langword="false"/> means the unlock
		/// was dropped, most often because the player's stats have not arrived yet
		/// (<see cref="SteamUserStats.StatsReceived"/>) or because the API Name is unknown or
		/// unpublished; the causes are not distinguishable.
		/// </returns>
		/// <remarks>
		/// Safe to call on an already-unlocked achievement — Steam ignores it and no second toast is
		/// shown. Unlocking is not the same as reporting progress: see
		/// <see cref="SteamUserStats.IndicateAchievementProgress"/>, which only shows a
		/// "17 of 50" notification and never unlocks anything.
		/// </remarks>
		public bool Trigger( bool apply = true )
		{
			var r = SteamUserStats.Internal.SetAchievement( Value );

			if ( apply && r )
			{
				SteamUserStats.Internal.StoreStats();
			}

			return r;
		}

		/// <summary>
		/// Relocks this achievement for the local player. A development and QA tool — there is no
		/// legitimate reason to take an achievement away from a real player, and doing so is visible
		/// on their profile.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if the reset was applied to Steam's local cache;
		/// <see langword="false"/> if it was dropped, typically because stats have not arrived
		/// (<see cref="SteamUserStats.StatsReceived"/>) or the API Name is unknown.
		/// </returns>
		/// <remarks>
		/// <b>Unlike <see cref="Trigger"/>, this does not store for you.</b> The achievement reads as
		/// locked immediately but stays unlocked on Valve's servers until you call
		/// <see cref="SteamUserStats.StoreStats"/> — and if you never do, the next
		/// <c>UserStatsReceived</c> will bring the unlocked state straight back. To wipe everything
		/// at once use <see cref="SteamUserStats.ResetAll"/> instead.
		/// </remarks>
		public bool Clear()
		{
			return SteamUserStats.Internal.ClearAchievement( Value );
		}
	}
}
