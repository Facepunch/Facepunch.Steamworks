using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamClient : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamClient( IntPtr pointer )
		{
			if ( Platform.IsWindows64 ) _pi = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) _pi = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) _pi = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) _pi = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) _pi = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid{ get{ return _pi != null && _pi.IsValid; } }
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( _pi != null )
			{
				_pi.Dispose();
				_pi = null;
			}
		}
		
		// bool
		public bool BReleaseSteamPipe( HSteamPipe hSteamPipe /*HSteamPipe*/ )
		{
			return _pi.ISteamClient_BReleaseSteamPipe( hSteamPipe.Value /*C*/ );
		}
		
		// bool
		public bool BShutdownIfAllPipesClosed()
		{
			return _pi.ISteamClient_BShutdownIfAllPipesClosed();
		}
		
		// HSteamUser
		public HSteamUser ConnectToGlobalUser( HSteamPipe hSteamPipe /*HSteamPipe*/ )
		{
			return _pi.ISteamClient_ConnectToGlobalUser( hSteamPipe.Value /*C*/ );
		}
		
		// HSteamUser
		public HSteamUser CreateLocalUser( out HSteamPipe phSteamPipe /*HSteamPipe **/, AccountType eAccountType /*EAccountType*/ )
		{
			return _pi.ISteamClient_CreateLocalUser( out phSteamPipe.Value /*B*/, eAccountType /*C*/ );
		}
		
		// HSteamPipe
		public HSteamPipe CreateSteamPipe()
		{
			return _pi.ISteamClient_CreateSteamPipe();
		}
		
		// uint
		public uint GetIPCCallCount()
		{
			return _pi.ISteamClient_GetIPCCallCount();
		}
		
		// ISteamAppList *
		public SteamAppList GetISteamAppList( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamAppList( hSteamUser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamAppList( interface_pointer );
		}
		
		// ISteamApps *
		public SteamApps GetISteamApps( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamApps( hSteamUser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamApps( interface_pointer );
		}
		
		// ISteamController *
		public SteamController GetISteamController( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamController( hSteamUser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamController( interface_pointer );
		}
		
		// ISteamFriends *
		public SteamFriends GetISteamFriends( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamFriends( hSteamUser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamFriends( interface_pointer );
		}
		
		// ISteamGameServer *
		public SteamGameServer GetISteamGameServer( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamGameServer( hSteamUser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamGameServer( interface_pointer );
		}
		
		// ISteamGameServerStats *
		public SteamGameServerStats GetISteamGameServerStats( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamGameServerStats( hSteamuser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamGameServerStats( interface_pointer );
		}
		
		// IntPtr
		public IntPtr GetISteamGenericInterface( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			return _pi.ISteamClient_GetISteamGenericInterface( hSteamUser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
		}
		
		// ISteamHTMLSurface *
		public SteamHTMLSurface GetISteamHTMLSurface( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamHTMLSurface( hSteamuser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamHTMLSurface( interface_pointer );
		}
		
		// ISteamHTTP *
		public SteamHTTP GetISteamHTTP( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamHTTP( hSteamuser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamHTTP( interface_pointer );
		}
		
		// ISteamInventory *
		public SteamInventory GetISteamInventory( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamInventory( hSteamuser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamInventory( interface_pointer );
		}
		
		// ISteamMatchmaking *
		public SteamMatchmaking GetISteamMatchmaking( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamMatchmaking( hSteamUser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamMatchmaking( interface_pointer );
		}
		
		// ISteamMatchmakingServers *
		public SteamMatchmakingServers GetISteamMatchmakingServers( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamMatchmakingServers( hSteamUser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamMatchmakingServers( interface_pointer );
		}
		
		// ISteamMusic *
		public SteamMusic GetISteamMusic( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamMusic( hSteamuser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamMusic( interface_pointer );
		}
		
		// ISteamMusicRemote *
		public SteamMusicRemote GetISteamMusicRemote( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamMusicRemote( hSteamuser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamMusicRemote( interface_pointer );
		}
		
		// ISteamNetworking *
		public SteamNetworking GetISteamNetworking( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamNetworking( hSteamUser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamNetworking( interface_pointer );
		}
		
		// ISteamRemoteStorage *
		public SteamRemoteStorage GetISteamRemoteStorage( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamRemoteStorage( hSteamuser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamRemoteStorage( interface_pointer );
		}
		
		// ISteamScreenshots *
		public SteamScreenshots GetISteamScreenshots( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamScreenshots( hSteamuser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamScreenshots( interface_pointer );
		}
		
		// ISteamUGC *
		public SteamUGC GetISteamUGC( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamUGC( hSteamUser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamUGC( interface_pointer );
		}
		
		// ISteamUnifiedMessages *
		public SteamUnifiedMessages GetISteamUnifiedMessages( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamUnifiedMessages( hSteamuser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamUnifiedMessages( interface_pointer );
		}
		
		// ISteamUser *
		public SteamUser GetISteamUser( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamUser( hSteamUser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamUser( interface_pointer );
		}
		
		// ISteamUserStats *
		public SteamUserStats GetISteamUserStats( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamUserStats( hSteamUser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamUserStats( interface_pointer );
		}
		
		// ISteamUtils *
		public SteamUtils GetISteamUtils( HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamUtils( hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamUtils( interface_pointer );
		}
		
		// ISteamVideo *
		public SteamVideo GetISteamVideo( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = _pi.ISteamClient_GetISteamVideo( hSteamuser.Value /*C*/, hSteamPipe.Value /*C*/, pchVersion /*C*/ );
			return new SteamVideo( interface_pointer );
		}
		
		// void
		public void ReleaseUser( HSteamPipe hSteamPipe /*HSteamPipe*/, HSteamUser hUser /*HSteamUser*/ )
		{
			_pi.ISteamClient_ReleaseUser( hSteamPipe.Value /*C*/, hUser.Value /*C*/ );
		}
		
		// void
		public void SetLocalIPBinding( uint unIP /*uint32*/, ushort usPort /*uint16*/ )
		{
			_pi.ISteamClient_SetLocalIPBinding( unIP /*C*/, usPort /*C*/ );
		}
		
		// void
		public void SetWarningMessageHook( IntPtr pFunction /*SteamAPIWarningMessageHook_t*/ )
		{
			_pi.ISteamClient_SetWarningMessageHook( (IntPtr) pFunction /*C*/ );
		}
		
	}
}
