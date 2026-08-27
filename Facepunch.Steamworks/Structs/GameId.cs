using System;

namespace Steamworks.Data
{
	/// <summary>
	/// What kind of thing a <see cref="GameId"/> refers to. Steam uses one identifier space for
	/// store apps and for the things that are not store apps but still show up as "playing" on a
	/// profile.
	/// </summary>
	public enum GameIdType : byte
	{
		/// <summary>
		/// An ordinary Steam application &#8212; something with a store page and an app id. This is
		/// what almost every <see cref="GameId"/> you encounter will be.
		/// </summary>
		App = 0,

		/// <summary>
		/// A mod running under a host app. The host's app id is in <see cref="GameId.AppId"/> and the
		/// mod is distinguished by <see cref="GameId.ModId"/>, which is how two mods of the same game
		/// appear as different "games" in the friends list.
		/// </summary>
		GameMod = 1,

		/// <summary>
		/// A non-Steam game the user added to their library as a shortcut. It has no real app id, so
		/// nothing you can look up in the store or the Web API.
		/// </summary>
		Shortcut = 2,

		/// <summary>
		/// A peer-to-peer file rather than an installed application. Valve documents no use for this
		/// in the current SDK; treat it as legacy.
		/// </summary>
		P2P = 3,
	}

	/// <summary>
	/// Identifies what a Steam user is playing. It is a packed 64-bit value rather than a plain app
	/// id, because "what you are playing" can be a mod or a non-Steam shortcut as well as a store app.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The layout, mirroring Valve's <c>CGameID</c> bitfield, is
	/// <c>0xAAAAAAAA_BBCCCCCC</c>: 32 bits of <see cref="ModId"/>, then 8 bits of
	/// <see cref="Type"/>, then 24 bits of <see cref="AppId"/>. That 24-bit app field is the reason
	/// <see cref="AppId"/> here is narrower than <see cref="Steamworks.AppId"/>, which is a full 32
	/// bits.
	/// </para>
	/// <para>
	/// <b>Do not compare a <see cref="GameId"/> against a bare app id.</b> For a plain app the packed
	/// value happens to equal the app id, but for a mod it does not &#8212; the mod bits are set.
	/// Compare <c>gameId.AppId</c> instead, or you will fail to recognise players running a mod of
	/// your game.
	/// </para>
	/// <para>
	/// <b>This type does not override <c>ToString()</c>,</b> unlike <see cref="Steamworks.SteamId"/>
	/// and <see cref="Steamworks.AppId"/>. Interpolating one into a string yields
	/// <c>"Steamworks.Data.GameId"</c> rather than a number, so log <see cref="Value"/> or
	/// <see cref="AppId"/> explicitly.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// var friend = new Friend( someSteamId );
	/// var playing = friend.GameInfo?.GameID ?? default;
	///
	/// if ( playing.AppId == SteamClient.AppId )
	/// {
	///     // Same game - true for a plain launch AND for someone running a mod of it,
	///     // which a comparison against the raw value would have missed.
	///     if ( playing.Type == GameIdType.GameMod )
	///         Console.WriteLine( $"playing mod {playing.ModId}" );
	/// }
	/// </code>
	/// </example>
	public struct GameId : IEquatable<GameId>
	{
		/*
		# ifdef VALVE_BIG_ENDIAN
			unsigned int m_nModID : 32;
			unsigned int m_nType : 8;
			unsigned int m_nAppID : 24;
		#else
			unsigned int m_nAppID : 24;
			unsigned int m_nType : 8;
			unsigned int m_nModID : 32;
		#endif
		*/
		
		// 0xAAAAAAAA_BBCCCCCC
		// A = m_nModID
		// B = m_nType
		// C = m_nAppID
		/// <summary>
		/// The whole packed 64-bit value. This is what you persist or transmit; <see cref="Type"/>,
		/// <see cref="AppId"/> and <see cref="ModId"/> are views onto slices of it.
		/// </summary>
		public ulong Value;

		/// <summary>
		/// What this identifier refers to &#8212; a store app, a mod, a non-Steam shortcut, or a
		/// peer-to-peer file. Check this before assuming <see cref="AppId"/> means anything useful,
		/// because a shortcut has no real app id.
		/// </summary>
		/// <remarks>
		/// The setter rewrites 8 bits of <see cref="Value"/> in place and leaves the other fields
		/// alone.
		/// </remarks>
		public GameIdType Type
		{
			get => (GameIdType)(byte)( Value >> 24 );
			set => Value = ( Value & 0xFFFFFFFF_00FFFFFF ) | ( (ulong)(byte)value << 24 );
		}

		/// <summary>
		/// The Steam application id, which for a mod is the <i>host</i> game's id rather than the
		/// mod's. This is the field to compare when you want "is this person playing my game", since
		/// it matches whether or not they are running a mod.
		/// </summary>
		/// <remarks>
		/// Only 24 bits wide, because that is all Valve's packed layout allows. App ids above
		/// 16,777,215 cannot be represented here and the setter silently masks them off. The
		/// standalone <see cref="Steamworks.AppId"/> type has the full 32 bits and does not have this
		/// limitation.
		/// </remarks>
		public uint AppId
		{
			get => (uint)( Value & 0x00000000_00FFFFFF );
			set => Value = ( Value & 0xFFFFFFFF_FF000000 ) | (value & 0x00000000_00FFFFFF);
		}

		/// <summary>
		/// Distinguishes one mod from another under the same host app. Only meaningful when
		/// <see cref="Type"/> is <see cref="GameIdType.GameMod"/>; it is 0 for a plain app.
		/// </summary>
		/// <remarks>
		/// Valve does not document how a mod's id is chosen or registered, and it is not a Workshop
		/// published file id. Treat it as an opaque discriminator.
		/// </remarks>
		public uint ModId
		{
			get => (uint)( Value >> 32 );
			set => Value = ( Value & 0x00000000_FFFFFFFF ) | ( (ulong)value << 32 );
		}

		/// <summary>
		/// Wraps a raw packed value. Implicit and unvalidated &#8212; note this takes the <b>whole
		/// packed value</b>, not an app id, so <c>GameId x = 252490;</c> only happens to be correct
		/// because a plain app packs to its own id.
		/// </summary>
		/// <param name="value">The packed 64-bit value.</param>
		/// <returns>A <see cref="GameId"/> carrying that value.</returns>
		public static implicit operator GameId( ulong value )
		{
			return new GameId { Value = value };
		}

		/// <summary>
		/// Unwraps to the raw packed value, for storage or transmission.
		/// </summary>
		/// <param name="value">The id to unwrap.</param>
		/// <returns>The underlying <see cref="Value"/>.</returns>
		public static implicit operator ulong( GameId value )
		{
			return value.Value;
		}

		/// <summary>
		/// Whether two identifiers are the same in every field. A mod and its host game are
		/// <b>not</b> equal even though they share an <see cref="AppId"/>.
		/// </summary>
		/// <param name="other">The identifier to compare against.</param>
		/// <returns><see langword="true"/> if the packed values match exactly.</returns>
		public bool Equals(GameId other)
		{
			return Value == other.Value;
		}

		/// <summary>
		/// Whether the object is a <see cref="GameId"/> with the same packed value.
		/// </summary>
		/// <param name="obj">The object to compare against.</param>
		/// <returns><see langword="true"/> for an equal <see cref="GameId"/>; <see langword="false"/> for anything else, including a bare <see cref="ulong"/> of the same value.</returns>
		public override bool Equals(object obj)
		{
			return obj is GameId other && Equals(other);
		}

		/// <summary>
		/// Hash of the packed value, so these can be used as dictionary keys.
		/// </summary>
		/// <returns>The hash code of <see cref="Value"/>.</returns>
		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		/// <summary>
		/// Whether two identifiers are the same in every field.
		/// </summary>
		/// <param name="left">The first identifier.</param>
		/// <param name="right">The second identifier.</param>
		/// <returns><see langword="true"/> if the packed values match exactly.</returns>
		public static bool operator ==(GameId left, GameId right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Whether two identifiers differ in any field.
		/// </summary>
		/// <param name="left">The first identifier.</param>
		/// <param name="right">The second identifier.</param>
		/// <returns><see langword="true"/> if the packed values differ.</returns>
		public static bool operator !=(GameId left, GameId right)
		{
			return !left.Equals(right);
		}
	}
}
