using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamVideo : SteamInterface
	{
		
		internal ISteamVideo( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamVideo_v002", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamVideo_v002();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamVideo_v002();
		
		
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
		internal bool GetOPFStringForApp( AppId unVideoAppID, out string pchBuffer, ref int pnBufferSize )
		{
			IntPtr mempchBuffer = Helpers.TakeMemory();
			var returnValue = _GetOPFStringForApp( Self, unVideoAppID, mempchBuffer, ref pnBufferSize );
			pchBuffer = Helpers.MemoryToString( mempchBuffer );
			return returnValue;
		}
		
	}
}
