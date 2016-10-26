using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct CallbackMsg_t
	{
		public int SteamUser; // m_hSteamUser HSteamUser
		public int Callback; // m_iCallback int
		public IntPtr ParamPtr; // m_pubParam uint8 *
		public int ParamCount; // m_cubParam int
		public static CallbackMsg_t FromPointer( IntPtr p ) { return (CallbackMsg_t) Marshal.PtrToStructure( p, typeof(CallbackMsg_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public int SteamUser; // m_hSteamUser HSteamUser
			public int Callback; // m_iCallback int
			public IntPtr ParamPtr; // m_pubParam uint8 *
			public int ParamCount; // m_cubParam int
			
			public static implicit operator CallbackMsg_t (  CallbackMsg_t.PackSmall d )
			{
				return new CallbackMsg_t()
				{
					SteamUser = d.SteamUser,
					Callback = d.Callback,
					ParamPtr = d.ParamPtr,
					ParamCount = d.ParamCount,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamServerConnectFailure_t
	{
		public Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		public bool StillRetrying; // m_bStillRetrying _Bool
		public static SteamServerConnectFailure_t FromPointer( IntPtr p ) { return (SteamServerConnectFailure_t) Marshal.PtrToStructure( p, typeof(SteamServerConnectFailure_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			public bool StillRetrying; // m_bStillRetrying _Bool
			
			public static implicit operator SteamServerConnectFailure_t (  SteamServerConnectFailure_t.PackSmall d )
			{
				return new SteamServerConnectFailure_t()
				{
					Result = d.Result,
					StillRetrying = d.StillRetrying,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamServersDisconnected_t
	{
		public Result Result; // m_eResult enum EResult
		public static SteamServersDisconnected_t FromPointer( IntPtr p ) { return (SteamServersDisconnected_t) Marshal.PtrToStructure( p, typeof(SteamServersDisconnected_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			
			public static implicit operator SteamServersDisconnected_t (  SteamServersDisconnected_t.PackSmall d )
			{
				return new SteamServersDisconnected_t()
				{
					Result = d.Result,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct ClientGameServerDeny_t
	{
		public uint AppID; // m_uAppID uint32
		public uint GameServerIP; // m_unGameServerIP uint32
		public ushort GameServerPort; // m_usGameServerPort uint16
		public ushort Secure; // m_bSecure uint16
		public uint Reason; // m_uReason uint32
		public static ClientGameServerDeny_t FromPointer( IntPtr p ) { return (ClientGameServerDeny_t) Marshal.PtrToStructure( p, typeof(ClientGameServerDeny_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint AppID; // m_uAppID uint32
			public uint GameServerIP; // m_unGameServerIP uint32
			public ushort GameServerPort; // m_usGameServerPort uint16
			public ushort Secure; // m_bSecure uint16
			public uint Reason; // m_uReason uint32
			
			public static implicit operator ClientGameServerDeny_t (  ClientGameServerDeny_t.PackSmall d )
			{
				return new ClientGameServerDeny_t()
				{
					AppID = d.AppID,
					GameServerIP = d.GameServerIP,
					GameServerPort = d.GameServerPort,
					Secure = d.Secure,
					Reason = d.Reason,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct ValidateAuthTicketResponse_t
	{
		public ulong SteamID; // m_SteamID class CSteamID
		public AuthSessionResponse AuthSessionResponse; // m_eAuthSessionResponse enum EAuthSessionResponse
		public ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
		public static ValidateAuthTicketResponse_t FromPointer( IntPtr p ) { return (ValidateAuthTicketResponse_t) Marshal.PtrToStructure( p, typeof(ValidateAuthTicketResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamID; // m_SteamID class CSteamID
			public AuthSessionResponse AuthSessionResponse; // m_eAuthSessionResponse enum EAuthSessionResponse
			public ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
			
			public static implicit operator ValidateAuthTicketResponse_t (  ValidateAuthTicketResponse_t.PackSmall d )
			{
				return new ValidateAuthTicketResponse_t()
				{
					SteamID = d.SteamID,
					AuthSessionResponse = d.AuthSessionResponse,
					OwnerSteamID = d.OwnerSteamID,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct MicroTxnAuthorizationResponse_t
	{
		public uint AppID; // m_unAppID uint32
		public ulong OrderID; // m_ulOrderID uint64
		public byte Authorized; // m_bAuthorized uint8
		public static MicroTxnAuthorizationResponse_t FromPointer( IntPtr p ) { return (MicroTxnAuthorizationResponse_t) Marshal.PtrToStructure( p, typeof(MicroTxnAuthorizationResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint AppID; // m_unAppID uint32
			public ulong OrderID; // m_ulOrderID uint64
			public byte Authorized; // m_bAuthorized uint8
			
			public static implicit operator MicroTxnAuthorizationResponse_t (  MicroTxnAuthorizationResponse_t.PackSmall d )
			{
				return new MicroTxnAuthorizationResponse_t()
				{
					AppID = d.AppID,
					OrderID = d.OrderID,
					Authorized = d.Authorized,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct EncryptedAppTicketResponse_t
	{
		public Result Result; // m_eResult enum EResult
		public static EncryptedAppTicketResponse_t FromPointer( IntPtr p ) { return (EncryptedAppTicketResponse_t) Marshal.PtrToStructure( p, typeof(EncryptedAppTicketResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			
			public static implicit operator EncryptedAppTicketResponse_t (  EncryptedAppTicketResponse_t.PackSmall d )
			{
				return new EncryptedAppTicketResponse_t()
				{
					Result = d.Result,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GetAuthSessionTicketResponse_t
	{
		public uint AuthTicket; // m_hAuthTicket HAuthTicket
		public Result Result; // m_eResult enum EResult
		public static GetAuthSessionTicketResponse_t FromPointer( IntPtr p ) { return (GetAuthSessionTicketResponse_t) Marshal.PtrToStructure( p, typeof(GetAuthSessionTicketResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint AuthTicket; // m_hAuthTicket HAuthTicket
			public Result Result; // m_eResult enum EResult
			
			public static implicit operator GetAuthSessionTicketResponse_t (  GetAuthSessionTicketResponse_t.PackSmall d )
			{
				return new GetAuthSessionTicketResponse_t()
				{
					AuthTicket = d.AuthTicket,
					Result = d.Result,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GameWebCallback_t
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string URL; // m_szURL char [256]
		public static GameWebCallback_t FromPointer( IntPtr p ) { return (GameWebCallback_t) Marshal.PtrToStructure( p, typeof(GameWebCallback_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string URL; // m_szURL char [256]
			
			public static implicit operator GameWebCallback_t (  GameWebCallback_t.PackSmall d )
			{
				return new GameWebCallback_t()
				{
					URL = d.URL,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct StoreAuthURLResponse_t
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
		public string URL; // m_szURL char [512]
		public static StoreAuthURLResponse_t FromPointer( IntPtr p ) { return (StoreAuthURLResponse_t) Marshal.PtrToStructure( p, typeof(StoreAuthURLResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
			public string URL; // m_szURL char [512]
			
			public static implicit operator StoreAuthURLResponse_t (  StoreAuthURLResponse_t.PackSmall d )
			{
				return new StoreAuthURLResponse_t()
				{
					URL = d.URL,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct FriendGameInfo_t
	{
		public ulong GameID; // m_gameID class CGameID
		public uint GameIP; // m_unGameIP uint32
		public ushort GamePort; // m_usGamePort uint16
		public ushort QueryPort; // m_usQueryPort uint16
		public ulong SteamIDLobby; // m_steamIDLobby class CSteamID
		public static FriendGameInfo_t FromPointer( IntPtr p ) { return (FriendGameInfo_t) Marshal.PtrToStructure( p, typeof(FriendGameInfo_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong GameID; // m_gameID class CGameID
			public uint GameIP; // m_unGameIP uint32
			public ushort GamePort; // m_usGamePort uint16
			public ushort QueryPort; // m_usQueryPort uint16
			public ulong SteamIDLobby; // m_steamIDLobby class CSteamID
			
			public static implicit operator FriendGameInfo_t (  FriendGameInfo_t.PackSmall d )
			{
				return new FriendGameInfo_t()
				{
					GameID = d.GameID,
					GameIP = d.GameIP,
					GamePort = d.GamePort,
					QueryPort = d.QueryPort,
					SteamIDLobby = d.SteamIDLobby,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct FriendSessionStateInfo_t
	{
		public uint IOnlineSessionInstances; // m_uiOnlineSessionInstances uint32
		public byte IPublishedToFriendsSessionInstance; // m_uiPublishedToFriendsSessionInstance uint8
		public static FriendSessionStateInfo_t FromPointer( IntPtr p ) { return (FriendSessionStateInfo_t) Marshal.PtrToStructure( p, typeof(FriendSessionStateInfo_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint IOnlineSessionInstances; // m_uiOnlineSessionInstances uint32
			public byte IPublishedToFriendsSessionInstance; // m_uiPublishedToFriendsSessionInstance uint8
			
			public static implicit operator FriendSessionStateInfo_t (  FriendSessionStateInfo_t.PackSmall d )
			{
				return new FriendSessionStateInfo_t()
				{
					IOnlineSessionInstances = d.IOnlineSessionInstances,
					IPublishedToFriendsSessionInstance = d.IPublishedToFriendsSessionInstance,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct PersonaStateChange_t
	{
		public ulong SteamID; // m_ulSteamID uint64
		public int ChangeFlags; // m_nChangeFlags int
		public static PersonaStateChange_t FromPointer( IntPtr p ) { return (PersonaStateChange_t) Marshal.PtrToStructure( p, typeof(PersonaStateChange_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamID; // m_ulSteamID uint64
			public int ChangeFlags; // m_nChangeFlags int
			
			public static implicit operator PersonaStateChange_t (  PersonaStateChange_t.PackSmall d )
			{
				return new PersonaStateChange_t()
				{
					SteamID = d.SteamID,
					ChangeFlags = d.ChangeFlags,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GameOverlayActivated_t
	{
		public byte Active; // m_bActive uint8
		public static GameOverlayActivated_t FromPointer( IntPtr p ) { return (GameOverlayActivated_t) Marshal.PtrToStructure( p, typeof(GameOverlayActivated_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public byte Active; // m_bActive uint8
			
			public static implicit operator GameOverlayActivated_t (  GameOverlayActivated_t.PackSmall d )
			{
				return new GameOverlayActivated_t()
				{
					Active = d.Active,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GameServerChangeRequested_t
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string Server; // m_rgchServer char [64]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string Password; // m_rgchPassword char [64]
		public static GameServerChangeRequested_t FromPointer( IntPtr p ) { return (GameServerChangeRequested_t) Marshal.PtrToStructure( p, typeof(GameServerChangeRequested_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string Server; // m_rgchServer char [64]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string Password; // m_rgchPassword char [64]
			
			public static implicit operator GameServerChangeRequested_t (  GameServerChangeRequested_t.PackSmall d )
			{
				return new GameServerChangeRequested_t()
				{
					Server = d.Server,
					Password = d.Password,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GameLobbyJoinRequested_t
	{
		public ulong SteamIDLobby; // m_steamIDLobby class CSteamID
		public ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		public static GameLobbyJoinRequested_t FromPointer( IntPtr p ) { return (GameLobbyJoinRequested_t) Marshal.PtrToStructure( p, typeof(GameLobbyJoinRequested_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDLobby; // m_steamIDLobby class CSteamID
			public ulong SteamIDFriend; // m_steamIDFriend class CSteamID
			
			public static implicit operator GameLobbyJoinRequested_t (  GameLobbyJoinRequested_t.PackSmall d )
			{
				return new GameLobbyJoinRequested_t()
				{
					SteamIDLobby = d.SteamIDLobby,
					SteamIDFriend = d.SteamIDFriend,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct AvatarImageLoaded_t
	{
		public ulong SteamID; // m_steamID class CSteamID
		public int Image; // m_iImage int
		public int Wide; // m_iWide int
		public int Tall; // m_iTall int
		public static AvatarImageLoaded_t FromPointer( IntPtr p ) { return (AvatarImageLoaded_t) Marshal.PtrToStructure( p, typeof(AvatarImageLoaded_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamID; // m_steamID class CSteamID
			public int Image; // m_iImage int
			public int Wide; // m_iWide int
			public int Tall; // m_iTall int
			
			public static implicit operator AvatarImageLoaded_t (  AvatarImageLoaded_t.PackSmall d )
			{
				return new AvatarImageLoaded_t()
				{
					SteamID = d.SteamID,
					Image = d.Image,
					Wide = d.Wide,
					Tall = d.Tall,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct ClanOfficerListResponse_t
	{
		public ulong SteamIDClan; // m_steamIDClan class CSteamID
		public int COfficers; // m_cOfficers int
		public byte Success; // m_bSuccess uint8
		public static ClanOfficerListResponse_t FromPointer( IntPtr p ) { return (ClanOfficerListResponse_t) Marshal.PtrToStructure( p, typeof(ClanOfficerListResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDClan; // m_steamIDClan class CSteamID
			public int COfficers; // m_cOfficers int
			public byte Success; // m_bSuccess uint8
			
			public static implicit operator ClanOfficerListResponse_t (  ClanOfficerListResponse_t.PackSmall d )
			{
				return new ClanOfficerListResponse_t()
				{
					SteamIDClan = d.SteamIDClan,
					COfficers = d.COfficers,
					Success = d.Success,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct FriendRichPresenceUpdate_t
	{
		public ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		public uint AppID; // m_nAppID AppId_t
		public static FriendRichPresenceUpdate_t FromPointer( IntPtr p ) { return (FriendRichPresenceUpdate_t) Marshal.PtrToStructure( p, typeof(FriendRichPresenceUpdate_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDFriend; // m_steamIDFriend class CSteamID
			public uint AppID; // m_nAppID AppId_t
			
			public static implicit operator FriendRichPresenceUpdate_t (  FriendRichPresenceUpdate_t.PackSmall d )
			{
				return new FriendRichPresenceUpdate_t()
				{
					SteamIDFriend = d.SteamIDFriend,
					AppID = d.AppID,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GameRichPresenceJoinRequested_t
	{
		public ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string Connect; // m_rgchConnect char [256]
		public static GameRichPresenceJoinRequested_t FromPointer( IntPtr p ) { return (GameRichPresenceJoinRequested_t) Marshal.PtrToStructure( p, typeof(GameRichPresenceJoinRequested_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDFriend; // m_steamIDFriend class CSteamID
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string Connect; // m_rgchConnect char [256]
			
			public static implicit operator GameRichPresenceJoinRequested_t (  GameRichPresenceJoinRequested_t.PackSmall d )
			{
				return new GameRichPresenceJoinRequested_t()
				{
					SteamIDFriend = d.SteamIDFriend,
					Connect = d.Connect,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GameConnectedClanChatMsg_t
	{
		public ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		public int MessageID; // m_iMessageID int
		public static GameConnectedClanChatMsg_t FromPointer( IntPtr p ) { return (GameConnectedClanChatMsg_t) Marshal.PtrToStructure( p, typeof(GameConnectedClanChatMsg_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			public int MessageID; // m_iMessageID int
			
			public static implicit operator GameConnectedClanChatMsg_t (  GameConnectedClanChatMsg_t.PackSmall d )
			{
				return new GameConnectedClanChatMsg_t()
				{
					SteamIDClanChat = d.SteamIDClanChat,
					SteamIDUser = d.SteamIDUser,
					MessageID = d.MessageID,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GameConnectedChatJoin_t
	{
		public ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		public static GameConnectedChatJoin_t FromPointer( IntPtr p ) { return (GameConnectedChatJoin_t) Marshal.PtrToStructure( p, typeof(GameConnectedChatJoin_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator GameConnectedChatJoin_t (  GameConnectedChatJoin_t.PackSmall d )
			{
				return new GameConnectedChatJoin_t()
				{
					SteamIDClanChat = d.SteamIDClanChat,
					SteamIDUser = d.SteamIDUser,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GameConnectedChatLeave_t
	{
		public ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		public bool Kicked; // m_bKicked _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool Dropped; // m_bDropped _Bool
		public static GameConnectedChatLeave_t FromPointer( IntPtr p ) { return (GameConnectedChatLeave_t) Marshal.PtrToStructure( p, typeof(GameConnectedChatLeave_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			[MarshalAs(UnmanagedType.I1)]
			public bool Kicked; // m_bKicked _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool Dropped; // m_bDropped _Bool
			
			public static implicit operator GameConnectedChatLeave_t (  GameConnectedChatLeave_t.PackSmall d )
			{
				return new GameConnectedChatLeave_t()
				{
					SteamIDClanChat = d.SteamIDClanChat,
					SteamIDUser = d.SteamIDUser,
					Kicked = d.Kicked,
					Dropped = d.Dropped,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct DownloadClanActivityCountsResult_t
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool Success; // m_bSuccess _Bool
		public static DownloadClanActivityCountsResult_t FromPointer( IntPtr p ) { return (DownloadClanActivityCountsResult_t) Marshal.PtrToStructure( p, typeof(DownloadClanActivityCountsResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool Success; // m_bSuccess _Bool
			
			public static implicit operator DownloadClanActivityCountsResult_t (  DownloadClanActivityCountsResult_t.PackSmall d )
			{
				return new DownloadClanActivityCountsResult_t()
				{
					Success = d.Success,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct JoinClanChatRoomCompletionResult_t
	{
		public ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		public ChatRoomEnterResponse ChatRoomEnterResponse; // m_eChatRoomEnterResponse enum EChatRoomEnterResponse
		public static JoinClanChatRoomCompletionResult_t FromPointer( IntPtr p ) { return (JoinClanChatRoomCompletionResult_t) Marshal.PtrToStructure( p, typeof(JoinClanChatRoomCompletionResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
			public ChatRoomEnterResponse ChatRoomEnterResponse; // m_eChatRoomEnterResponse enum EChatRoomEnterResponse
			
			public static implicit operator JoinClanChatRoomCompletionResult_t (  JoinClanChatRoomCompletionResult_t.PackSmall d )
			{
				return new JoinClanChatRoomCompletionResult_t()
				{
					SteamIDClanChat = d.SteamIDClanChat,
					ChatRoomEnterResponse = d.ChatRoomEnterResponse,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GameConnectedFriendChatMsg_t
	{
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		public int MessageID; // m_iMessageID int
		public static GameConnectedFriendChatMsg_t FromPointer( IntPtr p ) { return (GameConnectedFriendChatMsg_t) Marshal.PtrToStructure( p, typeof(GameConnectedFriendChatMsg_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			public int MessageID; // m_iMessageID int
			
			public static implicit operator GameConnectedFriendChatMsg_t (  GameConnectedFriendChatMsg_t.PackSmall d )
			{
				return new GameConnectedFriendChatMsg_t()
				{
					SteamIDUser = d.SteamIDUser,
					MessageID = d.MessageID,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct FriendsGetFollowerCount_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong SteamID; // m_steamID class CSteamID
		public int Count; // m_nCount int
		public static FriendsGetFollowerCount_t FromPointer( IntPtr p ) { return (FriendsGetFollowerCount_t) Marshal.PtrToStructure( p, typeof(FriendsGetFollowerCount_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong SteamID; // m_steamID class CSteamID
			public int Count; // m_nCount int
			
			public static implicit operator FriendsGetFollowerCount_t (  FriendsGetFollowerCount_t.PackSmall d )
			{
				return new FriendsGetFollowerCount_t()
				{
					Result = d.Result,
					SteamID = d.SteamID,
					Count = d.Count,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct FriendsIsFollowing_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong SteamID; // m_steamID class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		public bool IsFollowing; // m_bIsFollowing _Bool
		public static FriendsIsFollowing_t FromPointer( IntPtr p ) { return (FriendsIsFollowing_t) Marshal.PtrToStructure( p, typeof(FriendsIsFollowing_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong SteamID; // m_steamID class CSteamID
			[MarshalAs(UnmanagedType.I1)]
			public bool IsFollowing; // m_bIsFollowing _Bool
			
			public static implicit operator FriendsIsFollowing_t (  FriendsIsFollowing_t.PackSmall d )
			{
				return new FriendsIsFollowing_t()
				{
					Result = d.Result,
					SteamID = d.SteamID,
					IsFollowing = d.IsFollowing,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct FriendsEnumerateFollowingList_t
	{
		public Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public ulong[] GSteamID; // m_rgSteamID class CSteamID [50]
		public int ResultsReturned; // m_nResultsReturned int32
		public int TotalResultCount; // m_nTotalResultCount int32
		public static FriendsEnumerateFollowingList_t FromPointer( IntPtr p ) { return (FriendsEnumerateFollowingList_t) Marshal.PtrToStructure( p, typeof(FriendsEnumerateFollowingList_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public ulong[] GSteamID; // m_rgSteamID class CSteamID [50]
			public int ResultsReturned; // m_nResultsReturned int32
			public int TotalResultCount; // m_nTotalResultCount int32
			
			public static implicit operator FriendsEnumerateFollowingList_t (  FriendsEnumerateFollowingList_t.PackSmall d )
			{
				return new FriendsEnumerateFollowingList_t()
				{
					Result = d.Result,
					GSteamID = d.GSteamID,
					ResultsReturned = d.ResultsReturned,
					TotalResultCount = d.TotalResultCount,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SetPersonaNameResponse_t
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool Success; // m_bSuccess _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool LocalSuccess; // m_bLocalSuccess _Bool
		public Result Esult; // m_result enum EResult
		public static SetPersonaNameResponse_t FromPointer( IntPtr p ) { return (SetPersonaNameResponse_t) Marshal.PtrToStructure( p, typeof(SetPersonaNameResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool Success; // m_bSuccess _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool LocalSuccess; // m_bLocalSuccess _Bool
			public Result Esult; // m_result enum EResult
			
			public static implicit operator SetPersonaNameResponse_t (  SetPersonaNameResponse_t.PackSmall d )
			{
				return new SetPersonaNameResponse_t()
				{
					Success = d.Success,
					LocalSuccess = d.LocalSuccess,
					Esult = d.Esult,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LowBatteryPower_t
	{
		public byte MinutesBatteryLeft; // m_nMinutesBatteryLeft uint8
		public static LowBatteryPower_t FromPointer( IntPtr p ) { return (LowBatteryPower_t) Marshal.PtrToStructure( p, typeof(LowBatteryPower_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public byte MinutesBatteryLeft; // m_nMinutesBatteryLeft uint8
			
			public static implicit operator LowBatteryPower_t (  LowBatteryPower_t.PackSmall d )
			{
				return new LowBatteryPower_t()
				{
					MinutesBatteryLeft = d.MinutesBatteryLeft,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamAPICallCompleted_t
	{
		public ulong AsyncCall; // m_hAsyncCall SteamAPICall_t
		public int Callback; // m_iCallback int
		public uint ParamCount; // m_cubParam uint32
		public static SteamAPICallCompleted_t FromPointer( IntPtr p ) { return (SteamAPICallCompleted_t) Marshal.PtrToStructure( p, typeof(SteamAPICallCompleted_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong AsyncCall; // m_hAsyncCall SteamAPICall_t
			public int Callback; // m_iCallback int
			public uint ParamCount; // m_cubParam uint32
			
			public static implicit operator SteamAPICallCompleted_t (  SteamAPICallCompleted_t.PackSmall d )
			{
				return new SteamAPICallCompleted_t()
				{
					AsyncCall = d.AsyncCall,
					Callback = d.Callback,
					ParamCount = d.ParamCount,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct CheckFileSignature_t
	{
		public CheckFileSignature CheckFileSignature; // m_eCheckFileSignature enum ECheckFileSignature
		public static CheckFileSignature_t FromPointer( IntPtr p ) { return (CheckFileSignature_t) Marshal.PtrToStructure( p, typeof(CheckFileSignature_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public CheckFileSignature CheckFileSignature; // m_eCheckFileSignature enum ECheckFileSignature
			
			public static implicit operator CheckFileSignature_t (  CheckFileSignature_t.PackSmall d )
			{
				return new CheckFileSignature_t()
				{
					CheckFileSignature = d.CheckFileSignature,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GamepadTextInputDismissed_t
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool Submitted; // m_bSubmitted _Bool
		public uint SubmittedText; // m_unSubmittedText uint32
		public static GamepadTextInputDismissed_t FromPointer( IntPtr p ) { return (GamepadTextInputDismissed_t) Marshal.PtrToStructure( p, typeof(GamepadTextInputDismissed_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool Submitted; // m_bSubmitted _Bool
			public uint SubmittedText; // m_unSubmittedText uint32
			
			public static implicit operator GamepadTextInputDismissed_t (  GamepadTextInputDismissed_t.PackSmall d )
			{
				return new GamepadTextInputDismissed_t()
				{
					Submitted = d.Submitted,
					SubmittedText = d.SubmittedText,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct MatchMakingKeyValuePair_t
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string Key; // m_szKey char [256]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string Value; // m_szValue char [256]
		public static MatchMakingKeyValuePair_t FromPointer( IntPtr p ) { return (MatchMakingKeyValuePair_t) Marshal.PtrToStructure( p, typeof(MatchMakingKeyValuePair_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string Key; // m_szKey char [256]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string Value; // m_szValue char [256]
			
			public static implicit operator MatchMakingKeyValuePair_t (  MatchMakingKeyValuePair_t.PackSmall d )
			{
				return new MatchMakingKeyValuePair_t()
				{
					Key = d.Key,
					Value = d.Value,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct servernetadr_t
	{
		public ushort ConnectionPort; // m_usConnectionPort uint16
		public ushort QueryPort; // m_usQueryPort uint16
		public uint IP; // m_unIP uint32
		public static servernetadr_t FromPointer( IntPtr p ) { return (servernetadr_t) Marshal.PtrToStructure( p, typeof(servernetadr_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ushort ConnectionPort; // m_usConnectionPort uint16
			public ushort QueryPort; // m_usQueryPort uint16
			public uint IP; // m_unIP uint32
			
			public static implicit operator servernetadr_t (  servernetadr_t.PackSmall d )
			{
				return new servernetadr_t()
				{
					ConnectionPort = d.ConnectionPort,
					QueryPort = d.QueryPort,
					IP = d.IP,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct gameserveritem_t
	{
		public servernetadr_t NetAdr; // m_NetAdr class servernetadr_t
		public int Ping; // m_nPing int
		[MarshalAs(UnmanagedType.I1)]
		public bool HadSuccessfulResponse; // m_bHadSuccessfulResponse _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool DoNotRefresh; // m_bDoNotRefresh _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string GameDir; // m_szGameDir char [32]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string Map; // m_szMap char [32]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string GameDescription; // m_szGameDescription char [64]
		public uint AppID; // m_nAppID uint32
		public int Players; // m_nPlayers int
		public int MaxPlayers; // m_nMaxPlayers int
		public int BotPlayers; // m_nBotPlayers int
		[MarshalAs(UnmanagedType.I1)]
		public bool Password; // m_bPassword _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool Secure; // m_bSecure _Bool
		public uint TimeLastPlayed; // m_ulTimeLastPlayed uint32
		public int ServerVersion; // m_nServerVersion int
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string ServerName; // m_szServerName char [64]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string GameTags; // m_szGameTags char [128]
		public ulong SteamID; // m_steamID class CSteamID
		public static gameserveritem_t FromPointer( IntPtr p ) { return (gameserveritem_t) Marshal.PtrToStructure( p, typeof(gameserveritem_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public servernetadr_t NetAdr; // m_NetAdr class servernetadr_t
			public int Ping; // m_nPing int
			[MarshalAs(UnmanagedType.I1)]
			public bool HadSuccessfulResponse; // m_bHadSuccessfulResponse _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool DoNotRefresh; // m_bDoNotRefresh _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string GameDir; // m_szGameDir char [32]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string Map; // m_szMap char [32]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string GameDescription; // m_szGameDescription char [64]
			public uint AppID; // m_nAppID uint32
			public int Players; // m_nPlayers int
			public int MaxPlayers; // m_nMaxPlayers int
			public int BotPlayers; // m_nBotPlayers int
			[MarshalAs(UnmanagedType.I1)]
			public bool Password; // m_bPassword _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool Secure; // m_bSecure _Bool
			public uint TimeLastPlayed; // m_ulTimeLastPlayed uint32
			public int ServerVersion; // m_nServerVersion int
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string ServerName; // m_szServerName char [64]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string GameTags; // m_szGameTags char [128]
			public ulong SteamID; // m_steamID class CSteamID
			
			public static implicit operator gameserveritem_t (  gameserveritem_t.PackSmall d )
			{
				return new gameserveritem_t()
				{
					NetAdr = d.NetAdr,
					Ping = d.Ping,
					HadSuccessfulResponse = d.HadSuccessfulResponse,
					DoNotRefresh = d.DoNotRefresh,
					GameDir = d.GameDir,
					Map = d.Map,
					GameDescription = d.GameDescription,
					AppID = d.AppID,
					Players = d.Players,
					MaxPlayers = d.MaxPlayers,
					BotPlayers = d.BotPlayers,
					Password = d.Password,
					Secure = d.Secure,
					TimeLastPlayed = d.TimeLastPlayed,
					ServerVersion = d.ServerVersion,
					ServerName = d.ServerName,
					GameTags = d.GameTags,
					SteamID = d.SteamID,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct FavoritesListChanged_t
	{
		public uint IP; // m_nIP uint32
		public uint QueryPort; // m_nQueryPort uint32
		public uint ConnPort; // m_nConnPort uint32
		public uint AppID; // m_nAppID uint32
		public uint Flags; // m_nFlags uint32
		[MarshalAs(UnmanagedType.I1)]
		public bool Add; // m_bAdd _Bool
		public uint AccountId; // m_unAccountId AccountID_t
		public static FavoritesListChanged_t FromPointer( IntPtr p ) { return (FavoritesListChanged_t) Marshal.PtrToStructure( p, typeof(FavoritesListChanged_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint IP; // m_nIP uint32
			public uint QueryPort; // m_nQueryPort uint32
			public uint ConnPort; // m_nConnPort uint32
			public uint AppID; // m_nAppID uint32
			public uint Flags; // m_nFlags uint32
			[MarshalAs(UnmanagedType.I1)]
			public bool Add; // m_bAdd _Bool
			public uint AccountId; // m_unAccountId AccountID_t
			
			public static implicit operator FavoritesListChanged_t (  FavoritesListChanged_t.PackSmall d )
			{
				return new FavoritesListChanged_t()
				{
					IP = d.IP,
					QueryPort = d.QueryPort,
					ConnPort = d.ConnPort,
					AppID = d.AppID,
					Flags = d.Flags,
					Add = d.Add,
					AccountId = d.AccountId,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LobbyInvite_t
	{
		public ulong SteamIDUser; // m_ulSteamIDUser uint64
		public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		public ulong GameID; // m_ulGameID uint64
		public static LobbyInvite_t FromPointer( IntPtr p ) { return (LobbyInvite_t) Marshal.PtrToStructure( p, typeof(LobbyInvite_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDUser; // m_ulSteamIDUser uint64
			public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			public ulong GameID; // m_ulGameID uint64
			
			public static implicit operator LobbyInvite_t (  LobbyInvite_t.PackSmall d )
			{
				return new LobbyInvite_t()
				{
					SteamIDUser = d.SteamIDUser,
					SteamIDLobby = d.SteamIDLobby,
					GameID = d.GameID,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LobbyEnter_t
	{
		public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		public uint GfChatPermissions; // m_rgfChatPermissions uint32
		[MarshalAs(UnmanagedType.I1)]
		public bool Locked; // m_bLocked _Bool
		public uint EChatRoomEnterResponse; // m_EChatRoomEnterResponse uint32
		public static LobbyEnter_t FromPointer( IntPtr p ) { return (LobbyEnter_t) Marshal.PtrToStructure( p, typeof(LobbyEnter_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			public uint GfChatPermissions; // m_rgfChatPermissions uint32
			[MarshalAs(UnmanagedType.I1)]
			public bool Locked; // m_bLocked _Bool
			public uint EChatRoomEnterResponse; // m_EChatRoomEnterResponse uint32
			
			public static implicit operator LobbyEnter_t (  LobbyEnter_t.PackSmall d )
			{
				return new LobbyEnter_t()
				{
					SteamIDLobby = d.SteamIDLobby,
					GfChatPermissions = d.GfChatPermissions,
					Locked = d.Locked,
					EChatRoomEnterResponse = d.EChatRoomEnterResponse,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LobbyDataUpdate_t
	{
		public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		public ulong SteamIDMember; // m_ulSteamIDMember uint64
		public byte Success; // m_bSuccess uint8
		public static LobbyDataUpdate_t FromPointer( IntPtr p ) { return (LobbyDataUpdate_t) Marshal.PtrToStructure( p, typeof(LobbyDataUpdate_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			public ulong SteamIDMember; // m_ulSteamIDMember uint64
			public byte Success; // m_bSuccess uint8
			
			public static implicit operator LobbyDataUpdate_t (  LobbyDataUpdate_t.PackSmall d )
			{
				return new LobbyDataUpdate_t()
				{
					SteamIDLobby = d.SteamIDLobby,
					SteamIDMember = d.SteamIDMember,
					Success = d.Success,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LobbyChatUpdate_t
	{
		public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		public ulong SteamIDUserChanged; // m_ulSteamIDUserChanged uint64
		public ulong SteamIDMakingChange; // m_ulSteamIDMakingChange uint64
		public uint GfChatMemberStateChange; // m_rgfChatMemberStateChange uint32
		public static LobbyChatUpdate_t FromPointer( IntPtr p ) { return (LobbyChatUpdate_t) Marshal.PtrToStructure( p, typeof(LobbyChatUpdate_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			public ulong SteamIDUserChanged; // m_ulSteamIDUserChanged uint64
			public ulong SteamIDMakingChange; // m_ulSteamIDMakingChange uint64
			public uint GfChatMemberStateChange; // m_rgfChatMemberStateChange uint32
			
			public static implicit operator LobbyChatUpdate_t (  LobbyChatUpdate_t.PackSmall d )
			{
				return new LobbyChatUpdate_t()
				{
					SteamIDLobby = d.SteamIDLobby,
					SteamIDUserChanged = d.SteamIDUserChanged,
					SteamIDMakingChange = d.SteamIDMakingChange,
					GfChatMemberStateChange = d.GfChatMemberStateChange,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LobbyChatMsg_t
	{
		public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		public ulong SteamIDUser; // m_ulSteamIDUser uint64
		public byte ChatEntryType; // m_eChatEntryType uint8
		public uint ChatID; // m_iChatID uint32
		public static LobbyChatMsg_t FromPointer( IntPtr p ) { return (LobbyChatMsg_t) Marshal.PtrToStructure( p, typeof(LobbyChatMsg_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			public ulong SteamIDUser; // m_ulSteamIDUser uint64
			public byte ChatEntryType; // m_eChatEntryType uint8
			public uint ChatID; // m_iChatID uint32
			
			public static implicit operator LobbyChatMsg_t (  LobbyChatMsg_t.PackSmall d )
			{
				return new LobbyChatMsg_t()
				{
					SteamIDLobby = d.SteamIDLobby,
					SteamIDUser = d.SteamIDUser,
					ChatEntryType = d.ChatEntryType,
					ChatID = d.ChatID,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LobbyGameCreated_t
	{
		public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		public ulong SteamIDGameServer; // m_ulSteamIDGameServer uint64
		public uint IP; // m_unIP uint32
		public ushort Port; // m_usPort uint16
		public static LobbyGameCreated_t FromPointer( IntPtr p ) { return (LobbyGameCreated_t) Marshal.PtrToStructure( p, typeof(LobbyGameCreated_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			public ulong SteamIDGameServer; // m_ulSteamIDGameServer uint64
			public uint IP; // m_unIP uint32
			public ushort Port; // m_usPort uint16
			
			public static implicit operator LobbyGameCreated_t (  LobbyGameCreated_t.PackSmall d )
			{
				return new LobbyGameCreated_t()
				{
					SteamIDLobby = d.SteamIDLobby,
					SteamIDGameServer = d.SteamIDGameServer,
					IP = d.IP,
					Port = d.Port,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LobbyMatchList_t
	{
		public uint LobbiesMatching; // m_nLobbiesMatching uint32
		public static LobbyMatchList_t FromPointer( IntPtr p ) { return (LobbyMatchList_t) Marshal.PtrToStructure( p, typeof(LobbyMatchList_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint LobbiesMatching; // m_nLobbiesMatching uint32
			
			public static implicit operator LobbyMatchList_t (  LobbyMatchList_t.PackSmall d )
			{
				return new LobbyMatchList_t()
				{
					LobbiesMatching = d.LobbiesMatching,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LobbyKicked_t
	{
		public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		public ulong SteamIDAdmin; // m_ulSteamIDAdmin uint64
		public byte KickedDueToDisconnect; // m_bKickedDueToDisconnect uint8
		public static LobbyKicked_t FromPointer( IntPtr p ) { return (LobbyKicked_t) Marshal.PtrToStructure( p, typeof(LobbyKicked_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			public ulong SteamIDAdmin; // m_ulSteamIDAdmin uint64
			public byte KickedDueToDisconnect; // m_bKickedDueToDisconnect uint8
			
			public static implicit operator LobbyKicked_t (  LobbyKicked_t.PackSmall d )
			{
				return new LobbyKicked_t()
				{
					SteamIDLobby = d.SteamIDLobby,
					SteamIDAdmin = d.SteamIDAdmin,
					KickedDueToDisconnect = d.KickedDueToDisconnect,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LobbyCreated_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		public static LobbyCreated_t FromPointer( IntPtr p ) { return (LobbyCreated_t) Marshal.PtrToStructure( p, typeof(LobbyCreated_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			
			public static implicit operator LobbyCreated_t (  LobbyCreated_t.PackSmall d )
			{
				return new LobbyCreated_t()
				{
					Result = d.Result,
					SteamIDLobby = d.SteamIDLobby,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct PSNGameBootInviteResult_t
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool GameBootInviteExists; // m_bGameBootInviteExists _Bool
		public ulong SteamIDLobby; // m_steamIDLobby class CSteamID
		public static PSNGameBootInviteResult_t FromPointer( IntPtr p ) { return (PSNGameBootInviteResult_t) Marshal.PtrToStructure( p, typeof(PSNGameBootInviteResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool GameBootInviteExists; // m_bGameBootInviteExists _Bool
			public ulong SteamIDLobby; // m_steamIDLobby class CSteamID
			
			public static implicit operator PSNGameBootInviteResult_t (  PSNGameBootInviteResult_t.PackSmall d )
			{
				return new PSNGameBootInviteResult_t()
				{
					GameBootInviteExists = d.GameBootInviteExists,
					SteamIDLobby = d.SteamIDLobby,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct FavoritesListAccountsUpdated_t
	{
		public Result Result; // m_eResult enum EResult
		public static FavoritesListAccountsUpdated_t FromPointer( IntPtr p ) { return (FavoritesListAccountsUpdated_t) Marshal.PtrToStructure( p, typeof(FavoritesListAccountsUpdated_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			
			public static implicit operator FavoritesListAccountsUpdated_t (  FavoritesListAccountsUpdated_t.PackSmall d )
			{
				return new FavoritesListAccountsUpdated_t()
				{
					Result = d.Result,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamParamStringArray_t
	{
		public IntPtr PpStrings; // m_ppStrings const char **
		public int NumStrings; // m_nNumStrings int32
		public static SteamParamStringArray_t FromPointer( IntPtr p ) { return (SteamParamStringArray_t) Marshal.PtrToStructure( p, typeof(SteamParamStringArray_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public IntPtr PpStrings; // m_ppStrings const char **
			public int NumStrings; // m_nNumStrings int32
			
			public static implicit operator SteamParamStringArray_t (  SteamParamStringArray_t.PackSmall d )
			{
				return new SteamParamStringArray_t()
				{
					PpStrings = d.PpStrings,
					NumStrings = d.NumStrings,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageAppSyncedClient_t
	{
		public uint AppID; // m_nAppID AppId_t
		public Result Result; // m_eResult enum EResult
		public int NumDownloads; // m_unNumDownloads int
		public static RemoteStorageAppSyncedClient_t FromPointer( IntPtr p ) { return (RemoteStorageAppSyncedClient_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncedClient_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint AppID; // m_nAppID AppId_t
			public Result Result; // m_eResult enum EResult
			public int NumDownloads; // m_unNumDownloads int
			
			public static implicit operator RemoteStorageAppSyncedClient_t (  RemoteStorageAppSyncedClient_t.PackSmall d )
			{
				return new RemoteStorageAppSyncedClient_t()
				{
					AppID = d.AppID,
					Result = d.Result,
					NumDownloads = d.NumDownloads,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageAppSyncedServer_t
	{
		public uint AppID; // m_nAppID AppId_t
		public Result Result; // m_eResult enum EResult
		public int NumUploads; // m_unNumUploads int
		public static RemoteStorageAppSyncedServer_t FromPointer( IntPtr p ) { return (RemoteStorageAppSyncedServer_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncedServer_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint AppID; // m_nAppID AppId_t
			public Result Result; // m_eResult enum EResult
			public int NumUploads; // m_unNumUploads int
			
			public static implicit operator RemoteStorageAppSyncedServer_t (  RemoteStorageAppSyncedServer_t.PackSmall d )
			{
				return new RemoteStorageAppSyncedServer_t()
				{
					AppID = d.AppID,
					Result = d.Result,
					NumUploads = d.NumUploads,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageAppSyncProgress_t
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string CurrentFile; // m_rgchCurrentFile char [260]
		public uint AppID; // m_nAppID AppId_t
		public uint BytesTransferredThisChunk; // m_uBytesTransferredThisChunk uint32
		public double DAppPercentComplete; // m_dAppPercentComplete double
		[MarshalAs(UnmanagedType.I1)]
		public bool Uploading; // m_bUploading _Bool
		public static RemoteStorageAppSyncProgress_t FromPointer( IntPtr p ) { return (RemoteStorageAppSyncProgress_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncProgress_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string CurrentFile; // m_rgchCurrentFile char [260]
			public uint AppID; // m_nAppID AppId_t
			public uint BytesTransferredThisChunk; // m_uBytesTransferredThisChunk uint32
			public double DAppPercentComplete; // m_dAppPercentComplete double
			[MarshalAs(UnmanagedType.I1)]
			public bool Uploading; // m_bUploading _Bool
			
			public static implicit operator RemoteStorageAppSyncProgress_t (  RemoteStorageAppSyncProgress_t.PackSmall d )
			{
				return new RemoteStorageAppSyncProgress_t()
				{
					CurrentFile = d.CurrentFile,
					AppID = d.AppID,
					BytesTransferredThisChunk = d.BytesTransferredThisChunk,
					DAppPercentComplete = d.DAppPercentComplete,
					Uploading = d.Uploading,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageAppSyncStatusCheck_t
	{
		public uint AppID; // m_nAppID AppId_t
		public Result Result; // m_eResult enum EResult
		public static RemoteStorageAppSyncStatusCheck_t FromPointer( IntPtr p ) { return (RemoteStorageAppSyncStatusCheck_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncStatusCheck_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint AppID; // m_nAppID AppId_t
			public Result Result; // m_eResult enum EResult
			
			public static implicit operator RemoteStorageAppSyncStatusCheck_t (  RemoteStorageAppSyncStatusCheck_t.PackSmall d )
			{
				return new RemoteStorageAppSyncStatusCheck_t()
				{
					AppID = d.AppID,
					Result = d.Result,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageFileShareResult_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong File; // m_hFile UGCHandle_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string Filename; // m_rgchFilename char [260]
		public static RemoteStorageFileShareResult_t FromPointer( IntPtr p ) { return (RemoteStorageFileShareResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageFileShareResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong File; // m_hFile UGCHandle_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string Filename; // m_rgchFilename char [260]
			
			public static implicit operator RemoteStorageFileShareResult_t (  RemoteStorageFileShareResult_t.PackSmall d )
			{
				return new RemoteStorageFileShareResult_t()
				{
					Result = d.Result,
					File = d.File,
					Filename = d.Filename,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStoragePublishFileResult_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		public bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		public static RemoteStoragePublishFileResult_t FromPointer( IntPtr p ) { return (RemoteStoragePublishFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishFileResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			[MarshalAs(UnmanagedType.I1)]
			public bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			
			public static implicit operator RemoteStoragePublishFileResult_t (  RemoteStoragePublishFileResult_t.PackSmall d )
			{
				return new RemoteStoragePublishFileResult_t()
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageDeletePublishedFileResult_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public static RemoteStorageDeletePublishedFileResult_t FromPointer( IntPtr p ) { return (RemoteStorageDeletePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageDeletePublishedFileResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoteStorageDeletePublishedFileResult_t (  RemoteStorageDeletePublishedFileResult_t.PackSmall d )
			{
				return new RemoteStorageDeletePublishedFileResult_t()
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageEnumerateUserPublishedFilesResult_t
	{
		public Result Result; // m_eResult enum EResult
		public int ResultsReturned; // m_nResultsReturned int32
		public int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		public static RemoteStorageEnumerateUserPublishedFilesResult_t FromPointer( IntPtr p ) { return (RemoteStorageEnumerateUserPublishedFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserPublishedFilesResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public int ResultsReturned; // m_nResultsReturned int32
			public int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			
			public static implicit operator RemoteStorageEnumerateUserPublishedFilesResult_t (  RemoteStorageEnumerateUserPublishedFilesResult_t.PackSmall d )
			{
				return new RemoteStorageEnumerateUserPublishedFilesResult_t()
				{
					Result = d.Result,
					ResultsReturned = d.ResultsReturned,
					TotalResultCount = d.TotalResultCount,
					GPublishedFileId = d.GPublishedFileId,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageSubscribePublishedFileResult_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public static RemoteStorageSubscribePublishedFileResult_t FromPointer( IntPtr p ) { return (RemoteStorageSubscribePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageSubscribePublishedFileResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoteStorageSubscribePublishedFileResult_t (  RemoteStorageSubscribePublishedFileResult_t.PackSmall d )
			{
				return new RemoteStorageSubscribePublishedFileResult_t()
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageEnumerateUserSubscribedFilesResult_t
	{
		public Result Result; // m_eResult enum EResult
		public int ResultsReturned; // m_nResultsReturned int32
		public int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public uint[] GRTimeSubscribed; // m_rgRTimeSubscribed uint32 [50]
		public static RemoteStorageEnumerateUserSubscribedFilesResult_t FromPointer( IntPtr p ) { return (RemoteStorageEnumerateUserSubscribedFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public int ResultsReturned; // m_nResultsReturned int32
			public int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public uint[] GRTimeSubscribed; // m_rgRTimeSubscribed uint32 [50]
			
			public static implicit operator RemoteStorageEnumerateUserSubscribedFilesResult_t (  RemoteStorageEnumerateUserSubscribedFilesResult_t.PackSmall d )
			{
				return new RemoteStorageEnumerateUserSubscribedFilesResult_t()
				{
					Result = d.Result,
					ResultsReturned = d.ResultsReturned,
					TotalResultCount = d.TotalResultCount,
					GPublishedFileId = d.GPublishedFileId,
					GRTimeSubscribed = d.GRTimeSubscribed,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageUnsubscribePublishedFileResult_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public static RemoteStorageUnsubscribePublishedFileResult_t FromPointer( IntPtr p ) { return (RemoteStorageUnsubscribePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUnsubscribePublishedFileResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoteStorageUnsubscribePublishedFileResult_t (  RemoteStorageUnsubscribePublishedFileResult_t.PackSmall d )
			{
				return new RemoteStorageUnsubscribePublishedFileResult_t()
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageUpdatePublishedFileResult_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		public bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		public static RemoteStorageUpdatePublishedFileResult_t FromPointer( IntPtr p ) { return (RemoteStorageUpdatePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUpdatePublishedFileResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			[MarshalAs(UnmanagedType.I1)]
			public bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			
			public static implicit operator RemoteStorageUpdatePublishedFileResult_t (  RemoteStorageUpdatePublishedFileResult_t.PackSmall d )
			{
				return new RemoteStorageUpdatePublishedFileResult_t()
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageDownloadUGCResult_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong File; // m_hFile UGCHandle_t
		public uint AppID; // m_nAppID AppId_t
		public int SizeInBytes; // m_nSizeInBytes int32
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string PchFileName; // m_pchFileName char [260]
		public ulong SteamIDOwner; // m_ulSteamIDOwner uint64
		public static RemoteStorageDownloadUGCResult_t FromPointer( IntPtr p ) { return (RemoteStorageDownloadUGCResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageDownloadUGCResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong File; // m_hFile UGCHandle_t
			public uint AppID; // m_nAppID AppId_t
			public int SizeInBytes; // m_nSizeInBytes int32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string PchFileName; // m_pchFileName char [260]
			public ulong SteamIDOwner; // m_ulSteamIDOwner uint64
			
			public static implicit operator RemoteStorageDownloadUGCResult_t (  RemoteStorageDownloadUGCResult_t.PackSmall d )
			{
				return new RemoteStorageDownloadUGCResult_t()
				{
					Result = d.Result,
					File = d.File,
					AppID = d.AppID,
					SizeInBytes = d.SizeInBytes,
					PchFileName = d.PchFileName,
					SteamIDOwner = d.SteamIDOwner,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageGetPublishedFileDetailsResult_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public uint CreatorAppID; // m_nCreatorAppID AppId_t
		public uint ConsumerAppID; // m_nConsumerAppID AppId_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
		public string Title; // m_rgchTitle char [129]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
		public string Description; // m_rgchDescription char [8000]
		public ulong File; // m_hFile UGCHandle_t
		public ulong PreviewFile; // m_hPreviewFile UGCHandle_t
		public ulong SteamIDOwner; // m_ulSteamIDOwner uint64
		public uint TimeCreated; // m_rtimeCreated uint32
		public uint TimeUpdated; // m_rtimeUpdated uint32
		public RemoteStoragePublishedFileVisibility Visibility; // m_eVisibility enum ERemoteStoragePublishedFileVisibility
		[MarshalAs(UnmanagedType.I1)]
		public bool Banned; // m_bBanned _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
		public string Tags; // m_rgchTags char [1025]
		[MarshalAs(UnmanagedType.I1)]
		public bool TagsTruncated; // m_bTagsTruncated _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string PchFileName; // m_pchFileName char [260]
		public int FileSize; // m_nFileSize int32
		public int PreviewFileSize; // m_nPreviewFileSize int32
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string URL; // m_rgchURL char [256]
		public WorkshopFileType FileType; // m_eFileType enum EWorkshopFileType
		[MarshalAs(UnmanagedType.I1)]
		public bool AcceptedForUse; // m_bAcceptedForUse _Bool
		public static RemoteStorageGetPublishedFileDetailsResult_t FromPointer( IntPtr p ) { return (RemoteStorageGetPublishedFileDetailsResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageGetPublishedFileDetailsResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public uint CreatorAppID; // m_nCreatorAppID AppId_t
			public uint ConsumerAppID; // m_nConsumerAppID AppId_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
			public string Title; // m_rgchTitle char [129]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
			public string Description; // m_rgchDescription char [8000]
			public ulong File; // m_hFile UGCHandle_t
			public ulong PreviewFile; // m_hPreviewFile UGCHandle_t
			public ulong SteamIDOwner; // m_ulSteamIDOwner uint64
			public uint TimeCreated; // m_rtimeCreated uint32
			public uint TimeUpdated; // m_rtimeUpdated uint32
			public RemoteStoragePublishedFileVisibility Visibility; // m_eVisibility enum ERemoteStoragePublishedFileVisibility
			[MarshalAs(UnmanagedType.I1)]
			public bool Banned; // m_bBanned _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
			public string Tags; // m_rgchTags char [1025]
			[MarshalAs(UnmanagedType.I1)]
			public bool TagsTruncated; // m_bTagsTruncated _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string PchFileName; // m_pchFileName char [260]
			public int FileSize; // m_nFileSize int32
			public int PreviewFileSize; // m_nPreviewFileSize int32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string URL; // m_rgchURL char [256]
			public WorkshopFileType FileType; // m_eFileType enum EWorkshopFileType
			[MarshalAs(UnmanagedType.I1)]
			public bool AcceptedForUse; // m_bAcceptedForUse _Bool
			
			public static implicit operator RemoteStorageGetPublishedFileDetailsResult_t (  RemoteStorageGetPublishedFileDetailsResult_t.PackSmall d )
			{
				return new RemoteStorageGetPublishedFileDetailsResult_t()
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					CreatorAppID = d.CreatorAppID,
					ConsumerAppID = d.ConsumerAppID,
					Title = d.Title,
					Description = d.Description,
					File = d.File,
					PreviewFile = d.PreviewFile,
					SteamIDOwner = d.SteamIDOwner,
					TimeCreated = d.TimeCreated,
					TimeUpdated = d.TimeUpdated,
					Visibility = d.Visibility,
					Banned = d.Banned,
					Tags = d.Tags,
					TagsTruncated = d.TagsTruncated,
					PchFileName = d.PchFileName,
					FileSize = d.FileSize,
					PreviewFileSize = d.PreviewFileSize,
					URL = d.URL,
					FileType = d.FileType,
					AcceptedForUse = d.AcceptedForUse,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageEnumerateWorkshopFilesResult_t
	{
		public Result Result; // m_eResult enum EResult
		public int ResultsReturned; // m_nResultsReturned int32
		public int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.R4)]
		public float[] GScore; // m_rgScore float [50]
		public uint AppId; // m_nAppId AppId_t
		public uint StartIndex; // m_unStartIndex uint32
		public static RemoteStorageEnumerateWorkshopFilesResult_t FromPointer( IntPtr p ) { return (RemoteStorageEnumerateWorkshopFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateWorkshopFilesResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public int ResultsReturned; // m_nResultsReturned int32
			public int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.R4)]
			public float[] GScore; // m_rgScore float [50]
			public uint AppId; // m_nAppId AppId_t
			public uint StartIndex; // m_unStartIndex uint32
			
			public static implicit operator RemoteStorageEnumerateWorkshopFilesResult_t (  RemoteStorageEnumerateWorkshopFilesResult_t.PackSmall d )
			{
				return new RemoteStorageEnumerateWorkshopFilesResult_t()
				{
					Result = d.Result,
					ResultsReturned = d.ResultsReturned,
					TotalResultCount = d.TotalResultCount,
					GPublishedFileId = d.GPublishedFileId,
					GScore = d.GScore,
					AppId = d.AppId,
					StartIndex = d.StartIndex,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageGetPublishedItemVoteDetailsResult_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_unPublishedFileId PublishedFileId_t
		public int VotesFor; // m_nVotesFor int32
		public int VotesAgainst; // m_nVotesAgainst int32
		public int Reports; // m_nReports int32
		public float FScore; // m_fScore float
		public static RemoteStorageGetPublishedItemVoteDetailsResult_t FromPointer( IntPtr p ) { return (RemoteStorageGetPublishedItemVoteDetailsResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageGetPublishedItemVoteDetailsResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_unPublishedFileId PublishedFileId_t
			public int VotesFor; // m_nVotesFor int32
			public int VotesAgainst; // m_nVotesAgainst int32
			public int Reports; // m_nReports int32
			public float FScore; // m_fScore float
			
			public static implicit operator RemoteStorageGetPublishedItemVoteDetailsResult_t (  RemoteStorageGetPublishedItemVoteDetailsResult_t.PackSmall d )
			{
				return new RemoteStorageGetPublishedItemVoteDetailsResult_t()
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					VotesFor = d.VotesFor,
					VotesAgainst = d.VotesAgainst,
					Reports = d.Reports,
					FScore = d.FScore,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStoragePublishedFileSubscribed_t
	{
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public uint AppID; // m_nAppID AppId_t
		public static RemoteStoragePublishedFileSubscribed_t FromPointer( IntPtr p ) { return (RemoteStoragePublishedFileSubscribed_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileSubscribed_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public uint AppID; // m_nAppID AppId_t
			
			public static implicit operator RemoteStoragePublishedFileSubscribed_t (  RemoteStoragePublishedFileSubscribed_t.PackSmall d )
			{
				return new RemoteStoragePublishedFileSubscribed_t()
				{
					PublishedFileId = d.PublishedFileId,
					AppID = d.AppID,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStoragePublishedFileUnsubscribed_t
	{
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public uint AppID; // m_nAppID AppId_t
		public static RemoteStoragePublishedFileUnsubscribed_t FromPointer( IntPtr p ) { return (RemoteStoragePublishedFileUnsubscribed_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileUnsubscribed_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public uint AppID; // m_nAppID AppId_t
			
			public static implicit operator RemoteStoragePublishedFileUnsubscribed_t (  RemoteStoragePublishedFileUnsubscribed_t.PackSmall d )
			{
				return new RemoteStoragePublishedFileUnsubscribed_t()
				{
					PublishedFileId = d.PublishedFileId,
					AppID = d.AppID,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStoragePublishedFileDeleted_t
	{
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public uint AppID; // m_nAppID AppId_t
		public static RemoteStoragePublishedFileDeleted_t FromPointer( IntPtr p ) { return (RemoteStoragePublishedFileDeleted_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileDeleted_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public uint AppID; // m_nAppID AppId_t
			
			public static implicit operator RemoteStoragePublishedFileDeleted_t (  RemoteStoragePublishedFileDeleted_t.PackSmall d )
			{
				return new RemoteStoragePublishedFileDeleted_t()
				{
					PublishedFileId = d.PublishedFileId,
					AppID = d.AppID,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageUpdateUserPublishedItemVoteResult_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public static RemoteStorageUpdateUserPublishedItemVoteResult_t FromPointer( IntPtr p ) { return (RemoteStorageUpdateUserPublishedItemVoteResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUpdateUserPublishedItemVoteResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoteStorageUpdateUserPublishedItemVoteResult_t (  RemoteStorageUpdateUserPublishedItemVoteResult_t.PackSmall d )
			{
				return new RemoteStorageUpdateUserPublishedItemVoteResult_t()
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageUserVoteDetails_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public WorkshopVote Vote; // m_eVote enum EWorkshopVote
		public static RemoteStorageUserVoteDetails_t FromPointer( IntPtr p ) { return (RemoteStorageUserVoteDetails_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUserVoteDetails_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public WorkshopVote Vote; // m_eVote enum EWorkshopVote
			
			public static implicit operator RemoteStorageUserVoteDetails_t (  RemoteStorageUserVoteDetails_t.PackSmall d )
			{
				return new RemoteStorageUserVoteDetails_t()
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					Vote = d.Vote,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t
	{
		public Result Result; // m_eResult enum EResult
		public int ResultsReturned; // m_nResultsReturned int32
		public int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		public static RemoteStorageEnumerateUserSharedWorkshopFilesResult_t FromPointer( IntPtr p ) { return (RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public int ResultsReturned; // m_nResultsReturned int32
			public int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			
			public static implicit operator RemoteStorageEnumerateUserSharedWorkshopFilesResult_t (  RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.PackSmall d )
			{
				return new RemoteStorageEnumerateUserSharedWorkshopFilesResult_t()
				{
					Result = d.Result,
					ResultsReturned = d.ResultsReturned,
					TotalResultCount = d.TotalResultCount,
					GPublishedFileId = d.GPublishedFileId,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageSetUserPublishedFileActionResult_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
		public static RemoteStorageSetUserPublishedFileActionResult_t FromPointer( IntPtr p ) { return (RemoteStorageSetUserPublishedFileActionResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageSetUserPublishedFileActionResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
			
			public static implicit operator RemoteStorageSetUserPublishedFileActionResult_t (  RemoteStorageSetUserPublishedFileActionResult_t.PackSmall d )
			{
				return new RemoteStorageSetUserPublishedFileActionResult_t()
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					Action = d.Action,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageEnumeratePublishedFilesByUserActionResult_t
	{
		public Result Result; // m_eResult enum EResult
		public WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
		public int ResultsReturned; // m_nResultsReturned int32
		public int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public uint[] GRTimeUpdated; // m_rgRTimeUpdated uint32 [50]
		public static RemoteStorageEnumeratePublishedFilesByUserActionResult_t FromPointer( IntPtr p ) { return (RemoteStorageEnumeratePublishedFilesByUserActionResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumeratePublishedFilesByUserActionResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
			public int ResultsReturned; // m_nResultsReturned int32
			public int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public uint[] GRTimeUpdated; // m_rgRTimeUpdated uint32 [50]
			
			public static implicit operator RemoteStorageEnumeratePublishedFilesByUserActionResult_t (  RemoteStorageEnumeratePublishedFilesByUserActionResult_t.PackSmall d )
			{
				return new RemoteStorageEnumeratePublishedFilesByUserActionResult_t()
				{
					Result = d.Result,
					Action = d.Action,
					ResultsReturned = d.ResultsReturned,
					TotalResultCount = d.TotalResultCount,
					GPublishedFileId = d.GPublishedFileId,
					GRTimeUpdated = d.GRTimeUpdated,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStoragePublishFileProgress_t
	{
		public double DPercentFile; // m_dPercentFile double
		[MarshalAs(UnmanagedType.I1)]
		public bool Preview; // m_bPreview _Bool
		public static RemoteStoragePublishFileProgress_t FromPointer( IntPtr p ) { return (RemoteStoragePublishFileProgress_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishFileProgress_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public double DPercentFile; // m_dPercentFile double
			[MarshalAs(UnmanagedType.I1)]
			public bool Preview; // m_bPreview _Bool
			
			public static implicit operator RemoteStoragePublishFileProgress_t (  RemoteStoragePublishFileProgress_t.PackSmall d )
			{
				return new RemoteStoragePublishFileProgress_t()
				{
					DPercentFile = d.DPercentFile,
					Preview = d.Preview,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStoragePublishedFileUpdated_t
	{
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public uint AppID; // m_nAppID AppId_t
		public ulong Unused; // m_ulUnused uint64
		public static RemoteStoragePublishedFileUpdated_t FromPointer( IntPtr p ) { return (RemoteStoragePublishedFileUpdated_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileUpdated_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public uint AppID; // m_nAppID AppId_t
			public ulong Unused; // m_ulUnused uint64
			
			public static implicit operator RemoteStoragePublishedFileUpdated_t (  RemoteStoragePublishedFileUpdated_t.PackSmall d )
			{
				return new RemoteStoragePublishedFileUpdated_t()
				{
					PublishedFileId = d.PublishedFileId,
					AppID = d.AppID,
					Unused = d.Unused,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageFileWriteAsyncComplete_t
	{
		public Result Result; // m_eResult enum EResult
		public static RemoteStorageFileWriteAsyncComplete_t FromPointer( IntPtr p ) { return (RemoteStorageFileWriteAsyncComplete_t) Marshal.PtrToStructure( p, typeof(RemoteStorageFileWriteAsyncComplete_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			
			public static implicit operator RemoteStorageFileWriteAsyncComplete_t (  RemoteStorageFileWriteAsyncComplete_t.PackSmall d )
			{
				return new RemoteStorageFileWriteAsyncComplete_t()
				{
					Result = d.Result,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageFileReadAsyncComplete_t
	{
		public ulong FileReadAsync; // m_hFileReadAsync SteamAPICall_t
		public Result Result; // m_eResult enum EResult
		public uint Offset; // m_nOffset uint32
		public uint Read; // m_cubRead uint32
		public static RemoteStorageFileReadAsyncComplete_t FromPointer( IntPtr p ) { return (RemoteStorageFileReadAsyncComplete_t) Marshal.PtrToStructure( p, typeof(RemoteStorageFileReadAsyncComplete_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong FileReadAsync; // m_hFileReadAsync SteamAPICall_t
			public Result Result; // m_eResult enum EResult
			public uint Offset; // m_nOffset uint32
			public uint Read; // m_cubRead uint32
			
			public static implicit operator RemoteStorageFileReadAsyncComplete_t (  RemoteStorageFileReadAsyncComplete_t.PackSmall d )
			{
				return new RemoteStorageFileReadAsyncComplete_t()
				{
					FileReadAsync = d.FileReadAsync,
					Result = d.Result,
					Offset = d.Offset,
					Read = d.Read,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct LeaderboardEntry_t
	{
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		public int GlobalRank; // m_nGlobalRank int32
		public int Score; // m_nScore int32
		public int CDetails; // m_cDetails int32
		public ulong UGC; // m_hUGC UGCHandle_t
		public static LeaderboardEntry_t FromPointer( IntPtr p ) { return (LeaderboardEntry_t) Marshal.PtrToStructure( p, typeof(LeaderboardEntry_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			public int GlobalRank; // m_nGlobalRank int32
			public int Score; // m_nScore int32
			public int CDetails; // m_cDetails int32
			public ulong UGC; // m_hUGC UGCHandle_t
			
			public static implicit operator LeaderboardEntry_t (  LeaderboardEntry_t.PackSmall d )
			{
				return new LeaderboardEntry_t()
				{
					SteamIDUser = d.SteamIDUser,
					GlobalRank = d.GlobalRank,
					Score = d.Score,
					CDetails = d.CDetails,
					UGC = d.UGC,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct UserStatsReceived_t
	{
		public ulong GameID; // m_nGameID uint64
		public Result Result; // m_eResult enum EResult
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		public static UserStatsReceived_t FromPointer( IntPtr p ) { return (UserStatsReceived_t) Marshal.PtrToStructure( p, typeof(UserStatsReceived_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong GameID; // m_nGameID uint64
			public Result Result; // m_eResult enum EResult
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator UserStatsReceived_t (  UserStatsReceived_t.PackSmall d )
			{
				return new UserStatsReceived_t()
				{
					GameID = d.GameID,
					Result = d.Result,
					SteamIDUser = d.SteamIDUser,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct UserStatsStored_t
	{
		public ulong GameID; // m_nGameID uint64
		public Result Result; // m_eResult enum EResult
		public static UserStatsStored_t FromPointer( IntPtr p ) { return (UserStatsStored_t) Marshal.PtrToStructure( p, typeof(UserStatsStored_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong GameID; // m_nGameID uint64
			public Result Result; // m_eResult enum EResult
			
			public static implicit operator UserStatsStored_t (  UserStatsStored_t.PackSmall d )
			{
				return new UserStatsStored_t()
				{
					GameID = d.GameID,
					Result = d.Result,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct UserAchievementStored_t
	{
		public ulong GameID; // m_nGameID uint64
		[MarshalAs(UnmanagedType.I1)]
		public bool GroupAchievement; // m_bGroupAchievement _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string AchievementName; // m_rgchAchievementName char [128]
		public uint CurProgress; // m_nCurProgress uint32
		public uint MaxProgress; // m_nMaxProgress uint32
		public static UserAchievementStored_t FromPointer( IntPtr p ) { return (UserAchievementStored_t) Marshal.PtrToStructure( p, typeof(UserAchievementStored_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong GameID; // m_nGameID uint64
			[MarshalAs(UnmanagedType.I1)]
			public bool GroupAchievement; // m_bGroupAchievement _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string AchievementName; // m_rgchAchievementName char [128]
			public uint CurProgress; // m_nCurProgress uint32
			public uint MaxProgress; // m_nMaxProgress uint32
			
			public static implicit operator UserAchievementStored_t (  UserAchievementStored_t.PackSmall d )
			{
				return new UserAchievementStored_t()
				{
					GameID = d.GameID,
					GroupAchievement = d.GroupAchievement,
					AchievementName = d.AchievementName,
					CurProgress = d.CurProgress,
					MaxProgress = d.MaxProgress,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LeaderboardFindResult_t
	{
		public ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		public byte LeaderboardFound; // m_bLeaderboardFound uint8
		public static LeaderboardFindResult_t FromPointer( IntPtr p ) { return (LeaderboardFindResult_t) Marshal.PtrToStructure( p, typeof(LeaderboardFindResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
			public byte LeaderboardFound; // m_bLeaderboardFound uint8
			
			public static implicit operator LeaderboardFindResult_t (  LeaderboardFindResult_t.PackSmall d )
			{
				return new LeaderboardFindResult_t()
				{
					SteamLeaderboard = d.SteamLeaderboard,
					LeaderboardFound = d.LeaderboardFound,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LeaderboardScoresDownloaded_t
	{
		public ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		public ulong SteamLeaderboardEntries; // m_hSteamLeaderboardEntries SteamLeaderboardEntries_t
		public int CEntryCount; // m_cEntryCount int
		public static LeaderboardScoresDownloaded_t FromPointer( IntPtr p ) { return (LeaderboardScoresDownloaded_t) Marshal.PtrToStructure( p, typeof(LeaderboardScoresDownloaded_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
			public ulong SteamLeaderboardEntries; // m_hSteamLeaderboardEntries SteamLeaderboardEntries_t
			public int CEntryCount; // m_cEntryCount int
			
			public static implicit operator LeaderboardScoresDownloaded_t (  LeaderboardScoresDownloaded_t.PackSmall d )
			{
				return new LeaderboardScoresDownloaded_t()
				{
					SteamLeaderboard = d.SteamLeaderboard,
					SteamLeaderboardEntries = d.SteamLeaderboardEntries,
					CEntryCount = d.CEntryCount,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LeaderboardScoreUploaded_t
	{
		public byte Success; // m_bSuccess uint8
		public ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		public int Score; // m_nScore int32
		public byte ScoreChanged; // m_bScoreChanged uint8
		public int GlobalRankNew; // m_nGlobalRankNew int
		public int GlobalRankPrevious; // m_nGlobalRankPrevious int
		public static LeaderboardScoreUploaded_t FromPointer( IntPtr p ) { return (LeaderboardScoreUploaded_t) Marshal.PtrToStructure( p, typeof(LeaderboardScoreUploaded_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public byte Success; // m_bSuccess uint8
			public ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
			public int Score; // m_nScore int32
			public byte ScoreChanged; // m_bScoreChanged uint8
			public int GlobalRankNew; // m_nGlobalRankNew int
			public int GlobalRankPrevious; // m_nGlobalRankPrevious int
			
			public static implicit operator LeaderboardScoreUploaded_t (  LeaderboardScoreUploaded_t.PackSmall d )
			{
				return new LeaderboardScoreUploaded_t()
				{
					Success = d.Success,
					SteamLeaderboard = d.SteamLeaderboard,
					Score = d.Score,
					ScoreChanged = d.ScoreChanged,
					GlobalRankNew = d.GlobalRankNew,
					GlobalRankPrevious = d.GlobalRankPrevious,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct NumberOfCurrentPlayers_t
	{
		public byte Success; // m_bSuccess uint8
		public int CPlayers; // m_cPlayers int32
		public static NumberOfCurrentPlayers_t FromPointer( IntPtr p ) { return (NumberOfCurrentPlayers_t) Marshal.PtrToStructure( p, typeof(NumberOfCurrentPlayers_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public byte Success; // m_bSuccess uint8
			public int CPlayers; // m_cPlayers int32
			
			public static implicit operator NumberOfCurrentPlayers_t (  NumberOfCurrentPlayers_t.PackSmall d )
			{
				return new NumberOfCurrentPlayers_t()
				{
					Success = d.Success,
					CPlayers = d.CPlayers,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct UserStatsUnloaded_t
	{
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		public static UserStatsUnloaded_t FromPointer( IntPtr p ) { return (UserStatsUnloaded_t) Marshal.PtrToStructure( p, typeof(UserStatsUnloaded_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator UserStatsUnloaded_t (  UserStatsUnloaded_t.PackSmall d )
			{
				return new UserStatsUnloaded_t()
				{
					SteamIDUser = d.SteamIDUser,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct UserAchievementIconFetched_t
	{
		public ulong GameID; // m_nGameID class CGameID
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string AchievementName; // m_rgchAchievementName char [128]
		[MarshalAs(UnmanagedType.I1)]
		public bool Achieved; // m_bAchieved _Bool
		public int IconHandle; // m_nIconHandle int
		public static UserAchievementIconFetched_t FromPointer( IntPtr p ) { return (UserAchievementIconFetched_t) Marshal.PtrToStructure( p, typeof(UserAchievementIconFetched_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong GameID; // m_nGameID class CGameID
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string AchievementName; // m_rgchAchievementName char [128]
			[MarshalAs(UnmanagedType.I1)]
			public bool Achieved; // m_bAchieved _Bool
			public int IconHandle; // m_nIconHandle int
			
			public static implicit operator UserAchievementIconFetched_t (  UserAchievementIconFetched_t.PackSmall d )
			{
				return new UserAchievementIconFetched_t()
				{
					GameID = d.GameID,
					AchievementName = d.AchievementName,
					Achieved = d.Achieved,
					IconHandle = d.IconHandle,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GlobalAchievementPercentagesReady_t
	{
		public ulong GameID; // m_nGameID uint64
		public Result Result; // m_eResult enum EResult
		public static GlobalAchievementPercentagesReady_t FromPointer( IntPtr p ) { return (GlobalAchievementPercentagesReady_t) Marshal.PtrToStructure( p, typeof(GlobalAchievementPercentagesReady_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong GameID; // m_nGameID uint64
			public Result Result; // m_eResult enum EResult
			
			public static implicit operator GlobalAchievementPercentagesReady_t (  GlobalAchievementPercentagesReady_t.PackSmall d )
			{
				return new GlobalAchievementPercentagesReady_t()
				{
					GameID = d.GameID,
					Result = d.Result,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LeaderboardUGCSet_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		public static LeaderboardUGCSet_t FromPointer( IntPtr p ) { return (LeaderboardUGCSet_t) Marshal.PtrToStructure( p, typeof(LeaderboardUGCSet_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
			
			public static implicit operator LeaderboardUGCSet_t (  LeaderboardUGCSet_t.PackSmall d )
			{
				return new LeaderboardUGCSet_t()
				{
					Result = d.Result,
					SteamLeaderboard = d.SteamLeaderboard,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct PS3TrophiesInstalled_t
	{
		public ulong GameID; // m_nGameID uint64
		public Result Result; // m_eResult enum EResult
		public ulong RequiredDiskSpace; // m_ulRequiredDiskSpace uint64
		public static PS3TrophiesInstalled_t FromPointer( IntPtr p ) { return (PS3TrophiesInstalled_t) Marshal.PtrToStructure( p, typeof(PS3TrophiesInstalled_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong GameID; // m_nGameID uint64
			public Result Result; // m_eResult enum EResult
			public ulong RequiredDiskSpace; // m_ulRequiredDiskSpace uint64
			
			public static implicit operator PS3TrophiesInstalled_t (  PS3TrophiesInstalled_t.PackSmall d )
			{
				return new PS3TrophiesInstalled_t()
				{
					GameID = d.GameID,
					Result = d.Result,
					RequiredDiskSpace = d.RequiredDiskSpace,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GlobalStatsReceived_t
	{
		public ulong GameID; // m_nGameID uint64
		public Result Result; // m_eResult enum EResult
		public static GlobalStatsReceived_t FromPointer( IntPtr p ) { return (GlobalStatsReceived_t) Marshal.PtrToStructure( p, typeof(GlobalStatsReceived_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong GameID; // m_nGameID uint64
			public Result Result; // m_eResult enum EResult
			
			public static implicit operator GlobalStatsReceived_t (  GlobalStatsReceived_t.PackSmall d )
			{
				return new GlobalStatsReceived_t()
				{
					GameID = d.GameID,
					Result = d.Result,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct DlcInstalled_t
	{
		public uint AppID; // m_nAppID AppId_t
		public static DlcInstalled_t FromPointer( IntPtr p ) { return (DlcInstalled_t) Marshal.PtrToStructure( p, typeof(DlcInstalled_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint AppID; // m_nAppID AppId_t
			
			public static implicit operator DlcInstalled_t (  DlcInstalled_t.PackSmall d )
			{
				return new DlcInstalled_t()
				{
					AppID = d.AppID,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RegisterActivationCodeResponse_t
	{
		public RegisterActivationCodeResult Result; // m_eResult enum ERegisterActivationCodeResult
		public uint PackageRegistered; // m_unPackageRegistered uint32
		public static RegisterActivationCodeResponse_t FromPointer( IntPtr p ) { return (RegisterActivationCodeResponse_t) Marshal.PtrToStructure( p, typeof(RegisterActivationCodeResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public RegisterActivationCodeResult Result; // m_eResult enum ERegisterActivationCodeResult
			public uint PackageRegistered; // m_unPackageRegistered uint32
			
			public static implicit operator RegisterActivationCodeResponse_t (  RegisterActivationCodeResponse_t.PackSmall d )
			{
				return new RegisterActivationCodeResponse_t()
				{
					Result = d.Result,
					PackageRegistered = d.PackageRegistered,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct AppProofOfPurchaseKeyResponse_t
	{
		public Result Result; // m_eResult enum EResult
		public uint AppID; // m_nAppID uint32
		public uint CchKeyLength; // m_cchKeyLength uint32
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 240)]
		public string Key; // m_rgchKey char [240]
		public static AppProofOfPurchaseKeyResponse_t FromPointer( IntPtr p ) { return (AppProofOfPurchaseKeyResponse_t) Marshal.PtrToStructure( p, typeof(AppProofOfPurchaseKeyResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public uint AppID; // m_nAppID uint32
			public uint CchKeyLength; // m_cchKeyLength uint32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 240)]
			public string Key; // m_rgchKey char [240]
			
			public static implicit operator AppProofOfPurchaseKeyResponse_t (  AppProofOfPurchaseKeyResponse_t.PackSmall d )
			{
				return new AppProofOfPurchaseKeyResponse_t()
				{
					Result = d.Result,
					AppID = d.AppID,
					CchKeyLength = d.CchKeyLength,
					Key = d.Key,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct FileDetailsResult_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong FileSize; // m_ulFileSize uint64
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
		public char FileSHA; // m_FileSHA uint8 [20]
		public uint Flags; // m_unFlags uint32
		public static FileDetailsResult_t FromPointer( IntPtr p ) { return (FileDetailsResult_t) Marshal.PtrToStructure( p, typeof(FileDetailsResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong FileSize; // m_ulFileSize uint64
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
			public char FileSHA; // m_FileSHA uint8 [20]
			public uint Flags; // m_unFlags uint32
			
			public static implicit operator FileDetailsResult_t (  FileDetailsResult_t.PackSmall d )
			{
				return new FileDetailsResult_t()
				{
					Result = d.Result,
					FileSize = d.FileSize,
					FileSHA = d.FileSHA,
					Flags = d.Flags,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct P2PSessionState_t
	{
		public byte ConnectionActive; // m_bConnectionActive uint8
		public byte Connecting; // m_bConnecting uint8
		public byte P2PSessionError; // m_eP2PSessionError uint8
		public byte UsingRelay; // m_bUsingRelay uint8
		public int BytesQueuedForSend; // m_nBytesQueuedForSend int32
		public int PacketsQueuedForSend; // m_nPacketsQueuedForSend int32
		public uint RemoteIP; // m_nRemoteIP uint32
		public ushort RemotePort; // m_nRemotePort uint16
		public static P2PSessionState_t FromPointer( IntPtr p ) { return (P2PSessionState_t) Marshal.PtrToStructure( p, typeof(P2PSessionState_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public byte ConnectionActive; // m_bConnectionActive uint8
			public byte Connecting; // m_bConnecting uint8
			public byte P2PSessionError; // m_eP2PSessionError uint8
			public byte UsingRelay; // m_bUsingRelay uint8
			public int BytesQueuedForSend; // m_nBytesQueuedForSend int32
			public int PacketsQueuedForSend; // m_nPacketsQueuedForSend int32
			public uint RemoteIP; // m_nRemoteIP uint32
			public ushort RemotePort; // m_nRemotePort uint16
			
			public static implicit operator P2PSessionState_t (  P2PSessionState_t.PackSmall d )
			{
				return new P2PSessionState_t()
				{
					ConnectionActive = d.ConnectionActive,
					Connecting = d.Connecting,
					P2PSessionError = d.P2PSessionError,
					UsingRelay = d.UsingRelay,
					BytesQueuedForSend = d.BytesQueuedForSend,
					PacketsQueuedForSend = d.PacketsQueuedForSend,
					RemoteIP = d.RemoteIP,
					RemotePort = d.RemotePort,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct P2PSessionRequest_t
	{
		public ulong SteamIDRemote; // m_steamIDRemote class CSteamID
		public static P2PSessionRequest_t FromPointer( IntPtr p ) { return (P2PSessionRequest_t) Marshal.PtrToStructure( p, typeof(P2PSessionRequest_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDRemote; // m_steamIDRemote class CSteamID
			
			public static implicit operator P2PSessionRequest_t (  P2PSessionRequest_t.PackSmall d )
			{
				return new P2PSessionRequest_t()
				{
					SteamIDRemote = d.SteamIDRemote,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct P2PSessionConnectFail_t
	{
		public ulong SteamIDRemote; // m_steamIDRemote class CSteamID
		public byte P2PSessionError; // m_eP2PSessionError uint8
		public static P2PSessionConnectFail_t FromPointer( IntPtr p ) { return (P2PSessionConnectFail_t) Marshal.PtrToStructure( p, typeof(P2PSessionConnectFail_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDRemote; // m_steamIDRemote class CSteamID
			public byte P2PSessionError; // m_eP2PSessionError uint8
			
			public static implicit operator P2PSessionConnectFail_t (  P2PSessionConnectFail_t.PackSmall d )
			{
				return new P2PSessionConnectFail_t()
				{
					SteamIDRemote = d.SteamIDRemote,
					P2PSessionError = d.P2PSessionError,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct SocketStatusCallback_t
	{
		public uint Socket; // m_hSocket SNetSocket_t
		public uint ListenSocket; // m_hListenSocket SNetListenSocket_t
		public ulong SteamIDRemote; // m_steamIDRemote class CSteamID
		public int SNetSocketState; // m_eSNetSocketState int
		public static SocketStatusCallback_t FromPointer( IntPtr p ) { return (SocketStatusCallback_t) Marshal.PtrToStructure( p, typeof(SocketStatusCallback_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint Socket; // m_hSocket SNetSocket_t
			public uint ListenSocket; // m_hListenSocket SNetListenSocket_t
			public ulong SteamIDRemote; // m_steamIDRemote class CSteamID
			public int SNetSocketState; // m_eSNetSocketState int
			
			public static implicit operator SocketStatusCallback_t (  SocketStatusCallback_t.PackSmall d )
			{
				return new SocketStatusCallback_t()
				{
					Socket = d.Socket,
					ListenSocket = d.ListenSocket,
					SteamIDRemote = d.SteamIDRemote,
					SNetSocketState = d.SNetSocketState,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct ScreenshotReady_t
	{
		public uint Local; // m_hLocal ScreenshotHandle
		public Result Result; // m_eResult enum EResult
		public static ScreenshotReady_t FromPointer( IntPtr p ) { return (ScreenshotReady_t) Marshal.PtrToStructure( p, typeof(ScreenshotReady_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint Local; // m_hLocal ScreenshotHandle
			public Result Result; // m_eResult enum EResult
			
			public static implicit operator ScreenshotReady_t (  ScreenshotReady_t.PackSmall d )
			{
				return new ScreenshotReady_t()
				{
					Local = d.Local,
					Result = d.Result,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct VolumeHasChanged_t
	{
		public float NewVolume; // m_flNewVolume float
		public static VolumeHasChanged_t FromPointer( IntPtr p ) { return (VolumeHasChanged_t) Marshal.PtrToStructure( p, typeof(VolumeHasChanged_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public float NewVolume; // m_flNewVolume float
			
			public static implicit operator VolumeHasChanged_t (  VolumeHasChanged_t.PackSmall d )
			{
				return new VolumeHasChanged_t()
				{
					NewVolume = d.NewVolume,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct MusicPlayerWantsShuffled_t
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool Shuffled; // m_bShuffled _Bool
		public static MusicPlayerWantsShuffled_t FromPointer( IntPtr p ) { return (MusicPlayerWantsShuffled_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsShuffled_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool Shuffled; // m_bShuffled _Bool
			
			public static implicit operator MusicPlayerWantsShuffled_t (  MusicPlayerWantsShuffled_t.PackSmall d )
			{
				return new MusicPlayerWantsShuffled_t()
				{
					Shuffled = d.Shuffled,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct MusicPlayerWantsLooped_t
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool Looped; // m_bLooped _Bool
		public static MusicPlayerWantsLooped_t FromPointer( IntPtr p ) { return (MusicPlayerWantsLooped_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsLooped_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool Looped; // m_bLooped _Bool
			
			public static implicit operator MusicPlayerWantsLooped_t (  MusicPlayerWantsLooped_t.PackSmall d )
			{
				return new MusicPlayerWantsLooped_t()
				{
					Looped = d.Looped,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct MusicPlayerWantsVolume_t
	{
		public float NewVolume; // m_flNewVolume float
		public static MusicPlayerWantsVolume_t FromPointer( IntPtr p ) { return (MusicPlayerWantsVolume_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsVolume_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public float NewVolume; // m_flNewVolume float
			
			public static implicit operator MusicPlayerWantsVolume_t (  MusicPlayerWantsVolume_t.PackSmall d )
			{
				return new MusicPlayerWantsVolume_t()
				{
					NewVolume = d.NewVolume,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct MusicPlayerSelectsQueueEntry_t
	{
		public int NID; // nID int
		public static MusicPlayerSelectsQueueEntry_t FromPointer( IntPtr p ) { return (MusicPlayerSelectsQueueEntry_t) Marshal.PtrToStructure( p, typeof(MusicPlayerSelectsQueueEntry_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public int NID; // nID int
			
			public static implicit operator MusicPlayerSelectsQueueEntry_t (  MusicPlayerSelectsQueueEntry_t.PackSmall d )
			{
				return new MusicPlayerSelectsQueueEntry_t()
				{
					NID = d.NID,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct MusicPlayerSelectsPlaylistEntry_t
	{
		public int NID; // nID int
		public static MusicPlayerSelectsPlaylistEntry_t FromPointer( IntPtr p ) { return (MusicPlayerSelectsPlaylistEntry_t) Marshal.PtrToStructure( p, typeof(MusicPlayerSelectsPlaylistEntry_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public int NID; // nID int
			
			public static implicit operator MusicPlayerSelectsPlaylistEntry_t (  MusicPlayerSelectsPlaylistEntry_t.PackSmall d )
			{
				return new MusicPlayerSelectsPlaylistEntry_t()
				{
					NID = d.NID,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct MusicPlayerWantsPlayingRepeatStatus_t
	{
		public int PlayingRepeatStatus; // m_nPlayingRepeatStatus int
		public static MusicPlayerWantsPlayingRepeatStatus_t FromPointer( IntPtr p ) { return (MusicPlayerWantsPlayingRepeatStatus_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsPlayingRepeatStatus_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public int PlayingRepeatStatus; // m_nPlayingRepeatStatus int
			
			public static implicit operator MusicPlayerWantsPlayingRepeatStatus_t (  MusicPlayerWantsPlayingRepeatStatus_t.PackSmall d )
			{
				return new MusicPlayerWantsPlayingRepeatStatus_t()
				{
					PlayingRepeatStatus = d.PlayingRepeatStatus,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTTPRequestCompleted_t
	{
		public uint Request; // m_hRequest HTTPRequestHandle
		public ulong ContextValue; // m_ulContextValue uint64
		[MarshalAs(UnmanagedType.I1)]
		public bool RequestSuccessful; // m_bRequestSuccessful _Bool
		public HTTPStatusCode StatusCode; // m_eStatusCode enum EHTTPStatusCode
		public uint BodySize; // m_unBodySize uint32
		public static HTTPRequestCompleted_t FromPointer( IntPtr p ) { return (HTTPRequestCompleted_t) Marshal.PtrToStructure( p, typeof(HTTPRequestCompleted_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint Request; // m_hRequest HTTPRequestHandle
			public ulong ContextValue; // m_ulContextValue uint64
			[MarshalAs(UnmanagedType.I1)]
			public bool RequestSuccessful; // m_bRequestSuccessful _Bool
			public HTTPStatusCode StatusCode; // m_eStatusCode enum EHTTPStatusCode
			public uint BodySize; // m_unBodySize uint32
			
			public static implicit operator HTTPRequestCompleted_t (  HTTPRequestCompleted_t.PackSmall d )
			{
				return new HTTPRequestCompleted_t()
				{
					Request = d.Request,
					ContextValue = d.ContextValue,
					RequestSuccessful = d.RequestSuccessful,
					StatusCode = d.StatusCode,
					BodySize = d.BodySize,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTTPRequestHeadersReceived_t
	{
		public uint Request; // m_hRequest HTTPRequestHandle
		public ulong ContextValue; // m_ulContextValue uint64
		public static HTTPRequestHeadersReceived_t FromPointer( IntPtr p ) { return (HTTPRequestHeadersReceived_t) Marshal.PtrToStructure( p, typeof(HTTPRequestHeadersReceived_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint Request; // m_hRequest HTTPRequestHandle
			public ulong ContextValue; // m_ulContextValue uint64
			
			public static implicit operator HTTPRequestHeadersReceived_t (  HTTPRequestHeadersReceived_t.PackSmall d )
			{
				return new HTTPRequestHeadersReceived_t()
				{
					Request = d.Request,
					ContextValue = d.ContextValue,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTTPRequestDataReceived_t
	{
		public uint Request; // m_hRequest HTTPRequestHandle
		public ulong ContextValue; // m_ulContextValue uint64
		public uint COffset; // m_cOffset uint32
		public uint CBytesReceived; // m_cBytesReceived uint32
		public static HTTPRequestDataReceived_t FromPointer( IntPtr p ) { return (HTTPRequestDataReceived_t) Marshal.PtrToStructure( p, typeof(HTTPRequestDataReceived_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint Request; // m_hRequest HTTPRequestHandle
			public ulong ContextValue; // m_ulContextValue uint64
			public uint COffset; // m_cOffset uint32
			public uint CBytesReceived; // m_cBytesReceived uint32
			
			public static implicit operator HTTPRequestDataReceived_t (  HTTPRequestDataReceived_t.PackSmall d )
			{
				return new HTTPRequestDataReceived_t()
				{
					Request = d.Request,
					ContextValue = d.ContextValue,
					COffset = d.COffset,
					CBytesReceived = d.CBytesReceived,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamUnifiedMessagesSendMethodResult_t
	{
		public ulong Handle; // m_hHandle ClientUnifiedMessageHandle
		public ulong Context; // m_unContext uint64
		public Result Result; // m_eResult enum EResult
		public uint ResponseSize; // m_unResponseSize uint32
		public static SteamUnifiedMessagesSendMethodResult_t FromPointer( IntPtr p ) { return (SteamUnifiedMessagesSendMethodResult_t) Marshal.PtrToStructure( p, typeof(SteamUnifiedMessagesSendMethodResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong Handle; // m_hHandle ClientUnifiedMessageHandle
			public ulong Context; // m_unContext uint64
			public Result Result; // m_eResult enum EResult
			public uint ResponseSize; // m_unResponseSize uint32
			
			public static implicit operator SteamUnifiedMessagesSendMethodResult_t (  SteamUnifiedMessagesSendMethodResult_t.PackSmall d )
			{
				return new SteamUnifiedMessagesSendMethodResult_t()
				{
					Handle = d.Handle,
					Context = d.Context,
					Result = d.Result,
					ResponseSize = d.ResponseSize,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct ControllerAnalogActionData_t
	{
		public ControllerSourceMode EMode; // eMode enum EControllerSourceMode
		public float X; // x float
		public float Y; // y float
		[MarshalAs(UnmanagedType.I1)]
		public bool BActive; // bActive _Bool
		public static ControllerAnalogActionData_t FromPointer( IntPtr p ) { return (ControllerAnalogActionData_t) Marshal.PtrToStructure( p, typeof(ControllerAnalogActionData_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ControllerSourceMode EMode; // eMode enum EControllerSourceMode
			public float X; // x float
			public float Y; // y float
			[MarshalAs(UnmanagedType.I1)]
			public bool BActive; // bActive _Bool
			
			public static implicit operator ControllerAnalogActionData_t (  ControllerAnalogActionData_t.PackSmall d )
			{
				return new ControllerAnalogActionData_t()
				{
					EMode = d.EMode,
					X = d.X,
					Y = d.Y,
					BActive = d.BActive,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct ControllerDigitalActionData_t
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool BState; // bState _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool BActive; // bActive _Bool
		public static ControllerDigitalActionData_t FromPointer( IntPtr p ) { return (ControllerDigitalActionData_t) Marshal.PtrToStructure( p, typeof(ControllerDigitalActionData_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool BState; // bState _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool BActive; // bActive _Bool
			
			public static implicit operator ControllerDigitalActionData_t (  ControllerDigitalActionData_t.PackSmall d )
			{
				return new ControllerDigitalActionData_t()
				{
					BState = d.BState,
					BActive = d.BActive,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct ControllerMotionData_t
	{
		public float RotQuatX; // rotQuatX float
		public float RotQuatY; // rotQuatY float
		public float RotQuatZ; // rotQuatZ float
		public float RotQuatW; // rotQuatW float
		public float PosAccelX; // posAccelX float
		public float PosAccelY; // posAccelY float
		public float PosAccelZ; // posAccelZ float
		public float RotVelX; // rotVelX float
		public float RotVelY; // rotVelY float
		public float RotVelZ; // rotVelZ float
		public static ControllerMotionData_t FromPointer( IntPtr p ) { return (ControllerMotionData_t) Marshal.PtrToStructure( p, typeof(ControllerMotionData_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public float RotQuatX; // rotQuatX float
			public float RotQuatY; // rotQuatY float
			public float RotQuatZ; // rotQuatZ float
			public float RotQuatW; // rotQuatW float
			public float PosAccelX; // posAccelX float
			public float PosAccelY; // posAccelY float
			public float PosAccelZ; // posAccelZ float
			public float RotVelX; // rotVelX float
			public float RotVelY; // rotVelY float
			public float RotVelZ; // rotVelZ float
			
			public static implicit operator ControllerMotionData_t (  ControllerMotionData_t.PackSmall d )
			{
				return new ControllerMotionData_t()
				{
					RotQuatX = d.RotQuatX,
					RotQuatY = d.RotQuatY,
					RotQuatZ = d.RotQuatZ,
					RotQuatW = d.RotQuatW,
					PosAccelX = d.PosAccelX,
					PosAccelY = d.PosAccelY,
					PosAccelZ = d.PosAccelZ,
					RotVelX = d.RotVelX,
					RotVelY = d.RotVelY,
					RotVelZ = d.RotVelZ,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamUGCDetails_t
	{
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public Result Result; // m_eResult enum EResult
		public WorkshopFileType FileType; // m_eFileType enum EWorkshopFileType
		public uint CreatorAppID; // m_nCreatorAppID AppId_t
		public uint ConsumerAppID; // m_nConsumerAppID AppId_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
		public string Title; // m_rgchTitle char [129]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
		public string Description; // m_rgchDescription char [8000]
		public ulong SteamIDOwner; // m_ulSteamIDOwner uint64
		public uint TimeCreated; // m_rtimeCreated uint32
		public uint TimeUpdated; // m_rtimeUpdated uint32
		public uint TimeAddedToUserList; // m_rtimeAddedToUserList uint32
		public RemoteStoragePublishedFileVisibility Visibility; // m_eVisibility enum ERemoteStoragePublishedFileVisibility
		[MarshalAs(UnmanagedType.I1)]
		public bool Banned; // m_bBanned _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool AcceptedForUse; // m_bAcceptedForUse _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool TagsTruncated; // m_bTagsTruncated _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
		public string Tags; // m_rgchTags char [1025]
		public ulong File; // m_hFile UGCHandle_t
		public ulong PreviewFile; // m_hPreviewFile UGCHandle_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string PchFileName; // m_pchFileName char [260]
		public int FileSize; // m_nFileSize int32
		public int PreviewFileSize; // m_nPreviewFileSize int32
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string URL; // m_rgchURL char [256]
		public uint VotesUp; // m_unVotesUp uint32
		public uint VotesDown; // m_unVotesDown uint32
		public float Score; // m_flScore float
		public uint NumChildren; // m_unNumChildren uint32
		public static SteamUGCDetails_t FromPointer( IntPtr p ) { return (SteamUGCDetails_t) Marshal.PtrToStructure( p, typeof(SteamUGCDetails_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public Result Result; // m_eResult enum EResult
			public WorkshopFileType FileType; // m_eFileType enum EWorkshopFileType
			public uint CreatorAppID; // m_nCreatorAppID AppId_t
			public uint ConsumerAppID; // m_nConsumerAppID AppId_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
			public string Title; // m_rgchTitle char [129]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
			public string Description; // m_rgchDescription char [8000]
			public ulong SteamIDOwner; // m_ulSteamIDOwner uint64
			public uint TimeCreated; // m_rtimeCreated uint32
			public uint TimeUpdated; // m_rtimeUpdated uint32
			public uint TimeAddedToUserList; // m_rtimeAddedToUserList uint32
			public RemoteStoragePublishedFileVisibility Visibility; // m_eVisibility enum ERemoteStoragePublishedFileVisibility
			[MarshalAs(UnmanagedType.I1)]
			public bool Banned; // m_bBanned _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool AcceptedForUse; // m_bAcceptedForUse _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool TagsTruncated; // m_bTagsTruncated _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
			public string Tags; // m_rgchTags char [1025]
			public ulong File; // m_hFile UGCHandle_t
			public ulong PreviewFile; // m_hPreviewFile UGCHandle_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string PchFileName; // m_pchFileName char [260]
			public int FileSize; // m_nFileSize int32
			public int PreviewFileSize; // m_nPreviewFileSize int32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string URL; // m_rgchURL char [256]
			public uint VotesUp; // m_unVotesUp uint32
			public uint VotesDown; // m_unVotesDown uint32
			public float Score; // m_flScore float
			public uint NumChildren; // m_unNumChildren uint32
			
			public static implicit operator SteamUGCDetails_t (  SteamUGCDetails_t.PackSmall d )
			{
				return new SteamUGCDetails_t()
				{
					PublishedFileId = d.PublishedFileId,
					Result = d.Result,
					FileType = d.FileType,
					CreatorAppID = d.CreatorAppID,
					ConsumerAppID = d.ConsumerAppID,
					Title = d.Title,
					Description = d.Description,
					SteamIDOwner = d.SteamIDOwner,
					TimeCreated = d.TimeCreated,
					TimeUpdated = d.TimeUpdated,
					TimeAddedToUserList = d.TimeAddedToUserList,
					Visibility = d.Visibility,
					Banned = d.Banned,
					AcceptedForUse = d.AcceptedForUse,
					TagsTruncated = d.TagsTruncated,
					Tags = d.Tags,
					File = d.File,
					PreviewFile = d.PreviewFile,
					PchFileName = d.PchFileName,
					FileSize = d.FileSize,
					PreviewFileSize = d.PreviewFileSize,
					URL = d.URL,
					VotesUp = d.VotesUp,
					VotesDown = d.VotesDown,
					Score = d.Score,
					NumChildren = d.NumChildren,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamUGCQueryCompleted_t
	{
		public ulong Andle; // m_handle UGCQueryHandle_t
		public Result Result; // m_eResult enum EResult
		public uint NumResultsReturned; // m_unNumResultsReturned uint32
		public uint TotalMatchingResults; // m_unTotalMatchingResults uint32
		[MarshalAs(UnmanagedType.I1)]
		public bool CachedData; // m_bCachedData _Bool
		public static SteamUGCQueryCompleted_t FromPointer( IntPtr p ) { return (SteamUGCQueryCompleted_t) Marshal.PtrToStructure( p, typeof(SteamUGCQueryCompleted_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong Andle; // m_handle UGCQueryHandle_t
			public Result Result; // m_eResult enum EResult
			public uint NumResultsReturned; // m_unNumResultsReturned uint32
			public uint TotalMatchingResults; // m_unTotalMatchingResults uint32
			[MarshalAs(UnmanagedType.I1)]
			public bool CachedData; // m_bCachedData _Bool
			
			public static implicit operator SteamUGCQueryCompleted_t (  SteamUGCQueryCompleted_t.PackSmall d )
			{
				return new SteamUGCQueryCompleted_t()
				{
					Andle = d.Andle,
					Result = d.Result,
					NumResultsReturned = d.NumResultsReturned,
					TotalMatchingResults = d.TotalMatchingResults,
					CachedData = d.CachedData,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamUGCRequestUGCDetailsResult_t
	{
		public SteamUGCDetails_t Details; // m_details struct SteamUGCDetails_t
		[MarshalAs(UnmanagedType.I1)]
		public bool CachedData; // m_bCachedData _Bool
		public static SteamUGCRequestUGCDetailsResult_t FromPointer( IntPtr p ) { return (SteamUGCRequestUGCDetailsResult_t) Marshal.PtrToStructure( p, typeof(SteamUGCRequestUGCDetailsResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public SteamUGCDetails_t Details; // m_details struct SteamUGCDetails_t
			[MarshalAs(UnmanagedType.I1)]
			public bool CachedData; // m_bCachedData _Bool
			
			public static implicit operator SteamUGCRequestUGCDetailsResult_t (  SteamUGCRequestUGCDetailsResult_t.PackSmall d )
			{
				return new SteamUGCRequestUGCDetailsResult_t()
				{
					Details = d.Details,
					CachedData = d.CachedData,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct CreateItemResult_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		public bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		public static CreateItemResult_t FromPointer( IntPtr p ) { return (CreateItemResult_t) Marshal.PtrToStructure( p, typeof(CreateItemResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			[MarshalAs(UnmanagedType.I1)]
			public bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			
			public static implicit operator CreateItemResult_t (  CreateItemResult_t.PackSmall d )
			{
				return new CreateItemResult_t()
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SubmitItemUpdateResult_t
	{
		public Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		public bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		public static SubmitItemUpdateResult_t FromPointer( IntPtr p ) { return (SubmitItemUpdateResult_t) Marshal.PtrToStructure( p, typeof(SubmitItemUpdateResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			public bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			
			public static implicit operator SubmitItemUpdateResult_t (  SubmitItemUpdateResult_t.PackSmall d )
			{
				return new SubmitItemUpdateResult_t()
				{
					Result = d.Result,
					UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct DownloadItemResult_t
	{
		public uint AppID; // m_unAppID AppId_t
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public Result Result; // m_eResult enum EResult
		public static DownloadItemResult_t FromPointer( IntPtr p ) { return (DownloadItemResult_t) Marshal.PtrToStructure( p, typeof(DownloadItemResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint AppID; // m_unAppID AppId_t
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public Result Result; // m_eResult enum EResult
			
			public static implicit operator DownloadItemResult_t (  DownloadItemResult_t.PackSmall d )
			{
				return new DownloadItemResult_t()
				{
					AppID = d.AppID,
					PublishedFileId = d.PublishedFileId,
					Result = d.Result,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct UserFavoriteItemsListChanged_t
	{
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		public bool WasAddRequest; // m_bWasAddRequest _Bool
		public static UserFavoriteItemsListChanged_t FromPointer( IntPtr p ) { return (UserFavoriteItemsListChanged_t) Marshal.PtrToStructure( p, typeof(UserFavoriteItemsListChanged_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			public bool WasAddRequest; // m_bWasAddRequest _Bool
			
			public static implicit operator UserFavoriteItemsListChanged_t (  UserFavoriteItemsListChanged_t.PackSmall d )
			{
				return new UserFavoriteItemsListChanged_t()
				{
					PublishedFileId = d.PublishedFileId,
					Result = d.Result,
					WasAddRequest = d.WasAddRequest,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SetUserItemVoteResult_t
	{
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		public bool VoteUp; // m_bVoteUp _Bool
		public static SetUserItemVoteResult_t FromPointer( IntPtr p ) { return (SetUserItemVoteResult_t) Marshal.PtrToStructure( p, typeof(SetUserItemVoteResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			public bool VoteUp; // m_bVoteUp _Bool
			
			public static implicit operator SetUserItemVoteResult_t (  SetUserItemVoteResult_t.PackSmall d )
			{
				return new SetUserItemVoteResult_t()
				{
					PublishedFileId = d.PublishedFileId,
					Result = d.Result,
					VoteUp = d.VoteUp,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GetUserItemVoteResult_t
	{
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		public bool VotedUp; // m_bVotedUp _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool VotedDown; // m_bVotedDown _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool VoteSkipped; // m_bVoteSkipped _Bool
		public static GetUserItemVoteResult_t FromPointer( IntPtr p ) { return (GetUserItemVoteResult_t) Marshal.PtrToStructure( p, typeof(GetUserItemVoteResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			public bool VotedUp; // m_bVotedUp _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool VotedDown; // m_bVotedDown _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool VoteSkipped; // m_bVoteSkipped _Bool
			
			public static implicit operator GetUserItemVoteResult_t (  GetUserItemVoteResult_t.PackSmall d )
			{
				return new GetUserItemVoteResult_t()
				{
					PublishedFileId = d.PublishedFileId,
					Result = d.Result,
					VotedUp = d.VotedUp,
					VotedDown = d.VotedDown,
					VoteSkipped = d.VoteSkipped,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct StartPlaytimeTrackingResult_t
	{
		public Result Result; // m_eResult enum EResult
		public static StartPlaytimeTrackingResult_t FromPointer( IntPtr p ) { return (StartPlaytimeTrackingResult_t) Marshal.PtrToStructure( p, typeof(StartPlaytimeTrackingResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			
			public static implicit operator StartPlaytimeTrackingResult_t (  StartPlaytimeTrackingResult_t.PackSmall d )
			{
				return new StartPlaytimeTrackingResult_t()
				{
					Result = d.Result,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct StopPlaytimeTrackingResult_t
	{
		public Result Result; // m_eResult enum EResult
		public static StopPlaytimeTrackingResult_t FromPointer( IntPtr p ) { return (StopPlaytimeTrackingResult_t) Marshal.PtrToStructure( p, typeof(StopPlaytimeTrackingResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			
			public static implicit operator StopPlaytimeTrackingResult_t (  StopPlaytimeTrackingResult_t.PackSmall d )
			{
				return new StopPlaytimeTrackingResult_t()
				{
					Result = d.Result,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamAppInstalled_t
	{
		public uint AppID; // m_nAppID AppId_t
		public static SteamAppInstalled_t FromPointer( IntPtr p ) { return (SteamAppInstalled_t) Marshal.PtrToStructure( p, typeof(SteamAppInstalled_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint AppID; // m_nAppID AppId_t
			
			public static implicit operator SteamAppInstalled_t (  SteamAppInstalled_t.PackSmall d )
			{
				return new SteamAppInstalled_t()
				{
					AppID = d.AppID,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamAppUninstalled_t
	{
		public uint AppID; // m_nAppID AppId_t
		public static SteamAppUninstalled_t FromPointer( IntPtr p ) { return (SteamAppUninstalled_t) Marshal.PtrToStructure( p, typeof(SteamAppUninstalled_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint AppID; // m_nAppID AppId_t
			
			public static implicit operator SteamAppUninstalled_t (  SteamAppUninstalled_t.PackSmall d )
			{
				return new SteamAppUninstalled_t()
				{
					AppID = d.AppID,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_BrowserReady_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public static HTML_BrowserReady_t FromPointer( IntPtr p ) { return (HTML_BrowserReady_t) Marshal.PtrToStructure( p, typeof(HTML_BrowserReady_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			
			public static implicit operator HTML_BrowserReady_t (  HTML_BrowserReady_t.PackSmall d )
			{
				return new HTML_BrowserReady_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_NeedsPaint_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PBGRA; // pBGRA const char *
		public uint UnWide; // unWide uint32
		public uint UnTall; // unTall uint32
		public uint UnUpdateX; // unUpdateX uint32
		public uint UnUpdateY; // unUpdateY uint32
		public uint UnUpdateWide; // unUpdateWide uint32
		public uint UnUpdateTall; // unUpdateTall uint32
		public uint UnScrollX; // unScrollX uint32
		public uint UnScrollY; // unScrollY uint32
		public float FlPageScale; // flPageScale float
		public uint UnPageSerial; // unPageSerial uint32
		public static HTML_NeedsPaint_t FromPointer( IntPtr p ) { return (HTML_NeedsPaint_t) Marshal.PtrToStructure( p, typeof(HTML_NeedsPaint_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PBGRA; // pBGRA const char *
			public uint UnWide; // unWide uint32
			public uint UnTall; // unTall uint32
			public uint UnUpdateX; // unUpdateX uint32
			public uint UnUpdateY; // unUpdateY uint32
			public uint UnUpdateWide; // unUpdateWide uint32
			public uint UnUpdateTall; // unUpdateTall uint32
			public uint UnScrollX; // unScrollX uint32
			public uint UnScrollY; // unScrollY uint32
			public float FlPageScale; // flPageScale float
			public uint UnPageSerial; // unPageSerial uint32
			
			public static implicit operator HTML_NeedsPaint_t (  HTML_NeedsPaint_t.PackSmall d )
			{
				return new HTML_NeedsPaint_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PBGRA = d.PBGRA,
					UnWide = d.UnWide,
					UnTall = d.UnTall,
					UnUpdateX = d.UnUpdateX,
					UnUpdateY = d.UnUpdateY,
					UnUpdateWide = d.UnUpdateWide,
					UnUpdateTall = d.UnUpdateTall,
					UnScrollX = d.UnScrollX,
					UnScrollY = d.UnScrollY,
					FlPageScale = d.FlPageScale,
					UnPageSerial = d.UnPageSerial,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_StartRequest_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchURL; // pchURL const char *
		public string PchTarget; // pchTarget const char *
		public string PchPostData; // pchPostData const char *
		[MarshalAs(UnmanagedType.I1)]
		public bool BIsRedirect; // bIsRedirect _Bool
		public static HTML_StartRequest_t FromPointer( IntPtr p ) { return (HTML_StartRequest_t) Marshal.PtrToStructure( p, typeof(HTML_StartRequest_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchURL; // pchURL const char *
			public string PchTarget; // pchTarget const char *
			public string PchPostData; // pchPostData const char *
			[MarshalAs(UnmanagedType.I1)]
			public bool BIsRedirect; // bIsRedirect _Bool
			
			public static implicit operator HTML_StartRequest_t (  HTML_StartRequest_t.PackSmall d )
			{
				return new HTML_StartRequest_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchURL = d.PchURL,
					PchTarget = d.PchTarget,
					PchPostData = d.PchPostData,
					BIsRedirect = d.BIsRedirect,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_CloseBrowser_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public static HTML_CloseBrowser_t FromPointer( IntPtr p ) { return (HTML_CloseBrowser_t) Marshal.PtrToStructure( p, typeof(HTML_CloseBrowser_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			
			public static implicit operator HTML_CloseBrowser_t (  HTML_CloseBrowser_t.PackSmall d )
			{
				return new HTML_CloseBrowser_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_URLChanged_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchURL; // pchURL const char *
		public string PchPostData; // pchPostData const char *
		[MarshalAs(UnmanagedType.I1)]
		public bool BIsRedirect; // bIsRedirect _Bool
		public string PchPageTitle; // pchPageTitle const char *
		[MarshalAs(UnmanagedType.I1)]
		public bool BNewNavigation; // bNewNavigation _Bool
		public static HTML_URLChanged_t FromPointer( IntPtr p ) { return (HTML_URLChanged_t) Marshal.PtrToStructure( p, typeof(HTML_URLChanged_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchURL; // pchURL const char *
			public string PchPostData; // pchPostData const char *
			[MarshalAs(UnmanagedType.I1)]
			public bool BIsRedirect; // bIsRedirect _Bool
			public string PchPageTitle; // pchPageTitle const char *
			[MarshalAs(UnmanagedType.I1)]
			public bool BNewNavigation; // bNewNavigation _Bool
			
			public static implicit operator HTML_URLChanged_t (  HTML_URLChanged_t.PackSmall d )
			{
				return new HTML_URLChanged_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchURL = d.PchURL,
					PchPostData = d.PchPostData,
					BIsRedirect = d.BIsRedirect,
					PchPageTitle = d.PchPageTitle,
					BNewNavigation = d.BNewNavigation,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_FinishedRequest_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchURL; // pchURL const char *
		public string PchPageTitle; // pchPageTitle const char *
		public static HTML_FinishedRequest_t FromPointer( IntPtr p ) { return (HTML_FinishedRequest_t) Marshal.PtrToStructure( p, typeof(HTML_FinishedRequest_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchURL; // pchURL const char *
			public string PchPageTitle; // pchPageTitle const char *
			
			public static implicit operator HTML_FinishedRequest_t (  HTML_FinishedRequest_t.PackSmall d )
			{
				return new HTML_FinishedRequest_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchURL = d.PchURL,
					PchPageTitle = d.PchPageTitle,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_OpenLinkInNewTab_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchURL; // pchURL const char *
		public static HTML_OpenLinkInNewTab_t FromPointer( IntPtr p ) { return (HTML_OpenLinkInNewTab_t) Marshal.PtrToStructure( p, typeof(HTML_OpenLinkInNewTab_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchURL; // pchURL const char *
			
			public static implicit operator HTML_OpenLinkInNewTab_t (  HTML_OpenLinkInNewTab_t.PackSmall d )
			{
				return new HTML_OpenLinkInNewTab_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchURL = d.PchURL,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_ChangedTitle_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchTitle; // pchTitle const char *
		public static HTML_ChangedTitle_t FromPointer( IntPtr p ) { return (HTML_ChangedTitle_t) Marshal.PtrToStructure( p, typeof(HTML_ChangedTitle_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchTitle; // pchTitle const char *
			
			public static implicit operator HTML_ChangedTitle_t (  HTML_ChangedTitle_t.PackSmall d )
			{
				return new HTML_ChangedTitle_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchTitle = d.PchTitle,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_SearchResults_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public uint UnResults; // unResults uint32
		public uint UnCurrentMatch; // unCurrentMatch uint32
		public static HTML_SearchResults_t FromPointer( IntPtr p ) { return (HTML_SearchResults_t) Marshal.PtrToStructure( p, typeof(HTML_SearchResults_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public uint UnResults; // unResults uint32
			public uint UnCurrentMatch; // unCurrentMatch uint32
			
			public static implicit operator HTML_SearchResults_t (  HTML_SearchResults_t.PackSmall d )
			{
				return new HTML_SearchResults_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					UnResults = d.UnResults,
					UnCurrentMatch = d.UnCurrentMatch,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_CanGoBackAndForward_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		[MarshalAs(UnmanagedType.I1)]
		public bool BCanGoBack; // bCanGoBack _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool BCanGoForward; // bCanGoForward _Bool
		public static HTML_CanGoBackAndForward_t FromPointer( IntPtr p ) { return (HTML_CanGoBackAndForward_t) Marshal.PtrToStructure( p, typeof(HTML_CanGoBackAndForward_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			[MarshalAs(UnmanagedType.I1)]
			public bool BCanGoBack; // bCanGoBack _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool BCanGoForward; // bCanGoForward _Bool
			
			public static implicit operator HTML_CanGoBackAndForward_t (  HTML_CanGoBackAndForward_t.PackSmall d )
			{
				return new HTML_CanGoBackAndForward_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					BCanGoBack = d.BCanGoBack,
					BCanGoForward = d.BCanGoForward,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_HorizontalScroll_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public uint UnScrollMax; // unScrollMax uint32
		public uint UnScrollCurrent; // unScrollCurrent uint32
		public float FlPageScale; // flPageScale float
		[MarshalAs(UnmanagedType.I1)]
		public bool BVisible; // bVisible _Bool
		public uint UnPageSize; // unPageSize uint32
		public static HTML_HorizontalScroll_t FromPointer( IntPtr p ) { return (HTML_HorizontalScroll_t) Marshal.PtrToStructure( p, typeof(HTML_HorizontalScroll_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public uint UnScrollMax; // unScrollMax uint32
			public uint UnScrollCurrent; // unScrollCurrent uint32
			public float FlPageScale; // flPageScale float
			[MarshalAs(UnmanagedType.I1)]
			public bool BVisible; // bVisible _Bool
			public uint UnPageSize; // unPageSize uint32
			
			public static implicit operator HTML_HorizontalScroll_t (  HTML_HorizontalScroll_t.PackSmall d )
			{
				return new HTML_HorizontalScroll_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					UnScrollMax = d.UnScrollMax,
					UnScrollCurrent = d.UnScrollCurrent,
					FlPageScale = d.FlPageScale,
					BVisible = d.BVisible,
					UnPageSize = d.UnPageSize,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_VerticalScroll_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public uint UnScrollMax; // unScrollMax uint32
		public uint UnScrollCurrent; // unScrollCurrent uint32
		public float FlPageScale; // flPageScale float
		[MarshalAs(UnmanagedType.I1)]
		public bool BVisible; // bVisible _Bool
		public uint UnPageSize; // unPageSize uint32
		public static HTML_VerticalScroll_t FromPointer( IntPtr p ) { return (HTML_VerticalScroll_t) Marshal.PtrToStructure( p, typeof(HTML_VerticalScroll_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public uint UnScrollMax; // unScrollMax uint32
			public uint UnScrollCurrent; // unScrollCurrent uint32
			public float FlPageScale; // flPageScale float
			[MarshalAs(UnmanagedType.I1)]
			public bool BVisible; // bVisible _Bool
			public uint UnPageSize; // unPageSize uint32
			
			public static implicit operator HTML_VerticalScroll_t (  HTML_VerticalScroll_t.PackSmall d )
			{
				return new HTML_VerticalScroll_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					UnScrollMax = d.UnScrollMax,
					UnScrollCurrent = d.UnScrollCurrent,
					FlPageScale = d.FlPageScale,
					BVisible = d.BVisible,
					UnPageSize = d.UnPageSize,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_LinkAtPosition_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public uint X; // x uint32
		public uint Y; // y uint32
		public string PchURL; // pchURL const char *
		[MarshalAs(UnmanagedType.I1)]
		public bool BInput; // bInput _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool BLiveLink; // bLiveLink _Bool
		public static HTML_LinkAtPosition_t FromPointer( IntPtr p ) { return (HTML_LinkAtPosition_t) Marshal.PtrToStructure( p, typeof(HTML_LinkAtPosition_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public uint X; // x uint32
			public uint Y; // y uint32
			public string PchURL; // pchURL const char *
			[MarshalAs(UnmanagedType.I1)]
			public bool BInput; // bInput _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool BLiveLink; // bLiveLink _Bool
			
			public static implicit operator HTML_LinkAtPosition_t (  HTML_LinkAtPosition_t.PackSmall d )
			{
				return new HTML_LinkAtPosition_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					X = d.X,
					Y = d.Y,
					PchURL = d.PchURL,
					BInput = d.BInput,
					BLiveLink = d.BLiveLink,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_JSAlert_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchMessage; // pchMessage const char *
		public static HTML_JSAlert_t FromPointer( IntPtr p ) { return (HTML_JSAlert_t) Marshal.PtrToStructure( p, typeof(HTML_JSAlert_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchMessage; // pchMessage const char *
			
			public static implicit operator HTML_JSAlert_t (  HTML_JSAlert_t.PackSmall d )
			{
				return new HTML_JSAlert_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchMessage = d.PchMessage,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_JSConfirm_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchMessage; // pchMessage const char *
		public static HTML_JSConfirm_t FromPointer( IntPtr p ) { return (HTML_JSConfirm_t) Marshal.PtrToStructure( p, typeof(HTML_JSConfirm_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchMessage; // pchMessage const char *
			
			public static implicit operator HTML_JSConfirm_t (  HTML_JSConfirm_t.PackSmall d )
			{
				return new HTML_JSConfirm_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchMessage = d.PchMessage,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_FileOpenDialog_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchTitle; // pchTitle const char *
		public string PchInitialFile; // pchInitialFile const char *
		public static HTML_FileOpenDialog_t FromPointer( IntPtr p ) { return (HTML_FileOpenDialog_t) Marshal.PtrToStructure( p, typeof(HTML_FileOpenDialog_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchTitle; // pchTitle const char *
			public string PchInitialFile; // pchInitialFile const char *
			
			public static implicit operator HTML_FileOpenDialog_t (  HTML_FileOpenDialog_t.PackSmall d )
			{
				return new HTML_FileOpenDialog_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchTitle = d.PchTitle,
					PchInitialFile = d.PchInitialFile,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_NewWindow_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchURL; // pchURL const char *
		public uint UnX; // unX uint32
		public uint UnY; // unY uint32
		public uint UnWide; // unWide uint32
		public uint UnTall; // unTall uint32
		public uint UnNewWindow_BrowserHandle; // unNewWindow_BrowserHandle HHTMLBrowser
		public static HTML_NewWindow_t FromPointer( IntPtr p ) { return (HTML_NewWindow_t) Marshal.PtrToStructure( p, typeof(HTML_NewWindow_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchURL; // pchURL const char *
			public uint UnX; // unX uint32
			public uint UnY; // unY uint32
			public uint UnWide; // unWide uint32
			public uint UnTall; // unTall uint32
			public uint UnNewWindow_BrowserHandle; // unNewWindow_BrowserHandle HHTMLBrowser
			
			public static implicit operator HTML_NewWindow_t (  HTML_NewWindow_t.PackSmall d )
			{
				return new HTML_NewWindow_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchURL = d.PchURL,
					UnX = d.UnX,
					UnY = d.UnY,
					UnWide = d.UnWide,
					UnTall = d.UnTall,
					UnNewWindow_BrowserHandle = d.UnNewWindow_BrowserHandle,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_SetCursor_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public uint EMouseCursor; // eMouseCursor uint32
		public static HTML_SetCursor_t FromPointer( IntPtr p ) { return (HTML_SetCursor_t) Marshal.PtrToStructure( p, typeof(HTML_SetCursor_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public uint EMouseCursor; // eMouseCursor uint32
			
			public static implicit operator HTML_SetCursor_t (  HTML_SetCursor_t.PackSmall d )
			{
				return new HTML_SetCursor_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					EMouseCursor = d.EMouseCursor,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_StatusText_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchMsg; // pchMsg const char *
		public static HTML_StatusText_t FromPointer( IntPtr p ) { return (HTML_StatusText_t) Marshal.PtrToStructure( p, typeof(HTML_StatusText_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchMsg; // pchMsg const char *
			
			public static implicit operator HTML_StatusText_t (  HTML_StatusText_t.PackSmall d )
			{
				return new HTML_StatusText_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchMsg = d.PchMsg,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_ShowToolTip_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchMsg; // pchMsg const char *
		public static HTML_ShowToolTip_t FromPointer( IntPtr p ) { return (HTML_ShowToolTip_t) Marshal.PtrToStructure( p, typeof(HTML_ShowToolTip_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchMsg; // pchMsg const char *
			
			public static implicit operator HTML_ShowToolTip_t (  HTML_ShowToolTip_t.PackSmall d )
			{
				return new HTML_ShowToolTip_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchMsg = d.PchMsg,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_UpdateToolTip_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchMsg; // pchMsg const char *
		public static HTML_UpdateToolTip_t FromPointer( IntPtr p ) { return (HTML_UpdateToolTip_t) Marshal.PtrToStructure( p, typeof(HTML_UpdateToolTip_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchMsg; // pchMsg const char *
			
			public static implicit operator HTML_UpdateToolTip_t (  HTML_UpdateToolTip_t.PackSmall d )
			{
				return new HTML_UpdateToolTip_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchMsg = d.PchMsg,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_HideToolTip_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public static HTML_HideToolTip_t FromPointer( IntPtr p ) { return (HTML_HideToolTip_t) Marshal.PtrToStructure( p, typeof(HTML_HideToolTip_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			
			public static implicit operator HTML_HideToolTip_t (  HTML_HideToolTip_t.PackSmall d )
			{
				return new HTML_HideToolTip_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamItemDetails_t
	{
		public ulong ItemId; // m_itemId SteamItemInstanceID_t
		public int Definition; // m_iDefinition SteamItemDef_t
		public ushort Quantity; // m_unQuantity uint16
		public ushort Flags; // m_unFlags uint16
		public static SteamItemDetails_t FromPointer( IntPtr p ) { return (SteamItemDetails_t) Marshal.PtrToStructure( p, typeof(SteamItemDetails_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong ItemId; // m_itemId SteamItemInstanceID_t
			public int Definition; // m_iDefinition SteamItemDef_t
			public ushort Quantity; // m_unQuantity uint16
			public ushort Flags; // m_unFlags uint16
			
			public static implicit operator SteamItemDetails_t (  SteamItemDetails_t.PackSmall d )
			{
				return new SteamItemDetails_t()
				{
					ItemId = d.ItemId,
					Definition = d.Definition,
					Quantity = d.Quantity,
					Flags = d.Flags,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamInventoryResultReady_t
	{
		public int Andle; // m_handle SteamInventoryResult_t
		public Result Esult; // m_result enum EResult
		public static SteamInventoryResultReady_t FromPointer( IntPtr p ) { return (SteamInventoryResultReady_t) Marshal.PtrToStructure( p, typeof(SteamInventoryResultReady_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public int Andle; // m_handle SteamInventoryResult_t
			public Result Esult; // m_result enum EResult
			
			public static implicit operator SteamInventoryResultReady_t (  SteamInventoryResultReady_t.PackSmall d )
			{
				return new SteamInventoryResultReady_t()
				{
					Andle = d.Andle,
					Esult = d.Esult,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamInventoryFullUpdate_t
	{
		public int Andle; // m_handle SteamInventoryResult_t
		public static SteamInventoryFullUpdate_t FromPointer( IntPtr p ) { return (SteamInventoryFullUpdate_t) Marshal.PtrToStructure( p, typeof(SteamInventoryFullUpdate_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public int Andle; // m_handle SteamInventoryResult_t
			
			public static implicit operator SteamInventoryFullUpdate_t (  SteamInventoryFullUpdate_t.PackSmall d )
			{
				return new SteamInventoryFullUpdate_t()
				{
					Andle = d.Andle,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct BroadcastUploadStop_t
	{
		public BroadcastUploadResult Result; // m_eResult enum EBroadcastUploadResult
		public static BroadcastUploadStop_t FromPointer( IntPtr p ) { return (BroadcastUploadStop_t) Marshal.PtrToStructure( p, typeof(BroadcastUploadStop_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public BroadcastUploadResult Result; // m_eResult enum EBroadcastUploadResult
			
			public static implicit operator BroadcastUploadStop_t (  BroadcastUploadStop_t.PackSmall d )
			{
				return new BroadcastUploadStop_t()
				{
					Result = d.Result,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GetVideoURLResult_t
	{
		public Result Result; // m_eResult enum EResult
		public uint VideoAppID; // m_unVideoAppID AppId_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string URL; // m_rgchURL char [256]
		public static GetVideoURLResult_t FromPointer( IntPtr p ) { return (GetVideoURLResult_t) Marshal.PtrToStructure( p, typeof(GetVideoURLResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public uint VideoAppID; // m_unVideoAppID AppId_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string URL; // m_rgchURL char [256]
			
			public static implicit operator GetVideoURLResult_t (  GetVideoURLResult_t.PackSmall d )
			{
				return new GetVideoURLResult_t()
				{
					Result = d.Result,
					VideoAppID = d.VideoAppID,
					URL = d.URL,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct CCallbackBase
	{
		public byte CallbackFlags; // m_nCallbackFlags uint8
		public int Callback; // m_iCallback int
		public static CCallbackBase FromPointer( IntPtr p ) { return (CCallbackBase) Marshal.PtrToStructure( p, typeof(CCallbackBase) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public byte CallbackFlags; // m_nCallbackFlags uint8
			public int Callback; // m_iCallback int
			
			public static implicit operator CCallbackBase (  CCallbackBase.PackSmall d )
			{
				return new CCallbackBase()
				{
					CallbackFlags = d.CallbackFlags,
					Callback = d.Callback,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GSClientApprove_t
	{
		public ulong SteamID; // m_SteamID class CSteamID
		public ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
		public static GSClientApprove_t FromPointer( IntPtr p ) { return (GSClientApprove_t) Marshal.PtrToStructure( p, typeof(GSClientApprove_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamID; // m_SteamID class CSteamID
			public ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
			
			public static implicit operator GSClientApprove_t (  GSClientApprove_t.PackSmall d )
			{
				return new GSClientApprove_t()
				{
					SteamID = d.SteamID,
					OwnerSteamID = d.OwnerSteamID,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GSClientDeny_t
	{
		public ulong SteamID; // m_SteamID class CSteamID
		public DenyReason DenyReason; // m_eDenyReason enum EDenyReason
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string OptionalText; // m_rgchOptionalText char [128]
		public static GSClientDeny_t FromPointer( IntPtr p ) { return (GSClientDeny_t) Marshal.PtrToStructure( p, typeof(GSClientDeny_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamID; // m_SteamID class CSteamID
			public DenyReason DenyReason; // m_eDenyReason enum EDenyReason
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string OptionalText; // m_rgchOptionalText char [128]
			
			public static implicit operator GSClientDeny_t (  GSClientDeny_t.PackSmall d )
			{
				return new GSClientDeny_t()
				{
					SteamID = d.SteamID,
					DenyReason = d.DenyReason,
					OptionalText = d.OptionalText,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GSClientKick_t
	{
		public ulong SteamID; // m_SteamID class CSteamID
		public DenyReason DenyReason; // m_eDenyReason enum EDenyReason
		public static GSClientKick_t FromPointer( IntPtr p ) { return (GSClientKick_t) Marshal.PtrToStructure( p, typeof(GSClientKick_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamID; // m_SteamID class CSteamID
			public DenyReason DenyReason; // m_eDenyReason enum EDenyReason
			
			public static implicit operator GSClientKick_t (  GSClientKick_t.PackSmall d )
			{
				return new GSClientKick_t()
				{
					SteamID = d.SteamID,
					DenyReason = d.DenyReason,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GSClientAchievementStatus_t
	{
		public ulong SteamID; // m_SteamID uint64
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string PchAchievement; // m_pchAchievement char [128]
		[MarshalAs(UnmanagedType.I1)]
		public bool Unlocked; // m_bUnlocked _Bool
		public static GSClientAchievementStatus_t FromPointer( IntPtr p ) { return (GSClientAchievementStatus_t) Marshal.PtrToStructure( p, typeof(GSClientAchievementStatus_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamID; // m_SteamID uint64
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string PchAchievement; // m_pchAchievement char [128]
			[MarshalAs(UnmanagedType.I1)]
			public bool Unlocked; // m_bUnlocked _Bool
			
			public static implicit operator GSClientAchievementStatus_t (  GSClientAchievementStatus_t.PackSmall d )
			{
				return new GSClientAchievementStatus_t()
				{
					SteamID = d.SteamID,
					PchAchievement = d.PchAchievement,
					Unlocked = d.Unlocked,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GSPolicyResponse_t
	{
		public byte Secure; // m_bSecure uint8
		public static GSPolicyResponse_t FromPointer( IntPtr p ) { return (GSPolicyResponse_t) Marshal.PtrToStructure( p, typeof(GSPolicyResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public byte Secure; // m_bSecure uint8
			
			public static implicit operator GSPolicyResponse_t (  GSPolicyResponse_t.PackSmall d )
			{
				return new GSPolicyResponse_t()
				{
					Secure = d.Secure,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GSGameplayStats_t
	{
		public Result Result; // m_eResult enum EResult
		public int Rank; // m_nRank int32
		public uint TotalConnects; // m_unTotalConnects uint32
		public uint TotalMinutesPlayed; // m_unTotalMinutesPlayed uint32
		public static GSGameplayStats_t FromPointer( IntPtr p ) { return (GSGameplayStats_t) Marshal.PtrToStructure( p, typeof(GSGameplayStats_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public int Rank; // m_nRank int32
			public uint TotalConnects; // m_unTotalConnects uint32
			public uint TotalMinutesPlayed; // m_unTotalMinutesPlayed uint32
			
			public static implicit operator GSGameplayStats_t (  GSGameplayStats_t.PackSmall d )
			{
				return new GSGameplayStats_t()
				{
					Result = d.Result,
					Rank = d.Rank,
					TotalConnects = d.TotalConnects,
					TotalMinutesPlayed = d.TotalMinutesPlayed,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GSClientGroupStatus_t
	{
		public ulong SteamIDUser; // m_SteamIDUser class CSteamID
		public ulong SteamIDGroup; // m_SteamIDGroup class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		public bool Member; // m_bMember _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool Officer; // m_bOfficer _Bool
		public static GSClientGroupStatus_t FromPointer( IntPtr p ) { return (GSClientGroupStatus_t) Marshal.PtrToStructure( p, typeof(GSClientGroupStatus_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDUser; // m_SteamIDUser class CSteamID
			public ulong SteamIDGroup; // m_SteamIDGroup class CSteamID
			[MarshalAs(UnmanagedType.I1)]
			public bool Member; // m_bMember _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool Officer; // m_bOfficer _Bool
			
			public static implicit operator GSClientGroupStatus_t (  GSClientGroupStatus_t.PackSmall d )
			{
				return new GSClientGroupStatus_t()
				{
					SteamIDUser = d.SteamIDUser,
					SteamIDGroup = d.SteamIDGroup,
					Member = d.Member,
					Officer = d.Officer,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GSReputation_t
	{
		public Result Result; // m_eResult enum EResult
		public uint ReputationScore; // m_unReputationScore uint32
		[MarshalAs(UnmanagedType.I1)]
		public bool Banned; // m_bBanned _Bool
		public uint BannedIP; // m_unBannedIP uint32
		public ushort BannedPort; // m_usBannedPort uint16
		public ulong BannedGameID; // m_ulBannedGameID uint64
		public uint BanExpires; // m_unBanExpires uint32
		public static GSReputation_t FromPointer( IntPtr p ) { return (GSReputation_t) Marshal.PtrToStructure( p, typeof(GSReputation_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public uint ReputationScore; // m_unReputationScore uint32
			[MarshalAs(UnmanagedType.I1)]
			public bool Banned; // m_bBanned _Bool
			public uint BannedIP; // m_unBannedIP uint32
			public ushort BannedPort; // m_usBannedPort uint16
			public ulong BannedGameID; // m_ulBannedGameID uint64
			public uint BanExpires; // m_unBanExpires uint32
			
			public static implicit operator GSReputation_t (  GSReputation_t.PackSmall d )
			{
				return new GSReputation_t()
				{
					Result = d.Result,
					ReputationScore = d.ReputationScore,
					Banned = d.Banned,
					BannedIP = d.BannedIP,
					BannedPort = d.BannedPort,
					BannedGameID = d.BannedGameID,
					BanExpires = d.BanExpires,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct AssociateWithClanResult_t
	{
		public Result Result; // m_eResult enum EResult
		public static AssociateWithClanResult_t FromPointer( IntPtr p ) { return (AssociateWithClanResult_t) Marshal.PtrToStructure( p, typeof(AssociateWithClanResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			
			public static implicit operator AssociateWithClanResult_t (  AssociateWithClanResult_t.PackSmall d )
			{
				return new AssociateWithClanResult_t()
				{
					Result = d.Result,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct ComputeNewPlayerCompatibilityResult_t
	{
		public Result Result; // m_eResult enum EResult
		public int CPlayersThatDontLikeCandidate; // m_cPlayersThatDontLikeCandidate int
		public int CPlayersThatCandidateDoesntLike; // m_cPlayersThatCandidateDoesntLike int
		public int CClanPlayersThatDontLikeCandidate; // m_cClanPlayersThatDontLikeCandidate int
		public ulong SteamIDCandidate; // m_SteamIDCandidate class CSteamID
		public static ComputeNewPlayerCompatibilityResult_t FromPointer( IntPtr p ) { return (ComputeNewPlayerCompatibilityResult_t) Marshal.PtrToStructure( p, typeof(ComputeNewPlayerCompatibilityResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public int CPlayersThatDontLikeCandidate; // m_cPlayersThatDontLikeCandidate int
			public int CPlayersThatCandidateDoesntLike; // m_cPlayersThatCandidateDoesntLike int
			public int CClanPlayersThatDontLikeCandidate; // m_cClanPlayersThatDontLikeCandidate int
			public ulong SteamIDCandidate; // m_SteamIDCandidate class CSteamID
			
			public static implicit operator ComputeNewPlayerCompatibilityResult_t (  ComputeNewPlayerCompatibilityResult_t.PackSmall d )
			{
				return new ComputeNewPlayerCompatibilityResult_t()
				{
					Result = d.Result,
					CPlayersThatDontLikeCandidate = d.CPlayersThatDontLikeCandidate,
					CPlayersThatCandidateDoesntLike = d.CPlayersThatCandidateDoesntLike,
					CClanPlayersThatDontLikeCandidate = d.CClanPlayersThatDontLikeCandidate,
					SteamIDCandidate = d.SteamIDCandidate,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GSStatsReceived_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		public static GSStatsReceived_t FromPointer( IntPtr p ) { return (GSStatsReceived_t) Marshal.PtrToStructure( p, typeof(GSStatsReceived_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator GSStatsReceived_t (  GSStatsReceived_t.PackSmall d )
			{
				return new GSStatsReceived_t()
				{
					Result = d.Result,
					SteamIDUser = d.SteamIDUser,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GSStatsStored_t
	{
		public Result Result; // m_eResult enum EResult
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		public static GSStatsStored_t FromPointer( IntPtr p ) { return (GSStatsStored_t) Marshal.PtrToStructure( p, typeof(GSStatsStored_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator GSStatsStored_t (  GSStatsStored_t.PackSmall d )
			{
				return new GSStatsStored_t()
				{
					Result = d.Result,
					SteamIDUser = d.SteamIDUser,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GSStatsUnloaded_t
	{
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		public static GSStatsUnloaded_t FromPointer( IntPtr p ) { return (GSStatsUnloaded_t) Marshal.PtrToStructure( p, typeof(GSStatsUnloaded_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator GSStatsUnloaded_t (  GSStatsUnloaded_t.PackSmall d )
			{
				return new GSStatsUnloaded_t()
				{
					SteamIDUser = d.SteamIDUser,
				};
			}
		}
	}
	
}
