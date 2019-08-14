using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamMusic : SteamInterface
	{
		public override string InterfaceName => "STEAMMUSIC_INTERFACE_VERSION001";
		
		public override void InitInternals()
		{
			_BIsEnabled = Marshal.GetDelegateForFunctionPointer<FBIsEnabled>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 0 ) ) );
			_BIsPlaying = Marshal.GetDelegateForFunctionPointer<FBIsPlaying>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 8 ) ) );
			_GetPlaybackStatus = Marshal.GetDelegateForFunctionPointer<FGetPlaybackStatus>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 16 ) ) );
			_Play = Marshal.GetDelegateForFunctionPointer<FPlay>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 24 ) ) );
			_Pause = Marshal.GetDelegateForFunctionPointer<FPause>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 32 ) ) );
			_PlayPrevious = Marshal.GetDelegateForFunctionPointer<FPlayPrevious>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 40 ) ) );
			_PlayNext = Marshal.GetDelegateForFunctionPointer<FPlayNext>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 48 ) ) );
			_SetVolume = Marshal.GetDelegateForFunctionPointer<FSetVolume>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 56 ) ) );
			_GetVolume = Marshal.GetDelegateForFunctionPointer<FGetVolume>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 64 ) ) );
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
			_BIsEnabled = null;
			_BIsPlaying = null;
			_GetPlaybackStatus = null;
			_Play = null;
			_Pause = null;
			_PlayPrevious = null;
			_PlayNext = null;
			_SetVolume = null;
			_GetVolume = null;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsEnabled( IntPtr self );
		private FBIsEnabled _BIsEnabled;
		
		#endregion
		internal bool BIsEnabled()
		{
			var returnValue = _BIsEnabled( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsPlaying( IntPtr self );
		private FBIsPlaying _BIsPlaying;
		
		#endregion
		internal bool BIsPlaying()
		{
			var returnValue = _BIsPlaying( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate MusicStatus FGetPlaybackStatus( IntPtr self );
		private FGetPlaybackStatus _GetPlaybackStatus;
		
		#endregion
		internal MusicStatus GetPlaybackStatus()
		{
			var returnValue = _GetPlaybackStatus( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FPlay( IntPtr self );
		private FPlay _Play;
		
		#endregion
		internal void Play()
		{
			_Play( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FPause( IntPtr self );
		private FPause _Pause;
		
		#endregion
		internal void Pause()
		{
			_Pause( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FPlayPrevious( IntPtr self );
		private FPlayPrevious _PlayPrevious;
		
		#endregion
		internal void PlayPrevious()
		{
			_PlayPrevious( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FPlayNext( IntPtr self );
		private FPlayNext _PlayNext;
		
		#endregion
		internal void PlayNext()
		{
			_PlayNext( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetVolume( IntPtr self, float flVolume );
		private FSetVolume _SetVolume;
		
		#endregion
		internal void SetVolume( float flVolume )
		{
			_SetVolume( Self, flVolume );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate float FGetVolume( IntPtr self );
		private FGetVolume _GetVolume;
		
		#endregion
		internal float GetVolume()
		{
			var returnValue = _GetVolume( Self );
			return returnValue;
		}
		
	}
}
