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
		static Action<SteamServerConnectFailure_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SteamServerConnectFailure_t)default(SteamServerConnectFailure_t).Fill( pvParam ) );
		static Action<SteamServerConnectFailure_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SteamServerConnectFailure_t)default(SteamServerConnectFailure_t).Fill( pvParam ) );
		public static void Install( Action<SteamServerConnectFailure_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SteamServerConnectFailure_t).GetStructSize(), CallbackIdentifiers.SteamUser + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SteamServerConnectFailure_t).GetStructSize(), CallbackIdentifiers.SteamUser + 2, false );
				actionClient = action;
			}
		}
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
		static Action<SteamServersDisconnected_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SteamServersDisconnected_t)default(SteamServersDisconnected_t).Fill( pvParam ) );
		static Action<SteamServersDisconnected_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SteamServersDisconnected_t)default(SteamServersDisconnected_t).Fill( pvParam ) );
		public static void Install( Action<SteamServersDisconnected_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SteamServersDisconnected_t).GetStructSize(), CallbackIdentifiers.SteamUser + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SteamServersDisconnected_t).GetStructSize(), CallbackIdentifiers.SteamUser + 3, false );
				actionClient = action;
			}
		}
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
		static Action<ClientGameServerDeny_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (ClientGameServerDeny_t)default(ClientGameServerDeny_t).Fill( pvParam ) );
		static Action<ClientGameServerDeny_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (ClientGameServerDeny_t)default(ClientGameServerDeny_t).Fill( pvParam ) );
		public static void Install( Action<ClientGameServerDeny_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(ClientGameServerDeny_t).GetStructSize(), CallbackIdentifiers.SteamUser + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(ClientGameServerDeny_t).GetStructSize(), CallbackIdentifiers.SteamUser + 13, false );
				actionClient = action;
			}
		}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(ValidateAuthTicketResponse_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((ValidateAuthTicketResponse_t)(ValidateAuthTicketResponse_t) Marshal.PtrToStructure( p, typeof(ValidateAuthTicketResponse_t) ) );
		static Action<ValidateAuthTicketResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (ValidateAuthTicketResponse_t)default(ValidateAuthTicketResponse_t).Fill( pvParam ) );
		static Action<ValidateAuthTicketResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (ValidateAuthTicketResponse_t)default(ValidateAuthTicketResponse_t).Fill( pvParam ) );
		public static void Install( Action<ValidateAuthTicketResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(ValidateAuthTicketResponse_t).GetStructSize(), CallbackIdentifiers.SteamUser + 43, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(ValidateAuthTicketResponse_t).GetStructSize(), CallbackIdentifiers.SteamUser + 43, false );
				actionClient = action;
			}
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
		static Action<MicroTxnAuthorizationResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (MicroTxnAuthorizationResponse_t)default(MicroTxnAuthorizationResponse_t).Fill( pvParam ) );
		static Action<MicroTxnAuthorizationResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (MicroTxnAuthorizationResponse_t)default(MicroTxnAuthorizationResponse_t).Fill( pvParam ) );
		public static void Install( Action<MicroTxnAuthorizationResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(MicroTxnAuthorizationResponse_t).GetStructSize(), CallbackIdentifiers.SteamUser + 52, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(MicroTxnAuthorizationResponse_t).GetStructSize(), CallbackIdentifiers.SteamUser + 52, false );
				actionClient = action;
			}
		}
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
		static Action<EncryptedAppTicketResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (EncryptedAppTicketResponse_t)default(EncryptedAppTicketResponse_t).Fill( pvParam ) );
		static Action<EncryptedAppTicketResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (EncryptedAppTicketResponse_t)default(EncryptedAppTicketResponse_t).Fill( pvParam ) );
		public static void Install( Action<EncryptedAppTicketResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(EncryptedAppTicketResponse_t).GetStructSize(), CallbackIdentifiers.SteamUser + 54, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(EncryptedAppTicketResponse_t).GetStructSize(), CallbackIdentifiers.SteamUser + 54, false );
				actionClient = action;
			}
		}
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
		static Action<GetAuthSessionTicketResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GetAuthSessionTicketResponse_t)default(GetAuthSessionTicketResponse_t).Fill( pvParam ) );
		static Action<GetAuthSessionTicketResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GetAuthSessionTicketResponse_t)default(GetAuthSessionTicketResponse_t).Fill( pvParam ) );
		public static void Install( Action<GetAuthSessionTicketResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GetAuthSessionTicketResponse_t).GetStructSize(), CallbackIdentifiers.SteamUser + 63, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GetAuthSessionTicketResponse_t).GetStructSize(), CallbackIdentifiers.SteamUser + 63, false );
				actionClient = action;
			}
		}
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
		static Action<GameWebCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GameWebCallback_t)default(GameWebCallback_t).Fill( pvParam ) );
		static Action<GameWebCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GameWebCallback_t)default(GameWebCallback_t).Fill( pvParam ) );
		public static void Install( Action<GameWebCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GameWebCallback_t).GetStructSize(), CallbackIdentifiers.SteamUser + 64, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GameWebCallback_t).GetStructSize(), CallbackIdentifiers.SteamUser + 64, false );
				actionClient = action;
			}
		}
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
		static Action<StoreAuthURLResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (StoreAuthURLResponse_t)default(StoreAuthURLResponse_t).Fill( pvParam ) );
		static Action<StoreAuthURLResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (StoreAuthURLResponse_t)default(StoreAuthURLResponse_t).Fill( pvParam ) );
		public static void Install( Action<StoreAuthURLResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(StoreAuthURLResponse_t).GetStructSize(), CallbackIdentifiers.SteamUser + 65, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(StoreAuthURLResponse_t).GetStructSize(), CallbackIdentifiers.SteamUser + 65, false );
				actionClient = action;
			}
		}
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
		static Action<MarketEligibilityResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (MarketEligibilityResponse_t)default(MarketEligibilityResponse_t).Fill( pvParam ) );
		static Action<MarketEligibilityResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (MarketEligibilityResponse_t)default(MarketEligibilityResponse_t).Fill( pvParam ) );
		public static void Install( Action<MarketEligibilityResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(MarketEligibilityResponse_t).GetStructSize(), CallbackIdentifiers.SteamUser + 66, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(MarketEligibilityResponse_t).GetStructSize(), CallbackIdentifiers.SteamUser + 66, false );
				actionClient = action;
			}
		}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendGameInfo_t) );
		public FriendGameInfo_t Fill( IntPtr p ) => ((FriendGameInfo_t)(FriendGameInfo_t) Marshal.PtrToStructure( p, typeof(FriendGameInfo_t) ) );
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
		static Action<FriendStateChange_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (FriendStateChange_t)default(FriendStateChange_t).Fill( pvParam ) );
		static Action<FriendStateChange_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (FriendStateChange_t)default(FriendStateChange_t).Fill( pvParam ) );
		public static void Install( Action<FriendStateChange_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(FriendStateChange_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(FriendStateChange_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 4, false );
				actionClient = action;
			}
		}
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
		static Action<GameOverlayActivated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GameOverlayActivated_t)default(GameOverlayActivated_t).Fill( pvParam ) );
		static Action<GameOverlayActivated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GameOverlayActivated_t)default(GameOverlayActivated_t).Fill( pvParam ) );
		public static void Install( Action<GameOverlayActivated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GameOverlayActivated_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 31, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GameOverlayActivated_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 31, false );
				actionClient = action;
			}
		}
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
		static Action<GameServerChangeRequested_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GameServerChangeRequested_t)default(GameServerChangeRequested_t).Fill( pvParam ) );
		static Action<GameServerChangeRequested_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GameServerChangeRequested_t)default(GameServerChangeRequested_t).Fill( pvParam ) );
		public static void Install( Action<GameServerChangeRequested_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GameServerChangeRequested_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 32, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GameServerChangeRequested_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 32, false );
				actionClient = action;
			}
		}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameLobbyJoinRequested_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((GameLobbyJoinRequested_t)(GameLobbyJoinRequested_t) Marshal.PtrToStructure( p, typeof(GameLobbyJoinRequested_t) ) );
		static Action<GameLobbyJoinRequested_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GameLobbyJoinRequested_t)default(GameLobbyJoinRequested_t).Fill( pvParam ) );
		static Action<GameLobbyJoinRequested_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GameLobbyJoinRequested_t)default(GameLobbyJoinRequested_t).Fill( pvParam ) );
		public static void Install( Action<GameLobbyJoinRequested_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GameLobbyJoinRequested_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 33, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GameLobbyJoinRequested_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 33, false );
				actionClient = action;
			}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(AvatarImageLoaded_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((AvatarImageLoaded_t)(AvatarImageLoaded_t) Marshal.PtrToStructure( p, typeof(AvatarImageLoaded_t) ) );
		static Action<AvatarImageLoaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (AvatarImageLoaded_t)default(AvatarImageLoaded_t).Fill( pvParam ) );
		static Action<AvatarImageLoaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (AvatarImageLoaded_t)default(AvatarImageLoaded_t).Fill( pvParam ) );
		public static void Install( Action<AvatarImageLoaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(AvatarImageLoaded_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 34, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(AvatarImageLoaded_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 34, false );
				actionClient = action;
			}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(ClanOfficerListResponse_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((ClanOfficerListResponse_t)(ClanOfficerListResponse_t) Marshal.PtrToStructure( p, typeof(ClanOfficerListResponse_t) ) );
		static Action<ClanOfficerListResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (ClanOfficerListResponse_t)default(ClanOfficerListResponse_t).Fill( pvParam ) );
		static Action<ClanOfficerListResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (ClanOfficerListResponse_t)default(ClanOfficerListResponse_t).Fill( pvParam ) );
		public static void Install( Action<ClanOfficerListResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(ClanOfficerListResponse_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 35, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(ClanOfficerListResponse_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 35, false );
				actionClient = action;
			}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendRichPresenceUpdate_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((FriendRichPresenceUpdate_t)(FriendRichPresenceUpdate_t) Marshal.PtrToStructure( p, typeof(FriendRichPresenceUpdate_t) ) );
		static Action<FriendRichPresenceUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (FriendRichPresenceUpdate_t)default(FriendRichPresenceUpdate_t).Fill( pvParam ) );
		static Action<FriendRichPresenceUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (FriendRichPresenceUpdate_t)default(FriendRichPresenceUpdate_t).Fill( pvParam ) );
		public static void Install( Action<FriendRichPresenceUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(FriendRichPresenceUpdate_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 36, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(FriendRichPresenceUpdate_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 36, false );
				actionClient = action;
			}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameRichPresenceJoinRequested_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((GameRichPresenceJoinRequested_t)(GameRichPresenceJoinRequested_t) Marshal.PtrToStructure( p, typeof(GameRichPresenceJoinRequested_t) ) );
		static Action<GameRichPresenceJoinRequested_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GameRichPresenceJoinRequested_t)default(GameRichPresenceJoinRequested_t).Fill( pvParam ) );
		static Action<GameRichPresenceJoinRequested_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GameRichPresenceJoinRequested_t)default(GameRichPresenceJoinRequested_t).Fill( pvParam ) );
		public static void Install( Action<GameRichPresenceJoinRequested_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GameRichPresenceJoinRequested_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 37, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GameRichPresenceJoinRequested_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 37, false );
				actionClient = action;
			}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameConnectedClanChatMsg_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((GameConnectedClanChatMsg_t)(GameConnectedClanChatMsg_t) Marshal.PtrToStructure( p, typeof(GameConnectedClanChatMsg_t) ) );
		static Action<GameConnectedClanChatMsg_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GameConnectedClanChatMsg_t)default(GameConnectedClanChatMsg_t).Fill( pvParam ) );
		static Action<GameConnectedClanChatMsg_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GameConnectedClanChatMsg_t)default(GameConnectedClanChatMsg_t).Fill( pvParam ) );
		public static void Install( Action<GameConnectedClanChatMsg_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GameConnectedClanChatMsg_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 38, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GameConnectedClanChatMsg_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 38, false );
				actionClient = action;
			}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameConnectedChatJoin_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((GameConnectedChatJoin_t)(GameConnectedChatJoin_t) Marshal.PtrToStructure( p, typeof(GameConnectedChatJoin_t) ) );
		static Action<GameConnectedChatJoin_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GameConnectedChatJoin_t)default(GameConnectedChatJoin_t).Fill( pvParam ) );
		static Action<GameConnectedChatJoin_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GameConnectedChatJoin_t)default(GameConnectedChatJoin_t).Fill( pvParam ) );
		public static void Install( Action<GameConnectedChatJoin_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GameConnectedChatJoin_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 39, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GameConnectedChatJoin_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 39, false );
				actionClient = action;
			}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameConnectedChatLeave_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((GameConnectedChatLeave_t)(GameConnectedChatLeave_t) Marshal.PtrToStructure( p, typeof(GameConnectedChatLeave_t) ) );
		static Action<GameConnectedChatLeave_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GameConnectedChatLeave_t)default(GameConnectedChatLeave_t).Fill( pvParam ) );
		static Action<GameConnectedChatLeave_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GameConnectedChatLeave_t)default(GameConnectedChatLeave_t).Fill( pvParam ) );
		public static void Install( Action<GameConnectedChatLeave_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GameConnectedChatLeave_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 40, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GameConnectedChatLeave_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 40, false );
				actionClient = action;
			}
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
		static Action<DownloadClanActivityCountsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (DownloadClanActivityCountsResult_t)default(DownloadClanActivityCountsResult_t).Fill( pvParam ) );
		static Action<DownloadClanActivityCountsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (DownloadClanActivityCountsResult_t)default(DownloadClanActivityCountsResult_t).Fill( pvParam ) );
		public static void Install( Action<DownloadClanActivityCountsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(DownloadClanActivityCountsResult_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 41, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(DownloadClanActivityCountsResult_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 41, false );
				actionClient = action;
			}
		}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(JoinClanChatRoomCompletionResult_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((JoinClanChatRoomCompletionResult_t)(JoinClanChatRoomCompletionResult_t) Marshal.PtrToStructure( p, typeof(JoinClanChatRoomCompletionResult_t) ) );
		static Action<JoinClanChatRoomCompletionResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (JoinClanChatRoomCompletionResult_t)default(JoinClanChatRoomCompletionResult_t).Fill( pvParam ) );
		static Action<JoinClanChatRoomCompletionResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (JoinClanChatRoomCompletionResult_t)default(JoinClanChatRoomCompletionResult_t).Fill( pvParam ) );
		public static void Install( Action<JoinClanChatRoomCompletionResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(JoinClanChatRoomCompletionResult_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 42, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(JoinClanChatRoomCompletionResult_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 42, false );
				actionClient = action;
			}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameConnectedFriendChatMsg_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((GameConnectedFriendChatMsg_t)(GameConnectedFriendChatMsg_t) Marshal.PtrToStructure( p, typeof(GameConnectedFriendChatMsg_t) ) );
		static Action<GameConnectedFriendChatMsg_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GameConnectedFriendChatMsg_t)default(GameConnectedFriendChatMsg_t).Fill( pvParam ) );
		static Action<GameConnectedFriendChatMsg_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GameConnectedFriendChatMsg_t)default(GameConnectedFriendChatMsg_t).Fill( pvParam ) );
		public static void Install( Action<GameConnectedFriendChatMsg_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GameConnectedFriendChatMsg_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 43, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GameConnectedFriendChatMsg_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 43, false );
				actionClient = action;
			}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendsGetFollowerCount_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((FriendsGetFollowerCount_t)(FriendsGetFollowerCount_t) Marshal.PtrToStructure( p, typeof(FriendsGetFollowerCount_t) ) );
		static Action<FriendsGetFollowerCount_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (FriendsGetFollowerCount_t)default(FriendsGetFollowerCount_t).Fill( pvParam ) );
		static Action<FriendsGetFollowerCount_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (FriendsGetFollowerCount_t)default(FriendsGetFollowerCount_t).Fill( pvParam ) );
		public static void Install( Action<FriendsGetFollowerCount_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(FriendsGetFollowerCount_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 44, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(FriendsGetFollowerCount_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 44, false );
				actionClient = action;
			}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendsIsFollowing_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((FriendsIsFollowing_t)(FriendsIsFollowing_t) Marshal.PtrToStructure( p, typeof(FriendsIsFollowing_t) ) );
		static Action<FriendsIsFollowing_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (FriendsIsFollowing_t)default(FriendsIsFollowing_t).Fill( pvParam ) );
		static Action<FriendsIsFollowing_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (FriendsIsFollowing_t)default(FriendsIsFollowing_t).Fill( pvParam ) );
		public static void Install( Action<FriendsIsFollowing_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(FriendsIsFollowing_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 45, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(FriendsIsFollowing_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 45, false );
				actionClient = action;
			}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendsEnumerateFollowingList_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((FriendsEnumerateFollowingList_t)(FriendsEnumerateFollowingList_t) Marshal.PtrToStructure( p, typeof(FriendsEnumerateFollowingList_t) ) );
		static Action<FriendsEnumerateFollowingList_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (FriendsEnumerateFollowingList_t)default(FriendsEnumerateFollowingList_t).Fill( pvParam ) );
		static Action<FriendsEnumerateFollowingList_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (FriendsEnumerateFollowingList_t)default(FriendsEnumerateFollowingList_t).Fill( pvParam ) );
		public static void Install( Action<FriendsEnumerateFollowingList_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(FriendsEnumerateFollowingList_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 46, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(FriendsEnumerateFollowingList_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 46, false );
				actionClient = action;
			}
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
		static Action<SetPersonaNameResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SetPersonaNameResponse_t)default(SetPersonaNameResponse_t).Fill( pvParam ) );
		static Action<SetPersonaNameResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SetPersonaNameResponse_t)default(SetPersonaNameResponse_t).Fill( pvParam ) );
		public static void Install( Action<SetPersonaNameResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SetPersonaNameResponse_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 47, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SetPersonaNameResponse_t).GetStructSize(), CallbackIdentifiers.SteamFriends + 47, false );
				actionClient = action;
			}
		}
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
		static Action<LowBatteryPower_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (LowBatteryPower_t)default(LowBatteryPower_t).Fill( pvParam ) );
		static Action<LowBatteryPower_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (LowBatteryPower_t)default(LowBatteryPower_t).Fill( pvParam ) );
		public static void Install( Action<LowBatteryPower_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(LowBatteryPower_t).GetStructSize(), CallbackIdentifiers.SteamUtils + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(LowBatteryPower_t).GetStructSize(), CallbackIdentifiers.SteamUtils + 2, false );
				actionClient = action;
			}
		}
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
		static Action<SteamAPICallCompleted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SteamAPICallCompleted_t)default(SteamAPICallCompleted_t).Fill( pvParam ) );
		static Action<SteamAPICallCompleted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SteamAPICallCompleted_t)default(SteamAPICallCompleted_t).Fill( pvParam ) );
		public static void Install( Action<SteamAPICallCompleted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SteamAPICallCompleted_t).GetStructSize(), CallbackIdentifiers.SteamUtils + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SteamAPICallCompleted_t).GetStructSize(), CallbackIdentifiers.SteamUtils + 3, false );
				actionClient = action;
			}
		}
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
		static Action<CheckFileSignature_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (CheckFileSignature_t)default(CheckFileSignature_t).Fill( pvParam ) );
		static Action<CheckFileSignature_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (CheckFileSignature_t)default(CheckFileSignature_t).Fill( pvParam ) );
		public static void Install( Action<CheckFileSignature_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(CheckFileSignature_t).GetStructSize(), CallbackIdentifiers.SteamUtils + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(CheckFileSignature_t).GetStructSize(), CallbackIdentifiers.SteamUtils + 5, false );
				actionClient = action;
			}
		}
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
		static Action<GamepadTextInputDismissed_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GamepadTextInputDismissed_t)default(GamepadTextInputDismissed_t).Fill( pvParam ) );
		static Action<GamepadTextInputDismissed_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GamepadTextInputDismissed_t)default(GamepadTextInputDismissed_t).Fill( pvParam ) );
		public static void Install( Action<GamepadTextInputDismissed_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GamepadTextInputDismissed_t).GetStructSize(), CallbackIdentifiers.SteamUtils + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GamepadTextInputDismissed_t).GetStructSize(), CallbackIdentifiers.SteamUtils + 14, false );
				actionClient = action;
			}
		}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(gameserveritem_t) );
		public gameserveritem_t Fill( IntPtr p ) => ((gameserveritem_t)(gameserveritem_t) Marshal.PtrToStructure( p, typeof(gameserveritem_t) ) );
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
		static Action<FavoritesListChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (FavoritesListChanged_t)default(FavoritesListChanged_t).Fill( pvParam ) );
		static Action<FavoritesListChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (FavoritesListChanged_t)default(FavoritesListChanged_t).Fill( pvParam ) );
		public static void Install( Action<FavoritesListChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(FavoritesListChanged_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(FavoritesListChanged_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 2, false );
				actionClient = action;
			}
		}
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
		static Action<LobbyInvite_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (LobbyInvite_t)default(LobbyInvite_t).Fill( pvParam ) );
		static Action<LobbyInvite_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (LobbyInvite_t)default(LobbyInvite_t).Fill( pvParam ) );
		public static void Install( Action<LobbyInvite_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(LobbyInvite_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(LobbyInvite_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 3, false );
				actionClient = action;
			}
		}
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
		static Action<LobbyEnter_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (LobbyEnter_t)default(LobbyEnter_t).Fill( pvParam ) );
		static Action<LobbyEnter_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (LobbyEnter_t)default(LobbyEnter_t).Fill( pvParam ) );
		public static void Install( Action<LobbyEnter_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(LobbyEnter_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(LobbyEnter_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 4, false );
				actionClient = action;
			}
		}
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
		static Action<LobbyDataUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (LobbyDataUpdate_t)default(LobbyDataUpdate_t).Fill( pvParam ) );
		static Action<LobbyDataUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (LobbyDataUpdate_t)default(LobbyDataUpdate_t).Fill( pvParam ) );
		public static void Install( Action<LobbyDataUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(LobbyDataUpdate_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(LobbyDataUpdate_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 5, false );
				actionClient = action;
			}
		}
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
		static Action<LobbyChatUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (LobbyChatUpdate_t)default(LobbyChatUpdate_t).Fill( pvParam ) );
		static Action<LobbyChatUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (LobbyChatUpdate_t)default(LobbyChatUpdate_t).Fill( pvParam ) );
		public static void Install( Action<LobbyChatUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(LobbyChatUpdate_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 6, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(LobbyChatUpdate_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 6, false );
				actionClient = action;
			}
		}
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
		static Action<LobbyChatMsg_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (LobbyChatMsg_t)default(LobbyChatMsg_t).Fill( pvParam ) );
		static Action<LobbyChatMsg_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (LobbyChatMsg_t)default(LobbyChatMsg_t).Fill( pvParam ) );
		public static void Install( Action<LobbyChatMsg_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(LobbyChatMsg_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 7, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(LobbyChatMsg_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 7, false );
				actionClient = action;
			}
		}
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
		static Action<LobbyGameCreated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (LobbyGameCreated_t)default(LobbyGameCreated_t).Fill( pvParam ) );
		static Action<LobbyGameCreated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (LobbyGameCreated_t)default(LobbyGameCreated_t).Fill( pvParam ) );
		public static void Install( Action<LobbyGameCreated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(LobbyGameCreated_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(LobbyGameCreated_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 9, false );
				actionClient = action;
			}
		}
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
		static Action<LobbyMatchList_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (LobbyMatchList_t)default(LobbyMatchList_t).Fill( pvParam ) );
		static Action<LobbyMatchList_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (LobbyMatchList_t)default(LobbyMatchList_t).Fill( pvParam ) );
		public static void Install( Action<LobbyMatchList_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(LobbyMatchList_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 10, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(LobbyMatchList_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 10, false );
				actionClient = action;
			}
		}
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
		static Action<LobbyKicked_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (LobbyKicked_t)default(LobbyKicked_t).Fill( pvParam ) );
		static Action<LobbyKicked_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (LobbyKicked_t)default(LobbyKicked_t).Fill( pvParam ) );
		public static void Install( Action<LobbyKicked_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(LobbyKicked_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(LobbyKicked_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 12, false );
				actionClient = action;
			}
		}
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
		static Action<LobbyCreated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (LobbyCreated_t)default(LobbyCreated_t).Fill( pvParam ) );
		static Action<LobbyCreated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (LobbyCreated_t)default(LobbyCreated_t).Fill( pvParam ) );
		public static void Install( Action<LobbyCreated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(LobbyCreated_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(LobbyCreated_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 13, false );
				actionClient = action;
			}
		}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(PSNGameBootInviteResult_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((PSNGameBootInviteResult_t)(PSNGameBootInviteResult_t) Marshal.PtrToStructure( p, typeof(PSNGameBootInviteResult_t) ) );
		static Action<PSNGameBootInviteResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (PSNGameBootInviteResult_t)default(PSNGameBootInviteResult_t).Fill( pvParam ) );
		static Action<PSNGameBootInviteResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (PSNGameBootInviteResult_t)default(PSNGameBootInviteResult_t).Fill( pvParam ) );
		public static void Install( Action<PSNGameBootInviteResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(PSNGameBootInviteResult_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 15, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(PSNGameBootInviteResult_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 15, false );
				actionClient = action;
			}
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
		static Action<FavoritesListAccountsUpdated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (FavoritesListAccountsUpdated_t)default(FavoritesListAccountsUpdated_t).Fill( pvParam ) );
		static Action<FavoritesListAccountsUpdated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (FavoritesListAccountsUpdated_t)default(FavoritesListAccountsUpdated_t).Fill( pvParam ) );
		public static void Install( Action<FavoritesListAccountsUpdated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(FavoritesListAccountsUpdated_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 16, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(FavoritesListAccountsUpdated_t).GetStructSize(), CallbackIdentifiers.SteamMatchmaking + 16, false );
				actionClient = action;
			}
		}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(SearchForGameProgressCallback_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((SearchForGameProgressCallback_t)(SearchForGameProgressCallback_t) Marshal.PtrToStructure( p, typeof(SearchForGameProgressCallback_t) ) );
		static Action<SearchForGameProgressCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SearchForGameProgressCallback_t)default(SearchForGameProgressCallback_t).Fill( pvParam ) );
		static Action<SearchForGameProgressCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SearchForGameProgressCallback_t)default(SearchForGameProgressCallback_t).Fill( pvParam ) );
		public static void Install( Action<SearchForGameProgressCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SearchForGameProgressCallback_t).GetStructSize(), CallbackIdentifiers.SteamGameSearch + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SearchForGameProgressCallback_t).GetStructSize(), CallbackIdentifiers.SteamGameSearch + 1, false );
				actionClient = action;
			}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(SearchForGameResultCallback_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((SearchForGameResultCallback_t)(SearchForGameResultCallback_t) Marshal.PtrToStructure( p, typeof(SearchForGameResultCallback_t) ) );
		static Action<SearchForGameResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SearchForGameResultCallback_t)default(SearchForGameResultCallback_t).Fill( pvParam ) );
		static Action<SearchForGameResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SearchForGameResultCallback_t)default(SearchForGameResultCallback_t).Fill( pvParam ) );
		public static void Install( Action<SearchForGameResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SearchForGameResultCallback_t).GetStructSize(), CallbackIdentifiers.SteamGameSearch + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SearchForGameResultCallback_t).GetStructSize(), CallbackIdentifiers.SteamGameSearch + 2, false );
				actionClient = action;
			}
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
		static Action<RequestPlayersForGameProgressCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RequestPlayersForGameProgressCallback_t)default(RequestPlayersForGameProgressCallback_t).Fill( pvParam ) );
		static Action<RequestPlayersForGameProgressCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RequestPlayersForGameProgressCallback_t)default(RequestPlayersForGameProgressCallback_t).Fill( pvParam ) );
		public static void Install( Action<RequestPlayersForGameProgressCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RequestPlayersForGameProgressCallback_t).GetStructSize(), CallbackIdentifiers.SteamGameSearch + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RequestPlayersForGameProgressCallback_t).GetStructSize(), CallbackIdentifiers.SteamGameSearch + 11, false );
				actionClient = action;
			}
		}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(RequestPlayersForGameResultCallback_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((RequestPlayersForGameResultCallback_t)(RequestPlayersForGameResultCallback_t) Marshal.PtrToStructure( p, typeof(RequestPlayersForGameResultCallback_t) ) );
		static Action<RequestPlayersForGameResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RequestPlayersForGameResultCallback_t)default(RequestPlayersForGameResultCallback_t).Fill( pvParam ) );
		static Action<RequestPlayersForGameResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RequestPlayersForGameResultCallback_t)default(RequestPlayersForGameResultCallback_t).Fill( pvParam ) );
		public static void Install( Action<RequestPlayersForGameResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RequestPlayersForGameResultCallback_t).GetStructSize(), CallbackIdentifiers.SteamGameSearch + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RequestPlayersForGameResultCallback_t).GetStructSize(), CallbackIdentifiers.SteamGameSearch + 12, false );
				actionClient = action;
			}
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
		static Action<RequestPlayersForGameFinalResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RequestPlayersForGameFinalResultCallback_t)default(RequestPlayersForGameFinalResultCallback_t).Fill( pvParam ) );
		static Action<RequestPlayersForGameFinalResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RequestPlayersForGameFinalResultCallback_t)default(RequestPlayersForGameFinalResultCallback_t).Fill( pvParam ) );
		public static void Install( Action<RequestPlayersForGameFinalResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RequestPlayersForGameFinalResultCallback_t).GetStructSize(), CallbackIdentifiers.SteamGameSearch + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RequestPlayersForGameFinalResultCallback_t).GetStructSize(), CallbackIdentifiers.SteamGameSearch + 13, false );
				actionClient = action;
			}
		}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(SubmitPlayerResultResultCallback_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((SubmitPlayerResultResultCallback_t)(SubmitPlayerResultResultCallback_t) Marshal.PtrToStructure( p, typeof(SubmitPlayerResultResultCallback_t) ) );
		static Action<SubmitPlayerResultResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SubmitPlayerResultResultCallback_t)default(SubmitPlayerResultResultCallback_t).Fill( pvParam ) );
		static Action<SubmitPlayerResultResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SubmitPlayerResultResultCallback_t)default(SubmitPlayerResultResultCallback_t).Fill( pvParam ) );
		public static void Install( Action<SubmitPlayerResultResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SubmitPlayerResultResultCallback_t).GetStructSize(), CallbackIdentifiers.SteamGameSearch + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SubmitPlayerResultResultCallback_t).GetStructSize(), CallbackIdentifiers.SteamGameSearch + 14, false );
				actionClient = action;
			}
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
		static Action<EndGameResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (EndGameResultCallback_t)default(EndGameResultCallback_t).Fill( pvParam ) );
		static Action<EndGameResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (EndGameResultCallback_t)default(EndGameResultCallback_t).Fill( pvParam ) );
		public static void Install( Action<EndGameResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(EndGameResultCallback_t).GetStructSize(), CallbackIdentifiers.SteamGameSearch + 15, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(EndGameResultCallback_t).GetStructSize(), CallbackIdentifiers.SteamGameSearch + 15, false );
				actionClient = action;
			}
		}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(JoinPartyCallback_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((JoinPartyCallback_t)(JoinPartyCallback_t) Marshal.PtrToStructure( p, typeof(JoinPartyCallback_t) ) );
		static Action<JoinPartyCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (JoinPartyCallback_t)default(JoinPartyCallback_t).Fill( pvParam ) );
		static Action<JoinPartyCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (JoinPartyCallback_t)default(JoinPartyCallback_t).Fill( pvParam ) );
		public static void Install( Action<JoinPartyCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(JoinPartyCallback_t).GetStructSize(), CallbackIdentifiers.SteamParties + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(JoinPartyCallback_t).GetStructSize(), CallbackIdentifiers.SteamParties + 1, false );
				actionClient = action;
			}
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
		static Action<CreateBeaconCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (CreateBeaconCallback_t)default(CreateBeaconCallback_t).Fill( pvParam ) );
		static Action<CreateBeaconCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (CreateBeaconCallback_t)default(CreateBeaconCallback_t).Fill( pvParam ) );
		public static void Install( Action<CreateBeaconCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(CreateBeaconCallback_t).GetStructSize(), CallbackIdentifiers.SteamParties + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(CreateBeaconCallback_t).GetStructSize(), CallbackIdentifiers.SteamParties + 2, false );
				actionClient = action;
			}
		}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(ReservationNotificationCallback_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((ReservationNotificationCallback_t)(ReservationNotificationCallback_t) Marshal.PtrToStructure( p, typeof(ReservationNotificationCallback_t) ) );
		static Action<ReservationNotificationCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (ReservationNotificationCallback_t)default(ReservationNotificationCallback_t).Fill( pvParam ) );
		static Action<ReservationNotificationCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (ReservationNotificationCallback_t)default(ReservationNotificationCallback_t).Fill( pvParam ) );
		public static void Install( Action<ReservationNotificationCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(ReservationNotificationCallback_t).GetStructSize(), CallbackIdentifiers.SteamParties + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(ReservationNotificationCallback_t).GetStructSize(), CallbackIdentifiers.SteamParties + 3, false );
				actionClient = action;
			}
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
		static Action<ChangeNumOpenSlotsCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (ChangeNumOpenSlotsCallback_t)default(ChangeNumOpenSlotsCallback_t).Fill( pvParam ) );
		static Action<ChangeNumOpenSlotsCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (ChangeNumOpenSlotsCallback_t)default(ChangeNumOpenSlotsCallback_t).Fill( pvParam ) );
		public static void Install( Action<ChangeNumOpenSlotsCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(ChangeNumOpenSlotsCallback_t).GetStructSize(), CallbackIdentifiers.SteamParties + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(ChangeNumOpenSlotsCallback_t).GetStructSize(), CallbackIdentifiers.SteamParties + 4, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageAppSyncedClient_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageAppSyncedClient_t)default(RemoteStorageAppSyncedClient_t).Fill( pvParam ) );
		static Action<RemoteStorageAppSyncedClient_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageAppSyncedClient_t)default(RemoteStorageAppSyncedClient_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageAppSyncedClient_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageAppSyncedClient_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageAppSyncedClient_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 1, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageAppSyncedServer_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageAppSyncedServer_t)default(RemoteStorageAppSyncedServer_t).Fill( pvParam ) );
		static Action<RemoteStorageAppSyncedServer_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageAppSyncedServer_t)default(RemoteStorageAppSyncedServer_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageAppSyncedServer_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageAppSyncedServer_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageAppSyncedServer_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 2, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageAppSyncProgress_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageAppSyncProgress_t)default(RemoteStorageAppSyncProgress_t).Fill( pvParam ) );
		static Action<RemoteStorageAppSyncProgress_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageAppSyncProgress_t)default(RemoteStorageAppSyncProgress_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageAppSyncProgress_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageAppSyncProgress_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageAppSyncProgress_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 3, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageAppSyncStatusCheck_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageAppSyncStatusCheck_t)default(RemoteStorageAppSyncStatusCheck_t).Fill( pvParam ) );
		static Action<RemoteStorageAppSyncStatusCheck_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageAppSyncStatusCheck_t)default(RemoteStorageAppSyncStatusCheck_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageAppSyncStatusCheck_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageAppSyncStatusCheck_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageAppSyncStatusCheck_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 5, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageFileShareResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageFileShareResult_t)default(RemoteStorageFileShareResult_t).Fill( pvParam ) );
		static Action<RemoteStorageFileShareResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageFileShareResult_t)default(RemoteStorageFileShareResult_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageFileShareResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageFileShareResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 7, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageFileShareResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 7, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStoragePublishFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStoragePublishFileResult_t)default(RemoteStoragePublishFileResult_t).Fill( pvParam ) );
		static Action<RemoteStoragePublishFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStoragePublishFileResult_t)default(RemoteStoragePublishFileResult_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStoragePublishFileResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStoragePublishFileResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 9, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageDeletePublishedFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageDeletePublishedFileResult_t)default(RemoteStorageDeletePublishedFileResult_t).Fill( pvParam ) );
		static Action<RemoteStorageDeletePublishedFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageDeletePublishedFileResult_t)default(RemoteStorageDeletePublishedFileResult_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageDeletePublishedFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageDeletePublishedFileResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageDeletePublishedFileResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 11, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageEnumerateUserPublishedFilesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageEnumerateUserPublishedFilesResult_t)default(RemoteStorageEnumerateUserPublishedFilesResult_t).Fill( pvParam ) );
		static Action<RemoteStorageEnumerateUserPublishedFilesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageEnumerateUserPublishedFilesResult_t)default(RemoteStorageEnumerateUserPublishedFilesResult_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumerateUserPublishedFilesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageEnumerateUserPublishedFilesResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageEnumerateUserPublishedFilesResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 12, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageSubscribePublishedFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageSubscribePublishedFileResult_t)default(RemoteStorageSubscribePublishedFileResult_t).Fill( pvParam ) );
		static Action<RemoteStorageSubscribePublishedFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageSubscribePublishedFileResult_t)default(RemoteStorageSubscribePublishedFileResult_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageSubscribePublishedFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageSubscribePublishedFileResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageSubscribePublishedFileResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 13, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageEnumerateUserSubscribedFilesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageEnumerateUserSubscribedFilesResult_t)default(RemoteStorageEnumerateUserSubscribedFilesResult_t).Fill( pvParam ) );
		static Action<RemoteStorageEnumerateUserSubscribedFilesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageEnumerateUserSubscribedFilesResult_t)default(RemoteStorageEnumerateUserSubscribedFilesResult_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumerateUserSubscribedFilesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageEnumerateUserSubscribedFilesResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageEnumerateUserSubscribedFilesResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 14, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageUnsubscribePublishedFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageUnsubscribePublishedFileResult_t)default(RemoteStorageUnsubscribePublishedFileResult_t).Fill( pvParam ) );
		static Action<RemoteStorageUnsubscribePublishedFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageUnsubscribePublishedFileResult_t)default(RemoteStorageUnsubscribePublishedFileResult_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageUnsubscribePublishedFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageUnsubscribePublishedFileResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 15, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageUnsubscribePublishedFileResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 15, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageUpdatePublishedFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageUpdatePublishedFileResult_t)default(RemoteStorageUpdatePublishedFileResult_t).Fill( pvParam ) );
		static Action<RemoteStorageUpdatePublishedFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageUpdatePublishedFileResult_t)default(RemoteStorageUpdatePublishedFileResult_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageUpdatePublishedFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageUpdatePublishedFileResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 16, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageUpdatePublishedFileResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 16, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageDownloadUGCResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageDownloadUGCResult_t)default(RemoteStorageDownloadUGCResult_t).Fill( pvParam ) );
		static Action<RemoteStorageDownloadUGCResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageDownloadUGCResult_t)default(RemoteStorageDownloadUGCResult_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageDownloadUGCResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageDownloadUGCResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 17, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageDownloadUGCResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 17, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageGetPublishedFileDetailsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageGetPublishedFileDetailsResult_t)default(RemoteStorageGetPublishedFileDetailsResult_t).Fill( pvParam ) );
		static Action<RemoteStorageGetPublishedFileDetailsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageGetPublishedFileDetailsResult_t)default(RemoteStorageGetPublishedFileDetailsResult_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageGetPublishedFileDetailsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageGetPublishedFileDetailsResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 18, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageGetPublishedFileDetailsResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 18, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageEnumerateWorkshopFilesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageEnumerateWorkshopFilesResult_t)default(RemoteStorageEnumerateWorkshopFilesResult_t).Fill( pvParam ) );
		static Action<RemoteStorageEnumerateWorkshopFilesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageEnumerateWorkshopFilesResult_t)default(RemoteStorageEnumerateWorkshopFilesResult_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumerateWorkshopFilesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageEnumerateWorkshopFilesResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 19, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageEnumerateWorkshopFilesResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 19, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageGetPublishedItemVoteDetailsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageGetPublishedItemVoteDetailsResult_t)default(RemoteStorageGetPublishedItemVoteDetailsResult_t).Fill( pvParam ) );
		static Action<RemoteStorageGetPublishedItemVoteDetailsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageGetPublishedItemVoteDetailsResult_t)default(RemoteStorageGetPublishedItemVoteDetailsResult_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageGetPublishedItemVoteDetailsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageGetPublishedItemVoteDetailsResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 20, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageGetPublishedItemVoteDetailsResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 20, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStoragePublishedFileSubscribed_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStoragePublishedFileSubscribed_t)default(RemoteStoragePublishedFileSubscribed_t).Fill( pvParam ) );
		static Action<RemoteStoragePublishedFileSubscribed_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStoragePublishedFileSubscribed_t)default(RemoteStoragePublishedFileSubscribed_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishedFileSubscribed_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStoragePublishedFileSubscribed_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 21, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStoragePublishedFileSubscribed_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 21, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStoragePublishedFileUnsubscribed_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStoragePublishedFileUnsubscribed_t)default(RemoteStoragePublishedFileUnsubscribed_t).Fill( pvParam ) );
		static Action<RemoteStoragePublishedFileUnsubscribed_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStoragePublishedFileUnsubscribed_t)default(RemoteStoragePublishedFileUnsubscribed_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishedFileUnsubscribed_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStoragePublishedFileUnsubscribed_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 22, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStoragePublishedFileUnsubscribed_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 22, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStoragePublishedFileDeleted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStoragePublishedFileDeleted_t)default(RemoteStoragePublishedFileDeleted_t).Fill( pvParam ) );
		static Action<RemoteStoragePublishedFileDeleted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStoragePublishedFileDeleted_t)default(RemoteStoragePublishedFileDeleted_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishedFileDeleted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStoragePublishedFileDeleted_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 23, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStoragePublishedFileDeleted_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 23, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageUpdateUserPublishedItemVoteResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageUpdateUserPublishedItemVoteResult_t)default(RemoteStorageUpdateUserPublishedItemVoteResult_t).Fill( pvParam ) );
		static Action<RemoteStorageUpdateUserPublishedItemVoteResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageUpdateUserPublishedItemVoteResult_t)default(RemoteStorageUpdateUserPublishedItemVoteResult_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageUpdateUserPublishedItemVoteResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageUpdateUserPublishedItemVoteResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 24, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageUpdateUserPublishedItemVoteResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 24, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageUserVoteDetails_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageUserVoteDetails_t)default(RemoteStorageUserVoteDetails_t).Fill( pvParam ) );
		static Action<RemoteStorageUserVoteDetails_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageUserVoteDetails_t)default(RemoteStorageUserVoteDetails_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageUserVoteDetails_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageUserVoteDetails_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 25, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageUserVoteDetails_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 25, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageEnumerateUserSharedWorkshopFilesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageEnumerateUserSharedWorkshopFilesResult_t)default(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t).Fill( pvParam ) );
		static Action<RemoteStorageEnumerateUserSharedWorkshopFilesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageEnumerateUserSharedWorkshopFilesResult_t)default(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumerateUserSharedWorkshopFilesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 26, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 26, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageSetUserPublishedFileActionResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageSetUserPublishedFileActionResult_t)default(RemoteStorageSetUserPublishedFileActionResult_t).Fill( pvParam ) );
		static Action<RemoteStorageSetUserPublishedFileActionResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageSetUserPublishedFileActionResult_t)default(RemoteStorageSetUserPublishedFileActionResult_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageSetUserPublishedFileActionResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageSetUserPublishedFileActionResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 27, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageSetUserPublishedFileActionResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 27, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageEnumeratePublishedFilesByUserActionResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageEnumeratePublishedFilesByUserActionResult_t)default(RemoteStorageEnumeratePublishedFilesByUserActionResult_t).Fill( pvParam ) );
		static Action<RemoteStorageEnumeratePublishedFilesByUserActionResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageEnumeratePublishedFilesByUserActionResult_t)default(RemoteStorageEnumeratePublishedFilesByUserActionResult_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumeratePublishedFilesByUserActionResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageEnumeratePublishedFilesByUserActionResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 28, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageEnumeratePublishedFilesByUserActionResult_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 28, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStoragePublishFileProgress_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStoragePublishFileProgress_t)default(RemoteStoragePublishFileProgress_t).Fill( pvParam ) );
		static Action<RemoteStoragePublishFileProgress_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStoragePublishFileProgress_t)default(RemoteStoragePublishFileProgress_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishFileProgress_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStoragePublishFileProgress_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 29, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStoragePublishFileProgress_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 29, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStoragePublishedFileUpdated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStoragePublishedFileUpdated_t)default(RemoteStoragePublishedFileUpdated_t).Fill( pvParam ) );
		static Action<RemoteStoragePublishedFileUpdated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStoragePublishedFileUpdated_t)default(RemoteStoragePublishedFileUpdated_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishedFileUpdated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStoragePublishedFileUpdated_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 30, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStoragePublishedFileUpdated_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 30, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageFileWriteAsyncComplete_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageFileWriteAsyncComplete_t)default(RemoteStorageFileWriteAsyncComplete_t).Fill( pvParam ) );
		static Action<RemoteStorageFileWriteAsyncComplete_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageFileWriteAsyncComplete_t)default(RemoteStorageFileWriteAsyncComplete_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageFileWriteAsyncComplete_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageFileWriteAsyncComplete_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 31, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageFileWriteAsyncComplete_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 31, false );
				actionClient = action;
			}
		}
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
		static Action<RemoteStorageFileReadAsyncComplete_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoteStorageFileReadAsyncComplete_t)default(RemoteStorageFileReadAsyncComplete_t).Fill( pvParam ) );
		static Action<RemoteStorageFileReadAsyncComplete_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoteStorageFileReadAsyncComplete_t)default(RemoteStorageFileReadAsyncComplete_t).Fill( pvParam ) );
		public static void Install( Action<RemoteStorageFileReadAsyncComplete_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoteStorageFileReadAsyncComplete_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 32, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoteStorageFileReadAsyncComplete_t).GetStructSize(), CallbackIdentifiers.ClientRemoteStorage + 32, false );
				actionClient = action;
			}
		}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserStatsReceived_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((UserStatsReceived_t)(UserStatsReceived_t) Marshal.PtrToStructure( p, typeof(UserStatsReceived_t) ) );
		static Action<UserStatsReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (UserStatsReceived_t)default(UserStatsReceived_t).Fill( pvParam ) );
		static Action<UserStatsReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (UserStatsReceived_t)default(UserStatsReceived_t).Fill( pvParam ) );
		public static void Install( Action<UserStatsReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(UserStatsReceived_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(UserStatsReceived_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 1, false );
				actionClient = action;
			}
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
		static Action<UserStatsStored_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (UserStatsStored_t)default(UserStatsStored_t).Fill( pvParam ) );
		static Action<UserStatsStored_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (UserStatsStored_t)default(UserStatsStored_t).Fill( pvParam ) );
		public static void Install( Action<UserStatsStored_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(UserStatsStored_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(UserStatsStored_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 2, false );
				actionClient = action;
			}
		}
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
		static Action<UserAchievementStored_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (UserAchievementStored_t)default(UserAchievementStored_t).Fill( pvParam ) );
		static Action<UserAchievementStored_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (UserAchievementStored_t)default(UserAchievementStored_t).Fill( pvParam ) );
		public static void Install( Action<UserAchievementStored_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(UserAchievementStored_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(UserAchievementStored_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 3, false );
				actionClient = action;
			}
		}
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
		static Action<LeaderboardFindResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (LeaderboardFindResult_t)default(LeaderboardFindResult_t).Fill( pvParam ) );
		static Action<LeaderboardFindResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (LeaderboardFindResult_t)default(LeaderboardFindResult_t).Fill( pvParam ) );
		public static void Install( Action<LeaderboardFindResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(LeaderboardFindResult_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(LeaderboardFindResult_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 4, false );
				actionClient = action;
			}
		}
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
		static Action<LeaderboardScoresDownloaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (LeaderboardScoresDownloaded_t)default(LeaderboardScoresDownloaded_t).Fill( pvParam ) );
		static Action<LeaderboardScoresDownloaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (LeaderboardScoresDownloaded_t)default(LeaderboardScoresDownloaded_t).Fill( pvParam ) );
		public static void Install( Action<LeaderboardScoresDownloaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(LeaderboardScoresDownloaded_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(LeaderboardScoresDownloaded_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 5, false );
				actionClient = action;
			}
		}
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
		static Action<LeaderboardScoreUploaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (LeaderboardScoreUploaded_t)default(LeaderboardScoreUploaded_t).Fill( pvParam ) );
		static Action<LeaderboardScoreUploaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (LeaderboardScoreUploaded_t)default(LeaderboardScoreUploaded_t).Fill( pvParam ) );
		public static void Install( Action<LeaderboardScoreUploaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(LeaderboardScoreUploaded_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 6, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(LeaderboardScoreUploaded_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 6, false );
				actionClient = action;
			}
		}
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
		static Action<NumberOfCurrentPlayers_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (NumberOfCurrentPlayers_t)default(NumberOfCurrentPlayers_t).Fill( pvParam ) );
		static Action<NumberOfCurrentPlayers_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (NumberOfCurrentPlayers_t)default(NumberOfCurrentPlayers_t).Fill( pvParam ) );
		public static void Install( Action<NumberOfCurrentPlayers_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(NumberOfCurrentPlayers_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 7, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(NumberOfCurrentPlayers_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 7, false );
				actionClient = action;
			}
		}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserStatsUnloaded_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((UserStatsUnloaded_t)(UserStatsUnloaded_t) Marshal.PtrToStructure( p, typeof(UserStatsUnloaded_t) ) );
		static Action<UserStatsUnloaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (UserStatsUnloaded_t)default(UserStatsUnloaded_t).Fill( pvParam ) );
		static Action<UserStatsUnloaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (UserStatsUnloaded_t)default(UserStatsUnloaded_t).Fill( pvParam ) );
		public static void Install( Action<UserStatsUnloaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(UserStatsUnloaded_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 8, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(UserStatsUnloaded_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 8, false );
				actionClient = action;
			}
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
		static Action<UserAchievementIconFetched_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (UserAchievementIconFetched_t)default(UserAchievementIconFetched_t).Fill( pvParam ) );
		static Action<UserAchievementIconFetched_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (UserAchievementIconFetched_t)default(UserAchievementIconFetched_t).Fill( pvParam ) );
		public static void Install( Action<UserAchievementIconFetched_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(UserAchievementIconFetched_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(UserAchievementIconFetched_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 9, false );
				actionClient = action;
			}
		}
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
		static Action<GlobalAchievementPercentagesReady_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GlobalAchievementPercentagesReady_t)default(GlobalAchievementPercentagesReady_t).Fill( pvParam ) );
		static Action<GlobalAchievementPercentagesReady_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GlobalAchievementPercentagesReady_t)default(GlobalAchievementPercentagesReady_t).Fill( pvParam ) );
		public static void Install( Action<GlobalAchievementPercentagesReady_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GlobalAchievementPercentagesReady_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 10, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GlobalAchievementPercentagesReady_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 10, false );
				actionClient = action;
			}
		}
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
		static Action<LeaderboardUGCSet_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (LeaderboardUGCSet_t)default(LeaderboardUGCSet_t).Fill( pvParam ) );
		static Action<LeaderboardUGCSet_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (LeaderboardUGCSet_t)default(LeaderboardUGCSet_t).Fill( pvParam ) );
		public static void Install( Action<LeaderboardUGCSet_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(LeaderboardUGCSet_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(LeaderboardUGCSet_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 11, false );
				actionClient = action;
			}
		}
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
		static Action<PS3TrophiesInstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (PS3TrophiesInstalled_t)default(PS3TrophiesInstalled_t).Fill( pvParam ) );
		static Action<PS3TrophiesInstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (PS3TrophiesInstalled_t)default(PS3TrophiesInstalled_t).Fill( pvParam ) );
		public static void Install( Action<PS3TrophiesInstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(PS3TrophiesInstalled_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(PS3TrophiesInstalled_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 12, false );
				actionClient = action;
			}
		}
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
		static Action<GlobalStatsReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GlobalStatsReceived_t)default(GlobalStatsReceived_t).Fill( pvParam ) );
		static Action<GlobalStatsReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GlobalStatsReceived_t)default(GlobalStatsReceived_t).Fill( pvParam ) );
		public static void Install( Action<GlobalStatsReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GlobalStatsReceived_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GlobalStatsReceived_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 12, false );
				actionClient = action;
			}
		}
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
		static Action<DlcInstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (DlcInstalled_t)default(DlcInstalled_t).Fill( pvParam ) );
		static Action<DlcInstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (DlcInstalled_t)default(DlcInstalled_t).Fill( pvParam ) );
		public static void Install( Action<DlcInstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(DlcInstalled_t).GetStructSize(), CallbackIdentifiers.SteamApps + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(DlcInstalled_t).GetStructSize(), CallbackIdentifiers.SteamApps + 5, false );
				actionClient = action;
			}
		}
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
		static Action<RegisterActivationCodeResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RegisterActivationCodeResponse_t)default(RegisterActivationCodeResponse_t).Fill( pvParam ) );
		static Action<RegisterActivationCodeResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RegisterActivationCodeResponse_t)default(RegisterActivationCodeResponse_t).Fill( pvParam ) );
		public static void Install( Action<RegisterActivationCodeResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RegisterActivationCodeResponse_t).GetStructSize(), CallbackIdentifiers.SteamApps + 8, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RegisterActivationCodeResponse_t).GetStructSize(), CallbackIdentifiers.SteamApps + 8, false );
				actionClient = action;
			}
		}
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
		static Action<AppProofOfPurchaseKeyResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (AppProofOfPurchaseKeyResponse_t)default(AppProofOfPurchaseKeyResponse_t).Fill( pvParam ) );
		static Action<AppProofOfPurchaseKeyResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (AppProofOfPurchaseKeyResponse_t)default(AppProofOfPurchaseKeyResponse_t).Fill( pvParam ) );
		public static void Install( Action<AppProofOfPurchaseKeyResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(AppProofOfPurchaseKeyResponse_t).GetStructSize(), CallbackIdentifiers.SteamApps + 21, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(AppProofOfPurchaseKeyResponse_t).GetStructSize(), CallbackIdentifiers.SteamApps + 21, false );
				actionClient = action;
			}
		}
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
		static Action<FileDetailsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (FileDetailsResult_t)default(FileDetailsResult_t).Fill( pvParam ) );
		static Action<FileDetailsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (FileDetailsResult_t)default(FileDetailsResult_t).Fill( pvParam ) );
		public static void Install( Action<FileDetailsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(FileDetailsResult_t).GetStructSize(), CallbackIdentifiers.SteamApps + 23, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(FileDetailsResult_t).GetStructSize(), CallbackIdentifiers.SteamApps + 23, false );
				actionClient = action;
			}
		}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(P2PSessionRequest_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((P2PSessionRequest_t)(P2PSessionRequest_t) Marshal.PtrToStructure( p, typeof(P2PSessionRequest_t) ) );
		static Action<P2PSessionRequest_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (P2PSessionRequest_t)default(P2PSessionRequest_t).Fill( pvParam ) );
		static Action<P2PSessionRequest_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (P2PSessionRequest_t)default(P2PSessionRequest_t).Fill( pvParam ) );
		public static void Install( Action<P2PSessionRequest_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(P2PSessionRequest_t).GetStructSize(), CallbackIdentifiers.SteamNetworking + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(P2PSessionRequest_t).GetStructSize(), CallbackIdentifiers.SteamNetworking + 2, false );
				actionClient = action;
			}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(P2PSessionConnectFail_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((P2PSessionConnectFail_t)(P2PSessionConnectFail_t) Marshal.PtrToStructure( p, typeof(P2PSessionConnectFail_t) ) );
		static Action<P2PSessionConnectFail_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (P2PSessionConnectFail_t)default(P2PSessionConnectFail_t).Fill( pvParam ) );
		static Action<P2PSessionConnectFail_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (P2PSessionConnectFail_t)default(P2PSessionConnectFail_t).Fill( pvParam ) );
		public static void Install( Action<P2PSessionConnectFail_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(P2PSessionConnectFail_t).GetStructSize(), CallbackIdentifiers.SteamNetworking + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(P2PSessionConnectFail_t).GetStructSize(), CallbackIdentifiers.SteamNetworking + 3, false );
				actionClient = action;
			}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(SocketStatusCallback_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((SocketStatusCallback_t)(SocketStatusCallback_t) Marshal.PtrToStructure( p, typeof(SocketStatusCallback_t) ) );
		static Action<SocketStatusCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SocketStatusCallback_t)default(SocketStatusCallback_t).Fill( pvParam ) );
		static Action<SocketStatusCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SocketStatusCallback_t)default(SocketStatusCallback_t).Fill( pvParam ) );
		public static void Install( Action<SocketStatusCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SocketStatusCallback_t).GetStructSize(), CallbackIdentifiers.SteamNetworking + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SocketStatusCallback_t).GetStructSize(), CallbackIdentifiers.SteamNetworking + 1, false );
				actionClient = action;
			}
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
		static Action<ScreenshotReady_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (ScreenshotReady_t)default(ScreenshotReady_t).Fill( pvParam ) );
		static Action<ScreenshotReady_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (ScreenshotReady_t)default(ScreenshotReady_t).Fill( pvParam ) );
		public static void Install( Action<ScreenshotReady_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(ScreenshotReady_t).GetStructSize(), CallbackIdentifiers.SteamScreenshots + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(ScreenshotReady_t).GetStructSize(), CallbackIdentifiers.SteamScreenshots + 1, false );
				actionClient = action;
			}
		}
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
		static Action<VolumeHasChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (VolumeHasChanged_t)default(VolumeHasChanged_t).Fill( pvParam ) );
		static Action<VolumeHasChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (VolumeHasChanged_t)default(VolumeHasChanged_t).Fill( pvParam ) );
		public static void Install( Action<VolumeHasChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(VolumeHasChanged_t).GetStructSize(), CallbackIdentifiers.SteamMusic + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(VolumeHasChanged_t).GetStructSize(), CallbackIdentifiers.SteamMusic + 2, false );
				actionClient = action;
			}
		}
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
		static Action<MusicPlayerWantsShuffled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (MusicPlayerWantsShuffled_t)default(MusicPlayerWantsShuffled_t).Fill( pvParam ) );
		static Action<MusicPlayerWantsShuffled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (MusicPlayerWantsShuffled_t)default(MusicPlayerWantsShuffled_t).Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsShuffled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(MusicPlayerWantsShuffled_t).GetStructSize(), CallbackIdentifiers.SteamMusicRemote + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(MusicPlayerWantsShuffled_t).GetStructSize(), CallbackIdentifiers.SteamMusicRemote + 9, false );
				actionClient = action;
			}
		}
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
		static Action<MusicPlayerWantsLooped_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (MusicPlayerWantsLooped_t)default(MusicPlayerWantsLooped_t).Fill( pvParam ) );
		static Action<MusicPlayerWantsLooped_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (MusicPlayerWantsLooped_t)default(MusicPlayerWantsLooped_t).Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsLooped_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(MusicPlayerWantsLooped_t).GetStructSize(), CallbackIdentifiers.SteamMusicRemote + 10, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(MusicPlayerWantsLooped_t).GetStructSize(), CallbackIdentifiers.SteamMusicRemote + 10, false );
				actionClient = action;
			}
		}
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
		static Action<MusicPlayerWantsVolume_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (MusicPlayerWantsVolume_t)default(MusicPlayerWantsVolume_t).Fill( pvParam ) );
		static Action<MusicPlayerWantsVolume_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (MusicPlayerWantsVolume_t)default(MusicPlayerWantsVolume_t).Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsVolume_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(MusicPlayerWantsVolume_t).GetStructSize(), CallbackIdentifiers.SteamMusic + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(MusicPlayerWantsVolume_t).GetStructSize(), CallbackIdentifiers.SteamMusic + 11, false );
				actionClient = action;
			}
		}
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
		static Action<MusicPlayerSelectsQueueEntry_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (MusicPlayerSelectsQueueEntry_t)default(MusicPlayerSelectsQueueEntry_t).Fill( pvParam ) );
		static Action<MusicPlayerSelectsQueueEntry_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (MusicPlayerSelectsQueueEntry_t)default(MusicPlayerSelectsQueueEntry_t).Fill( pvParam ) );
		public static void Install( Action<MusicPlayerSelectsQueueEntry_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(MusicPlayerSelectsQueueEntry_t).GetStructSize(), CallbackIdentifiers.SteamMusic + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(MusicPlayerSelectsQueueEntry_t).GetStructSize(), CallbackIdentifiers.SteamMusic + 12, false );
				actionClient = action;
			}
		}
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
		static Action<MusicPlayerSelectsPlaylistEntry_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (MusicPlayerSelectsPlaylistEntry_t)default(MusicPlayerSelectsPlaylistEntry_t).Fill( pvParam ) );
		static Action<MusicPlayerSelectsPlaylistEntry_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (MusicPlayerSelectsPlaylistEntry_t)default(MusicPlayerSelectsPlaylistEntry_t).Fill( pvParam ) );
		public static void Install( Action<MusicPlayerSelectsPlaylistEntry_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(MusicPlayerSelectsPlaylistEntry_t).GetStructSize(), CallbackIdentifiers.SteamMusic + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(MusicPlayerSelectsPlaylistEntry_t).GetStructSize(), CallbackIdentifiers.SteamMusic + 13, false );
				actionClient = action;
			}
		}
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
		static Action<MusicPlayerWantsPlayingRepeatStatus_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (MusicPlayerWantsPlayingRepeatStatus_t)default(MusicPlayerWantsPlayingRepeatStatus_t).Fill( pvParam ) );
		static Action<MusicPlayerWantsPlayingRepeatStatus_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (MusicPlayerWantsPlayingRepeatStatus_t)default(MusicPlayerWantsPlayingRepeatStatus_t).Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsPlayingRepeatStatus_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(MusicPlayerWantsPlayingRepeatStatus_t).GetStructSize(), CallbackIdentifiers.SteamMusicRemote + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(MusicPlayerWantsPlayingRepeatStatus_t).GetStructSize(), CallbackIdentifiers.SteamMusicRemote + 14, false );
				actionClient = action;
			}
		}
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
		static Action<HTTPRequestCompleted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTTPRequestCompleted_t)default(HTTPRequestCompleted_t).Fill( pvParam ) );
		static Action<HTTPRequestCompleted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTTPRequestCompleted_t)default(HTTPRequestCompleted_t).Fill( pvParam ) );
		public static void Install( Action<HTTPRequestCompleted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTTPRequestCompleted_t).GetStructSize(), CallbackIdentifiers.ClientHTTP + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTTPRequestCompleted_t).GetStructSize(), CallbackIdentifiers.ClientHTTP + 1, false );
				actionClient = action;
			}
		}
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
		static Action<HTTPRequestHeadersReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTTPRequestHeadersReceived_t)default(HTTPRequestHeadersReceived_t).Fill( pvParam ) );
		static Action<HTTPRequestHeadersReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTTPRequestHeadersReceived_t)default(HTTPRequestHeadersReceived_t).Fill( pvParam ) );
		public static void Install( Action<HTTPRequestHeadersReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTTPRequestHeadersReceived_t).GetStructSize(), CallbackIdentifiers.ClientHTTP + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTTPRequestHeadersReceived_t).GetStructSize(), CallbackIdentifiers.ClientHTTP + 2, false );
				actionClient = action;
			}
		}
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
		static Action<HTTPRequestDataReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTTPRequestDataReceived_t)default(HTTPRequestDataReceived_t).Fill( pvParam ) );
		static Action<HTTPRequestDataReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTTPRequestDataReceived_t)default(HTTPRequestDataReceived_t).Fill( pvParam ) );
		public static void Install( Action<HTTPRequestDataReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTTPRequestDataReceived_t).GetStructSize(), CallbackIdentifiers.ClientHTTP + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTTPRequestDataReceived_t).GetStructSize(), CallbackIdentifiers.ClientHTTP + 3, false );
				actionClient = action;
			}
		}
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
		static Action<SteamUGCQueryCompleted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SteamUGCQueryCompleted_t)default(SteamUGCQueryCompleted_t).Fill( pvParam ) );
		static Action<SteamUGCQueryCompleted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SteamUGCQueryCompleted_t)default(SteamUGCQueryCompleted_t).Fill( pvParam ) );
		public static void Install( Action<SteamUGCQueryCompleted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SteamUGCQueryCompleted_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SteamUGCQueryCompleted_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 1, false );
				actionClient = action;
			}
		}
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
		static Action<SteamUGCRequestUGCDetailsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SteamUGCRequestUGCDetailsResult_t)default(SteamUGCRequestUGCDetailsResult_t).Fill( pvParam ) );
		static Action<SteamUGCRequestUGCDetailsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SteamUGCRequestUGCDetailsResult_t)default(SteamUGCRequestUGCDetailsResult_t).Fill( pvParam ) );
		public static void Install( Action<SteamUGCRequestUGCDetailsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SteamUGCRequestUGCDetailsResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SteamUGCRequestUGCDetailsResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 2, false );
				actionClient = action;
			}
		}
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
		static Action<CreateItemResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (CreateItemResult_t)default(CreateItemResult_t).Fill( pvParam ) );
		static Action<CreateItemResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (CreateItemResult_t)default(CreateItemResult_t).Fill( pvParam ) );
		public static void Install( Action<CreateItemResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(CreateItemResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(CreateItemResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 3, false );
				actionClient = action;
			}
		}
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
		static Action<SubmitItemUpdateResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SubmitItemUpdateResult_t)default(SubmitItemUpdateResult_t).Fill( pvParam ) );
		static Action<SubmitItemUpdateResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SubmitItemUpdateResult_t)default(SubmitItemUpdateResult_t).Fill( pvParam ) );
		public static void Install( Action<SubmitItemUpdateResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SubmitItemUpdateResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SubmitItemUpdateResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 4, false );
				actionClient = action;
			}
		}
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
		static Action<DownloadItemResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (DownloadItemResult_t)default(DownloadItemResult_t).Fill( pvParam ) );
		static Action<DownloadItemResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (DownloadItemResult_t)default(DownloadItemResult_t).Fill( pvParam ) );
		public static void Install( Action<DownloadItemResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(DownloadItemResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 6, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(DownloadItemResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 6, false );
				actionClient = action;
			}
		}
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
		static Action<UserFavoriteItemsListChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (UserFavoriteItemsListChanged_t)default(UserFavoriteItemsListChanged_t).Fill( pvParam ) );
		static Action<UserFavoriteItemsListChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (UserFavoriteItemsListChanged_t)default(UserFavoriteItemsListChanged_t).Fill( pvParam ) );
		public static void Install( Action<UserFavoriteItemsListChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(UserFavoriteItemsListChanged_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 7, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(UserFavoriteItemsListChanged_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 7, false );
				actionClient = action;
			}
		}
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
		static Action<SetUserItemVoteResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SetUserItemVoteResult_t)default(SetUserItemVoteResult_t).Fill( pvParam ) );
		static Action<SetUserItemVoteResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SetUserItemVoteResult_t)default(SetUserItemVoteResult_t).Fill( pvParam ) );
		public static void Install( Action<SetUserItemVoteResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SetUserItemVoteResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 8, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SetUserItemVoteResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 8, false );
				actionClient = action;
			}
		}
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
		static Action<GetUserItemVoteResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GetUserItemVoteResult_t)default(GetUserItemVoteResult_t).Fill( pvParam ) );
		static Action<GetUserItemVoteResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GetUserItemVoteResult_t)default(GetUserItemVoteResult_t).Fill( pvParam ) );
		public static void Install( Action<GetUserItemVoteResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GetUserItemVoteResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GetUserItemVoteResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 9, false );
				actionClient = action;
			}
		}
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
		static Action<StartPlaytimeTrackingResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (StartPlaytimeTrackingResult_t)default(StartPlaytimeTrackingResult_t).Fill( pvParam ) );
		static Action<StartPlaytimeTrackingResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (StartPlaytimeTrackingResult_t)default(StartPlaytimeTrackingResult_t).Fill( pvParam ) );
		public static void Install( Action<StartPlaytimeTrackingResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(StartPlaytimeTrackingResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 10, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(StartPlaytimeTrackingResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 10, false );
				actionClient = action;
			}
		}
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
		static Action<StopPlaytimeTrackingResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (StopPlaytimeTrackingResult_t)default(StopPlaytimeTrackingResult_t).Fill( pvParam ) );
		static Action<StopPlaytimeTrackingResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (StopPlaytimeTrackingResult_t)default(StopPlaytimeTrackingResult_t).Fill( pvParam ) );
		public static void Install( Action<StopPlaytimeTrackingResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(StopPlaytimeTrackingResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(StopPlaytimeTrackingResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 11, false );
				actionClient = action;
			}
		}
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
		static Action<AddUGCDependencyResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (AddUGCDependencyResult_t)default(AddUGCDependencyResult_t).Fill( pvParam ) );
		static Action<AddUGCDependencyResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (AddUGCDependencyResult_t)default(AddUGCDependencyResult_t).Fill( pvParam ) );
		public static void Install( Action<AddUGCDependencyResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(AddUGCDependencyResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(AddUGCDependencyResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 12, false );
				actionClient = action;
			}
		}
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
		static Action<RemoveUGCDependencyResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoveUGCDependencyResult_t)default(RemoveUGCDependencyResult_t).Fill( pvParam ) );
		static Action<RemoveUGCDependencyResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoveUGCDependencyResult_t)default(RemoveUGCDependencyResult_t).Fill( pvParam ) );
		public static void Install( Action<RemoveUGCDependencyResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoveUGCDependencyResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoveUGCDependencyResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 13, false );
				actionClient = action;
			}
		}
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
		static Action<AddAppDependencyResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (AddAppDependencyResult_t)default(AddAppDependencyResult_t).Fill( pvParam ) );
		static Action<AddAppDependencyResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (AddAppDependencyResult_t)default(AddAppDependencyResult_t).Fill( pvParam ) );
		public static void Install( Action<AddAppDependencyResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(AddAppDependencyResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(AddAppDependencyResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 14, false );
				actionClient = action;
			}
		}
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
		static Action<RemoveAppDependencyResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (RemoveAppDependencyResult_t)default(RemoveAppDependencyResult_t).Fill( pvParam ) );
		static Action<RemoveAppDependencyResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (RemoveAppDependencyResult_t)default(RemoveAppDependencyResult_t).Fill( pvParam ) );
		public static void Install( Action<RemoveAppDependencyResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(RemoveAppDependencyResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 15, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(RemoveAppDependencyResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 15, false );
				actionClient = action;
			}
		}
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
		static Action<GetAppDependenciesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GetAppDependenciesResult_t)default(GetAppDependenciesResult_t).Fill( pvParam ) );
		static Action<GetAppDependenciesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GetAppDependenciesResult_t)default(GetAppDependenciesResult_t).Fill( pvParam ) );
		public static void Install( Action<GetAppDependenciesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GetAppDependenciesResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 16, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GetAppDependenciesResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 16, false );
				actionClient = action;
			}
		}
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
		static Action<DeleteItemResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (DeleteItemResult_t)default(DeleteItemResult_t).Fill( pvParam ) );
		static Action<DeleteItemResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (DeleteItemResult_t)default(DeleteItemResult_t).Fill( pvParam ) );
		public static void Install( Action<DeleteItemResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(DeleteItemResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 17, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(DeleteItemResult_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 17, false );
				actionClient = action;
			}
		}
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
		static Action<SteamAppInstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SteamAppInstalled_t)default(SteamAppInstalled_t).Fill( pvParam ) );
		static Action<SteamAppInstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SteamAppInstalled_t)default(SteamAppInstalled_t).Fill( pvParam ) );
		public static void Install( Action<SteamAppInstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SteamAppInstalled_t).GetStructSize(), CallbackIdentifiers.SteamAppList + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SteamAppInstalled_t).GetStructSize(), CallbackIdentifiers.SteamAppList + 1, false );
				actionClient = action;
			}
		}
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
		static Action<SteamAppUninstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SteamAppUninstalled_t)default(SteamAppUninstalled_t).Fill( pvParam ) );
		static Action<SteamAppUninstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SteamAppUninstalled_t)default(SteamAppUninstalled_t).Fill( pvParam ) );
		public static void Install( Action<SteamAppUninstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SteamAppUninstalled_t).GetStructSize(), CallbackIdentifiers.SteamAppList + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SteamAppUninstalled_t).GetStructSize(), CallbackIdentifiers.SteamAppList + 2, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_BrowserReady_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_BrowserReady_t)default(HTML_BrowserReady_t).Fill( pvParam ) );
		static Action<HTML_BrowserReady_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_BrowserReady_t)default(HTML_BrowserReady_t).Fill( pvParam ) );
		public static void Install( Action<HTML_BrowserReady_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_BrowserReady_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_BrowserReady_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 1, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_URLChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_URLChanged_t)default(HTML_URLChanged_t).Fill( pvParam ) );
		static Action<HTML_URLChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_URLChanged_t)default(HTML_URLChanged_t).Fill( pvParam ) );
		public static void Install( Action<HTML_URLChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_URLChanged_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_URLChanged_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 5, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_FinishedRequest_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_FinishedRequest_t)default(HTML_FinishedRequest_t).Fill( pvParam ) );
		static Action<HTML_FinishedRequest_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_FinishedRequest_t)default(HTML_FinishedRequest_t).Fill( pvParam ) );
		public static void Install( Action<HTML_FinishedRequest_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_FinishedRequest_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 6, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_FinishedRequest_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 6, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_OpenLinkInNewTab_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_OpenLinkInNewTab_t)default(HTML_OpenLinkInNewTab_t).Fill( pvParam ) );
		static Action<HTML_OpenLinkInNewTab_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_OpenLinkInNewTab_t)default(HTML_OpenLinkInNewTab_t).Fill( pvParam ) );
		public static void Install( Action<HTML_OpenLinkInNewTab_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_OpenLinkInNewTab_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 7, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_OpenLinkInNewTab_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 7, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_ChangedTitle_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_ChangedTitle_t)default(HTML_ChangedTitle_t).Fill( pvParam ) );
		static Action<HTML_ChangedTitle_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_ChangedTitle_t)default(HTML_ChangedTitle_t).Fill( pvParam ) );
		public static void Install( Action<HTML_ChangedTitle_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_ChangedTitle_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 8, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_ChangedTitle_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 8, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_SearchResults_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_SearchResults_t)default(HTML_SearchResults_t).Fill( pvParam ) );
		static Action<HTML_SearchResults_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_SearchResults_t)default(HTML_SearchResults_t).Fill( pvParam ) );
		public static void Install( Action<HTML_SearchResults_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_SearchResults_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_SearchResults_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 9, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_CanGoBackAndForward_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_CanGoBackAndForward_t)default(HTML_CanGoBackAndForward_t).Fill( pvParam ) );
		static Action<HTML_CanGoBackAndForward_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_CanGoBackAndForward_t)default(HTML_CanGoBackAndForward_t).Fill( pvParam ) );
		public static void Install( Action<HTML_CanGoBackAndForward_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_CanGoBackAndForward_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 10, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_CanGoBackAndForward_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 10, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_HorizontalScroll_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_HorizontalScroll_t)default(HTML_HorizontalScroll_t).Fill( pvParam ) );
		static Action<HTML_HorizontalScroll_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_HorizontalScroll_t)default(HTML_HorizontalScroll_t).Fill( pvParam ) );
		public static void Install( Action<HTML_HorizontalScroll_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_HorizontalScroll_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_HorizontalScroll_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 11, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_VerticalScroll_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_VerticalScroll_t)default(HTML_VerticalScroll_t).Fill( pvParam ) );
		static Action<HTML_VerticalScroll_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_VerticalScroll_t)default(HTML_VerticalScroll_t).Fill( pvParam ) );
		public static void Install( Action<HTML_VerticalScroll_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_VerticalScroll_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_VerticalScroll_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 12, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_LinkAtPosition_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_LinkAtPosition_t)default(HTML_LinkAtPosition_t).Fill( pvParam ) );
		static Action<HTML_LinkAtPosition_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_LinkAtPosition_t)default(HTML_LinkAtPosition_t).Fill( pvParam ) );
		public static void Install( Action<HTML_LinkAtPosition_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_LinkAtPosition_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_LinkAtPosition_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 13, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_JSAlert_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_JSAlert_t)default(HTML_JSAlert_t).Fill( pvParam ) );
		static Action<HTML_JSAlert_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_JSAlert_t)default(HTML_JSAlert_t).Fill( pvParam ) );
		public static void Install( Action<HTML_JSAlert_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_JSAlert_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_JSAlert_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 14, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_JSConfirm_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_JSConfirm_t)default(HTML_JSConfirm_t).Fill( pvParam ) );
		static Action<HTML_JSConfirm_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_JSConfirm_t)default(HTML_JSConfirm_t).Fill( pvParam ) );
		public static void Install( Action<HTML_JSConfirm_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_JSConfirm_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 15, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_JSConfirm_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 15, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_FileOpenDialog_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_FileOpenDialog_t)default(HTML_FileOpenDialog_t).Fill( pvParam ) );
		static Action<HTML_FileOpenDialog_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_FileOpenDialog_t)default(HTML_FileOpenDialog_t).Fill( pvParam ) );
		public static void Install( Action<HTML_FileOpenDialog_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_FileOpenDialog_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 16, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_FileOpenDialog_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 16, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_NewWindow_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_NewWindow_t)default(HTML_NewWindow_t).Fill( pvParam ) );
		static Action<HTML_NewWindow_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_NewWindow_t)default(HTML_NewWindow_t).Fill( pvParam ) );
		public static void Install( Action<HTML_NewWindow_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_NewWindow_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 21, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_NewWindow_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 21, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_SetCursor_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_SetCursor_t)default(HTML_SetCursor_t).Fill( pvParam ) );
		static Action<HTML_SetCursor_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_SetCursor_t)default(HTML_SetCursor_t).Fill( pvParam ) );
		public static void Install( Action<HTML_SetCursor_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_SetCursor_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 22, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_SetCursor_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 22, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_StatusText_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_StatusText_t)default(HTML_StatusText_t).Fill( pvParam ) );
		static Action<HTML_StatusText_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_StatusText_t)default(HTML_StatusText_t).Fill( pvParam ) );
		public static void Install( Action<HTML_StatusText_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_StatusText_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 23, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_StatusText_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 23, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_ShowToolTip_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_ShowToolTip_t)default(HTML_ShowToolTip_t).Fill( pvParam ) );
		static Action<HTML_ShowToolTip_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_ShowToolTip_t)default(HTML_ShowToolTip_t).Fill( pvParam ) );
		public static void Install( Action<HTML_ShowToolTip_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_ShowToolTip_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 24, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_ShowToolTip_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 24, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_UpdateToolTip_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_UpdateToolTip_t)default(HTML_UpdateToolTip_t).Fill( pvParam ) );
		static Action<HTML_UpdateToolTip_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_UpdateToolTip_t)default(HTML_UpdateToolTip_t).Fill( pvParam ) );
		public static void Install( Action<HTML_UpdateToolTip_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_UpdateToolTip_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 25, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_UpdateToolTip_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 25, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_HideToolTip_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_HideToolTip_t)default(HTML_HideToolTip_t).Fill( pvParam ) );
		static Action<HTML_HideToolTip_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_HideToolTip_t)default(HTML_HideToolTip_t).Fill( pvParam ) );
		public static void Install( Action<HTML_HideToolTip_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_HideToolTip_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 26, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_HideToolTip_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 26, false );
				actionClient = action;
			}
		}
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
		static Action<HTML_BrowserRestarted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (HTML_BrowserRestarted_t)default(HTML_BrowserRestarted_t).Fill( pvParam ) );
		static Action<HTML_BrowserRestarted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (HTML_BrowserRestarted_t)default(HTML_BrowserRestarted_t).Fill( pvParam ) );
		public static void Install( Action<HTML_BrowserRestarted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(HTML_BrowserRestarted_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 27, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(HTML_BrowserRestarted_t).GetStructSize(), CallbackIdentifiers.SteamHTMLSurface + 27, false );
				actionClient = action;
			}
		}
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
		static Action<SteamInventoryResultReady_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SteamInventoryResultReady_t)default(SteamInventoryResultReady_t).Fill( pvParam ) );
		static Action<SteamInventoryResultReady_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SteamInventoryResultReady_t)default(SteamInventoryResultReady_t).Fill( pvParam ) );
		public static void Install( Action<SteamInventoryResultReady_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SteamInventoryResultReady_t).GetStructSize(), CallbackIdentifiers.ClientInventory + 0, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SteamInventoryResultReady_t).GetStructSize(), CallbackIdentifiers.ClientInventory + 0, false );
				actionClient = action;
			}
		}
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
		static Action<SteamInventoryFullUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SteamInventoryFullUpdate_t)default(SteamInventoryFullUpdate_t).Fill( pvParam ) );
		static Action<SteamInventoryFullUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SteamInventoryFullUpdate_t)default(SteamInventoryFullUpdate_t).Fill( pvParam ) );
		public static void Install( Action<SteamInventoryFullUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SteamInventoryFullUpdate_t).GetStructSize(), CallbackIdentifiers.ClientInventory + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SteamInventoryFullUpdate_t).GetStructSize(), CallbackIdentifiers.ClientInventory + 1, false );
				actionClient = action;
			}
		}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamInventoryEligiblePromoItemDefIDs_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((SteamInventoryEligiblePromoItemDefIDs_t)(SteamInventoryEligiblePromoItemDefIDs_t) Marshal.PtrToStructure( p, typeof(SteamInventoryEligiblePromoItemDefIDs_t) ) );
		static Action<SteamInventoryEligiblePromoItemDefIDs_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SteamInventoryEligiblePromoItemDefIDs_t)default(SteamInventoryEligiblePromoItemDefIDs_t).Fill( pvParam ) );
		static Action<SteamInventoryEligiblePromoItemDefIDs_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SteamInventoryEligiblePromoItemDefIDs_t)default(SteamInventoryEligiblePromoItemDefIDs_t).Fill( pvParam ) );
		public static void Install( Action<SteamInventoryEligiblePromoItemDefIDs_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SteamInventoryEligiblePromoItemDefIDs_t).GetStructSize(), CallbackIdentifiers.ClientInventory + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SteamInventoryEligiblePromoItemDefIDs_t).GetStructSize(), CallbackIdentifiers.ClientInventory + 3, false );
				actionClient = action;
			}
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
		static Action<SteamInventoryStartPurchaseResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SteamInventoryStartPurchaseResult_t)default(SteamInventoryStartPurchaseResult_t).Fill( pvParam ) );
		static Action<SteamInventoryStartPurchaseResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SteamInventoryStartPurchaseResult_t)default(SteamInventoryStartPurchaseResult_t).Fill( pvParam ) );
		public static void Install( Action<SteamInventoryStartPurchaseResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SteamInventoryStartPurchaseResult_t).GetStructSize(), CallbackIdentifiers.ClientInventory + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SteamInventoryStartPurchaseResult_t).GetStructSize(), CallbackIdentifiers.ClientInventory + 4, false );
				actionClient = action;
			}
		}
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
		static Action<SteamInventoryRequestPricesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SteamInventoryRequestPricesResult_t)default(SteamInventoryRequestPricesResult_t).Fill( pvParam ) );
		static Action<SteamInventoryRequestPricesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SteamInventoryRequestPricesResult_t)default(SteamInventoryRequestPricesResult_t).Fill( pvParam ) );
		public static void Install( Action<SteamInventoryRequestPricesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SteamInventoryRequestPricesResult_t).GetStructSize(), CallbackIdentifiers.ClientInventory + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SteamInventoryRequestPricesResult_t).GetStructSize(), CallbackIdentifiers.ClientInventory + 5, false );
				actionClient = action;
			}
		}
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
		static Action<BroadcastUploadStop_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (BroadcastUploadStop_t)default(BroadcastUploadStop_t).Fill( pvParam ) );
		static Action<BroadcastUploadStop_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (BroadcastUploadStop_t)default(BroadcastUploadStop_t).Fill( pvParam ) );
		public static void Install( Action<BroadcastUploadStop_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(BroadcastUploadStop_t).GetStructSize(), CallbackIdentifiers.ClientVideo + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(BroadcastUploadStop_t).GetStructSize(), CallbackIdentifiers.ClientVideo + 5, false );
				actionClient = action;
			}
		}
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
		static Action<GetVideoURLResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GetVideoURLResult_t)default(GetVideoURLResult_t).Fill( pvParam ) );
		static Action<GetVideoURLResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GetVideoURLResult_t)default(GetVideoURLResult_t).Fill( pvParam ) );
		public static void Install( Action<GetVideoURLResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GetVideoURLResult_t).GetStructSize(), CallbackIdentifiers.ClientVideo + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GetVideoURLResult_t).GetStructSize(), CallbackIdentifiers.ClientVideo + 11, false );
				actionClient = action;
			}
		}
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
		static Action<GetOPFSettingsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GetOPFSettingsResult_t)default(GetOPFSettingsResult_t).Fill( pvParam ) );
		static Action<GetOPFSettingsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GetOPFSettingsResult_t)default(GetOPFSettingsResult_t).Fill( pvParam ) );
		public static void Install( Action<GetOPFSettingsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GetOPFSettingsResult_t).GetStructSize(), CallbackIdentifiers.ClientVideo + 24, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GetOPFSettingsResult_t).GetStructSize(), CallbackIdentifiers.ClientVideo + 24, false );
				actionClient = action;
			}
		}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientApprove_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((GSClientApprove_t)(GSClientApprove_t) Marshal.PtrToStructure( p, typeof(GSClientApprove_t) ) );
		static Action<GSClientApprove_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GSClientApprove_t)default(GSClientApprove_t).Fill( pvParam ) );
		static Action<GSClientApprove_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GSClientApprove_t)default(GSClientApprove_t).Fill( pvParam ) );
		public static void Install( Action<GSClientApprove_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GSClientApprove_t).GetStructSize(), CallbackIdentifiers.SteamGameServer + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GSClientApprove_t).GetStructSize(), CallbackIdentifiers.SteamGameServer + 1, false );
				actionClient = action;
			}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientDeny_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((GSClientDeny_t)(GSClientDeny_t) Marshal.PtrToStructure( p, typeof(GSClientDeny_t) ) );
		static Action<GSClientDeny_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GSClientDeny_t)default(GSClientDeny_t).Fill( pvParam ) );
		static Action<GSClientDeny_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GSClientDeny_t)default(GSClientDeny_t).Fill( pvParam ) );
		public static void Install( Action<GSClientDeny_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GSClientDeny_t).GetStructSize(), CallbackIdentifiers.SteamGameServer + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GSClientDeny_t).GetStructSize(), CallbackIdentifiers.SteamGameServer + 2, false );
				actionClient = action;
			}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientKick_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((GSClientKick_t)(GSClientKick_t) Marshal.PtrToStructure( p, typeof(GSClientKick_t) ) );
		static Action<GSClientKick_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GSClientKick_t)default(GSClientKick_t).Fill( pvParam ) );
		static Action<GSClientKick_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GSClientKick_t)default(GSClientKick_t).Fill( pvParam ) );
		public static void Install( Action<GSClientKick_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GSClientKick_t).GetStructSize(), CallbackIdentifiers.SteamGameServer + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GSClientKick_t).GetStructSize(), CallbackIdentifiers.SteamGameServer + 3, false );
				actionClient = action;
			}
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
		static Action<GSClientAchievementStatus_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GSClientAchievementStatus_t)default(GSClientAchievementStatus_t).Fill( pvParam ) );
		static Action<GSClientAchievementStatus_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GSClientAchievementStatus_t)default(GSClientAchievementStatus_t).Fill( pvParam ) );
		public static void Install( Action<GSClientAchievementStatus_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GSClientAchievementStatus_t).GetStructSize(), CallbackIdentifiers.SteamGameServer + 6, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GSClientAchievementStatus_t).GetStructSize(), CallbackIdentifiers.SteamGameServer + 6, false );
				actionClient = action;
			}
		}
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
		static Action<GSPolicyResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GSPolicyResponse_t)default(GSPolicyResponse_t).Fill( pvParam ) );
		static Action<GSPolicyResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GSPolicyResponse_t)default(GSPolicyResponse_t).Fill( pvParam ) );
		public static void Install( Action<GSPolicyResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GSPolicyResponse_t).GetStructSize(), CallbackIdentifiers.SteamUser + 15, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GSPolicyResponse_t).GetStructSize(), CallbackIdentifiers.SteamUser + 15, false );
				actionClient = action;
			}
		}
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
		static Action<GSGameplayStats_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GSGameplayStats_t)default(GSGameplayStats_t).Fill( pvParam ) );
		static Action<GSGameplayStats_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GSGameplayStats_t)default(GSGameplayStats_t).Fill( pvParam ) );
		public static void Install( Action<GSGameplayStats_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GSGameplayStats_t).GetStructSize(), CallbackIdentifiers.SteamGameServer + 7, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GSGameplayStats_t).GetStructSize(), CallbackIdentifiers.SteamGameServer + 7, false );
				actionClient = action;
			}
		}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientGroupStatus_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((GSClientGroupStatus_t)(GSClientGroupStatus_t) Marshal.PtrToStructure( p, typeof(GSClientGroupStatus_t) ) );
		static Action<GSClientGroupStatus_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GSClientGroupStatus_t)default(GSClientGroupStatus_t).Fill( pvParam ) );
		static Action<GSClientGroupStatus_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GSClientGroupStatus_t)default(GSClientGroupStatus_t).Fill( pvParam ) );
		public static void Install( Action<GSClientGroupStatus_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GSClientGroupStatus_t).GetStructSize(), CallbackIdentifiers.SteamGameServer + 8, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GSClientGroupStatus_t).GetStructSize(), CallbackIdentifiers.SteamGameServer + 8, false );
				actionClient = action;
			}
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
		static Action<GSReputation_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GSReputation_t)default(GSReputation_t).Fill( pvParam ) );
		static Action<GSReputation_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GSReputation_t)default(GSReputation_t).Fill( pvParam ) );
		public static void Install( Action<GSReputation_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GSReputation_t).GetStructSize(), CallbackIdentifiers.SteamGameServer + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GSReputation_t).GetStructSize(), CallbackIdentifiers.SteamGameServer + 9, false );
				actionClient = action;
			}
		}
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
		static Action<AssociateWithClanResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (AssociateWithClanResult_t)default(AssociateWithClanResult_t).Fill( pvParam ) );
		static Action<AssociateWithClanResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (AssociateWithClanResult_t)default(AssociateWithClanResult_t).Fill( pvParam ) );
		public static void Install( Action<AssociateWithClanResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(AssociateWithClanResult_t).GetStructSize(), CallbackIdentifiers.SteamGameServer + 10, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(AssociateWithClanResult_t).GetStructSize(), CallbackIdentifiers.SteamGameServer + 10, false );
				actionClient = action;
			}
		}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(ComputeNewPlayerCompatibilityResult_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((ComputeNewPlayerCompatibilityResult_t)(ComputeNewPlayerCompatibilityResult_t) Marshal.PtrToStructure( p, typeof(ComputeNewPlayerCompatibilityResult_t) ) );
		static Action<ComputeNewPlayerCompatibilityResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (ComputeNewPlayerCompatibilityResult_t)default(ComputeNewPlayerCompatibilityResult_t).Fill( pvParam ) );
		static Action<ComputeNewPlayerCompatibilityResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (ComputeNewPlayerCompatibilityResult_t)default(ComputeNewPlayerCompatibilityResult_t).Fill( pvParam ) );
		public static void Install( Action<ComputeNewPlayerCompatibilityResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(ComputeNewPlayerCompatibilityResult_t).GetStructSize(), CallbackIdentifiers.SteamGameServer + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(ComputeNewPlayerCompatibilityResult_t).GetStructSize(), CallbackIdentifiers.SteamGameServer + 11, false );
				actionClient = action;
			}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSStatsReceived_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((GSStatsReceived_t)(GSStatsReceived_t) Marshal.PtrToStructure( p, typeof(GSStatsReceived_t) ) );
		static Action<GSStatsReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GSStatsReceived_t)default(GSStatsReceived_t).Fill( pvParam ) );
		static Action<GSStatsReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GSStatsReceived_t)default(GSStatsReceived_t).Fill( pvParam ) );
		public static void Install( Action<GSStatsReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GSStatsReceived_t).GetStructSize(), CallbackIdentifiers.SteamGameServerStats + 0, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GSStatsReceived_t).GetStructSize(), CallbackIdentifiers.SteamGameServerStats + 0, false );
				actionClient = action;
			}
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
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSStatsStored_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((GSStatsStored_t)(GSStatsStored_t) Marshal.PtrToStructure( p, typeof(GSStatsStored_t) ) );
		static Action<GSStatsStored_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GSStatsStored_t)default(GSStatsStored_t).Fill( pvParam ) );
		static Action<GSStatsStored_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GSStatsStored_t)default(GSStatsStored_t).Fill( pvParam ) );
		public static void Install( Action<GSStatsStored_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GSStatsStored_t).GetStructSize(), CallbackIdentifiers.SteamGameServerStats + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GSStatsStored_t).GetStructSize(), CallbackIdentifiers.SteamGameServerStats + 1, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSStatsUnloaded_t : Steamworks.ISteamCallback
	{
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region ISteamCallback
		public int GetCallbackId() => CallbackIdentifiers.SteamUserStats + 8;
		public int GetStructSize() => System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSStatsUnloaded_t) );
		public Steamworks.ISteamCallback Fill( IntPtr p ) => ((GSStatsUnloaded_t)(GSStatsUnloaded_t) Marshal.PtrToStructure( p, typeof(GSStatsUnloaded_t) ) );
		static Action<GSStatsUnloaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GSStatsUnloaded_t)default(GSStatsUnloaded_t).Fill( pvParam ) );
		static Action<GSStatsUnloaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GSStatsUnloaded_t)default(GSStatsUnloaded_t).Fill( pvParam ) );
		public static void Install( Action<GSStatsUnloaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GSStatsUnloaded_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 8, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GSStatsUnloaded_t).GetStructSize(), CallbackIdentifiers.SteamUserStats + 8, false );
				actionClient = action;
			}
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
		static Action<PlaybackStatusHasChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (PlaybackStatusHasChanged_t)default(PlaybackStatusHasChanged_t).Fill( pvParam ) );
		static Action<PlaybackStatusHasChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (PlaybackStatusHasChanged_t)default(PlaybackStatusHasChanged_t).Fill( pvParam ) );
		public static void Install( Action<PlaybackStatusHasChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(PlaybackStatusHasChanged_t).GetStructSize(), CallbackIdentifiers.SteamMusic + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(PlaybackStatusHasChanged_t).GetStructSize(), CallbackIdentifiers.SteamMusic + 1, false );
				actionClient = action;
			}
		}
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
		static Action<BroadcastUploadStart_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (BroadcastUploadStart_t)default(BroadcastUploadStart_t).Fill( pvParam ) );
		static Action<BroadcastUploadStart_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (BroadcastUploadStart_t)default(BroadcastUploadStart_t).Fill( pvParam ) );
		public static void Install( Action<BroadcastUploadStart_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(BroadcastUploadStart_t).GetStructSize(), CallbackIdentifiers.ClientVideo + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(BroadcastUploadStart_t).GetStructSize(), CallbackIdentifiers.ClientVideo + 4, false );
				actionClient = action;
			}
		}
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
		static Action<NewUrlLaunchParameters_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (NewUrlLaunchParameters_t)default(NewUrlLaunchParameters_t).Fill( pvParam ) );
		static Action<NewUrlLaunchParameters_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (NewUrlLaunchParameters_t)default(NewUrlLaunchParameters_t).Fill( pvParam ) );
		public static void Install( Action<NewUrlLaunchParameters_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(NewUrlLaunchParameters_t).GetStructSize(), CallbackIdentifiers.SteamApps + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(NewUrlLaunchParameters_t).GetStructSize(), CallbackIdentifiers.SteamApps + 14, false );
				actionClient = action;
			}
		}
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
		static Action<ItemInstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (ItemInstalled_t)default(ItemInstalled_t).Fill( pvParam ) );
		static Action<ItemInstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (ItemInstalled_t)default(ItemInstalled_t).Fill( pvParam ) );
		public static void Install( Action<ItemInstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(ItemInstalled_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(ItemInstalled_t).GetStructSize(), CallbackIdentifiers.ClientUGC + 5, false );
				actionClient = action;
			}
		}
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
		static Action<SteamInventoryDefinitionUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SteamInventoryDefinitionUpdate_t)default(SteamInventoryDefinitionUpdate_t).Fill( pvParam ) );
		static Action<SteamInventoryDefinitionUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SteamInventoryDefinitionUpdate_t)default(SteamInventoryDefinitionUpdate_t).Fill( pvParam ) );
		public static void Install( Action<SteamInventoryDefinitionUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SteamInventoryDefinitionUpdate_t).GetStructSize(), CallbackIdentifiers.ClientInventory + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SteamInventoryDefinitionUpdate_t).GetStructSize(), CallbackIdentifiers.ClientInventory + 2, false );
				actionClient = action;
			}
		}
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
		static Action<SteamParentalSettingsChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SteamParentalSettingsChanged_t)default(SteamParentalSettingsChanged_t).Fill( pvParam ) );
		static Action<SteamParentalSettingsChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SteamParentalSettingsChanged_t)default(SteamParentalSettingsChanged_t).Fill( pvParam ) );
		public static void Install( Action<SteamParentalSettingsChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SteamParentalSettingsChanged_t).GetStructSize(), CallbackIdentifiers.SteamParentalSettings + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SteamParentalSettingsChanged_t).GetStructSize(), CallbackIdentifiers.SteamParentalSettings + 1, false );
				actionClient = action;
			}
		}
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
		static Action<SteamServersConnected_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SteamServersConnected_t)default(SteamServersConnected_t).Fill( pvParam ) );
		static Action<SteamServersConnected_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SteamServersConnected_t)default(SteamServersConnected_t).Fill( pvParam ) );
		public static void Install( Action<SteamServersConnected_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SteamServersConnected_t).GetStructSize(), CallbackIdentifiers.SteamUser + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SteamServersConnected_t).GetStructSize(), CallbackIdentifiers.SteamUser + 1, false );
				actionClient = action;
			}
		}
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
		static Action<GCMessageAvailable_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GCMessageAvailable_t)default(GCMessageAvailable_t).Fill( pvParam ) );
		static Action<GCMessageAvailable_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GCMessageAvailable_t)default(GCMessageAvailable_t).Fill( pvParam ) );
		public static void Install( Action<GCMessageAvailable_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GCMessageAvailable_t).GetStructSize(), CallbackIdentifiers.SteamGameCoordinator + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GCMessageAvailable_t).GetStructSize(), CallbackIdentifiers.SteamGameCoordinator + 1, false );
				actionClient = action;
			}
		}
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
		static Action<GCMessageFailed_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (GCMessageFailed_t)default(GCMessageFailed_t).Fill( pvParam ) );
		static Action<GCMessageFailed_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (GCMessageFailed_t)default(GCMessageFailed_t).Fill( pvParam ) );
		public static void Install( Action<GCMessageFailed_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(GCMessageFailed_t).GetStructSize(), CallbackIdentifiers.SteamGameCoordinator + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(GCMessageFailed_t).GetStructSize(), CallbackIdentifiers.SteamGameCoordinator + 2, false );
				actionClient = action;
			}
		}
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
		static Action<ScreenshotRequested_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (ScreenshotRequested_t)default(ScreenshotRequested_t).Fill( pvParam ) );
		static Action<ScreenshotRequested_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (ScreenshotRequested_t)default(ScreenshotRequested_t).Fill( pvParam ) );
		public static void Install( Action<ScreenshotRequested_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(ScreenshotRequested_t).GetStructSize(), CallbackIdentifiers.SteamScreenshots + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(ScreenshotRequested_t).GetStructSize(), CallbackIdentifiers.SteamScreenshots + 2, false );
				actionClient = action;
			}
		}
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
		static Action<LicensesUpdated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (LicensesUpdated_t)default(LicensesUpdated_t).Fill( pvParam ) );
		static Action<LicensesUpdated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (LicensesUpdated_t)default(LicensesUpdated_t).Fill( pvParam ) );
		public static void Install( Action<LicensesUpdated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(LicensesUpdated_t).GetStructSize(), CallbackIdentifiers.SteamUser + 25, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(LicensesUpdated_t).GetStructSize(), CallbackIdentifiers.SteamUser + 25, false );
				actionClient = action;
			}
		}
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
		static Action<SteamShutdown_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (SteamShutdown_t)default(SteamShutdown_t).Fill( pvParam ) );
		static Action<SteamShutdown_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (SteamShutdown_t)default(SteamShutdown_t).Fill( pvParam ) );
		public static void Install( Action<SteamShutdown_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(SteamShutdown_t).GetStructSize(), CallbackIdentifiers.SteamUtils + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(SteamShutdown_t).GetStructSize(), CallbackIdentifiers.SteamUtils + 4, false );
				actionClient = action;
			}
		}
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
		static Action<IPCountry_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (IPCountry_t)default(IPCountry_t).Fill( pvParam ) );
		static Action<IPCountry_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (IPCountry_t)default(IPCountry_t).Fill( pvParam ) );
		public static void Install( Action<IPCountry_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(IPCountry_t).GetStructSize(), CallbackIdentifiers.SteamUtils + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(IPCountry_t).GetStructSize(), CallbackIdentifiers.SteamUtils + 1, false );
				actionClient = action;
			}
		}
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
		static Action<IPCFailure_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( (IPCFailure_t)default(IPCFailure_t).Fill( pvParam ) );
		static Action<IPCFailure_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( (IPCFailure_t)default(IPCFailure_t).Fill( pvParam ) );
		public static void Install( Action<IPCFailure_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, default(IPCFailure_t).GetStructSize(), CallbackIdentifiers.SteamUser + 17, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, default(IPCFailure_t).GetStructSize(), CallbackIdentifiers.SteamUser + 17, false );
				actionClient = action;
			}
		}
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
