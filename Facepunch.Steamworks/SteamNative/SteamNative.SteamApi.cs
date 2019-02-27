using Facepunch.Steamworks;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SteamNative
{
	internal unsafe class SteamApi : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamApi()
		{
			
			if ( Platform.IsWindows64 ) platform = new Platform.Win64( ((IntPtr)1) );
			else if ( Platform.IsWindows32 ) platform = new Platform.Win32( ((IntPtr)1) );
			else if ( Platform.IsLinux32 ) platform = new Platform.Linux32( ((IntPtr)1) );
			else if ( Platform.IsLinux64 ) platform = new Platform.Linux64( ((IntPtr)1) );
			else if ( Platform.IsOsx ) platform = new Platform.Mac( ((IntPtr)1) );
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
		
		// HSteamPipe
		public HSteamPipe SteamAPI_GetHSteamPipe()
		{
			return platform.SteamApi_SteamAPI_GetHSteamPipe();
		}
		
		// HSteamUser
		public HSteamUser SteamAPI_GetHSteamUser()
		{
			return platform.SteamApi_SteamAPI_GetHSteamUser();
		}
		
		// bool
		public bool SteamAPI_Init()
		{
			return platform.SteamApi_SteamAPI_Init();
		}
		
		// void
		public void SteamAPI_RegisterCallback( IntPtr pCallback /*void **/, int callback /*int*/ )
		{
			platform.SteamApi_SteamAPI_RegisterCallback( (IntPtr) pCallback, callback );
		}
		
		// void
		public void SteamAPI_RegisterCallResult( IntPtr pCallback /*void **/, SteamAPICall_t callback /*SteamAPICall_t*/ )
		{
			platform.SteamApi_SteamAPI_RegisterCallResult( (IntPtr) pCallback, callback.Value );
		}
		
		// bool
		public bool SteamAPI_RestartAppIfNecessary( uint unOwnAppID /*uint32*/ )
		{
			return platform.SteamApi_SteamAPI_RestartAppIfNecessary( unOwnAppID );
		}
		
		// void
		public void SteamAPI_RunCallbacks()
		{
			platform.SteamApi_SteamAPI_RunCallbacks();
		}
		
		// void
		public void SteamAPI_Shutdown()
		{
			platform.SteamApi_SteamAPI_Shutdown();
		}
		
		// void
		public void SteamAPI_UnregisterCallback( IntPtr pCallback /*void **/ )
		{
			platform.SteamApi_SteamAPI_UnregisterCallback( (IntPtr) pCallback );
		}
		
		// void
		public void SteamAPI_UnregisterCallResult( IntPtr pCallback /*void **/, SteamAPICall_t callback /*SteamAPICall_t*/ )
		{
			platform.SteamApi_SteamAPI_UnregisterCallResult( (IntPtr) pCallback, callback.Value );
		}
		
		// HSteamPipe
		public HSteamPipe SteamGameServer_GetHSteamPipe()
		{
			return platform.SteamApi_SteamGameServer_GetHSteamPipe();
		}
		
		// HSteamUser
		public HSteamUser SteamGameServer_GetHSteamUser()
		{
			return platform.SteamApi_SteamGameServer_GetHSteamUser();
		}
		
		// void
		public void SteamGameServer_RunCallbacks()
		{
			platform.SteamApi_SteamGameServer_RunCallbacks();
		}
		
		// void
		public void SteamGameServer_Shutdown()
		{
			platform.SteamApi_SteamGameServer_Shutdown();
		}
		
		// IntPtr
		public IntPtr SteamInternal_CreateInterface( string version /*const char **/ )
		{
			return platform.SteamApi_SteamInternal_CreateInterface( Utility.GetUtf8Bytes(version) );
		}
		
		// bool
		public bool SteamInternal_GameServer_Init( uint unIP /*uint32*/, ushort usPort /*uint16*/, ushort usGamePort /*uint16*/, ushort usQueryPort /*uint16*/, int eServerMode /*int*/, string pchVersionString /*const char **/ )
		{
			return platform.SteamApi_SteamInternal_GameServer_Init( unIP, usPort, usGamePort, usQueryPort, eServerMode, Utility.GetUtf8Bytes(pchVersionString) );
		}
		
	}
}
