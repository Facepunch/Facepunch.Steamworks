using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class Globals
	{
		// void
		public static void SteamAPI_Init()
		{
			if ( Platform.IsWindows32 ) Platform.Win32.Global.SteamAPI_Init(  );
			else Platform.Win64.Global.SteamAPI_Init(  );
		}
		
		// void
		public static void SteamAPI_RunCallbacks()
		{
			if ( Platform.IsWindows32 ) Platform.Win32.Global.SteamAPI_RunCallbacks(  );
			else Platform.Win64.Global.SteamAPI_RunCallbacks(  );
		}
		
		// void
		public static void SteamGameServer_RunCallbacks()
		{
			if ( Platform.IsWindows32 ) Platform.Win32.Global.SteamGameServer_RunCallbacks(  );
			else Platform.Win64.Global.SteamGameServer_RunCallbacks(  );
		}
		
		// void
		public static void SteamAPI_RegisterCallback( IntPtr pCallback /*void **/, int callback /*int*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.Global.SteamAPI_RegisterCallback( (IntPtr) pCallback, callback );
			else Platform.Win64.Global.SteamAPI_RegisterCallback( (IntPtr) pCallback, callback );
		}
		
		// void
		public static void SteamAPI_UnregisterCallback( IntPtr pCallback /*void **/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.Global.SteamAPI_UnregisterCallback( (IntPtr) pCallback );
			else Platform.Win64.Global.SteamAPI_UnregisterCallback( (IntPtr) pCallback );
		}
		
		// bool
		public static bool SteamInternal_GameServer_Init( uint unIP /*uint32*/, ushort usPort /*uint16*/, ushort usGamePort /*uint16*/, ushort usQueryPort /*uint16*/, int eServerMode /*int*/, string pchVersionString /*const char **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.Global.SteamInternal_GameServer_Init( unIP, usPort, usGamePort, usQueryPort, eServerMode, pchVersionString );
			else return Platform.Win64.Global.SteamInternal_GameServer_Init( unIP, usPort, usGamePort, usQueryPort, eServerMode, pchVersionString );
		}
		
		// void
		public static void SteamAPI_Shutdown()
		{
			if ( Platform.IsWindows32 ) Platform.Win32.Global.SteamAPI_Shutdown(  );
			else Platform.Win64.Global.SteamAPI_Shutdown(  );
		}
		
		// HSteamUser
		public static HSteamUser SteamAPI_GetHSteamUser()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.Global.SteamAPI_GetHSteamUser(  );
			else return Platform.Win64.Global.SteamAPI_GetHSteamUser(  );
		}
		
		// HSteamPipe
		public static HSteamPipe SteamAPI_GetHSteamPipe()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.Global.SteamAPI_GetHSteamPipe(  );
			else return Platform.Win64.Global.SteamAPI_GetHSteamPipe(  );
		}
		
		// HSteamUser
		public static HSteamUser SteamGameServer_GetHSteamUser()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.Global.SteamGameServer_GetHSteamUser(  );
			else return Platform.Win64.Global.SteamGameServer_GetHSteamUser(  );
		}
		
		// HSteamPipe
		public static HSteamPipe SteamGameServer_GetHSteamPipe()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.Global.SteamGameServer_GetHSteamPipe(  );
			else return Platform.Win64.Global.SteamGameServer_GetHSteamPipe(  );
		}
		
		// IntPtr
		public static IntPtr SteamInternal_CreateInterface( string version /*const char **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.Global.SteamInternal_CreateInterface( version );
			else return Platform.Win64.Global.SteamInternal_CreateInterface( version );
		}
		
	}
}
