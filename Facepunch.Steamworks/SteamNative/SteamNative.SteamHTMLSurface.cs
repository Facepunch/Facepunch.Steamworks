using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamHTMLSurface : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamHTMLSurface( IntPtr pointer )
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
		
		// void
		public void AddHeader( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, string pchKey /*const char **/, string pchValue /*const char **/ )
		{
			_pi.ISteamHTMLSurface_AddHeader( unBrowserHandle.Value, pchKey, pchValue );
		}
		
		// void
		public void AllowStartRequest( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, bool bAllowed /*bool*/ )
		{
			_pi.ISteamHTMLSurface_AllowStartRequest( unBrowserHandle.Value, bAllowed );
		}
		
		// void
		public void CopyToClipboard( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			_pi.ISteamHTMLSurface_CopyToClipboard( unBrowserHandle.Value );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t CreateBrowser( string pchUserAgent /*const char **/, string pchUserCSS /*const char **/ )
		{
			return _pi.ISteamHTMLSurface_CreateBrowser( pchUserAgent, pchUserCSS );
		}
		
		// void
		public void DestructISteamHTMLSurface()
		{
			_pi.ISteamHTMLSurface_DestructISteamHTMLSurface();
		}
		
		// void
		public void ExecuteJavascript( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, string pchScript /*const char **/ )
		{
			_pi.ISteamHTMLSurface_ExecuteJavascript( unBrowserHandle.Value, pchScript );
		}
		
		// void
		public void Find( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, string pchSearchStr /*const char **/, bool bCurrentlyInFind /*bool*/, bool bReverse /*bool*/ )
		{
			_pi.ISteamHTMLSurface_Find( unBrowserHandle.Value, pchSearchStr, bCurrentlyInFind, bReverse );
		}
		
		// void
		public void GetLinkAtPosition( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, int x /*int*/, int y /*int*/ )
		{
			_pi.ISteamHTMLSurface_GetLinkAtPosition( unBrowserHandle.Value, x, y );
		}
		
		// void
		public void GoBack( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			_pi.ISteamHTMLSurface_GoBack( unBrowserHandle.Value );
		}
		
		// void
		public void GoForward( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			_pi.ISteamHTMLSurface_GoForward( unBrowserHandle.Value );
		}
		
		// bool
		public bool Init()
		{
			return _pi.ISteamHTMLSurface_Init();
		}
		
		// void
		public void JSDialogResponse( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, bool bResult /*bool*/ )
		{
			_pi.ISteamHTMLSurface_JSDialogResponse( unBrowserHandle.Value, bResult );
		}
		
		// void
		public void KeyChar( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, uint cUnicodeChar /*uint32*/, HTMLKeyModifiers eHTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ )
		{
			_pi.ISteamHTMLSurface_KeyChar( unBrowserHandle.Value, cUnicodeChar, eHTMLKeyModifiers );
		}
		
		// void
		public void KeyDown( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, uint nNativeKeyCode /*uint32*/, HTMLKeyModifiers eHTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ )
		{
			_pi.ISteamHTMLSurface_KeyDown( unBrowserHandle.Value, nNativeKeyCode, eHTMLKeyModifiers );
		}
		
		// void
		public void KeyUp( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, uint nNativeKeyCode /*uint32*/, HTMLKeyModifiers eHTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ )
		{
			_pi.ISteamHTMLSurface_KeyUp( unBrowserHandle.Value, nNativeKeyCode, eHTMLKeyModifiers );
		}
		
		// void
		public void LoadURL( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, string pchURL /*const char **/, string pchPostData /*const char **/ )
		{
			_pi.ISteamHTMLSurface_LoadURL( unBrowserHandle.Value, pchURL, pchPostData );
		}
		
		// void
		public void MouseDoubleClick( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, HTMLMouseButton eMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ )
		{
			_pi.ISteamHTMLSurface_MouseDoubleClick( unBrowserHandle.Value, eMouseButton );
		}
		
		// void
		public void MouseDown( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, HTMLMouseButton eMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ )
		{
			_pi.ISteamHTMLSurface_MouseDown( unBrowserHandle.Value, eMouseButton );
		}
		
		// void
		public void MouseMove( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, int x /*int*/, int y /*int*/ )
		{
			_pi.ISteamHTMLSurface_MouseMove( unBrowserHandle.Value, x, y );
		}
		
		// void
		public void MouseUp( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, HTMLMouseButton eMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ )
		{
			_pi.ISteamHTMLSurface_MouseUp( unBrowserHandle.Value, eMouseButton );
		}
		
		// void
		public void MouseWheel( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, int nDelta /*int32*/ )
		{
			_pi.ISteamHTMLSurface_MouseWheel( unBrowserHandle.Value, nDelta );
		}
		
		// void
		public void PasteFromClipboard( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			_pi.ISteamHTMLSurface_PasteFromClipboard( unBrowserHandle.Value );
		}
		
		// void
		public void Reload( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			_pi.ISteamHTMLSurface_Reload( unBrowserHandle.Value );
		}
		
		// void
		public void RemoveBrowser( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			_pi.ISteamHTMLSurface_RemoveBrowser( unBrowserHandle.Value );
		}
		
		// void
		public void SetBackgroundMode( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, bool bBackgroundMode /*bool*/ )
		{
			_pi.ISteamHTMLSurface_SetBackgroundMode( unBrowserHandle.Value, bBackgroundMode );
		}
		
		// void
		public void SetCookie( string pchHostname /*const char **/, string pchKey /*const char **/, string pchValue /*const char **/, string pchPath /*const char **/, RTime32 nExpires /*RTime32*/, bool bSecure /*bool*/, bool bHTTPOnly /*bool*/ )
		{
			_pi.ISteamHTMLSurface_SetCookie( pchHostname, pchKey, pchValue, pchPath, nExpires.Value, bSecure, bHTTPOnly );
		}
		
		// void
		public void SetHorizontalScroll( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, uint nAbsolutePixelScroll /*uint32*/ )
		{
			_pi.ISteamHTMLSurface_SetHorizontalScroll( unBrowserHandle.Value, nAbsolutePixelScroll );
		}
		
		// void
		public void SetKeyFocus( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, bool bHasKeyFocus /*bool*/ )
		{
			_pi.ISteamHTMLSurface_SetKeyFocus( unBrowserHandle.Value, bHasKeyFocus );
		}
		
		// void
		public void SetPageScaleFactor( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, float flZoom /*float*/, int nPointX /*int*/, int nPointY /*int*/ )
		{
			_pi.ISteamHTMLSurface_SetPageScaleFactor( unBrowserHandle.Value, flZoom, nPointX, nPointY );
		}
		
		// void
		public void SetSize( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, uint unWidth /*uint32*/, uint unHeight /*uint32*/ )
		{
			_pi.ISteamHTMLSurface_SetSize( unBrowserHandle.Value, unWidth, unHeight );
		}
		
		// void
		public void SetVerticalScroll( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, uint nAbsolutePixelScroll /*uint32*/ )
		{
			_pi.ISteamHTMLSurface_SetVerticalScroll( unBrowserHandle.Value, nAbsolutePixelScroll );
		}
		
		// bool
		public bool Shutdown()
		{
			return _pi.ISteamHTMLSurface_Shutdown();
		}
		
		// void
		public void StopFind( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			_pi.ISteamHTMLSurface_StopFind( unBrowserHandle.Value );
		}
		
		// void
		public void StopLoad( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			_pi.ISteamHTMLSurface_StopLoad( unBrowserHandle.Value );
		}
		
		// void
		public void ViewSource( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			_pi.ISteamHTMLSurface_ViewSource( unBrowserHandle.Value );
		}
		
	}
}
