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
			_WriteScreenshot = Marshal.GetDelegateForFunctionPointer<FWriteScreenshot>( Marshal.ReadIntPtr( VTable, 0) );
			_AddScreenshotToLibrary = Marshal.GetDelegateForFunctionPointer<FAddScreenshotToLibrary>( Marshal.ReadIntPtr( VTable, 8) );
			_TriggerScreenshot = Marshal.GetDelegateForFunctionPointer<FTriggerScreenshot>( Marshal.ReadIntPtr( VTable, 16) );
			_HookScreenshots = Marshal.GetDelegateForFunctionPointer<FHookScreenshots>( Marshal.ReadIntPtr( VTable, 24) );
			_SetLocation = Marshal.GetDelegateForFunctionPointer<FSetLocation>( Marshal.ReadIntPtr( VTable, 32) );
			_TagUser = Marshal.GetDelegateForFunctionPointer<FTagUser>( Marshal.ReadIntPtr( VTable, 40) );
			_TagPublishedFile = Marshal.GetDelegateForFunctionPointer<FTagPublishedFile>( Marshal.ReadIntPtr( VTable, 48) );
			_IsScreenshotsHooked = Marshal.GetDelegateForFunctionPointer<FIsScreenshotsHooked>( Marshal.ReadIntPtr( VTable, 56) );
			_AddVRScreenshotToLibrary = Marshal.GetDelegateForFunctionPointer<FAddVRScreenshotToLibrary>( Marshal.ReadIntPtr( VTable, 64) );
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
			_WriteScreenshot = null;
			_AddScreenshotToLibrary = null;
			_TriggerScreenshot = null;
			_HookScreenshots = null;
			_SetLocation = null;
			_TagUser = null;
			_TagPublishedFile = null;
			_IsScreenshotsHooked = null;
			_AddVRScreenshotToLibrary = null;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate ScreenshotHandle FWriteScreenshot( IntPtr self, IntPtr pubRGB, uint cubRGB, int nWidth, int nHeight );
		private FWriteScreenshot _WriteScreenshot;
		
		#endregion
		internal ScreenshotHandle WriteScreenshot( IntPtr pubRGB, uint cubRGB, int nWidth, int nHeight )
		{
			return _WriteScreenshot( Self, pubRGB, cubRGB, nWidth, nHeight );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate ScreenshotHandle FAddScreenshotToLibrary( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string pchFilename, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string pchThumbnailFilename, int nWidth, int nHeight );
		private FAddScreenshotToLibrary _AddScreenshotToLibrary;
		
		#endregion
		internal ScreenshotHandle AddScreenshotToLibrary( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string pchFilename, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string pchThumbnailFilename, int nWidth, int nHeight )
		{
			return _AddScreenshotToLibrary( Self, pchFilename, pchThumbnailFilename, nWidth, nHeight );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FTriggerScreenshot( IntPtr self );
		private FTriggerScreenshot _TriggerScreenshot;
		
		#endregion
		internal void TriggerScreenshot()
		{
			_TriggerScreenshot( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FHookScreenshots( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bHook );
		private FHookScreenshots _HookScreenshots;
		
		#endregion
		internal void HookScreenshots( [MarshalAs( UnmanagedType.U1 )] bool bHook )
		{
			_HookScreenshots( Self, bHook );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetLocation( IntPtr self, ScreenshotHandle hScreenshot, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string pchLocation );
		private FSetLocation _SetLocation;
		
		#endregion
		internal bool SetLocation( ScreenshotHandle hScreenshot, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string pchLocation )
		{
			return _SetLocation( Self, hScreenshot, pchLocation );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FTagUser( IntPtr self, ScreenshotHandle hScreenshot, SteamId steamID );
		private FTagUser _TagUser;
		
		#endregion
		internal bool TagUser( ScreenshotHandle hScreenshot, SteamId steamID )
		{
			return _TagUser( Self, hScreenshot, steamID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FTagPublishedFile( IntPtr self, ScreenshotHandle hScreenshot, PublishedFileId unPublishedFileID );
		private FTagPublishedFile _TagPublishedFile;
		
		#endregion
		internal bool TagPublishedFile( ScreenshotHandle hScreenshot, PublishedFileId unPublishedFileID )
		{
			return _TagPublishedFile( Self, hScreenshot, unPublishedFileID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsScreenshotsHooked( IntPtr self );
		private FIsScreenshotsHooked _IsScreenshotsHooked;
		
		#endregion
		internal bool IsScreenshotsHooked()
		{
			return _IsScreenshotsHooked( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate ScreenshotHandle FAddVRScreenshotToLibrary( IntPtr self, VRScreenshotType eType, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string pchFilename, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string pchVRFilename );
		private FAddVRScreenshotToLibrary _AddVRScreenshotToLibrary;
		
		#endregion
		internal ScreenshotHandle AddVRScreenshotToLibrary( VRScreenshotType eType, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string pchFilename, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string pchVRFilename )
		{
			return _AddVRScreenshotToLibrary( Self, eType, pchFilename, pchVRFilename );
		}
		
	}
}
