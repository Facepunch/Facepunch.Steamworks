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
	internal struct SteamServerConnectFailure_t
	{
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool StillRetrying; // m_bStillRetrying _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamServerConnectFailure_t) );
		internal static SteamServerConnectFailure_t Fill( IntPtr p ) => ((SteamServerConnectFailure_t)(SteamServerConnectFailure_t) Marshal.PtrToStructure( p, typeof(SteamServerConnectFailure_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamServersDisconnected_t
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamServersDisconnected_t) );
		internal static SteamServersDisconnected_t Fill( IntPtr p ) => ((SteamServersDisconnected_t)(SteamServersDisconnected_t) Marshal.PtrToStructure( p, typeof(SteamServersDisconnected_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct ClientGameServerDeny_t
	{
		internal uint AppID; // m_uAppID uint32
		internal uint GameServerIP; // m_unGameServerIP uint32
		internal ushort GameServerPort; // m_usGameServerPort uint16
		internal ushort Secure; // m_bSecure uint16
		internal uint Reason; // m_uReason uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ClientGameServerDeny_t) );
		internal static ClientGameServerDeny_t Fill( IntPtr p ) => ((ClientGameServerDeny_t)(ClientGameServerDeny_t) Marshal.PtrToStructure( p, typeof(ClientGameServerDeny_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MicroTxnAuthorizationResponse_t
	{
		internal uint AppID; // m_unAppID uint32
		internal ulong OrderID; // m_ulOrderID uint64
		internal byte Authorized; // m_bAuthorized uint8
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MicroTxnAuthorizationResponse_t) );
		internal static MicroTxnAuthorizationResponse_t Fill( IntPtr p ) => ((MicroTxnAuthorizationResponse_t)(MicroTxnAuthorizationResponse_t) Marshal.PtrToStructure( p, typeof(MicroTxnAuthorizationResponse_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct EncryptedAppTicketResponse_t
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(EncryptedAppTicketResponse_t) );
		internal static EncryptedAppTicketResponse_t Fill( IntPtr p ) => ((EncryptedAppTicketResponse_t)(EncryptedAppTicketResponse_t) Marshal.PtrToStructure( p, typeof(EncryptedAppTicketResponse_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GetAuthSessionTicketResponse_t
	{
		internal uint AuthTicket; // m_hAuthTicket HAuthTicket
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GetAuthSessionTicketResponse_t) );
		internal static GetAuthSessionTicketResponse_t Fill( IntPtr p ) => ((GetAuthSessionTicketResponse_t)(GetAuthSessionTicketResponse_t) Marshal.PtrToStructure( p, typeof(GetAuthSessionTicketResponse_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GameWebCallback_t
	{
		internal string URLUTF8() => System.Text.Encoding.UTF8.GetString( URL, 0, System.Array.IndexOf<byte>( URL, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // byte[] m_szURL
		internal byte[] URL; // m_szURL char [256]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameWebCallback_t) );
		internal static GameWebCallback_t Fill( IntPtr p ) => ((GameWebCallback_t)(GameWebCallback_t) Marshal.PtrToStructure( p, typeof(GameWebCallback_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct StoreAuthURLResponse_t
	{
		internal string URLUTF8() => System.Text.Encoding.UTF8.GetString( URL, 0, System.Array.IndexOf<byte>( URL, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)] // byte[] m_szURL
		internal byte[] URL; // m_szURL char [512]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(StoreAuthURLResponse_t) );
		internal static StoreAuthURLResponse_t Fill( IntPtr p ) => ((StoreAuthURLResponse_t)(StoreAuthURLResponse_t) Marshal.PtrToStructure( p, typeof(StoreAuthURLResponse_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MarketEligibilityResponse_t
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Allowed; // m_bAllowed _Bool
		internal MarketNotAllowedReasonFlags NotAllowedReason; // m_eNotAllowedReason enum EMarketNotAllowedReasonFlags
		internal uint TAllowedAtTime; // m_rtAllowedAtTime RTime32
		internal int CdaySteamGuardRequiredDays; // m_cdaySteamGuardRequiredDays int
		internal int CdayNewDeviceCooldown; // m_cdayNewDeviceCooldown int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MarketEligibilityResponse_t) );
		internal static MarketEligibilityResponse_t Fill( IntPtr p ) => ((MarketEligibilityResponse_t)(MarketEligibilityResponse_t) Marshal.PtrToStructure( p, typeof(MarketEligibilityResponse_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	internal struct FriendStateChange_t
	{
		internal ulong SteamID; // m_ulSteamID uint64
		internal int ChangeFlags; // m_nChangeFlags int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FriendStateChange_t) );
		internal static FriendStateChange_t Fill( IntPtr p ) => ((FriendStateChange_t)(FriendStateChange_t) Marshal.PtrToStructure( p, typeof(FriendStateChange_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GameOverlayActivated_t
	{
		internal byte Active; // m_bActive uint8
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameOverlayActivated_t) );
		internal static GameOverlayActivated_t Fill( IntPtr p ) => ((GameOverlayActivated_t)(GameOverlayActivated_t) Marshal.PtrToStructure( p, typeof(GameOverlayActivated_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GameServerChangeRequested_t
	{
		internal string ServerUTF8() => System.Text.Encoding.UTF8.GetString( Server, 0, System.Array.IndexOf<byte>( Server, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)] // byte[] m_rgchServer
		internal byte[] Server; // m_rgchServer char [64]
		internal string PasswordUTF8() => System.Text.Encoding.UTF8.GetString( Password, 0, System.Array.IndexOf<byte>( Password, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)] // byte[] m_rgchPassword
		internal byte[] Password; // m_rgchPassword char [64]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GameServerChangeRequested_t) );
		internal static GameServerChangeRequested_t Fill( IntPtr p ) => ((GameServerChangeRequested_t)(GameServerChangeRequested_t) Marshal.PtrToStructure( p, typeof(GameServerChangeRequested_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GameRichPresenceJoinRequested_t
	{
		internal ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		internal string ConnectUTF8() => System.Text.Encoding.UTF8.GetString( Connect, 0, System.Array.IndexOf<byte>( Connect, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // byte[] m_rgchConnect
		internal byte[] Connect; // m_rgchConnect char [256]
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct DownloadClanActivityCountsResult_t
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Success; // m_bSuccess _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(DownloadClanActivityCountsResult_t) );
		internal static DownloadClanActivityCountsResult_t Fill( IntPtr p ) => ((DownloadClanActivityCountsResult_t)(DownloadClanActivityCountsResult_t) Marshal.PtrToStructure( p, typeof(DownloadClanActivityCountsResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SetPersonaNameResponse_t
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Success; // m_bSuccess _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool LocalSuccess; // m_bLocalSuccess _Bool
		internal Result Result; // m_result enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SetPersonaNameResponse_t) );
		internal static SetPersonaNameResponse_t Fill( IntPtr p ) => ((SetPersonaNameResponse_t)(SetPersonaNameResponse_t) Marshal.PtrToStructure( p, typeof(SetPersonaNameResponse_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LowBatteryPower_t
	{
		internal byte MinutesBatteryLeft; // m_nMinutesBatteryLeft uint8
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LowBatteryPower_t) );
		internal static LowBatteryPower_t Fill( IntPtr p ) => ((LowBatteryPower_t)(LowBatteryPower_t) Marshal.PtrToStructure( p, typeof(LowBatteryPower_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamAPICallCompleted_t
	{
		internal ulong AsyncCall; // m_hAsyncCall SteamAPICall_t
		internal int Callback; // m_iCallback int
		internal uint ParamCount; // m_cubParam uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamAPICallCompleted_t) );
		internal static SteamAPICallCompleted_t Fill( IntPtr p ) => ((SteamAPICallCompleted_t)(SteamAPICallCompleted_t) Marshal.PtrToStructure( p, typeof(SteamAPICallCompleted_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct CheckFileSignature_t
	{
		internal CheckFileSignature CheckFileSignature; // m_eCheckFileSignature enum ECheckFileSignature
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(CheckFileSignature_t) );
		internal static CheckFileSignature_t Fill( IntPtr p ) => ((CheckFileSignature_t)(CheckFileSignature_t) Marshal.PtrToStructure( p, typeof(CheckFileSignature_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GamepadTextInputDismissed_t
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Submitted; // m_bSubmitted _Bool
		internal uint SubmittedText; // m_unSubmittedText uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GamepadTextInputDismissed_t) );
		internal static GamepadTextInputDismissed_t Fill( IntPtr p ) => ((GamepadTextInputDismissed_t)(GamepadTextInputDismissed_t) Marshal.PtrToStructure( p, typeof(GamepadTextInputDismissed_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FavoritesListChanged_t) );
		internal static FavoritesListChanged_t Fill( IntPtr p ) => ((FavoritesListChanged_t)(FavoritesListChanged_t) Marshal.PtrToStructure( p, typeof(FavoritesListChanged_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyInvite_t
	{
		internal ulong SteamIDUser; // m_ulSteamIDUser uint64
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong GameID; // m_ulGameID uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyInvite_t) );
		internal static LobbyInvite_t Fill( IntPtr p ) => ((LobbyInvite_t)(LobbyInvite_t) Marshal.PtrToStructure( p, typeof(LobbyInvite_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyEnter_t
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal uint GfChatPermissions; // m_rgfChatPermissions uint32
		[MarshalAs(UnmanagedType.I1)]
		internal bool Locked; // m_bLocked _Bool
		internal uint EChatRoomEnterResponse; // m_EChatRoomEnterResponse uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyEnter_t) );
		internal static LobbyEnter_t Fill( IntPtr p ) => ((LobbyEnter_t)(LobbyEnter_t) Marshal.PtrToStructure( p, typeof(LobbyEnter_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyDataUpdate_t
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDMember; // m_ulSteamIDMember uint64
		internal byte Success; // m_bSuccess uint8
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyDataUpdate_t) );
		internal static LobbyDataUpdate_t Fill( IntPtr p ) => ((LobbyDataUpdate_t)(LobbyDataUpdate_t) Marshal.PtrToStructure( p, typeof(LobbyDataUpdate_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyChatUpdate_t
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDUserChanged; // m_ulSteamIDUserChanged uint64
		internal ulong SteamIDMakingChange; // m_ulSteamIDMakingChange uint64
		internal uint GfChatMemberStateChange; // m_rgfChatMemberStateChange uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyChatUpdate_t) );
		internal static LobbyChatUpdate_t Fill( IntPtr p ) => ((LobbyChatUpdate_t)(LobbyChatUpdate_t) Marshal.PtrToStructure( p, typeof(LobbyChatUpdate_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyChatMsg_t
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDUser; // m_ulSteamIDUser uint64
		internal byte ChatEntryType; // m_eChatEntryType uint8
		internal uint ChatID; // m_iChatID uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyChatMsg_t) );
		internal static LobbyChatMsg_t Fill( IntPtr p ) => ((LobbyChatMsg_t)(LobbyChatMsg_t) Marshal.PtrToStructure( p, typeof(LobbyChatMsg_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyGameCreated_t
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDGameServer; // m_ulSteamIDGameServer uint64
		internal uint IP; // m_unIP uint32
		internal ushort Port; // m_usPort uint16
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyGameCreated_t) );
		internal static LobbyGameCreated_t Fill( IntPtr p ) => ((LobbyGameCreated_t)(LobbyGameCreated_t) Marshal.PtrToStructure( p, typeof(LobbyGameCreated_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyMatchList_t
	{
		internal uint LobbiesMatching; // m_nLobbiesMatching uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyMatchList_t) );
		internal static LobbyMatchList_t Fill( IntPtr p ) => ((LobbyMatchList_t)(LobbyMatchList_t) Marshal.PtrToStructure( p, typeof(LobbyMatchList_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyKicked_t
	{
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		internal ulong SteamIDAdmin; // m_ulSteamIDAdmin uint64
		internal byte KickedDueToDisconnect; // m_bKickedDueToDisconnect uint8
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyKicked_t) );
		internal static LobbyKicked_t Fill( IntPtr p ) => ((LobbyKicked_t)(LobbyKicked_t) Marshal.PtrToStructure( p, typeof(LobbyKicked_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LobbyCreated_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LobbyCreated_t) );
		internal static LobbyCreated_t Fill( IntPtr p ) => ((LobbyCreated_t)(LobbyCreated_t) Marshal.PtrToStructure( p, typeof(LobbyCreated_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct FavoritesListAccountsUpdated_t
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FavoritesListAccountsUpdated_t) );
		internal static FavoritesListAccountsUpdated_t Fill( IntPtr p ) => ((FavoritesListAccountsUpdated_t)(FavoritesListAccountsUpdated_t) Marshal.PtrToStructure( p, typeof(FavoritesListAccountsUpdated_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RequestPlayersForGameProgressCallback_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong LSearchID; // m_ullSearchID uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RequestPlayersForGameProgressCallback_t) );
		internal static RequestPlayersForGameProgressCallback_t Fill( IntPtr p ) => ((RequestPlayersForGameProgressCallback_t)(RequestPlayersForGameProgressCallback_t) Marshal.PtrToStructure( p, typeof(RequestPlayersForGameProgressCallback_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RequestPlayersForGameFinalResultCallback_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong LSearchID; // m_ullSearchID uint64
		internal ulong LUniqueGameID; // m_ullUniqueGameID uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RequestPlayersForGameFinalResultCallback_t) );
		internal static RequestPlayersForGameFinalResultCallback_t Fill( IntPtr p ) => ((RequestPlayersForGameFinalResultCallback_t)(RequestPlayersForGameFinalResultCallback_t) Marshal.PtrToStructure( p, typeof(RequestPlayersForGameFinalResultCallback_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct EndGameResultCallback_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong UllUniqueGameID; // ullUniqueGameID uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(EndGameResultCallback_t) );
		internal static EndGameResultCallback_t Fill( IntPtr p ) => ((EndGameResultCallback_t)(EndGameResultCallback_t) Marshal.PtrToStructure( p, typeof(EndGameResultCallback_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct JoinPartyCallback_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
		internal ulong SteamIDBeaconOwner; // m_SteamIDBeaconOwner class CSteamID
		internal string ConnectStringUTF8() => System.Text.Encoding.UTF8.GetString( ConnectString, 0, System.Array.IndexOf<byte>( ConnectString, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // byte[] m_rgchConnectString
		internal byte[] ConnectString; // m_rgchConnectString char [256]
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct CreateBeaconCallback_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong BeaconID; // m_ulBeaconID PartyBeaconID_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(CreateBeaconCallback_t) );
		internal static CreateBeaconCallback_t Fill( IntPtr p ) => ((CreateBeaconCallback_t)(CreateBeaconCallback_t) Marshal.PtrToStructure( p, typeof(CreateBeaconCallback_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct ChangeNumOpenSlotsCallback_t
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ChangeNumOpenSlotsCallback_t) );
		internal static ChangeNumOpenSlotsCallback_t Fill( IntPtr p ) => ((ChangeNumOpenSlotsCallback_t)(ChangeNumOpenSlotsCallback_t) Marshal.PtrToStructure( p, typeof(ChangeNumOpenSlotsCallback_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	internal struct RemoteStorageAppSyncedClient_t
	{
		internal AppId AppID; // m_nAppID AppId_t
		internal Result Result; // m_eResult enum EResult
		internal int NumDownloads; // m_unNumDownloads int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageAppSyncedClient_t) );
		internal static RemoteStorageAppSyncedClient_t Fill( IntPtr p ) => ((RemoteStorageAppSyncedClient_t)(RemoteStorageAppSyncedClient_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncedClient_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageAppSyncedServer_t
	{
		internal AppId AppID; // m_nAppID AppId_t
		internal Result Result; // m_eResult enum EResult
		internal int NumUploads; // m_unNumUploads int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageAppSyncedServer_t) );
		internal static RemoteStorageAppSyncedServer_t Fill( IntPtr p ) => ((RemoteStorageAppSyncedServer_t)(RemoteStorageAppSyncedServer_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncedServer_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageAppSyncProgress_t
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageAppSyncProgress_t) );
		internal static RemoteStorageAppSyncProgress_t Fill( IntPtr p ) => ((RemoteStorageAppSyncProgress_t)(RemoteStorageAppSyncProgress_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncProgress_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageAppSyncStatusCheck_t
	{
		internal AppId AppID; // m_nAppID AppId_t
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageAppSyncStatusCheck_t) );
		internal static RemoteStorageAppSyncStatusCheck_t Fill( IntPtr p ) => ((RemoteStorageAppSyncStatusCheck_t)(RemoteStorageAppSyncStatusCheck_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncStatusCheck_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageFileShareResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong File; // m_hFile UGCHandle_t
		internal string FilenameUTF8() => System.Text.Encoding.UTF8.GetString( Filename, 0, System.Array.IndexOf<byte>( Filename, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)] // byte[] m_rgchFilename
		internal byte[] Filename; // m_rgchFilename char [260]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageFileShareResult_t) );
		internal static RemoteStorageFileShareResult_t Fill( IntPtr p ) => ((RemoteStorageFileShareResult_t)(RemoteStorageFileShareResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageFileShareResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStoragePublishFileResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStoragePublishFileResult_t) );
		internal static RemoteStoragePublishFileResult_t Fill( IntPtr p ) => ((RemoteStoragePublishFileResult_t)(RemoteStoragePublishFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishFileResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageDeletePublishedFileResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageDeletePublishedFileResult_t) );
		internal static RemoteStorageDeletePublishedFileResult_t Fill( IntPtr p ) => ((RemoteStorageDeletePublishedFileResult_t)(RemoteStorageDeletePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageDeletePublishedFileResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageEnumerateUserPublishedFilesResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageEnumerateUserPublishedFilesResult_t) );
		internal static RemoteStorageEnumerateUserPublishedFilesResult_t Fill( IntPtr p ) => ((RemoteStorageEnumerateUserPublishedFilesResult_t)(RemoteStorageEnumerateUserPublishedFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserPublishedFilesResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageSubscribePublishedFileResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageSubscribePublishedFileResult_t) );
		internal static RemoteStorageSubscribePublishedFileResult_t Fill( IntPtr p ) => ((RemoteStorageSubscribePublishedFileResult_t)(RemoteStorageSubscribePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageSubscribePublishedFileResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t) );
		internal static RemoteStorageEnumerateUserSubscribedFilesResult_t Fill( IntPtr p ) => ((RemoteStorageEnumerateUserSubscribedFilesResult_t)(RemoteStorageEnumerateUserSubscribedFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageUnsubscribePublishedFileResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageUnsubscribePublishedFileResult_t) );
		internal static RemoteStorageUnsubscribePublishedFileResult_t Fill( IntPtr p ) => ((RemoteStorageUnsubscribePublishedFileResult_t)(RemoteStorageUnsubscribePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUnsubscribePublishedFileResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageUpdatePublishedFileResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageUpdatePublishedFileResult_t) );
		internal static RemoteStorageUpdatePublishedFileResult_t Fill( IntPtr p ) => ((RemoteStorageUpdatePublishedFileResult_t)(RemoteStorageUpdatePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUpdatePublishedFileResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageDownloadUGCResult_t
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageDownloadUGCResult_t) );
		internal static RemoteStorageDownloadUGCResult_t Fill( IntPtr p ) => ((RemoteStorageDownloadUGCResult_t)(RemoteStorageDownloadUGCResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageDownloadUGCResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageGetPublishedFileDetailsResult_t
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageGetPublishedFileDetailsResult_t) );
		internal static RemoteStorageGetPublishedFileDetailsResult_t Fill( IntPtr p ) => ((RemoteStorageGetPublishedFileDetailsResult_t)(RemoteStorageGetPublishedFileDetailsResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageGetPublishedFileDetailsResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageEnumerateWorkshopFilesResult_t) );
		internal static RemoteStorageEnumerateWorkshopFilesResult_t Fill( IntPtr p ) => ((RemoteStorageEnumerateWorkshopFilesResult_t)(RemoteStorageEnumerateWorkshopFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateWorkshopFilesResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageGetPublishedItemVoteDetailsResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_unPublishedFileId PublishedFileId_t
		internal int VotesFor; // m_nVotesFor int32
		internal int VotesAgainst; // m_nVotesAgainst int32
		internal int Reports; // m_nReports int32
		internal float FScore; // m_fScore float
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageGetPublishedItemVoteDetailsResult_t) );
		internal static RemoteStorageGetPublishedItemVoteDetailsResult_t Fill( IntPtr p ) => ((RemoteStorageGetPublishedItemVoteDetailsResult_t)(RemoteStorageGetPublishedItemVoteDetailsResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageGetPublishedItemVoteDetailsResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStoragePublishedFileSubscribed_t
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStoragePublishedFileSubscribed_t) );
		internal static RemoteStoragePublishedFileSubscribed_t Fill( IntPtr p ) => ((RemoteStoragePublishedFileSubscribed_t)(RemoteStoragePublishedFileSubscribed_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileSubscribed_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStoragePublishedFileUnsubscribed_t
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStoragePublishedFileUnsubscribed_t) );
		internal static RemoteStoragePublishedFileUnsubscribed_t Fill( IntPtr p ) => ((RemoteStoragePublishedFileUnsubscribed_t)(RemoteStoragePublishedFileUnsubscribed_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileUnsubscribed_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStoragePublishedFileDeleted_t
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStoragePublishedFileDeleted_t) );
		internal static RemoteStoragePublishedFileDeleted_t Fill( IntPtr p ) => ((RemoteStoragePublishedFileDeleted_t)(RemoteStoragePublishedFileDeleted_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileDeleted_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageUpdateUserPublishedItemVoteResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageUpdateUserPublishedItemVoteResult_t) );
		internal static RemoteStorageUpdateUserPublishedItemVoteResult_t Fill( IntPtr p ) => ((RemoteStorageUpdateUserPublishedItemVoteResult_t)(RemoteStorageUpdateUserPublishedItemVoteResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUpdateUserPublishedItemVoteResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageUserVoteDetails_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal WorkshopVote Vote; // m_eVote enum EWorkshopVote
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageUserVoteDetails_t) );
		internal static RemoteStorageUserVoteDetails_t Fill( IntPtr p ) => ((RemoteStorageUserVoteDetails_t)(RemoteStorageUserVoteDetails_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUserVoteDetails_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal int ResultsReturned; // m_nResultsReturned int32
		internal int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) );
		internal static RemoteStorageEnumerateUserSharedWorkshopFilesResult_t Fill( IntPtr p ) => ((RemoteStorageEnumerateUserSharedWorkshopFilesResult_t)(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageSetUserPublishedFileActionResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageSetUserPublishedFileActionResult_t) );
		internal static RemoteStorageSetUserPublishedFileActionResult_t Fill( IntPtr p ) => ((RemoteStorageSetUserPublishedFileActionResult_t)(RemoteStorageSetUserPublishedFileActionResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageSetUserPublishedFileActionResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageEnumeratePublishedFilesByUserActionResult_t) );
		internal static RemoteStorageEnumeratePublishedFilesByUserActionResult_t Fill( IntPtr p ) => ((RemoteStorageEnumeratePublishedFilesByUserActionResult_t)(RemoteStorageEnumeratePublishedFilesByUserActionResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumeratePublishedFilesByUserActionResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStoragePublishFileProgress_t
	{
		internal double DPercentFile; // m_dPercentFile double
		[MarshalAs(UnmanagedType.I1)]
		internal bool Preview; // m_bPreview _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStoragePublishFileProgress_t) );
		internal static RemoteStoragePublishFileProgress_t Fill( IntPtr p ) => ((RemoteStoragePublishFileProgress_t)(RemoteStoragePublishFileProgress_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishFileProgress_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStoragePublishedFileUpdated_t
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		internal ulong Unused; // m_ulUnused uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStoragePublishedFileUpdated_t) );
		internal static RemoteStoragePublishedFileUpdated_t Fill( IntPtr p ) => ((RemoteStoragePublishedFileUpdated_t)(RemoteStoragePublishedFileUpdated_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileUpdated_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageFileWriteAsyncComplete_t
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageFileWriteAsyncComplete_t) );
		internal static RemoteStorageFileWriteAsyncComplete_t Fill( IntPtr p ) => ((RemoteStorageFileWriteAsyncComplete_t)(RemoteStorageFileWriteAsyncComplete_t) Marshal.PtrToStructure( p, typeof(RemoteStorageFileWriteAsyncComplete_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoteStorageFileReadAsyncComplete_t
	{
		internal ulong FileReadAsync; // m_hFileReadAsync SteamAPICall_t
		internal Result Result; // m_eResult enum EResult
		internal uint Offset; // m_nOffset uint32
		internal uint Read; // m_cubRead uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoteStorageFileReadAsyncComplete_t) );
		internal static RemoteStorageFileReadAsyncComplete_t Fill( IntPtr p ) => ((RemoteStorageFileReadAsyncComplete_t)(RemoteStorageFileReadAsyncComplete_t) Marshal.PtrToStructure( p, typeof(RemoteStorageFileReadAsyncComplete_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct UserStatsStored_t
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserStatsStored_t) );
		internal static UserStatsStored_t Fill( IntPtr p ) => ((UserStatsStored_t)(UserStatsStored_t) Marshal.PtrToStructure( p, typeof(UserStatsStored_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct UserAchievementStored_t
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserAchievementStored_t) );
		internal static UserAchievementStored_t Fill( IntPtr p ) => ((UserAchievementStored_t)(UserAchievementStored_t) Marshal.PtrToStructure( p, typeof(UserAchievementStored_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LeaderboardFindResult_t
	{
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		internal byte LeaderboardFound; // m_bLeaderboardFound uint8
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LeaderboardFindResult_t) );
		internal static LeaderboardFindResult_t Fill( IntPtr p ) => ((LeaderboardFindResult_t)(LeaderboardFindResult_t) Marshal.PtrToStructure( p, typeof(LeaderboardFindResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LeaderboardScoresDownloaded_t
	{
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		internal ulong SteamLeaderboardEntries; // m_hSteamLeaderboardEntries SteamLeaderboardEntries_t
		internal int CEntryCount; // m_cEntryCount int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LeaderboardScoresDownloaded_t) );
		internal static LeaderboardScoresDownloaded_t Fill( IntPtr p ) => ((LeaderboardScoresDownloaded_t)(LeaderboardScoresDownloaded_t) Marshal.PtrToStructure( p, typeof(LeaderboardScoresDownloaded_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LeaderboardScoreUploaded_t
	{
		internal byte Success; // m_bSuccess uint8
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		internal int Score; // m_nScore int32
		internal byte ScoreChanged; // m_bScoreChanged uint8
		internal int GlobalRankNew; // m_nGlobalRankNew int
		internal int GlobalRankPrevious; // m_nGlobalRankPrevious int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LeaderboardScoreUploaded_t) );
		internal static LeaderboardScoreUploaded_t Fill( IntPtr p ) => ((LeaderboardScoreUploaded_t)(LeaderboardScoreUploaded_t) Marshal.PtrToStructure( p, typeof(LeaderboardScoreUploaded_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct NumberOfCurrentPlayers_t
	{
		internal byte Success; // m_bSuccess uint8
		internal int CPlayers; // m_cPlayers int32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(NumberOfCurrentPlayers_t) );
		internal static NumberOfCurrentPlayers_t Fill( IntPtr p ) => ((NumberOfCurrentPlayers_t)(NumberOfCurrentPlayers_t) Marshal.PtrToStructure( p, typeof(NumberOfCurrentPlayers_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct UserAchievementIconFetched_t
	{
		internal GameId GameID; // m_nGameID class CGameID
		internal string AchievementNameUTF8() => System.Text.Encoding.UTF8.GetString( AchievementName, 0, System.Array.IndexOf<byte>( AchievementName, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)] // byte[] m_rgchAchievementName
		internal byte[] AchievementName; // m_rgchAchievementName char [128]
		[MarshalAs(UnmanagedType.I1)]
		internal bool Achieved; // m_bAchieved _Bool
		internal int IconHandle; // m_nIconHandle int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserAchievementIconFetched_t) );
		internal static UserAchievementIconFetched_t Fill( IntPtr p ) => ((UserAchievementIconFetched_t)(UserAchievementIconFetched_t) Marshal.PtrToStructure( p, typeof(UserAchievementIconFetched_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GlobalAchievementPercentagesReady_t
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GlobalAchievementPercentagesReady_t) );
		internal static GlobalAchievementPercentagesReady_t Fill( IntPtr p ) => ((GlobalAchievementPercentagesReady_t)(GlobalAchievementPercentagesReady_t) Marshal.PtrToStructure( p, typeof(GlobalAchievementPercentagesReady_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LeaderboardUGCSet_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LeaderboardUGCSet_t) );
		internal static LeaderboardUGCSet_t Fill( IntPtr p ) => ((LeaderboardUGCSet_t)(LeaderboardUGCSet_t) Marshal.PtrToStructure( p, typeof(LeaderboardUGCSet_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct PS3TrophiesInstalled_t
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		internal ulong RequiredDiskSpace; // m_ulRequiredDiskSpace uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(PS3TrophiesInstalled_t) );
		internal static PS3TrophiesInstalled_t Fill( IntPtr p ) => ((PS3TrophiesInstalled_t)(PS3TrophiesInstalled_t) Marshal.PtrToStructure( p, typeof(PS3TrophiesInstalled_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GlobalStatsReceived_t
	{
		internal ulong GameID; // m_nGameID uint64
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GlobalStatsReceived_t) );
		internal static GlobalStatsReceived_t Fill( IntPtr p ) => ((GlobalStatsReceived_t)(GlobalStatsReceived_t) Marshal.PtrToStructure( p, typeof(GlobalStatsReceived_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct DlcInstalled_t
	{
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(DlcInstalled_t) );
		internal static DlcInstalled_t Fill( IntPtr p ) => ((DlcInstalled_t)(DlcInstalled_t) Marshal.PtrToStructure( p, typeof(DlcInstalled_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RegisterActivationCodeResponse_t
	{
		internal RegisterActivationCodeResult Result; // m_eResult enum ERegisterActivationCodeResult
		internal uint PackageRegistered; // m_unPackageRegistered uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RegisterActivationCodeResponse_t) );
		internal static RegisterActivationCodeResponse_t Fill( IntPtr p ) => ((RegisterActivationCodeResponse_t)(RegisterActivationCodeResponse_t) Marshal.PtrToStructure( p, typeof(RegisterActivationCodeResponse_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct AppProofOfPurchaseKeyResponse_t
	{
		internal Result Result; // m_eResult enum EResult
		internal uint AppID; // m_nAppID uint32
		internal uint CchKeyLength; // m_cchKeyLength uint32
		internal string KeyUTF8() => System.Text.Encoding.UTF8.GetString( Key, 0, System.Array.IndexOf<byte>( Key, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 240)] // byte[] m_rgchKey
		internal byte[] Key; // m_rgchKey char [240]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(AppProofOfPurchaseKeyResponse_t) );
		internal static AppProofOfPurchaseKeyResponse_t Fill( IntPtr p ) => ((AppProofOfPurchaseKeyResponse_t)(AppProofOfPurchaseKeyResponse_t) Marshal.PtrToStructure( p, typeof(AppProofOfPurchaseKeyResponse_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct FileDetailsResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal ulong FileSize; // m_ulFileSize uint64
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] //  m_FileSHA
		internal byte[] FileSHA; // m_FileSHA uint8 [20]
		internal uint Flags; // m_unFlags uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(FileDetailsResult_t) );
		internal static FileDetailsResult_t Fill( IntPtr p ) => ((FileDetailsResult_t)(FileDetailsResult_t) Marshal.PtrToStructure( p, typeof(FileDetailsResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct ScreenshotReady_t
	{
		internal uint Local; // m_hLocal ScreenshotHandle
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ScreenshotReady_t) );
		internal static ScreenshotReady_t Fill( IntPtr p ) => ((ScreenshotReady_t)(ScreenshotReady_t) Marshal.PtrToStructure( p, typeof(ScreenshotReady_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct VolumeHasChanged_t
	{
		internal float NewVolume; // m_flNewVolume float
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(VolumeHasChanged_t) );
		internal static VolumeHasChanged_t Fill( IntPtr p ) => ((VolumeHasChanged_t)(VolumeHasChanged_t) Marshal.PtrToStructure( p, typeof(VolumeHasChanged_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerWantsShuffled_t
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Shuffled; // m_bShuffled _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerWantsShuffled_t) );
		internal static MusicPlayerWantsShuffled_t Fill( IntPtr p ) => ((MusicPlayerWantsShuffled_t)(MusicPlayerWantsShuffled_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsShuffled_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerWantsLooped_t
	{
		[MarshalAs(UnmanagedType.I1)]
		internal bool Looped; // m_bLooped _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerWantsLooped_t) );
		internal static MusicPlayerWantsLooped_t Fill( IntPtr p ) => ((MusicPlayerWantsLooped_t)(MusicPlayerWantsLooped_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsLooped_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerWantsVolume_t
	{
		internal float NewVolume; // m_flNewVolume float
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerWantsVolume_t) );
		internal static MusicPlayerWantsVolume_t Fill( IntPtr p ) => ((MusicPlayerWantsVolume_t)(MusicPlayerWantsVolume_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsVolume_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerSelectsQueueEntry_t
	{
		internal int NID; // nID int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerSelectsQueueEntry_t) );
		internal static MusicPlayerSelectsQueueEntry_t Fill( IntPtr p ) => ((MusicPlayerSelectsQueueEntry_t)(MusicPlayerSelectsQueueEntry_t) Marshal.PtrToStructure( p, typeof(MusicPlayerSelectsQueueEntry_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerSelectsPlaylistEntry_t
	{
		internal int NID; // nID int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerSelectsPlaylistEntry_t) );
		internal static MusicPlayerSelectsPlaylistEntry_t Fill( IntPtr p ) => ((MusicPlayerSelectsPlaylistEntry_t)(MusicPlayerSelectsPlaylistEntry_t) Marshal.PtrToStructure( p, typeof(MusicPlayerSelectsPlaylistEntry_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct MusicPlayerWantsPlayingRepeatStatus_t
	{
		internal int PlayingRepeatStatus; // m_nPlayingRepeatStatus int
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(MusicPlayerWantsPlayingRepeatStatus_t) );
		internal static MusicPlayerWantsPlayingRepeatStatus_t Fill( IntPtr p ) => ((MusicPlayerWantsPlayingRepeatStatus_t)(MusicPlayerWantsPlayingRepeatStatus_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsPlayingRepeatStatus_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTTPRequestCompleted_t
	{
		internal uint Request; // m_hRequest HTTPRequestHandle
		internal ulong ContextValue; // m_ulContextValue uint64
		[MarshalAs(UnmanagedType.I1)]
		internal bool RequestSuccessful; // m_bRequestSuccessful _Bool
		internal HTTPStatusCode StatusCode; // m_eStatusCode enum EHTTPStatusCode
		internal uint BodySize; // m_unBodySize uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTTPRequestCompleted_t) );
		internal static HTTPRequestCompleted_t Fill( IntPtr p ) => ((HTTPRequestCompleted_t)(HTTPRequestCompleted_t) Marshal.PtrToStructure( p, typeof(HTTPRequestCompleted_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTTPRequestHeadersReceived_t
	{
		internal uint Request; // m_hRequest HTTPRequestHandle
		internal ulong ContextValue; // m_ulContextValue uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTTPRequestHeadersReceived_t) );
		internal static HTTPRequestHeadersReceived_t Fill( IntPtr p ) => ((HTTPRequestHeadersReceived_t)(HTTPRequestHeadersReceived_t) Marshal.PtrToStructure( p, typeof(HTTPRequestHeadersReceived_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTTPRequestDataReceived_t
	{
		internal uint Request; // m_hRequest HTTPRequestHandle
		internal ulong ContextValue; // m_ulContextValue uint64
		internal uint COffset; // m_cOffset uint32
		internal uint CBytesReceived; // m_cBytesReceived uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTTPRequestDataReceived_t) );
		internal static HTTPRequestDataReceived_t Fill( IntPtr p ) => ((HTTPRequestDataReceived_t)(HTTPRequestDataReceived_t) Marshal.PtrToStructure( p, typeof(HTTPRequestDataReceived_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	internal struct SteamUGCQueryCompleted_t
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamUGCQueryCompleted_t) );
		internal static SteamUGCQueryCompleted_t Fill( IntPtr p ) => ((SteamUGCQueryCompleted_t)(SteamUGCQueryCompleted_t) Marshal.PtrToStructure( p, typeof(SteamUGCQueryCompleted_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamUGCRequestUGCDetailsResult_t
	{
		internal SteamUGCDetails_t Details; // m_details struct SteamUGCDetails_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData; // m_bCachedData _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamUGCRequestUGCDetailsResult_t) );
		internal static SteamUGCRequestUGCDetailsResult_t Fill( IntPtr p ) => ((SteamUGCRequestUGCDetailsResult_t)(SteamUGCRequestUGCDetailsResult_t) Marshal.PtrToStructure( p, typeof(SteamUGCRequestUGCDetailsResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct CreateItemResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(CreateItemResult_t) );
		internal static CreateItemResult_t Fill( IntPtr p ) => ((CreateItemResult_t)(CreateItemResult_t) Marshal.PtrToStructure( p, typeof(CreateItemResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SubmitItemUpdateResult_t
	{
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SubmitItemUpdateResult_t) );
		internal static SubmitItemUpdateResult_t Fill( IntPtr p ) => ((SubmitItemUpdateResult_t)(SubmitItemUpdateResult_t) Marshal.PtrToStructure( p, typeof(SubmitItemUpdateResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct DownloadItemResult_t
	{
		internal AppId AppID; // m_unAppID AppId_t
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(DownloadItemResult_t) );
		internal static DownloadItemResult_t Fill( IntPtr p ) => ((DownloadItemResult_t)(DownloadItemResult_t) Marshal.PtrToStructure( p, typeof(DownloadItemResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct UserFavoriteItemsListChanged_t
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool WasAddRequest; // m_bWasAddRequest _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(UserFavoriteItemsListChanged_t) );
		internal static UserFavoriteItemsListChanged_t Fill( IntPtr p ) => ((UserFavoriteItemsListChanged_t)(UserFavoriteItemsListChanged_t) Marshal.PtrToStructure( p, typeof(UserFavoriteItemsListChanged_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SetUserItemVoteResult_t
	{
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		internal bool VoteUp; // m_bVoteUp _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SetUserItemVoteResult_t) );
		internal static SetUserItemVoteResult_t Fill( IntPtr p ) => ((SetUserItemVoteResult_t)(SetUserItemVoteResult_t) Marshal.PtrToStructure( p, typeof(SetUserItemVoteResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GetUserItemVoteResult_t) );
		internal static GetUserItemVoteResult_t Fill( IntPtr p ) => ((GetUserItemVoteResult_t)(GetUserItemVoteResult_t) Marshal.PtrToStructure( p, typeof(GetUserItemVoteResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct StartPlaytimeTrackingResult_t
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(StartPlaytimeTrackingResult_t) );
		internal static StartPlaytimeTrackingResult_t Fill( IntPtr p ) => ((StartPlaytimeTrackingResult_t)(StartPlaytimeTrackingResult_t) Marshal.PtrToStructure( p, typeof(StartPlaytimeTrackingResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct StopPlaytimeTrackingResult_t
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(StopPlaytimeTrackingResult_t) );
		internal static StopPlaytimeTrackingResult_t Fill( IntPtr p ) => ((StopPlaytimeTrackingResult_t)(StopPlaytimeTrackingResult_t) Marshal.PtrToStructure( p, typeof(StopPlaytimeTrackingResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct AddUGCDependencyResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal PublishedFileId ChildPublishedFileId; // m_nChildPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(AddUGCDependencyResult_t) );
		internal static AddUGCDependencyResult_t Fill( IntPtr p ) => ((AddUGCDependencyResult_t)(AddUGCDependencyResult_t) Marshal.PtrToStructure( p, typeof(AddUGCDependencyResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoveUGCDependencyResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal PublishedFileId ChildPublishedFileId; // m_nChildPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoveUGCDependencyResult_t) );
		internal static RemoveUGCDependencyResult_t Fill( IntPtr p ) => ((RemoveUGCDependencyResult_t)(RemoveUGCDependencyResult_t) Marshal.PtrToStructure( p, typeof(RemoveUGCDependencyResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct AddAppDependencyResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(AddAppDependencyResult_t) );
		internal static AddAppDependencyResult_t Fill( IntPtr p ) => ((AddAppDependencyResult_t)(AddAppDependencyResult_t) Marshal.PtrToStructure( p, typeof(AddAppDependencyResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct RemoveAppDependencyResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(RemoveAppDependencyResult_t) );
		internal static RemoveAppDependencyResult_t Fill( IntPtr p ) => ((RemoveAppDependencyResult_t)(RemoveAppDependencyResult_t) Marshal.PtrToStructure( p, typeof(RemoveAppDependencyResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GetAppDependenciesResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.U4)]
		internal AppId[] GAppIDs; // m_rgAppIDs AppId_t [32]
		internal uint NumAppDependencies; // m_nNumAppDependencies uint32
		internal uint TotalNumAppDependencies; // m_nTotalNumAppDependencies uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GetAppDependenciesResult_t) );
		internal static GetAppDependenciesResult_t Fill( IntPtr p ) => ((GetAppDependenciesResult_t)(GetAppDependenciesResult_t) Marshal.PtrToStructure( p, typeof(GetAppDependenciesResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct DeleteItemResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(DeleteItemResult_t) );
		internal static DeleteItemResult_t Fill( IntPtr p ) => ((DeleteItemResult_t)(DeleteItemResult_t) Marshal.PtrToStructure( p, typeof(DeleteItemResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamAppInstalled_t
	{
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamAppInstalled_t) );
		internal static SteamAppInstalled_t Fill( IntPtr p ) => ((SteamAppInstalled_t)(SteamAppInstalled_t) Marshal.PtrToStructure( p, typeof(SteamAppInstalled_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamAppUninstalled_t
	{
		internal AppId AppID; // m_nAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamAppUninstalled_t) );
		internal static SteamAppUninstalled_t Fill( IntPtr p ) => ((SteamAppUninstalled_t)(SteamAppUninstalled_t) Marshal.PtrToStructure( p, typeof(SteamAppUninstalled_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_BrowserReady_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_BrowserReady_t) );
		internal static HTML_BrowserReady_t Fill( IntPtr p ) => ((HTML_BrowserReady_t)(HTML_BrowserReady_t) Marshal.PtrToStructure( p, typeof(HTML_BrowserReady_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_URLChanged_t) );
		internal static HTML_URLChanged_t Fill( IntPtr p ) => ((HTML_URLChanged_t)(HTML_URLChanged_t) Marshal.PtrToStructure( p, typeof(HTML_URLChanged_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_FinishedRequest_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		internal string PchPageTitle; // pchPageTitle const char *
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_FinishedRequest_t) );
		internal static HTML_FinishedRequest_t Fill( IntPtr p ) => ((HTML_FinishedRequest_t)(HTML_FinishedRequest_t) Marshal.PtrToStructure( p, typeof(HTML_FinishedRequest_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_OpenLinkInNewTab_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchURL; // pchURL const char *
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_OpenLinkInNewTab_t) );
		internal static HTML_OpenLinkInNewTab_t Fill( IntPtr p ) => ((HTML_OpenLinkInNewTab_t)(HTML_OpenLinkInNewTab_t) Marshal.PtrToStructure( p, typeof(HTML_OpenLinkInNewTab_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_ChangedTitle_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchTitle; // pchTitle const char *
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_ChangedTitle_t) );
		internal static HTML_ChangedTitle_t Fill( IntPtr p ) => ((HTML_ChangedTitle_t)(HTML_ChangedTitle_t) Marshal.PtrToStructure( p, typeof(HTML_ChangedTitle_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_SearchResults_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint UnResults; // unResults uint32
		internal uint UnCurrentMatch; // unCurrentMatch uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_SearchResults_t) );
		internal static HTML_SearchResults_t Fill( IntPtr p ) => ((HTML_SearchResults_t)(HTML_SearchResults_t) Marshal.PtrToStructure( p, typeof(HTML_SearchResults_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_CanGoBackAndForward_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		[MarshalAs(UnmanagedType.I1)]
		internal bool BCanGoBack; // bCanGoBack _Bool
		[MarshalAs(UnmanagedType.I1)]
		internal bool BCanGoForward; // bCanGoForward _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_CanGoBackAndForward_t) );
		internal static HTML_CanGoBackAndForward_t Fill( IntPtr p ) => ((HTML_CanGoBackAndForward_t)(HTML_CanGoBackAndForward_t) Marshal.PtrToStructure( p, typeof(HTML_CanGoBackAndForward_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_HorizontalScroll_t) );
		internal static HTML_HorizontalScroll_t Fill( IntPtr p ) => ((HTML_HorizontalScroll_t)(HTML_HorizontalScroll_t) Marshal.PtrToStructure( p, typeof(HTML_HorizontalScroll_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_VerticalScroll_t) );
		internal static HTML_VerticalScroll_t Fill( IntPtr p ) => ((HTML_VerticalScroll_t)(HTML_VerticalScroll_t) Marshal.PtrToStructure( p, typeof(HTML_VerticalScroll_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_LinkAtPosition_t) );
		internal static HTML_LinkAtPosition_t Fill( IntPtr p ) => ((HTML_LinkAtPosition_t)(HTML_LinkAtPosition_t) Marshal.PtrToStructure( p, typeof(HTML_LinkAtPosition_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_JSAlert_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMessage; // pchMessage const char *
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_JSAlert_t) );
		internal static HTML_JSAlert_t Fill( IntPtr p ) => ((HTML_JSAlert_t)(HTML_JSAlert_t) Marshal.PtrToStructure( p, typeof(HTML_JSAlert_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_JSConfirm_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMessage; // pchMessage const char *
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_JSConfirm_t) );
		internal static HTML_JSConfirm_t Fill( IntPtr p ) => ((HTML_JSConfirm_t)(HTML_JSConfirm_t) Marshal.PtrToStructure( p, typeof(HTML_JSConfirm_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_FileOpenDialog_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchTitle; // pchTitle const char *
		internal string PchInitialFile; // pchInitialFile const char *
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_FileOpenDialog_t) );
		internal static HTML_FileOpenDialog_t Fill( IntPtr p ) => ((HTML_FileOpenDialog_t)(HTML_FileOpenDialog_t) Marshal.PtrToStructure( p, typeof(HTML_FileOpenDialog_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_NewWindow_t) );
		internal static HTML_NewWindow_t Fill( IntPtr p ) => ((HTML_NewWindow_t)(HTML_NewWindow_t) Marshal.PtrToStructure( p, typeof(HTML_NewWindow_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_SetCursor_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint EMouseCursor; // eMouseCursor uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_SetCursor_t) );
		internal static HTML_SetCursor_t Fill( IntPtr p ) => ((HTML_SetCursor_t)(HTML_SetCursor_t) Marshal.PtrToStructure( p, typeof(HTML_SetCursor_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_StatusText_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMsg; // pchMsg const char *
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_StatusText_t) );
		internal static HTML_StatusText_t Fill( IntPtr p ) => ((HTML_StatusText_t)(HTML_StatusText_t) Marshal.PtrToStructure( p, typeof(HTML_StatusText_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_ShowToolTip_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMsg; // pchMsg const char *
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_ShowToolTip_t) );
		internal static HTML_ShowToolTip_t Fill( IntPtr p ) => ((HTML_ShowToolTip_t)(HTML_ShowToolTip_t) Marshal.PtrToStructure( p, typeof(HTML_ShowToolTip_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_UpdateToolTip_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal string PchMsg; // pchMsg const char *
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_UpdateToolTip_t) );
		internal static HTML_UpdateToolTip_t Fill( IntPtr p ) => ((HTML_UpdateToolTip_t)(HTML_UpdateToolTip_t) Marshal.PtrToStructure( p, typeof(HTML_UpdateToolTip_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_HideToolTip_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_HideToolTip_t) );
		internal static HTML_HideToolTip_t Fill( IntPtr p ) => ((HTML_HideToolTip_t)(HTML_HideToolTip_t) Marshal.PtrToStructure( p, typeof(HTML_HideToolTip_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct HTML_BrowserRestarted_t
	{
		internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		internal uint UnOldBrowserHandle; // unOldBrowserHandle HHTMLBrowser
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(HTML_BrowserRestarted_t) );
		internal static HTML_BrowserRestarted_t Fill( IntPtr p ) => ((HTML_BrowserRestarted_t)(HTML_BrowserRestarted_t) Marshal.PtrToStructure( p, typeof(HTML_BrowserRestarted_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	internal struct SteamInventoryResultReady_t
	{
		internal int Handle; // m_handle SteamInventoryResult_t
		internal Result Result; // m_result enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamInventoryResultReady_t) );
		internal static SteamInventoryResultReady_t Fill( IntPtr p ) => ((SteamInventoryResultReady_t)(SteamInventoryResultReady_t) Marshal.PtrToStructure( p, typeof(SteamInventoryResultReady_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamInventoryFullUpdate_t
	{
		internal int Handle; // m_handle SteamInventoryResult_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamInventoryFullUpdate_t) );
		internal static SteamInventoryFullUpdate_t Fill( IntPtr p ) => ((SteamInventoryFullUpdate_t)(SteamInventoryFullUpdate_t) Marshal.PtrToStructure( p, typeof(SteamInventoryFullUpdate_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamInventoryStartPurchaseResult_t
	{
		internal Result Result; // m_result enum EResult
		internal ulong OrderID; // m_ulOrderID uint64
		internal ulong TransID; // m_ulTransID uint64
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamInventoryStartPurchaseResult_t) );
		internal static SteamInventoryStartPurchaseResult_t Fill( IntPtr p ) => ((SteamInventoryStartPurchaseResult_t)(SteamInventoryStartPurchaseResult_t) Marshal.PtrToStructure( p, typeof(SteamInventoryStartPurchaseResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamInventoryRequestPricesResult_t
	{
		internal Result Result; // m_result enum EResult
		internal string CurrencyUTF8() => System.Text.Encoding.UTF8.GetString( Currency, 0, System.Array.IndexOf<byte>( Currency, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] // byte[] m_rgchCurrency
		internal byte[] Currency; // m_rgchCurrency char [4]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamInventoryRequestPricesResult_t) );
		internal static SteamInventoryRequestPricesResult_t Fill( IntPtr p ) => ((SteamInventoryRequestPricesResult_t)(SteamInventoryRequestPricesResult_t) Marshal.PtrToStructure( p, typeof(SteamInventoryRequestPricesResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct BroadcastUploadStop_t
	{
		internal BroadcastUploadResult Result; // m_eResult enum EBroadcastUploadResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(BroadcastUploadStop_t) );
		internal static BroadcastUploadStop_t Fill( IntPtr p ) => ((BroadcastUploadStop_t)(BroadcastUploadStop_t) Marshal.PtrToStructure( p, typeof(BroadcastUploadStop_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GetVideoURLResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal AppId VideoAppID; // m_unVideoAppID AppId_t
		internal string URLUTF8() => System.Text.Encoding.UTF8.GetString( URL, 0, System.Array.IndexOf<byte>( URL, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] // byte[] m_rgchURL
		internal byte[] URL; // m_rgchURL char [256]
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GetVideoURLResult_t) );
		internal static GetVideoURLResult_t Fill( IntPtr p ) => ((GetVideoURLResult_t)(GetVideoURLResult_t) Marshal.PtrToStructure( p, typeof(GetVideoURLResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GetOPFSettingsResult_t
	{
		internal Result Result; // m_eResult enum EResult
		internal AppId VideoAppID; // m_unVideoAppID AppId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GetOPFSettingsResult_t) );
		internal static GetOPFSettingsResult_t Fill( IntPtr p ) => ((GetOPFSettingsResult_t)(GetOPFSettingsResult_t) Marshal.PtrToStructure( p, typeof(GetOPFSettingsResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal struct GSClientDeny_t
	{
		internal ulong SteamID; // m_SteamID class CSteamID
		internal DenyReason DenyReason; // m_eDenyReason enum EDenyReason
		internal string OptionalTextUTF8() => System.Text.Encoding.UTF8.GetString( OptionalText, 0, System.Array.IndexOf<byte>( OptionalText, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)] // byte[] m_rgchOptionalText
		internal byte[] OptionalText; // m_rgchOptionalText char [128]
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GSClientAchievementStatus_t
	{
		internal ulong SteamID; // m_SteamID uint64
		internal string PchAchievementUTF8() => System.Text.Encoding.UTF8.GetString( PchAchievement, 0, System.Array.IndexOf<byte>( PchAchievement, 0 ) );
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)] // byte[] m_pchAchievement
		internal byte[] PchAchievement; // m_pchAchievement char [128]
		[MarshalAs(UnmanagedType.I1)]
		internal bool Unlocked; // m_bUnlocked _Bool
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSClientAchievementStatus_t) );
		internal static GSClientAchievementStatus_t Fill( IntPtr p ) => ((GSClientAchievementStatus_t)(GSClientAchievementStatus_t) Marshal.PtrToStructure( p, typeof(GSClientAchievementStatus_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GSPolicyResponse_t
	{
		internal byte Secure; // m_bSecure uint8
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSPolicyResponse_t) );
		internal static GSPolicyResponse_t Fill( IntPtr p ) => ((GSPolicyResponse_t)(GSPolicyResponse_t) Marshal.PtrToStructure( p, typeof(GSPolicyResponse_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GSGameplayStats_t
	{
		internal Result Result; // m_eResult enum EResult
		internal int Rank; // m_nRank int32
		internal uint TotalConnects; // m_unTotalConnects uint32
		internal uint TotalMinutesPlayed; // m_unTotalMinutesPlayed uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSGameplayStats_t) );
		internal static GSGameplayStats_t Fill( IntPtr p ) => ((GSGameplayStats_t)(GSGameplayStats_t) Marshal.PtrToStructure( p, typeof(GSGameplayStats_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
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
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GSReputation_t) );
		internal static GSReputation_t Fill( IntPtr p ) => ((GSReputation_t)(GSReputation_t) Marshal.PtrToStructure( p, typeof(GSReputation_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct AssociateWithClanResult_t
	{
		internal Result Result; // m_eResult enum EResult
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(AssociateWithClanResult_t) );
		internal static AssociateWithClanResult_t Fill( IntPtr p ) => ((AssociateWithClanResult_t)(AssociateWithClanResult_t) Marshal.PtrToStructure( p, typeof(AssociateWithClanResult_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct AvailableBeaconLocationsUpdated_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(AvailableBeaconLocationsUpdated_t) );
		internal static AvailableBeaconLocationsUpdated_t Fill( IntPtr p ) => ((AvailableBeaconLocationsUpdated_t)(AvailableBeaconLocationsUpdated_t) Marshal.PtrToStructure( p, typeof(AvailableBeaconLocationsUpdated_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct ActiveBeaconsUpdated_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ActiveBeaconsUpdated_t) );
		internal static ActiveBeaconsUpdated_t Fill( IntPtr p ) => ((ActiveBeaconsUpdated_t)(ActiveBeaconsUpdated_t) Marshal.PtrToStructure( p, typeof(ActiveBeaconsUpdated_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct PlaybackStatusHasChanged_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(PlaybackStatusHasChanged_t) );
		internal static PlaybackStatusHasChanged_t Fill( IntPtr p ) => ((PlaybackStatusHasChanged_t)(PlaybackStatusHasChanged_t) Marshal.PtrToStructure( p, typeof(PlaybackStatusHasChanged_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct BroadcastUploadStart_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(BroadcastUploadStart_t) );
		internal static BroadcastUploadStart_t Fill( IntPtr p ) => ((BroadcastUploadStart_t)(BroadcastUploadStart_t) Marshal.PtrToStructure( p, typeof(BroadcastUploadStart_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct NewUrlLaunchParameters_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(NewUrlLaunchParameters_t) );
		internal static NewUrlLaunchParameters_t Fill( IntPtr p ) => ((NewUrlLaunchParameters_t)(NewUrlLaunchParameters_t) Marshal.PtrToStructure( p, typeof(NewUrlLaunchParameters_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct ItemInstalled_t
	{
		internal AppId AppID; // m_unAppID AppId_t
		internal PublishedFileId PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ItemInstalled_t) );
		internal static ItemInstalled_t Fill( IntPtr p ) => ((ItemInstalled_t)(ItemInstalled_t) Marshal.PtrToStructure( p, typeof(ItemInstalled_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamNetConnectionStatusChangedCallback_t
	{
		internal Connection Conn; // m_hConn HSteamNetConnection
		internal ConnectionInfo Nfo; // m_info SteamNetConnectionInfo_t
		internal ConnectionState OldState; // m_eOldState ESteamNetworkingConnectionState
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamNetConnectionStatusChangedCallback_t) );
		internal static SteamNetConnectionStatusChangedCallback_t Fill( IntPtr p ) => ((SteamNetConnectionStatusChangedCallback_t)(SteamNetConnectionStatusChangedCallback_t) Marshal.PtrToStructure( p, typeof(SteamNetConnectionStatusChangedCallback_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamInventoryDefinitionUpdate_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamInventoryDefinitionUpdate_t) );
		internal static SteamInventoryDefinitionUpdate_t Fill( IntPtr p ) => ((SteamInventoryDefinitionUpdate_t)(SteamInventoryDefinitionUpdate_t) Marshal.PtrToStructure( p, typeof(SteamInventoryDefinitionUpdate_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamParentalSettingsChanged_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamParentalSettingsChanged_t) );
		internal static SteamParentalSettingsChanged_t Fill( IntPtr p ) => ((SteamParentalSettingsChanged_t)(SteamParentalSettingsChanged_t) Marshal.PtrToStructure( p, typeof(SteamParentalSettingsChanged_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamServersConnected_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamServersConnected_t) );
		internal static SteamServersConnected_t Fill( IntPtr p ) => ((SteamServersConnected_t)(SteamServersConnected_t) Marshal.PtrToStructure( p, typeof(SteamServersConnected_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct NewLaunchQueryParameters_t
	{
		
		#region Marshalling
		internal static NewLaunchQueryParameters_t Fill( IntPtr p ) => ((NewLaunchQueryParameters_t)(NewLaunchQueryParameters_t) Marshal.PtrToStructure( p, typeof(NewLaunchQueryParameters_t) ) );
		#endregion
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GCMessageAvailable_t
	{
		internal uint MessageSize; // m_nMessageSize uint32
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GCMessageAvailable_t) );
		internal static GCMessageAvailable_t Fill( IntPtr p ) => ((GCMessageAvailable_t)(GCMessageAvailable_t) Marshal.PtrToStructure( p, typeof(GCMessageAvailable_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct GCMessageFailed_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(GCMessageFailed_t) );
		internal static GCMessageFailed_t Fill( IntPtr p ) => ((GCMessageFailed_t)(GCMessageFailed_t) Marshal.PtrToStructure( p, typeof(GCMessageFailed_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct ScreenshotRequested_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(ScreenshotRequested_t) );
		internal static ScreenshotRequested_t Fill( IntPtr p ) => ((ScreenshotRequested_t)(ScreenshotRequested_t) Marshal.PtrToStructure( p, typeof(ScreenshotRequested_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct LicensesUpdated_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(LicensesUpdated_t) );
		internal static LicensesUpdated_t Fill( IntPtr p ) => ((LicensesUpdated_t)(LicensesUpdated_t) Marshal.PtrToStructure( p, typeof(LicensesUpdated_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct SteamShutdown_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(SteamShutdown_t) );
		internal static SteamShutdown_t Fill( IntPtr p ) => ((SteamShutdown_t)(SteamShutdown_t) Marshal.PtrToStructure( p, typeof(SteamShutdown_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct IPCountry_t
	{
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(IPCountry_t) );
		internal static IPCountry_t Fill( IntPtr p ) => ((IPCountry_t)(IPCountry_t) Marshal.PtrToStructure( p, typeof(IPCountry_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	internal struct IPCFailure_t
	{
		internal byte FailureType; // m_eFailureType uint8
		
		#region SteamCallback
		internal static readonly int StructSize = System.Runtime.InteropServices.Marshal.SizeOf( typeof(IPCFailure_t) );
		internal static IPCFailure_t Fill( IntPtr p ) => ((IPCFailure_t)(IPCFailure_t) Marshal.PtrToStructure( p, typeof(IPCFailure_t) ) );
		
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
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
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
	}
	
}
