using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	/// <summary>
	/// Describe the state of a connection
	/// </summary>
	[StructLayout( LayoutKind.Sequential, Size = 696 )]
	public unsafe struct ConnectionInfo
	{
		internal NetIdentity identity;
		internal long userData;
		internal Socket listenSocket;
		internal NetAddress address;
		internal ushort pad;
		internal SteamNetworkingPOPID popRemote;
		internal SteamNetworkingPOPID popRelay;
		internal ConnectionState state;
		internal int endReason;

		/// <summary>
		/// <c>char m_szEndDebug[k_cchSteamNetworkingMaxConnectionCloseReason]</c>, 128 bytes
		/// inline. See <see cref="EndDebug"/>.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Held as a fixed size buffer rather than
		/// <c>[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] string</c>. Both occupy
		/// exactly the same 128 inline native bytes, so the ABI is unchanged - but a
		/// <c>string</c> field is a managed reference, and one reference anywhere in a struct
		/// makes the whole struct non-blittable.
		/// </para>
		/// <para>
		/// That mattered a great deal here, because <c>ConnectionInfo</c> is embedded in
		/// <c>SteamNetConnectionStatusChangedCallback_t</c> - the connection-state callback,
		/// one of the highest frequency callbacks in a networked game - and it dragged that
		/// struct out of the blittable set with it. Every delivery therefore had to go
		/// through the full <c>Marshal.PtrToStructure</c> field walker, measured at
		/// <b>256 bytes and 3,932 ns</b> per delivery against roughly 90 ns for a blittable
		/// callback of similar size. With both buffers converted the callback reads with a
		/// raw pointer dereference instead: <b>0 bytes, ~7 ns</b>.
		/// </para>
		/// <para>
		/// The strings are now built only when someone asks for one, which on the overwhelming
		/// majority of connection events is never.
		/// </para>
		/// </remarks>
		internal fixed byte endDebug[128];

		/// <summary>
		/// <c>char m_szConnectionDescription[k_cchSteamNetworkingMaxConnectionDescription]</c>,
		/// 128 bytes inline. See <see cref="ConnectionDescription"/> and the remarks on
		/// <see cref="endDebug"/> for why this is a fixed buffer.
		/// </summary>
		internal fixed byte connectionDescription[128];

		// These are "readonly" members, not just readonly-by-convention. On a 696-byte
		// mutable struct that distinction is expensive: reading a non-readonly member
		// through an "in" parameter or a readonly field forces the compiler to emit a
		// defensive copy of the whole struct first. Measured over three non-inlined frames
		// with a 696-byte struct: 38.6 ns by value, 33.6 ns by "in" with mutable members
		// (the defensive copies eat almost the entire saving), 10.0 ns by "in" with readonly
		// members. Nothing in this library passes ConnectionInfo by "in" yet - the virtual
		// and interface methods that would have to change are public API - so this buys
		// nothing today on its own. It is what makes that change worth making later, and it
		// is free now.

		/// <summary>
		/// High level state of the connection
		/// </summary>
		public readonly ConnectionState State => state;

		/// <summary>
		/// Remote address.  Might be all 0's if we don't know it, or if this is N/A.
		/// </summary>
		public readonly NetAddress Address => address;

		/// <summary>
		/// Who is on the other end?  Depending on the connection type and phase of the connection, we might not know
		/// </summary>
		public readonly NetIdentity Identity => identity;

		/// <summary>
		/// Basic cause of the connection termination or problem.
		/// </summary>
		public readonly NetConnectionEnd EndReason => (NetConnectionEnd)endReason;

		/// <summary>
		/// Human-readable, but non-localized explanation for connection termination or
		/// problem. Intended for debugging and diagnostics only, not to show to users.
		/// </summary>
		/// <remarks>
		/// Decoded from the native buffer on each read, so hold on to the result rather than
		/// calling this in a loop.
		/// </remarks>
		public readonly string EndDebug
		{
			get
			{
				fixed ( byte* b = endDebug )
					return Utility.ReadNullTerminatedUTF8String( b, 128 );
			}
		}

		/// <summary>
		/// Debug description, including the internal connection ID, the connection type and
		/// peer information, and any name the app gave the connection.
		/// </summary>
		/// <remarks>
		/// Decoded from the native buffer on each read, so hold on to the result rather than
		/// calling this in a loop.
		/// </remarks>
		public readonly string ConnectionDescription
		{
			get
			{
				fixed ( byte* b = connectionDescription )
					return Utility.ReadNullTerminatedUTF8String( b, 128 );
			}
		}
	}
}