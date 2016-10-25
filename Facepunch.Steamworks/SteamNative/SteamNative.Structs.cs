using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct ValvePackingSentinel_t
	{
		public uint m_u32; // uint32
		public ulong m_u64; // uint64
		public ushort m_u16; // uint16
		public double m_d; // double
		public static ValvePackingSentinel_t FromPointer( IntPtr p ) { return (ValvePackingSentinel_t) Marshal.PtrToStructure( p, typeof(ValvePackingSentinel_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_u32; // uint32
			public ulong m_u64; // uint64
			public ushort m_u16; // uint16
			public double m_d; // double
			
			public ValvePackingSentinel_t Get
			{
				get
				{
					return new ValvePackingSentinel_t()
					{
						m_u32 = this.m_u32,
						m_u64 = this.m_u64,
						m_u16 = this.m_u16,
						m_d = this.m_d,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct CallbackMsg_t
	{
		public int m_hSteamUser; // HSteamUser
		public int m_iCallback; // int
		public IntPtr m_pubParam; // uint8 *
		public int m_cubParam; // int
		public static CallbackMsg_t FromPointer( IntPtr p ) { return (CallbackMsg_t) Marshal.PtrToStructure( p, typeof(CallbackMsg_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public int m_hSteamUser; // HSteamUser
			public int m_iCallback; // int
			public IntPtr m_pubParam; // uint8 *
			public int m_cubParam; // int
			
			public CallbackMsg_t Get
			{
				get
				{
					return new CallbackMsg_t()
					{
						m_hSteamUser = this.m_hSteamUser,
						m_iCallback = this.m_iCallback,
						m_pubParam = this.m_pubParam,
						m_cubParam = this.m_cubParam,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamServerConnectFailure_t
	{
		public Result m_eResult; // enum EResult
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bStillRetrying; // _Bool
		public static SteamServerConnectFailure_t FromPointer( IntPtr p ) { return (SteamServerConnectFailure_t) Marshal.PtrToStructure( p, typeof(SteamServerConnectFailure_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bStillRetrying; // _Bool
			
			public SteamServerConnectFailure_t Get
			{
				get
				{
					return new SteamServerConnectFailure_t()
					{
						m_eResult = this.m_eResult,
						m_bStillRetrying = this.m_bStillRetrying,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamServersDisconnected_t
	{
		public Result m_eResult; // enum EResult
		public static SteamServersDisconnected_t FromPointer( IntPtr p ) { return (SteamServersDisconnected_t) Marshal.PtrToStructure( p, typeof(SteamServersDisconnected_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			
			public SteamServersDisconnected_t Get
			{
				get
				{
					return new SteamServersDisconnected_t()
					{
						m_eResult = this.m_eResult,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct ClientGameServerDeny_t
	{
		public uint m_uAppID; // uint32
		public uint m_unGameServerIP; // uint32
		public ushort m_usGameServerPort; // uint16
		public ushort m_bSecure; // uint16
		public uint m_uReason; // uint32
		public static ClientGameServerDeny_t FromPointer( IntPtr p ) { return (ClientGameServerDeny_t) Marshal.PtrToStructure( p, typeof(ClientGameServerDeny_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_uAppID; // uint32
			public uint m_unGameServerIP; // uint32
			public ushort m_usGameServerPort; // uint16
			public ushort m_bSecure; // uint16
			public uint m_uReason; // uint32
			
			public ClientGameServerDeny_t Get
			{
				get
				{
					return new ClientGameServerDeny_t()
					{
						m_uAppID = this.m_uAppID,
						m_unGameServerIP = this.m_unGameServerIP,
						m_usGameServerPort = this.m_usGameServerPort,
						m_bSecure = this.m_bSecure,
						m_uReason = this.m_uReason,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct ValidateAuthTicketResponse_t
	{
		public ulong m_SteamID; // class CSteamID
		public AuthSessionResponse m_eAuthSessionResponse; // enum EAuthSessionResponse
		public ulong m_OwnerSteamID; // class CSteamID
		public static ValidateAuthTicketResponse_t FromPointer( IntPtr p ) { return (ValidateAuthTicketResponse_t) Marshal.PtrToStructure( p, typeof(ValidateAuthTicketResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_SteamID; // class CSteamID
			public AuthSessionResponse m_eAuthSessionResponse; // enum EAuthSessionResponse
			public ulong m_OwnerSteamID; // class CSteamID
			
			public ValidateAuthTicketResponse_t Get
			{
				get
				{
					return new ValidateAuthTicketResponse_t()
					{
						m_SteamID = this.m_SteamID,
						m_eAuthSessionResponse = this.m_eAuthSessionResponse,
						m_OwnerSteamID = this.m_OwnerSteamID,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct MicroTxnAuthorizationResponse_t
	{
		public uint m_unAppID; // uint32
		public ulong m_ulOrderID; // uint64
		public byte m_bAuthorized; // uint8
		public static MicroTxnAuthorizationResponse_t FromPointer( IntPtr p ) { return (MicroTxnAuthorizationResponse_t) Marshal.PtrToStructure( p, typeof(MicroTxnAuthorizationResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_unAppID; // uint32
			public ulong m_ulOrderID; // uint64
			public byte m_bAuthorized; // uint8
			
			public MicroTxnAuthorizationResponse_t Get
			{
				get
				{
					return new MicroTxnAuthorizationResponse_t()
					{
						m_unAppID = this.m_unAppID,
						m_ulOrderID = this.m_ulOrderID,
						m_bAuthorized = this.m_bAuthorized,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct EncryptedAppTicketResponse_t
	{
		public Result m_eResult; // enum EResult
		public static EncryptedAppTicketResponse_t FromPointer( IntPtr p ) { return (EncryptedAppTicketResponse_t) Marshal.PtrToStructure( p, typeof(EncryptedAppTicketResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			
			public EncryptedAppTicketResponse_t Get
			{
				get
				{
					return new EncryptedAppTicketResponse_t()
					{
						m_eResult = this.m_eResult,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GetAuthSessionTicketResponse_t
	{
		public uint m_hAuthTicket; // HAuthTicket
		public Result m_eResult; // enum EResult
		public static GetAuthSessionTicketResponse_t FromPointer( IntPtr p ) { return (GetAuthSessionTicketResponse_t) Marshal.PtrToStructure( p, typeof(GetAuthSessionTicketResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_hAuthTicket; // HAuthTicket
			public Result m_eResult; // enum EResult
			
			public GetAuthSessionTicketResponse_t Get
			{
				get
				{
					return new GetAuthSessionTicketResponse_t()
					{
						m_hAuthTicket = this.m_hAuthTicket,
						m_eResult = this.m_eResult,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GameWebCallback_t
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_szURL; // char [256]
		public static GameWebCallback_t FromPointer( IntPtr p ) { return (GameWebCallback_t) Marshal.PtrToStructure( p, typeof(GameWebCallback_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string m_szURL; // char [256]
			
			public GameWebCallback_t Get
			{
				get
				{
					return new GameWebCallback_t()
					{
						m_szURL = this.m_szURL,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct StoreAuthURLResponse_t
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
		public string m_szURL; // char [512]
		public static StoreAuthURLResponse_t FromPointer( IntPtr p ) { return (StoreAuthURLResponse_t) Marshal.PtrToStructure( p, typeof(StoreAuthURLResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
			public string m_szURL; // char [512]
			
			public StoreAuthURLResponse_t Get
			{
				get
				{
					return new StoreAuthURLResponse_t()
					{
						m_szURL = this.m_szURL,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct FriendGameInfo_t
	{
		public ulong m_gameID; // class CGameID
		public uint m_unGameIP; // uint32
		public ushort m_usGamePort; // uint16
		public ushort m_usQueryPort; // uint16
		public ulong m_steamIDLobby; // class CSteamID
		public static FriendGameInfo_t FromPointer( IntPtr p ) { return (FriendGameInfo_t) Marshal.PtrToStructure( p, typeof(FriendGameInfo_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_gameID; // class CGameID
			public uint m_unGameIP; // uint32
			public ushort m_usGamePort; // uint16
			public ushort m_usQueryPort; // uint16
			public ulong m_steamIDLobby; // class CSteamID
			
			public FriendGameInfo_t Get
			{
				get
				{
					return new FriendGameInfo_t()
					{
						m_gameID = this.m_gameID,
						m_unGameIP = this.m_unGameIP,
						m_usGamePort = this.m_usGamePort,
						m_usQueryPort = this.m_usQueryPort,
						m_steamIDLobby = this.m_steamIDLobby,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct FriendSessionStateInfo_t
	{
		public uint m_uiOnlineSessionInstances; // uint32
		public byte m_uiPublishedToFriendsSessionInstance; // uint8
		public static FriendSessionStateInfo_t FromPointer( IntPtr p ) { return (FriendSessionStateInfo_t) Marshal.PtrToStructure( p, typeof(FriendSessionStateInfo_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_uiOnlineSessionInstances; // uint32
			public byte m_uiPublishedToFriendsSessionInstance; // uint8
			
			public FriendSessionStateInfo_t Get
			{
				get
				{
					return new FriendSessionStateInfo_t()
					{
						m_uiOnlineSessionInstances = this.m_uiOnlineSessionInstances,
						m_uiPublishedToFriendsSessionInstance = this.m_uiPublishedToFriendsSessionInstance,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct PersonaStateChange_t
	{
		public ulong m_ulSteamID; // uint64
		public int m_nChangeFlags; // int
		public static PersonaStateChange_t FromPointer( IntPtr p ) { return (PersonaStateChange_t) Marshal.PtrToStructure( p, typeof(PersonaStateChange_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_ulSteamID; // uint64
			public int m_nChangeFlags; // int
			
			public PersonaStateChange_t Get
			{
				get
				{
					return new PersonaStateChange_t()
					{
						m_ulSteamID = this.m_ulSteamID,
						m_nChangeFlags = this.m_nChangeFlags,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GameOverlayActivated_t
	{
		public byte m_bActive; // uint8
		public static GameOverlayActivated_t FromPointer( IntPtr p ) { return (GameOverlayActivated_t) Marshal.PtrToStructure( p, typeof(GameOverlayActivated_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public byte m_bActive; // uint8
			
			public GameOverlayActivated_t Get
			{
				get
				{
					return new GameOverlayActivated_t()
					{
						m_bActive = this.m_bActive,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GameServerChangeRequested_t
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string m_rgchServer; // char [64]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string m_rgchPassword; // char [64]
		public static GameServerChangeRequested_t FromPointer( IntPtr p ) { return (GameServerChangeRequested_t) Marshal.PtrToStructure( p, typeof(GameServerChangeRequested_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string m_rgchServer; // char [64]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string m_rgchPassword; // char [64]
			
			public GameServerChangeRequested_t Get
			{
				get
				{
					return new GameServerChangeRequested_t()
					{
						m_rgchServer = this.m_rgchServer,
						m_rgchPassword = this.m_rgchPassword,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GameLobbyJoinRequested_t
	{
		public ulong m_steamIDLobby; // class CSteamID
		public ulong m_steamIDFriend; // class CSteamID
		public static GameLobbyJoinRequested_t FromPointer( IntPtr p ) { return (GameLobbyJoinRequested_t) Marshal.PtrToStructure( p, typeof(GameLobbyJoinRequested_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_steamIDLobby; // class CSteamID
			public ulong m_steamIDFriend; // class CSteamID
			
			public GameLobbyJoinRequested_t Get
			{
				get
				{
					return new GameLobbyJoinRequested_t()
					{
						m_steamIDLobby = this.m_steamIDLobby,
						m_steamIDFriend = this.m_steamIDFriend,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct AvatarImageLoaded_t
	{
		public ulong m_steamID; // class CSteamID
		public int m_iImage; // int
		public int m_iWide; // int
		public int m_iTall; // int
		public static AvatarImageLoaded_t FromPointer( IntPtr p ) { return (AvatarImageLoaded_t) Marshal.PtrToStructure( p, typeof(AvatarImageLoaded_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_steamID; // class CSteamID
			public int m_iImage; // int
			public int m_iWide; // int
			public int m_iTall; // int
			
			public AvatarImageLoaded_t Get
			{
				get
				{
					return new AvatarImageLoaded_t()
					{
						m_steamID = this.m_steamID,
						m_iImage = this.m_iImage,
						m_iWide = this.m_iWide,
						m_iTall = this.m_iTall,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct ClanOfficerListResponse_t
	{
		public ulong m_steamIDClan; // class CSteamID
		public int m_cOfficers; // int
		public byte m_bSuccess; // uint8
		public static ClanOfficerListResponse_t FromPointer( IntPtr p ) { return (ClanOfficerListResponse_t) Marshal.PtrToStructure( p, typeof(ClanOfficerListResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_steamIDClan; // class CSteamID
			public int m_cOfficers; // int
			public byte m_bSuccess; // uint8
			
			public ClanOfficerListResponse_t Get
			{
				get
				{
					return new ClanOfficerListResponse_t()
					{
						m_steamIDClan = this.m_steamIDClan,
						m_cOfficers = this.m_cOfficers,
						m_bSuccess = this.m_bSuccess,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct FriendRichPresenceUpdate_t
	{
		public ulong m_steamIDFriend; // class CSteamID
		public uint m_nAppID; // AppId_t
		public static FriendRichPresenceUpdate_t FromPointer( IntPtr p ) { return (FriendRichPresenceUpdate_t) Marshal.PtrToStructure( p, typeof(FriendRichPresenceUpdate_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_steamIDFriend; // class CSteamID
			public uint m_nAppID; // AppId_t
			
			public FriendRichPresenceUpdate_t Get
			{
				get
				{
					return new FriendRichPresenceUpdate_t()
					{
						m_steamIDFriend = this.m_steamIDFriend,
						m_nAppID = this.m_nAppID,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GameRichPresenceJoinRequested_t
	{
		public ulong m_steamIDFriend; // class CSteamID
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_rgchConnect; // char [256]
		public static GameRichPresenceJoinRequested_t FromPointer( IntPtr p ) { return (GameRichPresenceJoinRequested_t) Marshal.PtrToStructure( p, typeof(GameRichPresenceJoinRequested_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_steamIDFriend; // class CSteamID
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string m_rgchConnect; // char [256]
			
			public GameRichPresenceJoinRequested_t Get
			{
				get
				{
					return new GameRichPresenceJoinRequested_t()
					{
						m_steamIDFriend = this.m_steamIDFriend,
						m_rgchConnect = this.m_rgchConnect,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GameConnectedClanChatMsg_t
	{
		public ulong m_steamIDClanChat; // class CSteamID
		public ulong m_steamIDUser; // class CSteamID
		public int m_iMessageID; // int
		public static GameConnectedClanChatMsg_t FromPointer( IntPtr p ) { return (GameConnectedClanChatMsg_t) Marshal.PtrToStructure( p, typeof(GameConnectedClanChatMsg_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_steamIDClanChat; // class CSteamID
			public ulong m_steamIDUser; // class CSteamID
			public int m_iMessageID; // int
			
			public GameConnectedClanChatMsg_t Get
			{
				get
				{
					return new GameConnectedClanChatMsg_t()
					{
						m_steamIDClanChat = this.m_steamIDClanChat,
						m_steamIDUser = this.m_steamIDUser,
						m_iMessageID = this.m_iMessageID,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GameConnectedChatJoin_t
	{
		public ulong m_steamIDClanChat; // class CSteamID
		public ulong m_steamIDUser; // class CSteamID
		public static GameConnectedChatJoin_t FromPointer( IntPtr p ) { return (GameConnectedChatJoin_t) Marshal.PtrToStructure( p, typeof(GameConnectedChatJoin_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_steamIDClanChat; // class CSteamID
			public ulong m_steamIDUser; // class CSteamID
			
			public GameConnectedChatJoin_t Get
			{
				get
				{
					return new GameConnectedChatJoin_t()
					{
						m_steamIDClanChat = this.m_steamIDClanChat,
						m_steamIDUser = this.m_steamIDUser,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GameConnectedChatLeave_t
	{
		public ulong m_steamIDClanChat; // class CSteamID
		public ulong m_steamIDUser; // class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bKicked; // _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bDropped; // _Bool
		public static GameConnectedChatLeave_t FromPointer( IntPtr p ) { return (GameConnectedChatLeave_t) Marshal.PtrToStructure( p, typeof(GameConnectedChatLeave_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_steamIDClanChat; // class CSteamID
			public ulong m_steamIDUser; // class CSteamID
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bKicked; // _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bDropped; // _Bool
			
			public GameConnectedChatLeave_t Get
			{
				get
				{
					return new GameConnectedChatLeave_t()
					{
						m_steamIDClanChat = this.m_steamIDClanChat,
						m_steamIDUser = this.m_steamIDUser,
						m_bKicked = this.m_bKicked,
						m_bDropped = this.m_bDropped,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct DownloadClanActivityCountsResult_t
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSuccess; // _Bool
		public static DownloadClanActivityCountsResult_t FromPointer( IntPtr p ) { return (DownloadClanActivityCountsResult_t) Marshal.PtrToStructure( p, typeof(DownloadClanActivityCountsResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bSuccess; // _Bool
			
			public DownloadClanActivityCountsResult_t Get
			{
				get
				{
					return new DownloadClanActivityCountsResult_t()
					{
						m_bSuccess = this.m_bSuccess,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct JoinClanChatRoomCompletionResult_t
	{
		public ulong m_steamIDClanChat; // class CSteamID
		public ChatRoomEnterResponse m_eChatRoomEnterResponse; // enum EChatRoomEnterResponse
		public static JoinClanChatRoomCompletionResult_t FromPointer( IntPtr p ) { return (JoinClanChatRoomCompletionResult_t) Marshal.PtrToStructure( p, typeof(JoinClanChatRoomCompletionResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_steamIDClanChat; // class CSteamID
			public ChatRoomEnterResponse m_eChatRoomEnterResponse; // enum EChatRoomEnterResponse
			
			public JoinClanChatRoomCompletionResult_t Get
			{
				get
				{
					return new JoinClanChatRoomCompletionResult_t()
					{
						m_steamIDClanChat = this.m_steamIDClanChat,
						m_eChatRoomEnterResponse = this.m_eChatRoomEnterResponse,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GameConnectedFriendChatMsg_t
	{
		public ulong m_steamIDUser; // class CSteamID
		public int m_iMessageID; // int
		public static GameConnectedFriendChatMsg_t FromPointer( IntPtr p ) { return (GameConnectedFriendChatMsg_t) Marshal.PtrToStructure( p, typeof(GameConnectedFriendChatMsg_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_steamIDUser; // class CSteamID
			public int m_iMessageID; // int
			
			public GameConnectedFriendChatMsg_t Get
			{
				get
				{
					return new GameConnectedFriendChatMsg_t()
					{
						m_steamIDUser = this.m_steamIDUser,
						m_iMessageID = this.m_iMessageID,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct FriendsGetFollowerCount_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_steamID; // class CSteamID
		public int m_nCount; // int
		public static FriendsGetFollowerCount_t FromPointer( IntPtr p ) { return (FriendsGetFollowerCount_t) Marshal.PtrToStructure( p, typeof(FriendsGetFollowerCount_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_steamID; // class CSteamID
			public int m_nCount; // int
			
			public FriendsGetFollowerCount_t Get
			{
				get
				{
					return new FriendsGetFollowerCount_t()
					{
						m_eResult = this.m_eResult,
						m_steamID = this.m_steamID,
						m_nCount = this.m_nCount,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct FriendsIsFollowing_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_steamID; // class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bIsFollowing; // _Bool
		public static FriendsIsFollowing_t FromPointer( IntPtr p ) { return (FriendsIsFollowing_t) Marshal.PtrToStructure( p, typeof(FriendsIsFollowing_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_steamID; // class CSteamID
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bIsFollowing; // _Bool
			
			public FriendsIsFollowing_t Get
			{
				get
				{
					return new FriendsIsFollowing_t()
					{
						m_eResult = this.m_eResult,
						m_steamID = this.m_steamID,
						m_bIsFollowing = this.m_bIsFollowing,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct FriendsEnumerateFollowingList_t
	{
		public Result m_eResult; // enum EResult
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public ulong[] m_rgSteamID; // class CSteamID [50]
		public int m_nResultsReturned; // int32
		public int m_nTotalResultCount; // int32
		public static FriendsEnumerateFollowingList_t FromPointer( IntPtr p ) { return (FriendsEnumerateFollowingList_t) Marshal.PtrToStructure( p, typeof(FriendsEnumerateFollowingList_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public ulong[] m_rgSteamID; // class CSteamID [50]
			public int m_nResultsReturned; // int32
			public int m_nTotalResultCount; // int32
			
			public FriendsEnumerateFollowingList_t Get
			{
				get
				{
					return new FriendsEnumerateFollowingList_t()
					{
						m_eResult = this.m_eResult,
						m_rgSteamID = this.m_rgSteamID,
						m_nResultsReturned = this.m_nResultsReturned,
						m_nTotalResultCount = this.m_nTotalResultCount,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SetPersonaNameResponse_t
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSuccess; // _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bLocalSuccess; // _Bool
		public Result m_result; // enum EResult
		public static SetPersonaNameResponse_t FromPointer( IntPtr p ) { return (SetPersonaNameResponse_t) Marshal.PtrToStructure( p, typeof(SetPersonaNameResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bSuccess; // _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bLocalSuccess; // _Bool
			public Result m_result; // enum EResult
			
			public SetPersonaNameResponse_t Get
			{
				get
				{
					return new SetPersonaNameResponse_t()
					{
						m_bSuccess = this.m_bSuccess,
						m_bLocalSuccess = this.m_bLocalSuccess,
						m_result = this.m_result,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LowBatteryPower_t
	{
		public byte m_nMinutesBatteryLeft; // uint8
		public static LowBatteryPower_t FromPointer( IntPtr p ) { return (LowBatteryPower_t) Marshal.PtrToStructure( p, typeof(LowBatteryPower_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public byte m_nMinutesBatteryLeft; // uint8
			
			public LowBatteryPower_t Get
			{
				get
				{
					return new LowBatteryPower_t()
					{
						m_nMinutesBatteryLeft = this.m_nMinutesBatteryLeft,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamAPICallCompleted_t
	{
		public ulong m_hAsyncCall; // SteamAPICall_t
		public int m_iCallback; // int
		public uint m_cubParam; // uint32
		public static SteamAPICallCompleted_t FromPointer( IntPtr p ) { return (SteamAPICallCompleted_t) Marshal.PtrToStructure( p, typeof(SteamAPICallCompleted_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_hAsyncCall; // SteamAPICall_t
			public int m_iCallback; // int
			public uint m_cubParam; // uint32
			
			public SteamAPICallCompleted_t Get
			{
				get
				{
					return new SteamAPICallCompleted_t()
					{
						m_hAsyncCall = this.m_hAsyncCall,
						m_iCallback = this.m_iCallback,
						m_cubParam = this.m_cubParam,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct CheckFileSignature_t
	{
		public CheckFileSignature m_eCheckFileSignature; // enum ECheckFileSignature
		public static CheckFileSignature_t FromPointer( IntPtr p ) { return (CheckFileSignature_t) Marshal.PtrToStructure( p, typeof(CheckFileSignature_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public CheckFileSignature m_eCheckFileSignature; // enum ECheckFileSignature
			
			public CheckFileSignature_t Get
			{
				get
				{
					return new CheckFileSignature_t()
					{
						m_eCheckFileSignature = this.m_eCheckFileSignature,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GamepadTextInputDismissed_t
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSubmitted; // _Bool
		public uint m_unSubmittedText; // uint32
		public static GamepadTextInputDismissed_t FromPointer( IntPtr p ) { return (GamepadTextInputDismissed_t) Marshal.PtrToStructure( p, typeof(GamepadTextInputDismissed_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bSubmitted; // _Bool
			public uint m_unSubmittedText; // uint32
			
			public GamepadTextInputDismissed_t Get
			{
				get
				{
					return new GamepadTextInputDismissed_t()
					{
						m_bSubmitted = this.m_bSubmitted,
						m_unSubmittedText = this.m_unSubmittedText,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct MatchMakingKeyValuePair_t
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_szKey; // char [256]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_szValue; // char [256]
		public static MatchMakingKeyValuePair_t FromPointer( IntPtr p ) { return (MatchMakingKeyValuePair_t) Marshal.PtrToStructure( p, typeof(MatchMakingKeyValuePair_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string m_szKey; // char [256]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string m_szValue; // char [256]
			
			public MatchMakingKeyValuePair_t Get
			{
				get
				{
					return new MatchMakingKeyValuePair_t()
					{
						m_szKey = this.m_szKey,
						m_szValue = this.m_szValue,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct servernetadr_t
	{
		public ushort m_usConnectionPort; // uint16
		public ushort m_usQueryPort; // uint16
		public uint m_unIP; // uint32
		public static servernetadr_t FromPointer( IntPtr p ) { return (servernetadr_t) Marshal.PtrToStructure( p, typeof(servernetadr_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ushort m_usConnectionPort; // uint16
			public ushort m_usQueryPort; // uint16
			public uint m_unIP; // uint32
			
			public servernetadr_t Get
			{
				get
				{
					return new servernetadr_t()
					{
						m_usConnectionPort = this.m_usConnectionPort,
						m_usQueryPort = this.m_usQueryPort,
						m_unIP = this.m_unIP,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct gameserveritem_t
	{
		public servernetadr_t m_NetAdr; // class servernetadr_t
		public int m_nPing; // int
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bHadSuccessfulResponse; // _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bDoNotRefresh; // _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string m_szGameDir; // char [32]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string m_szMap; // char [32]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string m_szGameDescription; // char [64]
		public uint m_nAppID; // uint32
		public int m_nPlayers; // int
		public int m_nMaxPlayers; // int
		public int m_nBotPlayers; // int
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bPassword; // _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSecure; // _Bool
		public uint m_ulTimeLastPlayed; // uint32
		public int m_nServerVersion; // int
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string m_szServerName; // char [64]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string m_szGameTags; // char [128]
		public ulong m_steamID; // class CSteamID
		public static gameserveritem_t FromPointer( IntPtr p ) { return (gameserveritem_t) Marshal.PtrToStructure( p, typeof(gameserveritem_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public servernetadr_t m_NetAdr; // class servernetadr_t
			public int m_nPing; // int
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bHadSuccessfulResponse; // _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bDoNotRefresh; // _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string m_szGameDir; // char [32]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string m_szMap; // char [32]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string m_szGameDescription; // char [64]
			public uint m_nAppID; // uint32
			public int m_nPlayers; // int
			public int m_nMaxPlayers; // int
			public int m_nBotPlayers; // int
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bPassword; // _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bSecure; // _Bool
			public uint m_ulTimeLastPlayed; // uint32
			public int m_nServerVersion; // int
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string m_szServerName; // char [64]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string m_szGameTags; // char [128]
			public ulong m_steamID; // class CSteamID
			
			public gameserveritem_t Get
			{
				get
				{
					return new gameserveritem_t()
					{
						m_NetAdr = this.m_NetAdr,
						m_nPing = this.m_nPing,
						m_bHadSuccessfulResponse = this.m_bHadSuccessfulResponse,
						m_bDoNotRefresh = this.m_bDoNotRefresh,
						m_szGameDir = this.m_szGameDir,
						m_szMap = this.m_szMap,
						m_szGameDescription = this.m_szGameDescription,
						m_nAppID = this.m_nAppID,
						m_nPlayers = this.m_nPlayers,
						m_nMaxPlayers = this.m_nMaxPlayers,
						m_nBotPlayers = this.m_nBotPlayers,
						m_bPassword = this.m_bPassword,
						m_bSecure = this.m_bSecure,
						m_ulTimeLastPlayed = this.m_ulTimeLastPlayed,
						m_nServerVersion = this.m_nServerVersion,
						m_szServerName = this.m_szServerName,
						m_szGameTags = this.m_szGameTags,
						m_steamID = this.m_steamID,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct FavoritesListChanged_t
	{
		public uint m_nIP; // uint32
		public uint m_nQueryPort; // uint32
		public uint m_nConnPort; // uint32
		public uint m_nAppID; // uint32
		public uint m_nFlags; // uint32
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAdd; // _Bool
		public uint m_unAccountId; // AccountID_t
		public static FavoritesListChanged_t FromPointer( IntPtr p ) { return (FavoritesListChanged_t) Marshal.PtrToStructure( p, typeof(FavoritesListChanged_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_nIP; // uint32
			public uint m_nQueryPort; // uint32
			public uint m_nConnPort; // uint32
			public uint m_nAppID; // uint32
			public uint m_nFlags; // uint32
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bAdd; // _Bool
			public uint m_unAccountId; // AccountID_t
			
			public FavoritesListChanged_t Get
			{
				get
				{
					return new FavoritesListChanged_t()
					{
						m_nIP = this.m_nIP,
						m_nQueryPort = this.m_nQueryPort,
						m_nConnPort = this.m_nConnPort,
						m_nAppID = this.m_nAppID,
						m_nFlags = this.m_nFlags,
						m_bAdd = this.m_bAdd,
						m_unAccountId = this.m_unAccountId,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LobbyInvite_t
	{
		public ulong m_ulSteamIDUser; // uint64
		public ulong m_ulSteamIDLobby; // uint64
		public ulong m_ulGameID; // uint64
		public static LobbyInvite_t FromPointer( IntPtr p ) { return (LobbyInvite_t) Marshal.PtrToStructure( p, typeof(LobbyInvite_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_ulSteamIDUser; // uint64
			public ulong m_ulSteamIDLobby; // uint64
			public ulong m_ulGameID; // uint64
			
			public LobbyInvite_t Get
			{
				get
				{
					return new LobbyInvite_t()
					{
						m_ulSteamIDUser = this.m_ulSteamIDUser,
						m_ulSteamIDLobby = this.m_ulSteamIDLobby,
						m_ulGameID = this.m_ulGameID,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LobbyEnter_t
	{
		public ulong m_ulSteamIDLobby; // uint64
		public uint m_rgfChatPermissions; // uint32
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bLocked; // _Bool
		public uint m_EChatRoomEnterResponse; // uint32
		public static LobbyEnter_t FromPointer( IntPtr p ) { return (LobbyEnter_t) Marshal.PtrToStructure( p, typeof(LobbyEnter_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_ulSteamIDLobby; // uint64
			public uint m_rgfChatPermissions; // uint32
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bLocked; // _Bool
			public uint m_EChatRoomEnterResponse; // uint32
			
			public LobbyEnter_t Get
			{
				get
				{
					return new LobbyEnter_t()
					{
						m_ulSteamIDLobby = this.m_ulSteamIDLobby,
						m_rgfChatPermissions = this.m_rgfChatPermissions,
						m_bLocked = this.m_bLocked,
						m_EChatRoomEnterResponse = this.m_EChatRoomEnterResponse,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LobbyDataUpdate_t
	{
		public ulong m_ulSteamIDLobby; // uint64
		public ulong m_ulSteamIDMember; // uint64
		public byte m_bSuccess; // uint8
		public static LobbyDataUpdate_t FromPointer( IntPtr p ) { return (LobbyDataUpdate_t) Marshal.PtrToStructure( p, typeof(LobbyDataUpdate_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_ulSteamIDLobby; // uint64
			public ulong m_ulSteamIDMember; // uint64
			public byte m_bSuccess; // uint8
			
			public LobbyDataUpdate_t Get
			{
				get
				{
					return new LobbyDataUpdate_t()
					{
						m_ulSteamIDLobby = this.m_ulSteamIDLobby,
						m_ulSteamIDMember = this.m_ulSteamIDMember,
						m_bSuccess = this.m_bSuccess,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LobbyChatUpdate_t
	{
		public ulong m_ulSteamIDLobby; // uint64
		public ulong m_ulSteamIDUserChanged; // uint64
		public ulong m_ulSteamIDMakingChange; // uint64
		public uint m_rgfChatMemberStateChange; // uint32
		public static LobbyChatUpdate_t FromPointer( IntPtr p ) { return (LobbyChatUpdate_t) Marshal.PtrToStructure( p, typeof(LobbyChatUpdate_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_ulSteamIDLobby; // uint64
			public ulong m_ulSteamIDUserChanged; // uint64
			public ulong m_ulSteamIDMakingChange; // uint64
			public uint m_rgfChatMemberStateChange; // uint32
			
			public LobbyChatUpdate_t Get
			{
				get
				{
					return new LobbyChatUpdate_t()
					{
						m_ulSteamIDLobby = this.m_ulSteamIDLobby,
						m_ulSteamIDUserChanged = this.m_ulSteamIDUserChanged,
						m_ulSteamIDMakingChange = this.m_ulSteamIDMakingChange,
						m_rgfChatMemberStateChange = this.m_rgfChatMemberStateChange,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LobbyChatMsg_t
	{
		public ulong m_ulSteamIDLobby; // uint64
		public ulong m_ulSteamIDUser; // uint64
		public byte m_eChatEntryType; // uint8
		public uint m_iChatID; // uint32
		public static LobbyChatMsg_t FromPointer( IntPtr p ) { return (LobbyChatMsg_t) Marshal.PtrToStructure( p, typeof(LobbyChatMsg_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_ulSteamIDLobby; // uint64
			public ulong m_ulSteamIDUser; // uint64
			public byte m_eChatEntryType; // uint8
			public uint m_iChatID; // uint32
			
			public LobbyChatMsg_t Get
			{
				get
				{
					return new LobbyChatMsg_t()
					{
						m_ulSteamIDLobby = this.m_ulSteamIDLobby,
						m_ulSteamIDUser = this.m_ulSteamIDUser,
						m_eChatEntryType = this.m_eChatEntryType,
						m_iChatID = this.m_iChatID,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LobbyGameCreated_t
	{
		public ulong m_ulSteamIDLobby; // uint64
		public ulong m_ulSteamIDGameServer; // uint64
		public uint m_unIP; // uint32
		public ushort m_usPort; // uint16
		public static LobbyGameCreated_t FromPointer( IntPtr p ) { return (LobbyGameCreated_t) Marshal.PtrToStructure( p, typeof(LobbyGameCreated_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_ulSteamIDLobby; // uint64
			public ulong m_ulSteamIDGameServer; // uint64
			public uint m_unIP; // uint32
			public ushort m_usPort; // uint16
			
			public LobbyGameCreated_t Get
			{
				get
				{
					return new LobbyGameCreated_t()
					{
						m_ulSteamIDLobby = this.m_ulSteamIDLobby,
						m_ulSteamIDGameServer = this.m_ulSteamIDGameServer,
						m_unIP = this.m_unIP,
						m_usPort = this.m_usPort,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LobbyMatchList_t
	{
		public uint m_nLobbiesMatching; // uint32
		public static LobbyMatchList_t FromPointer( IntPtr p ) { return (LobbyMatchList_t) Marshal.PtrToStructure( p, typeof(LobbyMatchList_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_nLobbiesMatching; // uint32
			
			public LobbyMatchList_t Get
			{
				get
				{
					return new LobbyMatchList_t()
					{
						m_nLobbiesMatching = this.m_nLobbiesMatching,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LobbyKicked_t
	{
		public ulong m_ulSteamIDLobby; // uint64
		public ulong m_ulSteamIDAdmin; // uint64
		public byte m_bKickedDueToDisconnect; // uint8
		public static LobbyKicked_t FromPointer( IntPtr p ) { return (LobbyKicked_t) Marshal.PtrToStructure( p, typeof(LobbyKicked_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_ulSteamIDLobby; // uint64
			public ulong m_ulSteamIDAdmin; // uint64
			public byte m_bKickedDueToDisconnect; // uint8
			
			public LobbyKicked_t Get
			{
				get
				{
					return new LobbyKicked_t()
					{
						m_ulSteamIDLobby = this.m_ulSteamIDLobby,
						m_ulSteamIDAdmin = this.m_ulSteamIDAdmin,
						m_bKickedDueToDisconnect = this.m_bKickedDueToDisconnect,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LobbyCreated_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_ulSteamIDLobby; // uint64
		public static LobbyCreated_t FromPointer( IntPtr p ) { return (LobbyCreated_t) Marshal.PtrToStructure( p, typeof(LobbyCreated_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_ulSteamIDLobby; // uint64
			
			public LobbyCreated_t Get
			{
				get
				{
					return new LobbyCreated_t()
					{
						m_eResult = this.m_eResult,
						m_ulSteamIDLobby = this.m_ulSteamIDLobby,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct PSNGameBootInviteResult_t
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bGameBootInviteExists; // _Bool
		public ulong m_steamIDLobby; // class CSteamID
		public static PSNGameBootInviteResult_t FromPointer( IntPtr p ) { return (PSNGameBootInviteResult_t) Marshal.PtrToStructure( p, typeof(PSNGameBootInviteResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bGameBootInviteExists; // _Bool
			public ulong m_steamIDLobby; // class CSteamID
			
			public PSNGameBootInviteResult_t Get
			{
				get
				{
					return new PSNGameBootInviteResult_t()
					{
						m_bGameBootInviteExists = this.m_bGameBootInviteExists,
						m_steamIDLobby = this.m_steamIDLobby,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct FavoritesListAccountsUpdated_t
	{
		public Result m_eResult; // enum EResult
		public static FavoritesListAccountsUpdated_t FromPointer( IntPtr p ) { return (FavoritesListAccountsUpdated_t) Marshal.PtrToStructure( p, typeof(FavoritesListAccountsUpdated_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			
			public FavoritesListAccountsUpdated_t Get
			{
				get
				{
					return new FavoritesListAccountsUpdated_t()
					{
						m_eResult = this.m_eResult,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamParamStringArray_t
	{
		public IntPtr m_ppStrings; // const char **
		public int m_nNumStrings; // int32
		public static SteamParamStringArray_t FromPointer( IntPtr p ) { return (SteamParamStringArray_t) Marshal.PtrToStructure( p, typeof(SteamParamStringArray_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public IntPtr m_ppStrings; // const char **
			public int m_nNumStrings; // int32
			
			public SteamParamStringArray_t Get
			{
				get
				{
					return new SteamParamStringArray_t()
					{
						m_ppStrings = this.m_ppStrings,
						m_nNumStrings = this.m_nNumStrings,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageAppSyncedClient_t
	{
		public uint m_nAppID; // AppId_t
		public Result m_eResult; // enum EResult
		public int m_unNumDownloads; // int
		public static RemoteStorageAppSyncedClient_t FromPointer( IntPtr p ) { return (RemoteStorageAppSyncedClient_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncedClient_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_nAppID; // AppId_t
			public Result m_eResult; // enum EResult
			public int m_unNumDownloads; // int
			
			public RemoteStorageAppSyncedClient_t Get
			{
				get
				{
					return new RemoteStorageAppSyncedClient_t()
					{
						m_nAppID = this.m_nAppID,
						m_eResult = this.m_eResult,
						m_unNumDownloads = this.m_unNumDownloads,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageAppSyncedServer_t
	{
		public uint m_nAppID; // AppId_t
		public Result m_eResult; // enum EResult
		public int m_unNumUploads; // int
		public static RemoteStorageAppSyncedServer_t FromPointer( IntPtr p ) { return (RemoteStorageAppSyncedServer_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncedServer_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_nAppID; // AppId_t
			public Result m_eResult; // enum EResult
			public int m_unNumUploads; // int
			
			public RemoteStorageAppSyncedServer_t Get
			{
				get
				{
					return new RemoteStorageAppSyncedServer_t()
					{
						m_nAppID = this.m_nAppID,
						m_eResult = this.m_eResult,
						m_unNumUploads = this.m_unNumUploads,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageAppSyncProgress_t
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string m_rgchCurrentFile; // char [260]
		public uint m_nAppID; // AppId_t
		public uint m_uBytesTransferredThisChunk; // uint32
		public double m_dAppPercentComplete; // double
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUploading; // _Bool
		public static RemoteStorageAppSyncProgress_t FromPointer( IntPtr p ) { return (RemoteStorageAppSyncProgress_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncProgress_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string m_rgchCurrentFile; // char [260]
			public uint m_nAppID; // AppId_t
			public uint m_uBytesTransferredThisChunk; // uint32
			public double m_dAppPercentComplete; // double
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bUploading; // _Bool
			
			public RemoteStorageAppSyncProgress_t Get
			{
				get
				{
					return new RemoteStorageAppSyncProgress_t()
					{
						m_rgchCurrentFile = this.m_rgchCurrentFile,
						m_nAppID = this.m_nAppID,
						m_uBytesTransferredThisChunk = this.m_uBytesTransferredThisChunk,
						m_dAppPercentComplete = this.m_dAppPercentComplete,
						m_bUploading = this.m_bUploading,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageAppSyncStatusCheck_t
	{
		public uint m_nAppID; // AppId_t
		public Result m_eResult; // enum EResult
		public static RemoteStorageAppSyncStatusCheck_t FromPointer( IntPtr p ) { return (RemoteStorageAppSyncStatusCheck_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncStatusCheck_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_nAppID; // AppId_t
			public Result m_eResult; // enum EResult
			
			public RemoteStorageAppSyncStatusCheck_t Get
			{
				get
				{
					return new RemoteStorageAppSyncStatusCheck_t()
					{
						m_nAppID = this.m_nAppID,
						m_eResult = this.m_eResult,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageConflictResolution_t
	{
		public uint m_nAppID; // AppId_t
		public Result m_eResult; // enum EResult
		public static RemoteStorageConflictResolution_t FromPointer( IntPtr p ) { return (RemoteStorageConflictResolution_t) Marshal.PtrToStructure( p, typeof(RemoteStorageConflictResolution_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_nAppID; // AppId_t
			public Result m_eResult; // enum EResult
			
			public RemoteStorageConflictResolution_t Get
			{
				get
				{
					return new RemoteStorageConflictResolution_t()
					{
						m_nAppID = this.m_nAppID,
						m_eResult = this.m_eResult,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageFileShareResult_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_hFile; // UGCHandle_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string m_rgchFilename; // char [260]
		public static RemoteStorageFileShareResult_t FromPointer( IntPtr p ) { return (RemoteStorageFileShareResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageFileShareResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_hFile; // UGCHandle_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string m_rgchFilename; // char [260]
			
			public RemoteStorageFileShareResult_t Get
			{
				get
				{
					return new RemoteStorageFileShareResult_t()
					{
						m_eResult = this.m_eResult,
						m_hFile = this.m_hFile,
						m_rgchFilename = this.m_rgchFilename,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStoragePublishFileResult_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_nPublishedFileId; // PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement; // _Bool
		public static RemoteStoragePublishFileResult_t FromPointer( IntPtr p ) { return (RemoteStoragePublishFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishFileResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_nPublishedFileId; // PublishedFileId_t
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bUserNeedsToAcceptWorkshopLegalAgreement; // _Bool
			
			public RemoteStoragePublishFileResult_t Get
			{
				get
				{
					return new RemoteStoragePublishFileResult_t()
					{
						m_eResult = this.m_eResult,
						m_nPublishedFileId = this.m_nPublishedFileId,
						m_bUserNeedsToAcceptWorkshopLegalAgreement = this.m_bUserNeedsToAcceptWorkshopLegalAgreement,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageDeletePublishedFileResult_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_nPublishedFileId; // PublishedFileId_t
		public static RemoteStorageDeletePublishedFileResult_t FromPointer( IntPtr p ) { return (RemoteStorageDeletePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageDeletePublishedFileResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_nPublishedFileId; // PublishedFileId_t
			
			public RemoteStorageDeletePublishedFileResult_t Get
			{
				get
				{
					return new RemoteStorageDeletePublishedFileResult_t()
					{
						m_eResult = this.m_eResult,
						m_nPublishedFileId = this.m_nPublishedFileId,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageEnumerateUserPublishedFilesResult_t
	{
		public Result m_eResult; // enum EResult
		public int m_nResultsReturned; // int32
		public int m_nTotalResultCount; // int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public ulong[] m_rgPublishedFileId; // PublishedFileId_t [50]
		public static RemoteStorageEnumerateUserPublishedFilesResult_t FromPointer( IntPtr p ) { return (RemoteStorageEnumerateUserPublishedFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserPublishedFilesResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public int m_nResultsReturned; // int32
			public int m_nTotalResultCount; // int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public ulong[] m_rgPublishedFileId; // PublishedFileId_t [50]
			
			public RemoteStorageEnumerateUserPublishedFilesResult_t Get
			{
				get
				{
					return new RemoteStorageEnumerateUserPublishedFilesResult_t()
					{
						m_eResult = this.m_eResult,
						m_nResultsReturned = this.m_nResultsReturned,
						m_nTotalResultCount = this.m_nTotalResultCount,
						m_rgPublishedFileId = this.m_rgPublishedFileId,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageSubscribePublishedFileResult_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_nPublishedFileId; // PublishedFileId_t
		public static RemoteStorageSubscribePublishedFileResult_t FromPointer( IntPtr p ) { return (RemoteStorageSubscribePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageSubscribePublishedFileResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_nPublishedFileId; // PublishedFileId_t
			
			public RemoteStorageSubscribePublishedFileResult_t Get
			{
				get
				{
					return new RemoteStorageSubscribePublishedFileResult_t()
					{
						m_eResult = this.m_eResult,
						m_nPublishedFileId = this.m_nPublishedFileId,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageEnumerateUserSubscribedFilesResult_t
	{
		public Result m_eResult; // enum EResult
		public int m_nResultsReturned; // int32
		public int m_nTotalResultCount; // int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public ulong[] m_rgPublishedFileId; // PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public uint[] m_rgRTimeSubscribed; // uint32 [50]
		public static RemoteStorageEnumerateUserSubscribedFilesResult_t FromPointer( IntPtr p ) { return (RemoteStorageEnumerateUserSubscribedFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public int m_nResultsReturned; // int32
			public int m_nTotalResultCount; // int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public ulong[] m_rgPublishedFileId; // PublishedFileId_t [50]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public uint[] m_rgRTimeSubscribed; // uint32 [50]
			
			public RemoteStorageEnumerateUserSubscribedFilesResult_t Get
			{
				get
				{
					return new RemoteStorageEnumerateUserSubscribedFilesResult_t()
					{
						m_eResult = this.m_eResult,
						m_nResultsReturned = this.m_nResultsReturned,
						m_nTotalResultCount = this.m_nTotalResultCount,
						m_rgPublishedFileId = this.m_rgPublishedFileId,
						m_rgRTimeSubscribed = this.m_rgRTimeSubscribed,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageUnsubscribePublishedFileResult_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_nPublishedFileId; // PublishedFileId_t
		public static RemoteStorageUnsubscribePublishedFileResult_t FromPointer( IntPtr p ) { return (RemoteStorageUnsubscribePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUnsubscribePublishedFileResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_nPublishedFileId; // PublishedFileId_t
			
			public RemoteStorageUnsubscribePublishedFileResult_t Get
			{
				get
				{
					return new RemoteStorageUnsubscribePublishedFileResult_t()
					{
						m_eResult = this.m_eResult,
						m_nPublishedFileId = this.m_nPublishedFileId,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageUpdatePublishedFileResult_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_nPublishedFileId; // PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement; // _Bool
		public static RemoteStorageUpdatePublishedFileResult_t FromPointer( IntPtr p ) { return (RemoteStorageUpdatePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUpdatePublishedFileResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_nPublishedFileId; // PublishedFileId_t
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bUserNeedsToAcceptWorkshopLegalAgreement; // _Bool
			
			public RemoteStorageUpdatePublishedFileResult_t Get
			{
				get
				{
					return new RemoteStorageUpdatePublishedFileResult_t()
					{
						m_eResult = this.m_eResult,
						m_nPublishedFileId = this.m_nPublishedFileId,
						m_bUserNeedsToAcceptWorkshopLegalAgreement = this.m_bUserNeedsToAcceptWorkshopLegalAgreement,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageDownloadUGCResult_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_hFile; // UGCHandle_t
		public uint m_nAppID; // AppId_t
		public int m_nSizeInBytes; // int32
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string m_pchFileName; // char [260]
		public ulong m_ulSteamIDOwner; // uint64
		public static RemoteStorageDownloadUGCResult_t FromPointer( IntPtr p ) { return (RemoteStorageDownloadUGCResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageDownloadUGCResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_hFile; // UGCHandle_t
			public uint m_nAppID; // AppId_t
			public int m_nSizeInBytes; // int32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string m_pchFileName; // char [260]
			public ulong m_ulSteamIDOwner; // uint64
			
			public RemoteStorageDownloadUGCResult_t Get
			{
				get
				{
					return new RemoteStorageDownloadUGCResult_t()
					{
						m_eResult = this.m_eResult,
						m_hFile = this.m_hFile,
						m_nAppID = this.m_nAppID,
						m_nSizeInBytes = this.m_nSizeInBytes,
						m_pchFileName = this.m_pchFileName,
						m_ulSteamIDOwner = this.m_ulSteamIDOwner,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageGetPublishedFileDetailsResult_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_nPublishedFileId; // PublishedFileId_t
		public uint m_nCreatorAppID; // AppId_t
		public uint m_nConsumerAppID; // AppId_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
		public string m_rgchTitle; // char [129]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
		public string m_rgchDescription; // char [8000]
		public ulong m_hFile; // UGCHandle_t
		public ulong m_hPreviewFile; // UGCHandle_t
		public ulong m_ulSteamIDOwner; // uint64
		public uint m_rtimeCreated; // uint32
		public uint m_rtimeUpdated; // uint32
		public RemoteStoragePublishedFileVisibility m_eVisibility; // enum ERemoteStoragePublishedFileVisibility
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bBanned; // _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
		public string m_rgchTags; // char [1025]
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bTagsTruncated; // _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string m_pchFileName; // char [260]
		public int m_nFileSize; // int32
		public int m_nPreviewFileSize; // int32
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_rgchURL; // char [256]
		public WorkshopFileType m_eFileType; // enum EWorkshopFileType
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAcceptedForUse; // _Bool
		public static RemoteStorageGetPublishedFileDetailsResult_t FromPointer( IntPtr p ) { return (RemoteStorageGetPublishedFileDetailsResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageGetPublishedFileDetailsResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_nPublishedFileId; // PublishedFileId_t
			public uint m_nCreatorAppID; // AppId_t
			public uint m_nConsumerAppID; // AppId_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
			public string m_rgchTitle; // char [129]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
			public string m_rgchDescription; // char [8000]
			public ulong m_hFile; // UGCHandle_t
			public ulong m_hPreviewFile; // UGCHandle_t
			public ulong m_ulSteamIDOwner; // uint64
			public uint m_rtimeCreated; // uint32
			public uint m_rtimeUpdated; // uint32
			public RemoteStoragePublishedFileVisibility m_eVisibility; // enum ERemoteStoragePublishedFileVisibility
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bBanned; // _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
			public string m_rgchTags; // char [1025]
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bTagsTruncated; // _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string m_pchFileName; // char [260]
			public int m_nFileSize; // int32
			public int m_nPreviewFileSize; // int32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string m_rgchURL; // char [256]
			public WorkshopFileType m_eFileType; // enum EWorkshopFileType
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bAcceptedForUse; // _Bool
			
			public RemoteStorageGetPublishedFileDetailsResult_t Get
			{
				get
				{
					return new RemoteStorageGetPublishedFileDetailsResult_t()
					{
						m_eResult = this.m_eResult,
						m_nPublishedFileId = this.m_nPublishedFileId,
						m_nCreatorAppID = this.m_nCreatorAppID,
						m_nConsumerAppID = this.m_nConsumerAppID,
						m_rgchTitle = this.m_rgchTitle,
						m_rgchDescription = this.m_rgchDescription,
						m_hFile = this.m_hFile,
						m_hPreviewFile = this.m_hPreviewFile,
						m_ulSteamIDOwner = this.m_ulSteamIDOwner,
						m_rtimeCreated = this.m_rtimeCreated,
						m_rtimeUpdated = this.m_rtimeUpdated,
						m_eVisibility = this.m_eVisibility,
						m_bBanned = this.m_bBanned,
						m_rgchTags = this.m_rgchTags,
						m_bTagsTruncated = this.m_bTagsTruncated,
						m_pchFileName = this.m_pchFileName,
						m_nFileSize = this.m_nFileSize,
						m_nPreviewFileSize = this.m_nPreviewFileSize,
						m_rgchURL = this.m_rgchURL,
						m_eFileType = this.m_eFileType,
						m_bAcceptedForUse = this.m_bAcceptedForUse,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageEnumerateWorkshopFilesResult_t
	{
		public Result m_eResult; // enum EResult
		public int m_nResultsReturned; // int32
		public int m_nTotalResultCount; // int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public ulong[] m_rgPublishedFileId; // PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.R4)]
		public float[] m_rgScore; // float [50]
		public uint m_nAppId; // AppId_t
		public uint m_unStartIndex; // uint32
		public static RemoteStorageEnumerateWorkshopFilesResult_t FromPointer( IntPtr p ) { return (RemoteStorageEnumerateWorkshopFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateWorkshopFilesResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public int m_nResultsReturned; // int32
			public int m_nTotalResultCount; // int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public ulong[] m_rgPublishedFileId; // PublishedFileId_t [50]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.R4)]
			public float[] m_rgScore; // float [50]
			public uint m_nAppId; // AppId_t
			public uint m_unStartIndex; // uint32
			
			public RemoteStorageEnumerateWorkshopFilesResult_t Get
			{
				get
				{
					return new RemoteStorageEnumerateWorkshopFilesResult_t()
					{
						m_eResult = this.m_eResult,
						m_nResultsReturned = this.m_nResultsReturned,
						m_nTotalResultCount = this.m_nTotalResultCount,
						m_rgPublishedFileId = this.m_rgPublishedFileId,
						m_rgScore = this.m_rgScore,
						m_nAppId = this.m_nAppId,
						m_unStartIndex = this.m_unStartIndex,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageGetPublishedItemVoteDetailsResult_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_unPublishedFileId; // PublishedFileId_t
		public int m_nVotesFor; // int32
		public int m_nVotesAgainst; // int32
		public int m_nReports; // int32
		public float m_fScore; // float
		public static RemoteStorageGetPublishedItemVoteDetailsResult_t FromPointer( IntPtr p ) { return (RemoteStorageGetPublishedItemVoteDetailsResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageGetPublishedItemVoteDetailsResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_unPublishedFileId; // PublishedFileId_t
			public int m_nVotesFor; // int32
			public int m_nVotesAgainst; // int32
			public int m_nReports; // int32
			public float m_fScore; // float
			
			public RemoteStorageGetPublishedItemVoteDetailsResult_t Get
			{
				get
				{
					return new RemoteStorageGetPublishedItemVoteDetailsResult_t()
					{
						m_eResult = this.m_eResult,
						m_unPublishedFileId = this.m_unPublishedFileId,
						m_nVotesFor = this.m_nVotesFor,
						m_nVotesAgainst = this.m_nVotesAgainst,
						m_nReports = this.m_nReports,
						m_fScore = this.m_fScore,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStoragePublishedFileSubscribed_t
	{
		public ulong m_nPublishedFileId; // PublishedFileId_t
		public uint m_nAppID; // AppId_t
		public static RemoteStoragePublishedFileSubscribed_t FromPointer( IntPtr p ) { return (RemoteStoragePublishedFileSubscribed_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileSubscribed_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_nPublishedFileId; // PublishedFileId_t
			public uint m_nAppID; // AppId_t
			
			public RemoteStoragePublishedFileSubscribed_t Get
			{
				get
				{
					return new RemoteStoragePublishedFileSubscribed_t()
					{
						m_nPublishedFileId = this.m_nPublishedFileId,
						m_nAppID = this.m_nAppID,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStoragePublishedFileUnsubscribed_t
	{
		public ulong m_nPublishedFileId; // PublishedFileId_t
		public uint m_nAppID; // AppId_t
		public static RemoteStoragePublishedFileUnsubscribed_t FromPointer( IntPtr p ) { return (RemoteStoragePublishedFileUnsubscribed_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileUnsubscribed_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_nPublishedFileId; // PublishedFileId_t
			public uint m_nAppID; // AppId_t
			
			public RemoteStoragePublishedFileUnsubscribed_t Get
			{
				get
				{
					return new RemoteStoragePublishedFileUnsubscribed_t()
					{
						m_nPublishedFileId = this.m_nPublishedFileId,
						m_nAppID = this.m_nAppID,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStoragePublishedFileDeleted_t
	{
		public ulong m_nPublishedFileId; // PublishedFileId_t
		public uint m_nAppID; // AppId_t
		public static RemoteStoragePublishedFileDeleted_t FromPointer( IntPtr p ) { return (RemoteStoragePublishedFileDeleted_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileDeleted_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_nPublishedFileId; // PublishedFileId_t
			public uint m_nAppID; // AppId_t
			
			public RemoteStoragePublishedFileDeleted_t Get
			{
				get
				{
					return new RemoteStoragePublishedFileDeleted_t()
					{
						m_nPublishedFileId = this.m_nPublishedFileId,
						m_nAppID = this.m_nAppID,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageUpdateUserPublishedItemVoteResult_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_nPublishedFileId; // PublishedFileId_t
		public static RemoteStorageUpdateUserPublishedItemVoteResult_t FromPointer( IntPtr p ) { return (RemoteStorageUpdateUserPublishedItemVoteResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUpdateUserPublishedItemVoteResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_nPublishedFileId; // PublishedFileId_t
			
			public RemoteStorageUpdateUserPublishedItemVoteResult_t Get
			{
				get
				{
					return new RemoteStorageUpdateUserPublishedItemVoteResult_t()
					{
						m_eResult = this.m_eResult,
						m_nPublishedFileId = this.m_nPublishedFileId,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageUserVoteDetails_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_nPublishedFileId; // PublishedFileId_t
		public WorkshopVote m_eVote; // enum EWorkshopVote
		public static RemoteStorageUserVoteDetails_t FromPointer( IntPtr p ) { return (RemoteStorageUserVoteDetails_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUserVoteDetails_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_nPublishedFileId; // PublishedFileId_t
			public WorkshopVote m_eVote; // enum EWorkshopVote
			
			public RemoteStorageUserVoteDetails_t Get
			{
				get
				{
					return new RemoteStorageUserVoteDetails_t()
					{
						m_eResult = this.m_eResult,
						m_nPublishedFileId = this.m_nPublishedFileId,
						m_eVote = this.m_eVote,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t
	{
		public Result m_eResult; // enum EResult
		public int m_nResultsReturned; // int32
		public int m_nTotalResultCount; // int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public ulong[] m_rgPublishedFileId; // PublishedFileId_t [50]
		public static RemoteStorageEnumerateUserSharedWorkshopFilesResult_t FromPointer( IntPtr p ) { return (RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public int m_nResultsReturned; // int32
			public int m_nTotalResultCount; // int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public ulong[] m_rgPublishedFileId; // PublishedFileId_t [50]
			
			public RemoteStorageEnumerateUserSharedWorkshopFilesResult_t Get
			{
				get
				{
					return new RemoteStorageEnumerateUserSharedWorkshopFilesResult_t()
					{
						m_eResult = this.m_eResult,
						m_nResultsReturned = this.m_nResultsReturned,
						m_nTotalResultCount = this.m_nTotalResultCount,
						m_rgPublishedFileId = this.m_rgPublishedFileId,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageSetUserPublishedFileActionResult_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_nPublishedFileId; // PublishedFileId_t
		public WorkshopFileAction m_eAction; // enum EWorkshopFileAction
		public static RemoteStorageSetUserPublishedFileActionResult_t FromPointer( IntPtr p ) { return (RemoteStorageSetUserPublishedFileActionResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageSetUserPublishedFileActionResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_nPublishedFileId; // PublishedFileId_t
			public WorkshopFileAction m_eAction; // enum EWorkshopFileAction
			
			public RemoteStorageSetUserPublishedFileActionResult_t Get
			{
				get
				{
					return new RemoteStorageSetUserPublishedFileActionResult_t()
					{
						m_eResult = this.m_eResult,
						m_nPublishedFileId = this.m_nPublishedFileId,
						m_eAction = this.m_eAction,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageEnumeratePublishedFilesByUserActionResult_t
	{
		public Result m_eResult; // enum EResult
		public WorkshopFileAction m_eAction; // enum EWorkshopFileAction
		public int m_nResultsReturned; // int32
		public int m_nTotalResultCount; // int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public ulong[] m_rgPublishedFileId; // PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public uint[] m_rgRTimeUpdated; // uint32 [50]
		public static RemoteStorageEnumeratePublishedFilesByUserActionResult_t FromPointer( IntPtr p ) { return (RemoteStorageEnumeratePublishedFilesByUserActionResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumeratePublishedFilesByUserActionResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public WorkshopFileAction m_eAction; // enum EWorkshopFileAction
			public int m_nResultsReturned; // int32
			public int m_nTotalResultCount; // int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public ulong[] m_rgPublishedFileId; // PublishedFileId_t [50]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public uint[] m_rgRTimeUpdated; // uint32 [50]
			
			public RemoteStorageEnumeratePublishedFilesByUserActionResult_t Get
			{
				get
				{
					return new RemoteStorageEnumeratePublishedFilesByUserActionResult_t()
					{
						m_eResult = this.m_eResult,
						m_eAction = this.m_eAction,
						m_nResultsReturned = this.m_nResultsReturned,
						m_nTotalResultCount = this.m_nTotalResultCount,
						m_rgPublishedFileId = this.m_rgPublishedFileId,
						m_rgRTimeUpdated = this.m_rgRTimeUpdated,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStoragePublishFileProgress_t
	{
		public double m_dPercentFile; // double
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bPreview; // _Bool
		public static RemoteStoragePublishFileProgress_t FromPointer( IntPtr p ) { return (RemoteStoragePublishFileProgress_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishFileProgress_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public double m_dPercentFile; // double
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bPreview; // _Bool
			
			public RemoteStoragePublishFileProgress_t Get
			{
				get
				{
					return new RemoteStoragePublishFileProgress_t()
					{
						m_dPercentFile = this.m_dPercentFile,
						m_bPreview = this.m_bPreview,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStoragePublishedFileUpdated_t
	{
		public ulong m_nPublishedFileId; // PublishedFileId_t
		public uint m_nAppID; // AppId_t
		public ulong m_ulUnused; // uint64
		public static RemoteStoragePublishedFileUpdated_t FromPointer( IntPtr p ) { return (RemoteStoragePublishedFileUpdated_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileUpdated_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_nPublishedFileId; // PublishedFileId_t
			public uint m_nAppID; // AppId_t
			public ulong m_ulUnused; // uint64
			
			public RemoteStoragePublishedFileUpdated_t Get
			{
				get
				{
					return new RemoteStoragePublishedFileUpdated_t()
					{
						m_nPublishedFileId = this.m_nPublishedFileId,
						m_nAppID = this.m_nAppID,
						m_ulUnused = this.m_ulUnused,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageFileWriteAsyncComplete_t
	{
		public Result m_eResult; // enum EResult
		public static RemoteStorageFileWriteAsyncComplete_t FromPointer( IntPtr p ) { return (RemoteStorageFileWriteAsyncComplete_t) Marshal.PtrToStructure( p, typeof(RemoteStorageFileWriteAsyncComplete_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			
			public RemoteStorageFileWriteAsyncComplete_t Get
			{
				get
				{
					return new RemoteStorageFileWriteAsyncComplete_t()
					{
						m_eResult = this.m_eResult,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RemoteStorageFileReadAsyncComplete_t
	{
		public ulong m_hFileReadAsync; // SteamAPICall_t
		public Result m_eResult; // enum EResult
		public uint m_nOffset; // uint32
		public uint m_cubRead; // uint32
		public static RemoteStorageFileReadAsyncComplete_t FromPointer( IntPtr p ) { return (RemoteStorageFileReadAsyncComplete_t) Marshal.PtrToStructure( p, typeof(RemoteStorageFileReadAsyncComplete_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_hFileReadAsync; // SteamAPICall_t
			public Result m_eResult; // enum EResult
			public uint m_nOffset; // uint32
			public uint m_cubRead; // uint32
			
			public RemoteStorageFileReadAsyncComplete_t Get
			{
				get
				{
					return new RemoteStorageFileReadAsyncComplete_t()
					{
						m_hFileReadAsync = this.m_hFileReadAsync,
						m_eResult = this.m_eResult,
						m_nOffset = this.m_nOffset,
						m_cubRead = this.m_cubRead,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct LeaderboardEntry_t
	{
		public ulong m_steamIDUser; // class CSteamID
		public int m_nGlobalRank; // int32
		public int m_nScore; // int32
		public int m_cDetails; // int32
		public ulong m_hUGC; // UGCHandle_t
		public static LeaderboardEntry_t FromPointer( IntPtr p ) { return (LeaderboardEntry_t) Marshal.PtrToStructure( p, typeof(LeaderboardEntry_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_steamIDUser; // class CSteamID
			public int m_nGlobalRank; // int32
			public int m_nScore; // int32
			public int m_cDetails; // int32
			public ulong m_hUGC; // UGCHandle_t
			
			public LeaderboardEntry_t Get
			{
				get
				{
					return new LeaderboardEntry_t()
					{
						m_steamIDUser = this.m_steamIDUser,
						m_nGlobalRank = this.m_nGlobalRank,
						m_nScore = this.m_nScore,
						m_cDetails = this.m_cDetails,
						m_hUGC = this.m_hUGC,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct UserStatsReceived_t
	{
		public ulong m_nGameID; // uint64
		public Result m_eResult; // enum EResult
		public ulong m_steamIDUser; // class CSteamID
		public static UserStatsReceived_t FromPointer( IntPtr p ) { return (UserStatsReceived_t) Marshal.PtrToStructure( p, typeof(UserStatsReceived_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_nGameID; // uint64
			public Result m_eResult; // enum EResult
			public ulong m_steamIDUser; // class CSteamID
			
			public UserStatsReceived_t Get
			{
				get
				{
					return new UserStatsReceived_t()
					{
						m_nGameID = this.m_nGameID,
						m_eResult = this.m_eResult,
						m_steamIDUser = this.m_steamIDUser,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct UserStatsStored_t
	{
		public ulong m_nGameID; // uint64
		public Result m_eResult; // enum EResult
		public static UserStatsStored_t FromPointer( IntPtr p ) { return (UserStatsStored_t) Marshal.PtrToStructure( p, typeof(UserStatsStored_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_nGameID; // uint64
			public Result m_eResult; // enum EResult
			
			public UserStatsStored_t Get
			{
				get
				{
					return new UserStatsStored_t()
					{
						m_nGameID = this.m_nGameID,
						m_eResult = this.m_eResult,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct UserAchievementStored_t
	{
		public ulong m_nGameID; // uint64
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bGroupAchievement; // _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string m_rgchAchievementName; // char [128]
		public uint m_nCurProgress; // uint32
		public uint m_nMaxProgress; // uint32
		public static UserAchievementStored_t FromPointer( IntPtr p ) { return (UserAchievementStored_t) Marshal.PtrToStructure( p, typeof(UserAchievementStored_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_nGameID; // uint64
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bGroupAchievement; // _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string m_rgchAchievementName; // char [128]
			public uint m_nCurProgress; // uint32
			public uint m_nMaxProgress; // uint32
			
			public UserAchievementStored_t Get
			{
				get
				{
					return new UserAchievementStored_t()
					{
						m_nGameID = this.m_nGameID,
						m_bGroupAchievement = this.m_bGroupAchievement,
						m_rgchAchievementName = this.m_rgchAchievementName,
						m_nCurProgress = this.m_nCurProgress,
						m_nMaxProgress = this.m_nMaxProgress,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LeaderboardFindResult_t
	{
		public ulong m_hSteamLeaderboard; // SteamLeaderboard_t
		public byte m_bLeaderboardFound; // uint8
		public static LeaderboardFindResult_t FromPointer( IntPtr p ) { return (LeaderboardFindResult_t) Marshal.PtrToStructure( p, typeof(LeaderboardFindResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_hSteamLeaderboard; // SteamLeaderboard_t
			public byte m_bLeaderboardFound; // uint8
			
			public LeaderboardFindResult_t Get
			{
				get
				{
					return new LeaderboardFindResult_t()
					{
						m_hSteamLeaderboard = this.m_hSteamLeaderboard,
						m_bLeaderboardFound = this.m_bLeaderboardFound,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LeaderboardScoresDownloaded_t
	{
		public ulong m_hSteamLeaderboard; // SteamLeaderboard_t
		public ulong m_hSteamLeaderboardEntries; // SteamLeaderboardEntries_t
		public int m_cEntryCount; // int
		public static LeaderboardScoresDownloaded_t FromPointer( IntPtr p ) { return (LeaderboardScoresDownloaded_t) Marshal.PtrToStructure( p, typeof(LeaderboardScoresDownloaded_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_hSteamLeaderboard; // SteamLeaderboard_t
			public ulong m_hSteamLeaderboardEntries; // SteamLeaderboardEntries_t
			public int m_cEntryCount; // int
			
			public LeaderboardScoresDownloaded_t Get
			{
				get
				{
					return new LeaderboardScoresDownloaded_t()
					{
						m_hSteamLeaderboard = this.m_hSteamLeaderboard,
						m_hSteamLeaderboardEntries = this.m_hSteamLeaderboardEntries,
						m_cEntryCount = this.m_cEntryCount,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LeaderboardScoreUploaded_t
	{
		public byte m_bSuccess; // uint8
		public ulong m_hSteamLeaderboard; // SteamLeaderboard_t
		public int m_nScore; // int32
		public byte m_bScoreChanged; // uint8
		public int m_nGlobalRankNew; // int
		public int m_nGlobalRankPrevious; // int
		public static LeaderboardScoreUploaded_t FromPointer( IntPtr p ) { return (LeaderboardScoreUploaded_t) Marshal.PtrToStructure( p, typeof(LeaderboardScoreUploaded_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public byte m_bSuccess; // uint8
			public ulong m_hSteamLeaderboard; // SteamLeaderboard_t
			public int m_nScore; // int32
			public byte m_bScoreChanged; // uint8
			public int m_nGlobalRankNew; // int
			public int m_nGlobalRankPrevious; // int
			
			public LeaderboardScoreUploaded_t Get
			{
				get
				{
					return new LeaderboardScoreUploaded_t()
					{
						m_bSuccess = this.m_bSuccess,
						m_hSteamLeaderboard = this.m_hSteamLeaderboard,
						m_nScore = this.m_nScore,
						m_bScoreChanged = this.m_bScoreChanged,
						m_nGlobalRankNew = this.m_nGlobalRankNew,
						m_nGlobalRankPrevious = this.m_nGlobalRankPrevious,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct NumberOfCurrentPlayers_t
	{
		public byte m_bSuccess; // uint8
		public int m_cPlayers; // int32
		public static NumberOfCurrentPlayers_t FromPointer( IntPtr p ) { return (NumberOfCurrentPlayers_t) Marshal.PtrToStructure( p, typeof(NumberOfCurrentPlayers_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public byte m_bSuccess; // uint8
			public int m_cPlayers; // int32
			
			public NumberOfCurrentPlayers_t Get
			{
				get
				{
					return new NumberOfCurrentPlayers_t()
					{
						m_bSuccess = this.m_bSuccess,
						m_cPlayers = this.m_cPlayers,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct UserStatsUnloaded_t
	{
		public ulong m_steamIDUser; // class CSteamID
		public static UserStatsUnloaded_t FromPointer( IntPtr p ) { return (UserStatsUnloaded_t) Marshal.PtrToStructure( p, typeof(UserStatsUnloaded_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_steamIDUser; // class CSteamID
			
			public UserStatsUnloaded_t Get
			{
				get
				{
					return new UserStatsUnloaded_t()
					{
						m_steamIDUser = this.m_steamIDUser,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct UserAchievementIconFetched_t
	{
		public ulong m_nGameID; // class CGameID
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string m_rgchAchievementName; // char [128]
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAchieved; // _Bool
		public int m_nIconHandle; // int
		public static UserAchievementIconFetched_t FromPointer( IntPtr p ) { return (UserAchievementIconFetched_t) Marshal.PtrToStructure( p, typeof(UserAchievementIconFetched_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_nGameID; // class CGameID
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string m_rgchAchievementName; // char [128]
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bAchieved; // _Bool
			public int m_nIconHandle; // int
			
			public UserAchievementIconFetched_t Get
			{
				get
				{
					return new UserAchievementIconFetched_t()
					{
						m_nGameID = this.m_nGameID,
						m_rgchAchievementName = this.m_rgchAchievementName,
						m_bAchieved = this.m_bAchieved,
						m_nIconHandle = this.m_nIconHandle,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GlobalAchievementPercentagesReady_t
	{
		public ulong m_nGameID; // uint64
		public Result m_eResult; // enum EResult
		public static GlobalAchievementPercentagesReady_t FromPointer( IntPtr p ) { return (GlobalAchievementPercentagesReady_t) Marshal.PtrToStructure( p, typeof(GlobalAchievementPercentagesReady_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_nGameID; // uint64
			public Result m_eResult; // enum EResult
			
			public GlobalAchievementPercentagesReady_t Get
			{
				get
				{
					return new GlobalAchievementPercentagesReady_t()
					{
						m_nGameID = this.m_nGameID,
						m_eResult = this.m_eResult,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct LeaderboardUGCSet_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_hSteamLeaderboard; // SteamLeaderboard_t
		public static LeaderboardUGCSet_t FromPointer( IntPtr p ) { return (LeaderboardUGCSet_t) Marshal.PtrToStructure( p, typeof(LeaderboardUGCSet_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_hSteamLeaderboard; // SteamLeaderboard_t
			
			public LeaderboardUGCSet_t Get
			{
				get
				{
					return new LeaderboardUGCSet_t()
					{
						m_eResult = this.m_eResult,
						m_hSteamLeaderboard = this.m_hSteamLeaderboard,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct PS3TrophiesInstalled_t
	{
		public ulong m_nGameID; // uint64
		public Result m_eResult; // enum EResult
		public ulong m_ulRequiredDiskSpace; // uint64
		public static PS3TrophiesInstalled_t FromPointer( IntPtr p ) { return (PS3TrophiesInstalled_t) Marshal.PtrToStructure( p, typeof(PS3TrophiesInstalled_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_nGameID; // uint64
			public Result m_eResult; // enum EResult
			public ulong m_ulRequiredDiskSpace; // uint64
			
			public PS3TrophiesInstalled_t Get
			{
				get
				{
					return new PS3TrophiesInstalled_t()
					{
						m_nGameID = this.m_nGameID,
						m_eResult = this.m_eResult,
						m_ulRequiredDiskSpace = this.m_ulRequiredDiskSpace,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GlobalStatsReceived_t
	{
		public ulong m_nGameID; // uint64
		public Result m_eResult; // enum EResult
		public static GlobalStatsReceived_t FromPointer( IntPtr p ) { return (GlobalStatsReceived_t) Marshal.PtrToStructure( p, typeof(GlobalStatsReceived_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_nGameID; // uint64
			public Result m_eResult; // enum EResult
			
			public GlobalStatsReceived_t Get
			{
				get
				{
					return new GlobalStatsReceived_t()
					{
						m_nGameID = this.m_nGameID,
						m_eResult = this.m_eResult,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct DlcInstalled_t
	{
		public uint m_nAppID; // AppId_t
		public static DlcInstalled_t FromPointer( IntPtr p ) { return (DlcInstalled_t) Marshal.PtrToStructure( p, typeof(DlcInstalled_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_nAppID; // AppId_t
			
			public DlcInstalled_t Get
			{
				get
				{
					return new DlcInstalled_t()
					{
						m_nAppID = this.m_nAppID,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct RegisterActivationCodeResponse_t
	{
		public RegisterActivationCodeResult m_eResult; // enum ERegisterActivationCodeResult
		public uint m_unPackageRegistered; // uint32
		public static RegisterActivationCodeResponse_t FromPointer( IntPtr p ) { return (RegisterActivationCodeResponse_t) Marshal.PtrToStructure( p, typeof(RegisterActivationCodeResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public RegisterActivationCodeResult m_eResult; // enum ERegisterActivationCodeResult
			public uint m_unPackageRegistered; // uint32
			
			public RegisterActivationCodeResponse_t Get
			{
				get
				{
					return new RegisterActivationCodeResponse_t()
					{
						m_eResult = this.m_eResult,
						m_unPackageRegistered = this.m_unPackageRegistered,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct AppProofOfPurchaseKeyResponse_t
	{
		public Result m_eResult; // enum EResult
		public uint m_nAppID; // uint32
		public uint m_cchKeyLength; // uint32
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 240)]
		public string m_rgchKey; // char [240]
		public static AppProofOfPurchaseKeyResponse_t FromPointer( IntPtr p ) { return (AppProofOfPurchaseKeyResponse_t) Marshal.PtrToStructure( p, typeof(AppProofOfPurchaseKeyResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public uint m_nAppID; // uint32
			public uint m_cchKeyLength; // uint32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 240)]
			public string m_rgchKey; // char [240]
			
			public AppProofOfPurchaseKeyResponse_t Get
			{
				get
				{
					return new AppProofOfPurchaseKeyResponse_t()
					{
						m_eResult = this.m_eResult,
						m_nAppID = this.m_nAppID,
						m_cchKeyLength = this.m_cchKeyLength,
						m_rgchKey = this.m_rgchKey,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct P2PSessionState_t
	{
		public byte m_bConnectionActive; // uint8
		public byte m_bConnecting; // uint8
		public byte m_eP2PSessionError; // uint8
		public byte m_bUsingRelay; // uint8
		public int m_nBytesQueuedForSend; // int32
		public int m_nPacketsQueuedForSend; // int32
		public uint m_nRemoteIP; // uint32
		public ushort m_nRemotePort; // uint16
		public static P2PSessionState_t FromPointer( IntPtr p ) { return (P2PSessionState_t) Marshal.PtrToStructure( p, typeof(P2PSessionState_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public byte m_bConnectionActive; // uint8
			public byte m_bConnecting; // uint8
			public byte m_eP2PSessionError; // uint8
			public byte m_bUsingRelay; // uint8
			public int m_nBytesQueuedForSend; // int32
			public int m_nPacketsQueuedForSend; // int32
			public uint m_nRemoteIP; // uint32
			public ushort m_nRemotePort; // uint16
			
			public P2PSessionState_t Get
			{
				get
				{
					return new P2PSessionState_t()
					{
						m_bConnectionActive = this.m_bConnectionActive,
						m_bConnecting = this.m_bConnecting,
						m_eP2PSessionError = this.m_eP2PSessionError,
						m_bUsingRelay = this.m_bUsingRelay,
						m_nBytesQueuedForSend = this.m_nBytesQueuedForSend,
						m_nPacketsQueuedForSend = this.m_nPacketsQueuedForSend,
						m_nRemoteIP = this.m_nRemoteIP,
						m_nRemotePort = this.m_nRemotePort,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct P2PSessionRequest_t
	{
		public ulong m_steamIDRemote; // class CSteamID
		public static P2PSessionRequest_t FromPointer( IntPtr p ) { return (P2PSessionRequest_t) Marshal.PtrToStructure( p, typeof(P2PSessionRequest_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_steamIDRemote; // class CSteamID
			
			public P2PSessionRequest_t Get
			{
				get
				{
					return new P2PSessionRequest_t()
					{
						m_steamIDRemote = this.m_steamIDRemote,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct P2PSessionConnectFail_t
	{
		public ulong m_steamIDRemote; // class CSteamID
		public byte m_eP2PSessionError; // uint8
		public static P2PSessionConnectFail_t FromPointer( IntPtr p ) { return (P2PSessionConnectFail_t) Marshal.PtrToStructure( p, typeof(P2PSessionConnectFail_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_steamIDRemote; // class CSteamID
			public byte m_eP2PSessionError; // uint8
			
			public P2PSessionConnectFail_t Get
			{
				get
				{
					return new P2PSessionConnectFail_t()
					{
						m_steamIDRemote = this.m_steamIDRemote,
						m_eP2PSessionError = this.m_eP2PSessionError,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct SocketStatusCallback_t
	{
		public uint m_hSocket; // SNetSocket_t
		public uint m_hListenSocket; // SNetListenSocket_t
		public ulong m_steamIDRemote; // class CSteamID
		public int m_eSNetSocketState; // int
		public static SocketStatusCallback_t FromPointer( IntPtr p ) { return (SocketStatusCallback_t) Marshal.PtrToStructure( p, typeof(SocketStatusCallback_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_hSocket; // SNetSocket_t
			public uint m_hListenSocket; // SNetListenSocket_t
			public ulong m_steamIDRemote; // class CSteamID
			public int m_eSNetSocketState; // int
			
			public SocketStatusCallback_t Get
			{
				get
				{
					return new SocketStatusCallback_t()
					{
						m_hSocket = this.m_hSocket,
						m_hListenSocket = this.m_hListenSocket,
						m_steamIDRemote = this.m_steamIDRemote,
						m_eSNetSocketState = this.m_eSNetSocketState,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct ScreenshotReady_t
	{
		public uint m_hLocal; // ScreenshotHandle
		public Result m_eResult; // enum EResult
		public static ScreenshotReady_t FromPointer( IntPtr p ) { return (ScreenshotReady_t) Marshal.PtrToStructure( p, typeof(ScreenshotReady_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_hLocal; // ScreenshotHandle
			public Result m_eResult; // enum EResult
			
			public ScreenshotReady_t Get
			{
				get
				{
					return new ScreenshotReady_t()
					{
						m_hLocal = this.m_hLocal,
						m_eResult = this.m_eResult,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct VolumeHasChanged_t
	{
		public float m_flNewVolume; // float
		public static VolumeHasChanged_t FromPointer( IntPtr p ) { return (VolumeHasChanged_t) Marshal.PtrToStructure( p, typeof(VolumeHasChanged_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public float m_flNewVolume; // float
			
			public VolumeHasChanged_t Get
			{
				get
				{
					return new VolumeHasChanged_t()
					{
						m_flNewVolume = this.m_flNewVolume,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct MusicPlayerWantsShuffled_t
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bShuffled; // _Bool
		public static MusicPlayerWantsShuffled_t FromPointer( IntPtr p ) { return (MusicPlayerWantsShuffled_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsShuffled_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bShuffled; // _Bool
			
			public MusicPlayerWantsShuffled_t Get
			{
				get
				{
					return new MusicPlayerWantsShuffled_t()
					{
						m_bShuffled = this.m_bShuffled,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct MusicPlayerWantsLooped_t
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bLooped; // _Bool
		public static MusicPlayerWantsLooped_t FromPointer( IntPtr p ) { return (MusicPlayerWantsLooped_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsLooped_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bLooped; // _Bool
			
			public MusicPlayerWantsLooped_t Get
			{
				get
				{
					return new MusicPlayerWantsLooped_t()
					{
						m_bLooped = this.m_bLooped,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct MusicPlayerWantsVolume_t
	{
		public float m_flNewVolume; // float
		public static MusicPlayerWantsVolume_t FromPointer( IntPtr p ) { return (MusicPlayerWantsVolume_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsVolume_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public float m_flNewVolume; // float
			
			public MusicPlayerWantsVolume_t Get
			{
				get
				{
					return new MusicPlayerWantsVolume_t()
					{
						m_flNewVolume = this.m_flNewVolume,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct MusicPlayerSelectsQueueEntry_t
	{
		public int nID; // int
		public static MusicPlayerSelectsQueueEntry_t FromPointer( IntPtr p ) { return (MusicPlayerSelectsQueueEntry_t) Marshal.PtrToStructure( p, typeof(MusicPlayerSelectsQueueEntry_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public int nID; // int
			
			public MusicPlayerSelectsQueueEntry_t Get
			{
				get
				{
					return new MusicPlayerSelectsQueueEntry_t()
					{
						nID = this.nID,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct MusicPlayerSelectsPlaylistEntry_t
	{
		public int nID; // int
		public static MusicPlayerSelectsPlaylistEntry_t FromPointer( IntPtr p ) { return (MusicPlayerSelectsPlaylistEntry_t) Marshal.PtrToStructure( p, typeof(MusicPlayerSelectsPlaylistEntry_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public int nID; // int
			
			public MusicPlayerSelectsPlaylistEntry_t Get
			{
				get
				{
					return new MusicPlayerSelectsPlaylistEntry_t()
					{
						nID = this.nID,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct MusicPlayerWantsPlayingRepeatStatus_t
	{
		public int m_nPlayingRepeatStatus; // int
		public static MusicPlayerWantsPlayingRepeatStatus_t FromPointer( IntPtr p ) { return (MusicPlayerWantsPlayingRepeatStatus_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsPlayingRepeatStatus_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public int m_nPlayingRepeatStatus; // int
			
			public MusicPlayerWantsPlayingRepeatStatus_t Get
			{
				get
				{
					return new MusicPlayerWantsPlayingRepeatStatus_t()
					{
						m_nPlayingRepeatStatus = this.m_nPlayingRepeatStatus,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTTPRequestCompleted_t
	{
		public uint m_hRequest; // HTTPRequestHandle
		public ulong m_ulContextValue; // uint64
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bRequestSuccessful; // _Bool
		public HTTPStatusCode m_eStatusCode; // enum EHTTPStatusCode
		public uint m_unBodySize; // uint32
		public static HTTPRequestCompleted_t FromPointer( IntPtr p ) { return (HTTPRequestCompleted_t) Marshal.PtrToStructure( p, typeof(HTTPRequestCompleted_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_hRequest; // HTTPRequestHandle
			public ulong m_ulContextValue; // uint64
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bRequestSuccessful; // _Bool
			public HTTPStatusCode m_eStatusCode; // enum EHTTPStatusCode
			public uint m_unBodySize; // uint32
			
			public HTTPRequestCompleted_t Get
			{
				get
				{
					return new HTTPRequestCompleted_t()
					{
						m_hRequest = this.m_hRequest,
						m_ulContextValue = this.m_ulContextValue,
						m_bRequestSuccessful = this.m_bRequestSuccessful,
						m_eStatusCode = this.m_eStatusCode,
						m_unBodySize = this.m_unBodySize,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTTPRequestHeadersReceived_t
	{
		public uint m_hRequest; // HTTPRequestHandle
		public ulong m_ulContextValue; // uint64
		public static HTTPRequestHeadersReceived_t FromPointer( IntPtr p ) { return (HTTPRequestHeadersReceived_t) Marshal.PtrToStructure( p, typeof(HTTPRequestHeadersReceived_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_hRequest; // HTTPRequestHandle
			public ulong m_ulContextValue; // uint64
			
			public HTTPRequestHeadersReceived_t Get
			{
				get
				{
					return new HTTPRequestHeadersReceived_t()
					{
						m_hRequest = this.m_hRequest,
						m_ulContextValue = this.m_ulContextValue,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTTPRequestDataReceived_t
	{
		public uint m_hRequest; // HTTPRequestHandle
		public ulong m_ulContextValue; // uint64
		public uint m_cOffset; // uint32
		public uint m_cBytesReceived; // uint32
		public static HTTPRequestDataReceived_t FromPointer( IntPtr p ) { return (HTTPRequestDataReceived_t) Marshal.PtrToStructure( p, typeof(HTTPRequestDataReceived_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_hRequest; // HTTPRequestHandle
			public ulong m_ulContextValue; // uint64
			public uint m_cOffset; // uint32
			public uint m_cBytesReceived; // uint32
			
			public HTTPRequestDataReceived_t Get
			{
				get
				{
					return new HTTPRequestDataReceived_t()
					{
						m_hRequest = this.m_hRequest,
						m_ulContextValue = this.m_ulContextValue,
						m_cOffset = this.m_cOffset,
						m_cBytesReceived = this.m_cBytesReceived,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamUnifiedMessagesSendMethodResult_t
	{
		public ulong m_hHandle; // ClientUnifiedMessageHandle
		public ulong m_unContext; // uint64
		public Result m_eResult; // enum EResult
		public uint m_unResponseSize; // uint32
		public static SteamUnifiedMessagesSendMethodResult_t FromPointer( IntPtr p ) { return (SteamUnifiedMessagesSendMethodResult_t) Marshal.PtrToStructure( p, typeof(SteamUnifiedMessagesSendMethodResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_hHandle; // ClientUnifiedMessageHandle
			public ulong m_unContext; // uint64
			public Result m_eResult; // enum EResult
			public uint m_unResponseSize; // uint32
			
			public SteamUnifiedMessagesSendMethodResult_t Get
			{
				get
				{
					return new SteamUnifiedMessagesSendMethodResult_t()
					{
						m_hHandle = this.m_hHandle,
						m_unContext = this.m_unContext,
						m_eResult = this.m_eResult,
						m_unResponseSize = this.m_unResponseSize,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct ControllerAnalogActionData_t
	{
		public ControllerSourceMode eMode; // enum EControllerSourceMode
		public float x; // float
		public float y; // float
		[MarshalAs(UnmanagedType.I1)]
		public bool bActive; // _Bool
		public static ControllerAnalogActionData_t FromPointer( IntPtr p ) { return (ControllerAnalogActionData_t) Marshal.PtrToStructure( p, typeof(ControllerAnalogActionData_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ControllerSourceMode eMode; // enum EControllerSourceMode
			public float x; // float
			public float y; // float
			[MarshalAs(UnmanagedType.I1)]
			public bool bActive; // _Bool
			
			public ControllerAnalogActionData_t Get
			{
				get
				{
					return new ControllerAnalogActionData_t()
					{
						eMode = this.eMode,
						x = this.x,
						y = this.y,
						bActive = this.bActive,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct ControllerDigitalActionData_t
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool bState; // _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool bActive; // _Bool
		public static ControllerDigitalActionData_t FromPointer( IntPtr p ) { return (ControllerDigitalActionData_t) Marshal.PtrToStructure( p, typeof(ControllerDigitalActionData_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool bState; // _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool bActive; // _Bool
			
			public ControllerDigitalActionData_t Get
			{
				get
				{
					return new ControllerDigitalActionData_t()
					{
						bState = this.bState,
						bActive = this.bActive,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamUGCDetails_t
	{
		public ulong m_nPublishedFileId; // PublishedFileId_t
		public Result m_eResult; // enum EResult
		public WorkshopFileType m_eFileType; // enum EWorkshopFileType
		public uint m_nCreatorAppID; // AppId_t
		public uint m_nConsumerAppID; // AppId_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
		public string m_rgchTitle; // char [129]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
		public string m_rgchDescription; // char [8000]
		public ulong m_ulSteamIDOwner; // uint64
		public uint m_rtimeCreated; // uint32
		public uint m_rtimeUpdated; // uint32
		public uint m_rtimeAddedToUserList; // uint32
		public RemoteStoragePublishedFileVisibility m_eVisibility; // enum ERemoteStoragePublishedFileVisibility
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bBanned; // _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAcceptedForUse; // _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bTagsTruncated; // _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
		public string m_rgchTags; // char [1025]
		public ulong m_hFile; // UGCHandle_t
		public ulong m_hPreviewFile; // UGCHandle_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string m_pchFileName; // char [260]
		public int m_nFileSize; // int32
		public int m_nPreviewFileSize; // int32
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_rgchURL; // char [256]
		public uint m_unVotesUp; // uint32
		public uint m_unVotesDown; // uint32
		public float m_flScore; // float
		public uint m_unNumChildren; // uint32
		public static SteamUGCDetails_t FromPointer( IntPtr p ) { return (SteamUGCDetails_t) Marshal.PtrToStructure( p, typeof(SteamUGCDetails_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_nPublishedFileId; // PublishedFileId_t
			public Result m_eResult; // enum EResult
			public WorkshopFileType m_eFileType; // enum EWorkshopFileType
			public uint m_nCreatorAppID; // AppId_t
			public uint m_nConsumerAppID; // AppId_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
			public string m_rgchTitle; // char [129]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
			public string m_rgchDescription; // char [8000]
			public ulong m_ulSteamIDOwner; // uint64
			public uint m_rtimeCreated; // uint32
			public uint m_rtimeUpdated; // uint32
			public uint m_rtimeAddedToUserList; // uint32
			public RemoteStoragePublishedFileVisibility m_eVisibility; // enum ERemoteStoragePublishedFileVisibility
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bBanned; // _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bAcceptedForUse; // _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bTagsTruncated; // _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
			public string m_rgchTags; // char [1025]
			public ulong m_hFile; // UGCHandle_t
			public ulong m_hPreviewFile; // UGCHandle_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string m_pchFileName; // char [260]
			public int m_nFileSize; // int32
			public int m_nPreviewFileSize; // int32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string m_rgchURL; // char [256]
			public uint m_unVotesUp; // uint32
			public uint m_unVotesDown; // uint32
			public float m_flScore; // float
			public uint m_unNumChildren; // uint32
			
			public SteamUGCDetails_t Get
			{
				get
				{
					return new SteamUGCDetails_t()
					{
						m_nPublishedFileId = this.m_nPublishedFileId,
						m_eResult = this.m_eResult,
						m_eFileType = this.m_eFileType,
						m_nCreatorAppID = this.m_nCreatorAppID,
						m_nConsumerAppID = this.m_nConsumerAppID,
						m_rgchTitle = this.m_rgchTitle,
						m_rgchDescription = this.m_rgchDescription,
						m_ulSteamIDOwner = this.m_ulSteamIDOwner,
						m_rtimeCreated = this.m_rtimeCreated,
						m_rtimeUpdated = this.m_rtimeUpdated,
						m_rtimeAddedToUserList = this.m_rtimeAddedToUserList,
						m_eVisibility = this.m_eVisibility,
						m_bBanned = this.m_bBanned,
						m_bAcceptedForUse = this.m_bAcceptedForUse,
						m_bTagsTruncated = this.m_bTagsTruncated,
						m_rgchTags = this.m_rgchTags,
						m_hFile = this.m_hFile,
						m_hPreviewFile = this.m_hPreviewFile,
						m_pchFileName = this.m_pchFileName,
						m_nFileSize = this.m_nFileSize,
						m_nPreviewFileSize = this.m_nPreviewFileSize,
						m_rgchURL = this.m_rgchURL,
						m_unVotesUp = this.m_unVotesUp,
						m_unVotesDown = this.m_unVotesDown,
						m_flScore = this.m_flScore,
						m_unNumChildren = this.m_unNumChildren,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamUGCQueryCompleted_t
	{
		public ulong m_handle; // UGCQueryHandle_t
		public Result m_eResult; // enum EResult
		public uint m_unNumResultsReturned; // uint32
		public uint m_unTotalMatchingResults; // uint32
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bCachedData; // _Bool
		public static SteamUGCQueryCompleted_t FromPointer( IntPtr p ) { return (SteamUGCQueryCompleted_t) Marshal.PtrToStructure( p, typeof(SteamUGCQueryCompleted_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_handle; // UGCQueryHandle_t
			public Result m_eResult; // enum EResult
			public uint m_unNumResultsReturned; // uint32
			public uint m_unTotalMatchingResults; // uint32
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bCachedData; // _Bool
			
			public SteamUGCQueryCompleted_t Get
			{
				get
				{
					return new SteamUGCQueryCompleted_t()
					{
						m_handle = this.m_handle,
						m_eResult = this.m_eResult,
						m_unNumResultsReturned = this.m_unNumResultsReturned,
						m_unTotalMatchingResults = this.m_unTotalMatchingResults,
						m_bCachedData = this.m_bCachedData,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamUGCRequestUGCDetailsResult_t
	{
		public SteamUGCDetails_t m_details; // struct SteamUGCDetails_t
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bCachedData; // _Bool
		public static SteamUGCRequestUGCDetailsResult_t FromPointer( IntPtr p ) { return (SteamUGCRequestUGCDetailsResult_t) Marshal.PtrToStructure( p, typeof(SteamUGCRequestUGCDetailsResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public SteamUGCDetails_t m_details; // struct SteamUGCDetails_t
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bCachedData; // _Bool
			
			public SteamUGCRequestUGCDetailsResult_t Get
			{
				get
				{
					return new SteamUGCRequestUGCDetailsResult_t()
					{
						m_details = this.m_details,
						m_bCachedData = this.m_bCachedData,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct CreateItemResult_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_nPublishedFileId; // PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement; // _Bool
		public static CreateItemResult_t FromPointer( IntPtr p ) { return (CreateItemResult_t) Marshal.PtrToStructure( p, typeof(CreateItemResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_nPublishedFileId; // PublishedFileId_t
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bUserNeedsToAcceptWorkshopLegalAgreement; // _Bool
			
			public CreateItemResult_t Get
			{
				get
				{
					return new CreateItemResult_t()
					{
						m_eResult = this.m_eResult,
						m_nPublishedFileId = this.m_nPublishedFileId,
						m_bUserNeedsToAcceptWorkshopLegalAgreement = this.m_bUserNeedsToAcceptWorkshopLegalAgreement,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SubmitItemUpdateResult_t
	{
		public Result m_eResult; // enum EResult
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement; // _Bool
		public static SubmitItemUpdateResult_t FromPointer( IntPtr p ) { return (SubmitItemUpdateResult_t) Marshal.PtrToStructure( p, typeof(SubmitItemUpdateResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bUserNeedsToAcceptWorkshopLegalAgreement; // _Bool
			
			public SubmitItemUpdateResult_t Get
			{
				get
				{
					return new SubmitItemUpdateResult_t()
					{
						m_eResult = this.m_eResult,
						m_bUserNeedsToAcceptWorkshopLegalAgreement = this.m_bUserNeedsToAcceptWorkshopLegalAgreement,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct DownloadItemResult_t
	{
		public uint m_unAppID; // AppId_t
		public ulong m_nPublishedFileId; // PublishedFileId_t
		public Result m_eResult; // enum EResult
		public static DownloadItemResult_t FromPointer( IntPtr p ) { return (DownloadItemResult_t) Marshal.PtrToStructure( p, typeof(DownloadItemResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_unAppID; // AppId_t
			public ulong m_nPublishedFileId; // PublishedFileId_t
			public Result m_eResult; // enum EResult
			
			public DownloadItemResult_t Get
			{
				get
				{
					return new DownloadItemResult_t()
					{
						m_unAppID = this.m_unAppID,
						m_nPublishedFileId = this.m_nPublishedFileId,
						m_eResult = this.m_eResult,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct UserFavoriteItemsListChanged_t
	{
		public ulong m_nPublishedFileId; // PublishedFileId_t
		public Result m_eResult; // enum EResult
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bWasAddRequest; // _Bool
		public static UserFavoriteItemsListChanged_t FromPointer( IntPtr p ) { return (UserFavoriteItemsListChanged_t) Marshal.PtrToStructure( p, typeof(UserFavoriteItemsListChanged_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_nPublishedFileId; // PublishedFileId_t
			public Result m_eResult; // enum EResult
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bWasAddRequest; // _Bool
			
			public UserFavoriteItemsListChanged_t Get
			{
				get
				{
					return new UserFavoriteItemsListChanged_t()
					{
						m_nPublishedFileId = this.m_nPublishedFileId,
						m_eResult = this.m_eResult,
						m_bWasAddRequest = this.m_bWasAddRequest,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SetUserItemVoteResult_t
	{
		public ulong m_nPublishedFileId; // PublishedFileId_t
		public Result m_eResult; // enum EResult
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVoteUp; // _Bool
		public static SetUserItemVoteResult_t FromPointer( IntPtr p ) { return (SetUserItemVoteResult_t) Marshal.PtrToStructure( p, typeof(SetUserItemVoteResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_nPublishedFileId; // PublishedFileId_t
			public Result m_eResult; // enum EResult
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bVoteUp; // _Bool
			
			public SetUserItemVoteResult_t Get
			{
				get
				{
					return new SetUserItemVoteResult_t()
					{
						m_nPublishedFileId = this.m_nPublishedFileId,
						m_eResult = this.m_eResult,
						m_bVoteUp = this.m_bVoteUp,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GetUserItemVoteResult_t
	{
		public ulong m_nPublishedFileId; // PublishedFileId_t
		public Result m_eResult; // enum EResult
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVotedUp; // _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVotedDown; // _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVoteSkipped; // _Bool
		public static GetUserItemVoteResult_t FromPointer( IntPtr p ) { return (GetUserItemVoteResult_t) Marshal.PtrToStructure( p, typeof(GetUserItemVoteResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_nPublishedFileId; // PublishedFileId_t
			public Result m_eResult; // enum EResult
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bVotedUp; // _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bVotedDown; // _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bVoteSkipped; // _Bool
			
			public GetUserItemVoteResult_t Get
			{
				get
				{
					return new GetUserItemVoteResult_t()
					{
						m_nPublishedFileId = this.m_nPublishedFileId,
						m_eResult = this.m_eResult,
						m_bVotedUp = this.m_bVotedUp,
						m_bVotedDown = this.m_bVotedDown,
						m_bVoteSkipped = this.m_bVoteSkipped,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamAppInstalled_t
	{
		public uint m_nAppID; // AppId_t
		public static SteamAppInstalled_t FromPointer( IntPtr p ) { return (SteamAppInstalled_t) Marshal.PtrToStructure( p, typeof(SteamAppInstalled_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_nAppID; // AppId_t
			
			public SteamAppInstalled_t Get
			{
				get
				{
					return new SteamAppInstalled_t()
					{
						m_nAppID = this.m_nAppID,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamAppUninstalled_t
	{
		public uint m_nAppID; // AppId_t
		public static SteamAppUninstalled_t FromPointer( IntPtr p ) { return (SteamAppUninstalled_t) Marshal.PtrToStructure( p, typeof(SteamAppUninstalled_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint m_nAppID; // AppId_t
			
			public SteamAppUninstalled_t Get
			{
				get
				{
					return new SteamAppUninstalled_t()
					{
						m_nAppID = this.m_nAppID,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_BrowserReady_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public static HTML_BrowserReady_t FromPointer( IntPtr p ) { return (HTML_BrowserReady_t) Marshal.PtrToStructure( p, typeof(HTML_BrowserReady_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			
			public HTML_BrowserReady_t Get
			{
				get
				{
					return new HTML_BrowserReady_t()
					{
						unBrowserHandle = this.unBrowserHandle,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_NeedsPaint_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public string pBGRA; // const char *
		public uint unWide; // uint32
		public uint unTall; // uint32
		public uint unUpdateX; // uint32
		public uint unUpdateY; // uint32
		public uint unUpdateWide; // uint32
		public uint unUpdateTall; // uint32
		public uint unScrollX; // uint32
		public uint unScrollY; // uint32
		public float flPageScale; // float
		public uint unPageSerial; // uint32
		public static HTML_NeedsPaint_t FromPointer( IntPtr p ) { return (HTML_NeedsPaint_t) Marshal.PtrToStructure( p, typeof(HTML_NeedsPaint_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			public string pBGRA; // const char *
			public uint unWide; // uint32
			public uint unTall; // uint32
			public uint unUpdateX; // uint32
			public uint unUpdateY; // uint32
			public uint unUpdateWide; // uint32
			public uint unUpdateTall; // uint32
			public uint unScrollX; // uint32
			public uint unScrollY; // uint32
			public float flPageScale; // float
			public uint unPageSerial; // uint32
			
			public HTML_NeedsPaint_t Get
			{
				get
				{
					return new HTML_NeedsPaint_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						pBGRA = this.pBGRA,
						unWide = this.unWide,
						unTall = this.unTall,
						unUpdateX = this.unUpdateX,
						unUpdateY = this.unUpdateY,
						unUpdateWide = this.unUpdateWide,
						unUpdateTall = this.unUpdateTall,
						unScrollX = this.unScrollX,
						unScrollY = this.unScrollY,
						flPageScale = this.flPageScale,
						unPageSerial = this.unPageSerial,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_StartRequest_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public string pchURL; // const char *
		public string pchTarget; // const char *
		public string pchPostData; // const char *
		[MarshalAs(UnmanagedType.I1)]
		public bool bIsRedirect; // _Bool
		public static HTML_StartRequest_t FromPointer( IntPtr p ) { return (HTML_StartRequest_t) Marshal.PtrToStructure( p, typeof(HTML_StartRequest_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			public string pchURL; // const char *
			public string pchTarget; // const char *
			public string pchPostData; // const char *
			[MarshalAs(UnmanagedType.I1)]
			public bool bIsRedirect; // _Bool
			
			public HTML_StartRequest_t Get
			{
				get
				{
					return new HTML_StartRequest_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						pchURL = this.pchURL,
						pchTarget = this.pchTarget,
						pchPostData = this.pchPostData,
						bIsRedirect = this.bIsRedirect,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_CloseBrowser_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public static HTML_CloseBrowser_t FromPointer( IntPtr p ) { return (HTML_CloseBrowser_t) Marshal.PtrToStructure( p, typeof(HTML_CloseBrowser_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			
			public HTML_CloseBrowser_t Get
			{
				get
				{
					return new HTML_CloseBrowser_t()
					{
						unBrowserHandle = this.unBrowserHandle,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_URLChanged_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public string pchURL; // const char *
		public string pchPostData; // const char *
		[MarshalAs(UnmanagedType.I1)]
		public bool bIsRedirect; // _Bool
		public string pchPageTitle; // const char *
		[MarshalAs(UnmanagedType.I1)]
		public bool bNewNavigation; // _Bool
		public static HTML_URLChanged_t FromPointer( IntPtr p ) { return (HTML_URLChanged_t) Marshal.PtrToStructure( p, typeof(HTML_URLChanged_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			public string pchURL; // const char *
			public string pchPostData; // const char *
			[MarshalAs(UnmanagedType.I1)]
			public bool bIsRedirect; // _Bool
			public string pchPageTitle; // const char *
			[MarshalAs(UnmanagedType.I1)]
			public bool bNewNavigation; // _Bool
			
			public HTML_URLChanged_t Get
			{
				get
				{
					return new HTML_URLChanged_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						pchURL = this.pchURL,
						pchPostData = this.pchPostData,
						bIsRedirect = this.bIsRedirect,
						pchPageTitle = this.pchPageTitle,
						bNewNavigation = this.bNewNavigation,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_FinishedRequest_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public string pchURL; // const char *
		public string pchPageTitle; // const char *
		public static HTML_FinishedRequest_t FromPointer( IntPtr p ) { return (HTML_FinishedRequest_t) Marshal.PtrToStructure( p, typeof(HTML_FinishedRequest_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			public string pchURL; // const char *
			public string pchPageTitle; // const char *
			
			public HTML_FinishedRequest_t Get
			{
				get
				{
					return new HTML_FinishedRequest_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						pchURL = this.pchURL,
						pchPageTitle = this.pchPageTitle,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_OpenLinkInNewTab_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public string pchURL; // const char *
		public static HTML_OpenLinkInNewTab_t FromPointer( IntPtr p ) { return (HTML_OpenLinkInNewTab_t) Marshal.PtrToStructure( p, typeof(HTML_OpenLinkInNewTab_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			public string pchURL; // const char *
			
			public HTML_OpenLinkInNewTab_t Get
			{
				get
				{
					return new HTML_OpenLinkInNewTab_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						pchURL = this.pchURL,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_ChangedTitle_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public string pchTitle; // const char *
		public static HTML_ChangedTitle_t FromPointer( IntPtr p ) { return (HTML_ChangedTitle_t) Marshal.PtrToStructure( p, typeof(HTML_ChangedTitle_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			public string pchTitle; // const char *
			
			public HTML_ChangedTitle_t Get
			{
				get
				{
					return new HTML_ChangedTitle_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						pchTitle = this.pchTitle,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_SearchResults_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public uint unResults; // uint32
		public uint unCurrentMatch; // uint32
		public static HTML_SearchResults_t FromPointer( IntPtr p ) { return (HTML_SearchResults_t) Marshal.PtrToStructure( p, typeof(HTML_SearchResults_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			public uint unResults; // uint32
			public uint unCurrentMatch; // uint32
			
			public HTML_SearchResults_t Get
			{
				get
				{
					return new HTML_SearchResults_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						unResults = this.unResults,
						unCurrentMatch = this.unCurrentMatch,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_CanGoBackAndForward_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		[MarshalAs(UnmanagedType.I1)]
		public bool bCanGoBack; // _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool bCanGoForward; // _Bool
		public static HTML_CanGoBackAndForward_t FromPointer( IntPtr p ) { return (HTML_CanGoBackAndForward_t) Marshal.PtrToStructure( p, typeof(HTML_CanGoBackAndForward_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			[MarshalAs(UnmanagedType.I1)]
			public bool bCanGoBack; // _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool bCanGoForward; // _Bool
			
			public HTML_CanGoBackAndForward_t Get
			{
				get
				{
					return new HTML_CanGoBackAndForward_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						bCanGoBack = this.bCanGoBack,
						bCanGoForward = this.bCanGoForward,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_HorizontalScroll_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public uint unScrollMax; // uint32
		public uint unScrollCurrent; // uint32
		public float flPageScale; // float
		[MarshalAs(UnmanagedType.I1)]
		public bool bVisible; // _Bool
		public uint unPageSize; // uint32
		public static HTML_HorizontalScroll_t FromPointer( IntPtr p ) { return (HTML_HorizontalScroll_t) Marshal.PtrToStructure( p, typeof(HTML_HorizontalScroll_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			public uint unScrollMax; // uint32
			public uint unScrollCurrent; // uint32
			public float flPageScale; // float
			[MarshalAs(UnmanagedType.I1)]
			public bool bVisible; // _Bool
			public uint unPageSize; // uint32
			
			public HTML_HorizontalScroll_t Get
			{
				get
				{
					return new HTML_HorizontalScroll_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						unScrollMax = this.unScrollMax,
						unScrollCurrent = this.unScrollCurrent,
						flPageScale = this.flPageScale,
						bVisible = this.bVisible,
						unPageSize = this.unPageSize,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_VerticalScroll_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public uint unScrollMax; // uint32
		public uint unScrollCurrent; // uint32
		public float flPageScale; // float
		[MarshalAs(UnmanagedType.I1)]
		public bool bVisible; // _Bool
		public uint unPageSize; // uint32
		public static HTML_VerticalScroll_t FromPointer( IntPtr p ) { return (HTML_VerticalScroll_t) Marshal.PtrToStructure( p, typeof(HTML_VerticalScroll_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			public uint unScrollMax; // uint32
			public uint unScrollCurrent; // uint32
			public float flPageScale; // float
			[MarshalAs(UnmanagedType.I1)]
			public bool bVisible; // _Bool
			public uint unPageSize; // uint32
			
			public HTML_VerticalScroll_t Get
			{
				get
				{
					return new HTML_VerticalScroll_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						unScrollMax = this.unScrollMax,
						unScrollCurrent = this.unScrollCurrent,
						flPageScale = this.flPageScale,
						bVisible = this.bVisible,
						unPageSize = this.unPageSize,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_LinkAtPosition_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public uint x; // uint32
		public uint y; // uint32
		public string pchURL; // const char *
		[MarshalAs(UnmanagedType.I1)]
		public bool bInput; // _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool bLiveLink; // _Bool
		public static HTML_LinkAtPosition_t FromPointer( IntPtr p ) { return (HTML_LinkAtPosition_t) Marshal.PtrToStructure( p, typeof(HTML_LinkAtPosition_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			public uint x; // uint32
			public uint y; // uint32
			public string pchURL; // const char *
			[MarshalAs(UnmanagedType.I1)]
			public bool bInput; // _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool bLiveLink; // _Bool
			
			public HTML_LinkAtPosition_t Get
			{
				get
				{
					return new HTML_LinkAtPosition_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						x = this.x,
						y = this.y,
						pchURL = this.pchURL,
						bInput = this.bInput,
						bLiveLink = this.bLiveLink,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_JSAlert_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public string pchMessage; // const char *
		public static HTML_JSAlert_t FromPointer( IntPtr p ) { return (HTML_JSAlert_t) Marshal.PtrToStructure( p, typeof(HTML_JSAlert_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			public string pchMessage; // const char *
			
			public HTML_JSAlert_t Get
			{
				get
				{
					return new HTML_JSAlert_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						pchMessage = this.pchMessage,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_JSConfirm_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public string pchMessage; // const char *
		public static HTML_JSConfirm_t FromPointer( IntPtr p ) { return (HTML_JSConfirm_t) Marshal.PtrToStructure( p, typeof(HTML_JSConfirm_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			public string pchMessage; // const char *
			
			public HTML_JSConfirm_t Get
			{
				get
				{
					return new HTML_JSConfirm_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						pchMessage = this.pchMessage,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_FileOpenDialog_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public string pchTitle; // const char *
		public string pchInitialFile; // const char *
		public static HTML_FileOpenDialog_t FromPointer( IntPtr p ) { return (HTML_FileOpenDialog_t) Marshal.PtrToStructure( p, typeof(HTML_FileOpenDialog_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			public string pchTitle; // const char *
			public string pchInitialFile; // const char *
			
			public HTML_FileOpenDialog_t Get
			{
				get
				{
					return new HTML_FileOpenDialog_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						pchTitle = this.pchTitle,
						pchInitialFile = this.pchInitialFile,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_NewWindow_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public string pchURL; // const char *
		public uint unX; // uint32
		public uint unY; // uint32
		public uint unWide; // uint32
		public uint unTall; // uint32
		public uint unNewWindow_BrowserHandle; // HHTMLBrowser
		public static HTML_NewWindow_t FromPointer( IntPtr p ) { return (HTML_NewWindow_t) Marshal.PtrToStructure( p, typeof(HTML_NewWindow_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			public string pchURL; // const char *
			public uint unX; // uint32
			public uint unY; // uint32
			public uint unWide; // uint32
			public uint unTall; // uint32
			public uint unNewWindow_BrowserHandle; // HHTMLBrowser
			
			public HTML_NewWindow_t Get
			{
				get
				{
					return new HTML_NewWindow_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						pchURL = this.pchURL,
						unX = this.unX,
						unY = this.unY,
						unWide = this.unWide,
						unTall = this.unTall,
						unNewWindow_BrowserHandle = this.unNewWindow_BrowserHandle,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_SetCursor_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public uint eMouseCursor; // uint32
		public static HTML_SetCursor_t FromPointer( IntPtr p ) { return (HTML_SetCursor_t) Marshal.PtrToStructure( p, typeof(HTML_SetCursor_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			public uint eMouseCursor; // uint32
			
			public HTML_SetCursor_t Get
			{
				get
				{
					return new HTML_SetCursor_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						eMouseCursor = this.eMouseCursor,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_StatusText_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public string pchMsg; // const char *
		public static HTML_StatusText_t FromPointer( IntPtr p ) { return (HTML_StatusText_t) Marshal.PtrToStructure( p, typeof(HTML_StatusText_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			public string pchMsg; // const char *
			
			public HTML_StatusText_t Get
			{
				get
				{
					return new HTML_StatusText_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						pchMsg = this.pchMsg,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_ShowToolTip_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public string pchMsg; // const char *
		public static HTML_ShowToolTip_t FromPointer( IntPtr p ) { return (HTML_ShowToolTip_t) Marshal.PtrToStructure( p, typeof(HTML_ShowToolTip_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			public string pchMsg; // const char *
			
			public HTML_ShowToolTip_t Get
			{
				get
				{
					return new HTML_ShowToolTip_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						pchMsg = this.pchMsg,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_UpdateToolTip_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public string pchMsg; // const char *
		public static HTML_UpdateToolTip_t FromPointer( IntPtr p ) { return (HTML_UpdateToolTip_t) Marshal.PtrToStructure( p, typeof(HTML_UpdateToolTip_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			public string pchMsg; // const char *
			
			public HTML_UpdateToolTip_t Get
			{
				get
				{
					return new HTML_UpdateToolTip_t()
					{
						unBrowserHandle = this.unBrowserHandle,
						pchMsg = this.pchMsg,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct HTML_HideToolTip_t
	{
		public uint unBrowserHandle; // HHTMLBrowser
		public static HTML_HideToolTip_t FromPointer( IntPtr p ) { return (HTML_HideToolTip_t) Marshal.PtrToStructure( p, typeof(HTML_HideToolTip_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public uint unBrowserHandle; // HHTMLBrowser
			
			public HTML_HideToolTip_t Get
			{
				get
				{
					return new HTML_HideToolTip_t()
					{
						unBrowserHandle = this.unBrowserHandle,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamItemDetails_t
	{
		public ulong m_itemId; // SteamItemInstanceID_t
		public int m_iDefinition; // SteamItemDef_t
		public ushort m_unQuantity; // uint16
		public ushort m_unFlags; // uint16
		public static SteamItemDetails_t FromPointer( IntPtr p ) { return (SteamItemDetails_t) Marshal.PtrToStructure( p, typeof(SteamItemDetails_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_itemId; // SteamItemInstanceID_t
			public int m_iDefinition; // SteamItemDef_t
			public ushort m_unQuantity; // uint16
			public ushort m_unFlags; // uint16
			
			public SteamItemDetails_t Get
			{
				get
				{
					return new SteamItemDetails_t()
					{
						m_itemId = this.m_itemId,
						m_iDefinition = this.m_iDefinition,
						m_unQuantity = this.m_unQuantity,
						m_unFlags = this.m_unFlags,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamInventoryResultReady_t
	{
		public int m_handle; // SteamInventoryResult_t
		public Result m_result; // enum EResult
		public static SteamInventoryResultReady_t FromPointer( IntPtr p ) { return (SteamInventoryResultReady_t) Marshal.PtrToStructure( p, typeof(SteamInventoryResultReady_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public int m_handle; // SteamInventoryResult_t
			public Result m_result; // enum EResult
			
			public SteamInventoryResultReady_t Get
			{
				get
				{
					return new SteamInventoryResultReady_t()
					{
						m_handle = this.m_handle,
						m_result = this.m_result,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct SteamInventoryFullUpdate_t
	{
		public int m_handle; // SteamInventoryResult_t
		public static SteamInventoryFullUpdate_t FromPointer( IntPtr p ) { return (SteamInventoryFullUpdate_t) Marshal.PtrToStructure( p, typeof(SteamInventoryFullUpdate_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public int m_handle; // SteamInventoryResult_t
			
			public SteamInventoryFullUpdate_t Get
			{
				get
				{
					return new SteamInventoryFullUpdate_t()
					{
						m_handle = this.m_handle,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct BroadcastUploadStop_t
	{
		public BroadcastUploadResult m_eResult; // enum EBroadcastUploadResult
		public static BroadcastUploadStop_t FromPointer( IntPtr p ) { return (BroadcastUploadStop_t) Marshal.PtrToStructure( p, typeof(BroadcastUploadStop_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public BroadcastUploadResult m_eResult; // enum EBroadcastUploadResult
			
			public BroadcastUploadStop_t Get
			{
				get
				{
					return new BroadcastUploadStop_t()
					{
						m_eResult = this.m_eResult,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GetVideoURLResult_t
	{
		public Result m_eResult; // enum EResult
		public uint m_unVideoAppID; // AppId_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_rgchURL; // char [256]
		public static GetVideoURLResult_t FromPointer( IntPtr p ) { return (GetVideoURLResult_t) Marshal.PtrToStructure( p, typeof(GetVideoURLResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public uint m_unVideoAppID; // AppId_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string m_rgchURL; // char [256]
			
			public GetVideoURLResult_t Get
			{
				get
				{
					return new GetVideoURLResult_t()
					{
						m_eResult = this.m_eResult,
						m_unVideoAppID = this.m_unVideoAppID,
						m_rgchURL = this.m_rgchURL,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct CCallbackBase
	{
		public byte m_nCallbackFlags; // uint8
		public int m_iCallback; // int
		public static CCallbackBase FromPointer( IntPtr p ) { return (CCallbackBase) Marshal.PtrToStructure( p, typeof(CCallbackBase) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public byte m_nCallbackFlags; // uint8
			public int m_iCallback; // int
			
			public CCallbackBase Get
			{
				get
				{
					return new CCallbackBase()
					{
						m_nCallbackFlags = this.m_nCallbackFlags,
						m_iCallback = this.m_iCallback,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GSClientApprove_t
	{
		public ulong m_SteamID; // class CSteamID
		public ulong m_OwnerSteamID; // class CSteamID
		public static GSClientApprove_t FromPointer( IntPtr p ) { return (GSClientApprove_t) Marshal.PtrToStructure( p, typeof(GSClientApprove_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_SteamID; // class CSteamID
			public ulong m_OwnerSteamID; // class CSteamID
			
			public GSClientApprove_t Get
			{
				get
				{
					return new GSClientApprove_t()
					{
						m_SteamID = this.m_SteamID,
						m_OwnerSteamID = this.m_OwnerSteamID,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GSClientDeny_t
	{
		public ulong m_SteamID; // class CSteamID
		public DenyReason m_eDenyReason; // enum EDenyReason
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string m_rgchOptionalText; // char [128]
		public static GSClientDeny_t FromPointer( IntPtr p ) { return (GSClientDeny_t) Marshal.PtrToStructure( p, typeof(GSClientDeny_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_SteamID; // class CSteamID
			public DenyReason m_eDenyReason; // enum EDenyReason
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string m_rgchOptionalText; // char [128]
			
			public GSClientDeny_t Get
			{
				get
				{
					return new GSClientDeny_t()
					{
						m_SteamID = this.m_SteamID,
						m_eDenyReason = this.m_eDenyReason,
						m_rgchOptionalText = this.m_rgchOptionalText,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GSClientKick_t
	{
		public ulong m_SteamID; // class CSteamID
		public DenyReason m_eDenyReason; // enum EDenyReason
		public static GSClientKick_t FromPointer( IntPtr p ) { return (GSClientKick_t) Marshal.PtrToStructure( p, typeof(GSClientKick_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_SteamID; // class CSteamID
			public DenyReason m_eDenyReason; // enum EDenyReason
			
			public GSClientKick_t Get
			{
				get
				{
					return new GSClientKick_t()
					{
						m_SteamID = this.m_SteamID,
						m_eDenyReason = this.m_eDenyReason,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GSClientAchievementStatus_t
	{
		public ulong m_SteamID; // uint64
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string m_pchAchievement; // char [128]
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUnlocked; // _Bool
		public static GSClientAchievementStatus_t FromPointer( IntPtr p ) { return (GSClientAchievementStatus_t) Marshal.PtrToStructure( p, typeof(GSClientAchievementStatus_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_SteamID; // uint64
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string m_pchAchievement; // char [128]
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bUnlocked; // _Bool
			
			public GSClientAchievementStatus_t Get
			{
				get
				{
					return new GSClientAchievementStatus_t()
					{
						m_SteamID = this.m_SteamID,
						m_pchAchievement = this.m_pchAchievement,
						m_bUnlocked = this.m_bUnlocked,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GSPolicyResponse_t
	{
		public byte m_bSecure; // uint8
		public static GSPolicyResponse_t FromPointer( IntPtr p ) { return (GSPolicyResponse_t) Marshal.PtrToStructure( p, typeof(GSPolicyResponse_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public byte m_bSecure; // uint8
			
			public GSPolicyResponse_t Get
			{
				get
				{
					return new GSPolicyResponse_t()
					{
						m_bSecure = this.m_bSecure,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GSGameplayStats_t
	{
		public Result m_eResult; // enum EResult
		public int m_nRank; // int32
		public uint m_unTotalConnects; // uint32
		public uint m_unTotalMinutesPlayed; // uint32
		public static GSGameplayStats_t FromPointer( IntPtr p ) { return (GSGameplayStats_t) Marshal.PtrToStructure( p, typeof(GSGameplayStats_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public int m_nRank; // int32
			public uint m_unTotalConnects; // uint32
			public uint m_unTotalMinutesPlayed; // uint32
			
			public GSGameplayStats_t Get
			{
				get
				{
					return new GSGameplayStats_t()
					{
						m_eResult = this.m_eResult,
						m_nRank = this.m_nRank,
						m_unTotalConnects = this.m_unTotalConnects,
						m_unTotalMinutesPlayed = this.m_unTotalMinutesPlayed,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GSClientGroupStatus_t
	{
		public ulong m_SteamIDUser; // class CSteamID
		public ulong m_SteamIDGroup; // class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bMember; // _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bOfficer; // _Bool
		public static GSClientGroupStatus_t FromPointer( IntPtr p ) { return (GSClientGroupStatus_t) Marshal.PtrToStructure( p, typeof(GSClientGroupStatus_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_SteamIDUser; // class CSteamID
			public ulong m_SteamIDGroup; // class CSteamID
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bMember; // _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bOfficer; // _Bool
			
			public GSClientGroupStatus_t Get
			{
				get
				{
					return new GSClientGroupStatus_t()
					{
						m_SteamIDUser = this.m_SteamIDUser,
						m_SteamIDGroup = this.m_SteamIDGroup,
						m_bMember = this.m_bMember,
						m_bOfficer = this.m_bOfficer,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct GSReputation_t
	{
		public Result m_eResult; // enum EResult
		public uint m_unReputationScore; // uint32
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bBanned; // _Bool
		public uint m_unBannedIP; // uint32
		public ushort m_usBannedPort; // uint16
		public ulong m_ulBannedGameID; // uint64
		public uint m_unBanExpires; // uint32
		public static GSReputation_t FromPointer( IntPtr p ) { return (GSReputation_t) Marshal.PtrToStructure( p, typeof(GSReputation_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public uint m_unReputationScore; // uint32
			[MarshalAs(UnmanagedType.I1)]
			public bool m_bBanned; // _Bool
			public uint m_unBannedIP; // uint32
			public ushort m_usBannedPort; // uint16
			public ulong m_ulBannedGameID; // uint64
			public uint m_unBanExpires; // uint32
			
			public GSReputation_t Get
			{
				get
				{
					return new GSReputation_t()
					{
						m_eResult = this.m_eResult,
						m_unReputationScore = this.m_unReputationScore,
						m_bBanned = this.m_bBanned,
						m_unBannedIP = this.m_unBannedIP,
						m_usBannedPort = this.m_usBannedPort,
						m_ulBannedGameID = this.m_ulBannedGameID,
						m_unBanExpires = this.m_unBanExpires,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	public struct AssociateWithClanResult_t
	{
		public Result m_eResult; // enum EResult
		public static AssociateWithClanResult_t FromPointer( IntPtr p ) { return (AssociateWithClanResult_t) Marshal.PtrToStructure( p, typeof(AssociateWithClanResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			
			public AssociateWithClanResult_t Get
			{
				get
				{
					return new AssociateWithClanResult_t()
					{
						m_eResult = this.m_eResult,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct ComputeNewPlayerCompatibilityResult_t
	{
		public Result m_eResult; // enum EResult
		public int m_cPlayersThatDontLikeCandidate; // int
		public int m_cPlayersThatCandidateDoesntLike; // int
		public int m_cClanPlayersThatDontLikeCandidate; // int
		public ulong m_SteamIDCandidate; // class CSteamID
		public static ComputeNewPlayerCompatibilityResult_t FromPointer( IntPtr p ) { return (ComputeNewPlayerCompatibilityResult_t) Marshal.PtrToStructure( p, typeof(ComputeNewPlayerCompatibilityResult_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public int m_cPlayersThatDontLikeCandidate; // int
			public int m_cPlayersThatCandidateDoesntLike; // int
			public int m_cClanPlayersThatDontLikeCandidate; // int
			public ulong m_SteamIDCandidate; // class CSteamID
			
			public ComputeNewPlayerCompatibilityResult_t Get
			{
				get
				{
					return new ComputeNewPlayerCompatibilityResult_t()
					{
						m_eResult = this.m_eResult,
						m_cPlayersThatDontLikeCandidate = this.m_cPlayersThatDontLikeCandidate,
						m_cPlayersThatCandidateDoesntLike = this.m_cPlayersThatCandidateDoesntLike,
						m_cClanPlayersThatDontLikeCandidate = this.m_cClanPlayersThatDontLikeCandidate,
						m_SteamIDCandidate = this.m_SteamIDCandidate,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GSStatsReceived_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_steamIDUser; // class CSteamID
		public static GSStatsReceived_t FromPointer( IntPtr p ) { return (GSStatsReceived_t) Marshal.PtrToStructure( p, typeof(GSStatsReceived_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_steamIDUser; // class CSteamID
			
			public GSStatsReceived_t Get
			{
				get
				{
					return new GSStatsReceived_t()
					{
						m_eResult = this.m_eResult,
						m_steamIDUser = this.m_steamIDUser,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GSStatsStored_t
	{
		public Result m_eResult; // enum EResult
		public ulong m_steamIDUser; // class CSteamID
		public static GSStatsStored_t FromPointer( IntPtr p ) { return (GSStatsStored_t) Marshal.PtrToStructure( p, typeof(GSStatsStored_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public Result m_eResult; // enum EResult
			public ulong m_steamIDUser; // class CSteamID
			
			public GSStatsStored_t Get
			{
				get
				{
					return new GSStatsStored_t()
					{
						m_eResult = this.m_eResult,
						m_steamIDUser = this.m_steamIDUser,
					};
				}
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	public struct GSStatsUnloaded_t
	{
		public ulong m_steamIDUser; // class CSteamID
		public static GSStatsUnloaded_t FromPointer( IntPtr p ) { return (GSStatsUnloaded_t) Marshal.PtrToStructure( p, typeof(GSStatsUnloaded_t) ); }
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct PackSmall
		{
			public ulong m_steamIDUser; // class CSteamID
			
			public GSStatsUnloaded_t Get
			{
				get
				{
					return new GSStatsUnloaded_t()
					{
						m_steamIDUser = this.m_steamIDUser,
					};
				}
			}
		}
	}
	
}
