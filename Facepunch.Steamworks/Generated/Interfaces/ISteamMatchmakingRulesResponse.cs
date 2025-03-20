using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe partial class ISteamMatchmakingRulesResponse : SteamInterface
	{
		internal ISteamMatchmakingRulesResponse( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingRulesResponse_RulesResponded", CallingConvention = Platform.CC)]
		private static extern void _RulesResponded( IntPtr self, IntPtr pchRule, IntPtr pchValue );
		
		#endregion
		internal void RulesResponded( string pchRule, string pchValue )
		{
			using var str__pchRule = new Utf8StringToNative( pchRule );
			using var str__pchValue = new Utf8StringToNative( pchValue );
			_RulesResponded( Self, str__pchRule.Pointer, str__pchValue.Pointer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingRulesResponse_RulesFailedToRespond", CallingConvention = Platform.CC)]
		private static extern void _RulesFailedToRespond( IntPtr self );
		
		#endregion
		internal void RulesFailedToRespond()
		{
			_RulesFailedToRespond( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingRulesResponse_RulesRefreshComplete", CallingConvention = Platform.CC)]
		private static extern void _RulesRefreshComplete( IntPtr self );
		
		#endregion
		internal void RulesRefreshComplete()
		{
			_RulesRefreshComplete( Self );
		}
		
	}
}
