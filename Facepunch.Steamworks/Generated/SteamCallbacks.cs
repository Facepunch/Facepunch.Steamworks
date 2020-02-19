using System;
using System.Runtime.InteropServices;
using System.Linq;
using Steamworks.Data;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamServersConnected_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamServersConnected_t) );
		public int DataSize => _datasize;
		public int CallbackId => 101;
		internal static SteamServersConnected_t Fill( IntPtr p ) => ((SteamServersConnected_t)Marshal.PtrToStructure( p, typeof(SteamServersConnected_t) ) );
		
		static Action<SteamServersConnected_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamServersConnected_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamServersConnected_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 101, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 101, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamServerConnectFailure_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool StillRetrying; // m_bStillRetrying bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamServerConnectFailure_t) );
		public int DataSize => _datasize;
		public int CallbackId => 102;
		internal static SteamServerConnectFailure_t Fill( IntPtr p ) => ((SteamServerConnectFailure_t)Marshal.PtrToStructure( p, typeof(SteamServerConnectFailure_t) ) );
		
		static Action<SteamServerConnectFailure_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamServerConnectFailure_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamServerConnectFailure_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 102, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 102, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamServersDisconnected_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamServersDisconnected_t) );
		public int DataSize => _datasize;
		public int CallbackId => 103;
		internal static SteamServersDisconnected_t Fill( IntPtr p ) => ((SteamServersDisconnected_t)Marshal.PtrToStructure( p, typeof(SteamServersDisconnected_t) ) );
		
		static Action<SteamServersDisconnected_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamServersDisconnected_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamServersDisconnected_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 103, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 103, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct ClientGameServerDeny_t : ICallbackData
	{
		internal uint AppID; // m_uAppID uint32
		internal uint GameServerIP; // m_unGameServerIP uint32
		internal ushort GameServerPort; // m_usGameServerPort uint16
		internal ushort Secure; // m_bSecure uint16
		internal uint Reason; // m_uReason uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ClientGameServerDeny_t) );
		public int DataSize => _datasize;
		public int CallbackId => 113;
		internal static ClientGameServerDeny_t Fill( IntPtr p ) => ((ClientGameServerDeny_t)Marshal.PtrToStructure( p, typeof(ClientGameServerDeny_t) ) );
		
		static Action<ClientGameServerDeny_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ClientGameServerDeny_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ClientGameServerDeny_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 113, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 113, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct IPCFailure_t : ICallbackData
	{
		internal byte FailureType; // m_eFailureType uint8
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(IPCFailure_t) );
		public int DataSize => _datasize;
		public int CallbackId => 117;
		internal static IPCFailure_t Fill( IntPtr p ) => ((IPCFailure_t)Marshal.PtrToStructure( p, typeof(IPCFailure_t) ) );
		
		static Action<IPCFailure_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<IPCFailure_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<IPCFailure_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 117, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 117, false );
				actionClient = action;
			}
		}
		#endregion
		internal enum EFailureType : int
		{
			FlushedCallbackQueue = 0,
			PipeFail = 1,
		}
		
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LicensesUpdated_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LicensesUpdated_t) );
		public int DataSize => _datasize;
		public int CallbackId => 125;
		internal static LicensesUpdated_t Fill( IntPtr p ) => ((LicensesUpdated_t)Marshal.PtrToStructure( p, typeof(LicensesUpdated_t) ) );
		
		static Action<LicensesUpdated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LicensesUpdated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LicensesUpdated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 125, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 125, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct ValidateAuthTicketResponse_t : ICallbackData
	{
		internal ulong SteamID; // m_SteamID CSteamID
		internal AuthResponse AuthSessionResponse; // m_eAuthSessionResponse EAuthSessionResponse
		internal ulong OwnerSteamID; // m_OwnerSteamID CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ValidateAuthTicketResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => 143;
		internal static ValidateAuthTicketResponse_t Fill( IntPtr p ) => ((ValidateAuthTicketResponse_t)Marshal.PtrToStructure( p, typeof(ValidateAuthTicketResponse_t) ) );
		
		static Action<ValidateAuthTicketResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ValidateAuthTicketResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ValidateAuthTicketResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 143, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 143, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MicroTxnAuthorizationResponse_t : ICallbackData
	{
		internal uint AppID; // m_unAppID uint32
		internal ulong OrderID; // m_ulOrderID uint64
		internal byte Authorized; // m_bAuthorized uint8
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MicroTxnAuthorizationResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => 152;
		internal static MicroTxnAuthorizationResponse_t Fill( IntPtr p ) => ((MicroTxnAuthorizationResponse_t)Marshal.PtrToStructure( p, typeof(MicroTxnAuthorizationResponse_t) ) );
		
		static Action<MicroTxnAuthorizationResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MicroTxnAuthorizationResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MicroTxnAuthorizationResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 152, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 152, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct EncryptedAppTicketResponse_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(EncryptedAppTicketResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => 154;
		internal static EncryptedAppTicketResponse_t Fill( IntPtr p ) => ((EncryptedAppTicketResponse_t)Marshal.PtrToStructure( p, typeof(EncryptedAppTicketResponse_t) ) );
		
		static Action<EncryptedAppTicketResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<EncryptedAppTicketResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<EncryptedAppTicketResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 154, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 154, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GetAuthSessionTicketResponse_t : ICallbackData
	{
		internal uint AuthTicket; // m_hAuthTicket HAuthTicket
		internal Result Result; // m_eResult EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GetAuthSessionTicketResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => 163;
		internal static GetAuthSessionTicketResponse_t Fill( IntPtr p ) => ((GetAuthSessionTicketResponse_t)Marshal.PtrToStructure( p, typeof(GetAuthSessionTicketResponse_t) ) );
		
		static Action<GetAuthSessionTicketResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GetAuthSessionTicketResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GetAuthSessionTicketResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 163, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 163, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GameWebCallback_t : ICallbackData
	{
		internal string URLUTF8() => System.Text.Encoding.UTF8.GetString( URL, 0, System.Array.IndexOf<byte>( URL, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // byte[] m_szURL
		internal byte[] URL; // m_szURL char [256]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameWebCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => 164;
		internal static GameWebCallback_t Fill( IntPtr p ) => ((GameWebCallback_t)Marshal.PtrToStructure( p, typeof(GameWebCallback_t) ) );
		
		static Action<GameWebCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameWebCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameWebCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 164, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 164, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct StoreAuthURLResponse_t : ICallbackData
	{
		internal string URLUTF8() => System.Text.Encoding.UTF8.GetString( URL, 0, System.Array.IndexOf<byte>( URL, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)] // byte[] m_szURL
		internal byte[] URL; // m_szURL char [512]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(StoreAuthURLResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => 165;
		internal static StoreAuthURLResponse_t Fill( IntPtr p ) => ((StoreAuthURLResponse_t)Marshal.PtrToStructure( p, typeof(StoreAuthURLResponse_t) ) );
		
		static Action<StoreAuthURLResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<StoreAuthURLResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<StoreAuthURLResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 165, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 165, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MarketEligibilityResponse_t : ICallbackData
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Allowed; // m_bAllowed bool
		internal MarketNotAllowedReasonFlags NotAllowedReason; // m_eNotAllowedReason EMarketNotAllowedReasonFlags
		internal uint TAllowedAtTime; // m_rtAllowedAtTime RTime32
		internal int CdaySteamGuardRequiredDays; // m_cdaySteamGuardRequiredDays int
		internal int CdayNewDeviceCooldown; // m_cdayNewDeviceCooldown int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MarketEligibilityResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => 166;
		internal static MarketEligibilityResponse_t Fill( IntPtr p ) => ((MarketEligibilityResponse_t)Marshal.PtrToStructure( p, typeof(MarketEligibilityResponse_t) ) );
		
		static Action<MarketEligibilityResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MarketEligibilityResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MarketEligibilityResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 166, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 166, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct DurationControl_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal AppId Appid; // m_appid AppId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool Applicable; // m_bApplicable bool
		internal int CsecsLast5h; // m_csecsLast5h int32
		internal DurationControlProgress Progress; // m_progress EDurationControlProgress
		internal DurationControlNotification Otification; // m_notification EDurationControlNotification
		internal int CsecsToday; // m_csecsToday int32
		internal int CsecsRemaining; // m_csecsRemaining int32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(DurationControl_t) );
		public int DataSize => _datasize;
		public int CallbackId => 167;
		internal static DurationControl_t Fill( IntPtr p ) => ((DurationControl_t)Marshal.PtrToStructure( p, typeof(DurationControl_t) ) );
		
		static Action<DurationControl_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<DurationControl_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<DurationControl_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 167, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 167, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct FriendStateChange_t : ICallbackData
	{
		internal ulong SteamID; // m_ulSteamID uint64
		internal int ChangeFlags; // m_nChangeFlags int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendStateChange_t) );
		public int DataSize => _datasize;
		public int CallbackId => 304;
		internal static FriendStateChange_t Fill( IntPtr p ) => ((FriendStateChange_t)Marshal.PtrToStructure( p, typeof(FriendStateChange_t) ) );
		
		static Action<FriendStateChange_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FriendStateChange_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FriendStateChange_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 304, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 304, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GameOverlayActivated_t : ICallbackData
	{
		internal byte Active; // m_bActive uint8
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameOverlayActivated_t) );
		public int DataSize => _datasize;
		public int CallbackId => 331;
		internal static GameOverlayActivated_t Fill( IntPtr p ) => ((GameOverlayActivated_t)Marshal.PtrToStructure( p, typeof(GameOverlayActivated_t) ) );
		
		static Action<GameOverlayActivated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameOverlayActivated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameOverlayActivated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 331, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 331, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GameServerChangeRequested_t : ICallbackData
	{
		internal string ServerUTF8() => System.Text.Encoding.UTF8.GetString( Server, 0, System.Array.IndexOf<byte>( Server, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)] // byte[] m_rgchServer
		internal byte[] Server; // m_rgchServer char [64]
		internal string PasswordUTF8() => System.Text.Encoding.UTF8.GetString( Password, 0, System.Array.IndexOf<byte>( Password, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)] // byte[] m_rgchPassword
		internal byte[] Password; // m_rgchPassword char [64]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameServerChangeRequested_t) );
		public int DataSize => _datasize;
		public int CallbackId => 332;
		internal static GameServerChangeRequested_t Fill( IntPtr p ) => ((GameServerChangeRequested_t)Marshal.PtrToStructure( p, typeof(GameServerChangeRequested_t) ) );
		
		static Action<GameServerChangeRequested_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameServerChangeRequested_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameServerChangeRequested_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 332, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 332, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GameLobbyJoinRequested_t : ICallbackData
	{
		internal ulong SteamIDLobby; // m_steamIDLobby CSteamID
		internal ulong SteamIDFriend; // m_steamIDFriend CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameLobbyJoinRequested_t) );
		public int DataSize => _datasize;
		public int CallbackId => 333;
		internal static GameLobbyJoinRequested_t Fill( IntPtr p ) => ((GameLobbyJoinRequested_t)Marshal.PtrToStructure( p, typeof(GameLobbyJoinRequested_t) ) );
		
		static Action<GameLobbyJoinRequested_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameLobbyJoinRequested_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameLobbyJoinRequested_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 333, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 333, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct AvatarImageLoaded_t : ICallbackData
	{
		internal ulong SteamID; // m_steamID CSteamID
		internal int Image; // m_iImage int
		internal int Wide; // m_iWide int
		internal int Tall; // m_iTall int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(AvatarImageLoaded_t) );
		public int DataSize => _datasize;
		public int CallbackId => 334;
		internal static AvatarImageLoaded_t Fill( IntPtr p ) => ((AvatarImageLoaded_t)Marshal.PtrToStructure( p, typeof(AvatarImageLoaded_t) ) );
		
		static Action<AvatarImageLoaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<AvatarImageLoaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<AvatarImageLoaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 334, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 334, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct ClanOfficerListResponse_t : ICallbackData
	{
		internal ulong SteamIDClan; // m_steamIDClan CSteamID
		internal int COfficers; // m_cOfficers int
		internal byte Success; // m_bSuccess uint8
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ClanOfficerListResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => 335;
		internal static ClanOfficerListResponse_t Fill( IntPtr p ) => ((ClanOfficerListResponse_t)Marshal.PtrToStructure( p, typeof(ClanOfficerListResponse_t) ) );
		
		static Action<ClanOfficerListResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ClanOfficerListResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ClanOfficerListResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 335, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 335, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct FriendRichPresenceUpdate_t : ICallbackData
	{
		internal ulong SteamIDFriend; // m_steamIDFriend CSteamID
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendRichPresenceUpdate_t) );
		public int DataSize => _datasize;
		public int CallbackId => 336;
		internal static FriendRichPresenceUpdate_t Fill( IntPtr p ) => ((FriendRichPresenceUpdate_t)Marshal.PtrToStructure( p, typeof(FriendRichPresenceUpdate_t) ) );
		
		static Action<FriendRichPresenceUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FriendRichPresenceUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FriendRichPresenceUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 336, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 336, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GameRichPresenceJoinRequested_t : ICallbackData
	{
		internal ulong SteamIDFriend; // m_steamIDFriend CSteamID
		internal string ConnectUTF8() => System.Text.Encoding.UTF8.GetString( Connect, 0, System.Array.IndexOf<byte>( Connect, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // byte[] m_rgchConnect
		internal byte[] Connect; // m_rgchConnect char [256]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameRichPresenceJoinRequested_t) );
		public int DataSize => _datasize;
		public int CallbackId => 337;
		internal static GameRichPresenceJoinRequested_t Fill( IntPtr p ) => ((GameRichPresenceJoinRequested_t)Marshal.PtrToStructure( p, typeof(GameRichPresenceJoinRequested_t) ) );
		
		static Action<GameRichPresenceJoinRequested_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameRichPresenceJoinRequested_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameRichPresenceJoinRequested_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 337, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 337, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GameConnectedClanChatMsg_t : ICallbackData
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat CSteamID
		internal ulong SteamIDUser; // m_steamIDUser CSteamID
		internal int MessageID; // m_iMessageID int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameConnectedClanChatMsg_t) );
		public int DataSize => _datasize;
		public int CallbackId => 338;
		internal static GameConnectedClanChatMsg_t Fill( IntPtr p ) => ((GameConnectedClanChatMsg_t)Marshal.PtrToStructure( p, typeof(GameConnectedClanChatMsg_t) ) );
		
		static Action<GameConnectedClanChatMsg_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameConnectedClanChatMsg_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameConnectedClanChatMsg_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 338, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 338, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GameConnectedChatJoin_t : ICallbackData
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat CSteamID
		internal ulong SteamIDUser; // m_steamIDUser CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameConnectedChatJoin_t) );
		public int DataSize => _datasize;
		public int CallbackId => 339;
		internal static GameConnectedChatJoin_t Fill( IntPtr p ) => ((GameConnectedChatJoin_t)Marshal.PtrToStructure( p, typeof(GameConnectedChatJoin_t) ) );
		
		static Action<GameConnectedChatJoin_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameConnectedChatJoin_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameConnectedChatJoin_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 339, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 339, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GameConnectedChatLeave_t : ICallbackData
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat CSteamID
		internal ulong SteamIDUser; // m_steamIDUser CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool Kicked; // m_bKicked bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool Dropped; // m_bDropped bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameConnectedChatLeave_t) );
		public int DataSize => _datasize;
		public int CallbackId => 340;
		internal static GameConnectedChatLeave_t Fill( IntPtr p ) => ((GameConnectedChatLeave_t)Marshal.PtrToStructure( p, typeof(GameConnectedChatLeave_t) ) );
		
		static Action<GameConnectedChatLeave_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameConnectedChatLeave_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameConnectedChatLeave_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 340, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 340, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct DownloadClanActivityCountsResult_t : ICallbackData
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Success; // m_bSuccess bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(DownloadClanActivityCountsResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 341;
		internal static DownloadClanActivityCountsResult_t Fill( IntPtr p ) => ((DownloadClanActivityCountsResult_t)Marshal.PtrToStructure( p, typeof(DownloadClanActivityCountsResult_t) ) );
		
		static Action<DownloadClanActivityCountsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<DownloadClanActivityCountsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<DownloadClanActivityCountsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 341, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 341, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct JoinClanChatRoomCompletionResult_t : ICallbackData
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat CSteamID
		internal RoomEnter ChatRoomEnterResponse; // m_eChatRoomEnterResponse EChatRoomEnterResponse
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(JoinClanChatRoomCompletionResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 342;
		internal static JoinClanChatRoomCompletionResult_t Fill( IntPtr p ) => ((JoinClanChatRoomCompletionResult_t)Marshal.PtrToStructure( p, typeof(JoinClanChatRoomCompletionResult_t) ) );
		
		static Action<JoinClanChatRoomCompletionResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<JoinClanChatRoomCompletionResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<JoinClanChatRoomCompletionResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 342, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 342, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GameConnectedFriendChatMsg_t : ICallbackData
	{
		internal ulong SteamIDUser; // m_steamIDUser CSteamID
		internal int MessageID; // m_iMessageID int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameConnectedFriendChatMsg_t) );
		public int DataSize => _datasize;
		public int CallbackId => 343;
		internal static GameConnectedFriendChatMsg_t Fill( IntPtr p ) => ((GameConnectedFriendChatMsg_t)Marshal.PtrToStructure( p, typeof(GameConnectedFriendChatMsg_t) ) );
		
		static Action<GameConnectedFriendChatMsg_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameConnectedFriendChatMsg_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameConnectedFriendChatMsg_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 343, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 343, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct FriendsGetFollowerCount_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal ulong SteamID; // m_steamID CSteamID
		internal int Count; // m_nCount int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendsGetFollowerCount_t) );
		public int DataSize => _datasize;
		public int CallbackId => 344;
		internal static FriendsGetFollowerCount_t Fill( IntPtr p ) => ((FriendsGetFollowerCount_t)Marshal.PtrToStructure( p, typeof(FriendsGetFollowerCount_t) ) );
		
		static Action<FriendsGetFollowerCount_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FriendsGetFollowerCount_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FriendsGetFollowerCount_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 344, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 344, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct FriendsIsFollowing_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal ulong SteamID; // m_steamID CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool IsFollowing; // m_bIsFollowing bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendsIsFollowing_t) );
		public int DataSize => _datasize;
		public int CallbackId => 345;
		internal static FriendsIsFollowing_t Fill( IntPtr p ) => ((FriendsIsFollowing_t)Marshal.PtrToStructure( p, typeof(FriendsIsFollowing_t) ) );
		
		static Action<FriendsIsFollowing_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FriendsIsFollowing_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FriendsIsFollowing_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 345, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 345, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct FriendsEnumerateFollowingList_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal ulong[] GSteamID; // m_rgSteamID CSteamID [50]
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendsEnumerateFollowingList_t) );
		public int DataSize => _datasize;
		public int CallbackId => 346;
		internal static FriendsEnumerateFollowingList_t Fill( IntPtr p ) => ((FriendsEnumerateFollowingList_t)Marshal.PtrToStructure( p, typeof(FriendsEnumerateFollowingList_t) ) );
		
		static Action<FriendsEnumerateFollowingList_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FriendsEnumerateFollowingList_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FriendsEnumerateFollowingList_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 346, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 346, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SetPersonaNameResponse_t : ICallbackData
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Success; // m_bSuccess bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool LocalSuccess; // m_bLocalSuccess bool
		internal Result Result; // m_result EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SetPersonaNameResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => 347;
		internal static SetPersonaNameResponse_t Fill( IntPtr p ) => ((SetPersonaNameResponse_t)Marshal.PtrToStructure( p, typeof(SetPersonaNameResponse_t) ) );
		
		static Action<SetPersonaNameResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SetPersonaNameResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SetPersonaNameResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 347, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 347, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct UnreadChatMessagesChanged_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UnreadChatMessagesChanged_t) );
		public int DataSize => _datasize;
		public int CallbackId => 348;
		internal static UnreadChatMessagesChanged_t Fill( IntPtr p ) => ((UnreadChatMessagesChanged_t)Marshal.PtrToStructure( p, typeof(UnreadChatMessagesChanged_t) ) );
		
		static Action<UnreadChatMessagesChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UnreadChatMessagesChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UnreadChatMessagesChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 348, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 348, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct IPCountry_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(IPCountry_t) );
		public int DataSize => _datasize;
		public int CallbackId => 701;
		internal static IPCountry_t Fill( IntPtr p ) => ((IPCountry_t)Marshal.PtrToStructure( p, typeof(IPCountry_t) ) );
		
		static Action<IPCountry_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<IPCountry_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<IPCountry_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 701, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 701, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LowBatteryPower_t : ICallbackData
	{
		internal byte MinutesBatteryLeft; // m_nMinutesBatteryLeft uint8
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LowBatteryPower_t) );
		public int DataSize => _datasize;
		public int CallbackId => 702;
		internal static LowBatteryPower_t Fill( IntPtr p ) => ((LowBatteryPower_t)Marshal.PtrToStructure( p, typeof(LowBatteryPower_t) ) );
		
		static Action<LowBatteryPower_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LowBatteryPower_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LowBatteryPower_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 702, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 702, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamAPICallCompleted_t : ICallbackData
	{
		internal ulong AsyncCall; // m_hAsyncCall SteamAPICall_t
		internal int Callback; // m_iCallback int
		internal uint ParamCount; // m_cubParam uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamAPICallCompleted_t) );
		public int DataSize => _datasize;
		public int CallbackId => 703;
		internal static SteamAPICallCompleted_t Fill( IntPtr p ) => ((SteamAPICallCompleted_t)Marshal.PtrToStructure( p, typeof(SteamAPICallCompleted_t) ) );
		
		static Action<SteamAPICallCompleted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamAPICallCompleted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamAPICallCompleted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 703, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 703, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamShutdown_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamShutdown_t) );
		public int DataSize => _datasize;
		public int CallbackId => 704;
		internal static SteamShutdown_t Fill( IntPtr p ) => ((SteamShutdown_t)Marshal.PtrToStructure( p, typeof(SteamShutdown_t) ) );
		
		static Action<SteamShutdown_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamShutdown_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamShutdown_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 704, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 704, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct CheckFileSignature_t : ICallbackData
	{
		internal CheckFileSignature CheckFileSignature; // m_eCheckFileSignature ECheckFileSignature
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(CheckFileSignature_t) );
		public int DataSize => _datasize;
		public int CallbackId => 705;
		internal static CheckFileSignature_t Fill( IntPtr p ) => ((CheckFileSignature_t)Marshal.PtrToStructure( p, typeof(CheckFileSignature_t) ) );
		
		static Action<CheckFileSignature_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<CheckFileSignature_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<CheckFileSignature_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 705, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 705, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GamepadTextInputDismissed_t : ICallbackData
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Submitted; // m_bSubmitted bool
		internal uint SubmittedText; // m_unSubmittedText uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GamepadTextInputDismissed_t) );
		public int DataSize => _datasize;
		public int CallbackId => 714;
		internal static GamepadTextInputDismissed_t Fill( IntPtr p ) => ((GamepadTextInputDismissed_t)Marshal.PtrToStructure( p, typeof(GamepadTextInputDismissed_t) ) );
		
		static Action<GamepadTextInputDismissed_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GamepadTextInputDismissed_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GamepadTextInputDismissed_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 714, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 714, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct FavoritesListChanged_t : ICallbackData
	{
		internal uint IP; // m_nIP uint32
		internal uint QueryPort; // m_nQueryPort uint32
		internal uint ConnPort; // m_nConnPort uint32
		internal uint AppID; // m_nAppID uint32
		internal uint Flags; // m_nFlags uint32
		[MarshalAs(UnmanagedType.I1)]
		internal bool Add; // m_bAdd bool
		internal uint AccountId; // m_unAccountId AccountID_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FavoritesListChanged_t) );
		public int DataSize => _datasize;
		public int CallbackId => 502;
		internal static FavoritesListChanged_t Fill( IntPtr p ) => ((FavoritesListChanged_t)Marshal.PtrToStructure( p, typeof(FavoritesListChanged_t) ) );
		
		static Action<FavoritesListChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FavoritesListChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FavoritesListChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 502, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 502, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyInvite_t : ICallbackData
	{
		internal ulong SteamIDUser; // m_ulSteamIDUser uint64
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong GameID; // m_ulGameID uint64
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyInvite_t) );
		public int DataSize => _datasize;
		public int CallbackId => 503;
		internal static LobbyInvite_t Fill( IntPtr p ) => ((LobbyInvite_t)Marshal.PtrToStructure( p, typeof(LobbyInvite_t) ) );
		
		static Action<LobbyInvite_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyInvite_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyInvite_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 503, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 503, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyEnter_t : ICallbackData
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal uint GfChatPermissions; // m_rgfChatPermissions uint32
		[MarshalAs(UnmanagedType.I1)]
		internal bool Locked; // m_bLocked bool
		internal uint EChatRoomEnterResponse; // m_EChatRoomEnterResponse uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyEnter_t) );
		public int DataSize => _datasize;
		public int CallbackId => 504;
		internal static LobbyEnter_t Fill( IntPtr p ) => ((LobbyEnter_t)Marshal.PtrToStructure( p, typeof(LobbyEnter_t) ) );
		
		static Action<LobbyEnter_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyEnter_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyEnter_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 504, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 504, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyDataUpdate_t : ICallbackData
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDMember; // m_ulSteamIDMember uint64
		internal byte Success; // m_bSuccess uint8
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyDataUpdate_t) );
		public int DataSize => _datasize;
		public int CallbackId => 505;
		internal static LobbyDataUpdate_t Fill( IntPtr p ) => ((LobbyDataUpdate_t)Marshal.PtrToStructure( p, typeof(LobbyDataUpdate_t) ) );
		
		static Action<LobbyDataUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyDataUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyDataUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 505, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 505, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyChatUpdate_t : ICallbackData
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDUserChanged; // m_ulSteamIDUserChanged uint64
		internal ulong SteamIDMakingChange; // m_ulSteamIDMakingChange uint64
		internal uint GfChatMemberStateChange; // m_rgfChatMemberStateChange uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyChatUpdate_t) );
		public int DataSize => _datasize;
		public int CallbackId => 506;
		internal static LobbyChatUpdate_t Fill( IntPtr p ) => ((LobbyChatUpdate_t)Marshal.PtrToStructure( p, typeof(LobbyChatUpdate_t) ) );
		
		static Action<LobbyChatUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyChatUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyChatUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 506, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 506, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyChatMsg_t : ICallbackData
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDUser; // m_ulSteamIDUser uint64
		internal byte ChatEntryType; // m_eChatEntryType uint8
		internal uint ChatID; // m_iChatID uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyChatMsg_t) );
		public int DataSize => _datasize;
		public int CallbackId => 507;
		internal static LobbyChatMsg_t Fill( IntPtr p ) => ((LobbyChatMsg_t)Marshal.PtrToStructure( p, typeof(LobbyChatMsg_t) ) );
		
		static Action<LobbyChatMsg_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyChatMsg_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyChatMsg_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 507, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 507, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyGameCreated_t : ICallbackData
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDGameServer; // m_ulSteamIDGameServer uint64
		internal uint IP; // m_unIP uint32
		internal ushort Port; // m_usPort uint16
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyGameCreated_t) );
		public int DataSize => _datasize;
		public int CallbackId => 509;
		internal static LobbyGameCreated_t Fill( IntPtr p ) => ((LobbyGameCreated_t)Marshal.PtrToStructure( p, typeof(LobbyGameCreated_t) ) );
		
		static Action<LobbyGameCreated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyGameCreated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyGameCreated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 509, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 509, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyMatchList_t : ICallbackData
	{
		internal uint LobbiesMatching; // m_nLobbiesMatching uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyMatchList_t) );
		public int DataSize => _datasize;
		public int CallbackId => 510;
		internal static LobbyMatchList_t Fill( IntPtr p ) => ((LobbyMatchList_t)Marshal.PtrToStructure( p, typeof(LobbyMatchList_t) ) );
		
		static Action<LobbyMatchList_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyMatchList_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyMatchList_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 510, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 510, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyKicked_t : ICallbackData
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDAdmin; // m_ulSteamIDAdmin uint64
		internal byte KickedDueToDisconnect; // m_bKickedDueToDisconnect uint8
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyKicked_t) );
		public int DataSize => _datasize;
		public int CallbackId => 512;
		internal static LobbyKicked_t Fill( IntPtr p ) => ((LobbyKicked_t)Marshal.PtrToStructure( p, typeof(LobbyKicked_t) ) );
		
		static Action<LobbyKicked_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyKicked_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyKicked_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 512, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 512, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyCreated_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyCreated_t) );
		public int DataSize => _datasize;
		public int CallbackId => 513;
		internal static LobbyCreated_t Fill( IntPtr p ) => ((LobbyCreated_t)Marshal.PtrToStructure( p, typeof(LobbyCreated_t) ) );
		
		static Action<LobbyCreated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyCreated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyCreated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 513, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 513, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct PSNGameBootInviteResult_t : ICallbackData
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool GameBootInviteExists; // m_bGameBootInviteExists bool
		internal ulong SteamIDLobby; // m_steamIDLobby CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(PSNGameBootInviteResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 515;
		internal static PSNGameBootInviteResult_t Fill( IntPtr p ) => ((PSNGameBootInviteResult_t)Marshal.PtrToStructure( p, typeof(PSNGameBootInviteResult_t) ) );
		
		static Action<PSNGameBootInviteResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<PSNGameBootInviteResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<PSNGameBootInviteResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 515, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 515, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct FavoritesListAccountsUpdated_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FavoritesListAccountsUpdated_t) );
		public int DataSize => _datasize;
		public int CallbackId => 516;
		internal static FavoritesListAccountsUpdated_t Fill( IntPtr p ) => ((FavoritesListAccountsUpdated_t)Marshal.PtrToStructure( p, typeof(FavoritesListAccountsUpdated_t) ) );
		
		static Action<FavoritesListAccountsUpdated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FavoritesListAccountsUpdated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FavoritesListAccountsUpdated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 516, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 516, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct SearchForGameProgressCallback_t : ICallbackData
	{
		internal ulong LSearchID; // m_ullSearchID uint64
		internal Result Result; // m_eResult EResult
		internal ulong LobbyID; // m_lobbyID CSteamID
		internal ulong SteamIDEndedSearch; // m_steamIDEndedSearch CSteamID
		internal int SecondsRemainingEstimate; // m_nSecondsRemainingEstimate int32
		internal int CPlayersSearching; // m_cPlayersSearching int32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SearchForGameProgressCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => 5201;
		internal static SearchForGameProgressCallback_t Fill( IntPtr p ) => ((SearchForGameProgressCallback_t)Marshal.PtrToStructure( p, typeof(SearchForGameProgressCallback_t) ) );
		
		static Action<SearchForGameProgressCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SearchForGameProgressCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SearchForGameProgressCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 5201, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 5201, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct SearchForGameResultCallback_t : ICallbackData
	{
		internal ulong LSearchID; // m_ullSearchID uint64
		internal Result Result; // m_eResult EResult
		internal int CountPlayersInGame; // m_nCountPlayersInGame int32
		internal int CountAcceptedGame; // m_nCountAcceptedGame int32
		internal ulong SteamIDHost; // m_steamIDHost CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool FinalCallback; // m_bFinalCallback bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SearchForGameResultCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => 5202;
		internal static SearchForGameResultCallback_t Fill( IntPtr p ) => ((SearchForGameResultCallback_t)Marshal.PtrToStructure( p, typeof(SearchForGameResultCallback_t) ) );
		
		static Action<SearchForGameResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SearchForGameResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SearchForGameResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 5202, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 5202, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RequestPlayersForGameProgressCallback_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal ulong LSearchID; // m_ullSearchID uint64
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RequestPlayersForGameProgressCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => 5211;
		internal static RequestPlayersForGameProgressCallback_t Fill( IntPtr p ) => ((RequestPlayersForGameProgressCallback_t)Marshal.PtrToStructure( p, typeof(RequestPlayersForGameProgressCallback_t) ) );
		
		static Action<RequestPlayersForGameProgressCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RequestPlayersForGameProgressCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RequestPlayersForGameProgressCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 5211, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 5211, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct RequestPlayersForGameResultCallback_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal ulong LSearchID; // m_ullSearchID uint64
		internal ulong SteamIDPlayerFound; // m_SteamIDPlayerFound CSteamID
		internal ulong SteamIDLobby; // m_SteamIDLobby CSteamID
		internal RequestPlayersForGameResultCallback_t.PlayerAcceptState_t PlayerAcceptState; // m_ePlayerAcceptState RequestPlayersForGameResultCallback_t::PlayerAcceptState_t
		internal int PlayerIndex; // m_nPlayerIndex int32
		internal int TotalPlayersFound; // m_nTotalPlayersFound int32
		internal int TotalPlayersAcceptedGame; // m_nTotalPlayersAcceptedGame int32
		internal int SuggestedTeamIndex; // m_nSuggestedTeamIndex int32
		internal ulong LUniqueGameID; // m_ullUniqueGameID uint64
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RequestPlayersForGameResultCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => 5212;
		internal static RequestPlayersForGameResultCallback_t Fill( IntPtr p ) => ((RequestPlayersForGameResultCallback_t)Marshal.PtrToStructure( p, typeof(RequestPlayersForGameResultCallback_t) ) );
		
		static Action<RequestPlayersForGameResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RequestPlayersForGameResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RequestPlayersForGameResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 5212, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 5212, false );
				actionClient = action;
			}
		}
		#endregion
		internal enum PlayerAcceptState_t : int
		{
			Unknown = 0,
			PlayerAccepted = 1,
			PlayerDeclined = 2,
		}
		
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RequestPlayersForGameFinalResultCallback_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal ulong LSearchID; // m_ullSearchID uint64
		internal ulong LUniqueGameID; // m_ullUniqueGameID uint64
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RequestPlayersForGameFinalResultCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => 5213;
		internal static RequestPlayersForGameFinalResultCallback_t Fill( IntPtr p ) => ((RequestPlayersForGameFinalResultCallback_t)Marshal.PtrToStructure( p, typeof(RequestPlayersForGameFinalResultCallback_t) ) );
		
		static Action<RequestPlayersForGameFinalResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RequestPlayersForGameFinalResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RequestPlayersForGameFinalResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 5213, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 5213, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct SubmitPlayerResultResultCallback_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal ulong UllUniqueGameID; // ullUniqueGameID uint64
		internal ulong SteamIDPlayer; // steamIDPlayer CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SubmitPlayerResultResultCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => 5214;
		internal static SubmitPlayerResultResultCallback_t Fill( IntPtr p ) => ((SubmitPlayerResultResultCallback_t)Marshal.PtrToStructure( p, typeof(SubmitPlayerResultResultCallback_t) ) );
		
		static Action<SubmitPlayerResultResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SubmitPlayerResultResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SubmitPlayerResultResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 5214, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 5214, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct EndGameResultCallback_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal ulong UllUniqueGameID; // ullUniqueGameID uint64
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(EndGameResultCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => 5215;
		internal static EndGameResultCallback_t Fill( IntPtr p ) => ((EndGameResultCallback_t)Marshal.PtrToStructure( p, typeof(EndGameResultCallback_t) ) );
		
		static Action<EndGameResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<EndGameResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<EndGameResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 5215, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 5215, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct JoinPartyCallback_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
		internal ulong SteamIDBeaconOwner; // m_SteamIDBeaconOwner CSteamID
		internal string ConnectStringUTF8() => System.Text.Encoding.UTF8.GetString( ConnectString, 0, System.Array.IndexOf<byte>( ConnectString, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // byte[] m_rgchConnectString
		internal byte[] ConnectString; // m_rgchConnectString char [256]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(JoinPartyCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => 5301;
		internal static JoinPartyCallback_t Fill( IntPtr p ) => ((JoinPartyCallback_t)Marshal.PtrToStructure( p, typeof(JoinPartyCallback_t) ) );
		
		static Action<JoinPartyCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<JoinPartyCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<JoinPartyCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 5301, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 5301, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct CreateBeaconCallback_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(CreateBeaconCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => 5302;
		internal static CreateBeaconCallback_t Fill( IntPtr p ) => ((CreateBeaconCallback_t)Marshal.PtrToStructure( p, typeof(CreateBeaconCallback_t) ) );
		
		static Action<CreateBeaconCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<CreateBeaconCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<CreateBeaconCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 5302, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 5302, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct ReservationNotificationCallback_t : ICallbackData
	{
		internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
		internal ulong SteamIDJoiner; // m_steamIDJoiner CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ReservationNotificationCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => 5303;
		internal static ReservationNotificationCallback_t Fill( IntPtr p ) => ((ReservationNotificationCallback_t)Marshal.PtrToStructure( p, typeof(ReservationNotificationCallback_t) ) );
		
		static Action<ReservationNotificationCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ReservationNotificationCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ReservationNotificationCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 5303, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 5303, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct ChangeNumOpenSlotsCallback_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ChangeNumOpenSlotsCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => 5304;
		internal static ChangeNumOpenSlotsCallback_t Fill( IntPtr p ) => ((ChangeNumOpenSlotsCallback_t)Marshal.PtrToStructure( p, typeof(ChangeNumOpenSlotsCallback_t) ) );
		
		static Action<ChangeNumOpenSlotsCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ChangeNumOpenSlotsCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ChangeNumOpenSlotsCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 5304, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 5304, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct AvailableBeaconLocationsUpdated_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(AvailableBeaconLocationsUpdated_t) );
		public int DataSize => _datasize;
		public int CallbackId => 5305;
		internal static AvailableBeaconLocationsUpdated_t Fill( IntPtr p ) => ((AvailableBeaconLocationsUpdated_t)Marshal.PtrToStructure( p, typeof(AvailableBeaconLocationsUpdated_t) ) );
		
		static Action<AvailableBeaconLocationsUpdated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<AvailableBeaconLocationsUpdated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<AvailableBeaconLocationsUpdated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 5305, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 5305, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct ActiveBeaconsUpdated_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ActiveBeaconsUpdated_t) );
		public int DataSize => _datasize;
		public int CallbackId => 5306;
		internal static ActiveBeaconsUpdated_t Fill( IntPtr p ) => ((ActiveBeaconsUpdated_t)Marshal.PtrToStructure( p, typeof(ActiveBeaconsUpdated_t) ) );
		
		static Action<ActiveBeaconsUpdated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ActiveBeaconsUpdated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ActiveBeaconsUpdated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 5306, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 5306, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageAppSyncedClient_t : ICallbackData
	{
		internal AppId AppID; // m_nAppID AppId_t
		internal Result Result; // m_eResult EResult
		internal int NumDownloads; // m_unNumDownloads int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageAppSyncedClient_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1301;
		internal static RemoteStorageAppSyncedClient_t Fill( IntPtr p ) => ((RemoteStorageAppSyncedClient_t)Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncedClient_t) ) );
		
		static Action<RemoteStorageAppSyncedClient_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageAppSyncedClient_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageAppSyncedClient_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1301, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1301, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageAppSyncedServer_t : ICallbackData
	{
		internal AppId AppID; // m_nAppID AppId_t
		internal Result Result; // m_eResult EResult
		internal int NumUploads; // m_unNumUploads int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageAppSyncedServer_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1302;
		internal static RemoteStorageAppSyncedServer_t Fill( IntPtr p ) => ((RemoteStorageAppSyncedServer_t)Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncedServer_t) ) );
		
		static Action<RemoteStorageAppSyncedServer_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageAppSyncedServer_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageAppSyncedServer_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1302, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1302, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageAppSyncProgress_t : ICallbackData
	{
		internal string CurrentFileUTF8() => System.Text.Encoding.UTF8.GetString( CurrentFile, 0, System.Array.IndexOf<byte>( CurrentFile, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)] // byte[] m_rgchCurrentFile
		internal byte[] CurrentFile; // m_rgchCurrentFile char [260]
		internal AppId AppID; // m_nAppID AppId_t
		internal uint BytesTransferredThisChunk; // m_uBytesTransferredThisChunk uint32
		internal double DAppPercentComplete; // m_dAppPercentComplete double
		[MarshalAs(UnmanagedType.I1)]
		internal bool Uploading; // m_bUploading bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageAppSyncProgress_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1303;
		internal static RemoteStorageAppSyncProgress_t Fill( IntPtr p ) => ((RemoteStorageAppSyncProgress_t)Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncProgress_t) ) );
		
		static Action<RemoteStorageAppSyncProgress_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageAppSyncProgress_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageAppSyncProgress_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1303, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1303, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageAppSyncStatusCheck_t : ICallbackData
	{
		internal AppId AppID; // m_nAppID AppId_t
		internal Result Result; // m_eResult EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageAppSyncStatusCheck_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1305;
		internal static RemoteStorageAppSyncStatusCheck_t Fill( IntPtr p ) => ((RemoteStorageAppSyncStatusCheck_t)Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncStatusCheck_t) ) );
		
		static Action<RemoteStorageAppSyncStatusCheck_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageAppSyncStatusCheck_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageAppSyncStatusCheck_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1305, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1305, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageFileShareResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal ulong File; // m_hFile UGCHandle_t
		internal string FilenameUTF8() => System.Text.Encoding.UTF8.GetString( Filename, 0, System.Array.IndexOf<byte>( Filename, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)] // byte[] m_rgchFilename
		internal byte[] Filename; // m_rgchFilename char [260]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageFileShareResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1307;
		internal static RemoteStorageFileShareResult_t Fill( IntPtr p ) => ((RemoteStorageFileShareResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageFileShareResult_t) ) );
		
		static Action<RemoteStorageFileShareResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageFileShareResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageFileShareResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1307, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1307, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStoragePublishFileResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStoragePublishFileResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1309;
		internal static RemoteStoragePublishFileResult_t Fill( IntPtr p ) => ((RemoteStoragePublishFileResult_t)Marshal.PtrToStructure( p, typeof(RemoteStoragePublishFileResult_t) ) );
		
		static Action<RemoteStoragePublishFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStoragePublishFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1309, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1309, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageDeletePublishedFileResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageDeletePublishedFileResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1311;
		internal static RemoteStorageDeletePublishedFileResult_t Fill( IntPtr p ) => ((RemoteStorageDeletePublishedFileResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageDeletePublishedFileResult_t) ) );
		
		static Action<RemoteStorageDeletePublishedFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageDeletePublishedFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageDeletePublishedFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1311, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1311, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageEnumerateUserPublishedFilesResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageEnumerateUserPublishedFilesResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1312;
		internal static RemoteStorageEnumerateUserPublishedFilesResult_t Fill( IntPtr p ) => ((RemoteStorageEnumerateUserPublishedFilesResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserPublishedFilesResult_t) ) );
		
		static Action<RemoteStorageEnumerateUserPublishedFilesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageEnumerateUserPublishedFilesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumerateUserPublishedFilesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1312, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1312, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageSubscribePublishedFileResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageSubscribePublishedFileResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1313;
		internal static RemoteStorageSubscribePublishedFileResult_t Fill( IntPtr p ) => ((RemoteStorageSubscribePublishedFileResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageSubscribePublishedFileResult_t) ) );
		
		static Action<RemoteStorageSubscribePublishedFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageSubscribePublishedFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageSubscribePublishedFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1313, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1313, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageEnumerateUserSubscribedFilesResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
		internal uint[] GRTimeSubscribed; // m_rgRTimeSubscribed uint32 [50]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1314;
		internal static RemoteStorageEnumerateUserSubscribedFilesResult_t Fill( IntPtr p ) => ((RemoteStorageEnumerateUserSubscribedFilesResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t) ) );
		
		static Action<RemoteStorageEnumerateUserSubscribedFilesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageEnumerateUserSubscribedFilesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumerateUserSubscribedFilesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1314, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1314, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageUnsubscribePublishedFileResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageUnsubscribePublishedFileResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1315;
		internal static RemoteStorageUnsubscribePublishedFileResult_t Fill( IntPtr p ) => ((RemoteStorageUnsubscribePublishedFileResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageUnsubscribePublishedFileResult_t) ) );
		
		static Action<RemoteStorageUnsubscribePublishedFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageUnsubscribePublishedFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageUnsubscribePublishedFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1315, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1315, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageUpdatePublishedFileResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageUpdatePublishedFileResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1316;
		internal static RemoteStorageUpdatePublishedFileResult_t Fill( IntPtr p ) => ((RemoteStorageUpdatePublishedFileResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageUpdatePublishedFileResult_t) ) );
		
		static Action<RemoteStorageUpdatePublishedFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageUpdatePublishedFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageUpdatePublishedFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1316, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1316, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageDownloadUGCResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal ulong File; // m_hFile UGCHandle_t
		internal AppId AppID; // m_nAppID AppId_t
		internal int SizeInBytes; // m_nSizeInBytes int32
		internal string PchFileNameUTF8() => System.Text.Encoding.UTF8.GetString( PchFileName, 0, System.Array.IndexOf<byte>( PchFileName, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)] // byte[] m_pchFileName
		internal byte[] PchFileName; // m_pchFileName char [260]
		internal ulong SteamIDOwner; // m_ulSteamIDOwner uint64
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageDownloadUGCResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1317;
		internal static RemoteStorageDownloadUGCResult_t Fill( IntPtr p ) => ((RemoteStorageDownloadUGCResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageDownloadUGCResult_t) ) );
		
		static Action<RemoteStorageDownloadUGCResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageDownloadUGCResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageDownloadUGCResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1317, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1317, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageGetPublishedFileDetailsResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId CreatorAppID; // m_nCreatorAppID AppId_t
		internal AppId ConsumerAppID; // m_nConsumerAppID AppId_t
		internal string TitleUTF8() => System.Text.Encoding.UTF8.GetString( Title, 0, System.Array.IndexOf<byte>( Title, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 129)] // byte[] m_rgchTitle
		internal byte[] Title; // m_rgchTitle char [129]
		internal string DescriptionUTF8() => System.Text.Encoding.UTF8.GetString( Description, 0, System.Array.IndexOf<byte>( Description, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8000)] // byte[] m_rgchDescription
		internal byte[] Description; // m_rgchDescription char [8000]
		internal ulong File; // m_hFile UGCHandle_t
		internal ulong PreviewFile; // m_hPreviewFile UGCHandle_t
		internal ulong SteamIDOwner; // m_ulSteamIDOwner uint64
		internal uint TimeCreated; // m_rtimeCreated uint32
		internal uint TimeUpdated; // m_rtimeUpdated uint32
		internal RemoteStoragePublishedFileVisibility Visibility; // m_eVisibility ERemoteStoragePublishedFileVisibility
		[MarshalAs(UnmanagedType.I1)]
		internal bool Banned; // m_bBanned bool
		internal string TagsUTF8() => System.Text.Encoding.UTF8.GetString( Tags, 0, System.Array.IndexOf<byte>( Tags, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1025)] // byte[] m_rgchTags
		internal byte[] Tags; // m_rgchTags char [1025]
		[MarshalAs(UnmanagedType.I1)]
		internal bool TagsTruncated; // m_bTagsTruncated bool
		internal string PchFileNameUTF8() => System.Text.Encoding.UTF8.GetString( PchFileName, 0, System.Array.IndexOf<byte>( PchFileName, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)] // byte[] m_pchFileName
		internal byte[] PchFileName; // m_pchFileName char [260]
		internal int FileSize; // m_nFileSize int32
		internal int PreviewFileSize; // m_nPreviewFileSize int32
		internal string URLUTF8() => System.Text.Encoding.UTF8.GetString( URL, 0, System.Array.IndexOf<byte>( URL, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // byte[] m_rgchURL
		internal byte[] URL; // m_rgchURL char [256]
		internal WorkshopFileType FileType; // m_eFileType EWorkshopFileType
		[MarshalAs(UnmanagedType.I1)]
		internal bool AcceptedForUse; // m_bAcceptedForUse bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageGetPublishedFileDetailsResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1318;
		internal static RemoteStorageGetPublishedFileDetailsResult_t Fill( IntPtr p ) => ((RemoteStorageGetPublishedFileDetailsResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageGetPublishedFileDetailsResult_t) ) );
		
		static Action<RemoteStorageGetPublishedFileDetailsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageGetPublishedFileDetailsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageGetPublishedFileDetailsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1318, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1318, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageEnumerateWorkshopFilesResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.R4)]
		internal float[] GScore; // m_rgScore float [50]
		internal AppId AppId; // m_nAppId AppId_t
		internal uint StartIndex; // m_unStartIndex uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageEnumerateWorkshopFilesResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1319;
		internal static RemoteStorageEnumerateWorkshopFilesResult_t Fill( IntPtr p ) => ((RemoteStorageEnumerateWorkshopFilesResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateWorkshopFilesResult_t) ) );
		
		static Action<RemoteStorageEnumerateWorkshopFilesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageEnumerateWorkshopFilesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumerateWorkshopFilesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1319, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1319, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageGetPublishedItemVoteDetailsResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal PublishedFileId PublishedFileId; // m_unPublishedFileId PublishedFileId_t
		internal int VotesFor; // m_nVotesFor int32
		internal int VotesAgainst; // m_nVotesAgainst int32
		internal int Reports; // m_nReports int32
		internal float FScore; // m_fScore float
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageGetPublishedItemVoteDetailsResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1320;
		internal static RemoteStorageGetPublishedItemVoteDetailsResult_t Fill( IntPtr p ) => ((RemoteStorageGetPublishedItemVoteDetailsResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageGetPublishedItemVoteDetailsResult_t) ) );
		
		static Action<RemoteStorageGetPublishedItemVoteDetailsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageGetPublishedItemVoteDetailsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageGetPublishedItemVoteDetailsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1320, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1320, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStoragePublishedFileSubscribed_t : ICallbackData
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStoragePublishedFileSubscribed_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1321;
		internal static RemoteStoragePublishedFileSubscribed_t Fill( IntPtr p ) => ((RemoteStoragePublishedFileSubscribed_t)Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileSubscribed_t) ) );
		
		static Action<RemoteStoragePublishedFileSubscribed_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStoragePublishedFileSubscribed_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishedFileSubscribed_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1321, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1321, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStoragePublishedFileUnsubscribed_t : ICallbackData
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStoragePublishedFileUnsubscribed_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1322;
		internal static RemoteStoragePublishedFileUnsubscribed_t Fill( IntPtr p ) => ((RemoteStoragePublishedFileUnsubscribed_t)Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileUnsubscribed_t) ) );
		
		static Action<RemoteStoragePublishedFileUnsubscribed_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStoragePublishedFileUnsubscribed_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishedFileUnsubscribed_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1322, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1322, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStoragePublishedFileDeleted_t : ICallbackData
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStoragePublishedFileDeleted_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1323;
		internal static RemoteStoragePublishedFileDeleted_t Fill( IntPtr p ) => ((RemoteStoragePublishedFileDeleted_t)Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileDeleted_t) ) );
		
		static Action<RemoteStoragePublishedFileDeleted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStoragePublishedFileDeleted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishedFileDeleted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1323, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1323, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageUpdateUserPublishedItemVoteResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageUpdateUserPublishedItemVoteResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1324;
		internal static RemoteStorageUpdateUserPublishedItemVoteResult_t Fill( IntPtr p ) => ((RemoteStorageUpdateUserPublishedItemVoteResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageUpdateUserPublishedItemVoteResult_t) ) );
		
		static Action<RemoteStorageUpdateUserPublishedItemVoteResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageUpdateUserPublishedItemVoteResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageUpdateUserPublishedItemVoteResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1324, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1324, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageUserVoteDetails_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal WorkshopVote Vote; // m_eVote EWorkshopVote
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageUserVoteDetails_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1325;
		internal static RemoteStorageUserVoteDetails_t Fill( IntPtr p ) => ((RemoteStorageUserVoteDetails_t)Marshal.PtrToStructure( p, typeof(RemoteStorageUserVoteDetails_t) ) );
		
		static Action<RemoteStorageUserVoteDetails_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageUserVoteDetails_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageUserVoteDetails_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1325, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1325, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1326;
		internal static RemoteStorageEnumerateUserSharedWorkshopFilesResult_t Fill( IntPtr p ) => ((RemoteStorageEnumerateUserSharedWorkshopFilesResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) ) );
		
		static Action<RemoteStorageEnumerateUserSharedWorkshopFilesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageEnumerateUserSharedWorkshopFilesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumerateUserSharedWorkshopFilesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1326, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1326, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageSetUserPublishedFileActionResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal WorkshopFileAction Action; // m_eAction EWorkshopFileAction
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageSetUserPublishedFileActionResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1327;
		internal static RemoteStorageSetUserPublishedFileActionResult_t Fill( IntPtr p ) => ((RemoteStorageSetUserPublishedFileActionResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageSetUserPublishedFileActionResult_t) ) );
		
		static Action<RemoteStorageSetUserPublishedFileActionResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageSetUserPublishedFileActionResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageSetUserPublishedFileActionResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1327, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1327, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageEnumeratePublishedFilesByUserActionResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal WorkshopFileAction Action; // m_eAction EWorkshopFileAction
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
		internal uint[] GRTimeUpdated; // m_rgRTimeUpdated uint32 [50]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageEnumeratePublishedFilesByUserActionResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1328;
		internal static RemoteStorageEnumeratePublishedFilesByUserActionResult_t Fill( IntPtr p ) => ((RemoteStorageEnumeratePublishedFilesByUserActionResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageEnumeratePublishedFilesByUserActionResult_t) ) );
		
		static Action<RemoteStorageEnumeratePublishedFilesByUserActionResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageEnumeratePublishedFilesByUserActionResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumeratePublishedFilesByUserActionResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1328, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1328, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStoragePublishFileProgress_t : ICallbackData
	{
		internal double DPercentFile; // m_dPercentFile double
		[MarshalAs(UnmanagedType.I1)]
		internal bool Preview; // m_bPreview bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStoragePublishFileProgress_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1329;
		internal static RemoteStoragePublishFileProgress_t Fill( IntPtr p ) => ((RemoteStoragePublishFileProgress_t)Marshal.PtrToStructure( p, typeof(RemoteStoragePublishFileProgress_t) ) );
		
		static Action<RemoteStoragePublishFileProgress_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStoragePublishFileProgress_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishFileProgress_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1329, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1329, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStoragePublishedFileUpdated_t : ICallbackData
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		internal ulong Unused; // m_ulUnused uint64
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStoragePublishedFileUpdated_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1330;
		internal static RemoteStoragePublishedFileUpdated_t Fill( IntPtr p ) => ((RemoteStoragePublishedFileUpdated_t)Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileUpdated_t) ) );
		
		static Action<RemoteStoragePublishedFileUpdated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStoragePublishedFileUpdated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishedFileUpdated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1330, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1330, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageFileWriteAsyncComplete_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageFileWriteAsyncComplete_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1331;
		internal static RemoteStorageFileWriteAsyncComplete_t Fill( IntPtr p ) => ((RemoteStorageFileWriteAsyncComplete_t)Marshal.PtrToStructure( p, typeof(RemoteStorageFileWriteAsyncComplete_t) ) );
		
		static Action<RemoteStorageFileWriteAsyncComplete_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageFileWriteAsyncComplete_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageFileWriteAsyncComplete_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1331, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1331, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageFileReadAsyncComplete_t : ICallbackData
	{
		internal ulong FileReadAsync; // m_hFileReadAsync SteamAPICall_t
		internal Result Result; // m_eResult EResult
		internal uint Offset; // m_nOffset uint32
		internal uint Read; // m_cubRead uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageFileReadAsyncComplete_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1332;
		internal static RemoteStorageFileReadAsyncComplete_t Fill( IntPtr p ) => ((RemoteStorageFileReadAsyncComplete_t)Marshal.PtrToStructure( p, typeof(RemoteStorageFileReadAsyncComplete_t) ) );
		
		static Action<RemoteStorageFileReadAsyncComplete_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageFileReadAsyncComplete_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageFileReadAsyncComplete_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1332, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1332, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct UserStatsReceived_t : ICallbackData
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult EResult
		internal ulong SteamIDUser; // m_steamIDUser CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserStatsReceived_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1101;
		internal static UserStatsReceived_t Fill( IntPtr p ) => ((UserStatsReceived_t)Marshal.PtrToStructure( p, typeof(UserStatsReceived_t) ) );
		
		static Action<UserStatsReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UserStatsReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UserStatsReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1101, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1101, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct UserStatsStored_t : ICallbackData
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserStatsStored_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1102;
		internal static UserStatsStored_t Fill( IntPtr p ) => ((UserStatsStored_t)Marshal.PtrToStructure( p, typeof(UserStatsStored_t) ) );
		
		static Action<UserStatsStored_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UserStatsStored_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UserStatsStored_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1102, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1102, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct UserAchievementStored_t : ICallbackData
	{
		internal ulong GameID; // m_nGameID uint64
		[MarshalAs(UnmanagedType.I1)]
		internal bool GroupAchievement; // m_bGroupAchievement bool
		internal string AchievementNameUTF8() => System.Text.Encoding.UTF8.GetString( AchievementName, 0, System.Array.IndexOf<byte>( AchievementName, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)] // byte[] m_rgchAchievementName
		internal byte[] AchievementName; // m_rgchAchievementName char [128]
		internal uint CurProgress; // m_nCurProgress uint32
		internal uint MaxProgress; // m_nMaxProgress uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserAchievementStored_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1103;
		internal static UserAchievementStored_t Fill( IntPtr p ) => ((UserAchievementStored_t)Marshal.PtrToStructure( p, typeof(UserAchievementStored_t) ) );
		
		static Action<UserAchievementStored_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UserAchievementStored_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UserAchievementStored_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1103, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1103, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LeaderboardFindResult_t : ICallbackData
	{
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		internal byte LeaderboardFound; // m_bLeaderboardFound uint8
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LeaderboardFindResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1104;
		internal static LeaderboardFindResult_t Fill( IntPtr p ) => ((LeaderboardFindResult_t)Marshal.PtrToStructure( p, typeof(LeaderboardFindResult_t) ) );
		
		static Action<LeaderboardFindResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LeaderboardFindResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LeaderboardFindResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1104, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1104, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LeaderboardScoresDownloaded_t : ICallbackData
	{
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		internal ulong SteamLeaderboardEntries; // m_hSteamLeaderboardEntries SteamLeaderboardEntries_t
		internal int CEntryCount; // m_cEntryCount int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LeaderboardScoresDownloaded_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1105;
		internal static LeaderboardScoresDownloaded_t Fill( IntPtr p ) => ((LeaderboardScoresDownloaded_t)Marshal.PtrToStructure( p, typeof(LeaderboardScoresDownloaded_t) ) );
		
		static Action<LeaderboardScoresDownloaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LeaderboardScoresDownloaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LeaderboardScoresDownloaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1105, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1105, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LeaderboardScoreUploaded_t : ICallbackData
	{
		internal byte Success; // m_bSuccess uint8
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		internal int Score; // m_nScore int32
		internal byte ScoreChanged; // m_bScoreChanged uint8
		internal int GlobalRankNew; // m_nGlobalRankNew int
		internal int GlobalRankPrevious; // m_nGlobalRankPrevious int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LeaderboardScoreUploaded_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1106;
		internal static LeaderboardScoreUploaded_t Fill( IntPtr p ) => ((LeaderboardScoreUploaded_t)Marshal.PtrToStructure( p, typeof(LeaderboardScoreUploaded_t) ) );
		
		static Action<LeaderboardScoreUploaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LeaderboardScoreUploaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LeaderboardScoreUploaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1106, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1106, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct NumberOfCurrentPlayers_t : ICallbackData
	{
		internal byte Success; // m_bSuccess uint8
		internal int CPlayers; // m_cPlayers int32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(NumberOfCurrentPlayers_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1107;
		internal static NumberOfCurrentPlayers_t Fill( IntPtr p ) => ((NumberOfCurrentPlayers_t)Marshal.PtrToStructure( p, typeof(NumberOfCurrentPlayers_t) ) );
		
		static Action<NumberOfCurrentPlayers_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<NumberOfCurrentPlayers_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<NumberOfCurrentPlayers_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1107, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1107, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct UserStatsUnloaded_t : ICallbackData
	{
		internal ulong SteamIDUser; // m_steamIDUser CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserStatsUnloaded_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1108;
		internal static UserStatsUnloaded_t Fill( IntPtr p ) => ((UserStatsUnloaded_t)Marshal.PtrToStructure( p, typeof(UserStatsUnloaded_t) ) );
		
		static Action<UserStatsUnloaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UserStatsUnloaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UserStatsUnloaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1108, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1108, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct UserAchievementIconFetched_t : ICallbackData
	{
		internal GameId GameID; // m_nGameID CGameID
		internal string AchievementNameUTF8() => System.Text.Encoding.UTF8.GetString( AchievementName, 0, System.Array.IndexOf<byte>( AchievementName, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)] // byte[] m_rgchAchievementName
		internal byte[] AchievementName; // m_rgchAchievementName char [128]
		[MarshalAs(UnmanagedType.I1)]
		internal bool Achieved; // m_bAchieved bool
		internal int IconHandle; // m_nIconHandle int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserAchievementIconFetched_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1109;
		internal static UserAchievementIconFetched_t Fill( IntPtr p ) => ((UserAchievementIconFetched_t)Marshal.PtrToStructure( p, typeof(UserAchievementIconFetched_t) ) );
		
		static Action<UserAchievementIconFetched_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UserAchievementIconFetched_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UserAchievementIconFetched_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1109, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1109, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GlobalAchievementPercentagesReady_t : ICallbackData
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GlobalAchievementPercentagesReady_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1110;
		internal static GlobalAchievementPercentagesReady_t Fill( IntPtr p ) => ((GlobalAchievementPercentagesReady_t)Marshal.PtrToStructure( p, typeof(GlobalAchievementPercentagesReady_t) ) );
		
		static Action<GlobalAchievementPercentagesReady_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GlobalAchievementPercentagesReady_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GlobalAchievementPercentagesReady_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1110, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1110, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LeaderboardUGCSet_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LeaderboardUGCSet_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1111;
		internal static LeaderboardUGCSet_t Fill( IntPtr p ) => ((LeaderboardUGCSet_t)Marshal.PtrToStructure( p, typeof(LeaderboardUGCSet_t) ) );
		
		static Action<LeaderboardUGCSet_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LeaderboardUGCSet_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LeaderboardUGCSet_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1111, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1111, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct PS3TrophiesInstalled_t : ICallbackData
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult EResult
		internal ulong RequiredDiskSpace; // m_ulRequiredDiskSpace uint64
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(PS3TrophiesInstalled_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1112;
		internal static PS3TrophiesInstalled_t Fill( IntPtr p ) => ((PS3TrophiesInstalled_t)Marshal.PtrToStructure( p, typeof(PS3TrophiesInstalled_t) ) );
		
		static Action<PS3TrophiesInstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<PS3TrophiesInstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<PS3TrophiesInstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1112, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1112, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GlobalStatsReceived_t : ICallbackData
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GlobalStatsReceived_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1112;
		internal static GlobalStatsReceived_t Fill( IntPtr p ) => ((GlobalStatsReceived_t)Marshal.PtrToStructure( p, typeof(GlobalStatsReceived_t) ) );
		
		static Action<GlobalStatsReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GlobalStatsReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GlobalStatsReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1112, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1112, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct DlcInstalled_t : ICallbackData
	{
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(DlcInstalled_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1005;
		internal static DlcInstalled_t Fill( IntPtr p ) => ((DlcInstalled_t)Marshal.PtrToStructure( p, typeof(DlcInstalled_t) ) );
		
		static Action<DlcInstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<DlcInstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<DlcInstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1005, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1005, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RegisterActivationCodeResponse_t : ICallbackData
	{
		internal RegisterActivationCodeResult Result; // m_eResult ERegisterActivationCodeResult
		internal uint PackageRegistered; // m_unPackageRegistered uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RegisterActivationCodeResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1008;
		internal static RegisterActivationCodeResponse_t Fill( IntPtr p ) => ((RegisterActivationCodeResponse_t)Marshal.PtrToStructure( p, typeof(RegisterActivationCodeResponse_t) ) );
		
		static Action<RegisterActivationCodeResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RegisterActivationCodeResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RegisterActivationCodeResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1008, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1008, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct NewUrlLaunchParameters_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(NewUrlLaunchParameters_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1014;
		internal static NewUrlLaunchParameters_t Fill( IntPtr p ) => ((NewUrlLaunchParameters_t)Marshal.PtrToStructure( p, typeof(NewUrlLaunchParameters_t) ) );
		
		static Action<NewUrlLaunchParameters_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<NewUrlLaunchParameters_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<NewUrlLaunchParameters_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1014, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1014, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct AppProofOfPurchaseKeyResponse_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal uint AppID; // m_nAppID uint32
		internal uint CchKeyLength; // m_cchKeyLength uint32
		internal string KeyUTF8() => System.Text.Encoding.UTF8.GetString( Key, 0, System.Array.IndexOf<byte>( Key, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 240)] // byte[] m_rgchKey
		internal byte[] Key; // m_rgchKey char [240]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(AppProofOfPurchaseKeyResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1021;
		internal static AppProofOfPurchaseKeyResponse_t Fill( IntPtr p ) => ((AppProofOfPurchaseKeyResponse_t)Marshal.PtrToStructure( p, typeof(AppProofOfPurchaseKeyResponse_t) ) );
		
		static Action<AppProofOfPurchaseKeyResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<AppProofOfPurchaseKeyResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<AppProofOfPurchaseKeyResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1021, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1021, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct FileDetailsResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal ulong FileSize; // m_ulFileSize uint64
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] //  m_FileSHA
		internal byte[] FileSHA; // m_FileSHA uint8 [20]
		internal uint Flags; // m_unFlags uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FileDetailsResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1023;
		internal static FileDetailsResult_t Fill( IntPtr p ) => ((FileDetailsResult_t)Marshal.PtrToStructure( p, typeof(FileDetailsResult_t) ) );
		
		static Action<FileDetailsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FileDetailsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FileDetailsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1023, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1023, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct P2PSessionRequest_t : ICallbackData
	{
		internal ulong SteamIDRemote; // m_steamIDRemote CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(P2PSessionRequest_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1202;
		internal static P2PSessionRequest_t Fill( IntPtr p ) => ((P2PSessionRequest_t)Marshal.PtrToStructure( p, typeof(P2PSessionRequest_t) ) );
		
		static Action<P2PSessionRequest_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<P2PSessionRequest_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<P2PSessionRequest_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1202, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1202, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct P2PSessionConnectFail_t : ICallbackData
	{
		internal ulong SteamIDRemote; // m_steamIDRemote CSteamID
		internal byte P2PSessionError; // m_eP2PSessionError uint8
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(P2PSessionConnectFail_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1203;
		internal static P2PSessionConnectFail_t Fill( IntPtr p ) => ((P2PSessionConnectFail_t)Marshal.PtrToStructure( p, typeof(P2PSessionConnectFail_t) ) );
		
		static Action<P2PSessionConnectFail_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<P2PSessionConnectFail_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<P2PSessionConnectFail_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1203, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1203, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct SocketStatusCallback_t : ICallbackData
	{
		internal uint Socket; // m_hSocket SNetSocket_t
		internal uint ListenSocket; // m_hListenSocket SNetListenSocket_t
		internal ulong SteamIDRemote; // m_steamIDRemote CSteamID
		internal int SNetSocketState; // m_eSNetSocketState int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SocketStatusCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1201;
		internal static SocketStatusCallback_t Fill( IntPtr p ) => ((SocketStatusCallback_t)Marshal.PtrToStructure( p, typeof(SocketStatusCallback_t) ) );
		
		static Action<SocketStatusCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SocketStatusCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SocketStatusCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1201, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1201, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct ScreenshotReady_t : ICallbackData
	{
		internal uint Local; // m_hLocal ScreenshotHandle
		internal Result Result; // m_eResult EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ScreenshotReady_t) );
		public int DataSize => _datasize;
		public int CallbackId => 2301;
		internal static ScreenshotReady_t Fill( IntPtr p ) => ((ScreenshotReady_t)Marshal.PtrToStructure( p, typeof(ScreenshotReady_t) ) );
		
		static Action<ScreenshotReady_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ScreenshotReady_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ScreenshotReady_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 2301, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 2301, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct ScreenshotRequested_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ScreenshotRequested_t) );
		public int DataSize => _datasize;
		public int CallbackId => 2302;
		internal static ScreenshotRequested_t Fill( IntPtr p ) => ((ScreenshotRequested_t)Marshal.PtrToStructure( p, typeof(ScreenshotRequested_t) ) );
		
		static Action<ScreenshotRequested_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ScreenshotRequested_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ScreenshotRequested_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 2302, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 2302, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct PlaybackStatusHasChanged_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(PlaybackStatusHasChanged_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4001;
		internal static PlaybackStatusHasChanged_t Fill( IntPtr p ) => ((PlaybackStatusHasChanged_t)Marshal.PtrToStructure( p, typeof(PlaybackStatusHasChanged_t) ) );
		
		static Action<PlaybackStatusHasChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<PlaybackStatusHasChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<PlaybackStatusHasChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4001, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4001, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct VolumeHasChanged_t : ICallbackData
	{
		internal float NewVolume; // m_flNewVolume float
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(VolumeHasChanged_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4002;
		internal static VolumeHasChanged_t Fill( IntPtr p ) => ((VolumeHasChanged_t)Marshal.PtrToStructure( p, typeof(VolumeHasChanged_t) ) );
		
		static Action<VolumeHasChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<VolumeHasChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<VolumeHasChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4002, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4002, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerRemoteWillActivate_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerRemoteWillActivate_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4101;
		internal static MusicPlayerRemoteWillActivate_t Fill( IntPtr p ) => ((MusicPlayerRemoteWillActivate_t)Marshal.PtrToStructure( p, typeof(MusicPlayerRemoteWillActivate_t) ) );
		
		static Action<MusicPlayerRemoteWillActivate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerRemoteWillActivate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerRemoteWillActivate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4101, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4101, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerRemoteWillDeactivate_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerRemoteWillDeactivate_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4102;
		internal static MusicPlayerRemoteWillDeactivate_t Fill( IntPtr p ) => ((MusicPlayerRemoteWillDeactivate_t)Marshal.PtrToStructure( p, typeof(MusicPlayerRemoteWillDeactivate_t) ) );
		
		static Action<MusicPlayerRemoteWillDeactivate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerRemoteWillDeactivate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerRemoteWillDeactivate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4102, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4102, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerRemoteToFront_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerRemoteToFront_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4103;
		internal static MusicPlayerRemoteToFront_t Fill( IntPtr p ) => ((MusicPlayerRemoteToFront_t)Marshal.PtrToStructure( p, typeof(MusicPlayerRemoteToFront_t) ) );
		
		static Action<MusicPlayerRemoteToFront_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerRemoteToFront_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerRemoteToFront_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4103, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4103, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerWillQuit_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerWillQuit_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4104;
		internal static MusicPlayerWillQuit_t Fill( IntPtr p ) => ((MusicPlayerWillQuit_t)Marshal.PtrToStructure( p, typeof(MusicPlayerWillQuit_t) ) );
		
		static Action<MusicPlayerWillQuit_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerWillQuit_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWillQuit_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4104, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4104, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerWantsPlay_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerWantsPlay_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4105;
		internal static MusicPlayerWantsPlay_t Fill( IntPtr p ) => ((MusicPlayerWantsPlay_t)Marshal.PtrToStructure( p, typeof(MusicPlayerWantsPlay_t) ) );
		
		static Action<MusicPlayerWantsPlay_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerWantsPlay_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsPlay_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4105, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4105, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerWantsPause_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerWantsPause_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4106;
		internal static MusicPlayerWantsPause_t Fill( IntPtr p ) => ((MusicPlayerWantsPause_t)Marshal.PtrToStructure( p, typeof(MusicPlayerWantsPause_t) ) );
		
		static Action<MusicPlayerWantsPause_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerWantsPause_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsPause_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4106, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4106, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerWantsPlayPrevious_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerWantsPlayPrevious_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4107;
		internal static MusicPlayerWantsPlayPrevious_t Fill( IntPtr p ) => ((MusicPlayerWantsPlayPrevious_t)Marshal.PtrToStructure( p, typeof(MusicPlayerWantsPlayPrevious_t) ) );
		
		static Action<MusicPlayerWantsPlayPrevious_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerWantsPlayPrevious_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsPlayPrevious_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4107, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4107, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerWantsPlayNext_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerWantsPlayNext_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4108;
		internal static MusicPlayerWantsPlayNext_t Fill( IntPtr p ) => ((MusicPlayerWantsPlayNext_t)Marshal.PtrToStructure( p, typeof(MusicPlayerWantsPlayNext_t) ) );
		
		static Action<MusicPlayerWantsPlayNext_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerWantsPlayNext_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsPlayNext_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4108, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4108, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerWantsShuffled_t : ICallbackData
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Shuffled; // m_bShuffled bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerWantsShuffled_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4109;
		internal static MusicPlayerWantsShuffled_t Fill( IntPtr p ) => ((MusicPlayerWantsShuffled_t)Marshal.PtrToStructure( p, typeof(MusicPlayerWantsShuffled_t) ) );
		
		static Action<MusicPlayerWantsShuffled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerWantsShuffled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsShuffled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4109, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4109, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerWantsLooped_t : ICallbackData
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Looped; // m_bLooped bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerWantsLooped_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4110;
		internal static MusicPlayerWantsLooped_t Fill( IntPtr p ) => ((MusicPlayerWantsLooped_t)Marshal.PtrToStructure( p, typeof(MusicPlayerWantsLooped_t) ) );
		
		static Action<MusicPlayerWantsLooped_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerWantsLooped_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsLooped_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4110, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4110, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerWantsVolume_t : ICallbackData
	{
		internal float NewVolume; // m_flNewVolume float
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerWantsVolume_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4011;
		internal static MusicPlayerWantsVolume_t Fill( IntPtr p ) => ((MusicPlayerWantsVolume_t)Marshal.PtrToStructure( p, typeof(MusicPlayerWantsVolume_t) ) );
		
		static Action<MusicPlayerWantsVolume_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerWantsVolume_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsVolume_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4011, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4011, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerSelectsQueueEntry_t : ICallbackData
	{
		internal int NID; // nID int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerSelectsQueueEntry_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4012;
		internal static MusicPlayerSelectsQueueEntry_t Fill( IntPtr p ) => ((MusicPlayerSelectsQueueEntry_t)Marshal.PtrToStructure( p, typeof(MusicPlayerSelectsQueueEntry_t) ) );
		
		static Action<MusicPlayerSelectsQueueEntry_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerSelectsQueueEntry_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerSelectsQueueEntry_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4012, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4012, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerSelectsPlaylistEntry_t : ICallbackData
	{
		internal int NID; // nID int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerSelectsPlaylistEntry_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4013;
		internal static MusicPlayerSelectsPlaylistEntry_t Fill( IntPtr p ) => ((MusicPlayerSelectsPlaylistEntry_t)Marshal.PtrToStructure( p, typeof(MusicPlayerSelectsPlaylistEntry_t) ) );
		
		static Action<MusicPlayerSelectsPlaylistEntry_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerSelectsPlaylistEntry_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerSelectsPlaylistEntry_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4013, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4013, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerWantsPlayingRepeatStatus_t : ICallbackData
	{
		internal int PlayingRepeatStatus; // m_nPlayingRepeatStatus int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerWantsPlayingRepeatStatus_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4114;
		internal static MusicPlayerWantsPlayingRepeatStatus_t Fill( IntPtr p ) => ((MusicPlayerWantsPlayingRepeatStatus_t)Marshal.PtrToStructure( p, typeof(MusicPlayerWantsPlayingRepeatStatus_t) ) );
		
		static Action<MusicPlayerWantsPlayingRepeatStatus_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerWantsPlayingRepeatStatus_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsPlayingRepeatStatus_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4114, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4114, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTTPRequestCompleted_t : ICallbackData
	{
		internal uint Request; // m_hRequest HTTPRequestHandle
		internal ulong ContextValue; // m_ulContextValue uint64
		[MarshalAs(UnmanagedType.I1)]
		internal bool RequestSuccessful; // m_bRequestSuccessful bool
		internal HTTPStatusCode StatusCode; // m_eStatusCode EHTTPStatusCode
		internal uint BodySize; // m_unBodySize uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTTPRequestCompleted_t) );
		public int DataSize => _datasize;
		public int CallbackId => 2101;
		internal static HTTPRequestCompleted_t Fill( IntPtr p ) => ((HTTPRequestCompleted_t)Marshal.PtrToStructure( p, typeof(HTTPRequestCompleted_t) ) );
		
		static Action<HTTPRequestCompleted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTTPRequestCompleted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTTPRequestCompleted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 2101, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 2101, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTTPRequestHeadersReceived_t : ICallbackData
	{
		internal uint Request; // m_hRequest HTTPRequestHandle
		internal ulong ContextValue; // m_ulContextValue uint64
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTTPRequestHeadersReceived_t) );
		public int DataSize => _datasize;
		public int CallbackId => 2102;
		internal static HTTPRequestHeadersReceived_t Fill( IntPtr p ) => ((HTTPRequestHeadersReceived_t)Marshal.PtrToStructure( p, typeof(HTTPRequestHeadersReceived_t) ) );
		
		static Action<HTTPRequestHeadersReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTTPRequestHeadersReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTTPRequestHeadersReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 2102, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 2102, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTTPRequestDataReceived_t : ICallbackData
	{
		internal uint Request; // m_hRequest HTTPRequestHandle
		internal ulong ContextValue; // m_ulContextValue uint64
		internal uint COffset; // m_cOffset uint32
		internal uint CBytesReceived; // m_cBytesReceived uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTTPRequestDataReceived_t) );
		public int DataSize => _datasize;
		public int CallbackId => 2103;
		internal static HTTPRequestDataReceived_t Fill( IntPtr p ) => ((HTTPRequestDataReceived_t)Marshal.PtrToStructure( p, typeof(HTTPRequestDataReceived_t) ) );
		
		static Action<HTTPRequestDataReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTTPRequestDataReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTTPRequestDataReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 2103, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 2103, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamUGCQueryCompleted_t : ICallbackData
	{
		internal ulong Handle; // m_handle UGCQueryHandle_t
		internal Result Result; // m_eResult EResult
		internal uint NumResultsReturned; // m_unNumResultsReturned uint32
		internal uint TotalMatchingResults; // m_unTotalMatchingResults uint32
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData; // m_bCachedData bool
		internal string NextCursorUTF8() => System.Text.Encoding.UTF8.GetString( NextCursor, 0, System.Array.IndexOf<byte>( NextCursor, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // byte[] m_rgchNextCursor
		internal byte[] NextCursor; // m_rgchNextCursor char [256]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamUGCQueryCompleted_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3401;
		internal static SteamUGCQueryCompleted_t Fill( IntPtr p ) => ((SteamUGCQueryCompleted_t)Marshal.PtrToStructure( p, typeof(SteamUGCQueryCompleted_t) ) );
		
		static Action<SteamUGCQueryCompleted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamUGCQueryCompleted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamUGCQueryCompleted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3401, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3401, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamUGCRequestUGCDetailsResult_t : ICallbackData
	{
		internal SteamUGCDetails_t Details; // m_details SteamUGCDetails_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData; // m_bCachedData bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamUGCRequestUGCDetailsResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3402;
		internal static SteamUGCRequestUGCDetailsResult_t Fill( IntPtr p ) => ((SteamUGCRequestUGCDetailsResult_t)Marshal.PtrToStructure( p, typeof(SteamUGCRequestUGCDetailsResult_t) ) );
		
		static Action<SteamUGCRequestUGCDetailsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamUGCRequestUGCDetailsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamUGCRequestUGCDetailsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3402, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3402, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct CreateItemResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(CreateItemResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3403;
		internal static CreateItemResult_t Fill( IntPtr p ) => ((CreateItemResult_t)Marshal.PtrToStructure( p, typeof(CreateItemResult_t) ) );
		
		static Action<CreateItemResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<CreateItemResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<CreateItemResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3403, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3403, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SubmitItemUpdateResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement bool
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SubmitItemUpdateResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3404;
		internal static SubmitItemUpdateResult_t Fill( IntPtr p ) => ((SubmitItemUpdateResult_t)Marshal.PtrToStructure( p, typeof(SubmitItemUpdateResult_t) ) );
		
		static Action<SubmitItemUpdateResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SubmitItemUpdateResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SubmitItemUpdateResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3404, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3404, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct ItemInstalled_t : ICallbackData
	{
		internal AppId AppID; // m_unAppID AppId_t
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ItemInstalled_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3405;
		internal static ItemInstalled_t Fill( IntPtr p ) => ((ItemInstalled_t)Marshal.PtrToStructure( p, typeof(ItemInstalled_t) ) );
		
		static Action<ItemInstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ItemInstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ItemInstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3405, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3405, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct DownloadItemResult_t : ICallbackData
	{
		internal AppId AppID; // m_unAppID AppId_t
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(DownloadItemResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3406;
		internal static DownloadItemResult_t Fill( IntPtr p ) => ((DownloadItemResult_t)Marshal.PtrToStructure( p, typeof(DownloadItemResult_t) ) );
		
		static Action<DownloadItemResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<DownloadItemResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<DownloadItemResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3406, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3406, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct UserFavoriteItemsListChanged_t : ICallbackData
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool WasAddRequest; // m_bWasAddRequest bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserFavoriteItemsListChanged_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3407;
		internal static UserFavoriteItemsListChanged_t Fill( IntPtr p ) => ((UserFavoriteItemsListChanged_t)Marshal.PtrToStructure( p, typeof(UserFavoriteItemsListChanged_t) ) );
		
		static Action<UserFavoriteItemsListChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UserFavoriteItemsListChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UserFavoriteItemsListChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3407, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3407, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SetUserItemVoteResult_t : ICallbackData
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool VoteUp; // m_bVoteUp bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SetUserItemVoteResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3408;
		internal static SetUserItemVoteResult_t Fill( IntPtr p ) => ((SetUserItemVoteResult_t)Marshal.PtrToStructure( p, typeof(SetUserItemVoteResult_t) ) );
		
		static Action<SetUserItemVoteResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SetUserItemVoteResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SetUserItemVoteResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3408, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3408, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GetUserItemVoteResult_t : ICallbackData
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool VotedUp; // m_bVotedUp bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool VotedDown; // m_bVotedDown bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool VoteSkipped; // m_bVoteSkipped bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GetUserItemVoteResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3409;
		internal static GetUserItemVoteResult_t Fill( IntPtr p ) => ((GetUserItemVoteResult_t)Marshal.PtrToStructure( p, typeof(GetUserItemVoteResult_t) ) );
		
		static Action<GetUserItemVoteResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GetUserItemVoteResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GetUserItemVoteResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3409, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3409, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct StartPlaytimeTrackingResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(StartPlaytimeTrackingResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3410;
		internal static StartPlaytimeTrackingResult_t Fill( IntPtr p ) => ((StartPlaytimeTrackingResult_t)Marshal.PtrToStructure( p, typeof(StartPlaytimeTrackingResult_t) ) );
		
		static Action<StartPlaytimeTrackingResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<StartPlaytimeTrackingResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<StartPlaytimeTrackingResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3410, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3410, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct StopPlaytimeTrackingResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(StopPlaytimeTrackingResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3411;
		internal static StopPlaytimeTrackingResult_t Fill( IntPtr p ) => ((StopPlaytimeTrackingResult_t)Marshal.PtrToStructure( p, typeof(StopPlaytimeTrackingResult_t) ) );
		
		static Action<StopPlaytimeTrackingResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<StopPlaytimeTrackingResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<StopPlaytimeTrackingResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3411, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3411, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct AddUGCDependencyResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal PublishedFileId ChildPublishedFileId; // m_nChildPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(AddUGCDependencyResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3412;
		internal static AddUGCDependencyResult_t Fill( IntPtr p ) => ((AddUGCDependencyResult_t)Marshal.PtrToStructure( p, typeof(AddUGCDependencyResult_t) ) );
		
		static Action<AddUGCDependencyResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<AddUGCDependencyResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<AddUGCDependencyResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3412, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3412, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoveUGCDependencyResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal PublishedFileId ChildPublishedFileId; // m_nChildPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoveUGCDependencyResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3413;
		internal static RemoveUGCDependencyResult_t Fill( IntPtr p ) => ((RemoveUGCDependencyResult_t)Marshal.PtrToStructure( p, typeof(RemoveUGCDependencyResult_t) ) );
		
		static Action<RemoveUGCDependencyResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoveUGCDependencyResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoveUGCDependencyResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3413, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3413, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct AddAppDependencyResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(AddAppDependencyResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3414;
		internal static AddAppDependencyResult_t Fill( IntPtr p ) => ((AddAppDependencyResult_t)Marshal.PtrToStructure( p, typeof(AddAppDependencyResult_t) ) );
		
		static Action<AddAppDependencyResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<AddAppDependencyResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<AddAppDependencyResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3414, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3414, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoveAppDependencyResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoveAppDependencyResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3415;
		internal static RemoveAppDependencyResult_t Fill( IntPtr p ) => ((RemoveAppDependencyResult_t)Marshal.PtrToStructure( p, typeof(RemoveAppDependencyResult_t) ) );
		
		static Action<RemoveAppDependencyResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoveAppDependencyResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoveAppDependencyResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3415, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3415, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GetAppDependenciesResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.U4)]
		internal AppId[] GAppIDs; // m_rgAppIDs AppId_t [32]
		internal uint NumAppDependencies; // m_nNumAppDependencies uint32
		internal uint TotalNumAppDependencies; // m_nTotalNumAppDependencies uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GetAppDependenciesResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3416;
		internal static GetAppDependenciesResult_t Fill( IntPtr p ) => ((GetAppDependenciesResult_t)Marshal.PtrToStructure( p, typeof(GetAppDependenciesResult_t) ) );
		
		static Action<GetAppDependenciesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GetAppDependenciesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GetAppDependenciesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3416, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3416, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct DeleteItemResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(DeleteItemResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3417;
		internal static DeleteItemResult_t Fill( IntPtr p ) => ((DeleteItemResult_t)Marshal.PtrToStructure( p, typeof(DeleteItemResult_t) ) );
		
		static Action<DeleteItemResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<DeleteItemResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<DeleteItemResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3417, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3417, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamAppInstalled_t : ICallbackData
	{
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamAppInstalled_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3901;
		internal static SteamAppInstalled_t Fill( IntPtr p ) => ((SteamAppInstalled_t)Marshal.PtrToStructure( p, typeof(SteamAppInstalled_t) ) );
		
		static Action<SteamAppInstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamAppInstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamAppInstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3901, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3901, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamAppUninstalled_t : ICallbackData
	{
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamAppUninstalled_t) );
		public int DataSize => _datasize;
		public int CallbackId => 3902;
		internal static SteamAppUninstalled_t Fill( IntPtr p ) => ((SteamAppUninstalled_t)Marshal.PtrToStructure( p, typeof(SteamAppUninstalled_t) ) );
		
		static Action<SteamAppUninstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamAppUninstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamAppUninstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 3902, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 3902, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_BrowserReady_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_BrowserReady_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4501;
		internal static HTML_BrowserReady_t Fill( IntPtr p ) => ((HTML_BrowserReady_t)Marshal.PtrToStructure( p, typeof(HTML_BrowserReady_t) ) );
		
		static Action<HTML_BrowserReady_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_BrowserReady_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_BrowserReady_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4501, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4501, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_NeedsPaint_t : ICallbackData
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
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_NeedsPaint_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4502;
		internal static HTML_NeedsPaint_t Fill( IntPtr p ) => ((HTML_NeedsPaint_t)Marshal.PtrToStructure( p, typeof(HTML_NeedsPaint_t) ) );
		
		static Action<HTML_NeedsPaint_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_NeedsPaint_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_NeedsPaint_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4502, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4502, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_StartRequest_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		internal string PchTarget; // pchTarget const char *
		internal string PchPostData; // pchPostData const char *
		[MarshalAs(UnmanagedType.I1)]
		internal bool BIsRedirect; // bIsRedirect bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_StartRequest_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4503;
		internal static HTML_StartRequest_t Fill( IntPtr p ) => ((HTML_StartRequest_t)Marshal.PtrToStructure( p, typeof(HTML_StartRequest_t) ) );
		
		static Action<HTML_StartRequest_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_StartRequest_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_StartRequest_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4503, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4503, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_CloseBrowser_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_CloseBrowser_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4504;
		internal static HTML_CloseBrowser_t Fill( IntPtr p ) => ((HTML_CloseBrowser_t)Marshal.PtrToStructure( p, typeof(HTML_CloseBrowser_t) ) );
		
		static Action<HTML_CloseBrowser_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_CloseBrowser_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_CloseBrowser_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4504, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4504, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_URLChanged_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		internal string PchPostData; // pchPostData const char *
		[MarshalAs(UnmanagedType.I1)]
		internal bool BIsRedirect; // bIsRedirect bool
		internal string PchPageTitle; // pchPageTitle const char *
		[MarshalAs(UnmanagedType.I1)]
		internal bool BNewNavigation; // bNewNavigation bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_URLChanged_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4505;
		internal static HTML_URLChanged_t Fill( IntPtr p ) => ((HTML_URLChanged_t)Marshal.PtrToStructure( p, typeof(HTML_URLChanged_t) ) );
		
		static Action<HTML_URLChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_URLChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_URLChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4505, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4505, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_FinishedRequest_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		internal string PchPageTitle; // pchPageTitle const char *
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_FinishedRequest_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4506;
		internal static HTML_FinishedRequest_t Fill( IntPtr p ) => ((HTML_FinishedRequest_t)Marshal.PtrToStructure( p, typeof(HTML_FinishedRequest_t) ) );
		
		static Action<HTML_FinishedRequest_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_FinishedRequest_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_FinishedRequest_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4506, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4506, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_OpenLinkInNewTab_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_OpenLinkInNewTab_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4507;
		internal static HTML_OpenLinkInNewTab_t Fill( IntPtr p ) => ((HTML_OpenLinkInNewTab_t)Marshal.PtrToStructure( p, typeof(HTML_OpenLinkInNewTab_t) ) );
		
		static Action<HTML_OpenLinkInNewTab_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_OpenLinkInNewTab_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_OpenLinkInNewTab_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4507, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4507, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_ChangedTitle_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchTitle; // pchTitle const char *
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_ChangedTitle_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4508;
		internal static HTML_ChangedTitle_t Fill( IntPtr p ) => ((HTML_ChangedTitle_t)Marshal.PtrToStructure( p, typeof(HTML_ChangedTitle_t) ) );
		
		static Action<HTML_ChangedTitle_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_ChangedTitle_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_ChangedTitle_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4508, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4508, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_SearchResults_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint UnResults; // unResults uint32
		internal uint UnCurrentMatch; // unCurrentMatch uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_SearchResults_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4509;
		internal static HTML_SearchResults_t Fill( IntPtr p ) => ((HTML_SearchResults_t)Marshal.PtrToStructure( p, typeof(HTML_SearchResults_t) ) );
		
		static Action<HTML_SearchResults_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_SearchResults_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_SearchResults_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4509, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4509, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_CanGoBackAndForward_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		[MarshalAs(UnmanagedType.I1)]
		internal bool BCanGoBack; // bCanGoBack bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool BCanGoForward; // bCanGoForward bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_CanGoBackAndForward_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4510;
		internal static HTML_CanGoBackAndForward_t Fill( IntPtr p ) => ((HTML_CanGoBackAndForward_t)Marshal.PtrToStructure( p, typeof(HTML_CanGoBackAndForward_t) ) );
		
		static Action<HTML_CanGoBackAndForward_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_CanGoBackAndForward_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_CanGoBackAndForward_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4510, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4510, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_HorizontalScroll_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint UnScrollMax; // unScrollMax uint32
		internal uint UnScrollCurrent; // unScrollCurrent uint32
		internal float FlPageScale; // flPageScale float
		[MarshalAs(UnmanagedType.I1)]
		internal bool BVisible; // bVisible bool
		internal uint UnPageSize; // unPageSize uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_HorizontalScroll_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4511;
		internal static HTML_HorizontalScroll_t Fill( IntPtr p ) => ((HTML_HorizontalScroll_t)Marshal.PtrToStructure( p, typeof(HTML_HorizontalScroll_t) ) );
		
		static Action<HTML_HorizontalScroll_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_HorizontalScroll_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_HorizontalScroll_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4511, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4511, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_VerticalScroll_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint UnScrollMax; // unScrollMax uint32
		internal uint UnScrollCurrent; // unScrollCurrent uint32
		internal float FlPageScale; // flPageScale float
		[MarshalAs(UnmanagedType.I1)]
		internal bool BVisible; // bVisible bool
		internal uint UnPageSize; // unPageSize uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_VerticalScroll_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4512;
		internal static HTML_VerticalScroll_t Fill( IntPtr p ) => ((HTML_VerticalScroll_t)Marshal.PtrToStructure( p, typeof(HTML_VerticalScroll_t) ) );
		
		static Action<HTML_VerticalScroll_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_VerticalScroll_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_VerticalScroll_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4512, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4512, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_LinkAtPosition_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint X; // x uint32
		internal uint Y; // y uint32
		internal string PchURL; // pchURL const char *
		[MarshalAs(UnmanagedType.I1)]
		internal bool BInput; // bInput bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool BLiveLink; // bLiveLink bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_LinkAtPosition_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4513;
		internal static HTML_LinkAtPosition_t Fill( IntPtr p ) => ((HTML_LinkAtPosition_t)Marshal.PtrToStructure( p, typeof(HTML_LinkAtPosition_t) ) );
		
		static Action<HTML_LinkAtPosition_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_LinkAtPosition_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_LinkAtPosition_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4513, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4513, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_JSAlert_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMessage; // pchMessage const char *
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_JSAlert_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4514;
		internal static HTML_JSAlert_t Fill( IntPtr p ) => ((HTML_JSAlert_t)Marshal.PtrToStructure( p, typeof(HTML_JSAlert_t) ) );
		
		static Action<HTML_JSAlert_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_JSAlert_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_JSAlert_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4514, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4514, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_JSConfirm_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMessage; // pchMessage const char *
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_JSConfirm_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4515;
		internal static HTML_JSConfirm_t Fill( IntPtr p ) => ((HTML_JSConfirm_t)Marshal.PtrToStructure( p, typeof(HTML_JSConfirm_t) ) );
		
		static Action<HTML_JSConfirm_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_JSConfirm_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_JSConfirm_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4515, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4515, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_FileOpenDialog_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchTitle; // pchTitle const char *
		internal string PchInitialFile; // pchInitialFile const char *
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_FileOpenDialog_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4516;
		internal static HTML_FileOpenDialog_t Fill( IntPtr p ) => ((HTML_FileOpenDialog_t)Marshal.PtrToStructure( p, typeof(HTML_FileOpenDialog_t) ) );
		
		static Action<HTML_FileOpenDialog_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_FileOpenDialog_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_FileOpenDialog_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4516, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4516, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_NewWindow_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		internal uint UnX; // unX uint32
		internal uint UnY; // unY uint32
		internal uint UnWide; // unWide uint32
		internal uint UnTall; // unTall uint32
		internal uint UnNewWindow_BrowserHandle_IGNORE; // unNewWindow_BrowserHandle_IGNORE HHTMLBrowser
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_NewWindow_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4521;
		internal static HTML_NewWindow_t Fill( IntPtr p ) => ((HTML_NewWindow_t)Marshal.PtrToStructure( p, typeof(HTML_NewWindow_t) ) );
		
		static Action<HTML_NewWindow_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_NewWindow_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_NewWindow_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4521, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4521, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_SetCursor_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint EMouseCursor; // eMouseCursor uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_SetCursor_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4522;
		internal static HTML_SetCursor_t Fill( IntPtr p ) => ((HTML_SetCursor_t)Marshal.PtrToStructure( p, typeof(HTML_SetCursor_t) ) );
		
		static Action<HTML_SetCursor_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_SetCursor_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_SetCursor_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4522, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4522, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_StatusText_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMsg; // pchMsg const char *
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_StatusText_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4523;
		internal static HTML_StatusText_t Fill( IntPtr p ) => ((HTML_StatusText_t)Marshal.PtrToStructure( p, typeof(HTML_StatusText_t) ) );
		
		static Action<HTML_StatusText_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_StatusText_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_StatusText_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4523, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4523, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_ShowToolTip_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMsg; // pchMsg const char *
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_ShowToolTip_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4524;
		internal static HTML_ShowToolTip_t Fill( IntPtr p ) => ((HTML_ShowToolTip_t)Marshal.PtrToStructure( p, typeof(HTML_ShowToolTip_t) ) );
		
		static Action<HTML_ShowToolTip_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_ShowToolTip_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_ShowToolTip_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4524, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4524, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_UpdateToolTip_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMsg; // pchMsg const char *
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_UpdateToolTip_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4525;
		internal static HTML_UpdateToolTip_t Fill( IntPtr p ) => ((HTML_UpdateToolTip_t)Marshal.PtrToStructure( p, typeof(HTML_UpdateToolTip_t) ) );
		
		static Action<HTML_UpdateToolTip_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_UpdateToolTip_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_UpdateToolTip_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4525, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4525, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_HideToolTip_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_HideToolTip_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4526;
		internal static HTML_HideToolTip_t Fill( IntPtr p ) => ((HTML_HideToolTip_t)Marshal.PtrToStructure( p, typeof(HTML_HideToolTip_t) ) );
		
		static Action<HTML_HideToolTip_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_HideToolTip_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_HideToolTip_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4526, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4526, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_BrowserRestarted_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint UnOldBrowserHandle; // unOldBrowserHandle HHTMLBrowser
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_BrowserRestarted_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4527;
		internal static HTML_BrowserRestarted_t Fill( IntPtr p ) => ((HTML_BrowserRestarted_t)Marshal.PtrToStructure( p, typeof(HTML_BrowserRestarted_t) ) );
		
		static Action<HTML_BrowserRestarted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_BrowserRestarted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_BrowserRestarted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4527, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4527, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamInventoryResultReady_t : ICallbackData
	{
		internal int Handle; // m_handle SteamInventoryResult_t
		internal Result Result; // m_result EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamInventoryResultReady_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4700;
		internal static SteamInventoryResultReady_t Fill( IntPtr p ) => ((SteamInventoryResultReady_t)Marshal.PtrToStructure( p, typeof(SteamInventoryResultReady_t) ) );
		
		static Action<SteamInventoryResultReady_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamInventoryResultReady_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamInventoryResultReady_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4700, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4700, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamInventoryFullUpdate_t : ICallbackData
	{
		internal int Handle; // m_handle SteamInventoryResult_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamInventoryFullUpdate_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4701;
		internal static SteamInventoryFullUpdate_t Fill( IntPtr p ) => ((SteamInventoryFullUpdate_t)Marshal.PtrToStructure( p, typeof(SteamInventoryFullUpdate_t) ) );
		
		static Action<SteamInventoryFullUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamInventoryFullUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamInventoryFullUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4701, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4701, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamInventoryDefinitionUpdate_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamInventoryDefinitionUpdate_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4702;
		internal static SteamInventoryDefinitionUpdate_t Fill( IntPtr p ) => ((SteamInventoryDefinitionUpdate_t)Marshal.PtrToStructure( p, typeof(SteamInventoryDefinitionUpdate_t) ) );
		
		static Action<SteamInventoryDefinitionUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamInventoryDefinitionUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamInventoryDefinitionUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4702, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4702, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct SteamInventoryEligiblePromoItemDefIDs_t : ICallbackData
	{
		internal Result Result; // m_result EResult
		internal ulong SteamID; // m_steamID CSteamID
		internal int UmEligiblePromoItemDefs; // m_numEligiblePromoItemDefs int
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData; // m_bCachedData bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamInventoryEligiblePromoItemDefIDs_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4703;
		internal static SteamInventoryEligiblePromoItemDefIDs_t Fill( IntPtr p ) => ((SteamInventoryEligiblePromoItemDefIDs_t)Marshal.PtrToStructure( p, typeof(SteamInventoryEligiblePromoItemDefIDs_t) ) );
		
		static Action<SteamInventoryEligiblePromoItemDefIDs_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamInventoryEligiblePromoItemDefIDs_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamInventoryEligiblePromoItemDefIDs_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4703, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4703, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamInventoryStartPurchaseResult_t : ICallbackData
	{
		internal Result Result; // m_result EResult
		internal ulong OrderID; // m_ulOrderID uint64
		internal ulong TransID; // m_ulTransID uint64
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamInventoryStartPurchaseResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4704;
		internal static SteamInventoryStartPurchaseResult_t Fill( IntPtr p ) => ((SteamInventoryStartPurchaseResult_t)Marshal.PtrToStructure( p, typeof(SteamInventoryStartPurchaseResult_t) ) );
		
		static Action<SteamInventoryStartPurchaseResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamInventoryStartPurchaseResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamInventoryStartPurchaseResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4704, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4704, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamInventoryRequestPricesResult_t : ICallbackData
	{
		internal Result Result; // m_result EResult
		internal string CurrencyUTF8() => System.Text.Encoding.UTF8.GetString( Currency, 0, System.Array.IndexOf<byte>( Currency, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] // byte[] m_rgchCurrency
		internal byte[] Currency; // m_rgchCurrency char [4]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamInventoryRequestPricesResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4705;
		internal static SteamInventoryRequestPricesResult_t Fill( IntPtr p ) => ((SteamInventoryRequestPricesResult_t)Marshal.PtrToStructure( p, typeof(SteamInventoryRequestPricesResult_t) ) );
		
		static Action<SteamInventoryRequestPricesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamInventoryRequestPricesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamInventoryRequestPricesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4705, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4705, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GetVideoURLResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal AppId VideoAppID; // m_unVideoAppID AppId_t
		internal string URLUTF8() => System.Text.Encoding.UTF8.GetString( URL, 0, System.Array.IndexOf<byte>( URL, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // byte[] m_rgchURL
		internal byte[] URL; // m_rgchURL char [256]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GetVideoURLResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4611;
		internal static GetVideoURLResult_t Fill( IntPtr p ) => ((GetVideoURLResult_t)Marshal.PtrToStructure( p, typeof(GetVideoURLResult_t) ) );
		
		static Action<GetVideoURLResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GetVideoURLResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GetVideoURLResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4611, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4611, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GetOPFSettingsResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal AppId VideoAppID; // m_unVideoAppID AppId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GetOPFSettingsResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4624;
		internal static GetOPFSettingsResult_t Fill( IntPtr p ) => ((GetOPFSettingsResult_t)Marshal.PtrToStructure( p, typeof(GetOPFSettingsResult_t) ) );
		
		static Action<GetOPFSettingsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GetOPFSettingsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GetOPFSettingsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4624, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4624, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct BroadcastUploadStart_t : ICallbackData
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool IsRTMP; // m_bIsRTMP bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(BroadcastUploadStart_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4604;
		internal static BroadcastUploadStart_t Fill( IntPtr p ) => ((BroadcastUploadStart_t)Marshal.PtrToStructure( p, typeof(BroadcastUploadStart_t) ) );
		
		static Action<BroadcastUploadStart_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<BroadcastUploadStart_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<BroadcastUploadStart_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4604, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4604, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct BroadcastUploadStop_t : ICallbackData
	{
		internal BroadcastUploadResult Result; // m_eResult EBroadcastUploadResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(BroadcastUploadStop_t) );
		public int DataSize => _datasize;
		public int CallbackId => 4605;
		internal static BroadcastUploadStop_t Fill( IntPtr p ) => ((BroadcastUploadStop_t)Marshal.PtrToStructure( p, typeof(BroadcastUploadStop_t) ) );
		
		static Action<BroadcastUploadStop_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<BroadcastUploadStop_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<BroadcastUploadStop_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 4605, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 4605, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamParentalSettingsChanged_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamParentalSettingsChanged_t) );
		public int DataSize => _datasize;
		public int CallbackId => 5001;
		internal static SteamParentalSettingsChanged_t Fill( IntPtr p ) => ((SteamParentalSettingsChanged_t)Marshal.PtrToStructure( p, typeof(SteamParentalSettingsChanged_t) ) );
		
		static Action<SteamParentalSettingsChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamParentalSettingsChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamParentalSettingsChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 5001, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 5001, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamRemotePlaySessionConnected_t : ICallbackData
	{
		internal uint SessionID; // m_unSessionID RemotePlaySessionID_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamRemotePlaySessionConnected_t) );
		public int DataSize => _datasize;
		public int CallbackId => 5701;
		internal static SteamRemotePlaySessionConnected_t Fill( IntPtr p ) => ((SteamRemotePlaySessionConnected_t)Marshal.PtrToStructure( p, typeof(SteamRemotePlaySessionConnected_t) ) );
		
		static Action<SteamRemotePlaySessionConnected_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamRemotePlaySessionConnected_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamRemotePlaySessionConnected_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 5701, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 5701, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamRemotePlaySessionDisconnected_t : ICallbackData
	{
		internal uint SessionID; // m_unSessionID RemotePlaySessionID_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamRemotePlaySessionDisconnected_t) );
		public int DataSize => _datasize;
		public int CallbackId => 5702;
		internal static SteamRemotePlaySessionDisconnected_t Fill( IntPtr p ) => ((SteamRemotePlaySessionDisconnected_t)Marshal.PtrToStructure( p, typeof(SteamRemotePlaySessionDisconnected_t) ) );
		
		static Action<SteamRemotePlaySessionDisconnected_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamRemotePlaySessionDisconnected_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamRemotePlaySessionDisconnected_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 5702, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 5702, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamNetConnectionStatusChangedCallback_t : ICallbackData
	{
		internal Connection Conn; // m_hConn HSteamNetConnection
		internal ConnectionInfo Nfo; // m_info SteamNetConnectionInfo_t
		internal ConnectionState OldState; // m_eOldState ESteamNetworkingConnectionState
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamNetConnectionStatusChangedCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1221;
		internal static SteamNetConnectionStatusChangedCallback_t Fill( IntPtr p ) => ((SteamNetConnectionStatusChangedCallback_t)Marshal.PtrToStructure( p, typeof(SteamNetConnectionStatusChangedCallback_t) ) );
		
		static Action<SteamNetConnectionStatusChangedCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamNetConnectionStatusChangedCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamNetConnectionStatusChangedCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1221, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1221, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamNetAuthenticationStatus_t : ICallbackData
	{
		internal SteamNetworkingAvailability Avail; // m_eAvail ESteamNetworkingAvailability
		internal string DebugMsgUTF8() => System.Text.Encoding.UTF8.GetString( DebugMsg, 0, System.Array.IndexOf<byte>( DebugMsg, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // byte[] m_debugMsg
		internal byte[] DebugMsg; // m_debugMsg char [256]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamNetAuthenticationStatus_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1222;
		internal static SteamNetAuthenticationStatus_t Fill( IntPtr p ) => ((SteamNetAuthenticationStatus_t)Marshal.PtrToStructure( p, typeof(SteamNetAuthenticationStatus_t) ) );
		
		static Action<SteamNetAuthenticationStatus_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamNetAuthenticationStatus_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamNetAuthenticationStatus_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1222, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1222, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamRelayNetworkStatus_t : ICallbackData
	{
		internal SteamNetworkingAvailability Avail; // m_eAvail ESteamNetworkingAvailability
		internal int PingMeasurementInProgress; // m_bPingMeasurementInProgress int
		internal SteamNetworkingAvailability AvailNetworkConfig; // m_eAvailNetworkConfig ESteamNetworkingAvailability
		internal SteamNetworkingAvailability AvailAnyRelay; // m_eAvailAnyRelay ESteamNetworkingAvailability
		internal string DebugMsgUTF8() => System.Text.Encoding.UTF8.GetString( DebugMsg, 0, System.Array.IndexOf<byte>( DebugMsg, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // byte[] m_debugMsg
		internal byte[] DebugMsg; // m_debugMsg char [256]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamRelayNetworkStatus_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1281;
		internal static SteamRelayNetworkStatus_t Fill( IntPtr p ) => ((SteamRelayNetworkStatus_t)Marshal.PtrToStructure( p, typeof(SteamRelayNetworkStatus_t) ) );
		
		static Action<SteamRelayNetworkStatus_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamRelayNetworkStatus_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamRelayNetworkStatus_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1281, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1281, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GSClientApprove_t : ICallbackData
	{
		internal ulong SteamID; // m_SteamID CSteamID
		internal ulong OwnerSteamID; // m_OwnerSteamID CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientApprove_t) );
		public int DataSize => _datasize;
		public int CallbackId => 201;
		internal static GSClientApprove_t Fill( IntPtr p ) => ((GSClientApprove_t)Marshal.PtrToStructure( p, typeof(GSClientApprove_t) ) );
		
		static Action<GSClientApprove_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSClientApprove_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSClientApprove_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 201, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 201, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GSClientDeny_t : ICallbackData
	{
		internal ulong SteamID; // m_SteamID CSteamID
		internal DenyReason DenyReason; // m_eDenyReason EDenyReason
		internal string OptionalTextUTF8() => System.Text.Encoding.UTF8.GetString( OptionalText, 0, System.Array.IndexOf<byte>( OptionalText, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)] // byte[] m_rgchOptionalText
		internal byte[] OptionalText; // m_rgchOptionalText char [128]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientDeny_t) );
		public int DataSize => _datasize;
		public int CallbackId => 202;
		internal static GSClientDeny_t Fill( IntPtr p ) => ((GSClientDeny_t)Marshal.PtrToStructure( p, typeof(GSClientDeny_t) ) );
		
		static Action<GSClientDeny_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSClientDeny_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSClientDeny_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 202, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 202, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GSClientKick_t : ICallbackData
	{
		internal ulong SteamID; // m_SteamID CSteamID
		internal DenyReason DenyReason; // m_eDenyReason EDenyReason
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientKick_t) );
		public int DataSize => _datasize;
		public int CallbackId => 203;
		internal static GSClientKick_t Fill( IntPtr p ) => ((GSClientKick_t)Marshal.PtrToStructure( p, typeof(GSClientKick_t) ) );
		
		static Action<GSClientKick_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSClientKick_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSClientKick_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 203, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 203, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GSClientAchievementStatus_t : ICallbackData
	{
		internal ulong SteamID; // m_SteamID uint64
		internal string PchAchievementUTF8() => System.Text.Encoding.UTF8.GetString( PchAchievement, 0, System.Array.IndexOf<byte>( PchAchievement, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)] // byte[] m_pchAchievement
		internal byte[] PchAchievement; // m_pchAchievement char [128]
		[MarshalAs(UnmanagedType.I1)]
		internal bool Unlocked; // m_bUnlocked bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientAchievementStatus_t) );
		public int DataSize => _datasize;
		public int CallbackId => 206;
		internal static GSClientAchievementStatus_t Fill( IntPtr p ) => ((GSClientAchievementStatus_t)Marshal.PtrToStructure( p, typeof(GSClientAchievementStatus_t) ) );
		
		static Action<GSClientAchievementStatus_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSClientAchievementStatus_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSClientAchievementStatus_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 206, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 206, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GSPolicyResponse_t : ICallbackData
	{
		internal byte Secure; // m_bSecure uint8
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSPolicyResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => 115;
		internal static GSPolicyResponse_t Fill( IntPtr p ) => ((GSPolicyResponse_t)Marshal.PtrToStructure( p, typeof(GSPolicyResponse_t) ) );
		
		static Action<GSPolicyResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSPolicyResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSPolicyResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 115, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 115, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GSGameplayStats_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal int Rank; // m_nRank int32
		internal uint TotalConnects; // m_unTotalConnects uint32
		internal uint TotalMinutesPlayed; // m_unTotalMinutesPlayed uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSGameplayStats_t) );
		public int DataSize => _datasize;
		public int CallbackId => 207;
		internal static GSGameplayStats_t Fill( IntPtr p ) => ((GSGameplayStats_t)Marshal.PtrToStructure( p, typeof(GSGameplayStats_t) ) );
		
		static Action<GSGameplayStats_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSGameplayStats_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSGameplayStats_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 207, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 207, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GSClientGroupStatus_t : ICallbackData
	{
		internal ulong SteamIDUser; // m_SteamIDUser CSteamID
		internal ulong SteamIDGroup; // m_SteamIDGroup CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool Member; // m_bMember bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool Officer; // m_bOfficer bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientGroupStatus_t) );
		public int DataSize => _datasize;
		public int CallbackId => 208;
		internal static GSClientGroupStatus_t Fill( IntPtr p ) => ((GSClientGroupStatus_t)Marshal.PtrToStructure( p, typeof(GSClientGroupStatus_t) ) );
		
		static Action<GSClientGroupStatus_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSClientGroupStatus_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSClientGroupStatus_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 208, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 208, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GSReputation_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal uint ReputationScore; // m_unReputationScore uint32
		[MarshalAs(UnmanagedType.I1)]
		internal bool Banned; // m_bBanned bool
		internal uint BannedIP; // m_unBannedIP uint32
		internal ushort BannedPort; // m_usBannedPort uint16
		internal ulong BannedGameID; // m_ulBannedGameID uint64
		internal uint BanExpires; // m_unBanExpires uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSReputation_t) );
		public int DataSize => _datasize;
		public int CallbackId => 209;
		internal static GSReputation_t Fill( IntPtr p ) => ((GSReputation_t)Marshal.PtrToStructure( p, typeof(GSReputation_t) ) );
		
		static Action<GSReputation_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSReputation_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSReputation_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 209, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 209, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct AssociateWithClanResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(AssociateWithClanResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 210;
		internal static AssociateWithClanResult_t Fill( IntPtr p ) => ((AssociateWithClanResult_t)Marshal.PtrToStructure( p, typeof(AssociateWithClanResult_t) ) );
		
		static Action<AssociateWithClanResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<AssociateWithClanResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<AssociateWithClanResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 210, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 210, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct ComputeNewPlayerCompatibilityResult_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal int CPlayersThatDontLikeCandidate; // m_cPlayersThatDontLikeCandidate int
		internal int CPlayersThatCandidateDoesntLike; // m_cPlayersThatCandidateDoesntLike int
		internal int CClanPlayersThatDontLikeCandidate; // m_cClanPlayersThatDontLikeCandidate int
		internal ulong SteamIDCandidate; // m_SteamIDCandidate CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ComputeNewPlayerCompatibilityResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => 211;
		internal static ComputeNewPlayerCompatibilityResult_t Fill( IntPtr p ) => ((ComputeNewPlayerCompatibilityResult_t)Marshal.PtrToStructure( p, typeof(ComputeNewPlayerCompatibilityResult_t) ) );
		
		static Action<ComputeNewPlayerCompatibilityResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ComputeNewPlayerCompatibilityResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ComputeNewPlayerCompatibilityResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 211, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 211, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GSStatsReceived_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal ulong SteamIDUser; // m_steamIDUser CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSStatsReceived_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1800;
		internal static GSStatsReceived_t Fill( IntPtr p ) => ((GSStatsReceived_t)Marshal.PtrToStructure( p, typeof(GSStatsReceived_t) ) );
		
		static Action<GSStatsReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSStatsReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSStatsReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1800, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1800, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GSStatsStored_t : ICallbackData
	{
		internal Result Result; // m_eResult EResult
		internal ulong SteamIDUser; // m_steamIDUser CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSStatsStored_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1801;
		internal static GSStatsStored_t Fill( IntPtr p ) => ((GSStatsStored_t)Marshal.PtrToStructure( p, typeof(GSStatsStored_t) ) );
		
		static Action<GSStatsStored_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSStatsStored_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSStatsStored_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1801, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1801, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GSStatsUnloaded_t : ICallbackData
	{
		internal ulong SteamIDUser; // m_steamIDUser CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSStatsUnloaded_t) );
		public int DataSize => _datasize;
		public int CallbackId => 1108;
		internal static GSStatsUnloaded_t Fill( IntPtr p ) => ((GSStatsUnloaded_t)Marshal.PtrToStructure( p, typeof(GSStatsUnloaded_t) ) );
		
		static Action<GSStatsUnloaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSStatsUnloaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSStatsUnloaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, 1108, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, 1108, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
}
