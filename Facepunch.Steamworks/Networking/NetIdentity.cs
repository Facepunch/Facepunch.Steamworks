using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	[StructLayout( LayoutKind.Explicit, Size = 136, Pack = 1 )]
	public struct NetIdentity
	{
		[FieldOffset( 0 )]
		internal IdentityType type;

		[FieldOffset( 4 )]
		internal int m_cbSize;

		[FieldOffset( 8 )]
		internal SteamId steamID;

		public static implicit operator NetIdentity( SteamId value )
		{
			return new NetIdentity { steamID = value, type = IdentityType.SteamID, m_cbSize = 8 };
		}

		public static implicit operator SteamId( NetIdentity value )
		{
			return value.steamID;
		}

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