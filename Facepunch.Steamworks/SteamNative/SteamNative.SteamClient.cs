using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamClient
	{
		internal IntPtr _ptr;
		
		public SteamClient( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// bool
		public bool BReleaseSteamPipe( HSteamPipe hSteamPipe /*HSteamPipe*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamClient.BReleaseSteamPipe( _ptr, hSteamPipe );
			else return Platform.Win64.ISteamClient.BReleaseSteamPipe( _ptr, hSteamPipe );
		}
		
		// bool
		public bool BShutdownIfAllPipesClosed()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamClient.BShutdownIfAllPipesClosed( _ptr );
			else return Platform.Win64.ISteamClient.BShutdownIfAllPipesClosed( _ptr );
		}
		
		// HSteamUser
		public HSteamUser ConnectToGlobalUser( HSteamPipe hSteamPipe /*HSteamPipe*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamClient.ConnectToGlobalUser( _ptr, hSteamPipe );
			else return Platform.Win64.ISteamClient.ConnectToGlobalUser( _ptr, hSteamPipe );
		}
		
		// HSteamUser
		public HSteamUser CreateLocalUser( out HSteamPipe phSteamPipe /*HSteamPipe **/, AccountType eAccountType /*EAccountType*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamClient.CreateLocalUser( _ptr, out phSteamPipe, eAccountType );
			else return Platform.Win64.ISteamClient.CreateLocalUser( _ptr, out phSteamPipe, eAccountType );
		}
		
		// HSteamPipe
		public HSteamPipe CreateSteamPipe()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamClient.CreateSteamPipe( _ptr );
			else return Platform.Win64.ISteamClient.CreateSteamPipe( _ptr );
		}
		
		// uint
		public uint GetIPCCallCount()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamClient.GetIPCCallCount( _ptr );
			else return Platform.Win64.ISteamClient.GetIPCCallCount( _ptr );
		}
		
		// ISteamAppList *
		public SteamAppList GetISteamAppList( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamAppList( _ptr, hSteamUser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamAppList( _ptr, hSteamUser, hSteamPipe, pchVersion );
			return new SteamAppList( interface_pointer );
		}
		
		// ISteamApps *
		public SteamApps GetISteamApps( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamApps( _ptr, hSteamUser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamApps( _ptr, hSteamUser, hSteamPipe, pchVersion );
			return new SteamApps( interface_pointer );
		}
		
		// ISteamController *
		public SteamController GetISteamController( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamController( _ptr, hSteamUser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamController( _ptr, hSteamUser, hSteamPipe, pchVersion );
			return new SteamController( interface_pointer );
		}
		
		// ISteamFriends *
		public SteamFriends GetISteamFriends( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamFriends( _ptr, hSteamUser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamFriends( _ptr, hSteamUser, hSteamPipe, pchVersion );
			return new SteamFriends( interface_pointer );
		}
		
		// ISteamGameServer *
		public SteamGameServer GetISteamGameServer( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamGameServer( _ptr, hSteamUser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamGameServer( _ptr, hSteamUser, hSteamPipe, pchVersion );
			return new SteamGameServer( interface_pointer );
		}
		
		// ISteamGameServerStats *
		public SteamGameServerStats GetISteamGameServerStats( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamGameServerStats( _ptr, hSteamuser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamGameServerStats( _ptr, hSteamuser, hSteamPipe, pchVersion );
			return new SteamGameServerStats( interface_pointer );
		}
		
		// IntPtr
		public IntPtr GetISteamGenericInterface( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamClient.GetISteamGenericInterface( _ptr, hSteamUser, hSteamPipe, pchVersion );
			else return Platform.Win64.ISteamClient.GetISteamGenericInterface( _ptr, hSteamUser, hSteamPipe, pchVersion );
		}
		
		// ISteamHTMLSurface *
		public SteamHTMLSurface GetISteamHTMLSurface( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamHTMLSurface( _ptr, hSteamuser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamHTMLSurface( _ptr, hSteamuser, hSteamPipe, pchVersion );
			return new SteamHTMLSurface( interface_pointer );
		}
		
		// ISteamHTTP *
		public SteamHTTP GetISteamHTTP( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamHTTP( _ptr, hSteamuser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamHTTP( _ptr, hSteamuser, hSteamPipe, pchVersion );
			return new SteamHTTP( interface_pointer );
		}
		
		// ISteamInventory *
		public SteamInventory GetISteamInventory( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamInventory( _ptr, hSteamuser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamInventory( _ptr, hSteamuser, hSteamPipe, pchVersion );
			return new SteamInventory( interface_pointer );
		}
		
		// ISteamMatchmaking *
		public SteamMatchmaking GetISteamMatchmaking( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamMatchmaking( _ptr, hSteamUser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamMatchmaking( _ptr, hSteamUser, hSteamPipe, pchVersion );
			return new SteamMatchmaking( interface_pointer );
		}
		
		// ISteamMatchmakingServers *
		public SteamMatchmakingServers GetISteamMatchmakingServers( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamMatchmakingServers( _ptr, hSteamUser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamMatchmakingServers( _ptr, hSteamUser, hSteamPipe, pchVersion );
			return new SteamMatchmakingServers( interface_pointer );
		}
		
		// ISteamMusic *
		public SteamMusic GetISteamMusic( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamMusic( _ptr, hSteamuser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamMusic( _ptr, hSteamuser, hSteamPipe, pchVersion );
			return new SteamMusic( interface_pointer );
		}
		
		// ISteamMusicRemote *
		public SteamMusicRemote GetISteamMusicRemote( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamMusicRemote( _ptr, hSteamuser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamMusicRemote( _ptr, hSteamuser, hSteamPipe, pchVersion );
			return new SteamMusicRemote( interface_pointer );
		}
		
		// ISteamNetworking *
		public SteamNetworking GetISteamNetworking( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamNetworking( _ptr, hSteamUser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamNetworking( _ptr, hSteamUser, hSteamPipe, pchVersion );
			return new SteamNetworking( interface_pointer );
		}
		
		// ISteamRemoteStorage *
		public SteamRemoteStorage GetISteamRemoteStorage( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamRemoteStorage( _ptr, hSteamuser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamRemoteStorage( _ptr, hSteamuser, hSteamPipe, pchVersion );
			return new SteamRemoteStorage( interface_pointer );
		}
		
		// ISteamScreenshots *
		public SteamScreenshots GetISteamScreenshots( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamScreenshots( _ptr, hSteamuser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamScreenshots( _ptr, hSteamuser, hSteamPipe, pchVersion );
			return new SteamScreenshots( interface_pointer );
		}
		
		// ISteamUGC *
		public SteamUGC GetISteamUGC( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamUGC( _ptr, hSteamUser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamUGC( _ptr, hSteamUser, hSteamPipe, pchVersion );
			return new SteamUGC( interface_pointer );
		}
		
		// ISteamUnifiedMessages *
		public SteamUnifiedMessages GetISteamUnifiedMessages( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamUnifiedMessages( _ptr, hSteamuser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamUnifiedMessages( _ptr, hSteamuser, hSteamPipe, pchVersion );
			return new SteamUnifiedMessages( interface_pointer );
		}
		
		// ISteamUser *
		public SteamUser GetISteamUser( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamUser( _ptr, hSteamUser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamUser( _ptr, hSteamUser, hSteamPipe, pchVersion );
			return new SteamUser( interface_pointer );
		}
		
		// ISteamUserStats *
		public SteamUserStats GetISteamUserStats( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamUserStats( _ptr, hSteamUser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamUserStats( _ptr, hSteamUser, hSteamPipe, pchVersion );
			return new SteamUserStats( interface_pointer );
		}
		
		// ISteamUtils *
		public SteamUtils GetISteamUtils( HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamUtils( _ptr, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamUtils( _ptr, hSteamPipe, pchVersion );
			return new SteamUtils( interface_pointer );
		}
		
		// ISteamVideo *
		public SteamVideo GetISteamVideo( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr interface_pointer;
			if ( Platform.IsWindows32 ) interface_pointer = Platform.Win32.ISteamClient.GetISteamVideo( _ptr, hSteamuser, hSteamPipe, pchVersion );
			else interface_pointer = Platform.Win64.ISteamClient.GetISteamVideo( _ptr, hSteamuser, hSteamPipe, pchVersion );
			return new SteamVideo( interface_pointer );
		}
		
		// void
		public void ReleaseUser( HSteamPipe hSteamPipe /*HSteamPipe*/, HSteamUser hUser /*HSteamUser*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamClient.ReleaseUser( _ptr, hSteamPipe, hUser );
			else Platform.Win64.ISteamClient.ReleaseUser( _ptr, hSteamPipe, hUser );
		}
		
		// void
		public void SetLocalIPBinding( uint unIP /*uint32*/, ushort usPort /*uint16*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamClient.SetLocalIPBinding( _ptr, unIP, usPort );
			else Platform.Win64.ISteamClient.SetLocalIPBinding( _ptr, unIP, usPort );
		}
		
		// void
		public void SetWarningMessageHook( IntPtr pFunction /*SteamAPIWarningMessageHook_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamClient.SetWarningMessageHook( _ptr, (IntPtr) pFunction );
			else Platform.Win64.ISteamClient.SetWarningMessageHook( _ptr, (IntPtr) pFunction );
		}
		
	}
}
