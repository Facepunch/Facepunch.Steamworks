using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks.Data
{
	public struct DlcInformation
	{
		public AppId AppId { get; internal set; }
		public string Name { get; internal set; }
		public bool Available { get; internal set; }
	}
}