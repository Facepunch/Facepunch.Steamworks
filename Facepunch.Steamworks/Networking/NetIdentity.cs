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


		public bool IsSteamId => type == IdentityType.SteamID;
		public bool IsIpAddress => type == IdentityType.IPAddress;

		/// <summary>
		/// Return true if this identity is localhost
		/// </summary>
		public bool IsLocalHost
		{
			get
			{
				NetIdentity id = default;
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
		public SteamId SteamId
		{
			get
			{
				if ( type != IdentityType.SteamID ) return default;
				var id = this;
				return InternalGetSteamID( ref id );
			}
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
				return addrptr.ToType<NetAddress>();
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