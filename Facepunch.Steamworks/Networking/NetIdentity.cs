using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	[StructLayout( LayoutKind.Explicit, Size = 136, Pack = 1 )]
	public partial struct NetIdentity
	{
		[FieldOffset( 0 )]
		internal IdentityType type;

		[FieldOffset( 4 )]
		internal int size;

		[FieldOffset( 8 )]
		internal ulong steamid;

		[FieldOffset( 8 )]
		internal NetAddress netaddress;

		/// <summary>
		/// Return a NetIdentity that represents LocalHost
		/// </summary>
		public static NetIdentity LocalHost
		{
			get
			{
				NetIdentity id = default;
				InternalSetLocalHost( ref id );
				return id;
			}
		}


		public readonly bool IsSteamId => type == IdentityType.SteamID;
		public readonly bool IsIpAddress => type == IdentityType.IPAddress;

		/// <summary>
		/// Return true if this identity is localhost
		/// </summary>
		public bool IsLocalHost
		{
			get
			{
				// This used to test "NetIdentity id = default", i.e. an Invalid identity that
				// is never localhost, so the property could only ever return false whatever
				// the receiver held. It has to be a local because the native call takes it by
				// ref; the mistake was initialising that local to default instead of to this.
				// Compare NetAddress.IsLocalHost, which has always been right.
				NetIdentity id = this;
				return InternalIsLocalHost( ref id );
			}
		}

		/// <summary>
		/// Convert to a SteamId
		/// </summary>
		/// <param name="value"></param>
		public static implicit operator NetIdentity( SteamId value )
		{
			NetIdentity id = default;
			InternalSetSteamID( ref id, value );
			return id;
		}

		/// <summary>
		/// Set the specified Address
		/// </summary>
		public static implicit operator NetIdentity( NetAddress address )
		{
			NetIdentity id = default;
			InternalSetIPAddr( ref id, ref address );
			return id;
		}

		/// <summary>
		/// Automatically convert to a SteamId
		/// </summary>
		/// <param name="value"></param>
		public static implicit operator SteamId( NetIdentity value )
		{
			return value.SteamId;
		}

		/// <summary>
		/// Returns NULL if we're not a SteamId
		/// </summary>
		/// <remarks>
		/// Reads the field directly rather than calling
		/// <c>SteamAPI_SteamNetworkingIdentity_GetSteamID64</c>, because that function is a
		/// pure field read behind the very check this property already performs.
		/// steamnetworkingtypes.h:1893 is the whole of it:
		/// <code>
		///   inline uint64 SteamNetworkingIdentity::GetSteamID64() const
		///   { return m_eType == k_ESteamNetworkingIdentityType_SteamID ? m_steamID64 : 0; }
		/// </code>
		/// <c>m_steamID64</c> is the union member this struct maps at <c>FieldOffset( 8 )</c>,
		/// so the managed read returns the identical value with no interop transition and no
		/// copy. That matters because this is on the per-message receive path -
		/// <c>SteamNetworkingMessages.ReceiveMessagesOnChannel</c> reads
		/// <c>msg-&gt;Identity.SteamId</c> once for every message delivered - where it
		/// previously cost one native call plus a 136-byte struct copy, since the native
		/// signature takes the identity by ref and so needed a local to point at.
		/// </remarks>
		public readonly SteamId SteamId
		{
			get
			{
				if ( type != IdentityType.SteamID ) return default;
				return steamid;
			}
		}
		
		/// <summary>
		/// Convert to a SteamId
		/// </summary>
		/// <param name="value"></param>
		public static implicit operator NetIdentity( string value )
		{
			NetIdentity id = default;
			using var str = new Utf8StringToNative( value );
			InternalSetGenericString( ref id, str.Pointer );
			return id;
		}

		/// <summary>
		/// Returns NULL if we're not a NetAddress
		/// </summary>
		public NetAddress Address
		{
			get
			{
				if ( type != IdentityType.IPAddress ) return default;
				var id = this;

				var addrptr = InternalGetIPAddr( ref id );
				return addrptr.ToTypeUnmanaged<NetAddress>();
			}
		}
		
		/// <summary>
		/// Returns NULL if we're not a NetAddress
		/// </summary>
		public string GenericString
		{
			get
			{
				if ( type != IdentityType.GenericString ) return default;
				var id = this;

				var addrptr = InternalGetGenericString( ref id );
				return addrptr;
			}
		}

		/// <summary>
		/// We override tostring to provide a sensible representation
		/// </summary>
		public override string ToString()
		{
			var id = this;
			SteamNetworkingUtils.Internal.SteamNetworkingIdentity_ToString( ref id, out var str );
			return str;
		}

		internal enum IdentityType
		{
			Invalid = 0,
			IPAddress = 1,
			GenericString = 2,
			GenericBytes = 3,
			SteamID = 16
		}
	}
}
