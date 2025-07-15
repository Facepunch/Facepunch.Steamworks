using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks.Data
{
	/// <summary>
	/// Provides information about a beta branch.
	/// </summary>
	public struct BetaInformation
	{
		/// <summary>
		/// The name of the beta branch.
		/// </summary>
		public string Name { get; internal set; }

		/// <summary>
		/// The description of the beta branch.
		/// </summary>
		public string Description { get; internal set; }

		/// <summary>
		/// The build ID of the beta branch.
		/// </summary>
		public uint BuildId { get; internal set; }

		/// <summary>
		/// The flags indicating the status of the beta branch.
		/// </summary>
		internal BetaBranchFlags Flags { get; set; }

		/// <summary>
		/// Whether this is the default branch.
		/// </summary>
		public bool IsDefault => (Flags & BetaBranchFlags.Default) != 0;

		/// <summary>
		/// Whether this beta branch is available for selection.
		/// </summary>
		public bool IsAvailable => (Flags & BetaBranchFlags.Available) != 0;

		/// <summary>
		/// Whether this beta branch is private (requires password or invitation).
		/// </summary>
		public bool IsPrivate => (Flags & BetaBranchFlags.Private) != 0;

		/// <summary>
		/// Whether this beta branch is currently selected by the user.
		/// </summary>
		public bool IsSelected => (Flags & BetaBranchFlags.Selected) != 0;

		/// <summary>
		/// Whether this beta branch is currently installed.
		/// </summary>
		public bool IsInstalled => (Flags & BetaBranchFlags.Installed) != 0;
	}
}