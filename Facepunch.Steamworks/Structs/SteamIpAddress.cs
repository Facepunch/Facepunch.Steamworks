using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks.Data
{
	[StructLayout( LayoutKind.Explicit, Pack = Platform.StructPlatformPackSize )]
	internal partial struct SteamIPAddress
	{
		[FieldOffset( 0 )]
		public uint Ip4Address; // Host Order

		[FieldOffset( 16 )]
		internal SteamIPType Type; // m_eType ESteamIPType

		public static implicit operator System.Net.IPAddress( SteamIPAddress value )
		{
			if ( value.Type == SteamIPType.Type4 )
				return Utility.Int32ToIp( value.Ip4Address );

			throw new System.Exception( $"Oops - can't convert SteamIPAddress to System.Net.IPAddress because no-one coded support for {value.Type} yet" );
		}
	}
}