
using Steamworks.Data;

namespace Steamworks.Data
{
	internal unsafe struct NetErrorMessage
	{
		public fixed char Value[1024];
	}
}