using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamScreenshots : SteamInterface
	{
		
		internal ISteamScreenshots( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamScreenshots_v003", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamScreenshots_v003();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamScreenshots_v003();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamScreenshots_WriteScreenshot", CallingConvention = Platform.CC)]
		private static extern ScreenshotHandle _WriteScreenshot( IntPtr self, IntPtr pubRGB, uint cubRGB, int nWidth, int nHeight );
		
		#endregion
		internal ScreenshotHandle WriteScreenshot( IntPtr pubRGB, uint cubRGB, int nWidth, int nHeight )
		{
			var returnValue = _WriteScreenshot( Self, pubRGB, cubRGB, nWidth, nHeight );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamScreenshots_AddScreenshotToLibrary", CallingConvention = Platform.CC)]
		private static extern ScreenshotHandle _AddScreenshotToLibrary( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFilename, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchThumbnailFilename, int nWidth, int nHeight );
		
		#endregion
		internal ScreenshotHandle AddScreenshotToLibrary( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFilename, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchThumbnailFilename, int nWidth, int nHeight )
		{
			var returnValue = _AddScreenshotToLibrary( Self, pchFilename, pchThumbnailFilename, nWidth, nHeight );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamScreenshots_TriggerScreenshot", CallingConvention = Platform.CC)]
		private static extern void _TriggerScreenshot( IntPtr self );
		
		#endregion
		internal void TriggerScreenshot()
		{
			_TriggerScreenshot( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamScreenshots_HookScreenshots", CallingConvention = Platform.CC)]
		private static extern void _HookScreenshots( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bHook );
		
		#endregion
		internal void HookScreenshots( [MarshalAs( UnmanagedType.U1 )] bool bHook )
		{
			_HookScreenshots( Self, bHook );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamScreenshots_SetLocation", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetLocation( IntPtr self, ScreenshotHandle hScreenshot, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLocation );
		
		#endregion
		internal bool SetLocation( ScreenshotHandle hScreenshot, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLocation )
		{
			var returnValue = _SetLocation( Self, hScreenshot, pchLocation );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamScreenshots_TagUser", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _TagUser( IntPtr self, ScreenshotHandle hScreenshot, SteamId steamID );
		
		#endregion
		internal bool TagUser( ScreenshotHandle hScreenshot, SteamId steamID )
		{
			var returnValue = _TagUser( Self, hScreenshot, steamID );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamScreenshots_TagPublishedFile", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _TagPublishedFile( IntPtr self, ScreenshotHandle hScreenshot, PublishedFileId unPublishedFileID );
		
		#endregion
		internal bool TagPublishedFile( ScreenshotHandle hScreenshot, PublishedFileId unPublishedFileID )
		{
			var returnValue = _TagPublishedFile( Self, hScreenshot, unPublishedFileID );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamScreenshots_IsScreenshotsHooked", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsScreenshotsHooked( IntPtr self );
		
		#endregion
		internal bool IsScreenshotsHooked()
		{
			var returnValue = _IsScreenshotsHooked( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamScreenshots_AddVRScreenshotToLibrary", CallingConvention = Platform.CC)]
		private static extern ScreenshotHandle _AddVRScreenshotToLibrary( IntPtr self, VRScreenshotType eType, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFilename, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchVRFilename );
		
		#endregion
		internal ScreenshotHandle AddVRScreenshotToLibrary( VRScreenshotType eType, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFilename, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchVRFilename )
		{
			var returnValue = _AddVRScreenshotToLibrary( Self, eType, pchFilename, pchVRFilename );
			return returnValue;
		}
		
	}
}
