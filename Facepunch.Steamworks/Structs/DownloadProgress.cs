using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks.Data
{
	/// <summary>
	/// Represents download progress.
	/// </summary>
	public struct DownloadProgress
	{
		/// <summary>
		/// Whether or not the download is currently active.
		/// </summary>
		public bool Active;

		/// <summary>
		/// How many bytes have been downloaded.
		/// </summary>
		public ulong BytesDownloaded;

		/// <summary>
		/// How many bytes in total the download is.
		/// </summary>
		public ulong BytesTotal;

		/// <summary>
		/// Gets the amount of bytes left that need to be downloaded.
		/// </summary>
		public ulong BytesRemaining => BytesTotal - BytesDownloaded;
	}
}
