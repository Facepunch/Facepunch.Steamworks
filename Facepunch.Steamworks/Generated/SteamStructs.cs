using System;
using System.Runtime.InteropServices;
using System.Linq;
using Steamworks.Data;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct FriendGameInfo_t
	{
		internal PackedId GameID; // m_gameID CGameID
		internal uint GameIP; // m_unGameIP uint32
		internal ushort GamePort; // m_usGamePort uint16
		internal ushort QueryPort; // m_usQueryPort uint16
		internal PackedId SteamIDLobby; // m_steamIDLobby CSteamID
		
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal partial struct servernetadr_t
	{
		internal ushort ConnectionPort; // m_usConnectionPort uint16
		internal ushort QueryPort; // m_usQueryPort uint16
		internal uint IP; // m_unIP uint32
		
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal unsafe partial struct gameserveritem_t
	{
		internal servernetadr_t NetAdr; // m_NetAdr servernetadr_t
		internal int Ping; // m_nPing int
		[MarshalAs(UnmanagedType.I1)]
		internal bool HadSuccessfulResponse; // m_bHadSuccessfulResponse bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool DoNotRefresh; // m_bDoNotRefresh bool
		internal string GameDirUTF8() { fixed ( byte* b = GameDir ) return Steamworks.Utility.ReadNullTerminatedUTF8String( b, 32 ); }
		internal fixed byte GameDir[32]; // m_szGameDir char [32]
		internal string MapUTF8() { fixed ( byte* b = Map ) return Steamworks.Utility.ReadNullTerminatedUTF8String( b, 32 ); }
		internal fixed byte Map[32]; // m_szMap char [32]
		internal string GameDescriptionUTF8() { fixed ( byte* b = GameDescription ) return Steamworks.Utility.ReadNullTerminatedUTF8String( b, 64 ); }
		internal fixed byte GameDescription[64]; // m_szGameDescription char [64]
		internal uint AppID; // m_nAppID uint32
		internal int Players; // m_nPlayers int
		internal int MaxPlayers; // m_nMaxPlayers int
		internal int BotPlayers; // m_nBotPlayers int
		[MarshalAs(UnmanagedType.I1)]
		internal bool Password; // m_bPassword bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool Secure; // m_bSecure bool
		internal uint TimeLastPlayed; // m_ulTimeLastPlayed uint32
		internal int ServerVersion; // m_nServerVersion int
		internal string ServerNameUTF8() { fixed ( byte* b = ServerName ) return Steamworks.Utility.ReadNullTerminatedUTF8String( b, 64 ); }
		internal fixed byte ServerName[64]; // m_szServerName char [64]
		internal string GameTagsUTF8() { fixed ( byte* b = GameTags ) return Steamworks.Utility.ReadNullTerminatedUTF8String( b, 128 ); }
		internal fixed byte GameTags[128]; // m_szGameTags char [128]
		internal PackedId SteamID; // m_steamID CSteamID
		
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamPartyBeaconLocation_t
	{
		internal SteamPartyBeaconLocationType Type; // m_eType ESteamPartyBeaconLocationType
		internal ulong LocationID; // m_ulLocationID uint64
		
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamParamStringArray_t
	{
		internal IntPtr Strings; // m_ppStrings const char **
		internal int NumStrings; // m_nNumStrings int32
		
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LeaderboardEntry_t
	{
		internal PackedId SteamIDUser; // m_steamIDUser CSteamID
		internal int GlobalRank; // m_nGlobalRank int32
		internal int Score; // m_nScore int32
		internal int CDetails; // m_cDetails int32
		internal ulong UGC; // m_hUGC UGCHandle_t
		
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct P2PSessionState_t
	{
		internal byte ConnectionActive; // m_bConnectionActive uint8
		internal byte Connecting; // m_bConnecting uint8
		internal byte P2PSessionError; // m_eP2PSessionError uint8
		internal byte UsingRelay; // m_bUsingRelay uint8
		internal int BytesQueuedForSend; // m_nBytesQueuedForSend int32
		internal int PacketsQueuedForSend; // m_nPacketsQueuedForSend int32
		internal uint RemoteIP; // m_nRemoteIP uint32
		internal ushort RemotePort; // m_nRemotePort uint16
		
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamInputActionEvent_t
	{
		internal ulong ControllerHandle; // controllerHandle InputHandle_t
		internal SteamInputActionEventType EEventType; // eEventType ESteamInputActionEventType
		// internal SteamInputActionEvent_t.AnalogAction_t AnalogAction; // analogAction SteamInputActionEvent_t::AnalogAction_t
		
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal unsafe struct SteamUGCDetails_t
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult EResult
		internal WorkshopFileType FileType; // m_eFileType EWorkshopFileType
		internal AppId CreatorAppID; // m_nCreatorAppID AppId_t
		internal AppId ConsumerAppID; // m_nConsumerAppID AppId_t
		internal string TitleUTF8() { fixed ( byte* b = Title ) return Steamworks.Utility.ReadNullTerminatedUTF8String( b, 129 ); }
		internal fixed byte Title[129]; // m_rgchTitle char [129]
		internal string DescriptionUTF8() { fixed ( byte* b = Description ) return Steamworks.Utility.ReadNullTerminatedUTF8String( b, 8000 ); }
		internal fixed byte Description[8000]; // m_rgchDescription char [8000]
		internal ulong SteamIDOwner; // m_ulSteamIDOwner uint64
		internal uint TimeCreated; // m_rtimeCreated uint32
		internal uint TimeUpdated; // m_rtimeUpdated uint32
		internal uint TimeAddedToUserList; // m_rtimeAddedToUserList uint32
		internal RemoteStoragePublishedFileVisibility Visibility; // m_eVisibility ERemoteStoragePublishedFileVisibility
		[MarshalAs(UnmanagedType.I1)]
		internal bool Banned; // m_bBanned bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool AcceptedForUse; // m_bAcceptedForUse bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool TagsTruncated; // m_bTagsTruncated bool
		internal string TagsUTF8() { fixed ( byte* b = Tags ) return Steamworks.Utility.ReadNullTerminatedUTF8String( b, 1025 ); }
		internal fixed byte Tags[1025]; // m_rgchTags char [1025]
		internal ulong File; // m_hFile UGCHandle_t
		internal ulong PreviewFile; // m_hPreviewFile UGCHandle_t
		internal string PchFileNameUTF8() { fixed ( byte* b = PchFileName ) return Steamworks.Utility.ReadNullTerminatedUTF8String( b, 260 ); }
		internal fixed byte PchFileName[260]; // m_pchFileName char [260]
		internal int FileSize; // m_nFileSize int32
		internal int PreviewFileSize; // m_nPreviewFileSize int32
		internal string URLUTF8() { fixed ( byte* b = URL ) return Steamworks.Utility.ReadNullTerminatedUTF8String( b, 256 ); }
		internal fixed byte URL[256]; // m_rgchURL char [256]
		internal uint VotesUp; // m_unVotesUp uint32
		internal uint VotesDown; // m_unVotesDown uint32
		internal float Score; // m_flScore float
		internal uint NumChildren; // m_unNumChildren uint32
		internal ulong TotalFilesSize; // m_ulTotalFilesSize uint64
		
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamItemDetails_t
	{
		internal InventoryItemId ItemId; // m_itemId SteamItemInstanceID_t
		internal InventoryDefId Definition; // m_iDefinition SteamItemDef_t
		internal ushort Quantity; // m_unQuantity uint16
		internal ushort Flags; // m_unFlags uint16
		
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal partial struct SteamDatagramHostedAddress
	{
		internal int CbSize; // m_cbSize int
		internal string DataUTF8() => Steamworks.Utility.Utf8NoBom.GetString( Data, 0, System.Array.IndexOf<byte>( Data, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		internal byte[] Data; // m_data char [128]
		
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal unsafe struct SteamDatagramGameCoordinatorServerLogin
	{
		internal NetIdentity Identity; // m_identity SteamNetworkingIdentity
		internal SteamDatagramHostedAddress Routing; // m_routing SteamDatagramHostedAddress
		internal AppId AppID; // m_nAppID AppId_t
		internal uint Time; // m_rtime RTime32
		internal int CbAppData; // m_cbAppData int
		internal string AppDataUTF8() { fixed ( byte* b = AppData ) return Steamworks.Utility.ReadNullTerminatedUTF8String( b, 2048 ); }
		internal fixed byte AppData[2048]; // m_appData char [2048]
		
	}
	
}
