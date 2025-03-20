using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe partial class ISteamScreenshots : SteamInterface
	{
		public const string Version = "STEAMSCREENSHOTS_INTERFACE_VERSION003";
		
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
		private static extern ScreenshotHandle _AddScreenshotToLibrary( IntPtr self, IntPtr pchFilename, IntPtr pchThumbnailFilename, int nWidth, int nHeight );
		
		#endregion
		internal ScreenshotHandle AddScreenshotToLibrary( string pchFilename, string pchThumbnailFilename, int nWidth, int nHeight )
		{
			using var str__pchFilename = new Utf8StringToNative( pchFilename );
			using var str__pchThumbnailFilename = new Utf8StringToNative( pchThumbnailFilename );
			var returnValue = _AddScreenshotToLibrary( Self, str__pchFilename.Pointer, str__pchThumbnailFilename.Pointer, nWidth, nHeight );
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
		private static extern bool _SetLocation( IntPtr self, ScreenshotHandle hScreenshot, IntPtr pchLocation );
		
		#endregion
		internal bool SetLocation( ScreenshotHandle hScreenshot, string pchLocation )
		{
			using var str__pchLocation = new Utf8StringToNative( pchLocation );
			var returnValue = _SetLocation( Self, hScreenshot, str__pchLocation.Pointer );
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
		private static extern ScreenshotHandle _AddVRScreenshotToLibrary( IntPtr self, VRScreenshotType eType, IntPtr pchFilename, IntPtr pchVRFilename );
		
		#endregion
		internal ScreenshotHandle AddVRScreenshotToLibrary( VRScreenshotType eType, string pchFilename, string pchVRFilename )
		{
			using var str__pchFilename = new Utf8StringToNative( pchFilename );
			using var str__pchVRFilename = new Utf8StringToNative( pchVRFilename );
			var returnValue = _AddVRScreenshotToLibrary( Self, eType, str__pchFilename.Pointer, str__pchVRFilename.Pointer );
			return returnValue;
		}
		
	}
}
