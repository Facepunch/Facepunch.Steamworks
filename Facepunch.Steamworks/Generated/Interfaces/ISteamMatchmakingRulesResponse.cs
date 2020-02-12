using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamMatchmakingRulesResponse : SteamInterface
	{
		public override IntPtr GetInterfacePointer() => GetApi.SteamMatchmakingRulesResponse();
		
		
		internal ISteamMatchmakingRulesResponse()
		{
			SetupInterface();
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingRulesResponse_RulesResponded")]
		private static extern void _RulesResponded( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchRule, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValue );
		
		#endregion
		internal void RulesResponded( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchRule, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValue )
		{
			_RulesResponded( Self, pchRule, pchValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingRulesResponse_RulesFailedToRespond")]
		private static extern void _RulesFailedToRespond( IntPtr self );
		
		#endregion
		internal void RulesFailedToRespond()
		{
			_RulesFailedToRespond( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingRulesResponse_RulesRefreshComplete")]
		private static extern void _RulesRefreshComplete( IntPtr self );
		
		#endregion
		internal void RulesRefreshComplete()
		{
			_RulesRefreshComplete( Self );
		}
		
	}
}
