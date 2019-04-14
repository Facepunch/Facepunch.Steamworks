using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


public static class SteamApi
{
	[DllImport( "Steam_api64", EntryPoint = "SteamAPI_Init", CallingConvention = CallingConvention.Cdecl )]
	[return: MarshalAs( UnmanagedType.U1 )]
	public static extern bool Init();

	[DllImport( "Steam_api64", EntryPoint = "SteamAPI_GetHSteamUser", CallingConvention = CallingConvention.Cdecl )]
	public static extern int GetHSteamUser();

	[DllImport( "Steam_api64", EntryPoint = "SteamAPI_RunCallbacks", CallingConvention = CallingConvention.Cdecl )]
	public static extern int RunCallbacks();

	[DllImport( "Steam_api64", EntryPoint = "SteamAPI_RegisterCallback", CallingConvention = CallingConvention.Cdecl )]
	public static extern int RegisterCallback( IntPtr pCallback, int callback );

	[DllImport( "Steam_api64", EntryPoint = "SteamAPI_UnregisterCallback", CallingConvention = CallingConvention.Cdecl )]
	public static extern int UnregisterCallback( IntPtr pCallback );

}
