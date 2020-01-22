using System.Net;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	[StructLayout( LayoutKind.Explicit, Size = 18, Pack = 1 )]
	public struct NetAddress
	{
		[FieldOffset( 0 )]
		internal IPV4 ip;

		[FieldOffset( 16 )]
		internal ushort port;

		internal struct IPV4
		{
			internal ulong m_8zeros;
			internal ushort m_0000;
			internal ushort m_ffff;
			internal byte ip0;
			internal byte ip1;
			internal byte ip2;
			internal byte ip3;
		}

		/// <summary>
		/// Any IP, specific port
		/// </summary>
		public static NetAddress AnyIp( ushort port )
		{
			return new NetAddress
			{
				ip = new IPV4
				{
					m_8zeros = 0,
					m_0000 = 0,
					m_ffff = 0,
					ip0 = 0,
					ip1 = 0,
					ip2 = 0,
					ip3 = 0,
				},

				port = port
			};
		}

		/// <summary>
		/// Localhost IP, specific port
		/// </summary>
		public static NetAddress LocalHost( ushort port )
		{
			return new NetAddress
			{
				ip = new IPV4
				{
					m_8zeros = 0,
					m_0000 = 0,
					m_ffff = 0,
					ip0 = 0,
					ip1 = 0,
					ip2 = 0,
					ip3 = 1,
				},

				port = port
			};
		}

		/// <summary>
		/// Specific IP, specific port
		/// </summary>
		public static NetAddress From( string addrStr, ushort port )
		{
			return From( IPAddress.Parse( addrStr ), port );
		}

		/// <summary>
		/// Specific IP, specific port
		/// </summary>
		public static NetAddress From( IPAddress address, ushort port )
		{
			var addr = address.GetAddressBytes();

			if ( addr.Length == 4 )
			{
				return new NetAddress
				{
					ip = new IPV4
					{
						m_8zeros = 0,
						m_0000 = 0,
						m_ffff = 0xffff,
						ip0 = addr[0],
						ip1 = addr[1],
						ip2 = addr[2],
						ip3 = addr[3],
					},

					port = port
				};
			}

			throw new System.NotImplementedException( "Oops - no IPV6 support yet?" );
		}
	}
}
