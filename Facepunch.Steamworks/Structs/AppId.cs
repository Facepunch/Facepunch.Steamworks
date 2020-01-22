using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks
{
	public struct AppId
	{
		public uint Value;

		public override string ToString() => Value.ToString();

		public static implicit operator AppId( uint value )
		{
			return new AppId{ Value = value };
		}

		public static implicit operator AppId( int value )
		{
			return new AppId { Value = (uint) value };
		}

		public static implicit operator uint( AppId value )
		{
			return value.Value;
		}
	}
}