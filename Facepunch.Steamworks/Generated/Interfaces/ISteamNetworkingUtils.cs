using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamNetworkingUtils : SteamInterface
	{
		public override IntPtr GetInterfacePointer() => GetApi.SteamNetworkingUtils();
		
	}
}
