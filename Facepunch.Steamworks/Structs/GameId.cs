using System;

namespace Steamworks.Data
{
	public enum GameIdType : byte
	{
		App = 0,
		GameMod = 1,
		Shortcut = 2,
		P2P = 3,
	}
	
	public struct GameId : IEquatable<GameId>
	{
		/*
		# ifdef VALVE_BIG_ENDIAN
			unsigned int m_nModID : 32;
			unsigned int m_nType : 8;
			unsigned int m_nAppID : 24;
		#else
			unsigned int m_nAppID : 24;
			unsigned int m_nType : 8;
			unsigned int m_nModID : 32;
		#endif
		*/
		
		// 0xAAAAAAAA_BBCCCCCC
		// A = m_nModID
		// B = m_nType
		// C = m_nAppID
		public ulong Value;

		public GameIdType Type
		{
			get => (GameIdType)(byte)( Value >> 24 );
			set => Value = ( Value & 0xFFFFFFFF_00FFFFFF ) | ( (ulong)(byte)value << 24 );
		}
		
		public uint AppId
		{
			get => (uint)( Value & 0x00000000_00FFFFFF );
			set => Value = ( Value & 0xFFFFFFFF_FF000000 ) | (value & 0x00000000_00FFFFFF);
		}
		
		public uint ModId
		{
			get => (uint)( Value >> 32 );
			set => Value = ( Value & 0x00000000_FFFFFFFF ) | ( (ulong)value << 32 );
		}

		public static implicit operator GameId( ulong value )
		{
			return new GameId { Value = value };
		}

		public static implicit operator ulong( GameId value )
		{
			return value.Value;
		}

		public bool Equals(GameId other)
		{
			return Value == other.Value;
		}

		public override bool Equals(object obj)
		{
			return obj is GameId other && Equals(other);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public static bool operator ==(GameId left, GameId right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(GameId left, GameId right)
		{
			return !left.Equals(right);
		}
	}
}
