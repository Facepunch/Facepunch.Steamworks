using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamMatchmakingServerListResponse : SteamInterface
	{
		public override IntPtr GetInterfacePointer() => GetApi.SteamMatchmakingServerListResponse();
		
		
		internal ISteamMatchmakingServerListResponse()
		{
			SetupInterface();
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServerListResponse_ServerResponded")]
		private static extern void _ServerResponded( IntPtr self, HServerListRequest hRequest, int iServer );
		
		#endregion
		internal void ServerResponded( HServerListRequest hRequest, int iServer )
		{
			_ServerResponded( Self, hRequest, iServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServerListResponse_ServerFailedToRespond")]
		private static extern void _ServerFailedToRespond( IntPtr self, HServerListRequest hRequest, int iServer );
		
		#endregion
		internal void ServerFailedToRespond( HServerListRequest hRequest, int iServer )
		{
			_ServerFailedToRespond( Self, hRequest, iServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServerListResponse_RefreshComplete")]
		private static extern void _RefreshComplete( IntPtr self, HServerListRequest hRequest, MatchMakingServerResponse response );
		
		#endregion
		internal void RefreshComplete( HServerListRequest hRequest, MatchMakingServerResponse response )
		{
			_RefreshComplete( Self, hRequest, response );
		}
		
	}
}
