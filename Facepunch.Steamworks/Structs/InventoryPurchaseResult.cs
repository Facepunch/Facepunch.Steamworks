using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	public struct InventoryPurchaseResult
	{
		public Result Result;
		public ulong OrderID;
		public ulong TransID;
	}
}