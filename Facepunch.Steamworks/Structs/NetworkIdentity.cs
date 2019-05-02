using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	[StructLayout( LayoutKind.Explicit, Size = 136 )]
	public struct NetworkIdentity
	{
		[FieldOffset( 0 )]
		internal IdentityType type;

		[FieldOffset( 4 )]
		internal int m_cbSize;

		[FieldOffset( 8 )]
		internal SteamId steamID;

		public static implicit operator NetworkIdentity( SteamId value )
		{
			return new NetworkIdentity { steamID = value, type = IdentityType.SteamID, m_cbSize = 8 };
		}

		public static implicit operator SteamId( NetworkIdentity value )
		{
			return value.steamID;
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