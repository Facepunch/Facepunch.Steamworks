using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	/// <summary>
	/// A native 64-bit Steam identifier — <c>CSteamID</c> or <c>CGameID</c> — exactly as it is
	/// laid out in memory written by Steam: eight bytes with an alignment of <b>one</b>.
	/// </summary>
	///
	/// <remarks>
	/// <para>
	/// <b><c>Pack = 1</c> is load-bearing. Do not remove it, and do not add a second field.</b>
	/// </para>
	///
	/// <para>
	/// Valve declares both id classes inside a packing block:
	/// <c>steamclientpublic.h:475</c> opens <c>#pragma pack( push, 1 )</c>, <c>class CSteamID</c>
	/// follows at line 480 and <c>class CGameID</c> at line 922, and the block is not popped
	/// until line 1108. Each wraps a single 8-byte union, so in native code:
	/// <c>sizeof == 8</c> and <c>alignof == 1</c>.
	/// </para>
	///
	/// <para>
	/// A C# <c>ulong</c> field, by contrast, wants alignment 8. That single difference is what
	/// mis-laid-out sixteen generated structs: whenever a native id does not happen to sit on an
	/// 8-byte boundary — <c>PSNGameBootInviteResult_t</c> puts one at offset 1, right after a
	/// <c>bool</c> — a <c>ulong</c> gets pushed forward and every following field shifts with
	/// it. The old workaround was to pack the whole struct down to 4
	/// (<c>Generator/SteamApiDefinition.cs</c>'s <c>IsPack4OnWindows</c>), but a struct-wide
	/// <c>Pack</c> cannot say "this 8-byte field aligns to 1 while that one aligns to 8", so it
	/// dragged genuine <c>uint64</c> members off their natural boundary and was wrong in the
	/// other direction. Modelling the alignment on the *type* is what removes the guess: with
	/// this struct in place, every generated struct can carry the platform pack from the header
	/// (8 on Windows, 4 on Linux/macOS — <c>steamclientpublic.h:1163-1176</c>) verbatim.
	/// </para>
	///
	/// <para>
	/// It is deliberately distinct from <see cref="Steamworks.SteamId"/>. <c>SteamId</c> is the
	/// public, ergonomic type games pass around and is used as a by-value P/Invoke argument in
	/// over a hundred places; this one exists purely to occupy the right bytes inside a
	/// marshalled struct. The implicit conversions below mean callers never have to know the
	/// difference.
	/// </para>
	///
	/// <para>
	/// Enforced by <c>verify-struct-layout.ps1</c> against <c>Tools/baselines/layout-win64.txt</c>.
	/// </para>
	/// </remarks>
	[StructLayout( LayoutKind.Sequential, Pack = 1 )]
	internal struct PackedId : IEquatable<PackedId>
	{
		/// <summary>
		/// The raw 64 bits. This must remain the one and only instance field — the whole point
		/// of the type is that it occupies exactly eight bytes at any alignment.
		/// </summary>
		internal ulong Value;

		public static implicit operator PackedId( ulong value ) => new PackedId { Value = value };
		public static implicit operator ulong( PackedId value ) => value.Value;

		public static implicit operator PackedId( SteamId value ) => new PackedId { Value = value.Value };
		public static implicit operator SteamId( PackedId value ) => new SteamId { Value = value.Value };

		public static implicit operator PackedId( GameId value ) => new PackedId { Value = value.Value };
		public static implicit operator GameId( PackedId value ) => new GameId { Value = value.Value };

		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public bool Equals( PackedId other ) => other.Value == Value;
		public override bool Equals( object p ) => p is PackedId other && Equals( other );
		public static bool operator ==( PackedId a, PackedId b ) => a.Equals( b );
		public static bool operator !=( PackedId a, PackedId b ) => !a.Equals( b );
	}
}
