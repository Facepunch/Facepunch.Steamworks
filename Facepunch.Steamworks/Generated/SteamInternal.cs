using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


public static class SteamInternal
{
	[DllImport( "Steam_api64", EntryPoint = "SteamInternal_FindOrCreateUserInterface", CallingConvention = CallingConvention.Cdecl )]
	public static extern IntPtr FindOrCreateUserInterface( int hSteamUser, [MarshalAs( UnmanagedType.LPStr )] string pszVersion );
}
