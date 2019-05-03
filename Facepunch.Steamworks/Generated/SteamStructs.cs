using System;
using System.Runtime.InteropServices;
using System.Linq;
using Steamworks.Data;
using System.Threading.Tasks;

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
		internal static CallbackMsg_t Fill( IntPtr p ) => Config.PackSmall ? ((CallbackMsg_t)(CallbackMsg_t) Marshal.PtrToStructure( p, typeof(CallbackMsg_t) )) : ((CallbackMsg_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
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
	internal struct SteamServerConnectFailure_t
	{
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool StillRetrying; // m_bStillRetrying _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamServerConnectFailure_t) : typeof(Pack8) );
		internal static SteamServerConnectFailure_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamServerConnectFailure_t)(SteamServerConnectFailure_t) Marshal.PtrToStructure( p, typeof(SteamServerConnectFailure_t) )) : ((SteamServerConnectFailure_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SteamServerConnectFailure_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamServerConnectFailure_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamServerConnectFailure_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUser + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUser + 2, false );
				actionClient = action;
			}
		}
		public static async Task<SteamServerConnectFailure_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUser + 2, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct SteamServersDisconnected_t
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamServersDisconnected_t) : typeof(Pack8) );
		internal static SteamServersDisconnected_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamServersDisconnected_t)(SteamServersDisconnected_t) Marshal.PtrToStructure( p, typeof(SteamServersDisconnected_t) )) : ((SteamServersDisconnected_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SteamServersDisconnected_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamServersDisconnected_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamServersDisconnected_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUser + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUser + 3, false );
				actionClient = action;
			}
		}
		public static async Task<SteamServersDisconnected_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUser + 3, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct ClientGameServerDeny_t
	{
		internal uint AppID; // m_uAppID uint32
		internal uint GameServerIP; // m_unGameServerIP uint32
		internal ushort GameServerPort; // m_usGameServerPort uint16
		internal ushort Secure; // m_bSecure uint16
		internal uint Reason; // m_uReason uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(ClientGameServerDeny_t) : typeof(Pack8) );
		internal static ClientGameServerDeny_t Fill( IntPtr p ) => Config.PackSmall ? ((ClientGameServerDeny_t)(ClientGameServerDeny_t) Marshal.PtrToStructure( p, typeof(ClientGameServerDeny_t) )) : ((ClientGameServerDeny_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<ClientGameServerDeny_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ClientGameServerDeny_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ClientGameServerDeny_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUser + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUser + 13, false );
				actionClient = action;
			}
		}
		public static async Task<ClientGameServerDeny_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUser + 13, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct ValidateAuthTicketResponse_t
	{
		internal ulong SteamID; // m_SteamID class CSteamID
		internal AuthResponse AuthSessionResponse; // m_eAuthSessionResponse enum EAuthSessionResponse
		internal ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ValidateAuthTicketResponse_t) );
		internal static ValidateAuthTicketResponse_t Fill( IntPtr p ) => ((ValidateAuthTicketResponse_t)(ValidateAuthTicketResponse_t) Marshal.PtrToStructure( p, typeof(ValidateAuthTicketResponse_t) ) );
		
		static Action<ValidateAuthTicketResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ValidateAuthTicketResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ValidateAuthTicketResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUser + 43, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUser + 43, false );
				actionClient = action;
			}
		}
		public static async Task<ValidateAuthTicketResponse_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUser + 43, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct MicroTxnAuthorizationResponse_t
	{
		internal uint AppID; // m_unAppID uint32
		internal ulong OrderID; // m_ulOrderID uint64
		internal byte Authorized; // m_bAuthorized uint8
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(MicroTxnAuthorizationResponse_t) : typeof(Pack8) );
		internal static MicroTxnAuthorizationResponse_t Fill( IntPtr p ) => Config.PackSmall ? ((MicroTxnAuthorizationResponse_t)(MicroTxnAuthorizationResponse_t) Marshal.PtrToStructure( p, typeof(MicroTxnAuthorizationResponse_t) )) : ((MicroTxnAuthorizationResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<MicroTxnAuthorizationResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MicroTxnAuthorizationResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MicroTxnAuthorizationResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUser + 52, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUser + 52, false );
				actionClient = action;
			}
		}
		public static async Task<MicroTxnAuthorizationResponse_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUser + 52, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct EncryptedAppTicketResponse_t
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(EncryptedAppTicketResponse_t) : typeof(Pack8) );
		internal static EncryptedAppTicketResponse_t Fill( IntPtr p ) => Config.PackSmall ? ((EncryptedAppTicketResponse_t)(EncryptedAppTicketResponse_t) Marshal.PtrToStructure( p, typeof(EncryptedAppTicketResponse_t) )) : ((EncryptedAppTicketResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<EncryptedAppTicketResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<EncryptedAppTicketResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<EncryptedAppTicketResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUser + 54, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUser + 54, false );
				actionClient = action;
			}
		}
		public static async Task<EncryptedAppTicketResponse_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUser + 54, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct GetAuthSessionTicketResponse_t
	{
		internal uint AuthTicket; // m_hAuthTicket HAuthTicket
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GetAuthSessionTicketResponse_t) : typeof(Pack8) );
		internal static GetAuthSessionTicketResponse_t Fill( IntPtr p ) => Config.PackSmall ? ((GetAuthSessionTicketResponse_t)(GetAuthSessionTicketResponse_t) Marshal.PtrToStructure( p, typeof(GetAuthSessionTicketResponse_t) )) : ((GetAuthSessionTicketResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<GetAuthSessionTicketResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GetAuthSessionTicketResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GetAuthSessionTicketResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUser + 63, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUser + 63, false );
				actionClient = action;
			}
		}
		public static async Task<GetAuthSessionTicketResponse_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUser + 63, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct GameWebCallback_t
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string URL; // m_szURL char [256]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GameWebCallback_t) : typeof(Pack8) );
		internal static GameWebCallback_t Fill( IntPtr p ) => Config.PackSmall ? ((GameWebCallback_t)(GameWebCallback_t) Marshal.PtrToStructure( p, typeof(GameWebCallback_t) )) : ((GameWebCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<GameWebCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameWebCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameWebCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUser + 64, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUser + 64, false );
				actionClient = action;
			}
		}
		public static async Task<GameWebCallback_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUser + 64, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct StoreAuthURLResponse_t
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
		internal string URL; // m_szURL char [512]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(StoreAuthURLResponse_t) : typeof(Pack8) );
		internal static StoreAuthURLResponse_t Fill( IntPtr p ) => Config.PackSmall ? ((StoreAuthURLResponse_t)(StoreAuthURLResponse_t) Marshal.PtrToStructure( p, typeof(StoreAuthURLResponse_t) )) : ((StoreAuthURLResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<StoreAuthURLResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<StoreAuthURLResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<StoreAuthURLResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUser + 65, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUser + 65, false );
				actionClient = action;
			}
		}
		public static async Task<StoreAuthURLResponse_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUser + 65, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct MarketEligibilityResponse_t
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Allowed; // m_bAllowed _Bool
		internal MarketNotAllowedReasonFlags NotAllowedReason; // m_eNotAllowedReason enum EMarketNotAllowedReasonFlags
		internal uint TAllowedAtTime; // m_rtAllowedAtTime RTime32
		internal int CdaySteamGuardRequiredDays; // m_cdaySteamGuardRequiredDays int
		internal int CdayNewDeviceCooldown; // m_cdayNewDeviceCooldown int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(MarketEligibilityResponse_t) : typeof(Pack8) );
		internal static MarketEligibilityResponse_t Fill( IntPtr p ) => Config.PackSmall ? ((MarketEligibilityResponse_t)(MarketEligibilityResponse_t) Marshal.PtrToStructure( p, typeof(MarketEligibilityResponse_t) )) : ((MarketEligibilityResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<MarketEligibilityResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MarketEligibilityResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MarketEligibilityResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUser + 66, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUser + 66, false );
				actionClient = action;
			}
		}
		public static async Task<MarketEligibilityResponse_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUser + 66, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
		internal static FriendGameInfo_t Fill( IntPtr p ) => ((FriendGameInfo_t)(FriendGameInfo_t) Marshal.PtrToStructure( p, typeof(FriendGameInfo_t) ) );
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FriendSessionStateInfo_t
	{
		internal uint IOnlineSessionInstances; // m_uiOnlineSessionInstances uint32
		internal byte IPublishedToFriendsSessionInstance; // m_uiPublishedToFriendsSessionInstance uint8
		
		#region Marshalling
		internal static FriendSessionStateInfo_t Fill( IntPtr p ) => Config.PackSmall ? ((FriendSessionStateInfo_t)(FriendSessionStateInfo_t) Marshal.PtrToStructure( p, typeof(FriendSessionStateInfo_t) )) : ((FriendSessionStateInfo_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
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
	internal struct FriendStateChange_t
	{
		internal ulong SteamID; // m_ulSteamID uint64
		internal int ChangeFlags; // m_nChangeFlags int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(FriendStateChange_t) : typeof(Pack8) );
		internal static FriendStateChange_t Fill( IntPtr p ) => Config.PackSmall ? ((FriendStateChange_t)(FriendStateChange_t) Marshal.PtrToStructure( p, typeof(FriendStateChange_t) )) : ((FriendStateChange_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<FriendStateChange_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FriendStateChange_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FriendStateChange_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamFriends + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamFriends + 4, false );
				actionClient = action;
			}
		}
		public static async Task<FriendStateChange_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamFriends + 4, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct GameOverlayActivated_t
	{
		internal byte Active; // m_bActive uint8
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GameOverlayActivated_t) : typeof(Pack8) );
		internal static GameOverlayActivated_t Fill( IntPtr p ) => Config.PackSmall ? ((GameOverlayActivated_t)(GameOverlayActivated_t) Marshal.PtrToStructure( p, typeof(GameOverlayActivated_t) )) : ((GameOverlayActivated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<GameOverlayActivated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameOverlayActivated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameOverlayActivated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamFriends + 31, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamFriends + 31, false );
				actionClient = action;
			}
		}
		public static async Task<GameOverlayActivated_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamFriends + 31, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct GameServerChangeRequested_t
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		internal string Server; // m_rgchServer char [64]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		internal string Password; // m_rgchPassword char [64]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GameServerChangeRequested_t) : typeof(Pack8) );
		internal static GameServerChangeRequested_t Fill( IntPtr p ) => Config.PackSmall ? ((GameServerChangeRequested_t)(GameServerChangeRequested_t) Marshal.PtrToStructure( p, typeof(GameServerChangeRequested_t) )) : ((GameServerChangeRequested_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<GameServerChangeRequested_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameServerChangeRequested_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameServerChangeRequested_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamFriends + 32, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamFriends + 32, false );
				actionClient = action;
			}
		}
		public static async Task<GameServerChangeRequested_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamFriends + 32, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct GameLobbyJoinRequested_t
	{
		internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
		internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameLobbyJoinRequested_t) );
		internal static GameLobbyJoinRequested_t Fill( IntPtr p ) => ((GameLobbyJoinRequested_t)(GameLobbyJoinRequested_t) Marshal.PtrToStructure( p, typeof(GameLobbyJoinRequested_t) ) );
		
		static Action<GameLobbyJoinRequested_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameLobbyJoinRequested_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameLobbyJoinRequested_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamFriends + 33, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamFriends + 33, false );
				actionClient = action;
			}
		}
		public static async Task<GameLobbyJoinRequested_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamFriends + 33, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct AvatarImageLoaded_t
	{
		internal ulong SteamID; // m_steamID class CSteamID
		internal int Image; // m_iImage int
		internal int Wide; // m_iWide int
		internal int Tall; // m_iTall int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(AvatarImageLoaded_t) );
		internal static AvatarImageLoaded_t Fill( IntPtr p ) => ((AvatarImageLoaded_t)(AvatarImageLoaded_t) Marshal.PtrToStructure( p, typeof(AvatarImageLoaded_t) ) );
		
		static Action<AvatarImageLoaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<AvatarImageLoaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<AvatarImageLoaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamFriends + 34, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamFriends + 34, false );
				actionClient = action;
			}
		}
		public static async Task<AvatarImageLoaded_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamFriends + 34, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct ClanOfficerListResponse_t
	{
		internal ulong SteamIDClan; // m_steamIDClan class CSteamID
		internal int COfficers; // m_cOfficers int
		internal byte Success; // m_bSuccess uint8
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ClanOfficerListResponse_t) );
		internal static ClanOfficerListResponse_t Fill( IntPtr p ) => ((ClanOfficerListResponse_t)(ClanOfficerListResponse_t) Marshal.PtrToStructure( p, typeof(ClanOfficerListResponse_t) ) );
		
		static Action<ClanOfficerListResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ClanOfficerListResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ClanOfficerListResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamFriends + 35, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamFriends + 35, false );
				actionClient = action;
			}
		}
		public static async Task<ClanOfficerListResponse_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamFriends + 35, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FriendRichPresenceUpdate_t
	{
		internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendRichPresenceUpdate_t) );
		internal static FriendRichPresenceUpdate_t Fill( IntPtr p ) => ((FriendRichPresenceUpdate_t)(FriendRichPresenceUpdate_t) Marshal.PtrToStructure( p, typeof(FriendRichPresenceUpdate_t) ) );
		
		static Action<FriendRichPresenceUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FriendRichPresenceUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FriendRichPresenceUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamFriends + 36, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamFriends + 36, false );
				actionClient = action;
			}
		}
		public static async Task<FriendRichPresenceUpdate_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamFriends + 36, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameRichPresenceJoinRequested_t
	{
		internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string Connect; // m_rgchConnect char [256]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameRichPresenceJoinRequested_t) );
		internal static GameRichPresenceJoinRequested_t Fill( IntPtr p ) => ((GameRichPresenceJoinRequested_t)(GameRichPresenceJoinRequested_t) Marshal.PtrToStructure( p, typeof(GameRichPresenceJoinRequested_t) ) );
		
		static Action<GameRichPresenceJoinRequested_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameRichPresenceJoinRequested_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameRichPresenceJoinRequested_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamFriends + 37, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamFriends + 37, false );
				actionClient = action;
			}
		}
		public static async Task<GameRichPresenceJoinRequested_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamFriends + 37, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameConnectedClanChatMsg_t
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		internal int MessageID; // m_iMessageID int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameConnectedClanChatMsg_t) );
		internal static GameConnectedClanChatMsg_t Fill( IntPtr p ) => ((GameConnectedClanChatMsg_t)(GameConnectedClanChatMsg_t) Marshal.PtrToStructure( p, typeof(GameConnectedClanChatMsg_t) ) );
		
		static Action<GameConnectedClanChatMsg_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameConnectedClanChatMsg_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameConnectedClanChatMsg_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamFriends + 38, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamFriends + 38, false );
				actionClient = action;
			}
		}
		public static async Task<GameConnectedClanChatMsg_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamFriends + 38, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameConnectedChatJoin_t
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameConnectedChatJoin_t) );
		internal static GameConnectedChatJoin_t Fill( IntPtr p ) => ((GameConnectedChatJoin_t)(GameConnectedChatJoin_t) Marshal.PtrToStructure( p, typeof(GameConnectedChatJoin_t) ) );
		
		static Action<GameConnectedChatJoin_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameConnectedChatJoin_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameConnectedChatJoin_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamFriends + 39, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamFriends + 39, false );
				actionClient = action;
			}
		}
		public static async Task<GameConnectedChatJoin_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamFriends + 39, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameConnectedChatLeave_t
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool Kicked; // m_bKicked _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool Dropped; // m_bDropped _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameConnectedChatLeave_t) );
		internal static GameConnectedChatLeave_t Fill( IntPtr p ) => ((GameConnectedChatLeave_t)(GameConnectedChatLeave_t) Marshal.PtrToStructure( p, typeof(GameConnectedChatLeave_t) ) );
		
		static Action<GameConnectedChatLeave_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameConnectedChatLeave_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameConnectedChatLeave_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamFriends + 40, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamFriends + 40, false );
				actionClient = action;
			}
		}
		public static async Task<GameConnectedChatLeave_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamFriends + 40, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct DownloadClanActivityCountsResult_t
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Success; // m_bSuccess _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(DownloadClanActivityCountsResult_t) : typeof(Pack8) );
		internal static DownloadClanActivityCountsResult_t Fill( IntPtr p ) => Config.PackSmall ? ((DownloadClanActivityCountsResult_t)(DownloadClanActivityCountsResult_t) Marshal.PtrToStructure( p, typeof(DownloadClanActivityCountsResult_t) )) : ((DownloadClanActivityCountsResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<DownloadClanActivityCountsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<DownloadClanActivityCountsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<DownloadClanActivityCountsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamFriends + 41, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamFriends + 41, false );
				actionClient = action;
			}
		}
		public static async Task<DownloadClanActivityCountsResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamFriends + 41, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct JoinClanChatRoomCompletionResult_t
	{
		internal ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		internal RoomEnter ChatRoomEnterResponse; // m_eChatRoomEnterResponse enum EChatRoomEnterResponse
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(JoinClanChatRoomCompletionResult_t) );
		internal static JoinClanChatRoomCompletionResult_t Fill( IntPtr p ) => ((JoinClanChatRoomCompletionResult_t)(JoinClanChatRoomCompletionResult_t) Marshal.PtrToStructure( p, typeof(JoinClanChatRoomCompletionResult_t) ) );
		
		static Action<JoinClanChatRoomCompletionResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<JoinClanChatRoomCompletionResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<JoinClanChatRoomCompletionResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamFriends + 42, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamFriends + 42, false );
				actionClient = action;
			}
		}
		public static async Task<JoinClanChatRoomCompletionResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamFriends + 42, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameConnectedFriendChatMsg_t
	{
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		internal int MessageID; // m_iMessageID int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameConnectedFriendChatMsg_t) );
		internal static GameConnectedFriendChatMsg_t Fill( IntPtr p ) => ((GameConnectedFriendChatMsg_t)(GameConnectedFriendChatMsg_t) Marshal.PtrToStructure( p, typeof(GameConnectedFriendChatMsg_t) ) );
		
		static Action<GameConnectedFriendChatMsg_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GameConnectedFriendChatMsg_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GameConnectedFriendChatMsg_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamFriends + 43, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamFriends + 43, false );
				actionClient = action;
			}
		}
		public static async Task<GameConnectedFriendChatMsg_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamFriends + 43, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FriendsGetFollowerCount_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamID; // m_steamID class CSteamID
		internal int Count; // m_nCount int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendsGetFollowerCount_t) );
		internal static FriendsGetFollowerCount_t Fill( IntPtr p ) => ((FriendsGetFollowerCount_t)(FriendsGetFollowerCount_t) Marshal.PtrToStructure( p, typeof(FriendsGetFollowerCount_t) ) );
		
		static Action<FriendsGetFollowerCount_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FriendsGetFollowerCount_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FriendsGetFollowerCount_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamFriends + 44, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamFriends + 44, false );
				actionClient = action;
			}
		}
		public static async Task<FriendsGetFollowerCount_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamFriends + 44, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FriendsIsFollowing_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamID; // m_steamID class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool IsFollowing; // m_bIsFollowing _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendsIsFollowing_t) );
		internal static FriendsIsFollowing_t Fill( IntPtr p ) => ((FriendsIsFollowing_t)(FriendsIsFollowing_t) Marshal.PtrToStructure( p, typeof(FriendsIsFollowing_t) ) );
		
		static Action<FriendsIsFollowing_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FriendsIsFollowing_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FriendsIsFollowing_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamFriends + 45, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamFriends + 45, false );
				actionClient = action;
			}
		}
		public static async Task<FriendsIsFollowing_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamFriends + 45, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FriendsEnumerateFollowingList_t
	{
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal ulong[] GSteamID; // m_rgSteamID class CSteamID [50]
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendsEnumerateFollowingList_t) );
		internal static FriendsEnumerateFollowingList_t Fill( IntPtr p ) => ((FriendsEnumerateFollowingList_t)(FriendsEnumerateFollowingList_t) Marshal.PtrToStructure( p, typeof(FriendsEnumerateFollowingList_t) ) );
		
		static Action<FriendsEnumerateFollowingList_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FriendsEnumerateFollowingList_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FriendsEnumerateFollowingList_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamFriends + 46, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamFriends + 46, false );
				actionClient = action;
			}
		}
		public static async Task<FriendsEnumerateFollowingList_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamFriends + 46, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SetPersonaNameResponse_t
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Success; // m_bSuccess _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool LocalSuccess; // m_bLocalSuccess _Bool
		internal Result Result; // m_result enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SetPersonaNameResponse_t) : typeof(Pack8) );
		internal static SetPersonaNameResponse_t Fill( IntPtr p ) => Config.PackSmall ? ((SetPersonaNameResponse_t)(SetPersonaNameResponse_t) Marshal.PtrToStructure( p, typeof(SetPersonaNameResponse_t) )) : ((SetPersonaNameResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SetPersonaNameResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SetPersonaNameResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SetPersonaNameResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamFriends + 47, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamFriends + 47, false );
				actionClient = action;
			}
		}
		public static async Task<SetPersonaNameResponse_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamFriends + 47, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct LowBatteryPower_t
	{
		internal byte MinutesBatteryLeft; // m_nMinutesBatteryLeft uint8
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LowBatteryPower_t) : typeof(Pack8) );
		internal static LowBatteryPower_t Fill( IntPtr p ) => Config.PackSmall ? ((LowBatteryPower_t)(LowBatteryPower_t) Marshal.PtrToStructure( p, typeof(LowBatteryPower_t) )) : ((LowBatteryPower_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<LowBatteryPower_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LowBatteryPower_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LowBatteryPower_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUtils + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUtils + 2, false );
				actionClient = action;
			}
		}
		public static async Task<LowBatteryPower_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUtils + 2, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct SteamAPICallCompleted_t
	{
		internal ulong AsyncCall; // m_hAsyncCall SteamAPICall_t
		internal int Callback; // m_iCallback int
		internal uint ParamCount; // m_cubParam uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamAPICallCompleted_t) : typeof(Pack8) );
		internal static SteamAPICallCompleted_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamAPICallCompleted_t)(SteamAPICallCompleted_t) Marshal.PtrToStructure( p, typeof(SteamAPICallCompleted_t) )) : ((SteamAPICallCompleted_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SteamAPICallCompleted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamAPICallCompleted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamAPICallCompleted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUtils + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUtils + 3, false );
				actionClient = action;
			}
		}
		public static async Task<SteamAPICallCompleted_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUtils + 3, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct CheckFileSignature_t
	{
		internal CheckFileSignature CheckFileSignature; // m_eCheckFileSignature enum ECheckFileSignature
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(CheckFileSignature_t) : typeof(Pack8) );
		internal static CheckFileSignature_t Fill( IntPtr p ) => Config.PackSmall ? ((CheckFileSignature_t)(CheckFileSignature_t) Marshal.PtrToStructure( p, typeof(CheckFileSignature_t) )) : ((CheckFileSignature_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<CheckFileSignature_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<CheckFileSignature_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<CheckFileSignature_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUtils + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUtils + 5, false );
				actionClient = action;
			}
		}
		public static async Task<CheckFileSignature_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUtils + 5, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct GamepadTextInputDismissed_t
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Submitted; // m_bSubmitted _Bool
		internal uint SubmittedText; // m_unSubmittedText uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GamepadTextInputDismissed_t) : typeof(Pack8) );
		internal static GamepadTextInputDismissed_t Fill( IntPtr p ) => Config.PackSmall ? ((GamepadTextInputDismissed_t)(GamepadTextInputDismissed_t) Marshal.PtrToStructure( p, typeof(GamepadTextInputDismissed_t) )) : ((GamepadTextInputDismissed_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<GamepadTextInputDismissed_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GamepadTextInputDismissed_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GamepadTextInputDismissed_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUtils + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUtils + 14, false );
				actionClient = action;
			}
		}
		public static async Task<GamepadTextInputDismissed_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUtils + 14, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
		internal static MatchMakingKeyValuePair_t Fill( IntPtr p ) => Config.PackSmall ? ((MatchMakingKeyValuePair_t)(MatchMakingKeyValuePair_t) Marshal.PtrToStructure( p, typeof(MatchMakingKeyValuePair_t) )) : ((MatchMakingKeyValuePair_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
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
		internal static servernetadr_t Fill( IntPtr p ) => Config.PackSmall ? ((servernetadr_t)(servernetadr_t) Marshal.PtrToStructure( p, typeof(servernetadr_t) )) : ((servernetadr_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
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
		internal static gameserveritem_t Fill( IntPtr p ) => ((gameserveritem_t)(gameserveritem_t) Marshal.PtrToStructure( p, typeof(gameserveritem_t) ) );
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamPartyBeaconLocation_t
	{
		internal SteamPartyBeaconLocationType Type; // m_eType enum ESteamPartyBeaconLocationType
		internal ulong LocationID; // m_ulLocationID uint64
		
		#region Marshalling
		internal static SteamPartyBeaconLocation_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamPartyBeaconLocation_t)(SteamPartyBeaconLocation_t) Marshal.PtrToStructure( p, typeof(SteamPartyBeaconLocation_t) )) : ((SteamPartyBeaconLocation_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
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
	internal struct FavoritesListChanged_t
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(FavoritesListChanged_t) : typeof(Pack8) );
		internal static FavoritesListChanged_t Fill( IntPtr p ) => Config.PackSmall ? ((FavoritesListChanged_t)(FavoritesListChanged_t) Marshal.PtrToStructure( p, typeof(FavoritesListChanged_t) )) : ((FavoritesListChanged_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<FavoritesListChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FavoritesListChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FavoritesListChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMatchmaking + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMatchmaking + 2, false );
				actionClient = action;
			}
		}
		public static async Task<FavoritesListChanged_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMatchmaking + 2, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct LobbyInvite_t
	{
		internal ulong SteamIDUser; // m_ulSteamIDUser uint64
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong GameID; // m_ulGameID uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LobbyInvite_t) : typeof(Pack8) );
		internal static LobbyInvite_t Fill( IntPtr p ) => Config.PackSmall ? ((LobbyInvite_t)(LobbyInvite_t) Marshal.PtrToStructure( p, typeof(LobbyInvite_t) )) : ((LobbyInvite_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<LobbyInvite_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyInvite_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyInvite_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMatchmaking + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMatchmaking + 3, false );
				actionClient = action;
			}
		}
		public static async Task<LobbyInvite_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMatchmaking + 3, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct LobbyEnter_t
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal uint GfChatPermissions; // m_rgfChatPermissions uint32
		[MarshalAs(UnmanagedType.I1)]
		internal bool Locked; // m_bLocked _Bool
		internal uint EChatRoomEnterResponse; // m_EChatRoomEnterResponse uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LobbyEnter_t) : typeof(Pack8) );
		internal static LobbyEnter_t Fill( IntPtr p ) => Config.PackSmall ? ((LobbyEnter_t)(LobbyEnter_t) Marshal.PtrToStructure( p, typeof(LobbyEnter_t) )) : ((LobbyEnter_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<LobbyEnter_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyEnter_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyEnter_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMatchmaking + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMatchmaking + 4, false );
				actionClient = action;
			}
		}
		public static async Task<LobbyEnter_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMatchmaking + 4, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct LobbyDataUpdate_t
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDMember; // m_ulSteamIDMember uint64
		internal byte Success; // m_bSuccess uint8
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LobbyDataUpdate_t) : typeof(Pack8) );
		internal static LobbyDataUpdate_t Fill( IntPtr p ) => Config.PackSmall ? ((LobbyDataUpdate_t)(LobbyDataUpdate_t) Marshal.PtrToStructure( p, typeof(LobbyDataUpdate_t) )) : ((LobbyDataUpdate_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<LobbyDataUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyDataUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyDataUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMatchmaking + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMatchmaking + 5, false );
				actionClient = action;
			}
		}
		public static async Task<LobbyDataUpdate_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMatchmaking + 5, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct LobbyChatUpdate_t
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDUserChanged; // m_ulSteamIDUserChanged uint64
		internal ulong SteamIDMakingChange; // m_ulSteamIDMakingChange uint64
		internal uint GfChatMemberStateChange; // m_rgfChatMemberStateChange uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LobbyChatUpdate_t) : typeof(Pack8) );
		internal static LobbyChatUpdate_t Fill( IntPtr p ) => Config.PackSmall ? ((LobbyChatUpdate_t)(LobbyChatUpdate_t) Marshal.PtrToStructure( p, typeof(LobbyChatUpdate_t) )) : ((LobbyChatUpdate_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<LobbyChatUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyChatUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyChatUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMatchmaking + 6, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMatchmaking + 6, false );
				actionClient = action;
			}
		}
		public static async Task<LobbyChatUpdate_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMatchmaking + 6, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct LobbyChatMsg_t
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDUser; // m_ulSteamIDUser uint64
		internal byte ChatEntryType; // m_eChatEntryType uint8
		internal uint ChatID; // m_iChatID uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LobbyChatMsg_t) : typeof(Pack8) );
		internal static LobbyChatMsg_t Fill( IntPtr p ) => Config.PackSmall ? ((LobbyChatMsg_t)(LobbyChatMsg_t) Marshal.PtrToStructure( p, typeof(LobbyChatMsg_t) )) : ((LobbyChatMsg_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<LobbyChatMsg_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyChatMsg_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyChatMsg_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMatchmaking + 7, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMatchmaking + 7, false );
				actionClient = action;
			}
		}
		public static async Task<LobbyChatMsg_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMatchmaking + 7, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct LobbyGameCreated_t
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDGameServer; // m_ulSteamIDGameServer uint64
		internal uint IP; // m_unIP uint32
		internal ushort Port; // m_usPort uint16
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LobbyGameCreated_t) : typeof(Pack8) );
		internal static LobbyGameCreated_t Fill( IntPtr p ) => Config.PackSmall ? ((LobbyGameCreated_t)(LobbyGameCreated_t) Marshal.PtrToStructure( p, typeof(LobbyGameCreated_t) )) : ((LobbyGameCreated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<LobbyGameCreated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyGameCreated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyGameCreated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMatchmaking + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMatchmaking + 9, false );
				actionClient = action;
			}
		}
		public static async Task<LobbyGameCreated_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMatchmaking + 9, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct LobbyMatchList_t
	{
		internal uint LobbiesMatching; // m_nLobbiesMatching uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LobbyMatchList_t) : typeof(Pack8) );
		internal static LobbyMatchList_t Fill( IntPtr p ) => Config.PackSmall ? ((LobbyMatchList_t)(LobbyMatchList_t) Marshal.PtrToStructure( p, typeof(LobbyMatchList_t) )) : ((LobbyMatchList_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<LobbyMatchList_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyMatchList_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyMatchList_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMatchmaking + 10, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMatchmaking + 10, false );
				actionClient = action;
			}
		}
		public static async Task<LobbyMatchList_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMatchmaking + 10, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct LobbyKicked_t
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDAdmin; // m_ulSteamIDAdmin uint64
		internal byte KickedDueToDisconnect; // m_bKickedDueToDisconnect uint8
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LobbyKicked_t) : typeof(Pack8) );
		internal static LobbyKicked_t Fill( IntPtr p ) => Config.PackSmall ? ((LobbyKicked_t)(LobbyKicked_t) Marshal.PtrToStructure( p, typeof(LobbyKicked_t) )) : ((LobbyKicked_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<LobbyKicked_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyKicked_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyKicked_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMatchmaking + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMatchmaking + 12, false );
				actionClient = action;
			}
		}
		public static async Task<LobbyKicked_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMatchmaking + 12, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct LobbyCreated_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LobbyCreated_t) : typeof(Pack8) );
		internal static LobbyCreated_t Fill( IntPtr p ) => Config.PackSmall ? ((LobbyCreated_t)(LobbyCreated_t) Marshal.PtrToStructure( p, typeof(LobbyCreated_t) )) : ((LobbyCreated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<LobbyCreated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LobbyCreated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LobbyCreated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMatchmaking + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMatchmaking + 13, false );
				actionClient = action;
			}
		}
		public static async Task<LobbyCreated_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMatchmaking + 13, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct PSNGameBootInviteResult_t
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool GameBootInviteExists; // m_bGameBootInviteExists _Bool
		internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(PSNGameBootInviteResult_t) );
		internal static PSNGameBootInviteResult_t Fill( IntPtr p ) => ((PSNGameBootInviteResult_t)(PSNGameBootInviteResult_t) Marshal.PtrToStructure( p, typeof(PSNGameBootInviteResult_t) ) );
		
		static Action<PSNGameBootInviteResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<PSNGameBootInviteResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<PSNGameBootInviteResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMatchmaking + 15, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMatchmaking + 15, false );
				actionClient = action;
			}
		}
		public static async Task<PSNGameBootInviteResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMatchmaking + 15, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FavoritesListAccountsUpdated_t
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(FavoritesListAccountsUpdated_t) : typeof(Pack8) );
		internal static FavoritesListAccountsUpdated_t Fill( IntPtr p ) => Config.PackSmall ? ((FavoritesListAccountsUpdated_t)(FavoritesListAccountsUpdated_t) Marshal.PtrToStructure( p, typeof(FavoritesListAccountsUpdated_t) )) : ((FavoritesListAccountsUpdated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<FavoritesListAccountsUpdated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FavoritesListAccountsUpdated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FavoritesListAccountsUpdated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMatchmaking + 16, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMatchmaking + 16, false );
				actionClient = action;
			}
		}
		public static async Task<FavoritesListAccountsUpdated_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMatchmaking + 16, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct SearchForGameProgressCallback_t
	{
		internal ulong LSearchID; // m_ullSearchID uint64
		internal Result Result; // m_eResult enum EResult
		internal ulong LobbyID; // m_lobbyID class CSteamID
		internal ulong SteamIDEndedSearch; // m_steamIDEndedSearch class CSteamID
		internal int SecondsRemainingEstimate; // m_nSecondsRemainingEstimate int32
		internal int CPlayersSearching; // m_cPlayersSearching int32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SearchForGameProgressCallback_t) );
		internal static SearchForGameProgressCallback_t Fill( IntPtr p ) => ((SearchForGameProgressCallback_t)(SearchForGameProgressCallback_t) Marshal.PtrToStructure( p, typeof(SearchForGameProgressCallback_t) ) );
		
		static Action<SearchForGameProgressCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SearchForGameProgressCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SearchForGameProgressCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameSearch + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameSearch + 1, false );
				actionClient = action;
			}
		}
		public static async Task<SearchForGameProgressCallback_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameSearch + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SearchForGameResultCallback_t
	{
		internal ulong LSearchID; // m_ullSearchID uint64
		internal Result Result; // m_eResult enum EResult
		internal int CountPlayersInGame; // m_nCountPlayersInGame int32
		internal int CountAcceptedGame; // m_nCountAcceptedGame int32
		internal ulong SteamIDHost; // m_steamIDHost class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool FinalCallback; // m_bFinalCallback _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SearchForGameResultCallback_t) );
		internal static SearchForGameResultCallback_t Fill( IntPtr p ) => ((SearchForGameResultCallback_t)(SearchForGameResultCallback_t) Marshal.PtrToStructure( p, typeof(SearchForGameResultCallback_t) ) );
		
		static Action<SearchForGameResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SearchForGameResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SearchForGameResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameSearch + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameSearch + 2, false );
				actionClient = action;
			}
		}
		public static async Task<SearchForGameResultCallback_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameSearch + 2, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RequestPlayersForGameProgressCallback_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong LSearchID; // m_ullSearchID uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RequestPlayersForGameProgressCallback_t) : typeof(Pack8) );
		internal static RequestPlayersForGameProgressCallback_t Fill( IntPtr p ) => Config.PackSmall ? ((RequestPlayersForGameProgressCallback_t)(RequestPlayersForGameProgressCallback_t) Marshal.PtrToStructure( p, typeof(RequestPlayersForGameProgressCallback_t) )) : ((RequestPlayersForGameProgressCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RequestPlayersForGameProgressCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RequestPlayersForGameProgressCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RequestPlayersForGameProgressCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameSearch + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameSearch + 11, false );
				actionClient = action;
			}
		}
		public static async Task<RequestPlayersForGameProgressCallback_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameSearch + 11, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RequestPlayersForGameResultCallback_t
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RequestPlayersForGameResultCallback_t) );
		internal static RequestPlayersForGameResultCallback_t Fill( IntPtr p ) => ((RequestPlayersForGameResultCallback_t)(RequestPlayersForGameResultCallback_t) Marshal.PtrToStructure( p, typeof(RequestPlayersForGameResultCallback_t) ) );
		
		static Action<RequestPlayersForGameResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RequestPlayersForGameResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RequestPlayersForGameResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameSearch + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameSearch + 12, false );
				actionClient = action;
			}
		}
		public static async Task<RequestPlayersForGameResultCallback_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameSearch + 12, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RequestPlayersForGameFinalResultCallback_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong LSearchID; // m_ullSearchID uint64
		internal ulong LUniqueGameID; // m_ullUniqueGameID uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RequestPlayersForGameFinalResultCallback_t) : typeof(Pack8) );
		internal static RequestPlayersForGameFinalResultCallback_t Fill( IntPtr p ) => Config.PackSmall ? ((RequestPlayersForGameFinalResultCallback_t)(RequestPlayersForGameFinalResultCallback_t) Marshal.PtrToStructure( p, typeof(RequestPlayersForGameFinalResultCallback_t) )) : ((RequestPlayersForGameFinalResultCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RequestPlayersForGameFinalResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RequestPlayersForGameFinalResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RequestPlayersForGameFinalResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameSearch + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameSearch + 13, false );
				actionClient = action;
			}
		}
		public static async Task<RequestPlayersForGameFinalResultCallback_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameSearch + 13, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct SubmitPlayerResultResultCallback_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong UllUniqueGameID; // ullUniqueGameID uint64
		internal ulong SteamIDPlayer; // steamIDPlayer class CSteamID
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SubmitPlayerResultResultCallback_t) );
		internal static SubmitPlayerResultResultCallback_t Fill( IntPtr p ) => ((SubmitPlayerResultResultCallback_t)(SubmitPlayerResultResultCallback_t) Marshal.PtrToStructure( p, typeof(SubmitPlayerResultResultCallback_t) ) );
		
		static Action<SubmitPlayerResultResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SubmitPlayerResultResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SubmitPlayerResultResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameSearch + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameSearch + 14, false );
				actionClient = action;
			}
		}
		public static async Task<SubmitPlayerResultResultCallback_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameSearch + 14, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct EndGameResultCallback_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong UllUniqueGameID; // ullUniqueGameID uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(EndGameResultCallback_t) : typeof(Pack8) );
		internal static EndGameResultCallback_t Fill( IntPtr p ) => Config.PackSmall ? ((EndGameResultCallback_t)(EndGameResultCallback_t) Marshal.PtrToStructure( p, typeof(EndGameResultCallback_t) )) : ((EndGameResultCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<EndGameResultCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<EndGameResultCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<EndGameResultCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameSearch + 15, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameSearch + 15, false );
				actionClient = action;
			}
		}
		public static async Task<EndGameResultCallback_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameSearch + 15, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct JoinPartyCallback_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
		internal ulong SteamIDBeaconOwner; // m_SteamIDBeaconOwner class CSteamID
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string ConnectString; // m_rgchConnectString char [256]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(JoinPartyCallback_t) );
		internal static JoinPartyCallback_t Fill( IntPtr p ) => ((JoinPartyCallback_t)(JoinPartyCallback_t) Marshal.PtrToStructure( p, typeof(JoinPartyCallback_t) ) );
		
		static Action<JoinPartyCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<JoinPartyCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<JoinPartyCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamParties + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamParties + 1, false );
				actionClient = action;
			}
		}
		public static async Task<JoinPartyCallback_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamParties + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct CreateBeaconCallback_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(CreateBeaconCallback_t) : typeof(Pack8) );
		internal static CreateBeaconCallback_t Fill( IntPtr p ) => Config.PackSmall ? ((CreateBeaconCallback_t)(CreateBeaconCallback_t) Marshal.PtrToStructure( p, typeof(CreateBeaconCallback_t) )) : ((CreateBeaconCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<CreateBeaconCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<CreateBeaconCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<CreateBeaconCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamParties + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamParties + 2, false );
				actionClient = action;
			}
		}
		public static async Task<CreateBeaconCallback_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamParties + 2, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct ReservationNotificationCallback_t
	{
		internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
		internal ulong SteamIDJoiner; // m_steamIDJoiner class CSteamID
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ReservationNotificationCallback_t) );
		internal static ReservationNotificationCallback_t Fill( IntPtr p ) => ((ReservationNotificationCallback_t)(ReservationNotificationCallback_t) Marshal.PtrToStructure( p, typeof(ReservationNotificationCallback_t) ) );
		
		static Action<ReservationNotificationCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ReservationNotificationCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ReservationNotificationCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamParties + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamParties + 3, false );
				actionClient = action;
			}
		}
		public static async Task<ReservationNotificationCallback_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamParties + 3, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct ChangeNumOpenSlotsCallback_t
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(ChangeNumOpenSlotsCallback_t) : typeof(Pack8) );
		internal static ChangeNumOpenSlotsCallback_t Fill( IntPtr p ) => Config.PackSmall ? ((ChangeNumOpenSlotsCallback_t)(ChangeNumOpenSlotsCallback_t) Marshal.PtrToStructure( p, typeof(ChangeNumOpenSlotsCallback_t) )) : ((ChangeNumOpenSlotsCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<ChangeNumOpenSlotsCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ChangeNumOpenSlotsCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ChangeNumOpenSlotsCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamParties + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamParties + 4, false );
				actionClient = action;
			}
		}
		public static async Task<ChangeNumOpenSlotsCallback_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamParties + 4, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
		internal static SteamParamStringArray_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamParamStringArray_t)(SteamParamStringArray_t) Marshal.PtrToStructure( p, typeof(SteamParamStringArray_t) )) : ((SteamParamStringArray_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
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
	internal struct RemoteStorageAppSyncedClient_t
	{
		internal AppId AppID; // m_nAppID AppId_t
		internal Result Result; // m_eResult enum EResult
		internal int NumDownloads; // m_unNumDownloads int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageAppSyncedClient_t) : typeof(Pack8) );
		internal static RemoteStorageAppSyncedClient_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageAppSyncedClient_t)(RemoteStorageAppSyncedClient_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncedClient_t) )) : ((RemoteStorageAppSyncedClient_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageAppSyncedClient_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageAppSyncedClient_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageAppSyncedClient_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 1, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageAppSyncedClient_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageAppSyncedServer_t
	{
		internal AppId AppID; // m_nAppID AppId_t
		internal Result Result; // m_eResult enum EResult
		internal int NumUploads; // m_unNumUploads int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageAppSyncedServer_t) : typeof(Pack8) );
		internal static RemoteStorageAppSyncedServer_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageAppSyncedServer_t)(RemoteStorageAppSyncedServer_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncedServer_t) )) : ((RemoteStorageAppSyncedServer_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageAppSyncedServer_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageAppSyncedServer_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageAppSyncedServer_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 2, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageAppSyncedServer_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 2, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageAppSyncProgress_t
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string CurrentFile; // m_rgchCurrentFile char [260]
		internal AppId AppID; // m_nAppID AppId_t
		internal uint BytesTransferredThisChunk; // m_uBytesTransferredThisChunk uint32
		internal double DAppPercentComplete; // m_dAppPercentComplete double
		[MarshalAs(UnmanagedType.I1)]
		internal bool Uploading; // m_bUploading _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageAppSyncProgress_t) : typeof(Pack8) );
		internal static RemoteStorageAppSyncProgress_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageAppSyncProgress_t)(RemoteStorageAppSyncProgress_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncProgress_t) )) : ((RemoteStorageAppSyncProgress_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageAppSyncProgress_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageAppSyncProgress_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageAppSyncProgress_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 3, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageAppSyncProgress_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 3, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageAppSyncStatusCheck_t
	{
		internal AppId AppID; // m_nAppID AppId_t
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageAppSyncStatusCheck_t) : typeof(Pack8) );
		internal static RemoteStorageAppSyncStatusCheck_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageAppSyncStatusCheck_t)(RemoteStorageAppSyncStatusCheck_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncStatusCheck_t) )) : ((RemoteStorageAppSyncStatusCheck_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageAppSyncStatusCheck_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageAppSyncStatusCheck_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageAppSyncStatusCheck_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 5, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageAppSyncStatusCheck_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 5, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageFileShareResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong File; // m_hFile UGCHandle_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string Filename; // m_rgchFilename char [260]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageFileShareResult_t) : typeof(Pack8) );
		internal static RemoteStorageFileShareResult_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageFileShareResult_t)(RemoteStorageFileShareResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageFileShareResult_t) )) : ((RemoteStorageFileShareResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageFileShareResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageFileShareResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageFileShareResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 7, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 7, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageFileShareResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 7, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStoragePublishFileResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStoragePublishFileResult_t) : typeof(Pack8) );
		internal static RemoteStoragePublishFileResult_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStoragePublishFileResult_t)(RemoteStoragePublishFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishFileResult_t) )) : ((RemoteStoragePublishFileResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStoragePublishFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStoragePublishFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 9, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStoragePublishFileResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 9, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageDeletePublishedFileResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageDeletePublishedFileResult_t) : typeof(Pack8) );
		internal static RemoteStorageDeletePublishedFileResult_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageDeletePublishedFileResult_t)(RemoteStorageDeletePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageDeletePublishedFileResult_t) )) : ((RemoteStorageDeletePublishedFileResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageDeletePublishedFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageDeletePublishedFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageDeletePublishedFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 11, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageDeletePublishedFileResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 11, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageEnumerateUserPublishedFilesResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageEnumerateUserPublishedFilesResult_t) : typeof(Pack8) );
		internal static RemoteStorageEnumerateUserPublishedFilesResult_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageEnumerateUserPublishedFilesResult_t)(RemoteStorageEnumerateUserPublishedFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserPublishedFilesResult_t) )) : ((RemoteStorageEnumerateUserPublishedFilesResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageEnumerateUserPublishedFilesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageEnumerateUserPublishedFilesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumerateUserPublishedFilesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 12, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageEnumerateUserPublishedFilesResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 12, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageSubscribePublishedFileResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageSubscribePublishedFileResult_t) : typeof(Pack8) );
		internal static RemoteStorageSubscribePublishedFileResult_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageSubscribePublishedFileResult_t)(RemoteStorageSubscribePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageSubscribePublishedFileResult_t) )) : ((RemoteStorageSubscribePublishedFileResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageSubscribePublishedFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageSubscribePublishedFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageSubscribePublishedFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 13, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageSubscribePublishedFileResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 13, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageEnumerateUserSubscribedFilesResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
		internal uint[] GRTimeSubscribed; // m_rgRTimeSubscribed uint32 [50]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t) : typeof(Pack8) );
		internal static RemoteStorageEnumerateUserSubscribedFilesResult_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageEnumerateUserSubscribedFilesResult_t)(RemoteStorageEnumerateUserSubscribedFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t) )) : ((RemoteStorageEnumerateUserSubscribedFilesResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageEnumerateUserSubscribedFilesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageEnumerateUserSubscribedFilesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumerateUserSubscribedFilesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 14, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageEnumerateUserSubscribedFilesResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 14, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageUnsubscribePublishedFileResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageUnsubscribePublishedFileResult_t) : typeof(Pack8) );
		internal static RemoteStorageUnsubscribePublishedFileResult_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageUnsubscribePublishedFileResult_t)(RemoteStorageUnsubscribePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUnsubscribePublishedFileResult_t) )) : ((RemoteStorageUnsubscribePublishedFileResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageUnsubscribePublishedFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageUnsubscribePublishedFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageUnsubscribePublishedFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 15, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 15, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageUnsubscribePublishedFileResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 15, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageUpdatePublishedFileResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageUpdatePublishedFileResult_t) : typeof(Pack8) );
		internal static RemoteStorageUpdatePublishedFileResult_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageUpdatePublishedFileResult_t)(RemoteStorageUpdatePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUpdatePublishedFileResult_t) )) : ((RemoteStorageUpdatePublishedFileResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageUpdatePublishedFileResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageUpdatePublishedFileResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageUpdatePublishedFileResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 16, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 16, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageUpdatePublishedFileResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 16, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageDownloadUGCResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong File; // m_hFile UGCHandle_t
		internal AppId AppID; // m_nAppID AppId_t
		internal int SizeInBytes; // m_nSizeInBytes int32
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string PchFileName; // m_pchFileName char [260]
		internal ulong SteamIDOwner; // m_ulSteamIDOwner uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageDownloadUGCResult_t) : typeof(Pack8) );
		internal static RemoteStorageDownloadUGCResult_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageDownloadUGCResult_t)(RemoteStorageDownloadUGCResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageDownloadUGCResult_t) )) : ((RemoteStorageDownloadUGCResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageDownloadUGCResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageDownloadUGCResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageDownloadUGCResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 17, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 17, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageDownloadUGCResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 17, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageGetPublishedFileDetailsResult_t
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
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageGetPublishedFileDetailsResult_t) : typeof(Pack8) );
		internal static RemoteStorageGetPublishedFileDetailsResult_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageGetPublishedFileDetailsResult_t)(RemoteStorageGetPublishedFileDetailsResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageGetPublishedFileDetailsResult_t) )) : ((RemoteStorageGetPublishedFileDetailsResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageGetPublishedFileDetailsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageGetPublishedFileDetailsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageGetPublishedFileDetailsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 18, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 18, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageGetPublishedFileDetailsResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 18, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageEnumerateWorkshopFilesResult_t
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageEnumerateWorkshopFilesResult_t) : typeof(Pack8) );
		internal static RemoteStorageEnumerateWorkshopFilesResult_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageEnumerateWorkshopFilesResult_t)(RemoteStorageEnumerateWorkshopFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateWorkshopFilesResult_t) )) : ((RemoteStorageEnumerateWorkshopFilesResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageEnumerateWorkshopFilesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageEnumerateWorkshopFilesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumerateWorkshopFilesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 19, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 19, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageEnumerateWorkshopFilesResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 19, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageGetPublishedItemVoteDetailsResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_unPublishedFileId PublishedFileId_t
		internal int VotesFor; // m_nVotesFor int32
		internal int VotesAgainst; // m_nVotesAgainst int32
		internal int Reports; // m_nReports int32
		internal float FScore; // m_fScore float
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageGetPublishedItemVoteDetailsResult_t) : typeof(Pack8) );
		internal static RemoteStorageGetPublishedItemVoteDetailsResult_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageGetPublishedItemVoteDetailsResult_t)(RemoteStorageGetPublishedItemVoteDetailsResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageGetPublishedItemVoteDetailsResult_t) )) : ((RemoteStorageGetPublishedItemVoteDetailsResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageGetPublishedItemVoteDetailsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageGetPublishedItemVoteDetailsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageGetPublishedItemVoteDetailsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 20, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 20, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageGetPublishedItemVoteDetailsResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 20, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStoragePublishedFileSubscribed_t
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStoragePublishedFileSubscribed_t) : typeof(Pack8) );
		internal static RemoteStoragePublishedFileSubscribed_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStoragePublishedFileSubscribed_t)(RemoteStoragePublishedFileSubscribed_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileSubscribed_t) )) : ((RemoteStoragePublishedFileSubscribed_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStoragePublishedFileSubscribed_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStoragePublishedFileSubscribed_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishedFileSubscribed_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 21, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 21, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStoragePublishedFileSubscribed_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 21, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStoragePublishedFileUnsubscribed_t
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStoragePublishedFileUnsubscribed_t) : typeof(Pack8) );
		internal static RemoteStoragePublishedFileUnsubscribed_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStoragePublishedFileUnsubscribed_t)(RemoteStoragePublishedFileUnsubscribed_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileUnsubscribed_t) )) : ((RemoteStoragePublishedFileUnsubscribed_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStoragePublishedFileUnsubscribed_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStoragePublishedFileUnsubscribed_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishedFileUnsubscribed_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 22, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 22, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStoragePublishedFileUnsubscribed_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 22, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStoragePublishedFileDeleted_t
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStoragePublishedFileDeleted_t) : typeof(Pack8) );
		internal static RemoteStoragePublishedFileDeleted_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStoragePublishedFileDeleted_t)(RemoteStoragePublishedFileDeleted_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileDeleted_t) )) : ((RemoteStoragePublishedFileDeleted_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStoragePublishedFileDeleted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStoragePublishedFileDeleted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishedFileDeleted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 23, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 23, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStoragePublishedFileDeleted_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 23, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageUpdateUserPublishedItemVoteResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageUpdateUserPublishedItemVoteResult_t) : typeof(Pack8) );
		internal static RemoteStorageUpdateUserPublishedItemVoteResult_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageUpdateUserPublishedItemVoteResult_t)(RemoteStorageUpdateUserPublishedItemVoteResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUpdateUserPublishedItemVoteResult_t) )) : ((RemoteStorageUpdateUserPublishedItemVoteResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageUpdateUserPublishedItemVoteResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageUpdateUserPublishedItemVoteResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageUpdateUserPublishedItemVoteResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 24, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 24, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageUpdateUserPublishedItemVoteResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 24, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageUserVoteDetails_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal WorkshopVote Vote; // m_eVote enum EWorkshopVote
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageUserVoteDetails_t) : typeof(Pack8) );
		internal static RemoteStorageUserVoteDetails_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageUserVoteDetails_t)(RemoteStorageUserVoteDetails_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUserVoteDetails_t) )) : ((RemoteStorageUserVoteDetails_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageUserVoteDetails_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageUserVoteDetails_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageUserVoteDetails_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 25, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 25, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageUserVoteDetails_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 25, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) : typeof(Pack8) );
		internal static RemoteStorageEnumerateUserSharedWorkshopFilesResult_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageEnumerateUserSharedWorkshopFilesResult_t)(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) )) : ((RemoteStorageEnumerateUserSharedWorkshopFilesResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageEnumerateUserSharedWorkshopFilesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageEnumerateUserSharedWorkshopFilesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumerateUserSharedWorkshopFilesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 26, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 26, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageEnumerateUserSharedWorkshopFilesResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 26, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageSetUserPublishedFileActionResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageSetUserPublishedFileActionResult_t) : typeof(Pack8) );
		internal static RemoteStorageSetUserPublishedFileActionResult_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageSetUserPublishedFileActionResult_t)(RemoteStorageSetUserPublishedFileActionResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageSetUserPublishedFileActionResult_t) )) : ((RemoteStorageSetUserPublishedFileActionResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageSetUserPublishedFileActionResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageSetUserPublishedFileActionResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageSetUserPublishedFileActionResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 27, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 27, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageSetUserPublishedFileActionResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 27, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageEnumeratePublishedFilesByUserActionResult_t
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageEnumeratePublishedFilesByUserActionResult_t) : typeof(Pack8) );
		internal static RemoteStorageEnumeratePublishedFilesByUserActionResult_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageEnumeratePublishedFilesByUserActionResult_t)(RemoteStorageEnumeratePublishedFilesByUserActionResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumeratePublishedFilesByUserActionResult_t) )) : ((RemoteStorageEnumeratePublishedFilesByUserActionResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageEnumeratePublishedFilesByUserActionResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageEnumeratePublishedFilesByUserActionResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageEnumeratePublishedFilesByUserActionResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 28, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 28, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageEnumeratePublishedFilesByUserActionResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 28, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStoragePublishFileProgress_t
	{
		internal double DPercentFile; // m_dPercentFile double
		[MarshalAs(UnmanagedType.I1)]
		internal bool Preview; // m_bPreview _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStoragePublishFileProgress_t) : typeof(Pack8) );
		internal static RemoteStoragePublishFileProgress_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStoragePublishFileProgress_t)(RemoteStoragePublishFileProgress_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishFileProgress_t) )) : ((RemoteStoragePublishFileProgress_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStoragePublishFileProgress_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStoragePublishFileProgress_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishFileProgress_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 29, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 29, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStoragePublishFileProgress_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 29, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStoragePublishedFileUpdated_t
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		internal ulong Unused; // m_ulUnused uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStoragePublishedFileUpdated_t) : typeof(Pack8) );
		internal static RemoteStoragePublishedFileUpdated_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStoragePublishedFileUpdated_t)(RemoteStoragePublishedFileUpdated_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileUpdated_t) )) : ((RemoteStoragePublishedFileUpdated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStoragePublishedFileUpdated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStoragePublishedFileUpdated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStoragePublishedFileUpdated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 30, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 30, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStoragePublishedFileUpdated_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 30, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageFileWriteAsyncComplete_t
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageFileWriteAsyncComplete_t) : typeof(Pack8) );
		internal static RemoteStorageFileWriteAsyncComplete_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageFileWriteAsyncComplete_t)(RemoteStorageFileWriteAsyncComplete_t) Marshal.PtrToStructure( p, typeof(RemoteStorageFileWriteAsyncComplete_t) )) : ((RemoteStorageFileWriteAsyncComplete_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageFileWriteAsyncComplete_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageFileWriteAsyncComplete_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageFileWriteAsyncComplete_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 31, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 31, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageFileWriteAsyncComplete_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 31, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoteStorageFileReadAsyncComplete_t
	{
		internal ulong FileReadAsync; // m_hFileReadAsync SteamAPICall_t
		internal Result Result; // m_eResult enum EResult
		internal uint Offset; // m_nOffset uint32
		internal uint Read; // m_cubRead uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoteStorageFileReadAsyncComplete_t) : typeof(Pack8) );
		internal static RemoteStorageFileReadAsyncComplete_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoteStorageFileReadAsyncComplete_t)(RemoteStorageFileReadAsyncComplete_t) Marshal.PtrToStructure( p, typeof(RemoteStorageFileReadAsyncComplete_t) )) : ((RemoteStorageFileReadAsyncComplete_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoteStorageFileReadAsyncComplete_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoteStorageFileReadAsyncComplete_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoteStorageFileReadAsyncComplete_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientRemoteStorage + 32, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientRemoteStorage + 32, false );
				actionClient = action;
			}
		}
		public static async Task<RemoteStorageFileReadAsyncComplete_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientRemoteStorage + 32, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
		internal static LeaderboardEntry_t Fill( IntPtr p ) => Config.PackSmall ? ((LeaderboardEntry_t)(LeaderboardEntry_t) Marshal.PtrToStructure( p, typeof(LeaderboardEntry_t) )) : ((LeaderboardEntry_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
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
	internal struct UserStatsReceived_t
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserStatsReceived_t) );
		internal static UserStatsReceived_t Fill( IntPtr p ) => ((UserStatsReceived_t)(UserStatsReceived_t) Marshal.PtrToStructure( p, typeof(UserStatsReceived_t) ) );
		
		static Action<UserStatsReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UserStatsReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UserStatsReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUserStats + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUserStats + 1, false );
				actionClient = action;
			}
		}
		public static async Task<UserStatsReceived_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUserStats + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct UserStatsStored_t
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(UserStatsStored_t) : typeof(Pack8) );
		internal static UserStatsStored_t Fill( IntPtr p ) => Config.PackSmall ? ((UserStatsStored_t)(UserStatsStored_t) Marshal.PtrToStructure( p, typeof(UserStatsStored_t) )) : ((UserStatsStored_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<UserStatsStored_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UserStatsStored_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UserStatsStored_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUserStats + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUserStats + 2, false );
				actionClient = action;
			}
		}
		public static async Task<UserStatsStored_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUserStats + 2, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct UserAchievementStored_t
	{
		internal ulong GameID; // m_nGameID uint64
		[MarshalAs(UnmanagedType.I1)]
		internal bool GroupAchievement; // m_bGroupAchievement _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string AchievementName; // m_rgchAchievementName char [128]
		internal uint CurProgress; // m_nCurProgress uint32
		internal uint MaxProgress; // m_nMaxProgress uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(UserAchievementStored_t) : typeof(Pack8) );
		internal static UserAchievementStored_t Fill( IntPtr p ) => Config.PackSmall ? ((UserAchievementStored_t)(UserAchievementStored_t) Marshal.PtrToStructure( p, typeof(UserAchievementStored_t) )) : ((UserAchievementStored_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<UserAchievementStored_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UserAchievementStored_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UserAchievementStored_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUserStats + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUserStats + 3, false );
				actionClient = action;
			}
		}
		public static async Task<UserAchievementStored_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUserStats + 3, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct LeaderboardFindResult_t
	{
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		internal byte LeaderboardFound; // m_bLeaderboardFound uint8
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LeaderboardFindResult_t) : typeof(Pack8) );
		internal static LeaderboardFindResult_t Fill( IntPtr p ) => Config.PackSmall ? ((LeaderboardFindResult_t)(LeaderboardFindResult_t) Marshal.PtrToStructure( p, typeof(LeaderboardFindResult_t) )) : ((LeaderboardFindResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<LeaderboardFindResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LeaderboardFindResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LeaderboardFindResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUserStats + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUserStats + 4, false );
				actionClient = action;
			}
		}
		public static async Task<LeaderboardFindResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUserStats + 4, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct LeaderboardScoresDownloaded_t
	{
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		internal ulong SteamLeaderboardEntries; // m_hSteamLeaderboardEntries SteamLeaderboardEntries_t
		internal int CEntryCount; // m_cEntryCount int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LeaderboardScoresDownloaded_t) : typeof(Pack8) );
		internal static LeaderboardScoresDownloaded_t Fill( IntPtr p ) => Config.PackSmall ? ((LeaderboardScoresDownloaded_t)(LeaderboardScoresDownloaded_t) Marshal.PtrToStructure( p, typeof(LeaderboardScoresDownloaded_t) )) : ((LeaderboardScoresDownloaded_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<LeaderboardScoresDownloaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LeaderboardScoresDownloaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LeaderboardScoresDownloaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUserStats + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUserStats + 5, false );
				actionClient = action;
			}
		}
		public static async Task<LeaderboardScoresDownloaded_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUserStats + 5, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct LeaderboardScoreUploaded_t
	{
		internal byte Success; // m_bSuccess uint8
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		internal int Score; // m_nScore int32
		internal byte ScoreChanged; // m_bScoreChanged uint8
		internal int GlobalRankNew; // m_nGlobalRankNew int
		internal int GlobalRankPrevious; // m_nGlobalRankPrevious int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LeaderboardScoreUploaded_t) : typeof(Pack8) );
		internal static LeaderboardScoreUploaded_t Fill( IntPtr p ) => Config.PackSmall ? ((LeaderboardScoreUploaded_t)(LeaderboardScoreUploaded_t) Marshal.PtrToStructure( p, typeof(LeaderboardScoreUploaded_t) )) : ((LeaderboardScoreUploaded_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<LeaderboardScoreUploaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LeaderboardScoreUploaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LeaderboardScoreUploaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUserStats + 6, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUserStats + 6, false );
				actionClient = action;
			}
		}
		public static async Task<LeaderboardScoreUploaded_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUserStats + 6, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct NumberOfCurrentPlayers_t
	{
		internal byte Success; // m_bSuccess uint8
		internal int CPlayers; // m_cPlayers int32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(NumberOfCurrentPlayers_t) : typeof(Pack8) );
		internal static NumberOfCurrentPlayers_t Fill( IntPtr p ) => Config.PackSmall ? ((NumberOfCurrentPlayers_t)(NumberOfCurrentPlayers_t) Marshal.PtrToStructure( p, typeof(NumberOfCurrentPlayers_t) )) : ((NumberOfCurrentPlayers_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<NumberOfCurrentPlayers_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<NumberOfCurrentPlayers_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<NumberOfCurrentPlayers_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUserStats + 7, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUserStats + 7, false );
				actionClient = action;
			}
		}
		public static async Task<NumberOfCurrentPlayers_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUserStats + 7, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct UserStatsUnloaded_t
	{
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserStatsUnloaded_t) );
		internal static UserStatsUnloaded_t Fill( IntPtr p ) => ((UserStatsUnloaded_t)(UserStatsUnloaded_t) Marshal.PtrToStructure( p, typeof(UserStatsUnloaded_t) ) );
		
		static Action<UserStatsUnloaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UserStatsUnloaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UserStatsUnloaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUserStats + 8, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUserStats + 8, false );
				actionClient = action;
			}
		}
		public static async Task<UserStatsUnloaded_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUserStats + 8, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct UserAchievementIconFetched_t
	{
		internal GameId GameID; // m_nGameID class CGameID
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string AchievementName; // m_rgchAchievementName char [128]
		[MarshalAs(UnmanagedType.I1)]
		internal bool Achieved; // m_bAchieved _Bool
		internal int IconHandle; // m_nIconHandle int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(UserAchievementIconFetched_t) : typeof(Pack8) );
		internal static UserAchievementIconFetched_t Fill( IntPtr p ) => Config.PackSmall ? ((UserAchievementIconFetched_t)(UserAchievementIconFetched_t) Marshal.PtrToStructure( p, typeof(UserAchievementIconFetched_t) )) : ((UserAchievementIconFetched_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<UserAchievementIconFetched_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UserAchievementIconFetched_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UserAchievementIconFetched_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUserStats + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUserStats + 9, false );
				actionClient = action;
			}
		}
		public static async Task<UserAchievementIconFetched_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUserStats + 9, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct GlobalAchievementPercentagesReady_t
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GlobalAchievementPercentagesReady_t) : typeof(Pack8) );
		internal static GlobalAchievementPercentagesReady_t Fill( IntPtr p ) => Config.PackSmall ? ((GlobalAchievementPercentagesReady_t)(GlobalAchievementPercentagesReady_t) Marshal.PtrToStructure( p, typeof(GlobalAchievementPercentagesReady_t) )) : ((GlobalAchievementPercentagesReady_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<GlobalAchievementPercentagesReady_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GlobalAchievementPercentagesReady_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GlobalAchievementPercentagesReady_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUserStats + 10, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUserStats + 10, false );
				actionClient = action;
			}
		}
		public static async Task<GlobalAchievementPercentagesReady_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUserStats + 10, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct LeaderboardUGCSet_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LeaderboardUGCSet_t) : typeof(Pack8) );
		internal static LeaderboardUGCSet_t Fill( IntPtr p ) => Config.PackSmall ? ((LeaderboardUGCSet_t)(LeaderboardUGCSet_t) Marshal.PtrToStructure( p, typeof(LeaderboardUGCSet_t) )) : ((LeaderboardUGCSet_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<LeaderboardUGCSet_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LeaderboardUGCSet_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LeaderboardUGCSet_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUserStats + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUserStats + 11, false );
				actionClient = action;
			}
		}
		public static async Task<LeaderboardUGCSet_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUserStats + 11, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct PS3TrophiesInstalled_t
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		internal ulong RequiredDiskSpace; // m_ulRequiredDiskSpace uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(PS3TrophiesInstalled_t) : typeof(Pack8) );
		internal static PS3TrophiesInstalled_t Fill( IntPtr p ) => Config.PackSmall ? ((PS3TrophiesInstalled_t)(PS3TrophiesInstalled_t) Marshal.PtrToStructure( p, typeof(PS3TrophiesInstalled_t) )) : ((PS3TrophiesInstalled_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<PS3TrophiesInstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<PS3TrophiesInstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<PS3TrophiesInstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUserStats + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUserStats + 12, false );
				actionClient = action;
			}
		}
		public static async Task<PS3TrophiesInstalled_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUserStats + 12, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct GlobalStatsReceived_t
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GlobalStatsReceived_t) : typeof(Pack8) );
		internal static GlobalStatsReceived_t Fill( IntPtr p ) => Config.PackSmall ? ((GlobalStatsReceived_t)(GlobalStatsReceived_t) Marshal.PtrToStructure( p, typeof(GlobalStatsReceived_t) )) : ((GlobalStatsReceived_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<GlobalStatsReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GlobalStatsReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GlobalStatsReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUserStats + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUserStats + 12, false );
				actionClient = action;
			}
		}
		public static async Task<GlobalStatsReceived_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUserStats + 12, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct DlcInstalled_t
	{
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(DlcInstalled_t) : typeof(Pack8) );
		internal static DlcInstalled_t Fill( IntPtr p ) => Config.PackSmall ? ((DlcInstalled_t)(DlcInstalled_t) Marshal.PtrToStructure( p, typeof(DlcInstalled_t) )) : ((DlcInstalled_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<DlcInstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<DlcInstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<DlcInstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamApps + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamApps + 5, false );
				actionClient = action;
			}
		}
		public static async Task<DlcInstalled_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamApps + 5, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RegisterActivationCodeResponse_t
	{
		internal RegisterActivationCodeResult Result; // m_eResult enum ERegisterActivationCodeResult
		internal uint PackageRegistered; // m_unPackageRegistered uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RegisterActivationCodeResponse_t) : typeof(Pack8) );
		internal static RegisterActivationCodeResponse_t Fill( IntPtr p ) => Config.PackSmall ? ((RegisterActivationCodeResponse_t)(RegisterActivationCodeResponse_t) Marshal.PtrToStructure( p, typeof(RegisterActivationCodeResponse_t) )) : ((RegisterActivationCodeResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RegisterActivationCodeResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RegisterActivationCodeResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RegisterActivationCodeResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamApps + 8, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamApps + 8, false );
				actionClient = action;
			}
		}
		public static async Task<RegisterActivationCodeResponse_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamApps + 8, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct AppProofOfPurchaseKeyResponse_t
	{
		internal Result Result; // m_eResult enum EResult
		internal uint AppID; // m_nAppID uint32
		internal uint CchKeyLength; // m_cchKeyLength uint32
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 240)]
		internal string Key; // m_rgchKey char [240]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(AppProofOfPurchaseKeyResponse_t) : typeof(Pack8) );
		internal static AppProofOfPurchaseKeyResponse_t Fill( IntPtr p ) => Config.PackSmall ? ((AppProofOfPurchaseKeyResponse_t)(AppProofOfPurchaseKeyResponse_t) Marshal.PtrToStructure( p, typeof(AppProofOfPurchaseKeyResponse_t) )) : ((AppProofOfPurchaseKeyResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<AppProofOfPurchaseKeyResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<AppProofOfPurchaseKeyResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<AppProofOfPurchaseKeyResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamApps + 21, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamApps + 21, false );
				actionClient = action;
			}
		}
		public static async Task<AppProofOfPurchaseKeyResponse_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamApps + 21, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct FileDetailsResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong FileSize; // m_ulFileSize uint64
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] //  m_FileSHA
		internal byte[] FileSHA; // m_FileSHA uint8 [20]
		internal uint Flags; // m_unFlags uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(FileDetailsResult_t) : typeof(Pack8) );
		internal static FileDetailsResult_t Fill( IntPtr p ) => Config.PackSmall ? ((FileDetailsResult_t)(FileDetailsResult_t) Marshal.PtrToStructure( p, typeof(FileDetailsResult_t) )) : ((FileDetailsResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<FileDetailsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<FileDetailsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<FileDetailsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamApps + 23, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamApps + 23, false );
				actionClient = action;
			}
		}
		public static async Task<FileDetailsResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamApps + 23, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
		internal static P2PSessionState_t Fill( IntPtr p ) => Config.PackSmall ? ((P2PSessionState_t)(P2PSessionState_t) Marshal.PtrToStructure( p, typeof(P2PSessionState_t) )) : ((P2PSessionState_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
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
	internal struct P2PSessionRequest_t
	{
		internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(P2PSessionRequest_t) );
		internal static P2PSessionRequest_t Fill( IntPtr p ) => ((P2PSessionRequest_t)(P2PSessionRequest_t) Marshal.PtrToStructure( p, typeof(P2PSessionRequest_t) ) );
		
		static Action<P2PSessionRequest_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<P2PSessionRequest_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<P2PSessionRequest_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamNetworking + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamNetworking + 2, false );
				actionClient = action;
			}
		}
		public static async Task<P2PSessionRequest_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamNetworking + 2, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct P2PSessionConnectFail_t
	{
		internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
		internal byte P2PSessionError; // m_eP2PSessionError uint8
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(P2PSessionConnectFail_t) );
		internal static P2PSessionConnectFail_t Fill( IntPtr p ) => ((P2PSessionConnectFail_t)(P2PSessionConnectFail_t) Marshal.PtrToStructure( p, typeof(P2PSessionConnectFail_t) ) );
		
		static Action<P2PSessionConnectFail_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<P2PSessionConnectFail_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<P2PSessionConnectFail_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamNetworking + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamNetworking + 3, false );
				actionClient = action;
			}
		}
		public static async Task<P2PSessionConnectFail_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamNetworking + 3, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SocketStatusCallback_t
	{
		internal uint Socket; // m_hSocket SNetSocket_t
		internal uint ListenSocket; // m_hListenSocket SNetListenSocket_t
		internal ulong SteamIDRemote; // m_steamIDRemote class CSteamID
		internal int SNetSocketState; // m_eSNetSocketState int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SocketStatusCallback_t) );
		internal static SocketStatusCallback_t Fill( IntPtr p ) => ((SocketStatusCallback_t)(SocketStatusCallback_t) Marshal.PtrToStructure( p, typeof(SocketStatusCallback_t) ) );
		
		static Action<SocketStatusCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SocketStatusCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SocketStatusCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamNetworking + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamNetworking + 1, false );
				actionClient = action;
			}
		}
		public static async Task<SocketStatusCallback_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamNetworking + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct ScreenshotReady_t
	{
		internal uint Local; // m_hLocal ScreenshotHandle
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(ScreenshotReady_t) : typeof(Pack8) );
		internal static ScreenshotReady_t Fill( IntPtr p ) => Config.PackSmall ? ((ScreenshotReady_t)(ScreenshotReady_t) Marshal.PtrToStructure( p, typeof(ScreenshotReady_t) )) : ((ScreenshotReady_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<ScreenshotReady_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ScreenshotReady_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ScreenshotReady_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamScreenshots + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamScreenshots + 1, false );
				actionClient = action;
			}
		}
		public static async Task<ScreenshotReady_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamScreenshots + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct VolumeHasChanged_t
	{
		internal float NewVolume; // m_flNewVolume float
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(VolumeHasChanged_t) : typeof(Pack8) );
		internal static VolumeHasChanged_t Fill( IntPtr p ) => Config.PackSmall ? ((VolumeHasChanged_t)(VolumeHasChanged_t) Marshal.PtrToStructure( p, typeof(VolumeHasChanged_t) )) : ((VolumeHasChanged_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<VolumeHasChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<VolumeHasChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<VolumeHasChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMusic + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMusic + 2, false );
				actionClient = action;
			}
		}
		public static async Task<VolumeHasChanged_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMusic + 2, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct MusicPlayerWantsShuffled_t
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Shuffled; // m_bShuffled _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(MusicPlayerWantsShuffled_t) : typeof(Pack8) );
		internal static MusicPlayerWantsShuffled_t Fill( IntPtr p ) => Config.PackSmall ? ((MusicPlayerWantsShuffled_t)(MusicPlayerWantsShuffled_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsShuffled_t) )) : ((MusicPlayerWantsShuffled_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<MusicPlayerWantsShuffled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerWantsShuffled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsShuffled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMusicRemote + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMusicRemote + 9, false );
				actionClient = action;
			}
		}
		public static async Task<MusicPlayerWantsShuffled_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMusicRemote + 9, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct MusicPlayerWantsLooped_t
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Looped; // m_bLooped _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(MusicPlayerWantsLooped_t) : typeof(Pack8) );
		internal static MusicPlayerWantsLooped_t Fill( IntPtr p ) => Config.PackSmall ? ((MusicPlayerWantsLooped_t)(MusicPlayerWantsLooped_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsLooped_t) )) : ((MusicPlayerWantsLooped_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<MusicPlayerWantsLooped_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerWantsLooped_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsLooped_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMusicRemote + 10, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMusicRemote + 10, false );
				actionClient = action;
			}
		}
		public static async Task<MusicPlayerWantsLooped_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMusicRemote + 10, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct MusicPlayerWantsVolume_t
	{
		internal float NewVolume; // m_flNewVolume float
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(MusicPlayerWantsVolume_t) : typeof(Pack8) );
		internal static MusicPlayerWantsVolume_t Fill( IntPtr p ) => Config.PackSmall ? ((MusicPlayerWantsVolume_t)(MusicPlayerWantsVolume_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsVolume_t) )) : ((MusicPlayerWantsVolume_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<MusicPlayerWantsVolume_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerWantsVolume_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsVolume_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMusic + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMusic + 11, false );
				actionClient = action;
			}
		}
		public static async Task<MusicPlayerWantsVolume_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMusic + 11, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct MusicPlayerSelectsQueueEntry_t
	{
		internal int NID; // nID int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(MusicPlayerSelectsQueueEntry_t) : typeof(Pack8) );
		internal static MusicPlayerSelectsQueueEntry_t Fill( IntPtr p ) => Config.PackSmall ? ((MusicPlayerSelectsQueueEntry_t)(MusicPlayerSelectsQueueEntry_t) Marshal.PtrToStructure( p, typeof(MusicPlayerSelectsQueueEntry_t) )) : ((MusicPlayerSelectsQueueEntry_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<MusicPlayerSelectsQueueEntry_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerSelectsQueueEntry_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerSelectsQueueEntry_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMusic + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMusic + 12, false );
				actionClient = action;
			}
		}
		public static async Task<MusicPlayerSelectsQueueEntry_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMusic + 12, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct MusicPlayerSelectsPlaylistEntry_t
	{
		internal int NID; // nID int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(MusicPlayerSelectsPlaylistEntry_t) : typeof(Pack8) );
		internal static MusicPlayerSelectsPlaylistEntry_t Fill( IntPtr p ) => Config.PackSmall ? ((MusicPlayerSelectsPlaylistEntry_t)(MusicPlayerSelectsPlaylistEntry_t) Marshal.PtrToStructure( p, typeof(MusicPlayerSelectsPlaylistEntry_t) )) : ((MusicPlayerSelectsPlaylistEntry_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<MusicPlayerSelectsPlaylistEntry_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerSelectsPlaylistEntry_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerSelectsPlaylistEntry_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMusic + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMusic + 13, false );
				actionClient = action;
			}
		}
		public static async Task<MusicPlayerSelectsPlaylistEntry_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMusic + 13, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct MusicPlayerWantsPlayingRepeatStatus_t
	{
		internal int PlayingRepeatStatus; // m_nPlayingRepeatStatus int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(MusicPlayerWantsPlayingRepeatStatus_t) : typeof(Pack8) );
		internal static MusicPlayerWantsPlayingRepeatStatus_t Fill( IntPtr p ) => Config.PackSmall ? ((MusicPlayerWantsPlayingRepeatStatus_t)(MusicPlayerWantsPlayingRepeatStatus_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsPlayingRepeatStatus_t) )) : ((MusicPlayerWantsPlayingRepeatStatus_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<MusicPlayerWantsPlayingRepeatStatus_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<MusicPlayerWantsPlayingRepeatStatus_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<MusicPlayerWantsPlayingRepeatStatus_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMusicRemote + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMusicRemote + 14, false );
				actionClient = action;
			}
		}
		public static async Task<MusicPlayerWantsPlayingRepeatStatus_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMusicRemote + 14, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTTPRequestCompleted_t
	{
		internal uint Request; // m_hRequest HTTPRequestHandle
		internal ulong ContextValue; // m_ulContextValue uint64
		[MarshalAs(UnmanagedType.I1)]
		internal bool RequestSuccessful; // m_bRequestSuccessful _Bool
		internal HTTPStatusCode StatusCode; // m_eStatusCode enum EHTTPStatusCode
		internal uint BodySize; // m_unBodySize uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTTPRequestCompleted_t) : typeof(Pack8) );
		internal static HTTPRequestCompleted_t Fill( IntPtr p ) => Config.PackSmall ? ((HTTPRequestCompleted_t)(HTTPRequestCompleted_t) Marshal.PtrToStructure( p, typeof(HTTPRequestCompleted_t) )) : ((HTTPRequestCompleted_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTTPRequestCompleted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTTPRequestCompleted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTTPRequestCompleted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientHTTP + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientHTTP + 1, false );
				actionClient = action;
			}
		}
		public static async Task<HTTPRequestCompleted_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientHTTP + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTTPRequestHeadersReceived_t
	{
		internal uint Request; // m_hRequest HTTPRequestHandle
		internal ulong ContextValue; // m_ulContextValue uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTTPRequestHeadersReceived_t) : typeof(Pack8) );
		internal static HTTPRequestHeadersReceived_t Fill( IntPtr p ) => Config.PackSmall ? ((HTTPRequestHeadersReceived_t)(HTTPRequestHeadersReceived_t) Marshal.PtrToStructure( p, typeof(HTTPRequestHeadersReceived_t) )) : ((HTTPRequestHeadersReceived_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTTPRequestHeadersReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTTPRequestHeadersReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTTPRequestHeadersReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientHTTP + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientHTTP + 2, false );
				actionClient = action;
			}
		}
		public static async Task<HTTPRequestHeadersReceived_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientHTTP + 2, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTTPRequestDataReceived_t
	{
		internal uint Request; // m_hRequest HTTPRequestHandle
		internal ulong ContextValue; // m_ulContextValue uint64
		internal uint COffset; // m_cOffset uint32
		internal uint CBytesReceived; // m_cBytesReceived uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTTPRequestDataReceived_t) : typeof(Pack8) );
		internal static HTTPRequestDataReceived_t Fill( IntPtr p ) => Config.PackSmall ? ((HTTPRequestDataReceived_t)(HTTPRequestDataReceived_t) Marshal.PtrToStructure( p, typeof(HTTPRequestDataReceived_t) )) : ((HTTPRequestDataReceived_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTTPRequestDataReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTTPRequestDataReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTTPRequestDataReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientHTTP + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientHTTP + 3, false );
				actionClient = action;
			}
		}
		public static async Task<HTTPRequestDataReceived_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientHTTP + 3, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
		internal static SteamUGCDetails_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamUGCDetails_t)(SteamUGCDetails_t) Marshal.PtrToStructure( p, typeof(SteamUGCDetails_t) )) : ((SteamUGCDetails_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
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
	internal struct SteamUGCQueryCompleted_t
	{
		internal ulong Handle; // m_handle UGCQueryHandle_t
		internal Result Result; // m_eResult enum EResult
		internal uint NumResultsReturned; // m_unNumResultsReturned uint32
		internal uint TotalMatchingResults; // m_unTotalMatchingResults uint32
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData; // m_bCachedData _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string NextCursor; // m_rgchNextCursor char [256]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamUGCQueryCompleted_t) : typeof(Pack8) );
		internal static SteamUGCQueryCompleted_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamUGCQueryCompleted_t)(SteamUGCQueryCompleted_t) Marshal.PtrToStructure( p, typeof(SteamUGCQueryCompleted_t) )) : ((SteamUGCQueryCompleted_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SteamUGCQueryCompleted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamUGCQueryCompleted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamUGCQueryCompleted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientUGC + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientUGC + 1, false );
				actionClient = action;
			}
		}
		public static async Task<SteamUGCQueryCompleted_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientUGC + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct SteamUGCRequestUGCDetailsResult_t
	{
		internal SteamUGCDetails_t Details; // m_details struct SteamUGCDetails_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData; // m_bCachedData _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamUGCRequestUGCDetailsResult_t) : typeof(Pack8) );
		internal static SteamUGCRequestUGCDetailsResult_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamUGCRequestUGCDetailsResult_t)(SteamUGCRequestUGCDetailsResult_t) Marshal.PtrToStructure( p, typeof(SteamUGCRequestUGCDetailsResult_t) )) : ((SteamUGCRequestUGCDetailsResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SteamUGCRequestUGCDetailsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamUGCRequestUGCDetailsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamUGCRequestUGCDetailsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientUGC + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientUGC + 2, false );
				actionClient = action;
			}
		}
		public static async Task<SteamUGCRequestUGCDetailsResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientUGC + 2, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct CreateItemResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(CreateItemResult_t) : typeof(Pack8) );
		internal static CreateItemResult_t Fill( IntPtr p ) => Config.PackSmall ? ((CreateItemResult_t)(CreateItemResult_t) Marshal.PtrToStructure( p, typeof(CreateItemResult_t) )) : ((CreateItemResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<CreateItemResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<CreateItemResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<CreateItemResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientUGC + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientUGC + 3, false );
				actionClient = action;
			}
		}
		public static async Task<CreateItemResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientUGC + 3, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct SubmitItemUpdateResult_t
	{
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SubmitItemUpdateResult_t) : typeof(Pack8) );
		internal static SubmitItemUpdateResult_t Fill( IntPtr p ) => Config.PackSmall ? ((SubmitItemUpdateResult_t)(SubmitItemUpdateResult_t) Marshal.PtrToStructure( p, typeof(SubmitItemUpdateResult_t) )) : ((SubmitItemUpdateResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SubmitItemUpdateResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SubmitItemUpdateResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SubmitItemUpdateResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientUGC + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientUGC + 4, false );
				actionClient = action;
			}
		}
		public static async Task<SubmitItemUpdateResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientUGC + 4, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct DownloadItemResult_t
	{
		internal AppId AppID; // m_unAppID AppId_t
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(DownloadItemResult_t) : typeof(Pack8) );
		internal static DownloadItemResult_t Fill( IntPtr p ) => Config.PackSmall ? ((DownloadItemResult_t)(DownloadItemResult_t) Marshal.PtrToStructure( p, typeof(DownloadItemResult_t) )) : ((DownloadItemResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<DownloadItemResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<DownloadItemResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<DownloadItemResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientUGC + 6, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientUGC + 6, false );
				actionClient = action;
			}
		}
		public static async Task<DownloadItemResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientUGC + 6, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct UserFavoriteItemsListChanged_t
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool WasAddRequest; // m_bWasAddRequest _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(UserFavoriteItemsListChanged_t) : typeof(Pack8) );
		internal static UserFavoriteItemsListChanged_t Fill( IntPtr p ) => Config.PackSmall ? ((UserFavoriteItemsListChanged_t)(UserFavoriteItemsListChanged_t) Marshal.PtrToStructure( p, typeof(UserFavoriteItemsListChanged_t) )) : ((UserFavoriteItemsListChanged_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<UserFavoriteItemsListChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<UserFavoriteItemsListChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<UserFavoriteItemsListChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientUGC + 7, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientUGC + 7, false );
				actionClient = action;
			}
		}
		public static async Task<UserFavoriteItemsListChanged_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientUGC + 7, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct SetUserItemVoteResult_t
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool VoteUp; // m_bVoteUp _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SetUserItemVoteResult_t) : typeof(Pack8) );
		internal static SetUserItemVoteResult_t Fill( IntPtr p ) => Config.PackSmall ? ((SetUserItemVoteResult_t)(SetUserItemVoteResult_t) Marshal.PtrToStructure( p, typeof(SetUserItemVoteResult_t) )) : ((SetUserItemVoteResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SetUserItemVoteResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SetUserItemVoteResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SetUserItemVoteResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientUGC + 8, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientUGC + 8, false );
				actionClient = action;
			}
		}
		public static async Task<SetUserItemVoteResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientUGC + 8, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct GetUserItemVoteResult_t
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GetUserItemVoteResult_t) : typeof(Pack8) );
		internal static GetUserItemVoteResult_t Fill( IntPtr p ) => Config.PackSmall ? ((GetUserItemVoteResult_t)(GetUserItemVoteResult_t) Marshal.PtrToStructure( p, typeof(GetUserItemVoteResult_t) )) : ((GetUserItemVoteResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<GetUserItemVoteResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GetUserItemVoteResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GetUserItemVoteResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientUGC + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientUGC + 9, false );
				actionClient = action;
			}
		}
		public static async Task<GetUserItemVoteResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientUGC + 9, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct StartPlaytimeTrackingResult_t
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(StartPlaytimeTrackingResult_t) : typeof(Pack8) );
		internal static StartPlaytimeTrackingResult_t Fill( IntPtr p ) => Config.PackSmall ? ((StartPlaytimeTrackingResult_t)(StartPlaytimeTrackingResult_t) Marshal.PtrToStructure( p, typeof(StartPlaytimeTrackingResult_t) )) : ((StartPlaytimeTrackingResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<StartPlaytimeTrackingResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<StartPlaytimeTrackingResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<StartPlaytimeTrackingResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientUGC + 10, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientUGC + 10, false );
				actionClient = action;
			}
		}
		public static async Task<StartPlaytimeTrackingResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientUGC + 10, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct StopPlaytimeTrackingResult_t
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(StopPlaytimeTrackingResult_t) : typeof(Pack8) );
		internal static StopPlaytimeTrackingResult_t Fill( IntPtr p ) => Config.PackSmall ? ((StopPlaytimeTrackingResult_t)(StopPlaytimeTrackingResult_t) Marshal.PtrToStructure( p, typeof(StopPlaytimeTrackingResult_t) )) : ((StopPlaytimeTrackingResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<StopPlaytimeTrackingResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<StopPlaytimeTrackingResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<StopPlaytimeTrackingResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientUGC + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientUGC + 11, false );
				actionClient = action;
			}
		}
		public static async Task<StopPlaytimeTrackingResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientUGC + 11, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct AddUGCDependencyResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal PublishedFileId ChildPublishedFileId; // m_nChildPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(AddUGCDependencyResult_t) : typeof(Pack8) );
		internal static AddUGCDependencyResult_t Fill( IntPtr p ) => Config.PackSmall ? ((AddUGCDependencyResult_t)(AddUGCDependencyResult_t) Marshal.PtrToStructure( p, typeof(AddUGCDependencyResult_t) )) : ((AddUGCDependencyResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<AddUGCDependencyResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<AddUGCDependencyResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<AddUGCDependencyResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientUGC + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientUGC + 12, false );
				actionClient = action;
			}
		}
		public static async Task<AddUGCDependencyResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientUGC + 12, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoveUGCDependencyResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal PublishedFileId ChildPublishedFileId; // m_nChildPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoveUGCDependencyResult_t) : typeof(Pack8) );
		internal static RemoveUGCDependencyResult_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoveUGCDependencyResult_t)(RemoveUGCDependencyResult_t) Marshal.PtrToStructure( p, typeof(RemoveUGCDependencyResult_t) )) : ((RemoveUGCDependencyResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoveUGCDependencyResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoveUGCDependencyResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoveUGCDependencyResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientUGC + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientUGC + 13, false );
				actionClient = action;
			}
		}
		public static async Task<RemoveUGCDependencyResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientUGC + 13, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct AddAppDependencyResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(AddAppDependencyResult_t) : typeof(Pack8) );
		internal static AddAppDependencyResult_t Fill( IntPtr p ) => Config.PackSmall ? ((AddAppDependencyResult_t)(AddAppDependencyResult_t) Marshal.PtrToStructure( p, typeof(AddAppDependencyResult_t) )) : ((AddAppDependencyResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<AddAppDependencyResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<AddAppDependencyResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<AddAppDependencyResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientUGC + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientUGC + 14, false );
				actionClient = action;
			}
		}
		public static async Task<AddAppDependencyResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientUGC + 14, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct RemoveAppDependencyResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(RemoveAppDependencyResult_t) : typeof(Pack8) );
		internal static RemoveAppDependencyResult_t Fill( IntPtr p ) => Config.PackSmall ? ((RemoveAppDependencyResult_t)(RemoveAppDependencyResult_t) Marshal.PtrToStructure( p, typeof(RemoveAppDependencyResult_t) )) : ((RemoveAppDependencyResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<RemoveAppDependencyResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<RemoveAppDependencyResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<RemoveAppDependencyResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientUGC + 15, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientUGC + 15, false );
				actionClient = action;
			}
		}
		public static async Task<RemoveAppDependencyResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientUGC + 15, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct GetAppDependenciesResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.U4)]
		internal AppId[] GAppIDs; // m_rgAppIDs AppId_t [32]
		internal uint NumAppDependencies; // m_nNumAppDependencies uint32
		internal uint TotalNumAppDependencies; // m_nTotalNumAppDependencies uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GetAppDependenciesResult_t) : typeof(Pack8) );
		internal static GetAppDependenciesResult_t Fill( IntPtr p ) => Config.PackSmall ? ((GetAppDependenciesResult_t)(GetAppDependenciesResult_t) Marshal.PtrToStructure( p, typeof(GetAppDependenciesResult_t) )) : ((GetAppDependenciesResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<GetAppDependenciesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GetAppDependenciesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GetAppDependenciesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientUGC + 16, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientUGC + 16, false );
				actionClient = action;
			}
		}
		public static async Task<GetAppDependenciesResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientUGC + 16, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct DeleteItemResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(DeleteItemResult_t) : typeof(Pack8) );
		internal static DeleteItemResult_t Fill( IntPtr p ) => Config.PackSmall ? ((DeleteItemResult_t)(DeleteItemResult_t) Marshal.PtrToStructure( p, typeof(DeleteItemResult_t) )) : ((DeleteItemResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<DeleteItemResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<DeleteItemResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<DeleteItemResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientUGC + 17, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientUGC + 17, false );
				actionClient = action;
			}
		}
		public static async Task<DeleteItemResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientUGC + 17, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct SteamAppInstalled_t
	{
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamAppInstalled_t) : typeof(Pack8) );
		internal static SteamAppInstalled_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamAppInstalled_t)(SteamAppInstalled_t) Marshal.PtrToStructure( p, typeof(SteamAppInstalled_t) )) : ((SteamAppInstalled_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SteamAppInstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamAppInstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamAppInstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamAppList + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamAppList + 1, false );
				actionClient = action;
			}
		}
		public static async Task<SteamAppInstalled_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamAppList + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct SteamAppUninstalled_t
	{
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamAppUninstalled_t) : typeof(Pack8) );
		internal static SteamAppUninstalled_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamAppUninstalled_t)(SteamAppUninstalled_t) Marshal.PtrToStructure( p, typeof(SteamAppUninstalled_t) )) : ((SteamAppUninstalled_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SteamAppUninstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamAppUninstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamAppUninstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamAppList + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamAppList + 2, false );
				actionClient = action;
			}
		}
		public static async Task<SteamAppUninstalled_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamAppList + 2, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_BrowserReady_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_BrowserReady_t) : typeof(Pack8) );
		internal static HTML_BrowserReady_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_BrowserReady_t)(HTML_BrowserReady_t) Marshal.PtrToStructure( p, typeof(HTML_BrowserReady_t) )) : ((HTML_BrowserReady_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_BrowserReady_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_BrowserReady_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_BrowserReady_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 1, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_BrowserReady_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
		internal static HTML_NeedsPaint_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_NeedsPaint_t)(HTML_NeedsPaint_t) Marshal.PtrToStructure( p, typeof(HTML_NeedsPaint_t) )) : ((HTML_NeedsPaint_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
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
		internal static HTML_StartRequest_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_StartRequest_t)(HTML_StartRequest_t) Marshal.PtrToStructure( p, typeof(HTML_StartRequest_t) )) : ((HTML_StartRequest_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
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
		internal static HTML_CloseBrowser_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_CloseBrowser_t)(HTML_CloseBrowser_t) Marshal.PtrToStructure( p, typeof(HTML_CloseBrowser_t) )) : ((HTML_CloseBrowser_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
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
	internal struct HTML_URLChanged_t
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_URLChanged_t) : typeof(Pack8) );
		internal static HTML_URLChanged_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_URLChanged_t)(HTML_URLChanged_t) Marshal.PtrToStructure( p, typeof(HTML_URLChanged_t) )) : ((HTML_URLChanged_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_URLChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_URLChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_URLChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 5, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_URLChanged_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 5, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_FinishedRequest_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		internal string PchPageTitle; // pchPageTitle const char *
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_FinishedRequest_t) : typeof(Pack8) );
		internal static HTML_FinishedRequest_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_FinishedRequest_t)(HTML_FinishedRequest_t) Marshal.PtrToStructure( p, typeof(HTML_FinishedRequest_t) )) : ((HTML_FinishedRequest_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_FinishedRequest_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_FinishedRequest_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_FinishedRequest_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 6, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 6, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_FinishedRequest_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 6, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_OpenLinkInNewTab_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_OpenLinkInNewTab_t) : typeof(Pack8) );
		internal static HTML_OpenLinkInNewTab_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_OpenLinkInNewTab_t)(HTML_OpenLinkInNewTab_t) Marshal.PtrToStructure( p, typeof(HTML_OpenLinkInNewTab_t) )) : ((HTML_OpenLinkInNewTab_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_OpenLinkInNewTab_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_OpenLinkInNewTab_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_OpenLinkInNewTab_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 7, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 7, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_OpenLinkInNewTab_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 7, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_ChangedTitle_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchTitle; // pchTitle const char *
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_ChangedTitle_t) : typeof(Pack8) );
		internal static HTML_ChangedTitle_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_ChangedTitle_t)(HTML_ChangedTitle_t) Marshal.PtrToStructure( p, typeof(HTML_ChangedTitle_t) )) : ((HTML_ChangedTitle_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_ChangedTitle_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_ChangedTitle_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_ChangedTitle_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 8, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 8, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_ChangedTitle_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 8, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_SearchResults_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint UnResults; // unResults uint32
		internal uint UnCurrentMatch; // unCurrentMatch uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_SearchResults_t) : typeof(Pack8) );
		internal static HTML_SearchResults_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_SearchResults_t)(HTML_SearchResults_t) Marshal.PtrToStructure( p, typeof(HTML_SearchResults_t) )) : ((HTML_SearchResults_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_SearchResults_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_SearchResults_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_SearchResults_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 9, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_SearchResults_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 9, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_CanGoBackAndForward_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		[MarshalAs(UnmanagedType.I1)]
		internal bool BCanGoBack; // bCanGoBack _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool BCanGoForward; // bCanGoForward _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_CanGoBackAndForward_t) : typeof(Pack8) );
		internal static HTML_CanGoBackAndForward_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_CanGoBackAndForward_t)(HTML_CanGoBackAndForward_t) Marshal.PtrToStructure( p, typeof(HTML_CanGoBackAndForward_t) )) : ((HTML_CanGoBackAndForward_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_CanGoBackAndForward_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_CanGoBackAndForward_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_CanGoBackAndForward_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 10, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 10, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_CanGoBackAndForward_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 10, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_HorizontalScroll_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint UnScrollMax; // unScrollMax uint32
		internal uint UnScrollCurrent; // unScrollCurrent uint32
		internal float FlPageScale; // flPageScale float
		[MarshalAs(UnmanagedType.I1)]
		internal bool BVisible; // bVisible _Bool
		internal uint UnPageSize; // unPageSize uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_HorizontalScroll_t) : typeof(Pack8) );
		internal static HTML_HorizontalScroll_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_HorizontalScroll_t)(HTML_HorizontalScroll_t) Marshal.PtrToStructure( p, typeof(HTML_HorizontalScroll_t) )) : ((HTML_HorizontalScroll_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_HorizontalScroll_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_HorizontalScroll_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_HorizontalScroll_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 11, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_HorizontalScroll_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 11, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_VerticalScroll_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint UnScrollMax; // unScrollMax uint32
		internal uint UnScrollCurrent; // unScrollCurrent uint32
		internal float FlPageScale; // flPageScale float
		[MarshalAs(UnmanagedType.I1)]
		internal bool BVisible; // bVisible _Bool
		internal uint UnPageSize; // unPageSize uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_VerticalScroll_t) : typeof(Pack8) );
		internal static HTML_VerticalScroll_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_VerticalScroll_t)(HTML_VerticalScroll_t) Marshal.PtrToStructure( p, typeof(HTML_VerticalScroll_t) )) : ((HTML_VerticalScroll_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_VerticalScroll_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_VerticalScroll_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_VerticalScroll_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 12, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 12, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_VerticalScroll_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 12, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_LinkAtPosition_t
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_LinkAtPosition_t) : typeof(Pack8) );
		internal static HTML_LinkAtPosition_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_LinkAtPosition_t)(HTML_LinkAtPosition_t) Marshal.PtrToStructure( p, typeof(HTML_LinkAtPosition_t) )) : ((HTML_LinkAtPosition_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_LinkAtPosition_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_LinkAtPosition_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_LinkAtPosition_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 13, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 13, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_LinkAtPosition_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 13, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_JSAlert_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMessage; // pchMessage const char *
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_JSAlert_t) : typeof(Pack8) );
		internal static HTML_JSAlert_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_JSAlert_t)(HTML_JSAlert_t) Marshal.PtrToStructure( p, typeof(HTML_JSAlert_t) )) : ((HTML_JSAlert_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_JSAlert_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_JSAlert_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_JSAlert_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 14, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_JSAlert_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 14, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_JSConfirm_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMessage; // pchMessage const char *
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_JSConfirm_t) : typeof(Pack8) );
		internal static HTML_JSConfirm_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_JSConfirm_t)(HTML_JSConfirm_t) Marshal.PtrToStructure( p, typeof(HTML_JSConfirm_t) )) : ((HTML_JSConfirm_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_JSConfirm_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_JSConfirm_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_JSConfirm_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 15, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 15, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_JSConfirm_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 15, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_FileOpenDialog_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchTitle; // pchTitle const char *
		internal string PchInitialFile; // pchInitialFile const char *
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_FileOpenDialog_t) : typeof(Pack8) );
		internal static HTML_FileOpenDialog_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_FileOpenDialog_t)(HTML_FileOpenDialog_t) Marshal.PtrToStructure( p, typeof(HTML_FileOpenDialog_t) )) : ((HTML_FileOpenDialog_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_FileOpenDialog_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_FileOpenDialog_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_FileOpenDialog_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 16, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 16, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_FileOpenDialog_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 16, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_NewWindow_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		internal uint UnX; // unX uint32
		internal uint UnY; // unY uint32
		internal uint UnWide; // unWide uint32
		internal uint UnTall; // unTall uint32
		internal uint UnNewWindow_BrowserHandle_IGNORE; // unNewWindow_BrowserHandle_IGNORE HHTMLBrowser
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_NewWindow_t) : typeof(Pack8) );
		internal static HTML_NewWindow_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_NewWindow_t)(HTML_NewWindow_t) Marshal.PtrToStructure( p, typeof(HTML_NewWindow_t) )) : ((HTML_NewWindow_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_NewWindow_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_NewWindow_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_NewWindow_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 21, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 21, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_NewWindow_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 21, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_SetCursor_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint EMouseCursor; // eMouseCursor uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_SetCursor_t) : typeof(Pack8) );
		internal static HTML_SetCursor_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_SetCursor_t)(HTML_SetCursor_t) Marshal.PtrToStructure( p, typeof(HTML_SetCursor_t) )) : ((HTML_SetCursor_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_SetCursor_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_SetCursor_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_SetCursor_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 22, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 22, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_SetCursor_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 22, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_StatusText_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMsg; // pchMsg const char *
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_StatusText_t) : typeof(Pack8) );
		internal static HTML_StatusText_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_StatusText_t)(HTML_StatusText_t) Marshal.PtrToStructure( p, typeof(HTML_StatusText_t) )) : ((HTML_StatusText_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_StatusText_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_StatusText_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_StatusText_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 23, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 23, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_StatusText_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 23, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_ShowToolTip_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMsg; // pchMsg const char *
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_ShowToolTip_t) : typeof(Pack8) );
		internal static HTML_ShowToolTip_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_ShowToolTip_t)(HTML_ShowToolTip_t) Marshal.PtrToStructure( p, typeof(HTML_ShowToolTip_t) )) : ((HTML_ShowToolTip_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_ShowToolTip_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_ShowToolTip_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_ShowToolTip_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 24, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 24, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_ShowToolTip_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 24, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_UpdateToolTip_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMsg; // pchMsg const char *
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_UpdateToolTip_t) : typeof(Pack8) );
		internal static HTML_UpdateToolTip_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_UpdateToolTip_t)(HTML_UpdateToolTip_t) Marshal.PtrToStructure( p, typeof(HTML_UpdateToolTip_t) )) : ((HTML_UpdateToolTip_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_UpdateToolTip_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_UpdateToolTip_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_UpdateToolTip_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 25, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 25, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_UpdateToolTip_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 25, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_HideToolTip_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_HideToolTip_t) : typeof(Pack8) );
		internal static HTML_HideToolTip_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_HideToolTip_t)(HTML_HideToolTip_t) Marshal.PtrToStructure( p, typeof(HTML_HideToolTip_t) )) : ((HTML_HideToolTip_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_HideToolTip_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_HideToolTip_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_HideToolTip_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 26, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 26, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_HideToolTip_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 26, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct HTML_BrowserRestarted_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint UnOldBrowserHandle; // unOldBrowserHandle HHTMLBrowser
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(HTML_BrowserRestarted_t) : typeof(Pack8) );
		internal static HTML_BrowserRestarted_t Fill( IntPtr p ) => Config.PackSmall ? ((HTML_BrowserRestarted_t)(HTML_BrowserRestarted_t) Marshal.PtrToStructure( p, typeof(HTML_BrowserRestarted_t) )) : ((HTML_BrowserRestarted_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<HTML_BrowserRestarted_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<HTML_BrowserRestarted_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<HTML_BrowserRestarted_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamHTMLSurface + 27, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamHTMLSurface + 27, false );
				actionClient = action;
			}
		}
		public static async Task<HTML_BrowserRestarted_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamHTMLSurface + 27, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
		internal static SteamItemDetails_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamItemDetails_t)(SteamItemDetails_t) Marshal.PtrToStructure( p, typeof(SteamItemDetails_t) )) : ((SteamItemDetails_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
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
	internal struct SteamInventoryResultReady_t
	{
		internal int Handle; // m_handle SteamInventoryResult_t
		internal Result Result; // m_result enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamInventoryResultReady_t) : typeof(Pack8) );
		internal static SteamInventoryResultReady_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamInventoryResultReady_t)(SteamInventoryResultReady_t) Marshal.PtrToStructure( p, typeof(SteamInventoryResultReady_t) )) : ((SteamInventoryResultReady_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SteamInventoryResultReady_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamInventoryResultReady_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamInventoryResultReady_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientInventory + 0, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientInventory + 0, false );
				actionClient = action;
			}
		}
		public static async Task<SteamInventoryResultReady_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientInventory + 0, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct SteamInventoryFullUpdate_t
	{
		internal int Handle; // m_handle SteamInventoryResult_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamInventoryFullUpdate_t) : typeof(Pack8) );
		internal static SteamInventoryFullUpdate_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamInventoryFullUpdate_t)(SteamInventoryFullUpdate_t) Marshal.PtrToStructure( p, typeof(SteamInventoryFullUpdate_t) )) : ((SteamInventoryFullUpdate_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SteamInventoryFullUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamInventoryFullUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamInventoryFullUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientInventory + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientInventory + 1, false );
				actionClient = action;
			}
		}
		public static async Task<SteamInventoryFullUpdate_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientInventory + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct SteamInventoryEligiblePromoItemDefIDs_t
	{
		internal Result Result; // m_result enum EResult
		internal ulong SteamID; // m_steamID class CSteamID
		internal int UmEligiblePromoItemDefs; // m_numEligiblePromoItemDefs int
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData; // m_bCachedData _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamInventoryEligiblePromoItemDefIDs_t) );
		internal static SteamInventoryEligiblePromoItemDefIDs_t Fill( IntPtr p ) => ((SteamInventoryEligiblePromoItemDefIDs_t)(SteamInventoryEligiblePromoItemDefIDs_t) Marshal.PtrToStructure( p, typeof(SteamInventoryEligiblePromoItemDefIDs_t) ) );
		
		static Action<SteamInventoryEligiblePromoItemDefIDs_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamInventoryEligiblePromoItemDefIDs_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamInventoryEligiblePromoItemDefIDs_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientInventory + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientInventory + 3, false );
				actionClient = action;
			}
		}
		public static async Task<SteamInventoryEligiblePromoItemDefIDs_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientInventory + 3, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SteamInventoryStartPurchaseResult_t
	{
		internal Result Result; // m_result enum EResult
		internal ulong OrderID; // m_ulOrderID uint64
		internal ulong TransID; // m_ulTransID uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamInventoryStartPurchaseResult_t) : typeof(Pack8) );
		internal static SteamInventoryStartPurchaseResult_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamInventoryStartPurchaseResult_t)(SteamInventoryStartPurchaseResult_t) Marshal.PtrToStructure( p, typeof(SteamInventoryStartPurchaseResult_t) )) : ((SteamInventoryStartPurchaseResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SteamInventoryStartPurchaseResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamInventoryStartPurchaseResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamInventoryStartPurchaseResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientInventory + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientInventory + 4, false );
				actionClient = action;
			}
		}
		public static async Task<SteamInventoryStartPurchaseResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientInventory + 4, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct SteamInventoryRequestPricesResult_t
	{
		internal Result Result; // m_result enum EResult
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
		internal string Currency; // m_rgchCurrency char [4]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamInventoryRequestPricesResult_t) : typeof(Pack8) );
		internal static SteamInventoryRequestPricesResult_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamInventoryRequestPricesResult_t)(SteamInventoryRequestPricesResult_t) Marshal.PtrToStructure( p, typeof(SteamInventoryRequestPricesResult_t) )) : ((SteamInventoryRequestPricesResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SteamInventoryRequestPricesResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamInventoryRequestPricesResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamInventoryRequestPricesResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientInventory + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientInventory + 5, false );
				actionClient = action;
			}
		}
		public static async Task<SteamInventoryRequestPricesResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientInventory + 5, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct BroadcastUploadStop_t
	{
		internal BroadcastUploadResult Result; // m_eResult enum EBroadcastUploadResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(BroadcastUploadStop_t) : typeof(Pack8) );
		internal static BroadcastUploadStop_t Fill( IntPtr p ) => Config.PackSmall ? ((BroadcastUploadStop_t)(BroadcastUploadStop_t) Marshal.PtrToStructure( p, typeof(BroadcastUploadStop_t) )) : ((BroadcastUploadStop_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<BroadcastUploadStop_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<BroadcastUploadStop_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<BroadcastUploadStop_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientVideo + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientVideo + 5, false );
				actionClient = action;
			}
		}
		public static async Task<BroadcastUploadStop_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientVideo + 5, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct GetVideoURLResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal AppId VideoAppID; // m_unVideoAppID AppId_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string URL; // m_rgchURL char [256]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GetVideoURLResult_t) : typeof(Pack8) );
		internal static GetVideoURLResult_t Fill( IntPtr p ) => Config.PackSmall ? ((GetVideoURLResult_t)(GetVideoURLResult_t) Marshal.PtrToStructure( p, typeof(GetVideoURLResult_t) )) : ((GetVideoURLResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<GetVideoURLResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GetVideoURLResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GetVideoURLResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientVideo + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientVideo + 11, false );
				actionClient = action;
			}
		}
		public static async Task<GetVideoURLResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientVideo + 11, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct GetOPFSettingsResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal AppId VideoAppID; // m_unVideoAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GetOPFSettingsResult_t) : typeof(Pack8) );
		internal static GetOPFSettingsResult_t Fill( IntPtr p ) => Config.PackSmall ? ((GetOPFSettingsResult_t)(GetOPFSettingsResult_t) Marshal.PtrToStructure( p, typeof(GetOPFSettingsResult_t) )) : ((GetOPFSettingsResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<GetOPFSettingsResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GetOPFSettingsResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GetOPFSettingsResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientVideo + 24, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientVideo + 24, false );
				actionClient = action;
			}
		}
		public static async Task<GetOPFSettingsResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientVideo + 24, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct GSClientApprove_t
	{
		internal ulong SteamID; // m_SteamID class CSteamID
		internal ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientApprove_t) );
		internal static GSClientApprove_t Fill( IntPtr p ) => ((GSClientApprove_t)(GSClientApprove_t) Marshal.PtrToStructure( p, typeof(GSClientApprove_t) ) );
		
		static Action<GSClientApprove_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSClientApprove_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSClientApprove_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameServer + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameServer + 1, false );
				actionClient = action;
			}
		}
		public static async Task<GSClientApprove_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameServer + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSClientDeny_t
	{
		internal ulong SteamID; // m_SteamID class CSteamID
		internal DenyReason DenyReason; // m_eDenyReason enum EDenyReason
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string OptionalText; // m_rgchOptionalText char [128]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientDeny_t) );
		internal static GSClientDeny_t Fill( IntPtr p ) => ((GSClientDeny_t)(GSClientDeny_t) Marshal.PtrToStructure( p, typeof(GSClientDeny_t) ) );
		
		static Action<GSClientDeny_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSClientDeny_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSClientDeny_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameServer + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameServer + 2, false );
				actionClient = action;
			}
		}
		public static async Task<GSClientDeny_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameServer + 2, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSClientKick_t
	{
		internal ulong SteamID; // m_SteamID class CSteamID
		internal DenyReason DenyReason; // m_eDenyReason enum EDenyReason
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientKick_t) );
		internal static GSClientKick_t Fill( IntPtr p ) => ((GSClientKick_t)(GSClientKick_t) Marshal.PtrToStructure( p, typeof(GSClientKick_t) ) );
		
		static Action<GSClientKick_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSClientKick_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSClientKick_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameServer + 3, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameServer + 3, false );
				actionClient = action;
			}
		}
		public static async Task<GSClientKick_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameServer + 3, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSClientAchievementStatus_t
	{
		internal ulong SteamID; // m_SteamID uint64
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string PchAchievement; // m_pchAchievement char [128]
		[MarshalAs(UnmanagedType.I1)]
		internal bool Unlocked; // m_bUnlocked _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GSClientAchievementStatus_t) : typeof(Pack8) );
		internal static GSClientAchievementStatus_t Fill( IntPtr p ) => Config.PackSmall ? ((GSClientAchievementStatus_t)(GSClientAchievementStatus_t) Marshal.PtrToStructure( p, typeof(GSClientAchievementStatus_t) )) : ((GSClientAchievementStatus_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<GSClientAchievementStatus_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSClientAchievementStatus_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSClientAchievementStatus_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameServer + 6, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameServer + 6, false );
				actionClient = action;
			}
		}
		public static async Task<GSClientAchievementStatus_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameServer + 6, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct GSPolicyResponse_t
	{
		internal byte Secure; // m_bSecure uint8
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GSPolicyResponse_t) : typeof(Pack8) );
		internal static GSPolicyResponse_t Fill( IntPtr p ) => Config.PackSmall ? ((GSPolicyResponse_t)(GSPolicyResponse_t) Marshal.PtrToStructure( p, typeof(GSPolicyResponse_t) )) : ((GSPolicyResponse_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<GSPolicyResponse_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSPolicyResponse_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSPolicyResponse_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUser + 15, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUser + 15, false );
				actionClient = action;
			}
		}
		public static async Task<GSPolicyResponse_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUser + 15, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct GSGameplayStats_t
	{
		internal Result Result; // m_eResult enum EResult
		internal int Rank; // m_nRank int32
		internal uint TotalConnects; // m_unTotalConnects uint32
		internal uint TotalMinutesPlayed; // m_unTotalMinutesPlayed uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GSGameplayStats_t) : typeof(Pack8) );
		internal static GSGameplayStats_t Fill( IntPtr p ) => Config.PackSmall ? ((GSGameplayStats_t)(GSGameplayStats_t) Marshal.PtrToStructure( p, typeof(GSGameplayStats_t) )) : ((GSGameplayStats_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<GSGameplayStats_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSGameplayStats_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSGameplayStats_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameServer + 7, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameServer + 7, false );
				actionClient = action;
			}
		}
		public static async Task<GSGameplayStats_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameServer + 7, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct GSClientGroupStatus_t
	{
		internal ulong SteamIDUser; // m_SteamIDUser class CSteamID
		internal ulong SteamIDGroup; // m_SteamIDGroup class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		internal bool Member; // m_bMember _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool Officer; // m_bOfficer _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientGroupStatus_t) );
		internal static GSClientGroupStatus_t Fill( IntPtr p ) => ((GSClientGroupStatus_t)(GSClientGroupStatus_t) Marshal.PtrToStructure( p, typeof(GSClientGroupStatus_t) ) );
		
		static Action<GSClientGroupStatus_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSClientGroupStatus_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSClientGroupStatus_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameServer + 8, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameServer + 8, false );
				actionClient = action;
			}
		}
		public static async Task<GSClientGroupStatus_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameServer + 8, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSReputation_t
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GSReputation_t) : typeof(Pack8) );
		internal static GSReputation_t Fill( IntPtr p ) => Config.PackSmall ? ((GSReputation_t)(GSReputation_t) Marshal.PtrToStructure( p, typeof(GSReputation_t) )) : ((GSReputation_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<GSReputation_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSReputation_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSReputation_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameServer + 9, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameServer + 9, false );
				actionClient = action;
			}
		}
		public static async Task<GSReputation_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameServer + 9, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct AssociateWithClanResult_t
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(AssociateWithClanResult_t) : typeof(Pack8) );
		internal static AssociateWithClanResult_t Fill( IntPtr p ) => Config.PackSmall ? ((AssociateWithClanResult_t)(AssociateWithClanResult_t) Marshal.PtrToStructure( p, typeof(AssociateWithClanResult_t) )) : ((AssociateWithClanResult_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<AssociateWithClanResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<AssociateWithClanResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<AssociateWithClanResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameServer + 10, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameServer + 10, false );
				actionClient = action;
			}
		}
		public static async Task<AssociateWithClanResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameServer + 10, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct ComputeNewPlayerCompatibilityResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal int CPlayersThatDontLikeCandidate; // m_cPlayersThatDontLikeCandidate int
		internal int CPlayersThatCandidateDoesntLike; // m_cPlayersThatCandidateDoesntLike int
		internal int CClanPlayersThatDontLikeCandidate; // m_cClanPlayersThatDontLikeCandidate int
		internal ulong SteamIDCandidate; // m_SteamIDCandidate class CSteamID
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ComputeNewPlayerCompatibilityResult_t) );
		internal static ComputeNewPlayerCompatibilityResult_t Fill( IntPtr p ) => ((ComputeNewPlayerCompatibilityResult_t)(ComputeNewPlayerCompatibilityResult_t) Marshal.PtrToStructure( p, typeof(ComputeNewPlayerCompatibilityResult_t) ) );
		
		static Action<ComputeNewPlayerCompatibilityResult_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ComputeNewPlayerCompatibilityResult_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ComputeNewPlayerCompatibilityResult_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameServer + 11, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameServer + 11, false );
				actionClient = action;
			}
		}
		public static async Task<ComputeNewPlayerCompatibilityResult_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameServer + 11, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSStatsReceived_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSStatsReceived_t) );
		internal static GSStatsReceived_t Fill( IntPtr p ) => ((GSStatsReceived_t)(GSStatsReceived_t) Marshal.PtrToStructure( p, typeof(GSStatsReceived_t) ) );
		
		static Action<GSStatsReceived_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSStatsReceived_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSStatsReceived_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameServerStats + 0, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameServerStats + 0, false );
				actionClient = action;
			}
		}
		public static async Task<GSStatsReceived_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameServerStats + 0, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSStatsStored_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSStatsStored_t) );
		internal static GSStatsStored_t Fill( IntPtr p ) => ((GSStatsStored_t)(GSStatsStored_t) Marshal.PtrToStructure( p, typeof(GSStatsStored_t) ) );
		
		static Action<GSStatsStored_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSStatsStored_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSStatsStored_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameServerStats + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameServerStats + 1, false );
				actionClient = action;
			}
		}
		public static async Task<GSStatsStored_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameServerStats + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSStatsUnloaded_t
	{
		internal ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSStatsUnloaded_t) );
		internal static GSStatsUnloaded_t Fill( IntPtr p ) => ((GSStatsUnloaded_t)(GSStatsUnloaded_t) Marshal.PtrToStructure( p, typeof(GSStatsUnloaded_t) ) );
		
		static Action<GSStatsUnloaded_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GSStatsUnloaded_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GSStatsUnloaded_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUserStats + 8, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUserStats + 8, false );
				actionClient = action;
			}
		}
		public static async Task<GSStatsUnloaded_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUserStats + 8, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct AvailableBeaconLocationsUpdated_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(AvailableBeaconLocationsUpdated_t) : typeof(Pack8) );
		internal static AvailableBeaconLocationsUpdated_t Fill( IntPtr p ) => Config.PackSmall ? ((AvailableBeaconLocationsUpdated_t)(AvailableBeaconLocationsUpdated_t) Marshal.PtrToStructure( p, typeof(AvailableBeaconLocationsUpdated_t) )) : ((AvailableBeaconLocationsUpdated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<AvailableBeaconLocationsUpdated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<AvailableBeaconLocationsUpdated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<AvailableBeaconLocationsUpdated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamParties + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamParties + 5, false );
				actionClient = action;
			}
		}
		public static async Task<AvailableBeaconLocationsUpdated_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamParties + 5, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator AvailableBeaconLocationsUpdated_t ( AvailableBeaconLocationsUpdated_t.Pack8 d ) => new AvailableBeaconLocationsUpdated_t{  };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct ActiveBeaconsUpdated_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(ActiveBeaconsUpdated_t) : typeof(Pack8) );
		internal static ActiveBeaconsUpdated_t Fill( IntPtr p ) => Config.PackSmall ? ((ActiveBeaconsUpdated_t)(ActiveBeaconsUpdated_t) Marshal.PtrToStructure( p, typeof(ActiveBeaconsUpdated_t) )) : ((ActiveBeaconsUpdated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<ActiveBeaconsUpdated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ActiveBeaconsUpdated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ActiveBeaconsUpdated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamParties + 6, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamParties + 6, false );
				actionClient = action;
			}
		}
		public static async Task<ActiveBeaconsUpdated_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamParties + 6, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			
			public static implicit operator ActiveBeaconsUpdated_t ( ActiveBeaconsUpdated_t.Pack8 d ) => new ActiveBeaconsUpdated_t{  };
		}
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct PlaybackStatusHasChanged_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(PlaybackStatusHasChanged_t) : typeof(Pack8) );
		internal static PlaybackStatusHasChanged_t Fill( IntPtr p ) => Config.PackSmall ? ((PlaybackStatusHasChanged_t)(PlaybackStatusHasChanged_t) Marshal.PtrToStructure( p, typeof(PlaybackStatusHasChanged_t) )) : ((PlaybackStatusHasChanged_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<PlaybackStatusHasChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<PlaybackStatusHasChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<PlaybackStatusHasChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamMusic + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamMusic + 1, false );
				actionClient = action;
			}
		}
		public static async Task<PlaybackStatusHasChanged_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamMusic + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct BroadcastUploadStart_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(BroadcastUploadStart_t) : typeof(Pack8) );
		internal static BroadcastUploadStart_t Fill( IntPtr p ) => Config.PackSmall ? ((BroadcastUploadStart_t)(BroadcastUploadStart_t) Marshal.PtrToStructure( p, typeof(BroadcastUploadStart_t) )) : ((BroadcastUploadStart_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<BroadcastUploadStart_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<BroadcastUploadStart_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<BroadcastUploadStart_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientVideo + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientVideo + 4, false );
				actionClient = action;
			}
		}
		public static async Task<BroadcastUploadStart_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientVideo + 4, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct NewUrlLaunchParameters_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(NewUrlLaunchParameters_t) : typeof(Pack8) );
		internal static NewUrlLaunchParameters_t Fill( IntPtr p ) => Config.PackSmall ? ((NewUrlLaunchParameters_t)(NewUrlLaunchParameters_t) Marshal.PtrToStructure( p, typeof(NewUrlLaunchParameters_t) )) : ((NewUrlLaunchParameters_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<NewUrlLaunchParameters_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<NewUrlLaunchParameters_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<NewUrlLaunchParameters_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamApps + 14, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamApps + 14, false );
				actionClient = action;
			}
		}
		public static async Task<NewUrlLaunchParameters_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamApps + 14, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct ItemInstalled_t
	{
		internal AppId AppID; // m_unAppID AppId_t
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(ItemInstalled_t) : typeof(Pack8) );
		internal static ItemInstalled_t Fill( IntPtr p ) => Config.PackSmall ? ((ItemInstalled_t)(ItemInstalled_t) Marshal.PtrToStructure( p, typeof(ItemInstalled_t) )) : ((ItemInstalled_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<ItemInstalled_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ItemInstalled_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ItemInstalled_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientUGC + 5, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientUGC + 5, false );
				actionClient = action;
			}
		}
		public static async Task<ItemInstalled_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientUGC + 5, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct SteamNetConnectionStatusChangedCallback_t
	{
		internal NetConnection Conn; // m_hConn HSteamNetConnection
		internal ConnectionInfo Nfo; // m_info SteamNetConnectionInfo_t
		internal ConnectionState OldState; // m_eOldState ESteamNetworkingConnectionState
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamNetConnectionStatusChangedCallback_t) : typeof(Pack8) );
		internal static SteamNetConnectionStatusChangedCallback_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamNetConnectionStatusChangedCallback_t)(SteamNetConnectionStatusChangedCallback_t) Marshal.PtrToStructure( p, typeof(SteamNetConnectionStatusChangedCallback_t) )) : ((SteamNetConnectionStatusChangedCallback_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SteamNetConnectionStatusChangedCallback_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamNetConnectionStatusChangedCallback_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamNetConnectionStatusChangedCallback_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamNetworkingSockets + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamNetworkingSockets + 1, false );
				actionClient = action;
			}
		}
		public static async Task<SteamNetConnectionStatusChangedCallback_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamNetworkingSockets + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
		#endregion
		#region Packed Versions
		
		[StructLayout( LayoutKind.Sequential, Pack = 8 )]
		public struct Pack8
		{
			internal NetConnection Conn; // m_hConn HSteamNetConnection
			internal ConnectionInfo Nfo; // m_info SteamNetConnectionInfo_t
			internal ConnectionState OldState; // m_eOldState ESteamNetworkingConnectionState
			
			public static implicit operator SteamNetConnectionStatusChangedCallback_t ( SteamNetConnectionStatusChangedCallback_t.Pack8 d ) => new SteamNetConnectionStatusChangedCallback_t{ Conn = d.Conn,Nfo = d.Nfo,OldState = d.OldState, };
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
		internal static InputAnalogActionData_t Fill( IntPtr p ) => Config.PackSmall ? ((InputAnalogActionData_t)(InputAnalogActionData_t) Marshal.PtrToStructure( p, typeof(InputAnalogActionData_t) )) : ((InputAnalogActionData_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
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
		internal static InputMotionData_t Fill( IntPtr p ) => Config.PackSmall ? ((InputMotionData_t)(InputMotionData_t) Marshal.PtrToStructure( p, typeof(InputMotionData_t) )) : ((InputMotionData_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
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
		internal static InputDigitalActionData_t Fill( IntPtr p ) => Config.PackSmall ? ((InputDigitalActionData_t)(InputDigitalActionData_t) Marshal.PtrToStructure( p, typeof(InputDigitalActionData_t) )) : ((InputDigitalActionData_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
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
	internal struct SteamInventoryDefinitionUpdate_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamInventoryDefinitionUpdate_t) : typeof(Pack8) );
		internal static SteamInventoryDefinitionUpdate_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamInventoryDefinitionUpdate_t)(SteamInventoryDefinitionUpdate_t) Marshal.PtrToStructure( p, typeof(SteamInventoryDefinitionUpdate_t) )) : ((SteamInventoryDefinitionUpdate_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SteamInventoryDefinitionUpdate_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamInventoryDefinitionUpdate_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamInventoryDefinitionUpdate_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.ClientInventory + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.ClientInventory + 2, false );
				actionClient = action;
			}
		}
		public static async Task<SteamInventoryDefinitionUpdate_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.ClientInventory + 2, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct SteamParentalSettingsChanged_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamParentalSettingsChanged_t) : typeof(Pack8) );
		internal static SteamParentalSettingsChanged_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamParentalSettingsChanged_t)(SteamParentalSettingsChanged_t) Marshal.PtrToStructure( p, typeof(SteamParentalSettingsChanged_t) )) : ((SteamParentalSettingsChanged_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SteamParentalSettingsChanged_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamParentalSettingsChanged_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamParentalSettingsChanged_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamParentalSettings + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamParentalSettings + 1, false );
				actionClient = action;
			}
		}
		public static async Task<SteamParentalSettingsChanged_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamParentalSettings + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct SteamServersConnected_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamServersConnected_t) : typeof(Pack8) );
		internal static SteamServersConnected_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamServersConnected_t)(SteamServersConnected_t) Marshal.PtrToStructure( p, typeof(SteamServersConnected_t) )) : ((SteamServersConnected_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SteamServersConnected_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamServersConnected_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamServersConnected_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUser + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUser + 1, false );
				actionClient = action;
			}
		}
		public static async Task<SteamServersConnected_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUser + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
		internal static NewLaunchQueryParameters_t Fill( IntPtr p ) => Config.PackSmall ? ((NewLaunchQueryParameters_t)(NewLaunchQueryParameters_t) Marshal.PtrToStructure( p, typeof(NewLaunchQueryParameters_t) )) : ((NewLaunchQueryParameters_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
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
	internal struct GCMessageAvailable_t
	{
		internal uint MessageSize; // m_nMessageSize uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GCMessageAvailable_t) : typeof(Pack8) );
		internal static GCMessageAvailable_t Fill( IntPtr p ) => Config.PackSmall ? ((GCMessageAvailable_t)(GCMessageAvailable_t) Marshal.PtrToStructure( p, typeof(GCMessageAvailable_t) )) : ((GCMessageAvailable_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<GCMessageAvailable_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GCMessageAvailable_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GCMessageAvailable_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameCoordinator + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameCoordinator + 1, false );
				actionClient = action;
			}
		}
		public static async Task<GCMessageAvailable_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameCoordinator + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct GCMessageFailed_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(GCMessageFailed_t) : typeof(Pack8) );
		internal static GCMessageFailed_t Fill( IntPtr p ) => Config.PackSmall ? ((GCMessageFailed_t)(GCMessageFailed_t) Marshal.PtrToStructure( p, typeof(GCMessageFailed_t) )) : ((GCMessageFailed_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<GCMessageFailed_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<GCMessageFailed_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<GCMessageFailed_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamGameCoordinator + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamGameCoordinator + 2, false );
				actionClient = action;
			}
		}
		public static async Task<GCMessageFailed_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamGameCoordinator + 2, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct ScreenshotRequested_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(ScreenshotRequested_t) : typeof(Pack8) );
		internal static ScreenshotRequested_t Fill( IntPtr p ) => Config.PackSmall ? ((ScreenshotRequested_t)(ScreenshotRequested_t) Marshal.PtrToStructure( p, typeof(ScreenshotRequested_t) )) : ((ScreenshotRequested_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<ScreenshotRequested_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<ScreenshotRequested_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<ScreenshotRequested_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamScreenshots + 2, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamScreenshots + 2, false );
				actionClient = action;
			}
		}
		public static async Task<ScreenshotRequested_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamScreenshots + 2, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct LicensesUpdated_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(LicensesUpdated_t) : typeof(Pack8) );
		internal static LicensesUpdated_t Fill( IntPtr p ) => Config.PackSmall ? ((LicensesUpdated_t)(LicensesUpdated_t) Marshal.PtrToStructure( p, typeof(LicensesUpdated_t) )) : ((LicensesUpdated_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<LicensesUpdated_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<LicensesUpdated_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<LicensesUpdated_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUser + 25, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUser + 25, false );
				actionClient = action;
			}
		}
		public static async Task<LicensesUpdated_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUser + 25, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct SteamShutdown_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(SteamShutdown_t) : typeof(Pack8) );
		internal static SteamShutdown_t Fill( IntPtr p ) => Config.PackSmall ? ((SteamShutdown_t)(SteamShutdown_t) Marshal.PtrToStructure( p, typeof(SteamShutdown_t) )) : ((SteamShutdown_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<SteamShutdown_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<SteamShutdown_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<SteamShutdown_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUtils + 4, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUtils + 4, false );
				actionClient = action;
			}
		}
		public static async Task<SteamShutdown_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUtils + 4, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct IPCountry_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(IPCountry_t) : typeof(Pack8) );
		internal static IPCountry_t Fill( IntPtr p ) => Config.PackSmall ? ((IPCountry_t)(IPCountry_t) Marshal.PtrToStructure( p, typeof(IPCountry_t) )) : ((IPCountry_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<IPCountry_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<IPCountry_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<IPCountry_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUtils + 1, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUtils + 1, false );
				actionClient = action;
			}
		}
		public static async Task<IPCountry_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUtils + 1, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
	internal struct IPCFailure_t
	{
		internal byte FailureType; // m_eFailureType uint8
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( Config.PackSmall ? typeof(IPCFailure_t) : typeof(Pack8) );
		internal static IPCFailure_t Fill( IntPtr p ) => Config.PackSmall ? ((IPCFailure_t)(IPCFailure_t) Marshal.PtrToStructure( p, typeof(IPCFailure_t) )) : ((IPCFailure_t)(Pack8) Marshal.PtrToStructure( p, typeof(Pack8) ));
		
		static Action<IPCFailure_t> actionClient;
		[MonoPInvokeCallback] static void OnClient( IntPtr thisptr, IntPtr pvParam ) => actionClient?.Invoke( Fill( pvParam ) );
		static Action<IPCFailure_t> actionServer;
		[MonoPInvokeCallback] static void OnServer( IntPtr thisptr, IntPtr pvParam ) => actionServer?.Invoke( Fill( pvParam ) );
		public static void Install( Action<IPCFailure_t> action, bool server = false )
		{
			if ( server )
			{
				Event.Register( OnServer, StructSize, CallbackIdentifiers.SteamUser + 17, true );
				actionServer = action;
			}
			else
			{
				Event.Register( OnClient, StructSize, CallbackIdentifiers.SteamUser + 17, false );
				actionClient = action;
			}
		}
		public static async Task<IPCFailure_t?> GetResultAsync( SteamAPICall_t handle )
		{
			bool failed = false;
			
			while ( !SteamUtils.IsCallComplete( handle, out failed ) )
			{
				await Task.Delay( 1 );
			}
			if ( failed ) return null;
			
			var ptr = Marshal.AllocHGlobal( StructSize );
			
			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( handle, ptr, StructSize, CallbackIdentifiers.SteamUser + 17, ref failed ) || failed )
					return null;
			
				return Fill( ptr );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
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
