using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace SteamNative
{
	internal unsafe class SteamHTMLSurface : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamHTMLSurface( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
		{
			this.steamworks = steamworks;
			
			if ( Platform.IsWindows64 ) platform = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) platform = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) platform = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) platform = new Platform.Linux64( pointer );
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
		
		// void
		public void AddHeader( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, string pchKey /*const char **/, string pchValue /*const char **/ )
		{
			platform.ISteamHTMLSurface_AddHeader( unBrowserHandle.Value, pchKey, pchValue );
		}
		
		// void
		public void AllowStartRequest( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, bool bAllowed /*bool*/ )
		{
			platform.ISteamHTMLSurface_AllowStartRequest( unBrowserHandle.Value, bAllowed );
		}
		
		// void
		public void CopyToClipboard( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			platform.ISteamHTMLSurface_CopyToClipboard( unBrowserHandle.Value );
		}
		
		// SteamAPICall_t
		public CallbackHandle CreateBrowser( string pchUserAgent /*const char **/, string pchUserCSS /*const char **/, Action<HTML_BrowserReady_t, bool> CallbackFunction = null /*Action<HTML_BrowserReady_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamHTMLSurface_CreateBrowser( pchUserAgent, pchUserCSS );
			
			if ( CallbackFunction == null ) return null;
			
			return HTML_BrowserReady_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// void
		public void DestructISteamHTMLSurface()
		{
			platform.ISteamHTMLSurface_DestructISteamHTMLSurface();
		}
		
		// void
		public void ExecuteJavascript( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, string pchScript /*const char **/ )
		{
			platform.ISteamHTMLSurface_ExecuteJavascript( unBrowserHandle.Value, pchScript );
		}
		
		// void
		public void Find( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, string pchSearchStr /*const char **/, bool bCurrentlyInFind /*bool*/, bool bReverse /*bool*/ )
		{
			platform.ISteamHTMLSurface_Find( unBrowserHandle.Value, pchSearchStr, bCurrentlyInFind, bReverse );
		}
		
		// void
		public void GetLinkAtPosition( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, int x /*int*/, int y /*int*/ )
		{
			platform.ISteamHTMLSurface_GetLinkAtPosition( unBrowserHandle.Value, x, y );
		}
		
		// void
		public void GoBack( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			platform.ISteamHTMLSurface_GoBack( unBrowserHandle.Value );
		}
		
		// void
		public void GoForward( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			platform.ISteamHTMLSurface_GoForward( unBrowserHandle.Value );
		}
		
		// bool
		public bool Init()
		{
			return platform.ISteamHTMLSurface_Init();
		}
		
		// void
		public void JSDialogResponse( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, bool bResult /*bool*/ )
		{
			platform.ISteamHTMLSurface_JSDialogResponse( unBrowserHandle.Value, bResult );
		}
		
		// void
		public void KeyChar( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, uint cUnicodeChar /*uint32*/, HTMLKeyModifiers eHTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ )
		{
			platform.ISteamHTMLSurface_KeyChar( unBrowserHandle.Value, cUnicodeChar, eHTMLKeyModifiers );
		}
		
		// void
		public void KeyDown( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, uint nNativeKeyCode /*uint32*/, HTMLKeyModifiers eHTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ )
		{
			platform.ISteamHTMLSurface_KeyDown( unBrowserHandle.Value, nNativeKeyCode, eHTMLKeyModifiers );
		}
		
		// void
		public void KeyUp( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, uint nNativeKeyCode /*uint32*/, HTMLKeyModifiers eHTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ )
		{
			platform.ISteamHTMLSurface_KeyUp( unBrowserHandle.Value, nNativeKeyCode, eHTMLKeyModifiers );
		}
		
		// void
		public void LoadURL( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, string pchURL /*const char **/, string pchPostData /*const char **/ )
		{
			platform.ISteamHTMLSurface_LoadURL( unBrowserHandle.Value, pchURL, pchPostData );
		}
		
		// void
		public void MouseDoubleClick( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, HTMLMouseButton eMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ )
		{
			platform.ISteamHTMLSurface_MouseDoubleClick( unBrowserHandle.Value, eMouseButton );
		}
		
		// void
		public void MouseDown( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, HTMLMouseButton eMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ )
		{
			platform.ISteamHTMLSurface_MouseDown( unBrowserHandle.Value, eMouseButton );
		}
		
		// void
		public void MouseMove( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, int x /*int*/, int y /*int*/ )
		{
			platform.ISteamHTMLSurface_MouseMove( unBrowserHandle.Value, x, y );
		}
		
		// void
		public void MouseUp( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, HTMLMouseButton eMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ )
		{
			platform.ISteamHTMLSurface_MouseUp( unBrowserHandle.Value, eMouseButton );
		}
		
		// void
		public void MouseWheel( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, int nDelta /*int32*/ )
		{
			platform.ISteamHTMLSurface_MouseWheel( unBrowserHandle.Value, nDelta );
		}
		
		// void
		public void PasteFromClipboard( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			platform.ISteamHTMLSurface_PasteFromClipboard( unBrowserHandle.Value );
		}
		
		// void
		public void Reload( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			platform.ISteamHTMLSurface_Reload( unBrowserHandle.Value );
		}
		
		// void
		public void RemoveBrowser( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			platform.ISteamHTMLSurface_RemoveBrowser( unBrowserHandle.Value );
		}
		
		// void
		public void SetBackgroundMode( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, bool bBackgroundMode /*bool*/ )
		{
			platform.ISteamHTMLSurface_SetBackgroundMode( unBrowserHandle.Value, bBackgroundMode );
		}
		
		// void
		public void SetCookie( string pchHostname /*const char **/, string pchKey /*const char **/, string pchValue /*const char **/, string pchPath /*const char **/, RTime32 nExpires /*RTime32*/, bool bSecure /*bool*/, bool bHTTPOnly /*bool*/ )
		{
			platform.ISteamHTMLSurface_SetCookie( pchHostname, pchKey, pchValue, pchPath, nExpires.Value, bSecure, bHTTPOnly );
		}
		
		// void
		public void SetDPIScalingFactor( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, float flDPIScaling /*float*/ )
		{
			platform.ISteamHTMLSurface_SetDPIScalingFactor( unBrowserHandle.Value, flDPIScaling );
		}
		
		// void
		public void SetHorizontalScroll( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, uint nAbsolutePixelScroll /*uint32*/ )
		{
			platform.ISteamHTMLSurface_SetHorizontalScroll( unBrowserHandle.Value, nAbsolutePixelScroll );
		}
		
		// void
		public void SetKeyFocus( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, bool bHasKeyFocus /*bool*/ )
		{
			platform.ISteamHTMLSurface_SetKeyFocus( unBrowserHandle.Value, bHasKeyFocus );
		}
		
		// void
		public void SetPageScaleFactor( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, float flZoom /*float*/, int nPointX /*int*/, int nPointY /*int*/ )
		{
			platform.ISteamHTMLSurface_SetPageScaleFactor( unBrowserHandle.Value, flZoom, nPointX, nPointY );
		}
		
		// void
		public void SetSize( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, uint unWidth /*uint32*/, uint unHeight /*uint32*/ )
		{
			platform.ISteamHTMLSurface_SetSize( unBrowserHandle.Value, unWidth, unHeight );
		}
		
		// void
		public void SetVerticalScroll( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, uint nAbsolutePixelScroll /*uint32*/ )
		{
			platform.ISteamHTMLSurface_SetVerticalScroll( unBrowserHandle.Value, nAbsolutePixelScroll );
		}
		
		// bool
		public bool Shutdown()
		{
			return platform.ISteamHTMLSurface_Shutdown();
		}
		
		// void
		public void StopFind( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			platform.ISteamHTMLSurface_StopFind( unBrowserHandle.Value );
		}
		
		// void
		public void StopLoad( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			platform.ISteamHTMLSurface_StopLoad( unBrowserHandle.Value );
		}
		
		// void
		public void ViewSource( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			platform.ISteamHTMLSurface_ViewSource( unBrowserHandle.Value );
		}
		
	}
}
