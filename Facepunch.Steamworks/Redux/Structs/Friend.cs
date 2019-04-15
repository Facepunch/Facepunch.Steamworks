using SteamNative;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks
{
	public struct Friend
	{
		public CSteamID Id;

		public Friend( CSteamID steamid )
		{
			Id = steamid;
		}

		public bool IsFriend => Relationship == FriendRelationship.Friend;
		public bool IsBlocked => Relationship == FriendRelationship.Blocked;


		public FriendRelationship Relationship => Friends.Internal.GetFriendRelationship( Id );
		public PersonaState State => Friends.Internal.GetFriendPersonaState( Id );
		public string Name => Friends.Internal.GetFriendPersonaName( Id );
		public IEnumerable<string> NameHistory
		{
			get
			{
				for( int i=0; i<32; i++ )
				{
					var n = Friends.Internal.GetFriendPersonaNameHistory( Id, i );
					if ( string.IsNullOrEmpty( n ) )
						break;

					yield return n;
				}
			}
		}

		public int SteamLevel => Friends.Internal.GetFriendSteamLevel( Id );



		public FriendGameInfo? GameInfo
		{
			get
			{
				FriendGameInfo_t gameInfo = default( FriendGameInfo_t );
				if ( !Friends.Internal.GetFriendGamePlayed( Id, ref gameInfo ) )
					return null;

				return FriendGameInfo.From( gameInfo );
			}
		}

		public struct FriendGameInfo
		{
			internal ulong GameID; // m_gameID class CGameID
			internal uint GameIP; // m_unGameIP uint32
			internal ushort GamePort; // m_usGamePort uint16
			internal ushort QueryPort; // m_usQueryPort uint16
			internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID

			public static FriendGameInfo From( FriendGameInfo_t i )
			{
				return new FriendGameInfo
				{
					GameID = i.GameID,
					GameIP = i.GameIP,
					GamePort = i.GamePort,
					QueryPort = i.QueryPort,
					SteamIDLobby = i.SteamIDLobby,
				};
			}
		}

	}
}