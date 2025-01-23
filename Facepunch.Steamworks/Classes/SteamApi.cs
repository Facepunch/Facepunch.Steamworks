using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal static class SteamAPI
	{
		internal static class Native
		{
			[DllImport( Platform.LibraryName, EntryPoint = "SteamInternal_SteamAPI_Init", CallingConvention = CallingConvention.Cdecl )]
			public static extern SteamAPIInitResult SteamInternal_SteamAPI_Init( IntPtr pszInternalCheckInterfaceVersions, IntPtr pOutErrMsg );

			[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_Shutdown", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_Shutdown();
						
			[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_GetHSteamPipe", CallingConvention = CallingConvention.Cdecl )]
			public static extern HSteamPipe SteamAPI_GetHSteamPipe();
			
			[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_RestartAppIfNecessary", CallingConvention = CallingConvention.Cdecl )]
			[return: MarshalAs( UnmanagedType.I1 )]
			public static extern bool SteamAPI_RestartAppIfNecessary( uint unOwnAppID );
			
		}
		
		static internal SteamAPIInitResult Init( string pszInternalCheckInterfaceVersions, out string pOutErrMsg )
		{
			// Marshal the interface versions string in-place rather than relying on a custom marshaller.
			// Fixes occasional Steam init error in Unity IL2CPP builds caused by use-after-free.
			byte[] bytes = Encoding.UTF8.GetBytes( pszInternalCheckInterfaceVersions );
			var versionsPtr = Marshal.AllocHGlobal( bytes.Length + 1 );
			Marshal.Copy( bytes, 0, versionsPtr, bytes.Length );
			Marshal.WriteByte( versionsPtr, bytes.Length, 0 );

			using var buffer = Helpers.Memory.Take();
			var result = Native.SteamInternal_SteamAPI_Init( versionsPtr, buffer.Ptr );
			pOutErrMsg = Helpers.MemoryToString( buffer.Ptr );

			Marshal.FreeHGlobal( versionsPtr );
			return result;
		}
		
		static internal void Shutdown()
		{
			Native.SteamAPI_Shutdown();
		}
				
		static internal HSteamPipe GetHSteamPipe()
		{
			return Native.SteamAPI_GetHSteamPipe();
		}
		
		static internal bool RestartAppIfNecessary( uint unOwnAppID )
		{
			return Native.SteamAPI_RestartAppIfNecessary( unOwnAppID );
		}
		
	}
}
