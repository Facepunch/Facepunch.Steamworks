using System;
using System.Runtime.InteropServices;
using System.Linq;
using Steamworks.Data;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct CallbackMsg_t
	{
		internal int SteamUser; // m_hSteamUser HSteamUser
		internal int Callback; // m_iCallback int
		internal IntPtr ParamPtr; // m_pubParam uint8 *
		internal int ParamCount; // m_cubParam int
		
		#region Marshalling
		internal static CallbackMsg_t Fill( IntPtr p ) => ((CallbackMsg_t)(CallbackMsg_t) Marshal.PtrToStructure( p, typeof(CallbackMsg_t) ) );
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamServerConnectFailure_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool StillRetrying; // m_bStillRetrying _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamServerConnectFailure_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUser + 2;
		internal static SteamServerConnectFailure_t Fill( IntPtr p ) => ((SteamServerConnectFailure_t)Marshal.PtrToStructure( p, typeof(SteamServerConnectFailure_t) ) );
		
		static Action<SteamServerConnectFailure_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamServerConnectFailure_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamServerConnectFailure_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUser + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUser + 2, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamServersDisconnected_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamServersDisconnected_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUser + 3;
		internal static SteamServersDisconnected_t Fill( IntPtr p ) => ((SteamServersDisconnected_t)Marshal.PtrToStructure( p, typeof(SteamServersDisconnected_t) ) );
		
		static Action<SteamServersDisconnected_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamServersDisconnected_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamServersDisconnected_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUser + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUser + 3, false );
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
		public int CallbackId => CallbackIdentifiers.SteamUser + 13;
		internal static ClientGameServerDeny_t Fill( IntPtr p ) => ((ClientGameServerDeny_t)Marshal.PtrToStructure( p, typeof(ClientGameServerDeny_t) ) );
		
		static Action<ClientGameServerDeny_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ClientGameServerDeny_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ClientGameServerDeny_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUser + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUser + 13, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct ValidateAuthTicketResponse_t : ICallbackData
	{
		internal ulong SteamID; // m_SteamID class CSteamID
		internal AuthResponse AuthSessionResponse; // m_eAuthSessionResponse enum EAuthSessionResponse
		internal ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ValidateAuthTicketResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUser + 43;
		internal static ValidateAuthTicketResponse_t Fill( IntPtr p ) => ((ValidateAuthTicketResponse_t)Marshal.PtrToStructure( p, typeof(ValidateAuthTicketResponse_t) ) );
		
		static Action<ValidateAuthTicketResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ValidateAuthTicketResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ValidateAuthTicketResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUser + 43, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUser + 43, false );
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
		public int CallbackId => CallbackIdentifiers.SteamUser + 52;
		internal static MicroTxnAuthorizationResponse_t Fill( IntPtr p ) => ((MicroTxnAuthorizationResponse_t)Marshal.PtrToStructure( p, typeof(MicroTxnAuthorizationResponse_t) ) );
		
		static Action<MicroTxnAuthorizationResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MicroTxnAuthorizationResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MicroTxnAuthorizationResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUser + 52, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUser + 52, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct EncryptedAppTicketResponse_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(EncryptedAppTicketResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUser + 54;
		internal static EncryptedAppTicketResponse_t Fill( IntPtr p ) => ((EncryptedAppTicketResponse_t)Marshal.PtrToStructure( p, typeof(EncryptedAppTicketResponse_t) ) );
		
		static Action<EncryptedAppTicketResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<EncryptedAppTicketResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<EncryptedAppTicketResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUser + 54, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUser + 54, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GetAuthSessionTicketResponse_t : ICallbackData
	{
		internal uint AuthTicket; // m_hAuthTicket HAuthTicket
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GetAuthSessionTicketResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUser + 63;
		internal static GetAuthSessionTicketResponse_t Fill( IntPtr p ) => ((GetAuthSessionTicketResponse_t)Marshal.PtrToStructure( p, typeof(GetAuthSessionTicketResponse_t) ) );
		
		static Action<GetAuthSessionTicketResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GetAuthSessionTicketResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GetAuthSessionTicketResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUser + 63, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUser + 63, false );
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
		public int CallbackId => CallbackIdentifiers.SteamUser + 64;
		internal static GameWebCallback_t Fill( IntPtr p ) => ((GameWebCallback_t)Marshal.PtrToStructure( p, typeof(GameWebCallback_t) ) );
		
		static Action<GameWebCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameWebCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameWebCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUser + 64, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUser + 64, false );
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
		public int CallbackId => CallbackIdentifiers.SteamUser + 65;
		internal static StoreAuthURLResponse_t Fill( IntPtr p ) => ((StoreAuthURLResponse_t)Marshal.PtrToStructure( p, typeof(StoreAuthURLResponse_t) ) );
		
		static Action<StoreAuthURLResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<StoreAuthURLResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<StoreAuthURLResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUser + 65, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUser + 65, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MarketEligibilityResponse_t : ICallbackData
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Allowed; // m_bAllowed _Bool
		internal MarketNotAllowedReasonFlags NotAllowedReason; // m_eNotAllowedReason enum EMarketNotAllowedReasonFlags
		internal uint TAllowedAtTime; // m_rtAllowedAtTime RTime32
		internal int CdaySteamGuardRequiredDays; // m_cdaySteamGuardRequiredDays int
		internal int CdayNewDeviceCooldown; // m_cdayNewDeviceCooldown int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MarketEligibilityResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUser + 66;
		internal static MarketEligibilityResponse_t Fill( IntPtr p ) => ((MarketEligibilityResponse_t)Marshal.PtrToStructure( p, typeof(MarketEligibilityResponse_t) ) );
		
		static Action<MarketEligibilityResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MarketEligibilityResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MarketEligibilityResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUser + 66, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUser + 66, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct FriendGameInfo_t
	{
		internal GameId GameID; // m_gameID class CGameID
		internal uint GameIP; // m_unGameIP uint32
		internal ushort GamePort; // m_usGamePort uint16
		internal ushort QueryPort; // m_usQueryPort uint16
		internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
		
		#region Marshalling
		internal static FriendGameInfo_t Fill( IntPtr p ) => ((FriendGameInfo_t)(FriendGameInfo_t) Marshal.PtrToStructure( p, typeof(FriendGameInfo_t) ) );
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct FriendSessionStateInfo_t
	{
		internal uint IOnlineSessionInstances; // m_uiOnlineSessionInstances uint32
		internal byte IPublishedToFriendsSessionInstance; // m_uiPublishedToFriendsSessionInstance uint8
		
		#region Marshalling
		internal static FriendSessionStateInfo_t Fill( IntPtr p ) => ((FriendSessionStateInfo_t)(FriendSessionStateInfo_t) Marshal.PtrToStructure( p, typeof(FriendSessionStateInfo_t) ) );
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
		public int CallbackId => CallbackIdentifiers.SteamFriends + 4;
		internal static FriendStateChange_t Fill( IntPtr p ) => ((FriendStateChange_t)Marshal.PtrToStructure( p, typeof(FriendStateChange_t) ) );
		
		static Action<FriendStateChange_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FriendStateChange_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FriendStateChange_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamFriends + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamFriends + 4, false );
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
		public int CallbackId => CallbackIdentifiers.SteamFriends + 31;
		internal static GameOverlayActivated_t Fill( IntPtr p ) => ((GameOverlayActivated_t)Marshal.PtrToStructure( p, typeof(GameOverlayActivated_t) ) );
		
		static Action<GameOverlayActivated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameOverlayActivated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameOverlayActivated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamFriends + 31, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamFriends + 31, false );
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
		public int CallbackId => CallbackIdentifiers.SteamFriends + 32;
		internal static GameServerChangeRequested_t Fill( IntPtr p ) => ((GameServerChangeRequested_t)Marshal.PtrToStructure( p, typeof(GameServerChangeRequested_t) ) );
		
		static Action<GameServerChangeRequested_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameServerChangeRequested_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameServerChangeRequested_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamFriends + 32, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamFriends + 32, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GameLobbyJoinRequested_t : ICallbackData
	{
		internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
		internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameLobbyJoinRequested_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamFriends + 33;
		internal static GameLobbyJoinRequested_t Fill( IntPtr p ) => ((GameLobbyJoinRequested_t)Marshal.PtrToStructure( p, typeof(GameLobbyJoinRequested_t) ) );
		
		static Action<GameLobbyJoinRequested_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameLobbyJoinRequested_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameLobbyJoinRequested_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamFriends + 33, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamFriends + 33, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct AvatarImageLoaded_t : ICallbackData
	{
		internal ulong SteamID; // m_steamID class CSteamID
		internal int Image; // m_iImage int
		internal int Wide; // m_iWide int
		internal int Tall; // m_iTall int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(AvatarImageLoaded_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamFriends + 34;
		internal static AvatarImageLoaded_t Fill( IntPtr p ) => ((AvatarImageLoaded_t)Marshal.PtrToStructure( p, typeof(AvatarImageLoaded_t) ) );
		
		static Action<AvatarImageLoaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<AvatarImageLoaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<AvatarImageLoaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamFriends + 34, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamFriends + 34, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct ClanOfficerListResponse_t : ICallbackData
	{
		internal ulong SteamIDClan; // m_steamIDClan class CSteamID
		internal int COfficers; // m_cOfficers int
		internal byte Success; // m_bSuccess uint8
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ClanOfficerListResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamFriends + 35;
		internal static ClanOfficerListResponse_t Fill( IntPtr p ) => ((ClanOfficerListResponse_t)Marshal.PtrToStructure( p, typeof(ClanOfficerListResponse_t) ) );
		
		static Action<ClanOfficerListResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ClanOfficerListResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ClanOfficerListResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamFriends + 35, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamFriends + 35, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct FriendRichPresenceUpdate_t : ICallbackData
	{
		internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendRichPresenceUpdate_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamFriends + 36;
		internal static FriendRichPresenceUpdate_t Fill( IntPtr p ) => ((FriendRichPresenceUpdate_t)Marshal.PtrToStructure( p, typeof(FriendRichPresenceUpdate_t) ) );
		
		static Action<FriendRichPresenceUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FriendRichPresenceUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FriendRichPresenceUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamFriends + 36, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamFriends + 36, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GameRichPresenceJoinRequested_t : ICallbackData
	{
		internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		internal string ConnectUTF8() => System.Text.Encoding.UTF8.GetString( Connect, 0, System.Array.IndexOf<byte>( Connect, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // byte[] m_rgchConnect
		internal byte[] Connect; // m_rgchConnect char [256]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameRichPresenceJoinRequested_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamFriends + 37;
		internal static GameRichPresenceJoinRequested_t Fill( IntPtr p ) => ((GameRichPresenceJoinRequested_t)Marshal.PtrToStructure( p, typeof(GameRichPresenceJoinRequested_t) ) );
		
		static Action<GameRichPresenceJoinRequested_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameRichPresenceJoinRequested_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameRichPresenceJoinRequested_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamFriends + 37, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamFriends + 37, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GameConnectedClanChatMsg_t : ICallbackData
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		internal int MessageID; // m_iMessageID int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameConnectedClanChatMsg_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamFriends + 38;
		internal static GameConnectedClanChatMsg_t Fill( IntPtr p ) => ((GameConnectedClanChatMsg_t)Marshal.PtrToStructure( p, typeof(GameConnectedClanChatMsg_t) ) );
		
		static Action<GameConnectedClanChatMsg_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameConnectedClanChatMsg_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameConnectedClanChatMsg_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamFriends + 38, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamFriends + 38, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GameConnectedChatJoin_t : ICallbackData
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameConnectedChatJoin_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamFriends + 39;
		internal static GameConnectedChatJoin_t Fill( IntPtr p ) => ((GameConnectedChatJoin_t)Marshal.PtrToStructure( p, typeof(GameConnectedChatJoin_t) ) );
		
		static Action<GameConnectedChatJoin_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameConnectedChatJoin_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameConnectedChatJoin_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamFriends + 39, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamFriends + 39, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GameConnectedChatLeave_t : ICallbackData
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool Kicked; // m_bKicked _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool Dropped; // m_bDropped _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameConnectedChatLeave_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamFriends + 40;
		internal static GameConnectedChatLeave_t Fill( IntPtr p ) => ((GameConnectedChatLeave_t)Marshal.PtrToStructure( p, typeof(GameConnectedChatLeave_t) ) );
		
		static Action<GameConnectedChatLeave_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameConnectedChatLeave_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameConnectedChatLeave_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamFriends + 40, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamFriends + 40, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct DownloadClanActivityCountsResult_t : ICallbackData
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Success; // m_bSuccess _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(DownloadClanActivityCountsResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamFriends + 41;
		internal static DownloadClanActivityCountsResult_t Fill( IntPtr p ) => ((DownloadClanActivityCountsResult_t)Marshal.PtrToStructure( p, typeof(DownloadClanActivityCountsResult_t) ) );
		
		static Action<DownloadClanActivityCountsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<DownloadClanActivityCountsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<DownloadClanActivityCountsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamFriends + 41, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamFriends + 41, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct JoinClanChatRoomCompletionResult_t : ICallbackData
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		internal RoomEnter ChatRoomEnterResponse; // m_eChatRoomEnterResponse enum EChatRoomEnterResponse
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(JoinClanChatRoomCompletionResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamFriends + 42;
		internal static JoinClanChatRoomCompletionResult_t Fill( IntPtr p ) => ((JoinClanChatRoomCompletionResult_t)Marshal.PtrToStructure( p, typeof(JoinClanChatRoomCompletionResult_t) ) );
		
		static Action<JoinClanChatRoomCompletionResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<JoinClanChatRoomCompletionResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<JoinClanChatRoomCompletionResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamFriends + 42, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamFriends + 42, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GameConnectedFriendChatMsg_t : ICallbackData
	{
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		internal int MessageID; // m_iMessageID int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameConnectedFriendChatMsg_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamFriends + 43;
		internal static GameConnectedFriendChatMsg_t Fill( IntPtr p ) => ((GameConnectedFriendChatMsg_t)Marshal.PtrToStructure( p, typeof(GameConnectedFriendChatMsg_t) ) );
		
		static Action<GameConnectedFriendChatMsg_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameConnectedFriendChatMsg_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameConnectedFriendChatMsg_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamFriends + 43, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamFriends + 43, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct FriendsGetFollowerCount_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamID; // m_steamID class CSteamID
		internal int Count; // m_nCount int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendsGetFollowerCount_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamFriends + 44;
		internal static FriendsGetFollowerCount_t Fill( IntPtr p ) => ((FriendsGetFollowerCount_t)Marshal.PtrToStructure( p, typeof(FriendsGetFollowerCount_t) ) );
		
		static Action<FriendsGetFollowerCount_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FriendsGetFollowerCount_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FriendsGetFollowerCount_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamFriends + 44, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamFriends + 44, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct FriendsIsFollowing_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamID; // m_steamID class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool IsFollowing; // m_bIsFollowing _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendsIsFollowing_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamFriends + 45;
		internal static FriendsIsFollowing_t Fill( IntPtr p ) => ((FriendsIsFollowing_t)Marshal.PtrToStructure( p, typeof(FriendsIsFollowing_t) ) );
		
		static Action<FriendsIsFollowing_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FriendsIsFollowing_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FriendsIsFollowing_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamFriends + 45, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamFriends + 45, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct FriendsEnumerateFollowingList_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal ulong[] GSteamID; // m_rgSteamID class CSteamID [50]
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendsEnumerateFollowingList_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamFriends + 46;
		internal static FriendsEnumerateFollowingList_t Fill( IntPtr p ) => ((FriendsEnumerateFollowingList_t)Marshal.PtrToStructure( p, typeof(FriendsEnumerateFollowingList_t) ) );
		
		static Action<FriendsEnumerateFollowingList_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FriendsEnumerateFollowingList_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FriendsEnumerateFollowingList_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamFriends + 46, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamFriends + 46, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SetPersonaNameResponse_t : ICallbackData
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Success; // m_bSuccess _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool LocalSuccess; // m_bLocalSuccess _Bool
		internal Result Result; // m_result enum EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SetPersonaNameResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamFriends + 47;
		internal static SetPersonaNameResponse_t Fill( IntPtr p ) => ((SetPersonaNameResponse_t)Marshal.PtrToStructure( p, typeof(SetPersonaNameResponse_t) ) );
		
		static Action<SetPersonaNameResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SetPersonaNameResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SetPersonaNameResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamFriends + 47, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamFriends + 47, false );
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
		public int CallbackId => CallbackIdentifiers.SteamUtils + 2;
		internal static LowBatteryPower_t Fill( IntPtr p ) => ((LowBatteryPower_t)Marshal.PtrToStructure( p, typeof(LowBatteryPower_t) ) );
		
		static Action<LowBatteryPower_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LowBatteryPower_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LowBatteryPower_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUtils + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUtils + 2, false );
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
		public int CallbackId => CallbackIdentifiers.SteamUtils + 3;
		internal static SteamAPICallCompleted_t Fill( IntPtr p ) => ((SteamAPICallCompleted_t)Marshal.PtrToStructure( p, typeof(SteamAPICallCompleted_t) ) );
		
		static Action<SteamAPICallCompleted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamAPICallCompleted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamAPICallCompleted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUtils + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUtils + 3, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct CheckFileSignature_t : ICallbackData
	{
		internal CheckFileSignature CheckFileSignature; // m_eCheckFileSignature enum ECheckFileSignature
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(CheckFileSignature_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUtils + 5;
		internal static CheckFileSignature_t Fill( IntPtr p ) => ((CheckFileSignature_t)Marshal.PtrToStructure( p, typeof(CheckFileSignature_t) ) );
		
		static Action<CheckFileSignature_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<CheckFileSignature_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<CheckFileSignature_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUtils + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUtils + 5, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GamepadTextInputDismissed_t : ICallbackData
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Submitted; // m_bSubmitted _Bool
		internal uint SubmittedText; // m_unSubmittedText uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GamepadTextInputDismissed_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUtils + 14;
		internal static GamepadTextInputDismissed_t Fill( IntPtr p ) => ((GamepadTextInputDismissed_t)Marshal.PtrToStructure( p, typeof(GamepadTextInputDismissed_t) ) );
		
		static Action<GamepadTextInputDismissed_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GamepadTextInputDismissed_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GamepadTextInputDismissed_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUtils + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUtils + 14, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct servernetadr_t
	{
		internal ushort ConnectionPort; // m_usConnectionPort uint16
		internal ushort QueryPort; // m_usQueryPort uint16
		internal uint IP; // m_unIP uint32
		
		#region Marshalling
		internal static servernetadr_t Fill( IntPtr p ) => ((servernetadr_t)(servernetadr_t) Marshal.PtrToStructure( p, typeof(servernetadr_t) ) );
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct gameserveritem_t
	{
		internal servernetadr_t NetAdr; // m_NetAdr class servernetadr_t
		internal int Ping; // m_nPing int
		[MarshalAs(UnmanagedType.I1)]
		internal bool HadSuccessfulResponse; // m_bHadSuccessfulResponse _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool DoNotRefresh; // m_bDoNotRefresh _Bool
		internal string GameDirUTF8() => System.Text.Encoding.UTF8.GetString( GameDir, 0, System.Array.IndexOf<byte>( GameDir, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)] // byte[] m_szGameDir
		internal byte[] GameDir; // m_szGameDir char [32]
		internal string MapUTF8() => System.Text.Encoding.UTF8.GetString( Map, 0, System.Array.IndexOf<byte>( Map, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)] // byte[] m_szMap
		internal byte[] Map; // m_szMap char [32]
		internal string GameDescriptionUTF8() => System.Text.Encoding.UTF8.GetString( GameDescription, 0, System.Array.IndexOf<byte>( GameDescription, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)] // byte[] m_szGameDescription
		internal byte[] GameDescription; // m_szGameDescription char [64]
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
		internal string ServerNameUTF8() => System.Text.Encoding.UTF8.GetString( ServerName, 0, System.Array.IndexOf<byte>( ServerName, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)] // byte[] m_szServerName
		internal byte[] ServerName; // m_szServerName char [64]
		internal string GameTagsUTF8() => System.Text.Encoding.UTF8.GetString( GameTags, 0, System.Array.IndexOf<byte>( GameTags, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)] // byte[] m_szGameTags
		internal byte[] GameTags; // m_szGameTags char [128]
		internal ulong SteamID; // m_steamID class CSteamID
		
		#region Marshalling
		internal static gameserveritem_t Fill( IntPtr p ) => ((gameserveritem_t)(gameserveritem_t) Marshal.PtrToStructure( p, typeof(gameserveritem_t) ) );
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamPartyBeaconLocation_t
	{
		internal SteamPartyBeaconLocationType Type; // m_eType enum ESteamPartyBeaconLocationType
		internal ulong LocationID; // m_ulLocationID uint64
		
		#region Marshalling
		internal static SteamPartyBeaconLocation_t Fill( IntPtr p ) => ((SteamPartyBeaconLocation_t)(SteamPartyBeaconLocation_t) Marshal.PtrToStructure( p, typeof(SteamPartyBeaconLocation_t) ) );
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
		internal bool Add; // m_bAdd _Bool
		internal uint AccountId; // m_unAccountId AccountID_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FavoritesListChanged_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamMatchmaking + 2;
		internal static FavoritesListChanged_t Fill( IntPtr p ) => ((FavoritesListChanged_t)Marshal.PtrToStructure( p, typeof(FavoritesListChanged_t) ) );
		
		static Action<FavoritesListChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FavoritesListChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FavoritesListChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMatchmaking + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMatchmaking + 2, false );
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
		public int CallbackId => CallbackIdentifiers.SteamMatchmaking + 3;
		internal static LobbyInvite_t Fill( IntPtr p ) => ((LobbyInvite_t)Marshal.PtrToStructure( p, typeof(LobbyInvite_t) ) );
		
		static Action<LobbyInvite_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyInvite_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyInvite_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMatchmaking + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMatchmaking + 3, false );
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
		internal bool Locked; // m_bLocked _Bool
		internal uint EChatRoomEnterResponse; // m_EChatRoomEnterResponse uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyEnter_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamMatchmaking + 4;
		internal static LobbyEnter_t Fill( IntPtr p ) => ((LobbyEnter_t)Marshal.PtrToStructure( p, typeof(LobbyEnter_t) ) );
		
		static Action<LobbyEnter_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyEnter_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyEnter_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMatchmaking + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMatchmaking + 4, false );
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
		public int CallbackId => CallbackIdentifiers.SteamMatchmaking + 5;
		internal static LobbyDataUpdate_t Fill( IntPtr p ) => ((LobbyDataUpdate_t)Marshal.PtrToStructure( p, typeof(LobbyDataUpdate_t) ) );
		
		static Action<LobbyDataUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyDataUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyDataUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMatchmaking + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMatchmaking + 5, false );
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
		public int CallbackId => CallbackIdentifiers.SteamMatchmaking + 6;
		internal static LobbyChatUpdate_t Fill( IntPtr p ) => ((LobbyChatUpdate_t)Marshal.PtrToStructure( p, typeof(LobbyChatUpdate_t) ) );
		
		static Action<LobbyChatUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyChatUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyChatUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMatchmaking + 6, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMatchmaking + 6, false );
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
		public int CallbackId => CallbackIdentifiers.SteamMatchmaking + 7;
		internal static LobbyChatMsg_t Fill( IntPtr p ) => ((LobbyChatMsg_t)Marshal.PtrToStructure( p, typeof(LobbyChatMsg_t) ) );
		
		static Action<LobbyChatMsg_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyChatMsg_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyChatMsg_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMatchmaking + 7, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMatchmaking + 7, false );
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
		public int CallbackId => CallbackIdentifiers.SteamMatchmaking + 9;
		internal static LobbyGameCreated_t Fill( IntPtr p ) => ((LobbyGameCreated_t)Marshal.PtrToStructure( p, typeof(LobbyGameCreated_t) ) );
		
		static Action<LobbyGameCreated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyGameCreated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyGameCreated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMatchmaking + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMatchmaking + 9, false );
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
		public int CallbackId => CallbackIdentifiers.SteamMatchmaking + 10;
		internal static LobbyMatchList_t Fill( IntPtr p ) => ((LobbyMatchList_t)Marshal.PtrToStructure( p, typeof(LobbyMatchList_t) ) );
		
		static Action<LobbyMatchList_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyMatchList_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyMatchList_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMatchmaking + 10, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMatchmaking + 10, false );
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
		public int CallbackId => CallbackIdentifiers.SteamMatchmaking + 12;
		internal static LobbyKicked_t Fill( IntPtr p ) => ((LobbyKicked_t)Marshal.PtrToStructure( p, typeof(LobbyKicked_t) ) );
		
		static Action<LobbyKicked_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyKicked_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyKicked_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMatchmaking + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMatchmaking + 12, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyCreated_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyCreated_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamMatchmaking + 13;
		internal static LobbyCreated_t Fill( IntPtr p ) => ((LobbyCreated_t)Marshal.PtrToStructure( p, typeof(LobbyCreated_t) ) );
		
		static Action<LobbyCreated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyCreated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyCreated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMatchmaking + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMatchmaking + 13, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct PSNGameBootInviteResult_t : ICallbackData
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool GameBootInviteExists; // m_bGameBootInviteExists _Bool
		internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(PSNGameBootInviteResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamMatchmaking + 15;
		internal static PSNGameBootInviteResult_t Fill( IntPtr p ) => ((PSNGameBootInviteResult_t)Marshal.PtrToStructure( p, typeof(PSNGameBootInviteResult_t) ) );
		
		static Action<PSNGameBootInviteResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<PSNGameBootInviteResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<PSNGameBootInviteResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMatchmaking + 15, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMatchmaking + 15, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct FavoritesListAccountsUpdated_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FavoritesListAccountsUpdated_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamMatchmaking + 16;
		internal static FavoritesListAccountsUpdated_t Fill( IntPtr p ) => ((FavoritesListAccountsUpdated_t)Marshal.PtrToStructure( p, typeof(FavoritesListAccountsUpdated_t) ) );
		
		static Action<FavoritesListAccountsUpdated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FavoritesListAccountsUpdated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FavoritesListAccountsUpdated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMatchmaking + 16, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMatchmaking + 16, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct SearchForGameProgressCallback_t : ICallbackData
	{
		internal ulong LSearchID; // m_ullSearchID uint64
		internal Result Result; // m_eResult enum EResult
		internal ulong LobbyID; // m_lobbyID class CSteamID
		internal ulong SteamIDEndedSearch; // m_steamIDEndedSearch class CSteamID
		internal int SecondsRemainingEstimate; // m_nSecondsRemainingEstimate int32
		internal int CPlayersSearching; // m_cPlayersSearching int32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SearchForGameProgressCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameSearch + 1;
		internal static SearchForGameProgressCallback_t Fill( IntPtr p ) => ((SearchForGameProgressCallback_t)Marshal.PtrToStructure( p, typeof(SearchForGameProgressCallback_t) ) );
		
		static Action<SearchForGameProgressCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SearchForGameProgressCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SearchForGameProgressCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameSearch + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameSearch + 1, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct SearchForGameResultCallback_t : ICallbackData
	{
		internal ulong LSearchID; // m_ullSearchID uint64
		internal Result Result; // m_eResult enum EResult
		internal int CountPlayersInGame; // m_nCountPlayersInGame int32
		internal int CountAcceptedGame; // m_nCountAcceptedGame int32
		internal ulong SteamIDHost; // m_steamIDHost class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool FinalCallback; // m_bFinalCallback _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SearchForGameResultCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameSearch + 2;
		internal static SearchForGameResultCallback_t Fill( IntPtr p ) => ((SearchForGameResultCallback_t)Marshal.PtrToStructure( p, typeof(SearchForGameResultCallback_t) ) );
		
		static Action<SearchForGameResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SearchForGameResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SearchForGameResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameSearch + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameSearch + 2, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RequestPlayersForGameProgressCallback_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong LSearchID; // m_ullSearchID uint64
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RequestPlayersForGameProgressCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameSearch + 11;
		internal static RequestPlayersForGameProgressCallback_t Fill( IntPtr p ) => ((RequestPlayersForGameProgressCallback_t)Marshal.PtrToStructure( p, typeof(RequestPlayersForGameProgressCallback_t) ) );
		
		static Action<RequestPlayersForGameProgressCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RequestPlayersForGameProgressCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RequestPlayersForGameProgressCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameSearch + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameSearch + 11, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct RequestPlayersForGameResultCallback_t : ICallbackData
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
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RequestPlayersForGameResultCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameSearch + 12;
		internal static RequestPlayersForGameResultCallback_t Fill( IntPtr p ) => ((RequestPlayersForGameResultCallback_t)Marshal.PtrToStructure( p, typeof(RequestPlayersForGameResultCallback_t) ) );
		
		static Action<RequestPlayersForGameResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RequestPlayersForGameResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RequestPlayersForGameResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameSearch + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameSearch + 12, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RequestPlayersForGameFinalResultCallback_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong LSearchID; // m_ullSearchID uint64
		internal ulong LUniqueGameID; // m_ullUniqueGameID uint64
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RequestPlayersForGameFinalResultCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameSearch + 13;
		internal static RequestPlayersForGameFinalResultCallback_t Fill( IntPtr p ) => ((RequestPlayersForGameFinalResultCallback_t)Marshal.PtrToStructure( p, typeof(RequestPlayersForGameFinalResultCallback_t) ) );
		
		static Action<RequestPlayersForGameFinalResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RequestPlayersForGameFinalResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RequestPlayersForGameFinalResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameSearch + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameSearch + 13, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct SubmitPlayerResultResultCallback_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong UllUniqueGameID; // ullUniqueGameID uint64
		internal ulong SteamIDPlayer; // steamIDPlayer class CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SubmitPlayerResultResultCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameSearch + 14;
		internal static SubmitPlayerResultResultCallback_t Fill( IntPtr p ) => ((SubmitPlayerResultResultCallback_t)Marshal.PtrToStructure( p, typeof(SubmitPlayerResultResultCallback_t) ) );
		
		static Action<SubmitPlayerResultResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SubmitPlayerResultResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SubmitPlayerResultResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameSearch + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameSearch + 14, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct EndGameResultCallback_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong UllUniqueGameID; // ullUniqueGameID uint64
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(EndGameResultCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameSearch + 15;
		internal static EndGameResultCallback_t Fill( IntPtr p ) => ((EndGameResultCallback_t)Marshal.PtrToStructure( p, typeof(EndGameResultCallback_t) ) );
		
		static Action<EndGameResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<EndGameResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<EndGameResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameSearch + 15, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameSearch + 15, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct JoinPartyCallback_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
		internal ulong SteamIDBeaconOwner; // m_SteamIDBeaconOwner class CSteamID
		internal string ConnectStringUTF8() => System.Text.Encoding.UTF8.GetString( ConnectString, 0, System.Array.IndexOf<byte>( ConnectString, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // byte[] m_rgchConnectString
		internal byte[] ConnectString; // m_rgchConnectString char [256]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(JoinPartyCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamParties + 1;
		internal static JoinPartyCallback_t Fill( IntPtr p ) => ((JoinPartyCallback_t)Marshal.PtrToStructure( p, typeof(JoinPartyCallback_t) ) );
		
		static Action<JoinPartyCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<JoinPartyCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<JoinPartyCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamParties + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamParties + 1, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct CreateBeaconCallback_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(CreateBeaconCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamParties + 2;
		internal static CreateBeaconCallback_t Fill( IntPtr p ) => ((CreateBeaconCallback_t)Marshal.PtrToStructure( p, typeof(CreateBeaconCallback_t) ) );
		
		static Action<CreateBeaconCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<CreateBeaconCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<CreateBeaconCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamParties + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamParties + 2, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct ReservationNotificationCallback_t : ICallbackData
	{
		internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
		internal ulong SteamIDJoiner; // m_steamIDJoiner class CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ReservationNotificationCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamParties + 3;
		internal static ReservationNotificationCallback_t Fill( IntPtr p ) => ((ReservationNotificationCallback_t)Marshal.PtrToStructure( p, typeof(ReservationNotificationCallback_t) ) );
		
		static Action<ReservationNotificationCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ReservationNotificationCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ReservationNotificationCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamParties + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamParties + 3, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct ChangeNumOpenSlotsCallback_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ChangeNumOpenSlotsCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamParties + 4;
		internal static ChangeNumOpenSlotsCallback_t Fill( IntPtr p ) => ((ChangeNumOpenSlotsCallback_t)Marshal.PtrToStructure( p, typeof(ChangeNumOpenSlotsCallback_t) ) );
		
		static Action<ChangeNumOpenSlotsCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ChangeNumOpenSlotsCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ChangeNumOpenSlotsCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamParties + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamParties + 4, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamParamStringArray_t
	{
		internal IntPtr Strings; // m_ppStrings const char **
		internal int NumStrings; // m_nNumStrings int32
		
		#region Marshalling
		internal static SteamParamStringArray_t Fill( IntPtr p ) => ((SteamParamStringArray_t)(SteamParamStringArray_t) Marshal.PtrToStructure( p, typeof(SteamParamStringArray_t) ) );
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageAppSyncedClient_t : ICallbackData
	{
		internal AppId AppID; // m_nAppID AppId_t
		internal Result Result; // m_eResult enum EResult
		internal int NumDownloads; // m_unNumDownloads int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageAppSyncedClient_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 1;
		internal static RemoteStorageAppSyncedClient_t Fill( IntPtr p ) => ((RemoteStorageAppSyncedClient_t)Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncedClient_t) ) );
		
		static Action<RemoteStorageAppSyncedClient_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageAppSyncedClient_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageAppSyncedClient_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 1, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageAppSyncedServer_t : ICallbackData
	{
		internal AppId AppID; // m_nAppID AppId_t
		internal Result Result; // m_eResult enum EResult
		internal int NumUploads; // m_unNumUploads int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageAppSyncedServer_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 2;
		internal static RemoteStorageAppSyncedServer_t Fill( IntPtr p ) => ((RemoteStorageAppSyncedServer_t)Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncedServer_t) ) );
		
		static Action<RemoteStorageAppSyncedServer_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageAppSyncedServer_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageAppSyncedServer_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 2, false );
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
		internal bool Uploading; // m_bUploading _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageAppSyncProgress_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 3;
		internal static RemoteStorageAppSyncProgress_t Fill( IntPtr p ) => ((RemoteStorageAppSyncProgress_t)Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncProgress_t) ) );
		
		static Action<RemoteStorageAppSyncProgress_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageAppSyncProgress_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageAppSyncProgress_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 3, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageAppSyncStatusCheck_t : ICallbackData
	{
		internal AppId AppID; // m_nAppID AppId_t
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageAppSyncStatusCheck_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 5;
		internal static RemoteStorageAppSyncStatusCheck_t Fill( IntPtr p ) => ((RemoteStorageAppSyncStatusCheck_t)Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncStatusCheck_t) ) );
		
		static Action<RemoteStorageAppSyncStatusCheck_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageAppSyncStatusCheck_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageAppSyncStatusCheck_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 5, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageFileShareResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong File; // m_hFile UGCHandle_t
		internal string FilenameUTF8() => System.Text.Encoding.UTF8.GetString( Filename, 0, System.Array.IndexOf<byte>( Filename, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)] // byte[] m_rgchFilename
		internal byte[] Filename; // m_rgchFilename char [260]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageFileShareResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 7;
		internal static RemoteStorageFileShareResult_t Fill( IntPtr p ) => ((RemoteStorageFileShareResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageFileShareResult_t) ) );
		
		static Action<RemoteStorageFileShareResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageFileShareResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageFileShareResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 7, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 7, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStoragePublishFileResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStoragePublishFileResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 9;
		internal static RemoteStoragePublishFileResult_t Fill( IntPtr p ) => ((RemoteStoragePublishFileResult_t)Marshal.PtrToStructure( p, typeof(RemoteStoragePublishFileResult_t) ) );
		
		static Action<RemoteStoragePublishFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStoragePublishFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 9, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageDeletePublishedFileResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageDeletePublishedFileResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 11;
		internal static RemoteStorageDeletePublishedFileResult_t Fill( IntPtr p ) => ((RemoteStorageDeletePublishedFileResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageDeletePublishedFileResult_t) ) );
		
		static Action<RemoteStorageDeletePublishedFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageDeletePublishedFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageDeletePublishedFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 11, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageEnumerateUserPublishedFilesResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageEnumerateUserPublishedFilesResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 12;
		internal static RemoteStorageEnumerateUserPublishedFilesResult_t Fill( IntPtr p ) => ((RemoteStorageEnumerateUserPublishedFilesResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserPublishedFilesResult_t) ) );
		
		static Action<RemoteStorageEnumerateUserPublishedFilesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageEnumerateUserPublishedFilesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumerateUserPublishedFilesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 12, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageSubscribePublishedFileResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageSubscribePublishedFileResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 13;
		internal static RemoteStorageSubscribePublishedFileResult_t Fill( IntPtr p ) => ((RemoteStorageSubscribePublishedFileResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageSubscribePublishedFileResult_t) ) );
		
		static Action<RemoteStorageSubscribePublishedFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageSubscribePublishedFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageSubscribePublishedFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 13, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageEnumerateUserSubscribedFilesResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
		internal uint[] GRTimeSubscribed; // m_rgRTimeSubscribed uint32 [50]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 14;
		internal static RemoteStorageEnumerateUserSubscribedFilesResult_t Fill( IntPtr p ) => ((RemoteStorageEnumerateUserSubscribedFilesResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t) ) );
		
		static Action<RemoteStorageEnumerateUserSubscribedFilesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageEnumerateUserSubscribedFilesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumerateUserSubscribedFilesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 14, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageUnsubscribePublishedFileResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageUnsubscribePublishedFileResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 15;
		internal static RemoteStorageUnsubscribePublishedFileResult_t Fill( IntPtr p ) => ((RemoteStorageUnsubscribePublishedFileResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageUnsubscribePublishedFileResult_t) ) );
		
		static Action<RemoteStorageUnsubscribePublishedFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageUnsubscribePublishedFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageUnsubscribePublishedFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 15, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 15, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageUpdatePublishedFileResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageUpdatePublishedFileResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 16;
		internal static RemoteStorageUpdatePublishedFileResult_t Fill( IntPtr p ) => ((RemoteStorageUpdatePublishedFileResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageUpdatePublishedFileResult_t) ) );
		
		static Action<RemoteStorageUpdatePublishedFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageUpdatePublishedFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageUpdatePublishedFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 16, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 16, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageDownloadUGCResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
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
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 17;
		internal static RemoteStorageDownloadUGCResult_t Fill( IntPtr p ) => ((RemoteStorageDownloadUGCResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageDownloadUGCResult_t) ) );
		
		static Action<RemoteStorageDownloadUGCResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageDownloadUGCResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageDownloadUGCResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 17, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 17, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageGetPublishedFileDetailsResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
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
		internal RemoteStoragePublishedFileVisibility Visibility; // m_eVisibility enum ERemoteStoragePublishedFileVisibility
		[MarshalAs(UnmanagedType.I1)]
		internal bool Banned; // m_bBanned _Bool
		internal string TagsUTF8() => System.Text.Encoding.UTF8.GetString( Tags, 0, System.Array.IndexOf<byte>( Tags, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1025)] // byte[] m_rgchTags
		internal byte[] Tags; // m_rgchTags char [1025]
		[MarshalAs(UnmanagedType.I1)]
		internal bool TagsTruncated; // m_bTagsTruncated _Bool
		internal string PchFileNameUTF8() => System.Text.Encoding.UTF8.GetString( PchFileName, 0, System.Array.IndexOf<byte>( PchFileName, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)] // byte[] m_pchFileName
		internal byte[] PchFileName; // m_pchFileName char [260]
		internal int FileSize; // m_nFileSize int32
		internal int PreviewFileSize; // m_nPreviewFileSize int32
		internal string URLUTF8() => System.Text.Encoding.UTF8.GetString( URL, 0, System.Array.IndexOf<byte>( URL, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // byte[] m_rgchURL
		internal byte[] URL; // m_rgchURL char [256]
		internal WorkshopFileType FileType; // m_eFileType enum EWorkshopFileType
		[MarshalAs(UnmanagedType.I1)]
		internal bool AcceptedForUse; // m_bAcceptedForUse _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageGetPublishedFileDetailsResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 18;
		internal static RemoteStorageGetPublishedFileDetailsResult_t Fill( IntPtr p ) => ((RemoteStorageGetPublishedFileDetailsResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageGetPublishedFileDetailsResult_t) ) );
		
		static Action<RemoteStorageGetPublishedFileDetailsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageGetPublishedFileDetailsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageGetPublishedFileDetailsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 18, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 18, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageEnumerateWorkshopFilesResult_t : ICallbackData
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
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageEnumerateWorkshopFilesResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 19;
		internal static RemoteStorageEnumerateWorkshopFilesResult_t Fill( IntPtr p ) => ((RemoteStorageEnumerateWorkshopFilesResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateWorkshopFilesResult_t) ) );
		
		static Action<RemoteStorageEnumerateWorkshopFilesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageEnumerateWorkshopFilesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumerateWorkshopFilesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 19, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 19, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageGetPublishedItemVoteDetailsResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_unPublishedFileId PublishedFileId_t
		internal int VotesFor; // m_nVotesFor int32
		internal int VotesAgainst; // m_nVotesAgainst int32
		internal int Reports; // m_nReports int32
		internal float FScore; // m_fScore float
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageGetPublishedItemVoteDetailsResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 20;
		internal static RemoteStorageGetPublishedItemVoteDetailsResult_t Fill( IntPtr p ) => ((RemoteStorageGetPublishedItemVoteDetailsResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageGetPublishedItemVoteDetailsResult_t) ) );
		
		static Action<RemoteStorageGetPublishedItemVoteDetailsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageGetPublishedItemVoteDetailsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageGetPublishedItemVoteDetailsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 20, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 20, false );
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
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 21;
		internal static RemoteStoragePublishedFileSubscribed_t Fill( IntPtr p ) => ((RemoteStoragePublishedFileSubscribed_t)Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileSubscribed_t) ) );
		
		static Action<RemoteStoragePublishedFileSubscribed_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStoragePublishedFileSubscribed_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishedFileSubscribed_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 21, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 21, false );
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
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 22;
		internal static RemoteStoragePublishedFileUnsubscribed_t Fill( IntPtr p ) => ((RemoteStoragePublishedFileUnsubscribed_t)Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileUnsubscribed_t) ) );
		
		static Action<RemoteStoragePublishedFileUnsubscribed_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStoragePublishedFileUnsubscribed_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishedFileUnsubscribed_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 22, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 22, false );
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
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 23;
		internal static RemoteStoragePublishedFileDeleted_t Fill( IntPtr p ) => ((RemoteStoragePublishedFileDeleted_t)Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileDeleted_t) ) );
		
		static Action<RemoteStoragePublishedFileDeleted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStoragePublishedFileDeleted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishedFileDeleted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 23, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 23, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageUpdateUserPublishedItemVoteResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageUpdateUserPublishedItemVoteResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 24;
		internal static RemoteStorageUpdateUserPublishedItemVoteResult_t Fill( IntPtr p ) => ((RemoteStorageUpdateUserPublishedItemVoteResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageUpdateUserPublishedItemVoteResult_t) ) );
		
		static Action<RemoteStorageUpdateUserPublishedItemVoteResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageUpdateUserPublishedItemVoteResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageUpdateUserPublishedItemVoteResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 24, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 24, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageUserVoteDetails_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal WorkshopVote Vote; // m_eVote enum EWorkshopVote
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageUserVoteDetails_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 25;
		internal static RemoteStorageUserVoteDetails_t Fill( IntPtr p ) => ((RemoteStorageUserVoteDetails_t)Marshal.PtrToStructure( p, typeof(RemoteStorageUserVoteDetails_t) ) );
		
		static Action<RemoteStorageUserVoteDetails_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageUserVoteDetails_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageUserVoteDetails_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 25, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 25, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 26;
		internal static RemoteStorageEnumerateUserSharedWorkshopFilesResult_t Fill( IntPtr p ) => ((RemoteStorageEnumerateUserSharedWorkshopFilesResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) ) );
		
		static Action<RemoteStorageEnumerateUserSharedWorkshopFilesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageEnumerateUserSharedWorkshopFilesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumerateUserSharedWorkshopFilesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 26, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 26, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageSetUserPublishedFileActionResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageSetUserPublishedFileActionResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 27;
		internal static RemoteStorageSetUserPublishedFileActionResult_t Fill( IntPtr p ) => ((RemoteStorageSetUserPublishedFileActionResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageSetUserPublishedFileActionResult_t) ) );
		
		static Action<RemoteStorageSetUserPublishedFileActionResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageSetUserPublishedFileActionResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageSetUserPublishedFileActionResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 27, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 27, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageEnumeratePublishedFilesByUserActionResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
		internal uint[] GRTimeUpdated; // m_rgRTimeUpdated uint32 [50]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageEnumeratePublishedFilesByUserActionResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 28;
		internal static RemoteStorageEnumeratePublishedFilesByUserActionResult_t Fill( IntPtr p ) => ((RemoteStorageEnumeratePublishedFilesByUserActionResult_t)Marshal.PtrToStructure( p, typeof(RemoteStorageEnumeratePublishedFilesByUserActionResult_t) ) );
		
		static Action<RemoteStorageEnumeratePublishedFilesByUserActionResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageEnumeratePublishedFilesByUserActionResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumeratePublishedFilesByUserActionResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 28, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 28, false );
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
		internal bool Preview; // m_bPreview _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStoragePublishFileProgress_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 29;
		internal static RemoteStoragePublishFileProgress_t Fill( IntPtr p ) => ((RemoteStoragePublishFileProgress_t)Marshal.PtrToStructure( p, typeof(RemoteStoragePublishFileProgress_t) ) );
		
		static Action<RemoteStoragePublishFileProgress_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStoragePublishFileProgress_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishFileProgress_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 29, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 29, false );
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
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 30;
		internal static RemoteStoragePublishedFileUpdated_t Fill( IntPtr p ) => ((RemoteStoragePublishedFileUpdated_t)Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileUpdated_t) ) );
		
		static Action<RemoteStoragePublishedFileUpdated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStoragePublishedFileUpdated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishedFileUpdated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 30, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 30, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageFileWriteAsyncComplete_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageFileWriteAsyncComplete_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 31;
		internal static RemoteStorageFileWriteAsyncComplete_t Fill( IntPtr p ) => ((RemoteStorageFileWriteAsyncComplete_t)Marshal.PtrToStructure( p, typeof(RemoteStorageFileWriteAsyncComplete_t) ) );
		
		static Action<RemoteStorageFileWriteAsyncComplete_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageFileWriteAsyncComplete_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageFileWriteAsyncComplete_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 31, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 31, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageFileReadAsyncComplete_t : ICallbackData
	{
		internal ulong FileReadAsync; // m_hFileReadAsync SteamAPICall_t
		internal Result Result; // m_eResult enum EResult
		internal uint Offset; // m_nOffset uint32
		internal uint Read; // m_cubRead uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageFileReadAsyncComplete_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientRemoteStorage + 32;
		internal static RemoteStorageFileReadAsyncComplete_t Fill( IntPtr p ) => ((RemoteStorageFileReadAsyncComplete_t)Marshal.PtrToStructure( p, typeof(RemoteStorageFileReadAsyncComplete_t) ) );
		
		static Action<RemoteStorageFileReadAsyncComplete_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageFileReadAsyncComplete_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageFileReadAsyncComplete_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientRemoteStorage + 32, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientRemoteStorage + 32, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct LeaderboardEntry_t
	{
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		internal int GlobalRank; // m_nGlobalRank int32
		internal int Score; // m_nScore int32
		internal int CDetails; // m_cDetails int32
		internal ulong UGC; // m_hUGC UGCHandle_t
		
		#region Marshalling
		internal static LeaderboardEntry_t Fill( IntPtr p ) => ((LeaderboardEntry_t)(LeaderboardEntry_t) Marshal.PtrToStructure( p, typeof(LeaderboardEntry_t) ) );
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct UserStatsReceived_t : ICallbackData
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserStatsReceived_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUserStats + 1;
		internal static UserStatsReceived_t Fill( IntPtr p ) => ((UserStatsReceived_t)Marshal.PtrToStructure( p, typeof(UserStatsReceived_t) ) );
		
		static Action<UserStatsReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UserStatsReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UserStatsReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUserStats + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUserStats + 1, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct UserStatsStored_t : ICallbackData
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserStatsStored_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUserStats + 2;
		internal static UserStatsStored_t Fill( IntPtr p ) => ((UserStatsStored_t)Marshal.PtrToStructure( p, typeof(UserStatsStored_t) ) );
		
		static Action<UserStatsStored_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UserStatsStored_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UserStatsStored_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUserStats + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUserStats + 2, false );
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
		internal bool GroupAchievement; // m_bGroupAchievement _Bool
		internal string AchievementNameUTF8() => System.Text.Encoding.UTF8.GetString( AchievementName, 0, System.Array.IndexOf<byte>( AchievementName, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)] // byte[] m_rgchAchievementName
		internal byte[] AchievementName; // m_rgchAchievementName char [128]
		internal uint CurProgress; // m_nCurProgress uint32
		internal uint MaxProgress; // m_nMaxProgress uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserAchievementStored_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUserStats + 3;
		internal static UserAchievementStored_t Fill( IntPtr p ) => ((UserAchievementStored_t)Marshal.PtrToStructure( p, typeof(UserAchievementStored_t) ) );
		
		static Action<UserAchievementStored_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UserAchievementStored_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UserAchievementStored_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUserStats + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUserStats + 3, false );
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
		public int CallbackId => CallbackIdentifiers.SteamUserStats + 4;
		internal static LeaderboardFindResult_t Fill( IntPtr p ) => ((LeaderboardFindResult_t)Marshal.PtrToStructure( p, typeof(LeaderboardFindResult_t) ) );
		
		static Action<LeaderboardFindResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LeaderboardFindResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LeaderboardFindResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUserStats + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUserStats + 4, false );
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
		public int CallbackId => CallbackIdentifiers.SteamUserStats + 5;
		internal static LeaderboardScoresDownloaded_t Fill( IntPtr p ) => ((LeaderboardScoresDownloaded_t)Marshal.PtrToStructure( p, typeof(LeaderboardScoresDownloaded_t) ) );
		
		static Action<LeaderboardScoresDownloaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LeaderboardScoresDownloaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LeaderboardScoresDownloaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUserStats + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUserStats + 5, false );
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
		public int CallbackId => CallbackIdentifiers.SteamUserStats + 6;
		internal static LeaderboardScoreUploaded_t Fill( IntPtr p ) => ((LeaderboardScoreUploaded_t)Marshal.PtrToStructure( p, typeof(LeaderboardScoreUploaded_t) ) );
		
		static Action<LeaderboardScoreUploaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LeaderboardScoreUploaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LeaderboardScoreUploaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUserStats + 6, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUserStats + 6, false );
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
		public int CallbackId => CallbackIdentifiers.SteamUserStats + 7;
		internal static NumberOfCurrentPlayers_t Fill( IntPtr p ) => ((NumberOfCurrentPlayers_t)Marshal.PtrToStructure( p, typeof(NumberOfCurrentPlayers_t) ) );
		
		static Action<NumberOfCurrentPlayers_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<NumberOfCurrentPlayers_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<NumberOfCurrentPlayers_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUserStats + 7, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUserStats + 7, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct UserStatsUnloaded_t : ICallbackData
	{
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserStatsUnloaded_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUserStats + 8;
		internal static UserStatsUnloaded_t Fill( IntPtr p ) => ((UserStatsUnloaded_t)Marshal.PtrToStructure( p, typeof(UserStatsUnloaded_t) ) );
		
		static Action<UserStatsUnloaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UserStatsUnloaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UserStatsUnloaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUserStats + 8, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUserStats + 8, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct UserAchievementIconFetched_t : ICallbackData
	{
		internal GameId GameID; // m_nGameID class CGameID
		internal string AchievementNameUTF8() => System.Text.Encoding.UTF8.GetString( AchievementName, 0, System.Array.IndexOf<byte>( AchievementName, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)] // byte[] m_rgchAchievementName
		internal byte[] AchievementName; // m_rgchAchievementName char [128]
		[MarshalAs(UnmanagedType.I1)]
		internal bool Achieved; // m_bAchieved _Bool
		internal int IconHandle; // m_nIconHandle int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserAchievementIconFetched_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUserStats + 9;
		internal static UserAchievementIconFetched_t Fill( IntPtr p ) => ((UserAchievementIconFetched_t)Marshal.PtrToStructure( p, typeof(UserAchievementIconFetched_t) ) );
		
		static Action<UserAchievementIconFetched_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UserAchievementIconFetched_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UserAchievementIconFetched_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUserStats + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUserStats + 9, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GlobalAchievementPercentagesReady_t : ICallbackData
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GlobalAchievementPercentagesReady_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUserStats + 10;
		internal static GlobalAchievementPercentagesReady_t Fill( IntPtr p ) => ((GlobalAchievementPercentagesReady_t)Marshal.PtrToStructure( p, typeof(GlobalAchievementPercentagesReady_t) ) );
		
		static Action<GlobalAchievementPercentagesReady_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GlobalAchievementPercentagesReady_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GlobalAchievementPercentagesReady_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUserStats + 10, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUserStats + 10, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LeaderboardUGCSet_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LeaderboardUGCSet_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUserStats + 11;
		internal static LeaderboardUGCSet_t Fill( IntPtr p ) => ((LeaderboardUGCSet_t)Marshal.PtrToStructure( p, typeof(LeaderboardUGCSet_t) ) );
		
		static Action<LeaderboardUGCSet_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LeaderboardUGCSet_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LeaderboardUGCSet_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUserStats + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUserStats + 11, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct PS3TrophiesInstalled_t : ICallbackData
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		internal ulong RequiredDiskSpace; // m_ulRequiredDiskSpace uint64
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(PS3TrophiesInstalled_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUserStats + 12;
		internal static PS3TrophiesInstalled_t Fill( IntPtr p ) => ((PS3TrophiesInstalled_t)Marshal.PtrToStructure( p, typeof(PS3TrophiesInstalled_t) ) );
		
		static Action<PS3TrophiesInstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<PS3TrophiesInstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<PS3TrophiesInstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUserStats + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUserStats + 12, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GlobalStatsReceived_t : ICallbackData
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GlobalStatsReceived_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUserStats + 12;
		internal static GlobalStatsReceived_t Fill( IntPtr p ) => ((GlobalStatsReceived_t)Marshal.PtrToStructure( p, typeof(GlobalStatsReceived_t) ) );
		
		static Action<GlobalStatsReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GlobalStatsReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GlobalStatsReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUserStats + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUserStats + 12, false );
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
		public int CallbackId => CallbackIdentifiers.SteamApps + 5;
		internal static DlcInstalled_t Fill( IntPtr p ) => ((DlcInstalled_t)Marshal.PtrToStructure( p, typeof(DlcInstalled_t) ) );
		
		static Action<DlcInstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<DlcInstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<DlcInstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamApps + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamApps + 5, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RegisterActivationCodeResponse_t : ICallbackData
	{
		internal RegisterActivationCodeResult Result; // m_eResult enum ERegisterActivationCodeResult
		internal uint PackageRegistered; // m_unPackageRegistered uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RegisterActivationCodeResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamApps + 8;
		internal static RegisterActivationCodeResponse_t Fill( IntPtr p ) => ((RegisterActivationCodeResponse_t)Marshal.PtrToStructure( p, typeof(RegisterActivationCodeResponse_t) ) );
		
		static Action<RegisterActivationCodeResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RegisterActivationCodeResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RegisterActivationCodeResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamApps + 8, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamApps + 8, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct AppProofOfPurchaseKeyResponse_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal uint AppID; // m_nAppID uint32
		internal uint CchKeyLength; // m_cchKeyLength uint32
		internal string KeyUTF8() => System.Text.Encoding.UTF8.GetString( Key, 0, System.Array.IndexOf<byte>( Key, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 240)] // byte[] m_rgchKey
		internal byte[] Key; // m_rgchKey char [240]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(AppProofOfPurchaseKeyResponse_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamApps + 21;
		internal static AppProofOfPurchaseKeyResponse_t Fill( IntPtr p ) => ((AppProofOfPurchaseKeyResponse_t)Marshal.PtrToStructure( p, typeof(AppProofOfPurchaseKeyResponse_t) ) );
		
		static Action<AppProofOfPurchaseKeyResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<AppProofOfPurchaseKeyResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<AppProofOfPurchaseKeyResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamApps + 21, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamApps + 21, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct FileDetailsResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong FileSize; // m_ulFileSize uint64
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] //  m_FileSHA
		internal byte[] FileSHA; // m_FileSHA uint8 [20]
		internal uint Flags; // m_unFlags uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FileDetailsResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamApps + 23;
		internal static FileDetailsResult_t Fill( IntPtr p ) => ((FileDetailsResult_t)Marshal.PtrToStructure( p, typeof(FileDetailsResult_t) ) );
		
		static Action<FileDetailsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FileDetailsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FileDetailsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamApps + 23, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamApps + 23, false );
				actionClient = action;
			}
		}
		#endregion
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
		
		#region Marshalling
		internal static P2PSessionState_t Fill( IntPtr p ) => ((P2PSessionState_t)(P2PSessionState_t) Marshal.PtrToStructure( p, typeof(P2PSessionState_t) ) );
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct P2PSessionRequest_t : ICallbackData
	{
		internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(P2PSessionRequest_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamNetworking + 2;
		internal static P2PSessionRequest_t Fill( IntPtr p ) => ((P2PSessionRequest_t)Marshal.PtrToStructure( p, typeof(P2PSessionRequest_t) ) );
		
		static Action<P2PSessionRequest_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<P2PSessionRequest_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<P2PSessionRequest_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamNetworking + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamNetworking + 2, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct P2PSessionConnectFail_t : ICallbackData
	{
		internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
		internal byte P2PSessionError; // m_eP2PSessionError uint8
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(P2PSessionConnectFail_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamNetworking + 3;
		internal static P2PSessionConnectFail_t Fill( IntPtr p ) => ((P2PSessionConnectFail_t)Marshal.PtrToStructure( p, typeof(P2PSessionConnectFail_t) ) );
		
		static Action<P2PSessionConnectFail_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<P2PSessionConnectFail_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<P2PSessionConnectFail_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamNetworking + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamNetworking + 3, false );
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
		internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
		internal int SNetSocketState; // m_eSNetSocketState int
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SocketStatusCallback_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamNetworking + 1;
		internal static SocketStatusCallback_t Fill( IntPtr p ) => ((SocketStatusCallback_t)Marshal.PtrToStructure( p, typeof(SocketStatusCallback_t) ) );
		
		static Action<SocketStatusCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SocketStatusCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SocketStatusCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamNetworking + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamNetworking + 1, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct ScreenshotReady_t : ICallbackData
	{
		internal uint Local; // m_hLocal ScreenshotHandle
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ScreenshotReady_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamScreenshots + 1;
		internal static ScreenshotReady_t Fill( IntPtr p ) => ((ScreenshotReady_t)Marshal.PtrToStructure( p, typeof(ScreenshotReady_t) ) );
		
		static Action<ScreenshotReady_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ScreenshotReady_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ScreenshotReady_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamScreenshots + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamScreenshots + 1, false );
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
		public int CallbackId => CallbackIdentifiers.SteamMusic + 2;
		internal static VolumeHasChanged_t Fill( IntPtr p ) => ((VolumeHasChanged_t)Marshal.PtrToStructure( p, typeof(VolumeHasChanged_t) ) );
		
		static Action<VolumeHasChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<VolumeHasChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<VolumeHasChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMusic + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMusic + 2, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerWantsShuffled_t : ICallbackData
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Shuffled; // m_bShuffled _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerWantsShuffled_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamMusicRemote + 9;
		internal static MusicPlayerWantsShuffled_t Fill( IntPtr p ) => ((MusicPlayerWantsShuffled_t)Marshal.PtrToStructure( p, typeof(MusicPlayerWantsShuffled_t) ) );
		
		static Action<MusicPlayerWantsShuffled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerWantsShuffled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsShuffled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMusicRemote + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMusicRemote + 9, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerWantsLooped_t : ICallbackData
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Looped; // m_bLooped _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerWantsLooped_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamMusicRemote + 10;
		internal static MusicPlayerWantsLooped_t Fill( IntPtr p ) => ((MusicPlayerWantsLooped_t)Marshal.PtrToStructure( p, typeof(MusicPlayerWantsLooped_t) ) );
		
		static Action<MusicPlayerWantsLooped_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerWantsLooped_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsLooped_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMusicRemote + 10, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMusicRemote + 10, false );
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
		public int CallbackId => CallbackIdentifiers.SteamMusic + 11;
		internal static MusicPlayerWantsVolume_t Fill( IntPtr p ) => ((MusicPlayerWantsVolume_t)Marshal.PtrToStructure( p, typeof(MusicPlayerWantsVolume_t) ) );
		
		static Action<MusicPlayerWantsVolume_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerWantsVolume_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsVolume_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMusic + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMusic + 11, false );
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
		public int CallbackId => CallbackIdentifiers.SteamMusic + 12;
		internal static MusicPlayerSelectsQueueEntry_t Fill( IntPtr p ) => ((MusicPlayerSelectsQueueEntry_t)Marshal.PtrToStructure( p, typeof(MusicPlayerSelectsQueueEntry_t) ) );
		
		static Action<MusicPlayerSelectsQueueEntry_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerSelectsQueueEntry_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerSelectsQueueEntry_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMusic + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMusic + 12, false );
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
		public int CallbackId => CallbackIdentifiers.SteamMusic + 13;
		internal static MusicPlayerSelectsPlaylistEntry_t Fill( IntPtr p ) => ((MusicPlayerSelectsPlaylistEntry_t)Marshal.PtrToStructure( p, typeof(MusicPlayerSelectsPlaylistEntry_t) ) );
		
		static Action<MusicPlayerSelectsPlaylistEntry_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerSelectsPlaylistEntry_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerSelectsPlaylistEntry_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMusic + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMusic + 13, false );
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
		public int CallbackId => CallbackIdentifiers.SteamMusicRemote + 14;
		internal static MusicPlayerWantsPlayingRepeatStatus_t Fill( IntPtr p ) => ((MusicPlayerWantsPlayingRepeatStatus_t)Marshal.PtrToStructure( p, typeof(MusicPlayerWantsPlayingRepeatStatus_t) ) );
		
		static Action<MusicPlayerWantsPlayingRepeatStatus_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerWantsPlayingRepeatStatus_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsPlayingRepeatStatus_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMusicRemote + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMusicRemote + 14, false );
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
		internal bool RequestSuccessful; // m_bRequestSuccessful _Bool
		internal HTTPStatusCode StatusCode; // m_eStatusCode enum EHTTPStatusCode
		internal uint BodySize; // m_unBodySize uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTTPRequestCompleted_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientHTTP + 1;
		internal static HTTPRequestCompleted_t Fill( IntPtr p ) => ((HTTPRequestCompleted_t)Marshal.PtrToStructure( p, typeof(HTTPRequestCompleted_t) ) );
		
		static Action<HTTPRequestCompleted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTTPRequestCompleted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTTPRequestCompleted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientHTTP + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientHTTP + 1, false );
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
		public int CallbackId => CallbackIdentifiers.ClientHTTP + 2;
		internal static HTTPRequestHeadersReceived_t Fill( IntPtr p ) => ((HTTPRequestHeadersReceived_t)Marshal.PtrToStructure( p, typeof(HTTPRequestHeadersReceived_t) ) );
		
		static Action<HTTPRequestHeadersReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTTPRequestHeadersReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTTPRequestHeadersReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientHTTP + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientHTTP + 2, false );
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
		public int CallbackId => CallbackIdentifiers.ClientHTTP + 3;
		internal static HTTPRequestDataReceived_t Fill( IntPtr p ) => ((HTTPRequestDataReceived_t)Marshal.PtrToStructure( p, typeof(HTTPRequestDataReceived_t) ) );
		
		static Action<HTTPRequestDataReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTTPRequestDataReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTTPRequestDataReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientHTTP + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientHTTP + 3, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamUGCDetails_t
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		internal WorkshopFileType FileType; // m_eFileType enum EWorkshopFileType
		internal AppId CreatorAppID; // m_nCreatorAppID AppId_t
		internal AppId ConsumerAppID; // m_nConsumerAppID AppId_t
		internal string TitleUTF8() => System.Text.Encoding.UTF8.GetString( Title, 0, System.Array.IndexOf<byte>( Title, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 129)] // byte[] m_rgchTitle
		internal byte[] Title; // m_rgchTitle char [129]
		internal string DescriptionUTF8() => System.Text.Encoding.UTF8.GetString( Description, 0, System.Array.IndexOf<byte>( Description, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8000)] // byte[] m_rgchDescription
		internal byte[] Description; // m_rgchDescription char [8000]
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
		internal string TagsUTF8() => System.Text.Encoding.UTF8.GetString( Tags, 0, System.Array.IndexOf<byte>( Tags, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1025)] // byte[] m_rgchTags
		internal byte[] Tags; // m_rgchTags char [1025]
		internal ulong File; // m_hFile UGCHandle_t
		internal ulong PreviewFile; // m_hPreviewFile UGCHandle_t
		internal string PchFileNameUTF8() => System.Text.Encoding.UTF8.GetString( PchFileName, 0, System.Array.IndexOf<byte>( PchFileName, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)] // byte[] m_pchFileName
		internal byte[] PchFileName; // m_pchFileName char [260]
		internal int FileSize; // m_nFileSize int32
		internal int PreviewFileSize; // m_nPreviewFileSize int32
		internal string URLUTF8() => System.Text.Encoding.UTF8.GetString( URL, 0, System.Array.IndexOf<byte>( URL, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // byte[] m_rgchURL
		internal byte[] URL; // m_rgchURL char [256]
		internal uint VotesUp; // m_unVotesUp uint32
		internal uint VotesDown; // m_unVotesDown uint32
		internal float Score; // m_flScore float
		internal uint NumChildren; // m_unNumChildren uint32
		
		#region Marshalling
		internal static SteamUGCDetails_t Fill( IntPtr p ) => ((SteamUGCDetails_t)(SteamUGCDetails_t) Marshal.PtrToStructure( p, typeof(SteamUGCDetails_t) ) );
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamUGCQueryCompleted_t : ICallbackData
	{
		internal ulong Handle; // m_handle UGCQueryHandle_t
		internal Result Result; // m_eResult enum EResult
		internal uint NumResultsReturned; // m_unNumResultsReturned uint32
		internal uint TotalMatchingResults; // m_unTotalMatchingResults uint32
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData; // m_bCachedData _Bool
		internal string NextCursorUTF8() => System.Text.Encoding.UTF8.GetString( NextCursor, 0, System.Array.IndexOf<byte>( NextCursor, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // byte[] m_rgchNextCursor
		internal byte[] NextCursor; // m_rgchNextCursor char [256]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamUGCQueryCompleted_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientUGC + 1;
		internal static SteamUGCQueryCompleted_t Fill( IntPtr p ) => ((SteamUGCQueryCompleted_t)Marshal.PtrToStructure( p, typeof(SteamUGCQueryCompleted_t) ) );
		
		static Action<SteamUGCQueryCompleted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamUGCQueryCompleted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamUGCQueryCompleted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientUGC + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientUGC + 1, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamUGCRequestUGCDetailsResult_t : ICallbackData
	{
		internal SteamUGCDetails_t Details; // m_details struct SteamUGCDetails_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData; // m_bCachedData _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamUGCRequestUGCDetailsResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientUGC + 2;
		internal static SteamUGCRequestUGCDetailsResult_t Fill( IntPtr p ) => ((SteamUGCRequestUGCDetailsResult_t)Marshal.PtrToStructure( p, typeof(SteamUGCRequestUGCDetailsResult_t) ) );
		
		static Action<SteamUGCRequestUGCDetailsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamUGCRequestUGCDetailsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamUGCRequestUGCDetailsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientUGC + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientUGC + 2, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct CreateItemResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(CreateItemResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientUGC + 3;
		internal static CreateItemResult_t Fill( IntPtr p ) => ((CreateItemResult_t)Marshal.PtrToStructure( p, typeof(CreateItemResult_t) ) );
		
		static Action<CreateItemResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<CreateItemResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<CreateItemResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientUGC + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientUGC + 3, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SubmitItemUpdateResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SubmitItemUpdateResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientUGC + 4;
		internal static SubmitItemUpdateResult_t Fill( IntPtr p ) => ((SubmitItemUpdateResult_t)Marshal.PtrToStructure( p, typeof(SubmitItemUpdateResult_t) ) );
		
		static Action<SubmitItemUpdateResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SubmitItemUpdateResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SubmitItemUpdateResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientUGC + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientUGC + 4, false );
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
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(DownloadItemResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientUGC + 6;
		internal static DownloadItemResult_t Fill( IntPtr p ) => ((DownloadItemResult_t)Marshal.PtrToStructure( p, typeof(DownloadItemResult_t) ) );
		
		static Action<DownloadItemResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<DownloadItemResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<DownloadItemResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientUGC + 6, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientUGC + 6, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct UserFavoriteItemsListChanged_t : ICallbackData
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool WasAddRequest; // m_bWasAddRequest _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserFavoriteItemsListChanged_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientUGC + 7;
		internal static UserFavoriteItemsListChanged_t Fill( IntPtr p ) => ((UserFavoriteItemsListChanged_t)Marshal.PtrToStructure( p, typeof(UserFavoriteItemsListChanged_t) ) );
		
		static Action<UserFavoriteItemsListChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UserFavoriteItemsListChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UserFavoriteItemsListChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientUGC + 7, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientUGC + 7, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SetUserItemVoteResult_t : ICallbackData
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool VoteUp; // m_bVoteUp _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SetUserItemVoteResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientUGC + 8;
		internal static SetUserItemVoteResult_t Fill( IntPtr p ) => ((SetUserItemVoteResult_t)Marshal.PtrToStructure( p, typeof(SetUserItemVoteResult_t) ) );
		
		static Action<SetUserItemVoteResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SetUserItemVoteResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SetUserItemVoteResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientUGC + 8, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientUGC + 8, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GetUserItemVoteResult_t : ICallbackData
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool VotedUp; // m_bVotedUp _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool VotedDown; // m_bVotedDown _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool VoteSkipped; // m_bVoteSkipped _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GetUserItemVoteResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientUGC + 9;
		internal static GetUserItemVoteResult_t Fill( IntPtr p ) => ((GetUserItemVoteResult_t)Marshal.PtrToStructure( p, typeof(GetUserItemVoteResult_t) ) );
		
		static Action<GetUserItemVoteResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GetUserItemVoteResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GetUserItemVoteResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientUGC + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientUGC + 9, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct StartPlaytimeTrackingResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(StartPlaytimeTrackingResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientUGC + 10;
		internal static StartPlaytimeTrackingResult_t Fill( IntPtr p ) => ((StartPlaytimeTrackingResult_t)Marshal.PtrToStructure( p, typeof(StartPlaytimeTrackingResult_t) ) );
		
		static Action<StartPlaytimeTrackingResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<StartPlaytimeTrackingResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<StartPlaytimeTrackingResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientUGC + 10, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientUGC + 10, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct StopPlaytimeTrackingResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(StopPlaytimeTrackingResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientUGC + 11;
		internal static StopPlaytimeTrackingResult_t Fill( IntPtr p ) => ((StopPlaytimeTrackingResult_t)Marshal.PtrToStructure( p, typeof(StopPlaytimeTrackingResult_t) ) );
		
		static Action<StopPlaytimeTrackingResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<StopPlaytimeTrackingResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<StopPlaytimeTrackingResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientUGC + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientUGC + 11, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct AddUGCDependencyResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal PublishedFileId ChildPublishedFileId; // m_nChildPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(AddUGCDependencyResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientUGC + 12;
		internal static AddUGCDependencyResult_t Fill( IntPtr p ) => ((AddUGCDependencyResult_t)Marshal.PtrToStructure( p, typeof(AddUGCDependencyResult_t) ) );
		
		static Action<AddUGCDependencyResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<AddUGCDependencyResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<AddUGCDependencyResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientUGC + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientUGC + 12, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoveUGCDependencyResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal PublishedFileId ChildPublishedFileId; // m_nChildPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoveUGCDependencyResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientUGC + 13;
		internal static RemoveUGCDependencyResult_t Fill( IntPtr p ) => ((RemoveUGCDependencyResult_t)Marshal.PtrToStructure( p, typeof(RemoveUGCDependencyResult_t) ) );
		
		static Action<RemoveUGCDependencyResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoveUGCDependencyResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoveUGCDependencyResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientUGC + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientUGC + 13, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct AddAppDependencyResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(AddAppDependencyResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientUGC + 14;
		internal static AddAppDependencyResult_t Fill( IntPtr p ) => ((AddAppDependencyResult_t)Marshal.PtrToStructure( p, typeof(AddAppDependencyResult_t) ) );
		
		static Action<AddAppDependencyResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<AddAppDependencyResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<AddAppDependencyResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientUGC + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientUGC + 14, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoveAppDependencyResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoveAppDependencyResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientUGC + 15;
		internal static RemoveAppDependencyResult_t Fill( IntPtr p ) => ((RemoveAppDependencyResult_t)Marshal.PtrToStructure( p, typeof(RemoveAppDependencyResult_t) ) );
		
		static Action<RemoveAppDependencyResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoveAppDependencyResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoveAppDependencyResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientUGC + 15, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientUGC + 15, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GetAppDependenciesResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.U4)]
		internal AppId[] GAppIDs; // m_rgAppIDs AppId_t [32]
		internal uint NumAppDependencies; // m_nNumAppDependencies uint32
		internal uint TotalNumAppDependencies; // m_nTotalNumAppDependencies uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GetAppDependenciesResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientUGC + 16;
		internal static GetAppDependenciesResult_t Fill( IntPtr p ) => ((GetAppDependenciesResult_t)Marshal.PtrToStructure( p, typeof(GetAppDependenciesResult_t) ) );
		
		static Action<GetAppDependenciesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GetAppDependenciesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GetAppDependenciesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientUGC + 16, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientUGC + 16, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct DeleteItemResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(DeleteItemResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientUGC + 17;
		internal static DeleteItemResult_t Fill( IntPtr p ) => ((DeleteItemResult_t)Marshal.PtrToStructure( p, typeof(DeleteItemResult_t) ) );
		
		static Action<DeleteItemResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<DeleteItemResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<DeleteItemResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientUGC + 17, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientUGC + 17, false );
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
		public int CallbackId => CallbackIdentifiers.SteamAppList + 1;
		internal static SteamAppInstalled_t Fill( IntPtr p ) => ((SteamAppInstalled_t)Marshal.PtrToStructure( p, typeof(SteamAppInstalled_t) ) );
		
		static Action<SteamAppInstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamAppInstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamAppInstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamAppList + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamAppList + 1, false );
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
		public int CallbackId => CallbackIdentifiers.SteamAppList + 2;
		internal static SteamAppUninstalled_t Fill( IntPtr p ) => ((SteamAppUninstalled_t)Marshal.PtrToStructure( p, typeof(SteamAppUninstalled_t) ) );
		
		static Action<SteamAppUninstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamAppUninstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamAppUninstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamAppList + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamAppList + 2, false );
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
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 1;
		internal static HTML_BrowserReady_t Fill( IntPtr p ) => ((HTML_BrowserReady_t)Marshal.PtrToStructure( p, typeof(HTML_BrowserReady_t) ) );
		
		static Action<HTML_BrowserReady_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_BrowserReady_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_BrowserReady_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 1, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
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
		internal static HTML_NeedsPaint_t Fill( IntPtr p ) => ((HTML_NeedsPaint_t)(HTML_NeedsPaint_t) Marshal.PtrToStructure( p, typeof(HTML_NeedsPaint_t) ) );
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_StartRequest_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		internal string PchTarget; // pchTarget const char *
		internal string PchPostData; // pchPostData const char *
		[MarshalAs(UnmanagedType.I1)]
		internal bool BIsRedirect; // bIsRedirect _Bool
		
		#region Marshalling
		internal static HTML_StartRequest_t Fill( IntPtr p ) => ((HTML_StartRequest_t)(HTML_StartRequest_t) Marshal.PtrToStructure( p, typeof(HTML_StartRequest_t) ) );
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_CloseBrowser_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		
		#region Marshalling
		internal static HTML_CloseBrowser_t Fill( IntPtr p ) => ((HTML_CloseBrowser_t)(HTML_CloseBrowser_t) Marshal.PtrToStructure( p, typeof(HTML_CloseBrowser_t) ) );
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_URLChanged_t : ICallbackData
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		internal string PchPostData; // pchPostData const char *
		[MarshalAs(UnmanagedType.I1)]
		internal bool BIsRedirect; // bIsRedirect _Bool
		internal string PchPageTitle; // pchPageTitle const char *
		[MarshalAs(UnmanagedType.I1)]
		internal bool BNewNavigation; // bNewNavigation _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_URLChanged_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 5;
		internal static HTML_URLChanged_t Fill( IntPtr p ) => ((HTML_URLChanged_t)Marshal.PtrToStructure( p, typeof(HTML_URLChanged_t) ) );
		
		static Action<HTML_URLChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_URLChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_URLChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 5, false );
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
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 6;
		internal static HTML_FinishedRequest_t Fill( IntPtr p ) => ((HTML_FinishedRequest_t)Marshal.PtrToStructure( p, typeof(HTML_FinishedRequest_t) ) );
		
		static Action<HTML_FinishedRequest_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_FinishedRequest_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_FinishedRequest_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 6, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 6, false );
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
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 7;
		internal static HTML_OpenLinkInNewTab_t Fill( IntPtr p ) => ((HTML_OpenLinkInNewTab_t)Marshal.PtrToStructure( p, typeof(HTML_OpenLinkInNewTab_t) ) );
		
		static Action<HTML_OpenLinkInNewTab_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_OpenLinkInNewTab_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_OpenLinkInNewTab_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 7, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 7, false );
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
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 8;
		internal static HTML_ChangedTitle_t Fill( IntPtr p ) => ((HTML_ChangedTitle_t)Marshal.PtrToStructure( p, typeof(HTML_ChangedTitle_t) ) );
		
		static Action<HTML_ChangedTitle_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_ChangedTitle_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_ChangedTitle_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 8, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 8, false );
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
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 9;
		internal static HTML_SearchResults_t Fill( IntPtr p ) => ((HTML_SearchResults_t)Marshal.PtrToStructure( p, typeof(HTML_SearchResults_t) ) );
		
		static Action<HTML_SearchResults_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_SearchResults_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_SearchResults_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 9, false );
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
		internal bool BCanGoBack; // bCanGoBack _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool BCanGoForward; // bCanGoForward _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_CanGoBackAndForward_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 10;
		internal static HTML_CanGoBackAndForward_t Fill( IntPtr p ) => ((HTML_CanGoBackAndForward_t)Marshal.PtrToStructure( p, typeof(HTML_CanGoBackAndForward_t) ) );
		
		static Action<HTML_CanGoBackAndForward_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_CanGoBackAndForward_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_CanGoBackAndForward_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 10, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 10, false );
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
		internal bool BVisible; // bVisible _Bool
		internal uint UnPageSize; // unPageSize uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_HorizontalScroll_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 11;
		internal static HTML_HorizontalScroll_t Fill( IntPtr p ) => ((HTML_HorizontalScroll_t)Marshal.PtrToStructure( p, typeof(HTML_HorizontalScroll_t) ) );
		
		static Action<HTML_HorizontalScroll_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_HorizontalScroll_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_HorizontalScroll_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 11, false );
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
		internal bool BVisible; // bVisible _Bool
		internal uint UnPageSize; // unPageSize uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_VerticalScroll_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 12;
		internal static HTML_VerticalScroll_t Fill( IntPtr p ) => ((HTML_VerticalScroll_t)Marshal.PtrToStructure( p, typeof(HTML_VerticalScroll_t) ) );
		
		static Action<HTML_VerticalScroll_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_VerticalScroll_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_VerticalScroll_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 12, false );
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
		internal bool BInput; // bInput _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool BLiveLink; // bLiveLink _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_LinkAtPosition_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 13;
		internal static HTML_LinkAtPosition_t Fill( IntPtr p ) => ((HTML_LinkAtPosition_t)Marshal.PtrToStructure( p, typeof(HTML_LinkAtPosition_t) ) );
		
		static Action<HTML_LinkAtPosition_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_LinkAtPosition_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_LinkAtPosition_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 13, false );
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
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 14;
		internal static HTML_JSAlert_t Fill( IntPtr p ) => ((HTML_JSAlert_t)Marshal.PtrToStructure( p, typeof(HTML_JSAlert_t) ) );
		
		static Action<HTML_JSAlert_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_JSAlert_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_JSAlert_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 14, false );
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
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 15;
		internal static HTML_JSConfirm_t Fill( IntPtr p ) => ((HTML_JSConfirm_t)Marshal.PtrToStructure( p, typeof(HTML_JSConfirm_t) ) );
		
		static Action<HTML_JSConfirm_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_JSConfirm_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_JSConfirm_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 15, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 15, false );
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
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 16;
		internal static HTML_FileOpenDialog_t Fill( IntPtr p ) => ((HTML_FileOpenDialog_t)Marshal.PtrToStructure( p, typeof(HTML_FileOpenDialog_t) ) );
		
		static Action<HTML_FileOpenDialog_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_FileOpenDialog_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_FileOpenDialog_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 16, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 16, false );
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
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 21;
		internal static HTML_NewWindow_t Fill( IntPtr p ) => ((HTML_NewWindow_t)Marshal.PtrToStructure( p, typeof(HTML_NewWindow_t) ) );
		
		static Action<HTML_NewWindow_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_NewWindow_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_NewWindow_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 21, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 21, false );
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
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 22;
		internal static HTML_SetCursor_t Fill( IntPtr p ) => ((HTML_SetCursor_t)Marshal.PtrToStructure( p, typeof(HTML_SetCursor_t) ) );
		
		static Action<HTML_SetCursor_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_SetCursor_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_SetCursor_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 22, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 22, false );
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
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 23;
		internal static HTML_StatusText_t Fill( IntPtr p ) => ((HTML_StatusText_t)Marshal.PtrToStructure( p, typeof(HTML_StatusText_t) ) );
		
		static Action<HTML_StatusText_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_StatusText_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_StatusText_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 23, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 23, false );
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
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 24;
		internal static HTML_ShowToolTip_t Fill( IntPtr p ) => ((HTML_ShowToolTip_t)Marshal.PtrToStructure( p, typeof(HTML_ShowToolTip_t) ) );
		
		static Action<HTML_ShowToolTip_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_ShowToolTip_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_ShowToolTip_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 24, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 24, false );
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
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 25;
		internal static HTML_UpdateToolTip_t Fill( IntPtr p ) => ((HTML_UpdateToolTip_t)Marshal.PtrToStructure( p, typeof(HTML_UpdateToolTip_t) ) );
		
		static Action<HTML_UpdateToolTip_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_UpdateToolTip_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_UpdateToolTip_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 25, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 25, false );
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
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 26;
		internal static HTML_HideToolTip_t Fill( IntPtr p ) => ((HTML_HideToolTip_t)Marshal.PtrToStructure( p, typeof(HTML_HideToolTip_t) ) );
		
		static Action<HTML_HideToolTip_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_HideToolTip_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_HideToolTip_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 26, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 26, false );
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
		public int CallbackId => CallbackIdentifiers.SteamHTMLSurface + 27;
		internal static HTML_BrowserRestarted_t Fill( IntPtr p ) => ((HTML_BrowserRestarted_t)Marshal.PtrToStructure( p, typeof(HTML_BrowserRestarted_t) ) );
		
		static Action<HTML_BrowserRestarted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_BrowserRestarted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_BrowserRestarted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamHTMLSurface + 27, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamHTMLSurface + 27, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamItemDetails_t
	{
		internal InventoryItemId ItemId; // m_itemId SteamItemInstanceID_t
		internal InventoryDefId Definition; // m_iDefinition SteamItemDef_t
		internal ushort Quantity; // m_unQuantity uint16
		internal ushort Flags; // m_unFlags uint16
		
		#region Marshalling
		internal static SteamItemDetails_t Fill( IntPtr p ) => ((SteamItemDetails_t)(SteamItemDetails_t) Marshal.PtrToStructure( p, typeof(SteamItemDetails_t) ) );
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamInventoryResultReady_t : ICallbackData
	{
		internal int Handle; // m_handle SteamInventoryResult_t
		internal Result Result; // m_result enum EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamInventoryResultReady_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientInventory + 0;
		internal static SteamInventoryResultReady_t Fill( IntPtr p ) => ((SteamInventoryResultReady_t)Marshal.PtrToStructure( p, typeof(SteamInventoryResultReady_t) ) );
		
		static Action<SteamInventoryResultReady_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamInventoryResultReady_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamInventoryResultReady_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientInventory + 0, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientInventory + 0, false );
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
		public int CallbackId => CallbackIdentifiers.ClientInventory + 1;
		internal static SteamInventoryFullUpdate_t Fill( IntPtr p ) => ((SteamInventoryFullUpdate_t)Marshal.PtrToStructure( p, typeof(SteamInventoryFullUpdate_t) ) );
		
		static Action<SteamInventoryFullUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamInventoryFullUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamInventoryFullUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientInventory + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientInventory + 1, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct SteamInventoryEligiblePromoItemDefIDs_t : ICallbackData
	{
		internal Result Result; // m_result enum EResult
		internal ulong SteamID; // m_steamID class CSteamID
		internal int UmEligiblePromoItemDefs; // m_numEligiblePromoItemDefs int
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData; // m_bCachedData _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamInventoryEligiblePromoItemDefIDs_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientInventory + 3;
		internal static SteamInventoryEligiblePromoItemDefIDs_t Fill( IntPtr p ) => ((SteamInventoryEligiblePromoItemDefIDs_t)Marshal.PtrToStructure( p, typeof(SteamInventoryEligiblePromoItemDefIDs_t) ) );
		
		static Action<SteamInventoryEligiblePromoItemDefIDs_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamInventoryEligiblePromoItemDefIDs_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamInventoryEligiblePromoItemDefIDs_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientInventory + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientInventory + 3, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamInventoryStartPurchaseResult_t : ICallbackData
	{
		internal Result Result; // m_result enum EResult
		internal ulong OrderID; // m_ulOrderID uint64
		internal ulong TransID; // m_ulTransID uint64
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamInventoryStartPurchaseResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientInventory + 4;
		internal static SteamInventoryStartPurchaseResult_t Fill( IntPtr p ) => ((SteamInventoryStartPurchaseResult_t)Marshal.PtrToStructure( p, typeof(SteamInventoryStartPurchaseResult_t) ) );
		
		static Action<SteamInventoryStartPurchaseResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamInventoryStartPurchaseResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamInventoryStartPurchaseResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientInventory + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientInventory + 4, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamInventoryRequestPricesResult_t : ICallbackData
	{
		internal Result Result; // m_result enum EResult
		internal string CurrencyUTF8() => System.Text.Encoding.UTF8.GetString( Currency, 0, System.Array.IndexOf<byte>( Currency, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] // byte[] m_rgchCurrency
		internal byte[] Currency; // m_rgchCurrency char [4]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamInventoryRequestPricesResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientInventory + 5;
		internal static SteamInventoryRequestPricesResult_t Fill( IntPtr p ) => ((SteamInventoryRequestPricesResult_t)Marshal.PtrToStructure( p, typeof(SteamInventoryRequestPricesResult_t) ) );
		
		static Action<SteamInventoryRequestPricesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamInventoryRequestPricesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamInventoryRequestPricesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientInventory + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientInventory + 5, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct BroadcastUploadStop_t : ICallbackData
	{
		internal BroadcastUploadResult Result; // m_eResult enum EBroadcastUploadResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(BroadcastUploadStop_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientVideo + 5;
		internal static BroadcastUploadStop_t Fill( IntPtr p ) => ((BroadcastUploadStop_t)Marshal.PtrToStructure( p, typeof(BroadcastUploadStop_t) ) );
		
		static Action<BroadcastUploadStop_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<BroadcastUploadStop_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<BroadcastUploadStop_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientVideo + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientVideo + 5, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GetVideoURLResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal AppId VideoAppID; // m_unVideoAppID AppId_t
		internal string URLUTF8() => System.Text.Encoding.UTF8.GetString( URL, 0, System.Array.IndexOf<byte>( URL, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // byte[] m_rgchURL
		internal byte[] URL; // m_rgchURL char [256]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GetVideoURLResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientVideo + 11;
		internal static GetVideoURLResult_t Fill( IntPtr p ) => ((GetVideoURLResult_t)Marshal.PtrToStructure( p, typeof(GetVideoURLResult_t) ) );
		
		static Action<GetVideoURLResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GetVideoURLResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GetVideoURLResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientVideo + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientVideo + 11, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GetOPFSettingsResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal AppId VideoAppID; // m_unVideoAppID AppId_t
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GetOPFSettingsResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientVideo + 24;
		internal static GetOPFSettingsResult_t Fill( IntPtr p ) => ((GetOPFSettingsResult_t)Marshal.PtrToStructure( p, typeof(GetOPFSettingsResult_t) ) );
		
		static Action<GetOPFSettingsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GetOPFSettingsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GetOPFSettingsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientVideo + 24, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientVideo + 24, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GSClientApprove_t : ICallbackData
	{
		internal ulong SteamID; // m_SteamID class CSteamID
		internal ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientApprove_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameServer + 1;
		internal static GSClientApprove_t Fill( IntPtr p ) => ((GSClientApprove_t)Marshal.PtrToStructure( p, typeof(GSClientApprove_t) ) );
		
		static Action<GSClientApprove_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSClientApprove_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSClientApprove_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameServer + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameServer + 1, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GSClientDeny_t : ICallbackData
	{
		internal ulong SteamID; // m_SteamID class CSteamID
		internal DenyReason DenyReason; // m_eDenyReason enum EDenyReason
		internal string OptionalTextUTF8() => System.Text.Encoding.UTF8.GetString( OptionalText, 0, System.Array.IndexOf<byte>( OptionalText, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)] // byte[] m_rgchOptionalText
		internal byte[] OptionalText; // m_rgchOptionalText char [128]
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientDeny_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameServer + 2;
		internal static GSClientDeny_t Fill( IntPtr p ) => ((GSClientDeny_t)Marshal.PtrToStructure( p, typeof(GSClientDeny_t) ) );
		
		static Action<GSClientDeny_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSClientDeny_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSClientDeny_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameServer + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameServer + 2, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GSClientKick_t : ICallbackData
	{
		internal ulong SteamID; // m_SteamID class CSteamID
		internal DenyReason DenyReason; // m_eDenyReason enum EDenyReason
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientKick_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameServer + 3;
		internal static GSClientKick_t Fill( IntPtr p ) => ((GSClientKick_t)Marshal.PtrToStructure( p, typeof(GSClientKick_t) ) );
		
		static Action<GSClientKick_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSClientKick_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSClientKick_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameServer + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameServer + 3, false );
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
		internal bool Unlocked; // m_bUnlocked _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientAchievementStatus_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameServer + 6;
		internal static GSClientAchievementStatus_t Fill( IntPtr p ) => ((GSClientAchievementStatus_t)Marshal.PtrToStructure( p, typeof(GSClientAchievementStatus_t) ) );
		
		static Action<GSClientAchievementStatus_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSClientAchievementStatus_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSClientAchievementStatus_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameServer + 6, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameServer + 6, false );
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
		public int CallbackId => CallbackIdentifiers.SteamUser + 15;
		internal static GSPolicyResponse_t Fill( IntPtr p ) => ((GSPolicyResponse_t)Marshal.PtrToStructure( p, typeof(GSPolicyResponse_t) ) );
		
		static Action<GSPolicyResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSPolicyResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSPolicyResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUser + 15, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUser + 15, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GSGameplayStats_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal int Rank; // m_nRank int32
		internal uint TotalConnects; // m_unTotalConnects uint32
		internal uint TotalMinutesPlayed; // m_unTotalMinutesPlayed uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSGameplayStats_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameServer + 7;
		internal static GSGameplayStats_t Fill( IntPtr p ) => ((GSGameplayStats_t)Marshal.PtrToStructure( p, typeof(GSGameplayStats_t) ) );
		
		static Action<GSGameplayStats_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSGameplayStats_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSGameplayStats_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameServer + 7, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameServer + 7, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GSClientGroupStatus_t : ICallbackData
	{
		internal ulong SteamIDUser; // m_SteamIDUser class CSteamID
		internal ulong SteamIDGroup; // m_SteamIDGroup class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool Member; // m_bMember _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool Officer; // m_bOfficer _Bool
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientGroupStatus_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameServer + 8;
		internal static GSClientGroupStatus_t Fill( IntPtr p ) => ((GSClientGroupStatus_t)Marshal.PtrToStructure( p, typeof(GSClientGroupStatus_t) ) );
		
		static Action<GSClientGroupStatus_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSClientGroupStatus_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSClientGroupStatus_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameServer + 8, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameServer + 8, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GSReputation_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal uint ReputationScore; // m_unReputationScore uint32
		[MarshalAs(UnmanagedType.I1)]
		internal bool Banned; // m_bBanned _Bool
		internal uint BannedIP; // m_unBannedIP uint32
		internal ushort BannedPort; // m_usBannedPort uint16
		internal ulong BannedGameID; // m_ulBannedGameID uint64
		internal uint BanExpires; // m_unBanExpires uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSReputation_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameServer + 9;
		internal static GSReputation_t Fill( IntPtr p ) => ((GSReputation_t)Marshal.PtrToStructure( p, typeof(GSReputation_t) ) );
		
		static Action<GSReputation_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSReputation_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSReputation_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameServer + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameServer + 9, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct AssociateWithClanResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(AssociateWithClanResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameServer + 10;
		internal static AssociateWithClanResult_t Fill( IntPtr p ) => ((AssociateWithClanResult_t)Marshal.PtrToStructure( p, typeof(AssociateWithClanResult_t) ) );
		
		static Action<AssociateWithClanResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<AssociateWithClanResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<AssociateWithClanResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameServer + 10, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameServer + 10, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct ComputeNewPlayerCompatibilityResult_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal int CPlayersThatDontLikeCandidate; // m_cPlayersThatDontLikeCandidate int
		internal int CPlayersThatCandidateDoesntLike; // m_cPlayersThatCandidateDoesntLike int
		internal int CClanPlayersThatDontLikeCandidate; // m_cClanPlayersThatDontLikeCandidate int
		internal ulong SteamIDCandidate; // m_SteamIDCandidate class CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ComputeNewPlayerCompatibilityResult_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameServer + 11;
		internal static ComputeNewPlayerCompatibilityResult_t Fill( IntPtr p ) => ((ComputeNewPlayerCompatibilityResult_t)Marshal.PtrToStructure( p, typeof(ComputeNewPlayerCompatibilityResult_t) ) );
		
		static Action<ComputeNewPlayerCompatibilityResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ComputeNewPlayerCompatibilityResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ComputeNewPlayerCompatibilityResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameServer + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameServer + 11, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GSStatsReceived_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSStatsReceived_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameServerStats + 0;
		internal static GSStatsReceived_t Fill( IntPtr p ) => ((GSStatsReceived_t)Marshal.PtrToStructure( p, typeof(GSStatsReceived_t) ) );
		
		static Action<GSStatsReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSStatsReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSStatsReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameServerStats + 0, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameServerStats + 0, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GSStatsStored_t : ICallbackData
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSStatsStored_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameServerStats + 1;
		internal static GSStatsStored_t Fill( IntPtr p ) => ((GSStatsStored_t)Marshal.PtrToStructure( p, typeof(GSStatsStored_t) ) );
		
		static Action<GSStatsStored_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSStatsStored_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSStatsStored_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameServerStats + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameServerStats + 1, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GSStatsUnloaded_t : ICallbackData
	{
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSStatsUnloaded_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUserStats + 8;
		internal static GSStatsUnloaded_t Fill( IntPtr p ) => ((GSStatsUnloaded_t)Marshal.PtrToStructure( p, typeof(GSStatsUnloaded_t) ) );
		
		static Action<GSStatsUnloaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSStatsUnloaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSStatsUnloaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUserStats + 8, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUserStats + 8, false );
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
		public int CallbackId => CallbackIdentifiers.SteamParties + 5;
		internal static AvailableBeaconLocationsUpdated_t Fill( IntPtr p ) => ((AvailableBeaconLocationsUpdated_t)Marshal.PtrToStructure( p, typeof(AvailableBeaconLocationsUpdated_t) ) );
		
		static Action<AvailableBeaconLocationsUpdated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<AvailableBeaconLocationsUpdated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<AvailableBeaconLocationsUpdated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamParties + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamParties + 5, false );
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
		public int CallbackId => CallbackIdentifiers.SteamParties + 6;
		internal static ActiveBeaconsUpdated_t Fill( IntPtr p ) => ((ActiveBeaconsUpdated_t)Marshal.PtrToStructure( p, typeof(ActiveBeaconsUpdated_t) ) );
		
		static Action<ActiveBeaconsUpdated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ActiveBeaconsUpdated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ActiveBeaconsUpdated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamParties + 6, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamParties + 6, false );
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
		public int CallbackId => CallbackIdentifiers.SteamMusic + 1;
		internal static PlaybackStatusHasChanged_t Fill( IntPtr p ) => ((PlaybackStatusHasChanged_t)Marshal.PtrToStructure( p, typeof(PlaybackStatusHasChanged_t) ) );
		
		static Action<PlaybackStatusHasChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<PlaybackStatusHasChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<PlaybackStatusHasChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamMusic + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamMusic + 1, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct BroadcastUploadStart_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(BroadcastUploadStart_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.ClientVideo + 4;
		internal static BroadcastUploadStart_t Fill( IntPtr p ) => ((BroadcastUploadStart_t)Marshal.PtrToStructure( p, typeof(BroadcastUploadStart_t) ) );
		
		static Action<BroadcastUploadStart_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<BroadcastUploadStart_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<BroadcastUploadStart_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientVideo + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientVideo + 4, false );
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
		public int CallbackId => CallbackIdentifiers.SteamApps + 14;
		internal static NewUrlLaunchParameters_t Fill( IntPtr p ) => ((NewUrlLaunchParameters_t)Marshal.PtrToStructure( p, typeof(NewUrlLaunchParameters_t) ) );
		
		static Action<NewUrlLaunchParameters_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<NewUrlLaunchParameters_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<NewUrlLaunchParameters_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamApps + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamApps + 14, false );
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
		public int CallbackId => CallbackIdentifiers.ClientUGC + 5;
		internal static ItemInstalled_t Fill( IntPtr p ) => ((ItemInstalled_t)Marshal.PtrToStructure( p, typeof(ItemInstalled_t) ) );
		
		static Action<ItemInstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ItemInstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ItemInstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientUGC + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientUGC + 5, false );
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
		public int CallbackId => CallbackIdentifiers.SteamNetworkingSockets + 1;
		internal static SteamNetConnectionStatusChangedCallback_t Fill( IntPtr p ) => ((SteamNetConnectionStatusChangedCallback_t)Marshal.PtrToStructure( p, typeof(SteamNetConnectionStatusChangedCallback_t) ) );
		
		static Action<SteamNetConnectionStatusChangedCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamNetConnectionStatusChangedCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamNetConnectionStatusChangedCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamNetworkingSockets + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamNetworkingSockets + 1, false );
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
		public int CallbackId => CallbackIdentifiers.ClientInventory + 2;
		internal static SteamInventoryDefinitionUpdate_t Fill( IntPtr p ) => ((SteamInventoryDefinitionUpdate_t)Marshal.PtrToStructure( p, typeof(SteamInventoryDefinitionUpdate_t) ) );
		
		static Action<SteamInventoryDefinitionUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamInventoryDefinitionUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamInventoryDefinitionUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.ClientInventory + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.ClientInventory + 2, false );
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
		public int CallbackId => CallbackIdentifiers.SteamParentalSettings + 1;
		internal static SteamParentalSettingsChanged_t Fill( IntPtr p ) => ((SteamParentalSettingsChanged_t)Marshal.PtrToStructure( p, typeof(SteamParentalSettingsChanged_t) ) );
		
		static Action<SteamParentalSettingsChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamParentalSettingsChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamParentalSettingsChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamParentalSettings + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamParentalSettings + 1, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamServersConnected_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamServersConnected_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUser + 1;
		internal static SteamServersConnected_t Fill( IntPtr p ) => ((SteamServersConnected_t)Marshal.PtrToStructure( p, typeof(SteamServersConnected_t) ) );
		
		static Action<SteamServersConnected_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamServersConnected_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamServersConnected_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUser + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUser + 1, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct NewLaunchQueryParameters_t
	{
		
		#region Marshalling
		internal static NewLaunchQueryParameters_t Fill( IntPtr p ) => ((NewLaunchQueryParameters_t)(NewLaunchQueryParameters_t) Marshal.PtrToStructure( p, typeof(NewLaunchQueryParameters_t) ) );
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GCMessageAvailable_t : ICallbackData
	{
		internal uint MessageSize; // m_nMessageSize uint32
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GCMessageAvailable_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameCoordinator + 1;
		internal static GCMessageAvailable_t Fill( IntPtr p ) => ((GCMessageAvailable_t)Marshal.PtrToStructure( p, typeof(GCMessageAvailable_t) ) );
		
		static Action<GCMessageAvailable_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GCMessageAvailable_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GCMessageAvailable_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameCoordinator + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameCoordinator + 1, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GCMessageFailed_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GCMessageFailed_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamGameCoordinator + 2;
		internal static GCMessageFailed_t Fill( IntPtr p ) => ((GCMessageFailed_t)Marshal.PtrToStructure( p, typeof(GCMessageFailed_t) ) );
		
		static Action<GCMessageFailed_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GCMessageFailed_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GCMessageFailed_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamGameCoordinator + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamGameCoordinator + 2, false );
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
		public int CallbackId => CallbackIdentifiers.SteamScreenshots + 2;
		internal static ScreenshotRequested_t Fill( IntPtr p ) => ((ScreenshotRequested_t)Marshal.PtrToStructure( p, typeof(ScreenshotRequested_t) ) );
		
		static Action<ScreenshotRequested_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ScreenshotRequested_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ScreenshotRequested_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamScreenshots + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamScreenshots + 2, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LicensesUpdated_t : ICallbackData
	{
		
		#region SteamCallback
		public static int _datasize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LicensesUpdated_t) );
		public int DataSize => _datasize;
		public int CallbackId => CallbackIdentifiers.SteamUser + 25;
		internal static LicensesUpdated_t Fill( IntPtr p ) => ((LicensesUpdated_t)Marshal.PtrToStructure( p, typeof(LicensesUpdated_t) ) );
		
		static Action<LicensesUpdated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LicensesUpdated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LicensesUpdated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUser + 25, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUser + 25, false );
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
		public int CallbackId => CallbackIdentifiers.SteamUtils + 4;
		internal static SteamShutdown_t Fill( IntPtr p ) => ((SteamShutdown_t)Marshal.PtrToStructure( p, typeof(SteamShutdown_t) ) );
		
		static Action<SteamShutdown_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamShutdown_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamShutdown_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUtils + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUtils + 4, false );
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
		public int CallbackId => CallbackIdentifiers.SteamUtils + 1;
		internal static IPCountry_t Fill( IntPtr p ) => ((IPCountry_t)Marshal.PtrToStructure( p, typeof(IPCountry_t) ) );
		
		static Action<IPCountry_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<IPCountry_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<IPCountry_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUtils + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUtils + 1, false );
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
		public int CallbackId => CallbackIdentifiers.SteamUser + 17;
		internal static IPCFailure_t Fill( IntPtr p ) => ((IPCFailure_t)Marshal.PtrToStructure( p, typeof(IPCFailure_t) ) );
		
		static Action<IPCFailure_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<IPCFailure_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<IPCFailure_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, _datasize, CallbackIdentifiers.SteamUser + 17, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, _datasize, CallbackIdentifiers.SteamUser + 17, false );
				actionClient = action;
			}
		}
		#endregion
	}
	
}
