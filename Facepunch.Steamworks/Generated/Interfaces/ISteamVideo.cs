using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe partial class ISteamVideo : SteamInterface
	{
		public const string Version = "STEAMVIDEO_INTERFACE_V007";
		
		internal ISteamVideo( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamVideo_v007", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamVideo_v007();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamVideo_v007();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamVideo_GetVideoURL", CallingConvention = Platform.CC)]
		private static extern void _GetVideoURL( IntPtr self, AppId unVideoAppID );
		
		#endregion
		internal void GetVideoURL( AppId unVideoAppID )
		{
			_GetVideoURL( Self, unVideoAppID );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamVideo_IsBroadcasting", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsBroadcasting( IntPtr self, ref int pnNumViewers );
		
		#endregion
		internal bool IsBroadcasting( ref int pnNumViewers )
		{
			var returnValue = _IsBroadcasting( Self, ref pnNumViewers );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamVideo_GetOPFSettings", CallingConvention = Platform.CC)]
		private static extern void _GetOPFSettings( IntPtr self, AppId unVideoAppID );
		
		#endregion
		internal void GetOPFSettings( AppId unVideoAppID )
		{
			_GetOPFSettings( Self, unVideoAppID );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamVideo_GetOPFStringForApp", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetOPFStringForApp( IntPtr self, AppId unVideoAppID, IntPtr pchBuffer, ref int pnBufferSize );
		
		#endregion
		internal bool GetOPFStringForApp( AppId unVideoAppID, out string pchBuffer )
		{
			using var mem__pchBuffer = Helpers.TakeMemory();
			int szpnBufferSize = (1024 * 32);
			var returnValue = _GetOPFStringForApp( Self, unVideoAppID, mem__pchBuffer, ref szpnBufferSize );
			pchBuffer = Helpers.MemoryToString( mem__pchBuffer );
			return returnValue;
		}
		
	}
}
