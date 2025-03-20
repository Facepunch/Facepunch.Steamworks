using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe partial class ISteamMatchmakingPlayersResponse : SteamInterface
	{
		internal ISteamMatchmakingPlayersResponse( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingPlayersResponse_AddPlayerToList", CallingConvention = Platform.CC)]
		private static extern void _AddPlayerToList( IntPtr self, IntPtr pchName, int nScore, float flTimePlayed );
		
		#endregion
		internal void AddPlayerToList( string pchName, int nScore, float flTimePlayed )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			_AddPlayerToList( Self, str__pchName.Pointer, nScore, flTimePlayed );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingPlayersResponse_PlayersFailedToRespond", CallingConvention = Platform.CC)]
		private static extern void _PlayersFailedToRespond( IntPtr self );
		
		#endregion
		internal void PlayersFailedToRespond()
		{
			_PlayersFailedToRespond( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingPlayersResponse_PlayersRefreshComplete", CallingConvention = Platform.CC)]
		private static extern void _PlayersRefreshComplete( IntPtr self );
		
		#endregion
		internal void PlayersRefreshComplete()
		{
			_PlayersRefreshComplete( Self );
		}
		
	}
}
