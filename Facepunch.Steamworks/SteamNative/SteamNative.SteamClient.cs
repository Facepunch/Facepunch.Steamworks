using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace SteamNative
{
	internal unsafe class SteamClient : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamClient( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
		{
			this.steamworks = steamworks;
			
			if ( Platform.IsWindows ) platform = new Platform.Windows( pointer );
			else if ( Platform.IsLinux ) platform = new Platform.Linux( pointer );
			else if ( Platform.IsOsx ) platform = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid{ get{ return platform != null && platform.IsValid; } }
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( platform != null )
			{
				platform.Dispose();
				platform = null;
			}
		}
		
		// bool
		public bool BReleaseSteamPipe( HSteamPipe hSteamPipe /*HSteamPipe*/ )
		{
			return platform.ISteamClient_BReleaseSteamPipe( hSteamPipe.Value );
		}
		
		// bool
		public bool BShutdownIfAllPipesClosed()
		{
			return platform.ISteamClient_BShutdownIfAllPipesClosed();
		}
		
		// HSteamUser
		public HSteamUser ConnectToGlobalUser( HSteamPipe hSteamPipe /*HSteamPipe*/ )
		{
			return platform.ISteamClient_ConnectToGlobalUser( hSteamPipe.Value );
		}
		
		// HSteamUser
		public HSteamUser CreateLocalUser( out HSteamPipe phSteamPipe /*HSteamPipe **/, AccountType eAccountType /*EAccountType*/ )
		{
			return platform.ISteamClient_CreateLocalUser( out phSteamPipe.Value, eAccountType );
		}
		
		// HSteamPipe
		public HSteamPipe CreateSteamPipe()
		{
			return platform.ISteamClient_CreateSteamPipe();
		}
		
		// uint
		public uint GetIPCCallCount()
		{
			return platform.ISteamClient_GetIPCCallCount();
		}
		
		// ISteamAppList *
		public SteamAppList GetISteamAppList( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamAppList( hSteamUser.Value, hSteamPipe.Value, pchVersion );
			return new SteamAppList( steamworks, interface_pointer );
		}
		
		// ISteamApps *
		public SteamApps GetISteamApps( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamApps( hSteamUser.Value, hSteamPipe.Value, pchVersion );
			return new SteamApps( steamworks, interface_pointer );
		}
		
		// ISteamController *
		public SteamController GetISteamController( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamController( hSteamUser.Value, hSteamPipe.Value, pchVersion );
			return new SteamController( steamworks, interface_pointer );
		}
		
		// ISteamFriends *
		public SteamFriends GetISteamFriends( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamFriends( hSteamUser.Value, hSteamPipe.Value, pchVersion );
			return new SteamFriends( steamworks, interface_pointer );
		}
		
		// ISteamGameServer *
		public SteamGameServer GetISteamGameServer( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamGameServer( hSteamUser.Value, hSteamPipe.Value, pchVersion );
			return new SteamGameServer( steamworks, interface_pointer );
		}
		
		// ISteamGameServerStats *
		public SteamGameServerStats GetISteamGameServerStats( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamGameServerStats( hSteamuser.Value, hSteamPipe.Value, pchVersion );
			return new SteamGameServerStats( steamworks, interface_pointer );
		}
		
		// IntPtr
		public IntPtr GetISteamGenericInterface( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			return platform.ISteamClient_GetISteamGenericInterface( hSteamUser.Value, hSteamPipe.Value, pchVersion );
		}
		
		// ISteamHTMLSurface *
		public SteamHTMLSurface GetISteamHTMLSurface( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamHTMLSurface( hSteamuser.Value, hSteamPipe.Value, pchVersion );
			return new SteamHTMLSurface( steamworks, interface_pointer );
		}
		
		// ISteamHTTP *
		public SteamHTTP GetISteamHTTP( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamHTTP( hSteamuser.Value, hSteamPipe.Value, pchVersion );
			return new SteamHTTP( steamworks, interface_pointer );
		}
		
		// ISteamInventory *
		public SteamInventory GetISteamInventory( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamInventory( hSteamuser.Value, hSteamPipe.Value, pchVersion );
			return new SteamInventory( steamworks, interface_pointer );
		}
		
		// ISteamMatchmaking *
		public SteamMatchmaking GetISteamMatchmaking( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamMatchmaking( hSteamUser.Value, hSteamPipe.Value, pchVersion );
			return new SteamMatchmaking( steamworks, interface_pointer );
		}
		
		// ISteamMatchmakingServers *
		public SteamMatchmakingServers GetISteamMatchmakingServers( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamMatchmakingServers( hSteamUser.Value, hSteamPipe.Value, pchVersion );
			return new SteamMatchmakingServers( steamworks, interface_pointer );
		}
		
		// ISteamMusic *
		public SteamMusic GetISteamMusic( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamMusic( hSteamuser.Value, hSteamPipe.Value, pchVersion );
			return new SteamMusic( steamworks, interface_pointer );
		}
		
		// ISteamMusicRemote *
		public SteamMusicRemote GetISteamMusicRemote( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamMusicRemote( hSteamuser.Value, hSteamPipe.Value, pchVersion );
			return new SteamMusicRemote( steamworks, interface_pointer );
		}
		
		// ISteamNetworking *
		public SteamNetworking GetISteamNetworking( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamNetworking( hSteamUser.Value, hSteamPipe.Value, pchVersion );
			return new SteamNetworking( steamworks, interface_pointer );
		}
		
		// ISteamParentalSettings *
		public SteamParentalSettings GetISteamParentalSettings( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamParentalSettings( hSteamuser.Value, hSteamPipe.Value, pchVersion );
			return new SteamParentalSettings( steamworks, interface_pointer );
		}
		
		// ISteamRemoteStorage *
		public SteamRemoteStorage GetISteamRemoteStorage( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamRemoteStorage( hSteamuser.Value, hSteamPipe.Value, pchVersion );
			return new SteamRemoteStorage( steamworks, interface_pointer );
		}
		
		// ISteamScreenshots *
		public SteamScreenshots GetISteamScreenshots( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamScreenshots( hSteamuser.Value, hSteamPipe.Value, pchVersion );
			return new SteamScreenshots( steamworks, interface_pointer );
		}
		
		// ISteamUGC *
		public SteamUGC GetISteamUGC( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamUGC( hSteamUser.Value, hSteamPipe.Value, pchVersion );
			return new SteamUGC( steamworks, interface_pointer );
		}
		
		// ISteamUser *
		public SteamUser GetISteamUser( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamUser( hSteamUser.Value, hSteamPipe.Value, pchVersion );
			return new SteamUser( steamworks, interface_pointer );
		}
		
		// ISteamUserStats *
		public SteamUserStats GetISteamUserStats( HSteamUser hSteamUser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamUserStats( hSteamUser.Value, hSteamPipe.Value, pchVersion );
			return new SteamUserStats( steamworks, interface_pointer );
		}
		
		// ISteamUtils *
		public SteamUtils GetISteamUtils( HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamUtils( hSteamPipe.Value, pchVersion );
			return new SteamUtils( steamworks, interface_pointer );
		}
		
		// ISteamVideo *
		public SteamVideo GetISteamVideo( HSteamUser hSteamuser /*HSteamUser*/, HSteamPipe hSteamPipe /*HSteamPipe*/, string pchVersion /*const char **/ )
		{
			IntPtr interface_pointer;
			interface_pointer = platform.ISteamClient_GetISteamVideo( hSteamuser.Value, hSteamPipe.Value, pchVersion );
			return new SteamVideo( steamworks, interface_pointer );
		}
		
		// void
		public void ReleaseUser( HSteamPipe hSteamPipe /*HSteamPipe*/, HSteamUser hUser /*HSteamUser*/ )
		{
			platform.ISteamClient_ReleaseUser( hSteamPipe.Value, hUser.Value );
		}
		
		// void
		public void SetLocalIPBinding( uint unIP /*uint32*/, ushort usPort /*uint16*/ )
		{
			platform.ISteamClient_SetLocalIPBinding( unIP, usPort );
		}
		
		// void
		public void SetWarningMessageHook( IntPtr pFunction /*SteamAPIWarningMessageHook_t*/ )
		{
			platform.ISteamClient_SetWarningMessageHook( (IntPtr) pFunction );
		}
		
	}
}
