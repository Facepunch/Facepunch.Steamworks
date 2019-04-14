using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SteamNative;


namespace Steamworks.Internal
{
	public class ISteamMusic : BaseSteamInterface
	{
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
		public delegate bool BIsEnabledDelegate( IntPtr self );
		private BIsEnabledDelegate BIsEnabledDelegatePointer;
		
		#endregion
		public bool BIsEnabled()
		{
			return BIsEnabledDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool BIsPlayingDelegate( IntPtr self );
		private BIsPlayingDelegate BIsPlayingDelegatePointer;
		
		#endregion
		public bool BIsPlaying()
		{
			return BIsPlayingDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate AudioPlayback_Status GetPlaybackStatusDelegate( IntPtr self );
		private GetPlaybackStatusDelegate GetPlaybackStatusDelegatePointer;
		
		#endregion
		public AudioPlayback_Status GetPlaybackStatus()
		{
			return GetPlaybackStatusDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void PlayDelegate( IntPtr self );
		private PlayDelegate PlayDelegatePointer;
		
		#endregion
		public void Play()
		{
			PlayDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void PauseDelegate( IntPtr self );
		private PauseDelegate PauseDelegatePointer;
		
		#endregion
		public void Pause()
		{
			PauseDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void PlayPreviousDelegate( IntPtr self );
		private PlayPreviousDelegate PlayPreviousDelegatePointer;
		
		#endregion
		public void PlayPrevious()
		{
			PlayPreviousDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void PlayNextDelegate( IntPtr self );
		private PlayNextDelegate PlayNextDelegatePointer;
		
		#endregion
		public void PlayNext()
		{
			PlayNextDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetVolumeDelegate( IntPtr self, float flVolume );
		private SetVolumeDelegate SetVolumeDelegatePointer;
		
		#endregion
		public void SetVolume( float flVolume )
		{
			SetVolumeDelegatePointer( Self, flVolume );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate float GetVolumeDelegate( IntPtr self );
		private GetVolumeDelegate GetVolumeDelegatePointer;
		
		#endregion
		public float GetVolume()
		{
			return GetVolumeDelegatePointer( Self );
		}
		
	}
}
