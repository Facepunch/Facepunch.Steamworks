using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks.Data
{
	/// <summary>
	/// Provides information about a DLC.
	/// </summary>
	public struct DlcInformation
	{
		/// <summary>
		/// The <see cref="Steamworks.AppId"/> of the DLC.
		/// </summary>
		public AppId AppId { get; internal set; }

		/// <summary>
		/// The name of the DLC.
		/// </summary>
		public string Name { get; internal set; }

		/// <summary>
		/// Whether or not the DLC is available.
		/// </summary>
		public bool Available { get; internal set; }
	}
}
