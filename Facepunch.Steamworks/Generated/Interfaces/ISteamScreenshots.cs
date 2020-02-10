using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamScreenshots : SteamInterface
	{
		public override string InterfaceName => "STEAMSCREENSHOTS_INTERFACE_VERSION003";
		
		public override void InitInternals()
		{
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate ScreenshotHandle FWriteScreenshot( IntPtr self, IntPtr pubRGB, uint cubRGB, int nWidth, int nHeight );
		private FWriteScreenshot _WriteScreenshot;
		
		#endregion
		internal ScreenshotHandle WriteScreenshot( IntPtr pubRGB, uint cubRGB, int nWidth, int nHeight )
		{
			var returnValue = _WriteScreenshot( Self, pubRGB, cubRGB, nWidth, nHeight );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate ScreenshotHandle FAddScreenshotToLibrary( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFilename, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchThumbnailFilename, int nWidth, int nHeight );
		private FAddScreenshotToLibrary _AddScreenshotToLibrary;
		
		#endregion
		internal ScreenshotHandle AddScreenshotToLibrary( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFilename, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchThumbnailFilename, int nWidth, int nHeight )
		{
			var returnValue = _AddScreenshotToLibrary( Self, pchFilename, pchThumbnailFilename, nWidth, nHeight );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FTriggerScreenshot( IntPtr self );
		private FTriggerScreenshot _TriggerScreenshot;
		
		#endregion
		internal void TriggerScreenshot()
		{
			_TriggerScreenshot( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FHookScreenshots( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bHook );
		private FHookScreenshots _HookScreenshots;
		
		#endregion
		internal void HookScreenshots( [MarshalAs( UnmanagedType.U1 )] bool bHook )
		{
			_HookScreenshots( Self, bHook );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetLocation( IntPtr self, ScreenshotHandle hScreenshot, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLocation );
		private FSetLocation _SetLocation;
		
		#endregion
		internal bool SetLocation( ScreenshotHandle hScreenshot, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLocation )
		{
			var returnValue = _SetLocation( Self, hScreenshot, pchLocation );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FTagUser( IntPtr self, ScreenshotHandle hScreenshot, SteamId steamID );
		private FTagUser _TagUser;
		
		#endregion
		internal bool TagUser( ScreenshotHandle hScreenshot, SteamId steamID )
		{
			var returnValue = _TagUser( Self, hScreenshot, steamID );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FTagPublishedFile( IntPtr self, ScreenshotHandle hScreenshot, PublishedFileId unPublishedFileID );
		private FTagPublishedFile _TagPublishedFile;
		
		#endregion
		internal bool TagPublishedFile( ScreenshotHandle hScreenshot, PublishedFileId unPublishedFileID )
		{
			var returnValue = _TagPublishedFile( Self, hScreenshot, unPublishedFileID );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsScreenshotsHooked( IntPtr self );
		private FIsScreenshotsHooked _IsScreenshotsHooked;
		
		#endregion
		internal bool IsScreenshotsHooked()
		{
			var returnValue = _IsScreenshotsHooked( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate ScreenshotHandle FAddVRScreenshotToLibrary( IntPtr self, VRScreenshotType eType, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFilename, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchVRFilename );
		private FAddVRScreenshotToLibrary _AddVRScreenshotToLibrary;
		
		#endregion
		internal ScreenshotHandle AddVRScreenshotToLibrary( VRScreenshotType eType, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFilename, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchVRFilename )
		{
			var returnValue = _AddVRScreenshotToLibrary( Self, eType, pchFilename, pchVRFilename );
			return returnValue;
		}
		
	}
}
