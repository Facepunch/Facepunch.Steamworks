using System;
using System.Runtime.InteropServices;
using System.Linq;
using Steamworks.Data;

namespace Steamworks.Data
{
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct CallbackMsg_t
	{
		internal int SteamUser; // m_hSteamUser HSteamUser
		internal int Callback; // m_iCallback int
		internal IntPtr ParamPtr; // m_pubParam uint8 *
		internal int ParamCount; // m_cubParam int
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(CallbackMsg_t) : typeof(Pack8) );
		public CallbackMsg_t Fill( IntPtr p ) => Config.PackSmall ? ((CallbackMsg_t)(CallbackMsg_t) Marshal.PtrToStructure( p, typeof(CallbackMsg_t) )) : ((CallbackMsg_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamServerConnectFailure_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool StillRetrying; // m_bStillRetrying _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamServerConnectFailure_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SteamServerConnectFailure_t)(SteamServerConnectFailure_t) Marshal.PtrToStructure( p, typeof(SteamServerConnectFailure_t) )) : ((SteamServerConnectFailure_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamServersDisconnected_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamServersDisconnected_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SteamServersDisconnected_t)(SteamServersDisconnected_t) Marshal.PtrToStructure( p, typeof(SteamServersDisconnected_t) )) : ((SteamServersDisconnected_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator SteamServersDisconnected_t ( SteamServersDisconnected_t.Pack8 d ) => new SteamServersDisconnected_t{ Result = d.Result, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct ClientGameServerDeny_t : Steamworks.ISteamCallback
	{
		internal uint AppID; // m_uAppID uint32
		internal uint GameServerIP; // m_unGameServerIP uint32
		internal ushort GameServerPort; // m_usGameServerPort uint16
		internal ushort Secure; // m_bSecure uint16
		internal uint Reason; // m_uReason uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 13;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(ClientGameServerDeny_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((ClientGameServerDeny_t)(ClientGameServerDeny_t) Marshal.PtrToStructure( p, typeof(ClientGameServerDeny_t) )) : ((ClientGameServerDeny_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct ValidateAuthTicketResponse_t : Steamworks.ISteamCallback
	{
		internal ulong SteamID; // m_SteamID class CSteamID
		internal AuthResponse AuthSessionResponse; // m_eAuthSessionResponse enum EAuthSessionResponse
		internal ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 43;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(ValidateAuthTicketResponse_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((ValidateAuthTicketResponse_t)(ValidateAuthTicketResponse_t) Marshal.PtrToStructure( p, typeof(ValidateAuthTicketResponse_t) )) : ((ValidateAuthTicketResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamID; // m_SteamID class CSteamID
			internal AuthResponse AuthSessionResponse; // m_eAuthSessionResponse enum EAuthSessionResponse
			internal ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
			
			public static implicit operator ValidateAuthTicketResponse_t ( ValidateAuthTicketResponse_t.Pack8 d ) => new ValidateAuthTicketResponse_t{ SteamID = d.SteamID,AuthSessionResponse = d.AuthSessionResponse,OwnerSteamID = d.OwnerSteamID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct MicroTxnAuthorizationResponse_t : Steamworks.ISteamCallback
	{
		internal uint AppID; // m_unAppID uint32
		internal ulong OrderID; // m_ulOrderID uint64
		internal byte Authorized; // m_bAuthorized uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 52;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(MicroTxnAuthorizationResponse_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((MicroTxnAuthorizationResponse_t)(MicroTxnAuthorizationResponse_t) Marshal.PtrToStructure( p, typeof(MicroTxnAuthorizationResponse_t) )) : ((MicroTxnAuthorizationResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct EncryptedAppTicketResponse_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 54;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(EncryptedAppTicketResponse_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((EncryptedAppTicketResponse_t)(EncryptedAppTicketResponse_t) Marshal.PtrToStructure( p, typeof(EncryptedAppTicketResponse_t) )) : ((EncryptedAppTicketResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator EncryptedAppTicketResponse_t ( EncryptedAppTicketResponse_t.Pack8 d ) => new EncryptedAppTicketResponse_t{ Result = d.Result, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GetAuthSessionTicketResponse_t : Steamworks.ISteamCallback
	{
		internal uint AuthTicket; // m_hAuthTicket HAuthTicket
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 63;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GetAuthSessionTicketResponse_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GetAuthSessionTicketResponse_t)(GetAuthSessionTicketResponse_t) Marshal.PtrToStructure( p, typeof(GetAuthSessionTicketResponse_t) )) : ((GetAuthSessionTicketResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint AuthTicket; // m_hAuthTicket HAuthTicket
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator GetAuthSessionTicketResponse_t ( GetAuthSessionTicketResponse_t.Pack8 d ) => new GetAuthSessionTicketResponse_t{ AuthTicket = d.AuthTicket,Result = d.Result, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameWebCallback_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string URL; // m_szURL char [256]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 64;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GameWebCallback_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GameWebCallback_t)(GameWebCallback_t) Marshal.PtrToStructure( p, typeof(GameWebCallback_t) )) : ((GameWebCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string URL; // m_szURL char [256]
			
			public static implicit operator GameWebCallback_t ( GameWebCallback_t.Pack8 d ) => new GameWebCallback_t{ URL = d.URL, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct StoreAuthURLResponse_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
		internal string URL; // m_szURL char [512]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 65;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(StoreAuthURLResponse_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((StoreAuthURLResponse_t)(StoreAuthURLResponse_t) Marshal.PtrToStructure( p, typeof(StoreAuthURLResponse_t) )) : ((StoreAuthURLResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
			internal string URL; // m_szURL char [512]
			
			public static implicit operator StoreAuthURLResponse_t ( StoreAuthURLResponse_t.Pack8 d ) => new StoreAuthURLResponse_t{ URL = d.URL, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct MarketEligibilityResponse_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Allowed; // m_bAllowed _Bool
		internal MarketNotAllowedReasonFlags NotAllowedReason; // m_eNotAllowedReason enum EMarketNotAllowedReasonFlags
		internal uint TAllowedAtTime; // m_rtAllowedAtTime RTime32
		internal int CdaySteamGuardRequiredDays; // m_cdaySteamGuardRequiredDays int
		internal int CdayNewDeviceCooldown; // m_cdayNewDeviceCooldown int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 66;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(MarketEligibilityResponse_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((MarketEligibilityResponse_t)(MarketEligibilityResponse_t) Marshal.PtrToStructure( p, typeof(MarketEligibilityResponse_t) )) : ((MarketEligibilityResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FriendGameInfo_t
	{
		internal GameId GameID; // m_gameID class CGameID
		internal uint GameIP; // m_unGameIP uint32
		internal ushort GamePort; // m_usGamePort uint16
		internal ushort QueryPort; // m_usQueryPort uint16
		internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(FriendGameInfo_t) : typeof(Pack8) );
		public FriendGameInfo_t Fill( IntPtr p ) => Config.PackSmall ? ((FriendGameInfo_t)(FriendGameInfo_t) Marshal.PtrToStructure( p, typeof(FriendGameInfo_t) )) : ((FriendGameInfo_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal GameId GameID; // m_gameID class CGameID
			internal uint GameIP; // m_unGameIP uint32
			internal ushort GamePort; // m_usGamePort uint16
			internal ushort QueryPort; // m_usQueryPort uint16
			internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
			
			public static implicit operator FriendGameInfo_t ( FriendGameInfo_t.Pack8 d ) => new FriendGameInfo_t{ GameID = d.GameID,GameIP = d.GameIP,GamePort = d.GamePort,QueryPort = d.QueryPort,SteamIDLobby = d.SteamIDLobby, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FriendSessionStateInfo_t
	{
		internal uint IOnlineSessionInstances; // m_uiOnlineSessionInstances uint32
		internal byte IPublishedToFriendsSessionInstance; // m_uiPublishedToFriendsSessionInstance uint8
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(FriendSessionStateInfo_t) : typeof(Pack8) );
		public FriendSessionStateInfo_t Fill( IntPtr p ) => Config.PackSmall ? ((FriendSessionStateInfo_t)(FriendSessionStateInfo_t) Marshal.PtrToStructure( p, typeof(FriendSessionStateInfo_t) )) : ((FriendSessionStateInfo_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint IOnlineSessionInstances; // m_uiOnlineSessionInstances uint32
			internal byte IPublishedToFriendsSessionInstance; // m_uiPublishedToFriendsSessionInstance uint8
			
			public static implicit operator FriendSessionStateInfo_t ( FriendSessionStateInfo_t.Pack8 d ) => new FriendSessionStateInfo_t{ IOnlineSessionInstances = d.IOnlineSessionInstances,IPublishedToFriendsSessionInstance = d.IPublishedToFriendsSessionInstance, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FriendStateChange_t : Steamworks.ISteamCallback
	{
		internal ulong SteamID; // m_ulSteamID uint64
		internal int ChangeFlags; // m_nChangeFlags int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 4;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(FriendStateChange_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((FriendStateChange_t)(FriendStateChange_t) Marshal.PtrToStructure( p, typeof(FriendStateChange_t) )) : ((FriendStateChange_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong SteamID; // m_ulSteamID uint64
			internal int ChangeFlags; // m_nChangeFlags int
			
			public static implicit operator FriendStateChange_t ( FriendStateChange_t.Pack8 d ) => new FriendStateChange_t{ SteamID = d.SteamID,ChangeFlags = d.ChangeFlags, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameOverlayActivated_t : Steamworks.ISteamCallback
	{
		internal byte Active; // m_bActive uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 31;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GameOverlayActivated_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GameOverlayActivated_t)(GameOverlayActivated_t) Marshal.PtrToStructure( p, typeof(GameOverlayActivated_t) )) : ((GameOverlayActivated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal byte Active; // m_bActive uint8
			
			public static implicit operator GameOverlayActivated_t ( GameOverlayActivated_t.Pack8 d ) => new GameOverlayActivated_t{ Active = d.Active, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameServerChangeRequested_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		internal string Server; // m_rgchServer char [64]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		internal string Password; // m_rgchPassword char [64]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 32;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GameServerChangeRequested_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GameServerChangeRequested_t)(GameServerChangeRequested_t) Marshal.PtrToStructure( p, typeof(GameServerChangeRequested_t) )) : ((GameServerChangeRequested_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameLobbyJoinRequested_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
		internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 33;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GameLobbyJoinRequested_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GameLobbyJoinRequested_t)(GameLobbyJoinRequested_t) Marshal.PtrToStructure( p, typeof(GameLobbyJoinRequested_t) )) : ((GameLobbyJoinRequested_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
			internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
			
			public static implicit operator GameLobbyJoinRequested_t ( GameLobbyJoinRequested_t.Pack8 d ) => new GameLobbyJoinRequested_t{ SteamIDLobby = d.SteamIDLobby,SteamIDFriend = d.SteamIDFriend, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct AvatarImageLoaded_t : Steamworks.ISteamCallback
	{
		internal ulong SteamID; // m_steamID class CSteamID
		internal int Image; // m_iImage int
		internal int Wide; // m_iWide int
		internal int Tall; // m_iTall int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 34;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(AvatarImageLoaded_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((AvatarImageLoaded_t)(AvatarImageLoaded_t) Marshal.PtrToStructure( p, typeof(AvatarImageLoaded_t) )) : ((AvatarImageLoaded_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct ClanOfficerListResponse_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDClan; // m_steamIDClan class CSteamID
		internal int COfficers; // m_cOfficers int
		internal byte Success; // m_bSuccess uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 35;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(ClanOfficerListResponse_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((ClanOfficerListResponse_t)(ClanOfficerListResponse_t) Marshal.PtrToStructure( p, typeof(ClanOfficerListResponse_t) )) : ((ClanOfficerListResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FriendRichPresenceUpdate_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		internal AppId AppID; // m_nAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 36;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(FriendRichPresenceUpdate_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((FriendRichPresenceUpdate_t)(FriendRichPresenceUpdate_t) Marshal.PtrToStructure( p, typeof(FriendRichPresenceUpdate_t) )) : ((FriendRichPresenceUpdate_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
			internal AppId AppID; // m_nAppID AppId_t
			
			public static implicit operator FriendRichPresenceUpdate_t ( FriendRichPresenceUpdate_t.Pack8 d ) => new FriendRichPresenceUpdate_t{ SteamIDFriend = d.SteamIDFriend,AppID = d.AppID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameRichPresenceJoinRequested_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string Connect; // m_rgchConnect char [256]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 37;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GameRichPresenceJoinRequested_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GameRichPresenceJoinRequested_t)(GameRichPresenceJoinRequested_t) Marshal.PtrToStructure( p, typeof(GameRichPresenceJoinRequested_t) )) : ((GameRichPresenceJoinRequested_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameConnectedClanChatMsg_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		internal int MessageID; // m_iMessageID int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 38;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GameConnectedClanChatMsg_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GameConnectedClanChatMsg_t)(GameConnectedClanChatMsg_t) Marshal.PtrToStructure( p, typeof(GameConnectedClanChatMsg_t) )) : ((GameConnectedClanChatMsg_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameConnectedChatJoin_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 39;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GameConnectedChatJoin_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GameConnectedChatJoin_t)(GameConnectedChatJoin_t) Marshal.PtrToStructure( p, typeof(GameConnectedChatJoin_t) )) : ((GameConnectedChatJoin_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator GameConnectedChatJoin_t ( GameConnectedChatJoin_t.Pack8 d ) => new GameConnectedChatJoin_t{ SteamIDClanChat = d.SteamIDClanChat,SteamIDUser = d.SteamIDUser, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameConnectedChatLeave_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool Kicked; // m_bKicked _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool Dropped; // m_bDropped _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 40;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GameConnectedChatLeave_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GameConnectedChatLeave_t)(GameConnectedChatLeave_t) Marshal.PtrToStructure( p, typeof(GameConnectedChatLeave_t) )) : ((GameConnectedChatLeave_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct DownloadClanActivityCountsResult_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Success; // m_bSuccess _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 41;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(DownloadClanActivityCountsResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((DownloadClanActivityCountsResult_t)(DownloadClanActivityCountsResult_t) Marshal.PtrToStructure( p, typeof(DownloadClanActivityCountsResult_t) )) : ((DownloadClanActivityCountsResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool Success; // m_bSuccess _Bool
			
			public static implicit operator DownloadClanActivityCountsResult_t ( DownloadClanActivityCountsResult_t.Pack8 d ) => new DownloadClanActivityCountsResult_t{ Success = d.Success, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct JoinClanChatRoomCompletionResult_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		internal ChatRoomEnterResponse ChatRoomEnterResponse; // m_eChatRoomEnterResponse enum EChatRoomEnterResponse
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 42;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(JoinClanChatRoomCompletionResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((JoinClanChatRoomCompletionResult_t)(JoinClanChatRoomCompletionResult_t) Marshal.PtrToStructure( p, typeof(JoinClanChatRoomCompletionResult_t) )) : ((JoinClanChatRoomCompletionResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
			internal ChatRoomEnterResponse ChatRoomEnterResponse; // m_eChatRoomEnterResponse enum EChatRoomEnterResponse
			
			public static implicit operator JoinClanChatRoomCompletionResult_t ( JoinClanChatRoomCompletionResult_t.Pack8 d ) => new JoinClanChatRoomCompletionResult_t{ SteamIDClanChat = d.SteamIDClanChat,ChatRoomEnterResponse = d.ChatRoomEnterResponse, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameConnectedFriendChatMsg_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		internal int MessageID; // m_iMessageID int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 43;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GameConnectedFriendChatMsg_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GameConnectedFriendChatMsg_t)(GameConnectedFriendChatMsg_t) Marshal.PtrToStructure( p, typeof(GameConnectedFriendChatMsg_t) )) : ((GameConnectedFriendChatMsg_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			internal int MessageID; // m_iMessageID int
			
			public static implicit operator GameConnectedFriendChatMsg_t ( GameConnectedFriendChatMsg_t.Pack8 d ) => new GameConnectedFriendChatMsg_t{ SteamIDUser = d.SteamIDUser,MessageID = d.MessageID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FriendsGetFollowerCount_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamID; // m_steamID class CSteamID
		internal int Count; // m_nCount int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 44;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(FriendsGetFollowerCount_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((FriendsGetFollowerCount_t)(FriendsGetFollowerCount_t) Marshal.PtrToStructure( p, typeof(FriendsGetFollowerCount_t) )) : ((FriendsGetFollowerCount_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FriendsIsFollowing_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamID; // m_steamID class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool IsFollowing; // m_bIsFollowing _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 45;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(FriendsIsFollowing_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((FriendsIsFollowing_t)(FriendsIsFollowing_t) Marshal.PtrToStructure( p, typeof(FriendsIsFollowing_t) )) : ((FriendsIsFollowing_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FriendsEnumerateFollowingList_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal ulong[] GSteamID; // m_rgSteamID class CSteamID [50]
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 46;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(FriendsEnumerateFollowingList_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((FriendsEnumerateFollowingList_t)(FriendsEnumerateFollowingList_t) Marshal.PtrToStructure( p, typeof(FriendsEnumerateFollowingList_t) )) : ((FriendsEnumerateFollowingList_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SetPersonaNameResponse_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Success; // m_bSuccess _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool LocalSuccess; // m_bLocalSuccess _Bool
		internal Result Result; // m_result enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamFriends + 47;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SetPersonaNameResponse_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SetPersonaNameResponse_t)(SetPersonaNameResponse_t) Marshal.PtrToStructure( p, typeof(SetPersonaNameResponse_t) )) : ((SetPersonaNameResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct LowBatteryPower_t : Steamworks.ISteamCallback
	{
		internal byte MinutesBatteryLeft; // m_nMinutesBatteryLeft uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUtils + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LowBatteryPower_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((LowBatteryPower_t)(LowBatteryPower_t) Marshal.PtrToStructure( p, typeof(LowBatteryPower_t) )) : ((LowBatteryPower_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal byte MinutesBatteryLeft; // m_nMinutesBatteryLeft uint8
			
			public static implicit operator LowBatteryPower_t ( LowBatteryPower_t.Pack8 d ) => new LowBatteryPower_t{ MinutesBatteryLeft = d.MinutesBatteryLeft, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamAPICallCompleted_t : Steamworks.ISteamCallback
	{
		internal ulong AsyncCall; // m_hAsyncCall SteamAPICall_t
		internal int Callback; // m_iCallback int
		internal uint ParamCount; // m_cubParam uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUtils + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamAPICallCompleted_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SteamAPICallCompleted_t)(SteamAPICallCompleted_t) Marshal.PtrToStructure( p, typeof(SteamAPICallCompleted_t) )) : ((SteamAPICallCompleted_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct CheckFileSignature_t : Steamworks.ISteamCallback
	{
		internal CheckFileSignature CheckFileSignature; // m_eCheckFileSignature enum ECheckFileSignature
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUtils + 5;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(CheckFileSignature_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((CheckFileSignature_t)(CheckFileSignature_t) Marshal.PtrToStructure( p, typeof(CheckFileSignature_t) )) : ((CheckFileSignature_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal CheckFileSignature CheckFileSignature; // m_eCheckFileSignature enum ECheckFileSignature
			
			public static implicit operator CheckFileSignature_t ( CheckFileSignature_t.Pack8 d ) => new CheckFileSignature_t{ CheckFileSignature = d.CheckFileSignature, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GamepadTextInputDismissed_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Submitted; // m_bSubmitted _Bool
		internal uint SubmittedText; // m_unSubmittedText uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUtils + 14;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GamepadTextInputDismissed_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GamepadTextInputDismissed_t)(GamepadTextInputDismissed_t) Marshal.PtrToStructure( p, typeof(GamepadTextInputDismissed_t) )) : ((GamepadTextInputDismissed_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct MatchMakingKeyValuePair_t
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string Key; // m_szKey char [256]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string Value; // m_szValue char [256]
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(MatchMakingKeyValuePair_t) : typeof(Pack8) );
		public MatchMakingKeyValuePair_t Fill( IntPtr p ) => Config.PackSmall ? ((MatchMakingKeyValuePair_t)(MatchMakingKeyValuePair_t) Marshal.PtrToStructure( p, typeof(MatchMakingKeyValuePair_t) )) : ((MatchMakingKeyValuePair_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct servernetadr_t
	{
		internal ushort ConnectionPort; // m_usConnectionPort uint16
		internal ushort QueryPort; // m_usQueryPort uint16
		internal uint IP; // m_unIP uint32
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(servernetadr_t) : typeof(Pack8) );
		public servernetadr_t Fill( IntPtr p ) => Config.PackSmall ? ((servernetadr_t)(servernetadr_t) Marshal.PtrToStructure( p, typeof(servernetadr_t) )) : ((servernetadr_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct gameserveritem_t
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(gameserveritem_t) : typeof(Pack8) );
		public gameserveritem_t Fill( IntPtr p ) => Config.PackSmall ? ((gameserveritem_t)(gameserveritem_t) Marshal.PtrToStructure( p, typeof(gameserveritem_t) )) : ((gameserveritem_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamPartyBeaconLocation_t
	{
		internal SteamPartyBeaconLocationType Type; // m_eType enum ESteamPartyBeaconLocationType
		internal ulong LocationID; // m_ulLocationID uint64
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamPartyBeaconLocation_t) : typeof(Pack8) );
		public SteamPartyBeaconLocation_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamPartyBeaconLocation_t)(SteamPartyBeaconLocation_t) Marshal.PtrToStructure( p, typeof(SteamPartyBeaconLocation_t) )) : ((SteamPartyBeaconLocation_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal SteamPartyBeaconLocationType Type; // m_eType enum ESteamPartyBeaconLocationType
			internal ulong LocationID; // m_ulLocationID uint64
			
			public static implicit operator SteamPartyBeaconLocation_t ( SteamPartyBeaconLocation_t.Pack8 d ) => new SteamPartyBeaconLocation_t{ Type = d.Type,LocationID = d.LocationID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FavoritesListChanged_t : Steamworks.ISteamCallback
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(FavoritesListChanged_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((FavoritesListChanged_t)(FavoritesListChanged_t) Marshal.PtrToStructure( p, typeof(FavoritesListChanged_t) )) : ((FavoritesListChanged_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct LobbyInvite_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDUser; // m_ulSteamIDUser uint64
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong GameID; // m_ulGameID uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LobbyInvite_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((LobbyInvite_t)(LobbyInvite_t) Marshal.PtrToStructure( p, typeof(LobbyInvite_t) )) : ((LobbyInvite_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct LobbyEnter_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal uint GfChatPermissions; // m_rgfChatPermissions uint32
		[MarshalAs(UnmanagedType.I1)]
		internal bool Locked; // m_bLocked _Bool
		internal uint EChatRoomEnterResponse; // m_EChatRoomEnterResponse uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 4;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LobbyEnter_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((LobbyEnter_t)(LobbyEnter_t) Marshal.PtrToStructure( p, typeof(LobbyEnter_t) )) : ((LobbyEnter_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct LobbyDataUpdate_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDMember; // m_ulSteamIDMember uint64
		internal byte Success; // m_bSuccess uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 5;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LobbyDataUpdate_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((LobbyDataUpdate_t)(LobbyDataUpdate_t) Marshal.PtrToStructure( p, typeof(LobbyDataUpdate_t) )) : ((LobbyDataUpdate_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct LobbyChatUpdate_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDUserChanged; // m_ulSteamIDUserChanged uint64
		internal ulong SteamIDMakingChange; // m_ulSteamIDMakingChange uint64
		internal uint GfChatMemberStateChange; // m_rgfChatMemberStateChange uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 6;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LobbyChatUpdate_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((LobbyChatUpdate_t)(LobbyChatUpdate_t) Marshal.PtrToStructure( p, typeof(LobbyChatUpdate_t) )) : ((LobbyChatUpdate_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct LobbyChatMsg_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDUser; // m_ulSteamIDUser uint64
		internal byte ChatEntryType; // m_eChatEntryType uint8
		internal uint ChatID; // m_iChatID uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 7;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LobbyChatMsg_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((LobbyChatMsg_t)(LobbyChatMsg_t) Marshal.PtrToStructure( p, typeof(LobbyChatMsg_t) )) : ((LobbyChatMsg_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct LobbyGameCreated_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDGameServer; // m_ulSteamIDGameServer uint64
		internal uint IP; // m_unIP uint32
		internal ushort Port; // m_usPort uint16
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 9;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LobbyGameCreated_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((LobbyGameCreated_t)(LobbyGameCreated_t) Marshal.PtrToStructure( p, typeof(LobbyGameCreated_t) )) : ((LobbyGameCreated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct LobbyMatchList_t : Steamworks.ISteamCallback
	{
		internal uint LobbiesMatching; // m_nLobbiesMatching uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 10;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LobbyMatchList_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((LobbyMatchList_t)(LobbyMatchList_t) Marshal.PtrToStructure( p, typeof(LobbyMatchList_t) )) : ((LobbyMatchList_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint LobbiesMatching; // m_nLobbiesMatching uint32
			
			public static implicit operator LobbyMatchList_t ( LobbyMatchList_t.Pack8 d ) => new LobbyMatchList_t{ LobbiesMatching = d.LobbiesMatching, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct LobbyKicked_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDAdmin; // m_ulSteamIDAdmin uint64
		internal byte KickedDueToDisconnect; // m_bKickedDueToDisconnect uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 12;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LobbyKicked_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((LobbyKicked_t)(LobbyKicked_t) Marshal.PtrToStructure( p, typeof(LobbyKicked_t) )) : ((LobbyKicked_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct LobbyCreated_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 13;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LobbyCreated_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((LobbyCreated_t)(LobbyCreated_t) Marshal.PtrToStructure( p, typeof(LobbyCreated_t) )) : ((LobbyCreated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			
			public static implicit operator LobbyCreated_t ( LobbyCreated_t.Pack8 d ) => new LobbyCreated_t{ Result = d.Result,SteamIDLobby = d.SteamIDLobby, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct PSNGameBootInviteResult_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool GameBootInviteExists; // m_bGameBootInviteExists _Bool
		internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 15;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(PSNGameBootInviteResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((PSNGameBootInviteResult_t)(PSNGameBootInviteResult_t) Marshal.PtrToStructure( p, typeof(PSNGameBootInviteResult_t) )) : ((PSNGameBootInviteResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FavoritesListAccountsUpdated_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMatchmaking + 16;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(FavoritesListAccountsUpdated_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((FavoritesListAccountsUpdated_t)(FavoritesListAccountsUpdated_t) Marshal.PtrToStructure( p, typeof(FavoritesListAccountsUpdated_t) )) : ((FavoritesListAccountsUpdated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator FavoritesListAccountsUpdated_t ( FavoritesListAccountsUpdated_t.Pack8 d ) => new FavoritesListAccountsUpdated_t{ Result = d.Result, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SearchForGameProgressCallback_t : Steamworks.ISteamCallback
	{
		internal ulong LSearchID; // m_ullSearchID uint64
		internal Result Result; // m_eResult enum EResult
		internal ulong LobbyID; // m_lobbyID class CSteamID
		internal ulong SteamIDEndedSearch; // m_steamIDEndedSearch class CSteamID
		internal int SecondsRemainingEstimate; // m_nSecondsRemainingEstimate int32
		internal int CPlayersSearching; // m_cPlayersSearching int32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameSearch + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SearchForGameProgressCallback_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SearchForGameProgressCallback_t)(SearchForGameProgressCallback_t) Marshal.PtrToStructure( p, typeof(SearchForGameProgressCallback_t) )) : ((SearchForGameProgressCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SearchForGameResultCallback_t : Steamworks.ISteamCallback
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SearchForGameResultCallback_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SearchForGameResultCallback_t)(SearchForGameResultCallback_t) Marshal.PtrToStructure( p, typeof(SearchForGameResultCallback_t) )) : ((SearchForGameResultCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RequestPlayersForGameProgressCallback_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong LSearchID; // m_ullSearchID uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameSearch + 11;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RequestPlayersForGameProgressCallback_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RequestPlayersForGameProgressCallback_t)(RequestPlayersForGameProgressCallback_t) Marshal.PtrToStructure( p, typeof(RequestPlayersForGameProgressCallback_t) )) : ((RequestPlayersForGameProgressCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong LSearchID; // m_ullSearchID uint64
			
			public static implicit operator RequestPlayersForGameProgressCallback_t ( RequestPlayersForGameProgressCallback_t.Pack8 d ) => new RequestPlayersForGameProgressCallback_t{ Result = d.Result,LSearchID = d.LSearchID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RequestPlayersForGameResultCallback_t : Steamworks.ISteamCallback
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RequestPlayersForGameResultCallback_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RequestPlayersForGameResultCallback_t)(RequestPlayersForGameResultCallback_t) Marshal.PtrToStructure( p, typeof(RequestPlayersForGameResultCallback_t) )) : ((RequestPlayersForGameResultCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RequestPlayersForGameFinalResultCallback_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong LSearchID; // m_ullSearchID uint64
		internal ulong LUniqueGameID; // m_ullUniqueGameID uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameSearch + 13;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RequestPlayersForGameFinalResultCallback_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RequestPlayersForGameFinalResultCallback_t)(RequestPlayersForGameFinalResultCallback_t) Marshal.PtrToStructure( p, typeof(RequestPlayersForGameFinalResultCallback_t) )) : ((RequestPlayersForGameFinalResultCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SubmitPlayerResultResultCallback_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong UllUniqueGameID; // ullUniqueGameID uint64
		internal ulong SteamIDPlayer; // steamIDPlayer class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameSearch + 14;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SubmitPlayerResultResultCallback_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SubmitPlayerResultResultCallback_t)(SubmitPlayerResultResultCallback_t) Marshal.PtrToStructure( p, typeof(SubmitPlayerResultResultCallback_t) )) : ((SubmitPlayerResultResultCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct EndGameResultCallback_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong UllUniqueGameID; // ullUniqueGameID uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameSearch + 15;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(EndGameResultCallback_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((EndGameResultCallback_t)(EndGameResultCallback_t) Marshal.PtrToStructure( p, typeof(EndGameResultCallback_t) )) : ((EndGameResultCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong UllUniqueGameID; // ullUniqueGameID uint64
			
			public static implicit operator EndGameResultCallback_t ( EndGameResultCallback_t.Pack8 d ) => new EndGameResultCallback_t{ Result = d.Result,UllUniqueGameID = d.UllUniqueGameID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct JoinPartyCallback_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
		internal ulong SteamIDBeaconOwner; // m_SteamIDBeaconOwner class CSteamID
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string ConnectString; // m_rgchConnectString char [256]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamParties + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(JoinPartyCallback_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((JoinPartyCallback_t)(JoinPartyCallback_t) Marshal.PtrToStructure( p, typeof(JoinPartyCallback_t) )) : ((JoinPartyCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct CreateBeaconCallback_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamParties + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(CreateBeaconCallback_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((CreateBeaconCallback_t)(CreateBeaconCallback_t) Marshal.PtrToStructure( p, typeof(CreateBeaconCallback_t) )) : ((CreateBeaconCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
			
			public static implicit operator CreateBeaconCallback_t ( CreateBeaconCallback_t.Pack8 d ) => new CreateBeaconCallback_t{ Result = d.Result,BeaconID = d.BeaconID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct ReservationNotificationCallback_t : Steamworks.ISteamCallback
	{
		internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
		internal ulong SteamIDJoiner; // m_steamIDJoiner class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamParties + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(ReservationNotificationCallback_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((ReservationNotificationCallback_t)(ReservationNotificationCallback_t) Marshal.PtrToStructure( p, typeof(ReservationNotificationCallback_t) )) : ((ReservationNotificationCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
			internal ulong SteamIDJoiner; // m_steamIDJoiner class CSteamID
			
			public static implicit operator ReservationNotificationCallback_t ( ReservationNotificationCallback_t.Pack8 d ) => new ReservationNotificationCallback_t{ BeaconID = d.BeaconID,SteamIDJoiner = d.SteamIDJoiner, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct ChangeNumOpenSlotsCallback_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamParties + 4;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(ChangeNumOpenSlotsCallback_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((ChangeNumOpenSlotsCallback_t)(ChangeNumOpenSlotsCallback_t) Marshal.PtrToStructure( p, typeof(ChangeNumOpenSlotsCallback_t) )) : ((ChangeNumOpenSlotsCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator ChangeNumOpenSlotsCallback_t ( ChangeNumOpenSlotsCallback_t.Pack8 d ) => new ChangeNumOpenSlotsCallback_t{ Result = d.Result, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamParamStringArray_t
	{
		internal IntPtr Strings; // m_ppStrings const char **
		internal int NumStrings; // m_nNumStrings int32
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamParamStringArray_t) : typeof(Pack8) );
		public SteamParamStringArray_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamParamStringArray_t)(SteamParamStringArray_t) Marshal.PtrToStructure( p, typeof(SteamParamStringArray_t) )) : ((SteamParamStringArray_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal IntPtr Strings; // m_ppStrings const char **
			internal int NumStrings; // m_nNumStrings int32
			
			public static implicit operator SteamParamStringArray_t ( SteamParamStringArray_t.Pack8 d ) => new SteamParamStringArray_t{ Strings = d.Strings,NumStrings = d.NumStrings, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageAppSyncedClient_t : Steamworks.ISteamCallback
	{
		internal AppId AppID; // m_nAppID AppId_t
		internal Result Result; // m_eResult enum EResult
		internal int NumDownloads; // m_unNumDownloads int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageAppSyncedClient_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageAppSyncedClient_t)(RemoteStorageAppSyncedClient_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncedClient_t) )) : ((RemoteStorageAppSyncedClient_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal AppId AppID; // m_nAppID AppId_t
			internal Result Result; // m_eResult enum EResult
			internal int NumDownloads; // m_unNumDownloads int
			
			public static implicit operator RemoteStorageAppSyncedClient_t ( RemoteStorageAppSyncedClient_t.Pack8 d ) => new RemoteStorageAppSyncedClient_t{ AppID = d.AppID,Result = d.Result,NumDownloads = d.NumDownloads, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageAppSyncedServer_t : Steamworks.ISteamCallback
	{
		internal AppId AppID; // m_nAppID AppId_t
		internal Result Result; // m_eResult enum EResult
		internal int NumUploads; // m_unNumUploads int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageAppSyncedServer_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageAppSyncedServer_t)(RemoteStorageAppSyncedServer_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncedServer_t) )) : ((RemoteStorageAppSyncedServer_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal AppId AppID; // m_nAppID AppId_t
			internal Result Result; // m_eResult enum EResult
			internal int NumUploads; // m_unNumUploads int
			
			public static implicit operator RemoteStorageAppSyncedServer_t ( RemoteStorageAppSyncedServer_t.Pack8 d ) => new RemoteStorageAppSyncedServer_t{ AppID = d.AppID,Result = d.Result,NumUploads = d.NumUploads, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageAppSyncProgress_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string CurrentFile; // m_rgchCurrentFile char [260]
		internal AppId AppID; // m_nAppID AppId_t
		internal uint BytesTransferredThisChunk; // m_uBytesTransferredThisChunk uint32
		internal double DAppPercentComplete; // m_dAppPercentComplete double
		[MarshalAs(UnmanagedType.I1)]
		internal bool Uploading; // m_bUploading _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageAppSyncProgress_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageAppSyncProgress_t)(RemoteStorageAppSyncProgress_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncProgress_t) )) : ((RemoteStorageAppSyncProgress_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string CurrentFile; // m_rgchCurrentFile char [260]
			internal AppId AppID; // m_nAppID AppId_t
			internal uint BytesTransferredThisChunk; // m_uBytesTransferredThisChunk uint32
			internal double DAppPercentComplete; // m_dAppPercentComplete double
			[MarshalAs(UnmanagedType.I1)]
			internal bool Uploading; // m_bUploading _Bool
			
			public static implicit operator RemoteStorageAppSyncProgress_t ( RemoteStorageAppSyncProgress_t.Pack8 d ) => new RemoteStorageAppSyncProgress_t{ CurrentFile = d.CurrentFile,AppID = d.AppID,BytesTransferredThisChunk = d.BytesTransferredThisChunk,DAppPercentComplete = d.DAppPercentComplete,Uploading = d.Uploading, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageAppSyncStatusCheck_t : Steamworks.ISteamCallback
	{
		internal AppId AppID; // m_nAppID AppId_t
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 5;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageAppSyncStatusCheck_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageAppSyncStatusCheck_t)(RemoteStorageAppSyncStatusCheck_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncStatusCheck_t) )) : ((RemoteStorageAppSyncStatusCheck_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal AppId AppID; // m_nAppID AppId_t
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator RemoteStorageAppSyncStatusCheck_t ( RemoteStorageAppSyncStatusCheck_t.Pack8 d ) => new RemoteStorageAppSyncStatusCheck_t{ AppID = d.AppID,Result = d.Result, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageFileShareResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong File; // m_hFile UGCHandle_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string Filename; // m_rgchFilename char [260]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 7;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageFileShareResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageFileShareResult_t)(RemoteStorageFileShareResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageFileShareResult_t) )) : ((RemoteStorageFileShareResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStoragePublishFileResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 9;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStoragePublishFileResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStoragePublishFileResult_t)(RemoteStoragePublishFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishFileResult_t) )) : ((RemoteStoragePublishFileResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			[MarshalAs(UnmanagedType.I1)]
			internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			
			public static implicit operator RemoteStoragePublishFileResult_t ( RemoteStoragePublishFileResult_t.Pack8 d ) => new RemoteStoragePublishFileResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageDeletePublishedFileResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 11;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageDeletePublishedFileResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageDeletePublishedFileResult_t)(RemoteStorageDeletePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageDeletePublishedFileResult_t) )) : ((RemoteStorageDeletePublishedFileResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoteStorageDeletePublishedFileResult_t ( RemoteStorageDeletePublishedFileResult_t.Pack8 d ) => new RemoteStorageDeletePublishedFileResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageEnumerateUserPublishedFilesResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 12;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageEnumerateUserPublishedFilesResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageEnumerateUserPublishedFilesResult_t)(RemoteStorageEnumerateUserPublishedFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserPublishedFilesResult_t) )) : ((RemoteStorageEnumerateUserPublishedFilesResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal int ResultsReturned; // m_nResultsReturned int32
			internal int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			
			public static implicit operator RemoteStorageEnumerateUserPublishedFilesResult_t ( RemoteStorageEnumerateUserPublishedFilesResult_t.Pack8 d ) => new RemoteStorageEnumerateUserPublishedFilesResult_t{ Result = d.Result,ResultsReturned = d.ResultsReturned,TotalResultCount = d.TotalResultCount,GPublishedFileId = d.GPublishedFileId, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageSubscribePublishedFileResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 13;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageSubscribePublishedFileResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageSubscribePublishedFileResult_t)(RemoteStorageSubscribePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageSubscribePublishedFileResult_t) )) : ((RemoteStorageSubscribePublishedFileResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoteStorageSubscribePublishedFileResult_t ( RemoteStorageSubscribePublishedFileResult_t.Pack8 d ) => new RemoteStorageSubscribePublishedFileResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageEnumerateUserSubscribedFilesResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
		internal uint[] GRTimeSubscribed; // m_rgRTimeSubscribed uint32 [50]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 14;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageEnumerateUserSubscribedFilesResult_t)(RemoteStorageEnumerateUserSubscribedFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t) )) : ((RemoteStorageEnumerateUserSubscribedFilesResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal int ResultsReturned; // m_nResultsReturned int32
			internal int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
			internal uint[] GRTimeSubscribed; // m_rgRTimeSubscribed uint32 [50]
			
			public static implicit operator RemoteStorageEnumerateUserSubscribedFilesResult_t ( RemoteStorageEnumerateUserSubscribedFilesResult_t.Pack8 d ) => new RemoteStorageEnumerateUserSubscribedFilesResult_t{ Result = d.Result,ResultsReturned = d.ResultsReturned,TotalResultCount = d.TotalResultCount,GPublishedFileId = d.GPublishedFileId,GRTimeSubscribed = d.GRTimeSubscribed, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageUnsubscribePublishedFileResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 15;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageUnsubscribePublishedFileResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageUnsubscribePublishedFileResult_t)(RemoteStorageUnsubscribePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUnsubscribePublishedFileResult_t) )) : ((RemoteStorageUnsubscribePublishedFileResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoteStorageUnsubscribePublishedFileResult_t ( RemoteStorageUnsubscribePublishedFileResult_t.Pack8 d ) => new RemoteStorageUnsubscribePublishedFileResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageUpdatePublishedFileResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 16;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageUpdatePublishedFileResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageUpdatePublishedFileResult_t)(RemoteStorageUpdatePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUpdatePublishedFileResult_t) )) : ((RemoteStorageUpdatePublishedFileResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			[MarshalAs(UnmanagedType.I1)]
			internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			
			public static implicit operator RemoteStorageUpdatePublishedFileResult_t ( RemoteStorageUpdatePublishedFileResult_t.Pack8 d ) => new RemoteStorageUpdatePublishedFileResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageDownloadUGCResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong File; // m_hFile UGCHandle_t
		internal AppId AppID; // m_nAppID AppId_t
		internal int SizeInBytes; // m_nSizeInBytes int32
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string PchFileName; // m_pchFileName char [260]
		internal ulong SteamIDOwner; // m_ulSteamIDOwner uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 17;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageDownloadUGCResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageDownloadUGCResult_t)(RemoteStorageDownloadUGCResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageDownloadUGCResult_t) )) : ((RemoteStorageDownloadUGCResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong File; // m_hFile UGCHandle_t
			internal AppId AppID; // m_nAppID AppId_t
			internal int SizeInBytes; // m_nSizeInBytes int32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string PchFileName; // m_pchFileName char [260]
			internal ulong SteamIDOwner; // m_ulSteamIDOwner uint64
			
			public static implicit operator RemoteStorageDownloadUGCResult_t ( RemoteStorageDownloadUGCResult_t.Pack8 d ) => new RemoteStorageDownloadUGCResult_t{ Result = d.Result,File = d.File,AppID = d.AppID,SizeInBytes = d.SizeInBytes,PchFileName = d.PchFileName,SteamIDOwner = d.SteamIDOwner, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageGetPublishedFileDetailsResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId CreatorAppID; // m_nCreatorAppID AppId_t
		internal AppId ConsumerAppID; // m_nConsumerAppID AppId_t
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageGetPublishedFileDetailsResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageGetPublishedFileDetailsResult_t)(RemoteStorageGetPublishedFileDetailsResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageGetPublishedFileDetailsResult_t) )) : ((RemoteStorageGetPublishedFileDetailsResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal AppId CreatorAppID; // m_nCreatorAppID AppId_t
			internal AppId ConsumerAppID; // m_nConsumerAppID AppId_t
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageEnumerateWorkshopFilesResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.R4)]
		internal float[] GScore; // m_rgScore float [50]
		internal AppId AppId; // m_nAppId AppId_t
		internal uint StartIndex; // m_unStartIndex uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 19;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageEnumerateWorkshopFilesResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageEnumerateWorkshopFilesResult_t)(RemoteStorageEnumerateWorkshopFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateWorkshopFilesResult_t) )) : ((RemoteStorageEnumerateWorkshopFilesResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal int ResultsReturned; // m_nResultsReturned int32
			internal int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.R4)]
			internal float[] GScore; // m_rgScore float [50]
			internal AppId AppId; // m_nAppId AppId_t
			internal uint StartIndex; // m_unStartIndex uint32
			
			public static implicit operator RemoteStorageEnumerateWorkshopFilesResult_t ( RemoteStorageEnumerateWorkshopFilesResult_t.Pack8 d ) => new RemoteStorageEnumerateWorkshopFilesResult_t{ Result = d.Result,ResultsReturned = d.ResultsReturned,TotalResultCount = d.TotalResultCount,GPublishedFileId = d.GPublishedFileId,GScore = d.GScore,AppId = d.AppId,StartIndex = d.StartIndex, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageGetPublishedItemVoteDetailsResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_unPublishedFileId PublishedFileId_t
		internal int VotesFor; // m_nVotesFor int32
		internal int VotesAgainst; // m_nVotesAgainst int32
		internal int Reports; // m_nReports int32
		internal float FScore; // m_fScore float
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 20;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageGetPublishedItemVoteDetailsResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageGetPublishedItemVoteDetailsResult_t)(RemoteStorageGetPublishedItemVoteDetailsResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageGetPublishedItemVoteDetailsResult_t) )) : ((RemoteStorageGetPublishedItemVoteDetailsResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal PublishedFileId PublishedFileId; // m_unPublishedFileId PublishedFileId_t
			internal int VotesFor; // m_nVotesFor int32
			internal int VotesAgainst; // m_nVotesAgainst int32
			internal int Reports; // m_nReports int32
			internal float FScore; // m_fScore float
			
			public static implicit operator RemoteStorageGetPublishedItemVoteDetailsResult_t ( RemoteStorageGetPublishedItemVoteDetailsResult_t.Pack8 d ) => new RemoteStorageGetPublishedItemVoteDetailsResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,VotesFor = d.VotesFor,VotesAgainst = d.VotesAgainst,Reports = d.Reports,FScore = d.FScore, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStoragePublishedFileSubscribed_t : Steamworks.ISteamCallback
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 21;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStoragePublishedFileSubscribed_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStoragePublishedFileSubscribed_t)(RemoteStoragePublishedFileSubscribed_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileSubscribed_t) )) : ((RemoteStoragePublishedFileSubscribed_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal AppId AppID; // m_nAppID AppId_t
			
			public static implicit operator RemoteStoragePublishedFileSubscribed_t ( RemoteStoragePublishedFileSubscribed_t.Pack8 d ) => new RemoteStoragePublishedFileSubscribed_t{ PublishedFileId = d.PublishedFileId,AppID = d.AppID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStoragePublishedFileUnsubscribed_t : Steamworks.ISteamCallback
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 22;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStoragePublishedFileUnsubscribed_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStoragePublishedFileUnsubscribed_t)(RemoteStoragePublishedFileUnsubscribed_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileUnsubscribed_t) )) : ((RemoteStoragePublishedFileUnsubscribed_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal AppId AppID; // m_nAppID AppId_t
			
			public static implicit operator RemoteStoragePublishedFileUnsubscribed_t ( RemoteStoragePublishedFileUnsubscribed_t.Pack8 d ) => new RemoteStoragePublishedFileUnsubscribed_t{ PublishedFileId = d.PublishedFileId,AppID = d.AppID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStoragePublishedFileDeleted_t : Steamworks.ISteamCallback
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 23;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStoragePublishedFileDeleted_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStoragePublishedFileDeleted_t)(RemoteStoragePublishedFileDeleted_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileDeleted_t) )) : ((RemoteStoragePublishedFileDeleted_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal AppId AppID; // m_nAppID AppId_t
			
			public static implicit operator RemoteStoragePublishedFileDeleted_t ( RemoteStoragePublishedFileDeleted_t.Pack8 d ) => new RemoteStoragePublishedFileDeleted_t{ PublishedFileId = d.PublishedFileId,AppID = d.AppID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageUpdateUserPublishedItemVoteResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 24;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageUpdateUserPublishedItemVoteResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageUpdateUserPublishedItemVoteResult_t)(RemoteStorageUpdateUserPublishedItemVoteResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUpdateUserPublishedItemVoteResult_t) )) : ((RemoteStorageUpdateUserPublishedItemVoteResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoteStorageUpdateUserPublishedItemVoteResult_t ( RemoteStorageUpdateUserPublishedItemVoteResult_t.Pack8 d ) => new RemoteStorageUpdateUserPublishedItemVoteResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageUserVoteDetails_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal WorkshopVote Vote; // m_eVote enum EWorkshopVote
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 25;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageUserVoteDetails_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageUserVoteDetails_t)(RemoteStorageUserVoteDetails_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUserVoteDetails_t) )) : ((RemoteStorageUserVoteDetails_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal WorkshopVote Vote; // m_eVote enum EWorkshopVote
			
			public static implicit operator RemoteStorageUserVoteDetails_t ( RemoteStorageUserVoteDetails_t.Pack8 d ) => new RemoteStorageUserVoteDetails_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,Vote = d.Vote, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 26;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageEnumerateUserSharedWorkshopFilesResult_t)(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) )) : ((RemoteStorageEnumerateUserSharedWorkshopFilesResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal int ResultsReturned; // m_nResultsReturned int32
			internal int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			
			public static implicit operator RemoteStorageEnumerateUserSharedWorkshopFilesResult_t ( RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.Pack8 d ) => new RemoteStorageEnumerateUserSharedWorkshopFilesResult_t{ Result = d.Result,ResultsReturned = d.ResultsReturned,TotalResultCount = d.TotalResultCount,GPublishedFileId = d.GPublishedFileId, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageSetUserPublishedFileActionResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 27;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageSetUserPublishedFileActionResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageSetUserPublishedFileActionResult_t)(RemoteStorageSetUserPublishedFileActionResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageSetUserPublishedFileActionResult_t) )) : ((RemoteStorageSetUserPublishedFileActionResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
			
			public static implicit operator RemoteStorageSetUserPublishedFileActionResult_t ( RemoteStorageSetUserPublishedFileActionResult_t.Pack8 d ) => new RemoteStorageSetUserPublishedFileActionResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,Action = d.Action, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageEnumeratePublishedFilesByUserActionResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
		internal uint[] GRTimeUpdated; // m_rgRTimeUpdated uint32 [50]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 28;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageEnumeratePublishedFilesByUserActionResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageEnumeratePublishedFilesByUserActionResult_t)(RemoteStorageEnumeratePublishedFilesByUserActionResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumeratePublishedFilesByUserActionResult_t) )) : ((RemoteStorageEnumeratePublishedFilesByUserActionResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
			internal int ResultsReturned; // m_nResultsReturned int32
			internal int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
			internal uint[] GRTimeUpdated; // m_rgRTimeUpdated uint32 [50]
			
			public static implicit operator RemoteStorageEnumeratePublishedFilesByUserActionResult_t ( RemoteStorageEnumeratePublishedFilesByUserActionResult_t.Pack8 d ) => new RemoteStorageEnumeratePublishedFilesByUserActionResult_t{ Result = d.Result,Action = d.Action,ResultsReturned = d.ResultsReturned,TotalResultCount = d.TotalResultCount,GPublishedFileId = d.GPublishedFileId,GRTimeUpdated = d.GRTimeUpdated, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStoragePublishFileProgress_t : Steamworks.ISteamCallback
	{
		internal double DPercentFile; // m_dPercentFile double
		[MarshalAs(UnmanagedType.I1)]
		internal bool Preview; // m_bPreview _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 29;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStoragePublishFileProgress_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStoragePublishFileProgress_t)(RemoteStoragePublishFileProgress_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishFileProgress_t) )) : ((RemoteStoragePublishFileProgress_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStoragePublishedFileUpdated_t : Steamworks.ISteamCallback
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		internal ulong Unused; // m_ulUnused uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 30;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStoragePublishedFileUpdated_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStoragePublishedFileUpdated_t)(RemoteStoragePublishedFileUpdated_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileUpdated_t) )) : ((RemoteStoragePublishedFileUpdated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal AppId AppID; // m_nAppID AppId_t
			internal ulong Unused; // m_ulUnused uint64
			
			public static implicit operator RemoteStoragePublishedFileUpdated_t ( RemoteStoragePublishedFileUpdated_t.Pack8 d ) => new RemoteStoragePublishedFileUpdated_t{ PublishedFileId = d.PublishedFileId,AppID = d.AppID,Unused = d.Unused, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageFileWriteAsyncComplete_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 31;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageFileWriteAsyncComplete_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageFileWriteAsyncComplete_t)(RemoteStorageFileWriteAsyncComplete_t) Marshal.PtrToStructure( p, typeof(RemoteStorageFileWriteAsyncComplete_t) )) : ((RemoteStorageFileWriteAsyncComplete_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator RemoteStorageFileWriteAsyncComplete_t ( RemoteStorageFileWriteAsyncComplete_t.Pack8 d ) => new RemoteStorageFileWriteAsyncComplete_t{ Result = d.Result, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoteStorageFileReadAsyncComplete_t : Steamworks.ISteamCallback
	{
		internal ulong FileReadAsync; // m_hFileReadAsync SteamAPICall_t
		internal Result Result; // m_eResult enum EResult
		internal uint Offset; // m_nOffset uint32
		internal uint Read; // m_cubRead uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientRemoteStorage + 32;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageFileReadAsyncComplete_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageFileReadAsyncComplete_t)(RemoteStorageFileReadAsyncComplete_t) Marshal.PtrToStructure( p, typeof(RemoteStorageFileReadAsyncComplete_t) )) : ((RemoteStorageFileReadAsyncComplete_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct LeaderboardEntry_t
	{
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		internal int GlobalRank; // m_nGlobalRank int32
		internal int Score; // m_nScore int32
		internal int CDetails; // m_cDetails int32
		internal ulong UGC; // m_hUGC UGCHandle_t
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LeaderboardEntry_t) : typeof(Pack8) );
		public LeaderboardEntry_t Fill( IntPtr p ) => Config.PackSmall ? ((LeaderboardEntry_t)(LeaderboardEntry_t) Marshal.PtrToStructure( p, typeof(LeaderboardEntry_t) )) : ((LeaderboardEntry_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct UserStatsReceived_t : Steamworks.ISteamCallback
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(UserStatsReceived_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((UserStatsReceived_t)(UserStatsReceived_t) Marshal.PtrToStructure( p, typeof(UserStatsReceived_t) )) : ((UserStatsReceived_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct UserStatsStored_t : Steamworks.ISteamCallback
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(UserStatsStored_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((UserStatsStored_t)(UserStatsStored_t) Marshal.PtrToStructure( p, typeof(UserStatsStored_t) )) : ((UserStatsStored_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong GameID; // m_nGameID uint64
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator UserStatsStored_t ( UserStatsStored_t.Pack8 d ) => new UserStatsStored_t{ GameID = d.GameID,Result = d.Result, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct UserAchievementStored_t : Steamworks.ISteamCallback
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(UserAchievementStored_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((UserAchievementStored_t)(UserAchievementStored_t) Marshal.PtrToStructure( p, typeof(UserAchievementStored_t) )) : ((UserAchievementStored_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct LeaderboardFindResult_t : Steamworks.ISteamCallback
	{
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		internal byte LeaderboardFound; // m_bLeaderboardFound uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 4;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LeaderboardFindResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((LeaderboardFindResult_t)(LeaderboardFindResult_t) Marshal.PtrToStructure( p, typeof(LeaderboardFindResult_t) )) : ((LeaderboardFindResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
			internal byte LeaderboardFound; // m_bLeaderboardFound uint8
			
			public static implicit operator LeaderboardFindResult_t ( LeaderboardFindResult_t.Pack8 d ) => new LeaderboardFindResult_t{ SteamLeaderboard = d.SteamLeaderboard,LeaderboardFound = d.LeaderboardFound, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct LeaderboardScoresDownloaded_t : Steamworks.ISteamCallback
	{
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		internal ulong SteamLeaderboardEntries; // m_hSteamLeaderboardEntries SteamLeaderboardEntries_t
		internal int CEntryCount; // m_cEntryCount int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 5;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LeaderboardScoresDownloaded_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((LeaderboardScoresDownloaded_t)(LeaderboardScoresDownloaded_t) Marshal.PtrToStructure( p, typeof(LeaderboardScoresDownloaded_t) )) : ((LeaderboardScoresDownloaded_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct LeaderboardScoreUploaded_t : Steamworks.ISteamCallback
	{
		internal byte Success; // m_bSuccess uint8
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		internal int Score; // m_nScore int32
		internal byte ScoreChanged; // m_bScoreChanged uint8
		internal int GlobalRankNew; // m_nGlobalRankNew int
		internal int GlobalRankPrevious; // m_nGlobalRankPrevious int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 6;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LeaderboardScoreUploaded_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((LeaderboardScoreUploaded_t)(LeaderboardScoreUploaded_t) Marshal.PtrToStructure( p, typeof(LeaderboardScoreUploaded_t) )) : ((LeaderboardScoreUploaded_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct NumberOfCurrentPlayers_t : Steamworks.ISteamCallback
	{
		internal byte Success; // m_bSuccess uint8
		internal int CPlayers; // m_cPlayers int32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 7;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(NumberOfCurrentPlayers_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((NumberOfCurrentPlayers_t)(NumberOfCurrentPlayers_t) Marshal.PtrToStructure( p, typeof(NumberOfCurrentPlayers_t) )) : ((NumberOfCurrentPlayers_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal byte Success; // m_bSuccess uint8
			internal int CPlayers; // m_cPlayers int32
			
			public static implicit operator NumberOfCurrentPlayers_t ( NumberOfCurrentPlayers_t.Pack8 d ) => new NumberOfCurrentPlayers_t{ Success = d.Success,CPlayers = d.CPlayers, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct UserStatsUnloaded_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 8;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(UserStatsUnloaded_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((UserStatsUnloaded_t)(UserStatsUnloaded_t) Marshal.PtrToStructure( p, typeof(UserStatsUnloaded_t) )) : ((UserStatsUnloaded_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator UserStatsUnloaded_t ( UserStatsUnloaded_t.Pack8 d ) => new UserStatsUnloaded_t{ SteamIDUser = d.SteamIDUser, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct UserAchievementIconFetched_t : Steamworks.ISteamCallback
	{
		internal GameId GameID; // m_nGameID class CGameID
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string AchievementName; // m_rgchAchievementName char [128]
		[MarshalAs(UnmanagedType.I1)]
		internal bool Achieved; // m_bAchieved _Bool
		internal int IconHandle; // m_nIconHandle int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 9;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(UserAchievementIconFetched_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((UserAchievementIconFetched_t)(UserAchievementIconFetched_t) Marshal.PtrToStructure( p, typeof(UserAchievementIconFetched_t) )) : ((UserAchievementIconFetched_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal GameId GameID; // m_nGameID class CGameID
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string AchievementName; // m_rgchAchievementName char [128]
			[MarshalAs(UnmanagedType.I1)]
			internal bool Achieved; // m_bAchieved _Bool
			internal int IconHandle; // m_nIconHandle int
			
			public static implicit operator UserAchievementIconFetched_t ( UserAchievementIconFetched_t.Pack8 d ) => new UserAchievementIconFetched_t{ GameID = d.GameID,AchievementName = d.AchievementName,Achieved = d.Achieved,IconHandle = d.IconHandle, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GlobalAchievementPercentagesReady_t : Steamworks.ISteamCallback
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 10;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GlobalAchievementPercentagesReady_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GlobalAchievementPercentagesReady_t)(GlobalAchievementPercentagesReady_t) Marshal.PtrToStructure( p, typeof(GlobalAchievementPercentagesReady_t) )) : ((GlobalAchievementPercentagesReady_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong GameID; // m_nGameID uint64
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator GlobalAchievementPercentagesReady_t ( GlobalAchievementPercentagesReady_t.Pack8 d ) => new GlobalAchievementPercentagesReady_t{ GameID = d.GameID,Result = d.Result, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct LeaderboardUGCSet_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 11;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LeaderboardUGCSet_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((LeaderboardUGCSet_t)(LeaderboardUGCSet_t) Marshal.PtrToStructure( p, typeof(LeaderboardUGCSet_t) )) : ((LeaderboardUGCSet_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
			
			public static implicit operator LeaderboardUGCSet_t ( LeaderboardUGCSet_t.Pack8 d ) => new LeaderboardUGCSet_t{ Result = d.Result,SteamLeaderboard = d.SteamLeaderboard, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct PS3TrophiesInstalled_t : Steamworks.ISteamCallback
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		internal ulong RequiredDiskSpace; // m_ulRequiredDiskSpace uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 12;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(PS3TrophiesInstalled_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((PS3TrophiesInstalled_t)(PS3TrophiesInstalled_t) Marshal.PtrToStructure( p, typeof(PS3TrophiesInstalled_t) )) : ((PS3TrophiesInstalled_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GlobalStatsReceived_t : Steamworks.ISteamCallback
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 12;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GlobalStatsReceived_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GlobalStatsReceived_t)(GlobalStatsReceived_t) Marshal.PtrToStructure( p, typeof(GlobalStatsReceived_t) )) : ((GlobalStatsReceived_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal ulong GameID; // m_nGameID uint64
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator GlobalStatsReceived_t ( GlobalStatsReceived_t.Pack8 d ) => new GlobalStatsReceived_t{ GameID = d.GameID,Result = d.Result, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct DlcInstalled_t : Steamworks.ISteamCallback
	{
		internal AppId AppID; // m_nAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamApps + 5;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(DlcInstalled_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((DlcInstalled_t)(DlcInstalled_t) Marshal.PtrToStructure( p, typeof(DlcInstalled_t) )) : ((DlcInstalled_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal AppId AppID; // m_nAppID AppId_t
			
			public static implicit operator DlcInstalled_t ( DlcInstalled_t.Pack8 d ) => new DlcInstalled_t{ AppID = d.AppID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RegisterActivationCodeResponse_t : Steamworks.ISteamCallback
	{
		internal RegisterActivationCodeResult Result; // m_eResult enum ERegisterActivationCodeResult
		internal uint PackageRegistered; // m_unPackageRegistered uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamApps + 8;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RegisterActivationCodeResponse_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RegisterActivationCodeResponse_t)(RegisterActivationCodeResponse_t) Marshal.PtrToStructure( p, typeof(RegisterActivationCodeResponse_t) )) : ((RegisterActivationCodeResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal RegisterActivationCodeResult Result; // m_eResult enum ERegisterActivationCodeResult
			internal uint PackageRegistered; // m_unPackageRegistered uint32
			
			public static implicit operator RegisterActivationCodeResponse_t ( RegisterActivationCodeResponse_t.Pack8 d ) => new RegisterActivationCodeResponse_t{ Result = d.Result,PackageRegistered = d.PackageRegistered, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct AppProofOfPurchaseKeyResponse_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal uint AppID; // m_nAppID uint32
		internal uint CchKeyLength; // m_cchKeyLength uint32
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 240)]
		internal string Key; // m_rgchKey char [240]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamApps + 21;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(AppProofOfPurchaseKeyResponse_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((AppProofOfPurchaseKeyResponse_t)(AppProofOfPurchaseKeyResponse_t) Marshal.PtrToStructure( p, typeof(AppProofOfPurchaseKeyResponse_t) )) : ((AppProofOfPurchaseKeyResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FileDetailsResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong FileSize; // m_ulFileSize uint64
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] //  m_FileSHA
		internal byte[] FileSHA; // m_FileSHA uint8 [20]
		internal uint Flags; // m_unFlags uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamApps + 23;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(FileDetailsResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((FileDetailsResult_t)(FileDetailsResult_t) Marshal.PtrToStructure( p, typeof(FileDetailsResult_t) )) : ((FileDetailsResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
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
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(P2PSessionState_t) : typeof(Pack8) );
		public P2PSessionState_t Fill( IntPtr p ) => Config.PackSmall ? ((P2PSessionState_t)(P2PSessionState_t) Marshal.PtrToStructure( p, typeof(P2PSessionState_t) )) : ((P2PSessionState_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct P2PSessionRequest_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamNetworking + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(P2PSessionRequest_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((P2PSessionRequest_t)(P2PSessionRequest_t) Marshal.PtrToStructure( p, typeof(P2PSessionRequest_t) )) : ((P2PSessionRequest_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
			
			public static implicit operator P2PSessionRequest_t ( P2PSessionRequest_t.Pack8 d ) => new P2PSessionRequest_t{ SteamIDRemote = d.SteamIDRemote, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct P2PSessionConnectFail_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
		internal byte P2PSessionError; // m_eP2PSessionError uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamNetworking + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(P2PSessionConnectFail_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((P2PSessionConnectFail_t)(P2PSessionConnectFail_t) Marshal.PtrToStructure( p, typeof(P2PSessionConnectFail_t) )) : ((P2PSessionConnectFail_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
			internal byte P2PSessionError; // m_eP2PSessionError uint8
			
			public static implicit operator P2PSessionConnectFail_t ( P2PSessionConnectFail_t.Pack8 d ) => new P2PSessionConnectFail_t{ SteamIDRemote = d.SteamIDRemote,P2PSessionError = d.P2PSessionError, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SocketStatusCallback_t : Steamworks.ISteamCallback
	{
		internal uint Socket; // m_hSocket SNetSocket_t
		internal uint ListenSocket; // m_hListenSocket SNetListenSocket_t
		internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
		internal int SNetSocketState; // m_eSNetSocketState int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamNetworking + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SocketStatusCallback_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SocketStatusCallback_t)(SocketStatusCallback_t) Marshal.PtrToStructure( p, typeof(SocketStatusCallback_t) )) : ((SocketStatusCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct ScreenshotReady_t : Steamworks.ISteamCallback
	{
		internal uint Local; // m_hLocal ScreenshotHandle
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamScreenshots + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(ScreenshotReady_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((ScreenshotReady_t)(ScreenshotReady_t) Marshal.PtrToStructure( p, typeof(ScreenshotReady_t) )) : ((ScreenshotReady_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint Local; // m_hLocal ScreenshotHandle
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator ScreenshotReady_t ( ScreenshotReady_t.Pack8 d ) => new ScreenshotReady_t{ Local = d.Local,Result = d.Result, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct VolumeHasChanged_t : Steamworks.ISteamCallback
	{
		internal float NewVolume; // m_flNewVolume float
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMusic + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(VolumeHasChanged_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((VolumeHasChanged_t)(VolumeHasChanged_t) Marshal.PtrToStructure( p, typeof(VolumeHasChanged_t) )) : ((VolumeHasChanged_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal float NewVolume; // m_flNewVolume float
			
			public static implicit operator VolumeHasChanged_t ( VolumeHasChanged_t.Pack8 d ) => new VolumeHasChanged_t{ NewVolume = d.NewVolume, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct MusicPlayerWantsShuffled_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Shuffled; // m_bShuffled _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMusicRemote + 9;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(MusicPlayerWantsShuffled_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((MusicPlayerWantsShuffled_t)(MusicPlayerWantsShuffled_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsShuffled_t) )) : ((MusicPlayerWantsShuffled_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool Shuffled; // m_bShuffled _Bool
			
			public static implicit operator MusicPlayerWantsShuffled_t ( MusicPlayerWantsShuffled_t.Pack8 d ) => new MusicPlayerWantsShuffled_t{ Shuffled = d.Shuffled, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct MusicPlayerWantsLooped_t : Steamworks.ISteamCallback
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Looped; // m_bLooped _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMusicRemote + 10;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(MusicPlayerWantsLooped_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((MusicPlayerWantsLooped_t)(MusicPlayerWantsLooped_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsLooped_t) )) : ((MusicPlayerWantsLooped_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			[MarshalAs(UnmanagedType.I1)]
			internal bool Looped; // m_bLooped _Bool
			
			public static implicit operator MusicPlayerWantsLooped_t ( MusicPlayerWantsLooped_t.Pack8 d ) => new MusicPlayerWantsLooped_t{ Looped = d.Looped, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct MusicPlayerWantsVolume_t : Steamworks.ISteamCallback
	{
		internal float NewVolume; // m_flNewVolume float
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMusic + 11;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(MusicPlayerWantsVolume_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((MusicPlayerWantsVolume_t)(MusicPlayerWantsVolume_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsVolume_t) )) : ((MusicPlayerWantsVolume_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal float NewVolume; // m_flNewVolume float
			
			public static implicit operator MusicPlayerWantsVolume_t ( MusicPlayerWantsVolume_t.Pack8 d ) => new MusicPlayerWantsVolume_t{ NewVolume = d.NewVolume, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct MusicPlayerSelectsQueueEntry_t : Steamworks.ISteamCallback
	{
		internal int NID; // nID int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMusic + 12;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(MusicPlayerSelectsQueueEntry_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((MusicPlayerSelectsQueueEntry_t)(MusicPlayerSelectsQueueEntry_t) Marshal.PtrToStructure( p, typeof(MusicPlayerSelectsQueueEntry_t) )) : ((MusicPlayerSelectsQueueEntry_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal int NID; // nID int
			
			public static implicit operator MusicPlayerSelectsQueueEntry_t ( MusicPlayerSelectsQueueEntry_t.Pack8 d ) => new MusicPlayerSelectsQueueEntry_t{ NID = d.NID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct MusicPlayerSelectsPlaylistEntry_t : Steamworks.ISteamCallback
	{
		internal int NID; // nID int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMusic + 13;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(MusicPlayerSelectsPlaylistEntry_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((MusicPlayerSelectsPlaylistEntry_t)(MusicPlayerSelectsPlaylistEntry_t) Marshal.PtrToStructure( p, typeof(MusicPlayerSelectsPlaylistEntry_t) )) : ((MusicPlayerSelectsPlaylistEntry_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal int NID; // nID int
			
			public static implicit operator MusicPlayerSelectsPlaylistEntry_t ( MusicPlayerSelectsPlaylistEntry_t.Pack8 d ) => new MusicPlayerSelectsPlaylistEntry_t{ NID = d.NID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct MusicPlayerWantsPlayingRepeatStatus_t : Steamworks.ISteamCallback
	{
		internal int PlayingRepeatStatus; // m_nPlayingRepeatStatus int
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMusicRemote + 14;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(MusicPlayerWantsPlayingRepeatStatus_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((MusicPlayerWantsPlayingRepeatStatus_t)(MusicPlayerWantsPlayingRepeatStatus_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsPlayingRepeatStatus_t) )) : ((MusicPlayerWantsPlayingRepeatStatus_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal int PlayingRepeatStatus; // m_nPlayingRepeatStatus int
			
			public static implicit operator MusicPlayerWantsPlayingRepeatStatus_t ( MusicPlayerWantsPlayingRepeatStatus_t.Pack8 d ) => new MusicPlayerWantsPlayingRepeatStatus_t{ PlayingRepeatStatus = d.PlayingRepeatStatus, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTTPRequestCompleted_t : Steamworks.ISteamCallback
	{
		internal uint Request; // m_hRequest HTTPRequestHandle
		internal ulong ContextValue; // m_ulContextValue uint64
		[MarshalAs(UnmanagedType.I1)]
		internal bool RequestSuccessful; // m_bRequestSuccessful _Bool
		internal HTTPStatusCode StatusCode; // m_eStatusCode enum EHTTPStatusCode
		internal uint BodySize; // m_unBodySize uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientHTTP + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTTPRequestCompleted_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTTPRequestCompleted_t)(HTTPRequestCompleted_t) Marshal.PtrToStructure( p, typeof(HTTPRequestCompleted_t) )) : ((HTTPRequestCompleted_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTTPRequestHeadersReceived_t : Steamworks.ISteamCallback
	{
		internal uint Request; // m_hRequest HTTPRequestHandle
		internal ulong ContextValue; // m_ulContextValue uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientHTTP + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTTPRequestHeadersReceived_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTTPRequestHeadersReceived_t)(HTTPRequestHeadersReceived_t) Marshal.PtrToStructure( p, typeof(HTTPRequestHeadersReceived_t) )) : ((HTTPRequestHeadersReceived_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint Request; // m_hRequest HTTPRequestHandle
			internal ulong ContextValue; // m_ulContextValue uint64
			
			public static implicit operator HTTPRequestHeadersReceived_t ( HTTPRequestHeadersReceived_t.Pack8 d ) => new HTTPRequestHeadersReceived_t{ Request = d.Request,ContextValue = d.ContextValue, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTTPRequestDataReceived_t : Steamworks.ISteamCallback
	{
		internal uint Request; // m_hRequest HTTPRequestHandle
		internal ulong ContextValue; // m_ulContextValue uint64
		internal uint COffset; // m_cOffset uint32
		internal uint CBytesReceived; // m_cBytesReceived uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientHTTP + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTTPRequestDataReceived_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTTPRequestDataReceived_t)(HTTPRequestDataReceived_t) Marshal.PtrToStructure( p, typeof(HTTPRequestDataReceived_t) )) : ((HTTPRequestDataReceived_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamUGCDetails_t
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		internal WorkshopFileType FileType; // m_eFileType enum EWorkshopFileType
		internal AppId CreatorAppID; // m_nCreatorAppID AppId_t
		internal AppId ConsumerAppID; // m_nConsumerAppID AppId_t
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamUGCDetails_t) : typeof(Pack8) );
		public SteamUGCDetails_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamUGCDetails_t)(SteamUGCDetails_t) Marshal.PtrToStructure( p, typeof(SteamUGCDetails_t) )) : ((SteamUGCDetails_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal Result Result; // m_eResult enum EResult
			internal WorkshopFileType FileType; // m_eFileType enum EWorkshopFileType
			internal AppId CreatorAppID; // m_nCreatorAppID AppId_t
			internal AppId ConsumerAppID; // m_nConsumerAppID AppId_t
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamUGCQueryCompleted_t : Steamworks.ISteamCallback
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamUGCQueryCompleted_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SteamUGCQueryCompleted_t)(SteamUGCQueryCompleted_t) Marshal.PtrToStructure( p, typeof(SteamUGCQueryCompleted_t) )) : ((SteamUGCQueryCompleted_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamUGCRequestUGCDetailsResult_t : Steamworks.ISteamCallback
	{
		internal SteamUGCDetails_t Details; // m_details struct SteamUGCDetails_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData; // m_bCachedData _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamUGCRequestUGCDetailsResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SteamUGCRequestUGCDetailsResult_t)(SteamUGCRequestUGCDetailsResult_t) Marshal.PtrToStructure( p, typeof(SteamUGCRequestUGCDetailsResult_t) )) : ((SteamUGCRequestUGCDetailsResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct CreateItemResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(CreateItemResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((CreateItemResult_t)(CreateItemResult_t) Marshal.PtrToStructure( p, typeof(CreateItemResult_t) )) : ((CreateItemResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			[MarshalAs(UnmanagedType.I1)]
			internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			
			public static implicit operator CreateItemResult_t ( CreateItemResult_t.Pack8 d ) => new CreateItemResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SubmitItemUpdateResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 4;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SubmitItemUpdateResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SubmitItemUpdateResult_t)(SubmitItemUpdateResult_t) Marshal.PtrToStructure( p, typeof(SubmitItemUpdateResult_t) )) : ((SubmitItemUpdateResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator SubmitItemUpdateResult_t ( SubmitItemUpdateResult_t.Pack8 d ) => new SubmitItemUpdateResult_t{ Result = d.Result,UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement,PublishedFileId = d.PublishedFileId, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct DownloadItemResult_t : Steamworks.ISteamCallback
	{
		internal AppId AppID; // m_unAppID AppId_t
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 6;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(DownloadItemResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((DownloadItemResult_t)(DownloadItemResult_t) Marshal.PtrToStructure( p, typeof(DownloadItemResult_t) )) : ((DownloadItemResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal AppId AppID; // m_unAppID AppId_t
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator DownloadItemResult_t ( DownloadItemResult_t.Pack8 d ) => new DownloadItemResult_t{ AppID = d.AppID,PublishedFileId = d.PublishedFileId,Result = d.Result, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct UserFavoriteItemsListChanged_t : Steamworks.ISteamCallback
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool WasAddRequest; // m_bWasAddRequest _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 7;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(UserFavoriteItemsListChanged_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((UserFavoriteItemsListChanged_t)(UserFavoriteItemsListChanged_t) Marshal.PtrToStructure( p, typeof(UserFavoriteItemsListChanged_t) )) : ((UserFavoriteItemsListChanged_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			internal bool WasAddRequest; // m_bWasAddRequest _Bool
			
			public static implicit operator UserFavoriteItemsListChanged_t ( UserFavoriteItemsListChanged_t.Pack8 d ) => new UserFavoriteItemsListChanged_t{ PublishedFileId = d.PublishedFileId,Result = d.Result,WasAddRequest = d.WasAddRequest, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SetUserItemVoteResult_t : Steamworks.ISteamCallback
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool VoteUp; // m_bVoteUp _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 8;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SetUserItemVoteResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SetUserItemVoteResult_t)(SetUserItemVoteResult_t) Marshal.PtrToStructure( p, typeof(SetUserItemVoteResult_t) )) : ((SetUserItemVoteResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			internal bool VoteUp; // m_bVoteUp _Bool
			
			public static implicit operator SetUserItemVoteResult_t ( SetUserItemVoteResult_t.Pack8 d ) => new SetUserItemVoteResult_t{ PublishedFileId = d.PublishedFileId,Result = d.Result,VoteUp = d.VoteUp, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GetUserItemVoteResult_t : Steamworks.ISteamCallback
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool VotedUp; // m_bVotedUp _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool VotedDown; // m_bVotedDown _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool VoteSkipped; // m_bVoteSkipped _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 9;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GetUserItemVoteResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GetUserItemVoteResult_t)(GetUserItemVoteResult_t) Marshal.PtrToStructure( p, typeof(GetUserItemVoteResult_t) )) : ((GetUserItemVoteResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct StartPlaytimeTrackingResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 10;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(StartPlaytimeTrackingResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((StartPlaytimeTrackingResult_t)(StartPlaytimeTrackingResult_t) Marshal.PtrToStructure( p, typeof(StartPlaytimeTrackingResult_t) )) : ((StartPlaytimeTrackingResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator StartPlaytimeTrackingResult_t ( StartPlaytimeTrackingResult_t.Pack8 d ) => new StartPlaytimeTrackingResult_t{ Result = d.Result, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct StopPlaytimeTrackingResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 11;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(StopPlaytimeTrackingResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((StopPlaytimeTrackingResult_t)(StopPlaytimeTrackingResult_t) Marshal.PtrToStructure( p, typeof(StopPlaytimeTrackingResult_t) )) : ((StopPlaytimeTrackingResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator StopPlaytimeTrackingResult_t ( StopPlaytimeTrackingResult_t.Pack8 d ) => new StopPlaytimeTrackingResult_t{ Result = d.Result, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct AddUGCDependencyResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal PublishedFileId ChildPublishedFileId; // m_nChildPublishedFileId PublishedFileId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 12;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(AddUGCDependencyResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((AddUGCDependencyResult_t)(AddUGCDependencyResult_t) Marshal.PtrToStructure( p, typeof(AddUGCDependencyResult_t) )) : ((AddUGCDependencyResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal PublishedFileId ChildPublishedFileId; // m_nChildPublishedFileId PublishedFileId_t
			
			public static implicit operator AddUGCDependencyResult_t ( AddUGCDependencyResult_t.Pack8 d ) => new AddUGCDependencyResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,ChildPublishedFileId = d.ChildPublishedFileId, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoveUGCDependencyResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal PublishedFileId ChildPublishedFileId; // m_nChildPublishedFileId PublishedFileId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 13;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoveUGCDependencyResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoveUGCDependencyResult_t)(RemoveUGCDependencyResult_t) Marshal.PtrToStructure( p, typeof(RemoveUGCDependencyResult_t) )) : ((RemoveUGCDependencyResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal PublishedFileId ChildPublishedFileId; // m_nChildPublishedFileId PublishedFileId_t
			
			public static implicit operator RemoveUGCDependencyResult_t ( RemoveUGCDependencyResult_t.Pack8 d ) => new RemoveUGCDependencyResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,ChildPublishedFileId = d.ChildPublishedFileId, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct AddAppDependencyResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 14;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(AddAppDependencyResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((AddAppDependencyResult_t)(AddAppDependencyResult_t) Marshal.PtrToStructure( p, typeof(AddAppDependencyResult_t) )) : ((AddAppDependencyResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal AppId AppID; // m_nAppID AppId_t
			
			public static implicit operator AddAppDependencyResult_t ( AddAppDependencyResult_t.Pack8 d ) => new AddAppDependencyResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,AppID = d.AppID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RemoveAppDependencyResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 15;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoveAppDependencyResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((RemoveAppDependencyResult_t)(RemoveAppDependencyResult_t) Marshal.PtrToStructure( p, typeof(RemoveAppDependencyResult_t) )) : ((RemoveAppDependencyResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			internal AppId AppID; // m_nAppID AppId_t
			
			public static implicit operator RemoveAppDependencyResult_t ( RemoveAppDependencyResult_t.Pack8 d ) => new RemoveAppDependencyResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,AppID = d.AppID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GetAppDependenciesResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.U4)]
		internal AppId[] GAppIDs; // m_rgAppIDs AppId_t [32]
		internal uint NumAppDependencies; // m_nNumAppDependencies uint32
		internal uint TotalNumAppDependencies; // m_nTotalNumAppDependencies uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 16;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GetAppDependenciesResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GetAppDependenciesResult_t)(GetAppDependenciesResult_t) Marshal.PtrToStructure( p, typeof(GetAppDependenciesResult_t) )) : ((GetAppDependenciesResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.U4)]
			internal AppId[] GAppIDs; // m_rgAppIDs AppId_t [32]
			internal uint NumAppDependencies; // m_nNumAppDependencies uint32
			internal uint TotalNumAppDependencies; // m_nTotalNumAppDependencies uint32
			
			public static implicit operator GetAppDependenciesResult_t ( GetAppDependenciesResult_t.Pack8 d ) => new GetAppDependenciesResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId,GAppIDs = d.GAppIDs,NumAppDependencies = d.NumAppDependencies,TotalNumAppDependencies = d.TotalNumAppDependencies, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct DeleteItemResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 17;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(DeleteItemResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((DeleteItemResult_t)(DeleteItemResult_t) Marshal.PtrToStructure( p, typeof(DeleteItemResult_t) )) : ((DeleteItemResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator DeleteItemResult_t ( DeleteItemResult_t.Pack8 d ) => new DeleteItemResult_t{ Result = d.Result,PublishedFileId = d.PublishedFileId, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamAppInstalled_t : Steamworks.ISteamCallback
	{
		internal AppId AppID; // m_nAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamAppList + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamAppInstalled_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SteamAppInstalled_t)(SteamAppInstalled_t) Marshal.PtrToStructure( p, typeof(SteamAppInstalled_t) )) : ((SteamAppInstalled_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal AppId AppID; // m_nAppID AppId_t
			
			public static implicit operator SteamAppInstalled_t ( SteamAppInstalled_t.Pack8 d ) => new SteamAppInstalled_t{ AppID = d.AppID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamAppUninstalled_t : Steamworks.ISteamCallback
	{
		internal AppId AppID; // m_nAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamAppList + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamAppUninstalled_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SteamAppUninstalled_t)(SteamAppUninstalled_t) Marshal.PtrToStructure( p, typeof(SteamAppUninstalled_t) )) : ((SteamAppUninstalled_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal AppId AppID; // m_nAppID AppId_t
			
			public static implicit operator SteamAppUninstalled_t ( SteamAppUninstalled_t.Pack8 d ) => new SteamAppUninstalled_t{ AppID = d.AppID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_BrowserReady_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_BrowserReady_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_BrowserReady_t)(HTML_BrowserReady_t) Marshal.PtrToStructure( p, typeof(HTML_BrowserReady_t) )) : ((HTML_BrowserReady_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			
			public static implicit operator HTML_BrowserReady_t ( HTML_BrowserReady_t.Pack8 d ) => new HTML_BrowserReady_t{ UnBrowserHandle = d.UnBrowserHandle, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_NeedsPaint_t
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_NeedsPaint_t) : typeof(Pack8) );
		public HTML_NeedsPaint_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_NeedsPaint_t)(HTML_NeedsPaint_t) Marshal.PtrToStructure( p, typeof(HTML_NeedsPaint_t) )) : ((HTML_NeedsPaint_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_StartRequest_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		internal string PchTarget; // pchTarget const char *
		internal string PchPostData; // pchPostData const char *
		[MarshalAs(UnmanagedType.I1)]
		internal bool BIsRedirect; // bIsRedirect _Bool
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_StartRequest_t) : typeof(Pack8) );
		public HTML_StartRequest_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_StartRequest_t)(HTML_StartRequest_t) Marshal.PtrToStructure( p, typeof(HTML_StartRequest_t) )) : ((HTML_StartRequest_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_CloseBrowser_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_CloseBrowser_t) : typeof(Pack8) );
		public HTML_CloseBrowser_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_CloseBrowser_t)(HTML_CloseBrowser_t) Marshal.PtrToStructure( p, typeof(HTML_CloseBrowser_t) )) : ((HTML_CloseBrowser_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			
			public static implicit operator HTML_CloseBrowser_t ( HTML_CloseBrowser_t.Pack8 d ) => new HTML_CloseBrowser_t{ UnBrowserHandle = d.UnBrowserHandle, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_URLChanged_t : Steamworks.ISteamCallback
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_URLChanged_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_URLChanged_t)(HTML_URLChanged_t) Marshal.PtrToStructure( p, typeof(HTML_URLChanged_t) )) : ((HTML_URLChanged_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_FinishedRequest_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		internal string PchPageTitle; // pchPageTitle const char *
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 6;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_FinishedRequest_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_FinishedRequest_t)(HTML_FinishedRequest_t) Marshal.PtrToStructure( p, typeof(HTML_FinishedRequest_t) )) : ((HTML_FinishedRequest_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_OpenLinkInNewTab_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 7;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_OpenLinkInNewTab_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_OpenLinkInNewTab_t)(HTML_OpenLinkInNewTab_t) Marshal.PtrToStructure( p, typeof(HTML_OpenLinkInNewTab_t) )) : ((HTML_OpenLinkInNewTab_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchURL; // pchURL const char *
			
			public static implicit operator HTML_OpenLinkInNewTab_t ( HTML_OpenLinkInNewTab_t.Pack8 d ) => new HTML_OpenLinkInNewTab_t{ UnBrowserHandle = d.UnBrowserHandle,PchURL = d.PchURL, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_ChangedTitle_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchTitle; // pchTitle const char *
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 8;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_ChangedTitle_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_ChangedTitle_t)(HTML_ChangedTitle_t) Marshal.PtrToStructure( p, typeof(HTML_ChangedTitle_t) )) : ((HTML_ChangedTitle_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchTitle; // pchTitle const char *
			
			public static implicit operator HTML_ChangedTitle_t ( HTML_ChangedTitle_t.Pack8 d ) => new HTML_ChangedTitle_t{ UnBrowserHandle = d.UnBrowserHandle,PchTitle = d.PchTitle, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_SearchResults_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint UnResults; // unResults uint32
		internal uint UnCurrentMatch; // unCurrentMatch uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 9;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_SearchResults_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_SearchResults_t)(HTML_SearchResults_t) Marshal.PtrToStructure( p, typeof(HTML_SearchResults_t) )) : ((HTML_SearchResults_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_CanGoBackAndForward_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		[MarshalAs(UnmanagedType.I1)]
		internal bool BCanGoBack; // bCanGoBack _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool BCanGoForward; // bCanGoForward _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 10;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_CanGoBackAndForward_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_CanGoBackAndForward_t)(HTML_CanGoBackAndForward_t) Marshal.PtrToStructure( p, typeof(HTML_CanGoBackAndForward_t) )) : ((HTML_CanGoBackAndForward_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_HorizontalScroll_t : Steamworks.ISteamCallback
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_HorizontalScroll_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_HorizontalScroll_t)(HTML_HorizontalScroll_t) Marshal.PtrToStructure( p, typeof(HTML_HorizontalScroll_t) )) : ((HTML_HorizontalScroll_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_VerticalScroll_t : Steamworks.ISteamCallback
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_VerticalScroll_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_VerticalScroll_t)(HTML_VerticalScroll_t) Marshal.PtrToStructure( p, typeof(HTML_VerticalScroll_t) )) : ((HTML_VerticalScroll_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_LinkAtPosition_t : Steamworks.ISteamCallback
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_LinkAtPosition_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_LinkAtPosition_t)(HTML_LinkAtPosition_t) Marshal.PtrToStructure( p, typeof(HTML_LinkAtPosition_t) )) : ((HTML_LinkAtPosition_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_JSAlert_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMessage; // pchMessage const char *
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 14;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_JSAlert_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_JSAlert_t)(HTML_JSAlert_t) Marshal.PtrToStructure( p, typeof(HTML_JSAlert_t) )) : ((HTML_JSAlert_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchMessage; // pchMessage const char *
			
			public static implicit operator HTML_JSAlert_t ( HTML_JSAlert_t.Pack8 d ) => new HTML_JSAlert_t{ UnBrowserHandle = d.UnBrowserHandle,PchMessage = d.PchMessage, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_JSConfirm_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMessage; // pchMessage const char *
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 15;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_JSConfirm_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_JSConfirm_t)(HTML_JSConfirm_t) Marshal.PtrToStructure( p, typeof(HTML_JSConfirm_t) )) : ((HTML_JSConfirm_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchMessage; // pchMessage const char *
			
			public static implicit operator HTML_JSConfirm_t ( HTML_JSConfirm_t.Pack8 d ) => new HTML_JSConfirm_t{ UnBrowserHandle = d.UnBrowserHandle,PchMessage = d.PchMessage, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_FileOpenDialog_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchTitle; // pchTitle const char *
		internal string PchInitialFile; // pchInitialFile const char *
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 16;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_FileOpenDialog_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_FileOpenDialog_t)(HTML_FileOpenDialog_t) Marshal.PtrToStructure( p, typeof(HTML_FileOpenDialog_t) )) : ((HTML_FileOpenDialog_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_NewWindow_t : Steamworks.ISteamCallback
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_NewWindow_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_NewWindow_t)(HTML_NewWindow_t) Marshal.PtrToStructure( p, typeof(HTML_NewWindow_t) )) : ((HTML_NewWindow_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_SetCursor_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint EMouseCursor; // eMouseCursor uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 22;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_SetCursor_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_SetCursor_t)(HTML_SetCursor_t) Marshal.PtrToStructure( p, typeof(HTML_SetCursor_t) )) : ((HTML_SetCursor_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal uint EMouseCursor; // eMouseCursor uint32
			
			public static implicit operator HTML_SetCursor_t ( HTML_SetCursor_t.Pack8 d ) => new HTML_SetCursor_t{ UnBrowserHandle = d.UnBrowserHandle,EMouseCursor = d.EMouseCursor, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_StatusText_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMsg; // pchMsg const char *
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 23;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_StatusText_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_StatusText_t)(HTML_StatusText_t) Marshal.PtrToStructure( p, typeof(HTML_StatusText_t) )) : ((HTML_StatusText_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchMsg; // pchMsg const char *
			
			public static implicit operator HTML_StatusText_t ( HTML_StatusText_t.Pack8 d ) => new HTML_StatusText_t{ UnBrowserHandle = d.UnBrowserHandle,PchMsg = d.PchMsg, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_ShowToolTip_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMsg; // pchMsg const char *
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 24;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_ShowToolTip_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_ShowToolTip_t)(HTML_ShowToolTip_t) Marshal.PtrToStructure( p, typeof(HTML_ShowToolTip_t) )) : ((HTML_ShowToolTip_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchMsg; // pchMsg const char *
			
			public static implicit operator HTML_ShowToolTip_t ( HTML_ShowToolTip_t.Pack8 d ) => new HTML_ShowToolTip_t{ UnBrowserHandle = d.UnBrowserHandle,PchMsg = d.PchMsg, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_UpdateToolTip_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMsg; // pchMsg const char *
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 25;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_UpdateToolTip_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_UpdateToolTip_t)(HTML_UpdateToolTip_t) Marshal.PtrToStructure( p, typeof(HTML_UpdateToolTip_t) )) : ((HTML_UpdateToolTip_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal string PchMsg; // pchMsg const char *
			
			public static implicit operator HTML_UpdateToolTip_t ( HTML_UpdateToolTip_t.Pack8 d ) => new HTML_UpdateToolTip_t{ UnBrowserHandle = d.UnBrowserHandle,PchMsg = d.PchMsg, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_HideToolTip_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 26;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_HideToolTip_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_HideToolTip_t)(HTML_HideToolTip_t) Marshal.PtrToStructure( p, typeof(HTML_HideToolTip_t) )) : ((HTML_HideToolTip_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			
			public static implicit operator HTML_HideToolTip_t ( HTML_HideToolTip_t.Pack8 d ) => new HTML_HideToolTip_t{ UnBrowserHandle = d.UnBrowserHandle, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct HTML_BrowserRestarted_t : Steamworks.ISteamCallback
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint UnOldBrowserHandle; // unOldBrowserHandle HHTMLBrowser
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamHTMLSurface + 27;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_BrowserRestarted_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((HTML_BrowserRestarted_t)(HTML_BrowserRestarted_t) Marshal.PtrToStructure( p, typeof(HTML_BrowserRestarted_t) )) : ((HTML_BrowserRestarted_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			internal uint UnOldBrowserHandle; // unOldBrowserHandle HHTMLBrowser
			
			public static implicit operator HTML_BrowserRestarted_t ( HTML_BrowserRestarted_t.Pack8 d ) => new HTML_BrowserRestarted_t{ UnBrowserHandle = d.UnBrowserHandle,UnOldBrowserHandle = d.UnOldBrowserHandle, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamItemDetails_t
	{
		internal InventoryItemId ItemId; // m_itemId SteamItemInstanceID_t
		internal InventoryDefId Definition; // m_iDefinition SteamItemDef_t
		internal ushort Quantity; // m_unQuantity uint16
		internal ushort Flags; // m_unFlags uint16
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamItemDetails_t) : typeof(Pack8) );
		public SteamItemDetails_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamItemDetails_t)(SteamItemDetails_t) Marshal.PtrToStructure( p, typeof(SteamItemDetails_t) )) : ((SteamItemDetails_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal InventoryItemId ItemId; // m_itemId SteamItemInstanceID_t
			internal InventoryDefId Definition; // m_iDefinition SteamItemDef_t
			internal ushort Quantity; // m_unQuantity uint16
			internal ushort Flags; // m_unFlags uint16
			
			public static implicit operator SteamItemDetails_t ( SteamItemDetails_t.Pack8 d ) => new SteamItemDetails_t{ ItemId = d.ItemId,Definition = d.Definition,Quantity = d.Quantity,Flags = d.Flags, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamInventoryResultReady_t : Steamworks.ISteamCallback
	{
		internal int Handle; // m_handle SteamInventoryResult_t
		internal Result Result; // m_result enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientInventory + 0;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamInventoryResultReady_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SteamInventoryResultReady_t)(SteamInventoryResultReady_t) Marshal.PtrToStructure( p, typeof(SteamInventoryResultReady_t) )) : ((SteamInventoryResultReady_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal int Handle; // m_handle SteamInventoryResult_t
			internal Result Result; // m_result enum EResult
			
			public static implicit operator SteamInventoryResultReady_t ( SteamInventoryResultReady_t.Pack8 d ) => new SteamInventoryResultReady_t{ Handle = d.Handle,Result = d.Result, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamInventoryFullUpdate_t : Steamworks.ISteamCallback
	{
		internal int Handle; // m_handle SteamInventoryResult_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientInventory + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamInventoryFullUpdate_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SteamInventoryFullUpdate_t)(SteamInventoryFullUpdate_t) Marshal.PtrToStructure( p, typeof(SteamInventoryFullUpdate_t) )) : ((SteamInventoryFullUpdate_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal int Handle; // m_handle SteamInventoryResult_t
			
			public static implicit operator SteamInventoryFullUpdate_t ( SteamInventoryFullUpdate_t.Pack8 d ) => new SteamInventoryFullUpdate_t{ Handle = d.Handle, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamInventoryEligiblePromoItemDefIDs_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_result enum EResult
		internal ulong SteamID; // m_steamID class CSteamID
		internal int UmEligiblePromoItemDefs; // m_numEligiblePromoItemDefs int
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData; // m_bCachedData _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientInventory + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamInventoryEligiblePromoItemDefIDs_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SteamInventoryEligiblePromoItemDefIDs_t)(SteamInventoryEligiblePromoItemDefIDs_t) Marshal.PtrToStructure( p, typeof(SteamInventoryEligiblePromoItemDefIDs_t) )) : ((SteamInventoryEligiblePromoItemDefIDs_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamInventoryStartPurchaseResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_result enum EResult
		internal ulong OrderID; // m_ulOrderID uint64
		internal ulong TransID; // m_ulTransID uint64
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientInventory + 4;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamInventoryStartPurchaseResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SteamInventoryStartPurchaseResult_t)(SteamInventoryStartPurchaseResult_t) Marshal.PtrToStructure( p, typeof(SteamInventoryStartPurchaseResult_t) )) : ((SteamInventoryStartPurchaseResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamInventoryRequestPricesResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_result enum EResult
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
		internal string Currency; // m_rgchCurrency char [4]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientInventory + 5;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamInventoryRequestPricesResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SteamInventoryRequestPricesResult_t)(SteamInventoryRequestPricesResult_t) Marshal.PtrToStructure( p, typeof(SteamInventoryRequestPricesResult_t) )) : ((SteamInventoryRequestPricesResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct BroadcastUploadStop_t : Steamworks.ISteamCallback
	{
		internal BroadcastUploadResult Result; // m_eResult enum EBroadcastUploadResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientVideo + 5;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(BroadcastUploadStop_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((BroadcastUploadStop_t)(BroadcastUploadStop_t) Marshal.PtrToStructure( p, typeof(BroadcastUploadStop_t) )) : ((BroadcastUploadStop_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal BroadcastUploadResult Result; // m_eResult enum EBroadcastUploadResult
			
			public static implicit operator BroadcastUploadStop_t ( BroadcastUploadStop_t.Pack8 d ) => new BroadcastUploadStop_t{ Result = d.Result, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GetVideoURLResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal AppId VideoAppID; // m_unVideoAppID AppId_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string URL; // m_rgchURL char [256]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientVideo + 11;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GetVideoURLResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GetVideoURLResult_t)(GetVideoURLResult_t) Marshal.PtrToStructure( p, typeof(GetVideoURLResult_t) )) : ((GetVideoURLResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal AppId VideoAppID; // m_unVideoAppID AppId_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string URL; // m_rgchURL char [256]
			
			public static implicit operator GetVideoURLResult_t ( GetVideoURLResult_t.Pack8 d ) => new GetVideoURLResult_t{ Result = d.Result,VideoAppID = d.VideoAppID,URL = d.URL, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GetOPFSettingsResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal AppId VideoAppID; // m_unVideoAppID AppId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientVideo + 24;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GetOPFSettingsResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GetOPFSettingsResult_t)(GetOPFSettingsResult_t) Marshal.PtrToStructure( p, typeof(GetOPFSettingsResult_t) )) : ((GetOPFSettingsResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal AppId VideoAppID; // m_unVideoAppID AppId_t
			
			public static implicit operator GetOPFSettingsResult_t ( GetOPFSettingsResult_t.Pack8 d ) => new GetOPFSettingsResult_t{ Result = d.Result,VideoAppID = d.VideoAppID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSClientApprove_t : Steamworks.ISteamCallback
	{
		internal ulong SteamID; // m_SteamID class CSteamID
		internal ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServer + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GSClientApprove_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GSClientApprove_t)(GSClientApprove_t) Marshal.PtrToStructure( p, typeof(GSClientApprove_t) )) : ((GSClientApprove_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamID; // m_SteamID class CSteamID
			internal ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
			
			public static implicit operator GSClientApprove_t ( GSClientApprove_t.Pack8 d ) => new GSClientApprove_t{ SteamID = d.SteamID,OwnerSteamID = d.OwnerSteamID, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSClientDeny_t : Steamworks.ISteamCallback
	{
		internal ulong SteamID; // m_SteamID class CSteamID
		internal DenyReason DenyReason; // m_eDenyReason enum EDenyReason
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string OptionalText; // m_rgchOptionalText char [128]
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServer + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GSClientDeny_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GSClientDeny_t)(GSClientDeny_t) Marshal.PtrToStructure( p, typeof(GSClientDeny_t) )) : ((GSClientDeny_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSClientKick_t : Steamworks.ISteamCallback
	{
		internal ulong SteamID; // m_SteamID class CSteamID
		internal DenyReason DenyReason; // m_eDenyReason enum EDenyReason
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServer + 3;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GSClientKick_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GSClientKick_t)(GSClientKick_t) Marshal.PtrToStructure( p, typeof(GSClientKick_t) )) : ((GSClientKick_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamID; // m_SteamID class CSteamID
			internal DenyReason DenyReason; // m_eDenyReason enum EDenyReason
			
			public static implicit operator GSClientKick_t ( GSClientKick_t.Pack8 d ) => new GSClientKick_t{ SteamID = d.SteamID,DenyReason = d.DenyReason, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSClientAchievementStatus_t : Steamworks.ISteamCallback
	{
		internal ulong SteamID; // m_SteamID uint64
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string PchAchievement; // m_pchAchievement char [128]
		[MarshalAs(UnmanagedType.I1)]
		internal bool Unlocked; // m_bUnlocked _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServer + 6;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GSClientAchievementStatus_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GSClientAchievementStatus_t)(GSClientAchievementStatus_t) Marshal.PtrToStructure( p, typeof(GSClientAchievementStatus_t) )) : ((GSClientAchievementStatus_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSPolicyResponse_t : Steamworks.ISteamCallback
	{
		internal byte Secure; // m_bSecure uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 15;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GSPolicyResponse_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GSPolicyResponse_t)(GSPolicyResponse_t) Marshal.PtrToStructure( p, typeof(GSPolicyResponse_t) )) : ((GSPolicyResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal byte Secure; // m_bSecure uint8
			
			public static implicit operator GSPolicyResponse_t ( GSPolicyResponse_t.Pack8 d ) => new GSPolicyResponse_t{ Secure = d.Secure, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSGameplayStats_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal int Rank; // m_nRank int32
		internal uint TotalConnects; // m_unTotalConnects uint32
		internal uint TotalMinutesPlayed; // m_unTotalMinutesPlayed uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServer + 7;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GSGameplayStats_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GSGameplayStats_t)(GSGameplayStats_t) Marshal.PtrToStructure( p, typeof(GSGameplayStats_t) )) : ((GSGameplayStats_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSClientGroupStatus_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDUser; // m_SteamIDUser class CSteamID
		internal ulong SteamIDGroup; // m_SteamIDGroup class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool Member; // m_bMember _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool Officer; // m_bOfficer _Bool
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServer + 8;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GSClientGroupStatus_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GSClientGroupStatus_t)(GSClientGroupStatus_t) Marshal.PtrToStructure( p, typeof(GSClientGroupStatus_t) )) : ((GSClientGroupStatus_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSReputation_t : Steamworks.ISteamCallback
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GSReputation_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GSReputation_t)(GSReputation_t) Marshal.PtrToStructure( p, typeof(GSReputation_t) )) : ((GSReputation_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct AssociateWithClanResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServer + 10;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(AssociateWithClanResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((AssociateWithClanResult_t)(AssociateWithClanResult_t) Marshal.PtrToStructure( p, typeof(AssociateWithClanResult_t) )) : ((AssociateWithClanResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			
			public static implicit operator AssociateWithClanResult_t ( AssociateWithClanResult_t.Pack8 d ) => new AssociateWithClanResult_t{ Result = d.Result, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct ComputeNewPlayerCompatibilityResult_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal int CPlayersThatDontLikeCandidate; // m_cPlayersThatDontLikeCandidate int
		internal int CPlayersThatCandidateDoesntLike; // m_cPlayersThatCandidateDoesntLike int
		internal int CClanPlayersThatDontLikeCandidate; // m_cClanPlayersThatDontLikeCandidate int
		internal ulong SteamIDCandidate; // m_SteamIDCandidate class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServer + 11;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(ComputeNewPlayerCompatibilityResult_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((ComputeNewPlayerCompatibilityResult_t)(ComputeNewPlayerCompatibilityResult_t) Marshal.PtrToStructure( p, typeof(ComputeNewPlayerCompatibilityResult_t) )) : ((ComputeNewPlayerCompatibilityResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSStatsReceived_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServerStats + 0;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GSStatsReceived_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GSStatsReceived_t)(GSStatsReceived_t) Marshal.PtrToStructure( p, typeof(GSStatsReceived_t) )) : ((GSStatsReceived_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator GSStatsReceived_t ( GSStatsReceived_t.Pack8 d ) => new GSStatsReceived_t{ Result = d.Result,SteamIDUser = d.SteamIDUser, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSStatsStored_t : Steamworks.ISteamCallback
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameServerStats + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GSStatsStored_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GSStatsStored_t)(GSStatsStored_t) Marshal.PtrToStructure( p, typeof(GSStatsStored_t) )) : ((GSStatsStored_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal Result Result; // m_eResult enum EResult
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator GSStatsStored_t ( GSStatsStored_t.Pack8 d ) => new GSStatsStored_t{ Result = d.Result,SteamIDUser = d.SteamIDUser, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSStatsUnloaded_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 8;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GSStatsUnloaded_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GSStatsUnloaded_t)(GSStatsUnloaded_t) Marshal.PtrToStructure( p, typeof(GSStatsUnloaded_t) )) : ((GSStatsUnloaded_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		public struct Pack8
		{
			internal ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			public static implicit operator GSStatsUnloaded_t ( GSStatsUnloaded_t.Pack8 d ) => new GSStatsUnloaded_t{ SteamIDUser = d.SteamIDUser, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct PlaybackStatusHasChanged_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamMusic + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(PlaybackStatusHasChanged_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((PlaybackStatusHasChanged_t)(PlaybackStatusHasChanged_t) Marshal.PtrToStructure( p, typeof(PlaybackStatusHasChanged_t) )) : ((PlaybackStatusHasChanged_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator PlaybackStatusHasChanged_t ( PlaybackStatusHasChanged_t.Pack8 d ) => new PlaybackStatusHasChanged_t{  };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct BroadcastUploadStart_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientVideo + 4;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(BroadcastUploadStart_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((BroadcastUploadStart_t)(BroadcastUploadStart_t) Marshal.PtrToStructure( p, typeof(BroadcastUploadStart_t) )) : ((BroadcastUploadStart_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator BroadcastUploadStart_t ( BroadcastUploadStart_t.Pack8 d ) => new BroadcastUploadStart_t{  };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct NewUrlLaunchParameters_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamApps + 14;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(NewUrlLaunchParameters_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((NewUrlLaunchParameters_t)(NewUrlLaunchParameters_t) Marshal.PtrToStructure( p, typeof(NewUrlLaunchParameters_t) )) : ((NewUrlLaunchParameters_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator NewUrlLaunchParameters_t ( NewUrlLaunchParameters_t.Pack8 d ) => new NewUrlLaunchParameters_t{  };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct ItemInstalled_t : Steamworks.ISteamCallback
	{
		internal AppId AppID; // m_unAppID AppId_t
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientUGC + 5;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(ItemInstalled_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((ItemInstalled_t)(ItemInstalled_t) Marshal.PtrToStructure( p, typeof(ItemInstalled_t) )) : ((ItemInstalled_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal AppId AppID; // m_unAppID AppId_t
			internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			public static implicit operator ItemInstalled_t ( ItemInstalled_t.Pack8 d ) => new ItemInstalled_t{ AppID = d.AppID,PublishedFileId = d.PublishedFileId, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct InputAnalogActionData_t
	{
		internal InputSourceMode EMode; // eMode EInputSourceMode
		internal float X; // x float
		internal float Y; // y float
		[MarshalAs(UnmanagedType.I1)]
		internal bool BActive; // bActive bool
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(InputAnalogActionData_t) : typeof(Pack8) );
		public InputAnalogActionData_t Fill( IntPtr p ) => Config.PackSmall ? ((InputAnalogActionData_t)(InputAnalogActionData_t) Marshal.PtrToStructure( p, typeof(InputAnalogActionData_t) )) : ((InputAnalogActionData_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct InputMotionData_t
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(InputMotionData_t) : typeof(Pack8) );
		public InputMotionData_t Fill( IntPtr p ) => Config.PackSmall ? ((InputMotionData_t)(InputMotionData_t) Marshal.PtrToStructure( p, typeof(InputMotionData_t) )) : ((InputMotionData_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct InputDigitalActionData_t
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool BState; // bState bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool BActive; // bActive bool
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(InputDigitalActionData_t) : typeof(Pack8) );
		public InputDigitalActionData_t Fill( IntPtr p ) => Config.PackSmall ? ((InputDigitalActionData_t)(InputDigitalActionData_t) Marshal.PtrToStructure( p, typeof(InputDigitalActionData_t) )) : ((InputDigitalActionData_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
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
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamInventoryDefinitionUpdate_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.ClientInventory + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamInventoryDefinitionUpdate_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SteamInventoryDefinitionUpdate_t)(SteamInventoryDefinitionUpdate_t) Marshal.PtrToStructure( p, typeof(SteamInventoryDefinitionUpdate_t) )) : ((SteamInventoryDefinitionUpdate_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator SteamInventoryDefinitionUpdate_t ( SteamInventoryDefinitionUpdate_t.Pack8 d ) => new SteamInventoryDefinitionUpdate_t{  };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamParentalSettingsChanged_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamParentalSettings + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamParentalSettingsChanged_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SteamParentalSettingsChanged_t)(SteamParentalSettingsChanged_t) Marshal.PtrToStructure( p, typeof(SteamParentalSettingsChanged_t) )) : ((SteamParentalSettingsChanged_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator SteamParentalSettingsChanged_t ( SteamParentalSettingsChanged_t.Pack8 d ) => new SteamParentalSettingsChanged_t{  };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamServersConnected_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamServersConnected_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SteamServersConnected_t)(SteamServersConnected_t) Marshal.PtrToStructure( p, typeof(SteamServersConnected_t) )) : ((SteamServersConnected_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator SteamServersConnected_t ( SteamServersConnected_t.Pack8 d ) => new SteamServersConnected_t{  };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct NewLaunchQueryParameters_t
	{
		
		#region Marshalling
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(NewLaunchQueryParameters_t) : typeof(Pack8) );
		public NewLaunchQueryParameters_t Fill( IntPtr p ) => Config.PackSmall ? ((NewLaunchQueryParameters_t)(NewLaunchQueryParameters_t) Marshal.PtrToStructure( p, typeof(NewLaunchQueryParameters_t) )) : ((NewLaunchQueryParameters_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator NewLaunchQueryParameters_t ( NewLaunchQueryParameters_t.Pack8 d ) => new NewLaunchQueryParameters_t{  };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GCMessageAvailable_t : Steamworks.ISteamCallback
	{
		internal uint MessageSize; // m_nMessageSize uint32
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameCoordinator + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GCMessageAvailable_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GCMessageAvailable_t)(GCMessageAvailable_t) Marshal.PtrToStructure( p, typeof(GCMessageAvailable_t) )) : ((GCMessageAvailable_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal uint MessageSize; // m_nMessageSize uint32
			
			public static implicit operator GCMessageAvailable_t ( GCMessageAvailable_t.Pack8 d ) => new GCMessageAvailable_t{ MessageSize = d.MessageSize, };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GCMessageFailed_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamGameCoordinator + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GCMessageFailed_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((GCMessageFailed_t)(GCMessageFailed_t) Marshal.PtrToStructure( p, typeof(GCMessageFailed_t) )) : ((GCMessageFailed_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator GCMessageFailed_t ( GCMessageFailed_t.Pack8 d ) => new GCMessageFailed_t{  };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct ScreenshotRequested_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamScreenshots + 2;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(ScreenshotRequested_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((ScreenshotRequested_t)(ScreenshotRequested_t) Marshal.PtrToStructure( p, typeof(ScreenshotRequested_t) )) : ((ScreenshotRequested_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator ScreenshotRequested_t ( ScreenshotRequested_t.Pack8 d ) => new ScreenshotRequested_t{  };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct LicensesUpdated_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 25;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LicensesUpdated_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((LicensesUpdated_t)(LicensesUpdated_t) Marshal.PtrToStructure( p, typeof(LicensesUpdated_t) )) : ((LicensesUpdated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator LicensesUpdated_t ( LicensesUpdated_t.Pack8 d ) => new LicensesUpdated_t{  };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamShutdown_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUtils + 4;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamShutdown_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((SteamShutdown_t)(SteamShutdown_t) Marshal.PtrToStructure( p, typeof(SteamShutdown_t) )) : ((SteamShutdown_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator SteamShutdown_t ( SteamShutdown_t.Pack8 d ) => new SteamShutdown_t{  };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct IPCountry_t : Steamworks.ISteamCallback
	{
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUtils + 1;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(IPCountry_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((IPCountry_t)(IPCountry_t) Marshal.PtrToStructure( p, typeof(IPCountry_t) )) : ((IPCountry_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator IPCountry_t ( IPCountry_t.Pack8 d ) => new IPCountry_t{  };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct IPCFailure_t : Steamworks.ISteamCallback
	{
		internal byte FailureType; // m_eFailureType uint8
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUser + 17;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(IPCFailure_t) : typeof(Pack8) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => Config.PackSmall ? ((IPCFailure_t)(IPCFailure_t) Marshal.PtrToStructure( p, typeof(IPCFailure_t) )) : ((IPCFailure_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal byte FailureType; // m_eFailureType uint8
			
			public static implicit operator IPCFailure_t ( IPCFailure_t.Pack8 d ) => new IPCFailure_t{ FailureType = d.FailureType, };
		}
		#endregion
	}
	
}
