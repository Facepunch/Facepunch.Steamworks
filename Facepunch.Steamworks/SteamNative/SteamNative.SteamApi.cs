using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamApi
	{
		internal Platform.Interface _pi;
		
		public SteamApi( IntPtr pointer )
		{
			if ( Platform.IsWindows64 ) _pi = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) _pi = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) _pi = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) _pi = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) _pi = new Platform.Mac( pointer );
		}
		
		public bool IsValid{ get{ return _pi != null && _pi.IsValid; } }
		
		// HSteamPipe
		public HSteamPipe SteamAPI_GetHSteamPipe()
		{
			return _pi.SteamApi_SteamAPI_GetHSteamPipe();
		}
		
		// HSteamUser
		public HSteamUser SteamAPI_GetHSteamUser()
		{
			return _pi.SteamApi_SteamAPI_GetHSteamUser();
		}
		
		// void
		public void SteamAPI_Init()
		{
			_pi.SteamApi_SteamAPI_Init();
		}
		
		// void
		public void SteamAPI_RegisterCallback( IntPtr pCallback /*void **/, int callback /*int*/ )
		{
			_pi.SteamApi_SteamAPI_RegisterCallback( (IntPtr) pCallback, callback );
		}
		
		// void
		public void SteamAPI_RunCallbacks()
		{
			_pi.SteamApi_SteamAPI_RunCallbacks();
		}
		
		// void
		public void SteamAPI_Shutdown()
		{
			_pi.SteamApi_SteamAPI_Shutdown();
		}
		
		// void
		public void SteamAPI_UnregisterCallback( IntPtr pCallback /*void **/ )
		{
			_pi.SteamApi_SteamAPI_UnregisterCallback( (IntPtr) pCallback );
		}
		
		// HSteamPipe
		public HSteamPipe SteamGameServer_GetHSteamPipe()
		{
			return _pi.SteamApi_SteamGameServer_GetHSteamPipe();
		}
		
		// HSteamUser
		public HSteamUser SteamGameServer_GetHSteamUser()
		{
			return _pi.SteamApi_SteamGameServer_GetHSteamUser();
		}
		
		// void
		public void SteamGameServer_RunCallbacks()
		{
			_pi.SteamApi_SteamGameServer_RunCallbacks();
		}
		
		// IntPtr
		public IntPtr SteamInternal_CreateInterface( string version /*const char **/ )
		{
			return _pi.SteamApi_SteamInternal_CreateInterface( version );
		}
		
		// bool
		public bool SteamInternal_GameServer_Init( uint unIP /*uint32*/, ushort usPort /*uint16*/, ushort usGamePort /*uint16*/, ushort usQueryPort /*uint16*/, int eServerMode /*int*/, string pchVersionString /*const char **/ )
		{
			return _pi.SteamApi_SteamInternal_GameServer_Init( unIP, usPort, usGamePort, usQueryPort, eServerMode, pchVersionString );
		}
		
	}
}
