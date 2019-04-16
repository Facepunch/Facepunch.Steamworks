using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamScreenshots : SteamInterface
	{
		public ISteamScreenshots( bool server = false ) : base( server )
		{
		}
		
		public override string InterfaceName => "STEAMSCREENSHOTS_INTERFACE_VERSION003";
		
		public override void InitInternals()
		{
			WriteScreenshotDelegatePointer = Marshal.GetDelegateForFunctionPointer<WriteScreenshotDelegate>( Marshal.ReadIntPtr( VTable, 0) );
			AddScreenshotToLibraryDelegatePointer = Marshal.GetDelegateForFunctionPointer<AddScreenshotToLibraryDelegate>( Marshal.ReadIntPtr( VTable, 8) );
			TriggerScreenshotDelegatePointer = Marshal.GetDelegateForFunctionPointer<TriggerScreenshotDelegate>( Marshal.ReadIntPtr( VTable, 16) );
			HookScreenshotsDelegatePointer = Marshal.GetDelegateForFunctionPointer<HookScreenshotsDelegate>( Marshal.ReadIntPtr( VTable, 24) );
			SetLocationDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetLocationDelegate>( Marshal.ReadIntPtr( VTable, 32) );
			TagUserDelegatePointer = Marshal.GetDelegateForFunctionPointer<TagUserDelegate>( Marshal.ReadIntPtr( VTable, 40) );
			TagPublishedFileDelegatePointer = Marshal.GetDelegateForFunctionPointer<TagPublishedFileDelegate>( Marshal.ReadIntPtr( VTable, 48) );
			IsScreenshotsHookedDelegatePointer = Marshal.GetDelegateForFunctionPointer<IsScreenshotsHookedDelegate>( Marshal.ReadIntPtr( VTable, 56) );
			AddVRScreenshotToLibraryDelegatePointer = Marshal.GetDelegateForFunctionPointer<AddVRScreenshotToLibraryDelegate>( Marshal.ReadIntPtr( VTable, 64) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate ScreenshotHandle WriteScreenshotDelegate( IntPtr self, IntPtr pubRGB, uint cubRGB, int nWidth, int nHeight );
		private WriteScreenshotDelegate WriteScreenshotDelegatePointer;
		
		#endregion
		internal ScreenshotHandle WriteScreenshot( IntPtr pubRGB, uint cubRGB, int nWidth, int nHeight )
		{
			return WriteScreenshotDelegatePointer( Self, pubRGB, cubRGB, nWidth, nHeight );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate ScreenshotHandle AddScreenshotToLibraryDelegate( IntPtr self, string pchFilename, string pchThumbnailFilename, int nWidth, int nHeight );
		private AddScreenshotToLibraryDelegate AddScreenshotToLibraryDelegatePointer;
		
		#endregion
		internal ScreenshotHandle AddScreenshotToLibrary( string pchFilename, string pchThumbnailFilename, int nWidth, int nHeight )
		{
			return AddScreenshotToLibraryDelegatePointer( Self, pchFilename, pchThumbnailFilename, nWidth, nHeight );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void TriggerScreenshotDelegate( IntPtr self );
		private TriggerScreenshotDelegate TriggerScreenshotDelegatePointer;
		
		#endregion
		internal void TriggerScreenshot()
		{
			TriggerScreenshotDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void HookScreenshotsDelegate( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bHook );
		private HookScreenshotsDelegate HookScreenshotsDelegatePointer;
		
		#endregion
		internal void HookScreenshots( [MarshalAs( UnmanagedType.U1 )] bool bHook )
		{
			HookScreenshotsDelegatePointer( Self, bHook );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool SetLocationDelegate( IntPtr self, ScreenshotHandle hScreenshot, string pchLocation );
		private SetLocationDelegate SetLocationDelegatePointer;
		
		#endregion
		internal bool SetLocation( ScreenshotHandle hScreenshot, string pchLocation )
		{
			return SetLocationDelegatePointer( Self, hScreenshot, pchLocation );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool TagUserDelegate( IntPtr self, ScreenshotHandle hScreenshot, SteamId steamID );
		private TagUserDelegate TagUserDelegatePointer;
		
		#endregion
		internal bool TagUser( ScreenshotHandle hScreenshot, SteamId steamID )
		{
			return TagUserDelegatePointer( Self, hScreenshot, steamID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool TagPublishedFileDelegate( IntPtr self, ScreenshotHandle hScreenshot, PublishedFileId unPublishedFileID );
		private TagPublishedFileDelegate TagPublishedFileDelegatePointer;
		
		#endregion
		internal bool TagPublishedFile( ScreenshotHandle hScreenshot, PublishedFileId unPublishedFileID )
		{
			return TagPublishedFileDelegatePointer( Self, hScreenshot, unPublishedFileID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool IsScreenshotsHookedDelegate( IntPtr self );
		private IsScreenshotsHookedDelegate IsScreenshotsHookedDelegatePointer;
		
		#endregion
		internal bool IsScreenshotsHooked()
		{
			return IsScreenshotsHookedDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate ScreenshotHandle AddVRScreenshotToLibraryDelegate( IntPtr self, VRScreenshotType eType, string pchFilename, string pchVRFilename );
		private AddVRScreenshotToLibraryDelegate AddVRScreenshotToLibraryDelegatePointer;
		
		#endregion
		internal ScreenshotHandle AddVRScreenshotToLibrary( VRScreenshotType eType, string pchFilename, string pchVRFilename )
		{
			return AddVRScreenshotToLibraryDelegatePointer( Self, eType, pchFilename, pchVRFilename );
		}
		
	}
}
