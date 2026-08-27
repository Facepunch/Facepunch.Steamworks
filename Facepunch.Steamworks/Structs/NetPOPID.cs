using System;

namespace Steamworks.Data
{
	/// <summary>
	/// Identifies a Steam Datagram Relay <b>Point of Presence</b> (POP) — one of Valve's relay
	/// clusters. A POP is a physical location full of Valve relay servers, named with a short
	/// airport-style code such as <c>"iad"</c> (Washington DC), <c>"sto"</c> (Stockholm) or
	/// <c>"lhr"</c> (London).
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>What this is for.</b> When two players connect over SDR, their traffic is not routed
	/// directly — it enters Valve's backbone at a relay near one player and leaves it near the
	/// other. Those entry and exit points are POPs. Enumerating them
	/// (<see cref="SteamNetworkingUtils.GetPOPList()"/>) and measuring latency to each
	/// (<see cref="SteamNetworkingUtils.GetDirectPingToPOP(NetPOPID)"/>) is how you answer
	/// "which of Valve's data centers is this player closest to?" — useful for picking a region,
	/// for deciding where to spin up a dedicated server, or just for a diagnostics screen.
	/// A hosted dedicated server also reports its own POP as one of these.
	/// </para>
	/// <para>
	/// <b>The encoding is strange, and that is Valve's doing, not ours.</b> A POP ID is a 3- or
	/// 4-character ASCII code packed into a <see cref="uint"/>. The obvious packing would put
	/// character 0 in the top byte; Valve instead shipped a 3-character format first and later had
	/// to add a fourth character without breaking IDs already stored in their databases. So the
	/// code <c>"abcd"</c> encodes as <c>0xddaabbcc</c> — the <i>fourth</i> character lands in the
	/// <i>most significant</i> byte. Valve's own header comments this with
	/// "deep regret and sadness". This type exists so you never have to think about it: construct
	/// with <see cref="FromCode(string)"/>, read back with <see cref="ToString"/>.
	/// </para>
	/// <para>
	/// Mirrors <c>SteamNetworkingPOPID</c>, <c>CalculateSteamNetworkingPOPIDFromString</c> and
	/// <c>GetSteamNetworkingLocationPOPStringFromID</c> in <c>steamnetworkingtypes.h</c>.
	/// </para>
	/// <para>
	/// The struct is a single <see cref="uint"/>, so copying it is free and it is safe to use as a
	/// dictionary key or to hold in large arrays. Nothing here allocates except
	/// <see cref="ToString"/>; use one of the <c>WriteCode</c> overloads when you need the text on
	/// a hot path.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// await SteamNetworkingUtils.WaitForPingDataAsync();
	///
	/// // Which Valve data center is this player closest to?
	/// var best = default( NetPOPID );
	/// var bestPing = int.MaxValue;
	///
	/// foreach ( var pop in SteamNetworkingUtils.GetPOPList() )
	/// {
	///     var ping = SteamNetworkingUtils.GetDirectPingToPOP( pop );
	///     if ( ping &lt; 0 ) continue;           // not measured yet, or unreachable
	///     if ( ping &gt;= bestPing ) continue;
	///
	///     best = pop;
	///     bestPing = ping;
	/// }
	///
	/// Console.WriteLine( $"Closest POP is {best} at {bestPing}ms" );  // e.g. "fra at 24ms"
	/// </code>
	/// </example>
	public readonly struct NetPOPID : IEquatable<NetPOPID>, IComparable<NetPOPID>
	{
		/// <summary>
		/// The raw packed 32-bit value, exactly as native Steam represents it. It is stable across
		/// runs and machines, so it is safe to send over the wire or store in a database. Prefer
		/// <see cref="ToString"/> whenever a human is going to read it.
		/// </summary>
		public readonly uint Value;

		/// <summary>
		/// The maximum number of characters in a POP code. Codes may be shorter — 3 is the common
		/// case — but are never longer.
		/// </summary>
		public const int MaxCodeLength = 4;

		/// <summary>
		/// The POP code <c>"dev"</c>, which Valve uses in non-production environments for testing.
		/// A dedicated server that reports this is not running in a real Valve data center.
		/// Mirrors <c>k_SteamDatagramPOPID_dev</c>.
		/// </summary>
		public static NetPOPID Dev => new NetPOPID( ( (uint)'d' << 16 ) | ( (uint)'e' << 8 ) | (uint)'v' );

		/// <summary>
		/// The "no POP" value (zero), which Steam returns when there is no data center to report.
		/// </summary>
		public static NetPOPID None => default;

		/// <summary>
		/// Wraps an already-packed native POP ID. If what you have is a human-readable code such as
		/// <c>"iad"</c>, use <see cref="FromCode(string)"/> instead — this constructor does
		/// <b>not</b> pack anything.
		/// </summary>
		public NetPOPID( uint value )
		{
			Value = value;
		}

		/// <summary>
		/// <see langword="false"/> for the zero ID, which Steam uses to mean "no POP" — for example
		/// the <c>viaRelay</c> output of
		/// <see cref="SteamNetworkingUtils.GetPingToDataCenter(NetPOPID, out NetPOPID)"/> when the
		/// route does not pass through an intermediate relay.
		/// </summary>
		public bool IsValid => Value != 0;

		/// <summary>
		/// Packs a 1- to 4-character ASCII POP code (for example <c>"iad"</c>) into its native ID.
		/// </summary>
		/// <param name="code">The POP code. Case sensitive; Valve's codes are lowercase.</param>
		/// <exception cref="ArgumentNullException"><paramref name="code"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="code"/> is empty, longer than <see cref="MaxCodeLength"/>, or contains a
		/// character outside printable ASCII.
		/// </exception>
		/// <remarks>
		/// The printable-ASCII restriction is <b>inferred</b>, not stated by Valve — the native
		/// helper validates nothing at all. It casts each character to <c>uint8</c>, so anything
		/// above U+00FF would silently truncate into a <i>different, valid-looking</i> POP ID, and
		/// every code Valve ships is lowercase ASCII. Rejecting the rest here turns a silent
		/// mis-route into an exception at the call site.
		/// </remarks>
		public static NetPOPID FromCode( string code )
		{
			if ( code == null )
				throw new ArgumentNullException( nameof( code ) );

			if ( !TryParse( code, out var result ) )
				throw new ArgumentException( $"'{code}' is not a valid POP code. Expected 1 to {MaxCodeLength} printable ASCII characters.", nameof( code ) );

			return result;
		}

		/// <summary>
		/// Non-throwing form of <see cref="FromCode(string)"/>. Use this for codes that arrived from
		/// a config file, a command line, or the network.
		/// </summary>
		/// <returns><see langword="true"/> if <paramref name="code"/> was packed successfully.</returns>
		public static unsafe bool TryParse( string code, out NetPOPID result )
		{
			result = default;

			if ( code == null || code.Length == 0 || code.Length > MaxCodeLength )
				return false;

			fixed ( char* ptr = code )
			{
				return TryPack( ptr, code.Length, out result );
			}
		}

#if NETSTANDARD2_1_OR_GREATER || NET
		/// <summary>
		/// Non-throwing form of <see cref="FromCode(string)"/> that parses straight out of a span,
		/// so slicing a larger buffer costs nothing. Available on netstandard2.1+/.NET builds.
		/// </summary>
		public static unsafe bool TryParse( ReadOnlySpan<char> code, out NetPOPID result )
		{
			result = default;

			if ( code.Length == 0 || code.Length > MaxCodeLength )
				return false;

			fixed ( char* ptr = code )
			{
				return TryPack( ptr, code.Length, out result );
			}
		}
#endif

		/// <summary>
		/// Mirrors <c>CalculateSteamNetworkingPOPIDFromString</c>: characters 0, 1 and 2 occupy
		/// bytes 2, 1 and 0, while character 3 occupies byte 3.
		/// </summary>
		private static unsafe bool TryPack( char* code, int length, out NetPOPID result )
		{
			result = default;

			uint value = 0;

			for ( int i = 0; i < length; i++ )
			{
				var c = code[i];

				// Printable ASCII only. NUL would terminate the native string early, and anything
				// above 0x7E either truncates in the uint8 cast or does not round-trip.
				if ( c < 0x21 || c > 0x7E )
					return false;

				value |= i == 3 ? (uint)c << 24 : (uint)c << ( 16 - ( i * 8 ) );
			}

			result = new NetPOPID( value );
			return true;
		}

		/// <summary>
		/// Mirrors <c>GetSteamNetworkingLocationPOPStringFromID</c>. Native always writes four
		/// bytes plus a NUL, so a 3-character code simply has a zero in the last slot; we stop at
		/// the first zero byte exactly as a C string reader would.
		/// </summary>
		private unsafe int WriteCodeCore( char* destination )
		{
			var length = 0;

			for ( int i = 0; i < MaxCodeLength; i++ )
			{
				var b = (byte)( i == 3 ? Value >> 24 : Value >> ( 16 - ( i * 8 ) ) );
				if ( b == 0 )
					break;

				destination[i] = (char)b;
				length++;
			}

			return length;
		}

		/// <summary>
		/// Unpacks the POP code into a caller-owned array without allocating. Use this on any path
		/// that runs per-frame, or in a loop over every POP.
		/// </summary>
		/// <param name="destination">
		/// Receives up to <see cref="MaxCodeLength"/> characters starting at
		/// <paramref name="offset"/>. No terminator is written.
		/// </param>
		/// <param name="offset">Index in <paramref name="destination"/> to start writing at.</param>
		/// <returns>The number of characters written. Zero for an invalid (zero) ID.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="destination"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> is negative.</exception>
		/// <exception cref="ArgumentException">
		/// There is less than <see cref="MaxCodeLength"/> characters of room after
		/// <paramref name="offset"/>.
		/// </exception>
		public unsafe int WriteCode( char[] destination, int offset = 0 )
		{
			if ( destination == null )
				throw new ArgumentNullException( nameof( destination ) );

			if ( offset < 0 )
				throw new ArgumentOutOfRangeException( nameof( offset ) );

			if ( destination.Length - offset < MaxCodeLength )
				throw new ArgumentException( $"Need room for {MaxCodeLength} characters after the offset.", nameof( destination ) );

			fixed ( char* ptr = destination )
			{
				return WriteCodeCore( ptr + offset );
			}
		}

#if NETSTANDARD2_1_OR_GREATER || NET
		/// <summary>
		/// Unpacks the POP code into a caller-owned span without allocating — pairs with
		/// <c>stackalloc</c> or a pooled buffer. Available on netstandard2.1+/.NET builds.
		/// </summary>
		/// <param name="destination">
		/// Receives up to <see cref="MaxCodeLength"/> characters. No terminator is written.
		/// </param>
		/// <returns>The number of characters written. Zero for an invalid (zero) ID.</returns>
		/// <exception cref="ArgumentException">
		/// <paramref name="destination"/> is shorter than <see cref="MaxCodeLength"/>.
		/// </exception>
		public unsafe int WriteCode( Span<char> destination )
		{
			if ( destination.Length < MaxCodeLength )
				throw new ArgumentException( $"Destination must be at least {MaxCodeLength} characters.", nameof( destination ) );

			fixed ( char* ptr = destination )
			{
				return WriteCodeCore( ptr );
			}
		}
#endif

		/// <summary>
		/// The human-readable POP code, for example <c>"iad"</c>. Returns an empty string for a
		/// zero (invalid) ID.
		/// </summary>
		/// <remarks>
		/// This allocates a small string. In a loop over every POP, prefer a <c>WriteCode</c>
		/// overload.
		/// </remarks>
		public override unsafe string ToString()
		{
			char* code = stackalloc char[MaxCodeLength];
			var length = WriteCodeCore( code );

			if ( length == 0 )
				return string.Empty;

			return new string( code, 0, length );
		}

		/// <summary>
		/// Wraps an already-packed native POP ID. Does <b>not</b> pack a code — see
		/// <see cref="FromCode(string)"/> for that.
		/// </summary>
		public static implicit operator NetPOPID( uint value ) => new NetPOPID( value );

		/// <summary>
		/// Unwraps to the raw packed 32-bit value.
		/// </summary>
		public static implicit operator uint( NetPOPID value ) => value.Value;

		/// <inheritdoc/>
		public bool Equals( NetPOPID other ) => other.Value == Value;

		/// <inheritdoc/>
		public override bool Equals( object p ) => p is NetPOPID id && Equals( id );

		/// <inheritdoc/>
		public override int GetHashCode() => Value.GetHashCode();

		public static bool operator ==( NetPOPID a, NetPOPID b ) => a.Equals( b );

		public static bool operator !=( NetPOPID a, NetPOPID b ) => !a.Equals( b );

		/// <summary>
		/// Orders by the raw packed value which — because of Valve's encoding — is <b>not</b>
		/// alphabetical by code. It is stable and cheap, so it is fine for producing deterministic
		/// output or for a sorted dictionary. Sort on <see cref="ToString"/> if you want
		/// alphabetical order for display.
		/// </summary>
		public int CompareTo( NetPOPID other ) => Value.CompareTo( other.Value );
	}
}
