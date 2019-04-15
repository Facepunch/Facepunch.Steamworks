using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


public static class SteamApi
{
	[DllImport( "Steam_api64", EntryPoint = "SteamAPI_Init", CallingConvention = CallingConvention.Cdecl )]
	[return: MarshalAs( UnmanagedType.U1 )]
	public static extern bool SteamAPI_Init();

	[DllImport( "Steam_api64", EntryPoint = "SteamAPI_Shutdown", CallingConvention = CallingConvention.Cdecl )]
	[return: MarshalAs( UnmanagedType.U1 )]
	public static extern bool SteamAPI_Shutdown();

	[DllImport( "Steam_api64", EntryPoint = "SteamInternal_GameServer_Init", CallingConvention = CallingConvention.Cdecl )]
	[return: MarshalAs( UnmanagedType.U1 )]
	public static extern bool SteamInternal_GameServer_Init( uint unIP, ushort usSteamPort, ushort usGamePort, ushort usQueryPort, int eServerMode, string pchVersionString);

	[DllImport( "Steam_api64", EntryPoint = "SteamAPI_GetHSteamUser", CallingConvention = CallingConvention.Cdecl )]
	public static extern int GetHSteamUser();

	[DllImport( "Steam_api64", EntryPoint = "SteamGameServer_GetHSteamUser", CallingConvention = CallingConvention.Cdecl )]
	public static extern int SteamGameServer_GetHSteamUser();

	[DllImport( "Steam_api64", EntryPoint = "SteamAPI_RunCallbacks", CallingConvention = CallingConvention.Cdecl )]
	public static extern int SteamAPI_RunCallbacks();

	[DllImport( "Steam_api64", EntryPoint = "SteamAPI_RegisterCallback", CallingConvention = CallingConvention.Cdecl )]
	public static extern int RegisterCallback( IntPtr pCallback, int callback );

	[DllImport( "Steam_api64", EntryPoint = "SteamAPI_UnregisterCallback", CallingConvention = CallingConvention.Cdecl )]
	public static extern int UnregisterCallback( IntPtr pCallback );



}
