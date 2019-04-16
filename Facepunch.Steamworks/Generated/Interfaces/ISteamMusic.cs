using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamMusic : SteamInterface
	{
		public ISteamMusic( bool server = false ) : base( server )
		{
		}
		
		public override string InterfaceName => "STEAMMUSIC_INTERFACE_VERSION001";
		
		public override void InitInternals()
		{
			BIsEnabledDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsEnabledDelegate>( Marshal.ReadIntPtr( VTable, 0) );
			BIsPlayingDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsPlayingDelegate>( Marshal.ReadIntPtr( VTable, 8) );
			GetPlaybackStatusDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetPlaybackStatusDelegate>( Marshal.ReadIntPtr( VTable, 16) );
			PlayDelegatePointer = Marshal.GetDelegateForFunctionPointer<PlayDelegate>( Marshal.ReadIntPtr( VTable, 24) );
			PauseDelegatePointer = Marshal.GetDelegateForFunctionPointer<PauseDelegate>( Marshal.ReadIntPtr( VTable, 32) );
			PlayPreviousDelegatePointer = Marshal.GetDelegateForFunctionPointer<PlayPreviousDelegate>( Marshal.ReadIntPtr( VTable, 40) );
			PlayNextDelegatePointer = Marshal.GetDelegateForFunctionPointer<PlayNextDelegate>( Marshal.ReadIntPtr( VTable, 48) );
			SetVolumeDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetVolumeDelegate>( Marshal.ReadIntPtr( VTable, 56) );
			GetVolumeDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetVolumeDelegate>( Marshal.ReadIntPtr( VTable, 64) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool BIsEnabledDelegate( IntPtr self );
		private BIsEnabledDelegate BIsEnabledDelegatePointer;
		
		#endregion
		internal bool BIsEnabled()
		{
			return BIsEnabledDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool BIsPlayingDelegate( IntPtr self );
		private BIsPlayingDelegate BIsPlayingDelegatePointer;
		
		#endregion
		internal bool BIsPlaying()
		{
			return BIsPlayingDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate MusicStatus GetPlaybackStatusDelegate( IntPtr self );
		private GetPlaybackStatusDelegate GetPlaybackStatusDelegatePointer;
		
		#endregion
		internal MusicStatus GetPlaybackStatus()
		{
			return GetPlaybackStatusDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void PlayDelegate( IntPtr self );
		private PlayDelegate PlayDelegatePointer;
		
		#endregion
		internal void Play()
		{
			PlayDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void PauseDelegate( IntPtr self );
		private PauseDelegate PauseDelegatePointer;
		
		#endregion
		internal void Pause()
		{
			PauseDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void PlayPreviousDelegate( IntPtr self );
		private PlayPreviousDelegate PlayPreviousDelegatePointer;
		
		#endregion
		internal void PlayPrevious()
		{
			PlayPreviousDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void PlayNextDelegate( IntPtr self );
		private PlayNextDelegate PlayNextDelegatePointer;
		
		#endregion
		internal void PlayNext()
		{
			PlayNextDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void SetVolumeDelegate( IntPtr self, float flVolume );
		private SetVolumeDelegate SetVolumeDelegatePointer;
		
		#endregion
		internal void SetVolume( float flVolume )
		{
			SetVolumeDelegatePointer( Self, flVolume );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate float GetVolumeDelegate( IntPtr self );
		private GetVolumeDelegate GetVolumeDelegatePointer;
		
		#endregion
		internal float GetVolume()
		{
			return GetVolumeDelegatePointer( Self );
		}
		
	}
}
