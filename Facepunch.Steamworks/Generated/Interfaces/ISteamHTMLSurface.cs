using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamHTMLSurface : SteamInterface
	{
		
		internal ISteamHTMLSurface( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamHTMLSurface_v005")]
		internal static extern IntPtr SteamAPI_SteamHTMLSurface_v005();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamHTMLSurface_v005();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_Init")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _Init( IntPtr self );
		
		#endregion
		internal bool Init()
		{
			var returnValue = _Init( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_Shutdown")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _Shutdown( IntPtr self );
		
		#endregion
		internal bool Shutdown()
		{
			var returnValue = _Shutdown( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_CreateBrowser")]
		private static extern SteamAPICall_t _CreateBrowser( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchUserAgent, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchUserCSS );
		
		#endregion
		internal CallbackResult<HTML_BrowserReady_t> CreateBrowser( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchUserAgent, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchUserCSS )
		{
			var returnValue = _CreateBrowser( Self, pchUserAgent, pchUserCSS );
			return new CallbackResult<HTML_BrowserReady_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_RemoveBrowser")]
		private static extern void _RemoveBrowser( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void RemoveBrowser( HHTMLBrowser unBrowserHandle )
		{
			_RemoveBrowser( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_LoadURL")]
		private static extern void _LoadURL( IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchURL, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPostData );
		
		#endregion
		internal void LoadURL( HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchURL, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPostData )
		{
			_LoadURL( Self, unBrowserHandle, pchURL, pchPostData );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetSize")]
		private static extern void _SetSize( IntPtr self, HHTMLBrowser unBrowserHandle, uint unWidth, uint unHeight );
		
		#endregion
		internal void SetSize( HHTMLBrowser unBrowserHandle, uint unWidth, uint unHeight )
		{
			_SetSize( Self, unBrowserHandle, unWidth, unHeight );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_StopLoad")]
		private static extern void _StopLoad( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void StopLoad( HHTMLBrowser unBrowserHandle )
		{
			_StopLoad( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_Reload")]
		private static extern void _Reload( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void Reload( HHTMLBrowser unBrowserHandle )
		{
			_Reload( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_GoBack")]
		private static extern void _GoBack( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void GoBack( HHTMLBrowser unBrowserHandle )
		{
			_GoBack( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_GoForward")]
		private static extern void _GoForward( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void GoForward( HHTMLBrowser unBrowserHandle )
		{
			_GoForward( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_AddHeader")]
		private static extern void _AddHeader( IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValue );
		
		#endregion
		internal void AddHeader( HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValue )
		{
			_AddHeader( Self, unBrowserHandle, pchKey, pchValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_ExecuteJavascript")]
		private static extern void _ExecuteJavascript( IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchScript );
		
		#endregion
		internal void ExecuteJavascript( HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchScript )
		{
			_ExecuteJavascript( Self, unBrowserHandle, pchScript );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseUp")]
		private static extern void _MouseUp( IntPtr self, HHTMLBrowser unBrowserHandle, IntPtr eMouseButton );
		
		#endregion
		internal void MouseUp( HHTMLBrowser unBrowserHandle, IntPtr eMouseButton )
		{
			_MouseUp( Self, unBrowserHandle, eMouseButton );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseDown")]
		private static extern void _MouseDown( IntPtr self, HHTMLBrowser unBrowserHandle, IntPtr eMouseButton );
		
		#endregion
		internal void MouseDown( HHTMLBrowser unBrowserHandle, IntPtr eMouseButton )
		{
			_MouseDown( Self, unBrowserHandle, eMouseButton );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseDoubleClick")]
		private static extern void _MouseDoubleClick( IntPtr self, HHTMLBrowser unBrowserHandle, IntPtr eMouseButton );
		
		#endregion
		internal void MouseDoubleClick( HHTMLBrowser unBrowserHandle, IntPtr eMouseButton )
		{
			_MouseDoubleClick( Self, unBrowserHandle, eMouseButton );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseMove")]
		private static extern void _MouseMove( IntPtr self, HHTMLBrowser unBrowserHandle, int x, int y );
		
		#endregion
		internal void MouseMove( HHTMLBrowser unBrowserHandle, int x, int y )
		{
			_MouseMove( Self, unBrowserHandle, x, y );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseWheel")]
		private static extern void _MouseWheel( IntPtr self, HHTMLBrowser unBrowserHandle, int nDelta );
		
		#endregion
		internal void MouseWheel( HHTMLBrowser unBrowserHandle, int nDelta )
		{
			_MouseWheel( Self, unBrowserHandle, nDelta );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_KeyDown")]
		private static extern void _KeyDown( IntPtr self, HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, IntPtr eHTMLKeyModifiers, [MarshalAs( UnmanagedType.U1 )] bool bIsSystemKey );
		
		#endregion
		internal void KeyDown( HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, IntPtr eHTMLKeyModifiers, [MarshalAs( UnmanagedType.U1 )] bool bIsSystemKey )
		{
			_KeyDown( Self, unBrowserHandle, nNativeKeyCode, eHTMLKeyModifiers, bIsSystemKey );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_KeyUp")]
		private static extern void _KeyUp( IntPtr self, HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, IntPtr eHTMLKeyModifiers );
		
		#endregion
		internal void KeyUp( HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, IntPtr eHTMLKeyModifiers )
		{
			_KeyUp( Self, unBrowserHandle, nNativeKeyCode, eHTMLKeyModifiers );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_KeyChar")]
		private static extern void _KeyChar( IntPtr self, HHTMLBrowser unBrowserHandle, uint cUnicodeChar, IntPtr eHTMLKeyModifiers );
		
		#endregion
		internal void KeyChar( HHTMLBrowser unBrowserHandle, uint cUnicodeChar, IntPtr eHTMLKeyModifiers )
		{
			_KeyChar( Self, unBrowserHandle, cUnicodeChar, eHTMLKeyModifiers );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetHorizontalScroll")]
		private static extern void _SetHorizontalScroll( IntPtr self, HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll );
		
		#endregion
		internal void SetHorizontalScroll( HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll )
		{
			_SetHorizontalScroll( Self, unBrowserHandle, nAbsolutePixelScroll );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetVerticalScroll")]
		private static extern void _SetVerticalScroll( IntPtr self, HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll );
		
		#endregion
		internal void SetVerticalScroll( HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll )
		{
			_SetVerticalScroll( Self, unBrowserHandle, nAbsolutePixelScroll );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetKeyFocus")]
		private static extern void _SetKeyFocus( IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.U1 )] bool bHasKeyFocus );
		
		#endregion
		internal void SetKeyFocus( HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.U1 )] bool bHasKeyFocus )
		{
			_SetKeyFocus( Self, unBrowserHandle, bHasKeyFocus );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_ViewSource")]
		private static extern void _ViewSource( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void ViewSource( HHTMLBrowser unBrowserHandle )
		{
			_ViewSource( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_CopyToClipboard")]
		private static extern void _CopyToClipboard( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void CopyToClipboard( HHTMLBrowser unBrowserHandle )
		{
			_CopyToClipboard( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_PasteFromClipboard")]
		private static extern void _PasteFromClipboard( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void PasteFromClipboard( HHTMLBrowser unBrowserHandle )
		{
			_PasteFromClipboard( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_Find")]
		private static extern void _Find( IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchSearchStr, [MarshalAs( UnmanagedType.U1 )] bool bCurrentlyInFind, [MarshalAs( UnmanagedType.U1 )] bool bReverse );
		
		#endregion
		internal void Find( HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchSearchStr, [MarshalAs( UnmanagedType.U1 )] bool bCurrentlyInFind, [MarshalAs( UnmanagedType.U1 )] bool bReverse )
		{
			_Find( Self, unBrowserHandle, pchSearchStr, bCurrentlyInFind, bReverse );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_StopFind")]
		private static extern void _StopFind( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void StopFind( HHTMLBrowser unBrowserHandle )
		{
			_StopFind( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_GetLinkAtPosition")]
		private static extern void _GetLinkAtPosition( IntPtr self, HHTMLBrowser unBrowserHandle, int x, int y );
		
		#endregion
		internal void GetLinkAtPosition( HHTMLBrowser unBrowserHandle, int x, int y )
		{
			_GetLinkAtPosition( Self, unBrowserHandle, x, y );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetCookie")]
		private static extern void _SetCookie( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchHostname, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValue, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPath, RTime32 nExpires, [MarshalAs( UnmanagedType.U1 )] bool bSecure, [MarshalAs( UnmanagedType.U1 )] bool bHTTPOnly );
		
		#endregion
		internal void SetCookie( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchHostname, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValue, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPath, RTime32 nExpires, [MarshalAs( UnmanagedType.U1 )] bool bSecure, [MarshalAs( UnmanagedType.U1 )] bool bHTTPOnly )
		{
			_SetCookie( Self, pchHostname, pchKey, pchValue, pchPath, nExpires, bSecure, bHTTPOnly );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetPageScaleFactor")]
		private static extern void _SetPageScaleFactor( IntPtr self, HHTMLBrowser unBrowserHandle, float flZoom, int nPointX, int nPointY );
		
		#endregion
		internal void SetPageScaleFactor( HHTMLBrowser unBrowserHandle, float flZoom, int nPointX, int nPointY )
		{
			_SetPageScaleFactor( Self, unBrowserHandle, flZoom, nPointX, nPointY );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetBackgroundMode")]
		private static extern void _SetBackgroundMode( IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.U1 )] bool bBackgroundMode );
		
		#endregion
		internal void SetBackgroundMode( HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.U1 )] bool bBackgroundMode )
		{
			_SetBackgroundMode( Self, unBrowserHandle, bBackgroundMode );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetDPIScalingFactor")]
		private static extern void _SetDPIScalingFactor( IntPtr self, HHTMLBrowser unBrowserHandle, float flDPIScaling );
		
		#endregion
		internal void SetDPIScalingFactor( HHTMLBrowser unBrowserHandle, float flDPIScaling )
		{
			_SetDPIScalingFactor( Self, unBrowserHandle, flDPIScaling );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_OpenDeveloperTools")]
		private static extern void _OpenDeveloperTools( IntPtr self, HHTMLBrowser unBrowserHandle );
		
		#endregion
		internal void OpenDeveloperTools( HHTMLBrowser unBrowserHandle )
		{
			_OpenDeveloperTools( Self, unBrowserHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_AllowStartRequest")]
		private static extern void _AllowStartRequest( IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.U1 )] bool bAllowed );
		
		#endregion
		internal void AllowStartRequest( HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.U1 )] bool bAllowed )
		{
			_AllowStartRequest( Self, unBrowserHandle, bAllowed );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_JSDialogResponse")]
		private static extern void _JSDialogResponse( IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.U1 )] bool bResult );
		
		#endregion
		internal void JSDialogResponse( HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.U1 )] bool bResult )
		{
			_JSDialogResponse( Self, unBrowserHandle, bResult );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTMLSurface_FileLoadDialogResponse")]
		private static extern void _FileLoadDialogResponse( IntPtr self, HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchSelectedFiles );
		
		#endregion
		internal void FileLoadDialogResponse( HHTMLBrowser unBrowserHandle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchSelectedFiles )
		{
			_FileLoadDialogResponse( Self, unBrowserHandle, pchSelectedFiles );
		}
		
	}
}
