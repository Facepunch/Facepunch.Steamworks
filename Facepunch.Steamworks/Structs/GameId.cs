using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks.Data
{
	public struct GameId
	{
		// TODO - Be able to access these vars

		/*
		 
		enum EGameIDType
		{
			k_EGameIDTypeApp		= 0,
			k_EGameIDTypeGameMod	= 1,
			k_EGameIDTypeShortcut	= 2,
			k_EGameIDTypeP2P		= 3,
		};

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
		public ulong Value;

		public static implicit operator GameId( ulong value )
		{
			return new GameId { Value = value };
		}

		public static implicit operator ulong( GameId value )
		{
			return value.Value;
		}
	}
}