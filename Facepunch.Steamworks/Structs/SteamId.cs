using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks
{
	/// <summary>
	/// Represents the ID of a user or steam lobby.
	/// </summary>
	public struct SteamId
	{
		public ulong Value;

		public static implicit operator SteamId( ulong value )
		{
			return new SteamId { Value = value };
		}

		public static implicit operator ulong( SteamId value )
		{
			return value.Value;
		}

		public override string ToString() => Value.ToString();

		public uint AccountId => (uint) (Value & 0xFFFFFFFFul);

		public bool IsValid => Value != default;
	}
}
