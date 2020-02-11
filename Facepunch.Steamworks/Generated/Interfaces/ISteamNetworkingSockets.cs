using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamNetworkingSockets : SteamInterface
	{
		public override IntPtr GetInterfacePointer() => GetApi.SteamNetworkingSockets();
		
		
		internal ISteamNetworkingSockets()
		{
			SetupInterface();
		}
		
	}
}
