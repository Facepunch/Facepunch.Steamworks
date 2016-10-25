using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamHTMLSurface
	{
		internal IntPtr _ptr;
		
		public SteamHTMLSurface( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// void
		public void AddHeader( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, string pchKey /*const char **/, string pchValue /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.AddHeader( _ptr, unBrowserHandle, pchKey, pchValue );
			else Platform.Win64.ISteamHTMLSurface.AddHeader( _ptr, unBrowserHandle, pchKey, pchValue );
		}
		
		// void
		public void AllowStartRequest( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, bool bAllowed /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.AllowStartRequest( _ptr, unBrowserHandle, bAllowed );
			else Platform.Win64.ISteamHTMLSurface.AllowStartRequest( _ptr, unBrowserHandle, bAllowed );
		}
		
		// void
		public void CopyToClipboard( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.CopyToClipboard( _ptr, unBrowserHandle );
			else Platform.Win64.ISteamHTMLSurface.CopyToClipboard( _ptr, unBrowserHandle );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t CreateBrowser( string pchUserAgent /*const char **/, string pchUserCSS /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTMLSurface.CreateBrowser( _ptr, pchUserAgent, pchUserCSS );
			else return Platform.Win64.ISteamHTMLSurface.CreateBrowser( _ptr, pchUserAgent, pchUserCSS );
		}
		
		// void
		public void DestructISteamHTMLSurface()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.DestructISteamHTMLSurface( _ptr );
			else Platform.Win64.ISteamHTMLSurface.DestructISteamHTMLSurface( _ptr );
		}
		
		// void
		public void ExecuteJavascript( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, string pchScript /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.ExecuteJavascript( _ptr, unBrowserHandle, pchScript );
			else Platform.Win64.ISteamHTMLSurface.ExecuteJavascript( _ptr, unBrowserHandle, pchScript );
		}
		
		// void
		public void Find( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, string pchSearchStr /*const char **/, bool bCurrentlyInFind /*bool*/, bool bReverse /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.Find( _ptr, unBrowserHandle, pchSearchStr, bCurrentlyInFind, bReverse );
			else Platform.Win64.ISteamHTMLSurface.Find( _ptr, unBrowserHandle, pchSearchStr, bCurrentlyInFind, bReverse );
		}
		
		// void
		public void GetLinkAtPosition( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, int x /*int*/, int y /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.GetLinkAtPosition( _ptr, unBrowserHandle, x, y );
			else Platform.Win64.ISteamHTMLSurface.GetLinkAtPosition( _ptr, unBrowserHandle, x, y );
		}
		
		// void
		public void GoBack( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.GoBack( _ptr, unBrowserHandle );
			else Platform.Win64.ISteamHTMLSurface.GoBack( _ptr, unBrowserHandle );
		}
		
		// void
		public void GoForward( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.GoForward( _ptr, unBrowserHandle );
			else Platform.Win64.ISteamHTMLSurface.GoForward( _ptr, unBrowserHandle );
		}
		
		// bool
		public bool Init()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTMLSurface.Init( _ptr );
			else return Platform.Win64.ISteamHTMLSurface.Init( _ptr );
		}
		
		// void
		public void JSDialogResponse( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, bool bResult /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.JSDialogResponse( _ptr, unBrowserHandle, bResult );
			else Platform.Win64.ISteamHTMLSurface.JSDialogResponse( _ptr, unBrowserHandle, bResult );
		}
		
		// void
		public void KeyChar( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, uint cUnicodeChar /*uint32*/, HTMLKeyModifiers eHTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.KeyChar( _ptr, unBrowserHandle, cUnicodeChar, eHTMLKeyModifiers );
			else Platform.Win64.ISteamHTMLSurface.KeyChar( _ptr, unBrowserHandle, cUnicodeChar, eHTMLKeyModifiers );
		}
		
		// void
		public void KeyDown( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, uint nNativeKeyCode /*uint32*/, HTMLKeyModifiers eHTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.KeyDown( _ptr, unBrowserHandle, nNativeKeyCode, eHTMLKeyModifiers );
			else Platform.Win64.ISteamHTMLSurface.KeyDown( _ptr, unBrowserHandle, nNativeKeyCode, eHTMLKeyModifiers );
		}
		
		// void
		public void KeyUp( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, uint nNativeKeyCode /*uint32*/, HTMLKeyModifiers eHTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.KeyUp( _ptr, unBrowserHandle, nNativeKeyCode, eHTMLKeyModifiers );
			else Platform.Win64.ISteamHTMLSurface.KeyUp( _ptr, unBrowserHandle, nNativeKeyCode, eHTMLKeyModifiers );
		}
		
		// void
		public void LoadURL( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, string pchURL /*const char **/, string pchPostData /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.LoadURL( _ptr, unBrowserHandle, pchURL, pchPostData );
			else Platform.Win64.ISteamHTMLSurface.LoadURL( _ptr, unBrowserHandle, pchURL, pchPostData );
		}
		
		// void
		public void MouseDoubleClick( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, HTMLMouseButton eMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.MouseDoubleClick( _ptr, unBrowserHandle, eMouseButton );
			else Platform.Win64.ISteamHTMLSurface.MouseDoubleClick( _ptr, unBrowserHandle, eMouseButton );
		}
		
		// void
		public void MouseDown( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, HTMLMouseButton eMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.MouseDown( _ptr, unBrowserHandle, eMouseButton );
			else Platform.Win64.ISteamHTMLSurface.MouseDown( _ptr, unBrowserHandle, eMouseButton );
		}
		
		// void
		public void MouseMove( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, int x /*int*/, int y /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.MouseMove( _ptr, unBrowserHandle, x, y );
			else Platform.Win64.ISteamHTMLSurface.MouseMove( _ptr, unBrowserHandle, x, y );
		}
		
		// void
		public void MouseUp( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, HTMLMouseButton eMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.MouseUp( _ptr, unBrowserHandle, eMouseButton );
			else Platform.Win64.ISteamHTMLSurface.MouseUp( _ptr, unBrowserHandle, eMouseButton );
		}
		
		// void
		public void MouseWheel( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, int nDelta /*int32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.MouseWheel( _ptr, unBrowserHandle, nDelta );
			else Platform.Win64.ISteamHTMLSurface.MouseWheel( _ptr, unBrowserHandle, nDelta );
		}
		
		// void
		public void PasteFromClipboard( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.PasteFromClipboard( _ptr, unBrowserHandle );
			else Platform.Win64.ISteamHTMLSurface.PasteFromClipboard( _ptr, unBrowserHandle );
		}
		
		// void
		public void Reload( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.Reload( _ptr, unBrowserHandle );
			else Platform.Win64.ISteamHTMLSurface.Reload( _ptr, unBrowserHandle );
		}
		
		// void
		public void RemoveBrowser( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.RemoveBrowser( _ptr, unBrowserHandle );
			else Platform.Win64.ISteamHTMLSurface.RemoveBrowser( _ptr, unBrowserHandle );
		}
		
		// void
		public void SetBackgroundMode( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, bool bBackgroundMode /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.SetBackgroundMode( _ptr, unBrowserHandle, bBackgroundMode );
			else Platform.Win64.ISteamHTMLSurface.SetBackgroundMode( _ptr, unBrowserHandle, bBackgroundMode );
		}
		
		// void
		public void SetCookie( string pchHostname /*const char **/, string pchKey /*const char **/, string pchValue /*const char **/, string pchPath /*const char **/, RTime32 nExpires /*RTime32*/, bool bSecure /*bool*/, bool bHTTPOnly /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.SetCookie( _ptr, pchHostname, pchKey, pchValue, pchPath, nExpires, bSecure, bHTTPOnly );
			else Platform.Win64.ISteamHTMLSurface.SetCookie( _ptr, pchHostname, pchKey, pchValue, pchPath, nExpires, bSecure, bHTTPOnly );
		}
		
		// void
		public void SetHorizontalScroll( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, uint nAbsolutePixelScroll /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.SetHorizontalScroll( _ptr, unBrowserHandle, nAbsolutePixelScroll );
			else Platform.Win64.ISteamHTMLSurface.SetHorizontalScroll( _ptr, unBrowserHandle, nAbsolutePixelScroll );
		}
		
		// void
		public void SetKeyFocus( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, bool bHasKeyFocus /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.SetKeyFocus( _ptr, unBrowserHandle, bHasKeyFocus );
			else Platform.Win64.ISteamHTMLSurface.SetKeyFocus( _ptr, unBrowserHandle, bHasKeyFocus );
		}
		
		// void
		public void SetPageScaleFactor( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, float flZoom /*float*/, int nPointX /*int*/, int nPointY /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.SetPageScaleFactor( _ptr, unBrowserHandle, flZoom, nPointX, nPointY );
			else Platform.Win64.ISteamHTMLSurface.SetPageScaleFactor( _ptr, unBrowserHandle, flZoom, nPointX, nPointY );
		}
		
		// void
		public void SetSize( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, uint unWidth /*uint32*/, uint unHeight /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.SetSize( _ptr, unBrowserHandle, unWidth, unHeight );
			else Platform.Win64.ISteamHTMLSurface.SetSize( _ptr, unBrowserHandle, unWidth, unHeight );
		}
		
		// void
		public void SetVerticalScroll( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/, uint nAbsolutePixelScroll /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.SetVerticalScroll( _ptr, unBrowserHandle, nAbsolutePixelScroll );
			else Platform.Win64.ISteamHTMLSurface.SetVerticalScroll( _ptr, unBrowserHandle, nAbsolutePixelScroll );
		}
		
		// bool
		public bool Shutdown()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTMLSurface.Shutdown( _ptr );
			else return Platform.Win64.ISteamHTMLSurface.Shutdown( _ptr );
		}
		
		// void
		public void StopFind( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.StopFind( _ptr, unBrowserHandle );
			else Platform.Win64.ISteamHTMLSurface.StopFind( _ptr, unBrowserHandle );
		}
		
		// void
		public void StopLoad( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.StopLoad( _ptr, unBrowserHandle );
			else Platform.Win64.ISteamHTMLSurface.StopLoad( _ptr, unBrowserHandle );
		}
		
		// void
		public void ViewSource( HHTMLBrowser unBrowserHandle /*HHTMLBrowser*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamHTMLSurface.ViewSource( _ptr, unBrowserHandle );
			else Platform.Win64.ISteamHTMLSurface.ViewSource( _ptr, unBrowserHandle );
		}
		
	}
}
