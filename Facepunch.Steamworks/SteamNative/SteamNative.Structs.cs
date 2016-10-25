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
			
			public static implicit operator  ValvePackingSentinel_t (  ValvePackingSentinel_t.PackSmall d )
			{
				return new ValvePackingSentinel_t()
				{
					m_u32 = d.m_u32,
					m_u64 = d.m_u64,
					m_u16 = d.m_u16,
					m_d = d.m_d,
				};
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
			
			public static implicit operator  CallbackMsg_t (  CallbackMsg_t.PackSmall d )
			{
				return new CallbackMsg_t()
				{
					m_hSteamUser = d.m_hSteamUser,
					m_iCallback = d.m_iCallback,
					m_pubParam = d.m_pubParam,
					m_cubParam = d.m_cubParam,
				};
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
			
			public static implicit operator  SteamServerConnectFailure_t (  SteamServerConnectFailure_t.PackSmall d )
			{
				return new SteamServerConnectFailure_t()
				{
					m_eResult = d.m_eResult,
					m_bStillRetrying = d.m_bStillRetrying,
				};
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
			
			public static implicit operator  SteamServersDisconnected_t (  SteamServersDisconnected_t.PackSmall d )
			{
				return new SteamServersDisconnected_t()
				{
					m_eResult = d.m_eResult,
				};
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
			
			public static implicit operator  ClientGameServerDeny_t (  ClientGameServerDeny_t.PackSmall d )
			{
				return new ClientGameServerDeny_t()
				{
					m_uAppID = d.m_uAppID,
					m_unGameServerIP = d.m_unGameServerIP,
					m_usGameServerPort = d.m_usGameServerPort,
					m_bSecure = d.m_bSecure,
					m_uReason = d.m_uReason,
				};
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
			
			public static implicit operator  ValidateAuthTicketResponse_t (  ValidateAuthTicketResponse_t.PackSmall d )
			{
				return new ValidateAuthTicketResponse_t()
				{
					m_SteamID = d.m_SteamID,
					m_eAuthSessionResponse = d.m_eAuthSessionResponse,
					m_OwnerSteamID = d.m_OwnerSteamID,
				};
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
			
			public static implicit operator  MicroTxnAuthorizationResponse_t (  MicroTxnAuthorizationResponse_t.PackSmall d )
			{
				return new MicroTxnAuthorizationResponse_t()
				{
					m_unAppID = d.m_unAppID,
					m_ulOrderID = d.m_ulOrderID,
					m_bAuthorized = d.m_bAuthorized,
				};
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
			
			public static implicit operator  EncryptedAppTicketResponse_t (  EncryptedAppTicketResponse_t.PackSmall d )
			{
				return new EncryptedAppTicketResponse_t()
				{
					m_eResult = d.m_eResult,
				};
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
			
			public static implicit operator  GetAuthSessionTicketResponse_t (  GetAuthSessionTicketResponse_t.PackSmall d )
			{
				return new GetAuthSessionTicketResponse_t()
				{
					m_hAuthTicket = d.m_hAuthTicket,
					m_eResult = d.m_eResult,
				};
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
			
			public static implicit operator  GameWebCallback_t (  GameWebCallback_t.PackSmall d )
			{
				return new GameWebCallback_t()
				{
					m_szURL = d.m_szURL,
				};
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
			
			public static implicit operator  StoreAuthURLResponse_t (  StoreAuthURLResponse_t.PackSmall d )
			{
				return new StoreAuthURLResponse_t()
				{
					m_szURL = d.m_szURL,
				};
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
			
			public static implicit operator  FriendGameInfo_t (  FriendGameInfo_t.PackSmall d )
			{
				return new FriendGameInfo_t()
				{
					m_gameID = d.m_gameID,
					m_unGameIP = d.m_unGameIP,
					m_usGamePort = d.m_usGamePort,
					m_usQueryPort = d.m_usQueryPort,
					m_steamIDLobby = d.m_steamIDLobby,
				};
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
			
			public static implicit operator  FriendSessionStateInfo_t (  FriendSessionStateInfo_t.PackSmall d )
			{
				return new FriendSessionStateInfo_t()
				{
					m_uiOnlineSessionInstances = d.m_uiOnlineSessionInstances,
					m_uiPublishedToFriendsSessionInstance = d.m_uiPublishedToFriendsSessionInstance,
				};
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
			
			public static implicit operator  PersonaStateChange_t (  PersonaStateChange_t.PackSmall d )
			{
				return new PersonaStateChange_t()
				{
					m_ulSteamID = d.m_ulSteamID,
					m_nChangeFlags = d.m_nChangeFlags,
				};
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
			
			public static implicit operator  GameOverlayActivated_t (  GameOverlayActivated_t.PackSmall d )
			{
				return new GameOverlayActivated_t()
				{
					m_bActive = d.m_bActive,
				};
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
			
			public static implicit operator  GameServerChangeRequested_t (  GameServerChangeRequested_t.PackSmall d )
			{
				return new GameServerChangeRequested_t()
				{
					m_rgchServer = d.m_rgchServer,
					m_rgchPassword = d.m_rgchPassword,
				};
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
			
			public static implicit operator  GameLobbyJoinRequested_t (  GameLobbyJoinRequested_t.PackSmall d )
			{
				return new GameLobbyJoinRequested_t()
				{
					m_steamIDLobby = d.m_steamIDLobby,
					m_steamIDFriend = d.m_steamIDFriend,
				};
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
			
			public static implicit operator  AvatarImageLoaded_t (  AvatarImageLoaded_t.PackSmall d )
			{
				return new AvatarImageLoaded_t()
				{
					m_steamID = d.m_steamID,
					m_iImage = d.m_iImage,
					m_iWide = d.m_iWide,
					m_iTall = d.m_iTall,
				};
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
			
			public static implicit operator  ClanOfficerListResponse_t (  ClanOfficerListResponse_t.PackSmall d )
			{
				return new ClanOfficerListResponse_t()
				{
					m_steamIDClan = d.m_steamIDClan,
					m_cOfficers = d.m_cOfficers,
					m_bSuccess = d.m_bSuccess,
				};
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
			
			public static implicit operator  FriendRichPresenceUpdate_t (  FriendRichPresenceUpdate_t.PackSmall d )
			{
				return new FriendRichPresenceUpdate_t()
				{
					m_steamIDFriend = d.m_steamIDFriend,
					m_nAppID = d.m_nAppID,
				};
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
			
			public static implicit operator  GameRichPresenceJoinRequested_t (  GameRichPresenceJoinRequested_t.PackSmall d )
			{
				return new GameRichPresenceJoinRequested_t()
				{
					m_steamIDFriend = d.m_steamIDFriend,
					m_rgchConnect = d.m_rgchConnect,
				};
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
			
			public static implicit operator  GameConnectedClanChatMsg_t (  GameConnectedClanChatMsg_t.PackSmall d )
			{
				return new GameConnectedClanChatMsg_t()
				{
					m_steamIDClanChat = d.m_steamIDClanChat,
					m_steamIDUser = d.m_steamIDUser,
					m_iMessageID = d.m_iMessageID,
				};
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
			
			public static implicit operator  GameConnectedChatJoin_t (  GameConnectedChatJoin_t.PackSmall d )
			{
				return new GameConnectedChatJoin_t()
				{
					m_steamIDClanChat = d.m_steamIDClanChat,
					m_steamIDUser = d.m_steamIDUser,
				};
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
			
			public static implicit operator  GameConnectedChatLeave_t (  GameConnectedChatLeave_t.PackSmall d )
			{
				return new GameConnectedChatLeave_t()
				{
					m_steamIDClanChat = d.m_steamIDClanChat,
					m_steamIDUser = d.m_steamIDUser,
					m_bKicked = d.m_bKicked,
					m_bDropped = d.m_bDropped,
				};
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
			
			public static implicit operator  DownloadClanActivityCountsResult_t (  DownloadClanActivityCountsResult_t.PackSmall d )
			{
				return new DownloadClanActivityCountsResult_t()
				{
					m_bSuccess = d.m_bSuccess,
				};
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
			
			public static implicit operator  JoinClanChatRoomCompletionResult_t (  JoinClanChatRoomCompletionResult_t.PackSmall d )
			{
				return new JoinClanChatRoomCompletionResult_t()
				{
					m_steamIDClanChat = d.m_steamIDClanChat,
					m_eChatRoomEnterResponse = d.m_eChatRoomEnterResponse,
				};
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
			
			public static implicit operator  GameConnectedFriendChatMsg_t (  GameConnectedFriendChatMsg_t.PackSmall d )
			{
				return new GameConnectedFriendChatMsg_t()
				{
					m_steamIDUser = d.m_steamIDUser,
					m_iMessageID = d.m_iMessageID,
				};
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
			
			public static implicit operator  FriendsGetFollowerCount_t (  FriendsGetFollowerCount_t.PackSmall d )
			{
				return new FriendsGetFollowerCount_t()
				{
					m_eResult = d.m_eResult,
					m_steamID = d.m_steamID,
					m_nCount = d.m_nCount,
				};
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
			
			public static implicit operator  FriendsIsFollowing_t (  FriendsIsFollowing_t.PackSmall d )
			{
				return new FriendsIsFollowing_t()
				{
					m_eResult = d.m_eResult,
					m_steamID = d.m_steamID,
					m_bIsFollowing = d.m_bIsFollowing,
				};
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
			
			public static implicit operator  FriendsEnumerateFollowingList_t (  FriendsEnumerateFollowingList_t.PackSmall d )
			{
				return new FriendsEnumerateFollowingList_t()
				{
					m_eResult = d.m_eResult,
					m_rgSteamID = d.m_rgSteamID,
					m_nResultsReturned = d.m_nResultsReturned,
					m_nTotalResultCount = d.m_nTotalResultCount,
				};
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
			
			public static implicit operator  SetPersonaNameResponse_t (  SetPersonaNameResponse_t.PackSmall d )
			{
				return new SetPersonaNameResponse_t()
				{
					m_bSuccess = d.m_bSuccess,
					m_bLocalSuccess = d.m_bLocalSuccess,
					m_result = d.m_result,
				};
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
			
			public static implicit operator  LowBatteryPower_t (  LowBatteryPower_t.PackSmall d )
			{
				return new LowBatteryPower_t()
				{
					m_nMinutesBatteryLeft = d.m_nMinutesBatteryLeft,
				};
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
			
			public static implicit operator  SteamAPICallCompleted_t (  SteamAPICallCompleted_t.PackSmall d )
			{
				return new SteamAPICallCompleted_t()
				{
					m_hAsyncCall = d.m_hAsyncCall,
					m_iCallback = d.m_iCallback,
					m_cubParam = d.m_cubParam,
				};
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
			
			public static implicit operator  CheckFileSignature_t (  CheckFileSignature_t.PackSmall d )
			{
				return new CheckFileSignature_t()
				{
					m_eCheckFileSignature = d.m_eCheckFileSignature,
				};
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
			
			public static implicit operator  GamepadTextInputDismissed_t (  GamepadTextInputDismissed_t.PackSmall d )
			{
				return new GamepadTextInputDismissed_t()
				{
					m_bSubmitted = d.m_bSubmitted,
					m_unSubmittedText = d.m_unSubmittedText,
				};
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
			
			public static implicit operator  MatchMakingKeyValuePair_t (  MatchMakingKeyValuePair_t.PackSmall d )
			{
				return new MatchMakingKeyValuePair_t()
				{
					m_szKey = d.m_szKey,
					m_szValue = d.m_szValue,
				};
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
			
			public static implicit operator  servernetadr_t (  servernetadr_t.PackSmall d )
			{
				return new servernetadr_t()
				{
					m_usConnectionPort = d.m_usConnectionPort,
					m_usQueryPort = d.m_usQueryPort,
					m_unIP = d.m_unIP,
				};
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
			
			public static implicit operator  gameserveritem_t (  gameserveritem_t.PackSmall d )
			{
				return new gameserveritem_t()
				{
					m_NetAdr = d.m_NetAdr,
					m_nPing = d.m_nPing,
					m_bHadSuccessfulResponse = d.m_bHadSuccessfulResponse,
					m_bDoNotRefresh = d.m_bDoNotRefresh,
					m_szGameDir = d.m_szGameDir,
					m_szMap = d.m_szMap,
					m_szGameDescription = d.m_szGameDescription,
					m_nAppID = d.m_nAppID,
					m_nPlayers = d.m_nPlayers,
					m_nMaxPlayers = d.m_nMaxPlayers,
					m_nBotPlayers = d.m_nBotPlayers,
					m_bPassword = d.m_bPassword,
					m_bSecure = d.m_bSecure,
					m_ulTimeLastPlayed = d.m_ulTimeLastPlayed,
					m_nServerVersion = d.m_nServerVersion,
					m_szServerName = d.m_szServerName,
					m_szGameTags = d.m_szGameTags,
					m_steamID = d.m_steamID,
				};
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
			
			public static implicit operator  FavoritesListChanged_t (  FavoritesListChanged_t.PackSmall d )
			{
				return new FavoritesListChanged_t()
				{
					m_nIP = d.m_nIP,
					m_nQueryPort = d.m_nQueryPort,
					m_nConnPort = d.m_nConnPort,
					m_nAppID = d.m_nAppID,
					m_nFlags = d.m_nFlags,
					m_bAdd = d.m_bAdd,
					m_unAccountId = d.m_unAccountId,
				};
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
			
			public static implicit operator  LobbyInvite_t (  LobbyInvite_t.PackSmall d )
			{
				return new LobbyInvite_t()
				{
					m_ulSteamIDUser = d.m_ulSteamIDUser,
					m_ulSteamIDLobby = d.m_ulSteamIDLobby,
					m_ulGameID = d.m_ulGameID,
				};
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
			
			public static implicit operator  LobbyEnter_t (  LobbyEnter_t.PackSmall d )
			{
				return new LobbyEnter_t()
				{
					m_ulSteamIDLobby = d.m_ulSteamIDLobby,
					m_rgfChatPermissions = d.m_rgfChatPermissions,
					m_bLocked = d.m_bLocked,
					m_EChatRoomEnterResponse = d.m_EChatRoomEnterResponse,
				};
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
			
			public static implicit operator  LobbyDataUpdate_t (  LobbyDataUpdate_t.PackSmall d )
			{
				return new LobbyDataUpdate_t()
				{
					m_ulSteamIDLobby = d.m_ulSteamIDLobby,
					m_ulSteamIDMember = d.m_ulSteamIDMember,
					m_bSuccess = d.m_bSuccess,
				};
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
			
			public static implicit operator  LobbyChatUpdate_t (  LobbyChatUpdate_t.PackSmall d )
			{
				return new LobbyChatUpdate_t()
				{
					m_ulSteamIDLobby = d.m_ulSteamIDLobby,
					m_ulSteamIDUserChanged = d.m_ulSteamIDUserChanged,
					m_ulSteamIDMakingChange = d.m_ulSteamIDMakingChange,
					m_rgfChatMemberStateChange = d.m_rgfChatMemberStateChange,
				};
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
			
			public static implicit operator  LobbyChatMsg_t (  LobbyChatMsg_t.PackSmall d )
			{
				return new LobbyChatMsg_t()
				{
					m_ulSteamIDLobby = d.m_ulSteamIDLobby,
					m_ulSteamIDUser = d.m_ulSteamIDUser,
					m_eChatEntryType = d.m_eChatEntryType,
					m_iChatID = d.m_iChatID,
				};
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
			
			public static implicit operator  LobbyGameCreated_t (  LobbyGameCreated_t.PackSmall d )
			{
				return new LobbyGameCreated_t()
				{
					m_ulSteamIDLobby = d.m_ulSteamIDLobby,
					m_ulSteamIDGameServer = d.m_ulSteamIDGameServer,
					m_unIP = d.m_unIP,
					m_usPort = d.m_usPort,
				};
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
			
			public static implicit operator  LobbyMatchList_t (  LobbyMatchList_t.PackSmall d )
			{
				return new LobbyMatchList_t()
				{
					m_nLobbiesMatching = d.m_nLobbiesMatching,
				};
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
			
			public static implicit operator  LobbyKicked_t (  LobbyKicked_t.PackSmall d )
			{
				return new LobbyKicked_t()
				{
					m_ulSteamIDLobby = d.m_ulSteamIDLobby,
					m_ulSteamIDAdmin = d.m_ulSteamIDAdmin,
					m_bKickedDueToDisconnect = d.m_bKickedDueToDisconnect,
				};
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
			
			public static implicit operator  LobbyCreated_t (  LobbyCreated_t.PackSmall d )
			{
				return new LobbyCreated_t()
				{
					m_eResult = d.m_eResult,
					m_ulSteamIDLobby = d.m_ulSteamIDLobby,
				};
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
			
			public static implicit operator  PSNGameBootInviteResult_t (  PSNGameBootInviteResult_t.PackSmall d )
			{
				return new PSNGameBootInviteResult_t()
				{
					m_bGameBootInviteExists = d.m_bGameBootInviteExists,
					m_steamIDLobby = d.m_steamIDLobby,
				};
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
			
			public static implicit operator  FavoritesListAccountsUpdated_t (  FavoritesListAccountsUpdated_t.PackSmall d )
			{
				return new FavoritesListAccountsUpdated_t()
				{
					m_eResult = d.m_eResult,
				};
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
			
			public static implicit operator  SteamParamStringArray_t (  SteamParamStringArray_t.PackSmall d )
			{
				return new SteamParamStringArray_t()
				{
					m_ppStrings = d.m_ppStrings,
					m_nNumStrings = d.m_nNumStrings,
				};
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
			
			public static implicit operator  RemoteStorageAppSyncedClient_t (  RemoteStorageAppSyncedClient_t.PackSmall d )
			{
				return new RemoteStorageAppSyncedClient_t()
				{
					m_nAppID = d.m_nAppID,
					m_eResult = d.m_eResult,
					m_unNumDownloads = d.m_unNumDownloads,
				};
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
			
			public static implicit operator  RemoteStorageAppSyncedServer_t (  RemoteStorageAppSyncedServer_t.PackSmall d )
			{
				return new RemoteStorageAppSyncedServer_t()
				{
					m_nAppID = d.m_nAppID,
					m_eResult = d.m_eResult,
					m_unNumUploads = d.m_unNumUploads,
				};
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
			
			public static implicit operator  RemoteStorageAppSyncProgress_t (  RemoteStorageAppSyncProgress_t.PackSmall d )
			{
				return new RemoteStorageAppSyncProgress_t()
				{
					m_rgchCurrentFile = d.m_rgchCurrentFile,
					m_nAppID = d.m_nAppID,
					m_uBytesTransferredThisChunk = d.m_uBytesTransferredThisChunk,
					m_dAppPercentComplete = d.m_dAppPercentComplete,
					m_bUploading = d.m_bUploading,
				};
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
			
			public static implicit operator  RemoteStorageAppSyncStatusCheck_t (  RemoteStorageAppSyncStatusCheck_t.PackSmall d )
			{
				return new RemoteStorageAppSyncStatusCheck_t()
				{
					m_nAppID = d.m_nAppID,
					m_eResult = d.m_eResult,
				};
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
			
			public static implicit operator  RemoteStorageConflictResolution_t (  RemoteStorageConflictResolution_t.PackSmall d )
			{
				return new RemoteStorageConflictResolution_t()
				{
					m_nAppID = d.m_nAppID,
					m_eResult = d.m_eResult,
				};
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
			
			public static implicit operator  RemoteStorageFileShareResult_t (  RemoteStorageFileShareResult_t.PackSmall d )
			{
				return new RemoteStorageFileShareResult_t()
				{
					m_eResult = d.m_eResult,
					m_hFile = d.m_hFile,
					m_rgchFilename = d.m_rgchFilename,
				};
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
			
			public static implicit operator  RemoteStoragePublishFileResult_t (  RemoteStoragePublishFileResult_t.PackSmall d )
			{
				return new RemoteStoragePublishFileResult_t()
				{
					m_eResult = d.m_eResult,
					m_nPublishedFileId = d.m_nPublishedFileId,
					m_bUserNeedsToAcceptWorkshopLegalAgreement = d.m_bUserNeedsToAcceptWorkshopLegalAgreement,
				};
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
			
			public static implicit operator  RemoteStorageDeletePublishedFileResult_t (  RemoteStorageDeletePublishedFileResult_t.PackSmall d )
			{
				return new RemoteStorageDeletePublishedFileResult_t()
				{
					m_eResult = d.m_eResult,
					m_nPublishedFileId = d.m_nPublishedFileId,
				};
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
			
			public static implicit operator  RemoteStorageEnumerateUserPublishedFilesResult_t (  RemoteStorageEnumerateUserPublishedFilesResult_t.PackSmall d )
			{
				return new RemoteStorageEnumerateUserPublishedFilesResult_t()
				{
					m_eResult = d.m_eResult,
					m_nResultsReturned = d.m_nResultsReturned,
					m_nTotalResultCount = d.m_nTotalResultCount,
					m_rgPublishedFileId = d.m_rgPublishedFileId,
				};
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
			
			public static implicit operator  RemoteStorageSubscribePublishedFileResult_t (  RemoteStorageSubscribePublishedFileResult_t.PackSmall d )
			{
				return new RemoteStorageSubscribePublishedFileResult_t()
				{
					m_eResult = d.m_eResult,
					m_nPublishedFileId = d.m_nPublishedFileId,
				};
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
			
			public static implicit operator  RemoteStorageEnumerateUserSubscribedFilesResult_t (  RemoteStorageEnumerateUserSubscribedFilesResult_t.PackSmall d )
			{
				return new RemoteStorageEnumerateUserSubscribedFilesResult_t()
				{
					m_eResult = d.m_eResult,
					m_nResultsReturned = d.m_nResultsReturned,
					m_nTotalResultCount = d.m_nTotalResultCount,
					m_rgPublishedFileId = d.m_rgPublishedFileId,
					m_rgRTimeSubscribed = d.m_rgRTimeSubscribed,
				};
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
			
			public static implicit operator  RemoteStorageUnsubscribePublishedFileResult_t (  RemoteStorageUnsubscribePublishedFileResult_t.PackSmall d )
			{
				return new RemoteStorageUnsubscribePublishedFileResult_t()
				{
					m_eResult = d.m_eResult,
					m_nPublishedFileId = d.m_nPublishedFileId,
				};
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
			
			public static implicit operator  RemoteStorageUpdatePublishedFileResult_t (  RemoteStorageUpdatePublishedFileResult_t.PackSmall d )
			{
				return new RemoteStorageUpdatePublishedFileResult_t()
				{
					m_eResult = d.m_eResult,
					m_nPublishedFileId = d.m_nPublishedFileId,
					m_bUserNeedsToAcceptWorkshopLegalAgreement = d.m_bUserNeedsToAcceptWorkshopLegalAgreement,
				};
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
			
			public static implicit operator  RemoteStorageDownloadUGCResult_t (  RemoteStorageDownloadUGCResult_t.PackSmall d )
			{
				return new RemoteStorageDownloadUGCResult_t()
				{
					m_eResult = d.m_eResult,
					m_hFile = d.m_hFile,
					m_nAppID = d.m_nAppID,
					m_nSizeInBytes = d.m_nSizeInBytes,
					m_pchFileName = d.m_pchFileName,
					m_ulSteamIDOwner = d.m_ulSteamIDOwner,
				};
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
			
			public static implicit operator  RemoteStorageGetPublishedFileDetailsResult_t (  RemoteStorageGetPublishedFileDetailsResult_t.PackSmall d )
			{
				return new RemoteStorageGetPublishedFileDetailsResult_t()
				{
					m_eResult = d.m_eResult,
					m_nPublishedFileId = d.m_nPublishedFileId,
					m_nCreatorAppID = d.m_nCreatorAppID,
					m_nConsumerAppID = d.m_nConsumerAppID,
					m_rgchTitle = d.m_rgchTitle,
					m_rgchDescription = d.m_rgchDescription,
					m_hFile = d.m_hFile,
					m_hPreviewFile = d.m_hPreviewFile,
					m_ulSteamIDOwner = d.m_ulSteamIDOwner,
					m_rtimeCreated = d.m_rtimeCreated,
					m_rtimeUpdated = d.m_rtimeUpdated,
					m_eVisibility = d.m_eVisibility,
					m_bBanned = d.m_bBanned,
					m_rgchTags = d.m_rgchTags,
					m_bTagsTruncated = d.m_bTagsTruncated,
					m_pchFileName = d.m_pchFileName,
					m_nFileSize = d.m_nFileSize,
					m_nPreviewFileSize = d.m_nPreviewFileSize,
					m_rgchURL = d.m_rgchURL,
					m_eFileType = d.m_eFileType,
					m_bAcceptedForUse = d.m_bAcceptedForUse,
				};
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
			
			public static implicit operator  RemoteStorageEnumerateWorkshopFilesResult_t (  RemoteStorageEnumerateWorkshopFilesResult_t.PackSmall d )
			{
				return new RemoteStorageEnumerateWorkshopFilesResult_t()
				{
					m_eResult = d.m_eResult,
					m_nResultsReturned = d.m_nResultsReturned,
					m_nTotalResultCount = d.m_nTotalResultCount,
					m_rgPublishedFileId = d.m_rgPublishedFileId,
					m_rgScore = d.m_rgScore,
					m_nAppId = d.m_nAppId,
					m_unStartIndex = d.m_unStartIndex,
				};
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
			
			public static implicit operator  RemoteStorageGetPublishedItemVoteDetailsResult_t (  RemoteStorageGetPublishedItemVoteDetailsResult_t.PackSmall d )
			{
				return new RemoteStorageGetPublishedItemVoteDetailsResult_t()
				{
					m_eResult = d.m_eResult,
					m_unPublishedFileId = d.m_unPublishedFileId,
					m_nVotesFor = d.m_nVotesFor,
					m_nVotesAgainst = d.m_nVotesAgainst,
					m_nReports = d.m_nReports,
					m_fScore = d.m_fScore,
				};
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
			
			public static implicit operator  RemoteStoragePublishedFileSubscribed_t (  RemoteStoragePublishedFileSubscribed_t.PackSmall d )
			{
				return new RemoteStoragePublishedFileSubscribed_t()
				{
					m_nPublishedFileId = d.m_nPublishedFileId,
					m_nAppID = d.m_nAppID,
				};
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
			
			public static implicit operator  RemoteStoragePublishedFileUnsubscribed_t (  RemoteStoragePublishedFileUnsubscribed_t.PackSmall d )
			{
				return new RemoteStoragePublishedFileUnsubscribed_t()
				{
					m_nPublishedFileId = d.m_nPublishedFileId,
					m_nAppID = d.m_nAppID,
				};
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
			
			public static implicit operator  RemoteStoragePublishedFileDeleted_t (  RemoteStoragePublishedFileDeleted_t.PackSmall d )
			{
				return new RemoteStoragePublishedFileDeleted_t()
				{
					m_nPublishedFileId = d.m_nPublishedFileId,
					m_nAppID = d.m_nAppID,
				};
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
			
			public static implicit operator  RemoteStorageUpdateUserPublishedItemVoteResult_t (  RemoteStorageUpdateUserPublishedItemVoteResult_t.PackSmall d )
			{
				return new RemoteStorageUpdateUserPublishedItemVoteResult_t()
				{
					m_eResult = d.m_eResult,
					m_nPublishedFileId = d.m_nPublishedFileId,
				};
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
			
			public static implicit operator  RemoteStorageUserVoteDetails_t (  RemoteStorageUserVoteDetails_t.PackSmall d )
			{
				return new RemoteStorageUserVoteDetails_t()
				{
					m_eResult = d.m_eResult,
					m_nPublishedFileId = d.m_nPublishedFileId,
					m_eVote = d.m_eVote,
				};
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
			
			public static implicit operator  RemoteStorageEnumerateUserSharedWorkshopFilesResult_t (  RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.PackSmall d )
			{
				return new RemoteStorageEnumerateUserSharedWorkshopFilesResult_t()
				{
					m_eResult = d.m_eResult,
					m_nResultsReturned = d.m_nResultsReturned,
					m_nTotalResultCount = d.m_nTotalResultCount,
					m_rgPublishedFileId = d.m_rgPublishedFileId,
				};
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
			
			public static implicit operator  RemoteStorageSetUserPublishedFileActionResult_t (  RemoteStorageSetUserPublishedFileActionResult_t.PackSmall d )
			{
				return new RemoteStorageSetUserPublishedFileActionResult_t()
				{
					m_eResult = d.m_eResult,
					m_nPublishedFileId = d.m_nPublishedFileId,
					m_eAction = d.m_eAction,
				};
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
			
			public static implicit operator  RemoteStorageEnumeratePublishedFilesByUserActionResult_t (  RemoteStorageEnumeratePublishedFilesByUserActionResult_t.PackSmall d )
			{
				return new RemoteStorageEnumeratePublishedFilesByUserActionResult_t()
				{
					m_eResult = d.m_eResult,
					m_eAction = d.m_eAction,
					m_nResultsReturned = d.m_nResultsReturned,
					m_nTotalResultCount = d.m_nTotalResultCount,
					m_rgPublishedFileId = d.m_rgPublishedFileId,
					m_rgRTimeUpdated = d.m_rgRTimeUpdated,
				};
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
			
			public static implicit operator  RemoteStoragePublishFileProgress_t (  RemoteStoragePublishFileProgress_t.PackSmall d )
			{
				return new RemoteStoragePublishFileProgress_t()
				{
					m_dPercentFile = d.m_dPercentFile,
					m_bPreview = d.m_bPreview,
				};
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
			
			public static implicit operator  RemoteStoragePublishedFileUpdated_t (  RemoteStoragePublishedFileUpdated_t.PackSmall d )
			{
				return new RemoteStoragePublishedFileUpdated_t()
				{
					m_nPublishedFileId = d.m_nPublishedFileId,
					m_nAppID = d.m_nAppID,
					m_ulUnused = d.m_ulUnused,
				};
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
			
			public static implicit operator  RemoteStorageFileWriteAsyncComplete_t (  RemoteStorageFileWriteAsyncComplete_t.PackSmall d )
			{
				return new RemoteStorageFileWriteAsyncComplete_t()
				{
					m_eResult = d.m_eResult,
				};
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
			
			public static implicit operator  RemoteStorageFileReadAsyncComplete_t (  RemoteStorageFileReadAsyncComplete_t.PackSmall d )
			{
				return new RemoteStorageFileReadAsyncComplete_t()
				{
					m_hFileReadAsync = d.m_hFileReadAsync,
					m_eResult = d.m_eResult,
					m_nOffset = d.m_nOffset,
					m_cubRead = d.m_cubRead,
				};
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
			
			public static implicit operator  LeaderboardEntry_t (  LeaderboardEntry_t.PackSmall d )
			{
				return new LeaderboardEntry_t()
				{
					m_steamIDUser = d.m_steamIDUser,
					m_nGlobalRank = d.m_nGlobalRank,
					m_nScore = d.m_nScore,
					m_cDetails = d.m_cDetails,
					m_hUGC = d.m_hUGC,
				};
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
			
			public static implicit operator  UserStatsReceived_t (  UserStatsReceived_t.PackSmall d )
			{
				return new UserStatsReceived_t()
				{
					m_nGameID = d.m_nGameID,
					m_eResult = d.m_eResult,
					m_steamIDUser = d.m_steamIDUser,
				};
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
			
			public static implicit operator  UserStatsStored_t (  UserStatsStored_t.PackSmall d )
			{
				return new UserStatsStored_t()
				{
					m_nGameID = d.m_nGameID,
					m_eResult = d.m_eResult,
				};
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
			
			public static implicit operator  UserAchievementStored_t (  UserAchievementStored_t.PackSmall d )
			{
				return new UserAchievementStored_t()
				{
					m_nGameID = d.m_nGameID,
					m_bGroupAchievement = d.m_bGroupAchievement,
					m_rgchAchievementName = d.m_rgchAchievementName,
					m_nCurProgress = d.m_nCurProgress,
					m_nMaxProgress = d.m_nMaxProgress,
				};
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
			
			public static implicit operator  LeaderboardFindResult_t (  LeaderboardFindResult_t.PackSmall d )
			{
				return new LeaderboardFindResult_t()
				{
					m_hSteamLeaderboard = d.m_hSteamLeaderboard,
					m_bLeaderboardFound = d.m_bLeaderboardFound,
				};
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
			
			public static implicit operator  LeaderboardScoresDownloaded_t (  LeaderboardScoresDownloaded_t.PackSmall d )
			{
				return new LeaderboardScoresDownloaded_t()
				{
					m_hSteamLeaderboard = d.m_hSteamLeaderboard,
					m_hSteamLeaderboardEntries = d.m_hSteamLeaderboardEntries,
					m_cEntryCount = d.m_cEntryCount,
				};
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
			
			public static implicit operator  LeaderboardScoreUploaded_t (  LeaderboardScoreUploaded_t.PackSmall d )
			{
				return new LeaderboardScoreUploaded_t()
				{
					m_bSuccess = d.m_bSuccess,
					m_hSteamLeaderboard = d.m_hSteamLeaderboard,
					m_nScore = d.m_nScore,
					m_bScoreChanged = d.m_bScoreChanged,
					m_nGlobalRankNew = d.m_nGlobalRankNew,
					m_nGlobalRankPrevious = d.m_nGlobalRankPrevious,
				};
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
			
			public static implicit operator  NumberOfCurrentPlayers_t (  NumberOfCurrentPlayers_t.PackSmall d )
			{
				return new NumberOfCurrentPlayers_t()
				{
					m_bSuccess = d.m_bSuccess,
					m_cPlayers = d.m_cPlayers,
				};
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
			
			public static implicit operator  UserStatsUnloaded_t (  UserStatsUnloaded_t.PackSmall d )
			{
				return new UserStatsUnloaded_t()
				{
					m_steamIDUser = d.m_steamIDUser,
				};
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
			
			public static implicit operator  UserAchievementIconFetched_t (  UserAchievementIconFetched_t.PackSmall d )
			{
				return new UserAchievementIconFetched_t()
				{
					m_nGameID = d.m_nGameID,
					m_rgchAchievementName = d.m_rgchAchievementName,
					m_bAchieved = d.m_bAchieved,
					m_nIconHandle = d.m_nIconHandle,
				};
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
			
			public static implicit operator  GlobalAchievementPercentagesReady_t (  GlobalAchievementPercentagesReady_t.PackSmall d )
			{
				return new GlobalAchievementPercentagesReady_t()
				{
					m_nGameID = d.m_nGameID,
					m_eResult = d.m_eResult,
				};
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
			
			public static implicit operator  LeaderboardUGCSet_t (  LeaderboardUGCSet_t.PackSmall d )
			{
				return new LeaderboardUGCSet_t()
				{
					m_eResult = d.m_eResult,
					m_hSteamLeaderboard = d.m_hSteamLeaderboard,
				};
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
			
			public static implicit operator  PS3TrophiesInstalled_t (  PS3TrophiesInstalled_t.PackSmall d )
			{
				return new PS3TrophiesInstalled_t()
				{
					m_nGameID = d.m_nGameID,
					m_eResult = d.m_eResult,
					m_ulRequiredDiskSpace = d.m_ulRequiredDiskSpace,
				};
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
			
			public static implicit operator  GlobalStatsReceived_t (  GlobalStatsReceived_t.PackSmall d )
			{
				return new GlobalStatsReceived_t()
				{
					m_nGameID = d.m_nGameID,
					m_eResult = d.m_eResult,
				};
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
			
			public static implicit operator  DlcInstalled_t (  DlcInstalled_t.PackSmall d )
			{
				return new DlcInstalled_t()
				{
					m_nAppID = d.m_nAppID,
				};
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
			
			public static implicit operator  RegisterActivationCodeResponse_t (  RegisterActivationCodeResponse_t.PackSmall d )
			{
				return new RegisterActivationCodeResponse_t()
				{
					m_eResult = d.m_eResult,
					m_unPackageRegistered = d.m_unPackageRegistered,
				};
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
			
			public static implicit operator  AppProofOfPurchaseKeyResponse_t (  AppProofOfPurchaseKeyResponse_t.PackSmall d )
			{
				return new AppProofOfPurchaseKeyResponse_t()
				{
					m_eResult = d.m_eResult,
					m_nAppID = d.m_nAppID,
					m_cchKeyLength = d.m_cchKeyLength,
					m_rgchKey = d.m_rgchKey,
				};
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
			
			public static implicit operator  P2PSessionState_t (  P2PSessionState_t.PackSmall d )
			{
				return new P2PSessionState_t()
				{
					m_bConnectionActive = d.m_bConnectionActive,
					m_bConnecting = d.m_bConnecting,
					m_eP2PSessionError = d.m_eP2PSessionError,
					m_bUsingRelay = d.m_bUsingRelay,
					m_nBytesQueuedForSend = d.m_nBytesQueuedForSend,
					m_nPacketsQueuedForSend = d.m_nPacketsQueuedForSend,
					m_nRemoteIP = d.m_nRemoteIP,
					m_nRemotePort = d.m_nRemotePort,
				};
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
			
			public static implicit operator  P2PSessionRequest_t (  P2PSessionRequest_t.PackSmall d )
			{
				return new P2PSessionRequest_t()
				{
					m_steamIDRemote = d.m_steamIDRemote,
				};
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
			
			public static implicit operator  P2PSessionConnectFail_t (  P2PSessionConnectFail_t.PackSmall d )
			{
				return new P2PSessionConnectFail_t()
				{
					m_steamIDRemote = d.m_steamIDRemote,
					m_eP2PSessionError = d.m_eP2PSessionError,
				};
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
			
			public static implicit operator  SocketStatusCallback_t (  SocketStatusCallback_t.PackSmall d )
			{
				return new SocketStatusCallback_t()
				{
					m_hSocket = d.m_hSocket,
					m_hListenSocket = d.m_hListenSocket,
					m_steamIDRemote = d.m_steamIDRemote,
					m_eSNetSocketState = d.m_eSNetSocketState,
				};
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
			
			public static implicit operator  ScreenshotReady_t (  ScreenshotReady_t.PackSmall d )
			{
				return new ScreenshotReady_t()
				{
					m_hLocal = d.m_hLocal,
					m_eResult = d.m_eResult,
				};
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
			
			public static implicit operator  VolumeHasChanged_t (  VolumeHasChanged_t.PackSmall d )
			{
				return new VolumeHasChanged_t()
				{
					m_flNewVolume = d.m_flNewVolume,
				};
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
			
			public static implicit operator  MusicPlayerWantsShuffled_t (  MusicPlayerWantsShuffled_t.PackSmall d )
			{
				return new MusicPlayerWantsShuffled_t()
				{
					m_bShuffled = d.m_bShuffled,
				};
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
			
			public static implicit operator  MusicPlayerWantsLooped_t (  MusicPlayerWantsLooped_t.PackSmall d )
			{
				return new MusicPlayerWantsLooped_t()
				{
					m_bLooped = d.m_bLooped,
				};
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
			
			public static implicit operator  MusicPlayerWantsVolume_t (  MusicPlayerWantsVolume_t.PackSmall d )
			{
				return new MusicPlayerWantsVolume_t()
				{
					m_flNewVolume = d.m_flNewVolume,
				};
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
			
			public static implicit operator  MusicPlayerSelectsQueueEntry_t (  MusicPlayerSelectsQueueEntry_t.PackSmall d )
			{
				return new MusicPlayerSelectsQueueEntry_t()
				{
					nID = d.nID,
				};
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
			
			public static implicit operator  MusicPlayerSelectsPlaylistEntry_t (  MusicPlayerSelectsPlaylistEntry_t.PackSmall d )
			{
				return new MusicPlayerSelectsPlaylistEntry_t()
				{
					nID = d.nID,
				};
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
			
			public static implicit operator  MusicPlayerWantsPlayingRepeatStatus_t (  MusicPlayerWantsPlayingRepeatStatus_t.PackSmall d )
			{
				return new MusicPlayerWantsPlayingRepeatStatus_t()
				{
					m_nPlayingRepeatStatus = d.m_nPlayingRepeatStatus,
				};
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
			
			public static implicit operator  HTTPRequestCompleted_t (  HTTPRequestCompleted_t.PackSmall d )
			{
				return new HTTPRequestCompleted_t()
				{
					m_hRequest = d.m_hRequest,
					m_ulContextValue = d.m_ulContextValue,
					m_bRequestSuccessful = d.m_bRequestSuccessful,
					m_eStatusCode = d.m_eStatusCode,
					m_unBodySize = d.m_unBodySize,
				};
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
			
			public static implicit operator  HTTPRequestHeadersReceived_t (  HTTPRequestHeadersReceived_t.PackSmall d )
			{
				return new HTTPRequestHeadersReceived_t()
				{
					m_hRequest = d.m_hRequest,
					m_ulContextValue = d.m_ulContextValue,
				};
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
			
			public static implicit operator  HTTPRequestDataReceived_t (  HTTPRequestDataReceived_t.PackSmall d )
			{
				return new HTTPRequestDataReceived_t()
				{
					m_hRequest = d.m_hRequest,
					m_ulContextValue = d.m_ulContextValue,
					m_cOffset = d.m_cOffset,
					m_cBytesReceived = d.m_cBytesReceived,
				};
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
			
			public static implicit operator  SteamUnifiedMessagesSendMethodResult_t (  SteamUnifiedMessagesSendMethodResult_t.PackSmall d )
			{
				return new SteamUnifiedMessagesSendMethodResult_t()
				{
					m_hHandle = d.m_hHandle,
					m_unContext = d.m_unContext,
					m_eResult = d.m_eResult,
					m_unResponseSize = d.m_unResponseSize,
				};
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
			
			public static implicit operator  ControllerAnalogActionData_t (  ControllerAnalogActionData_t.PackSmall d )
			{
				return new ControllerAnalogActionData_t()
				{
					eMode = d.eMode,
					x = d.x,
					y = d.y,
					bActive = d.bActive,
				};
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
			
			public static implicit operator  ControllerDigitalActionData_t (  ControllerDigitalActionData_t.PackSmall d )
			{
				return new ControllerDigitalActionData_t()
				{
					bState = d.bState,
					bActive = d.bActive,
				};
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
			
			public static implicit operator  SteamUGCDetails_t (  SteamUGCDetails_t.PackSmall d )
			{
				return new SteamUGCDetails_t()
				{
					m_nPublishedFileId = d.m_nPublishedFileId,
					m_eResult = d.m_eResult,
					m_eFileType = d.m_eFileType,
					m_nCreatorAppID = d.m_nCreatorAppID,
					m_nConsumerAppID = d.m_nConsumerAppID,
					m_rgchTitle = d.m_rgchTitle,
					m_rgchDescription = d.m_rgchDescription,
					m_ulSteamIDOwner = d.m_ulSteamIDOwner,
					m_rtimeCreated = d.m_rtimeCreated,
					m_rtimeUpdated = d.m_rtimeUpdated,
					m_rtimeAddedToUserList = d.m_rtimeAddedToUserList,
					m_eVisibility = d.m_eVisibility,
					m_bBanned = d.m_bBanned,
					m_bAcceptedForUse = d.m_bAcceptedForUse,
					m_bTagsTruncated = d.m_bTagsTruncated,
					m_rgchTags = d.m_rgchTags,
					m_hFile = d.m_hFile,
					m_hPreviewFile = d.m_hPreviewFile,
					m_pchFileName = d.m_pchFileName,
					m_nFileSize = d.m_nFileSize,
					m_nPreviewFileSize = d.m_nPreviewFileSize,
					m_rgchURL = d.m_rgchURL,
					m_unVotesUp = d.m_unVotesUp,
					m_unVotesDown = d.m_unVotesDown,
					m_flScore = d.m_flScore,
					m_unNumChildren = d.m_unNumChildren,
				};
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
			
			public static implicit operator  SteamUGCQueryCompleted_t (  SteamUGCQueryCompleted_t.PackSmall d )
			{
				return new SteamUGCQueryCompleted_t()
				{
					m_handle = d.m_handle,
					m_eResult = d.m_eResult,
					m_unNumResultsReturned = d.m_unNumResultsReturned,
					m_unTotalMatchingResults = d.m_unTotalMatchingResults,
					m_bCachedData = d.m_bCachedData,
				};
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
			
			public static implicit operator  SteamUGCRequestUGCDetailsResult_t (  SteamUGCRequestUGCDetailsResult_t.PackSmall d )
			{
				return new SteamUGCRequestUGCDetailsResult_t()
				{
					m_details = d.m_details,
					m_bCachedData = d.m_bCachedData,
				};
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
			
			public static implicit operator  CreateItemResult_t (  CreateItemResult_t.PackSmall d )
			{
				return new CreateItemResult_t()
				{
					m_eResult = d.m_eResult,
					m_nPublishedFileId = d.m_nPublishedFileId,
					m_bUserNeedsToAcceptWorkshopLegalAgreement = d.m_bUserNeedsToAcceptWorkshopLegalAgreement,
				};
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
			
			public static implicit operator  SubmitItemUpdateResult_t (  SubmitItemUpdateResult_t.PackSmall d )
			{
				return new SubmitItemUpdateResult_t()
				{
					m_eResult = d.m_eResult,
					m_bUserNeedsToAcceptWorkshopLegalAgreement = d.m_bUserNeedsToAcceptWorkshopLegalAgreement,
				};
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
			
			public static implicit operator  DownloadItemResult_t (  DownloadItemResult_t.PackSmall d )
			{
				return new DownloadItemResult_t()
				{
					m_unAppID = d.m_unAppID,
					m_nPublishedFileId = d.m_nPublishedFileId,
					m_eResult = d.m_eResult,
				};
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
			
			public static implicit operator  UserFavoriteItemsListChanged_t (  UserFavoriteItemsListChanged_t.PackSmall d )
			{
				return new UserFavoriteItemsListChanged_t()
				{
					m_nPublishedFileId = d.m_nPublishedFileId,
					m_eResult = d.m_eResult,
					m_bWasAddRequest = d.m_bWasAddRequest,
				};
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
			
			public static implicit operator  SetUserItemVoteResult_t (  SetUserItemVoteResult_t.PackSmall d )
			{
				return new SetUserItemVoteResult_t()
				{
					m_nPublishedFileId = d.m_nPublishedFileId,
					m_eResult = d.m_eResult,
					m_bVoteUp = d.m_bVoteUp,
				};
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
			
			public static implicit operator  GetUserItemVoteResult_t (  GetUserItemVoteResult_t.PackSmall d )
			{
				return new GetUserItemVoteResult_t()
				{
					m_nPublishedFileId = d.m_nPublishedFileId,
					m_eResult = d.m_eResult,
					m_bVotedUp = d.m_bVotedUp,
					m_bVotedDown = d.m_bVotedDown,
					m_bVoteSkipped = d.m_bVoteSkipped,
				};
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
			
			public static implicit operator  SteamAppInstalled_t (  SteamAppInstalled_t.PackSmall d )
			{
				return new SteamAppInstalled_t()
				{
					m_nAppID = d.m_nAppID,
				};
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
			
			public static implicit operator  SteamAppUninstalled_t (  SteamAppUninstalled_t.PackSmall d )
			{
				return new SteamAppUninstalled_t()
				{
					m_nAppID = d.m_nAppID,
				};
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
			
			public static implicit operator  HTML_BrowserReady_t (  HTML_BrowserReady_t.PackSmall d )
			{
				return new HTML_BrowserReady_t()
				{
					unBrowserHandle = d.unBrowserHandle,
				};
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
			
			public static implicit operator  HTML_NeedsPaint_t (  HTML_NeedsPaint_t.PackSmall d )
			{
				return new HTML_NeedsPaint_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					pBGRA = d.pBGRA,
					unWide = d.unWide,
					unTall = d.unTall,
					unUpdateX = d.unUpdateX,
					unUpdateY = d.unUpdateY,
					unUpdateWide = d.unUpdateWide,
					unUpdateTall = d.unUpdateTall,
					unScrollX = d.unScrollX,
					unScrollY = d.unScrollY,
					flPageScale = d.flPageScale,
					unPageSerial = d.unPageSerial,
				};
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
			
			public static implicit operator  HTML_StartRequest_t (  HTML_StartRequest_t.PackSmall d )
			{
				return new HTML_StartRequest_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					pchURL = d.pchURL,
					pchTarget = d.pchTarget,
					pchPostData = d.pchPostData,
					bIsRedirect = d.bIsRedirect,
				};
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
			
			public static implicit operator  HTML_CloseBrowser_t (  HTML_CloseBrowser_t.PackSmall d )
			{
				return new HTML_CloseBrowser_t()
				{
					unBrowserHandle = d.unBrowserHandle,
				};
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
			
			public static implicit operator  HTML_URLChanged_t (  HTML_URLChanged_t.PackSmall d )
			{
				return new HTML_URLChanged_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					pchURL = d.pchURL,
					pchPostData = d.pchPostData,
					bIsRedirect = d.bIsRedirect,
					pchPageTitle = d.pchPageTitle,
					bNewNavigation = d.bNewNavigation,
				};
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
			
			public static implicit operator  HTML_FinishedRequest_t (  HTML_FinishedRequest_t.PackSmall d )
			{
				return new HTML_FinishedRequest_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					pchURL = d.pchURL,
					pchPageTitle = d.pchPageTitle,
				};
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
			
			public static implicit operator  HTML_OpenLinkInNewTab_t (  HTML_OpenLinkInNewTab_t.PackSmall d )
			{
				return new HTML_OpenLinkInNewTab_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					pchURL = d.pchURL,
				};
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
			
			public static implicit operator  HTML_ChangedTitle_t (  HTML_ChangedTitle_t.PackSmall d )
			{
				return new HTML_ChangedTitle_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					pchTitle = d.pchTitle,
				};
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
			
			public static implicit operator  HTML_SearchResults_t (  HTML_SearchResults_t.PackSmall d )
			{
				return new HTML_SearchResults_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					unResults = d.unResults,
					unCurrentMatch = d.unCurrentMatch,
				};
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
			
			public static implicit operator  HTML_CanGoBackAndForward_t (  HTML_CanGoBackAndForward_t.PackSmall d )
			{
				return new HTML_CanGoBackAndForward_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					bCanGoBack = d.bCanGoBack,
					bCanGoForward = d.bCanGoForward,
				};
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
			
			public static implicit operator  HTML_HorizontalScroll_t (  HTML_HorizontalScroll_t.PackSmall d )
			{
				return new HTML_HorizontalScroll_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					unScrollMax = d.unScrollMax,
					unScrollCurrent = d.unScrollCurrent,
					flPageScale = d.flPageScale,
					bVisible = d.bVisible,
					unPageSize = d.unPageSize,
				};
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
			
			public static implicit operator  HTML_VerticalScroll_t (  HTML_VerticalScroll_t.PackSmall d )
			{
				return new HTML_VerticalScroll_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					unScrollMax = d.unScrollMax,
					unScrollCurrent = d.unScrollCurrent,
					flPageScale = d.flPageScale,
					bVisible = d.bVisible,
					unPageSize = d.unPageSize,
				};
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
			
			public static implicit operator  HTML_LinkAtPosition_t (  HTML_LinkAtPosition_t.PackSmall d )
			{
				return new HTML_LinkAtPosition_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					x = d.x,
					y = d.y,
					pchURL = d.pchURL,
					bInput = d.bInput,
					bLiveLink = d.bLiveLink,
				};
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
			
			public static implicit operator  HTML_JSAlert_t (  HTML_JSAlert_t.PackSmall d )
			{
				return new HTML_JSAlert_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					pchMessage = d.pchMessage,
				};
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
			
			public static implicit operator  HTML_JSConfirm_t (  HTML_JSConfirm_t.PackSmall d )
			{
				return new HTML_JSConfirm_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					pchMessage = d.pchMessage,
				};
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
			
			public static implicit operator  HTML_FileOpenDialog_t (  HTML_FileOpenDialog_t.PackSmall d )
			{
				return new HTML_FileOpenDialog_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					pchTitle = d.pchTitle,
					pchInitialFile = d.pchInitialFile,
				};
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
			
			public static implicit operator  HTML_NewWindow_t (  HTML_NewWindow_t.PackSmall d )
			{
				return new HTML_NewWindow_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					pchURL = d.pchURL,
					unX = d.unX,
					unY = d.unY,
					unWide = d.unWide,
					unTall = d.unTall,
					unNewWindow_BrowserHandle = d.unNewWindow_BrowserHandle,
				};
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
			
			public static implicit operator  HTML_SetCursor_t (  HTML_SetCursor_t.PackSmall d )
			{
				return new HTML_SetCursor_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					eMouseCursor = d.eMouseCursor,
				};
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
			
			public static implicit operator  HTML_StatusText_t (  HTML_StatusText_t.PackSmall d )
			{
				return new HTML_StatusText_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					pchMsg = d.pchMsg,
				};
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
			
			public static implicit operator  HTML_ShowToolTip_t (  HTML_ShowToolTip_t.PackSmall d )
			{
				return new HTML_ShowToolTip_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					pchMsg = d.pchMsg,
				};
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
			
			public static implicit operator  HTML_UpdateToolTip_t (  HTML_UpdateToolTip_t.PackSmall d )
			{
				return new HTML_UpdateToolTip_t()
				{
					unBrowserHandle = d.unBrowserHandle,
					pchMsg = d.pchMsg,
				};
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
			
			public static implicit operator  HTML_HideToolTip_t (  HTML_HideToolTip_t.PackSmall d )
			{
				return new HTML_HideToolTip_t()
				{
					unBrowserHandle = d.unBrowserHandle,
				};
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
			
			public static implicit operator  SteamItemDetails_t (  SteamItemDetails_t.PackSmall d )
			{
				return new SteamItemDetails_t()
				{
					m_itemId = d.m_itemId,
					m_iDefinition = d.m_iDefinition,
					m_unQuantity = d.m_unQuantity,
					m_unFlags = d.m_unFlags,
				};
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
			
			public static implicit operator  SteamInventoryResultReady_t (  SteamInventoryResultReady_t.PackSmall d )
			{
				return new SteamInventoryResultReady_t()
				{
					m_handle = d.m_handle,
					m_result = d.m_result,
				};
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
			
			public static implicit operator  SteamInventoryFullUpdate_t (  SteamInventoryFullUpdate_t.PackSmall d )
			{
				return new SteamInventoryFullUpdate_t()
				{
					m_handle = d.m_handle,
				};
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
			
			public static implicit operator  BroadcastUploadStop_t (  BroadcastUploadStop_t.PackSmall d )
			{
				return new BroadcastUploadStop_t()
				{
					m_eResult = d.m_eResult,
				};
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
			
			public static implicit operator  GetVideoURLResult_t (  GetVideoURLResult_t.PackSmall d )
			{
				return new GetVideoURLResult_t()
				{
					m_eResult = d.m_eResult,
					m_unVideoAppID = d.m_unVideoAppID,
					m_rgchURL = d.m_rgchURL,
				};
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
			
			public static implicit operator  CCallbackBase (  CCallbackBase.PackSmall d )
			{
				return new CCallbackBase()
				{
					m_nCallbackFlags = d.m_nCallbackFlags,
					m_iCallback = d.m_iCallback,
				};
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
			
			public static implicit operator  GSClientApprove_t (  GSClientApprove_t.PackSmall d )
			{
				return new GSClientApprove_t()
				{
					m_SteamID = d.m_SteamID,
					m_OwnerSteamID = d.m_OwnerSteamID,
				};
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
			
			public static implicit operator  GSClientDeny_t (  GSClientDeny_t.PackSmall d )
			{
				return new GSClientDeny_t()
				{
					m_SteamID = d.m_SteamID,
					m_eDenyReason = d.m_eDenyReason,
					m_rgchOptionalText = d.m_rgchOptionalText,
				};
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
			
			public static implicit operator  GSClientKick_t (  GSClientKick_t.PackSmall d )
			{
				return new GSClientKick_t()
				{
					m_SteamID = d.m_SteamID,
					m_eDenyReason = d.m_eDenyReason,
				};
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
			
			public static implicit operator  GSClientAchievementStatus_t (  GSClientAchievementStatus_t.PackSmall d )
			{
				return new GSClientAchievementStatus_t()
				{
					m_SteamID = d.m_SteamID,
					m_pchAchievement = d.m_pchAchievement,
					m_bUnlocked = d.m_bUnlocked,
				};
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
			
			public static implicit operator  GSPolicyResponse_t (  GSPolicyResponse_t.PackSmall d )
			{
				return new GSPolicyResponse_t()
				{
					m_bSecure = d.m_bSecure,
				};
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
			
			public static implicit operator  GSGameplayStats_t (  GSGameplayStats_t.PackSmall d )
			{
				return new GSGameplayStats_t()
				{
					m_eResult = d.m_eResult,
					m_nRank = d.m_nRank,
					m_unTotalConnects = d.m_unTotalConnects,
					m_unTotalMinutesPlayed = d.m_unTotalMinutesPlayed,
				};
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
			
			public static implicit operator  GSClientGroupStatus_t (  GSClientGroupStatus_t.PackSmall d )
			{
				return new GSClientGroupStatus_t()
				{
					m_SteamIDUser = d.m_SteamIDUser,
					m_SteamIDGroup = d.m_SteamIDGroup,
					m_bMember = d.m_bMember,
					m_bOfficer = d.m_bOfficer,
				};
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
			
			public static implicit operator  GSReputation_t (  GSReputation_t.PackSmall d )
			{
				return new GSReputation_t()
				{
					m_eResult = d.m_eResult,
					m_unReputationScore = d.m_unReputationScore,
					m_bBanned = d.m_bBanned,
					m_unBannedIP = d.m_unBannedIP,
					m_usBannedPort = d.m_usBannedPort,
					m_ulBannedGameID = d.m_ulBannedGameID,
					m_unBanExpires = d.m_unBanExpires,
				};
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
			
			public static implicit operator  AssociateWithClanResult_t (  AssociateWithClanResult_t.PackSmall d )
			{
				return new AssociateWithClanResult_t()
				{
					m_eResult = d.m_eResult,
				};
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
			
			public static implicit operator  ComputeNewPlayerCompatibilityResult_t (  ComputeNewPlayerCompatibilityResult_t.PackSmall d )
			{
				return new ComputeNewPlayerCompatibilityResult_t()
				{
					m_eResult = d.m_eResult,
					m_cPlayersThatDontLikeCandidate = d.m_cPlayersThatDontLikeCandidate,
					m_cPlayersThatCandidateDoesntLike = d.m_cPlayersThatCandidateDoesntLike,
					m_cClanPlayersThatDontLikeCandidate = d.m_cClanPlayersThatDontLikeCandidate,
					m_SteamIDCandidate = d.m_SteamIDCandidate,
				};
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
			
			public static implicit operator  GSStatsReceived_t (  GSStatsReceived_t.PackSmall d )
			{
				return new GSStatsReceived_t()
				{
					m_eResult = d.m_eResult,
					m_steamIDUser = d.m_steamIDUser,
				};
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
			
			public static implicit operator  GSStatsStored_t (  GSStatsStored_t.PackSmall d )
			{
				return new GSStatsStored_t()
				{
					m_eResult = d.m_eResult,
					m_steamIDUser = d.m_steamIDUser,
				};
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
			
			public static implicit operator  GSStatsUnloaded_t (  GSStatsUnloaded_t.PackSmall d )
			{
				return new GSStatsUnloaded_t()
				{
					m_steamIDUser = d.m_steamIDUser,
				};
			}
		}
	}
	
}
