using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Provides Steam Networking utilities.
	/// </summary>
	public class SteamNetworkingUtils : SteamSharedClass<SteamNetworkingUtils>
	{
		internal static ISteamNetworkingUtils Internal => Interface as ISteamNetworkingUtils;

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamNetworkingUtils( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			InstallCallbacks( server );

			return true;
		}

		static void InstallCallbacks( bool server )
		{
			Dispatch.Install<SteamRelayNetworkStatus_t>( x =>
			{
				Status = x.Avail;
			}, server );
		}

		/// <summary>
		/// A function to receive debug network information on. This will do nothing
		/// unless you set <see cref="DebugLevel"/> to something other than <see cref="NetDebugOutput.None"/>.
		/// 
		/// You should set this to an appropriate level instead of setting it to the highest
		/// and then filtering it by hand because a lot of energy is used by creating the strings
		/// and your frame rate will tank and you won't know why.
		/// </summary>

		public static event Action<NetDebugOutput, string> OnDebugOutput;

		/// <summary>
		/// The latest available status gathered from the SteamRelayNetworkStatus callback
		/// </summary>
		public static SteamNetworkingAvailability Status { get; private set; }

		/// <summary>
		/// If you know that you are going to be using the relay network (for example,
		/// because you anticipate making P2P connections), call this to initialize the
		/// relay network.  If you do not call this, the initialization will
		/// be delayed until the first time you use a feature that requires access
		/// to the relay network, which will delay that first access.
		/// <para>
		/// You can also call this to force a retry if the previous attempt has failed.
		/// Performing any action that requires access to the relay network will also
		/// trigger a retry, and so calling this function is never strictly necessary,
		/// but it can be useful to call it a program launch time, if access to the
		/// relay network is anticipated.
		/// </para>
		/// <para>
		/// Use GetRelayNetworkStatus or listen for SteamRelayNetworkStatus_t
		/// callbacks to know when initialization has completed.
		/// Typically initialization completes in a few seconds.
		/// </para>
		/// <para>
		/// Note: dedicated servers hosted in known data centers do *not* need
		/// to call this, since they do not make routing decisions.  However, if
		/// the dedicated server will be using P2P functionality, it will act as
		/// a "client" and this should be called.
		/// </para>
		/// </summary>
		public static void InitRelayNetworkAccess()
		{
			Internal.InitRelayNetworkAccess();
		}

		/// <summary>
		/// Return location info for the current host.
		///
		/// It takes a few seconds to initialize access to the relay network.  If
		/// you call this very soon after startup the data may not be available yet.
		///
		/// This always return the most up-to-date information we have available
		/// right now, even if we are in the middle of re-calculating ping times.
		/// </summary>
		public static NetPingLocation? LocalPingLocation
		{
			get
			{
				NetPingLocation location = default;
				var age = Internal.GetLocalPingLocation( ref location );
				if ( age < 0 )
					return null;

				return location;
			}
		}

		/// <summary>
		/// Same as PingLocation.EstimatePingTo, but assumes that one location is the local host.
		/// This is a bit faster, especially if you need to calculate a bunch of
		/// these in a loop to find the fastest one.
		/// </summary>
		public static int EstimatePingTo( NetPingLocation target )
		{
			return Internal.EstimatePingTimeFromLocalHost( ref target );
		}

		/// <summary>
		/// If you need ping information straight away, wait on this. It will return
		/// immediately if you already have up to date ping data.
		/// </summary>
		public static async Task WaitForPingDataAsync( float maxAgeInSeconds = 60 * 5 )
		{
			if ( Internal.CheckPingDataUpToDate( maxAgeInSeconds ) )
				return;

			SteamRelayNetworkStatus_t status = default;

			while ( Internal.GetRelayNetworkStatus( ref status ) != SteamNetworkingAvailability.Current )
			{
				await Task.Delay( 10 );
			}
		}

		#region Ping Locations

		/// <summary>
		/// The largest a ping location string can ever be, in bytes, including the terminating
		/// NUL. Mirrors <c>k_cchMaxSteamNetworkingPingLocationString</c>.
		/// </summary>
		/// <remarks>
		/// Valve describes this as "an extremely conservative worst case value which leaves room
		/// for future syntax enhancements" — real strings are far shorter, typically a few dozen
		/// bytes. Size a <i>buffer</i> with this constant, but do not size a <i>database column</i>
		/// or a network packet with it; store the actual string length instead.
		/// </remarks>
		public const int MaxPingLocationStringLength = 1024;

		/// <summary>
		/// Returned by the ping estimation functions when the estimate failed because of a
		/// networking problem, such as being unable to ping the relays.
		/// Mirrors <c>k_nSteamNetworkingPing_Failed</c>.
		/// </summary>
		public const int PingFailed = -1;

		/// <summary>
		/// Returned by the ping estimation functions when the answer is simply not known yet —
		/// most often because relay ping measurement has not finished. Wait on
		/// <see cref="WaitForPingDataAsync(float)"/> and try again.
		/// Mirrors <c>k_nSteamNetworkingPing_Unknown</c>.
		/// </summary>
		public const int PingUnknown = -2;

		/// <summary>
		/// Estimate the round-trip latency, in milliseconds, between two arbitrary locations —
		/// <b>neither of which needs to be this machine</b>. This is the core primitive for
		/// latency-aware matchmaking: it answers "if I put these two players together, how bad
		/// will it be?" without sending a single packet.
		/// </summary>
		/// <param name="location1">One endpoint's location.</param>
		/// <param name="location2">The other endpoint's location.</param>
		/// <returns>
		/// Estimated round-trip time in milliseconds, or a negative value —
		/// <see cref="PingFailed"/> or <see cref="PingUnknown"/> — if no estimate is available.
		/// <b>Always check for negatives</b>; treating <c>-2</c> as a two-millisecond ping is the
		/// classic bug here, and it will silently make unmeasured players look like the best
		/// possible match.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Wraps <c>EstimatePingTimeBetweenTwoLocations</c>.
		/// </para>
		/// <para>
		/// The estimate is conservative and assumes the connection is routed through the relay
		/// network. If the two peers end up with a direct route (via NAT traversal) the real ping
		/// may be better — or, since plain IP routing is often suboptimal, slightly worse. Treat it
		/// as a reasonable upper bound, not a measurement.
		/// </para>
		/// <para>
		/// This is a pure local computation against Steam's cached latency map: it returns
		/// immediately and sends nothing. It is cheap enough to call for every pair in a candidate
		/// list. Because <see cref="NetPingLocation"/> is 512 bytes, this overload takes both
		/// arguments by <see langword="ref"/> to avoid copying 1 KB per call in a matchmaking loop;
		/// neither argument is modified.
		/// </para>
		/// <para>
		/// If one of the two locations is this machine, prefer
		/// <see cref="EstimatePingTo(ref NetPingLocation)"/> — it is faster and slightly more
		/// accurate, because Steam knows more about its own routing than a location string can
		/// carry.
		/// </para>
		/// <para>
		/// Doing this on a backend rather than in the game client is a different problem: Valve
		/// ships a separate "ticketgen"/game-coordinator library for that. This binding only wraps
		/// the in-process API.
		/// </para>
		/// </remarks>
		/// <example>
		/// <code>
		/// // Server-browser style: pick the lobby with the best estimated ping for a joiner.
		/// var joiner = SteamNetworkingUtils.LocalPingLocation.Value;
		///
		/// var bestPing = int.MaxValue;
		/// Lobby? best = null;
		///
		/// foreach ( var candidate in lobbies )
		/// {
		///     // The host published its location string in lobby data.
		///     if ( !SteamNetworkingUtils.TryParsePingLocation( candidate.GetData( "loc" ), out var host ) )
		///         continue;
		///
		///     var ping = SteamNetworkingUtils.EstimatePingBetween( ref joiner, ref host );
		///     if ( ping &lt; 0 || ping &gt;= bestPing ) continue;
		///
		///     bestPing = ping;
		///     best = candidate;
		/// }
		/// </code>
		/// </example>
		public static int EstimatePingBetween( ref NetPingLocation location1, ref NetPingLocation location2 )
		{
			return Internal.EstimatePingTimeBetweenTwoLocations( ref location1, ref location2 );
		}

		/// <summary>
		/// Convenience by-value form of
		/// <see cref="EstimatePingBetween(ref NetPingLocation, ref NetPingLocation)"/>.
		/// </summary>
		/// <remarks>
		/// Each argument is a 512-byte copy. That is irrelevant for a one-off call and measurable
		/// in a loop over hundreds of candidates — use the <see langword="ref"/> overload there.
		/// </remarks>
		public static int EstimatePingBetween( NetPingLocation location1, NetPingLocation location2 )
		{
			return Internal.EstimatePingTimeBetweenTwoLocations( ref location1, ref location2 );
		}

		/// <summary>
		/// Same as <see cref="EstimatePingBetween(ref NetPingLocation, ref NetPingLocation)"/>, but
		/// assumes one location is the local host — which is both faster and slightly more accurate.
		/// This <see langword="ref"/> overload avoids copying the 512-byte location, which matters
		/// when you are scanning a list to find the closest server.
		/// </summary>
		/// <returns>
		/// Estimated round-trip time in milliseconds, or a negative value
		/// (<see cref="PingFailed"/>, <see cref="PingUnknown"/>).
		/// </returns>
		/// <remarks>
		/// Wraps <c>EstimatePingTimeFromLocalHost</c>. The argument is not modified.
		/// </remarks>
		public static int EstimatePingTo( ref NetPingLocation target )
		{
			return Internal.EstimatePingTimeFromLocalHost( ref target );
		}

		/// <summary>
		/// Serialises a ping location into the compact text form that is safe to send over your own
		/// network, store in lobby metadata, or write to a database.
		/// </summary>
		/// <returns>The location string. Never <see langword="null"/>.</returns>
		/// <remarks>
		/// <para>
		/// Wraps <c>ConvertPingLocationToString</c>.
		/// </para>
		/// <para>
		/// <b>This is the whole point of ping locations.</b> A <see cref="NetPingLocation"/> is a
		/// 512-byte opaque blob that Valve explicitly says must never be serialised, sent over the
		/// wire, or persisted — it is only meaningful inside the process that produced it. The
		/// string form is the supported way to move one between machines: player A converts their
		/// location to a string, sends it to your matchmaker, and player B (or your server) parses
		/// it back with <see cref="TryParsePingLocation(string, out NetPingLocation)"/> and feeds it
		/// to <see cref="EstimatePingBetween(ref NetPingLocation, ref NetPingLocation)"/>.
		/// </para>
		/// <para>
		/// <b>Do not parse the string yourself.</b> Valve states the format is subject to change.
		/// Treat it as an opaque token.
		/// </para>
		/// <para>
		/// <b>Lifetime.</b> The string describes network topology as Steam currently understands
		/// it. Valve does not document an expiry, but the underlying data is refreshed periodically
		/// and a player's real location can change (they move, their ISP re-routes). Treat a stored
		/// string as a cache with a lifetime of hours, not months; refresh it when a session starts
		/// rather than persisting it in a player profile forever.
		/// <i>This paragraph is inferred from how the data is maintained, not stated by Valve.</i>
		/// </para>
		/// <para>
		/// Never longer than <see cref="MaxPingLocationStringLength"/> bytes including the
		/// terminator. This overload allocates exactly one string and uses a stack buffer for the
		/// native call. It takes the location by <see langword="ref"/> to avoid a 512-byte copy; the
		/// argument is not modified.
		/// </para>
		/// </remarks>
		/// <example>
		/// <code>
		/// // Host side: publish where we are, so joiners can estimate ping without connecting.
		/// await SteamNetworkingUtils.WaitForPingDataAsync();
		///
		/// if ( SteamNetworkingUtils.LocalPingLocation is NetPingLocation here )
		/// {
		///     lobby.SetData( "loc", SteamNetworkingUtils.ConvertPingLocationToString( ref here ) );
		/// }
		/// </code>
		/// </example>
		public static unsafe string ConvertPingLocationToString( ref NetPingLocation location )
		{
			byte* buffer = stackalloc byte[MaxPingLocationStringLength];
			buffer[0] = 0;

			Internal.ConvertPingLocationToString( ref location, (IntPtr)buffer, MaxPingLocationStringLength );

			var length = StringLength( buffer, MaxPingLocationStringLength );
			if ( length == 0 )
				return string.Empty;

			return Utility.Utf8NoBom.GetString( buffer, length );
		}

		/// <summary>
		/// Serialises a ping location as NUL-terminated UTF-8 straight into a caller-owned array,
		/// with no string allocation at all. Use this when you are writing the location into a
		/// packet or a pooled buffer.
		/// </summary>
		/// <param name="location">The location to serialise. Not modified.</param>
		/// <param name="utf8Destination">
		/// Destination buffer. Must have at least <see cref="MaxPingLocationStringLength"/> bytes
		/// available after <paramref name="offset"/> — that is Valve's stated requirement, not ours.
		/// </param>
		/// <param name="offset">Index in <paramref name="utf8Destination"/> to start writing at.</param>
		/// <returns>
		/// The number of bytes written, <b>excluding</b> the terminating NUL. The NUL is written
		/// too, so the buffer is directly usable as a C string.
		/// </returns>
		/// <exception cref="ArgumentNullException"><paramref name="utf8Destination"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> is negative.</exception>
		/// <exception cref="ArgumentException">
		/// There is less than <see cref="MaxPingLocationStringLength"/> bytes of room after
		/// <paramref name="offset"/>.
		/// </exception>
		/// <remarks>
		/// The full-size requirement is enforced rather than trusted to truncate: Valve documents
		/// the minimum buffer size but says nothing about what happens if you pass less, so a
		/// smaller buffer is rejected here instead of risking a silently truncated — and therefore
		/// unparseable — location.
		/// </remarks>
		public static unsafe int ConvertPingLocationToString( ref NetPingLocation location, byte[] utf8Destination, int offset = 0 )
		{
			if ( utf8Destination == null )
				throw new ArgumentNullException( nameof( utf8Destination ) );

			if ( offset < 0 )
				throw new ArgumentOutOfRangeException( nameof( offset ) );

			if ( utf8Destination.Length - offset < MaxPingLocationStringLength )
				throw new ArgumentException( $"Need room for {MaxPingLocationStringLength} bytes after the offset.", nameof( utf8Destination ) );

			fixed ( byte* ptr = utf8Destination )
			{
				// Report the real remaining capacity, matching the Span overload. Passing a
				// fixed MaxPingLocationStringLength here instead would be safe but would mean
				// the two overloads tell native different capacities for equivalent input,
				// which is the kind of inconsistency someone later "fixes" in the wrong
				// direction.
				return ConvertPingLocationToString( ref location, ptr + offset, utf8Destination.Length - offset );
			}
		}

#if NETSTANDARD2_1_OR_GREATER || NET
		/// <summary>
		/// Span form of
		/// <see cref="ConvertPingLocationToString(ref NetPingLocation, byte[], int)"/> — pairs with
		/// <c>stackalloc</c> or a slice of a pooled array. Available on netstandard2.1+/.NET builds.
		/// </summary>
		/// <exception cref="ArgumentException">
		/// <paramref name="utf8Destination"/> is shorter than
		/// <see cref="MaxPingLocationStringLength"/>.
		/// </exception>
		public static unsafe int ConvertPingLocationToString( ref NetPingLocation location, Span<byte> utf8Destination )
		{
			if ( utf8Destination.Length < MaxPingLocationStringLength )
				throw new ArgumentException( $"Destination must be at least {MaxPingLocationStringLength} bytes.", nameof( utf8Destination ) );

			fixed ( byte* ptr = utf8Destination )
			{
				return ConvertPingLocationToString( ref location, ptr, utf8Destination.Length );
			}
		}
#endif

		private static unsafe int ConvertPingLocationToString( ref NetPingLocation location, byte* buffer, int bufferSize )
		{
			// Terminate both ends before the call. Valve's contract says the function always
			// NUL-terminates within the capacity we give it, but if that were ever violated the
			// subsequent length scan would read uninitialised stack memory straight into the
			// returned string. Two byte writes remove the dependency on native behaving.
			buffer[0] = 0;
			buffer[bufferSize - 1] = 0;
			Internal.ConvertPingLocationToString( ref location, (IntPtr)buffer, bufferSize );
			return StringLength( buffer, bufferSize );
		}

		/// <summary>
		/// Parses a ping location string produced by
		/// <see cref="ConvertPingLocationToString(ref NetPingLocation)"/> — typically one that
		/// arrived from another player or from your own backend — back into a usable
		/// <see cref="NetPingLocation"/>.
		/// </summary>
		/// <param name="str">The location string.</param>
		/// <param name="location">Receives the parsed location on success; <c>default</c> on failure.</param>
		/// <returns>
		/// <see langword="false"/> if Steam could not understand the string. This is the expected
		/// outcome for corrupt or hostile input, so always check it — the input came off the
		/// network.
		/// </returns>
		/// <exception cref="ArgumentNullException"><paramref name="str"/> is <see langword="null"/>.</exception>
		/// <remarks>
		/// <para>
		/// Wraps <c>ParsePingLocationString</c>. Equivalent to
		/// <see cref="NetPingLocation.TryParseFromString(string)"/>, exposed here so the whole
		/// round-trip reads from one class.
		/// </para>
		/// <para>
		/// A string longer than <see cref="MaxPingLocationStringLength"/> cannot have come from
		/// Steam and is rejected without entering native code.
		/// </para>
		/// </remarks>
		public static bool TryParsePingLocation( string str, out NetPingLocation location )
		{
			if ( str == null )
				throw new ArgumentNullException( nameof( str ) );

			location = default;

			// Anything this long did not come out of ConvertPingLocationToString. Reject it here
			// rather than handing an oversized buffer to the parser.
			//
			// Measured in UTF-8 BYTES, not string.Length. MaxPingLocationStringLength mirrors
			// k_cchMaxSteamNetworkingPingLocationString, which is a byte budget, whereas
			// string.Length counts UTF-16 code units. A string of astral characters is 2 units
			// but 4 bytes each, so a length check would pass strings that are comfortably over
			// the byte budget - and this method exists specifically to validate strings that
			// arrived from another player, i.e. exactly the input you cannot trust.
			if ( Utility.Utf8NoBom.GetByteCount( str ) >= MaxPingLocationStringLength )
				return false;

			return Internal.ParsePingLocationString( str, ref location );
		}

#if NETSTANDARD2_1_OR_GREATER || NET
		/// <summary>
		/// Parses a ping location straight out of a UTF-8 buffer — a slice of a received packet,
		/// say — without allocating a <see cref="string"/> first. Available on
		/// netstandard2.1+/.NET builds.
		/// </summary>
		/// <param name="utf8String">
		/// The location string as UTF-8. It does <b>not</b> need to be NUL-terminated; a
		/// terminated copy is made on the stack.
		/// </param>
		/// <param name="location">Receives the parsed location on success.</param>
		/// <returns><see langword="false"/> if Steam could not understand the string.</returns>
		public static unsafe bool TryParsePingLocation( ReadOnlySpan<byte> utf8String, out NetPingLocation location )
		{
			location = default;

			// Leave room for the NUL we add. Anything at or beyond the limit did not come from
			// ConvertPingLocationToString.
			if ( utf8String.Length == 0 || utf8String.Length >= MaxPingLocationStringLength )
				return false;

			byte* buffer = stackalloc byte[MaxPingLocationStringLength];

			fixed ( byte* src = utf8String )
			{
				Buffer.MemoryCopy( src, buffer, MaxPingLocationStringLength, utf8String.Length );
			}

			buffer[utf8String.Length] = 0;

			return Internal.ParsePingLocationString( (IntPtr)buffer, ref location );
		}
#endif

		/// <summary>
		/// Length of a NUL-terminated buffer, capped so a missing terminator cannot walk off the end.
		/// </summary>
		private static unsafe int StringLength( byte* buffer, int capacity )
		{
			var length = 0;
			while ( length < capacity && buffer[length] != 0 )
				length++;

			return length;
		}

		#endregion

		#region Points of Presence

		/// <summary>
		/// How many Valve relay clusters (points of presence) Steam currently knows about.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Wraps <c>GetPOPCount</c>. Use this to size a buffer for
		/// <see cref="GetPOPList(NetPOPID[], int)"/>.
		/// </para>
		/// <para>
		/// Returns <c>0</c> until the relay network config has been fetched — call
		/// <see cref="InitRelayNetworkAccess"/> at startup, or await
		/// <see cref="WaitForPingDataAsync(float)"/>, before relying on this. The count can change
		/// at runtime when Valve updates the config, so re-read it rather than caching it forever.
		/// </para>
		/// </remarks>
		public static int POPCount => Internal.GetPOPCount();

		/// <summary>
		/// Every relay cluster Steam knows about, as a freshly allocated array.
		/// </summary>
		/// <returns>
		/// The POP list, or an empty array if the relay config has not been fetched yet. Never
		/// <see langword="null"/>.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Wraps <c>GetPOPCount</c> + <c>GetPOPList</c>. A "POP" is one of Valve's relay clusters —
		/// a data center your traffic can enter or leave the SDR backbone through. Pair this with
		/// <see cref="GetDirectPingToPOP(NetPOPID)"/> to answer "which Valve data center is this
		/// player closest to?", which is what you want when choosing a region or deciding where to
		/// spin up a dedicated server.
		/// </para>
		/// <para>
		/// The values are ordinary <see cref="NetPOPID"/>s, so they compare directly against
		/// <c>SteamNetworkingSockets.HostedDedicatedServerPopId</c> — that is how a hosted server
		/// works out which entry in this list is itself.
		/// </para>
		/// <para>
		/// This allocates. It is intended for startup or for a settings screen — somewhere you
		/// enumerate once. If you re-scan every frame (a live latency HUD, say), keep one array
		/// around and call <see cref="GetPOPList(NetPOPID[], int)"/> instead, which allocates
		/// nothing.
		/// </para>
		/// </remarks>
		/// <example>
		/// <code>
		/// SteamNetworkingUtils.InitRelayNetworkAccess();
		/// await SteamNetworkingUtils.WaitForPingDataAsync();
		///
		/// foreach ( var pop in SteamNetworkingUtils.GetPOPList() )
		/// {
		///     var direct = SteamNetworkingUtils.GetDirectPingToPOP( pop );
		///     var routed = SteamNetworkingUtils.GetPingToDataCenter( pop, out var via );
		///
		///     Console.WriteLine( via.IsValid
		///         ? $"{pop}: {direct}ms direct, {routed}ms via {via}"
		///         : $"{pop}: {direct}ms direct, {routed}ms" );
		/// }
		/// </code>
		/// </example>
		public static NetPOPID[] GetPOPList()
		{
			var count = Internal.GetPOPCount();
			if ( count <= 0 )
				return Array.Empty<NetPOPID>();

			var list = new NetPOPID[count];
			var written = GetPOPList( list, 0 );

			// Steam is allowed to report fewer entries than GetPOPCount promised (the config can
			// change between the two calls). Shrink rather than hand back trailing zero IDs, which
			// would look like real-but-invalid POPs to the caller.
			if ( written == list.Length )
				return list;

			var trimmed = new NetPOPID[written];
			System.Array.Copy( list, trimmed, written );
			return trimmed;
		}

		/// <summary>
		/// Fills a caller-owned array with the relay clusters Steam knows about. Allocates nothing.
		/// </summary>
		/// <param name="destination">Buffer to fill. Size it with <see cref="POPCount"/>.</param>
		/// <param name="offset">Index in <paramref name="destination"/> to start writing at.</param>
		/// <returns>
		/// The number of entries actually written, which may be fewer than the space available.
		/// If it equals the space available, there may be more POPs that did not fit.
		/// </returns>
		/// <exception cref="ArgumentNullException"><paramref name="destination"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="offset"/> is negative or past the end of <paramref name="destination"/>.
		/// </exception>
		/// <remarks>
		/// Wraps <c>GetPOPList</c>.
		/// </remarks>
		public static unsafe int GetPOPList( NetPOPID[] destination, int offset = 0 )
		{
			if ( destination == null )
				throw new ArgumentNullException( nameof( destination ) );

			if ( offset < 0 || offset > destination.Length )
				throw new ArgumentOutOfRangeException( nameof( offset ) );

			var capacity = destination.Length - offset;
			if ( capacity == 0 )
				return 0;

			fixed ( NetPOPID* ptr = destination )
			{
				return GetPOPList( ptr + offset, capacity );
			}
		}

#if NETSTANDARD2_1_OR_GREATER || NET
		/// <summary>
		/// Span form of <see cref="GetPOPList(NetPOPID[], int)"/>, so you can fill a
		/// <c>stackalloc</c> buffer or a slice of a pooled array. Available on
		/// netstandard2.1+/.NET builds.
		/// </summary>
		/// <returns>The number of entries written.</returns>
		public static unsafe int GetPOPList( Span<NetPOPID> destination )
		{
			if ( destination.Length == 0 )
				return 0;

			fixed ( NetPOPID* ptr = destination )
			{
				return GetPOPList( ptr, destination.Length );
			}
		}
#endif

		/// <summary>
		/// <see cref="NetPOPID"/> and the native <c>SteamNetworkingPOPID</c> are both a single
		/// <see cref="uint"/>, so the native call can fill the caller's buffer directly with no
		/// intermediate array.
		/// </summary>
		private static unsafe int GetPOPList( NetPOPID* destination, int capacity )
		{
			return Internal.GetPOPList( ref *(SteamNetworkingPOPID*)destination, capacity );
		}

		/// <summary>
		/// Round-trip latency, in milliseconds, straight from this machine to the relays at a
		/// given data center — no intermediate hop through Valve's backbone.
		/// </summary>
		/// <param name="pop">The relay cluster to measure against.</param>
		/// <returns>
		/// Round-trip time in milliseconds, or a negative value (<see cref="PingFailed"/>,
		/// <see cref="PingUnknown"/>) when it has not been measured.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Wraps <c>GetDirectPingToPOP</c>. This reads an already-measured value; it does not send
		/// packets and returns immediately. Values only become available once relay ping
		/// measurement has run — see <see cref="WaitForPingDataAsync(float)"/>.
		/// </para>
		/// <para>
		/// Use this when you care about the first hop: "which Valve data center should I host in
		/// for this player?". Use
		/// <see cref="GetPingToDataCenter(NetPOPID, out NetPOPID)"/> when you care about the ping
		/// an actual SDR connection would experience, which may take a better route through the
		/// backbone than the public internet offers.
		/// </para>
		/// </remarks>
		public static int GetDirectPingToPOP( NetPOPID pop )
		{
			return Internal.GetDirectPingToPOP( pop.Value );
		}

		/// <summary>
		/// Round-trip latency, in milliseconds, of the best relayed route from this machine to a
		/// data center — which is what an SDR connection would actually get.
		/// </summary>
		/// <param name="pop">The relay cluster to measure against.</param>
		/// <param name="viaRelay">
		/// Receives the intermediate relay cluster the best route passes through, if there is one.
		/// Check <see cref="NetPOPID.IsValid"/>: a zero ID means the route is direct. This is
		/// diagnostic information — useful in a network debug overlay, not needed for matchmaking.
		/// </param>
		/// <returns>
		/// Round-trip time in milliseconds, or a negative value (<see cref="PingFailed"/>,
		/// <see cref="PingUnknown"/>).
		/// </returns>
		/// <remarks>
		/// <para>
		/// Wraps <c>GetPingToDataCenter</c>. Like <see cref="GetDirectPingToPOP(NetPOPID)"/> this
		/// reads cached measurements and returns immediately.
		/// </para>
		/// <para>
		/// This can be <i>lower</i> than the direct ping to the same POP. That is not a bug: Valve's
		/// backbone frequently beats public internet routing, so entering the network at a nearby
		/// relay and travelling privately can be faster than going straight there.
		/// </para>
		/// <para>
		/// The native function accepts a null out-parameter to skip the relay information. The
		/// generated binding requires a real destination, so the parameterless overload simply
		/// discards it — the cost is one stack slot.
		/// </para>
		/// </remarks>
		public static int GetPingToDataCenter( NetPOPID pop, out NetPOPID viaRelay )
		{
			SteamNetworkingPOPID relay = default;
			var ping = Internal.GetPingToDataCenter( pop.Value, ref relay );

			viaRelay = new NetPOPID( relay );
			return ping;
		}

		/// <summary>
		/// <see cref="GetPingToDataCenter(NetPOPID, out NetPOPID)"/> without the intermediate-relay
		/// information.
		/// </summary>
		public static int GetPingToDataCenter( NetPOPID pop )
		{
			SteamNetworkingPOPID relay = default;
			return Internal.GetPingToDataCenter( pop.Value, ref relay );
		}

		#endregion

		public static long LocalTimestamp => Internal.GetLocalTimestamp();


		/// <summary>
		/// [0 - 100] - Randomly discard N pct of packets.
		/// </summary>
		public static float FakeSendPacketLoss
		{
			get => GetConfigFloat( NetConfig.FakePacketLoss_Send );
			set => SetConfigFloat( NetConfig.FakePacketLoss_Send, value );
		}

		/// <summary>
		/// [0 - 100] - Randomly discard N pct of packets.
		/// </summary>
		public static float FakeRecvPacketLoss
		{
			get => GetConfigFloat( NetConfig.FakePacketLoss_Recv );
			set => SetConfigFloat( NetConfig.FakePacketLoss_Recv, value );
		}

		/// <summary>
		/// Delay all packets by N ms.
		/// </summary>
		public static float FakeSendPacketLag
		{
			get => GetConfigFloat( NetConfig.FakePacketLag_Send );
			set => SetConfigFloat( NetConfig.FakePacketLag_Send, value );
		}

		/// <summary>
		/// Delay all packets by N ms.
		/// </summary>
		public static float FakeRecvPacketLag
		{
			get => GetConfigFloat( NetConfig.FakePacketLag_Recv );
			set => SetConfigFloat( NetConfig.FakePacketLag_Recv, value );
		}

		/// <summary>
		/// Timeout value (in ms) to use when first connecting.
		/// </summary>
		public static int ConnectionTimeout
		{
			get => GetConfigInt( NetConfig.TimeoutInitial );
			set => SetConfigInt( NetConfig.TimeoutInitial, value );
		}

		/// <summary>
		/// Timeout value (in ms) to use after connection is established.
		/// </summary>
		public static int Timeout
		{
			get => GetConfigInt( NetConfig.TimeoutConnected );
			set => SetConfigInt( NetConfig.TimeoutConnected, value );
		}

		/// <summary>
		/// Upper limit of buffered pending bytes to be sent.
		/// If this is reached SendMessage will return LimitExceeded.
		/// Default is 524288 bytes (512k).
		/// </summary>
		public static int SendBufferSize
		{
			get => GetConfigInt( NetConfig.SendBufferSize );
			set => SetConfigInt( NetConfig.SendBufferSize, value );
		}

		/// <summary>
		/// Minimum send rate clamp, 0 is no limit.
		/// This value will control the min allowed sending rate that 
		/// bandwidth estimation is allowed to reach.  Default is 0 (no-limit)
		/// </summary>
		public static int SendRateMin
		{
			get => GetConfigInt( NetConfig.SendRateMin );
			set => SetConfigInt( NetConfig.SendRateMin, value );
		}

		/// <summary>
		/// Maximum send rate clamp, 0 is no limit.
		/// This value will control the max allowed sending rate that 
		/// bandwidth estimation is allowed to reach.  Default is 0 (no-limit)
		/// </summary>
		public static int SendRateMax
		{
			get => GetConfigInt( NetConfig.SendRateMax );
			set => SetConfigInt( NetConfig.SendRateMax, value );
		}

		/// <summary>
		/// Nagle time, in microseconds.  When SendMessage is called, if
		/// the outgoing message is less than the size of the MTU, it will be
		/// queued for a delay equal to the Nagle timer value.  This is to ensure
		/// that if the application sends several small messages rapidly, they are
		/// coalesced into a single packet.
		/// See historical RFC 896.  Value is in microseconds. 
		/// Default is 5000us (5ms).
		/// </summary>
		public static int NagleTime
		{
			get => GetConfigInt( NetConfig.NagleTime );
			set => SetConfigInt( NetConfig.NagleTime, value );
		}

		/// <summary>
		/// Don't automatically fail IP connections that don't have
		/// strong auth.  On clients, this means we will attempt the connection even if
		/// we don't know our identity or can't get a cert.  On the server, it means that
		/// we won't automatically reject a connection due to a failure to authenticate.
		/// (You can examine the incoming connection and decide whether to accept it.)
		/// <para>
		/// This is a dev configuration value, and you should not let users modify it in
		/// production.
		/// </para>
		/// </summary>
		public static int AllowWithoutAuth
		{
			get => GetConfigInt( NetConfig.IP_AllowWithoutAuth );
			set => SetConfigInt( NetConfig.IP_AllowWithoutAuth, value );
		}

		/// <summary>
		/// Allow unencrypted (and unauthenticated) communication.
		/// 0: Not allowed (the default)
		/// 1: Allowed, but prefer encrypted
		/// 2: Allowed, and preferred
		/// 3: Required.  (Fail the connection if the peer requires encryption.)
		/// <para>
		/// This is a dev configuration value, since its purpose is to disable encryption.
		/// You should not let users modify it in production.  (But note that it requires
		/// the peer to also modify their value in order for encryption to be disabled.)
		/// </para>
		/// </summary>
		public static int Unencrypted
		{
			get => GetConfigInt( NetConfig.Unencrypted );
			set => SetConfigInt( NetConfig.Unencrypted, value );
		}

		/// <summary>
		/// Log RTT calculations for inline pings and replies.
		/// </summary>
		public static int DebugLevelAckRTT
		{
			get => GetConfigInt( NetConfig.LogLevel_AckRTT );
			set => SetConfigInt( NetConfig.LogLevel_AckRTT, value );
		}

		/// <summary>
		/// Log SNP packets send.
		/// </summary>
		public static int DebugLevelPacketDecode
		{
			get => GetConfigInt( NetConfig.LogLevel_PacketDecode );
			set => SetConfigInt( NetConfig.LogLevel_PacketDecode, value );
		}

		/// <summary>
		/// Log each message send/recv.
		/// </summary>
		public static int DebugLevelMessage
		{
			get => GetConfigInt( NetConfig.LogLevel_Message );
			set => SetConfigInt( NetConfig.LogLevel_Message, value );
		}

		/// <summary>
		/// Log dropped packets.
		/// </summary>
		public static int DebugLevelPacketGaps
		{
			get => GetConfigInt( NetConfig.LogLevel_PacketGaps );
			set => SetConfigInt( NetConfig.LogLevel_PacketGaps, value );
		}

		/// <summary>
		/// Log P2P rendezvous messages.
		/// </summary>
		public static int DebugLevelP2PRendezvous
		{
			get => GetConfigInt( NetConfig.LogLevel_P2PRendezvous );
			set => SetConfigInt( NetConfig.LogLevel_P2PRendezvous, value );
		}

		/// <summary>
		/// Log ping relays.
		/// </summary>
		public static int DebugLevelSDRRelayPings
		{
			get => GetConfigInt( NetConfig.LogLevel_SDRRelayPings );
			set => SetConfigInt( NetConfig.LogLevel_SDRRelayPings, value );
		}

		/// <summary>
		/// Get Debug Information via <see cref="OnDebugOutput"/> event.
		/// <para>
		/// Except when debugging, you should only use <see cref="NetDebugOutput.Msg"/>
		/// or <see cref="NetDebugOutput.Warning"/>.  For best performance, do NOT
		/// request a high detail level and then filter out messages in the callback.  
		/// </para>
		/// <para>
		/// This incurs all of the expense of formatting the messages, which are then discarded.  
		/// Setting a high priority value (low numeric value) here allows the library to avoid 
		/// doing this work.
		/// </para>
		/// </summary>
		public static NetDebugOutput DebugLevel
		{
			get => _debugLevel;
			set
			{
				_debugLevel = value;
				_debugFunc = new NetDebugFunc( OnDebugMessage );

				Internal.SetDebugOutputFunction( value, _debugFunc );
			}
		}

		/// <summary>
		/// So we can remember and provide a Get for DebugLevel.
		/// </summary>
		private static NetDebugOutput _debugLevel;

		/// <summary>
		/// We need to keep the delegate around until it's not used anymore.
		/// </summary>
		static NetDebugFunc _debugFunc;

		struct DebugMessage
		{
			public NetDebugOutput Type;
			public string Msg;
		}

		private static System.Collections.Concurrent.ConcurrentQueue<DebugMessage> debugMessages = new System.Collections.Concurrent.ConcurrentQueue<DebugMessage>();

		/// <summary>
		/// This can be called from other threads - so we're going to queue these up and process them in a safe place.
		/// </summary>
		[MonoPInvokeCallback]
		private static void OnDebugMessage( NetDebugOutput nType, IntPtr str )
		{
			debugMessages.Enqueue( new DebugMessage { Type = nType, Msg = Helpers.MemoryToString( str ) } );
		}

		internal static void LogDebugMessage( NetDebugOutput type, string message )
        {
			debugMessages.Enqueue( new DebugMessage { Type = type, Msg = message } );
        }

		/// <summary>
		/// Called regularly from the Dispatch loop so we can provide a timely
		/// stream of messages.
		/// </summary>
		internal static void OutputDebugMessages()
		{
			if ( debugMessages.IsEmpty )
				return;

			while ( debugMessages.TryDequeue( out var result ) )
			{
				OnDebugOutput?.Invoke( result.Type, result.Msg );
			}
		}

        internal static unsafe NetMsg* AllocateMessage()
        {
            return Internal.AllocateMessage(0);
        }

		#region Config Internals

		internal unsafe static bool SetConfigInt( NetConfig type, int value )
		{
			int* ptr = &value;
			return Internal.SetConfigValue( type, NetConfigScope.Global, IntPtr.Zero, NetConfigType.Int32, (IntPtr)ptr );
		}

		internal unsafe static int GetConfigInt( NetConfig type )
		{
			int value = 0;
			NetConfigType dtype = NetConfigType.Int32;
			int* ptr = &value;
			UIntPtr size = new UIntPtr( sizeof( int ) );
			var result = Internal.GetConfigValue( type, NetConfigScope.Global, IntPtr.Zero, ref dtype, (IntPtr) ptr, ref size );
			if ( result != NetConfigResult.OK )
				return 0;

			return value;
		}

		internal unsafe static bool SetConfigFloat( NetConfig type, float value )
		{
			float* ptr = &value;
			return Internal.SetConfigValue( type, NetConfigScope.Global, IntPtr.Zero, NetConfigType.Float, (IntPtr)ptr );
		}

		internal unsafe static float GetConfigFloat( NetConfig type )
		{
			float value = 0;
			NetConfigType dtype = NetConfigType.Float;
			float* ptr = &value;
			UIntPtr size = new UIntPtr( sizeof( float ) );
			var result = Internal.GetConfigValue( type, NetConfigScope.Global, IntPtr.Zero, ref dtype, (IntPtr)ptr, ref size );
			if ( result != NetConfigResult.OK )
				return 0;

			return value;
		}

		internal unsafe static bool SetConfigString( NetConfig type, string value )
		{
			var bytes = Utility.Utf8NoBom.GetBytes( value );

			fixed ( byte* ptr = bytes )
			{
				return Internal.SetConfigValue( type, NetConfigScope.Global, IntPtr.Zero, NetConfigType.String, (IntPtr)ptr );
			}
		}

		/*
		internal unsafe static float GetConfigString( NetConfig type )
		{

			float value = 0;
			NetConfigType dtype = NetConfigType.Float;
			float* ptr = &value;
			ulong size = sizeof( float );
			var result = Internal.GetConfigValue( type, NetScope.Global, 0, ref dtype, (IntPtr)ptr, ref size );
			if ( result != SteamNetworkingGetConfigValueResult.OK )
				return 0;

			return value;
		}
		*/


		/*

		TODO - Connection object

		internal unsafe static bool SetConnectionConfig( uint con, NetConfig type, int value )
		{
			int* ptr = &value;
			return Internal.SetConfigValue( type, NetScope.Connection, con, NetConfigType.Int32, (IntPtr)ptr );
		}

		internal unsafe static bool SetConnectionConfig( uint con, NetConfig type, float value )
		{
			float* ptr = &value;
			return Internal.SetConfigValue( type, NetScope.Connection, con, NetConfigType.Float, (IntPtr)ptr );
		}

		internal unsafe static bool SetConnectionConfig( uint con, NetConfig type, string value )
		{
			var bytes = Utility.Utf8NoBom.GetBytes( value );

			fixed ( byte* ptr = bytes )
			{
				return Internal.SetConfigValue( type, NetScope.Connection, con, NetConfigType.String, (IntPtr)ptr );
			}
		}*/

#endregion
	}
}
