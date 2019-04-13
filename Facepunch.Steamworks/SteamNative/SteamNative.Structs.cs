using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace SteamNative
{
	public struct CallbackMsg_t
	{
		internal int SteamUser; // m_hSteamUser HSteamUser
		internal int Callback; // m_iCallback int
		internal IntPtr ParamPtr; // m_pubParam uint8 *
		internal int ParamCount; // m_cubParam int
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public CallbackMsg_t Fill( IntPtr p ) => Platform.PackSmall ? ((CallbackMsg_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((CallbackMsg_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal int SteamUser; // m_hSteamUser HSteamUser
			internal int Callback; // m_iCallback int
			internal IntPtr ParamPtr; // m_pubParam uint8 *
			internal int ParamCount; // m_cubParam int
			
			public static implicit operator CallbackMsg_t ( CallbackMsg_t.Pack4 d ) => new CallbackMsg_t{ SteamUser = d.SteamUser,Callback = d.Callback,ParamPtr = d.ParamPtr,ParamCount = d.ParamCount, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal int SteamUser; // m_hSteamUser HSteamUser
			internal int Callback; // m_iCallback int
			internal IntPtr ParamPtr; // m_pubParam uint8 *
			internal int ParamCount; // m_cubParam int
			
			public static implicit operator CallbackMsg_t ( CallbackMsg_t.Pack8 d ) => new CallbackMsg_t{ SteamUser = d.SteamUser,Callback = d.Callback,ParamPtr = d.ParamPtr,ParamCount = d.ParamCount, };
		}
		#endregion
	}
	
	public struct SteamServerConnectFailure_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool StillRetrying; // m_bStillRetrying _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SteamServerConnectFailure_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamServerConnectFailure_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			internal bool StillRetrying; // m_bStillRetrying _Bool
			
			public static implicit operator SteamServerConnectFailure_t ( SteamServerConnectFailure_t.Pack4 d ) => new SteamServerConnectFailure_t{ Result = d.Result,StillRetrying = d.StillRetrying, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			internal bool StillRetrying; // m_bStillRetrying _Bool
			
			public static implicit operator SteamServerConnectFailure_t ( SteamServerConnectFailure_t.Pack8 d ) => new SteamServerConnectFailure_t{ Result = d.Result,StillRetrying = d.StillRetrying, };
		}
		#endregion
	}
	
	public struct SteamServersDisconnected_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SteamServersDisconnected_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamServersDisconnected_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator SteamServersDisconnected_t ( SteamServersDisconnected_t.Pack4 d ) => new SteamServersDisconnected_t{ Result = d.Result, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator SteamServersDisconnected_t ( SteamServersDisconnected_t.Pack8 d ) => new SteamServersDisconnected_t{ Result = d.Result, };
		}
		#endregion
	}
	
	public struct ClientGameServerDeny_t : Steamworks.ISteamCallback
	{
		internal uint AppID; // m_uAppID uint32
		internal uint GameServerIP; // m_unGameServerIP uint32
		internal ushort GameServerPort; // m_usGameServerPort uint16
		internal ushort Secure; // m_bSecure uint16
		internal uint Reason; // m_uReason uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 13;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((ClientGameServerDeny_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((ClientGameServerDeny_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint AppID; // m_uAppID uint32
			internal uint GameServerIP; // m_unGameServerIP uint32
			internal ushort GameServerPort; // m_usGameServerPort uint16
			internal ushort Secure; // m_bSecure uint16
			internal uint Reason; // m_uReason uint32
			
			public static implicit operator ClientGameServerDeny_t ( ClientGameServerDeny_t.Pack4 d ) => new ClientGameServerDeny_t{ AppID = d.AppID,GameServerIP = d.GameServerIP,GameServerPort = d.GameServerPort,Secure = d.Secure,Reason = d.Reason, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint AppID; // m_uAppID uint32
			internal uint GameServerIP; // m_unGameServerIP uint32
			internal ushort GameServerPort; // m_usGameServerPort uint16
			internal ushort Secure; // m_bSecure uint16
			internal uint Reason; // m_uReason uint32
			
			public static implicit operator ClientGameServerDeny_t ( ClientGameServerDeny_t.Pack8 d ) => new ClientGameServerDeny_t{ AppID = d.AppID,GameServerIP = d.GameServerIP,GameServerPort = d.GameServerPort,Secure = d.Secure,Reason = d.Reason, };
		}
		#endregion
	}
	
	public struct ValidateAuthTicketResponse_t : Steamworks.ISteamCallback
	{
		internal ulong SteamID; // m_SteamID class CSteamID
		internal AuthSessionResponse AuthSessionResponse; // m_eAuthSessionResponse enum EAuthSessionResponse
		internal ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 43;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((ValidateAuthTicketResponse_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((ValidateAuthTicketResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamID; // m_SteamID class CSteamID
			internal AuthSessionResponse AuthSessionResponse; // m_eAuthSessionResponse enum EAuthSessionResponse
			internal ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
			
			public static implicit operator ValidateAuthTicketResponse_t ( ValidateAuthTicketResponse_t.Pack4 d ) => new ValidateAuthTicketResponse_t{ SteamID = d.SteamID,AuthSessionResponse = d.AuthSessionResponse,OwnerSteamID = d.OwnerSteamID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamID; // m_SteamID class CSteamID
			internal AuthSessionResponse AuthSessionResponse; // m_eAuthSessionResponse enum EAuthSessionResponse
			internal ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
			
			public static implicit operator ValidateAuthTicketResponse_t ( ValidateAuthTicketResponse_t.Pack8 d ) => new ValidateAuthTicketResponse_t{ SteamID = d.SteamID,AuthSessionResponse = d.AuthSessionResponse,OwnerSteamID = d.OwnerSteamID, };
		}
		#endregion
	}
	
	public struct MicroTxnAuthorizationResponse_t : Steamworks.ISteamCallback
	{
		internal uint AppID; // m_unAppID uint32
		internal ulong OrderID; // m_ulOrderID uint64
		internal byte Authorized; // m_bAuthorized uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 52;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((MicroTxnAuthorizationResponse_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((MicroTxnAuthorizationResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint AppID; // m_unAppID uint32
			internal ulong OrderID; // m_ulOrderID uint64
			internal byte Authorized; // m_bAuthorized uint8
			
			public static implicit operator MicroTxnAuthorizationResponse_t ( MicroTxnAuthorizationResponse_t.Pack4 d ) => new MicroTxnAuthorizationResponse_t{ AppID = d.AppID,OrderID = d.OrderID,Authorized = d.Authorized, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint AppID; // m_unAppID uint32
			internal ulong OrderID; // m_ulOrderID uint64
			internal byte Authorized; // m_bAuthorized uint8
			
			public static implicit operator MicroTxnAuthorizationResponse_t ( MicroTxnAuthorizationResponse_t.Pack8 d ) => new MicroTxnAuthorizationResponse_t{ AppID = d.AppID,OrderID = d.OrderID,Authorized = d.Authorized, };
		}
		#endregion
	}
	
	public struct EncryptedAppTicketResponse_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 54;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((EncryptedAppTicketResponse_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((EncryptedAppTicketResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator EncryptedAppTicketResponse_t ( EncryptedAppTicketResponse_t.Pack4 d ) => new EncryptedAppTicketResponse_t{ Result = d.Result, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator EncryptedAppTicketResponse_t ( EncryptedAppTicketResponse_t.Pack8 d ) => new EncryptedAppTicketResponse_t{ Result = d.Result, };
		}
		#endregion
	}
	
	public struct GetAuthSessionTicketResponse_t : Steamworks.ISteamCallback
	{
		internal uint AuthTicket; // m_hAuthTicket HAuthTicket
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 63;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GetAuthSessionTicketResponse_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GetAuthSessionTicketResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint AuthTicket; // m_hAuthTicket HAuthTicket
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator GetAuthSessionTicketResponse_t ( GetAuthSessionTicketResponse_t.Pack4 d ) => new GetAuthSessionTicketResponse_t{ AuthTicket = d.AuthTicket,Result = d.Result, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint AuthTicket; // m_hAuthTicket HAuthTicket
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator GetAuthSessionTicketResponse_t ( GetAuthSessionTicketResponse_t.Pack8 d ) => new GetAuthSessionTicketResponse_t{ AuthTicket = d.AuthTicket,Result = d.Result, };
		}
		#endregion
	}
	
	public struct GameWebCallback_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string URL; // m_szURL char [256]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 64;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GameWebCallback_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GameWebCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string URL; // m_szURL char [256]
			
			public static implicit operator GameWebCallback_t ( GameWebCallback_t.Pack4 d ) => new GameWebCallback_t{ URL = d.URL, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string URL; // m_szURL char [256]
			
			public static implicit operator GameWebCallback_t ( GameWebCallback_t.Pack8 d ) => new GameWebCallback_t{ URL = d.URL, };
		}
		#endregion
	}
	
	public struct StoreAuthURLResponse_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
		internal string URL; // m_szURL char [512]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 65;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((StoreAuthURLResponse_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((StoreAuthURLResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
			internal string URL; // m_szURL char [512]
			
			public static implicit operator StoreAuthURLResponse_t ( StoreAuthURLResponse_t.Pack4 d ) => new StoreAuthURLResponse_t{ URL = d.URL, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
			internal string URL; // m_szURL char [512]
			
			public static implicit operator StoreAuthURLResponse_t ( StoreAuthURLResponse_t.Pack8 d ) => new StoreAuthURLResponse_t{ URL = d.URL, };
		}
		#endregion
	}
	
	public struct MarketEligibilityResponse_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Allowed; // m_bAllowed _Bool
		internal MarketNotAllowedReasonFlags NotAllowedReason; // m_eNotAllowedReason enum EMarketNotAllowedReasonFlags
		internal uint TAllowedAtTime; // m_rtAllowedAtTime RTime32
		internal int CdaySteamGuardRequiredDays; // m_cdaySteamGuardRequiredDays int
		internal int CdayNewDeviceCooldown; // m_cdayNewDeviceCooldown int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 66;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((MarketEligibilityResponse_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((MarketEligibilityResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool Allowed; // m_bAllowed _Bool
			internal MarketNotAllowedReasonFlags NotAllowedReason; // m_eNotAllowedReason enum EMarketNotAllowedReasonFlags
			internal uint TAllowedAtTime; // m_rtAllowedAtTime RTime32
			internal int CdaySteamGuardRequiredDays; // m_cdaySteamGuardRequiredDays int
			internal int CdayNewDeviceCooldown; // m_cdayNewDeviceCooldown int
			
			public static implicit operator MarketEligibilityResponse_t ( MarketEligibilityResponse_t.Pack4 d ) => new MarketEligibilityResponse_t{ Allowed = d.Allowed,NotAllowedReason = d.NotAllowedReason,TAllowedAtTime = d.TAllowedAtTime,CdaySteamGuardRequiredDays = d.CdaySteamGuardRequiredDays,CdayNewDeviceCooldown = d.CdayNewDeviceCooldown, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool Allowed; // m_bAllowed _Bool
			internal MarketNotAllowedReasonFlags NotAllowedReason; // m_eNotAllowedReason enum EMarketNotAllowedReasonFlags
			internal uint TAllowedAtTime; // m_rtAllowedAtTime RTime32
			internal int CdaySteamGuardRequiredDays; // m_cdaySteamGuardRequiredDays int
			internal int CdayNewDeviceCooldown; // m_cdayNewDeviceCooldown int
			
			public static implicit operator MarketEligibilityResponse_t ( MarketEligibilityResponse_t.Pack8 d ) => new MarketEligibilityResponse_t{ Allowed = d.Allowed,NotAllowedReason = d.NotAllowedReason,TAllowedAtTime = d.TAllowedAtTime,CdaySteamGuardRequiredDays = d.CdaySteamGuardRequiredDays,CdayNewDeviceCooldown = d.CdayNewDeviceCooldown, };
		}
		#endregion
	}
	
	public struct FriendGameInfo_t
	{
		internal ulong GameID; // m_gameID class CGameID
		internal uint GameIP; // m_unGameIP uint32
		internal ushort GamePort; // m_usGamePort uint16
		internal ushort QueryPort; // m_usQueryPort uint16
		internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public FriendGameInfo_t Fill( IntPtr p ) => Platform.PackSmall ? ((FriendGameInfo_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((FriendGameInfo_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong GameID; // m_gameID class CGameID
			internal uint GameIP; // m_unGameIP uint32
			internal ushort GamePort; // m_usGamePort uint16
			internal ushort QueryPort; // m_usQueryPort uint16
			internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
			
			public static implicit operator FriendGameInfo_t ( FriendGameInfo_t.Pack4 d ) => new FriendGameInfo_t{ GameID = d.GameID,GameIP = d.GameIP,GamePort = d.GamePort,QueryPort = d.QueryPort,SteamIDLobby = d.SteamIDLobby, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong GameID; // m_gameID class CGameID
			internal uint GameIP; // m_unGameIP uint32
			internal ushort GamePort; // m_usGamePort uint16
			internal ushort QueryPort; // m_usQueryPort uint16
			internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
			
			public static implicit operator FriendGameInfo_t ( FriendGameInfo_t.Pack8 d ) => new FriendGameInfo_t{ GameID = d.GameID,GameIP = d.GameIP,GamePort = d.GamePort,QueryPort = d.QueryPort,SteamIDLobby = d.SteamIDLobby, };
		}
		#endregion
	}
	
	public struct FriendSessionStateInfo_t
	{
		internal uint IOnlineSessionInstances; // m_uiOnlineSessionInstances uint32
		internal byte IPublishedToFriendsSessionInstance; // m_uiPublishedToFriendsSessionInstance uint8
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public FriendSessionStateInfo_t Fill( IntPtr p ) => Platform.PackSmall ? ((FriendSessionStateInfo_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((FriendSessionStateInfo_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint IOnlineSessionInstances; // m_uiOnlineSessionInstances uint32
			internal byte IPublishedToFriendsSessionInstance; // m_uiPublishedToFriendsSessionInstance uint8
			
			public static implicit operator FriendSessionStateInfo_t ( FriendSessionStateInfo_t.Pack4 d ) => new FriendSessionStateInfo_t{ IOnlineSessionInstances = d.IOnlineSessionInstances,IPublishedToFriendsSessionInstance = d.IPublishedToFriendsSessionInstance, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint IOnlineSessionInstances; // m_uiOnlineSessionInstances uint32
			internal byte IPublishedToFriendsSessionInstance; // m_uiPublishedToFriendsSessionInstance uint8
			
			public static implicit operator FriendSessionStateInfo_t ( FriendSessionStateInfo_t.Pack8 d ) => new FriendSessionStateInfo_t{ IOnlineSessionInstances = d.IOnlineSessionInstances,IPublishedToFriendsSessionInstance = d.IPublishedToFriendsSessionInstance, };
		}
		#endregion
	}
	
	public struct PersonaStateChange_t : Steamworks.ISteamCallback
	{
		internal ulong SteamID; // m_ulSteamID uint64
		internal int ChangeFlags; // m_nChangeFlags int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 4;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((PersonaStateChange_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((PersonaStateChange_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamID; // m_ulSteamID uint64
			internal int ChangeFlags; // m_nChangeFlags int
			
			public static implicit operator PersonaStateChange_t ( PersonaStateChange_t.Pack4 d ) => new PersonaStateChange_t{ SteamID = d.SteamID,ChangeFlags = d.ChangeFlags, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong SteamID; // m_ulSteamID uint64
			internal int ChangeFlags; // m_nChangeFlags int
			
			public static implicit operator PersonaStateChange_t ( PersonaStateChange_t.Pack8 d ) => new PersonaStateChange_t{ SteamID = d.SteamID,ChangeFlags = d.ChangeFlags, };
		}
		#endregion
	}
	
	public struct GameOverlayActivated_t : Steamworks.ISteamCallback
	{
		internal byte Active; // m_bActive uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 31;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GameOverlayActivated_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GameOverlayActivated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal byte Active; // m_bActive uint8
			
			public static implicit operator GameOverlayActivated_t ( GameOverlayActivated_t.Pack4 d ) => new GameOverlayActivated_t{ Active = d.Active, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal byte Active; // m_bActive uint8
			
			public static implicit operator GameOverlayActivated_t ( GameOverlayActivated_t.Pack8 d ) => new GameOverlayActivated_t{ Active = d.Active, };
		}
		#endregion
	}
	
	public struct GameServerChangeRequested_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		internal string Server; // m_rgchServer char [64]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		internal string Password; // m_rgchPassword char [64]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 32;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GameServerChangeRequested_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GameServerChangeRequested_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			internal string Server; // m_rgchServer char [64]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			internal string Password; // m_rgchPassword char [64]
			
			public static implicit operator GameServerChangeRequested_t ( GameServerChangeRequested_t.Pack4 d ) => new GameServerChangeRequested_t{ Server = d.Server,Password = d.Password, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			internal string Server; // m_rgchServer char [64]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			internal string Password; // m_rgchPassword char [64]
			
			public static implicit operator GameServerChangeRequested_t ( GameServerChangeRequested_t.Pack8 d ) => new GameServerChangeRequested_t{ Server = d.Server,Password = d.Password, };
		}
		#endregion
	}
	
	public struct GameLobbyJoinRequested_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
		internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 33;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GameLobbyJoinRequested_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GameLobbyJoinRequested_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
			internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
			
			public static implicit operator GameLobbyJoinRequested_t ( GameLobbyJoinRequested_t.Pack4 d ) => new GameLobbyJoinRequested_t{ SteamIDLobby = d.SteamIDLobby,SteamIDFriend = d.SteamIDFriend, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
			internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
			
			public static implicit operator GameLobbyJoinRequested_t ( GameLobbyJoinRequested_t.Pack8 d ) => new GameLobbyJoinRequested_t{ SteamIDLobby = d.SteamIDLobby,SteamIDFriend = d.SteamIDFriend, };
		}
		#endregion
	}
	
	public struct AvatarImageLoaded_t : Steamworks.ISteamCallback
	{
		internal ulong SteamID; // m_steamID class CSteamID
		internal int Image; // m_iImage int
		internal int Wide; // m_iWide int
		internal int Tall; // m_iTall int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 34;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((AvatarImageLoaded_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((AvatarImageLoaded_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamID; // m_steamID class CSteamID
			internal int Image; // m_iImage int
			internal int Wide; // m_iWide int
			internal int Tall; // m_iTall int
			
			public static implicit operator AvatarImageLoaded_t ( AvatarImageLoaded_t.Pack4 d ) => new AvatarImageLoaded_t{ SteamID = d.SteamID,Image = d.Image,Wide = d.Wide,Tall = d.Tall, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamID; // m_steamID class CSteamID
			internal int Image; // m_iImage int
			internal int Wide; // m_iWide int
			internal int Tall; // m_iTall int
			
			public static implicit operator AvatarImageLoaded_t ( AvatarImageLoaded_t.Pack8 d ) => new AvatarImageLoaded_t{ SteamID = d.SteamID,Image = d.Image,Wide = d.Wide,Tall = d.Tall, };
		}
		#endregion
	}
	
	public struct ClanOfficerListResponse_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDClan; // m_steamIDClan class CSteamID
		internal int COfficers; // m_cOfficers int
		internal byte Success; // m_bSuccess uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 35;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((ClanOfficerListResponse_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((ClanOfficerListResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDClan; // m_steamIDClan class CSteamID
			internal int COfficers; // m_cOfficers int
			internal byte Success; // m_bSuccess uint8
			
			public static implicit operator ClanOfficerListResponse_t ( ClanOfficerListResponse_t.Pack4 d ) => new ClanOfficerListResponse_t{ SteamIDClan = d.SteamIDClan,COfficers = d.COfficers,Success = d.Success, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDClan; // m_steamIDClan class CSteamID
			internal int COfficers; // m_cOfficers int
			internal byte Success; // m_bSuccess uint8
			
			public static implicit operator ClanOfficerListResponse_t ( ClanOfficerListResponse_t.Pack8 d ) => new ClanOfficerListResponse_t{ SteamIDClan = d.SteamIDClan,COfficers = d.COfficers,Success = d.Success, };
		}
		#endregion
	}
	
	public struct FriendRichPresenceUpdate_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		internal uint AppID; // m_nAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 36;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((FriendRichPresenceUpdate_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((FriendRichPresenceUpdate_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
			internal uint AppID; // m_nAppID AppId_t
			
			public static implicit operator FriendRichPresenceUpdate_t ( FriendRichPresenceUpdate_t.Pack4 d ) => new FriendRichPresenceUpdate_t{ SteamIDFriend = d.SteamIDFriend,AppID = d.AppID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
			internal uint AppID; // m_nAppID AppId_t
			
			public static implicit operator FriendRichPresenceUpdate_t ( FriendRichPresenceUpdate_t.Pack8 d ) => new FriendRichPresenceUpdate_t{ SteamIDFriend = d.SteamIDFriend,AppID = d.AppID, };
		}
		#endregion
	}
	
	public struct GameRichPresenceJoinRequested_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string Connect; // m_rgchConnect char [256]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 37;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GameRichPresenceJoinRequested_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GameRichPresenceJoinRequested_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string Connect; // m_rgchConnect char [256]
			
			public static implicit operator GameRichPresenceJoinRequested_t ( GameRichPresenceJoinRequested_t.Pack4 d ) => new GameRichPresenceJoinRequested_t{ SteamIDFriend = d.SteamIDFriend,Connect = d.Connect, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string Connect; // m_rgchConnect char [256]
			
			public static implicit operator GameRichPresenceJoinRequested_t ( GameRichPresenceJoinRequested_t.Pack8 d ) => new GameRichPresenceJoinRequested_t{ SteamIDFriend = d.SteamIDFriend,Connect = d.Connect, };
		}
		#endregion
	}
	
	public struct GameConnectedClanChatMsg_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		internal int MessageID; // m_iMessageID int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 38;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GameConnectedClanChatMsg_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GameConnectedClanChatMsg_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			internal int MessageID; // m_iMessageID int
			
			public static implicit operator GameConnectedClanChatMsg_t ( GameConnectedClanChatMsg_t.Pack4 d ) => new GameConnectedClanChatMsg_t{ SteamIDClanChat = d.SteamIDClanChat,SteamIDUser = d.SteamIDUser,MessageID = d.MessageID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			internal int MessageID; // m_iMessageID int
			
			public static implicit operator GameConnectedClanChatMsg_t ( GameConnectedClanChatMsg_t.Pack8 d ) => new GameConnectedClanChatMsg_t{ SteamIDClanChat = d.SteamIDClanChat,SteamIDUser = d.SteamIDUser,MessageID = d.MessageID, };
		}
		#endregion
	}
	
	public struct GameConnectedChatJoin_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 39;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GameConnectedChatJoin_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GameConnectedChatJoin_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator GameConnectedChatJoin_t ( GameConnectedChatJoin_t.Pack4 d ) => new GameConnectedChatJoin_t{ SteamIDClanChat = d.SteamIDClanChat,SteamIDUser = d.SteamIDUser, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator GameConnectedChatJoin_t ( GameConnectedChatJoin_t.Pack8 d ) => new GameConnectedChatJoin_t{ SteamIDClanChat = d.SteamIDClanChat,SteamIDUser = d.SteamIDUser, };
		}
		#endregion
	}
	
	public struct GameConnectedChatLeave_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool Kicked; // m_bKicked _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool Dropped; // m_bDropped _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 40;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GameConnectedChatLeave_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GameConnectedChatLeave_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			[MarshalAs(UnmanagedType.I1)]
			internal bool Kicked; // m_bKicked _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool Dropped; // m_bDropped _Bool
			
			public static implicit operator GameConnectedChatLeave_t ( GameConnectedChatLeave_t.Pack4 d ) => new GameConnectedChatLeave_t{ SteamIDClanChat = d.SteamIDClanChat,SteamIDUser = d.SteamIDUser,Kicked = d.Kicked,Dropped = d.Dropped, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			[MarshalAs(UnmanagedType.I1)]
			internal bool Kicked; // m_bKicked _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool Dropped; // m_bDropped _Bool
			
			public static implicit operator GameConnectedChatLeave_t ( GameConnectedChatLeave_t.Pack8 d ) => new GameConnectedChatLeave_t{ SteamIDClanChat = d.SteamIDClanChat,SteamIDUser = d.SteamIDUser,Kicked = d.Kicked,Dropped = d.Dropped, };
		}
		#endregion
	}
	
	public struct DownloadClanActivityCountsResult_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Success; // m_bSuccess _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 41;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((DownloadClanActivityCountsResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((DownloadClanActivityCountsResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool Success; // m_bSuccess _Bool
			
			public static implicit operator DownloadClanActivityCountsResult_t ( DownloadClanActivityCountsResult_t.Pack4 d ) => new DownloadClanActivityCountsResult_t{ Success = d.Success, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool Success; // m_bSuccess _Bool
			
			public static implicit operator DownloadClanActivityCountsResult_t ( DownloadClanActivityCountsResult_t.Pack8 d ) => new DownloadClanActivityCountsResult_t{ Success = d.Success, };
		}
		#endregion
	}
	
	public struct JoinClanChatRoomCompletionResult_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		internal ChatRoomEnterResponse ChatRoomEnterResponse; // m_eChatRoomEnterResponse enum EChatRoomEnterResponse
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 42;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((JoinClanChatRoomCompletionResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((JoinClanChatRoomCompletionResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
			internal ChatRoomEnterResponse ChatRoomEnterResponse; // m_eChatRoomEnterResponse enum EChatRoomEnterResponse
			
			public static implicit operator JoinClanChatRoomCompletionResult_t ( JoinClanChatRoomCompletionResult_t.Pack4 d ) => new JoinClanChatRoomCompletionResult_t{ SteamIDClanChat = d.SteamIDClanChat,ChatRoomEnterResponse = d.ChatRoomEnterResponse, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
			internal ChatRoomEnterResponse ChatRoomEnterResponse; // m_eChatRoomEnterResponse enum EChatRoomEnterResponse
			
			public static implicit operator JoinClanChatRoomCompletionResult_t ( JoinClanChatRoomCompletionResult_t.Pack8 d ) => new JoinClanChatRoomCompletionResult_t{ SteamIDClanChat = d.SteamIDClanChat,ChatRoomEnterResponse = d.ChatRoomEnterResponse, };
		}
		#endregion
	}
	
	public struct GameConnectedFriendChatMsg_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		internal int MessageID; // m_iMessageID int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 43;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GameConnectedFriendChatMsg_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GameConnectedFriendChatMsg_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			internal int MessageID; // m_iMessageID int
			
			public static implicit operator GameConnectedFriendChatMsg_t ( GameConnectedFriendChatMsg_t.Pack4 d ) => new GameConnectedFriendChatMsg_t{ SteamIDUser = d.SteamIDUser,MessageID = d.MessageID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			internal int MessageID; // m_iMessageID int
			
			public static implicit operator GameConnectedFriendChatMsg_t ( GameConnectedFriendChatMsg_t.Pack8 d ) => new GameConnectedFriendChatMsg_t{ SteamIDUser = d.SteamIDUser,MessageID = d.MessageID, };
		}
		#endregion
	}
	
	public struct FriendsGetFollowerCount_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamID; // m_steamID class CSteamID
		internal int Count; // m_nCount int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 44;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((FriendsGetFollowerCount_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((FriendsGetFollowerCount_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong SteamID; // m_steamID class CSteamID
			internal int Count; // m_nCount int
			
			public static implicit operator FriendsGetFollowerCount_t ( FriendsGetFollowerCount_t.Pack4 d ) => new FriendsGetFollowerCount_t{ Result = d.Result,SteamID = d.SteamID,Count = d.Count, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong SteamID; // m_steamID class CSteamID
			internal int Count; // m_nCount int
			
			public static implicit operator FriendsGetFollowerCount_t ( FriendsGetFollowerCount_t.Pack8 d ) => new FriendsGetFollowerCount_t{ Result = d.Result,SteamID = d.SteamID,Count = d.Count, };
		}
		#endregion
	}
	
	public struct FriendsIsFollowing_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamID; // m_steamID class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool IsFollowing; // m_bIsFollowing _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 45;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((FriendsIsFollowing_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((FriendsIsFollowing_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong SteamID; // m_steamID class CSteamID
			[MarshalAs(UnmanagedType.I1)]
			internal bool IsFollowing; // m_bIsFollowing _Bool
			
			public static implicit operator FriendsIsFollowing_t ( FriendsIsFollowing_t.Pack4 d ) => new FriendsIsFollowing_t{ Result = d.Result,SteamID = d.SteamID,IsFollowing = d.IsFollowing, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong SteamID; // m_steamID class CSteamID
			[MarshalAs(UnmanagedType.I1)]
			internal bool IsFollowing; // m_bIsFollowing _Bool
			
			public static implicit operator FriendsIsFollowing_t ( FriendsIsFollowing_t.Pack8 d ) => new FriendsIsFollowing_t{ Result = d.Result,SteamID = d.SteamID,IsFollowing = d.IsFollowing, };
		}
		#endregion
	}
	
	public struct FriendsEnumerateFollowingList_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal ulong[] GSteamID; // m_rgSteamID class CSteamID [50]
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 46;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((FriendsEnumerateFollowingList_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((FriendsEnumerateFollowingList_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal ulong[] GSteamID; // m_rgSteamID class CSteamID [50]
			internal int ResultsReturned; // m_nResultsReturned int32
			internal int TotalResultCount; // m_nTotalResultCount int32
			
			public static implicit operator FriendsEnumerateFollowingList_t ( FriendsEnumerateFollowingList_t.Pack4 d ) => new FriendsEnumerateFollowingList_t{ Result = d.Result,GSteamID = d.GSteamID,ResultsReturned = d.ResultsReturned,TotalResultCount = d.TotalResultCount, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal ulong[] GSteamID; // m_rgSteamID class CSteamID [50]
			internal int ResultsReturned; // m_nResultsReturned int32
			internal int TotalResultCount; // m_nTotalResultCount int32
			
			public static implicit operator FriendsEnumerateFollowingList_t ( FriendsEnumerateFollowingList_t.Pack8 d ) => new FriendsEnumerateFollowingList_t{ Result = d.Result,GSteamID = d.GSteamID,ResultsReturned = d.ResultsReturned,TotalResultCount = d.TotalResultCount, };
		}
		#endregion
	}
	
	public struct SetPersonaNameResponse_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Success; // m_bSuccess _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool LocalSuccess; // m_bLocalSuccess _Bool
		internal Result Result; // m_result enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 47;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SetPersonaNameResponse_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SetPersonaNameResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool Success; // m_bSuccess _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool LocalSuccess; // m_bLocalSuccess _Bool
			internal Result Result; // m_result enum EResult
			
			public static implicit operator SetPersonaNameResponse_t ( SetPersonaNameResponse_t.Pack4 d ) => new SetPersonaNameResponse_t{ Success = d.Success,LocalSuccess = d.LocalSuccess,Result = d.Result, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool Success; // m_bSuccess _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool LocalSuccess; // m_bLocalSuccess _Bool
			internal Result Result; // m_result enum EResult
			
			public static implicit operator SetPersonaNameResponse_t ( SetPersonaNameResponse_t.Pack8 d ) => new SetPersonaNameResponse_t{ Success = d.Success,LocalSuccess = d.LocalSuccess,Result = d.Result, };
		}
		#endregion
	}
	
	public struct LowBatteryPower_t : Steamworks.ISteamCallback
	{
		internal byte MinutesBatteryLeft; // m_nMinutesBatteryLeft uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUtils + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((LowBatteryPower_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((LowBatteryPower_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal byte MinutesBatteryLeft; // m_nMinutesBatteryLeft uint8
			
			public static implicit operator LowBatteryPower_t ( LowBatteryPower_t.Pack4 d ) => new LowBatteryPower_t{ MinutesBatteryLeft = d.MinutesBatteryLeft, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal byte MinutesBatteryLeft; // m_nMinutesBatteryLeft uint8
			
			public static implicit operator LowBatteryPower_t ( LowBatteryPower_t.Pack8 d ) => new LowBatteryPower_t{ MinutesBatteryLeft = d.MinutesBatteryLeft, };
		}
		#endregion
	}
	
	public struct SteamAPICallCompleted_t : Steamworks.ISteamCallback
	{
		internal ulong AsyncCall; // m_hAsyncCall SteamAPICall_t
		internal int Callback; // m_iCallback int
		internal uint ParamCount; // m_cubParam uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUtils + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SteamAPICallCompleted_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamAPICallCompleted_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong AsyncCall; // m_hAsyncCall SteamAPICall_t
			internal int Callback; // m_iCallback int
			internal uint ParamCount; // m_cubParam uint32
			
			public static implicit operator SteamAPICallCompleted_t ( SteamAPICallCompleted_t.Pack4 d ) => new SteamAPICallCompleted_t{ AsyncCall = d.AsyncCall,Callback = d.Callback,ParamCount = d.ParamCount, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong AsyncCall; // m_hAsyncCall SteamAPICall_t
			internal int Callback; // m_iCallback int
			internal uint ParamCount; // m_cubParam uint32
			
			public static implicit operator SteamAPICallCompleted_t ( SteamAPICallCompleted_t.Pack8 d ) => new SteamAPICallCompleted_t{ AsyncCall = d.AsyncCall,Callback = d.Callback,ParamCount = d.ParamCount, };
		}
		#endregion
	}
	
	public struct CheckFileSignature_t : Steamworks.ISteamCallback
	{
		internal CheckFileSignature CheckFileSignature; // m_eCheckFileSignature enum ECheckFileSignature
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUtils + 5;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((CheckFileSignature_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((CheckFileSignature_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal CheckFileSignature CheckFileSignature; // m_eCheckFileSignature enum ECheckFileSignature
			
			public static implicit operator CheckFileSignature_t ( CheckFileSignature_t.Pack4 d ) => new CheckFileSignature_t{ CheckFileSignature = d.CheckFileSignature, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal CheckFileSignature CheckFileSignature; // m_eCheckFileSignature enum ECheckFileSignature
			
			public static implicit operator CheckFileSignature_t ( CheckFileSignature_t.Pack8 d ) => new CheckFileSignature_t{ CheckFileSignature = d.CheckFileSignature, };
		}
		#endregion
	}
	
	public struct GamepadTextInputDismissed_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Submitted; // m_bSubmitted _Bool
		internal uint SubmittedText; // m_unSubmittedText uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUtils + 14;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GamepadTextInputDismissed_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GamepadTextInputDismissed_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool Submitted; // m_bSubmitted _Bool
			internal uint SubmittedText; // m_unSubmittedText uint32
			
			public static implicit operator GamepadTextInputDismissed_t ( GamepadTextInputDismissed_t.Pack4 d ) => new GamepadTextInputDismissed_t{ Submitted = d.Submitted,SubmittedText = d.SubmittedText, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool Submitted; // m_bSubmitted _Bool
			internal uint SubmittedText; // m_unSubmittedText uint32
			
			public static implicit operator GamepadTextInputDismissed_t ( GamepadTextInputDismissed_t.Pack8 d ) => new GamepadTextInputDismissed_t{ Submitted = d.Submitted,SubmittedText = d.SubmittedText, };
		}
		#endregion
	}
	
	public struct MatchMakingKeyValuePair_t
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string Key; // m_szKey char [256]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string Value; // m_szValue char [256]
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public MatchMakingKeyValuePair_t Fill( IntPtr p ) => Platform.PackSmall ? ((MatchMakingKeyValuePair_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((MatchMakingKeyValuePair_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string Key; // m_szKey char [256]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string Value; // m_szValue char [256]
			
			public static implicit operator MatchMakingKeyValuePair_t ( MatchMakingKeyValuePair_t.Pack4 d ) => new MatchMakingKeyValuePair_t{ Key = d.Key,Value = d.Value, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string Key; // m_szKey char [256]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string Value; // m_szValue char [256]
			
			public static implicit operator MatchMakingKeyValuePair_t ( MatchMakingKeyValuePair_t.Pack8 d ) => new MatchMakingKeyValuePair_t{ Key = d.Key,Value = d.Value, };
		}
		#endregion
	}
	
	public struct servernetadr_t
	{
		internal ushort ConnectionPort; // m_usConnectionPort uint16
		internal ushort QueryPort; // m_usQueryPort uint16
		internal uint IP; // m_unIP uint32
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public servernetadr_t Fill( IntPtr p ) => Platform.PackSmall ? ((servernetadr_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((servernetadr_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ushort ConnectionPort; // m_usConnectionPort uint16
			internal ushort QueryPort; // m_usQueryPort uint16
			internal uint IP; // m_unIP uint32
			
			public static implicit operator servernetadr_t ( servernetadr_t.Pack4 d ) => new servernetadr_t{ ConnectionPort = d.ConnectionPort,QueryPort = d.QueryPort,IP = d.IP, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ushort ConnectionPort; // m_usConnectionPort uint16
			internal ushort QueryPort; // m_usQueryPort uint16
			internal uint IP; // m_unIP uint32
			
			public static implicit operator servernetadr_t ( servernetadr_t.Pack8 d ) => new servernetadr_t{ ConnectionPort = d.ConnectionPort,QueryPort = d.QueryPort,IP = d.IP, };
		}
		#endregion
	}
	
	public struct gameserveritem_t
	{
		internal servernetadr_t NetAdr; // m_NetAdr class servernetadr_t
		internal int Ping; // m_nPing int
		[MarshalAs(UnmanagedType.I1)]
		internal bool HadSuccessfulResponse; // m_bHadSuccessfulResponse _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool DoNotRefresh; // m_bDoNotRefresh _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		internal string GameDir; // m_szGameDir char [32]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		internal string Map; // m_szMap char [32]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		internal string GameDescription; // m_szGameDescription char [64]
		internal uint AppID; // m_nAppID uint32
		internal int Players; // m_nPlayers int
		internal int MaxPlayers; // m_nMaxPlayers int
		internal int BotPlayers; // m_nBotPlayers int
		[MarshalAs(UnmanagedType.I1)]
		internal bool Password; // m_bPassword _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool Secure; // m_bSecure _Bool
		internal uint TimeLastPlayed; // m_ulTimeLastPlayed uint32
		internal int ServerVersion; // m_nServerVersion int
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		internal string ServerName; // m_szServerName char [64]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string GameTags; // m_szGameTags char [128]
		internal ulong SteamID; // m_steamID class CSteamID
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public gameserveritem_t Fill( IntPtr p ) => Platform.PackSmall ? ((gameserveritem_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((gameserveritem_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal servernetadr_t NetAdr; // m_NetAdr class servernetadr_t
			internal int Ping; // m_nPing int
			[MarshalAs(UnmanagedType.I1)]
			internal bool HadSuccessfulResponse; // m_bHadSuccessfulResponse _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool DoNotRefresh; // m_bDoNotRefresh _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			internal string GameDir; // m_szGameDir char [32]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			internal string Map; // m_szMap char [32]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			internal string GameDescription; // m_szGameDescription char [64]
			internal uint AppID; // m_nAppID uint32
			internal int Players; // m_nPlayers int
			internal int MaxPlayers; // m_nMaxPlayers int
			internal int BotPlayers; // m_nBotPlayers int
			[MarshalAs(UnmanagedType.I1)]
			internal bool Password; // m_bPassword _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool Secure; // m_bSecure _Bool
			internal uint TimeLastPlayed; // m_ulTimeLastPlayed uint32
			internal int ServerVersion; // m_nServerVersion int
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			internal string ServerName; // m_szServerName char [64]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string GameTags; // m_szGameTags char [128]
			internal ulong SteamID; // m_steamID class CSteamID
			
			public static implicit operator gameserveritem_t ( gameserveritem_t.Pack4 d ) => new gameserveritem_t{ NetAdr = d.NetAdr,Ping = d.Ping,HadSuccessfulResponse = d.HadSuccessfulResponse,DoNotRefresh = d.DoNotRefresh,GameDir = d.GameDir,Map = d.Map,GameDescription = d.GameDescription,AppID = d.AppID,Players = d.Players,MaxPlayers = d.MaxPlayers,BotPlayers = d.BotPlayers,Password = d.Password,Secure = d.Secure,TimeLastPlayed = d.TimeLastPlayed,ServerVersion = d.ServerVersion,ServerName = d.ServerName,GameTags = d.GameTags,SteamID = d.SteamID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal servernetadr_t NetAdr; // m_NetAdr class servernetadr_t
			internal int Ping; // m_nPing int
			[MarshalAs(UnmanagedType.I1)]
			internal bool HadSuccessfulResponse; // m_bHadSuccessfulResponse _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool DoNotRefresh; // m_bDoNotRefresh _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			internal string GameDir; // m_szGameDir char [32]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			internal string Map; // m_szMap char [32]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			internal string GameDescription; // m_szGameDescription char [64]
			internal uint AppID; // m_nAppID uint32
			internal int Players; // m_nPlayers int
			internal int MaxPlayers; // m_nMaxPlayers int
			internal int BotPlayers; // m_nBotPlayers int
			[MarshalAs(UnmanagedType.I1)]
			internal bool Password; // m_bPassword _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool Secure; // m_bSecure _Bool
			internal uint TimeLastPlayed; // m_ulTimeLastPlayed uint32
			internal int ServerVersion; // m_nServerVersion int
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			internal string ServerName; // m_szServerName char [64]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string GameTags; // m_szGameTags char [128]
			internal ulong SteamID; // m_steamID class CSteamID
			
			public static implicit operator gameserveritem_t ( gameserveritem_t.Pack8 d ) => new gameserveritem_t{ NetAdr = d.NetAdr,Ping = d.Ping,HadSuccessfulResponse = d.HadSuccessfulResponse,DoNotRefresh = d.DoNotRefresh,GameDir = d.GameDir,Map = d.Map,GameDescription = d.GameDescription,AppID = d.AppID,Players = d.Players,MaxPlayers = d.MaxPlayers,BotPlayers = d.BotPlayers,Password = d.Password,Secure = d.Secure,TimeLastPlayed = d.TimeLastPlayed,ServerVersion = d.ServerVersion,ServerName = d.ServerName,GameTags = d.GameTags,SteamID = d.SteamID, };
		}
		#endregion
	}
	
	public struct SteamPartyBeaconLocation_t
	{
		internal SteamPartyBeaconLocationType Type; // m_eType enum ESteamPartyBeaconLocationType
		internal ulong LocationID; // m_ulLocationID uint64
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public SteamPartyBeaconLocation_t Fill( IntPtr p ) => Platform.PackSmall ? ((SteamPartyBeaconLocation_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamPartyBeaconLocation_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal SteamPartyBeaconLocationType Type; // m_eType enum ESteamPartyBeaconLocationType
			internal ulong LocationID; // m_ulLocationID uint64
			
			public static implicit operator SteamPartyBeaconLocation_t ( SteamPartyBeaconLocation_t.Pack4 d ) => new SteamPartyBeaconLocation_t{ Type = d.Type,LocationID = d.LocationID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal SteamPartyBeaconLocationType Type; // m_eType enum ESteamPartyBeaconLocationType
			internal ulong LocationID; // m_ulLocationID uint64
			
			public static implicit operator SteamPartyBeaconLocation_t ( SteamPartyBeaconLocation_t.Pack8 d ) => new SteamPartyBeaconLocation_t{ Type = d.Type,LocationID = d.LocationID, };
		}
		#endregion
	}
	
	public struct FavoritesListChanged_t : Steamworks.ISteamCallback
	{
		internal uint IP; // m_nIP uint32
		internal uint QueryPort; // m_nQueryPort uint32
		internal uint ConnPort; // m_nConnPort uint32
		internal uint AppID; // m_nAppID uint32
		internal uint Flags; // m_nFlags uint32
		[MarshalAs(UnmanagedType.I1)]
		internal bool Add; // m_bAdd _Bool
		internal uint AccountId; // m_unAccountId AccountID_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((FavoritesListChanged_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((FavoritesListChanged_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint IP; // m_nIP uint32
			internal uint QueryPort; // m_nQueryPort uint32
			internal uint ConnPort; // m_nConnPort uint32
			internal uint AppID; // m_nAppID uint32
			internal uint Flags; // m_nFlags uint32
			[MarshalAs(UnmanagedType.I1)]
			internal bool Add; // m_bAdd _Bool
			internal uint AccountId; // m_unAccountId AccountID_t
			
			public static implicit operator FavoritesListChanged_t ( FavoritesListChanged_t.Pack4 d ) => new FavoritesListChanged_t{ IP = d.IP,QueryPort = d.QueryPort,ConnPort = d.ConnPort,AppID = d.AppID,Flags = d.Flags,Add = d.Add,AccountId = d.AccountId, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint IP; // m_nIP uint32
			internal uint QueryPort; // m_nQueryPort uint32
			internal uint ConnPort; // m_nConnPort uint32
			internal uint AppID; // m_nAppID uint32
			internal uint Flags; // m_nFlags uint32
			[MarshalAs(UnmanagedType.I1)]
			internal bool Add; // m_bAdd _Bool
			internal uint AccountId; // m_unAccountId AccountID_t
			
			public static implicit operator FavoritesListChanged_t ( FavoritesListChanged_t.Pack8 d ) => new FavoritesListChanged_t{ IP = d.IP,QueryPort = d.QueryPort,ConnPort = d.ConnPort,AppID = d.AppID,Flags = d.Flags,Add = d.Add,AccountId = d.AccountId, };
		}
		#endregion
	}
	
	public struct LobbyInvite_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDUser; // m_ulSteamIDUser uint64
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong GameID; // m_ulGameID uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((LobbyInvite_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((LobbyInvite_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDUser; // m_ulSteamIDUser uint64
			internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			internal ulong GameID; // m_ulGameID uint64
			
			public static implicit operator LobbyInvite_t ( LobbyInvite_t.Pack4 d ) => new LobbyInvite_t{ SteamIDUser = d.SteamIDUser,SteamIDLobby = d.SteamIDLobby,GameID = d.GameID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong SteamIDUser; // m_ulSteamIDUser uint64
			internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			internal ulong GameID; // m_ulGameID uint64
			
			public static implicit operator LobbyInvite_t ( LobbyInvite_t.Pack8 d ) => new LobbyInvite_t{ SteamIDUser = d.SteamIDUser,SteamIDLobby = d.SteamIDLobby,GameID = d.GameID, };
		}
		#endregion
	}
	
	public struct LobbyEnter_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal uint GfChatPermissions; // m_rgfChatPermissions uint32
		[MarshalAs(UnmanagedType.I1)]
		internal bool Locked; // m_bLocked _Bool
		internal uint EChatRoomEnterResponse; // m_EChatRoomEnterResponse uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 4;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((LobbyEnter_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((LobbyEnter_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			internal uint GfChatPermissions; // m_rgfChatPermissions uint32
			[MarshalAs(UnmanagedType.I1)]
			internal bool Locked; // m_bLocked _Bool
			internal uint EChatRoomEnterResponse; // m_EChatRoomEnterResponse uint32
			
			public static implicit operator LobbyEnter_t ( LobbyEnter_t.Pack4 d ) => new LobbyEnter_t{ SteamIDLobby = d.SteamIDLobby,GfChatPermissions = d.GfChatPermissions,Locked = d.Locked,EChatRoomEnterResponse = d.EChatRoomEnterResponse, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			internal uint GfChatPermissions; // m_rgfChatPermissions uint32
			[MarshalAs(UnmanagedType.I1)]
			internal bool Locked; // m_bLocked _Bool
			internal uint EChatRoomEnterResponse; // m_EChatRoomEnterResponse uint32
			
			public static implicit operator LobbyEnter_t ( LobbyEnter_t.Pack8 d ) => new LobbyEnter_t{ SteamIDLobby = d.SteamIDLobby,GfChatPermissions = d.GfChatPermissions,Locked = d.Locked,EChatRoomEnterResponse = d.EChatRoomEnterResponse, };
		}
		#endregion
	}
	
	public struct LobbyDataUpdate_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDMember; // m_ulSteamIDMember uint64
		internal byte Success; // m_bSuccess uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 5;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((LobbyDataUpdate_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((LobbyDataUpdate_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			internal ulong SteamIDMember; // m_ulSteamIDMember uint64
			internal byte Success; // m_bSuccess uint8
			
			public static implicit operator LobbyDataUpdate_t ( LobbyDataUpdate_t.Pack4 d ) => new LobbyDataUpdate_t{ SteamIDLobby = d.SteamIDLobby,SteamIDMember = d.SteamIDMember,Success = d.Success, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			internal ulong SteamIDMember; // m_ulSteamIDMember uint64
			internal byte Success; // m_bSuccess uint8
			
			public static implicit operator LobbyDataUpdate_t ( LobbyDataUpdate_t.Pack8 d ) => new LobbyDataUpdate_t{ SteamIDLobby = d.SteamIDLobby,SteamIDMember = d.SteamIDMember,Success = d.Success, };
		}
		#endregion
	}
	
	public struct LobbyChatUpdate_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDUserChanged; // m_ulSteamIDUserChanged uint64
		internal ulong SteamIDMakingChange; // m_ulSteamIDMakingChange uint64
		internal uint GfChatMemberStateChange; // m_rgfChatMemberStateChange uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 6;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((LobbyChatUpdate_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((LobbyChatUpdate_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			internal ulong SteamIDUserChanged; // m_ulSteamIDUserChanged uint64
			internal ulong SteamIDMakingChange; // m_ulSteamIDMakingChange uint64
			internal uint GfChatMemberStateChange; // m_rgfChatMemberStateChange uint32
			
			public static implicit operator LobbyChatUpdate_t ( LobbyChatUpdate_t.Pack4 d ) => new LobbyChatUpdate_t{ SteamIDLobby = d.SteamIDLobby,SteamIDUserChanged = d.SteamIDUserChanged,SteamIDMakingChange = d.SteamIDMakingChange,GfChatMemberStateChange = d.GfChatMemberStateChange, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			internal ulong SteamIDUserChanged; // m_ulSteamIDUserChanged uint64
			internal ulong SteamIDMakingChange; // m_ulSteamIDMakingChange uint64
			internal uint GfChatMemberStateChange; // m_rgfChatMemberStateChange uint32
			
			public static implicit operator LobbyChatUpdate_t ( LobbyChatUpdate_t.Pack8 d ) => new LobbyChatUpdate_t{ SteamIDLobby = d.SteamIDLobby,SteamIDUserChanged = d.SteamIDUserChanged,SteamIDMakingChange = d.SteamIDMakingChange,GfChatMemberStateChange = d.GfChatMemberStateChange, };
		}
		#endregion
	}
	
	public struct LobbyChatMsg_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDUser; // m_ulSteamIDUser uint64
		internal byte ChatEntryType; // m_eChatEntryType uint8
		internal uint ChatID; // m_iChatID uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 7;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((LobbyChatMsg_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((LobbyChatMsg_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			internal ulong SteamIDUser; // m_ulSteamIDUser uint64
			internal byte ChatEntryType; // m_eChatEntryType uint8
			internal uint ChatID; // m_iChatID uint32
			
			public static implicit operator LobbyChatMsg_t ( LobbyChatMsg_t.Pack4 d ) => new LobbyChatMsg_t{ SteamIDLobby = d.SteamIDLobby,SteamIDUser = d.SteamIDUser,ChatEntryType = d.ChatEntryType,ChatID = d.ChatID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			internal ulong SteamIDUser; // m_ulSteamIDUser uint64
			internal byte ChatEntryType; // m_eChatEntryType uint8
			internal uint ChatID; // m_iChatID uint32
			
			public static implicit operator LobbyChatMsg_t ( LobbyChatMsg_t.Pack8 d ) => new LobbyChatMsg_t{ SteamIDLobby = d.SteamIDLobby,SteamIDUser = d.SteamIDUser,ChatEntryType = d.ChatEntryType,ChatID = d.ChatID, };
		}
		#endregion
	}
	
	public struct LobbyGameCreated_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDGameServer; // m_ulSteamIDGameServer uint64
		internal uint IP; // m_unIP uint32
		internal ushort Port; // m_usPort uint16
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 9;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((LobbyGameCreated_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((LobbyGameCreated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			internal ulong SteamIDGameServer; // m_ulSteamIDGameServer uint64
			internal uint IP; // m_unIP uint32
			internal ushort Port; // m_usPort uint16
			
			public static implicit operator LobbyGameCreated_t ( LobbyGameCreated_t.Pack4 d ) => new LobbyGameCreated_t{ SteamIDLobby = d.SteamIDLobby,SteamIDGameServer = d.SteamIDGameServer,IP = d.IP,Port = d.Port, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			internal ulong SteamIDGameServer; // m_ulSteamIDGameServer uint64
			internal uint IP; // m_unIP uint32
			internal ushort Port; // m_usPort uint16
			
			public static implicit operator LobbyGameCreated_t ( LobbyGameCreated_t.Pack8 d ) => new LobbyGameCreated_t{ SteamIDLobby = d.SteamIDLobby,SteamIDGameServer = d.SteamIDGameServer,IP = d.IP,Port = d.Port, };
		}
		#endregion
	}
	
	public struct LobbyMatchList_t : Steamworks.ISteamCallback
	{
		internal uint LobbiesMatching; // m_nLobbiesMatching uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 10;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((LobbyMatchList_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((LobbyMatchList_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint LobbiesMatching; // m_nLobbiesMatching uint32
			
			public static implicit operator LobbyMatchList_t ( LobbyMatchList_t.Pack4 d ) => new LobbyMatchList_t{ LobbiesMatching = d.LobbiesMatching, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint LobbiesMatching; // m_nLobbiesMatching uint32
			
			public static implicit operator LobbyMatchList_t ( LobbyMatchList_t.Pack8 d ) => new LobbyMatchList_t{ LobbiesMatching = d.LobbiesMatching, };
		}
		#endregion
	}
	
	public struct LobbyKicked_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDAdmin; // m_ulSteamIDAdmin uint64
		internal byte KickedDueToDisconnect; // m_bKickedDueToDisconnect uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 12;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((LobbyKicked_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((LobbyKicked_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			internal ulong SteamIDAdmin; // m_ulSteamIDAdmin uint64
			internal byte KickedDueToDisconnect; // m_bKickedDueToDisconnect uint8
			
			public static implicit operator LobbyKicked_t ( LobbyKicked_t.Pack4 d ) => new LobbyKicked_t{ SteamIDLobby = d.SteamIDLobby,SteamIDAdmin = d.SteamIDAdmin,KickedDueToDisconnect = d.KickedDueToDisconnect, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			internal ulong SteamIDAdmin; // m_ulSteamIDAdmin uint64
			internal byte KickedDueToDisconnect; // m_bKickedDueToDisconnect uint8
			
			public static implicit operator LobbyKicked_t ( LobbyKicked_t.Pack8 d ) => new LobbyKicked_t{ SteamIDLobby = d.SteamIDLobby,SteamIDAdmin = d.SteamIDAdmin,KickedDueToDisconnect = d.KickedDueToDisconnect, };
		}
		#endregion
	}
	
	public struct LobbyCreated_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 13;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((LobbyCreated_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((LobbyCreated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			
			public static implicit operator LobbyCreated_t ( LobbyCreated_t.Pack4 d ) => new LobbyCreated_t{ Result = d.Result,SteamIDLobby = d.SteamIDLobby, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			
			public static implicit operator LobbyCreated_t ( LobbyCreated_t.Pack8 d ) => new LobbyCreated_t{ Result = d.Result,SteamIDLobby = d.SteamIDLobby, };
		}
		#endregion
	}
	
	public struct PSNGameBootInviteResult_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool GameBootInviteExists; // m_bGameBootInviteExists _Bool
		internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 15;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((PSNGameBootInviteResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((PSNGameBootInviteResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool GameBootInviteExists; // m_bGameBootInviteExists _Bool
			internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
			
			public static implicit operator PSNGameBootInviteResult_t ( PSNGameBootInviteResult_t.Pack4 d ) => new PSNGameBootInviteResult_t{ GameBootInviteExists = d.GameBootInviteExists,SteamIDLobby = d.SteamIDLobby, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool GameBootInviteExists; // m_bGameBootInviteExists _Bool
			internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
			
			public static implicit operator PSNGameBootInviteResult_t ( PSNGameBootInviteResult_t.Pack8 d ) => new PSNGameBootInviteResult_t{ GameBootInviteExists = d.GameBootInviteExists,SteamIDLobby = d.SteamIDLobby, };
		}
		#endregion
	}
	
	public struct FavoritesListAccountsUpdated_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 16;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((FavoritesListAccountsUpdated_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((FavoritesListAccountsUpdated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator FavoritesListAccountsUpdated_t ( FavoritesListAccountsUpdated_t.Pack4 d ) => new FavoritesListAccountsUpdated_t{ Result = d.Result, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator FavoritesListAccountsUpdated_t ( FavoritesListAccountsUpdated_t.Pack8 d ) => new FavoritesListAccountsUpdated_t{ Result = d.Result, };
		}
		#endregion
	}
	
	public struct SearchForGameProgressCallback_t : Steamworks.ISteamCallback
	{
		internal ulong LSearchID; // m_ullSearchID uint64
		internal Result Result; // m_eResult enum EResult
		internal ulong LobbyID; // m_lobbyID class CSteamID
		internal ulong SteamIDEndedSearch; // m_steamIDEndedSearch class CSteamID
		internal int SecondsRemainingEstimate; // m_nSecondsRemainingEstimate int32
		internal int CPlayersSearching; // m_cPlayersSearching int32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameSearch + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SearchForGameProgressCallback_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SearchForGameProgressCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong LSearchID; // m_ullSearchID uint64
			internal Result Result; // m_eResult enum EResult
			internal ulong LobbyID; // m_lobbyID class CSteamID
			internal ulong SteamIDEndedSearch; // m_steamIDEndedSearch class CSteamID
			internal int SecondsRemainingEstimate; // m_nSecondsRemainingEstimate int32
			internal int CPlayersSearching; // m_cPlayersSearching int32
			
			public static implicit operator SearchForGameProgressCallback_t ( SearchForGameProgressCallback_t.Pack4 d ) => new SearchForGameProgressCallback_t{ LSearchID = d.LSearchID,Result = d.Result,LobbyID = d.LobbyID,SteamIDEndedSearch = d.SteamIDEndedSearch,SecondsRemainingEstimate = d.SecondsRemainingEstimate,CPlayersSearching = d.CPlayersSearching, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong LSearchID; // m_ullSearchID uint64
			internal Result Result; // m_eResult enum EResult
			internal ulong LobbyID; // m_lobbyID class CSteamID
			internal ulong SteamIDEndedSearch; // m_steamIDEndedSearch class CSteamID
			internal int SecondsRemainingEstimate; // m_nSecondsRemainingEstimate int32
			internal int CPlayersSearching; // m_cPlayersSearching int32
			
			public static implicit operator SearchForGameProgressCallback_t ( SearchForGameProgressCallback_t.Pack8 d ) => new SearchForGameProgressCallback_t{ LSearchID = d.LSearchID,Result = d.Result,LobbyID = d.LobbyID,SteamIDEndedSearch = d.SteamIDEndedSearch,SecondsRemainingEstimate = d.SecondsRemainingEstimate,CPlayersSearching = d.CPlayersSearching, };
		}
		#endregion
	}
	
	public struct SearchForGameResultCallback_t : Steamworks.ISteamCallback
	{
		internal ulong LSearchID; // m_ullSearchID uint64
		internal Result Result; // m_eResult enum EResult
		internal int CountPlayersInGame; // m_nCountPlayersInGame int32
		internal int CountAcceptedGame; // m_nCountAcceptedGame int32
		internal ulong SteamIDHost; // m_steamIDHost class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool FinalCallback; // m_bFinalCallback _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameSearch + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SearchForGameResultCallback_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SearchForGameResultCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong LSearchID; // m_ullSearchID uint64
			internal Result Result; // m_eResult enum EResult
			internal int CountPlayersInGame; // m_nCountPlayersInGame int32
			internal int CountAcceptedGame; // m_nCountAcceptedGame int32
			internal ulong SteamIDHost; // m_steamIDHost class CSteamID
			[MarshalAs(UnmanagedType.I1)]
			internal bool FinalCallback; // m_bFinalCallback _Bool
			
			public static implicit operator SearchForGameResultCallback_t ( SearchForGameResultCallback_t.Pack4 d ) => new SearchForGameResultCallback_t{ LSearchID = d.LSearchID,Result = d.Result,CountPlayersInGame = d.CountPlayersInGame,CountAcceptedGame = d.CountAcceptedGame,SteamIDHost = d.SteamIDHost,FinalCallback = d.FinalCallback, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong LSearchID; // m_ullSearchID uint64
			internal Result Result; // m_eResult enum EResult
			internal int CountPlayersInGame; // m_nCountPlayersInGame int32
			internal int CountAcceptedGame; // m_nCountAcceptedGame int32
			internal ulong SteamIDHost; // m_steamIDHost class CSteamID
			[MarshalAs(UnmanagedType.I1)]
			internal bool FinalCallback; // m_bFinalCallback _Bool
			
			public static implicit operator SearchForGameResultCallback_t ( SearchForGameResultCallback_t.Pack8 d ) => new SearchForGameResultCallback_t{ LSearchID = d.LSearchID,Result = d.Result,CountPlayersInGame = d.CountPlayersInGame,CountAcceptedGame = d.CountAcceptedGame,SteamIDHost = d.SteamIDHost,FinalCallback = d.FinalCallback, };
		}
		#endregion
	}
	
	public struct RequestPlayersForGameProgressCallback_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong LSearchID; // m_ullSearchID uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameSearch + 11;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RequestPlayersForGameProgressCallback_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RequestPlayersForGameProgressCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong LSearchID; // m_ullSearchID uint64
			
			public static implicit operator RequestPlayersForGameProgressCallback_t ( RequestPlayersForGameProgressCallback_t.Pack4 d ) => new RequestPlayersForGameProgressCallback_t{ Result = d.Result,LSearchID = d.LSearchID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong LSearchID; // m_ullSearchID uint64
			
			public static implicit operator RequestPlayersForGameProgressCallback_t ( RequestPlayersForGameProgressCallback_t.Pack8 d ) => new RequestPlayersForGameProgressCallback_t{ Result = d.Result,LSearchID = d.LSearchID, };
		}
		#endregion
	}
	
	public struct RequestPlayersForGameResultCallback_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong LSearchID; // m_ullSearchID uint64
		internal ulong SteamIDPlayerFound; // m_SteamIDPlayerFound class CSteamID
		internal ulong SteamIDLobby; // m_SteamIDLobby class CSteamID
		internal PlayerAcceptState_t PlayerAcceptState; // m_ePlayerAcceptState PlayerAcceptState_t
		internal int PlayerIndex; // m_nPlayerIndex int32
		internal int TotalPlayersFound; // m_nTotalPlayersFound int32
		internal int TotalPlayersAcceptedGame; // m_nTotalPlayersAcceptedGame int32
		internal int SuggestedTeamIndex; // m_nSuggestedTeamIndex int32
		internal ulong LUniqueGameID; // m_ullUniqueGameID uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameSearch + 12;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RequestPlayersForGameResultCallback_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RequestPlayersForGameResultCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong LSearchID; // m_ullSearchID uint64
			internal ulong SteamIDPlayerFound; // m_SteamIDPlayerFound class CSteamID
			internal ulong SteamIDLobby; // m_SteamIDLobby class CSteamID
			internal PlayerAcceptState_t PlayerAcceptState; // m_ePlayerAcceptState PlayerAcceptState_t
			internal int PlayerIndex; // m_nPlayerIndex int32
			internal int TotalPlayersFound; // m_nTotalPlayersFound int32
			internal int TotalPlayersAcceptedGame; // m_nTotalPlayersAcceptedGame int32
			internal int SuggestedTeamIndex; // m_nSuggestedTeamIndex int32
			internal ulong LUniqueGameID; // m_ullUniqueGameID uint64
			
			public static implicit operator RequestPlayersForGameResultCallback_t ( RequestPlayersForGameResultCallback_t.Pack4 d ) => new RequestPlayersForGameResultCallback_t{ Result = d.Result,LSearchID = d.LSearchID,SteamIDPlayerFound = d.SteamIDPlayerFound,SteamIDLobby = d.SteamIDLobby,PlayerAcceptState = d.PlayerAcceptState,PlayerIndex = d.PlayerIndex,TotalPlayersFound = d.TotalPlayersFound,TotalPlayersAcceptedGame = d.TotalPlayersAcceptedGame,SuggestedTeamIndex = d.SuggestedTeamIndex,LUniqueGameID = d.LUniqueGameID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong LSearchID; // m_ullSearchID uint64
			internal ulong SteamIDPlayerFound; // m_SteamIDPlayerFound class CSteamID
			internal ulong SteamIDLobby; // m_SteamIDLobby class CSteamID
			internal PlayerAcceptState_t PlayerAcceptState; // m_ePlayerAcceptState PlayerAcceptState_t
			internal int PlayerIndex; // m_nPlayerIndex int32
			internal int TotalPlayersFound; // m_nTotalPlayersFound int32
			internal int TotalPlayersAcceptedGame; // m_nTotalPlayersAcceptedGame int32
			internal int SuggestedTeamIndex; // m_nSuggestedTeamIndex int32
			internal ulong LUniqueGameID; // m_ullUniqueGameID uint64
			
			public static implicit operator RequestPlayersForGameResultCallback_t ( RequestPlayersForGameResultCallback_t.Pack8 d ) => new RequestPlayersForGameResultCallback_t{ Result = d.Result,LSearchID = d.LSearchID,SteamIDPlayerFound = d.SteamIDPlayerFound,SteamIDLobby = d.SteamIDLobby,PlayerAcceptState = d.PlayerAcceptState,PlayerIndex = d.PlayerIndex,TotalPlayersFound = d.TotalPlayersFound,TotalPlayersAcceptedGame = d.TotalPlayersAcceptedGame,SuggestedTeamIndex = d.SuggestedTeamIndex,LUniqueGameID = d.LUniqueGameID, };
		}
		#endregion
	}
	
	public struct RequestPlayersForGameFinalResultCallback_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong LSearchID; // m_ullSearchID uint64
		internal ulong LUniqueGameID; // m_ullUniqueGameID uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameSearch + 13;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RequestPlayersForGameFinalResultCallback_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RequestPlayersForGameFinalResultCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong LSearchID; // m_ullSearchID uint64
			internal ulong LUniqueGameID; // m_ullUniqueGameID uint64
			
			public static implicit operator RequestPlayersForGameFinalResultCallback_t ( RequestPlayersForGameFinalResultCallback_t.Pack4 d ) => new RequestPlayersForGameFinalResultCallback_t{ Result = d.Result,LSearchID = d.LSearchID,LUniqueGameID = d.LUniqueGameID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong LSearchID; // m_ullSearchID uint64
			internal ulong LUniqueGameID; // m_ullUniqueGameID uint64
			
			public static implicit operator RequestPlayersForGameFinalResultCallback_t ( RequestPlayersForGameFinalResultCallback_t.Pack8 d ) => new RequestPlayersForGameFinalResultCallback_t{ Result = d.Result,LSearchID = d.LSearchID,LUniqueGameID = d.LUniqueGameID, };
		}
		#endregion
	}
	
	public struct SubmitPlayerResultResultCallback_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong UllUniqueGameID; // ullUniqueGameID uint64
		internal ulong SteamIDPlayer; // steamIDPlayer class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameSearch + 14;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SubmitPlayerResultResultCallback_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SubmitPlayerResultResultCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong UllUniqueGameID; // ullUniqueGameID uint64
			internal ulong SteamIDPlayer; // steamIDPlayer class CSteamID
			
			public static implicit operator SubmitPlayerResultResultCallback_t ( SubmitPlayerResultResultCallback_t.Pack4 d ) => new SubmitPlayerResultResultCallback_t{ Result = d.Result,UllUniqueGameID = d.UllUniqueGameID,SteamIDPlayer = d.SteamIDPlayer, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong UllUniqueGameID; // ullUniqueGameID uint64
			internal ulong SteamIDPlayer; // steamIDPlayer class CSteamID
			
			public static implicit operator SubmitPlayerResultResultCallback_t ( SubmitPlayerResultResultCallback_t.Pack8 d ) => new SubmitPlayerResultResultCallback_t{ Result = d.Result,UllUniqueGameID = d.UllUniqueGameID,SteamIDPlayer = d.SteamIDPlayer, };
		}
		#endregion
	}
	
	public struct EndGameResultCallback_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong UllUniqueGameID; // ullUniqueGameID uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameSearch + 15;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((EndGameResultCallback_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((EndGameResultCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong UllUniqueGameID; // ullUniqueGameID uint64
			
			public static implicit operator EndGameResultCallback_t ( EndGameResultCallback_t.Pack4 d ) => new EndGameResultCallback_t{ Result = d.Result,UllUniqueGameID = d.UllUniqueGameID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong UllUniqueGameID; // ullUniqueGameID uint64
			
			public static implicit operator EndGameResultCallback_t ( EndGameResultCallback_t.Pack8 d ) => new EndGameResultCallback_t{ Result = d.Result,UllUniqueGameID = d.UllUniqueGameID, };
		}
		#endregion
	}
	
	public struct JoinPartyCallback_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
		internal ulong SteamIDBeaconOwner; // m_SteamIDBeaconOwner class CSteamID
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string ConnectString; // m_rgchConnectString char [256]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamParties + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((JoinPartyCallback_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((JoinPartyCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
			internal ulong SteamIDBeaconOwner; // m_SteamIDBeaconOwner class CSteamID
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string ConnectString; // m_rgchConnectString char [256]
			
			public static implicit operator JoinPartyCallback_t ( JoinPartyCallback_t.Pack4 d ) => new JoinPartyCallback_t{ Result = d.Result,BeaconID = d.BeaconID,SteamIDBeaconOwner = d.SteamIDBeaconOwner,ConnectString = d.ConnectString, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
			internal ulong SteamIDBeaconOwner; // m_SteamIDBeaconOwner class CSteamID
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string ConnectString; // m_rgchConnectString char [256]
			
			public static implicit operator JoinPartyCallback_t ( JoinPartyCallback_t.Pack8 d ) => new JoinPartyCallback_t{ Result = d.Result,BeaconID = d.BeaconID,SteamIDBeaconOwner = d.SteamIDBeaconOwner,ConnectString = d.ConnectString, };
		}
		#endregion
	}
	
	public struct CreateBeaconCallback_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamParties + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((CreateBeaconCallback_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((CreateBeaconCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
			
			public static implicit operator CreateBeaconCallback_t ( CreateBeaconCallback_t.Pack4 d ) => new CreateBeaconCallback_t{ Result = d.Result,BeaconID = d.BeaconID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
			
			public static implicit operator CreateBeaconCallback_t ( CreateBeaconCallback_t.Pack8 d ) => new CreateBeaconCallback_t{ Result = d.Result,BeaconID = d.BeaconID, };
		}
		#endregion
	}
	
	public struct ReservationNotificationCallback_t : Steamworks.ISteamCallback
	{
		internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
		internal ulong SteamIDJoiner; // m_steamIDJoiner class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamParties + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((ReservationNotificationCallback_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((ReservationNotificationCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
			internal ulong SteamIDJoiner; // m_steamIDJoiner class CSteamID
			
			public static implicit operator ReservationNotificationCallback_t ( ReservationNotificationCallback_t.Pack4 d ) => new ReservationNotificationCallback_t{ BeaconID = d.BeaconID,SteamIDJoiner = d.SteamIDJoiner, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
			internal ulong SteamIDJoiner; // m_steamIDJoiner class CSteamID
			
			public static implicit operator ReservationNotificationCallback_t ( ReservationNotificationCallback_t.Pack8 d ) => new ReservationNotificationCallback_t{ BeaconID = d.BeaconID,SteamIDJoiner = d.SteamIDJoiner, };
		}
		#endregion
	}
	
	public struct ChangeNumOpenSlotsCallback_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamParties + 4;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((ChangeNumOpenSlotsCallback_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((ChangeNumOpenSlotsCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator ChangeNumOpenSlotsCallback_t ( ChangeNumOpenSlotsCallback_t.Pack4 d ) => new ChangeNumOpenSlotsCallback_t{ Result = d.Result, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator ChangeNumOpenSlotsCallback_t ( ChangeNumOpenSlotsCallback_t.Pack8 d ) => new ChangeNumOpenSlotsCallback_t{ Result = d.Result, };
		}
		#endregion
	}
	
	public struct SteamParamStringArray_t
	{
		internal IntPtr Strings; // m_ppStrings const char **
		internal int NumStrings; // m_nNumStrings int32
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public SteamParamStringArray_t Fill( IntPtr p ) => Platform.PackSmall ? ((SteamParamStringArray_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamParamStringArray_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal IntPtr Strings; // m_ppStrings const char **
			internal int NumStrings; // m_nNumStrings int32
			
			public static implicit operator SteamParamStringArray_t ( SteamParamStringArray_t.Pack4 d ) => new SteamParamStringArray_t{ Strings = d.Strings,NumStrings = d.NumStrings, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal IntPtr Strings; // m_ppStrings const char **
			internal int NumStrings; // m_nNumStrings int32
			
			public static implicit operator SteamParamStringArray_t ( SteamParamStringArray_t.Pack8 d ) => new SteamParamStringArray_t{ Strings = d.Strings,NumStrings = d.NumStrings, };
		}
		#endregion
	}
	
	public struct RemoteStorageAppSyncedClient_t : Steamworks.ISteamCallback
	{
		internal uint AppID; // m_nAppID AppId_t
		internal Result Result; // m_eResult enum EResult
		internal int NumDownloads; // m_unNumDownloads int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageAppSyncedClient_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageAppSyncedClient_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint AppID; // m_nAppID AppId_t
			internal Result Result; // m_eResult enum EResult
			internal int NumDownloads; // m_unNumDownloads int
			
			public static implicit operator RemoteStorageAppSyncedClient_t ( RemoteStorageAppSyncedClient_t.Pack4 d ) => new RemoteStorageAppSyncedClient_t{ AppID = d.AppID,Result = d.Result,NumDownloads = d.NumDownloads, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint AppID; // m_nAppID AppId_t
			internal Result Result; // m_eResult enum EResult
			internal int NumDownloads; // m_unNumDownloads int
			
			public static implicit operator RemoteStorageAppSyncedClient_t ( RemoteStorageAppSyncedClient_t.Pack8 d ) => new RemoteStorageAppSyncedClient_t{ AppID = d.AppID,Result = d.Result,NumDownloads = d.NumDownloads, };
		}
		#endregion
	}
	
	public struct RemoteStorageAppSyncedServer_t : Steamworks.ISteamCallback
	{
		internal uint AppID; // m_nAppID AppId_t
		internal Result Result; // m_eResult enum EResult
		internal int NumUploads; // m_unNumUploads int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageAppSyncedServer_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageAppSyncedServer_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint AppID; // m_nAppID AppId_t
			internal Result Result; // m_eResult enum EResult
			internal int NumUploads; // m_unNumUploads int
			
			public static implicit operator RemoteStorageAppSyncedServer_t ( RemoteStorageAppSyncedServer_t.Pack4 d ) => new RemoteStorageAppSyncedServer_t{ AppID = d.AppID,Result = d.Result,NumUploads = d.NumUploads, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint AppID; // m_nAppID AppId_t
			internal Result Result; // m_eResult enum EResult
			internal int NumUploads; // m_unNumUploads int
			
			public static implicit operator RemoteStorageAppSyncedServer_t ( RemoteStorageAppSyncedServer_t.Pack8 d ) => new RemoteStorageAppSyncedServer_t{ AppID = d.AppID,Result = d.Result,NumUploads = d.NumUploads, };
		}
		#endregion
	}
	
	public struct RemoteStorageAppSyncProgress_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string CurrentFile; // m_rgchCurrentFile char [260]
		internal uint AppID; // m_nAppID AppId_t
		internal uint BytesTransferredThisChunk; // m_uBytesTransferredThisChunk uint32
		internal double DAppPercentComplete; // m_dAppPercentComplete double
		[MarshalAs(UnmanagedType.I1)]
		internal bool Uploading; // m_bUploading _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageAppSyncProgress_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageAppSyncProgress_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string CurrentFile; // m_rgchCurrentFile char [260]
			internal uint AppID; // m_nAppID AppId_t
			internal uint BytesTransferredThisChunk; // m_uBytesTransferredThisChunk uint32
			internal double DAppPercentComplete; // m_dAppPercentComplete double
			[MarshalAs(UnmanagedType.I1)]
			internal bool Uploading; // m_bUploading _Bool
			
			public static implicit operator RemoteStorageAppSyncProgress_t ( RemoteStorageAppSyncProgress_t.Pack4 d ) => new RemoteStorageAppSyncProgress_t{ CurrentFile = d.CurrentFile,AppID = d.AppID,BytesTransferredThisChunk = d.BytesTransferredThisChunk,DAppPercentComplete = d.DAppPercentComplete,Uploading = d.Uploading, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string CurrentFile; // m_rgchCurrentFile char [260]
			internal uint AppID; // m_nAppID AppId_t
			internal uint BytesTransferredThisChunk; // m_uBytesTransferredThisChunk uint32
			internal double DAppPercentComplete; // m_dAppPercentComplete double
			[MarshalAs(UnmanagedType.I1)]
			internal bool Uploading; // m_bUploading _Bool
			
			public static implicit operator RemoteStorageAppSyncProgress_t ( RemoteStorageAppSyncProgress_t.Pack8 d ) => new RemoteStorageAppSyncProgress_t{ CurrentFile = d.CurrentFile,AppID = d.AppID,BytesTransferredThisChunk = d.BytesTransferredThisChunk,DAppPercentComplete = d.DAppPercentComplete,Uploading = d.Uploading, };
		}
		#endregion
	}
	
	public struct RemoteStorageAppSyncStatusCheck_t : Steamworks.ISteamCallback
	{
		internal uint AppID; // m_nAppID AppId_t
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 5;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageAppSyncStatusCheck_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageAppSyncStatusCheck_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint AppID; // m_nAppID AppId_t
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator RemoteStorageAppSyncStatusCheck_t ( RemoteStorageAppSyncStatusCheck_t.Pack4 d ) => new RemoteStorageAppSyncStatusCheck_t{ AppID = d.AppID,Result = d.Result, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint AppID; // m_nAppID AppId_t
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator RemoteStorageAppSyncStatusCheck_t ( RemoteStorageAppSyncStatusCheck_t.Pack8 d ) => new RemoteStorageAppSyncStatusCheck_t{ AppID = d.AppID,Result = d.Result, };
		}
		#endregion
	}
	
	public struct RemoteStorageFileShareResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong File; // m_hFile UGCHandle_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string Filename; // m_rgchFilename char [260]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 7;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageFileShareResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageFileShareResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong File; // m_hFile UGCHandle_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string Filename; // m_rgchFilename char [260]
			
			public static implicit operator RemoteStorageFileShareResult_t ( RemoteStorageFileShareResult_t.Pack4 d ) => new RemoteStorageFileShareResult_t{ Result = d.Result,File = d.File,Filename = d.Filename, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong File; // m_hFile UGCHandle_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string Filename; // m_rgchFilename char [260]
			
			public static implicit operator RemoteStorageFileShareResult_t ( RemoteStorageFileShareResult_t.Pack8 d ) => new RemoteStorageFileShareResult_t{ Result = d.Result,File = d.File,Filename = d.Filename, };
		}
		#endregion
	}
	
	public struct RemoteStoragePublishFileResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 9;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStoragePublishFileResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStoragePublishFileResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			[MarshalAs(UnmanagedType.I1)]
			internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			
			public static implicit operator RemoteStoragePublishFileResult_t ( RemoteStoragePublishFileResult_t.Pack4 d ) => new RemoteStoragePublishFileResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			[MarshalAs(UnmanagedType.I1)]
			internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			
			public static implicit operator RemoteStoragePublishFileResult_t ( RemoteStoragePublishFileResult_t.Pack8 d ) => new RemoteStoragePublishFileResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement, };
		}
		#endregion
	}
	
	public struct RemoteStorageDeletePublishedFileResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 11;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageDeletePublishedFileResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageDeletePublishedFileResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoteStorageDeletePublishedFileResult_t ( RemoteStorageDeletePublishedFileResult_t.Pack4 d ) => new RemoteStorageDeletePublishedFileResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoteStorageDeletePublishedFileResult_t ( RemoteStorageDeletePublishedFileResult_t.Pack8 d ) => new RemoteStorageDeletePublishedFileResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId, };
		}
		#endregion
	}
	
	public struct RemoteStorageEnumerateUserPublishedFilesResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 12;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageEnumerateUserPublishedFilesResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageEnumerateUserPublishedFilesResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal int ResultsReturned; // m_nResultsReturned int32
			internal int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			
			public static implicit operator RemoteStorageEnumerateUserPublishedFilesResult_t ( RemoteStorageEnumerateUserPublishedFilesResult_t.Pack4 d ) => new RemoteStorageEnumerateUserPublishedFilesResult_t{ Result = d.Result,ResultsReturned = d.ResultsReturned,TotalResultCount = d.TotalResultCount,GPublishedFileId = d.GPublishedFileId, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal int ResultsReturned; // m_nResultsReturned int32
			internal int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			
			public static implicit operator RemoteStorageEnumerateUserPublishedFilesResult_t ( RemoteStorageEnumerateUserPublishedFilesResult_t.Pack8 d ) => new RemoteStorageEnumerateUserPublishedFilesResult_t{ Result = d.Result,ResultsReturned = d.ResultsReturned,TotalResultCount = d.TotalResultCount,GPublishedFileId = d.GPublishedFileId, };
		}
		#endregion
	}
	
	public struct RemoteStorageSubscribePublishedFileResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 13;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageSubscribePublishedFileResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageSubscribePublishedFileResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoteStorageSubscribePublishedFileResult_t ( RemoteStorageSubscribePublishedFileResult_t.Pack4 d ) => new RemoteStorageSubscribePublishedFileResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoteStorageSubscribePublishedFileResult_t ( RemoteStorageSubscribePublishedFileResult_t.Pack8 d ) => new RemoteStorageSubscribePublishedFileResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId, };
		}
		#endregion
	}
	
	public struct RemoteStorageEnumerateUserSubscribedFilesResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
		internal uint[] GRTimeSubscribed; // m_rgRTimeSubscribed uint32 [50]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 14;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageEnumerateUserSubscribedFilesResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageEnumerateUserSubscribedFilesResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal int ResultsReturned; // m_nResultsReturned int32
			internal int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
			internal uint[] GRTimeSubscribed; // m_rgRTimeSubscribed uint32 [50]
			
			public static implicit operator RemoteStorageEnumerateUserSubscribedFilesResult_t ( RemoteStorageEnumerateUserSubscribedFilesResult_t.Pack4 d ) => new RemoteStorageEnumerateUserSubscribedFilesResult_t{ Result = d.Result,ResultsReturned = d.ResultsReturned,TotalResultCount = d.TotalResultCount,GPublishedFileId = d.GPublishedFileId,GRTimeSubscribed = d.GRTimeSubscribed, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal int ResultsReturned; // m_nResultsReturned int32
			internal int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
			internal uint[] GRTimeSubscribed; // m_rgRTimeSubscribed uint32 [50]
			
			public static implicit operator RemoteStorageEnumerateUserSubscribedFilesResult_t ( RemoteStorageEnumerateUserSubscribedFilesResult_t.Pack8 d ) => new RemoteStorageEnumerateUserSubscribedFilesResult_t{ Result = d.Result,ResultsReturned = d.ResultsReturned,TotalResultCount = d.TotalResultCount,GPublishedFileId = d.GPublishedFileId,GRTimeSubscribed = d.GRTimeSubscribed, };
		}
		#endregion
	}
	
	public struct RemoteStorageUnsubscribePublishedFileResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 15;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageUnsubscribePublishedFileResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageUnsubscribePublishedFileResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoteStorageUnsubscribePublishedFileResult_t ( RemoteStorageUnsubscribePublishedFileResult_t.Pack4 d ) => new RemoteStorageUnsubscribePublishedFileResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoteStorageUnsubscribePublishedFileResult_t ( RemoteStorageUnsubscribePublishedFileResult_t.Pack8 d ) => new RemoteStorageUnsubscribePublishedFileResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId, };
		}
		#endregion
	}
	
	public struct RemoteStorageUpdatePublishedFileResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 16;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageUpdatePublishedFileResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageUpdatePublishedFileResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			[MarshalAs(UnmanagedType.I1)]
			internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			
			public static implicit operator RemoteStorageUpdatePublishedFileResult_t ( RemoteStorageUpdatePublishedFileResult_t.Pack4 d ) => new RemoteStorageUpdatePublishedFileResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			[MarshalAs(UnmanagedType.I1)]
			internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			
			public static implicit operator RemoteStorageUpdatePublishedFileResult_t ( RemoteStorageUpdatePublishedFileResult_t.Pack8 d ) => new RemoteStorageUpdatePublishedFileResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement, };
		}
		#endregion
	}
	
	public struct RemoteStorageDownloadUGCResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong File; // m_hFile UGCHandle_t
		internal uint AppID; // m_nAppID AppId_t
		internal int SizeInBytes; // m_nSizeInBytes int32
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string PchFileName; // m_pchFileName char [260]
		internal ulong SteamIDOwner; // m_ulSteamIDOwner uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 17;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageDownloadUGCResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageDownloadUGCResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong File; // m_hFile UGCHandle_t
			internal uint AppID; // m_nAppID AppId_t
			internal int SizeInBytes; // m_nSizeInBytes int32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string PchFileName; // m_pchFileName char [260]
			internal ulong SteamIDOwner; // m_ulSteamIDOwner uint64
			
			public static implicit operator RemoteStorageDownloadUGCResult_t ( RemoteStorageDownloadUGCResult_t.Pack4 d ) => new RemoteStorageDownloadUGCResult_t{ Result = d.Result,File = d.File,AppID = d.AppID,SizeInBytes = d.SizeInBytes,PchFileName = d.PchFileName,SteamIDOwner = d.SteamIDOwner, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong File; // m_hFile UGCHandle_t
			internal uint AppID; // m_nAppID AppId_t
			internal int SizeInBytes; // m_nSizeInBytes int32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string PchFileName; // m_pchFileName char [260]
			internal ulong SteamIDOwner; // m_ulSteamIDOwner uint64
			
			public static implicit operator RemoteStorageDownloadUGCResult_t ( RemoteStorageDownloadUGCResult_t.Pack8 d ) => new RemoteStorageDownloadUGCResult_t{ Result = d.Result,File = d.File,AppID = d.AppID,SizeInBytes = d.SizeInBytes,PchFileName = d.PchFileName,SteamIDOwner = d.SteamIDOwner, };
		}
		#endregion
	}
	
	public struct RemoteStorageGetPublishedFileDetailsResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal uint CreatorAppID; // m_nCreatorAppID AppId_t
		internal uint ConsumerAppID; // m_nConsumerAppID AppId_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
		internal string Title; // m_rgchTitle char [129]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
		internal string Description; // m_rgchDescription char [8000]
		internal ulong File; // m_hFile UGCHandle_t
		internal ulong PreviewFile; // m_hPreviewFile UGCHandle_t
		internal ulong SteamIDOwner; // m_ulSteamIDOwner uint64
		internal uint TimeCreated; // m_rtimeCreated uint32
		internal uint TimeUpdated; // m_rtimeUpdated uint32
		internal RemoteStoragePublishedFileVisibility Visibility; // m_eVisibility enum ERemoteStoragePublishedFileVisibility
		[MarshalAs(UnmanagedType.I1)]
		internal bool Banned; // m_bBanned _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
		internal string Tags; // m_rgchTags char [1025]
		[MarshalAs(UnmanagedType.I1)]
		internal bool TagsTruncated; // m_bTagsTruncated _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string PchFileName; // m_pchFileName char [260]
		internal int FileSize; // m_nFileSize int32
		internal int PreviewFileSize; // m_nPreviewFileSize int32
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string URL; // m_rgchURL char [256]
		internal WorkshopFileType FileType; // m_eFileType enum EWorkshopFileType
		[MarshalAs(UnmanagedType.I1)]
		internal bool AcceptedForUse; // m_bAcceptedForUse _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 18;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageGetPublishedFileDetailsResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageGetPublishedFileDetailsResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal uint CreatorAppID; // m_nCreatorAppID AppId_t
			internal uint ConsumerAppID; // m_nConsumerAppID AppId_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
			internal string Title; // m_rgchTitle char [129]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
			internal string Description; // m_rgchDescription char [8000]
			internal ulong File; // m_hFile UGCHandle_t
			internal ulong PreviewFile; // m_hPreviewFile UGCHandle_t
			internal ulong SteamIDOwner; // m_ulSteamIDOwner uint64
			internal uint TimeCreated; // m_rtimeCreated uint32
			internal uint TimeUpdated; // m_rtimeUpdated uint32
			internal RemoteStoragePublishedFileVisibility Visibility; // m_eVisibility enum ERemoteStoragePublishedFileVisibility
			[MarshalAs(UnmanagedType.I1)]
			internal bool Banned; // m_bBanned _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
			internal string Tags; // m_rgchTags char [1025]
			[MarshalAs(UnmanagedType.I1)]
			internal bool TagsTruncated; // m_bTagsTruncated _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string PchFileName; // m_pchFileName char [260]
			internal int FileSize; // m_nFileSize int32
			internal int PreviewFileSize; // m_nPreviewFileSize int32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string URL; // m_rgchURL char [256]
			internal WorkshopFileType FileType; // m_eFileType enum EWorkshopFileType
			[MarshalAs(UnmanagedType.I1)]
			internal bool AcceptedForUse; // m_bAcceptedForUse _Bool
			
			public static implicit operator RemoteStorageGetPublishedFileDetailsResult_t ( RemoteStorageGetPublishedFileDetailsResult_t.Pack4 d ) => new RemoteStorageGetPublishedFileDetailsResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,CreatorAppID = d.CreatorAppID,ConsumerAppID = d.ConsumerAppID,Title = d.Title,Description = d.Description,File = d.File,PreviewFile = d.PreviewFile,SteamIDOwner = d.SteamIDOwner,TimeCreated = d.TimeCreated,TimeUpdated = d.TimeUpdated,Visibility = d.Visibility,Banned = d.Banned,Tags = d.Tags,TagsTruncated = d.TagsTruncated,PchFileName = d.PchFileName,FileSize = d.FileSize,PreviewFileSize = d.PreviewFileSize,URL = d.URL,FileType = d.FileType,AcceptedForUse = d.AcceptedForUse, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal uint CreatorAppID; // m_nCreatorAppID AppId_t
			internal uint ConsumerAppID; // m_nConsumerAppID AppId_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
			internal string Title; // m_rgchTitle char [129]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
			internal string Description; // m_rgchDescription char [8000]
			internal ulong File; // m_hFile UGCHandle_t
			internal ulong PreviewFile; // m_hPreviewFile UGCHandle_t
			internal ulong SteamIDOwner; // m_ulSteamIDOwner uint64
			internal uint TimeCreated; // m_rtimeCreated uint32
			internal uint TimeUpdated; // m_rtimeUpdated uint32
			internal RemoteStoragePublishedFileVisibility Visibility; // m_eVisibility enum ERemoteStoragePublishedFileVisibility
			[MarshalAs(UnmanagedType.I1)]
			internal bool Banned; // m_bBanned _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
			internal string Tags; // m_rgchTags char [1025]
			[MarshalAs(UnmanagedType.I1)]
			internal bool TagsTruncated; // m_bTagsTruncated _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string PchFileName; // m_pchFileName char [260]
			internal int FileSize; // m_nFileSize int32
			internal int PreviewFileSize; // m_nPreviewFileSize int32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string URL; // m_rgchURL char [256]
			internal WorkshopFileType FileType; // m_eFileType enum EWorkshopFileType
			[MarshalAs(UnmanagedType.I1)]
			internal bool AcceptedForUse; // m_bAcceptedForUse _Bool
			
			public static implicit operator RemoteStorageGetPublishedFileDetailsResult_t ( RemoteStorageGetPublishedFileDetailsResult_t.Pack8 d ) => new RemoteStorageGetPublishedFileDetailsResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,CreatorAppID = d.CreatorAppID,ConsumerAppID = d.ConsumerAppID,Title = d.Title,Description = d.Description,File = d.File,PreviewFile = d.PreviewFile,SteamIDOwner = d.SteamIDOwner,TimeCreated = d.TimeCreated,TimeUpdated = d.TimeUpdated,Visibility = d.Visibility,Banned = d.Banned,Tags = d.Tags,TagsTruncated = d.TagsTruncated,PchFileName = d.PchFileName,FileSize = d.FileSize,PreviewFileSize = d.PreviewFileSize,URL = d.URL,FileType = d.FileType,AcceptedForUse = d.AcceptedForUse, };
		}
		#endregion
	}
	
	public struct RemoteStorageEnumerateWorkshopFilesResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.R4)]
		internal float[] GScore; // m_rgScore float [50]
		internal uint AppId; // m_nAppId AppId_t
		internal uint StartIndex; // m_unStartIndex uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 19;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageEnumerateWorkshopFilesResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageEnumerateWorkshopFilesResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal int ResultsReturned; // m_nResultsReturned int32
			internal int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.R4)]
			internal float[] GScore; // m_rgScore float [50]
			internal uint AppId; // m_nAppId AppId_t
			internal uint StartIndex; // m_unStartIndex uint32
			
			public static implicit operator RemoteStorageEnumerateWorkshopFilesResult_t ( RemoteStorageEnumerateWorkshopFilesResult_t.Pack4 d ) => new RemoteStorageEnumerateWorkshopFilesResult_t{ Result = d.Result,ResultsReturned = d.ResultsReturned,TotalResultCount = d.TotalResultCount,GPublishedFileId = d.GPublishedFileId,GScore = d.GScore,AppId = d.AppId,StartIndex = d.StartIndex, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal int ResultsReturned; // m_nResultsReturned int32
			internal int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.R4)]
			internal float[] GScore; // m_rgScore float [50]
			internal uint AppId; // m_nAppId AppId_t
			internal uint StartIndex; // m_unStartIndex uint32
			
			public static implicit operator RemoteStorageEnumerateWorkshopFilesResult_t ( RemoteStorageEnumerateWorkshopFilesResult_t.Pack8 d ) => new RemoteStorageEnumerateWorkshopFilesResult_t{ Result = d.Result,ResultsReturned = d.ResultsReturned,TotalResultCount = d.TotalResultCount,GPublishedFileId = d.GPublishedFileId,GScore = d.GScore,AppId = d.AppId,StartIndex = d.StartIndex, };
		}
		#endregion
	}
	
	public struct RemoteStorageGetPublishedItemVoteDetailsResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong PublishedFileId; // m_unPublishedFileId PublishedFileId_t
		internal int VotesFor; // m_nVotesFor int32
		internal int VotesAgainst; // m_nVotesAgainst int32
		internal int Reports; // m_nReports int32
		internal float FScore; // m_fScore float
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 20;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageGetPublishedItemVoteDetailsResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageGetPublishedItemVoteDetailsResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_unPublishedFileId PublishedFileId_t
			internal int VotesFor; // m_nVotesFor int32
			internal int VotesAgainst; // m_nVotesAgainst int32
			internal int Reports; // m_nReports int32
			internal float FScore; // m_fScore float
			
			public static implicit operator RemoteStorageGetPublishedItemVoteDetailsResult_t ( RemoteStorageGetPublishedItemVoteDetailsResult_t.Pack4 d ) => new RemoteStorageGetPublishedItemVoteDetailsResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,VotesFor = d.VotesFor,VotesAgainst = d.VotesAgainst,Reports = d.Reports,FScore = d.FScore, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_unPublishedFileId PublishedFileId_t
			internal int VotesFor; // m_nVotesFor int32
			internal int VotesAgainst; // m_nVotesAgainst int32
			internal int Reports; // m_nReports int32
			internal float FScore; // m_fScore float
			
			public static implicit operator RemoteStorageGetPublishedItemVoteDetailsResult_t ( RemoteStorageGetPublishedItemVoteDetailsResult_t.Pack8 d ) => new RemoteStorageGetPublishedItemVoteDetailsResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,VotesFor = d.VotesFor,VotesAgainst = d.VotesAgainst,Reports = d.Reports,FScore = d.FScore, };
		}
		#endregion
	}
	
	public struct RemoteStoragePublishedFileSubscribed_t : Steamworks.ISteamCallback
	{
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal uint AppID; // m_nAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 21;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStoragePublishedFileSubscribed_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStoragePublishedFileSubscribed_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal uint AppID; // m_nAppID AppId_t
			
			public static implicit operator RemoteStoragePublishedFileSubscribed_t ( RemoteStoragePublishedFileSubscribed_t.Pack4 d ) => new RemoteStoragePublishedFileSubscribed_t{ PublishedFileId = d.PublishedFileId,AppID = d.AppID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal uint AppID; // m_nAppID AppId_t
			
			public static implicit operator RemoteStoragePublishedFileSubscribed_t ( RemoteStoragePublishedFileSubscribed_t.Pack8 d ) => new RemoteStoragePublishedFileSubscribed_t{ PublishedFileId = d.PublishedFileId,AppID = d.AppID, };
		}
		#endregion
	}
	
	public struct RemoteStoragePublishedFileUnsubscribed_t : Steamworks.ISteamCallback
	{
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal uint AppID; // m_nAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 22;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStoragePublishedFileUnsubscribed_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStoragePublishedFileUnsubscribed_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal uint AppID; // m_nAppID AppId_t
			
			public static implicit operator RemoteStoragePublishedFileUnsubscribed_t ( RemoteStoragePublishedFileUnsubscribed_t.Pack4 d ) => new RemoteStoragePublishedFileUnsubscribed_t{ PublishedFileId = d.PublishedFileId,AppID = d.AppID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal uint AppID; // m_nAppID AppId_t
			
			public static implicit operator RemoteStoragePublishedFileUnsubscribed_t ( RemoteStoragePublishedFileUnsubscribed_t.Pack8 d ) => new RemoteStoragePublishedFileUnsubscribed_t{ PublishedFileId = d.PublishedFileId,AppID = d.AppID, };
		}
		#endregion
	}
	
	public struct RemoteStoragePublishedFileDeleted_t : Steamworks.ISteamCallback
	{
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal uint AppID; // m_nAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 23;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStoragePublishedFileDeleted_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStoragePublishedFileDeleted_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal uint AppID; // m_nAppID AppId_t
			
			public static implicit operator RemoteStoragePublishedFileDeleted_t ( RemoteStoragePublishedFileDeleted_t.Pack4 d ) => new RemoteStoragePublishedFileDeleted_t{ PublishedFileId = d.PublishedFileId,AppID = d.AppID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal uint AppID; // m_nAppID AppId_t
			
			public static implicit operator RemoteStoragePublishedFileDeleted_t ( RemoteStoragePublishedFileDeleted_t.Pack8 d ) => new RemoteStoragePublishedFileDeleted_t{ PublishedFileId = d.PublishedFileId,AppID = d.AppID, };
		}
		#endregion
	}
	
	public struct RemoteStorageUpdateUserPublishedItemVoteResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 24;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageUpdateUserPublishedItemVoteResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageUpdateUserPublishedItemVoteResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoteStorageUpdateUserPublishedItemVoteResult_t ( RemoteStorageUpdateUserPublishedItemVoteResult_t.Pack4 d ) => new RemoteStorageUpdateUserPublishedItemVoteResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoteStorageUpdateUserPublishedItemVoteResult_t ( RemoteStorageUpdateUserPublishedItemVoteResult_t.Pack8 d ) => new RemoteStorageUpdateUserPublishedItemVoteResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId, };
		}
		#endregion
	}
	
	public struct RemoteStorageUserVoteDetails_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal WorkshopVote Vote; // m_eVote enum EWorkshopVote
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 25;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageUserVoteDetails_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageUserVoteDetails_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal WorkshopVote Vote; // m_eVote enum EWorkshopVote
			
			public static implicit operator RemoteStorageUserVoteDetails_t ( RemoteStorageUserVoteDetails_t.Pack4 d ) => new RemoteStorageUserVoteDetails_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,Vote = d.Vote, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal WorkshopVote Vote; // m_eVote enum EWorkshopVote
			
			public static implicit operator RemoteStorageUserVoteDetails_t ( RemoteStorageUserVoteDetails_t.Pack8 d ) => new RemoteStorageUserVoteDetails_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,Vote = d.Vote, };
		}
		#endregion
	}
	
	public struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 26;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageEnumerateUserSharedWorkshopFilesResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageEnumerateUserSharedWorkshopFilesResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal int ResultsReturned; // m_nResultsReturned int32
			internal int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			
			public static implicit operator RemoteStorageEnumerateUserSharedWorkshopFilesResult_t ( RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.Pack4 d ) => new RemoteStorageEnumerateUserSharedWorkshopFilesResult_t{ Result = d.Result,ResultsReturned = d.ResultsReturned,TotalResultCount = d.TotalResultCount,GPublishedFileId = d.GPublishedFileId, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal int ResultsReturned; // m_nResultsReturned int32
			internal int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			
			public static implicit operator RemoteStorageEnumerateUserSharedWorkshopFilesResult_t ( RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.Pack8 d ) => new RemoteStorageEnumerateUserSharedWorkshopFilesResult_t{ Result = d.Result,ResultsReturned = d.ResultsReturned,TotalResultCount = d.TotalResultCount,GPublishedFileId = d.GPublishedFileId, };
		}
		#endregion
	}
	
	public struct RemoteStorageSetUserPublishedFileActionResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 27;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageSetUserPublishedFileActionResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageSetUserPublishedFileActionResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
			
			public static implicit operator RemoteStorageSetUserPublishedFileActionResult_t ( RemoteStorageSetUserPublishedFileActionResult_t.Pack4 d ) => new RemoteStorageSetUserPublishedFileActionResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,Action = d.Action, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
			
			public static implicit operator RemoteStorageSetUserPublishedFileActionResult_t ( RemoteStorageSetUserPublishedFileActionResult_t.Pack8 d ) => new RemoteStorageSetUserPublishedFileActionResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,Action = d.Action, };
		}
		#endregion
	}
	
	public struct RemoteStorageEnumeratePublishedFilesByUserActionResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
		internal uint[] GRTimeUpdated; // m_rgRTimeUpdated uint32 [50]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 28;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageEnumeratePublishedFilesByUserActionResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageEnumeratePublishedFilesByUserActionResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
			internal int ResultsReturned; // m_nResultsReturned int32
			internal int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
			internal uint[] GRTimeUpdated; // m_rgRTimeUpdated uint32 [50]
			
			public static implicit operator RemoteStorageEnumeratePublishedFilesByUserActionResult_t ( RemoteStorageEnumeratePublishedFilesByUserActionResult_t.Pack4 d ) => new RemoteStorageEnumeratePublishedFilesByUserActionResult_t{ Result = d.Result,Action = d.Action,ResultsReturned = d.ResultsReturned,TotalResultCount = d.TotalResultCount,GPublishedFileId = d.GPublishedFileId,GRTimeUpdated = d.GRTimeUpdated, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
			internal int ResultsReturned; // m_nResultsReturned int32
			internal int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
			internal uint[] GRTimeUpdated; // m_rgRTimeUpdated uint32 [50]
			
			public static implicit operator RemoteStorageEnumeratePublishedFilesByUserActionResult_t ( RemoteStorageEnumeratePublishedFilesByUserActionResult_t.Pack8 d ) => new RemoteStorageEnumeratePublishedFilesByUserActionResult_t{ Result = d.Result,Action = d.Action,ResultsReturned = d.ResultsReturned,TotalResultCount = d.TotalResultCount,GPublishedFileId = d.GPublishedFileId,GRTimeUpdated = d.GRTimeUpdated, };
		}
		#endregion
	}
	
	public struct RemoteStoragePublishFileProgress_t : Steamworks.ISteamCallback
	{
		internal double DPercentFile; // m_dPercentFile double
		[MarshalAs(UnmanagedType.I1)]
		internal bool Preview; // m_bPreview _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 29;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStoragePublishFileProgress_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStoragePublishFileProgress_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal double DPercentFile; // m_dPercentFile double
			[MarshalAs(UnmanagedType.I1)]
			internal bool Preview; // m_bPreview _Bool
			
			public static implicit operator RemoteStoragePublishFileProgress_t ( RemoteStoragePublishFileProgress_t.Pack4 d ) => new RemoteStoragePublishFileProgress_t{ DPercentFile = d.DPercentFile,Preview = d.Preview, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal double DPercentFile; // m_dPercentFile double
			[MarshalAs(UnmanagedType.I1)]
			internal bool Preview; // m_bPreview _Bool
			
			public static implicit operator RemoteStoragePublishFileProgress_t ( RemoteStoragePublishFileProgress_t.Pack8 d ) => new RemoteStoragePublishFileProgress_t{ DPercentFile = d.DPercentFile,Preview = d.Preview, };
		}
		#endregion
	}
	
	public struct RemoteStoragePublishedFileUpdated_t : Steamworks.ISteamCallback
	{
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal uint AppID; // m_nAppID AppId_t
		internal ulong Unused; // m_ulUnused uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 30;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStoragePublishedFileUpdated_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStoragePublishedFileUpdated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal uint AppID; // m_nAppID AppId_t
			internal ulong Unused; // m_ulUnused uint64
			
			public static implicit operator RemoteStoragePublishedFileUpdated_t ( RemoteStoragePublishedFileUpdated_t.Pack4 d ) => new RemoteStoragePublishedFileUpdated_t{ PublishedFileId = d.PublishedFileId,AppID = d.AppID,Unused = d.Unused, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal uint AppID; // m_nAppID AppId_t
			internal ulong Unused; // m_ulUnused uint64
			
			public static implicit operator RemoteStoragePublishedFileUpdated_t ( RemoteStoragePublishedFileUpdated_t.Pack8 d ) => new RemoteStoragePublishedFileUpdated_t{ PublishedFileId = d.PublishedFileId,AppID = d.AppID,Unused = d.Unused, };
		}
		#endregion
	}
	
	public struct RemoteStorageFileWriteAsyncComplete_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 31;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageFileWriteAsyncComplete_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageFileWriteAsyncComplete_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator RemoteStorageFileWriteAsyncComplete_t ( RemoteStorageFileWriteAsyncComplete_t.Pack4 d ) => new RemoteStorageFileWriteAsyncComplete_t{ Result = d.Result, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator RemoteStorageFileWriteAsyncComplete_t ( RemoteStorageFileWriteAsyncComplete_t.Pack8 d ) => new RemoteStorageFileWriteAsyncComplete_t{ Result = d.Result, };
		}
		#endregion
	}
	
	public struct RemoteStorageFileReadAsyncComplete_t : Steamworks.ISteamCallback
	{
		internal ulong FileReadAsync; // m_hFileReadAsync SteamAPICall_t
		internal Result Result; // m_eResult enum EResult
		internal uint Offset; // m_nOffset uint32
		internal uint Read; // m_cubRead uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 32;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoteStorageFileReadAsyncComplete_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoteStorageFileReadAsyncComplete_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong FileReadAsync; // m_hFileReadAsync SteamAPICall_t
			internal Result Result; // m_eResult enum EResult
			internal uint Offset; // m_nOffset uint32
			internal uint Read; // m_cubRead uint32
			
			public static implicit operator RemoteStorageFileReadAsyncComplete_t ( RemoteStorageFileReadAsyncComplete_t.Pack4 d ) => new RemoteStorageFileReadAsyncComplete_t{ FileReadAsync = d.FileReadAsync,Result = d.Result,Offset = d.Offset,Read = d.Read, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong FileReadAsync; // m_hFileReadAsync SteamAPICall_t
			internal Result Result; // m_eResult enum EResult
			internal uint Offset; // m_nOffset uint32
			internal uint Read; // m_cubRead uint32
			
			public static implicit operator RemoteStorageFileReadAsyncComplete_t ( RemoteStorageFileReadAsyncComplete_t.Pack8 d ) => new RemoteStorageFileReadAsyncComplete_t{ FileReadAsync = d.FileReadAsync,Result = d.Result,Offset = d.Offset,Read = d.Read, };
		}
		#endregion
	}
	
	public struct LeaderboardEntry_t
	{
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		internal int GlobalRank; // m_nGlobalRank int32
		internal int Score; // m_nScore int32
		internal int CDetails; // m_cDetails int32
		internal ulong UGC; // m_hUGC UGCHandle_t
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public LeaderboardEntry_t Fill( IntPtr p ) => Platform.PackSmall ? ((LeaderboardEntry_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((LeaderboardEntry_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			internal int GlobalRank; // m_nGlobalRank int32
			internal int Score; // m_nScore int32
			internal int CDetails; // m_cDetails int32
			internal ulong UGC; // m_hUGC UGCHandle_t
			
			public static implicit operator LeaderboardEntry_t ( LeaderboardEntry_t.Pack4 d ) => new LeaderboardEntry_t{ SteamIDUser = d.SteamIDUser,GlobalRank = d.GlobalRank,Score = d.Score,CDetails = d.CDetails,UGC = d.UGC, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			internal int GlobalRank; // m_nGlobalRank int32
			internal int Score; // m_nScore int32
			internal int CDetails; // m_cDetails int32
			internal ulong UGC; // m_hUGC UGCHandle_t
			
			public static implicit operator LeaderboardEntry_t ( LeaderboardEntry_t.Pack8 d ) => new LeaderboardEntry_t{ SteamIDUser = d.SteamIDUser,GlobalRank = d.GlobalRank,Score = d.Score,CDetails = d.CDetails,UGC = d.UGC, };
		}
		#endregion
	}
	
	public struct UserStatsReceived_t : Steamworks.ISteamCallback
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((UserStatsReceived_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((UserStatsReceived_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong GameID; // m_nGameID uint64
			internal Result Result; // m_eResult enum EResult
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator UserStatsReceived_t ( UserStatsReceived_t.Pack4 d ) => new UserStatsReceived_t{ GameID = d.GameID,Result = d.Result,SteamIDUser = d.SteamIDUser, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong GameID; // m_nGameID uint64
			internal Result Result; // m_eResult enum EResult
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator UserStatsReceived_t ( UserStatsReceived_t.Pack8 d ) => new UserStatsReceived_t{ GameID = d.GameID,Result = d.Result,SteamIDUser = d.SteamIDUser, };
		}
		#endregion
	}
	
	public struct UserStatsStored_t : Steamworks.ISteamCallback
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((UserStatsStored_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((UserStatsStored_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong GameID; // m_nGameID uint64
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator UserStatsStored_t ( UserStatsStored_t.Pack4 d ) => new UserStatsStored_t{ GameID = d.GameID,Result = d.Result, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong GameID; // m_nGameID uint64
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator UserStatsStored_t ( UserStatsStored_t.Pack8 d ) => new UserStatsStored_t{ GameID = d.GameID,Result = d.Result, };
		}
		#endregion
	}
	
	public struct UserAchievementStored_t : Steamworks.ISteamCallback
	{
		internal ulong GameID; // m_nGameID uint64
		[MarshalAs(UnmanagedType.I1)]
		internal bool GroupAchievement; // m_bGroupAchievement _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string AchievementName; // m_rgchAchievementName char [128]
		internal uint CurProgress; // m_nCurProgress uint32
		internal uint MaxProgress; // m_nMaxProgress uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((UserAchievementStored_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((UserAchievementStored_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong GameID; // m_nGameID uint64
			[MarshalAs(UnmanagedType.I1)]
			internal bool GroupAchievement; // m_bGroupAchievement _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string AchievementName; // m_rgchAchievementName char [128]
			internal uint CurProgress; // m_nCurProgress uint32
			internal uint MaxProgress; // m_nMaxProgress uint32
			
			public static implicit operator UserAchievementStored_t ( UserAchievementStored_t.Pack4 d ) => new UserAchievementStored_t{ GameID = d.GameID,GroupAchievement = d.GroupAchievement,AchievementName = d.AchievementName,CurProgress = d.CurProgress,MaxProgress = d.MaxProgress, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong GameID; // m_nGameID uint64
			[MarshalAs(UnmanagedType.I1)]
			internal bool GroupAchievement; // m_bGroupAchievement _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string AchievementName; // m_rgchAchievementName char [128]
			internal uint CurProgress; // m_nCurProgress uint32
			internal uint MaxProgress; // m_nMaxProgress uint32
			
			public static implicit operator UserAchievementStored_t ( UserAchievementStored_t.Pack8 d ) => new UserAchievementStored_t{ GameID = d.GameID,GroupAchievement = d.GroupAchievement,AchievementName = d.AchievementName,CurProgress = d.CurProgress,MaxProgress = d.MaxProgress, };
		}
		#endregion
	}
	
	public struct LeaderboardFindResult_t : Steamworks.ISteamCallback
	{
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		internal byte LeaderboardFound; // m_bLeaderboardFound uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 4;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((LeaderboardFindResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((LeaderboardFindResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
			internal byte LeaderboardFound; // m_bLeaderboardFound uint8
			
			public static implicit operator LeaderboardFindResult_t ( LeaderboardFindResult_t.Pack4 d ) => new LeaderboardFindResult_t{ SteamLeaderboard = d.SteamLeaderboard,LeaderboardFound = d.LeaderboardFound, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
			internal byte LeaderboardFound; // m_bLeaderboardFound uint8
			
			public static implicit operator LeaderboardFindResult_t ( LeaderboardFindResult_t.Pack8 d ) => new LeaderboardFindResult_t{ SteamLeaderboard = d.SteamLeaderboard,LeaderboardFound = d.LeaderboardFound, };
		}
		#endregion
	}
	
	public struct LeaderboardScoresDownloaded_t : Steamworks.ISteamCallback
	{
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		internal ulong SteamLeaderboardEntries; // m_hSteamLeaderboardEntries SteamLeaderboardEntries_t
		internal int CEntryCount; // m_cEntryCount int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 5;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((LeaderboardScoresDownloaded_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((LeaderboardScoresDownloaded_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
			internal ulong SteamLeaderboardEntries; // m_hSteamLeaderboardEntries SteamLeaderboardEntries_t
			internal int CEntryCount; // m_cEntryCount int
			
			public static implicit operator LeaderboardScoresDownloaded_t ( LeaderboardScoresDownloaded_t.Pack4 d ) => new LeaderboardScoresDownloaded_t{ SteamLeaderboard = d.SteamLeaderboard,SteamLeaderboardEntries = d.SteamLeaderboardEntries,CEntryCount = d.CEntryCount, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
			internal ulong SteamLeaderboardEntries; // m_hSteamLeaderboardEntries SteamLeaderboardEntries_t
			internal int CEntryCount; // m_cEntryCount int
			
			public static implicit operator LeaderboardScoresDownloaded_t ( LeaderboardScoresDownloaded_t.Pack8 d ) => new LeaderboardScoresDownloaded_t{ SteamLeaderboard = d.SteamLeaderboard,SteamLeaderboardEntries = d.SteamLeaderboardEntries,CEntryCount = d.CEntryCount, };
		}
		#endregion
	}
	
	public struct LeaderboardScoreUploaded_t : Steamworks.ISteamCallback
	{
		internal byte Success; // m_bSuccess uint8
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		internal int Score; // m_nScore int32
		internal byte ScoreChanged; // m_bScoreChanged uint8
		internal int GlobalRankNew; // m_nGlobalRankNew int
		internal int GlobalRankPrevious; // m_nGlobalRankPrevious int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 6;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((LeaderboardScoreUploaded_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((LeaderboardScoreUploaded_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal byte Success; // m_bSuccess uint8
			internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
			internal int Score; // m_nScore int32
			internal byte ScoreChanged; // m_bScoreChanged uint8
			internal int GlobalRankNew; // m_nGlobalRankNew int
			internal int GlobalRankPrevious; // m_nGlobalRankPrevious int
			
			public static implicit operator LeaderboardScoreUploaded_t ( LeaderboardScoreUploaded_t.Pack4 d ) => new LeaderboardScoreUploaded_t{ Success = d.Success,SteamLeaderboard = d.SteamLeaderboard,Score = d.Score,ScoreChanged = d.ScoreChanged,GlobalRankNew = d.GlobalRankNew,GlobalRankPrevious = d.GlobalRankPrevious, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal byte Success; // m_bSuccess uint8
			internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
			internal int Score; // m_nScore int32
			internal byte ScoreChanged; // m_bScoreChanged uint8
			internal int GlobalRankNew; // m_nGlobalRankNew int
			internal int GlobalRankPrevious; // m_nGlobalRankPrevious int
			
			public static implicit operator LeaderboardScoreUploaded_t ( LeaderboardScoreUploaded_t.Pack8 d ) => new LeaderboardScoreUploaded_t{ Success = d.Success,SteamLeaderboard = d.SteamLeaderboard,Score = d.Score,ScoreChanged = d.ScoreChanged,GlobalRankNew = d.GlobalRankNew,GlobalRankPrevious = d.GlobalRankPrevious, };
		}
		#endregion
	}
	
	public struct NumberOfCurrentPlayers_t : Steamworks.ISteamCallback
	{
		internal byte Success; // m_bSuccess uint8
		internal int CPlayers; // m_cPlayers int32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 7;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((NumberOfCurrentPlayers_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((NumberOfCurrentPlayers_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal byte Success; // m_bSuccess uint8
			internal int CPlayers; // m_cPlayers int32
			
			public static implicit operator NumberOfCurrentPlayers_t ( NumberOfCurrentPlayers_t.Pack4 d ) => new NumberOfCurrentPlayers_t{ Success = d.Success,CPlayers = d.CPlayers, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal byte Success; // m_bSuccess uint8
			internal int CPlayers; // m_cPlayers int32
			
			public static implicit operator NumberOfCurrentPlayers_t ( NumberOfCurrentPlayers_t.Pack8 d ) => new NumberOfCurrentPlayers_t{ Success = d.Success,CPlayers = d.CPlayers, };
		}
		#endregion
	}
	
	public struct UserStatsUnloaded_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 8;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((UserStatsUnloaded_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((UserStatsUnloaded_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator UserStatsUnloaded_t ( UserStatsUnloaded_t.Pack4 d ) => new UserStatsUnloaded_t{ SteamIDUser = d.SteamIDUser, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator UserStatsUnloaded_t ( UserStatsUnloaded_t.Pack8 d ) => new UserStatsUnloaded_t{ SteamIDUser = d.SteamIDUser, };
		}
		#endregion
	}
	
	public struct UserAchievementIconFetched_t : Steamworks.ISteamCallback
	{
		internal ulong GameID; // m_nGameID class CGameID
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string AchievementName; // m_rgchAchievementName char [128]
		[MarshalAs(UnmanagedType.I1)]
		internal bool Achieved; // m_bAchieved _Bool
		internal int IconHandle; // m_nIconHandle int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 9;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((UserAchievementIconFetched_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((UserAchievementIconFetched_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong GameID; // m_nGameID class CGameID
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string AchievementName; // m_rgchAchievementName char [128]
			[MarshalAs(UnmanagedType.I1)]
			internal bool Achieved; // m_bAchieved _Bool
			internal int IconHandle; // m_nIconHandle int
			
			public static implicit operator UserAchievementIconFetched_t ( UserAchievementIconFetched_t.Pack4 d ) => new UserAchievementIconFetched_t{ GameID = d.GameID,AchievementName = d.AchievementName,Achieved = d.Achieved,IconHandle = d.IconHandle, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong GameID; // m_nGameID class CGameID
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string AchievementName; // m_rgchAchievementName char [128]
			[MarshalAs(UnmanagedType.I1)]
			internal bool Achieved; // m_bAchieved _Bool
			internal int IconHandle; // m_nIconHandle int
			
			public static implicit operator UserAchievementIconFetched_t ( UserAchievementIconFetched_t.Pack8 d ) => new UserAchievementIconFetched_t{ GameID = d.GameID,AchievementName = d.AchievementName,Achieved = d.Achieved,IconHandle = d.IconHandle, };
		}
		#endregion
	}
	
	public struct GlobalAchievementPercentagesReady_t : Steamworks.ISteamCallback
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 10;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GlobalAchievementPercentagesReady_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GlobalAchievementPercentagesReady_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong GameID; // m_nGameID uint64
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator GlobalAchievementPercentagesReady_t ( GlobalAchievementPercentagesReady_t.Pack4 d ) => new GlobalAchievementPercentagesReady_t{ GameID = d.GameID,Result = d.Result, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong GameID; // m_nGameID uint64
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator GlobalAchievementPercentagesReady_t ( GlobalAchievementPercentagesReady_t.Pack8 d ) => new GlobalAchievementPercentagesReady_t{ GameID = d.GameID,Result = d.Result, };
		}
		#endregion
	}
	
	public struct LeaderboardUGCSet_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 11;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((LeaderboardUGCSet_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((LeaderboardUGCSet_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
			
			public static implicit operator LeaderboardUGCSet_t ( LeaderboardUGCSet_t.Pack4 d ) => new LeaderboardUGCSet_t{ Result = d.Result,SteamLeaderboard = d.SteamLeaderboard, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
			
			public static implicit operator LeaderboardUGCSet_t ( LeaderboardUGCSet_t.Pack8 d ) => new LeaderboardUGCSet_t{ Result = d.Result,SteamLeaderboard = d.SteamLeaderboard, };
		}
		#endregion
	}
	
	public struct PS3TrophiesInstalled_t : Steamworks.ISteamCallback
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		internal ulong RequiredDiskSpace; // m_ulRequiredDiskSpace uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 12;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((PS3TrophiesInstalled_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((PS3TrophiesInstalled_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong GameID; // m_nGameID uint64
			internal Result Result; // m_eResult enum EResult
			internal ulong RequiredDiskSpace; // m_ulRequiredDiskSpace uint64
			
			public static implicit operator PS3TrophiesInstalled_t ( PS3TrophiesInstalled_t.Pack4 d ) => new PS3TrophiesInstalled_t{ GameID = d.GameID,Result = d.Result,RequiredDiskSpace = d.RequiredDiskSpace, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong GameID; // m_nGameID uint64
			internal Result Result; // m_eResult enum EResult
			internal ulong RequiredDiskSpace; // m_ulRequiredDiskSpace uint64
			
			public static implicit operator PS3TrophiesInstalled_t ( PS3TrophiesInstalled_t.Pack8 d ) => new PS3TrophiesInstalled_t{ GameID = d.GameID,Result = d.Result,RequiredDiskSpace = d.RequiredDiskSpace, };
		}
		#endregion
	}
	
	public struct GlobalStatsReceived_t : Steamworks.ISteamCallback
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 12;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GlobalStatsReceived_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GlobalStatsReceived_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong GameID; // m_nGameID uint64
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator GlobalStatsReceived_t ( GlobalStatsReceived_t.Pack4 d ) => new GlobalStatsReceived_t{ GameID = d.GameID,Result = d.Result, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong GameID; // m_nGameID uint64
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator GlobalStatsReceived_t ( GlobalStatsReceived_t.Pack8 d ) => new GlobalStatsReceived_t{ GameID = d.GameID,Result = d.Result, };
		}
		#endregion
	}
	
	public struct DlcInstalled_t : Steamworks.ISteamCallback
	{
		internal uint AppID; // m_nAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamApps + 5;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((DlcInstalled_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((DlcInstalled_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint AppID; // m_nAppID AppId_t
			
			public static implicit operator DlcInstalled_t ( DlcInstalled_t.Pack4 d ) => new DlcInstalled_t{ AppID = d.AppID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint AppID; // m_nAppID AppId_t
			
			public static implicit operator DlcInstalled_t ( DlcInstalled_t.Pack8 d ) => new DlcInstalled_t{ AppID = d.AppID, };
		}
		#endregion
	}
	
	public struct RegisterActivationCodeResponse_t : Steamworks.ISteamCallback
	{
		internal RegisterActivationCodeResult Result; // m_eResult enum ERegisterActivationCodeResult
		internal uint PackageRegistered; // m_unPackageRegistered uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamApps + 8;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RegisterActivationCodeResponse_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RegisterActivationCodeResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal RegisterActivationCodeResult Result; // m_eResult enum ERegisterActivationCodeResult
			internal uint PackageRegistered; // m_unPackageRegistered uint32
			
			public static implicit operator RegisterActivationCodeResponse_t ( RegisterActivationCodeResponse_t.Pack4 d ) => new RegisterActivationCodeResponse_t{ Result = d.Result,PackageRegistered = d.PackageRegistered, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal RegisterActivationCodeResult Result; // m_eResult enum ERegisterActivationCodeResult
			internal uint PackageRegistered; // m_unPackageRegistered uint32
			
			public static implicit operator RegisterActivationCodeResponse_t ( RegisterActivationCodeResponse_t.Pack8 d ) => new RegisterActivationCodeResponse_t{ Result = d.Result,PackageRegistered = d.PackageRegistered, };
		}
		#endregion
	}
	
	public struct AppProofOfPurchaseKeyResponse_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal uint AppID; // m_nAppID uint32
		internal uint CchKeyLength; // m_cchKeyLength uint32
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 240)]
		internal string Key; // m_rgchKey char [240]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamApps + 21;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((AppProofOfPurchaseKeyResponse_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((AppProofOfPurchaseKeyResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal uint AppID; // m_nAppID uint32
			internal uint CchKeyLength; // m_cchKeyLength uint32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 240)]
			internal string Key; // m_rgchKey char [240]
			
			public static implicit operator AppProofOfPurchaseKeyResponse_t ( AppProofOfPurchaseKeyResponse_t.Pack4 d ) => new AppProofOfPurchaseKeyResponse_t{ Result = d.Result,AppID = d.AppID,CchKeyLength = d.CchKeyLength,Key = d.Key, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal uint AppID; // m_nAppID uint32
			internal uint CchKeyLength; // m_cchKeyLength uint32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 240)]
			internal string Key; // m_rgchKey char [240]
			
			public static implicit operator AppProofOfPurchaseKeyResponse_t ( AppProofOfPurchaseKeyResponse_t.Pack8 d ) => new AppProofOfPurchaseKeyResponse_t{ Result = d.Result,AppID = d.AppID,CchKeyLength = d.CchKeyLength,Key = d.Key, };
		}
		#endregion
	}
	
	public struct FileDetailsResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong FileSize; // m_ulFileSize uint64
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] //  m_FileSHA
		internal byte[] FileSHA; // m_FileSHA uint8 [20]
		internal uint Flags; // m_unFlags uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamApps + 23;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((FileDetailsResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((FileDetailsResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong FileSize; // m_ulFileSize uint64
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] //  m_FileSHA
			internal byte[] FileSHA; // m_FileSHA uint8 [20]
			internal uint Flags; // m_unFlags uint32
			
			public static implicit operator FileDetailsResult_t ( FileDetailsResult_t.Pack4 d ) => new FileDetailsResult_t{ Result = d.Result,FileSize = d.FileSize,FileSHA = d.FileSHA,Flags = d.Flags, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong FileSize; // m_ulFileSize uint64
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] //  m_FileSHA
			internal byte[] FileSHA; // m_FileSHA uint8 [20]
			internal uint Flags; // m_unFlags uint32
			
			public static implicit operator FileDetailsResult_t ( FileDetailsResult_t.Pack8 d ) => new FileDetailsResult_t{ Result = d.Result,FileSize = d.FileSize,FileSHA = d.FileSHA,Flags = d.Flags, };
		}
		#endregion
	}
	
	public struct P2PSessionState_t
	{
		internal byte ConnectionActive; // m_bConnectionActive uint8
		internal byte Connecting; // m_bConnecting uint8
		internal byte P2PSessionError; // m_eP2PSessionError uint8
		internal byte UsingRelay; // m_bUsingRelay uint8
		internal int BytesQueuedForSend; // m_nBytesQueuedForSend int32
		internal int PacketsQueuedForSend; // m_nPacketsQueuedForSend int32
		internal uint RemoteIP; // m_nRemoteIP uint32
		internal ushort RemotePort; // m_nRemotePort uint16
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public P2PSessionState_t Fill( IntPtr p ) => Platform.PackSmall ? ((P2PSessionState_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((P2PSessionState_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal byte ConnectionActive; // m_bConnectionActive uint8
			internal byte Connecting; // m_bConnecting uint8
			internal byte P2PSessionError; // m_eP2PSessionError uint8
			internal byte UsingRelay; // m_bUsingRelay uint8
			internal int BytesQueuedForSend; // m_nBytesQueuedForSend int32
			internal int PacketsQueuedForSend; // m_nPacketsQueuedForSend int32
			internal uint RemoteIP; // m_nRemoteIP uint32
			internal ushort RemotePort; // m_nRemotePort uint16
			
			public static implicit operator P2PSessionState_t ( P2PSessionState_t.Pack4 d ) => new P2PSessionState_t{ ConnectionActive = d.ConnectionActive,Connecting = d.Connecting,P2PSessionError = d.P2PSessionError,UsingRelay = d.UsingRelay,BytesQueuedForSend = d.BytesQueuedForSend,PacketsQueuedForSend = d.PacketsQueuedForSend,RemoteIP = d.RemoteIP,RemotePort = d.RemotePort, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal byte ConnectionActive; // m_bConnectionActive uint8
			internal byte Connecting; // m_bConnecting uint8
			internal byte P2PSessionError; // m_eP2PSessionError uint8
			internal byte UsingRelay; // m_bUsingRelay uint8
			internal int BytesQueuedForSend; // m_nBytesQueuedForSend int32
			internal int PacketsQueuedForSend; // m_nPacketsQueuedForSend int32
			internal uint RemoteIP; // m_nRemoteIP uint32
			internal ushort RemotePort; // m_nRemotePort uint16
			
			public static implicit operator P2PSessionState_t ( P2PSessionState_t.Pack8 d ) => new P2PSessionState_t{ ConnectionActive = d.ConnectionActive,Connecting = d.Connecting,P2PSessionError = d.P2PSessionError,UsingRelay = d.UsingRelay,BytesQueuedForSend = d.BytesQueuedForSend,PacketsQueuedForSend = d.PacketsQueuedForSend,RemoteIP = d.RemoteIP,RemotePort = d.RemotePort, };
		}
		#endregion
	}
	
	public struct P2PSessionRequest_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamNetworking + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((P2PSessionRequest_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((P2PSessionRequest_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
			
			public static implicit operator P2PSessionRequest_t ( P2PSessionRequest_t.Pack4 d ) => new P2PSessionRequest_t{ SteamIDRemote = d.SteamIDRemote, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
			
			public static implicit operator P2PSessionRequest_t ( P2PSessionRequest_t.Pack8 d ) => new P2PSessionRequest_t{ SteamIDRemote = d.SteamIDRemote, };
		}
		#endregion
	}
	
	public struct P2PSessionConnectFail_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
		internal byte P2PSessionError; // m_eP2PSessionError uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamNetworking + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((P2PSessionConnectFail_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((P2PSessionConnectFail_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
			internal byte P2PSessionError; // m_eP2PSessionError uint8
			
			public static implicit operator P2PSessionConnectFail_t ( P2PSessionConnectFail_t.Pack4 d ) => new P2PSessionConnectFail_t{ SteamIDRemote = d.SteamIDRemote,P2PSessionError = d.P2PSessionError, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
			internal byte P2PSessionError; // m_eP2PSessionError uint8
			
			public static implicit operator P2PSessionConnectFail_t ( P2PSessionConnectFail_t.Pack8 d ) => new P2PSessionConnectFail_t{ SteamIDRemote = d.SteamIDRemote,P2PSessionError = d.P2PSessionError, };
		}
		#endregion
	}
	
	public struct SocketStatusCallback_t : Steamworks.ISteamCallback
	{
		internal uint Socket; // m_hSocket SNetSocket_t
		internal uint ListenSocket; // m_hListenSocket SNetListenSocket_t
		internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
		internal int SNetSocketState; // m_eSNetSocketState int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamNetworking + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SocketStatusCallback_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SocketStatusCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint Socket; // m_hSocket SNetSocket_t
			internal uint ListenSocket; // m_hListenSocket SNetListenSocket_t
			internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
			internal int SNetSocketState; // m_eSNetSocketState int
			
			public static implicit operator SocketStatusCallback_t ( SocketStatusCallback_t.Pack4 d ) => new SocketStatusCallback_t{ Socket = d.Socket,ListenSocket = d.ListenSocket,SteamIDRemote = d.SteamIDRemote,SNetSocketState = d.SNetSocketState, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal uint Socket; // m_hSocket SNetSocket_t
			internal uint ListenSocket; // m_hListenSocket SNetListenSocket_t
			internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
			internal int SNetSocketState; // m_eSNetSocketState int
			
			public static implicit operator SocketStatusCallback_t ( SocketStatusCallback_t.Pack8 d ) => new SocketStatusCallback_t{ Socket = d.Socket,ListenSocket = d.ListenSocket,SteamIDRemote = d.SteamIDRemote,SNetSocketState = d.SNetSocketState, };
		}
		#endregion
	}
	
	public struct ScreenshotReady_t : Steamworks.ISteamCallback
	{
		internal uint Local; // m_hLocal ScreenshotHandle
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamScreenshots + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((ScreenshotReady_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((ScreenshotReady_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint Local; // m_hLocal ScreenshotHandle
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator ScreenshotReady_t ( ScreenshotReady_t.Pack4 d ) => new ScreenshotReady_t{ Local = d.Local,Result = d.Result, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint Local; // m_hLocal ScreenshotHandle
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator ScreenshotReady_t ( ScreenshotReady_t.Pack8 d ) => new ScreenshotReady_t{ Local = d.Local,Result = d.Result, };
		}
		#endregion
	}
	
	public struct VolumeHasChanged_t : Steamworks.ISteamCallback
	{
		internal float NewVolume; // m_flNewVolume float
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMusic + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((VolumeHasChanged_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((VolumeHasChanged_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal float NewVolume; // m_flNewVolume float
			
			public static implicit operator VolumeHasChanged_t ( VolumeHasChanged_t.Pack4 d ) => new VolumeHasChanged_t{ NewVolume = d.NewVolume, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal float NewVolume; // m_flNewVolume float
			
			public static implicit operator VolumeHasChanged_t ( VolumeHasChanged_t.Pack8 d ) => new VolumeHasChanged_t{ NewVolume = d.NewVolume, };
		}
		#endregion
	}
	
	public struct MusicPlayerWantsShuffled_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Shuffled; // m_bShuffled _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMusicRemote + 9;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((MusicPlayerWantsShuffled_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((MusicPlayerWantsShuffled_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool Shuffled; // m_bShuffled _Bool
			
			public static implicit operator MusicPlayerWantsShuffled_t ( MusicPlayerWantsShuffled_t.Pack4 d ) => new MusicPlayerWantsShuffled_t{ Shuffled = d.Shuffled, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool Shuffled; // m_bShuffled _Bool
			
			public static implicit operator MusicPlayerWantsShuffled_t ( MusicPlayerWantsShuffled_t.Pack8 d ) => new MusicPlayerWantsShuffled_t{ Shuffled = d.Shuffled, };
		}
		#endregion
	}
	
	public struct MusicPlayerWantsLooped_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Looped; // m_bLooped _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMusicRemote + 10;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((MusicPlayerWantsLooped_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((MusicPlayerWantsLooped_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool Looped; // m_bLooped _Bool
			
			public static implicit operator MusicPlayerWantsLooped_t ( MusicPlayerWantsLooped_t.Pack4 d ) => new MusicPlayerWantsLooped_t{ Looped = d.Looped, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool Looped; // m_bLooped _Bool
			
			public static implicit operator MusicPlayerWantsLooped_t ( MusicPlayerWantsLooped_t.Pack8 d ) => new MusicPlayerWantsLooped_t{ Looped = d.Looped, };
		}
		#endregion
	}
	
	public struct MusicPlayerWantsVolume_t : Steamworks.ISteamCallback
	{
		internal float NewVolume; // m_flNewVolume float
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMusic + 11;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((MusicPlayerWantsVolume_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((MusicPlayerWantsVolume_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal float NewVolume; // m_flNewVolume float
			
			public static implicit operator MusicPlayerWantsVolume_t ( MusicPlayerWantsVolume_t.Pack4 d ) => new MusicPlayerWantsVolume_t{ NewVolume = d.NewVolume, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal float NewVolume; // m_flNewVolume float
			
			public static implicit operator MusicPlayerWantsVolume_t ( MusicPlayerWantsVolume_t.Pack8 d ) => new MusicPlayerWantsVolume_t{ NewVolume = d.NewVolume, };
		}
		#endregion
	}
	
	public struct MusicPlayerSelectsQueueEntry_t : Steamworks.ISteamCallback
	{
		internal int NID; // nID int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMusic + 12;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((MusicPlayerSelectsQueueEntry_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((MusicPlayerSelectsQueueEntry_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal int NID; // nID int
			
			public static implicit operator MusicPlayerSelectsQueueEntry_t ( MusicPlayerSelectsQueueEntry_t.Pack4 d ) => new MusicPlayerSelectsQueueEntry_t{ NID = d.NID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal int NID; // nID int
			
			public static implicit operator MusicPlayerSelectsQueueEntry_t ( MusicPlayerSelectsQueueEntry_t.Pack8 d ) => new MusicPlayerSelectsQueueEntry_t{ NID = d.NID, };
		}
		#endregion
	}
	
	public struct MusicPlayerSelectsPlaylistEntry_t : Steamworks.ISteamCallback
	{
		internal int NID; // nID int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMusic + 13;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((MusicPlayerSelectsPlaylistEntry_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((MusicPlayerSelectsPlaylistEntry_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal int NID; // nID int
			
			public static implicit operator MusicPlayerSelectsPlaylistEntry_t ( MusicPlayerSelectsPlaylistEntry_t.Pack4 d ) => new MusicPlayerSelectsPlaylistEntry_t{ NID = d.NID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal int NID; // nID int
			
			public static implicit operator MusicPlayerSelectsPlaylistEntry_t ( MusicPlayerSelectsPlaylistEntry_t.Pack8 d ) => new MusicPlayerSelectsPlaylistEntry_t{ NID = d.NID, };
		}
		#endregion
	}
	
	public struct MusicPlayerWantsPlayingRepeatStatus_t : Steamworks.ISteamCallback
	{
		internal int PlayingRepeatStatus; // m_nPlayingRepeatStatus int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMusicRemote + 14;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((MusicPlayerWantsPlayingRepeatStatus_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((MusicPlayerWantsPlayingRepeatStatus_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal int PlayingRepeatStatus; // m_nPlayingRepeatStatus int
			
			public static implicit operator MusicPlayerWantsPlayingRepeatStatus_t ( MusicPlayerWantsPlayingRepeatStatus_t.Pack4 d ) => new MusicPlayerWantsPlayingRepeatStatus_t{ PlayingRepeatStatus = d.PlayingRepeatStatus, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal int PlayingRepeatStatus; // m_nPlayingRepeatStatus int
			
			public static implicit operator MusicPlayerWantsPlayingRepeatStatus_t ( MusicPlayerWantsPlayingRepeatStatus_t.Pack8 d ) => new MusicPlayerWantsPlayingRepeatStatus_t{ PlayingRepeatStatus = d.PlayingRepeatStatus, };
		}
		#endregion
	}
	
	public struct HTTPRequestCompleted_t : Steamworks.ISteamCallback
	{
		internal uint Request; // m_hRequest HTTPRequestHandle
		internal ulong ContextValue; // m_ulContextValue uint64
		[MarshalAs(UnmanagedType.I1)]
		internal bool RequestSuccessful; // m_bRequestSuccessful _Bool
		internal HTTPStatusCode StatusCode; // m_eStatusCode enum EHTTPStatusCode
		internal uint BodySize; // m_unBodySize uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientHTTP + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTTPRequestCompleted_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTTPRequestCompleted_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint Request; // m_hRequest HTTPRequestHandle
			internal ulong ContextValue; // m_ulContextValue uint64
			[MarshalAs(UnmanagedType.I1)]
			internal bool RequestSuccessful; // m_bRequestSuccessful _Bool
			internal HTTPStatusCode StatusCode; // m_eStatusCode enum EHTTPStatusCode
			internal uint BodySize; // m_unBodySize uint32
			
			public static implicit operator HTTPRequestCompleted_t ( HTTPRequestCompleted_t.Pack4 d ) => new HTTPRequestCompleted_t{ Request = d.Request,ContextValue = d.ContextValue,RequestSuccessful = d.RequestSuccessful,StatusCode = d.StatusCode,BodySize = d.BodySize, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint Request; // m_hRequest HTTPRequestHandle
			internal ulong ContextValue; // m_ulContextValue uint64
			[MarshalAs(UnmanagedType.I1)]
			internal bool RequestSuccessful; // m_bRequestSuccessful _Bool
			internal HTTPStatusCode StatusCode; // m_eStatusCode enum EHTTPStatusCode
			internal uint BodySize; // m_unBodySize uint32
			
			public static implicit operator HTTPRequestCompleted_t ( HTTPRequestCompleted_t.Pack8 d ) => new HTTPRequestCompleted_t{ Request = d.Request,ContextValue = d.ContextValue,RequestSuccessful = d.RequestSuccessful,StatusCode = d.StatusCode,BodySize = d.BodySize, };
		}
		#endregion
	}
	
	public struct HTTPRequestHeadersReceived_t : Steamworks.ISteamCallback
	{
		internal uint Request; // m_hRequest HTTPRequestHandle
		internal ulong ContextValue; // m_ulContextValue uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientHTTP + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTTPRequestHeadersReceived_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTTPRequestHeadersReceived_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint Request; // m_hRequest HTTPRequestHandle
			internal ulong ContextValue; // m_ulContextValue uint64
			
			public static implicit operator HTTPRequestHeadersReceived_t ( HTTPRequestHeadersReceived_t.Pack4 d ) => new HTTPRequestHeadersReceived_t{ Request = d.Request,ContextValue = d.ContextValue, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint Request; // m_hRequest HTTPRequestHandle
			internal ulong ContextValue; // m_ulContextValue uint64
			
			public static implicit operator HTTPRequestHeadersReceived_t ( HTTPRequestHeadersReceived_t.Pack8 d ) => new HTTPRequestHeadersReceived_t{ Request = d.Request,ContextValue = d.ContextValue, };
		}
		#endregion
	}
	
	public struct HTTPRequestDataReceived_t : Steamworks.ISteamCallback
	{
		internal uint Request; // m_hRequest HTTPRequestHandle
		internal ulong ContextValue; // m_ulContextValue uint64
		internal uint COffset; // m_cOffset uint32
		internal uint CBytesReceived; // m_cBytesReceived uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientHTTP + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTTPRequestDataReceived_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTTPRequestDataReceived_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint Request; // m_hRequest HTTPRequestHandle
			internal ulong ContextValue; // m_ulContextValue uint64
			internal uint COffset; // m_cOffset uint32
			internal uint CBytesReceived; // m_cBytesReceived uint32
			
			public static implicit operator HTTPRequestDataReceived_t ( HTTPRequestDataReceived_t.Pack4 d ) => new HTTPRequestDataReceived_t{ Request = d.Request,ContextValue = d.ContextValue,COffset = d.COffset,CBytesReceived = d.CBytesReceived, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint Request; // m_hRequest HTTPRequestHandle
			internal ulong ContextValue; // m_ulContextValue uint64
			internal uint COffset; // m_cOffset uint32
			internal uint CBytesReceived; // m_cBytesReceived uint32
			
			public static implicit operator HTTPRequestDataReceived_t ( HTTPRequestDataReceived_t.Pack8 d ) => new HTTPRequestDataReceived_t{ Request = d.Request,ContextValue = d.ContextValue,COffset = d.COffset,CBytesReceived = d.CBytesReceived, };
		}
		#endregion
	}
	
	public struct SteamUGCDetails_t
	{
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		internal WorkshopFileType FileType; // m_eFileType enum EWorkshopFileType
		internal uint CreatorAppID; // m_nCreatorAppID AppId_t
		internal uint ConsumerAppID; // m_nConsumerAppID AppId_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
		internal string Title; // m_rgchTitle char [129]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
		internal string Description; // m_rgchDescription char [8000]
		internal ulong SteamIDOwner; // m_ulSteamIDOwner uint64
		internal uint TimeCreated; // m_rtimeCreated uint32
		internal uint TimeUpdated; // m_rtimeUpdated uint32
		internal uint TimeAddedToUserList; // m_rtimeAddedToUserList uint32
		internal RemoteStoragePublishedFileVisibility Visibility; // m_eVisibility enum ERemoteStoragePublishedFileVisibility
		[MarshalAs(UnmanagedType.I1)]
		internal bool Banned; // m_bBanned _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool AcceptedForUse; // m_bAcceptedForUse _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool TagsTruncated; // m_bTagsTruncated _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
		internal string Tags; // m_rgchTags char [1025]
		internal ulong File; // m_hFile UGCHandle_t
		internal ulong PreviewFile; // m_hPreviewFile UGCHandle_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string PchFileName; // m_pchFileName char [260]
		internal int FileSize; // m_nFileSize int32
		internal int PreviewFileSize; // m_nPreviewFileSize int32
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string URL; // m_rgchURL char [256]
		internal uint VotesUp; // m_unVotesUp uint32
		internal uint VotesDown; // m_unVotesDown uint32
		internal float Score; // m_flScore float
		internal uint NumChildren; // m_unNumChildren uint32
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public SteamUGCDetails_t Fill( IntPtr p ) => Platform.PackSmall ? ((SteamUGCDetails_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamUGCDetails_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal Result Result; // m_eResult enum EResult
			internal WorkshopFileType FileType; // m_eFileType enum EWorkshopFileType
			internal uint CreatorAppID; // m_nCreatorAppID AppId_t
			internal uint ConsumerAppID; // m_nConsumerAppID AppId_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
			internal string Title; // m_rgchTitle char [129]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
			internal string Description; // m_rgchDescription char [8000]
			internal ulong SteamIDOwner; // m_ulSteamIDOwner uint64
			internal uint TimeCreated; // m_rtimeCreated uint32
			internal uint TimeUpdated; // m_rtimeUpdated uint32
			internal uint TimeAddedToUserList; // m_rtimeAddedToUserList uint32
			internal RemoteStoragePublishedFileVisibility Visibility; // m_eVisibility enum ERemoteStoragePublishedFileVisibility
			[MarshalAs(UnmanagedType.I1)]
			internal bool Banned; // m_bBanned _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool AcceptedForUse; // m_bAcceptedForUse _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool TagsTruncated; // m_bTagsTruncated _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
			internal string Tags; // m_rgchTags char [1025]
			internal ulong File; // m_hFile UGCHandle_t
			internal ulong PreviewFile; // m_hPreviewFile UGCHandle_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string PchFileName; // m_pchFileName char [260]
			internal int FileSize; // m_nFileSize int32
			internal int PreviewFileSize; // m_nPreviewFileSize int32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string URL; // m_rgchURL char [256]
			internal uint VotesUp; // m_unVotesUp uint32
			internal uint VotesDown; // m_unVotesDown uint32
			internal float Score; // m_flScore float
			internal uint NumChildren; // m_unNumChildren uint32
			
			public static implicit operator SteamUGCDetails_t ( SteamUGCDetails_t.Pack4 d ) => new SteamUGCDetails_t{ PublishedFileId = d.PublishedFileId,Result = d.Result,FileType = d.FileType,CreatorAppID = d.CreatorAppID,ConsumerAppID = d.ConsumerAppID,Title = d.Title,Description = d.Description,SteamIDOwner = d.SteamIDOwner,TimeCreated = d.TimeCreated,TimeUpdated = d.TimeUpdated,TimeAddedToUserList = d.TimeAddedToUserList,Visibility = d.Visibility,Banned = d.Banned,AcceptedForUse = d.AcceptedForUse,TagsTruncated = d.TagsTruncated,Tags = d.Tags,File = d.File,PreviewFile = d.PreviewFile,PchFileName = d.PchFileName,FileSize = d.FileSize,PreviewFileSize = d.PreviewFileSize,URL = d.URL,VotesUp = d.VotesUp,VotesDown = d.VotesDown,Score = d.Score,NumChildren = d.NumChildren, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal Result Result; // m_eResult enum EResult
			internal WorkshopFileType FileType; // m_eFileType enum EWorkshopFileType
			internal uint CreatorAppID; // m_nCreatorAppID AppId_t
			internal uint ConsumerAppID; // m_nConsumerAppID AppId_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
			internal string Title; // m_rgchTitle char [129]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
			internal string Description; // m_rgchDescription char [8000]
			internal ulong SteamIDOwner; // m_ulSteamIDOwner uint64
			internal uint TimeCreated; // m_rtimeCreated uint32
			internal uint TimeUpdated; // m_rtimeUpdated uint32
			internal uint TimeAddedToUserList; // m_rtimeAddedToUserList uint32
			internal RemoteStoragePublishedFileVisibility Visibility; // m_eVisibility enum ERemoteStoragePublishedFileVisibility
			[MarshalAs(UnmanagedType.I1)]
			internal bool Banned; // m_bBanned _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool AcceptedForUse; // m_bAcceptedForUse _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool TagsTruncated; // m_bTagsTruncated _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
			internal string Tags; // m_rgchTags char [1025]
			internal ulong File; // m_hFile UGCHandle_t
			internal ulong PreviewFile; // m_hPreviewFile UGCHandle_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string PchFileName; // m_pchFileName char [260]
			internal int FileSize; // m_nFileSize int32
			internal int PreviewFileSize; // m_nPreviewFileSize int32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string URL; // m_rgchURL char [256]
			internal uint VotesUp; // m_unVotesUp uint32
			internal uint VotesDown; // m_unVotesDown uint32
			internal float Score; // m_flScore float
			internal uint NumChildren; // m_unNumChildren uint32
			
			public static implicit operator SteamUGCDetails_t ( SteamUGCDetails_t.Pack8 d ) => new SteamUGCDetails_t{ PublishedFileId = d.PublishedFileId,Result = d.Result,FileType = d.FileType,CreatorAppID = d.CreatorAppID,ConsumerAppID = d.ConsumerAppID,Title = d.Title,Description = d.Description,SteamIDOwner = d.SteamIDOwner,TimeCreated = d.TimeCreated,TimeUpdated = d.TimeUpdated,TimeAddedToUserList = d.TimeAddedToUserList,Visibility = d.Visibility,Banned = d.Banned,AcceptedForUse = d.AcceptedForUse,TagsTruncated = d.TagsTruncated,Tags = d.Tags,File = d.File,PreviewFile = d.PreviewFile,PchFileName = d.PchFileName,FileSize = d.FileSize,PreviewFileSize = d.PreviewFileSize,URL = d.URL,VotesUp = d.VotesUp,VotesDown = d.VotesDown,Score = d.Score,NumChildren = d.NumChildren, };
		}
		#endregion
	}
	
	public struct SteamUGCQueryCompleted_t : Steamworks.ISteamCallback
	{
		internal ulong Handle; // m_handle UGCQueryHandle_t
		internal Result Result; // m_eResult enum EResult
		internal uint NumResultsReturned; // m_unNumResultsReturned uint32
		internal uint TotalMatchingResults; // m_unTotalMatchingResults uint32
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData; // m_bCachedData _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string NextCursor; // m_rgchNextCursor char [256]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SteamUGCQueryCompleted_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamUGCQueryCompleted_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong Handle; // m_handle UGCQueryHandle_t
			internal Result Result; // m_eResult enum EResult
			internal uint NumResultsReturned; // m_unNumResultsReturned uint32
			internal uint TotalMatchingResults; // m_unTotalMatchingResults uint32
			[MarshalAs(UnmanagedType.I1)]
			internal bool CachedData; // m_bCachedData _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string NextCursor; // m_rgchNextCursor char [256]
			
			public static implicit operator SteamUGCQueryCompleted_t ( SteamUGCQueryCompleted_t.Pack4 d ) => new SteamUGCQueryCompleted_t{ Handle = d.Handle,Result = d.Result,NumResultsReturned = d.NumResultsReturned,TotalMatchingResults = d.TotalMatchingResults,CachedData = d.CachedData,NextCursor = d.NextCursor, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong Handle; // m_handle UGCQueryHandle_t
			internal Result Result; // m_eResult enum EResult
			internal uint NumResultsReturned; // m_unNumResultsReturned uint32
			internal uint TotalMatchingResults; // m_unTotalMatchingResults uint32
			[MarshalAs(UnmanagedType.I1)]
			internal bool CachedData; // m_bCachedData _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string NextCursor; // m_rgchNextCursor char [256]
			
			public static implicit operator SteamUGCQueryCompleted_t ( SteamUGCQueryCompleted_t.Pack8 d ) => new SteamUGCQueryCompleted_t{ Handle = d.Handle,Result = d.Result,NumResultsReturned = d.NumResultsReturned,TotalMatchingResults = d.TotalMatchingResults,CachedData = d.CachedData,NextCursor = d.NextCursor, };
		}
		#endregion
	}
	
	public struct SteamUGCRequestUGCDetailsResult_t : Steamworks.ISteamCallback
	{
		internal SteamUGCDetails_t Details; // m_details struct SteamUGCDetails_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData; // m_bCachedData _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SteamUGCRequestUGCDetailsResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamUGCRequestUGCDetailsResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal SteamUGCDetails_t Details; // m_details struct SteamUGCDetails_t
			[MarshalAs(UnmanagedType.I1)]
			internal bool CachedData; // m_bCachedData _Bool
			
			public static implicit operator SteamUGCRequestUGCDetailsResult_t ( SteamUGCRequestUGCDetailsResult_t.Pack4 d ) => new SteamUGCRequestUGCDetailsResult_t{ Details = d.Details,CachedData = d.CachedData, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal SteamUGCDetails_t Details; // m_details struct SteamUGCDetails_t
			[MarshalAs(UnmanagedType.I1)]
			internal bool CachedData; // m_bCachedData _Bool
			
			public static implicit operator SteamUGCRequestUGCDetailsResult_t ( SteamUGCRequestUGCDetailsResult_t.Pack8 d ) => new SteamUGCRequestUGCDetailsResult_t{ Details = d.Details,CachedData = d.CachedData, };
		}
		#endregion
	}
	
	public struct CreateItemResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((CreateItemResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((CreateItemResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			[MarshalAs(UnmanagedType.I1)]
			internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			
			public static implicit operator CreateItemResult_t ( CreateItemResult_t.Pack4 d ) => new CreateItemResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			[MarshalAs(UnmanagedType.I1)]
			internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			
			public static implicit operator CreateItemResult_t ( CreateItemResult_t.Pack8 d ) => new CreateItemResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement, };
		}
		#endregion
	}
	
	public struct SubmitItemUpdateResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 4;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SubmitItemUpdateResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SubmitItemUpdateResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator SubmitItemUpdateResult_t ( SubmitItemUpdateResult_t.Pack4 d ) => new SubmitItemUpdateResult_t{ Result = d.Result,UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement,PublishedFileId = d.PublishedFileId, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator SubmitItemUpdateResult_t ( SubmitItemUpdateResult_t.Pack8 d ) => new SubmitItemUpdateResult_t{ Result = d.Result,UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement,PublishedFileId = d.PublishedFileId, };
		}
		#endregion
	}
	
	public struct DownloadItemResult_t : Steamworks.ISteamCallback
	{
		internal uint AppID; // m_unAppID AppId_t
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 6;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((DownloadItemResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((DownloadItemResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint AppID; // m_unAppID AppId_t
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator DownloadItemResult_t ( DownloadItemResult_t.Pack4 d ) => new DownloadItemResult_t{ AppID = d.AppID,PublishedFileId = d.PublishedFileId,Result = d.Result, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint AppID; // m_unAppID AppId_t
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator DownloadItemResult_t ( DownloadItemResult_t.Pack8 d ) => new DownloadItemResult_t{ AppID = d.AppID,PublishedFileId = d.PublishedFileId,Result = d.Result, };
		}
		#endregion
	}
	
	public struct UserFavoriteItemsListChanged_t : Steamworks.ISteamCallback
	{
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool WasAddRequest; // m_bWasAddRequest _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 7;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((UserFavoriteItemsListChanged_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((UserFavoriteItemsListChanged_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			internal bool WasAddRequest; // m_bWasAddRequest _Bool
			
			public static implicit operator UserFavoriteItemsListChanged_t ( UserFavoriteItemsListChanged_t.Pack4 d ) => new UserFavoriteItemsListChanged_t{ PublishedFileId = d.PublishedFileId,Result = d.Result,WasAddRequest = d.WasAddRequest, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			internal bool WasAddRequest; // m_bWasAddRequest _Bool
			
			public static implicit operator UserFavoriteItemsListChanged_t ( UserFavoriteItemsListChanged_t.Pack8 d ) => new UserFavoriteItemsListChanged_t{ PublishedFileId = d.PublishedFileId,Result = d.Result,WasAddRequest = d.WasAddRequest, };
		}
		#endregion
	}
	
	public struct SetUserItemVoteResult_t : Steamworks.ISteamCallback
	{
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool VoteUp; // m_bVoteUp _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 8;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SetUserItemVoteResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SetUserItemVoteResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			internal bool VoteUp; // m_bVoteUp _Bool
			
			public static implicit operator SetUserItemVoteResult_t ( SetUserItemVoteResult_t.Pack4 d ) => new SetUserItemVoteResult_t{ PublishedFileId = d.PublishedFileId,Result = d.Result,VoteUp = d.VoteUp, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			internal bool VoteUp; // m_bVoteUp _Bool
			
			public static implicit operator SetUserItemVoteResult_t ( SetUserItemVoteResult_t.Pack8 d ) => new SetUserItemVoteResult_t{ PublishedFileId = d.PublishedFileId,Result = d.Result,VoteUp = d.VoteUp, };
		}
		#endregion
	}
	
	public struct GetUserItemVoteResult_t : Steamworks.ISteamCallback
	{
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool VotedUp; // m_bVotedUp _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool VotedDown; // m_bVotedDown _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool VoteSkipped; // m_bVoteSkipped _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 9;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GetUserItemVoteResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GetUserItemVoteResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			internal bool VotedUp; // m_bVotedUp _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool VotedDown; // m_bVotedDown _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool VoteSkipped; // m_bVoteSkipped _Bool
			
			public static implicit operator GetUserItemVoteResult_t ( GetUserItemVoteResult_t.Pack4 d ) => new GetUserItemVoteResult_t{ PublishedFileId = d.PublishedFileId,Result = d.Result,VotedUp = d.VotedUp,VotedDown = d.VotedDown,VoteSkipped = d.VoteSkipped, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			internal bool VotedUp; // m_bVotedUp _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool VotedDown; // m_bVotedDown _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool VoteSkipped; // m_bVoteSkipped _Bool
			
			public static implicit operator GetUserItemVoteResult_t ( GetUserItemVoteResult_t.Pack8 d ) => new GetUserItemVoteResult_t{ PublishedFileId = d.PublishedFileId,Result = d.Result,VotedUp = d.VotedUp,VotedDown = d.VotedDown,VoteSkipped = d.VoteSkipped, };
		}
		#endregion
	}
	
	public struct StartPlaytimeTrackingResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 10;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((StartPlaytimeTrackingResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((StartPlaytimeTrackingResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator StartPlaytimeTrackingResult_t ( StartPlaytimeTrackingResult_t.Pack4 d ) => new StartPlaytimeTrackingResult_t{ Result = d.Result, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator StartPlaytimeTrackingResult_t ( StartPlaytimeTrackingResult_t.Pack8 d ) => new StartPlaytimeTrackingResult_t{ Result = d.Result, };
		}
		#endregion
	}
	
	public struct StopPlaytimeTrackingResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 11;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((StopPlaytimeTrackingResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((StopPlaytimeTrackingResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator StopPlaytimeTrackingResult_t ( StopPlaytimeTrackingResult_t.Pack4 d ) => new StopPlaytimeTrackingResult_t{ Result = d.Result, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator StopPlaytimeTrackingResult_t ( StopPlaytimeTrackingResult_t.Pack8 d ) => new StopPlaytimeTrackingResult_t{ Result = d.Result, };
		}
		#endregion
	}
	
	public struct AddUGCDependencyResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal ulong ChildPublishedFileId; // m_nChildPublishedFileId PublishedFileId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 12;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((AddUGCDependencyResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((AddUGCDependencyResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal ulong ChildPublishedFileId; // m_nChildPublishedFileId PublishedFileId_t
			
			public static implicit operator AddUGCDependencyResult_t ( AddUGCDependencyResult_t.Pack4 d ) => new AddUGCDependencyResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,ChildPublishedFileId = d.ChildPublishedFileId, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal ulong ChildPublishedFileId; // m_nChildPublishedFileId PublishedFileId_t
			
			public static implicit operator AddUGCDependencyResult_t ( AddUGCDependencyResult_t.Pack8 d ) => new AddUGCDependencyResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,ChildPublishedFileId = d.ChildPublishedFileId, };
		}
		#endregion
	}
	
	public struct RemoveUGCDependencyResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal ulong ChildPublishedFileId; // m_nChildPublishedFileId PublishedFileId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 13;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoveUGCDependencyResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoveUGCDependencyResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal ulong ChildPublishedFileId; // m_nChildPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoveUGCDependencyResult_t ( RemoveUGCDependencyResult_t.Pack4 d ) => new RemoveUGCDependencyResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,ChildPublishedFileId = d.ChildPublishedFileId, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal ulong ChildPublishedFileId; // m_nChildPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoveUGCDependencyResult_t ( RemoveUGCDependencyResult_t.Pack8 d ) => new RemoveUGCDependencyResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,ChildPublishedFileId = d.ChildPublishedFileId, };
		}
		#endregion
	}
	
	public struct AddAppDependencyResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal uint AppID; // m_nAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 14;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((AddAppDependencyResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((AddAppDependencyResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal uint AppID; // m_nAppID AppId_t
			
			public static implicit operator AddAppDependencyResult_t ( AddAppDependencyResult_t.Pack4 d ) => new AddAppDependencyResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,AppID = d.AppID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal uint AppID; // m_nAppID AppId_t
			
			public static implicit operator AddAppDependencyResult_t ( AddAppDependencyResult_t.Pack8 d ) => new AddAppDependencyResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,AppID = d.AppID, };
		}
		#endregion
	}
	
	public struct RemoveAppDependencyResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal uint AppID; // m_nAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 15;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((RemoveAppDependencyResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((RemoveAppDependencyResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal uint AppID; // m_nAppID AppId_t
			
			public static implicit operator RemoveAppDependencyResult_t ( RemoveAppDependencyResult_t.Pack4 d ) => new RemoveAppDependencyResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,AppID = d.AppID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal uint AppID; // m_nAppID AppId_t
			
			public static implicit operator RemoveAppDependencyResult_t ( RemoveAppDependencyResult_t.Pack8 d ) => new RemoveAppDependencyResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,AppID = d.AppID, };
		}
		#endregion
	}
	
	public struct GetAppDependenciesResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.U4)]
		internal AppId_t[] GAppIDs; // m_rgAppIDs AppId_t [32]
		internal uint NumAppDependencies; // m_nNumAppDependencies uint32
		internal uint TotalNumAppDependencies; // m_nTotalNumAppDependencies uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 16;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GetAppDependenciesResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GetAppDependenciesResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.U4)]
			internal AppId_t[] GAppIDs; // m_rgAppIDs AppId_t [32]
			internal uint NumAppDependencies; // m_nNumAppDependencies uint32
			internal uint TotalNumAppDependencies; // m_nTotalNumAppDependencies uint32
			
			public static implicit operator GetAppDependenciesResult_t ( GetAppDependenciesResult_t.Pack4 d ) => new GetAppDependenciesResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,GAppIDs = d.GAppIDs,NumAppDependencies = d.NumAppDependencies,TotalNumAppDependencies = d.TotalNumAppDependencies, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.U4)]
			internal AppId_t[] GAppIDs; // m_rgAppIDs AppId_t [32]
			internal uint NumAppDependencies; // m_nNumAppDependencies uint32
			internal uint TotalNumAppDependencies; // m_nTotalNumAppDependencies uint32
			
			public static implicit operator GetAppDependenciesResult_t ( GetAppDependenciesResult_t.Pack8 d ) => new GetAppDependenciesResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,GAppIDs = d.GAppIDs,NumAppDependencies = d.NumAppDependencies,TotalNumAppDependencies = d.TotalNumAppDependencies, };
		}
		#endregion
	}
	
	public struct DeleteItemResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 17;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((DeleteItemResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((DeleteItemResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator DeleteItemResult_t ( DeleteItemResult_t.Pack4 d ) => new DeleteItemResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator DeleteItemResult_t ( DeleteItemResult_t.Pack8 d ) => new DeleteItemResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId, };
		}
		#endregion
	}
	
	public struct SteamAppInstalled_t : Steamworks.ISteamCallback
	{
		internal uint AppID; // m_nAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamAppList + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SteamAppInstalled_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamAppInstalled_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint AppID; // m_nAppID AppId_t
			
			public static implicit operator SteamAppInstalled_t ( SteamAppInstalled_t.Pack4 d ) => new SteamAppInstalled_t{ AppID = d.AppID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint AppID; // m_nAppID AppId_t
			
			public static implicit operator SteamAppInstalled_t ( SteamAppInstalled_t.Pack8 d ) => new SteamAppInstalled_t{ AppID = d.AppID, };
		}
		#endregion
	}
	
	public struct SteamAppUninstalled_t : Steamworks.ISteamCallback
	{
		internal uint AppID; // m_nAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamAppList + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SteamAppUninstalled_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamAppUninstalled_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint AppID; // m_nAppID AppId_t
			
			public static implicit operator SteamAppUninstalled_t ( SteamAppUninstalled_t.Pack4 d ) => new SteamAppUninstalled_t{ AppID = d.AppID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint AppID; // m_nAppID AppId_t
			
			public static implicit operator SteamAppUninstalled_t ( SteamAppUninstalled_t.Pack8 d ) => new SteamAppUninstalled_t{ AppID = d.AppID, };
		}
		#endregion
	}
	
	public struct HTML_BrowserReady_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_BrowserReady_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_BrowserReady_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			
			public static implicit operator HTML_BrowserReady_t ( HTML_BrowserReady_t.Pack4 d ) => new HTML_BrowserReady_t{ UnBrowserHandle = d.UnBrowserHandle, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			
			public static implicit operator HTML_BrowserReady_t ( HTML_BrowserReady_t.Pack8 d ) => new HTML_BrowserReady_t{ UnBrowserHandle = d.UnBrowserHandle, };
		}
		#endregion
	}
	
	public struct HTML_NeedsPaint_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PBGRA; // pBGRA const char *
		internal uint UnWide; // unWide uint32
		internal uint UnTall; // unTall uint32
		internal uint UnUpdateX; // unUpdateX uint32
		internal uint UnUpdateY; // unUpdateY uint32
		internal uint UnUpdateWide; // unUpdateWide uint32
		internal uint UnUpdateTall; // unUpdateTall uint32
		internal uint UnScrollX; // unScrollX uint32
		internal uint UnScrollY; // unScrollY uint32
		internal float FlPageScale; // flPageScale float
		internal uint UnPageSerial; // unPageSerial uint32
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public HTML_NeedsPaint_t Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_NeedsPaint_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_NeedsPaint_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PBGRA; // pBGRA const char *
			internal uint UnWide; // unWide uint32
			internal uint UnTall; // unTall uint32
			internal uint UnUpdateX; // unUpdateX uint32
			internal uint UnUpdateY; // unUpdateY uint32
			internal uint UnUpdateWide; // unUpdateWide uint32
			internal uint UnUpdateTall; // unUpdateTall uint32
			internal uint UnScrollX; // unScrollX uint32
			internal uint UnScrollY; // unScrollY uint32
			internal float FlPageScale; // flPageScale float
			internal uint UnPageSerial; // unPageSerial uint32
			
			public static implicit operator HTML_NeedsPaint_t ( HTML_NeedsPaint_t.Pack4 d ) => new HTML_NeedsPaint_t{ UnBrowserHandle = d.UnBrowserHandle,PBGRA = d.PBGRA,UnWide = d.UnWide,UnTall = d.UnTall,UnUpdateX = d.UnUpdateX,UnUpdateY = d.UnUpdateY,UnUpdateWide = d.UnUpdateWide,UnUpdateTall = d.UnUpdateTall,UnScrollX = d.UnScrollX,UnScrollY = d.UnScrollY,FlPageScale = d.FlPageScale,UnPageSerial = d.UnPageSerial, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PBGRA; // pBGRA const char *
			internal uint UnWide; // unWide uint32
			internal uint UnTall; // unTall uint32
			internal uint UnUpdateX; // unUpdateX uint32
			internal uint UnUpdateY; // unUpdateY uint32
			internal uint UnUpdateWide; // unUpdateWide uint32
			internal uint UnUpdateTall; // unUpdateTall uint32
			internal uint UnScrollX; // unScrollX uint32
			internal uint UnScrollY; // unScrollY uint32
			internal float FlPageScale; // flPageScale float
			internal uint UnPageSerial; // unPageSerial uint32
			
			public static implicit operator HTML_NeedsPaint_t ( HTML_NeedsPaint_t.Pack8 d ) => new HTML_NeedsPaint_t{ UnBrowserHandle = d.UnBrowserHandle,PBGRA = d.PBGRA,UnWide = d.UnWide,UnTall = d.UnTall,UnUpdateX = d.UnUpdateX,UnUpdateY = d.UnUpdateY,UnUpdateWide = d.UnUpdateWide,UnUpdateTall = d.UnUpdateTall,UnScrollX = d.UnScrollX,UnScrollY = d.UnScrollY,FlPageScale = d.FlPageScale,UnPageSerial = d.UnPageSerial, };
		}
		#endregion
	}
	
	public struct HTML_StartRequest_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		internal string PchTarget; // pchTarget const char *
		internal string PchPostData; // pchPostData const char *
		[MarshalAs(UnmanagedType.I1)]
		internal bool BIsRedirect; // bIsRedirect _Bool
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public HTML_StartRequest_t Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_StartRequest_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_StartRequest_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchURL; // pchURL const char *
			internal string PchTarget; // pchTarget const char *
			internal string PchPostData; // pchPostData const char *
			[MarshalAs(UnmanagedType.I1)]
			internal bool BIsRedirect; // bIsRedirect _Bool
			
			public static implicit operator HTML_StartRequest_t ( HTML_StartRequest_t.Pack4 d ) => new HTML_StartRequest_t{ UnBrowserHandle = d.UnBrowserHandle,PchURL = d.PchURL,PchTarget = d.PchTarget,PchPostData = d.PchPostData,BIsRedirect = d.BIsRedirect, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchURL; // pchURL const char *
			internal string PchTarget; // pchTarget const char *
			internal string PchPostData; // pchPostData const char *
			[MarshalAs(UnmanagedType.I1)]
			internal bool BIsRedirect; // bIsRedirect _Bool
			
			public static implicit operator HTML_StartRequest_t ( HTML_StartRequest_t.Pack8 d ) => new HTML_StartRequest_t{ UnBrowserHandle = d.UnBrowserHandle,PchURL = d.PchURL,PchTarget = d.PchTarget,PchPostData = d.PchPostData,BIsRedirect = d.BIsRedirect, };
		}
		#endregion
	}
	
	public struct HTML_CloseBrowser_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public HTML_CloseBrowser_t Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_CloseBrowser_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_CloseBrowser_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			
			public static implicit operator HTML_CloseBrowser_t ( HTML_CloseBrowser_t.Pack4 d ) => new HTML_CloseBrowser_t{ UnBrowserHandle = d.UnBrowserHandle, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			
			public static implicit operator HTML_CloseBrowser_t ( HTML_CloseBrowser_t.Pack8 d ) => new HTML_CloseBrowser_t{ UnBrowserHandle = d.UnBrowserHandle, };
		}
		#endregion
	}
	
	public struct HTML_URLChanged_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		internal string PchPostData; // pchPostData const char *
		[MarshalAs(UnmanagedType.I1)]
		internal bool BIsRedirect; // bIsRedirect _Bool
		internal string PchPageTitle; // pchPageTitle const char *
		[MarshalAs(UnmanagedType.I1)]
		internal bool BNewNavigation; // bNewNavigation _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 5;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_URLChanged_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_URLChanged_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchURL; // pchURL const char *
			internal string PchPostData; // pchPostData const char *
			[MarshalAs(UnmanagedType.I1)]
			internal bool BIsRedirect; // bIsRedirect _Bool
			internal string PchPageTitle; // pchPageTitle const char *
			[MarshalAs(UnmanagedType.I1)]
			internal bool BNewNavigation; // bNewNavigation _Bool
			
			public static implicit operator HTML_URLChanged_t ( HTML_URLChanged_t.Pack4 d ) => new HTML_URLChanged_t{ UnBrowserHandle = d.UnBrowserHandle,PchURL = d.PchURL,PchPostData = d.PchPostData,BIsRedirect = d.BIsRedirect,PchPageTitle = d.PchPageTitle,BNewNavigation = d.BNewNavigation, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchURL; // pchURL const char *
			internal string PchPostData; // pchPostData const char *
			[MarshalAs(UnmanagedType.I1)]
			internal bool BIsRedirect; // bIsRedirect _Bool
			internal string PchPageTitle; // pchPageTitle const char *
			[MarshalAs(UnmanagedType.I1)]
			internal bool BNewNavigation; // bNewNavigation _Bool
			
			public static implicit operator HTML_URLChanged_t ( HTML_URLChanged_t.Pack8 d ) => new HTML_URLChanged_t{ UnBrowserHandle = d.UnBrowserHandle,PchURL = d.PchURL,PchPostData = d.PchPostData,BIsRedirect = d.BIsRedirect,PchPageTitle = d.PchPageTitle,BNewNavigation = d.BNewNavigation, };
		}
		#endregion
	}
	
	public struct HTML_FinishedRequest_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		internal string PchPageTitle; // pchPageTitle const char *
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 6;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_FinishedRequest_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_FinishedRequest_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchURL; // pchURL const char *
			internal string PchPageTitle; // pchPageTitle const char *
			
			public static implicit operator HTML_FinishedRequest_t ( HTML_FinishedRequest_t.Pack4 d ) => new HTML_FinishedRequest_t{ UnBrowserHandle = d.UnBrowserHandle,PchURL = d.PchURL,PchPageTitle = d.PchPageTitle, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchURL; // pchURL const char *
			internal string PchPageTitle; // pchPageTitle const char *
			
			public static implicit operator HTML_FinishedRequest_t ( HTML_FinishedRequest_t.Pack8 d ) => new HTML_FinishedRequest_t{ UnBrowserHandle = d.UnBrowserHandle,PchURL = d.PchURL,PchPageTitle = d.PchPageTitle, };
		}
		#endregion
	}
	
	public struct HTML_OpenLinkInNewTab_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 7;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_OpenLinkInNewTab_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_OpenLinkInNewTab_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchURL; // pchURL const char *
			
			public static implicit operator HTML_OpenLinkInNewTab_t ( HTML_OpenLinkInNewTab_t.Pack4 d ) => new HTML_OpenLinkInNewTab_t{ UnBrowserHandle = d.UnBrowserHandle,PchURL = d.PchURL, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchURL; // pchURL const char *
			
			public static implicit operator HTML_OpenLinkInNewTab_t ( HTML_OpenLinkInNewTab_t.Pack8 d ) => new HTML_OpenLinkInNewTab_t{ UnBrowserHandle = d.UnBrowserHandle,PchURL = d.PchURL, };
		}
		#endregion
	}
	
	public struct HTML_ChangedTitle_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchTitle; // pchTitle const char *
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 8;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_ChangedTitle_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_ChangedTitle_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchTitle; // pchTitle const char *
			
			public static implicit operator HTML_ChangedTitle_t ( HTML_ChangedTitle_t.Pack4 d ) => new HTML_ChangedTitle_t{ UnBrowserHandle = d.UnBrowserHandle,PchTitle = d.PchTitle, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchTitle; // pchTitle const char *
			
			public static implicit operator HTML_ChangedTitle_t ( HTML_ChangedTitle_t.Pack8 d ) => new HTML_ChangedTitle_t{ UnBrowserHandle = d.UnBrowserHandle,PchTitle = d.PchTitle, };
		}
		#endregion
	}
	
	public struct HTML_SearchResults_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint UnResults; // unResults uint32
		internal uint UnCurrentMatch; // unCurrentMatch uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 9;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_SearchResults_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_SearchResults_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal uint UnResults; // unResults uint32
			internal uint UnCurrentMatch; // unCurrentMatch uint32
			
			public static implicit operator HTML_SearchResults_t ( HTML_SearchResults_t.Pack4 d ) => new HTML_SearchResults_t{ UnBrowserHandle = d.UnBrowserHandle,UnResults = d.UnResults,UnCurrentMatch = d.UnCurrentMatch, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal uint UnResults; // unResults uint32
			internal uint UnCurrentMatch; // unCurrentMatch uint32
			
			public static implicit operator HTML_SearchResults_t ( HTML_SearchResults_t.Pack8 d ) => new HTML_SearchResults_t{ UnBrowserHandle = d.UnBrowserHandle,UnResults = d.UnResults,UnCurrentMatch = d.UnCurrentMatch, };
		}
		#endregion
	}
	
	public struct HTML_CanGoBackAndForward_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		[MarshalAs(UnmanagedType.I1)]
		internal bool BCanGoBack; // bCanGoBack _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool BCanGoForward; // bCanGoForward _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 10;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_CanGoBackAndForward_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_CanGoBackAndForward_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			[MarshalAs(UnmanagedType.I1)]
			internal bool BCanGoBack; // bCanGoBack _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool BCanGoForward; // bCanGoForward _Bool
			
			public static implicit operator HTML_CanGoBackAndForward_t ( HTML_CanGoBackAndForward_t.Pack4 d ) => new HTML_CanGoBackAndForward_t{ UnBrowserHandle = d.UnBrowserHandle,BCanGoBack = d.BCanGoBack,BCanGoForward = d.BCanGoForward, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			[MarshalAs(UnmanagedType.I1)]
			internal bool BCanGoBack; // bCanGoBack _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool BCanGoForward; // bCanGoForward _Bool
			
			public static implicit operator HTML_CanGoBackAndForward_t ( HTML_CanGoBackAndForward_t.Pack8 d ) => new HTML_CanGoBackAndForward_t{ UnBrowserHandle = d.UnBrowserHandle,BCanGoBack = d.BCanGoBack,BCanGoForward = d.BCanGoForward, };
		}
		#endregion
	}
	
	public struct HTML_HorizontalScroll_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint UnScrollMax; // unScrollMax uint32
		internal uint UnScrollCurrent; // unScrollCurrent uint32
		internal float FlPageScale; // flPageScale float
		[MarshalAs(UnmanagedType.I1)]
		internal bool BVisible; // bVisible _Bool
		internal uint UnPageSize; // unPageSize uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 11;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_HorizontalScroll_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_HorizontalScroll_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal uint UnScrollMax; // unScrollMax uint32
			internal uint UnScrollCurrent; // unScrollCurrent uint32
			internal float FlPageScale; // flPageScale float
			[MarshalAs(UnmanagedType.I1)]
			internal bool BVisible; // bVisible _Bool
			internal uint UnPageSize; // unPageSize uint32
			
			public static implicit operator HTML_HorizontalScroll_t ( HTML_HorizontalScroll_t.Pack4 d ) => new HTML_HorizontalScroll_t{ UnBrowserHandle = d.UnBrowserHandle,UnScrollMax = d.UnScrollMax,UnScrollCurrent = d.UnScrollCurrent,FlPageScale = d.FlPageScale,BVisible = d.BVisible,UnPageSize = d.UnPageSize, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal uint UnScrollMax; // unScrollMax uint32
			internal uint UnScrollCurrent; // unScrollCurrent uint32
			internal float FlPageScale; // flPageScale float
			[MarshalAs(UnmanagedType.I1)]
			internal bool BVisible; // bVisible _Bool
			internal uint UnPageSize; // unPageSize uint32
			
			public static implicit operator HTML_HorizontalScroll_t ( HTML_HorizontalScroll_t.Pack8 d ) => new HTML_HorizontalScroll_t{ UnBrowserHandle = d.UnBrowserHandle,UnScrollMax = d.UnScrollMax,UnScrollCurrent = d.UnScrollCurrent,FlPageScale = d.FlPageScale,BVisible = d.BVisible,UnPageSize = d.UnPageSize, };
		}
		#endregion
	}
	
	public struct HTML_VerticalScroll_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint UnScrollMax; // unScrollMax uint32
		internal uint UnScrollCurrent; // unScrollCurrent uint32
		internal float FlPageScale; // flPageScale float
		[MarshalAs(UnmanagedType.I1)]
		internal bool BVisible; // bVisible _Bool
		internal uint UnPageSize; // unPageSize uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 12;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_VerticalScroll_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_VerticalScroll_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal uint UnScrollMax; // unScrollMax uint32
			internal uint UnScrollCurrent; // unScrollCurrent uint32
			internal float FlPageScale; // flPageScale float
			[MarshalAs(UnmanagedType.I1)]
			internal bool BVisible; // bVisible _Bool
			internal uint UnPageSize; // unPageSize uint32
			
			public static implicit operator HTML_VerticalScroll_t ( HTML_VerticalScroll_t.Pack4 d ) => new HTML_VerticalScroll_t{ UnBrowserHandle = d.UnBrowserHandle,UnScrollMax = d.UnScrollMax,UnScrollCurrent = d.UnScrollCurrent,FlPageScale = d.FlPageScale,BVisible = d.BVisible,UnPageSize = d.UnPageSize, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal uint UnScrollMax; // unScrollMax uint32
			internal uint UnScrollCurrent; // unScrollCurrent uint32
			internal float FlPageScale; // flPageScale float
			[MarshalAs(UnmanagedType.I1)]
			internal bool BVisible; // bVisible _Bool
			internal uint UnPageSize; // unPageSize uint32
			
			public static implicit operator HTML_VerticalScroll_t ( HTML_VerticalScroll_t.Pack8 d ) => new HTML_VerticalScroll_t{ UnBrowserHandle = d.UnBrowserHandle,UnScrollMax = d.UnScrollMax,UnScrollCurrent = d.UnScrollCurrent,FlPageScale = d.FlPageScale,BVisible = d.BVisible,UnPageSize = d.UnPageSize, };
		}
		#endregion
	}
	
	public struct HTML_LinkAtPosition_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint X; // x uint32
		internal uint Y; // y uint32
		internal string PchURL; // pchURL const char *
		[MarshalAs(UnmanagedType.I1)]
		internal bool BInput; // bInput _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool BLiveLink; // bLiveLink _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 13;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_LinkAtPosition_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_LinkAtPosition_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal uint X; // x uint32
			internal uint Y; // y uint32
			internal string PchURL; // pchURL const char *
			[MarshalAs(UnmanagedType.I1)]
			internal bool BInput; // bInput _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool BLiveLink; // bLiveLink _Bool
			
			public static implicit operator HTML_LinkAtPosition_t ( HTML_LinkAtPosition_t.Pack4 d ) => new HTML_LinkAtPosition_t{ UnBrowserHandle = d.UnBrowserHandle,X = d.X,Y = d.Y,PchURL = d.PchURL,BInput = d.BInput,BLiveLink = d.BLiveLink, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal uint X; // x uint32
			internal uint Y; // y uint32
			internal string PchURL; // pchURL const char *
			[MarshalAs(UnmanagedType.I1)]
			internal bool BInput; // bInput _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool BLiveLink; // bLiveLink _Bool
			
			public static implicit operator HTML_LinkAtPosition_t ( HTML_LinkAtPosition_t.Pack8 d ) => new HTML_LinkAtPosition_t{ UnBrowserHandle = d.UnBrowserHandle,X = d.X,Y = d.Y,PchURL = d.PchURL,BInput = d.BInput,BLiveLink = d.BLiveLink, };
		}
		#endregion
	}
	
	public struct HTML_JSAlert_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMessage; // pchMessage const char *
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 14;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_JSAlert_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_JSAlert_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchMessage; // pchMessage const char *
			
			public static implicit operator HTML_JSAlert_t ( HTML_JSAlert_t.Pack4 d ) => new HTML_JSAlert_t{ UnBrowserHandle = d.UnBrowserHandle,PchMessage = d.PchMessage, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchMessage; // pchMessage const char *
			
			public static implicit operator HTML_JSAlert_t ( HTML_JSAlert_t.Pack8 d ) => new HTML_JSAlert_t{ UnBrowserHandle = d.UnBrowserHandle,PchMessage = d.PchMessage, };
		}
		#endregion
	}
	
	public struct HTML_JSConfirm_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMessage; // pchMessage const char *
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 15;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_JSConfirm_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_JSConfirm_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchMessage; // pchMessage const char *
			
			public static implicit operator HTML_JSConfirm_t ( HTML_JSConfirm_t.Pack4 d ) => new HTML_JSConfirm_t{ UnBrowserHandle = d.UnBrowserHandle,PchMessage = d.PchMessage, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchMessage; // pchMessage const char *
			
			public static implicit operator HTML_JSConfirm_t ( HTML_JSConfirm_t.Pack8 d ) => new HTML_JSConfirm_t{ UnBrowserHandle = d.UnBrowserHandle,PchMessage = d.PchMessage, };
		}
		#endregion
	}
	
	public struct HTML_FileOpenDialog_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchTitle; // pchTitle const char *
		internal string PchInitialFile; // pchInitialFile const char *
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 16;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_FileOpenDialog_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_FileOpenDialog_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchTitle; // pchTitle const char *
			internal string PchInitialFile; // pchInitialFile const char *
			
			public static implicit operator HTML_FileOpenDialog_t ( HTML_FileOpenDialog_t.Pack4 d ) => new HTML_FileOpenDialog_t{ UnBrowserHandle = d.UnBrowserHandle,PchTitle = d.PchTitle,PchInitialFile = d.PchInitialFile, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchTitle; // pchTitle const char *
			internal string PchInitialFile; // pchInitialFile const char *
			
			public static implicit operator HTML_FileOpenDialog_t ( HTML_FileOpenDialog_t.Pack8 d ) => new HTML_FileOpenDialog_t{ UnBrowserHandle = d.UnBrowserHandle,PchTitle = d.PchTitle,PchInitialFile = d.PchInitialFile, };
		}
		#endregion
	}
	
	public struct HTML_NewWindow_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		internal uint UnX; // unX uint32
		internal uint UnY; // unY uint32
		internal uint UnWide; // unWide uint32
		internal uint UnTall; // unTall uint32
		internal uint UnNewWindow_BrowserHandle_IGNORE; // unNewWindow_BrowserHandle_IGNORE HHTMLBrowser
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 21;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_NewWindow_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_NewWindow_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchURL; // pchURL const char *
			internal uint UnX; // unX uint32
			internal uint UnY; // unY uint32
			internal uint UnWide; // unWide uint32
			internal uint UnTall; // unTall uint32
			internal uint UnNewWindow_BrowserHandle_IGNORE; // unNewWindow_BrowserHandle_IGNORE HHTMLBrowser
			
			public static implicit operator HTML_NewWindow_t ( HTML_NewWindow_t.Pack4 d ) => new HTML_NewWindow_t{ UnBrowserHandle = d.UnBrowserHandle,PchURL = d.PchURL,UnX = d.UnX,UnY = d.UnY,UnWide = d.UnWide,UnTall = d.UnTall,UnNewWindow_BrowserHandle_IGNORE = d.UnNewWindow_BrowserHandle_IGNORE, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchURL; // pchURL const char *
			internal uint UnX; // unX uint32
			internal uint UnY; // unY uint32
			internal uint UnWide; // unWide uint32
			internal uint UnTall; // unTall uint32
			internal uint UnNewWindow_BrowserHandle_IGNORE; // unNewWindow_BrowserHandle_IGNORE HHTMLBrowser
			
			public static implicit operator HTML_NewWindow_t ( HTML_NewWindow_t.Pack8 d ) => new HTML_NewWindow_t{ UnBrowserHandle = d.UnBrowserHandle,PchURL = d.PchURL,UnX = d.UnX,UnY = d.UnY,UnWide = d.UnWide,UnTall = d.UnTall,UnNewWindow_BrowserHandle_IGNORE = d.UnNewWindow_BrowserHandle_IGNORE, };
		}
		#endregion
	}
	
	public struct HTML_SetCursor_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint EMouseCursor; // eMouseCursor uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 22;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_SetCursor_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_SetCursor_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal uint EMouseCursor; // eMouseCursor uint32
			
			public static implicit operator HTML_SetCursor_t ( HTML_SetCursor_t.Pack4 d ) => new HTML_SetCursor_t{ UnBrowserHandle = d.UnBrowserHandle,EMouseCursor = d.EMouseCursor, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal uint EMouseCursor; // eMouseCursor uint32
			
			public static implicit operator HTML_SetCursor_t ( HTML_SetCursor_t.Pack8 d ) => new HTML_SetCursor_t{ UnBrowserHandle = d.UnBrowserHandle,EMouseCursor = d.EMouseCursor, };
		}
		#endregion
	}
	
	public struct HTML_StatusText_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMsg; // pchMsg const char *
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 23;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_StatusText_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_StatusText_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchMsg; // pchMsg const char *
			
			public static implicit operator HTML_StatusText_t ( HTML_StatusText_t.Pack4 d ) => new HTML_StatusText_t{ UnBrowserHandle = d.UnBrowserHandle,PchMsg = d.PchMsg, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchMsg; // pchMsg const char *
			
			public static implicit operator HTML_StatusText_t ( HTML_StatusText_t.Pack8 d ) => new HTML_StatusText_t{ UnBrowserHandle = d.UnBrowserHandle,PchMsg = d.PchMsg, };
		}
		#endregion
	}
	
	public struct HTML_ShowToolTip_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMsg; // pchMsg const char *
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 24;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_ShowToolTip_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_ShowToolTip_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchMsg; // pchMsg const char *
			
			public static implicit operator HTML_ShowToolTip_t ( HTML_ShowToolTip_t.Pack4 d ) => new HTML_ShowToolTip_t{ UnBrowserHandle = d.UnBrowserHandle,PchMsg = d.PchMsg, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchMsg; // pchMsg const char *
			
			public static implicit operator HTML_ShowToolTip_t ( HTML_ShowToolTip_t.Pack8 d ) => new HTML_ShowToolTip_t{ UnBrowserHandle = d.UnBrowserHandle,PchMsg = d.PchMsg, };
		}
		#endregion
	}
	
	public struct HTML_UpdateToolTip_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMsg; // pchMsg const char *
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 25;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_UpdateToolTip_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_UpdateToolTip_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchMsg; // pchMsg const char *
			
			public static implicit operator HTML_UpdateToolTip_t ( HTML_UpdateToolTip_t.Pack4 d ) => new HTML_UpdateToolTip_t{ UnBrowserHandle = d.UnBrowserHandle,PchMsg = d.PchMsg, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchMsg; // pchMsg const char *
			
			public static implicit operator HTML_UpdateToolTip_t ( HTML_UpdateToolTip_t.Pack8 d ) => new HTML_UpdateToolTip_t{ UnBrowserHandle = d.UnBrowserHandle,PchMsg = d.PchMsg, };
		}
		#endregion
	}
	
	public struct HTML_HideToolTip_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 26;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_HideToolTip_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_HideToolTip_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			
			public static implicit operator HTML_HideToolTip_t ( HTML_HideToolTip_t.Pack4 d ) => new HTML_HideToolTip_t{ UnBrowserHandle = d.UnBrowserHandle, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			
			public static implicit operator HTML_HideToolTip_t ( HTML_HideToolTip_t.Pack8 d ) => new HTML_HideToolTip_t{ UnBrowserHandle = d.UnBrowserHandle, };
		}
		#endregion
	}
	
	public struct HTML_BrowserRestarted_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint UnOldBrowserHandle; // unOldBrowserHandle HHTMLBrowser
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 27;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((HTML_BrowserRestarted_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((HTML_BrowserRestarted_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal uint UnOldBrowserHandle; // unOldBrowserHandle HHTMLBrowser
			
			public static implicit operator HTML_BrowserRestarted_t ( HTML_BrowserRestarted_t.Pack4 d ) => new HTML_BrowserRestarted_t{ UnBrowserHandle = d.UnBrowserHandle,UnOldBrowserHandle = d.UnOldBrowserHandle, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal uint UnOldBrowserHandle; // unOldBrowserHandle HHTMLBrowser
			
			public static implicit operator HTML_BrowserRestarted_t ( HTML_BrowserRestarted_t.Pack8 d ) => new HTML_BrowserRestarted_t{ UnBrowserHandle = d.UnBrowserHandle,UnOldBrowserHandle = d.UnOldBrowserHandle, };
		}
		#endregion
	}
	
	public struct SteamItemDetails_t
	{
		internal ulong ItemId; // m_itemId SteamItemInstanceID_t
		internal int Definition; // m_iDefinition SteamItemDef_t
		internal ushort Quantity; // m_unQuantity uint16
		internal ushort Flags; // m_unFlags uint16
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public SteamItemDetails_t Fill( IntPtr p ) => Platform.PackSmall ? ((SteamItemDetails_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamItemDetails_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong ItemId; // m_itemId SteamItemInstanceID_t
			internal int Definition; // m_iDefinition SteamItemDef_t
			internal ushort Quantity; // m_unQuantity uint16
			internal ushort Flags; // m_unFlags uint16
			
			public static implicit operator SteamItemDetails_t ( SteamItemDetails_t.Pack4 d ) => new SteamItemDetails_t{ ItemId = d.ItemId,Definition = d.Definition,Quantity = d.Quantity,Flags = d.Flags, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong ItemId; // m_itemId SteamItemInstanceID_t
			internal int Definition; // m_iDefinition SteamItemDef_t
			internal ushort Quantity; // m_unQuantity uint16
			internal ushort Flags; // m_unFlags uint16
			
			public static implicit operator SteamItemDetails_t ( SteamItemDetails_t.Pack8 d ) => new SteamItemDetails_t{ ItemId = d.ItemId,Definition = d.Definition,Quantity = d.Quantity,Flags = d.Flags, };
		}
		#endregion
	}
	
	public struct SteamInventoryResultReady_t : Steamworks.ISteamCallback
	{
		internal int Handle; // m_handle SteamInventoryResult_t
		internal Result Result; // m_result enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientInventory + 0;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SteamInventoryResultReady_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamInventoryResultReady_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal int Handle; // m_handle SteamInventoryResult_t
			internal Result Result; // m_result enum EResult
			
			public static implicit operator SteamInventoryResultReady_t ( SteamInventoryResultReady_t.Pack4 d ) => new SteamInventoryResultReady_t{ Handle = d.Handle,Result = d.Result, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal int Handle; // m_handle SteamInventoryResult_t
			internal Result Result; // m_result enum EResult
			
			public static implicit operator SteamInventoryResultReady_t ( SteamInventoryResultReady_t.Pack8 d ) => new SteamInventoryResultReady_t{ Handle = d.Handle,Result = d.Result, };
		}
		#endregion
	}
	
	public struct SteamInventoryFullUpdate_t : Steamworks.ISteamCallback
	{
		internal int Handle; // m_handle SteamInventoryResult_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientInventory + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SteamInventoryFullUpdate_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamInventoryFullUpdate_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal int Handle; // m_handle SteamInventoryResult_t
			
			public static implicit operator SteamInventoryFullUpdate_t ( SteamInventoryFullUpdate_t.Pack4 d ) => new SteamInventoryFullUpdate_t{ Handle = d.Handle, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal int Handle; // m_handle SteamInventoryResult_t
			
			public static implicit operator SteamInventoryFullUpdate_t ( SteamInventoryFullUpdate_t.Pack8 d ) => new SteamInventoryFullUpdate_t{ Handle = d.Handle, };
		}
		#endregion
	}
	
	public struct SteamInventoryEligiblePromoItemDefIDs_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_result enum EResult
		internal ulong SteamID; // m_steamID class CSteamID
		internal int UmEligiblePromoItemDefs; // m_numEligiblePromoItemDefs int
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData; // m_bCachedData _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientInventory + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SteamInventoryEligiblePromoItemDefIDs_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamInventoryEligiblePromoItemDefIDs_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_result enum EResult
			internal ulong SteamID; // m_steamID class CSteamID
			internal int UmEligiblePromoItemDefs; // m_numEligiblePromoItemDefs int
			[MarshalAs(UnmanagedType.I1)]
			internal bool CachedData; // m_bCachedData _Bool
			
			public static implicit operator SteamInventoryEligiblePromoItemDefIDs_t ( SteamInventoryEligiblePromoItemDefIDs_t.Pack4 d ) => new SteamInventoryEligiblePromoItemDefIDs_t{ Result = d.Result,SteamID = d.SteamID,UmEligiblePromoItemDefs = d.UmEligiblePromoItemDefs,CachedData = d.CachedData, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal Result Result; // m_result enum EResult
			internal ulong SteamID; // m_steamID class CSteamID
			internal int UmEligiblePromoItemDefs; // m_numEligiblePromoItemDefs int
			[MarshalAs(UnmanagedType.I1)]
			internal bool CachedData; // m_bCachedData _Bool
			
			public static implicit operator SteamInventoryEligiblePromoItemDefIDs_t ( SteamInventoryEligiblePromoItemDefIDs_t.Pack8 d ) => new SteamInventoryEligiblePromoItemDefIDs_t{ Result = d.Result,SteamID = d.SteamID,UmEligiblePromoItemDefs = d.UmEligiblePromoItemDefs,CachedData = d.CachedData, };
		}
		#endregion
	}
	
	public struct SteamInventoryStartPurchaseResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_result enum EResult
		internal ulong OrderID; // m_ulOrderID uint64
		internal ulong TransID; // m_ulTransID uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientInventory + 4;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SteamInventoryStartPurchaseResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamInventoryStartPurchaseResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_result enum EResult
			internal ulong OrderID; // m_ulOrderID uint64
			internal ulong TransID; // m_ulTransID uint64
			
			public static implicit operator SteamInventoryStartPurchaseResult_t ( SteamInventoryStartPurchaseResult_t.Pack4 d ) => new SteamInventoryStartPurchaseResult_t{ Result = d.Result,OrderID = d.OrderID,TransID = d.TransID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_result enum EResult
			internal ulong OrderID; // m_ulOrderID uint64
			internal ulong TransID; // m_ulTransID uint64
			
			public static implicit operator SteamInventoryStartPurchaseResult_t ( SteamInventoryStartPurchaseResult_t.Pack8 d ) => new SteamInventoryStartPurchaseResult_t{ Result = d.Result,OrderID = d.OrderID,TransID = d.TransID, };
		}
		#endregion
	}
	
	public struct SteamInventoryRequestPricesResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_result enum EResult
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
		internal string Currency; // m_rgchCurrency char [4]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientInventory + 5;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SteamInventoryRequestPricesResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamInventoryRequestPricesResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_result enum EResult
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
			internal string Currency; // m_rgchCurrency char [4]
			
			public static implicit operator SteamInventoryRequestPricesResult_t ( SteamInventoryRequestPricesResult_t.Pack4 d ) => new SteamInventoryRequestPricesResult_t{ Result = d.Result,Currency = d.Currency, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_result enum EResult
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
			internal string Currency; // m_rgchCurrency char [4]
			
			public static implicit operator SteamInventoryRequestPricesResult_t ( SteamInventoryRequestPricesResult_t.Pack8 d ) => new SteamInventoryRequestPricesResult_t{ Result = d.Result,Currency = d.Currency, };
		}
		#endregion
	}
	
	public struct BroadcastUploadStop_t : Steamworks.ISteamCallback
	{
		internal BroadcastUploadResult Result; // m_eResult enum EBroadcastUploadResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientVideo + 5;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((BroadcastUploadStop_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((BroadcastUploadStop_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal BroadcastUploadResult Result; // m_eResult enum EBroadcastUploadResult
			
			public static implicit operator BroadcastUploadStop_t ( BroadcastUploadStop_t.Pack4 d ) => new BroadcastUploadStop_t{ Result = d.Result, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal BroadcastUploadResult Result; // m_eResult enum EBroadcastUploadResult
			
			public static implicit operator BroadcastUploadStop_t ( BroadcastUploadStop_t.Pack8 d ) => new BroadcastUploadStop_t{ Result = d.Result, };
		}
		#endregion
	}
	
	public struct GetVideoURLResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal uint VideoAppID; // m_unVideoAppID AppId_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string URL; // m_rgchURL char [256]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientVideo + 11;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GetVideoURLResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GetVideoURLResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal uint VideoAppID; // m_unVideoAppID AppId_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string URL; // m_rgchURL char [256]
			
			public static implicit operator GetVideoURLResult_t ( GetVideoURLResult_t.Pack4 d ) => new GetVideoURLResult_t{ Result = d.Result,VideoAppID = d.VideoAppID,URL = d.URL, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal uint VideoAppID; // m_unVideoAppID AppId_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string URL; // m_rgchURL char [256]
			
			public static implicit operator GetVideoURLResult_t ( GetVideoURLResult_t.Pack8 d ) => new GetVideoURLResult_t{ Result = d.Result,VideoAppID = d.VideoAppID,URL = d.URL, };
		}
		#endregion
	}
	
	public struct GetOPFSettingsResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal uint VideoAppID; // m_unVideoAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientVideo + 24;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GetOPFSettingsResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GetOPFSettingsResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal uint VideoAppID; // m_unVideoAppID AppId_t
			
			public static implicit operator GetOPFSettingsResult_t ( GetOPFSettingsResult_t.Pack4 d ) => new GetOPFSettingsResult_t{ Result = d.Result,VideoAppID = d.VideoAppID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal uint VideoAppID; // m_unVideoAppID AppId_t
			
			public static implicit operator GetOPFSettingsResult_t ( GetOPFSettingsResult_t.Pack8 d ) => new GetOPFSettingsResult_t{ Result = d.Result,VideoAppID = d.VideoAppID, };
		}
		#endregion
	}
	
	public struct GSClientApprove_t : Steamworks.ISteamCallback
	{
		internal ulong SteamID; // m_SteamID class CSteamID
		internal ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServer + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GSClientApprove_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GSClientApprove_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamID; // m_SteamID class CSteamID
			internal ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
			
			public static implicit operator GSClientApprove_t ( GSClientApprove_t.Pack4 d ) => new GSClientApprove_t{ SteamID = d.SteamID,OwnerSteamID = d.OwnerSteamID, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamID; // m_SteamID class CSteamID
			internal ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
			
			public static implicit operator GSClientApprove_t ( GSClientApprove_t.Pack8 d ) => new GSClientApprove_t{ SteamID = d.SteamID,OwnerSteamID = d.OwnerSteamID, };
		}
		#endregion
	}
	
	public struct GSClientDeny_t : Steamworks.ISteamCallback
	{
		internal ulong SteamID; // m_SteamID class CSteamID
		internal DenyReason DenyReason; // m_eDenyReason enum EDenyReason
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string OptionalText; // m_rgchOptionalText char [128]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServer + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GSClientDeny_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GSClientDeny_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamID; // m_SteamID class CSteamID
			internal DenyReason DenyReason; // m_eDenyReason enum EDenyReason
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string OptionalText; // m_rgchOptionalText char [128]
			
			public static implicit operator GSClientDeny_t ( GSClientDeny_t.Pack4 d ) => new GSClientDeny_t{ SteamID = d.SteamID,DenyReason = d.DenyReason,OptionalText = d.OptionalText, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamID; // m_SteamID class CSteamID
			internal DenyReason DenyReason; // m_eDenyReason enum EDenyReason
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string OptionalText; // m_rgchOptionalText char [128]
			
			public static implicit operator GSClientDeny_t ( GSClientDeny_t.Pack8 d ) => new GSClientDeny_t{ SteamID = d.SteamID,DenyReason = d.DenyReason,OptionalText = d.OptionalText, };
		}
		#endregion
	}
	
	public struct GSClientKick_t : Steamworks.ISteamCallback
	{
		internal ulong SteamID; // m_SteamID class CSteamID
		internal DenyReason DenyReason; // m_eDenyReason enum EDenyReason
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServer + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GSClientKick_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GSClientKick_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamID; // m_SteamID class CSteamID
			internal DenyReason DenyReason; // m_eDenyReason enum EDenyReason
			
			public static implicit operator GSClientKick_t ( GSClientKick_t.Pack4 d ) => new GSClientKick_t{ SteamID = d.SteamID,DenyReason = d.DenyReason, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamID; // m_SteamID class CSteamID
			internal DenyReason DenyReason; // m_eDenyReason enum EDenyReason
			
			public static implicit operator GSClientKick_t ( GSClientKick_t.Pack8 d ) => new GSClientKick_t{ SteamID = d.SteamID,DenyReason = d.DenyReason, };
		}
		#endregion
	}
	
	public struct GSClientAchievementStatus_t : Steamworks.ISteamCallback
	{
		internal ulong SteamID; // m_SteamID uint64
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string PchAchievement; // m_pchAchievement char [128]
		[MarshalAs(UnmanagedType.I1)]
		internal bool Unlocked; // m_bUnlocked _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServer + 6;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GSClientAchievementStatus_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GSClientAchievementStatus_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamID; // m_SteamID uint64
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string PchAchievement; // m_pchAchievement char [128]
			[MarshalAs(UnmanagedType.I1)]
			internal bool Unlocked; // m_bUnlocked _Bool
			
			public static implicit operator GSClientAchievementStatus_t ( GSClientAchievementStatus_t.Pack4 d ) => new GSClientAchievementStatus_t{ SteamID = d.SteamID,PchAchievement = d.PchAchievement,Unlocked = d.Unlocked, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong SteamID; // m_SteamID uint64
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string PchAchievement; // m_pchAchievement char [128]
			[MarshalAs(UnmanagedType.I1)]
			internal bool Unlocked; // m_bUnlocked _Bool
			
			public static implicit operator GSClientAchievementStatus_t ( GSClientAchievementStatus_t.Pack8 d ) => new GSClientAchievementStatus_t{ SteamID = d.SteamID,PchAchievement = d.PchAchievement,Unlocked = d.Unlocked, };
		}
		#endregion
	}
	
	public struct GSPolicyResponse_t : Steamworks.ISteamCallback
	{
		internal byte Secure; // m_bSecure uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 15;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GSPolicyResponse_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GSPolicyResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal byte Secure; // m_bSecure uint8
			
			public static implicit operator GSPolicyResponse_t ( GSPolicyResponse_t.Pack4 d ) => new GSPolicyResponse_t{ Secure = d.Secure, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal byte Secure; // m_bSecure uint8
			
			public static implicit operator GSPolicyResponse_t ( GSPolicyResponse_t.Pack8 d ) => new GSPolicyResponse_t{ Secure = d.Secure, };
		}
		#endregion
	}
	
	public struct GSGameplayStats_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal int Rank; // m_nRank int32
		internal uint TotalConnects; // m_unTotalConnects uint32
		internal uint TotalMinutesPlayed; // m_unTotalMinutesPlayed uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServer + 7;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GSGameplayStats_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GSGameplayStats_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal int Rank; // m_nRank int32
			internal uint TotalConnects; // m_unTotalConnects uint32
			internal uint TotalMinutesPlayed; // m_unTotalMinutesPlayed uint32
			
			public static implicit operator GSGameplayStats_t ( GSGameplayStats_t.Pack4 d ) => new GSGameplayStats_t{ Result = d.Result,Rank = d.Rank,TotalConnects = d.TotalConnects,TotalMinutesPlayed = d.TotalMinutesPlayed, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal int Rank; // m_nRank int32
			internal uint TotalConnects; // m_unTotalConnects uint32
			internal uint TotalMinutesPlayed; // m_unTotalMinutesPlayed uint32
			
			public static implicit operator GSGameplayStats_t ( GSGameplayStats_t.Pack8 d ) => new GSGameplayStats_t{ Result = d.Result,Rank = d.Rank,TotalConnects = d.TotalConnects,TotalMinutesPlayed = d.TotalMinutesPlayed, };
		}
		#endregion
	}
	
	public struct GSClientGroupStatus_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDUser; // m_SteamIDUser class CSteamID
		internal ulong SteamIDGroup; // m_SteamIDGroup class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool Member; // m_bMember _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool Officer; // m_bOfficer _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServer + 8;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GSClientGroupStatus_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GSClientGroupStatus_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDUser; // m_SteamIDUser class CSteamID
			internal ulong SteamIDGroup; // m_SteamIDGroup class CSteamID
			[MarshalAs(UnmanagedType.I1)]
			internal bool Member; // m_bMember _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool Officer; // m_bOfficer _Bool
			
			public static implicit operator GSClientGroupStatus_t ( GSClientGroupStatus_t.Pack4 d ) => new GSClientGroupStatus_t{ SteamIDUser = d.SteamIDUser,SteamIDGroup = d.SteamIDGroup,Member = d.Member,Officer = d.Officer, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDUser; // m_SteamIDUser class CSteamID
			internal ulong SteamIDGroup; // m_SteamIDGroup class CSteamID
			[MarshalAs(UnmanagedType.I1)]
			internal bool Member; // m_bMember _Bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool Officer; // m_bOfficer _Bool
			
			public static implicit operator GSClientGroupStatus_t ( GSClientGroupStatus_t.Pack8 d ) => new GSClientGroupStatus_t{ SteamIDUser = d.SteamIDUser,SteamIDGroup = d.SteamIDGroup,Member = d.Member,Officer = d.Officer, };
		}
		#endregion
	}
	
	public struct GSReputation_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal uint ReputationScore; // m_unReputationScore uint32
		[MarshalAs(UnmanagedType.I1)]
		internal bool Banned; // m_bBanned _Bool
		internal uint BannedIP; // m_unBannedIP uint32
		internal ushort BannedPort; // m_usBannedPort uint16
		internal ulong BannedGameID; // m_ulBannedGameID uint64
		internal uint BanExpires; // m_unBanExpires uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServer + 9;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GSReputation_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GSReputation_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal uint ReputationScore; // m_unReputationScore uint32
			[MarshalAs(UnmanagedType.I1)]
			internal bool Banned; // m_bBanned _Bool
			internal uint BannedIP; // m_unBannedIP uint32
			internal ushort BannedPort; // m_usBannedPort uint16
			internal ulong BannedGameID; // m_ulBannedGameID uint64
			internal uint BanExpires; // m_unBanExpires uint32
			
			public static implicit operator GSReputation_t ( GSReputation_t.Pack4 d ) => new GSReputation_t{ Result = d.Result,ReputationScore = d.ReputationScore,Banned = d.Banned,BannedIP = d.BannedIP,BannedPort = d.BannedPort,BannedGameID = d.BannedGameID,BanExpires = d.BanExpires, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal uint ReputationScore; // m_unReputationScore uint32
			[MarshalAs(UnmanagedType.I1)]
			internal bool Banned; // m_bBanned _Bool
			internal uint BannedIP; // m_unBannedIP uint32
			internal ushort BannedPort; // m_usBannedPort uint16
			internal ulong BannedGameID; // m_ulBannedGameID uint64
			internal uint BanExpires; // m_unBanExpires uint32
			
			public static implicit operator GSReputation_t ( GSReputation_t.Pack8 d ) => new GSReputation_t{ Result = d.Result,ReputationScore = d.ReputationScore,Banned = d.Banned,BannedIP = d.BannedIP,BannedPort = d.BannedPort,BannedGameID = d.BannedGameID,BanExpires = d.BanExpires, };
		}
		#endregion
	}
	
	public struct AssociateWithClanResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServer + 10;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((AssociateWithClanResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((AssociateWithClanResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator AssociateWithClanResult_t ( AssociateWithClanResult_t.Pack4 d ) => new AssociateWithClanResult_t{ Result = d.Result, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator AssociateWithClanResult_t ( AssociateWithClanResult_t.Pack8 d ) => new AssociateWithClanResult_t{ Result = d.Result, };
		}
		#endregion
	}
	
	public struct ComputeNewPlayerCompatibilityResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal int CPlayersThatDontLikeCandidate; // m_cPlayersThatDontLikeCandidate int
		internal int CPlayersThatCandidateDoesntLike; // m_cPlayersThatCandidateDoesntLike int
		internal int CClanPlayersThatDontLikeCandidate; // m_cClanPlayersThatDontLikeCandidate int
		internal ulong SteamIDCandidate; // m_SteamIDCandidate class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServer + 11;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((ComputeNewPlayerCompatibilityResult_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((ComputeNewPlayerCompatibilityResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal int CPlayersThatDontLikeCandidate; // m_cPlayersThatDontLikeCandidate int
			internal int CPlayersThatCandidateDoesntLike; // m_cPlayersThatCandidateDoesntLike int
			internal int CClanPlayersThatDontLikeCandidate; // m_cClanPlayersThatDontLikeCandidate int
			internal ulong SteamIDCandidate; // m_SteamIDCandidate class CSteamID
			
			public static implicit operator ComputeNewPlayerCompatibilityResult_t ( ComputeNewPlayerCompatibilityResult_t.Pack4 d ) => new ComputeNewPlayerCompatibilityResult_t{ Result = d.Result,CPlayersThatDontLikeCandidate = d.CPlayersThatDontLikeCandidate,CPlayersThatCandidateDoesntLike = d.CPlayersThatCandidateDoesntLike,CClanPlayersThatDontLikeCandidate = d.CClanPlayersThatDontLikeCandidate,SteamIDCandidate = d.SteamIDCandidate, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal int CPlayersThatDontLikeCandidate; // m_cPlayersThatDontLikeCandidate int
			internal int CPlayersThatCandidateDoesntLike; // m_cPlayersThatCandidateDoesntLike int
			internal int CClanPlayersThatDontLikeCandidate; // m_cClanPlayersThatDontLikeCandidate int
			internal ulong SteamIDCandidate; // m_SteamIDCandidate class CSteamID
			
			public static implicit operator ComputeNewPlayerCompatibilityResult_t ( ComputeNewPlayerCompatibilityResult_t.Pack8 d ) => new ComputeNewPlayerCompatibilityResult_t{ Result = d.Result,CPlayersThatDontLikeCandidate = d.CPlayersThatDontLikeCandidate,CPlayersThatCandidateDoesntLike = d.CPlayersThatCandidateDoesntLike,CClanPlayersThatDontLikeCandidate = d.CClanPlayersThatDontLikeCandidate,SteamIDCandidate = d.SteamIDCandidate, };
		}
		#endregion
	}
	
	public struct GSStatsReceived_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServerStats + 0;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GSStatsReceived_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GSStatsReceived_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator GSStatsReceived_t ( GSStatsReceived_t.Pack4 d ) => new GSStatsReceived_t{ Result = d.Result,SteamIDUser = d.SteamIDUser, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator GSStatsReceived_t ( GSStatsReceived_t.Pack8 d ) => new GSStatsReceived_t{ Result = d.Result,SteamIDUser = d.SteamIDUser, };
		}
		#endregion
	}
	
	public struct GSStatsStored_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServerStats + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GSStatsStored_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GSStatsStored_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator GSStatsStored_t ( GSStatsStored_t.Pack4 d ) => new GSStatsStored_t{ Result = d.Result,SteamIDUser = d.SteamIDUser, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator GSStatsStored_t ( GSStatsStored_t.Pack8 d ) => new GSStatsStored_t{ Result = d.Result,SteamIDUser = d.SteamIDUser, };
		}
		#endregion
	}
	
	public struct GSStatsUnloaded_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 8;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GSStatsUnloaded_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GSStatsUnloaded_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator GSStatsUnloaded_t ( GSStatsUnloaded_t.Pack4 d ) => new GSStatsUnloaded_t{ SteamIDUser = d.SteamIDUser, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator GSStatsUnloaded_t ( GSStatsUnloaded_t.Pack8 d ) => new GSStatsUnloaded_t{ SteamIDUser = d.SteamIDUser, };
		}
		#endregion
	}
	
	public struct NewUrlLaunchParameters_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamApps + 14;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((NewUrlLaunchParameters_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((NewUrlLaunchParameters_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			
			public static implicit operator NewUrlLaunchParameters_t ( NewUrlLaunchParameters_t.Pack4 d ) => new NewUrlLaunchParameters_t{  };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator NewUrlLaunchParameters_t ( NewUrlLaunchParameters_t.Pack8 d ) => new NewUrlLaunchParameters_t{  };
		}
		#endregion
	}
	
	public struct ItemInstalled_t : Steamworks.ISteamCallback
	{
		internal uint AppID; // m_unAppID AppId_t
		internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 5;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((ItemInstalled_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((ItemInstalled_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint AppID; // m_unAppID AppId_t
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator ItemInstalled_t ( ItemInstalled_t.Pack4 d ) => new ItemInstalled_t{ AppID = d.AppID,PublishedFileId = d.PublishedFileId, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint AppID; // m_unAppID AppId_t
			internal ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator ItemInstalled_t ( ItemInstalled_t.Pack8 d ) => new ItemInstalled_t{ AppID = d.AppID,PublishedFileId = d.PublishedFileId, };
		}
		#endregion
	}
	
	public struct InputAnalogActionData_t
	{
		internal InputSourceMode EMode; // eMode EInputSourceMode
		internal float X; // x float
		internal float Y; // y float
		[MarshalAs(UnmanagedType.I1)]
		internal bool BActive; // bActive bool
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public InputAnalogActionData_t Fill( IntPtr p ) => Platform.PackSmall ? ((InputAnalogActionData_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((InputAnalogActionData_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal InputSourceMode EMode; // eMode EInputSourceMode
			internal float X; // x float
			internal float Y; // y float
			[MarshalAs(UnmanagedType.I1)]
			internal bool BActive; // bActive bool
			
			public static implicit operator InputAnalogActionData_t ( InputAnalogActionData_t.Pack4 d ) => new InputAnalogActionData_t{ EMode = d.EMode,X = d.X,Y = d.Y,BActive = d.BActive, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal InputSourceMode EMode; // eMode EInputSourceMode
			internal float X; // x float
			internal float Y; // y float
			[MarshalAs(UnmanagedType.I1)]
			internal bool BActive; // bActive bool
			
			public static implicit operator InputAnalogActionData_t ( InputAnalogActionData_t.Pack8 d ) => new InputAnalogActionData_t{ EMode = d.EMode,X = d.X,Y = d.Y,BActive = d.BActive, };
		}
		#endregion
	}
	
	public struct InputMotionData_t
	{
		internal float RotQuatX; // rotQuatX float
		internal float RotQuatY; // rotQuatY float
		internal float RotQuatZ; // rotQuatZ float
		internal float RotQuatW; // rotQuatW float
		internal float PosAccelX; // posAccelX float
		internal float PosAccelY; // posAccelY float
		internal float PosAccelZ; // posAccelZ float
		internal float RotVelX; // rotVelX float
		internal float RotVelY; // rotVelY float
		internal float RotVelZ; // rotVelZ float
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public InputMotionData_t Fill( IntPtr p ) => Platform.PackSmall ? ((InputMotionData_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((InputMotionData_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal float RotQuatX; // rotQuatX float
			internal float RotQuatY; // rotQuatY float
			internal float RotQuatZ; // rotQuatZ float
			internal float RotQuatW; // rotQuatW float
			internal float PosAccelX; // posAccelX float
			internal float PosAccelY; // posAccelY float
			internal float PosAccelZ; // posAccelZ float
			internal float RotVelX; // rotVelX float
			internal float RotVelY; // rotVelY float
			internal float RotVelZ; // rotVelZ float
			
			public static implicit operator InputMotionData_t ( InputMotionData_t.Pack4 d ) => new InputMotionData_t{ RotQuatX = d.RotQuatX,RotQuatY = d.RotQuatY,RotQuatZ = d.RotQuatZ,RotQuatW = d.RotQuatW,PosAccelX = d.PosAccelX,PosAccelY = d.PosAccelY,PosAccelZ = d.PosAccelZ,RotVelX = d.RotVelX,RotVelY = d.RotVelY,RotVelZ = d.RotVelZ, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal float RotQuatX; // rotQuatX float
			internal float RotQuatY; // rotQuatY float
			internal float RotQuatZ; // rotQuatZ float
			internal float RotQuatW; // rotQuatW float
			internal float PosAccelX; // posAccelX float
			internal float PosAccelY; // posAccelY float
			internal float PosAccelZ; // posAccelZ float
			internal float RotVelX; // rotVelX float
			internal float RotVelY; // rotVelY float
			internal float RotVelZ; // rotVelZ float
			
			public static implicit operator InputMotionData_t ( InputMotionData_t.Pack8 d ) => new InputMotionData_t{ RotQuatX = d.RotQuatX,RotQuatY = d.RotQuatY,RotQuatZ = d.RotQuatZ,RotQuatW = d.RotQuatW,PosAccelX = d.PosAccelX,PosAccelY = d.PosAccelY,PosAccelZ = d.PosAccelZ,RotVelX = d.RotVelX,RotVelY = d.RotVelY,RotVelZ = d.RotVelZ, };
		}
		#endregion
	}
	
	public struct InputDigitalActionData_t
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool BState; // bState bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool BActive; // bActive bool
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public InputDigitalActionData_t Fill( IntPtr p ) => Platform.PackSmall ? ((InputDigitalActionData_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((InputDigitalActionData_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool BState; // bState bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool BActive; // bActive bool
			
			public static implicit operator InputDigitalActionData_t ( InputDigitalActionData_t.Pack4 d ) => new InputDigitalActionData_t{ BState = d.BState,BActive = d.BActive, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool BState; // bState bool
			[MarshalAs(UnmanagedType.I1)]
			internal bool BActive; // bActive bool
			
			public static implicit operator InputDigitalActionData_t ( InputDigitalActionData_t.Pack8 d ) => new InputDigitalActionData_t{ BState = d.BState,BActive = d.BActive, };
		}
		#endregion
	}
	
	public struct SteamInventoryDefinitionUpdate_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientInventory + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SteamInventoryDefinitionUpdate_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamInventoryDefinitionUpdate_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			
			public static implicit operator SteamInventoryDefinitionUpdate_t ( SteamInventoryDefinitionUpdate_t.Pack4 d ) => new SteamInventoryDefinitionUpdate_t{  };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator SteamInventoryDefinitionUpdate_t ( SteamInventoryDefinitionUpdate_t.Pack8 d ) => new SteamInventoryDefinitionUpdate_t{  };
		}
		#endregion
	}
	
	public struct SteamParentalSettingsChanged_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamParentalSettings + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SteamParentalSettingsChanged_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamParentalSettingsChanged_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			
			public static implicit operator SteamParentalSettingsChanged_t ( SteamParentalSettingsChanged_t.Pack4 d ) => new SteamParentalSettingsChanged_t{  };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator SteamParentalSettingsChanged_t ( SteamParentalSettingsChanged_t.Pack8 d ) => new SteamParentalSettingsChanged_t{  };
		}
		#endregion
	}
	
	public struct SteamServersConnected_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SteamServersConnected_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamServersConnected_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			
			public static implicit operator SteamServersConnected_t ( SteamServersConnected_t.Pack4 d ) => new SteamServersConnected_t{  };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator SteamServersConnected_t ( SteamServersConnected_t.Pack8 d ) => new SteamServersConnected_t{  };
		}
		#endregion
	}
	
	public struct NewLaunchQueryParameters_t
	{
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public NewLaunchQueryParameters_t Fill( IntPtr p ) => Platform.PackSmall ? ((NewLaunchQueryParameters_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((NewLaunchQueryParameters_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			
			public static implicit operator NewLaunchQueryParameters_t ( NewLaunchQueryParameters_t.Pack4 d ) => new NewLaunchQueryParameters_t{  };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator NewLaunchQueryParameters_t ( NewLaunchQueryParameters_t.Pack8 d ) => new NewLaunchQueryParameters_t{  };
		}
		#endregion
	}
	
	public struct GCMessageAvailable_t : Steamworks.ISteamCallback
	{
		internal uint MessageSize; // m_nMessageSize uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameCoordinator + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GCMessageAvailable_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GCMessageAvailable_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal uint MessageSize; // m_nMessageSize uint32
			
			public static implicit operator GCMessageAvailable_t ( GCMessageAvailable_t.Pack4 d ) => new GCMessageAvailable_t{ MessageSize = d.MessageSize, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint MessageSize; // m_nMessageSize uint32
			
			public static implicit operator GCMessageAvailable_t ( GCMessageAvailable_t.Pack8 d ) => new GCMessageAvailable_t{ MessageSize = d.MessageSize, };
		}
		#endregion
	}
	
	public struct GCMessageFailed_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameCoordinator + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((GCMessageFailed_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((GCMessageFailed_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			
			public static implicit operator GCMessageFailed_t ( GCMessageFailed_t.Pack4 d ) => new GCMessageFailed_t{  };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator GCMessageFailed_t ( GCMessageFailed_t.Pack8 d ) => new GCMessageFailed_t{  };
		}
		#endregion
	}
	
	public struct ScreenshotRequested_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamScreenshots + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((ScreenshotRequested_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((ScreenshotRequested_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			
			public static implicit operator ScreenshotRequested_t ( ScreenshotRequested_t.Pack4 d ) => new ScreenshotRequested_t{  };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator ScreenshotRequested_t ( ScreenshotRequested_t.Pack8 d ) => new ScreenshotRequested_t{  };
		}
		#endregion
	}
	
	public struct LicensesUpdated_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 25;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((LicensesUpdated_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((LicensesUpdated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			
			public static implicit operator LicensesUpdated_t ( LicensesUpdated_t.Pack4 d ) => new LicensesUpdated_t{  };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator LicensesUpdated_t ( LicensesUpdated_t.Pack8 d ) => new LicensesUpdated_t{  };
		}
		#endregion
	}
	
	public struct SteamShutdown_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUtils + 4;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((SteamShutdown_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((SteamShutdown_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			
			public static implicit operator SteamShutdown_t ( SteamShutdown_t.Pack4 d ) => new SteamShutdown_t{  };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator SteamShutdown_t ( SteamShutdown_t.Pack8 d ) => new SteamShutdown_t{  };
		}
		#endregion
	}
	
	public struct IPCountry_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUtils + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((IPCountry_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((IPCountry_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			
			public static implicit operator IPCountry_t ( IPCountry_t.Pack4 d ) => new IPCountry_t{  };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator IPCountry_t ( IPCountry_t.Pack8 d ) => new IPCountry_t{  };
		}
		#endregion
	}
	
	public struct IPCFailure_t : Steamworks.ISteamCallback
	{
		internal byte FailureType; // m_eFailureType uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 17;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Platform.PackSmall ? typeof(Pack4) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Platform.PackSmall ? ((IPCFailure_t)(Pack4) Marshal.PtrToStructure( p, typeof(Pack4) )) : ((IPCFailure_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack4
		{
			internal byte FailureType; // m_eFailureType uint8
			
			public static implicit operator IPCFailure_t ( IPCFailure_t.Pack4 d ) => new IPCFailure_t{ FailureType = d.FailureType, };
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal byte FailureType; // m_eFailureType uint8
			
			public static implicit operator IPCFailure_t ( IPCFailure_t.Pack8 d ) => new IPCFailure_t{ FailureType = d.FailureType, };
		}
		#endregion
	}
	
	internal static class Callbacks
	{
		internal static void RegisterCallbacks( Facepunch.Steamworks.BaseSteamworks steamworks )
		{
			new CallbackHandle<SteamServerConnectFailure_t>( steamworks );
			new CallbackHandle<SteamServersDisconnected_t>( steamworks );
			new CallbackHandle<ClientGameServerDeny_t>( steamworks );
			new CallbackHandle<ValidateAuthTicketResponse_t>( steamworks );
			new CallbackHandle<MicroTxnAuthorizationResponse_t>( steamworks );
			new CallbackHandle<EncryptedAppTicketResponse_t>( steamworks );
			new CallbackHandle<GetAuthSessionTicketResponse_t>( steamworks );
			new CallbackHandle<GameWebCallback_t>( steamworks );
			new CallbackHandle<StoreAuthURLResponse_t>( steamworks );
			new CallbackHandle<MarketEligibilityResponse_t>( steamworks );
			new CallbackHandle<PersonaStateChange_t>( steamworks );
			new CallbackHandle<GameOverlayActivated_t>( steamworks );
			new CallbackHandle<GameServerChangeRequested_t>( steamworks );
			new CallbackHandle<GameLobbyJoinRequested_t>( steamworks );
			new CallbackHandle<AvatarImageLoaded_t>( steamworks );
			new CallbackHandle<ClanOfficerListResponse_t>( steamworks );
			new CallbackHandle<FriendRichPresenceUpdate_t>( steamworks );
			new CallbackHandle<GameRichPresenceJoinRequested_t>( steamworks );
			new CallbackHandle<GameConnectedClanChatMsg_t>( steamworks );
			new CallbackHandle<GameConnectedChatJoin_t>( steamworks );
			new CallbackHandle<GameConnectedChatLeave_t>( steamworks );
			new CallbackHandle<DownloadClanActivityCountsResult_t>( steamworks );
			new CallbackHandle<JoinClanChatRoomCompletionResult_t>( steamworks );
			new CallbackHandle<GameConnectedFriendChatMsg_t>( steamworks );
			new CallbackHandle<FriendsGetFollowerCount_t>( steamworks );
			new CallbackHandle<FriendsIsFollowing_t>( steamworks );
			new CallbackHandle<FriendsEnumerateFollowingList_t>( steamworks );
			new CallbackHandle<SetPersonaNameResponse_t>( steamworks );
			new CallbackHandle<LowBatteryPower_t>( steamworks );
			new CallbackHandle<SteamAPICallCompleted_t>( steamworks );
			new CallbackHandle<CheckFileSignature_t>( steamworks );
			new CallbackHandle<GamepadTextInputDismissed_t>( steamworks );
			new CallbackHandle<FavoritesListChanged_t>( steamworks );
			new CallbackHandle<LobbyInvite_t>( steamworks );
			new CallbackHandle<LobbyEnter_t>( steamworks );
			new CallbackHandle<LobbyDataUpdate_t>( steamworks );
			new CallbackHandle<LobbyChatUpdate_t>( steamworks );
			new CallbackHandle<LobbyChatMsg_t>( steamworks );
			new CallbackHandle<LobbyGameCreated_t>( steamworks );
			new CallbackHandle<LobbyMatchList_t>( steamworks );
			new CallbackHandle<LobbyKicked_t>( steamworks );
			new CallbackHandle<LobbyCreated_t>( steamworks );
			new CallbackHandle<PSNGameBootInviteResult_t>( steamworks );
			new CallbackHandle<FavoritesListAccountsUpdated_t>( steamworks );
			new CallbackHandle<SearchForGameProgressCallback_t>( steamworks );
			new CallbackHandle<SearchForGameResultCallback_t>( steamworks );
			new CallbackHandle<RequestPlayersForGameProgressCallback_t>( steamworks );
			new CallbackHandle<RequestPlayersForGameResultCallback_t>( steamworks );
			new CallbackHandle<RequestPlayersForGameFinalResultCallback_t>( steamworks );
			new CallbackHandle<SubmitPlayerResultResultCallback_t>( steamworks );
			new CallbackHandle<EndGameResultCallback_t>( steamworks );
			new CallbackHandle<JoinPartyCallback_t>( steamworks );
			new CallbackHandle<CreateBeaconCallback_t>( steamworks );
			new CallbackHandle<ReservationNotificationCallback_t>( steamworks );
			new CallbackHandle<ChangeNumOpenSlotsCallback_t>( steamworks );
			new CallbackHandle<RemoteStorageAppSyncedClient_t>( steamworks );
			new CallbackHandle<RemoteStorageAppSyncedServer_t>( steamworks );
			new CallbackHandle<RemoteStorageAppSyncProgress_t>( steamworks );
			new CallbackHandle<RemoteStorageAppSyncStatusCheck_t>( steamworks );
			new CallbackHandle<RemoteStorageFileShareResult_t>( steamworks );
			new CallbackHandle<RemoteStoragePublishFileResult_t>( steamworks );
			new CallbackHandle<RemoteStorageDeletePublishedFileResult_t>( steamworks );
			new CallbackHandle<RemoteStorageEnumerateUserPublishedFilesResult_t>( steamworks );
			new CallbackHandle<RemoteStorageSubscribePublishedFileResult_t>( steamworks );
			new CallbackHandle<RemoteStorageEnumerateUserSubscribedFilesResult_t>( steamworks );
			new CallbackHandle<RemoteStorageUnsubscribePublishedFileResult_t>( steamworks );
			new CallbackHandle<RemoteStorageUpdatePublishedFileResult_t>( steamworks );
			new CallbackHandle<RemoteStorageDownloadUGCResult_t>( steamworks );
			new CallbackHandle<RemoteStorageGetPublishedFileDetailsResult_t>( steamworks );
			new CallbackHandle<RemoteStorageEnumerateWorkshopFilesResult_t>( steamworks );
			new CallbackHandle<RemoteStorageGetPublishedItemVoteDetailsResult_t>( steamworks );
			new CallbackHandle<RemoteStoragePublishedFileSubscribed_t>( steamworks );
			new CallbackHandle<RemoteStoragePublishedFileUnsubscribed_t>( steamworks );
			new CallbackHandle<RemoteStoragePublishedFileDeleted_t>( steamworks );
			new CallbackHandle<RemoteStorageUpdateUserPublishedItemVoteResult_t>( steamworks );
			new CallbackHandle<RemoteStorageUserVoteDetails_t>( steamworks );
			new CallbackHandle<RemoteStorageEnumerateUserSharedWorkshopFilesResult_t>( steamworks );
			new CallbackHandle<RemoteStorageSetUserPublishedFileActionResult_t>( steamworks );
			new CallbackHandle<RemoteStorageEnumeratePublishedFilesByUserActionResult_t>( steamworks );
			new CallbackHandle<RemoteStoragePublishFileProgress_t>( steamworks );
			new CallbackHandle<RemoteStoragePublishedFileUpdated_t>( steamworks );
			new CallbackHandle<RemoteStorageFileWriteAsyncComplete_t>( steamworks );
			new CallbackHandle<RemoteStorageFileReadAsyncComplete_t>( steamworks );
			new CallbackHandle<UserStatsReceived_t>( steamworks );
			new CallbackHandle<UserStatsStored_t>( steamworks );
			new CallbackHandle<UserAchievementStored_t>( steamworks );
			new CallbackHandle<LeaderboardFindResult_t>( steamworks );
			new CallbackHandle<LeaderboardScoresDownloaded_t>( steamworks );
			new CallbackHandle<LeaderboardScoreUploaded_t>( steamworks );
			new CallbackHandle<NumberOfCurrentPlayers_t>( steamworks );
			new CallbackHandle<UserStatsUnloaded_t>( steamworks );
			new CallbackHandle<UserAchievementIconFetched_t>( steamworks );
			new CallbackHandle<GlobalAchievementPercentagesReady_t>( steamworks );
			new CallbackHandle<LeaderboardUGCSet_t>( steamworks );
			new CallbackHandle<PS3TrophiesInstalled_t>( steamworks );
			new CallbackHandle<GlobalStatsReceived_t>( steamworks );
			new CallbackHandle<DlcInstalled_t>( steamworks );
			new CallbackHandle<RegisterActivationCodeResponse_t>( steamworks );
			new CallbackHandle<AppProofOfPurchaseKeyResponse_t>( steamworks );
			new CallbackHandle<FileDetailsResult_t>( steamworks );
			new CallbackHandle<P2PSessionRequest_t>( steamworks );
			new CallbackHandle<P2PSessionConnectFail_t>( steamworks );
			new CallbackHandle<SocketStatusCallback_t>( steamworks );
			new CallbackHandle<ScreenshotReady_t>( steamworks );
			new CallbackHandle<VolumeHasChanged_t>( steamworks );
			new CallbackHandle<MusicPlayerWantsShuffled_t>( steamworks );
			new CallbackHandle<MusicPlayerWantsLooped_t>( steamworks );
			new CallbackHandle<MusicPlayerWantsVolume_t>( steamworks );
			new CallbackHandle<MusicPlayerSelectsQueueEntry_t>( steamworks );
			new CallbackHandle<MusicPlayerSelectsPlaylistEntry_t>( steamworks );
			new CallbackHandle<MusicPlayerWantsPlayingRepeatStatus_t>( steamworks );
			new CallbackHandle<HTTPRequestCompleted_t>( steamworks );
			new CallbackHandle<HTTPRequestHeadersReceived_t>( steamworks );
			new CallbackHandle<HTTPRequestDataReceived_t>( steamworks );
			new CallbackHandle<SteamUGCQueryCompleted_t>( steamworks );
			new CallbackHandle<SteamUGCRequestUGCDetailsResult_t>( steamworks );
			new CallbackHandle<CreateItemResult_t>( steamworks );
			new CallbackHandle<SubmitItemUpdateResult_t>( steamworks );
			new CallbackHandle<DownloadItemResult_t>( steamworks );
			new CallbackHandle<UserFavoriteItemsListChanged_t>( steamworks );
			new CallbackHandle<SetUserItemVoteResult_t>( steamworks );
			new CallbackHandle<GetUserItemVoteResult_t>( steamworks );
			new CallbackHandle<StartPlaytimeTrackingResult_t>( steamworks );
			new CallbackHandle<StopPlaytimeTrackingResult_t>( steamworks );
			new CallbackHandle<AddUGCDependencyResult_t>( steamworks );
			new CallbackHandle<RemoveUGCDependencyResult_t>( steamworks );
			new CallbackHandle<AddAppDependencyResult_t>( steamworks );
			new CallbackHandle<RemoveAppDependencyResult_t>( steamworks );
			new CallbackHandle<GetAppDependenciesResult_t>( steamworks );
			new CallbackHandle<DeleteItemResult_t>( steamworks );
			new CallbackHandle<SteamAppInstalled_t>( steamworks );
			new CallbackHandle<SteamAppUninstalled_t>( steamworks );
			new CallbackHandle<HTML_BrowserReady_t>( steamworks );
			new CallbackHandle<HTML_URLChanged_t>( steamworks );
			new CallbackHandle<HTML_FinishedRequest_t>( steamworks );
			new CallbackHandle<HTML_OpenLinkInNewTab_t>( steamworks );
			new CallbackHandle<HTML_ChangedTitle_t>( steamworks );
			new CallbackHandle<HTML_SearchResults_t>( steamworks );
			new CallbackHandle<HTML_CanGoBackAndForward_t>( steamworks );
			new CallbackHandle<HTML_HorizontalScroll_t>( steamworks );
			new CallbackHandle<HTML_VerticalScroll_t>( steamworks );
			new CallbackHandle<HTML_LinkAtPosition_t>( steamworks );
			new CallbackHandle<HTML_JSAlert_t>( steamworks );
			new CallbackHandle<HTML_JSConfirm_t>( steamworks );
			new CallbackHandle<HTML_FileOpenDialog_t>( steamworks );
			new CallbackHandle<HTML_NewWindow_t>( steamworks );
			new CallbackHandle<HTML_SetCursor_t>( steamworks );
			new CallbackHandle<HTML_StatusText_t>( steamworks );
			new CallbackHandle<HTML_ShowToolTip_t>( steamworks );
			new CallbackHandle<HTML_UpdateToolTip_t>( steamworks );
			new CallbackHandle<HTML_HideToolTip_t>( steamworks );
			new CallbackHandle<HTML_BrowserRestarted_t>( steamworks );
			new CallbackHandle<SteamInventoryResultReady_t>( steamworks );
			new CallbackHandle<SteamInventoryFullUpdate_t>( steamworks );
			new CallbackHandle<SteamInventoryEligiblePromoItemDefIDs_t>( steamworks );
			new CallbackHandle<SteamInventoryStartPurchaseResult_t>( steamworks );
			new CallbackHandle<SteamInventoryRequestPricesResult_t>( steamworks );
			new CallbackHandle<BroadcastUploadStop_t>( steamworks );
			new CallbackHandle<GetVideoURLResult_t>( steamworks );
			new CallbackHandle<GetOPFSettingsResult_t>( steamworks );
			new CallbackHandle<GSClientApprove_t>( steamworks );
			new CallbackHandle<GSClientDeny_t>( steamworks );
			new CallbackHandle<GSClientKick_t>( steamworks );
			new CallbackHandle<GSClientAchievementStatus_t>( steamworks );
			new CallbackHandle<GSPolicyResponse_t>( steamworks );
			new CallbackHandle<GSGameplayStats_t>( steamworks );
			new CallbackHandle<GSClientGroupStatus_t>( steamworks );
			new CallbackHandle<GSReputation_t>( steamworks );
			new CallbackHandle<AssociateWithClanResult_t>( steamworks );
			new CallbackHandle<ComputeNewPlayerCompatibilityResult_t>( steamworks );
			new CallbackHandle<GSStatsReceived_t>( steamworks );
			new CallbackHandle<GSStatsStored_t>( steamworks );
			new CallbackHandle<GSStatsUnloaded_t>( steamworks );
			new CallbackHandle<NewUrlLaunchParameters_t>( steamworks );
			new CallbackHandle<ItemInstalled_t>( steamworks );
			new CallbackHandle<SteamInventoryDefinitionUpdate_t>( steamworks );
			new CallbackHandle<SteamParentalSettingsChanged_t>( steamworks );
			new CallbackHandle<SteamServersConnected_t>( steamworks );
			new CallbackHandle<GCMessageAvailable_t>( steamworks );
			new CallbackHandle<GCMessageFailed_t>( steamworks );
			new CallbackHandle<ScreenshotRequested_t>( steamworks );
			new CallbackHandle<LicensesUpdated_t>( steamworks );
			new CallbackHandle<SteamShutdown_t>( steamworks );
			new CallbackHandle<IPCountry_t>( steamworks );
			new CallbackHandle<IPCFailure_t>( steamworks );
		}
	}
}
