
using Steamworks.Data;

namespace Steamworks.Data
{

	internal unsafe struct SteamNetworkingErrMsg
	{
		public fixed char Value[1024];
	}
}