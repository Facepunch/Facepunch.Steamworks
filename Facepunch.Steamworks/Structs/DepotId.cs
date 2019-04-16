using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks
{
	public struct DepotId
	{
		public uint Value;

		public static implicit operator DepotId( uint value )
		{
			return new DepotId { Value = value };
		}

		public static implicit operator DepotId( int value )
		{
			return new DepotId { Value = (uint) value };
		}

		public static implicit operator uint( DepotId value )
		{
			return value.Value;
		}
	}
}