using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct CallbackMsg_t
	{
		public int SteamUser; // m_hSteamUser HSteamUser
		public int Callback; // m_iCallback int
		public IntPtr ParamPtr; // m_pubParam uint8 *
		public int ParamCount; // m_cubParam int
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static CallbackMsg_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (CallbackMsg_t) Marshal.PtrToStructure( p, typeof(CallbackMsg_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public int SteamUser; // m_hSteamUser HSteamUser
			public int Callback; // m_iCallback int
			public IntPtr ParamPtr; // m_pubParam uint8 *
			public int ParamCount; // m_cubParam int
			
			//
			// Easily convert from PackSmall to CallbackMsg_t
			//
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
	internal struct SteamServerConnectFailure_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUser + 2;
		public Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		public bool StillRetrying; // m_bStillRetrying _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static SteamServerConnectFailure_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (SteamServerConnectFailure_t) Marshal.PtrToStructure( p, typeof(SteamServerConnectFailure_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			public bool StillRetrying; // m_bStillRetrying _Bool
			
			//
			// Easily convert from PackSmall to SteamServerConnectFailure_t
			//
			public static implicit operator SteamServerConnectFailure_t (  SteamServerConnectFailure_t.PackSmall d )
			{
				return new SteamServerConnectFailure_t()
				{
					Result = d.Result,
					StillRetrying = d.StillRetrying,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<SteamServerConnectFailure_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( SteamServerConnectFailure_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct SteamServersDisconnected_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUser + 3;
		public Result Result; // m_eResult enum EResult
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static SteamServersDisconnected_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (SteamServersDisconnected_t) Marshal.PtrToStructure( p, typeof(SteamServersDisconnected_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			
			//
			// Easily convert from PackSmall to SteamServersDisconnected_t
			//
			public static implicit operator SteamServersDisconnected_t (  SteamServersDisconnected_t.PackSmall d )
			{
				return new SteamServersDisconnected_t()
				{
					Result = d.Result,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<SteamServersDisconnected_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( SteamServersDisconnected_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct ClientGameServerDeny_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUser + 13;
		public uint AppID; // m_uAppID uint32
		public uint GameServerIP; // m_unGameServerIP uint32
		public ushort GameServerPort; // m_usGameServerPort uint16
		public ushort Secure; // m_bSecure uint16
		public uint Reason; // m_uReason uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static ClientGameServerDeny_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (ClientGameServerDeny_t) Marshal.PtrToStructure( p, typeof(ClientGameServerDeny_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint AppID; // m_uAppID uint32
			public uint GameServerIP; // m_unGameServerIP uint32
			public ushort GameServerPort; // m_usGameServerPort uint16
			public ushort Secure; // m_bSecure uint16
			public uint Reason; // m_uReason uint32
			
			//
			// Easily convert from PackSmall to ClientGameServerDeny_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<ClientGameServerDeny_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( ClientGameServerDeny_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct ValidateAuthTicketResponse_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUser + 43;
		public ulong SteamID; // m_SteamID class CSteamID
		public AuthSessionResponse AuthSessionResponse; // m_eAuthSessionResponse enum EAuthSessionResponse
		public ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static ValidateAuthTicketResponse_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (ValidateAuthTicketResponse_t) Marshal.PtrToStructure( p, typeof(ValidateAuthTicketResponse_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamID; // m_SteamID class CSteamID
			public AuthSessionResponse AuthSessionResponse; // m_eAuthSessionResponse enum EAuthSessionResponse
			public ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
			
			//
			// Easily convert from PackSmall to ValidateAuthTicketResponse_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<ValidateAuthTicketResponse_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( ValidateAuthTicketResponse_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct MicroTxnAuthorizationResponse_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUser + 52;
		public uint AppID; // m_unAppID uint32
		public ulong OrderID; // m_ulOrderID uint64
		public byte Authorized; // m_bAuthorized uint8
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static MicroTxnAuthorizationResponse_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (MicroTxnAuthorizationResponse_t) Marshal.PtrToStructure( p, typeof(MicroTxnAuthorizationResponse_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint AppID; // m_unAppID uint32
			public ulong OrderID; // m_ulOrderID uint64
			public byte Authorized; // m_bAuthorized uint8
			
			//
			// Easily convert from PackSmall to MicroTxnAuthorizationResponse_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<MicroTxnAuthorizationResponse_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( MicroTxnAuthorizationResponse_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct EncryptedAppTicketResponse_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUser + 54;
		public Result Result; // m_eResult enum EResult
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static EncryptedAppTicketResponse_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (EncryptedAppTicketResponse_t) Marshal.PtrToStructure( p, typeof(EncryptedAppTicketResponse_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			
			//
			// Easily convert from PackSmall to EncryptedAppTicketResponse_t
			//
			public static implicit operator EncryptedAppTicketResponse_t (  EncryptedAppTicketResponse_t.PackSmall d )
			{
				return new EncryptedAppTicketResponse_t()
				{
					Result = d.Result,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<EncryptedAppTicketResponse_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( EncryptedAppTicketResponse_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct GetAuthSessionTicketResponse_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUser + 63;
		public uint AuthTicket; // m_hAuthTicket HAuthTicket
		public Result Result; // m_eResult enum EResult
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GetAuthSessionTicketResponse_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GetAuthSessionTicketResponse_t) Marshal.PtrToStructure( p, typeof(GetAuthSessionTicketResponse_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint AuthTicket; // m_hAuthTicket HAuthTicket
			public Result Result; // m_eResult enum EResult
			
			//
			// Easily convert from PackSmall to GetAuthSessionTicketResponse_t
			//
			public static implicit operator GetAuthSessionTicketResponse_t (  GetAuthSessionTicketResponse_t.PackSmall d )
			{
				return new GetAuthSessionTicketResponse_t()
				{
					AuthTicket = d.AuthTicket,
					Result = d.Result,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GetAuthSessionTicketResponse_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GetAuthSessionTicketResponse_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct GameWebCallback_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUser + 64;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string URL; // m_szURL char [256]
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GameWebCallback_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GameWebCallback_t) Marshal.PtrToStructure( p, typeof(GameWebCallback_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string URL; // m_szURL char [256]
			
			//
			// Easily convert from PackSmall to GameWebCallback_t
			//
			public static implicit operator GameWebCallback_t (  GameWebCallback_t.PackSmall d )
			{
				return new GameWebCallback_t()
				{
					URL = d.URL,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GameWebCallback_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GameWebCallback_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct StoreAuthURLResponse_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUser + 65;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
		public string URL; // m_szURL char [512]
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static StoreAuthURLResponse_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (StoreAuthURLResponse_t) Marshal.PtrToStructure( p, typeof(StoreAuthURLResponse_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
			public string URL; // m_szURL char [512]
			
			//
			// Easily convert from PackSmall to StoreAuthURLResponse_t
			//
			public static implicit operator StoreAuthURLResponse_t (  StoreAuthURLResponse_t.PackSmall d )
			{
				return new StoreAuthURLResponse_t()
				{
					URL = d.URL,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<StoreAuthURLResponse_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( StoreAuthURLResponse_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FriendGameInfo_t
	{
		public ulong GameID; // m_gameID class CGameID
		public uint GameIP; // m_unGameIP uint32
		public ushort GamePort; // m_usGamePort uint16
		public ushort QueryPort; // m_usQueryPort uint16
		public ulong SteamIDLobby; // m_steamIDLobby class CSteamID
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static FriendGameInfo_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (FriendGameInfo_t) Marshal.PtrToStructure( p, typeof(FriendGameInfo_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong GameID; // m_gameID class CGameID
			public uint GameIP; // m_unGameIP uint32
			public ushort GamePort; // m_usGamePort uint16
			public ushort QueryPort; // m_usQueryPort uint16
			public ulong SteamIDLobby; // m_steamIDLobby class CSteamID
			
			//
			// Easily convert from PackSmall to FriendGameInfo_t
			//
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
	internal struct FriendSessionStateInfo_t
	{
		public uint IOnlineSessionInstances; // m_uiOnlineSessionInstances uint32
		public byte IPublishedToFriendsSessionInstance; // m_uiPublishedToFriendsSessionInstance uint8
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static FriendSessionStateInfo_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (FriendSessionStateInfo_t) Marshal.PtrToStructure( p, typeof(FriendSessionStateInfo_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint IOnlineSessionInstances; // m_uiOnlineSessionInstances uint32
			public byte IPublishedToFriendsSessionInstance; // m_uiPublishedToFriendsSessionInstance uint8
			
			//
			// Easily convert from PackSmall to FriendSessionStateInfo_t
			//
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
	internal struct PersonaStateChange_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamFriends + 4;
		public ulong SteamID; // m_ulSteamID uint64
		public int ChangeFlags; // m_nChangeFlags int
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static PersonaStateChange_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (PersonaStateChange_t) Marshal.PtrToStructure( p, typeof(PersonaStateChange_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamID; // m_ulSteamID uint64
			public int ChangeFlags; // m_nChangeFlags int
			
			//
			// Easily convert from PackSmall to PersonaStateChange_t
			//
			public static implicit operator PersonaStateChange_t (  PersonaStateChange_t.PackSmall d )
			{
				return new PersonaStateChange_t()
				{
					SteamID = d.SteamID,
					ChangeFlags = d.ChangeFlags,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<PersonaStateChange_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( PersonaStateChange_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct GameOverlayActivated_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamFriends + 31;
		public byte Active; // m_bActive uint8
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GameOverlayActivated_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GameOverlayActivated_t) Marshal.PtrToStructure( p, typeof(GameOverlayActivated_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public byte Active; // m_bActive uint8
			
			//
			// Easily convert from PackSmall to GameOverlayActivated_t
			//
			public static implicit operator GameOverlayActivated_t (  GameOverlayActivated_t.PackSmall d )
			{
				return new GameOverlayActivated_t()
				{
					Active = d.Active,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GameOverlayActivated_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GameOverlayActivated_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct GameServerChangeRequested_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamFriends + 32;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string Server; // m_rgchServer char [64]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string Password; // m_rgchPassword char [64]
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GameServerChangeRequested_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GameServerChangeRequested_t) Marshal.PtrToStructure( p, typeof(GameServerChangeRequested_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string Server; // m_rgchServer char [64]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string Password; // m_rgchPassword char [64]
			
			//
			// Easily convert from PackSmall to GameServerChangeRequested_t
			//
			public static implicit operator GameServerChangeRequested_t (  GameServerChangeRequested_t.PackSmall d )
			{
				return new GameServerChangeRequested_t()
				{
					Server = d.Server,
					Password = d.Password,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GameServerChangeRequested_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GameServerChangeRequested_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameLobbyJoinRequested_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamFriends + 33;
		public ulong SteamIDLobby; // m_steamIDLobby class CSteamID
		public ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GameLobbyJoinRequested_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GameLobbyJoinRequested_t) Marshal.PtrToStructure( p, typeof(GameLobbyJoinRequested_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDLobby; // m_steamIDLobby class CSteamID
			public ulong SteamIDFriend; // m_steamIDFriend class CSteamID
			
			//
			// Easily convert from PackSmall to GameLobbyJoinRequested_t
			//
			public static implicit operator GameLobbyJoinRequested_t (  GameLobbyJoinRequested_t.PackSmall d )
			{
				return new GameLobbyJoinRequested_t()
				{
					SteamIDLobby = d.SteamIDLobby,
					SteamIDFriend = d.SteamIDFriend,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GameLobbyJoinRequested_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GameLobbyJoinRequested_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct AvatarImageLoaded_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamFriends + 34;
		public ulong SteamID; // m_steamID class CSteamID
		public int Image; // m_iImage int
		public int Wide; // m_iWide int
		public int Tall; // m_iTall int
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static AvatarImageLoaded_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (AvatarImageLoaded_t) Marshal.PtrToStructure( p, typeof(AvatarImageLoaded_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamID; // m_steamID class CSteamID
			public int Image; // m_iImage int
			public int Wide; // m_iWide int
			public int Tall; // m_iTall int
			
			//
			// Easily convert from PackSmall to AvatarImageLoaded_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<AvatarImageLoaded_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( AvatarImageLoaded_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct ClanOfficerListResponse_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamFriends + 35;
		public ulong SteamIDClan; // m_steamIDClan class CSteamID
		public int COfficers; // m_cOfficers int
		public byte Success; // m_bSuccess uint8
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static ClanOfficerListResponse_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (ClanOfficerListResponse_t) Marshal.PtrToStructure( p, typeof(ClanOfficerListResponse_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDClan; // m_steamIDClan class CSteamID
			public int COfficers; // m_cOfficers int
			public byte Success; // m_bSuccess uint8
			
			//
			// Easily convert from PackSmall to ClanOfficerListResponse_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<ClanOfficerListResponse_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( ClanOfficerListResponse_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FriendRichPresenceUpdate_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamFriends + 36;
		public ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		public uint AppID; // m_nAppID AppId_t
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static FriendRichPresenceUpdate_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (FriendRichPresenceUpdate_t) Marshal.PtrToStructure( p, typeof(FriendRichPresenceUpdate_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDFriend; // m_steamIDFriend class CSteamID
			public uint AppID; // m_nAppID AppId_t
			
			//
			// Easily convert from PackSmall to FriendRichPresenceUpdate_t
			//
			public static implicit operator FriendRichPresenceUpdate_t (  FriendRichPresenceUpdate_t.PackSmall d )
			{
				return new FriendRichPresenceUpdate_t()
				{
					SteamIDFriend = d.SteamIDFriend,
					AppID = d.AppID,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<FriendRichPresenceUpdate_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( FriendRichPresenceUpdate_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameRichPresenceJoinRequested_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamFriends + 37;
		public ulong SteamIDFriend; // m_steamIDFriend class CSteamID
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string Connect; // m_rgchConnect char [256]
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GameRichPresenceJoinRequested_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GameRichPresenceJoinRequested_t) Marshal.PtrToStructure( p, typeof(GameRichPresenceJoinRequested_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDFriend; // m_steamIDFriend class CSteamID
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string Connect; // m_rgchConnect char [256]
			
			//
			// Easily convert from PackSmall to GameRichPresenceJoinRequested_t
			//
			public static implicit operator GameRichPresenceJoinRequested_t (  GameRichPresenceJoinRequested_t.PackSmall d )
			{
				return new GameRichPresenceJoinRequested_t()
				{
					SteamIDFriend = d.SteamIDFriend,
					Connect = d.Connect,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GameRichPresenceJoinRequested_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GameRichPresenceJoinRequested_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameConnectedClanChatMsg_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamFriends + 38;
		public ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		public int MessageID; // m_iMessageID int
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GameConnectedClanChatMsg_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GameConnectedClanChatMsg_t) Marshal.PtrToStructure( p, typeof(GameConnectedClanChatMsg_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			public int MessageID; // m_iMessageID int
			
			//
			// Easily convert from PackSmall to GameConnectedClanChatMsg_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GameConnectedClanChatMsg_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GameConnectedClanChatMsg_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameConnectedChatJoin_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamFriends + 39;
		public ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GameConnectedChatJoin_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GameConnectedChatJoin_t) Marshal.PtrToStructure( p, typeof(GameConnectedChatJoin_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			//
			// Easily convert from PackSmall to GameConnectedChatJoin_t
			//
			public static implicit operator GameConnectedChatJoin_t (  GameConnectedChatJoin_t.PackSmall d )
			{
				return new GameConnectedChatJoin_t()
				{
					SteamIDClanChat = d.SteamIDClanChat,
					SteamIDUser = d.SteamIDUser,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GameConnectedChatJoin_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GameConnectedChatJoin_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameConnectedChatLeave_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamFriends + 40;
		public ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		public bool Kicked; // m_bKicked _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool Dropped; // m_bDropped _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GameConnectedChatLeave_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GameConnectedChatLeave_t) Marshal.PtrToStructure( p, typeof(GameConnectedChatLeave_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			[MarshalAs(UnmanagedType.I1)]
			public bool Kicked; // m_bKicked _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool Dropped; // m_bDropped _Bool
			
			//
			// Easily convert from PackSmall to GameConnectedChatLeave_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GameConnectedChatLeave_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GameConnectedChatLeave_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct DownloadClanActivityCountsResult_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamFriends + 41;
		[MarshalAs(UnmanagedType.I1)]
		public bool Success; // m_bSuccess _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static DownloadClanActivityCountsResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (DownloadClanActivityCountsResult_t) Marshal.PtrToStructure( p, typeof(DownloadClanActivityCountsResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool Success; // m_bSuccess _Bool
			
			//
			// Easily convert from PackSmall to DownloadClanActivityCountsResult_t
			//
			public static implicit operator DownloadClanActivityCountsResult_t (  DownloadClanActivityCountsResult_t.PackSmall d )
			{
				return new DownloadClanActivityCountsResult_t()
				{
					Success = d.Success,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<DownloadClanActivityCountsResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( DownloadClanActivityCountsResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct JoinClanChatRoomCompletionResult_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamFriends + 42;
		public ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
		public ChatRoomEnterResponse ChatRoomEnterResponse; // m_eChatRoomEnterResponse enum EChatRoomEnterResponse
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static JoinClanChatRoomCompletionResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (JoinClanChatRoomCompletionResult_t) Marshal.PtrToStructure( p, typeof(JoinClanChatRoomCompletionResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDClanChat; // m_steamIDClanChat class CSteamID
			public ChatRoomEnterResponse ChatRoomEnterResponse; // m_eChatRoomEnterResponse enum EChatRoomEnterResponse
			
			//
			// Easily convert from PackSmall to JoinClanChatRoomCompletionResult_t
			//
			public static implicit operator JoinClanChatRoomCompletionResult_t (  JoinClanChatRoomCompletionResult_t.PackSmall d )
			{
				return new JoinClanChatRoomCompletionResult_t()
				{
					SteamIDClanChat = d.SteamIDClanChat,
					ChatRoomEnterResponse = d.ChatRoomEnterResponse,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<JoinClanChatRoomCompletionResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( JoinClanChatRoomCompletionResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GameConnectedFriendChatMsg_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamFriends + 43;
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		public int MessageID; // m_iMessageID int
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GameConnectedFriendChatMsg_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GameConnectedFriendChatMsg_t) Marshal.PtrToStructure( p, typeof(GameConnectedFriendChatMsg_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			public int MessageID; // m_iMessageID int
			
			//
			// Easily convert from PackSmall to GameConnectedFriendChatMsg_t
			//
			public static implicit operator GameConnectedFriendChatMsg_t (  GameConnectedFriendChatMsg_t.PackSmall d )
			{
				return new GameConnectedFriendChatMsg_t()
				{
					SteamIDUser = d.SteamIDUser,
					MessageID = d.MessageID,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GameConnectedFriendChatMsg_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GameConnectedFriendChatMsg_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FriendsGetFollowerCount_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamFriends + 44;
		public Result Result; // m_eResult enum EResult
		public ulong SteamID; // m_steamID class CSteamID
		public int Count; // m_nCount int
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static FriendsGetFollowerCount_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (FriendsGetFollowerCount_t) Marshal.PtrToStructure( p, typeof(FriendsGetFollowerCount_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong SteamID; // m_steamID class CSteamID
			public int Count; // m_nCount int
			
			//
			// Easily convert from PackSmall to FriendsGetFollowerCount_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<FriendsGetFollowerCount_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( FriendsGetFollowerCount_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FriendsIsFollowing_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamFriends + 45;
		public Result Result; // m_eResult enum EResult
		public ulong SteamID; // m_steamID class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		public bool IsFollowing; // m_bIsFollowing _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static FriendsIsFollowing_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (FriendsIsFollowing_t) Marshal.PtrToStructure( p, typeof(FriendsIsFollowing_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong SteamID; // m_steamID class CSteamID
			[MarshalAs(UnmanagedType.I1)]
			public bool IsFollowing; // m_bIsFollowing _Bool
			
			//
			// Easily convert from PackSmall to FriendsIsFollowing_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<FriendsIsFollowing_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( FriendsIsFollowing_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct FriendsEnumerateFollowingList_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamFriends + 46;
		public Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public ulong[] GSteamID; // m_rgSteamID class CSteamID [50]
		public int ResultsReturned; // m_nResultsReturned int32
		public int TotalResultCount; // m_nTotalResultCount int32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static FriendsEnumerateFollowingList_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (FriendsEnumerateFollowingList_t) Marshal.PtrToStructure( p, typeof(FriendsEnumerateFollowingList_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public ulong[] GSteamID; // m_rgSteamID class CSteamID [50]
			public int ResultsReturned; // m_nResultsReturned int32
			public int TotalResultCount; // m_nTotalResultCount int32
			
			//
			// Easily convert from PackSmall to FriendsEnumerateFollowingList_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<FriendsEnumerateFollowingList_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( FriendsEnumerateFollowingList_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct SetPersonaNameResponse_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamFriends + 47;
		[MarshalAs(UnmanagedType.I1)]
		public bool Success; // m_bSuccess _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool LocalSuccess; // m_bLocalSuccess _Bool
		public Result Esult; // m_result enum EResult
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static SetPersonaNameResponse_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (SetPersonaNameResponse_t) Marshal.PtrToStructure( p, typeof(SetPersonaNameResponse_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool Success; // m_bSuccess _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool LocalSuccess; // m_bLocalSuccess _Bool
			public Result Esult; // m_result enum EResult
			
			//
			// Easily convert from PackSmall to SetPersonaNameResponse_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<SetPersonaNameResponse_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( SetPersonaNameResponse_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct LowBatteryPower_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUtils + 2;
		public byte MinutesBatteryLeft; // m_nMinutesBatteryLeft uint8
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static LowBatteryPower_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (LowBatteryPower_t) Marshal.PtrToStructure( p, typeof(LowBatteryPower_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public byte MinutesBatteryLeft; // m_nMinutesBatteryLeft uint8
			
			//
			// Easily convert from PackSmall to LowBatteryPower_t
			//
			public static implicit operator LowBatteryPower_t (  LowBatteryPower_t.PackSmall d )
			{
				return new LowBatteryPower_t()
				{
					MinutesBatteryLeft = d.MinutesBatteryLeft,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<LowBatteryPower_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( LowBatteryPower_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct SteamAPICallCompleted_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUtils + 3;
		public ulong AsyncCall; // m_hAsyncCall SteamAPICall_t
		public int Callback; // m_iCallback int
		public uint ParamCount; // m_cubParam uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static SteamAPICallCompleted_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (SteamAPICallCompleted_t) Marshal.PtrToStructure( p, typeof(SteamAPICallCompleted_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong AsyncCall; // m_hAsyncCall SteamAPICall_t
			public int Callback; // m_iCallback int
			public uint ParamCount; // m_cubParam uint32
			
			//
			// Easily convert from PackSmall to SteamAPICallCompleted_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<SteamAPICallCompleted_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( SteamAPICallCompleted_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct CheckFileSignature_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUtils + 5;
		public CheckFileSignature CheckFileSignature; // m_eCheckFileSignature enum ECheckFileSignature
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static CheckFileSignature_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (CheckFileSignature_t) Marshal.PtrToStructure( p, typeof(CheckFileSignature_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public CheckFileSignature CheckFileSignature; // m_eCheckFileSignature enum ECheckFileSignature
			
			//
			// Easily convert from PackSmall to CheckFileSignature_t
			//
			public static implicit operator CheckFileSignature_t (  CheckFileSignature_t.PackSmall d )
			{
				return new CheckFileSignature_t()
				{
					CheckFileSignature = d.CheckFileSignature,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<CheckFileSignature_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( CheckFileSignature_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct GamepadTextInputDismissed_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUtils + 14;
		[MarshalAs(UnmanagedType.I1)]
		public bool Submitted; // m_bSubmitted _Bool
		public uint SubmittedText; // m_unSubmittedText uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GamepadTextInputDismissed_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GamepadTextInputDismissed_t) Marshal.PtrToStructure( p, typeof(GamepadTextInputDismissed_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool Submitted; // m_bSubmitted _Bool
			public uint SubmittedText; // m_unSubmittedText uint32
			
			//
			// Easily convert from PackSmall to GamepadTextInputDismissed_t
			//
			public static implicit operator GamepadTextInputDismissed_t (  GamepadTextInputDismissed_t.PackSmall d )
			{
				return new GamepadTextInputDismissed_t()
				{
					Submitted = d.Submitted,
					SubmittedText = d.SubmittedText,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GamepadTextInputDismissed_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GamepadTextInputDismissed_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct MatchMakingKeyValuePair_t
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string Key; // m_szKey char [256]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string Value; // m_szValue char [256]
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static MatchMakingKeyValuePair_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (MatchMakingKeyValuePair_t) Marshal.PtrToStructure( p, typeof(MatchMakingKeyValuePair_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string Key; // m_szKey char [256]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string Value; // m_szValue char [256]
			
			//
			// Easily convert from PackSmall to MatchMakingKeyValuePair_t
			//
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
	internal struct servernetadr_t
	{
		public ushort ConnectionPort; // m_usConnectionPort uint16
		public ushort QueryPort; // m_usQueryPort uint16
		public uint IP; // m_unIP uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static servernetadr_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (servernetadr_t) Marshal.PtrToStructure( p, typeof(servernetadr_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ushort ConnectionPort; // m_usConnectionPort uint16
			public ushort QueryPort; // m_usQueryPort uint16
			public uint IP; // m_unIP uint32
			
			//
			// Easily convert from PackSmall to servernetadr_t
			//
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
	internal struct gameserveritem_t
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
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static gameserveritem_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (gameserveritem_t) Marshal.PtrToStructure( p, typeof(gameserveritem_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
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
			
			//
			// Easily convert from PackSmall to gameserveritem_t
			//
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
	internal struct FavoritesListChanged_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMatchmaking + 2;
		public uint IP; // m_nIP uint32
		public uint QueryPort; // m_nQueryPort uint32
		public uint ConnPort; // m_nConnPort uint32
		public uint AppID; // m_nAppID uint32
		public uint Flags; // m_nFlags uint32
		[MarshalAs(UnmanagedType.I1)]
		public bool Add; // m_bAdd _Bool
		public uint AccountId; // m_unAccountId AccountID_t
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static FavoritesListChanged_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (FavoritesListChanged_t) Marshal.PtrToStructure( p, typeof(FavoritesListChanged_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint IP; // m_nIP uint32
			public uint QueryPort; // m_nQueryPort uint32
			public uint ConnPort; // m_nConnPort uint32
			public uint AppID; // m_nAppID uint32
			public uint Flags; // m_nFlags uint32
			[MarshalAs(UnmanagedType.I1)]
			public bool Add; // m_bAdd _Bool
			public uint AccountId; // m_unAccountId AccountID_t
			
			//
			// Easily convert from PackSmall to FavoritesListChanged_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<FavoritesListChanged_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( FavoritesListChanged_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct LobbyInvite_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMatchmaking + 3;
		public ulong SteamIDUser; // m_ulSteamIDUser uint64
		public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		public ulong GameID; // m_ulGameID uint64
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static LobbyInvite_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (LobbyInvite_t) Marshal.PtrToStructure( p, typeof(LobbyInvite_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDUser; // m_ulSteamIDUser uint64
			public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			public ulong GameID; // m_ulGameID uint64
			
			//
			// Easily convert from PackSmall to LobbyInvite_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<LobbyInvite_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( LobbyInvite_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct LobbyEnter_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMatchmaking + 4;
		public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		public uint GfChatPermissions; // m_rgfChatPermissions uint32
		[MarshalAs(UnmanagedType.I1)]
		public bool Locked; // m_bLocked _Bool
		public uint EChatRoomEnterResponse; // m_EChatRoomEnterResponse uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static LobbyEnter_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (LobbyEnter_t) Marshal.PtrToStructure( p, typeof(LobbyEnter_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			public uint GfChatPermissions; // m_rgfChatPermissions uint32
			[MarshalAs(UnmanagedType.I1)]
			public bool Locked; // m_bLocked _Bool
			public uint EChatRoomEnterResponse; // m_EChatRoomEnterResponse uint32
			
			//
			// Easily convert from PackSmall to LobbyEnter_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<LobbyEnter_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( LobbyEnter_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct LobbyDataUpdate_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMatchmaking + 5;
		public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		public ulong SteamIDMember; // m_ulSteamIDMember uint64
		public byte Success; // m_bSuccess uint8
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static LobbyDataUpdate_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (LobbyDataUpdate_t) Marshal.PtrToStructure( p, typeof(LobbyDataUpdate_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			public ulong SteamIDMember; // m_ulSteamIDMember uint64
			public byte Success; // m_bSuccess uint8
			
			//
			// Easily convert from PackSmall to LobbyDataUpdate_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<LobbyDataUpdate_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( LobbyDataUpdate_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct LobbyChatUpdate_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMatchmaking + 6;
		public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		public ulong SteamIDUserChanged; // m_ulSteamIDUserChanged uint64
		public ulong SteamIDMakingChange; // m_ulSteamIDMakingChange uint64
		public uint GfChatMemberStateChange; // m_rgfChatMemberStateChange uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static LobbyChatUpdate_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (LobbyChatUpdate_t) Marshal.PtrToStructure( p, typeof(LobbyChatUpdate_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			public ulong SteamIDUserChanged; // m_ulSteamIDUserChanged uint64
			public ulong SteamIDMakingChange; // m_ulSteamIDMakingChange uint64
			public uint GfChatMemberStateChange; // m_rgfChatMemberStateChange uint32
			
			//
			// Easily convert from PackSmall to LobbyChatUpdate_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<LobbyChatUpdate_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( LobbyChatUpdate_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct LobbyChatMsg_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMatchmaking + 7;
		public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		public ulong SteamIDUser; // m_ulSteamIDUser uint64
		public byte ChatEntryType; // m_eChatEntryType uint8
		public uint ChatID; // m_iChatID uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static LobbyChatMsg_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (LobbyChatMsg_t) Marshal.PtrToStructure( p, typeof(LobbyChatMsg_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			public ulong SteamIDUser; // m_ulSteamIDUser uint64
			public byte ChatEntryType; // m_eChatEntryType uint8
			public uint ChatID; // m_iChatID uint32
			
			//
			// Easily convert from PackSmall to LobbyChatMsg_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<LobbyChatMsg_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( LobbyChatMsg_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct LobbyGameCreated_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMatchmaking + 9;
		public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		public ulong SteamIDGameServer; // m_ulSteamIDGameServer uint64
		public uint IP; // m_unIP uint32
		public ushort Port; // m_usPort uint16
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static LobbyGameCreated_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (LobbyGameCreated_t) Marshal.PtrToStructure( p, typeof(LobbyGameCreated_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			public ulong SteamIDGameServer; // m_ulSteamIDGameServer uint64
			public uint IP; // m_unIP uint32
			public ushort Port; // m_usPort uint16
			
			//
			// Easily convert from PackSmall to LobbyGameCreated_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<LobbyGameCreated_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( LobbyGameCreated_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct LobbyMatchList_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMatchmaking + 10;
		public uint LobbiesMatching; // m_nLobbiesMatching uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static LobbyMatchList_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (LobbyMatchList_t) Marshal.PtrToStructure( p, typeof(LobbyMatchList_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint LobbiesMatching; // m_nLobbiesMatching uint32
			
			//
			// Easily convert from PackSmall to LobbyMatchList_t
			//
			public static implicit operator LobbyMatchList_t (  LobbyMatchList_t.PackSmall d )
			{
				return new LobbyMatchList_t()
				{
					LobbiesMatching = d.LobbiesMatching,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<LobbyMatchList_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( LobbyMatchList_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct LobbyKicked_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMatchmaking + 12;
		public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		public ulong SteamIDAdmin; // m_ulSteamIDAdmin uint64
		public byte KickedDueToDisconnect; // m_bKickedDueToDisconnect uint8
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static LobbyKicked_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (LobbyKicked_t) Marshal.PtrToStructure( p, typeof(LobbyKicked_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			public ulong SteamIDAdmin; // m_ulSteamIDAdmin uint64
			public byte KickedDueToDisconnect; // m_bKickedDueToDisconnect uint8
			
			//
			// Easily convert from PackSmall to LobbyKicked_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<LobbyKicked_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( LobbyKicked_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct LobbyCreated_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMatchmaking + 13;
		public Result Result; // m_eResult enum EResult
		public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static LobbyCreated_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (LobbyCreated_t) Marshal.PtrToStructure( p, typeof(LobbyCreated_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong SteamIDLobby; // m_ulSteamIDLobby uint64
			
			//
			// Easily convert from PackSmall to LobbyCreated_t
			//
			public static implicit operator LobbyCreated_t (  LobbyCreated_t.PackSmall d )
			{
				return new LobbyCreated_t()
				{
					Result = d.Result,
					SteamIDLobby = d.SteamIDLobby,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<LobbyCreated_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( LobbyCreated_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct PSNGameBootInviteResult_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMatchmaking + 15;
		[MarshalAs(UnmanagedType.I1)]
		public bool GameBootInviteExists; // m_bGameBootInviteExists _Bool
		public ulong SteamIDLobby; // m_steamIDLobby class CSteamID
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static PSNGameBootInviteResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (PSNGameBootInviteResult_t) Marshal.PtrToStructure( p, typeof(PSNGameBootInviteResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool GameBootInviteExists; // m_bGameBootInviteExists _Bool
			public ulong SteamIDLobby; // m_steamIDLobby class CSteamID
			
			//
			// Easily convert from PackSmall to PSNGameBootInviteResult_t
			//
			public static implicit operator PSNGameBootInviteResult_t (  PSNGameBootInviteResult_t.PackSmall d )
			{
				return new PSNGameBootInviteResult_t()
				{
					GameBootInviteExists = d.GameBootInviteExists,
					SteamIDLobby = d.SteamIDLobby,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<PSNGameBootInviteResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( PSNGameBootInviteResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct FavoritesListAccountsUpdated_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMatchmaking + 16;
		public Result Result; // m_eResult enum EResult
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static FavoritesListAccountsUpdated_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (FavoritesListAccountsUpdated_t) Marshal.PtrToStructure( p, typeof(FavoritesListAccountsUpdated_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			
			//
			// Easily convert from PackSmall to FavoritesListAccountsUpdated_t
			//
			public static implicit operator FavoritesListAccountsUpdated_t (  FavoritesListAccountsUpdated_t.PackSmall d )
			{
				return new FavoritesListAccountsUpdated_t()
				{
					Result = d.Result,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<FavoritesListAccountsUpdated_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( FavoritesListAccountsUpdated_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct SteamParamStringArray_t
	{
		public IntPtr Strings; // m_ppStrings const char **
		public int NumStrings; // m_nNumStrings int32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static SteamParamStringArray_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (SteamParamStringArray_t) Marshal.PtrToStructure( p, typeof(SteamParamStringArray_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public IntPtr Strings; // m_ppStrings const char **
			public int NumStrings; // m_nNumStrings int32
			
			//
			// Easily convert from PackSmall to SteamParamStringArray_t
			//
			public static implicit operator SteamParamStringArray_t (  SteamParamStringArray_t.PackSmall d )
			{
				return new SteamParamStringArray_t()
				{
					Strings = d.Strings,
					NumStrings = d.NumStrings,
				};
			}
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageAppSyncedClient_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 1;
		public uint AppID; // m_nAppID AppId_t
		public Result Result; // m_eResult enum EResult
		public int NumDownloads; // m_unNumDownloads int
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageAppSyncedClient_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageAppSyncedClient_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncedClient_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint AppID; // m_nAppID AppId_t
			public Result Result; // m_eResult enum EResult
			public int NumDownloads; // m_unNumDownloads int
			
			//
			// Easily convert from PackSmall to RemoteStorageAppSyncedClient_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<RemoteStorageAppSyncedClient_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageAppSyncedClient_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageAppSyncedServer_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 2;
		public uint AppID; // m_nAppID AppId_t
		public Result Result; // m_eResult enum EResult
		public int NumUploads; // m_unNumUploads int
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageAppSyncedServer_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageAppSyncedServer_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncedServer_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint AppID; // m_nAppID AppId_t
			public Result Result; // m_eResult enum EResult
			public int NumUploads; // m_unNumUploads int
			
			//
			// Easily convert from PackSmall to RemoteStorageAppSyncedServer_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<RemoteStorageAppSyncedServer_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageAppSyncedServer_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageAppSyncProgress_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 3;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string CurrentFile; // m_rgchCurrentFile char [260]
		public uint AppID; // m_nAppID AppId_t
		public uint BytesTransferredThisChunk; // m_uBytesTransferredThisChunk uint32
		public double DAppPercentComplete; // m_dAppPercentComplete double
		[MarshalAs(UnmanagedType.I1)]
		public bool Uploading; // m_bUploading _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageAppSyncProgress_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageAppSyncProgress_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncProgress_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string CurrentFile; // m_rgchCurrentFile char [260]
			public uint AppID; // m_nAppID AppId_t
			public uint BytesTransferredThisChunk; // m_uBytesTransferredThisChunk uint32
			public double DAppPercentComplete; // m_dAppPercentComplete double
			[MarshalAs(UnmanagedType.I1)]
			public bool Uploading; // m_bUploading _Bool
			
			//
			// Easily convert from PackSmall to RemoteStorageAppSyncProgress_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<RemoteStorageAppSyncProgress_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageAppSyncProgress_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageAppSyncStatusCheck_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 5;
		public uint AppID; // m_nAppID AppId_t
		public Result Result; // m_eResult enum EResult
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageAppSyncStatusCheck_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageAppSyncStatusCheck_t) Marshal.PtrToStructure( p, typeof(RemoteStorageAppSyncStatusCheck_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint AppID; // m_nAppID AppId_t
			public Result Result; // m_eResult enum EResult
			
			//
			// Easily convert from PackSmall to RemoteStorageAppSyncStatusCheck_t
			//
			public static implicit operator RemoteStorageAppSyncStatusCheck_t (  RemoteStorageAppSyncStatusCheck_t.PackSmall d )
			{
				return new RemoteStorageAppSyncStatusCheck_t()
				{
					AppID = d.AppID,
					Result = d.Result,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<RemoteStorageAppSyncStatusCheck_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageAppSyncStatusCheck_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageFileShareResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 7;
		public Result Result; // m_eResult enum EResult
		public ulong File; // m_hFile UGCHandle_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string Filename; // m_rgchFilename char [260]
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageFileShareResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageFileShareResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageFileShareResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong File; // m_hFile UGCHandle_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string Filename; // m_rgchFilename char [260]
			
			//
			// Easily convert from PackSmall to RemoteStorageFileShareResult_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageFileShareResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageFileShareResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStoragePublishFileResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 9;
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		public bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStoragePublishFileResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStoragePublishFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishFileResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			[MarshalAs(UnmanagedType.I1)]
			public bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			
			//
			// Easily convert from PackSmall to RemoteStoragePublishFileResult_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<RemoteStoragePublishFileResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStoragePublishFileResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageDeletePublishedFileResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 11;
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageDeletePublishedFileResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageDeletePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageDeletePublishedFileResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			//
			// Easily convert from PackSmall to RemoteStorageDeletePublishedFileResult_t
			//
			public static implicit operator RemoteStorageDeletePublishedFileResult_t (  RemoteStorageDeletePublishedFileResult_t.PackSmall d )
			{
				return new RemoteStorageDeletePublishedFileResult_t()
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageDeletePublishedFileResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageDeletePublishedFileResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageEnumerateUserPublishedFilesResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 12;
		public Result Result; // m_eResult enum EResult
		public int ResultsReturned; // m_nResultsReturned int32
		public int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageEnumerateUserPublishedFilesResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageEnumerateUserPublishedFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserPublishedFilesResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public int ResultsReturned; // m_nResultsReturned int32
			public int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			
			//
			// Easily convert from PackSmall to RemoteStorageEnumerateUserPublishedFilesResult_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageEnumerateUserPublishedFilesResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageEnumerateUserPublishedFilesResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageSubscribePublishedFileResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 13;
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageSubscribePublishedFileResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageSubscribePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageSubscribePublishedFileResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			//
			// Easily convert from PackSmall to RemoteStorageSubscribePublishedFileResult_t
			//
			public static implicit operator RemoteStorageSubscribePublishedFileResult_t (  RemoteStorageSubscribePublishedFileResult_t.PackSmall d )
			{
				return new RemoteStorageSubscribePublishedFileResult_t()
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageSubscribePublishedFileResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageSubscribePublishedFileResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageEnumerateUserSubscribedFilesResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 14;
		public Result Result; // m_eResult enum EResult
		public int ResultsReturned; // m_nResultsReturned int32
		public int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public uint[] GRTimeSubscribed; // m_rgRTimeSubscribed uint32 [50]
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageEnumerateUserSubscribedFilesResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageEnumerateUserSubscribedFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public int ResultsReturned; // m_nResultsReturned int32
			public int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public uint[] GRTimeSubscribed; // m_rgRTimeSubscribed uint32 [50]
			
			//
			// Easily convert from PackSmall to RemoteStorageEnumerateUserSubscribedFilesResult_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageEnumerateUserSubscribedFilesResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageEnumerateUserSubscribedFilesResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageUnsubscribePublishedFileResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 15;
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageUnsubscribePublishedFileResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageUnsubscribePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUnsubscribePublishedFileResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			//
			// Easily convert from PackSmall to RemoteStorageUnsubscribePublishedFileResult_t
			//
			public static implicit operator RemoteStorageUnsubscribePublishedFileResult_t (  RemoteStorageUnsubscribePublishedFileResult_t.PackSmall d )
			{
				return new RemoteStorageUnsubscribePublishedFileResult_t()
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageUnsubscribePublishedFileResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageUnsubscribePublishedFileResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageUpdatePublishedFileResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 16;
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		public bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageUpdatePublishedFileResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageUpdatePublishedFileResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUpdatePublishedFileResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			[MarshalAs(UnmanagedType.I1)]
			public bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			
			//
			// Easily convert from PackSmall to RemoteStorageUpdatePublishedFileResult_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageUpdatePublishedFileResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageUpdatePublishedFileResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageDownloadUGCResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 17;
		public Result Result; // m_eResult enum EResult
		public ulong File; // m_hFile UGCHandle_t
		public uint AppID; // m_nAppID AppId_t
		public int SizeInBytes; // m_nSizeInBytes int32
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string PchFileName; // m_pchFileName char [260]
		public ulong SteamIDOwner; // m_ulSteamIDOwner uint64
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageDownloadUGCResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageDownloadUGCResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageDownloadUGCResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong File; // m_hFile UGCHandle_t
			public uint AppID; // m_nAppID AppId_t
			public int SizeInBytes; // m_nSizeInBytes int32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string PchFileName; // m_pchFileName char [260]
			public ulong SteamIDOwner; // m_ulSteamIDOwner uint64
			
			//
			// Easily convert from PackSmall to RemoteStorageDownloadUGCResult_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageDownloadUGCResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageDownloadUGCResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageGetPublishedFileDetailsResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 18;
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
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageGetPublishedFileDetailsResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageGetPublishedFileDetailsResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageGetPublishedFileDetailsResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
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
			
			//
			// Easily convert from PackSmall to RemoteStorageGetPublishedFileDetailsResult_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageGetPublishedFileDetailsResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageGetPublishedFileDetailsResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageEnumerateWorkshopFilesResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 19;
		public Result Result; // m_eResult enum EResult
		public int ResultsReturned; // m_nResultsReturned int32
		public int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.R4)]
		public float[] GScore; // m_rgScore float [50]
		public uint AppId; // m_nAppId AppId_t
		public uint StartIndex; // m_unStartIndex uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageEnumerateWorkshopFilesResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageEnumerateWorkshopFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateWorkshopFilesResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
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
			
			//
			// Easily convert from PackSmall to RemoteStorageEnumerateWorkshopFilesResult_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageEnumerateWorkshopFilesResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageEnumerateWorkshopFilesResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageGetPublishedItemVoteDetailsResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 20;
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_unPublishedFileId PublishedFileId_t
		public int VotesFor; // m_nVotesFor int32
		public int VotesAgainst; // m_nVotesAgainst int32
		public int Reports; // m_nReports int32
		public float FScore; // m_fScore float
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageGetPublishedItemVoteDetailsResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageGetPublishedItemVoteDetailsResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageGetPublishedItemVoteDetailsResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_unPublishedFileId PublishedFileId_t
			public int VotesFor; // m_nVotesFor int32
			public int VotesAgainst; // m_nVotesAgainst int32
			public int Reports; // m_nReports int32
			public float FScore; // m_fScore float
			
			//
			// Easily convert from PackSmall to RemoteStorageGetPublishedItemVoteDetailsResult_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageGetPublishedItemVoteDetailsResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageGetPublishedItemVoteDetailsResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStoragePublishedFileSubscribed_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 21;
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public uint AppID; // m_nAppID AppId_t
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStoragePublishedFileSubscribed_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStoragePublishedFileSubscribed_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileSubscribed_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public uint AppID; // m_nAppID AppId_t
			
			//
			// Easily convert from PackSmall to RemoteStoragePublishedFileSubscribed_t
			//
			public static implicit operator RemoteStoragePublishedFileSubscribed_t (  RemoteStoragePublishedFileSubscribed_t.PackSmall d )
			{
				return new RemoteStoragePublishedFileSubscribed_t()
				{
					PublishedFileId = d.PublishedFileId,
					AppID = d.AppID,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<RemoteStoragePublishedFileSubscribed_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStoragePublishedFileSubscribed_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStoragePublishedFileUnsubscribed_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 22;
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public uint AppID; // m_nAppID AppId_t
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStoragePublishedFileUnsubscribed_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStoragePublishedFileUnsubscribed_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileUnsubscribed_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public uint AppID; // m_nAppID AppId_t
			
			//
			// Easily convert from PackSmall to RemoteStoragePublishedFileUnsubscribed_t
			//
			public static implicit operator RemoteStoragePublishedFileUnsubscribed_t (  RemoteStoragePublishedFileUnsubscribed_t.PackSmall d )
			{
				return new RemoteStoragePublishedFileUnsubscribed_t()
				{
					PublishedFileId = d.PublishedFileId,
					AppID = d.AppID,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<RemoteStoragePublishedFileUnsubscribed_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStoragePublishedFileUnsubscribed_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStoragePublishedFileDeleted_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 23;
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public uint AppID; // m_nAppID AppId_t
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStoragePublishedFileDeleted_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStoragePublishedFileDeleted_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileDeleted_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public uint AppID; // m_nAppID AppId_t
			
			//
			// Easily convert from PackSmall to RemoteStoragePublishedFileDeleted_t
			//
			public static implicit operator RemoteStoragePublishedFileDeleted_t (  RemoteStoragePublishedFileDeleted_t.PackSmall d )
			{
				return new RemoteStoragePublishedFileDeleted_t()
				{
					PublishedFileId = d.PublishedFileId,
					AppID = d.AppID,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<RemoteStoragePublishedFileDeleted_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStoragePublishedFileDeleted_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageUpdateUserPublishedItemVoteResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 24;
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageUpdateUserPublishedItemVoteResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageUpdateUserPublishedItemVoteResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUpdateUserPublishedItemVoteResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			//
			// Easily convert from PackSmall to RemoteStorageUpdateUserPublishedItemVoteResult_t
			//
			public static implicit operator RemoteStorageUpdateUserPublishedItemVoteResult_t (  RemoteStorageUpdateUserPublishedItemVoteResult_t.PackSmall d )
			{
				return new RemoteStorageUpdateUserPublishedItemVoteResult_t()
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageUpdateUserPublishedItemVoteResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageUpdateUserPublishedItemVoteResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageUserVoteDetails_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 25;
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public WorkshopVote Vote; // m_eVote enum EWorkshopVote
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageUserVoteDetails_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageUserVoteDetails_t) Marshal.PtrToStructure( p, typeof(RemoteStorageUserVoteDetails_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public WorkshopVote Vote; // m_eVote enum EWorkshopVote
			
			//
			// Easily convert from PackSmall to RemoteStorageUserVoteDetails_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<RemoteStorageUserVoteDetails_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageUserVoteDetails_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 26;
		public Result Result; // m_eResult enum EResult
		public int ResultsReturned; // m_nResultsReturned int32
		public int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageEnumerateUserSharedWorkshopFilesResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public int ResultsReturned; // m_nResultsReturned int32
			public int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			
			//
			// Easily convert from PackSmall to RemoteStorageEnumerateUserSharedWorkshopFilesResult_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<RemoteStorageEnumerateUserSharedWorkshopFilesResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageEnumerateUserSharedWorkshopFilesResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageSetUserPublishedFileActionResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 27;
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageSetUserPublishedFileActionResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageSetUserPublishedFileActionResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageSetUserPublishedFileActionResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
			
			//
			// Easily convert from PackSmall to RemoteStorageSetUserPublishedFileActionResult_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageSetUserPublishedFileActionResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageSetUserPublishedFileActionResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageEnumeratePublishedFilesByUserActionResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 28;
		public Result Result; // m_eResult enum EResult
		public WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
		public int ResultsReturned; // m_nResultsReturned int32
		public int TotalResultCount; // m_nTotalResultCount int32
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		public uint[] GRTimeUpdated; // m_rgRTimeUpdated uint32 [50]
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageEnumeratePublishedFilesByUserActionResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageEnumeratePublishedFilesByUserActionResult_t) Marshal.PtrToStructure( p, typeof(RemoteStorageEnumeratePublishedFilesByUserActionResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public WorkshopFileAction Action; // m_eAction enum EWorkshopFileAction
			public int ResultsReturned; // m_nResultsReturned int32
			public int TotalResultCount; // m_nTotalResultCount int32
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public ulong[] GPublishedFileId; // m_rgPublishedFileId PublishedFileId_t [50]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			public uint[] GRTimeUpdated; // m_rgRTimeUpdated uint32 [50]
			
			//
			// Easily convert from PackSmall to RemoteStorageEnumeratePublishedFilesByUserActionResult_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageEnumeratePublishedFilesByUserActionResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageEnumeratePublishedFilesByUserActionResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStoragePublishFileProgress_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 29;
		public double DPercentFile; // m_dPercentFile double
		[MarshalAs(UnmanagedType.I1)]
		public bool Preview; // m_bPreview _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStoragePublishFileProgress_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStoragePublishFileProgress_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishFileProgress_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public double DPercentFile; // m_dPercentFile double
			[MarshalAs(UnmanagedType.I1)]
			public bool Preview; // m_bPreview _Bool
			
			//
			// Easily convert from PackSmall to RemoteStoragePublishFileProgress_t
			//
			public static implicit operator RemoteStoragePublishFileProgress_t (  RemoteStoragePublishFileProgress_t.PackSmall d )
			{
				return new RemoteStoragePublishFileProgress_t()
				{
					DPercentFile = d.DPercentFile,
					Preview = d.Preview,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStoragePublishFileProgress_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStoragePublishFileProgress_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStoragePublishedFileUpdated_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 30;
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public uint AppID; // m_nAppID AppId_t
		public ulong Unused; // m_ulUnused uint64
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStoragePublishedFileUpdated_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStoragePublishedFileUpdated_t) Marshal.PtrToStructure( p, typeof(RemoteStoragePublishedFileUpdated_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public uint AppID; // m_nAppID AppId_t
			public ulong Unused; // m_ulUnused uint64
			
			//
			// Easily convert from PackSmall to RemoteStoragePublishedFileUpdated_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<RemoteStoragePublishedFileUpdated_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStoragePublishedFileUpdated_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageFileWriteAsyncComplete_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 31;
		public Result Result; // m_eResult enum EResult
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageFileWriteAsyncComplete_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageFileWriteAsyncComplete_t) Marshal.PtrToStructure( p, typeof(RemoteStorageFileWriteAsyncComplete_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			
			//
			// Easily convert from PackSmall to RemoteStorageFileWriteAsyncComplete_t
			//
			public static implicit operator RemoteStorageFileWriteAsyncComplete_t (  RemoteStorageFileWriteAsyncComplete_t.PackSmall d )
			{
				return new RemoteStorageFileWriteAsyncComplete_t()
				{
					Result = d.Result,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageFileWriteAsyncComplete_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageFileWriteAsyncComplete_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RemoteStorageFileReadAsyncComplete_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientRemoteStorage + 32;
		public ulong FileReadAsync; // m_hFileReadAsync SteamAPICall_t
		public Result Result; // m_eResult enum EResult
		public uint Offset; // m_nOffset uint32
		public uint Read; // m_cubRead uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RemoteStorageFileReadAsyncComplete_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RemoteStorageFileReadAsyncComplete_t) Marshal.PtrToStructure( p, typeof(RemoteStorageFileReadAsyncComplete_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong FileReadAsync; // m_hFileReadAsync SteamAPICall_t
			public Result Result; // m_eResult enum EResult
			public uint Offset; // m_nOffset uint32
			public uint Read; // m_cubRead uint32
			
			//
			// Easily convert from PackSmall to RemoteStorageFileReadAsyncComplete_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageFileReadAsyncComplete_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RemoteStorageFileReadAsyncComplete_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct LeaderboardEntry_t
	{
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		public int GlobalRank; // m_nGlobalRank int32
		public int Score; // m_nScore int32
		public int CDetails; // m_cDetails int32
		public ulong UGC; // m_hUGC UGCHandle_t
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static LeaderboardEntry_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (LeaderboardEntry_t) Marshal.PtrToStructure( p, typeof(LeaderboardEntry_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			public int GlobalRank; // m_nGlobalRank int32
			public int Score; // m_nScore int32
			public int CDetails; // m_cDetails int32
			public ulong UGC; // m_hUGC UGCHandle_t
			
			//
			// Easily convert from PackSmall to LeaderboardEntry_t
			//
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
	internal struct UserStatsReceived_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUserStats + 1;
		public ulong GameID; // m_nGameID uint64
		public Result Result; // m_eResult enum EResult
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static UserStatsReceived_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (UserStatsReceived_t) Marshal.PtrToStructure( p, typeof(UserStatsReceived_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong GameID; // m_nGameID uint64
			public Result Result; // m_eResult enum EResult
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			//
			// Easily convert from PackSmall to UserStatsReceived_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<UserStatsReceived_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( UserStatsReceived_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct UserStatsStored_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUserStats + 2;
		public ulong GameID; // m_nGameID uint64
		public Result Result; // m_eResult enum EResult
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static UserStatsStored_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (UserStatsStored_t) Marshal.PtrToStructure( p, typeof(UserStatsStored_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong GameID; // m_nGameID uint64
			public Result Result; // m_eResult enum EResult
			
			//
			// Easily convert from PackSmall to UserStatsStored_t
			//
			public static implicit operator UserStatsStored_t (  UserStatsStored_t.PackSmall d )
			{
				return new UserStatsStored_t()
				{
					GameID = d.GameID,
					Result = d.Result,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<UserStatsStored_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( UserStatsStored_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct UserAchievementStored_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUserStats + 3;
		public ulong GameID; // m_nGameID uint64
		[MarshalAs(UnmanagedType.I1)]
		public bool GroupAchievement; // m_bGroupAchievement _Bool
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string AchievementName; // m_rgchAchievementName char [128]
		public uint CurProgress; // m_nCurProgress uint32
		public uint MaxProgress; // m_nMaxProgress uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static UserAchievementStored_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (UserAchievementStored_t) Marshal.PtrToStructure( p, typeof(UserAchievementStored_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong GameID; // m_nGameID uint64
			[MarshalAs(UnmanagedType.I1)]
			public bool GroupAchievement; // m_bGroupAchievement _Bool
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string AchievementName; // m_rgchAchievementName char [128]
			public uint CurProgress; // m_nCurProgress uint32
			public uint MaxProgress; // m_nMaxProgress uint32
			
			//
			// Easily convert from PackSmall to UserAchievementStored_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<UserAchievementStored_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( UserAchievementStored_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct LeaderboardFindResult_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUserStats + 4;
		public ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		public byte LeaderboardFound; // m_bLeaderboardFound uint8
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static LeaderboardFindResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (LeaderboardFindResult_t) Marshal.PtrToStructure( p, typeof(LeaderboardFindResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
			public byte LeaderboardFound; // m_bLeaderboardFound uint8
			
			//
			// Easily convert from PackSmall to LeaderboardFindResult_t
			//
			public static implicit operator LeaderboardFindResult_t (  LeaderboardFindResult_t.PackSmall d )
			{
				return new LeaderboardFindResult_t()
				{
					SteamLeaderboard = d.SteamLeaderboard,
					LeaderboardFound = d.LeaderboardFound,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<LeaderboardFindResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( LeaderboardFindResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct LeaderboardScoresDownloaded_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUserStats + 5;
		public ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		public ulong SteamLeaderboardEntries; // m_hSteamLeaderboardEntries SteamLeaderboardEntries_t
		public int CEntryCount; // m_cEntryCount int
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static LeaderboardScoresDownloaded_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (LeaderboardScoresDownloaded_t) Marshal.PtrToStructure( p, typeof(LeaderboardScoresDownloaded_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
			public ulong SteamLeaderboardEntries; // m_hSteamLeaderboardEntries SteamLeaderboardEntries_t
			public int CEntryCount; // m_cEntryCount int
			
			//
			// Easily convert from PackSmall to LeaderboardScoresDownloaded_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<LeaderboardScoresDownloaded_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( LeaderboardScoresDownloaded_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct LeaderboardScoreUploaded_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUserStats + 6;
		public byte Success; // m_bSuccess uint8
		public ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		public int Score; // m_nScore int32
		public byte ScoreChanged; // m_bScoreChanged uint8
		public int GlobalRankNew; // m_nGlobalRankNew int
		public int GlobalRankPrevious; // m_nGlobalRankPrevious int
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static LeaderboardScoreUploaded_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (LeaderboardScoreUploaded_t) Marshal.PtrToStructure( p, typeof(LeaderboardScoreUploaded_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public byte Success; // m_bSuccess uint8
			public ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
			public int Score; // m_nScore int32
			public byte ScoreChanged; // m_bScoreChanged uint8
			public int GlobalRankNew; // m_nGlobalRankNew int
			public int GlobalRankPrevious; // m_nGlobalRankPrevious int
			
			//
			// Easily convert from PackSmall to LeaderboardScoreUploaded_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<LeaderboardScoreUploaded_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( LeaderboardScoreUploaded_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct NumberOfCurrentPlayers_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUserStats + 7;
		public byte Success; // m_bSuccess uint8
		public int CPlayers; // m_cPlayers int32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static NumberOfCurrentPlayers_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (NumberOfCurrentPlayers_t) Marshal.PtrToStructure( p, typeof(NumberOfCurrentPlayers_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public byte Success; // m_bSuccess uint8
			public int CPlayers; // m_cPlayers int32
			
			//
			// Easily convert from PackSmall to NumberOfCurrentPlayers_t
			//
			public static implicit operator NumberOfCurrentPlayers_t (  NumberOfCurrentPlayers_t.PackSmall d )
			{
				return new NumberOfCurrentPlayers_t()
				{
					Success = d.Success,
					CPlayers = d.CPlayers,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<NumberOfCurrentPlayers_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( NumberOfCurrentPlayers_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct UserStatsUnloaded_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUserStats + 8;
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static UserStatsUnloaded_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (UserStatsUnloaded_t) Marshal.PtrToStructure( p, typeof(UserStatsUnloaded_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			//
			// Easily convert from PackSmall to UserStatsUnloaded_t
			//
			public static implicit operator UserStatsUnloaded_t (  UserStatsUnloaded_t.PackSmall d )
			{
				return new UserStatsUnloaded_t()
				{
					SteamIDUser = d.SteamIDUser,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<UserStatsUnloaded_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( UserStatsUnloaded_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct UserAchievementIconFetched_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUserStats + 9;
		public ulong GameID; // m_nGameID class CGameID
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string AchievementName; // m_rgchAchievementName char [128]
		[MarshalAs(UnmanagedType.I1)]
		public bool Achieved; // m_bAchieved _Bool
		public int IconHandle; // m_nIconHandle int
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static UserAchievementIconFetched_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (UserAchievementIconFetched_t) Marshal.PtrToStructure( p, typeof(UserAchievementIconFetched_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong GameID; // m_nGameID class CGameID
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string AchievementName; // m_rgchAchievementName char [128]
			[MarshalAs(UnmanagedType.I1)]
			public bool Achieved; // m_bAchieved _Bool
			public int IconHandle; // m_nIconHandle int
			
			//
			// Easily convert from PackSmall to UserAchievementIconFetched_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<UserAchievementIconFetched_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( UserAchievementIconFetched_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct GlobalAchievementPercentagesReady_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUserStats + 10;
		public ulong GameID; // m_nGameID uint64
		public Result Result; // m_eResult enum EResult
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GlobalAchievementPercentagesReady_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GlobalAchievementPercentagesReady_t) Marshal.PtrToStructure( p, typeof(GlobalAchievementPercentagesReady_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong GameID; // m_nGameID uint64
			public Result Result; // m_eResult enum EResult
			
			//
			// Easily convert from PackSmall to GlobalAchievementPercentagesReady_t
			//
			public static implicit operator GlobalAchievementPercentagesReady_t (  GlobalAchievementPercentagesReady_t.PackSmall d )
			{
				return new GlobalAchievementPercentagesReady_t()
				{
					GameID = d.GameID,
					Result = d.Result,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<GlobalAchievementPercentagesReady_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GlobalAchievementPercentagesReady_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct LeaderboardUGCSet_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUserStats + 11;
		public Result Result; // m_eResult enum EResult
		public ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static LeaderboardUGCSet_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (LeaderboardUGCSet_t) Marshal.PtrToStructure( p, typeof(LeaderboardUGCSet_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong SteamLeaderboard; // m_hSteamLeaderboard SteamLeaderboard_t
			
			//
			// Easily convert from PackSmall to LeaderboardUGCSet_t
			//
			public static implicit operator LeaderboardUGCSet_t (  LeaderboardUGCSet_t.PackSmall d )
			{
				return new LeaderboardUGCSet_t()
				{
					Result = d.Result,
					SteamLeaderboard = d.SteamLeaderboard,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<LeaderboardUGCSet_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( LeaderboardUGCSet_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct PS3TrophiesInstalled_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUserStats + 12;
		public ulong GameID; // m_nGameID uint64
		public Result Result; // m_eResult enum EResult
		public ulong RequiredDiskSpace; // m_ulRequiredDiskSpace uint64
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static PS3TrophiesInstalled_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (PS3TrophiesInstalled_t) Marshal.PtrToStructure( p, typeof(PS3TrophiesInstalled_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong GameID; // m_nGameID uint64
			public Result Result; // m_eResult enum EResult
			public ulong RequiredDiskSpace; // m_ulRequiredDiskSpace uint64
			
			//
			// Easily convert from PackSmall to PS3TrophiesInstalled_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<PS3TrophiesInstalled_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( PS3TrophiesInstalled_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct GlobalStatsReceived_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUserStats + 12;
		public ulong GameID; // m_nGameID uint64
		public Result Result; // m_eResult enum EResult
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GlobalStatsReceived_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GlobalStatsReceived_t) Marshal.PtrToStructure( p, typeof(GlobalStatsReceived_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong GameID; // m_nGameID uint64
			public Result Result; // m_eResult enum EResult
			
			//
			// Easily convert from PackSmall to GlobalStatsReceived_t
			//
			public static implicit operator GlobalStatsReceived_t (  GlobalStatsReceived_t.PackSmall d )
			{
				return new GlobalStatsReceived_t()
				{
					GameID = d.GameID,
					Result = d.Result,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<GlobalStatsReceived_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GlobalStatsReceived_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct DlcInstalled_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamApps + 5;
		public uint AppID; // m_nAppID AppId_t
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static DlcInstalled_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (DlcInstalled_t) Marshal.PtrToStructure( p, typeof(DlcInstalled_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint AppID; // m_nAppID AppId_t
			
			//
			// Easily convert from PackSmall to DlcInstalled_t
			//
			public static implicit operator DlcInstalled_t (  DlcInstalled_t.PackSmall d )
			{
				return new DlcInstalled_t()
				{
					AppID = d.AppID,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<DlcInstalled_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( DlcInstalled_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct RegisterActivationCodeResponse_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamApps + 8;
		public RegisterActivationCodeResult Result; // m_eResult enum ERegisterActivationCodeResult
		public uint PackageRegistered; // m_unPackageRegistered uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static RegisterActivationCodeResponse_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (RegisterActivationCodeResponse_t) Marshal.PtrToStructure( p, typeof(RegisterActivationCodeResponse_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public RegisterActivationCodeResult Result; // m_eResult enum ERegisterActivationCodeResult
			public uint PackageRegistered; // m_unPackageRegistered uint32
			
			//
			// Easily convert from PackSmall to RegisterActivationCodeResponse_t
			//
			public static implicit operator RegisterActivationCodeResponse_t (  RegisterActivationCodeResponse_t.PackSmall d )
			{
				return new RegisterActivationCodeResponse_t()
				{
					Result = d.Result,
					PackageRegistered = d.PackageRegistered,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<RegisterActivationCodeResponse_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( RegisterActivationCodeResponse_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct AppProofOfPurchaseKeyResponse_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamApps + 21;
		public Result Result; // m_eResult enum EResult
		public uint AppID; // m_nAppID uint32
		public uint CchKeyLength; // m_cchKeyLength uint32
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 240)]
		public string Key; // m_rgchKey char [240]
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static AppProofOfPurchaseKeyResponse_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (AppProofOfPurchaseKeyResponse_t) Marshal.PtrToStructure( p, typeof(AppProofOfPurchaseKeyResponse_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public uint AppID; // m_nAppID uint32
			public uint CchKeyLength; // m_cchKeyLength uint32
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 240)]
			public string Key; // m_rgchKey char [240]
			
			//
			// Easily convert from PackSmall to AppProofOfPurchaseKeyResponse_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<AppProofOfPurchaseKeyResponse_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( AppProofOfPurchaseKeyResponse_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct FileDetailsResult_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamApps + 23;
		public Result Result; // m_eResult enum EResult
		public ulong FileSize; // m_ulFileSize uint64
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
		public char FileSHA; // m_FileSHA uint8 [20]
		public uint Flags; // m_unFlags uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static FileDetailsResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (FileDetailsResult_t) Marshal.PtrToStructure( p, typeof(FileDetailsResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong FileSize; // m_ulFileSize uint64
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
			public char FileSHA; // m_FileSHA uint8 [20]
			public uint Flags; // m_unFlags uint32
			
			//
			// Easily convert from PackSmall to FileDetailsResult_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<FileDetailsResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( FileDetailsResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct P2PSessionState_t
	{
		public byte ConnectionActive; // m_bConnectionActive uint8
		public byte Connecting; // m_bConnecting uint8
		public byte P2PSessionError; // m_eP2PSessionError uint8
		public byte UsingRelay; // m_bUsingRelay uint8
		public int BytesQueuedForSend; // m_nBytesQueuedForSend int32
		public int PacketsQueuedForSend; // m_nPacketsQueuedForSend int32
		public uint RemoteIP; // m_nRemoteIP uint32
		public ushort RemotePort; // m_nRemotePort uint16
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static P2PSessionState_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (P2PSessionState_t) Marshal.PtrToStructure( p, typeof(P2PSessionState_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public byte ConnectionActive; // m_bConnectionActive uint8
			public byte Connecting; // m_bConnecting uint8
			public byte P2PSessionError; // m_eP2PSessionError uint8
			public byte UsingRelay; // m_bUsingRelay uint8
			public int BytesQueuedForSend; // m_nBytesQueuedForSend int32
			public int PacketsQueuedForSend; // m_nPacketsQueuedForSend int32
			public uint RemoteIP; // m_nRemoteIP uint32
			public ushort RemotePort; // m_nRemotePort uint16
			
			//
			// Easily convert from PackSmall to P2PSessionState_t
			//
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
	internal struct P2PSessionRequest_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamNetworking + 2;
		public ulong SteamIDRemote; // m_steamIDRemote class CSteamID
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static P2PSessionRequest_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (P2PSessionRequest_t) Marshal.PtrToStructure( p, typeof(P2PSessionRequest_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDRemote; // m_steamIDRemote class CSteamID
			
			//
			// Easily convert from PackSmall to P2PSessionRequest_t
			//
			public static implicit operator P2PSessionRequest_t (  P2PSessionRequest_t.PackSmall d )
			{
				return new P2PSessionRequest_t()
				{
					SteamIDRemote = d.SteamIDRemote,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<P2PSessionRequest_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( P2PSessionRequest_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct P2PSessionConnectFail_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamNetworking + 3;
		public ulong SteamIDRemote; // m_steamIDRemote class CSteamID
		public byte P2PSessionError; // m_eP2PSessionError uint8
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static P2PSessionConnectFail_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (P2PSessionConnectFail_t) Marshal.PtrToStructure( p, typeof(P2PSessionConnectFail_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDRemote; // m_steamIDRemote class CSteamID
			public byte P2PSessionError; // m_eP2PSessionError uint8
			
			//
			// Easily convert from PackSmall to P2PSessionConnectFail_t
			//
			public static implicit operator P2PSessionConnectFail_t (  P2PSessionConnectFail_t.PackSmall d )
			{
				return new P2PSessionConnectFail_t()
				{
					SteamIDRemote = d.SteamIDRemote,
					P2PSessionError = d.P2PSessionError,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<P2PSessionConnectFail_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( P2PSessionConnectFail_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct SocketStatusCallback_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamNetworking + 1;
		public uint Socket; // m_hSocket SNetSocket_t
		public uint ListenSocket; // m_hListenSocket SNetListenSocket_t
		public ulong SteamIDRemote; // m_steamIDRemote class CSteamID
		public int SNetSocketState; // m_eSNetSocketState int
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static SocketStatusCallback_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (SocketStatusCallback_t) Marshal.PtrToStructure( p, typeof(SocketStatusCallback_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint Socket; // m_hSocket SNetSocket_t
			public uint ListenSocket; // m_hListenSocket SNetListenSocket_t
			public ulong SteamIDRemote; // m_steamIDRemote class CSteamID
			public int SNetSocketState; // m_eSNetSocketState int
			
			//
			// Easily convert from PackSmall to SocketStatusCallback_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<SocketStatusCallback_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( SocketStatusCallback_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct ScreenshotReady_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamScreenshots + 1;
		public uint Local; // m_hLocal ScreenshotHandle
		public Result Result; // m_eResult enum EResult
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static ScreenshotReady_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (ScreenshotReady_t) Marshal.PtrToStructure( p, typeof(ScreenshotReady_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint Local; // m_hLocal ScreenshotHandle
			public Result Result; // m_eResult enum EResult
			
			//
			// Easily convert from PackSmall to ScreenshotReady_t
			//
			public static implicit operator ScreenshotReady_t (  ScreenshotReady_t.PackSmall d )
			{
				return new ScreenshotReady_t()
				{
					Local = d.Local,
					Result = d.Result,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<ScreenshotReady_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( ScreenshotReady_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct VolumeHasChanged_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMusic + 2;
		public float NewVolume; // m_flNewVolume float
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static VolumeHasChanged_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (VolumeHasChanged_t) Marshal.PtrToStructure( p, typeof(VolumeHasChanged_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public float NewVolume; // m_flNewVolume float
			
			//
			// Easily convert from PackSmall to VolumeHasChanged_t
			//
			public static implicit operator VolumeHasChanged_t (  VolumeHasChanged_t.PackSmall d )
			{
				return new VolumeHasChanged_t()
				{
					NewVolume = d.NewVolume,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<VolumeHasChanged_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( VolumeHasChanged_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct MusicPlayerWantsShuffled_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMusicRemote + 9;
		[MarshalAs(UnmanagedType.I1)]
		public bool Shuffled; // m_bShuffled _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static MusicPlayerWantsShuffled_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (MusicPlayerWantsShuffled_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsShuffled_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool Shuffled; // m_bShuffled _Bool
			
			//
			// Easily convert from PackSmall to MusicPlayerWantsShuffled_t
			//
			public static implicit operator MusicPlayerWantsShuffled_t (  MusicPlayerWantsShuffled_t.PackSmall d )
			{
				return new MusicPlayerWantsShuffled_t()
				{
					Shuffled = d.Shuffled,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<MusicPlayerWantsShuffled_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( MusicPlayerWantsShuffled_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct MusicPlayerWantsLooped_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMusicRemote + 10;
		[MarshalAs(UnmanagedType.I1)]
		public bool Looped; // m_bLooped _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static MusicPlayerWantsLooped_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (MusicPlayerWantsLooped_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsLooped_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool Looped; // m_bLooped _Bool
			
			//
			// Easily convert from PackSmall to MusicPlayerWantsLooped_t
			//
			public static implicit operator MusicPlayerWantsLooped_t (  MusicPlayerWantsLooped_t.PackSmall d )
			{
				return new MusicPlayerWantsLooped_t()
				{
					Looped = d.Looped,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<MusicPlayerWantsLooped_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( MusicPlayerWantsLooped_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct MusicPlayerWantsVolume_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMusic + 11;
		public float NewVolume; // m_flNewVolume float
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static MusicPlayerWantsVolume_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (MusicPlayerWantsVolume_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsVolume_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public float NewVolume; // m_flNewVolume float
			
			//
			// Easily convert from PackSmall to MusicPlayerWantsVolume_t
			//
			public static implicit operator MusicPlayerWantsVolume_t (  MusicPlayerWantsVolume_t.PackSmall d )
			{
				return new MusicPlayerWantsVolume_t()
				{
					NewVolume = d.NewVolume,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<MusicPlayerWantsVolume_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( MusicPlayerWantsVolume_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct MusicPlayerSelectsQueueEntry_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMusic + 12;
		public int NID; // nID int
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static MusicPlayerSelectsQueueEntry_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (MusicPlayerSelectsQueueEntry_t) Marshal.PtrToStructure( p, typeof(MusicPlayerSelectsQueueEntry_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public int NID; // nID int
			
			//
			// Easily convert from PackSmall to MusicPlayerSelectsQueueEntry_t
			//
			public static implicit operator MusicPlayerSelectsQueueEntry_t (  MusicPlayerSelectsQueueEntry_t.PackSmall d )
			{
				return new MusicPlayerSelectsQueueEntry_t()
				{
					NID = d.NID,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<MusicPlayerSelectsQueueEntry_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( MusicPlayerSelectsQueueEntry_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct MusicPlayerSelectsPlaylistEntry_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMusic + 13;
		public int NID; // nID int
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static MusicPlayerSelectsPlaylistEntry_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (MusicPlayerSelectsPlaylistEntry_t) Marshal.PtrToStructure( p, typeof(MusicPlayerSelectsPlaylistEntry_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public int NID; // nID int
			
			//
			// Easily convert from PackSmall to MusicPlayerSelectsPlaylistEntry_t
			//
			public static implicit operator MusicPlayerSelectsPlaylistEntry_t (  MusicPlayerSelectsPlaylistEntry_t.PackSmall d )
			{
				return new MusicPlayerSelectsPlaylistEntry_t()
				{
					NID = d.NID,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<MusicPlayerSelectsPlaylistEntry_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( MusicPlayerSelectsPlaylistEntry_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct MusicPlayerWantsPlayingRepeatStatus_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamMusicRemote + 14;
		public int PlayingRepeatStatus; // m_nPlayingRepeatStatus int
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static MusicPlayerWantsPlayingRepeatStatus_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (MusicPlayerWantsPlayingRepeatStatus_t) Marshal.PtrToStructure( p, typeof(MusicPlayerWantsPlayingRepeatStatus_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public int PlayingRepeatStatus; // m_nPlayingRepeatStatus int
			
			//
			// Easily convert from PackSmall to MusicPlayerWantsPlayingRepeatStatus_t
			//
			public static implicit operator MusicPlayerWantsPlayingRepeatStatus_t (  MusicPlayerWantsPlayingRepeatStatus_t.PackSmall d )
			{
				return new MusicPlayerWantsPlayingRepeatStatus_t()
				{
					PlayingRepeatStatus = d.PlayingRepeatStatus,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<MusicPlayerWantsPlayingRepeatStatus_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( MusicPlayerWantsPlayingRepeatStatus_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTTPRequestCompleted_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientHTTP + 1;
		public uint Request; // m_hRequest HTTPRequestHandle
		public ulong ContextValue; // m_ulContextValue uint64
		[MarshalAs(UnmanagedType.I1)]
		public bool RequestSuccessful; // m_bRequestSuccessful _Bool
		public HTTPStatusCode StatusCode; // m_eStatusCode enum EHTTPStatusCode
		public uint BodySize; // m_unBodySize uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTTPRequestCompleted_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTTPRequestCompleted_t) Marshal.PtrToStructure( p, typeof(HTTPRequestCompleted_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint Request; // m_hRequest HTTPRequestHandle
			public ulong ContextValue; // m_ulContextValue uint64
			[MarshalAs(UnmanagedType.I1)]
			public bool RequestSuccessful; // m_bRequestSuccessful _Bool
			public HTTPStatusCode StatusCode; // m_eStatusCode enum EHTTPStatusCode
			public uint BodySize; // m_unBodySize uint32
			
			//
			// Easily convert from PackSmall to HTTPRequestCompleted_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTTPRequestCompleted_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTTPRequestCompleted_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTTPRequestHeadersReceived_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientHTTP + 2;
		public uint Request; // m_hRequest HTTPRequestHandle
		public ulong ContextValue; // m_ulContextValue uint64
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTTPRequestHeadersReceived_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTTPRequestHeadersReceived_t) Marshal.PtrToStructure( p, typeof(HTTPRequestHeadersReceived_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint Request; // m_hRequest HTTPRequestHandle
			public ulong ContextValue; // m_ulContextValue uint64
			
			//
			// Easily convert from PackSmall to HTTPRequestHeadersReceived_t
			//
			public static implicit operator HTTPRequestHeadersReceived_t (  HTTPRequestHeadersReceived_t.PackSmall d )
			{
				return new HTTPRequestHeadersReceived_t()
				{
					Request = d.Request,
					ContextValue = d.ContextValue,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTTPRequestHeadersReceived_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTTPRequestHeadersReceived_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTTPRequestDataReceived_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientHTTP + 3;
		public uint Request; // m_hRequest HTTPRequestHandle
		public ulong ContextValue; // m_ulContextValue uint64
		public uint COffset; // m_cOffset uint32
		public uint CBytesReceived; // m_cBytesReceived uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTTPRequestDataReceived_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTTPRequestDataReceived_t) Marshal.PtrToStructure( p, typeof(HTTPRequestDataReceived_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint Request; // m_hRequest HTTPRequestHandle
			public ulong ContextValue; // m_ulContextValue uint64
			public uint COffset; // m_cOffset uint32
			public uint CBytesReceived; // m_cBytesReceived uint32
			
			//
			// Easily convert from PackSmall to HTTPRequestDataReceived_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTTPRequestDataReceived_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTTPRequestDataReceived_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct SteamUnifiedMessagesSendMethodResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientUnifiedMessages + 1;
		public ulong Handle; // m_hHandle ClientUnifiedMessageHandle
		public ulong Context; // m_unContext uint64
		public Result Result; // m_eResult enum EResult
		public uint ResponseSize; // m_unResponseSize uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static SteamUnifiedMessagesSendMethodResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (SteamUnifiedMessagesSendMethodResult_t) Marshal.PtrToStructure( p, typeof(SteamUnifiedMessagesSendMethodResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong Handle; // m_hHandle ClientUnifiedMessageHandle
			public ulong Context; // m_unContext uint64
			public Result Result; // m_eResult enum EResult
			public uint ResponseSize; // m_unResponseSize uint32
			
			//
			// Easily convert from PackSmall to SteamUnifiedMessagesSendMethodResult_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<SteamUnifiedMessagesSendMethodResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( SteamUnifiedMessagesSendMethodResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct ControllerAnalogActionData_t
	{
		public ControllerSourceMode EMode; // eMode enum EControllerSourceMode
		public float X; // x float
		public float Y; // y float
		[MarshalAs(UnmanagedType.I1)]
		public bool BActive; // bActive _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static ControllerAnalogActionData_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (ControllerAnalogActionData_t) Marshal.PtrToStructure( p, typeof(ControllerAnalogActionData_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ControllerSourceMode EMode; // eMode enum EControllerSourceMode
			public float X; // x float
			public float Y; // y float
			[MarshalAs(UnmanagedType.I1)]
			public bool BActive; // bActive _Bool
			
			//
			// Easily convert from PackSmall to ControllerAnalogActionData_t
			//
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
	internal struct ControllerDigitalActionData_t
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool BState; // bState _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool BActive; // bActive _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static ControllerDigitalActionData_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (ControllerDigitalActionData_t) Marshal.PtrToStructure( p, typeof(ControllerDigitalActionData_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool BState; // bState _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool BActive; // bActive _Bool
			
			//
			// Easily convert from PackSmall to ControllerDigitalActionData_t
			//
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
	internal struct ControllerMotionData_t
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
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static ControllerMotionData_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (ControllerMotionData_t) Marshal.PtrToStructure( p, typeof(ControllerMotionData_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
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
			
			//
			// Easily convert from PackSmall to ControllerMotionData_t
			//
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
	internal struct SteamUGCDetails_t
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
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static SteamUGCDetails_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (SteamUGCDetails_t) Marshal.PtrToStructure( p, typeof(SteamUGCDetails_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
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
			
			//
			// Easily convert from PackSmall to SteamUGCDetails_t
			//
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
	internal struct SteamUGCQueryCompleted_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientUGC + 1;
		public ulong Handle; // m_handle UGCQueryHandle_t
		public Result Result; // m_eResult enum EResult
		public uint NumResultsReturned; // m_unNumResultsReturned uint32
		public uint TotalMatchingResults; // m_unTotalMatchingResults uint32
		[MarshalAs(UnmanagedType.I1)]
		public bool CachedData; // m_bCachedData _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static SteamUGCQueryCompleted_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (SteamUGCQueryCompleted_t) Marshal.PtrToStructure( p, typeof(SteamUGCQueryCompleted_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong Handle; // m_handle UGCQueryHandle_t
			public Result Result; // m_eResult enum EResult
			public uint NumResultsReturned; // m_unNumResultsReturned uint32
			public uint TotalMatchingResults; // m_unTotalMatchingResults uint32
			[MarshalAs(UnmanagedType.I1)]
			public bool CachedData; // m_bCachedData _Bool
			
			//
			// Easily convert from PackSmall to SteamUGCQueryCompleted_t
			//
			public static implicit operator SteamUGCQueryCompleted_t (  SteamUGCQueryCompleted_t.PackSmall d )
			{
				return new SteamUGCQueryCompleted_t()
				{
					Handle = d.Handle,
					Result = d.Result,
					NumResultsReturned = d.NumResultsReturned,
					TotalMatchingResults = d.TotalMatchingResults,
					CachedData = d.CachedData,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<SteamUGCQueryCompleted_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( SteamUGCQueryCompleted_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct SteamUGCRequestUGCDetailsResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientUGC + 2;
		public SteamUGCDetails_t Details; // m_details struct SteamUGCDetails_t
		[MarshalAs(UnmanagedType.I1)]
		public bool CachedData; // m_bCachedData _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static SteamUGCRequestUGCDetailsResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (SteamUGCRequestUGCDetailsResult_t) Marshal.PtrToStructure( p, typeof(SteamUGCRequestUGCDetailsResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public SteamUGCDetails_t Details; // m_details struct SteamUGCDetails_t
			[MarshalAs(UnmanagedType.I1)]
			public bool CachedData; // m_bCachedData _Bool
			
			//
			// Easily convert from PackSmall to SteamUGCRequestUGCDetailsResult_t
			//
			public static implicit operator SteamUGCRequestUGCDetailsResult_t (  SteamUGCRequestUGCDetailsResult_t.PackSmall d )
			{
				return new SteamUGCRequestUGCDetailsResult_t()
				{
					Details = d.Details,
					CachedData = d.CachedData,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<SteamUGCRequestUGCDetailsResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( SteamUGCRequestUGCDetailsResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct CreateItemResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientUGC + 3;
		public Result Result; // m_eResult enum EResult
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		[MarshalAs(UnmanagedType.I1)]
		public bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static CreateItemResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (CreateItemResult_t) Marshal.PtrToStructure( p, typeof(CreateItemResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			[MarshalAs(UnmanagedType.I1)]
			public bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			
			//
			// Easily convert from PackSmall to CreateItemResult_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<CreateItemResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( CreateItemResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct SubmitItemUpdateResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientUGC + 4;
		public Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		public bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static SubmitItemUpdateResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (SubmitItemUpdateResult_t) Marshal.PtrToStructure( p, typeof(SubmitItemUpdateResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			public bool UserNeedsToAcceptWorkshopLegalAgreement; // m_bUserNeedsToAcceptWorkshopLegalAgreement _Bool
			
			//
			// Easily convert from PackSmall to SubmitItemUpdateResult_t
			//
			public static implicit operator SubmitItemUpdateResult_t (  SubmitItemUpdateResult_t.PackSmall d )
			{
				return new SubmitItemUpdateResult_t()
				{
					Result = d.Result,
					UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<SubmitItemUpdateResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( SubmitItemUpdateResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct DownloadItemResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientUGC + 6;
		public uint AppID; // m_unAppID AppId_t
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public Result Result; // m_eResult enum EResult
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static DownloadItemResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (DownloadItemResult_t) Marshal.PtrToStructure( p, typeof(DownloadItemResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint AppID; // m_unAppID AppId_t
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public Result Result; // m_eResult enum EResult
			
			//
			// Easily convert from PackSmall to DownloadItemResult_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<DownloadItemResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( DownloadItemResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct UserFavoriteItemsListChanged_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientUGC + 7;
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		public bool WasAddRequest; // m_bWasAddRequest _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static UserFavoriteItemsListChanged_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (UserFavoriteItemsListChanged_t) Marshal.PtrToStructure( p, typeof(UserFavoriteItemsListChanged_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			public bool WasAddRequest; // m_bWasAddRequest _Bool
			
			//
			// Easily convert from PackSmall to UserFavoriteItemsListChanged_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<UserFavoriteItemsListChanged_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( UserFavoriteItemsListChanged_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct SetUserItemVoteResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientUGC + 8;
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		public bool VoteUp; // m_bVoteUp _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static SetUserItemVoteResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (SetUserItemVoteResult_t) Marshal.PtrToStructure( p, typeof(SetUserItemVoteResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			public bool VoteUp; // m_bVoteUp _Bool
			
			//
			// Easily convert from PackSmall to SetUserItemVoteResult_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<SetUserItemVoteResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( SetUserItemVoteResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct GetUserItemVoteResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientUGC + 9;
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		public Result Result; // m_eResult enum EResult
		[MarshalAs(UnmanagedType.I1)]
		public bool VotedUp; // m_bVotedUp _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool VotedDown; // m_bVotedDown _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool VoteSkipped; // m_bVoteSkipped _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GetUserItemVoteResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GetUserItemVoteResult_t) Marshal.PtrToStructure( p, typeof(GetUserItemVoteResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			public Result Result; // m_eResult enum EResult
			[MarshalAs(UnmanagedType.I1)]
			public bool VotedUp; // m_bVotedUp _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool VotedDown; // m_bVotedDown _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool VoteSkipped; // m_bVoteSkipped _Bool
			
			//
			// Easily convert from PackSmall to GetUserItemVoteResult_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<GetUserItemVoteResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GetUserItemVoteResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct StartPlaytimeTrackingResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientUGC + 10;
		public Result Result; // m_eResult enum EResult
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static StartPlaytimeTrackingResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (StartPlaytimeTrackingResult_t) Marshal.PtrToStructure( p, typeof(StartPlaytimeTrackingResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			
			//
			// Easily convert from PackSmall to StartPlaytimeTrackingResult_t
			//
			public static implicit operator StartPlaytimeTrackingResult_t (  StartPlaytimeTrackingResult_t.PackSmall d )
			{
				return new StartPlaytimeTrackingResult_t()
				{
					Result = d.Result,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<StartPlaytimeTrackingResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( StartPlaytimeTrackingResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct StopPlaytimeTrackingResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientUGC + 11;
		public Result Result; // m_eResult enum EResult
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static StopPlaytimeTrackingResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (StopPlaytimeTrackingResult_t) Marshal.PtrToStructure( p, typeof(StopPlaytimeTrackingResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			
			//
			// Easily convert from PackSmall to StopPlaytimeTrackingResult_t
			//
			public static implicit operator StopPlaytimeTrackingResult_t (  StopPlaytimeTrackingResult_t.PackSmall d )
			{
				return new StopPlaytimeTrackingResult_t()
				{
					Result = d.Result,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<StopPlaytimeTrackingResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( StopPlaytimeTrackingResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct SteamAppInstalled_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamAppList + 1;
		public uint AppID; // m_nAppID AppId_t
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static SteamAppInstalled_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (SteamAppInstalled_t) Marshal.PtrToStructure( p, typeof(SteamAppInstalled_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint AppID; // m_nAppID AppId_t
			
			//
			// Easily convert from PackSmall to SteamAppInstalled_t
			//
			public static implicit operator SteamAppInstalled_t (  SteamAppInstalled_t.PackSmall d )
			{
				return new SteamAppInstalled_t()
				{
					AppID = d.AppID,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<SteamAppInstalled_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( SteamAppInstalled_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct SteamAppUninstalled_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamAppList + 2;
		public uint AppID; // m_nAppID AppId_t
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static SteamAppUninstalled_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (SteamAppUninstalled_t) Marshal.PtrToStructure( p, typeof(SteamAppUninstalled_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint AppID; // m_nAppID AppId_t
			
			//
			// Easily convert from PackSmall to SteamAppUninstalled_t
			//
			public static implicit operator SteamAppUninstalled_t (  SteamAppUninstalled_t.PackSmall d )
			{
				return new SteamAppUninstalled_t()
				{
					AppID = d.AppID,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<SteamAppUninstalled_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( SteamAppUninstalled_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_BrowserReady_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 1;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_BrowserReady_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_BrowserReady_t) Marshal.PtrToStructure( p, typeof(HTML_BrowserReady_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			
			//
			// Easily convert from PackSmall to HTML_BrowserReady_t
			//
			public static implicit operator HTML_BrowserReady_t (  HTML_BrowserReady_t.PackSmall d )
			{
				return new HTML_BrowserReady_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<HTML_BrowserReady_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_BrowserReady_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_NeedsPaint_t
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
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_NeedsPaint_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_NeedsPaint_t) Marshal.PtrToStructure( p, typeof(HTML_NeedsPaint_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
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
			
			//
			// Easily convert from PackSmall to HTML_NeedsPaint_t
			//
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
	internal struct HTML_StartRequest_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchURL; // pchURL const char *
		public string PchTarget; // pchTarget const char *
		public string PchPostData; // pchPostData const char *
		[MarshalAs(UnmanagedType.I1)]
		public bool BIsRedirect; // bIsRedirect _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_StartRequest_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_StartRequest_t) Marshal.PtrToStructure( p, typeof(HTML_StartRequest_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchURL; // pchURL const char *
			public string PchTarget; // pchTarget const char *
			public string PchPostData; // pchPostData const char *
			[MarshalAs(UnmanagedType.I1)]
			public bool BIsRedirect; // bIsRedirect _Bool
			
			//
			// Easily convert from PackSmall to HTML_StartRequest_t
			//
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
	internal struct HTML_CloseBrowser_t
	{
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_CloseBrowser_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_CloseBrowser_t) Marshal.PtrToStructure( p, typeof(HTML_CloseBrowser_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			
			//
			// Easily convert from PackSmall to HTML_CloseBrowser_t
			//
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
	internal struct HTML_URLChanged_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 5;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchURL; // pchURL const char *
		public string PchPostData; // pchPostData const char *
		[MarshalAs(UnmanagedType.I1)]
		public bool BIsRedirect; // bIsRedirect _Bool
		public string PchPageTitle; // pchPageTitle const char *
		[MarshalAs(UnmanagedType.I1)]
		public bool BNewNavigation; // bNewNavigation _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_URLChanged_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_URLChanged_t) Marshal.PtrToStructure( p, typeof(HTML_URLChanged_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchURL; // pchURL const char *
			public string PchPostData; // pchPostData const char *
			[MarshalAs(UnmanagedType.I1)]
			public bool BIsRedirect; // bIsRedirect _Bool
			public string PchPageTitle; // pchPageTitle const char *
			[MarshalAs(UnmanagedType.I1)]
			public bool BNewNavigation; // bNewNavigation _Bool
			
			//
			// Easily convert from PackSmall to HTML_URLChanged_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTML_URLChanged_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_URLChanged_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_FinishedRequest_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 6;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchURL; // pchURL const char *
		public string PchPageTitle; // pchPageTitle const char *
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_FinishedRequest_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_FinishedRequest_t) Marshal.PtrToStructure( p, typeof(HTML_FinishedRequest_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchURL; // pchURL const char *
			public string PchPageTitle; // pchPageTitle const char *
			
			//
			// Easily convert from PackSmall to HTML_FinishedRequest_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTML_FinishedRequest_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_FinishedRequest_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_OpenLinkInNewTab_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 7;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchURL; // pchURL const char *
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_OpenLinkInNewTab_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_OpenLinkInNewTab_t) Marshal.PtrToStructure( p, typeof(HTML_OpenLinkInNewTab_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchURL; // pchURL const char *
			
			//
			// Easily convert from PackSmall to HTML_OpenLinkInNewTab_t
			//
			public static implicit operator HTML_OpenLinkInNewTab_t (  HTML_OpenLinkInNewTab_t.PackSmall d )
			{
				return new HTML_OpenLinkInNewTab_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchURL = d.PchURL,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTML_OpenLinkInNewTab_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_OpenLinkInNewTab_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_ChangedTitle_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 8;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchTitle; // pchTitle const char *
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_ChangedTitle_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_ChangedTitle_t) Marshal.PtrToStructure( p, typeof(HTML_ChangedTitle_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchTitle; // pchTitle const char *
			
			//
			// Easily convert from PackSmall to HTML_ChangedTitle_t
			//
			public static implicit operator HTML_ChangedTitle_t (  HTML_ChangedTitle_t.PackSmall d )
			{
				return new HTML_ChangedTitle_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchTitle = d.PchTitle,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTML_ChangedTitle_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_ChangedTitle_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_SearchResults_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 9;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public uint UnResults; // unResults uint32
		public uint UnCurrentMatch; // unCurrentMatch uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_SearchResults_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_SearchResults_t) Marshal.PtrToStructure( p, typeof(HTML_SearchResults_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public uint UnResults; // unResults uint32
			public uint UnCurrentMatch; // unCurrentMatch uint32
			
			//
			// Easily convert from PackSmall to HTML_SearchResults_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTML_SearchResults_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_SearchResults_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_CanGoBackAndForward_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 10;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		[MarshalAs(UnmanagedType.I1)]
		public bool BCanGoBack; // bCanGoBack _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool BCanGoForward; // bCanGoForward _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_CanGoBackAndForward_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_CanGoBackAndForward_t) Marshal.PtrToStructure( p, typeof(HTML_CanGoBackAndForward_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			[MarshalAs(UnmanagedType.I1)]
			public bool BCanGoBack; // bCanGoBack _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool BCanGoForward; // bCanGoForward _Bool
			
			//
			// Easily convert from PackSmall to HTML_CanGoBackAndForward_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTML_CanGoBackAndForward_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_CanGoBackAndForward_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_HorizontalScroll_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 11;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public uint UnScrollMax; // unScrollMax uint32
		public uint UnScrollCurrent; // unScrollCurrent uint32
		public float FlPageScale; // flPageScale float
		[MarshalAs(UnmanagedType.I1)]
		public bool BVisible; // bVisible _Bool
		public uint UnPageSize; // unPageSize uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_HorizontalScroll_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_HorizontalScroll_t) Marshal.PtrToStructure( p, typeof(HTML_HorizontalScroll_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public uint UnScrollMax; // unScrollMax uint32
			public uint UnScrollCurrent; // unScrollCurrent uint32
			public float FlPageScale; // flPageScale float
			[MarshalAs(UnmanagedType.I1)]
			public bool BVisible; // bVisible _Bool
			public uint UnPageSize; // unPageSize uint32
			
			//
			// Easily convert from PackSmall to HTML_HorizontalScroll_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTML_HorizontalScroll_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_HorizontalScroll_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_VerticalScroll_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 12;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public uint UnScrollMax; // unScrollMax uint32
		public uint UnScrollCurrent; // unScrollCurrent uint32
		public float FlPageScale; // flPageScale float
		[MarshalAs(UnmanagedType.I1)]
		public bool BVisible; // bVisible _Bool
		public uint UnPageSize; // unPageSize uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_VerticalScroll_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_VerticalScroll_t) Marshal.PtrToStructure( p, typeof(HTML_VerticalScroll_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public uint UnScrollMax; // unScrollMax uint32
			public uint UnScrollCurrent; // unScrollCurrent uint32
			public float FlPageScale; // flPageScale float
			[MarshalAs(UnmanagedType.I1)]
			public bool BVisible; // bVisible _Bool
			public uint UnPageSize; // unPageSize uint32
			
			//
			// Easily convert from PackSmall to HTML_VerticalScroll_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTML_VerticalScroll_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_VerticalScroll_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_LinkAtPosition_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 13;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public uint X; // x uint32
		public uint Y; // y uint32
		public string PchURL; // pchURL const char *
		[MarshalAs(UnmanagedType.I1)]
		public bool BInput; // bInput _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool BLiveLink; // bLiveLink _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_LinkAtPosition_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_LinkAtPosition_t) Marshal.PtrToStructure( p, typeof(HTML_LinkAtPosition_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public uint X; // x uint32
			public uint Y; // y uint32
			public string PchURL; // pchURL const char *
			[MarshalAs(UnmanagedType.I1)]
			public bool BInput; // bInput _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool BLiveLink; // bLiveLink _Bool
			
			//
			// Easily convert from PackSmall to HTML_LinkAtPosition_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTML_LinkAtPosition_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_LinkAtPosition_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_JSAlert_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 14;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchMessage; // pchMessage const char *
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_JSAlert_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_JSAlert_t) Marshal.PtrToStructure( p, typeof(HTML_JSAlert_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchMessage; // pchMessage const char *
			
			//
			// Easily convert from PackSmall to HTML_JSAlert_t
			//
			public static implicit operator HTML_JSAlert_t (  HTML_JSAlert_t.PackSmall d )
			{
				return new HTML_JSAlert_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchMessage = d.PchMessage,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTML_JSAlert_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_JSAlert_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_JSConfirm_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 15;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchMessage; // pchMessage const char *
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_JSConfirm_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_JSConfirm_t) Marshal.PtrToStructure( p, typeof(HTML_JSConfirm_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchMessage; // pchMessage const char *
			
			//
			// Easily convert from PackSmall to HTML_JSConfirm_t
			//
			public static implicit operator HTML_JSConfirm_t (  HTML_JSConfirm_t.PackSmall d )
			{
				return new HTML_JSConfirm_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchMessage = d.PchMessage,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTML_JSConfirm_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_JSConfirm_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_FileOpenDialog_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 16;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchTitle; // pchTitle const char *
		public string PchInitialFile; // pchInitialFile const char *
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_FileOpenDialog_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_FileOpenDialog_t) Marshal.PtrToStructure( p, typeof(HTML_FileOpenDialog_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchTitle; // pchTitle const char *
			public string PchInitialFile; // pchInitialFile const char *
			
			//
			// Easily convert from PackSmall to HTML_FileOpenDialog_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTML_FileOpenDialog_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_FileOpenDialog_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_NewWindow_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 21;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchURL; // pchURL const char *
		public uint UnX; // unX uint32
		public uint UnY; // unY uint32
		public uint UnWide; // unWide uint32
		public uint UnTall; // unTall uint32
		public uint UnNewWindow_BrowserHandle; // unNewWindow_BrowserHandle HHTMLBrowser
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_NewWindow_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_NewWindow_t) Marshal.PtrToStructure( p, typeof(HTML_NewWindow_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchURL; // pchURL const char *
			public uint UnX; // unX uint32
			public uint UnY; // unY uint32
			public uint UnWide; // unWide uint32
			public uint UnTall; // unTall uint32
			public uint UnNewWindow_BrowserHandle; // unNewWindow_BrowserHandle HHTMLBrowser
			
			//
			// Easily convert from PackSmall to HTML_NewWindow_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTML_NewWindow_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_NewWindow_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_SetCursor_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 22;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public uint EMouseCursor; // eMouseCursor uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_SetCursor_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_SetCursor_t) Marshal.PtrToStructure( p, typeof(HTML_SetCursor_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public uint EMouseCursor; // eMouseCursor uint32
			
			//
			// Easily convert from PackSmall to HTML_SetCursor_t
			//
			public static implicit operator HTML_SetCursor_t (  HTML_SetCursor_t.PackSmall d )
			{
				return new HTML_SetCursor_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					EMouseCursor = d.EMouseCursor,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTML_SetCursor_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_SetCursor_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_StatusText_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 23;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchMsg; // pchMsg const char *
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_StatusText_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_StatusText_t) Marshal.PtrToStructure( p, typeof(HTML_StatusText_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchMsg; // pchMsg const char *
			
			//
			// Easily convert from PackSmall to HTML_StatusText_t
			//
			public static implicit operator HTML_StatusText_t (  HTML_StatusText_t.PackSmall d )
			{
				return new HTML_StatusText_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchMsg = d.PchMsg,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTML_StatusText_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_StatusText_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_ShowToolTip_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 24;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchMsg; // pchMsg const char *
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_ShowToolTip_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_ShowToolTip_t) Marshal.PtrToStructure( p, typeof(HTML_ShowToolTip_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchMsg; // pchMsg const char *
			
			//
			// Easily convert from PackSmall to HTML_ShowToolTip_t
			//
			public static implicit operator HTML_ShowToolTip_t (  HTML_ShowToolTip_t.PackSmall d )
			{
				return new HTML_ShowToolTip_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchMsg = d.PchMsg,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTML_ShowToolTip_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_ShowToolTip_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_UpdateToolTip_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 25;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		public string PchMsg; // pchMsg const char *
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_UpdateToolTip_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_UpdateToolTip_t) Marshal.PtrToStructure( p, typeof(HTML_UpdateToolTip_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			public string PchMsg; // pchMsg const char *
			
			//
			// Easily convert from PackSmall to HTML_UpdateToolTip_t
			//
			public static implicit operator HTML_UpdateToolTip_t (  HTML_UpdateToolTip_t.PackSmall d )
			{
				return new HTML_UpdateToolTip_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchMsg = d.PchMsg,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTML_UpdateToolTip_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_UpdateToolTip_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct HTML_HideToolTip_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamHTMLSurface + 26;
		public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static HTML_HideToolTip_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (HTML_HideToolTip_t) Marshal.PtrToStructure( p, typeof(HTML_HideToolTip_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
			
			//
			// Easily convert from PackSmall to HTML_HideToolTip_t
			//
			public static implicit operator HTML_HideToolTip_t (  HTML_HideToolTip_t.PackSmall d )
			{
				return new HTML_HideToolTip_t()
				{
					UnBrowserHandle = d.UnBrowserHandle,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<HTML_HideToolTip_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( HTML_HideToolTip_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct SteamItemDetails_t
	{
		public ulong ItemId; // m_itemId SteamItemInstanceID_t
		public int Definition; // m_iDefinition SteamItemDef_t
		public ushort Quantity; // m_unQuantity uint16
		public ushort Flags; // m_unFlags uint16
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static SteamItemDetails_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (SteamItemDetails_t) Marshal.PtrToStructure( p, typeof(SteamItemDetails_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong ItemId; // m_itemId SteamItemInstanceID_t
			public int Definition; // m_iDefinition SteamItemDef_t
			public ushort Quantity; // m_unQuantity uint16
			public ushort Flags; // m_unFlags uint16
			
			//
			// Easily convert from PackSmall to SteamItemDetails_t
			//
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
	internal struct SteamInventoryResultReady_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientInventory + 0;
		public int Handle; // m_handle SteamInventoryResult_t
		public Result Esult; // m_result enum EResult
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static SteamInventoryResultReady_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (SteamInventoryResultReady_t) Marshal.PtrToStructure( p, typeof(SteamInventoryResultReady_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public int Handle; // m_handle SteamInventoryResult_t
			public Result Esult; // m_result enum EResult
			
			//
			// Easily convert from PackSmall to SteamInventoryResultReady_t
			//
			public static implicit operator SteamInventoryResultReady_t (  SteamInventoryResultReady_t.PackSmall d )
			{
				return new SteamInventoryResultReady_t()
				{
					Handle = d.Handle,
					Esult = d.Esult,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<SteamInventoryResultReady_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( SteamInventoryResultReady_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct SteamInventoryFullUpdate_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientInventory + 1;
		public int Handle; // m_handle SteamInventoryResult_t
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static SteamInventoryFullUpdate_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (SteamInventoryFullUpdate_t) Marshal.PtrToStructure( p, typeof(SteamInventoryFullUpdate_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public int Handle; // m_handle SteamInventoryResult_t
			
			//
			// Easily convert from PackSmall to SteamInventoryFullUpdate_t
			//
			public static implicit operator SteamInventoryFullUpdate_t (  SteamInventoryFullUpdate_t.PackSmall d )
			{
				return new SteamInventoryFullUpdate_t()
				{
					Handle = d.Handle,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<SteamInventoryFullUpdate_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( SteamInventoryFullUpdate_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct BroadcastUploadStop_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientVideo + 5;
		public BroadcastUploadResult Result; // m_eResult enum EBroadcastUploadResult
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static BroadcastUploadStop_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (BroadcastUploadStop_t) Marshal.PtrToStructure( p, typeof(BroadcastUploadStop_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public BroadcastUploadResult Result; // m_eResult enum EBroadcastUploadResult
			
			//
			// Easily convert from PackSmall to BroadcastUploadStop_t
			//
			public static implicit operator BroadcastUploadStop_t (  BroadcastUploadStop_t.PackSmall d )
			{
				return new BroadcastUploadStop_t()
				{
					Result = d.Result,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<BroadcastUploadStop_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( BroadcastUploadStop_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct GetVideoURLResult_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientVideo + 11;
		public Result Result; // m_eResult enum EResult
		public uint VideoAppID; // m_unVideoAppID AppId_t
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string URL; // m_rgchURL char [256]
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GetVideoURLResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GetVideoURLResult_t) Marshal.PtrToStructure( p, typeof(GetVideoURLResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public uint VideoAppID; // m_unVideoAppID AppId_t
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string URL; // m_rgchURL char [256]
			
			//
			// Easily convert from PackSmall to GetVideoURLResult_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GetVideoURLResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GetVideoURLResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSClientApprove_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamGameServer + 1;
		public ulong SteamID; // m_SteamID class CSteamID
		public ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GSClientApprove_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GSClientApprove_t) Marshal.PtrToStructure( p, typeof(GSClientApprove_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamID; // m_SteamID class CSteamID
			public ulong OwnerSteamID; // m_OwnerSteamID class CSteamID
			
			//
			// Easily convert from PackSmall to GSClientApprove_t
			//
			public static implicit operator GSClientApprove_t (  GSClientApprove_t.PackSmall d )
			{
				return new GSClientApprove_t()
				{
					SteamID = d.SteamID,
					OwnerSteamID = d.OwnerSteamID,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GSClientApprove_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GSClientApprove_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSClientDeny_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamGameServer + 2;
		public ulong SteamID; // m_SteamID class CSteamID
		public DenyReason DenyReason; // m_eDenyReason enum EDenyReason
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string OptionalText; // m_rgchOptionalText char [128]
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GSClientDeny_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GSClientDeny_t) Marshal.PtrToStructure( p, typeof(GSClientDeny_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamID; // m_SteamID class CSteamID
			public DenyReason DenyReason; // m_eDenyReason enum EDenyReason
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string OptionalText; // m_rgchOptionalText char [128]
			
			//
			// Easily convert from PackSmall to GSClientDeny_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GSClientDeny_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GSClientDeny_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSClientKick_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamGameServer + 3;
		public ulong SteamID; // m_SteamID class CSteamID
		public DenyReason DenyReason; // m_eDenyReason enum EDenyReason
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GSClientKick_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GSClientKick_t) Marshal.PtrToStructure( p, typeof(GSClientKick_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamID; // m_SteamID class CSteamID
			public DenyReason DenyReason; // m_eDenyReason enum EDenyReason
			
			//
			// Easily convert from PackSmall to GSClientKick_t
			//
			public static implicit operator GSClientKick_t (  GSClientKick_t.PackSmall d )
			{
				return new GSClientKick_t()
				{
					SteamID = d.SteamID,
					DenyReason = d.DenyReason,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GSClientKick_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GSClientKick_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct GSClientAchievementStatus_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamGameServer + 6;
		public ulong SteamID; // m_SteamID uint64
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string PchAchievement; // m_pchAchievement char [128]
		[MarshalAs(UnmanagedType.I1)]
		public bool Unlocked; // m_bUnlocked _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GSClientAchievementStatus_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GSClientAchievementStatus_t) Marshal.PtrToStructure( p, typeof(GSClientAchievementStatus_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamID; // m_SteamID uint64
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string PchAchievement; // m_pchAchievement char [128]
			[MarshalAs(UnmanagedType.I1)]
			public bool Unlocked; // m_bUnlocked _Bool
			
			//
			// Easily convert from PackSmall to GSClientAchievementStatus_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GSClientAchievementStatus_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GSClientAchievementStatus_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct GSPolicyResponse_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUser + 15;
		public byte Secure; // m_bSecure uint8
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GSPolicyResponse_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GSPolicyResponse_t) Marshal.PtrToStructure( p, typeof(GSPolicyResponse_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public byte Secure; // m_bSecure uint8
			
			//
			// Easily convert from PackSmall to GSPolicyResponse_t
			//
			public static implicit operator GSPolicyResponse_t (  GSPolicyResponse_t.PackSmall d )
			{
				return new GSPolicyResponse_t()
				{
					Secure = d.Secure,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GSPolicyResponse_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GSPolicyResponse_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct GSGameplayStats_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamGameServer + 7;
		public Result Result; // m_eResult enum EResult
		public int Rank; // m_nRank int32
		public uint TotalConnects; // m_unTotalConnects uint32
		public uint TotalMinutesPlayed; // m_unTotalMinutesPlayed uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GSGameplayStats_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GSGameplayStats_t) Marshal.PtrToStructure( p, typeof(GSGameplayStats_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public int Rank; // m_nRank int32
			public uint TotalConnects; // m_unTotalConnects uint32
			public uint TotalMinutesPlayed; // m_unTotalMinutesPlayed uint32
			
			//
			// Easily convert from PackSmall to GSGameplayStats_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GSGameplayStats_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GSGameplayStats_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSClientGroupStatus_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamGameServer + 8;
		public ulong SteamIDUser; // m_SteamIDUser class CSteamID
		public ulong SteamIDGroup; // m_SteamIDGroup class CSteamID
		[MarshalAs(UnmanagedType.I1)]
		public bool Member; // m_bMember _Bool
		[MarshalAs(UnmanagedType.I1)]
		public bool Officer; // m_bOfficer _Bool
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GSClientGroupStatus_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GSClientGroupStatus_t) Marshal.PtrToStructure( p, typeof(GSClientGroupStatus_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDUser; // m_SteamIDUser class CSteamID
			public ulong SteamIDGroup; // m_SteamIDGroup class CSteamID
			[MarshalAs(UnmanagedType.I1)]
			public bool Member; // m_bMember _Bool
			[MarshalAs(UnmanagedType.I1)]
			public bool Officer; // m_bOfficer _Bool
			
			//
			// Easily convert from PackSmall to GSClientGroupStatus_t
			//
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
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GSClientGroupStatus_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GSClientGroupStatus_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct GSReputation_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamGameServer + 9;
		public Result Result; // m_eResult enum EResult
		public uint ReputationScore; // m_unReputationScore uint32
		[MarshalAs(UnmanagedType.I1)]
		public bool Banned; // m_bBanned _Bool
		public uint BannedIP; // m_unBannedIP uint32
		public ushort BannedPort; // m_usBannedPort uint16
		public ulong BannedGameID; // m_ulBannedGameID uint64
		public uint BanExpires; // m_unBanExpires uint32
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GSReputation_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GSReputation_t) Marshal.PtrToStructure( p, typeof(GSReputation_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public uint ReputationScore; // m_unReputationScore uint32
			[MarshalAs(UnmanagedType.I1)]
			public bool Banned; // m_bBanned _Bool
			public uint BannedIP; // m_unBannedIP uint32
			public ushort BannedPort; // m_usBannedPort uint16
			public ulong BannedGameID; // m_ulBannedGameID uint64
			public uint BanExpires; // m_unBanExpires uint32
			
			//
			// Easily convert from PackSmall to GSReputation_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<GSReputation_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GSReputation_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct AssociateWithClanResult_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamGameServer + 10;
		public Result Result; // m_eResult enum EResult
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static AssociateWithClanResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (AssociateWithClanResult_t) Marshal.PtrToStructure( p, typeof(AssociateWithClanResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			
			//
			// Easily convert from PackSmall to AssociateWithClanResult_t
			//
			public static implicit operator AssociateWithClanResult_t (  AssociateWithClanResult_t.PackSmall d )
			{
				return new AssociateWithClanResult_t()
				{
					Result = d.Result,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<AssociateWithClanResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( AssociateWithClanResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct ComputeNewPlayerCompatibilityResult_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamGameServer + 11;
		public Result Result; // m_eResult enum EResult
		public int CPlayersThatDontLikeCandidate; // m_cPlayersThatDontLikeCandidate int
		public int CPlayersThatCandidateDoesntLike; // m_cPlayersThatCandidateDoesntLike int
		public int CClanPlayersThatDontLikeCandidate; // m_cClanPlayersThatDontLikeCandidate int
		public ulong SteamIDCandidate; // m_SteamIDCandidate class CSteamID
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static ComputeNewPlayerCompatibilityResult_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (ComputeNewPlayerCompatibilityResult_t) Marshal.PtrToStructure( p, typeof(ComputeNewPlayerCompatibilityResult_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public int CPlayersThatDontLikeCandidate; // m_cPlayersThatDontLikeCandidate int
			public int CPlayersThatCandidateDoesntLike; // m_cPlayersThatCandidateDoesntLike int
			public int CClanPlayersThatDontLikeCandidate; // m_cClanPlayersThatDontLikeCandidate int
			public ulong SteamIDCandidate; // m_SteamIDCandidate class CSteamID
			
			//
			// Easily convert from PackSmall to ComputeNewPlayerCompatibilityResult_t
			//
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
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<ComputeNewPlayerCompatibilityResult_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( ComputeNewPlayerCompatibilityResult_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSStatsReceived_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamGameServerStats + 0;
		public Result Result; // m_eResult enum EResult
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GSStatsReceived_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GSStatsReceived_t) Marshal.PtrToStructure( p, typeof(GSStatsReceived_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			//
			// Easily convert from PackSmall to GSStatsReceived_t
			//
			public static implicit operator GSStatsReceived_t (  GSStatsReceived_t.PackSmall d )
			{
				return new GSStatsReceived_t()
				{
					Result = d.Result,
					SteamIDUser = d.SteamIDUser,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<GSStatsReceived_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GSStatsReceived_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSStatsStored_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamGameServerStats + 1;
		public Result Result; // m_eResult enum EResult
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GSStatsStored_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GSStatsStored_t) Marshal.PtrToStructure( p, typeof(GSStatsStored_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public Result Result; // m_eResult enum EResult
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			//
			// Easily convert from PackSmall to GSStatsStored_t
			//
			public static implicit operator GSStatsStored_t (  GSStatsStored_t.PackSmall d )
			{
				return new GSStatsStored_t()
				{
					Result = d.Result,
					SteamIDUser = d.SteamIDUser,
				};
			}
		}
		
		public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<GSStatsStored_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			handle.CallResultHandle = call;
			handle.CallResult = true;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => {  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => 
			{
				if ( hSteamAPICall != call ) return;
				
				handle.CallResultHandle = 0;
				handle.Dispose();
				
				CallbackFunction( FromPointer( p ), bIOFailure );
			};
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GSStatsStored_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );
			
			return handle;
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct GSStatsUnloaded_t
	{
		public const int CallbackId = CallbackIdentifiers.SteamUserStats + 8;
		public ulong SteamIDUser; // m_steamIDUser class CSteamID
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static GSStatsUnloaded_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (GSStatsUnloaded_t) Marshal.PtrToStructure( p, typeof(GSStatsUnloaded_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public ulong SteamIDUser; // m_steamIDUser class CSteamID
			
			//
			// Easily convert from PackSmall to GSStatsUnloaded_t
			//
			public static implicit operator GSStatsUnloaded_t (  GSStatsUnloaded_t.PackSmall d )
			{
				return new GSStatsUnloaded_t()
				{
					SteamIDUser = d.SteamIDUser,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<GSStatsUnloaded_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( GSStatsUnloaded_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
	[StructLayout( LayoutKind.Sequential, Pack = 8 )]
	internal struct ItemInstalled_t
	{
		public const int CallbackId = CallbackIdentifiers.ClientUGC + 5;
		public uint AppID; // m_unAppID AppId_t
		public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
		
		//
		// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff.
		//
		public static ItemInstalled_t FromPointer( IntPtr p )
		{
			if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );
			return (ItemInstalled_t) Marshal.PtrToStructure( p, typeof(ItemInstalled_t) );
		}
		
		[StructLayout( LayoutKind.Sequential, Pack = 4 )]
		internal struct PackSmall
		{
			public uint AppID; // m_unAppID AppId_t
			public ulong PublishedFileId; // m_nPublishedFileId PublishedFileId_t
			
			//
			// Easily convert from PackSmall to ItemInstalled_t
			//
			public static implicit operator ItemInstalled_t (  ItemInstalled_t.PackSmall d )
			{
				return new ItemInstalled_t()
				{
					AppID = d.AppID,
					PublishedFileId = d.PublishedFileId,
				};
			}
		}
		
		public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<ItemInstalled_t, bool> CallbackFunction )
		{
			var handle = new CallbackHandle();
			handle.steamworks = steamworks;
			
			//
			// Create the functions we need for the vtable
			//
			Callback.Result         funcA = ( _, p ) => { CallbackFunction( FromPointer( p ), false ); };
			Callback.ResultWithInfo funcB = ( _, p, bIOFailure, hSteamAPICall ) => { CallbackFunction( FromPointer( p ), bIOFailure ); };
			Callback.GetSize        funcC = ( _ ) => { return Marshal.SizeOf( typeof( ItemInstalled_t ) ); };
			
			//
			// If this platform is PackSmall, use PackSmall versions of everything instead
			//
			if ( Platform.PackSmall )
			{
				funcC = ( _ ) => { return Marshal.SizeOf( typeof( PackSmall ) ); };
			}
			
			//
			// Allocate a handle to each function, so they don't get disposed
			//
			handle.FuncA = GCHandle.Alloc( funcA );
			handle.FuncB = GCHandle.Alloc( funcB );
			handle.FuncC = GCHandle.Alloc( funcC );
			
			//
			// Create the VTable by manually allocating the memory and copying across
			//
			handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
			var vTable = new Callback.VTable()
			{
				ResultA = Marshal.GetFunctionPointerForDelegate( funcA ),
				ResultB = Marshal.GetFunctionPointerForDelegate( funcB ),
				GetSize = Marshal.GetFunctionPointerForDelegate( funcC ),
			};
			//
			// The order of these functions are swapped on Windows
			//
			if ( Platform.IsWindows )
			{
				vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );
				vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );
			}
			Marshal.StructureToPtr( vTable, handle.vTablePtr, false );
			
			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = handle.vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;
			cb.CallbackId = CallbackId;
			
			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );
			
			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );
			
			steamworks.RegisterCallbackHandle( handle );
		}
	}
	
}
