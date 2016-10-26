using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamApi : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamApi( IntPtr pointer )
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
			_pi.SteamApi_SteamAPI_RegisterCallback( (IntPtr) pCallback /*C*/, callback /*C*/ );
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
			_pi.SteamApi_SteamAPI_UnregisterCallback( (IntPtr) pCallback /*C*/ );
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
			return _pi.SteamApi_SteamInternal_CreateInterface( version /*C*/ );
		}
		
		// bool
		public bool SteamInternal_GameServer_Init( uint unIP /*uint32*/, ushort usPort /*uint16*/, ushort usGamePort /*uint16*/, ushort usQueryPort /*uint16*/, int eServerMode /*int*/, string pchVersionString /*const char **/ )
		{
			return _pi.SteamApi_SteamInternal_GameServer_Init( unIP /*C*/, usPort /*C*/, usGamePort /*C*/, usQueryPort /*C*/, eServerMode /*C*/, pchVersionString /*C*/ );
		}
		
	}
}
