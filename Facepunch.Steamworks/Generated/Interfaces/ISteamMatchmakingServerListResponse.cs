using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamMatchmakingServerListResponse : SteamInterface
	{
		
		internal ISteamMatchmakingServerListResponse( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServerListResponse_ServerResponded", CallingConvention = Platform.CC)]
		private static extern void _ServerResponded( IntPtr self, HServerListRequest hRequest, int iServer );
		
		#endregion
		internal void ServerResponded( HServerListRequest hRequest, int iServer )
		{
			_ServerResponded( Self, hRequest, iServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServerListResponse_ServerFailedToRespond", CallingConvention = Platform.CC)]
		private static extern void _ServerFailedToRespond( IntPtr self, HServerListRequest hRequest, int iServer );
		
		#endregion
		internal void ServerFailedToRespond( HServerListRequest hRequest, int iServer )
		{
			_ServerFailedToRespond( Self, hRequest, iServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServerListResponse_RefreshComplete", CallingConvention = Platform.CC)]
		private static extern void _RefreshComplete( IntPtr self, HServerListRequest hRequest, MatchMakingServerResponse response );
		
		#endregion
		internal void RefreshComplete( HServerListRequest hRequest, MatchMakingServerResponse response )
		{
			_RefreshComplete( Self, hRequest, response );
		}
		
	}
}
